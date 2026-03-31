var builder = DistributedApplication.CreateBuilder(args);

var backendApi = builder.AddProject<Projects.MoviePicks_Api>("moviepicks-api");

// Frontend web UI project
builder.AddProject<Projects.MoviePicks_Web>("moviepicks-web")
	.WithReference(backendApi);

builder.Build().Run();
