namespace MoviePicks.Api.Routing;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;

public static class EndpointRouteBuilderExtensions
{
    public static void MapEndpoints(this WebApplication app)
    {
        MapApiEndpoints(app);
        MapObservabilityEndpoints(app);

        if (app.Environment.IsDevelopment())
        {
            MapDevelopmentExceptionEndpoint(app);
            MapTestEndpoints(app);
        }
    }

    private static void MapApiEndpoints(WebApplication app)
    {
        app.MapControllers();
    }

    private static void MapObservabilityEndpoints(WebApplication app)
    {
        // Add Aspire telemetry endpoints
        app.MapDefaultEndpoints();
    }

    // Development-only error handler used by UseExceptionHandler to return detailed error responses.
    private static void MapDevelopmentExceptionEndpoint(WebApplication app)
    {
        app.MapGet(ExceptionHandlingRoutes.DevelopmentException, async (HttpContext httpContext) =>
        {
            var exceptionFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionFeature?.Error;

            var logger = httpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError(exception, "Unhandled exception occurred in development.");

            return Results.Problem(
                detail: exception?.Message,
                statusCode: StatusCodes.Status500InternalServerError,
                title: "An error occurred");
        });
    }

    // Theese endpoints are for testing and development only.
    private static void MapTestEndpoints(WebApplication app)
    {
        app.MapGet("/test-exception", (HttpContext _) =>
        {
            throw new Exception("Test exception");
            return Results.Problem(); // unreachable, but helps metadata
        });

        app.MapGet("/test-bad-status-code-handler/{statusCode}", (int statusCode) => Results.StatusCode(statusCode));
    }
}
