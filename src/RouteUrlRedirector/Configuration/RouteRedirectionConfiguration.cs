using System;
using System.Collections.Generic;

namespace RouteUrlRedirector.Configuration
{
    public class RouteRedirectionConfiguration : IRouteRedirectAction, IRouteOptions, IRoutePermanency
    {
        private IDictionary<string, RouteItem> _routes { get; }
        private string _beforeUrl;

        public RouteRedirectionConfiguration()
        {
            _routes = new Dictionary<string, RouteItem>();
        }

        public IRouteOptions ForPath(string beforeUrl)
        {
            _beforeUrl = beforeUrl;
            _routes.Add(_beforeUrl, new RouteItem());

            return this;
        }

        public IRoutePermanency RedirectTo(string afterUrl)
        {
            RouteItem rs = _routes[_beforeUrl];
            if (rs != null)
                _routes[_beforeUrl].afterUrl = afterUrl;

            return this;
        }

        public IRoutePermanency RedirectTo(Func<string> action)
        {
            RouteItem rs = _routes[_beforeUrl];
            if (rs != null)
                _routes[_beforeUrl].Action = action;

            return this;
        }

        public IDictionary<string, RouteItem> BuildOptions()
        {
            return _routes;
        }

        public void Permanently()
        {
            _routes[_beforeUrl].PermanencyType = RoutePermanencyType.Permanently;
        }

        public void Temporarily()
        {
            _routes[_beforeUrl].PermanencyType = RoutePermanencyType.Temporarily;
        }
    }
}