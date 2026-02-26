namespace TravisMovieRatings.Models;

using System.ComponentModel.DataAnnotations;
using System.Text;

public class MovieViewModel
{
    private const string EmptyString = "";

    // The purpose of this constructor is to ensure that each property is not null by default.
    public MovieViewModel()
    {
        this.ImdbId = EmptyString;
        this.Rating = 0;
        this.ReviewHeading = EmptyString;
        this.ReviewComments = EmptyString;
        this.Title = EmptyString;
        this.Year = EmptyString;
        this.Rated = EmptyString;
        this.Released = EmptyString;
        this.Runtime = EmptyString;
        this.Genre = EmptyString;
        this.Director = EmptyString;
        this.Writer = EmptyString;
        this.Actors = EmptyString;
        this.Plot = EmptyString;
        this.Language = EmptyString;
        this.Country = EmptyString;
        this.Awards = EmptyString;
        this.Poster = EmptyString;
        this.Type = EmptyString;
    }

    //[HiddenInput(DisplayValue = false)] // TODO: Enable to Hide this property from display
    [Display(Name = "Movie ID")] // TODO: Not needed here when property is hidden
    public int MovieId { get; set; }

    [Display(Name = "IMDB ID")]
    public string ImdbId { get; set; }

    // This will go into a separate Review class when multiple users are allowed because then a dto can have multiple reviews.
    #region Movie Rating Information

    [Range(1, 10)]
    [Display(Name = "Personal Rating")]
    public int Rating { get; set; }

    [Display(Name = "Review Heading")]
    [DataType(DataType.Text)]
    [MaxLength(80, ErrorMessage = "Review heading too long")]
    public string? ReviewHeading { get; set; }

    [Display(Name = "Review Comments")]
    [DataType(DataType.MultilineText)]
    [MaxLength(300, ErrorMessage = "Review comments too long")]
    public string? ReviewComments { get; set; }

    #endregion Movie Rating Information

    // The following information are dto details from OMDB
    public string Title { get; set; }

    [Display(Name = "Year")]
    public string Year { get; set; }

    public string Rated { get; set; }

    public string Released { get; set; }

    public string Runtime { get; set; }

    // Example values: "Action, Adventure, Sci-Fi",
    public string Genre { get; set; }

    [Display(Name = "Directed by")]
    public string Director { get; set; }

    // Example values: "Stan Lee, Steve Ditko, David Koepp",
    [Display(Name = "Writers")]
    public string Writer { get; set; }

    // Example values: "Tobey Maguire, Kirsten Dunst, Willem Dafoe",
    [Display(Name = "Starring")]
    public string Actors { get; set; }

    [Display(Name = "PlotSize")]
    [DataType(DataType.MultilineText)]
    [MaxLength(300, ErrorMessage = "Description too long")]
    public string Plot { get; set; }

    [Display(Name = "Languages")]
    public string Language { get; set; }

    public string Country { get; set; }

    public string Awards { get; set; }

    // Example value: "https://m.media-amazon.com/images/M/MV5BZDEyN2NhMjgtMjdhNi00MmNlLWE5YTgtZGE4MzNjMTRlMGEwXkEyXkFqcGdeQXVyNDUyOTg3Njg@._V1_SX300.jpg",
    public string Poster { get; set; }

    // Example Ratings JSON array
    //
    // "Ratings": [
    //     {
    //         "Source": "Internet ObsoleteMultireview_Movie Database",
    //         "Value": "7.4/10"
    //     },
    //     {
    //         "Source": "Rotten Tomatoes",
    //         "Value": "90%"
    //     },
    //     {
    //         "Source": "Metacritic",
    //         "Value": "73/100"
    //     }
    // ],
    // public OmdbRating[]? Ratings { get; set; }

    // public string? Metascore { get; set; }  //Example value: "73"
    // public string? imdbRating { get; set; } //Example value: "7.4"
    // public string? imdbVotes { get; set; }  //Example value: "810,392"
    // public string? imdbID { get; set; }     //Example value: "tt0145487"
    public string Type { get; set; }        //Example value: "movie"

    // public string? DVD { get; set; }        //Example value: "01 Nov 2002"
    // public string? BoxOffice { get; set; }  //Example value: "$407,022,860"
    // public string? Production { get; set; } //Example value "N/A"
    // public string? Website { get; set; }    //Example value: "N/A"
    // public string? Response { get; set; }   //Value is "True" if the dto was found. "False" if dto not found.

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"MovieId: {this.MovieId}");
        sb.AppendLine($"imdbID: {this.ImdbId}");
        sb.AppendLine($"Rating: {this.Rating}");
        sb.AppendLine($"Review Heading: {this.ReviewHeading}");
        sb.AppendLine($"Review Comments: {this.ReviewComments}");

        sb.AppendLine($"Title: {this.Title}");
        sb.AppendLine($"Year: {this.Year}");
        sb.AppendLine($"Rated: {this.Rated}");
        sb.AppendLine($"Released: {this.Released}");
        sb.AppendLine($"Runtime: {this.Runtime}");
        sb.AppendLine($"Genre: {this.Genre}");
        sb.AppendLine($"Director: {this.Director}");
        sb.AppendLine($"Writer: {this.Writer}");
        sb.AppendLine($"Actors: {this.Actors}");
        sb.AppendLine($"PlotSize: {this.Plot}");
        sb.AppendLine($"Language: {this.Language}");
        sb.AppendLine($"Country: {this.Country}");
        sb.AppendLine($"Awards: {this.Awards}");
        sb.AppendLine($"Poster: {this.Poster}");
        sb.AppendLine($"Type: {this.Type}");

        return sb.ToString();
    }
}
