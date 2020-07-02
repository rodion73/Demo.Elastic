using System;
using System.ComponentModel.DataAnnotations;

namespace FkThat.Sso.Models
{
    public class WeatherModel
    {
        [Range(typeof(DateTimeOffset), "01/01/1900", "12/31/2100",
            ConvertValueInInvariantCulture = true)]
        public DateTimeOffset Date { get; set; }

        [Range(-50, 60)]
        public int TemperatureC { get; set; }

        [Required]
        public string? Summary { get; set; }
    }
}
