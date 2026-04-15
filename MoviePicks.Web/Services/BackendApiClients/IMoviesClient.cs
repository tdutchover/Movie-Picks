namespace MoviePicks.Web.Services.BackendApiClients;

using MoviePicks.Contracts.DTOs;
using MoviePicks.Contracts.Enums;
using MoviePicks.Web.Models;

public interface IMoviesClient
{
    Task<List<GenreDto>> GetAllGenresAsync();

    Task<List<MovieViewModel>> GetAllMoviesAsync();

    Task<List<MovieViewModel>> GetFilteredMovies(MovieFilterFormModel filterCriteria);

    Task<MovieViewModel> GetMovieAsync(int movieId, PlotSize plotSize);

    Task CreateMovieAsync(MovieViewModel movieViewModel);

    Task<bool> DeleteMovieAsync(int movieId);

    Task UpdateMovieAsync(MovieDto movie);
}
