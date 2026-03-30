namespace MoviePicks.Web.Infrastructure;

using MoviePicks.Contracts.DTOs;
using MoviePicks.Web.Models;

public static class MovieViewModelMapper
{
    public static MovieDto ToMovieDTO(this MovieViewModel movieViewModel)
    {
        return new MovieDto()
        {
            Id = movieViewModel.MovieId,
            ImdbId = movieViewModel.ImdbId,
            Rating = movieViewModel.Rating,
            ReviewHeading = movieViewModel.ReviewHeading,
            ReviewComments = movieViewModel.ReviewComments,
        };
    }
}
