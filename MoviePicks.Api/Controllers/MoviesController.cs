namespace MoviePicks.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using MoviePicks.Api.Infrastructure.Mappers;
using MoviePicks.Api.Models;
using MoviePicks.Api.Services.BusinessServices;
using MoviePicks.Contracts;
using MoviePicks.Contracts.DTOs;
using MoviePicks.Contracts.Enums;

[Route(ApiRoutes.App.Movies.Base)]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMoviesService moviesService;

    public MoviesController(IMoviesService moviesService)
    {
        this.moviesService = moviesService;
    }

    // TODO: Refactor to use a DTO instead of MovieViewModel to better separate the API layer from the UI layer,
    // reduce the risk of overposting attacks, and ensure that only relevant data is sent to the client.
    // This change will improve the maintainability and security of the application.
    //
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost(Name = nameof(CreateMovie))]
    public async Task<IActionResult> CreateMovie([FromBody] MovieViewModel movieViewModel)
    {
        await this.moviesService.CreateMovieAsync(movieViewModel);
        return this.Created();
    }

    [HttpDelete("{movieId:int}", Name = nameof(DeleteMovie))]
    public async Task<IActionResult> DeleteMovie(int movieId)
    {
        bool deleted = await this.moviesService.DeleteMovieAsync(movieId);
        if (!deleted)
        {
            // 204 No Content if the entity was not found or already deleted
            return this.NoContent();
        }

        return this.Ok();
    }

    [HttpGet(ApiRoutes.App.Movies.GenresSegment, Name = nameof(GetAllGenres))]
    public async Task<IActionResult> GetAllGenres()
    {
        List<GenreDto> genres = await this.moviesService.GetAllGenresAsync();
        return this.Ok(genres);
    }

    [HttpGet(Name = nameof(GetAllMovies))]
    public async Task<List<MovieViewModel>> GetAllMovies()
    {
        return await this.moviesService.GetAllMovieViewModels();
    }

    /// <summary>
    /// Gets movies based on filter criteria.
    /// </summary>
    /// <param name="filterDTO">The filtering criteria.
    /// The filtering criteria used to filter the movie view models. This includes:
    /// - `Rating`: Optional. The minimum rating (inclusive) that movies must have to be included in the result.
    /// - `Genres`: Optional. A list of genre names. Movies must match these genres based on the `GenreFilterMode`.
    /// - `GenreFilterMode`: Determines whether movies must match all specified genres (`MatchAll`) or any of them (`MatchAny`).
    /// </param>
    /// <response code="200">Returns the list of movie view models.</response>
    /// <response code="500">If there is an internal server error.</response>
    [HttpGet(ApiRoutes.App.Movies.FilterSegment, Name = nameof(GetFilteredMovies))]
    [ProducesResponseType(typeof(List<MovieViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetFilteredMovies([FromQuery] MovieFilterDto filterDTO)
    {
        var movieViewModels = await this.moviesService.GetFilteredMovieViewModels(filterDTO);
        return this.Ok(movieViewModels);
    }

    [HttpGet("{movieId:int}", Name = nameof(GetMovie))]
    public async Task<MovieViewModel> GetMovie(int movieId, PlotSize plotSize)
    {
        return await this.moviesService.GetMovieViewModel(movieId, plotSize);
    }

    [HttpPut(Name = nameof(UpdateMovie))]
    public async Task<ActionResult<CompositeMovie>> UpdateMovie(MovieDto movieDTO)
    {
        try
        {
            Movie movie = movieDTO.ToMovie();
            await this.moviesService.UpdateMovie(movie);
            return this.Ok();
        }
        catch (Exception ex)
        {
            // TODO Log exception
            return this.NotFound(ex.Message);
        }
    }
}