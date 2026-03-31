using MoviePicks.Contracts.DTOs;

namespace MoviePicks.Api.Models;

public class CompositeMovie
{
    public CompositeMovie(Movie movie, OmdbMovieDetailsDto omdbMovieDetails)
    {
        this.Movie = movie;
        this.MovieDetails = omdbMovieDetails;
    }

    public Movie Movie { get; set; }

    public OmdbMovieDetailsDto MovieDetails { get; set; }
}
