# Route URL Redirector Middleware for ASP.NET Core
###### A beautifully simple fluent API for preserving your website's link juice and traffic by redirecting legacy routes in ASP.NET Core MVC

    app.UseRouteUrlRedirect(redirect =>
    {
        redirect.ForPath("/5/legacy-post-url/").RedirectTo("/new-post-url/").Permanently();
        redirect.ForPath("/6/temporary-redirect/").RedirectTo("/temporary-redirect/").Temporarily();
    });
