namespace MovieRatingsBackendWebApi.Models;

using MR.Models;

public class Genre : GenreCore, IIdentifiable
{
    public ICollection<MovieGenre> MovieGenres { get; set; }
}
