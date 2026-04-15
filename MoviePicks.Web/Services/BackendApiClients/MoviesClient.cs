namespace MoviePicks.Web.Services.BackendApiClients;

using Microsoft.AspNetCore.Mvc;
using MoviePicks.Contracts;
using MoviePicks.Contracts.DTOs;
using MoviePicks.Contracts.Enums;
using MoviePicks.Web.Models;
using System.Text.Json;

public class MoviesClient : IMoviesClient
{
    private static readonly TimeSpan FifteenSecondTimeout = TimeSpan.FromSeconds(15);
    private readonly HttpClient httpClient;
    private readonly ILogger<MoviesClient> logger;

    public MoviesClient(HttpClient httpClient, ILogger<MoviesClient> logger)
    {
        this.httpClient = httpClient;
        this.logger = logger;
    }

    public async Task<List<GenreDto>> GetAllGenresAsync()
    {
        const string path = ApiRoutes.App.Movies.Paths.Genres;
        using HttpResponseMessage response = await this.httpClient.GetAsync(path);
        response.EnsureSuccessStatusCode();

        var genreList = await response.Content.ReadFromJsonAsync<List<GenreDto>>();

        if (genreList == null)
        {
            // TODO: This should never occur.
            //       Log the error and return an empty list instead of throwing an exception
            genreList = new List<GenreDto>();
        }

        return genreList;
    }

    public async Task CreateMovieAsync(MovieViewModel movieViewModel)
    {
        const string path = $"{ApiRoutes.App.Movies.Paths.Movies}";
        using HttpResponseMessage httpResponse = await this.httpClient.PostAsJsonAsync(path, movieViewModel);

        // TODO Change to use this more refined API that sends only a Movie object instead of the larger MovieViewModel
        //      using HttpResponseMessage httpResponse = await httpClient.PostAsJsonAsync<Movie>(path, movieViewModel.ToMovie());
        //using HttpResponseMessage httpResponse = await httpClient.PostAsJsonAsync(path, movieViewModel);

        httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
    }

    public async Task<bool> DeleteMovieAsync(int movieId)
    {
        string path = $"{ApiRoutes.App.Movies.Paths.Movies}/{movieId}";
        using HttpResponseMessage httpResponse = await this.httpClient.DeleteAsync(path);
        httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
        return true;
    }

    public async Task<List<MovieViewModel>> GetAllMoviesAsync()
    {
        const string path = $"{ApiRoutes.App.Movies.Paths.Movies}";
        using HttpResponseMessage httpResponse = await this.httpClient.GetAsync(path);
        httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299

        var movieViewModels = await httpResponse.Content.ReadFromJsonAsync<List<MovieViewModel>>();

        if (movieViewModels == null)
        {
            throw new Exception("Failed to retrieve movie information");
        }

        return movieViewModels;
    }

    public async Task<List<MovieViewModel>> GetFilteredMovies(MovieFilterFormModel filterCriteria)
    {
        string queryString = BuildMovieFilterQueryString(filterCriteria);
        string path = $"{ApiRoutes.App.Movies.Paths.Filter}{queryString}";

        using HttpResponseMessage httpResponse = await this.httpClient.GetAsync(path);

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

    public async Task<MovieViewModel> GetMovieAsync(int movieId, PlotSize plotSize)
    {
        string path = $"{ApiRoutes.App.Movies.Paths.Movies}/{movieId}?plotSize={plotSize.ToString()}";

        using HttpResponseMessage httpResponse = await this.httpClient.GetAsync(path);
        httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299

        var movieViewModel = await httpResponse.Content.ReadFromJsonAsync<MovieViewModel>();

        if (movieViewModel == null)
        {
            throw new Exception("Failed to retrieve movie information");
        }

        return movieViewModel;
    }

    public async Task UpdateMovieAsync(MovieDto movieDTO)
    {
        const string path = $"{ApiRoutes.App.Movies.Paths.Movies}";
        using HttpResponseMessage httpResponse = await this.httpClient.PutAsJsonAsync(path, movieDTO);
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
}