# Route Url Redirector Middleware for ASP.NET Core
###### A beautifully simple fluent API for preserving your link juice and traffic by redirecting legacy routes in ASP.NET Core MVC

    app.UseRouteUrlRedirect(redirect =>
    {
        redirect.ForUrl("/5/legacy-post-url/").RedirectTo("/new-post-url/").Permanently();
        redirect.ForUrl("/6/temporary-redirect/").RedirectTo("/temporary-redirect/").Temporarily();
    });
