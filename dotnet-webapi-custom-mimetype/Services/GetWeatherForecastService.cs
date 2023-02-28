namespace dotnet_webapi_custom_mimetype.Services;

public class GetWeatherForecastService
{
  public IEnumerable<WeatherForecast> GetDefault() => GetResponse(details: null);

  public IEnumerable<WeatherForecast> GetDetailedV1() =>
      GetResponse(details: new ForecastDetailsV1 { Extra = "SOME EXTRA INFO" });

  public IEnumerable<WeatherForecast> GetDetailedV2() =>
      GetResponse(details: new ForecastDetailsV2 { More = "SOME MORE INFO" });

  private IEnumerable<WeatherForecast> GetResponse(dynamic details) => Enumerable.Range(1, 5)
     .Select(
             index => new WeatherForecast
             {
                 Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                 TemperatureC = Random.Shared.Next(-20, 55),
                 Summary = WeatherForecast.Summaries[Random.Shared.Next(WeatherForecast.Summaries.Length)],
                 Details = details,
             })
     .ToArray();
}