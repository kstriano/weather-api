using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace WeatherApi.Services
{
    public interface ITimeZoneService
    {
        Task<string> GetTimeZoneAsync(decimal latitude, decimal longitude);
    }

    public class TimeZoneService : ITimeZoneService
    {
        private string _apiKey;

        public TimeZoneService(IOptions<ApiKeys> optionsAccessor)
        {
            optionsAccessor.MustNotBeNull();

            _apiKey = optionsAccessor.Value.GoogleApiKey;
        }

        public async Task<string> GetTimeZoneAsync(decimal latitude, decimal longitude)
        {
            var restRequest = new RestRequest(Method.GET);

            restRequest.AddParameter("key", _apiKey);
            restRequest.AddParameter("location", $"{latitude},{longitude}");
            restRequest.AddParameter("timestamp", DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            var client = new RestClient("https://maps.googleapis.com/maps/api/timezone/json");

            var response = await client.ExecuteTaskAsync(restRequest);
            var data = JObject.Parse(response.Content);

            return data["timeZoneName"].ToString();
        }
    }
}