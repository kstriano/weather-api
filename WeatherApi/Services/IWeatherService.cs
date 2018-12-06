using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Threading.Tasks;
using WeatherApi.Services.Responses;

namespace WeatherApi.Services
{
    public interface IWeatherService
    {
        Task<GetCityDataResponse> GetCityDataAsync(string zipCode);
    }

    public class WeatherService : IWeatherService
    {
        private string _apiKey;

        public WeatherService(IOptions<ApiKeys> optionsAccessor)
        {
            optionsAccessor.MustNotBeNull();

            _apiKey = optionsAccessor.Value.WeatherApiKey;
        }

        public async Task<GetCityDataResponse> GetCityDataAsync(string zipCode)
        {
            var restRequest = new RestRequest(Method.GET);

            restRequest.AddParameter("appid", _apiKey);
            restRequest.AddParameter("units", "imperial");
            restRequest.AddParameter("zip", zipCode);

            var client = new RestClient("https://api.openweathermap.org/data/2.5/weather");

            var response = await client.ExecuteTaskAsync(restRequest);
            var data = JObject.Parse(response.Content);

            return new GetCityDataResponse
            {
                CityName = data["name"].ToString(),
                Latitude = data["coord"]["lat"].ToObject<decimal>(),
                Longitude = data["coord"]["lon"].ToObject<decimal>(),
                Temperature = data["main"]["temp"].ToObject<decimal>()
            };
        }
    }
}