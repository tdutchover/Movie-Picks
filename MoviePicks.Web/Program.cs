using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using TravisMovieRatings.Extensions;

var builder = WebApplication.CreateBuilder(args);

// TODO: Configure Serilog from a file
// builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// Serilog website:     https://serilog.net/
Log.Logger = new LoggerConfiguration()

    .Enrich.FromLogContext()    // Enrichment documentation: https://github.com/serilog/serilog/wiki/Enrichment
    .WriteTo.Console()          // Add console as logging target

    // Want to easily find warnings and higher severity in a separate log file.
    // Log file content written in JSON as an example only. Not for any practical purpose.
    .WriteTo.File(new JsonFormatter(), "Logs/important-logs.json", restrictedToMinimumLevel: LogEventLevel.Warning)

    // Add a log file that will be replaced by a new log file each day
    .WriteTo.File("Logs/all-daily-.logs", rollingInterval: RollingInterval.Day)

    .MinimumLevel.Debug()       // Set default minimum log level
    .CreateLogger();

Log.Logger.Warning("First log using serilog in Program.cs");

// Requires NuGet package "Serilog.AspNetCore"
// builder.Host.UseSerilog((context, configuration) => 
//    configuration.ReadFrom.Configuration(context.Configuration));
// Also see the following (old article) to add context to Serilog logs that was there in the standard .NET logger
// Title: "Logging the selected Endpoint Name with Serilog : Using Serilog.AspNetCore in ASP.NET Core 3.0 - Part 2"
//          https://andrewlock.net/using-serilog-aspnetcore-in-asp-net-core-3-logging-the-selected-endpoint-name-with-serilog/

// Another Serilog article
// Title: "Configure Serilog in ASP.NET Core – few practical tips Posted by Mario Mucalo on November 21, 2020"
//      https://mariomucalo.com/configure-serilog-in-asp-net-core-few-practical-tips/

// Standard .NET logger configuration.
// Output log files to the command window for this project. Replaces default config for storing log files.
// builder.Host.ConfigureLogging(logging =>
// {
//    logging.ClearProviders();
//    logging.AddConsole();
// });

builder.Services.ConfigureServices();

var app = builder.Build();
app.ConfigureMiddleware();

app.Run();
