namespace MoviePicks.Api.Repositories.Core;

using MoviePicks.Api.Models;
using MoviePicks.Api.Repositories;

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
