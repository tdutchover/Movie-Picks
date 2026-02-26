namespace MovieRatingsBackendWebApi.Models;

using MR.Models;

/// <summary>
/// This is an intersection table between Movie and Genre.
/// </summary>
public class MovieGenre
{
    public MovieGenre()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MovieGenre"/> class.
    /// This constructor is specifically designed for creating new instances
    /// that represent the many-to-many relationship between Movie and Genre entities.
    /// Only the foreign keys are required to establish the relationship in the database.
    /// </summary>
    /// <param name="movieId">The ID of the movie. Required for establishing the relationship.</param>
    /// <param name="genreId">The ID of the genre. Required for establishing the relationship.</param>
    public MovieGenre(int movieId, int genreId)
    {
        this.MovieId = movieId;
        this.GenreId = genreId;
    }

    public int MovieId { get; set; }

    public Movie? Movie { get; set; }

    public int GenreId { get; set; }

    public Genre? Genre { get; set; }
}
