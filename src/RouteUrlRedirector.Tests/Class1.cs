using Xunit;

namespace RouteUrlRedirector.Tests
{
    public class Class1
    {
        [Fact]
        public void Test()
        {
            string res = "Hello";

            Assert.Equal("Hello", res);
        }

        [Fact]
        public void Test2()
        {
            var res = new UseRouteUrlRedirect();
        }


    }
}