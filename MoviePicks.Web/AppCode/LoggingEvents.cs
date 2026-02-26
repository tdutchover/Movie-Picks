namespace MoviePicks.Web.AppCode
{
    /// <summary>
    /// Provides a centralized location for defining logging event IDs used throughout the application.
    /// These event IDs help categorize and identify specific logging events, making it easier to filter
    /// and analyze log data.
    /// </summary>
    public record LoggingEvents
    {
        /// <summary>
        /// Event ID for logging actions related to managing movies.
        /// </summary>
        public const int ManageMovies = 1000;
    }
}
