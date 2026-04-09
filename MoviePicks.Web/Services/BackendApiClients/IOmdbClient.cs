using MoviePicks.Contracts.DTOs;

namespace MoviePicks.Web.Services.BackendApiClients
{
    public interface IOmdbClient
    {
        Task<IEnumerable<OmdbMovieShortDetailsDto>> SearchOmdbMoviesByTitlePatternAsync(string titlePattern);

        Task<OmdbMovieDetailsDto> GetMovieByImdbIdAsync(string imdbId);
    }
}
