using MusicPortalWebApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Context>(options => options.UseSqlServer(connection));
builder.Services.AddControllers();

var app = builder.Build();

app.UseStaticFiles();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();