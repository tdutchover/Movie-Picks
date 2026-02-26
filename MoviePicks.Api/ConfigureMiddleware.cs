namespace MovieRatingsBackendWebApi.Extensions;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRatingsBackendWebApi.Models;
using System.Net.Http;
using System;

public static partial class WebApplicationExtensions
{
    private static readonly string DevExceptionHandlerEndpointPath = "/development-exception-handler";

    public static void ConfigureMiddleware(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            ConfigureDevelopmentMiddleware(app);
            ConfigureTestingEndpoints(app);
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

        app.MapControllers();
    }

    private static void ConfigureDevelopmentMiddleware(WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseExceptionHandler(DevExceptionHandlerEndpointPath); // Routes exceptions to the minimal API endpoint
        ConfigureExceptionHandlingEndpoint(app);

        // Configure Cross-Origin Resource Sharing (CORS) policies.
        // The order of UseCors in the middleware pipeline is critical:
        // 1. Place UseCors before components that rely on cross-origin requests, such as controllers or endpoints.
        // 2. In this backend Web API, UseCors is placed before UseAuthorization to ensure that CORS policies are applied to all incoming requests, including those requiring authorization.
        // For more information, see:
        // - See Enable Cross-Origin Requests (CORS) in ASP.NET Core at https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-9.0
        // - See Middleware order at: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-9.0#middleware-order
        app.UseCors();
    }

    // This minimal API endpoint returns the prepared ProblemDetails response when an exception occurs.
    // This endpoint is only used in development mode.
    private static void ConfigureExceptionHandlingEndpoint(WebApplication app)
    {
        // Custom error handling for development mode with detailed information and logging
        app.MapGet(DevExceptionHandlerEndpointPath, async (HttpContext httpContext) =>
          {
            var exceptionFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionFeature?.Error;

            // Retrieve a logger instance using the built-in ASP.NET Core logging
            var logger = httpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError(exception, "Unhandled exception occurred in development.");

            var problemDetails = new ProblemDetails
                  {
                    Title = "An error occurred",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = exception?.Message // Detailed error information for development
                  };

            return Results.Problem(
                detail: problemDetails.Detail,
                title: problemDetails.Title,
                statusCode: problemDetails.Status);
                });
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

    private static void ConfigureTestingEndpoints(WebApplication app)
    {
        // The following minimal API endpoints are used for testing and development only.
        app.MapGet("/test-exception", (HttpContext context) => throw new Exception("Test exception"));
        app.MapGet("/test-bad-status-code-handler/{statusCode}", (int statusCode) => Results.StatusCode(statusCode));
    }
}
