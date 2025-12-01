using System.Text.RegularExpressions;

#nullable disable

#pragma warning disable IDE0079
#pragma warning disable IDE0130

namespace ShootRunner
{
    public partial class TextTools
    {
        public static string NormalizeLineEndings(string text)
        {
            if (text == null)
            {
                return null;
            }
            return text.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n");
        }

        [GeneratedRegex(@"^(http|https)://[^ ]*$")]
        private static partial Regex MatchHttps();

        public static bool IsURL(String url)
        {
            if (url == null)
            {
                return false;
            }

            return (MatchHttps().IsMatch(url));
        }

    }
}
