using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Text;
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
    builder.Services.AddScoped(sp => new AuthenticationOptions
    {
        AppId = "7b489440-7c64-49f4-ba28-8c4d03b44d9e"
    });
}
else
{
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:7176/") });
    builder.Services.AddScoped(sp => new AuthenticationOptions
    {
        AppId = "e936bb96-7260-4c11-a827-59c50958d3be"
    });
}

builder.Services.AddMsalAuthentication(options =>
{
    options.ProviderOptions.DefaultAccessTokenScopes.Add("openid");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("offline_access");
    options.ProviderOptions.LoginMode = "redirect";
    
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
});

builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("ContentEditor", policy =>
        policy.RequireClaim("jobTitle",
            "Editor", "editor"));
});

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IFirebaseInterop, FirebaseInterop>();

await builder.Build().RunAsync();
