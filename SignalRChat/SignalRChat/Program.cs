using Microsoft.EntityFrameworkCore;
using SignalRChat.Model;
using SignalRChat;

var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ChatDBContext>(options => options.UseSqlServer(connection));
builder.Services.AddSignalR();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapHub<ChatHub>("/chat");

app.Run();