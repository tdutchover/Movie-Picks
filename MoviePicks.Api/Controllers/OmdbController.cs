namespace MovieRatingsBackendWebApi.Controllers;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MovieRatingsBackendWebApi.Models;
using MovieRatingsBackendWebApi.Services;
using MovieRatingsBackendWebApi.Services.ThirdPartyApiClients;
using MovieRatingsBackendWebApi.Shared;
using MR.Models.Enums;

// FindById out about swagger:  http://swagger.io
// From OMDB on October 17, 2022
//  Swagger files (YAML, JSON) to expose current OMDB API abilities and upcoming REST functions.
//      https://www.omdbapi.com/swagger.json

// [Route("api/[controller]/[action]")]  //Add the action name to the path for all APIs
[Route("api/[controller]")]
[ApiController]
public class OmdbController : ControllerBase
{
    private readonly IOmdbApiMovieReader omdbMovieReader;

    public OmdbController(IOmdbApiMovieReader omdbMovieReader)
    {
        this.omdbMovieReader = omdbMovieReader;
    }

    [EnableCors(BackendConstants.CorsPolicyName_For_TravisMovieRatings_Project)]
    [HttpGet("movies/{titlePattern}")]
    public async Task<ActionResult<IEnumerable<OmdbMovieShortDetails>>> SearchOmdbMoviesByTitlePattern(string titlePattern)
    {
        return await this.omdbMovieReader.SearchMoviesByTitle(titlePattern);
    }

    [EnableCors(BackendConstants.CorsPolicyName_For_TravisMovieRatings_Project)]
    [HttpGet("movie")]
    public async Task<ActionResult<OmdbMovieDetails>> GetMovieByImdbId([FromQuery][BindRequired] string imdbId)
    {
        if (imdbId == null || imdbId.Length == 0)
        {
            return this.BadRequest("imdbId not specified");
        }

        var result = await this.omdbMovieReader.GetMovieByImdbId(imdbId, PlotSize.Short);

        if (result != null)
        {
            return result;
        }
        else
        {
            return this.NotFound();
        }
    }
}
