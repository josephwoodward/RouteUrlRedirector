using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;
using Shouldly;

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
            responseMessage.StatusCode.ShouldBe(HttpStatusCode.MovedPermanently);
            responseMessage.Headers.Location.ToString().ShouldBe("/new/");
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
            responseMessage.StatusCode.ShouldBe(HttpStatusCode.Redirect);
            responseMessage.Headers.Location.ToString().ShouldBe("/new/");
        }
    }
}