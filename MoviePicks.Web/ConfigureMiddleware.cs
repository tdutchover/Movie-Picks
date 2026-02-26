namespace TravisMovieRatings.Extensions;

using Serilog;
using System.Diagnostics;   // For .Debug class

public static partial class WebApplicationExtensions
{
    public static void ConfigureMiddleware(this WebApplication app)
    {
        // Don't use Hsts yet until this app has an https port configured.
        // Force use of https.
        //{
        //    // For details, see:   https://www.acunetix.com/blog/articles/what-is-hsts-why-use-it/
        //    app.UseHsts();
        //    app.UseHttpsRedirection();
        //}

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
        }

        app.MapStaticAssets();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Lifetime.ApplicationStopping.Register(() => Debug.WriteLine("ApplicationStopping has been called via Debug."));
        // TODO: Must fix. This doesn't seem to be invoked to flush the Serilog as we desire.
        Action stoppingCallback = () =>
        {
            Log.CloseAndFlush();
        };
        app.Lifetime.ApplicationStopping.Register(stoppingCallback);

        app.Lifetime.ApplicationStopping.Register(() => Console.WriteLine("ApplicationStopping has been called via WriteLine."));

        //app.Lifetime.ApplicationStopping.Register(() =>
        //{
        //    //Log.Logger.Warning("Log just after Run() and before Log.CloseAndFlush();");
        //    //Console.WriteLine("Log BEFORE Log.CloseAndFlush();");

        //    Log.CloseAndFlush();
        //    //Console.WriteLine("Log AFTER Log.CloseAndFlush();");

        //    // This is just to keep the console visible when deployed in non-debug mode. Delete it after
        //    // confirming that Log.CloseAndFlush() gets invoked on closing the app.
        //    //Console.ReadLine();
        //});
    }
}
