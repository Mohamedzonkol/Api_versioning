using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Apiversioning.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        [SwaggerOperation(
            Summary = "Retrieves a collection of weather forecasts.",
            Description = "Returns a collection of weather forecasts.",
            OperationId = "GetWeatherForecast",
            Tags = new[] { "WeatherForecast" }
        )]
        [SwaggerResponse(200, "Success", typeof(IEnumerable<WeatherForecast>))]
        [MapToApiVersion("1.0")]
        //[ResponseCache(Duration = 50)]
        [ResponseCache(CacheProfileName = "Default")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Retrieves a collection of some values.",
            Description = "Returns a collection of some values.",
            OperationId = "GetSomeValue",
            Tags = new[] { "WeatherForecast" }
        )]
        [SwaggerResponse(200, "Success", typeof(IEnumerable<string>))]
        [MapToApiVersion("2.0")]
        public IEnumerable<string> GetSomeValue()
        {
            return new List<string> { "value1", "value2" };
        }
    }
}
