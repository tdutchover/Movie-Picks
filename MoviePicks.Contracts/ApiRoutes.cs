namespace MoviePicks.Contracts;

/// <summary>
/// Provides a centralized definition of HTTP API route templates.
/// </summary>
public static class ApiRoutes
{
    private const string ApiRoot = "api";

    public static class Movies
    {
        public const string Base = $"{ApiRoot}/movies";

        public const string Genres = "genres";
        public const string Filter = "filter";
    }

    public static class Omdb
    {
        public const string Base = $"{ApiRoot}/omdb";

        public const string Search = "movies";
        public const string Detail = "movie";
    }
}
