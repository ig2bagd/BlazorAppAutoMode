using BlazorAppAutoMode.Client.Models;

namespace BlazorAppAutoMode.Services;

public class OpenMeteoService(HttpClient httpClient) : IOpenMeteoService
{
    private readonly HttpClient _httpClient = httpClient;
    public async Task<OpenMeteoResult?> GetWeather()
    {
        return await _httpClient.GetFromJsonAsync<OpenMeteoResult>("?latitude=51.51&longitude=-0.13&current_weather=true&timezone=GMT");        
    }
}
