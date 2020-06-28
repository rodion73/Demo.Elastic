using Demo.Elastic.Weather.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Elastic.Weather.Controllers
{
    [ApiController]
    [Route("api/weather")]
    public class WeatherController : ControllerBase
    {
        [HttpGet("")]
        public WeatherModel GetWeather() =>
            new WeatherModel {
                Temperature = 9,
                Pressure = 1013,
                Humidity = 78,
                Wind = 7
            };
    }
}
