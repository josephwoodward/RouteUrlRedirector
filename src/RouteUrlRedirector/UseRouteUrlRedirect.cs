using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RouteUrlRedirector.Configuration;

namespace RouteUrlRedirector
{
    public class UseRouteUrlRedirect
    {
        private readonly RequestDelegate _next;
        private readonly Action<RouteRedirectionConfiguration> _routeConfigurations;
        private IDictionary<string, RouteItem> _builtRouteItems;

        public UseRouteUrlRedirect(RequestDelegate next, Action<RouteRedirectionConfiguration> routeConfigurations)
        {
            _next = next;
            _routeConfigurations = routeConfigurations;
        }

        public Task Invoke(HttpContext context)
        {
            if (_builtRouteItems == null)
            {
                var config = new RouteRedirectionConfiguration();
                _routeConfigurations(config);
                _builtRouteItems = config.BuildOptions();
            }

            if (!context.Request.Path.HasValue || !_builtRouteItems.ContainsKey(context.Request.Path.Value))
                return _next(context);

            RouteItem newPath = _builtRouteItems[context.Request.Path.Value];
            context.Response.Redirect(newPath.Result, newPath.PermanencyType == RoutePermanencyType.Permanently);

            return _next(context);
        }
    }
}