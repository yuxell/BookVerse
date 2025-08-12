using BookWebApp.Business.Utilities;
using BookWebApp.Data;
using BookWebApp.Models;
using BookWebApp.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace BookWebApp.Repositories.Concrete
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {

        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _tables;

        protected BaseRepository(AppDbContext context)
        {
            _context = context;
            _tables = _context.Set<TEntity>();
        }
        public async Task<Result> AddAsync(TEntity entity)
        {
            await _tables.AddAsync(entity);
            var result = await _context.SaveChangesAsync() > 0;
            if (result)
            {
                // yeni kaydın ID'sini almak için
                var idProperty = entity.GetType().GetProperty("Id")
                      ?? entity.GetType().GetProperty(entity.GetType().Name + "ID");
                if (idProperty != null)
                {
                    var idValue = idProperty.GetValue(entity);
                    return Result.Ok("Ekleme başarılı", int.Parse(idValue.ToString())); // yeni kaydın id'sini result ile gönderiyoruz
                }   
            }
            
            return Result.Fail("Ekleme başarısız!");


        }

        public async Task<Result> DeleteAsync(int id)
        {
            var entity = await GetByIDAsync(id);
            _tables.Remove(entity);
            return await _context.SaveChangesAsync() > 0 ? Result.Ok("Kayıt silindi!") : Result.Fail("Silme işlemi başarısız oldu!");
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _tables.ToListAsync();
        }

        public async Task<TEntity> GetByIDAsync(int id)
        {
            return await _tables.FindAsync(id);
        }

        public async Task<Result> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _tables.Update(entity);

            return await _context.SaveChangesAsync() > 0 
                ? Result.Ok("Güncelleme başarılı!") 
                : Result.Fail("Güncelleme başarısız!");
        }

        public async Task<IEnumerable<TResult>> FilterAllAsync<TResult>(
            Expression<Func<TEntity, TResult>> select,
            Expression<Func<TEntity, bool>> where,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int take = 2)
        {
            IQueryable<TEntity> query = _tables.AsNoTracking();

            if (where != null)
                query = query.Where(where);
            if (include != null)
                query = include(query);


            if (orderBy != null)
                query = orderBy(query);

            if (take != 0 && orderBy != null)
                query = query.Take(take);

            
                return await query.Select(select).ToListAsync();
        }
    }
}
