namespace MoviePicks.Web.Infrastructure;

using MR.Models.DTOs;
using TravisMovieRatings.Models;

public static class MovieViewModelMapper
{
    public static MovieDTO ToMovieDTO(this MovieViewModel movieViewModel)
    {
        return new MovieDTO()
        {
            Id = movieViewModel.MovieId,
            ImdbId = movieViewModel.ImdbId,
            Rating = movieViewModel.Rating,
            ReviewHeading = movieViewModel.ReviewHeading,
            ReviewComments = movieViewModel.ReviewComments,
        };
    }
}
