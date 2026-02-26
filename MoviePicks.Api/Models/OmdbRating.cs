namespace MovieRatingsBackendWebApi.Models;

/// <summary>
/// This represents one element of a JSON array.
/// The following is an example parent JSON array.
/// 
/// "Ratings": [
///		{
///			"Source": "Internet ObsoleteMultireview_Movie Database",
///			"Value": "7.4/10"
///		},
///		{
///         "Source": "Rotten Tomatoes",
///			"Value": "90%"
///
///        },
///		{
///         "Source": "Metacritic",
///			"Value": "73/100"
///
///        }
///    ],
/// </summary>
public class OmdbRating
{
    public string? Source { get; set; } // Example value: "Internet ObsoleteMultireview_Movie Database",

    public string? Value { get; set; } // Example value: "7.4/10"
}
