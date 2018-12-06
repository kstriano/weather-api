namespace WeatherApi.Services.Responses
{
    public class GetCityDataResponse
    {
        public string CityName { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal Temperature { get; set; }
    }
}