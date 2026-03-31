namespace MoviePicks.Api;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MoviePicks.Api.Repositories;
using MoviePicks.Api.Repositories.Core;
using MoviePicks.Api.Services.BusinessServices;
using MoviePicks.Api.Models;
using MoviePicks.Api.Infrastructure.ThirdPartyApiClients;

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

        return services;
    }
}
