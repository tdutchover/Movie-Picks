namespace MoviePicks.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using MoviePicks.Web.Models;
using MoviePicks.Web.Shared;
using System.Diagnostics;

/// <summary>
/// Manages the display of the splash screen on the first visit and redirects
/// a returning user to the landing page.
/// </summary>
public class HomeController : Controller
{
    private readonly ILogger<HomeController> logger;

    public HomeController(ILogger<HomeController> logger)
    {
        this.logger = logger;
    }

    public IActionResult Index()
    {
        if (this.IsFirstVisitForThisBrowser())
        {
            this.MarkVisitForThisBrowser();

            // Display splash screen
            return this.SplashScreen();
        }
        else
        {
            return this.RedirectToAction("Gallery", "Movies");
        }
    }

    public IActionResult SplashScreen()
    {
        return this.View("Index");
    }

    /// <remarks>
    /// [ResponseCache] ensures error pages are not cached to always reflect the current state of the application.
    /// </remarks>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
    }

    /// <summary>
    /// Determines if this is the first visit to the app by checking for a specific cookie in the browser.
    /// </summary>
    /// <returns>True if this is the first visit by the browser.</returns>
    private bool IsFirstVisitForThisBrowser()
    {
        return !this.Request.Cookies.ContainsKey(FrontendConstants.CookieNames.HasVisitedThisApp);
    }

    /// <summary>
    /// Mark the app as visited by setting a specific cookie in the browser.
    /// </summary>
    private void MarkVisitForThisBrowser()
    {
        this.Response.Cookies.Append(FrontendConstants.CookieNames.HasVisitedThisApp, "true", new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddDays(1),
        });
    }
}