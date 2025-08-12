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




// JSon verilerini �ekerken property isimlerinin ilk harflerini lowercase yap�yor, bunu disable etmek i�in:
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Varsay�lan k���k harfle ba�lamay� kapat�r
    });

// Yetkisiz eri�im i�in �zel bir sayfa eklemek i�in
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Home/UnAuthorized"; // Giri� yap�lmam��sa buraya y�nlendirme yapar (Login ekran�na gitmesini istemedik)
    options.AccessDeniedPath = "/Home/UnAuthorized"; // Yetkisiz eri�imlerde y�nlendirme yap�lacak sayfa
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // 500 Hatalar� i�in
    app.UseStatusCodePagesWithReExecute("/Home/Error", "?code={0}"); // 404 dahil t�m durum kodlar� i�in
}



app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Area'lar� etkinle�tirmek i�in
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

// 404 sayfas�n� URL de�i�tirmeden g�stermek i�in
app.UseStatusCodePagesWithReExecute("/Home/NotFound");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
