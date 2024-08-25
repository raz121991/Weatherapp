using System.Text.Json;
using WeatherReport.DTO;
using WeatherReport.Model;

namespace WeatherReport.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        public WeatherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["WeatherApi:ApiKey"];
        }

        public async Task<WeatherDto> GetWeatherAsync(string city)
        {
            if (string.IsNullOrEmpty(_apiKey))
            {
                
                throw new InvalidOperationException("Weather API key is not configured. Please check the appsettings.json file.");
            }

            var response = await _httpClient.GetStringAsync(
                $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric");

            var weatherData = JsonSerializer.Deserialize<WeatherData>(response);

            if(weatherData == null)
            {
                throw new Exception("Could not find weather data for the specified city: " + city);
            }

            var weatherDto = new WeatherDto
            {
                Name = weatherData.Name,
                Temperature = weatherData.Main.Temp,
                WeatherDescription = weatherData.Weather[0].Description,
                Humidity = weatherData.Main.Humidity,
                WindSpeed = weatherData.Wind.Speed
            };

            return weatherDto;
        }

        public async Task<WeatherDto> GetWeatherByCoordinatesAsync(double latitude, double longitude)
        {
            if (string.IsNullOrEmpty(_apiKey))
            {

                throw new InvalidOperationException("Weather API key is not configured. Please check the appsettings.json file.");
            }


            var response = await _httpClient.GetStringAsync(
                $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={_apiKey}&units=metric");

            var weatherData = JsonSerializer.Deserialize<WeatherData>(response);


            var weatherDto = new WeatherDto
            {
                Name = weatherData.Name,
                Temperature = weatherData.Main.Temp,
                WeatherDescription = weatherData.Weather[0].Description,
                Humidity = weatherData.Main.Humidity,
                WindSpeed = weatherData.Wind.Speed
            };

            return weatherDto;
        }
    }
}
