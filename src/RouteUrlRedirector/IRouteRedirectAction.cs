namespace RouteUrlRedirector
{
    public interface IRouteRedirectAction
    {
        IRouteOptions ForPath(string beforePath);
    }
}