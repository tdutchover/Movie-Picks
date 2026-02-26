namespace MovieRatingsBackendWebApi.Services.ThirdPartyApiClients;

using MovieRatingsBackendWebApi.Models;
using MR.Models.Enums;

public class OmdbApiMovieReader : IOmdbApiMovieReader
{
    private const string OmdbApiBaseUri = "http://www.omdbapi.com/";
    private readonly IHttpClientFactory httpClientFactory;
    private readonly string apiKey;

    public OmdbApiMovieReader(IConfiguration configurationManager, IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
        this.apiKey = configurationManager[ConfigurationManagerKeys.OpenMovieDatabaseApiKey] ??
            throw new ArgumentNullException(nameof(configurationManager), $"Failed to access configuration for key {ConfigurationManagerKeys.OpenMovieDatabaseApiKey}");
    }

    public async Task<OmdbMovieDetails> GetMovieByImdbId(string imdbId, PlotSize plotSize)
    {
        HttpClient httpClient = this.httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(OmdbApiBaseUri);

        string url = $"?apikey={this.apiKey}&i={imdbId}";

        if (plotSize == PlotSize.Full)
        {
            url += "&plot=full";    // Retrieve full plot, not the short plot
        }

        OmdbMovieDetails? response = await httpClient.GetFromJsonAsync<OmdbMovieDetails>(url);

        if (response == null)
        {
            throw new InvalidOperationException("The JSON response is empty or invalid.");
        }

        return response;
    }

    public async Task<List<OmdbMovieShortDetails>> SearchMoviesByTitle(string title)
    {
        HttpClient httpClient = this.httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(OmdbApiBaseUri);
        var url = $"?apikey={this.apiKey}&s={title}";

        OmdbMovieSearchResult? result = await httpClient.GetFromJsonAsync<OmdbMovieSearchResult>(url);

        if (result?.search != null && result.Response?.Equals("True") == true)
        {
            return result.search.ToList();
        }
        else
        {
            // Typically will get here if the server doesn't have any movies to return because the title-pattern doesn't match any movies.
            // In this case, result will be valid. But result.Response will be "False".
            //
            // TODO: If result is null, then log something. Or perhaps throw InvalidOperationException and allow the caller to handle it and log it.
            return new List<OmdbMovieShortDetails>();
        }
    }

    private static class ConfigurationManagerKeys // TODO Move to it's own file when more keys are needed by other files
    {
        public static readonly string OpenMovieDatabaseApiKey = "OpenMovieDatabaseApiKey";
    }
}