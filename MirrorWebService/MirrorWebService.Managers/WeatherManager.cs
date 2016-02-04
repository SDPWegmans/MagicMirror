using MagicMirror.DTO.Weather;
using MagicMirror.Web;
using System;
using System.Threading.Tasks;

namespace MirrorWebService.Managers
{
    public class WeatherManager
    {
        public WeatherResponse GetData()
        {
            var result = Task.Run(() => GetWeatherResponseAsync()).Result;
            return result;
        }
        public async Task<WeatherResponse> GetWeatherResponseAsync()
        {
            var weatherServiceManager =
               new ServiceManager(new Uri("https://api.forecast.io/forecast/bfca1ae07ffdf0931899e6d17c7b2875/43.1117230,-77.4087580"));

            var weatherResponse = await weatherServiceManager.CallService<WeatherResponse>();
            var currentWeather = weatherResponse.currently;
            if (weatherResponse == null)
            {
                currentWeather.summary = "Love";
                currentWeather.temperature = 69;
                currentWeather.icon = "lobster";
            }

            return weatherResponse;
        }
    }
}
