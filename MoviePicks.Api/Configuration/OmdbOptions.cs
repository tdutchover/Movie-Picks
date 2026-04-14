namespace MoviePicks.Api.Configuration;

using System.ComponentModel.DataAnnotations;

public class OmdbOptions
{
    public const string SectionName = "Omdb";

    [Required]
    [MinLength(1)]
    public string BaseUrl { get; set; } = string.Empty;
}
