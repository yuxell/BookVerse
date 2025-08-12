using BookWebApp.Data;
using BookWebApp.Manager.Abstract;
using BookWebApp.Manager.Concrete;
using BookWebApp.Models;
using BookWebApp.Repositories.Abstract;
using BookWebApp.Repositories.Concrete;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


string strConn = builder.Configuration.GetConnectionString("ConnStr");
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(strConn));

builder.Services
    .AddIdentity<AppUser, AppRole>(x => x.SignIn.RequireConfirmedEmail = false)
    .AddEntityFrameworkStores<AppDbContext>()
    .AddRoles<AppRole>();

//builder.Services.AddTransient<IBookManager, BookManager>();
//builder.Services.AddScoped<IBookManager>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookManager, BookManager>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryManager, CategoryManager>();

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IAuthorManager, AuthorManager>();

builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
builder.Services.AddScoped<IPublisherManager, PublisherManager>();

builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewManager, ReviewManager>();




// JSon verilerini çekerken property isimlerinin ilk harflerini lowercase yapýyor, bunu disable etmek için:
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Varsayýlan küçük harfle baþlamayý kapatýr
    });

// Yetkisiz eriþim için özel bir sayfa eklemek için
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Home/UnAuthorized"; // Giriþ yapýlmamýþsa buraya yönlendirme yapar (Login ekranýna gitmesini istemedik)
    options.AccessDeniedPath = "/Home/UnAuthorized"; // Yetkisiz eriþimlerde yönlendirme yapýlacak sayfa
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // 500 Hatalarý için
    app.UseStatusCodePagesWithReExecute("/Home/Error", "?code={0}"); // 404 dahil tüm durum kodlarý için
}



app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Area'larý etkinleþtirmek için
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

// 404 sayfasýný URL deðiþtirmeden göstermek için
app.UseStatusCodePagesWithReExecute("/Home/NotFound");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
