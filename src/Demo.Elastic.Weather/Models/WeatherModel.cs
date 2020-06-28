using System;

namespace FkThat.Sso.Models
{
    public class WeatherModel
    {
        public DateTimeOffset Date { get; set; }

        public int TemperatureC { get; set; }

        public string? Summary { get; set; }
    }
}
