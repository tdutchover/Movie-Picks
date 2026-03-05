namespace MoviePicks.Web.Services.BackendApiClients;

using MoviePicks.Contracts.DTOs;
using MoviePicks.Contracts.Enums;
using MoviePicks.Web.Models;

public interface IBackendMovieApiClient
{
    Task<List<GenreDTO>> GetAllGenres();

    Task<List<CompositeMovie>> GetAllMovies();

    Task<List<MovieViewModel>> GetAllMovieViewModels();

    Task<List<MovieViewModel>> GetFilteredMovieViewModels(MovieFilterFormModel filterCriteria);

    Task<MovieViewModel> GetMovieViewModel(int movieId, PlotSize plotSize);

    Task AddMovie(MovieViewModel movieViewModel);

    Task<bool> DeleteMovie(int movieId);

    Task UpdateMovie(MovieDTO movie);
}
