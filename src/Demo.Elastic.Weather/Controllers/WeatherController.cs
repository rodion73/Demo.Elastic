using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using FkThat.Sso.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace Demo.Elastic.Weather.Controllers
{
    [ApiController]
    [Route("api/weather")]
    public class WeatherController : ControllerBase
    {
        private static readonly JsonSerializerOptions _jsonOptions =
            new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

        private readonly IFileProvider _fileProvider;

        public WeatherController(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        [HttpGet("")]
        public async Task<IEnumerable<WeatherModel>> GetWeather()
        {
            var fileInfo = _fileProvider.GetFileInfo("weather.json");

            if (!fileInfo.Exists)
            {
                throw new InvalidOperationException("File doesn't exists.");
            }

            using var stream = fileInfo.CreateReadStream();

            return await JsonSerializer
                .DeserializeAsync<IEnumerable<WeatherModel>>(stream, _jsonOptions)
                .ConfigureAwait(false);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style",
            "IDE0060:Remove unused parameter", Justification = "Demo method.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage",
            "CA1801:Review unused parameters", Justification = "Demo method.")]
        [HttpPost("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public void AddWeather([FromBody] WeatherModel model)
        {
        }

        [HttpGet("500")]
        [ProducesResponseType(500)]
        public void GetError500()
        {
            throw new InvalidOperationException("This is a demo exception.");
        }
    }
}
