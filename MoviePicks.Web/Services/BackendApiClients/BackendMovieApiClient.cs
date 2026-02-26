namespace TravisMovieRatings.Services.BackendApiClients;

using Microsoft.AspNetCore.Mvc;
using MoviePicks.Web.Models;
using MR.Models;
using MR.Models.DTOs;
using MR.Models.Enums;
using System.Text.Json;
using TravisMovieRatings.Models;
using TravisMovieRatings.Shared;

public class BackendMovieApiClient : IBackendMovieApiClient
{
    private const string BaseUrl = "http://localhost:5053/api/CompositeMovie/";
    private static readonly TimeSpan FifteenSecondTimeout = TimeSpan.FromSeconds(15);
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ILogger<BackendMovieApiClient> logger;

    public BackendMovieApiClient(IHttpClientFactory httpClientFactory, ILogger<BackendMovieApiClient> logger)
    {
        this.httpClientFactory = httpClientFactory;
        this.logger = logger;
    }

    public async Task<List<GenreDTO>> GetAllGenres()
    {
        var httpClient = this.CreateHttpClient();
        string relativeUri = $"GetAllGenres";
        using HttpResponseMessage response = await httpClient.GetAsync(relativeUri);
        response.EnsureSuccessStatusCode();

        var genreList = await response.Content.ReadFromJsonAsync<List<GenreDTO>>();

        if (genreList == null)
        {
            // TODO: This should never occur.
            //       Log the error and return an empty list instead of throwing an exception
            genreList = new List<GenreDTO>();
        }

        return genreList;
    }

    public async Task AddMovie(MovieViewModel movieViewModel)
    {
        HttpClient httpClient = this.CreateHttpClient();
        string relativeUri = $"AddMovie";

        // TODO Change to use this more refined API that sends only a Movie object instead of the larger MovieViewModel
        //      using HttpResponseMessage httpResponse = await httpClient.PostAsJsonAsync<Movie>(relativeUri, movieViewModel.ToMovie());
        using HttpResponseMessage httpResponse = await httpClient.PostAsJsonAsync(relativeUri, movieViewModel);

        httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
    }

    public async Task<bool> DeleteMovie(int movieId)
    {
        HttpClient httpClient = this.CreateHttpClient();
        string relativeUri = $"DeleteMovie/{movieId}";

        using HttpResponseMessage httpResponse = await httpClient.DeleteAsync(relativeUri);
        httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
        return true;
    }

    public async Task<List<CompositeMovie>> GetAllMovies()
    {
        throw new NotImplementedException("RemoteCompositeMovieRepository::GetAllMovies not implemented.");
    }

    public async Task<List<MovieViewModel>> GetAllMovieViewModels()
    {
        HttpClient httpClient = this.CreateHttpClient();
        string relativeUrl = $"GetAllMovieViewModels";

        using HttpResponseMessage httpResponse = await httpClient.GetAsync(relativeUrl);
        httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299

        var movieViewModels = await httpResponse.Content.ReadFromJsonAsync<List<MovieViewModel>>();

        if (movieViewModels == null)
        {
            throw new Exception("Failed to retrieve movie information");
        }

        return movieViewModels;
    }

    public async Task<List<MovieViewModel>> GetFilteredMovieViewModels(MovieFilterFormModel filterCriteria)
    {
        string queryString = BuildMovieFilterQueryString(filterCriteria);
        string relativeUrl = $"GetFilteredMovieViewModels{queryString}";

        HttpClient httpClient = this.CreateHttpClient();
        using HttpResponseMessage httpResponse = await httpClient.GetAsync(relativeUrl);

        if (!httpResponse.IsSuccessStatusCode)
        {
            var errorContent = await httpResponse.Content.ReadAsStringAsync();
            var baseErrorMessage = "Failed to retrieve movie information.";

            try
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(errorContent);

                if (problemDetails != null)
                {
                    this.logger.LogError(
                        "{BaseErrorMessage} Problem: {ProblemTitle}, Detail: {ProblemDetail}",
                        baseErrorMessage,
                        problemDetails.Title,
                        problemDetails.Detail);

                    throw new HttpRequestException($@"
                        {baseErrorMessage} 
                        Problem: {problemDetails.Title}, 
                        Detail: {problemDetails.Detail}");
                }
            }
            catch (JsonException ex) // Fallback if the content is not a valid ProblemDetails JSON
            {
                this.logger.LogError(
                    ex,
                    "{BaseErrorMessage} Status code: {StatusCode}, Response: {Response}",
                    baseErrorMessage,
                    httpResponse.StatusCode,
                    errorContent);

                throw new HttpRequestException($@"
                    {baseErrorMessage} 
                    Status code: {httpResponse.StatusCode}, 
                    Response: {errorContent}, 
                    Error: {ex.Message}");
            }
        }

        var movieViewModels = await httpResponse.Content.ReadFromJsonAsync<List<MovieViewModel>>();

        if (movieViewModels == null)
        {
            throw new InvalidOperationException("Failed to deserialize the movie view models.");
        }

        return movieViewModels;
    }

    public async Task<MovieViewModel> GetMovieViewModel(int movieId, PlotSize plotSize)
    {
        HttpClient httpClient = this.CreateHttpClient();
        string relativeUrl = $"GetMovieViewModel/{movieId}?plotSize={plotSize.ToString()}";

        using HttpResponseMessage httpResponse = await httpClient.GetAsync(relativeUrl);
        httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299

        var movieViewModel = await httpResponse.Content.ReadFromJsonAsync<MovieViewModel>();

        if (movieViewModel == null)
        {
            throw new Exception("Failed to retrieve movie information");
        }

        return movieViewModel;
    }

    public async Task UpdateMovie(MovieDTO movieDTO)
    {
        HttpClient httpClient = this.CreateHttpClient();
        string relativeUri = $"UpdateMovie";

        using HttpResponseMessage httpResponse = await httpClient.PutAsJsonAsync(relativeUri, movieDTO);
        httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
    }

    private static string BuildMovieFilterQueryString(MovieFilterFormModel filterCriteria)
    {
        var queryParameters = new List<KeyValuePair<string, string>>();

        if (filterCriteria.Rating.HasValue)
        {
            queryParameters.Add(KeyValuePair.Create("rating", filterCriteria.Rating.Value.ToString()));
        }

        foreach (var genre in filterCriteria.Genres)
        {
            queryParameters.Add(KeyValuePair.Create("genres", genre));
        }

        queryParameters.Add(KeyValuePair.Create(nameof(MovieFilterCriteriaBase.GenreFilterMode), filterCriteria.GenreFilterMode.ToString()));

        // Using QueryString.Create for URL encoding and concatenation
        var queryString = QueryString.Create(queryParameters).ToString();

        return queryString;
    }

    private HttpClient CreateHttpClient()
    {
        return this.httpClientFactory.CreateClient(FrontendConstants.HttpClientNameTags.BackendMovieApiClientName);
    }
}