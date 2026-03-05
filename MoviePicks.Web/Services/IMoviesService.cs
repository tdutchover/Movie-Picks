namespace MoviePicks.Web.Services;

using MoviePicks.Web.Models;

public interface IMoviesService
{
    Task<MoviesViewModel> FetchAllMovieViewModelsAsync();

    Task<MoviesViewModel> FetchFilteredMovieViewModelsAsync(MovieFilterFormModel filterCriteria);
}
