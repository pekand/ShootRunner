using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ShootRunner
{
    public class TextTools
    {
        public static string NormalizeLineEndings(string text)
        {
            if (text == null)
            {
                return null;
            }
            return text.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n");
        }

        public static bool IsURL(String url)
        {
            if (url == null)
            {
                return false;
            }

            return (Regex.IsMatch(url, @"^(http|https)://[^ ]*$"));
        }
    }
}
