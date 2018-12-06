using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeatherApi.Services;

namespace WeatherApi.Controllers
{
    [Route("api/weather")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IElevationService _elevationService;
        private readonly ITimeZoneService _timeZoneService;
        private readonly IWeatherService _weatherService;

        public WeatherController(
            IElevationService elevationService,
            ITimeZoneService timeZoneService,
            IWeatherService weatherService)
        {
            elevationService.MustNotBeNull();
            timeZoneService.MustNotBeNull();
            weatherService.MustNotBeNull();

            _elevationService = elevationService;
            _timeZoneService = timeZoneService;
            _weatherService = weatherService;
        }

        [Route("{zipCode}")]
        public async Task<IActionResult> GetByZipCode(string zipCode)
        {
            zipCode.MustNotBeNullOrWhiteSpace();

            var weatherResponse = await _weatherService.GetCityDataAsync(zipCode);
            var elevation = await _elevationService.GetElevationAsync(weatherResponse.Latitude, weatherResponse.Longitude);
            var timeZone = await _timeZoneService.GetTimeZoneAsync(weatherResponse.Latitude, weatherResponse.Longitude);

            return Ok(new
            {
                City = weatherResponse.CityName,
                Elevation = elevation,
                Latitude = weatherResponse.Latitude,
                Longitude = weatherResponse.Longitude,
                Temperature = weatherResponse.Temperature,
                TimeZone = timeZone
            });
        }
    }
}