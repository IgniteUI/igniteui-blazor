using IgniteBlazorAppIGB_NS_SEGMENT.Models;

namespace IgniteBlazorAppIGB_NS_SEGMENT.Services;

public class WeatherForecastService
{
    private static readonly string[] Summaries =
        ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

    public async Task<WeatherForecast[]> GetForecastsAsync(DateOnly start, int count = 50)
    {
        // Simulate asynchronous loading to demonstrate streaming rendering
        await Task.Delay(500);

        var forecasts = Enumerable.Range(1, count).Select(i => new WeatherForecast
        {
            Date = start.AddDays(i),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)],
        }).ToArray();
        return forecasts;
    }
}
