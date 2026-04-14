namespace MoviePicks.Api.Infrastructure.ThirdPartyApiClients;

using Microsoft.Extensions.Options;
using MoviePicks.Api.Configuration;

/// <summary>
/// Registers OMDb API integration components, including configuration validation
/// and the HTTP client used to communicate with the external service.
/// </summary>
public static class OmdbClientServiceCollectionExtensions
{
    /// <summary>
    /// Adds OMDb configuration and HTTP client to the service container.
    /// Fails fast during application startup if configuration is invalid.
    /// </summary>
    public static IServiceCollection AddOmdbIntegration(this IServiceCollection services, IConfiguration configuration)
    {
        // Validate OMDb configuration at startup so invalid settings fail fast,
        // instead of causing runtime errors when the client is first used.
        // The built-in [Url] attribute is intentionally not used because it allows
        // unsupported schemes (e.g., ftp://); this check restricts values to
        // HTTP/HTTPS URIs.
        services.AddOptions<OmdbOptions>()
            .Bind(configuration.GetSection(OmdbOptions.SectionName))
            .ValidateDataAnnotations()
            .Validate(
                options =>
                    Uri.TryCreate(options.BaseUrl, UriKind.Absolute, out var uri)
                    && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps),
                "Invalid configuration: Omdb:BaseUrl must be a valid absolute HTTP/HTTPS URI. Check appsettings.json.")
            .ValidateOnStart();

        // Configure a typed HTTP client for OMDb.
        // The base address comes from validated configuration above, so no additional checks are needed here.
        services.AddHttpClient<IOmdbApiMoviesReader, OmdbApiMoviesReader>(
            (serviceProvider, client) =>
            {
                var options = serviceProvider
                    .GetRequiredService<IOptions<OmdbOptions>>()
                    .Value;
                client.BaseAddress = new Uri(options.BaseUrl);
            });

        return services;
    }
}
