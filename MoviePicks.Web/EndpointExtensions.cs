using MoviePicks.Contracts.DTOs;
using MoviePicks.Web.Services.BackendApiClients;
using Serilog;

namespace MoviePicks.Web
{
    public static partial class EndpointExtensions
    {
        public static void MapProxyEndpoint(this WebApplication app)
        {
            // This is a proxy endpoint for the JavaScript to invoke to get OMDB movies using a title pattern search.
            // This simplifies client-side code by allowing the proxy to resolve the named backend URL using Aspire.
            // It also elminates the need for backend CORS handling because the Javascript calls the front-end's
            // proxy endpoint which then calls the backend API, keeping all calls within the same origin from the browser's perspective.
            app.MapGet("/api/proxy/omdb/movies/{titlePattern}", async (string titlePattern, IBackendMovieApiClient client) =>
            {
                try
                {
                    IEnumerable<OmdbMovieShortDetailsDTO> movieResults = await client.SearchOmdbMoviesByTitlePatternAsync(titlePattern);
                    return Results.Ok(movieResults);
                }
                catch (Exception ex)
                {
                    // This hits your Serilog 'important-logs.json' because it's an Error
                    Log.Error(ex, "Proxy failed to fetch OMDB metadata for pattern: {TitlePattern}", titlePattern);

                    // Returns a 500 status with a clean message for the browser
                    return Results.Problem("The movie service is currently unavailable.");
                }
            });

            app.MapGet("/api/proxy/omdb/movie", async (string imdbId, IBackendMovieApiClient client) =>
            {
                try
                {
                    OmdbMovieDetailsDto movieResult = await client.GetMovieByImdbIdAsync(imdbId);
                    return Results.Ok(movieResult);
                }
                catch (Exception ex)
                {
                    // This hits your Serilog 'important-logs.json' because it's an Error
                    Log.Error(ex, "Proxy failed to fetch movie from OMDB for pattern: {imdbId}", imdbId);

                    // Returns a 500 status with a clean message for the browser
                    return Results.Problem("The movie service is currently unavailable.");
                }
            });
        }
    }
}
