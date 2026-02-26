using MovieRatingsBackendWebApi.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration, builder.Environment);

var app = builder.Build();
app.ConfigureMiddleware();

app.Run();
