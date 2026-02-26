namespace MovieRatingsBackendWebApi.Repositories;

using MovieRatingsBackendWebApi.Models;
using MovieRatingsBackendWebApi.Repositories.Core;

public class GenreRepository : Repository<Genre>, IGenreRepository
{
    private readonly DbMovieContext movieContext;

    public GenreRepository(DbMovieContext movieContext)
        : base(movieContext)
    {
        this.movieContext = movieContext;
    }
}
