namespace MovieRatingsBackendWebApi.Infrastructure.Mappers;

using MovieRatingsBackendWebApi.Models;
using MR.Models.DTOs;

public static class MovieMapper
{
    public static Movie ToMovie(this MovieDTO movieDTO)
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
