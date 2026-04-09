namespace MoviePicks.Web.Extensions;

using MoviePicks.Contracts.DTOs;
using MoviePicks.Web.Services.BackendApiClients;
using Serilog;

/// <summary>
/// Provides extension methods for mapping proxy endpoints that are intended to be called
/// exclusively by the frontend JavaScript moviesClient. These endpoints forward requests to backend
/// OMDB-related APIs, simplify moviesClient-side code, resolve backend URLs using Aspire, and eliminate
/// the need for CORS handling by keeping all browser requests within the same origin.
/// </summary>
public static partial class ProxyEndpointExtensions
{
    /// <summary>
    /// Maps proxy endpoints for OMDB movie search and details retrieval.
    /// </summary>
    public static void MapProxyEndpoints(this WebApplication app)
    {
        var omdbProxy = app.MapGroup("/api/proxy/omdb");

        // Proxy endpoint for searching OMDB movies by title pattern (route parameter)..
        omdbProxy.MapGet("/movies/{titlePattern}", async (string titlePattern, IOmdbClient client) =>
        {
            try
            {
                IEnumerable<OmdbMovieShortDetailsDto> movieResults =
                    await client.SearchOmdbMoviesByTitlePatternAsync(titlePattern);

                return Results.Ok(movieResults);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Proxy failed to fetch OMDB metadata for pattern: {TitlePattern}", titlePattern);
                return Results.Problem("The movie service is currently unavailable.");
            }
        });

        // Proxy endpoint for retrieving OMDB movie details by IMDb ID (query parameter).
        omdbProxy.MapGet($"/movie", async (string imdbId, IOmdbClient client) =>
        {
            try
            {
                OmdbMovieDetailsDto movieResult = await client.GetMovieByImdbIdAsync(imdbId);
                return Results.Ok(movieResult);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Proxy failed to fetch movie from OMDB for pattern: {imdbId}", imdbId);
                return Results.Problem("The movie service is currently unavailable.");
            }
        });
    }
}
