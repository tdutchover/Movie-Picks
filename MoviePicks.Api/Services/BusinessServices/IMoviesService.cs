namespace MoviePicks.Api.Services.BusinessServices;

using MoviePicks.Api.Models;
using MoviePicks.Contracts.DTOs;
using MoviePicks.Contracts.Enums;

public interface IMoviesService
{
    Task<List<GenreDto>> GetAllGenresAsync();

    Task<List<MovieViewModel>> GetFilteredMovieViewModels(MovieFilterDto filterDTO);

    Task<List<MovieViewModel>> GetAllMovieViewModels();

    Task<MovieViewModel> GetMovieViewModel(int movieId, PlotSize plotSize);

    Task CreateMovieAsync(MovieViewModel movieViewModel);

    Task<bool> DeleteMovieAsync(int movieId);

    Task UpdateMovie(Movie movie);
}
