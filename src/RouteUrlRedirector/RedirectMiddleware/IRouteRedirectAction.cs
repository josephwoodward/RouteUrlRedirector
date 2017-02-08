namespace RouteUrlRedirector.RedirectMiddleware
{
    public interface IRouteRedirectAction
    {
        IRouteOptions ForPath(string beforePath);
    }
}