namespace MovieRatingsBackendWebApi.Repositories.Core;

using MovieRatingsBackendWebApi.Repositories;

public interface IUnitOfWork
{
    IMovieRepository MovieRepository { get; }

    IGenreRepository GenreRepository { get; }

    public Task SaveAsync();
}
