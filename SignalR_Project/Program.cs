using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SignalR_Project.Data;
using SignalR_Project.Hubs;
using SignalR_Project.Models.Entities;
using SignalR_Project.Models.Services.Interface;
using SignalR_Project.Models.Services.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mvcBuilder = builder.Services.AddControllersWithViews();

#if DEBUG
mvcBuilder.AddRazorRuntimeCompilation();
#endif
builder.Services.AddSignalR();
builder.Services.AddDbContext<DataBaseContext>(o => o.UseSqlServer(DataBase.ConnectionString()));
builder.Services.AddScoped<IChatRoomService, RChatRoomService>();
builder.Services.AddScoped<IMessageService, RMessageService>();
builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(o =>
{
    o.LoginPath = "/Home/login";
})
;
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapHub<SiteChatHub>("/chathub");
app.MapHub<SupportHub>("/supporthub");
app.Run();
