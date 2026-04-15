using Microsoft.Extensions.Options;
using MoviePicks.Api.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.ConfigureServices(builder.Configuration, builder.Environment);

var app = builder.Build();

// Add Aspire telemetry configuration
app.MapDefaultEndpoints();

var logger = app.Services.GetRequiredService<ILogger<Program>>();

try
{
    app.ConfigureMiddleware();
    app.Run();
}
catch (OptionsValidationException ex)
{
    logger.LogCritical(ex, "Application failed to start due to invalid configuration.");
    throw;
}
