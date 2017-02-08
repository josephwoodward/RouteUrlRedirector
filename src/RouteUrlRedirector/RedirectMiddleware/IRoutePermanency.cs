namespace RouteUrlRedirector.RedirectMiddleware
{
    public interface IRoutePermanency
    {
        void Permanently();

        void Temporarily();
    }
}