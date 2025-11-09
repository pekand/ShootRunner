using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShootRunner
{
    public class Os
    {
        public static void CopyOrMove(string sourcePath, string destDirectory, bool move = false)
        {
            if (!Directory.Exists(destDirectory))
                Directory.CreateDirectory(destDirectory);

            if (File.Exists(sourcePath))
            {
                string destFile = Path.Combine(destDirectory, Path.GetFileName(sourcePath));
                destFile = GetUniquePath(destFile);
                if (move)
                    File.Move(sourcePath, destFile);
                else
                    File.Copy(sourcePath, destFile);
                return;
            }

            if (Directory.Exists(sourcePath))
            {
                string destFolder = Path.Combine(destDirectory, Path.GetFileName(sourcePath));
                destFolder = GetUniquePath(destFolder);
                if (move)
                    MoveDirectory(sourcePath, destFolder);
                else
                    CopyDirectoryRecursive(sourcePath, destFolder);
                return;
            }

            throw new FileNotFoundException("Source not found: " + sourcePath);
        }

        private static void CopyDirectoryRecursive(string sourceDir, string destDir)
        {
            Directory.CreateDirectory(destDir);

            foreach (string file in Directory.GetFiles(sourceDir))
            {
                string destFile = Path.Combine(destDir, Path.GetFileName(file));
                destFile = GetUniquePath(destFile);
                File.Copy(file, destFile);
            }

            foreach (string dir in Directory.GetDirectories(sourceDir))
            {
                string destSubDir = Path.Combine(destDir, Path.GetFileName(dir));
                destSubDir = GetUniquePath(destSubDir);
                CopyDirectoryRecursive(dir, destSubDir);
            }
        }

        private static void MoveDirectory(string sourceDir, string destDir)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(destDir)!);
            Directory.Move(sourceDir, destDir);
        }

        private static string GetUniquePath(string path)
        {
            if (!File.Exists(path) && !Directory.Exists(path))
                return path;

            string dir = Path.GetDirectoryName(path)!;
            string name = Path.GetFileNameWithoutExtension(path);
            string ext = Path.GetExtension(path);
            int counter = 1;

            string newPath;
            do
            {
                newPath = Path.Combine(dir, $"{name} ({counter}){ext}");
                counter++;
            } while (File.Exists(newPath) || Directory.Exists(newPath));

            return newPath;
        }

        public static string SaveTextWithAutoRename(string folderPath, string text, string baseName = "text", string ext = ".txt")
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, baseName + ext);

            int counter = 1;
            while (File.Exists(filePath))
            {
                filePath = Path.Combine(folderPath, $"{baseName} ({counter}){ext}");
                counter++;
            }

            File.WriteAllText(filePath, text);
            return filePath;
        }
        public static string SaveInternetShortcut(string folderPath, string url, string title = "link")
        {
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            string safe = SanitizeFileName(title);
            string path = Path.Combine(folderPath, safe + ".url");
            path = GetUniquePath(path);
            string content = "[InternetShortcut]\r\nURL=" + url + "\r\n";
            File.WriteAllText(path, content, Encoding.UTF8);
            return path;
        }

        public static string SaveHtmlRedirect(string folderPath, string url, string title = "link")
        {
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            string safe = SanitizeFileName(title);
            string path = Path.Combine(folderPath, safe + ".html");
            path = GetUniquePath(path);
            string html = "<!doctype html><html><head><meta charset=\"utf-8\"><meta http-equiv=\"refresh\" content=\"0;url=" + System.Net.WebUtility.HtmlEncode(url) + "\"></head><body><a href=\"" + System.Net.WebUtility.HtmlEncode(url) + "\">Open link</a></body></html>";
            File.WriteAllText(path, html, Encoding.UTF8);
            return path;
        }

        private static string SanitizeFileName(string name)
        {
            var invalid = Path.GetInvalidFileNameChars();
            return string.Concat(name.Where(c => !invalid.Contains(c))).Trim();
        }

        public static async Task<string> GetPageTitleAsync(string url)
        {
            try
            {
                using var client = new HttpClient();
                string html = await client.GetStringAsync(url);

                var match = Regex.Match(html, @"<title>(.*?)</title>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                if (match.Success)
                    return SanitizeFileName(match.Groups[1].Value.Trim());
            }
            catch
            {
            }
            // fallback: hostname if title not found
            return SanitizeFileName(new Uri(url).Host);
        }

        public static string ExtractHtmlFragment(string html)
        {

            var lines = html.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            int startIndex = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                string trimmedStart = lines[i].TrimStart();
                if (trimmedStart.StartsWith("<"))
                {
                    startIndex = i;
                    break;
                }
            }

            return string.Join("\r\n", lines, startIndex, lines.Length - startIndex);
        }
    }
}
