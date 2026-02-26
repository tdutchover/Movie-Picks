namespace TravisMovieRatings.Models;

using MR.Models.DTOs;

public class MoviesViewModel
{
    public IEnumerable<MovieViewModel> Movies { get; set; }

    public IEnumerable<GenreDTO> Genres { get; set; }
}
