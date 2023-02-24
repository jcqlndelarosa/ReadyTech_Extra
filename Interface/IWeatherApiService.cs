using ReadyTech_Extra.Models;

namespace ReadyTech_Extra.Interface
{
    public interface IWeatherApiService
    {
        Task<OpenWeatherResponse> GetTemperature(string city);
    }
}
