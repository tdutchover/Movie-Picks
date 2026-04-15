using MoviePicks.Contracts;

var builder = DistributedApplication.CreateBuilder(args);

var backendApi = builder.AddProject<Projects.MoviePicks_Api>(
    AspireResourceName
        .ServiceDiscovery
        .Project
        .MoviePicksApi);

// Frontend web UI project
builder.AddProject<Projects.MoviePicks_Web>(
    AspireResourceName
        .AppHostOnly
        .Project
        .MoviePicksWeb)
    .WithReference(backendApi);

builder.Build().Run();