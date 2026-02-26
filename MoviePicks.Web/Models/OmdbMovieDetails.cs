namespace TravisMovieRatings.Models;

/// <summary>
/// Verbose details for a single movie.
/// 
/// The following is an example of the JSON represented by this class as returned by the following URL,
/// that uses query string parameter t:
///     http://www.omdbapi.com/?apikey=<OMDB_API_KEY>&t=Spider-man.
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
    // The following JSON results from searching for a specific movie
    //  by title (using option t):        http://www.omdbapi.com/?apikey=<OMDB_API_KEY>&t=Spider-man
    //  or by imdbID (using option i):    http://www.omdbapi.com/?apikey=<OMDB_API_KEY>&i=tt0145487

    public string Title { get; set; }
    public string Year { get; set; }
    public string Rated { get; set; }
    public string Released { get; set; }
    public string Runtime { get; set; }
    public string Genre { get; set; }           // Example values: "Action, Adventure, Sci-Fi",
    public string Director { get; set; }
    public string Writer { get; set; }          // Example values: "Stan Lee, Steve Ditko, David Koepp",
    public string Actors { get; set; }  // Example values: "Tobey Maguire, Kirsten Dunst, Willem Dafoe",
    public string Plot { get; set; }
    public string Language { get; set; }
    public string Country { get; set; }
    public string Awards { get; set; }
    public string Poster { get; set; }  // Example value: "https://m.media-amazon.com/images/M/MV5BZDEyN2NhMjgtMjdhNi00MmNlLWE5YTgtZGE4MzNjMTRlMGEwXkEyXkFqcGdeQXVyNDUyOTg3Njg@._V1_SX300.jpg"

    /// Example Ratings JSON array
    /// 
    /// "Ratings": [
    ///     {
    ///         "Source": "Internet ObsoleteMultireview_Movie Database",
    ///         "Value": "7.4/10"
    ///     },
    ///     {
    ///         "Source": "Rotten Tomatoes",
    ///         "Value": "90%"
    ///     },
    ///     {
    ///         "Source": "Metacritic",
    ///         "Value": "73/100"
    ///     }
    /// ],
    public OmdbRating[]? Ratings { get; set; }

    public string? Metascore { get; set; }  //Example value: "73"
    public string? imdbRating { get; set; } //Example value: "7.4"
    public string? imdbVotes { get; set; }  //Example value: "810,392"
    public string? imdbID { get; set; }        //Example value: "tt0145487"
    public string? Type { get; set; }        //Example value: "movie"
    public string? DVD { get; set; }        //Example value: "01 Nov 2002"
    public string? BoxOffice { get; set; }    //Example value: "$407,022,860"
    public string? Production { get; set; } //Example value "N/A"
    public string? Website { get; set; }    //Example value: "N/A"
    public string? Response { get; set; }    //Value is "True" if the movie was found. "False" if movie not found.
}
