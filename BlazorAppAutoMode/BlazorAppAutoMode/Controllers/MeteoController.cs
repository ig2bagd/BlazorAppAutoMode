using BlazorAppAutoMode.Client.Models;
using BlazorAppAutoMode.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAppAutoMode.Controllers;

public class MeteoController(IOpenMeteoService openMeteoService) : Controller
{
    private readonly IOpenMeteoService _openMeteoService = openMeteoService;

    [HttpGet]
    [Route("api/gwd")]
    public async Task<OpenMeteoResult?> GetWeatherData()
    {
        return await _openMeteoService.GetWeather();
    }
}
