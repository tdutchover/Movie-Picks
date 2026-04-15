namespace MoviePicks.Contracts;

/// <summary>
/// Centralized definition of HTTP API routes.
/// Ensures consistency between API controllers and their HttpClient consumers.
///
/// Convention:
///   ControllerRoute  — absolute route for the [Route] attribute on the controller class.
///   Actions.*        — sub-segments for [HttpGet] / [HttpPost] / etc. on action methods.
///                      These are relative to ControllerRoute and must not include it.
///   Paths.*          — paths relative to the HttpClient.BaseAddress registered in DI.
///                      Used by HttpClient consumers (e.g. MoviesClient, OmdbClient).
/// </summary>
public static class ApiRoutes
{
    private const string ApiRoot = "api";

    /// <summary>Routes for internal application endpoints.</summary>
    public static class App
    {
        /// <summary>
        /// Appended to the host when constructing BaseAddress for MoviesClient.
        /// e.g. https+http://moviepicks-api/api/app/
        /// </summary>
        public const string BasePath = $"{ApiRoot}/app";

        public static class Movies
        {
            // Controller routing (API layer)
            public const string ControllerRoute = $"{BasePath}/movies";

            public static class Actions
            {
                public const string Genres = "genres";
                public const string Filter = "filter";
            }

            // HttpClient paths (relative to BaseAddress)
            public static class Paths
            {
                public const string Movies = "movies";
                public const string Genres = "movies/genres";
                public const string Filter = "movies/filter";
            }
        }
    }

    /// <summary>Routes for OMDB proxy endpoints.</summary>
    public static class Omdb
    {
        /// <summary>
        /// Appended to the host when constructing BaseAddress for OmdbClient.
        /// e.g. https+http://moviepicks-api/api/omdb/
        /// </summary>
        public const string BasePath = $"{ApiRoot}/omdb";

        public static class Movies
        {
            // Controller routing (API layer)
            public const string ControllerRoute = $"{BasePath}/movies";

            // HttpClient paths (relative to BaseAddress)
            public static class Paths
            {
                public const string Movies = "movies";
            }
        }
    }
}