namespace MoviePicks.Web.Extensions;

using MoviePicks.Contracts;
using MoviePicks.Web.Services;
using MoviePicks.Web.Services.BackendApiClients;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        // Add services to the container.
        services.AddControllersWithViews();

        // Registers typed HttpClients that are each injected with their BaseAddress pre-configured.
        {
            string apiServiceHost = AspireResourceName
                    .ServiceDiscovery
                    .Project
                    .MoviePicksApi;

            services.AddHttpClient<IMoviesClient, MoviesClient>(client =>
            {
                client.BaseAddress = new Uri($"https+http://{apiServiceHost}/{ApiRoutes.App.BasePath}/");
#if DEBUG
                client.Timeout = Timeout.InfiniteTimeSpan; // No timeout for debugging
#else
                client.Timeout = TimeSpan.FromSeconds(15); // Production timeout
#endif
            });

            services.AddHttpClient<IOmdbClient, OmdbClient>(client =>
            {
                client.BaseAddress = new Uri($"https+http://{apiServiceHost}/{ApiRoutes.Omdb.BasePath}/");
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