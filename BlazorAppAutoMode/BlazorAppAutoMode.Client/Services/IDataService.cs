using BlazorAppAutoMode.Client.Models;

namespace BlazorAppAutoMode.Client.Services;

public interface IDataService
{
    Task<OpenMeteoResult?> GetData();
}
