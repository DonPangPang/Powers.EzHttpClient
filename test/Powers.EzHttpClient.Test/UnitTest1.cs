using Powers.EzHttpClient.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace Powers.EzHttpClient.Test
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            var res = await HttpClientWrapper
                .Create()
                .Url("http://localhost:5299/WeatherForecast")
                .GetAsync<IEnumerable<WeatherForecast>>();
            Assert.NotNull(res);
            Assert.True(res.Count() == 5);
        }
    }

    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}