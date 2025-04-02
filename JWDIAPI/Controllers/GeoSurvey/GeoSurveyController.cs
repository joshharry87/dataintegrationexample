using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using JWDIACONTRACTS.Interfaces.GeoSurvey;
using JWDIACONTRACTS.DTO.GeoSurveyDTO;



namespace JWDIAPI.Controllers.GeoSurvey;


[Route("api/[controller]")]
[ApiController]
public class GeoSurveyController: BaseApiController
{
         private readonly IGeoSurveyService _geoSurveyService;
    public GeoSurveyController(IGeoSurveyService geoSurveyService)
    {
        _geoSurveyService = geoSurveyService;
    }


    // public HomeController(ILogger<HomeController> logger)
    // {
    //     _logger = logger;
    // }
    [Authorize]
    [Route("api/[controller]/GetAll")]
    [HttpGet]
    public async Task<ActionResult<List<GeochemSurvey>>> GetAllGeoSurveyDataAsync()
    {
        var geoSurveyData = await _geoSurveyService.GetAllGeoSurveyDataAsync();
        return Ok(geoSurveyData);
    }

    [Route("api/[controller]/GetLocationByElement")]
    [HttpGet]
    public async Task<ActionResult<List<GeoChemTopElementLocations>>> GetAllGeoSurveyLocationsByElementAsync(string element){
        
        var geoSurveyData = await _geoSurveyService.GetAllGeoSurveyLocationsByElementAsync(element);
        return Ok(geoSurveyData);
    }

    
    [Route("api/[controller]/GetAllUniqueElement")]
    [HttpGet]
    public async Task<ActionResult<List<string>>> GetAllUniqueElementsAsync(){
        
        var geoSurveyData = await _geoSurveyService.GetAllUniqueElementsAsync();
        return Ok(geoSurveyData);
    }

    [Route("api/[controller]/DoesNothing")]
    [HttpGet]
    public async Task<ActionResult<List<string>>> GetAThing(){
        
        List<string>  nothingReally = new List<string>(new string[] { "element1", "element2", "element3" });  
        return Ok(nothingReally);
    }





    
}
