using Microsoft.Extensions.Options;
using MoviePicks.Api.Startup;
using MoviePicks.Api.Routing;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.ConfigureServices(builder.Configuration, builder.Environment);

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();

try
{
    app.ConfigureMiddleware();
    app.MapEndpoints();
    app.Run();
}
catch (OptionsValidationException ex)
{
    logger.LogCritical(ex, "Application failed to start due to invalid configuration.");
    throw;
}
