using MoviePicks.Api;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.ConfigureServices(builder.Configuration, builder.Environment);

var app = builder.Build();

app.MapDefaultEndpoints();
app.ConfigureMiddleware();

app.Run();
