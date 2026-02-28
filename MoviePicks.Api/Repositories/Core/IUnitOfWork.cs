namespace MoviePicks.Api.Repositories.Core;

using MoviePicks.Api.Repositories;

public interface IUnitOfWork
{
    IMovieRepository MovieRepository { get; }

    IGenreRepository GenreRepository { get; }

    public Task SaveAsync();
}
