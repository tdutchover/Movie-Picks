namespace MoviePicks.Api.Startup;

using MoviePicks.Api.Models;
using MoviePicks.Api.Routing;

public static partial class WebApplicationExtensions
{
    public static void ConfigureMiddleware(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            ConfigureDevelopmentMiddleware(app);
        }
        else
        {
            ConfigureProductionMiddleware(app);
        }

        // Adds a Problem Details body to any response that has an error status code
        // and no body. This middleware means that api endpoint handlers don't have
        // to manually create ProblemDetails responses for error status codes,
        // unless they want to customize the response.
        app.UseStatusCodePages();

        // The database migration technique is mutually exclusive with this EnsureCreated technique.
        // As a separate technique, DB migration will create the database and incrementally change the database as I change my model classes.
        // That in contrast to the following code that requires deleting the database and re-creating it whenever there are any model class
        // changes.
        //
        // Therefore, if DB migration technique is used, then:
        //  1. disable lines 37 to 42 below
        //  2. Keep the database service configuration above because that is also used by the DB migration technique.
        using (var scope = app.Services.CreateScope()) // This must go after line app.Environment...
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<DbMovieContext>();

            // dbContext.Database.EnsureDeleted();   // Enable this to delete the old database if it exists
            dbContext.Database.EnsureCreated();     // Creates database, associated with DbMovieContext, only if it doesn't exist already
        }

        app.UseAuthorization();
    }

    private static void ConfigureDevelopmentMiddleware(WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseExceptionHandler(ExceptionHandlingRoutes.DevelopmentException); // Routes exceptions to the minimal API endpoint
    }

    private static void ConfigureProductionMiddleware(WebApplication app)
    {
        // ExceptionHandlerMiddleware doesn't leak sensitive information in production.
        //
        // Automatically converts all exceptions to Problem Details responses.
        // This ExceptionHandlerMiddleware performs this behavior because no error-handling
        // path is specified. This ExceptionHandlerMiddleware uses registered service
        // IProblemDetailsService to provide the ProblemDetails response.
        app.UseExceptionHandler();
    }
}
