namespace TravisMovieRatings.Models;

/// <summary>
/// This is the limited information for a particular movie that appears in the results when searching for a movie title pattern.
/// Example:
/// {
///     "Title": "The Avengers",
///     "Year": "2012",
///     "imdbID": "tt0848228",
///     "Type": "movie",
///     "Poster": "https://m.media-amazon.com/images/M/MV5BNDYxNjQyMjAtNTdiOS00NGYwLWFmNTAtNThmYjU5ZGI2YTI1XkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_SX300.jpg"
///  }
/// </summary>
public class OmdbMovieShortDetails
{
    public string? Title { get; set; }

    public string? Year { get; set; }

    public string? imdbID { get; set; }

    public string? Type { get; set; }

    public string? Poster { get; set; }
}
