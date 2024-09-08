using BlazorAppAutoMode;
using BlazorAppAutoMode.Client.Services;
using BlazorAppAutoMode.Components;
using BlazorAppAutoMode.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

// Create Blazor project with Interactive Auto render mode in .NET 8 (Authorised Territory)
// https://www.youtube.com/watch?v=T0Wt6PqmQPk

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

//builder.Services.Configure<AppOptions>(builder.Configuration.GetSection(AppOptions.ConfigurationSection));

builder.Services.AddOptions<AppOptions>()
    .BindConfiguration(AppOptions.ConfigurationSection)
    .ValidateDataAnnotations()
    .ValidateOnStart();

// Registration and configuration for a typed client must be done both in server and client projects
var baseAddress = builder.Configuration["BaseAddress"];
//builder.Configuration.GetValue<string>("BaseAddress", "default_value") allows you to specify a default value that will be returned if the key is not found in the configuration.
// The RIGHT Way To Use HttpClient In .NET
// https://www.youtube.com/watch?v=g-JGay_lnWI
builder.Services.AddHttpClient<IDataService, DataService>((serviceProvider, httpClient) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<AppOptions>>().Value;

    //https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-8.0
    //httpClient.BaseAddress = new Uri("https://localhost:7037");
    //httpClient.BaseAddress = new Uri(baseAddress!);

    httpClient.BaseAddress = new Uri(settings.BaseAddress);
})
.ConfigurePrimaryHttpMessageHandler(() =>                   // Use transient typed client in Singleton service
{
    return new SocketsHttpHandler
    {
        PooledConnectionLifetime = TimeSpan.FromMinutes(5)
    };
})
.SetHandlerLifetime(Timeout.InfiniteTimeSpan);


builder.Services.AddHttpClient<IOpenMeteoService, OpenMeteoService>((HttpClient client) =>
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
