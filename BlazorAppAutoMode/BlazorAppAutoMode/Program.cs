using BlazorAppAutoMode.Client.Pages;
using BlazorAppAutoMode.Client.Services;
using BlazorAppAutoMode.Components;
using BlazorAppAutoMode.Services;

// Create Blazor project with Interactive Auto render mode in .NET 8 (Authorised Territory)
// https://www.youtube.com/watch?v=T0Wt6PqmQPk

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Registration and configuration for a typed client must be done both in server and client projects
var baseAddress = builder.Configuration["BaseAddress"];
//builder.Configuration.GetValue<string>("BaseAddress", "default_value") allows you to specify a default value that will be returned if the key is not found in the configuration.
builder.Services.AddHttpClient<IDataService, DataService>("api", (HttpClient client) =>
{
    //https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-8.0
    //client.BaseAddress = new Uri("https://localhost:7037");
    client.BaseAddress = new Uri(baseAddress!);
});

builder.Services.AddHttpClient<IOpenMeteoService, OpenMeteoService>("open-meteo", (HttpClient client) =>
{
    client.BaseAddress = new Uri("https://api.open-meteo.com/v1/forecast");
});

builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();           // for Swagger with minimal APIs               

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorAppAutoMode.Client._Imports).Assembly);

app.Run();
