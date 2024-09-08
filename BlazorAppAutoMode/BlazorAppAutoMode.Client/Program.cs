using BlazorAppAutoMode.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddHttpClient<IDataService, DataService>((HttpClient client) =>
{
    //https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/configuration?view=aspnetcore-8.0#app-settings-configuration
    //client.BaseAddress = new Uri("https://localhost:7037");
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

await builder.Build().RunAsync();
