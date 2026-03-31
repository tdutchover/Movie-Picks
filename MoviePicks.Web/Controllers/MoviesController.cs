namespace MoviePicks.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using MoviePicks.Contracts.DTOs;
using MoviePicks.Contracts.Enums;
using MoviePicks.Web.AppCode;
using MoviePicks.Web.Infrastructure;
using MoviePicks.Web.Models;
using MoviePicks.Web.Services;
using MoviePicks.Web.Services.BackendApiClients;

public class MoviesController : Controller
{
    private const string NotApplicable = "N/A";
    private const string NoMoviePosterAvailable = NotApplicable;
    private const string NoImageAvailablePlaceholder = "~/images/No_Image.jpg";

    private readonly IWebHostEnvironment env;
    private readonly IBackendMovieApiClient backendMovieApiClient;
    private readonly ILogger<MoviesController> logger;
    private readonly IMoviesService moviesService;

    public MoviesController(
        IWebHostEnvironment env,
        ILogger<MoviesController> logger,
        IBackendMovieApiClient backendMovieApiClient,
        IMoviesService moviesService)
    {
        this.env = env;
        this.logger = logger;
        this.backendMovieApiClient = backendMovieApiClient;
        this.moviesService = moviesService;
    }

    // Shows page to manage movies via CRUD operations.
    public async Task<ActionResult> Index()
    {
        try
        {
            this.logger.LogInformation(LoggingEvents.ManageMovies, "Manage Movies");
            List<MovieViewModel> movies = await this.backendMovieApiClient.GetAllMovieViewModels();
            return this.View(movies);
        }
        catch (Exception ex)
        {
            return this.NotFound(ex.Message);
        }
    }

    public async Task<ActionResult> Gallery()
    {
        try
        {
            MoviesViewModel viewModel = await this.moviesService.FetchAllMovieViewModelsAsync();
            this.NormalizeMoviePosters(viewModel.Movies.ToList());
            return this.View(viewModel);
        }
        catch (Exception ex)
        {
            return this.NotFound(ex.Message);  // Returns result code 404 - not found
        }
    }

    [HttpPost]
    public async Task<ActionResult> Gallery([FromForm] MovieFilterFormModel filterCriteria)
    {
        try
        {
            MoviesViewModel viewModel = await this.moviesService.FetchFilteredMovieViewModelsAsync(filterCriteria);
            this.NormalizeMoviePosters(viewModel.Movies.ToList());
            return this.View(viewModel);
        }
        catch (Exception ex)
        {
            return this.NotFound(ex.Message);  // Returns result code 404 - not found
        }
    }

    public async Task<ActionResult> Details(int movieId)
    {
        try
        {
            var movieViewModel = await this.backendMovieApiClient.GetMovieViewModel(movieId, PlotSize.Full);
            movieViewModel.Poster = this.GetMoviePosterOrDefaultNoImage(moviePosterUri: movieViewModel.Poster);
            return this.View(movieViewModel);
        }
        catch (Exception ex)
        {
            //TODO Show a nice message to the user then do something acceptable like keeping the user on the same page or route to the home page.
            //
            // Returns result code 404 - not found. This looks bad to the user because it's just a black browser page with the error message displayed.
            return this.NotFound(ex.Message);
        }
    }

    public ActionResult Create()
    {
        // This empty model object is used for:
        //  1. Generating form fields and labels with data binding and display annotations.
        //  2. Enabling client-side validation through data annotations.
        MovieViewModel movieViewModel = new MovieViewModel();
        return this.View(movieViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(MovieViewModel movieViewModel)
    {
        if (!this.ModelState.IsValid)
        {
            // TODO: Log
            return this.BadRequest(this.ModelState);
        }

        try
        {
            await this.backendMovieApiClient.AddMovie(movieViewModel);
            return this.RedirectToAction(nameof(this.Index));
        }
        catch (Exception ex)
        {
            // TODO: Log exception, then maybe don't display error in development environment.
            string developmentMsg = $"Error adding movie. {ex.Message}";
            string productionMsg = "Error adding movie.";
            this.ViewBag.AddProductErrorMessage = this.env.IsDevelopment() ? developmentMsg : productionMsg;

            // Keep user on same view showing movies, to correct the data problem.
            return this.View(movieViewModel);
        }
    }

    public async Task<ActionResult> Edit(int id)
    {
        try
        {
            MovieViewModel movieViewModel = await this.backendMovieApiClient.GetMovieViewModel(id, PlotSize.Short);
            movieViewModel.Poster = this.GetMoviePosterOrDefaultNoImage(moviePosterUri: movieViewModel.Poster);
            return this.View(movieViewModel);
        }
        catch (Exception ex)
        {
            //TODO Show a nice message to the user then do something acceptable like keeping the user on the same page or route to the home page.
            //
            // Returns result code 404 - not found. This looks bad to the user because it's just a black browser page with the error message displayed.
            return this.NotFound(ex.Message);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(MovieViewModel movieViewModel)
    {
        if (this.ModelState.IsValid)
        {
            MovieDto movieDTO = movieViewModel.ToMovieDTO();
            await this.backendMovieApiClient.UpdateMovie(movieDTO);
            return this.RedirectToAction("Index");
        }
        else
        {
            // Show persistent error message and allow the user to correct the data.
            this.ViewBag.ErrorMessage = "Error updating movie";
            return this.View(movieViewModel);
        }
    }

    // [ValidateAntiForgeryToken]
    [HttpDelete]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await this.backendMovieApiClient.DeleteMovie(id);
            this.TempData["SuccessMessage"] = "Movie deleted successfully";
            var urlToRedirect = this.Url.Action("Index", "Movies");
            return this.Json(new { redirectUrl = urlToRedirect });

        }
        catch (Exception ex)
        {
            // TODO: Log the exception here

            this.TempData["ErrorMessage"] = "An error occurred while deleting the movie.";
            var urlToRedirect = this.Url.Action("Index", "Movies");

            return this.Json(new
            {
                status = StatusCodes.Status500InternalServerError,
                redirectUrl = urlToRedirect,
            });
        }
    }

    /// <summary>
    /// Normalizes the poster URIs for a collection of movies by ensuring each URI is either a
    /// valid image URI or a default "no image" placeholder. Note: This process does not convert
    /// virtual paths (starting with '~/') to application absolute paths. The conversion to absolute paths
    /// suitable for client-side rendering should be handled externally, such as in the controller.
    /// </summary>
    /// <param name="movies">The collection of movies to normalize poster URIs for.</param>
    private void NormalizeMoviePosters(List<MovieViewModel> movies)
    {
        movies.ForEach(movie => movie.Poster = this.GetMoviePosterOrDefaultNoImage(moviePosterUri: movie.Poster));
    }

    // Determines what movie poster image to display for a movie.
    private string GetMoviePosterOrDefaultNoImage(string moviePosterUri)
    {
        if (this.IsValidMoviePosterUri(moviePosterUri))
        {
            return moviePosterUri;
        }
        else
        {
            return this.GetDefaultMoviePosterUri();
        }
    }

    private bool IsValidMoviePosterUri(string moviePosterUri)
    {
        return !string.IsNullOrEmpty(moviePosterUri) && moviePosterUri != NoMoviePosterAvailable;
    }

    private string GetDefaultMoviePosterUri()
    {
        return this.Url.Content(NoImageAvailablePlaceholder);
    }
}