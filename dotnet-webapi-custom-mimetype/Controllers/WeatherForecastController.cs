using System.Text.Json;
using dotnet_webapi_custom_mimetype.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace dotnet_webapi_custom_mimetype;

[ApiController]
[Route("weather/forecast")]
public class WeatherForecastController : ControllerBase
{
  // JSON response serialization is automatically detected from the custom mime type. Remove +json in any above to test.
  [HttpGet(Name = "GetWeatherForecast")]
  [Produces(WeatherForecastFormat.Default, WeatherForecastFormat.DetailedV1, WeatherForecastFormat.DetailedV2)]
  [SwaggerResponseExample(200, typeof(WeatherForecastExamples))]
  public IEnumerable<WeatherForecast> Get() => Request.Headers.Accept.First() switch
  {
      WeatherForecastFormat.DetailedV1 => new GetWeatherForecastService().GetDetailedV1(),
      WeatherForecastFormat.DetailedV2 => new GetWeatherForecastService().GetDetailedV2(),
      _ => new GetWeatherForecastService().GetDefault(),
  };

  [HttpPost(Name = "PostWeatherForecast")]
  [Consumes(WeatherUpdateFormat.Simple, WeatherUpdateFormat.Attributed)]
  [SwaggerRequestExample(typeof(string), typeof(WeatherUpdateExamples))]
  public IActionResult Post(dynamic update)
  {
    var service = new UpdateWeatherForecastService();

    var response = Request.ContentType switch
    {
        WeatherUpdateFormat.Attributed => service.SaveUpdateV2(
                                                               JsonSerializer.Deserialize<WeatherUpdate>(
                                                                update.ToString())),
        _ => service.SaveUpdateV1(update.ToString()),
    };

    if (response) return Ok();
    return Problem();
  }
}

class WeatherForecastFormat
{
  public const string Default = "application/vnd.company.weather+json;version=1";
  public const string DetailedV1 = "application/vnd.company.weather.detailed+json;version=1";
  public const string DetailedV2 = "application/vnd.company.weather.detailed+json;version=2";
}

public class WeatherForecastExamples : IMultipleExamplesProvider<WeatherForecast>
{
  public IEnumerable<SwaggerExample<WeatherForecast>> GetExamples()
  {
    yield return SwaggerExample.Create(
                                       WeatherForecastFormat.Default,
                                       new WeatherForecast
                                       {
                                           Date = DateOnly.FromDateTime(DateTime.Now),
                                           TemperatureC = Random.Shared.Next(-20, 55),
                                           Summary = WeatherForecast.Summaries[0],
                                           Details = null,
                                       });

    yield return SwaggerExample.Create(
                                       WeatherForecastFormat.DetailedV1,
                                       new WeatherForecast
                                       {
                                           Date = DateOnly.FromDateTime(DateTime.Now),
                                           TemperatureC = Random.Shared.Next(-20, 55),
                                           Summary = WeatherForecast.Summaries[0],
                                           Details = new ForecastDetailsV1 { Extra = "SOME EXTRA INFO" }
                                       });

    yield return SwaggerExample.Create(
                                       WeatherForecastFormat.DetailedV2,
                                       new WeatherForecast
                                       {
                                           Date = DateOnly.FromDateTime(DateTime.Now),
                                           TemperatureC = Random.Shared.Next(-20, 55),
                                           Summary = WeatherForecast.Summaries[0],
                                           Details = new ForecastDetailsV2 { More = "SOME MORE INFO" }
                                       });
  }
}

class WeatherUpdateFormat
{
  public const string Simple = "application/vnd.company.weather-update+json;version=1";
  public const string Attributed = "application/vnd.company.weather-update+json;version=2";
}

public class WeatherUpdateExamples : IMultipleExamplesProvider<string>
{
  public IEnumerable<SwaggerExample<string>> GetExamples()
  {
    yield return SwaggerExample.Create(
                                       WeatherUpdateFormat.Simple,
                                       "SOME UPDATE");

    yield return SwaggerExample.Create(
                                       WeatherUpdateFormat.Attributed,
                                       JsonSerializer.Serialize(
                                                                new WeatherUpdate
                                                                {
                                                                    Update = "SOME AUTHOR UPDATE",
                                                                    Author = "SOME AUTHOR"
                                                                }));
  }
}

public class WeatherUpdate
{
  public string Update { get; set; }
  public string Author { get; set; }
};