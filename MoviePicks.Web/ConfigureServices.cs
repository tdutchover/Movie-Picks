namespace TravisMovieRatings.Extensions;

using TravisMovieRatings.Services;
using TravisMovieRatings.Services.BackendApiClients;
using TravisMovieRatings.Shared;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        // Add services to the container.
        services.AddControllersWithViews();

        // Needed to call web api. Registers service IHttpClientFactory

        // Registers a named HttpClient for BackendMovieApiClient. Utilizes IHttpClientFactory for efficient
        // management and lifecycle handling of HttpClient instances. This approach ensures optimal resource
        // usage and addresses common issues such as DNS changes over time.

        // Registers a named HttpClient with the tag "BackendMovieApiClient". This setup leverages IHttpClientFactory for
        // efficient HttpClient management and lifecycle handling, ensuring optimal resource usage and mitigating
        // common issues like DNS changes over time.

        services.AddHttpClient(FrontendConstants.HttpClientNameTags.BackendMovieApiClientName, client =>
        {
            client.BaseAddress = new Uri("http://localhost:5053/api/CompositeMovie/");
#if DEBUG
            client.Timeout = Timeout.InfiniteTimeSpan; // No timeout for debugging
#else
            client.Timeout = TimeSpan.FromSeconds(15); // Production timeout
#endif
        });

        services.AddScoped<IBackendMovieApiClient, BackendMovieApiClient>();
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