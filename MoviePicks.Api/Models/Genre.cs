namespace MoviePicks.Api.Models;

using MoviePicks.Contracts;

public class Genre : GenreCore, IIdentifiable
{
    public ICollection<MovieGenre> MovieGenres { get; set; }
}
