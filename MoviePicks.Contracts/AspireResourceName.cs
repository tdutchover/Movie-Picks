namespace MoviePicks.Contracts;

public static class AspireResourceName
{
    /// <summary>
    /// Virtual hostnames used by the Aspire Service Discovery mechanism
    /// to resolve the network address of backend projects.
    /// </summary>
    public static class ServiceDiscovery
    {
        public static class Project
        {
            public const string MoviePicksApi = "moviepicks-api";
        }
    }

    /// <summary>
    /// Resource identifiers used exclusively within the AppHost
    /// for lifecycle management and orchestration.
    /// </summary>
    public static class AppHostOnly
    {
        public static class Project
        {
            public const string MoviePicksWeb = "moviepicks-web";
        }
    }
}