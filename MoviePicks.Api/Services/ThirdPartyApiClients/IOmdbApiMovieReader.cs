namespace MoviePicks.Api.Services.ThirdPartyApiClients;

using MoviePicks.Api.Models;
using MoviePicks.Contracts.Enums;

/// <summary>
/// Service to read public information about movies
/// </summary>
public interface IOmdbApiMovieReader
{
    Task<List<OmdbMovieShortDetails>> SearchMoviesByTitle(string title);

    Task<OmdbMovieDetails> GetMovieByImdbId(string imdbId, PlotSize plotSize);
}
