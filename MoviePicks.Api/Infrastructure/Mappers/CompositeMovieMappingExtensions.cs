namespace MovieRatingsBackendWebApi.Infrastructure.Mappers;

using MovieRatingsBackendWebApi.Models;

public static class CompositeMovieMappingExtensions
{
    public static MovieViewModel ToMovieViewModel(this CompositeMovie cmovie)
    {
        MovieViewModel vm = new MovieViewModel();

        vm.MovieId = cmovie.Movie.Id;
        vm.ImdbId = cmovie.Movie.ImdbId;
        vm.Rating = cmovie.Movie.Rating;
        vm.ReviewHeading = cmovie.Movie.ReviewHeading;
        vm.ReviewComments = cmovie.Movie.ReviewComments;

        vm.Title = cmovie.MovieDetails.Title;
        vm.Year = cmovie.MovieDetails.Year;
        vm.Rated = cmovie.MovieDetails.Rated;
        vm.Released = cmovie.MovieDetails.Released;
        vm.Runtime = cmovie.MovieDetails.Runtime;
        vm.Genre = cmovie.MovieDetails.Genre;
        vm.Director = cmovie.MovieDetails.Director;
        vm.Writer = cmovie.MovieDetails.Writer;
        vm.Actors = cmovie.MovieDetails.Actors;
        vm.Plot = cmovie.MovieDetails.Plot;
        vm.Language = cmovie.MovieDetails.Language;
        vm.Country = cmovie.MovieDetails.Country;
        vm.Awards = cmovie.MovieDetails.Awards;
        vm.Poster = cmovie.MovieDetails.Poster;
        vm.Type = cmovie.MovieDetails.Type;
        return vm;
    }
}
