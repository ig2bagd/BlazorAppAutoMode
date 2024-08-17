using BlazorAppAutoMode.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddHttpClient<IDataService, DataService>("api", (HttpClient client) =>
{
    client.BaseAddress = new Uri("https://localhost:7037");
});

await builder.Build().RunAsync();
