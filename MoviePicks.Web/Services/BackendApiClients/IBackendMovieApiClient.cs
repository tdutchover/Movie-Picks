namespace MoviePicks.Web.Services.BackendApiClients;

using MoviePicks.Contracts.DTOs;
using MoviePicks.Contracts.Enums;
using MoviePicks.Web.Models;

public interface IBackendMovieApiClient
{
    Task <List<GenreDto>> GetAllGenres();

    Task<List<CompositeMovie>> GetAllMovies();

    Task<IEnumerable<OmdbMovieShortDetailsDto>> SearchOmdbMoviesByTitlePatternAsync(string titlePattern);

    Task<OmdbMovieDetailsDto> GetMovieByImdbIdAsync(string imdbId);

    Task<List<MovieViewModel>> GetAllMovieViewModels();

    Task<List<MovieViewModel>> GetFilteredMovieViewModels(MovieFilterFormModel filterCriteria);

    Task<MovieViewModel> GetMovieViewModel(int movieId, PlotSize plotSize);

    Task AddMovie(MovieViewModel movieViewModel);

    Task<bool> DeleteMovie(int movieId);

    Task UpdateMovie(MovieDto movie);
}
