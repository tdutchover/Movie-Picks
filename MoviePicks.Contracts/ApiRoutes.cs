namespace MoviePicks.Contracts;

/// <summary>
/// Centralized definition of HTTP API route templates. 
/// Ensures consistency between API Controllers and HttpClient consumers.
/// </summary>
public static class ApiRoutes
{
    private const string ApiRoot = "api";

    /// <summary>Routes for internal application resources.</summary>
    public static class App
    {
        /// <summary>The logical root for all internal API calls. Use for HttpClient.BaseAddress.</summary>
        public const string Root = $"{ApiRoot}/app";

        public static class Movies
        {
            /// <summary>The URI segment identifying the movies resource.</summary>
            public const string Resource = "movies";

            /// <summary>The absolute route template for the MoviesController [Route] attribute.</summary>
            public const string Base = $"{Root}/{Resource}";

            /// <summary>Sub-resource segments for Controller action attributes.</summary>
            public const string GenresSegment = "genres";
            public const string FilterSegment = "filter";

            /// <summary>Relative paths for client-side calls.</summary>
            public const string Genres = $"{Resource}/{GenresSegment}";
            public const string Filter = $"{Resource}/{FilterSegment}";
        }
    }

    /// <summary>Routes for third-party OMDB proxy endpoints.</summary>
    public static class Omdb
    {
        /// <summary>The logical root for OMDB-related calls. Use for HttpClient.BaseAddress.</summary>
        public const string Root = $"{ApiRoot}/omdb";

        public static class Movies
        {
            /// <summary>The URI segment identifying the OMDB movies resource.</summary>
            public const string Resource = "movies";

            /// <summary>The absolute route template for the OmdbMoviesController [Route] attribute.</summary>
            public const string Base = $"{Root}/{Resource}";
        }
    }
}