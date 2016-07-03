# Route Url Redirector Middleware for ASP.NET Core
Redirect legacy routes in ASP.NET Core MVC

    app.UseRouteUrlRedirect(redirect =>
    {
      redirect.ForPath("/5/old-post-url/").RedirectTo("/new-post-url/").Permanently();
      redirect.ForPath("/6/temporary-redirect/").RedirectTo("/temporary-redirect/").Temporarily();
    });
