namespace RouteUrlRedirector
{
    public interface IRouteRedirectAction
    {
        IRouteOptions ForUrl(string beforeUrl);
    }
}