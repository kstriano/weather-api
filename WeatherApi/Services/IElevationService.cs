using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Threading.Tasks;

namespace WeatherApi.Services
{
    public interface IElevationService
    {
        Task<decimal> GetElevationAsync(decimal latitude, decimal longitude);
    }

    public class ElevationService : IElevationService
    {
        private string _apiKey;

        public ElevationService(IOptions<ApiKeys> optionsAccessor)
        {
            optionsAccessor.MustNotBeNull();

            _apiKey = optionsAccessor.Value.GoogleApiKey;
        }

        public async Task<decimal> GetElevationAsync(decimal latitude, decimal longitude)
        {
            var restRequest = new RestRequest(Method.GET);

            restRequest.AddParameter("key", _apiKey);
            restRequest.AddParameter("locations", $"{latitude},{longitude}");

            var client = new RestClient("https://maps.googleapis.com/maps/api/elevation/json");

            var response = await client.ExecuteTaskAsync(restRequest);
            var data = JObject.Parse(response.Content);

            return data["results"][0]["elevation"].ToObject<decimal>();
        }
    }
}