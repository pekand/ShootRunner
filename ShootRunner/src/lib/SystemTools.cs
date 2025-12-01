using Microsoft.Win32;
using System.Diagnostics;
using System.Management.Automation;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable

#pragma warning disable IDE0130


namespace ShootRunner
{
    public class SystemTools
    {
        public static void LockPc()
        {
            WinApi.LockWorkStation();
        }

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
            using RegistryKey extKey = Registry.ClassesRoot.OpenSubKey(fileExtension);
            if (extKey != null)
            {
                object progIdObj = extKey.GetValue("");
                if (progIdObj != null)
                {
                    return progIdObj.ToString();
                }
            }

            return null; // ProgID not found
        }

        // Method to get the open command for a given ProgID
        public static string GetOpenCommand(string progId)
        {
            string commandPath = $@"{progId}\shell\open\command";
            using RegistryKey commandKey = Registry.ClassesRoot.OpenSubKey(commandPath);
            if (commandKey != null)
            {
                object commandObj = commandKey.GetValue("");
                if (commandObj != null)
                {
                    return commandObj.ToString();
                }
            }

            return null; // Open command not found
        }

        // Method to extract the executable path from the command string
        public static string ExtractExecutablePath(string command)
        {
            // Define the regex pattern
            string pattern = @"(?<path>[A-Za-z]:\\(?:[^\\/:*?""<>|\r\n]+\\)*[^\\/:*?""<>|\r\n]+\.[A-Za-z]{2,4})";

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
                ProcessStartInfo psi = new()
                {
                    FileName = editorPath,
                    Arguments = $"\"{filePath}\"",
                    UseShellExecute = false
                };

                Process.Start(psi);
            }
            catch (Exception ex)
            {
                Program.Error(ex.Message);
            }
        }

        public static void OpenFile(string filePath, string workdir = null)
        {
            try
            {
                ProcessStartInfo process = new(filePath) { UseShellExecute = true };

                if (workdir != null)
                {
                    process.WorkingDirectory = workdir;
                }

                Process.Start(process);
            }
            catch (Exception)
            {

            }
        }

        public static void OpenDirectory(string directoryPath, string workdir = null)
        {
            try
            {
                ProcessStartInfo process = new(directoryPath) { UseShellExecute = true };

                if (workdir != null)
                {
                    process.WorkingDirectory = workdir;
                }

                Process.Start(process);
            }
            catch (Exception)
            {

            }
        }

        public static void OpenHyperlink(string url, string workdir = null)
        {
            try
            {
                ProcessStartInfo process = new(url) { UseShellExecute = true };

                if (workdir != null)
                {
                    process.WorkingDirectory = workdir;
                }

                Process.Start(process);
            }
            catch (Exception)
            {

            }
        }

        public static void RunScript(string script, string workdir = null, bool silentCommand = true)
        {
            string fullCommand = string.Join(" & ",
                script
                    .Split([ '\r', '\n' ], StringSplitOptions.RemoveEmptyEntries)
                    .Select(line => line.Trim())
                    .Where(line => !string.IsNullOrWhiteSpace(line))
            );

            var psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/C " + fullCommand,
                UseShellExecute = false,
                RedirectStandardOutput = false,
                RedirectStandardError = false,
            };

            if (silentCommand) {
                psi.CreateNoWindow = true;
                psi.WindowStyle = ProcessWindowStyle.Hidden;
            }

            if (workdir != null) { 
                psi.WorkingDirectory = workdir;
            }

            Process.Start(psi);
        }

        public static async Task<bool> RunScriptWithTimeoutAsync(string script, string workdir = null, bool silentCommand = true, int timeoutMs = 60_000)
        {
            if (string.IsNullOrWhiteSpace(script))
                throw new ArgumentException("script is empty", nameof(script));

            string fullCommand = string.Join(" & ",
                script
                    .Split([ '\r', '\n' ], StringSplitOptions.RemoveEmptyEntries)
                    .Select(line => line.Trim())
                    .Where(line => !string.IsNullOrWhiteSpace(line))
            );

            var psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/C " + fullCommand,
                UseShellExecute = false,
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                WorkingDirectory = workdir
            };

            if (silentCommand)
            {
                psi.CreateNoWindow = true;
                psi.WindowStyle = ProcessWindowStyle.Hidden;
            }

            using var proc = new Process { StartInfo = psi, EnableRaisingEvents = true };
            if (!proc.Start()) throw new InvalidOperationException("Failed to start process.");

            var cts = new CancellationTokenSource();
            var waitTask = proc.WaitForExitAsync(cts.Token); // .NET 5+
            var delayTask = Task.Delay(timeoutMs, cts.Token);

            var finished = await Task.WhenAny(waitTask, delayTask).ConfigureAwait(false);

            if (finished == waitTask)
            {
                // finished within timeout
                cts.Cancel(); // cancel delay if still running
                return true;
            }

            // timeout expired -> try to kill process tree
            try
            {
                proc.Kill(true); // kills process tree on supported runtimes
            }
            catch
            {
                try
                {
                    using var tkill = Process.Start(new ProcessStartInfo
                    {
                        FileName = "taskkill",
                        Arguments = $"/PID {proc.Id} /T /F",
                        CreateNoWindow = true,
                        UseShellExecute = false
                    });
                    tkill?.WaitForExit(5_000);
                }
                catch { /* ignore fallback errors */ }
            }

            // Optionally wait a short while for proc to exit
            try { await proc.WaitForExitAsync().ConfigureAwait(false); } catch { }

            return false;
        }

        public static void RunPowershellScriptVisible(string script, string workdir = null, bool usePwsh = false)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(script)) throw new ArgumentException("script empty", nameof(script));

                byte[] bytes = Encoding.Unicode.GetBytes(script);
                string b64 = Convert.ToBase64String(bytes);

                string exe = usePwsh ? "pwsh.exe" : "powershell.exe";
                string args = $"-NoProfile -ExecutionPolicy Bypass -NoExit -EncodedCommand {b64}";

                var psi = new ProcessStartInfo
                {
                    FileName = exe,
                    Arguments = args,
                    UseShellExecute = true,
                    CreateNoWindow = false,
                    WindowStyle = ProcessWindowStyle.Normal,
                    WorkingDirectory = workdir
                };

                Process.Start(psi);
            }
            catch (Exception)
            {


            }

        }

        public static async Task<bool> RunPowershellScriptVisibleWithTimeout(string script, string workdir = null, bool usePwsh = false, int timeoutMs = 60000)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(script)) throw new ArgumentException("script empty", nameof(script));

                byte[] bytes = Encoding.Unicode.GetBytes(script);
                string b64 = Convert.ToBase64String(bytes);

                string exe = usePwsh ? "pwsh.exe" : "powershell.exe";
                string args = $"-NoProfile -ExecutionPolicy Bypass -NoExit -EncodedCommand {b64}";

                var psi = new ProcessStartInfo
                {
                    FileName = exe,
                    Arguments = args,
                    UseShellExecute = true,
                    CreateNoWindow = false,
                    WindowStyle = ProcessWindowStyle.Normal,
                    WorkingDirectory = workdir
                };


                using var proc = new Process { StartInfo = psi, EnableRaisingEvents = true };
                if (!proc.Start()) throw new InvalidOperationException("Failed to start process.");

                var cts = new CancellationTokenSource();
                var waitTask = proc.WaitForExitAsync(cts.Token); // .NET 5+
                var delayTask = Task.Delay(timeoutMs, cts.Token);

                var finished = await Task.WhenAny(waitTask, delayTask).ConfigureAwait(false);

                if (finished == waitTask)
                {
                    // finished within timeout
                    cts.Cancel(); // cancel delay if still running
                    return true;
                }

                // timeout expired -> try to kill process tree
                try
                {
                    proc.Kill(true); // kills process tree on supported runtimes
                }
                catch
                {
                    try
                    {
                        using var tkill = Process.Start(new ProcessStartInfo
                        {
                            FileName = "taskkill",
                            Arguments = $"/PID {proc.Id} /T /F",
                            CreateNoWindow = true,
                            UseShellExecute = false
                        });
                        tkill?.WaitForExit(5_000);
                    }
                    catch { }
                }

                try { await proc.WaitForExitAsync().ConfigureAwait(false); } catch { }

                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<bool> RunPowershellScriptWithTimeoutAsync(string script, string workdir = null, bool silentCommand = false, int timeoutMs = 60_000)
        {
            if (string.IsNullOrWhiteSpace(script))
                throw new ArgumentException("Script is empty", nameof(script));

            using var ps = PowerShell.Create();

            if (workdir != null && Directory.Exists(workdir))
            {
                ps.AddScript($"Set-Location -Path \"{workdir}\"");
            }

            ps.AddScript(script);

            using var cts = new CancellationTokenSource(timeoutMs);

            try
            {
                var asyncResult = ps.BeginInvoke();

                // Wait asynchronously for either completion or timeout
                while (!asyncResult.IsCompleted)
                {
                    if (cts.Token.IsCancellationRequested)
                    {
                        try { ps.Stop(); } catch { }
                        return false; // timeout
                    }
                    await Task.Delay(100);
                }

                ps.EndInvoke(asyncResult);
                return true;
            }
            catch (OperationCanceledException)
            {
                try { ps.Stop(); } catch { }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("PowerShell error: " + ex.Message);
                return false;
            }
        }

    }
}
