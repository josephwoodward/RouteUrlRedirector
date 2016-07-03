using System;

namespace RouteUrlRedirector
{
    public interface IRouteOptions
    {
        IRoutePermanency RedirectTo(string afterUrl);

        IRoutePermanency RedirectTo(Func<string> action);
    }
}