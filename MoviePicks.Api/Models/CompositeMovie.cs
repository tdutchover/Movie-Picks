namespace MovieRatingsBackendWebApi.Models;
using MR.Models;

public class CompositeMovie
{
    public CompositeMovie(Movie movie, OmdbMovieDetails omdbMovieDetails)
    {
        this.Movie = movie;
        this.MovieDetails = omdbMovieDetails;
    }

    public Movie Movie { get; set; }

    public OmdbMovieDetails MovieDetails { get; set; }
}
