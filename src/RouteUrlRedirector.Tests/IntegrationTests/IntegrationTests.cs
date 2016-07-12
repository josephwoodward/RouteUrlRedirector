using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace RouteUrlRedirector.Tests.IntegrationTests
{
    public class IntegrationTests
    {
        [Fact]
        public async void Should_Redirect_Permanently()
        {
            // Arrange
            var builder = new WebHostBuilder()
                .Configure(app => {
                    app.UseRequestRedirect(r => r.ForPath("/old/").RedirectTo("/new/").Permanently());
                }
            );

            var server = new TestServer(builder);

            // Act
            var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), "/old/");
            var responseMessage = await server.CreateClient().SendAsync(requestMessage);

            // Assert
            Assert.Equal(HttpStatusCode.MovedPermanently, responseMessage.StatusCode);
            Assert.Equal(responseMessage.Headers.Location.ToString(), "/new/");
        }

        [Fact]
        public async void Should_Redirect_Temporarily()
        {
            // Arrange
            var builder = new WebHostBuilder()
                .Configure(app => {
                    app.UseRequestRedirect(r => r.ForPath("/old/").RedirectTo("/new/").Temporarily());
                });

            var server = new TestServer(builder);

            // Act
            var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), "/old/");
            var responseMessage = await server.CreateClient().SendAsync(requestMessage);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, responseMessage.StatusCode);
            Assert.Equal(responseMessage.Headers.Location.ToString(), "/new/");
        }
    }
}