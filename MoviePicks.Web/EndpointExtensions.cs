using MoviePicks.Contracts.DTOs;
using MoviePicks.Web.Services.BackendApiClients;
using Serilog;

namespace MoviePicks.Web;

/// <summary>
/// Provides extension methods for mapping proxy endpoints that are intended to be called
/// exclusively by the frontend JavaScript client. These endpoints forward requests to backend
/// OMDB-related APIs, simplify client-side code, resolve backend URLs using Aspire, and eliminate
/// the need for CORS handling by keeping all browser requests within the same origin.
/// </summary>
public static partial class EndpointExtensions
{
    /// <summary>
    /// Maps proxy endpoints for OMDB movie search and details retrieval.
    /// </summary>
    public static void MapProxyEndpoints(this WebApplication app)
    {
        // Proxy endpoint for searching OMDB movies by title pattern (route parameter)..
        app.MapGet("/api/proxy/omdb/movies/{titlePattern}", async (string titlePattern, IBackendMovieApiClient client) =>
        {
            try
            {
                IEnumerable<OmdbMovieShortDetailsDto> movieResults = await client.SearchOmdbMoviesByTitlePatternAsync(titlePattern);
                return Results.Ok(movieResults);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Proxy failed to fetch OMDB metadata for pattern: {TitlePattern}", titlePattern);
                return Results.Problem("The movie service is currently unavailable.");
            }
        });

        // Proxy endpoint for retrieving OMDB movie details by IMDb ID (query parameter).
        app.MapGet("/api/proxy/omdb/movie", async (string imdbId, IBackendMovieApiClient client) =>
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
