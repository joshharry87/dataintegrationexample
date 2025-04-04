using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using JWDIADATA.Data.Entities;
using JWDIACONTRACTS.Interfaces.Weather;
using JWDIAPI.Helpers.CustomPolicies;


namespace JWDIAPI.Controllers.Weather;

[Route("api/[controller]")]
[ApiController]
public class WeatherController : BaseApiController
{
 

    private readonly IWeatherDataService _weatherDataService;
    public WeatherController(IWeatherDataService weatherDataService)
    {
        _weatherDataService = weatherDataService;
    }


    [ServiceFilter(typeof(UsageLimitFilter))] // Example of generic usage filtering application
    [Authorize(Roles = "User")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WeatherDataModel>>> GetAllWeatherData()
    {
        var weatherData = await _weatherDataService.GetAllWeatherDataAsync();
        return Ok(weatherData);
    }


}
