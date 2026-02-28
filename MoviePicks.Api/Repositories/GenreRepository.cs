namespace MoviePicks.Api.Repositories;

using MoviePicks.Api.Models;
using MoviePicks.Api.Repositories.Core;

public class GenreRepository : Repository<Genre>, IGenreRepository
{
    private readonly DbMovieContext movieContext;

    public GenreRepository(DbMovieContext movieContext)
        : base(movieContext)
    {
        this.movieContext = movieContext;
    }
}
