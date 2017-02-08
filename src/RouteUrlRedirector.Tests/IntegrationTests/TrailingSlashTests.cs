using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using RouteUrlRedirector.TrailingSlashMiddleware;
using Shouldly;
using Xunit;

namespace RouteUrlRedirector.Tests.IntegrationTests
{
	public class TrailingSlashTests
	{
		[Fact]
		public async void Should_Redirect_Permanently()
		{
			// Arrange
			var builder = new WebHostBuilder()
				.UseContentRoot(System.IO.Directory.GetCurrentDirectory())
				.Configure(app => {
					app.ApplyTrailingSlashPolicy(TrailingSlashPolicy.WithSlash);
				});

			const string beforeUrl = "/Before-Url";
			const string afterUrl = "/before-url";

			var server = new TestServer(builder);

			// Act
			var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), beforeUrl);
			var responseMessage = await server.CreateClient().SendAsync(requestMessage);

			// Assert
			var responseCode = responseMessage.StatusCode;
			responseCode.ShouldBe(HttpStatusCode.MovedPermanently);

			var path = responseMessage.Headers.Location.ToString();
			path.ShouldBe(afterUrl);
		}
	}
}