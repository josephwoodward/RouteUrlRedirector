using System;
using Microsoft.AspNetCore.Builder;
using RouteUrlRedirector.LowercaseMiddleware;
using RouteUrlRedirector.RedirectMiddleware;
using RouteUrlRedirector.TrailingSlashMiddleware;

namespace RouteUrlRedirector
{
    public static class UrlToolboxMiddlewareExtension
    {
        public static IApplicationBuilder UseRequestRedirect(this IApplicationBuilder app, Action<IRouteRedirectAction> configureRedirects)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            if (configureRedirects == null)
                throw new ArgumentNullException(nameof(configureRedirects));

            return app.UseMiddleware<RouteUrlRedirectMiddleware>(configureRedirects);
        }

        public static IApplicationBuilder ForceLowercaseUrl(this IApplicationBuilder app)
	    {
		    if (app == null)
			    throw new ArgumentNullException(nameof(app));

		    return app.UseMiddleware<ForceLowercaseUrlMiddleware>();
	    }

	    public static IApplicationBuilder ApplyTrailingSlashPolicy(this IApplicationBuilder app, TrailingSlashPolicy trailingSlashPolicy)
	    {
		    if (app == null)
			    throw new ArgumentNullException(nameof(app));

		    return app.UseMiddleware<UrlTrailingSlashMiddleware>(trailingSlashPolicy);
	    }
    }
}