using System;

namespace RouteUrlRedirector
{
    public interface IRouteOptions
    {
        IRoutePermanency RedirectTo(string afterPath);

        IRoutePermanency RedirectTo(Func<string> action);
    }
}