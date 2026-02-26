namespace MovieRatingsBackendWebApi.Services.BusinessServices;

using MovieRatingsBackendWebApi.Models;
using MR.Models.DTOs;
using MR.Models.Enums;
using TravisMovieRatings.DataTransferObjects;

public interface ICompositeMovieService
{
    Task<List<GenreDTO>> GetAllGenresAsync();

    Task<List<CompositeMovie>> GetAllMovies();

    Task<List<MovieViewModel>> GetFilteredMovieViewModels(MovieFilterDTO filterDTO);

    Task<List<MovieViewModel>> GetAllMovieViewModels();

    Task<MovieViewModel> GetMovieViewModel(int movieId, PlotSize plotSize);

    Task AddMovieAsync(MovieViewModel movieViewModel);

    Task<bool> DeleteMovieAsync(int movieId);

    Task UpdateMovie(Movie movie);
}
