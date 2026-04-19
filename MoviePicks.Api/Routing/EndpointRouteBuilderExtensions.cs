namespace MoviePicks.Api.Routing;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Scalar.AspNetCore;
using System;

public static class EndpointRouteBuilderExtensions
{
    public static void MapEndpoints(this WebApplication app)
    {
        MapApiEndpoints(app);
        MapObservabilityEndpoints(app);

        if (app.Environment.IsDevelopment())
        {
            MapOpenApiDocumentation(app);

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

    private static void MapOpenApiDocumentation(WebApplication app)
    {
        // Generates the OpenAPI JSON document that describes the API's endpoints and schemas.
        app.MapOpenApi();

        // Displays the interactive user interface that allows developers to
        // browse and test the API using the generated OpenApi document.
        app.MapScalarApiReference(options =>
        {
            options.WithTitle("Movie Picks API")
                   .WithTheme(ScalarTheme.DeepSpace) // Modern dark/orange theme
                   .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
        });
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

    // These endpoints are for infrastructure verification and are only available in development.
    private static void MapTestEndpoints(WebApplication app)
    {
        app.MapGet("/test-exception", (HttpContext _) =>
        {
            // Verified by the configured development exception handler or the 
            // standard ExceptionHandlerMiddleware in production.
            throw new Exception("Test exception");
        })
        .WithName("TestException")
        .WithSummary("Trigger a server-side exception")
        .WithDescription("Verifies that the exception handling middleware correctly captures and formats unhandled crashes into Problem Details.")
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        app.MapGet("/test-status-code/{statusCode}", (int statusCode) => Results.StatusCode(statusCode))
        .WithName("TestStatusCode")
        .WithSummary("Return any specified HTTP status code")
        .WithDescription("A pass-through endpoint to verify how the UI and middleware (like UseStatusCodePages) respond to different HTTP status results.")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}
