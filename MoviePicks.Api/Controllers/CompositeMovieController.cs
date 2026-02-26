namespace MovieRatingsBackendWebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using MovieRatingsBackendWebApi.Infrastructure.Mappers;
using MovieRatingsBackendWebApi.Models;
using MovieRatingsBackendWebApi.Services.BusinessServices;
using MR.Models.DTOs;
using MR.Models.Enums;
using TravisMovieRatings.DataTransferObjects;

[Route("api/[controller]/[action]")]
[ApiController]
public class CompositeMovieController : ControllerBase
{
    private readonly ICompositeMovieService compositeMovieService;

    public CompositeMovieController(ICompositeMovieService compositeMovieService)
    {
        this.compositeMovieService = compositeMovieService;
    }

    // TODO: Refactor to use a DTO instead of MovieViewModel to better separate the API layer from the UI layer,
    // reduce the risk of overposting attacks, and ensure that only relevant data is sent to the client.
    // This change will improve the maintainability and security of the application.
    //
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<IActionResult> AddMovie([FromBody] MovieViewModel movieViewModel)
    {
        await this.compositeMovieService.AddMovieAsync(movieViewModel);
        return this.Ok();
    }

    // Example URL pattern: DELETE api/[controller]/{movieId}
    [HttpDelete("{movieId}")]
    public async Task<IActionResult> DeleteMovie(int movieId)
    {
        bool deleted = await this.compositeMovieService.DeleteMovieAsync(movieId);
        if (!deleted)
        {
            // 204 No Content if the entity was not found or already deleted
            return this.NoContent();
        }

        return this.Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGenres()
    {
        List<GenreDTO> genres = await this.compositeMovieService.GetAllGenresAsync();
        return this.Ok(genres);
    }

    // TODO: GetAllMovies is not used by client. Should delete it from public access.
    //       But maybe keep it for test purposes. Perhaps keep it by putting it into a controller
    //       that provides only internal HTTP access for testing.
    [HttpGet]
    public async Task<List<CompositeMovie>> GetAllCompositeMovies()
    {
        return await this.compositeMovieService.GetAllMovies();
    }

    [HttpGet]
    public async Task<List<MovieViewModel>> GetAllMovieViewModels()
    {
        return await this.compositeMovieService.GetAllMovieViewModels();
    }

    /// <summary>
    /// Gets filtered movie view models based on provided criteria.
    /// </summary>
    /// <param name="filterDTO">The filtering criteria.
    /// The filtering criteria used to filter the movie view models. This includes:
    /// - `Rating`: Optional. The minimum rating (inclusive) that movies must have to be included in the result.
    /// - `Genres`: Optional. A list of genre names. Movies must match these genres based on the `GenreFilterMode`.
    /// - `GenreFilterMode`: Determines whether movies must match all specified genres (`MatchAll`) or any of them (`MatchAny`).
    /// </param>
    /// <response code="200">Returns the list of movie view models.</response>
    /// <response code="500">If there is an internal server error.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<MovieViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetFilteredMovieViewModels([FromQuery] MovieFilterDTO filterDTO)
    {
        var movieViewModels = await this.compositeMovieService.GetFilteredMovieViewModels(filterDTO);
        return this.Ok(movieViewModels);
    }

    [HttpGet("{movieId}")]
    public async Task<MovieViewModel> GetMovieViewModel(int movieId, PlotSize plotSize)
    {
        return await this.compositeMovieService.GetMovieViewModel(movieId, plotSize);
    }

    [HttpPut]
    public async Task<ActionResult<CompositeMovie>> UpdateMovie(MovieDTO movieDTO)
    {
        try
        {
            Movie movie = movieDTO.ToMovie();
            await this.compositeMovieService.UpdateMovie(movie);
            return this.Ok();
        }
        catch (Exception ex)
        {
            // TODO Log exception
            return this.NotFound(ex.Message);
        }
    }
}
