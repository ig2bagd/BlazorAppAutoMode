using System.ComponentModel.DataAnnotations;

namespace BlazorAppAutoMode;

public class AppOptions
{
    public const string ConfigurationSection = "MySection";

    [Required, Url]
    public string BaseAddress { get; set; } = string.Empty;
}
