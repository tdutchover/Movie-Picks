using MoviePicks.Contracts;
using MoviePicks.Contracts.DTOs;

namespace MoviePicks.Web.Services.BackendApiClients;

public class OmdbClient : IOmdbClient
{
    private readonly HttpClient httpClient;

    public OmdbClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<OmdbMovieShortDetailsDto>> SearchOmdbMoviesByTitlePatternAsync(string titlePattern)
    {
        string path = $"{ApiRoutes.Omdb.Movies.Resource}?titlePattern={titlePattern}";
        using HttpResponseMessage httpResponse = await this.httpClient.GetAsync(path);
        httpResponse.EnsureSuccessStatusCode();

        var results = await httpResponse.Content.ReadFromJsonAsync<IEnumerable<OmdbMovieShortDetailsDto>>();

        if (results == null)
        {
            throw new Exception($"Failed to retrieve results from OMDB movies matching title pattern: {titlePattern}");
        }

        return results;
    }

    public async Task<OmdbMovieDetailsDto> GetMovieByImdbIdAsync(string imdbId)
    {
        string path = $"{ApiRoutes.Omdb.Movies.Resource}/{imdbId}";
        using HttpResponseMessage httpResponse = await this.httpClient.GetAsync(path);
        httpResponse.EnsureSuccessStatusCode();

        var results = await httpResponse.Content.ReadFromJsonAsync<OmdbMovieDetailsDto>();

        if (results == null)
        {
            throw new Exception($"Failed to retrieve results from OMDB movie for imdbId: {imdbId}");
        }

        return results;
    }
}