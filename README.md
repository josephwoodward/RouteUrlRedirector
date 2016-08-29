# Route URL Redirector Middleware for ASP.NET Core MVC

A super-simple fluent API for preserving your website's link juice and traffic by redirecting legacy routes in ASP.NET Core MVC.
 

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        ...
        app.UseRouteUrlRedirect(redirect =>
        {
            // Permanently redirect (emits 301 redirect to search engines)
            redirect.ForPath("/5/legacy-post-url/").RedirectTo("/new-post-url/").Permanently();

            // Temporary redirect (emits 302 redirect to search engines)
            redirect.ForPath("/6/temporary-redirect/").RedirectTo("/temporary-redirect/").Temporarily();
        });
        ...
    }

## Need to query a database for a URL? We've got you covered!

If you need to query a database or other location for a URL then you can also do the following:

    app.UseRouteUrlRedirect(redirect =>
    {
        redirect.ForPath("/6/temporary-redirect/").RedirectTo(DeferredQueryDbForPath).Temporarily();
    });

    private string DeferredQueryDbForPath(string oldPath){
        /* Query database for new path only if old path is hit */
        return newPath;
    }

**Note**: The lookup method will only be executed if, and only if the `ForPath` URL is hit.

## Installation

You can install Route URL Redirector by copying and pasting the following command into your Package Manager Console within Visual Studio (Tools > NuGet Package Manager > Package Manager Console).

`Install-Package RouteUrlRedirector`

Then all you need to do is call the `app.UseRouteUrlRedirect(...)` extension method in your `Configure` method within `Startup.cs` to add Route URL Redirector to your pipeline and configure your URLs as above.