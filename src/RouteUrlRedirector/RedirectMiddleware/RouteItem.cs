using System;

namespace RouteUrlRedirector.RedirectMiddleware
{
    public class RouteItem
    {
        public string AfterPath { private get; set; }

        public Func<string> Action { private get; set; }

        public string Result => AfterPath ?? Action();

        public RoutePermanencyType PermanencyType { get; set; }
    }
}