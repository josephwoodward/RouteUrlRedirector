namespace RouteUrlRedirector.TrailingSlashMiddleware
{
	public class TrailingSlashPolicyCheck
	{
		public static bool TryParsePath(string currentPath, TrailingSlashPolicy trailingSlashPolicy, out string newPath)
		{
			// page.html?res=1

			// page?res=1
			// /page/?res=1
			// page/#hello
			// page#hello


			int charLength = trailingSlashPolicy == TrailingSlashPolicy.WithSlash ? currentPath.Length + 1 : currentPath.Length;
			char[] pathChars = new char[charLength];

			bool forceTrailingSlashPolicy = trailingSlashPolicy == TrailingSlashPolicy.WithSlash;
			for (int i = currentPath.Length - 1; i >= 0; i--)
			{
				char currentChar = currentPath[i];
				char prevChar = currentPath[i - 1];
				// If . is detected, ignore policy as path would be invalid
				if (currentChar == '/' || prevChar == '.' && forceTrailingSlashPolicy)
				{
					newPath = currentPath;
					return false;
				}

				if (currentChar == '?' || currentChar == '#' && prevChar == '/')
				{
					newPath = currentPath;
					return false;
				}

				pathChars[i] = currentChar;

				//bool hasStopChar = currentPath[i] == '#' || currentPath[i] == '/' || currentPath[i] == '?';
			}

			newPath = new string(pathChars);

			return true;
		}
	}
}