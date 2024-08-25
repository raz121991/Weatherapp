using Microsoft.AspNetCore.Mvc;
using WeatherReport.Services;

namespace WeatherReport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> GetWeather(string city)
        {
            try
            {
                var weatherDto = await _weatherService.GetWeatherAsync(city);
                return Ok(weatherDto);
            }
            catch(Exception e)
            {
                //loggin the error but for the demo we will just return it
                return BadRequest(e.Message);
            }
           
        }

        [HttpGet("coordinates")]
        public async Task<IActionResult> GetWeatherByCoordinates([FromQuery] double lat, [FromQuery] double lon)
        {
            try
            {
                var weatherDto = await _weatherService.GetWeatherByCoordinatesAsync(lat, lon);
                return Ok(weatherDto);
            }
            catch(Exception e)
            {
                //loggin the error but for the demo we will just return it
                return BadRequest(e.Message);
            }
            
        }
    }
}
