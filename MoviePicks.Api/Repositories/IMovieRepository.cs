namespace MovieRatingsBackendWebApi.Repositories;

using MovieRatingsBackendWebApi.Models;
using MovieRatingsBackendWebApi.Repositories.Core;

public interface IMovieRepository : IRepository<Movie>
{
    Movie GetMovie(int movieId);

    void UpdateMovie(Movie movie);
}
