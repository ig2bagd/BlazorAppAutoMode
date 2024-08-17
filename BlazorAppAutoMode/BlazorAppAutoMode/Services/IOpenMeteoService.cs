using BlazorAppAutoMode.Client.Models;

namespace BlazorAppAutoMode.Services;

public interface IOpenMeteoService
{
    Task<OpenMeteoResult?> GetWeather();
}
