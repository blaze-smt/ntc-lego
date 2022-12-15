using BricklinkSharp.Client;

using Microsoft.EntityFrameworkCore;

using NTC_Lego.Server;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Add database connection string, should be specified by environment with unique appsettings.json files for each
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().WriteTo.File("logs/log.txt"));

// BrickLink API Setup
BricklinkClientConfiguration.Instance.TokenValue = builder.Configuration.GetSection("AppSetting:BricklinkTokenValue").Value;
BricklinkClientConfiguration.Instance.TokenSecret = builder.Configuration.GetSection("AppSetting:BricklinkTokenSecrete").Value;
BricklinkClientConfiguration.Instance.ConsumerKey = builder.Configuration.GetSection("AppSetting:BricklinkConsumerKey").Value;
BricklinkClientConfiguration.Instance.ConsumerSecret = builder.Configuration.GetSection("AppSetting:BricklinkConsumerSecret").Value;

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

/* 
 * Console Logging to track which enviroment is active and which connection string is being used
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
*/

app.Run();
