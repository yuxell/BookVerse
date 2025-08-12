using BookWebApp.Business.Utilities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace BookWebApp.Repositories.Abstract
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync(); // Hata almaz ise boş liste gelir
        Task<TEntity> GetByIDAsync(int id); // Geri dönen değer null ise bulunamadı
        Task<Result> AddAsync(TEntity entity);
        Task<Result> UpdateAsync(TEntity entity);
        Task<Result> DeleteAsync(int id);

        public Task<IEnumerable<TResult>> FilterAllAsync<TResult>(
            Expression<Func<TEntity, TResult>> select,
            Expression<Func<TEntity, bool>> where,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int take = 0);
    }
}
