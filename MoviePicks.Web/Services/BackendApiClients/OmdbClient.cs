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
        string relativeUrl = $"{titlePattern}";
        using HttpResponseMessage httpResponse = await this.httpClient.GetAsync(relativeUrl);
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
        string relativeUrl = $"?imdbId={imdbId}";
        using HttpResponseMessage httpResponse = await this.httpClient.GetAsync(relativeUrl);
        httpResponse.EnsureSuccessStatusCode();

        var results = await httpResponse.Content.ReadFromJsonAsync<OmdbMovieDetailsDto>();

        if (results == null)
        {
            throw new Exception($"Failed to retrieve results from OMDB movie for imdbId: {imdbId}");
        }

        return results;
    }
}