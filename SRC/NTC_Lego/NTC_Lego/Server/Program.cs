using BricklinkSharp.Client;

using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using NTC_Lego.Server;
using NTC_Lego.Server.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().WriteTo.File("logs/log.txt"));

BricklinkClientConfiguration.Instance.TokenValue = "D062DAB7AB694D8A80F6BD4369604125";
BricklinkClientConfiguration.Instance.TokenSecret = "9756A009A7294AE7AA59BC53E59AA5E1";
BricklinkClientConfiguration.Instance.ConsumerKey = "FC2DB0355B8D49B590B8C7B74F351817";
BricklinkClientConfiguration.Instance.ConsumerSecret = "0998A310B2794F65A02EBC4714AA02DC";

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

try
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();
        Console.WriteLine($"\n*** Environment Name: {app.Environment.EnvironmentName}\n");
        Console.WriteLine($"\n*** Connection String: {context.Database.GetDbConnection().ConnectionString}\n");
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

app.Run();
