namespace TravisMovieRatings.Services;

using MoviePicks.Web.Models;
using TravisMovieRatings.Models;
using TravisMovieRatings.Services.BackendApiClients;

public class MoviesService : IMoviesService
{
    private readonly IBackendMovieApiClient backendMovieApiClient;

    public MoviesService(IBackendMovieApiClient backendMovieApiClient)
    {
        this.backendMovieApiClient = backendMovieApiClient;
    }

    /// <summary>
    /// Retrieves a list of all movies and all genres.
    /// </summary>
    public async Task<MoviesViewModel> FetchAllMovieViewModelsAsync()
    {
        return await this.FetchViewModelsAsync(() => this.backendMovieApiClient.GetAllMovieViewModels());
    }

    /// <summary>
    /// Retrieves a list of all movies that satisfies the specified filter criteria, and all genres.
    /// </summary>
    public async Task<MoviesViewModel> FetchFilteredMovieViewModelsAsync(MovieFilterFormModel filterCriteria)
    {
        return await this.FetchViewModelsAsync(() => this.backendMovieApiClient.GetFilteredMovieViewModels(filterCriteria));
    }

    private async Task<MoviesViewModel> FetchViewModelsAsync(Func<Task<List<MovieViewModel>>> fetchMoviesFunc)
    {
        var moviesTask = fetchMoviesFunc();
        var genresTask = this.backendMovieApiClient.GetAllGenres();
        await Task.WhenAll(moviesTask, genresTask);

        var viewModel = new MoviesViewModel
        {
            Movies = await moviesTask,
            Genres = await genresTask,
        };

        return viewModel;
    }
}
