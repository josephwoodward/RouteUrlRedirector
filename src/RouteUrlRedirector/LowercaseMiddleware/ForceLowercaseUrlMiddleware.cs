﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RouteUrlRedirector.LowercaseMiddleware
{
	internal class ForceLowercaseUrlMiddleware
	{
		private readonly RequestDelegate _next;

		public ForceLowercaseUrlMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			if (!ContainsUppercaseChar(context.Request.Path)){
				await _next(context);
			} else {
				Redirect(context, context.Request.Path);
			}			
		}

		private static void Redirect(HttpContext context, string path)
		{
			context.Response.Redirect(path.ToLower(), true);
		}

		private static bool ContainsUppercaseChar(string input)
		{
			for (int i = 0; i < input.Length; i++)
			{
				if (char.IsUpper(input[i]))
					return true;
			}

			return false;
		}
	}
}