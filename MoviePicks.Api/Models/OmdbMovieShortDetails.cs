namespace MovieRatingsBackendWebApi.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Represents the limited information for a particular movie that appears in the results when searching for a movie title pattern.
/// Below is an example of the JSON structure for a movie search result.
/// {
///     "Title": "The Avengers",
///     "Year": "2012",
///     "imdbID": "tt0848228",
///     "Type": "movie",
///     "Poster": "https://m.media-amazon.com/images/M/MV5BNDYxNjQyMjAtNTdiOS00NGYwLWFmNTAtNThmYjU5ZGI2YTI1XkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_SX300.jpg"
/// }
/// </summary>
public class OmdbMovieShortDetails
{
    [JsonPropertyName("Title")]
    public string? Title { get; set; }

    [JsonPropertyName("Year")]
    public string? Year { get; set; }

    [JsonPropertyName("imdbID")]
    public string? imdbID { get; set; }

    [JsonPropertyName("Type")]
    public string? Type { get; set; }

    [JsonPropertyName("Poster")]
    public string? Poster { get; set; }
}
