using System;

namespace RouteUrlRedirector
{
    public class RouteItem
    {
        public string afterUrl { private get; set; }

        public Func<string> Action { private get; set; }

        public string Result => afterUrl ?? Action();

        public RoutePermanencyType PermanencyType { get; set; }
    }
}