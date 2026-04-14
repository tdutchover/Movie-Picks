namespace MoviePicks.Api.Infrastructure.ThirdPartyApiClients;

using MoviePicks.Contracts.DTOs;
using MoviePicks.Contracts.Enums;

/// <summary>
/// Service to read public information about movies
/// </summary>
public interface IOmdbApiMoviesReader
{
    Task<List<OmdbMovieShortDetailsDto>> SearchMoviesByTitle(string title);

    Task<OmdbMovieDetailsDto> GetMovieByImdbId(string imdbId, PlotSize plotSize);
}
