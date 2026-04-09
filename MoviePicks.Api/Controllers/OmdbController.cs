namespace MoviePicks.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MoviePicks.Api.Infrastructure.ThirdPartyApiClients;
using MoviePicks.Contracts;
using MoviePicks.Contracts.DTOs;
using MoviePicks.Contracts.Enums;

[Route(ApiRoutes.Omdb.Base)]
[ApiController]
public class OmdbController : Controller
{
    private readonly IOmdbApiMovieReader omdbApiMovieReader;

    public OmdbController(IOmdbApiMovieReader omdbApiMovieReader)
    {
        this.omdbApiMovieReader = omdbApiMovieReader;
    }

    // TODO: Put the title pattern into a query parameter instead of the route to follow RESTful conventions.
    [HttpGet("{titlePattern}", Name = nameof(SearchOmdbMoviesByTitlePattern))]
    public async Task<ActionResult<IEnumerable<OmdbMovieShortDetailsDto>>> SearchOmdbMoviesByTitlePattern(string titlePattern)
    {
        return await this.omdbApiMovieReader.SearchMoviesByTitle(titlePattern);
    }

    // TODO: Put the imdbId into a route segment to follow RESTful conventions.
    [HttpGet(Name = nameof(GetMovieByImdbId))]
    public async Task<ActionResult<OmdbMovieDetailsDto>> GetMovieByImdbId([FromQuery][BindRequired] string imdbId)
    {
        if (imdbId == null || imdbId.Length == 0)
        {
            return this.BadRequest("imdbId not specified");
        }

        var result = await this.omdbApiMovieReader.GetMovieByImdbId(imdbId, PlotSize.Short);

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
