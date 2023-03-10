using CI_plateform.Models.Models;
using CI_plateform.Repository.Repository;
using CI_plateform.Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CiplateformContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("dbConnectionStrings")));

builder.Services.AddScoped<UserInterface,UserRepository>();
builder.Services.AddScoped<PlateformInterface, PlateformRepository>();
builder.Services.AddSession();
builder.Services.AddMemoryCache();
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

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=login}/{id?}");

app.Run();
