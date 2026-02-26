namespace MovieRatingsBackendWebApi.Repositories.Core;

using MovieRatingsBackendWebApi.Models;
using MovieRatingsBackendWebApi.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbMovieContext db;

    public IMovieRepository MovieRepository { get; }

    public IGenreRepository GenreRepository { get; }

    public UnitOfWork(DbMovieContext db)
    {
        this.db = db;
        this.MovieRepository = new DbMovieRepository(db);
        this.GenreRepository = new GenreRepository(db);
    }

    public async Task SaveAsync()
    {
        await this.db.SaveChangesAsync();
    }
}
