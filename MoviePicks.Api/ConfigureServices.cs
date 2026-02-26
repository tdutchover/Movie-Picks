namespace MovieRatingsBackendWebApi.Extensions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MovieRatingsBackendWebApi.Models;
using MovieRatingsBackendWebApi.Shared;
using MovieRatingsBackendWebApi.Services.ThirdPartyApiClients;
using MovieRatingsBackendWebApi.Services.BusinessServices;
using MovieRatingsBackendWebApi.Repositories.Core;
using MovieRatingsBackendWebApi.Repositories;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(
        this IServiceCollection services,
        IConfiguration configurationManager,
        IWebHostEnvironment environment)
    {
        // Add the IProblemDetailsService implementation that is used by
        // both ExceptionHandlerMiddleware and UseStatusCodePagesMiddleware
        // to provide ProblemDetails responses.
        services.AddProblemDetails();

        services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICompositeMovieService, CompositeMovieService>();
        services.AddScoped<IMovieRepository, DbMovieRepository>();    // database repository service
        services.AddScoped<IOmdbApiMovieReader, OmdbApiMovieReader>();

        // Secrets are configured as follows:
        //      Development environment: secrets are retrieved from the local secrets.json file on a developer's machine.
        //      Production environment:  secrets are retrieved from environment variables.
        // The following secrets are used by this backend service:
        //      OMDB API Key
        //      database connection string
        services.AddDbContext<DbMovieContext>(options => options.UseSqlServer(configurationManager.GetConnectionString("movieDatabaseSqlServer")));

        // Alternate SQLite DB for possible use during deployment in case SQLServer hosting costs money
        //builder.Services.AddDbContext<DbMovieContext>(options => options.UseSqlite("Data Source=MovieDb.db"));

        services.AddHttpClient();   // Needed to call web api. Registers service IHttpClientFactory

        // We don't allow Cors during non-development mode to keep highest security during production
        if (environment.IsDevelopment())
        {
            // Enabling Cors during development only, allows this backend HTTP API server to
            // respond successfully to HTTP requests from the front-end UI project.
            // In particular, before adding CORS for development, the front-end calls to
            // OmdbController failed whether the request came from JavaScript
            // or from Swagger API tests in the browser.
            services.AddCorsPolicyForOmdbController();
        }

        return services;
    }

    private static void AddCorsPolicyForOmdbController(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(
                name: BackendConstants.CorsPolicyName_For_TravisMovieRatings_Project,
                configurePolicy: corsPolicyBuilder =>
                {
                    const string clientOriginForTravisMovieRatingsProject = "http://localhost:5173";

                    corsPolicyBuilder.WithOrigins(clientOriginForTravisMovieRatingsProject)
                          .AllowAnyHeader()
                          .WithMethods("GET");
                });
        });
    }
}
