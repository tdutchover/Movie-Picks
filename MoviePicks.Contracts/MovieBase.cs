namespace MR.Models;

using System.ComponentModel.DataAnnotations;


public class MovieBase : IIdentifiable
{
    // Primary key
    // [Key]     <- This attribute is not required because the property name follows the naming convention for a primary key
    [Display(Name = "Movie Id")]
    public int Id { get; set; }

    public string? ImdbId { get; set; }

    // Movie rating information is below.
    // This will go into a separate Review class when multiple users are allowed because then a movie can have multiple reviews.

    [Display(Name = "Rating")]
    public int Rating { get; set; }

    [Display(Name = "Review Heading")]
    [DataType(DataType.Text)]
    [MaxLength(80, ErrorMessage = "Heading too long")]
    public string? ReviewHeading { get; set; }

    [Display(Name = "Review Comments")]
    [DataType(DataType.MultilineText)]
    [MaxLength(300, ErrorMessage = "Review comments too long")]
    public string? ReviewComments { get; set; }
}
