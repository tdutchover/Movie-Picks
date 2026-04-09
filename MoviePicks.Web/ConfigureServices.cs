namespace MoviePicks.Web;

using MoviePicks.Contracts;
using MoviePicks.Web.Services;
using MoviePicks.Web.Services.BackendApiClients;
using MoviePicks.Web.Shared;
using System.Runtime.Intrinsics.Arm;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        // Add services to the container.
        services.AddControllersWithViews();

        // Needed to call web api. Registers service IHttpClientFactory

        // Registers a named HttpClient for MoviesClient. Utilizes IHttpClientFactory for efficient
        // management and lifecycle handling of HttpClient instances. This approach ensures optimal resource
        // usage and addresses common issues such as DNS changes over time.

        // Registers a named HttpClient with the tag "MoviesClient". This setup leverages IHttpClientFactory for
        // efficient HttpClient management and lifecycle handling, ensuring optimal resource usage and mitigating
        // common issues like DNS changes over time.

        {
            string apiServiceHost = AspireResourceName
                    .ServiceDiscovery
                    .Project
                    .MoviePicksApi;

            services.AddHttpClient<IMoviesClient, MoviesClient>(client =>
            {
                client.BaseAddress = new Uri($"https+http://{apiServiceHost}/{ApiRoutes.Movies.Base}/");
#if DEBUG
                client.Timeout = Timeout.InfiniteTimeSpan; // No timeout for debugging
#else
                client.Timeout = TimeSpan.FromSeconds(15); // Production timeout
#endif
            });

            services.AddHttpClient<IOmdbClient, OmdbClient>(client =>
            {
                client.BaseAddress = new Uri($"https+http://{apiServiceHost}/{ApiRoutes.Omdb.Base}/");
#if DEBUG
                client.Timeout = Timeout.InfiniteTimeSpan; // No timeout for debugging
#else
                client.Timeout = TimeSpan.FromSeconds(15); // Production timeout
#endif
            });
        }

        services.AddScoped<IMoviesService, MoviesService>();

        // Configuration for Hsts
        // Later, consider changing this to short term (e.g. 30 days) for development and long term (365 days) for production.
        //services.Configure<HstsOptions>(options =>
        //{
        //    options.MaxAge = TimeSpan.FromMilliseconds(1);    // Previously set to 1ms to clear hsts from chrome browser
        //    options.IncludeSubDomains = true;
        //    options.Preload = true;
        //});

        return services;
    }
}