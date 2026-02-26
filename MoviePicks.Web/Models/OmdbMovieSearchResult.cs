namespace TravisMovieRatings.Models;

/// <summary>
/// List of results when searching for a movie by title. A search by title
/// can result in multiple movies with a similar title.
///
/// The following is an example of the JSON represented by this class as returned by the following URL,
/// that uses query string parameter s:
///     http://www.omdbapi.com/?apikey=<OMDB_API_KEY>&s=forrest+gump
///     http://www.omdbapi.com/?apikey=<OMDB_API_KEY>&s=Spider-man
///
/// {
///   "Search": [
///       {
///           "Title": "Forrest Gump",
///           "Year": "1994",
///           "imdbID": "tt0109830",
///           "Type": "movie",
///           "Poster": "https://m.media-amazon.com/images/M/MV5BNWIwODRlZTUtY2U3ZS00Yzg1LWJhNzYtMmZiYmEyNmU1NjMzXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_SX300.jpg"
///       },
///       {
///           "Title": "Through the Eyes of Forrest Gump",
///           "Year": "1994",
///           "imdbID": "tt0234886",
///           "Type": "movie",
///           "Poster": "https://m.media-amazon.com/images/M/MV5BNmI0ZDdjOWEtYjQyMy00OTQ2LTkwNmUtZjJiNzY4M2MyZDVlXkEyXkFqcGdeQXVyODcyODYwMTQ@._V1_SX300.jpg"
///       },
///       {
///           "Title": "Getting Past Impossible: Forrest Gump and the Visual Effects Revolution",
///           "Year": "2009",
///           "imdbID": "tt1507284",
///           "Type": "movie",
///           "Poster": "N/A"
///       }
///   ],
///   "totalResults": "3",
///   "Response": "True"
/// }
///
/// </summary>
public class OmdbMovieSearchResult
{
    public OmdbMovieShortDetails[]? search { get; set; }

    // Example value "144"
    public string? totalResults { get; set; }

    // Example value "True"
    public string? Response { get; set; }

    public void MovieSearch()
    {
        // string url = "http://www.omdbapi.com/?s=" + txtMovieName.Text.Trim() + "&apikey=XXXX"; using (WebClient wc = new WebClient()) { string json = wc.DownloadString(url); JavaScriptSerializer oJS = new JavaScriptSerializer(); ImdbEntity_Array movies2 = oJS.Deserialize<imdbentity_array>(json); var movies = oJS.Serialize(movies2); foreach (var mov in movies2.Search) { TableRow row = new TableRow(); TableCell cell1 = new TableCell(); TableCell cell2 = new TableCell(); TableCell cell3 = new TableCell(); TableCell cell4 = new TableCell(); TableCell cell5 = new TableCell(); TableCell cell6 = new TableCell(); cell1.Text = "Poster"; cell2.Text = mov.Title; cell2.Font.Size = 15; cell3.Text = mov.Year; cell4.Text = mov.imdbRating; cell5.Text = mov.imdbID; cell6.Text = "Button"; row.Cells.Add(cell1); row.Cells.Add(cell2); row.Cells.Add(cell3); row.Cells.Add(cell4); row.Cells.Add(cell5); row.Cells.Add(cell6); Image img = new Image(); img.Width = 70; img.ImageUrl = mov.Poster; myTable.Rows.Add(row); myTable.Rows[myTable.Rows.Count - 1].Cells[0].Controls.Add(img); } }
        // ASP Code: <asp:Table ID="myTable" runat="server" Width="100%" Height="48px"><asp:tablerow><asp:tablecell><asp:tablecell>Title<asp:tablecell>Year<asp:tablecell>IMDB Rating<asp:tablecell>IMDB ID<asp:tablecell>Add<asp:HiddenField ID="btnAdd" runat="server" />
    }
}
