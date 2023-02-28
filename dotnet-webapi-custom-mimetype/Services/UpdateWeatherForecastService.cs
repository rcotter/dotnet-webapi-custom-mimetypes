namespace dotnet_webapi_custom_mimetype.Services;

public class UpdateWeatherForecastService
{
  public bool SaveUpdateV1(string? update)
  {
    Console.WriteLine($"V1 update: {update}");
    return true;
  }

  public bool SaveUpdateV2(WeatherUpdate update)
  {
    Console.WriteLine($"V2 update: {update.Update} by {update.Author}");
    return true;
  }
}