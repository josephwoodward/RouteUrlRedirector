using System;

namespace RouteUrlRedirector
{
    public class RouteItem
    {
        public string afterPath { private get; set; }

        public Func<string> Action { private get; set; }

        public string Result => afterPath ?? Action();

        public RoutePermanencyType PermanencyType { get; set; }
    }
}