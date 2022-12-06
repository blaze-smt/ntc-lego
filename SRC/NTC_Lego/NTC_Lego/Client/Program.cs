global using Blazored.LocalStorage;

global using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using NTC_Lego.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Uses CustomAuthStateProvider when authenticating user roles (www.youtube.com/watch?v=Yh16E2u2pio)
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();

/*builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    // options are set here
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
*/
// Adds local storage for tracking JWT during site navigation

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<CurrentPage>();

await builder.Build().RunAsync();
