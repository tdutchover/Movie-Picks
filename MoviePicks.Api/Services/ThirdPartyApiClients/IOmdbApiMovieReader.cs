namespace MovieRatingsBackendWebApi.Services.ThirdPartyApiClients;

using MovieRatingsBackendWebApi.Models;
using MR.Models.Enums;

/// <summary>
/// Service to read public information about movies
/// </summary>
public interface IOmdbApiMovieReader
{
    Task<List<OmdbMovieShortDetails>> SearchMoviesByTitle(string title);

    Task<OmdbMovieDetails> GetMovieByImdbId(string imdbId, PlotSize plotSize);
}
