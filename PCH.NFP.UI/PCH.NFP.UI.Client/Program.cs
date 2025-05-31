using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using PCH.NFP.UI.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();

await builder.Build().RunAsync();
