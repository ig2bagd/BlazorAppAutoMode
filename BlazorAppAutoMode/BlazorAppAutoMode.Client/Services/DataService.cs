using BlazorAppAutoMode.Client.Models;
using System.Net.Http.Json;

namespace BlazorAppAutoMode.Client.Services;

public class DataService(HttpClient httpClient) : IDataService
{
    private readonly HttpClient _httpClient = httpClient;
    public async Task<OpenMeteoResult?> GetData()
    {
        //var apiResult = await _httpClient.GetAsync("api/gwd");
        //return apiResult.IsSuccessStatusCode ? await apiResult.Content.ReadFromJsonAsync<OpenMeteoResult>() : null;
        return await _httpClient.GetFromJsonAsync<OpenMeteoResult>("api/gwd");
    }
}
