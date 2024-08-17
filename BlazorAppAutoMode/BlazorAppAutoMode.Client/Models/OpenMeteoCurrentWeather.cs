namespace BlazorAppAutoMode.Client.Models;

public class OpenMeteoCurrentWeather
{
    public decimal temperature { get; set; }
    public decimal windspeed { get; set; }
    public decimal winddirection { get; set; }
    public int weathercode { get; set; }
    public int is_day { get; set; }
    public string time { get; set; } = "";
}
