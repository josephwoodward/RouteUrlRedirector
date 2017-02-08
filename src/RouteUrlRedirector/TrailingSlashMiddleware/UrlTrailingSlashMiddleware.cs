using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RouteUrlRedirector.TrailingSlashMiddleware
{
	internal class UrlTrailingSlashMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly TrailingSlashPolicy _trailingSlashPolicy;

		public UrlTrailingSlashMiddleware(RequestDelegate next, TrailingSlashPolicy trailingSlashPolicy)
		{
			_next = next;
			_trailingSlashPolicy = trailingSlashPolicy;
		}

		public async Task Invoke(HttpContext context)
		{
			string newPath;
			if (PathMatchesTrailingSlashPolicy(context.Request.Path, _trailingSlashPolicy, out newPath)){
				await _next(context);
			} else {
				Redirect(context, newPath);
			}
		}

		private static bool PathMatchesTrailingSlashPolicy(string currentPath, TrailingSlashPolicy trailingSlashPolicy, out string newPath)
		{
			return TrailingSlashPolicyCheck.TryParsePath(currentPath, trailingSlashPolicy, out newPath);
		}

		private static void Redirect(HttpContext context, string path)
		{
			context.Response.Redirect(path.ToLower(), true);
		}
	}
}