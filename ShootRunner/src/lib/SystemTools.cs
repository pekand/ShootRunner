using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

#nullable disable


namespace ShootRunner
{
    public class SystemTools
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool LockWorkStation();

        public static void LockPc()
        {
            LockWorkStation();
        }

        ////////////////////////////////////////////////////////////////////

        // Method to get the default editor path for a given file extension
        public static string GetDefaultEditorPath(string fileExtension)
        {
            string progId = GetProgId(fileExtension);
            if (string.IsNullOrEmpty(progId))
            {
                return null;
            }

            string command = GetOpenCommand(progId);
            if (string.IsNullOrEmpty(command))
            {
                return null;
            }

            string editorPath = ExtractExecutablePath(command);
            return editorPath;
        }

        // Method to get the ProgID from the registry
        public static string GetProgId(string fileExtension)
        {
            // Attempt to read UserChoice ProgID
            string userChoicePath = $@"Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\{fileExtension}\UserChoice";
            using (RegistryKey userChoiceKey = Registry.CurrentUser.OpenSubKey(userChoicePath))
            {
                if (userChoiceKey != null)
                {
                    object progIdObj = userChoiceKey.GetValue("ProgId");
                    if (progIdObj != null)
                    {
                        return progIdObj.ToString();
                    }
                }
            }

            // Fallback: Read the default ProgID from HKEY_CLASSES_ROOT
            using (RegistryKey extKey = Registry.ClassesRoot.OpenSubKey(fileExtension))
            {
                if (extKey != null)
                {
                    object progIdObj = extKey.GetValue("");
                    if (progIdObj != null)
                    {
                        return progIdObj.ToString();
                    }
                }
            }

            return null; // ProgID not found
        }

        // Method to get the open command for a given ProgID
        public static string GetOpenCommand(string progId)
        {
            string commandPath = $@"{progId}\shell\open\command";
            using (RegistryKey commandKey = Registry.ClassesRoot.OpenSubKey(commandPath))
            {
                if (commandKey != null)
                {
                    object commandObj = commandKey.GetValue("");
                    if (commandObj != null)
                    {
                        return commandObj.ToString();
                    }
                }
            }

            return null; // Open command not found
        }

        // Method to extract the executable path from the command string
        public static string ExtractExecutablePath(string command)
        {
            // Define the regex pattern
            string pattern = @"(?<path>[A-Za-z]:\\(?:[^\\/:*?""<>|\r\n]+\\)*[^\\/:*?""<>|\r\n]+\.[A-Za-z]{2,4})";

            // Initialize the list to hold matches
            List<string> filePaths = new List<string>();

            // Perform the regex match
            MatchCollection matches = Regex.Matches(command, pattern);

            foreach (Match match in matches)
            {
                if (match.Groups["path"].Success && File.Exists(match.Groups["path"].Value))
                {
                    return match.Groups["path"].Value;
                }
            }

            return null;
        }

        // Method to open the file with the specified editor
        public static void OpenFileWithEditor(string editorPath, string filePath)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = editorPath,
                    Arguments = $"\"{filePath}\"",
                    UseShellExecute = false
                };

                Process.Start(psi);
            }
            catch (Exception ex)
            {
                Program.error(ex.Message);
            }
        }

    }
}
