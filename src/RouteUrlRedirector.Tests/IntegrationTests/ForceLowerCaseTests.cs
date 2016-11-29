using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;
using Shouldly;

namespace RouteUrlRedirector.Tests.IntegrationTests
{
	public class ForceLowerCaseTests
	{
		[Fact]
		public async void Should_Redirect_Permanently()
		{
			// Arrange
			var builder = new WebHostBuilder()
				.UseContentRoot(System.IO.Directory.GetCurrentDirectory())
				.Configure(app => {
					app.ForceLowercaseUrl();
				});

			const string beforeUrl = "/Before-Url";
			const string afterUrl = "/before-url";

			var server = new TestServer(builder);

			// Act
			var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), beforeUrl);
			var responseMessage = await server.CreateClient().SendAsync(requestMessage);

			// Assert
			var path = responseMessage.Headers.Location.ToString();
			var responseCode = responseMessage.StatusCode;

			responseMessage.StatusCode.ShouldBe(HttpStatusCode.MovedPermanently);
			responseMessage.Headers.Location.ToString().ShouldBe(afterUrl);
		}

		[Fact]
		public async void Should_Not_Redirect()
		{
			// Arrange
			var builder = new WebHostBuilder()
				.UseContentRoot(System.IO.Directory.GetCurrentDirectory())
				.Configure(app => {
					app.ForceLowercaseUrl();
				});

			var server = new TestServer(builder);

			const string beforeUrl = "/before-url";

			// Act
			var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), beforeUrl);
			var responseMessage = await server.CreateClient().SendAsync(requestMessage);

			// Assert
			responseMessage.Headers.Location.ToString().ShouldBe(beforeUrl);
		}
	}
}