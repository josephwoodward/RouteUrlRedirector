using System;
using System.Collections.Generic;

namespace RouteUrlRedirector.Configuration
{
    public class RouteRedirectionConfiguration : IRouteRedirectAction, IRouteOptions, IRoutePermanency
    {
        private IDictionary<string, RouteItem> _routes { get; }
        private string _beforePath;

        public RouteRedirectionConfiguration()
        {
            _routes = new Dictionary<string, RouteItem>();
        }

        public IRouteOptions ForPath(string beforePath)
        {
            _beforePath = beforePath;
            _routes.Add(_beforePath, new RouteItem());

            return this;
        }

        public IRoutePermanency RedirectTo(string afterPath)
        {
            RouteItem rs = _routes[_beforePath];
            if (rs != null)
                _routes[_beforePath].afterUrl = afterPath;

            return this;
        }

        public IRoutePermanency RedirectTo(Func<string> action)
        {
            RouteItem rs = _routes[_beforePath];
            if (rs != null)
                _routes[_beforePath].Action = action;

            return this;
        }

        public IDictionary<string, RouteItem> BuildOptions()
        {
            return _routes;
        }

        public void Permanently()
        {
            _routes[_beforePath].PermanencyType = RoutePermanencyType.Permanently;
        }

        public void Temporarily()
        {
            _routes[_beforePath].PermanencyType = RoutePermanencyType.Temporarily;
        }
    }
}