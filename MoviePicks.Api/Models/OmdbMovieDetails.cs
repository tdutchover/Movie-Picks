namespace MovieRatingsBackendWebApi.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Verbose details for a single movie
///
/// The following is an example of the JSON represented by this class as returned by the following URL,
/// that uses query string parameter
/// http://www.omdbapi.com/?apikey=<API_KEY><OMDB_API_KEY>&t=Spider-man
///
/// {
///     "Title": "Spider-Man",
///     "Year": "2002",
///     "Rated": "PG-13",
///     "Released": "03 May 2002",
///     "Runtime": "121 min",
///     "Genre": "Action, Adventure, Sci-Fi",
///     "Director": "Sam Raimi",
///     "Writer": "Stan Lee, Steve Ditko, David Koepp",
///     "Actors": "Tobey Maguire, Kirsten Dunst, Willem Dafoe",
///     "Plot": "After being bitten by a genetically-modified spider, a shy teenager gains spider-like abilities that he uses to fight injustice as a masked superhero and face a vengeful enemy.",
///     "Language": "English",
///     "Country": "United States",
///     "Awards": "Nominated for 2 Oscars. 16 wins & 63 nominations total",
///     "Poster": "https://m.media-amazon.com/images/M/MV5BZDEyN2NhMjgtMjdhNi00MmNlLWE5YTgtZGE4MzNjMTRlMGEwXkEyXkFqcGdeQXVyNDUyOTg3Njg@._V1_SX300.jpg",
///     "Ratings": [
///         {
///             "Source": "Internet ObsoleteMultireview_Movie Database",
///             "Value": "7.4/10"
///         },
///         {
///             "Source": "Rotten Tomatoes",
///             "Value": "90%"
///         },
///         {
///             "Source": "Metacritic",
///             "Value": "73/100"
///         }
///     ],
///     "Metascore": "73",
///     "imdbRating": "7.4",
///     "imdbVotes": "810,392",
///     "imdbID": "tt0145487",
///     "Type": "movie",
///     "DVD": "01 Nov 2002",
///     "BoxOffice": "$407,022,860",
///     "Production": "N/A",
///     "Website": "N/A",
///     "Response": "True"
/// }
/// </summary>
public class OmdbMovieDetails
{
    [JsonPropertyName("Title")]
    public string Title { get; set; }

    [JsonPropertyName("Year")]
    public string Year { get; set; }

    [JsonPropertyName("Rated")]
    public string Rated { get; set; }

    [JsonPropertyName("Released")]
    public string Released { get; set; }

    [JsonPropertyName("Runtime")]
    public string Runtime { get; set; }

    [JsonPropertyName("Genre")]
    public string Genre { get; set; }

    [JsonPropertyName("Director")]
    public string Director { get; set; }

    [JsonPropertyName("Writer")]
    public string Writer { get; set; }

    [JsonPropertyName("Actors")]
    public string Actors { get; set; }

    [JsonPropertyName("Plot")]
    public string Plot { get; set; }

    [JsonPropertyName("Language")]
    public string Language { get; set; }

    [JsonPropertyName("Country")]
    public string Country { get; set; }

    [JsonPropertyName("Awards")]
    public string Awards { get; set; }

    [JsonPropertyName("Poster")]
    public string Poster { get; set; }

    [JsonPropertyName("Ratings")]
    public OmdbRating[]? Ratings { get; set; }

    [JsonPropertyName("Metascore")]
    public string? Metascore { get; set; }

    [JsonPropertyName("imdbRating")]
    public string? imdbRating { get; set; }

    [JsonPropertyName("imdbVotes")]
    public string? imdbVotes { get; set; }

    [JsonPropertyName("imdbID")]
    public string? imdbID { get; set; }

    [JsonPropertyName("Type")]
    public string? Type { get; set; }

    [JsonPropertyName("DVD")]
    public string? DVD { get; set; }

    [JsonPropertyName("BoxOffice")]
    public string? BoxOffice { get; set; }

    [JsonPropertyName("Production")]
    public string? Production { get; set; }

    [JsonPropertyName("Website")]
    public string? Website { get; set; }

    [JsonPropertyName("Response")]
    public string? Response { get; set; }
}
