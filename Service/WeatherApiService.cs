using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReadyTech_Extra.Interface;
using ReadyTech_Extra.Models;
using System.Text.Json;

namespace ReadyTech_Extra.Service
{
    public class WeatherApiService : IWeatherApiService
    {
        private static readonly HttpClient client;

        static WeatherApiService()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("http://api.openweathermap.org")
            };
        }

        public async Task<OpenWeatherResponse> GetTemperature(string city)
        {

            var url = string.Format("/data/2.5/weather?q={0}&appid=c25f3bca34e164bc9cb801112cb4588a&units=metric", city);
            var result = new OpenWeatherResponse();
            result.Main = new Main();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

               
                var rawWeather = JsonConvert.DeserializeObject<OpenWeatherResponse>(stringResponse);

                result.Main.Temp = rawWeather.Main.Temp;
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;

        }
    }
}
