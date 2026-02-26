namespace TravisMovieRatings.Services.BackendApiClients;

using MoviePicks.Web.Models;
using MR.Models.DTOs;
using MR.Models.Enums;
using TravisMovieRatings.Models;

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
