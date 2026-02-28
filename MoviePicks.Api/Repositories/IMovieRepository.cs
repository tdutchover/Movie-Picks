namespace MoviePicks.Api.Repositories;

using MoviePicks.Api.Models;
using MoviePicks.Api.Repositories.Core;

public interface IMovieRepository : IRepository<Movie>
{
    Movie GetMovie(int movieId);

    void UpdateMovie(Movie movie);
}
