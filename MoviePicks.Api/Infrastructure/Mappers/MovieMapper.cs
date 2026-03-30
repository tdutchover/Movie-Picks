namespace MoviePicks.Api.Infrastructure.Mappers;

using MoviePicks.Api.Models;
using MoviePicks.Contracts.DTOs;

public static class MovieMapper
{
    public static Movie ToMovie(this MovieDto movieDTO)
    {
        return new Movie()
        {
            Id = movieDTO.Id,
            ImdbId = movieDTO.ImdbId,
            Rating = movieDTO.Rating,
            ReviewHeading = movieDTO.ReviewHeading,
            ReviewComments = movieDTO.ReviewComments,
        };
    }
}
