namespace MovieRatingsBackendWebApi.Models;

using Microsoft.EntityFrameworkCore;
using MR.Models;

public class DbMovieContext : DbContext
{
    public DbMovieContext(DbContextOptions<DbMovieContext> options)
        : base(options)
    {
        this.Movies = Set<Movie>();
    }

    public DbSet<Movie> Movies { get; set; }

    public DbSet<Genre> Genres { get; set; }

    public DbSet<MovieGenre> MovieGenres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieGenre>()
                    .HasKey(mg => new { mg.MovieId, mg.GenreId });

        modelBuilder.Entity<Genre>().HasData(
            new Genre() { Id = 1, Name = "Action" },
            new Genre() { Id = 2, Name = "Comedy" },
            new Genre() { Id = 3, Name = "Drama" },
            new Genre() { Id = 4, Name = "Thriller" },
            new Genre() { Id = 5, Name = "Sci-Fi" },
            new Genre() { Id = 6, Name = "Romance" },
            new Genre() { Id = 7, Name = "Horror" },
            new Genre() { Id = 8, Name = "Adventure" },
            new Genre() { Id = 9, Name = "Mystery" },
            new Genre() { Id = 10, Name = "Fantasy" },
            new Genre() { Id = 11, Name = "Crime" },
            new Genre() { Id = 12, Name = "Short" },
            new Genre() { Id = 13, Name = "Biography" },
            new Genre() { Id = 14, Name = "War" },
            new Genre() { Id = 15, Name = "Music" },
            new Genre() { Id = 16, Name = "Animation" });

        modelBuilder.Entity<MovieGenre>().HasData(
            new MovieGenre() { MovieId = 1, GenreId = 1 },
            new MovieGenre() { MovieId = 1, GenreId = 2 },
            new MovieGenre() { MovieId = 1, GenreId = 6 },
            new MovieGenre() { MovieId = 2, GenreId = 3 },
            new MovieGenre() { MovieId = 2, GenreId = 4 },
            new MovieGenre() { MovieId = 2, GenreId = 7 },
            new MovieGenre() { MovieId = 3, GenreId = 5 },
            new MovieGenre() { MovieId = 3, GenreId = 8 },
            new MovieGenre() { MovieId = 4, GenreId = 9 },
            new MovieGenre() { MovieId = 5, GenreId = 1 });

        modelBuilder.Entity<Movie>().HasData(
            new Movie() { Id = 1, ImdbId = "tt3581920", Rating = 5, ReviewHeading = "Heading: The Last of Us2", ReviewComments = "Movie1 review" },
            new Movie() { Id = 2, ImdbId = "tt1188729", Rating = 10, ReviewHeading = "Heading: Pandorum", ReviewComments = "Movie2 review" },
            new Movie() { Id = 3, ImdbId = "tt0348150", Rating = 1, ReviewHeading = "Heading: Superman Returns", ReviewComments = "Movie3 review" },
            new Movie() { Id = 4, ImdbId = "tt6565702", Rating = 8, ReviewHeading = "Heading: Dark Phoenix", ReviewComments = "Movie4 review" },
            new Movie() { Id = 5, ImdbId = "tt0100813", Rating = 3, ReviewHeading = "Heading: Treasure Island", ReviewComments = "Movie5 review" });
    }
}
