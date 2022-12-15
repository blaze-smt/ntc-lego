global using Blazored.LocalStorage;

global using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using NTC_Lego.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Uses CustomAuthStateProvider when authenticating user roles
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();
// Adds local storage for tracking JWT during site navigation
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<CurrentPage>();
builder.Services.AddScoped<Search>();


await builder.Build().RunAsync();
