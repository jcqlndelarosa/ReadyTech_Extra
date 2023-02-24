using Microsoft.AspNetCore.Mvc;
using ReadyTech.Models;
using System.Globalization;
using ReadyTech.Validation;
using Newtonsoft.Json;
using ReadyTech_Extra.Models;
using ReadyTech_Extra.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReadyTech.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BrewCoffeeController : ControllerBase
    {
        private readonly IWeatherApiService _weatherApiService;

        public BrewCoffeeController(IWeatherApiService weatherApiService)
        {
            _weatherApiService = weatherApiService;
        }

        // GET: BrewCoffee
        [HttpGet("/brew-coffee")]
        public async Task<IActionResult> Get()
        {
            //DateTime aprilFools = new DateTime(2023, 4, 1);
            //bool _isMonthDayMatch = Util.IsMonthDayMatch(aprilFools, 4, 1);
            
            bool _isMonthDayMatch = Util.IsMonthDayMatch(DateTime.Now, 4, 1);

            if (_isMonthDayMatch)
            {
                return StatusCode(StatusCodes.Status418ImATeapot, String.Empty);
            }


            //Check Weather --- Call GetTemperature
            OpenWeatherResponse weatherResponse = new OpenWeatherResponse();
            weatherResponse = await _weatherApiService.GetTemperature("Adelaide");

            BrewCoffee brewCoffee = new BrewCoffee();

            brewCoffee.Message = "Your piping hot coffee is ready";
            brewCoffee.Prepared = DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz", CultureInfo.InvariantCulture);


            if (weatherResponse != null &&
                 weatherResponse.Main.Temp > 30)
            {
                brewCoffee.Message = "Your refreshing iced coffee is ready";
            }
            
            return Ok(brewCoffee);
        }


        [HttpGet("/brew-coffee/{city}")]
        public async Task<IActionResult> Get(string city)
        {
            //DateTime aprilFools = new DateTime(2023, 4, 1);
            //bool _isMonthDayMatch = Util.IsMonthDayMatch(aprilFools, 4, 1);

            bool _isMonthDayMatch = Util.IsMonthDayMatch(DateTime.Now, 4, 1);
           
            if (_isMonthDayMatch)
            {
                return StatusCode(StatusCodes.Status418ImATeapot, String.Empty);
            }


            //Check Weather --- Call GetTemperature
            OpenWeatherResponse weatherResponse = new OpenWeatherResponse();
            weatherResponse = await _weatherApiService.GetTemperature(city);

            BrewCoffee brewCoffee = new BrewCoffee();

            brewCoffee.Message = "Your piping hot coffee is ready";
            brewCoffee.Prepared = DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz", CultureInfo.InvariantCulture);


            if (weatherResponse != null &&
                 weatherResponse.Main.Temp > 30)
            {
                brewCoffee.Message = "Your refreshing iced coffee is ready";
            }

            return Ok(brewCoffee);
        }

    }
}
