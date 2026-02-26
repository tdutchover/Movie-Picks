namespace MovieRatingsBackendWebApi.Repositories;

using MovieRatingsBackendWebApi.Models;
using MovieRatingsBackendWebApi.Repositories.Core;

public class DbMovieRepository : Repository<Movie>, IMovieRepository
{
    private readonly DbMovieContext movieContext;

    public DbMovieRepository(DbMovieContext movieContext)
        : base(movieContext)
    {
        this.movieContext = movieContext;
    }

    public Movie GetMovie(int movieId)
    {
        Movie? movie = FindById(movieId);

        if (movie == null)
        {
            // TODO Should log this detailed message and only return a generic message to the caller
            throw new ArgumentException($"Movie record not found in database for MovieId {movieId}");
        }

        if (string.IsNullOrWhiteSpace(movie.ImdbId))
        {
            // TODO Should log this detailed message and only return a generic message to the caller
            throw new ArgumentException($"Movie record with MovieId {movieId} is missing a valid ImdbId");
        }

        return movie;
    }

    // TODO This method should return an error if there's no existing movie to update
    public void UpdateMovie(Movie movie)
    {
        Movie? dbMovie = movieContext.Movies.Find(movie.Id);

        if (dbMovie != null)
        {
            dbMovie.ImdbId = movie.ImdbId;
            dbMovie.Rating = movie.Rating;
            dbMovie.ReviewHeading = movie.ReviewHeading;
            dbMovie.ReviewComments = movie.ReviewComments;
        }
    }
}
