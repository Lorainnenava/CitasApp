using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using PortalAdmin;
using PortalAdmin.Components;
using PortalAdmin.Services.Http;
using PortalAdmin.Services.Interfaces;
using System.Text.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Detectar entorno y elegir el archivo de configuración correspondiente
var environment = builder.HostEnvironment.Environment;
var settingsFile = environment == "Development"
    ? "appsettings.Development.json"
    : "appsettings.Production.json";

// HttpClient temporal solo para leer configuración
using var configClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
var json = await (await configClient.GetAsync(settingsFile)).Content.ReadAsStringAsync();
var apiBaseUrl = JsonDocument.Parse(json).RootElement
    .GetProperty("ApiSettings")
    .GetProperty("BaseUrl")
    .GetString();

// HttpClient principal para tu app
builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(apiBaseUrl!) });

builder.Services.AddScoped<IHttpAdapter, HttpAdapterClient>();

builder.Services.AddMudServices();
builder.Services.AddScoped<ToastService>();

await builder.Build().RunAsync();
