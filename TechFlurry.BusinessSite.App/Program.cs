using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TechFlurry.BusinessSite.App;
using TechFlurry.BusinessSite.App.Authentication;
using TechFlurry.BusinessSite.App.Interops;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#wrapper");
builder.RootComponents.Add<HeadOutlet>("head::after");

var hostEnv = builder.HostEnvironment;

if (!hostEnv.IsDevelopment())
{
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://techflurryapi.azurewebsites.net/") });
}
else
{
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:7176/") });
}
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IFirebaseInterop, FirebaseInterop>();

await builder.Build().RunAsync();
