namespace MoviePicks.Api.Services.ThirdPartyApiClients;

using MoviePicks.Api.Models;
using MoviePicks.Contracts.DTOs;
using MoviePicks.Contracts.Enums;

/// <summary>
/// Service to read public information about movies
/// </summary>
public interface IOmdbApiMovieReader
{
    Task<List<OmdbMovieShortDetailsDTO>> SearchMoviesByTitle(string title);

    Task<OmdbMovieDetailsDto> GetMovieByImdbId(string imdbId, PlotSize plotSize);
}
