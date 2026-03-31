namespace MoviePicks.Api.Services.BusinessServices;

using MoviePicks.Api.Models;
using MoviePicks.Contracts.DTOs;
using MoviePicks.Contracts.Enums;

public interface ICompositeMovieService
{
    Task<List<OmdbMovieShortDetailsDto>> SearchMoviesByTitle(string title);

    Task<OmdbMovieDetailsDto> GetMovieByImdbId(string imdbId, PlotSize plotSize);

    Task<List<GenreDto>> GetAllGenresAsync();

    Task<List<CompositeMovie>> GetAllMovies();

    Task<List<MovieViewModel>> GetFilteredMovieViewModels(MovieFilterDto filterDTO);

    Task<List<MovieViewModel>> GetAllMovieViewModels();

    Task<MovieViewModel> GetMovieViewModel(int movieId, PlotSize plotSize);

    Task AddMovieAsync(MovieViewModel movieViewModel);

    Task<bool> DeleteMovieAsync(int movieId);

    Task UpdateMovie(Movie movie);
}
