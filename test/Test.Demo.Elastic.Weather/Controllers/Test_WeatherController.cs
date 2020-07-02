using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq.AutoMock;
using Moq;
using Xunit;
using Microsoft.Extensions.FileProviders;
using System.Text;
using System.IO;
using NSwag;
using FkThat.Sso.Models;

namespace Demo.Elastic.Weather.Controllers
{
    public class Test_WeatherController
    {
        [Fact]
        public void GetWeather_WithNonexistentWeatherFile_ShouldThrow()
        {
            var mocker = new AutoMocker();

            var fileInfoMock = new Mock<IFileInfo>();

            mocker.GetMock<IFileProvider>().Setup(m => m.GetFileInfo("weather.json"))
                .Returns(fileInfoMock.Object);

            fileInfoMock.Setup(m => m.Exists).Returns(false);

            var sut = mocker.CreateInstance<WeatherController>();
            sut.Invoking(s => s.GetWeather()).Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public async Task GetWeather_ShouldReturnWeather()
        {
            var weatherJson =
                "[                                           " +
                "  {                                         " +
                "    \"date\": \"2018-05-06T00:00:00.000Z\", " +
                "    \"temperatureC\": 1,                    " +
                "    \"summary\": \"Freezing\"               " +
                "  },                                        " +
                "  {                                         " +
                "    \"date\": \"2018-05-07T00:00:00.000Z\", " +
                "    \"temperatureC\": 14,                   " +
                "    \"summary\": \"Bracing\"                " +
                "  }                                         " +
                "]                                           ";

            var memStream = new MemoryStream(Encoding.UTF8.GetBytes(weatherJson));

            var mocker = new AutoMocker();

            var fileInfoMock = new Mock<IFileInfo>();

            mocker.GetMock<IFileProvider>().Setup(m => m.GetFileInfo("weather.json"))
                .Returns(fileInfoMock.Object);

            fileInfoMock.Setup(m => m.Exists).Returns(true);
            fileInfoMock.Setup(m => m.CreateReadStream()).Returns(memStream);

            var sut = mocker.CreateInstance<WeatherController>();
            var result = await sut.GetWeather();

            result.Should().BeEquivalentTo(new[] {
                new WeatherModel {
                    Date = new DateTimeOffset(2018, 05, 06, 0, 0, 0, TimeSpan.Zero),
                    TemperatureC = 1,
                    Summary = "Freezing"
                },
                new WeatherModel {
                    Date = new DateTimeOffset(2018, 05, 07, 0, 0, 0, TimeSpan.Zero),
                    TemperatureC = 14,
                    Summary = "Bracing"
                },
            });
        }
    }
}
