using AbrigoHub.Web.Models;
using System.Net.Http.Json;

namespace AbrigoHub.Web.Services
{
    public interface IWeatherService
    {
        Task<WeatherResponse> GetWeatherAsync(string city);
    }

    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public WeatherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<WeatherResponse> GetWeatherAsync(string city)
        {
            var apiKey = _configuration["OpenWeather:ApiKey"];
            var baseUrl = _configuration["OpenWeather:BaseUrl"];
            
            var response = await _httpClient.GetFromJsonAsync<WeatherResponse>(
                $"{baseUrl}/weather?q={city}&appid={apiKey}&units=metric&lang=pt_br");

            return response;
        }
    }
} 