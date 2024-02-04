using BootstrapBlazor.Components;
using HealthcareMonitoring.Client;
using HealthcareMonitoring.Client.Services;
using HealthcareMonitoring.Shared.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBootstrapBlazor(configureOptions: options =>
{
    options.IgnoreLocalizerMissing = true;
    options.DefaultCultureInfo = "en-US";
});

builder.Services.AddScoped<IAppContext, DefaultAppContext>();

builder.Services.AddScoped<DoctorService>();


builder.Services.AddHttpClient("HealthcareMonitoring.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("HealthcareMonitoring.ServerAPI"));

builder.Services.AddApiAuthorization();

builder.Services.AddRadzenComponents();

builder.Services.AddScoped<Radzen.DialogService>();

await builder.Build().RunAsync();
