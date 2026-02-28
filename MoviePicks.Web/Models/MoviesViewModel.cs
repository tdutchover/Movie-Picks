namespace MoviePicks.Web.Models;

using MoviePicks.Contracts.DTOs;

public class MoviesViewModel
{
    public IEnumerable<MovieViewModel> Movies { get; set; }

    public IEnumerable<GenreDTO> Genres { get; set; }
}
