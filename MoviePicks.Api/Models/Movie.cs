namespace MoviePicks.Api.Models;

using MoviePicks.Contracts;

public class Movie : MovieBase
{
    public ICollection<MovieGenre> MovieGenres { get; set; }
}
