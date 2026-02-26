namespace MR.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Core attributes of a movie genre.
/// </summary>
public class GenreCore : IIdentifiable
{
    [Display(Name = "Genre Id")]
    public int Id { get; set; }

    public string Name { get; set; }
}
