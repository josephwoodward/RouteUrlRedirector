using System.Collections.Generic;
using RouteUrlRedirector.TrailingSlashMiddleware;
using Shouldly;
using Xunit;

namespace RouteUrlRedirector.Tests.UnitTests
{
	public class PathMatchesTrailingSlashPolicyTests
	{
		[Fact]
		public void ShouldIgnorePathsWithFileExtensions()
		{
			// Arrange
			var paths = new List<string>
			{
				"/page/about.html",
				"/page/about.css"
			};

			// Act
			foreach (string path in paths)
			{
				string newPath;
				bool result = TrailingSlashPolicyCheck.TryParsePath(path, TrailingSlashPolicy.WithSlash, out newPath);

				// Assert
				result.ShouldBe(false);
				newPath.ShouldBe(path);
			}
		}

		[Fact]
		public void ShouldIgnorePolicy()
		{
			// Arrange
			var paths = new List<string>
			{
				"/page/about/?res=1",
				"/page/about/#title1"
			};

			// Act
			foreach (string path in paths)
			{
				string newPath;
				bool result = TrailingSlashPolicyCheck.TryParsePath(path, TrailingSlashPolicy.WithSlash, out newPath);

				// Assert
				result.ShouldBe(false);
				newPath.ShouldBe(path);
			}
		}

		[Fact]
		public void ShouldForceSlashPolicy()
		{
			// Arrange
			string path = "/page/about";


			// Act
			string newPath;
			bool result = TrailingSlashPolicyCheck.TryParsePath(path, TrailingSlashPolicy.WithSlash, out newPath);

			// Assert
			result.ShouldBe(true);
			newPath.ShouldBe($"{path}/");
		}

		[Fact]
		public void ShouldMatchAlreadyMatchedSlashPolicy()
		{
			// Arrange
			string path = "/page/about/";


			// Act
			string newPath;
			bool result = TrailingSlashPolicyCheck.TryParsePath(path, TrailingSlashPolicy.WithSlash, out newPath);

			// Assert
			result.ShouldBe(false);
			newPath.ShouldBe(path);
		}
	}
}