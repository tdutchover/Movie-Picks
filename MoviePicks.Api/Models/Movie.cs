namespace MovieRatingsBackendWebApi.Models;

using MR.Models;

public class Movie : MovieBase
{
    public ICollection<MovieGenre> MovieGenres { get; set; }
}
