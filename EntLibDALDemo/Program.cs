using System.Data.Common;
using EntLibDALDemo.Models;
using EntLibDALDemo.Util;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MySql.Data.MySqlClient;
using Npgsql;
using Org.BouncyCastle.Crypto;

var builder = WebApplication.CreateBuilder( args );

IConfiguration configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;


// Add services to the container.
builder.Services.Configure<DatabaseProviders>( configuration.GetSection( DatabaseProviders.SectionName ) );

builder.Services.AddSingleton<DbFactory>();
builder.Services.AddScoped<DbHelper>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if ( !app.Environment.IsDevelopment() )
{
    app.UseExceptionHandler( "/Home/Error" );
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}" );

app.Run();
