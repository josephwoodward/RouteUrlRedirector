using System;

namespace RouteUrlRedirector.RedirectMiddleware
{
    public interface IRouteOptions
    {
        IRoutePermanency RedirectTo(string afterPath);

        IRoutePermanency RedirectTo(Func<string> action);
    }
}