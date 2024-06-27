using Microsoft.EntityFrameworkCore;
using MusicPortal.BLL;
using MusicPortal.BLL.Services;
using MusicPortal.Services;

var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddContext(connection!);
builder.Services.AddSaveUnitService();

builder.Services.AddScoped<ILangService, LanguageService>();
builder.Services.AddScoped<ICryptographyService, CryptographyService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISongService, SongService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IPerformerService, PerformerService>();

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.Name = "Session";
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession(); // Не забываем использовать сессии
app.MapControllerRoute(name: "default", pattern: "{controller=Music}/{action=Index}/{id?}");

app.Run();