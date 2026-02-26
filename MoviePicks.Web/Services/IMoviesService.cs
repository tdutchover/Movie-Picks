namespace TravisMovieRatings.Services;

using MoviePicks.Web.Models;
using TravisMovieRatings.Models;

public interface IMoviesService
{
    Task<MoviesViewModel> FetchAllMovieViewModelsAsync();

    Task<MoviesViewModel> FetchFilteredMovieViewModelsAsync(MovieFilterFormModel filterCriteria);
}
