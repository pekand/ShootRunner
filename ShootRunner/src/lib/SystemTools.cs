using Microsoft.Win32;
using System;
using System.ComponentModel;
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

        /********************************************************************************/

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

        public static string EscapeString(string input)
        {
            StringBuilder escaped = new();
            foreach (char c in input)
            {
                switch (c)
                {
                    case '\\':
                        escaped.Append(@"\\");
                        break;
                    case '\"':
                        escaped.Append("\\\"");
                        break;
                    case '\'':
                        escaped.Append("\\'");
                        break;
                    case '\n':
                        escaped.Append(" ; ");
                        break;
                    case '\r':
                        escaped.Append("");
                        break;
                    default:
                        escaped.Append(c);
                        break;
                }
            }
            return escaped.ToString();
        }

        /********************************************************************************/

        // Method to open the file with the specified editor
        public static void OpenFileWithEditor(string editorPath, string filePath)
        {
            try
            {
                if (File.Exists(editorPath) && File.Exists(filePath))
                {
                    ProcessStartInfo psi = new()
                    {
                        FileName = editorPath,
                        Arguments = $"\"{filePath}\"",
                        UseShellExecute = false,
                        WorkingDirectory = Path.GetDirectoryName(filePath),
                    };

                    Process.Start(psi);
                }
            }
            catch (Exception ex)
            {
                Program.Error("OpenFileWithEditor error: " + ex.Message);
            }
        }

        public static void OpenFileInSystem(string open)
        {
            try
            {
                Process.Start(new ProcessStartInfo(open) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                Program.Debug("OpenFileInSystem: " + ex.Message);
            }
        }

        public static void OpenFile(string filePath, string workdir = null)
        {
            try
            {
                ProcessStartInfo process = new(filePath) { UseShellExecute = true };

                if (workdir != null && Directory.Exists(workdir))
                {
                    process.WorkingDirectory = workdir;
                }

                Process.Start(process);
            }
            catch (Exception ex)
            {

                Program.Error("OpenFile error: " + ex.Message);
            }
        }

        public static void OpenDirectory(string directoryPath, string workdir = null)
        {
            try
            {
                ProcessStartInfo process = new(directoryPath) { UseShellExecute = true };

                if (workdir != null && Directory.Exists(workdir))
                {
                    process.WorkingDirectory = workdir;
                }

                Process.Start(process);
            }
            catch (Exception ex)
            {
                Program.Error("OpenDirectory error: " + ex.Message);
            }
        }

        public static void OpenHyperlink(string url, string workdir = null)
        {
            try
            {
                ProcessStartInfo process = new(url) { UseShellExecute = true };

                if (workdir != null && Directory.Exists(workdir))
                {
                    process.WorkingDirectory = workdir;
                }

                Process.Start(process);
            }
            catch (Exception ex)
            {

                Program.Error("OpenHyperlink error: " + ex.Message);
            }
        }

        public static void RunCommand(string cmd, string parameters, string workdir = null, bool silent = false)
        {
            Job.DoJob(
                new DoWorkEventHandler(
                    delegate (object o, DoWorkEventArgs args)
                    {
                        if (TextTools.IsURL(cmd) && parameters == null)
                        {
                            Process.Start(new ProcessStartInfo(cmd) { UseShellExecute = true });
                        }
                        else if (Directory.Exists(cmd) && parameters == null)
                        {
                            System.Diagnostics.Process.Start("explorer.exe", cmd);
                        }
                        else if (File.Exists(cmd) && parameters == null)
                        {
                            System.Diagnostics.Process.Start(cmd);
                        }
                        else
                        {
                            try
                            {

                                Process process = new();
                                ProcessStartInfo startInfo = new();

                                if (silent)
                                {
                                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                }


                                startInfo.FileName = cmd;
                                startInfo.Arguments = parameters;
                                if (workdir != null && Directory.Exists(workdir))
                                {
                                    startInfo.WorkingDirectory = workdir;
                                }
                                startInfo.UseShellExecute = true;
                                startInfo.RedirectStandardOutput = false;
                                startInfo.RedirectStandardError = false;
                                startInfo.CreateNoWindow = false;
                                process.StartInfo = startInfo;
                                process.Start();
                            }
                            catch (Exception ex)
                            {
                                Program.Error("RunCommand error: " + ex.Message);
                            }
                        }

                    }
                ),
                new RunWorkerCompletedEventHandler(
                    delegate (object o, RunWorkerCompletedEventArgs args)
                    {

                    }
                )
            );
        }

        /********************************************************************************/

        public static List<Process> FindProcess(string path)
        {
            List<Process> list = [];

            try
            {
                var processes = Process.GetProcesses();

                foreach (var process in processes)
                {
                    try
                    {
                        if (process.MainModule.FileName.Equals(path, StringComparison.OrdinalIgnoreCase))
                        {
                            list.Add(process);
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.Error("FindProcess process error: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {

                Program.Error("FindProcess error: " + ex.Message);
            }


            return list;
        }

        public static async Task<Window> StartProcessAndGetWindowHandleAsync(string cmd, string parameters, string workdir = null, bool silent = false, bool guesWindow = false)
        {

            try
            {
                List<IntPtr> taskbarWindows1 = null;
                if (guesWindow)
                {
                    taskbarWindows1 = ToolsWindow.GetTaskbarWindows();
                }

                Process process = new();
                ProcessStartInfo startInfo = new();
                process.StartInfo = startInfo;
                startInfo.FileName = cmd;
                startInfo.Arguments = parameters;

                if (workdir != null && Directory.Exists(workdir))
                {
                    startInfo.WorkingDirectory = workdir;
                }

                if (silent)
                {
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.CreateNoWindow = true;
                }

                process.Start();

                if (!process.HasExited)
                {
                    await System.Threading.Tasks.Task.Run(() => process.WaitForInputIdle());
                }

                //  check if process have main window
                if (process.MainWindowHandle != IntPtr.Zero)
                {
                    Window window = new()
                    {
                        Handle = process.MainWindowHandle
                    };
                    return window;
                }

                // get all process windows and try find wisible window
                List<Window> processWindows = ToolsWindow.FindWindowByProcessId((uint)process.Id);
                if (processWindows.Count == 1)
                {
                    Window window = new()
                    {
                        Handle = processWindows[0].Handle
                    };
                    return window;

                }

                if (guesWindow) // compare taskbar windows and check for change
                {
                    Thread.Sleep(3000);

                    // calculate diff between opened windows
                    List<IntPtr> taskbarWindows2 = ToolsWindow.GetTaskbarWindows();
                    List<IntPtr> foundWindows = [];
                    foreach (IntPtr win2 in taskbarWindows2)
                    {
                        bool found = false;
                        foreach (IntPtr win1 in taskbarWindows1)
                        {
                            if (win1 == win2)
                            {
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                        {
                            foundWindows.Add(win2);
                        }
                    }

                    // Check only  new windows
                    foreach (IntPtr win3 in foundWindows)
                    {
                        string path = ToolsWindow.GetApplicationPathFromWindow(win3);
                        if (path == cmd)
                        {
                            Window window = new()
                            {
                                Handle = win3
                            };
                            return window;
                        }
                    }

                    // Check all windows
                    foreach (IntPtr win4 in taskbarWindows2)
                    {
                        string path = ToolsWindow.GetApplicationPathFromWindow(win4);
                        if (path == cmd)
                        {
                            Window window = new()
                            {
                                Handle = win4
                            };
                            return window;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Program.Error("StartProcessAndGetWindowHandleAsync error" + ex.Message);
            }

            return null;
        }

        /********************************************************************************/

        public static void RunScript(string script, string workdir = null, bool silentCommand = true)
        {
            try
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

                if (workdir != null && Directory.Exists(workdir)) { 
                    psi.WorkingDirectory = workdir;
                }

                Process.Start(psi);
            }
            catch (Exception ex)
            {

                Program.Error("RunScript error: " + ex.Message);
            }
        }

        public static async Task<bool> RunScriptWithTimeoutAsync(string script, string workdir = null, bool silentCommand = true, int timeoutMs = 60_000)
        {
            try
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
                };

                if (workdir != null && Directory.Exists(workdir)) {
                    psi.WorkingDirectory = workdir;
                }

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
            }
            catch (Exception ex)
            {

                Program.Error("RunScriptWithTimeoutAsync error: " + ex.Message);
            }

            return false;
        }
        

        /********************************************************************************/

        public static void RunPowerShellCommand(string cmd, string workdir = null, bool silent = false)
        {
            if (Program.powershell == null)
            {
                Program.Error("Powershell not available. Install powershell.");
                return;
            }

            Job.DoJob(
                new DoWorkEventHandler(
                    delegate (object o, DoWorkEventArgs args)
                    {
                        var job = (BackgroundJob)args.Argument;


                        try
                        {
                            if (silent)
                            {
                                try
                                {
                                    using PowerShell ps = PowerShell.Create();
                                    if (workdir != null && Directory.Exists(workdir))
                                    {
                                        ps.AddScript($"Set-Location -Path \"{workdir}\"");
                                    }

                                    ps.AddScript(cmd);

                                    try
                                    {
                                        var result = ps.BeginInvoke();
                                        Program.Write(result.ToString());
                                        while (!result.IsCompleted)
                                        {
                                            if (job.token.IsCancellationRequested)
                                            {
                                                ps.Stop();
                                                job.token.ThrowIfCancellationRequested();
                                            }
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        Program.Error(ex.Message);
                                    }
                                }
                                catch (Exception ex)
                                {

                                    Program.Error(ex.Message);
                                }

                            }
                            else
                            {
                                Process process = new();
                                ProcessStartInfo startInfo = new()
                                {
                                    FileName = Program.powershell
                                };
                                string escapedCmd = SystemTools.EscapeString(cmd);
                                if (silent)
                                {
                                    startInfo.Arguments = $"-NoProfile -Command \"{escapedCmd}\"";
                                }
                                else
                                {
                                    startInfo.Arguments = $"-NoExit -NoProfile -Command \"{escapedCmd}\"";
                                }

                                startInfo.WorkingDirectory = workdir;
                                startInfo.UseShellExecute = true;
                                startInfo.RedirectStandardOutput = false;
                                startInfo.RedirectStandardError = false;
                                startInfo.CreateNoWindow = false;
                                if (silent)
                                {
                                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                    startInfo.CreateNoWindow = true;
                                }
                                process.StartInfo = startInfo;
                                process.Start();
                            }
                        }
                        catch (Exception ex)
                        {
                            Program.Error("RunPowerShellCommand error: " + ex.Message);
                        }

                    }
                ),
                new RunWorkerCompletedEventHandler(
                    delegate (object o, RunWorkerCompletedEventArgs args)
                    {

                    }
                )
            );
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
                    WindowStyle = ProcessWindowStyle.Normal
                };

                if (workdir != null && Directory.Exists(workdir))
                {
                    psi.WorkingDirectory = workdir;
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
                    catch { }
                }

                try { await proc.WaitForExitAsync().ConfigureAwait(false); } catch { }

                return false;
            }
            catch (Exception ex)
            {

                Program.Error("RunPowershellScriptVisibleWithTimeout error: " + ex.Message);
            }

            return false;
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
                Console.WriteLine("RunPowershellScriptWithTimeoutAsync error: " + ex.Message);
                return false;
            }
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
                };

                if (workdir != null && Directory.Exists(workdir))
                {
                    psi.WorkingDirectory = workdir;
                }


                Process.Start(psi);
            }
            catch (Exception ex)
            {

                Program.Error("RunPowershellScriptVisible error: " + ex.Message);
            }
        }

        public static void RunPowerShellCommand(Pin pin)
        {
            if (Program.powershell == null)
            {
                Program.Error("Powershell not available. Install powershell.");
                return;
            }

            if (pin.usePowershell)
            {
                Job.DoJob(
                    new DoWorkEventHandler(
                        delegate (object o, DoWorkEventArgs args)
                        {
                            var job = (BackgroundJob)args.Argument;

                            try
                            {
                                List<IntPtr> taskbarWindows1 = null;

                                if (pin.matchNewWindow)
                                {
                                    taskbarWindows1 = ToolsWindow.GetTaskbarWindows();
                                }

                                if (pin.silentCommand)
                                {
                                    try
                                    {
                                        using PowerShell ps = PowerShell.Create();
                                        if (pin.useWorkdir && pin.workdir != null && Directory.Exists(pin.workdir))
                                        {
                                            ps.AddScript($"Set-Location -Path \"{pin.workdir}\"");
                                        }

                                        ps.AddScript(pin.command);

                                        try
                                        {
                                            var result = ps.BeginInvoke();
                                            Program.Write(result.ToString());
                                            while (!result.IsCompleted)
                                            {
                                                if (job.token.IsCancellationRequested)
                                                {
                                                    ps.Stop();
                                                    job.token.ThrowIfCancellationRequested();
                                                }
                                            }

                                        }
                                        catch (Exception ex)
                                        {
                                            Program.Error(ex.Message);
                                        }
                                    }
                                    catch (Exception ex)
                                    {

                                        Program.Error(ex.Message);
                                    }

                                }
                                else
                                {
                                    Process process = new();
                                    ProcessStartInfo startInfo = new()
                                    {
                                        FileName = Program.powershell
                                    };
                                    string escapedCmd = SystemTools.EscapeString(pin.command);
                                    if (pin.silentCommand)
                                    {
                                        startInfo.Arguments = $"-NoProfile -Command \"{escapedCmd}\"";
                                    }
                                    else
                                    {
                                        startInfo.Arguments = $"-NoExit -NoProfile -Command \"{escapedCmd}\"";
                                    }

                                    //startInfo.WorkingDirectory = workdir;
                                    startInfo.UseShellExecute = true;
                                    startInfo.RedirectStandardOutput = false;
                                    startInfo.RedirectStandardError = false;
                                    startInfo.CreateNoWindow = false;

                                    if (pin.useWorkdir && pin.workdir != null && Directory.Exists(pin.workdir))
                                    {
                                        startInfo.WorkingDirectory = pin.workdir;
                                    }

                                    if (pin.silentCommand)
                                    {
                                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                        startInfo.CreateNoWindow = true;
                                    }

                                    process.StartInfo = startInfo;
                                    process.Start();
                                }

                                if (pin.matchNewWindow) // compare taskbar windows and check for change
                                {
                                    Thread.Sleep(1500);

                                    List<IntPtr> taskbarWindows2 = ToolsWindow.GetTaskbarWindows();
                                    List<IntPtr> foundWindows = [];

                                    foreach (IntPtr win2 in taskbarWindows2)
                                    {
                                        bool found = false;
                                        foreach (IntPtr win1 in taskbarWindows1)
                                        {
                                            if (win1 == win2)
                                            {
                                                found = true;
                                                break;
                                            }
                                        }

                                        if (!found)
                                        {
                                            foundWindows.Add(win2);
                                        }
                                    }

                                    if (foundWindows.Count == 1)
                                    {
                                        pin.window.Handle = foundWindows[0];
                                        pin.doubleClickCommand = false;
                                        ToolsWindow.SetWindowData(pin.window);
                                        pin.window.Type = "COMMAND";
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Program.Error("RunPowerShellCommand error: " + ex.Message);
                            }
                        }
                    ),
                    new RunWorkerCompletedEventHandler(
                        delegate (object o, RunWorkerCompletedEventArgs args)
                        {

                        }
                    )
                );
            }

            if (pin.useCmdshell)
            {
                Job.DoJob(
                    new DoWorkEventHandler(
                        delegate (object o, DoWorkEventArgs args)
                        {
                            var job = (BackgroundJob)args.Argument;

                            try
                            {
                                List<IntPtr> taskbarWindows1 = null;

                                if (pin.matchNewWindow)
                                {
                                    taskbarWindows1 = ToolsWindow.GetTaskbarWindows();
                                }

                                if (pin.silentCommand)
                                {
                                    try
                                    {
                                        using PowerShell ps = PowerShell.Create();
                                        ps.AddScript(pin.command);

                                        // string workingDirectory = @"C:\Your\Desired\Path";
                                        // ps.AddScript($"Set-Location -Path \"{workingDirectory}\"");

                                        try
                                        {
                                            var result = ps.BeginInvoke();
                                            Program.Write(result.ToString());
                                            while (!result.IsCompleted)
                                            {
                                                if (job.token.IsCancellationRequested)
                                                {
                                                    ps.Stop();
                                                    job.token.ThrowIfCancellationRequested();
                                                }
                                            }

                                        }
                                        catch (Exception ex)
                                        {
                                            Program.Error(ex.Message);
                                        }
                                    }
                                    catch (Exception ex)
                                    {

                                        Program.Error(ex.Message);
                                    }

                                }
                                else
                                {
                                    Process process = new();
                                    ProcessStartInfo startInfo = new()
                                    {
                                        FileName = Program.powershell
                                    };
                                    string escapedCmd = SystemTools.EscapeString(pin.command);
                                    if (pin.silentCommand)
                                    {
                                        startInfo.Arguments = $"-NoProfile -Command \"{escapedCmd}\"";
                                    }
                                    else
                                    {
                                        startInfo.Arguments = $"-NoExit -NoProfile -Command \"{escapedCmd}\"";
                                    }

                                    //startInfo.WorkingDirectory = workdir;
                                    startInfo.UseShellExecute = true;
                                    startInfo.RedirectStandardOutput = false;
                                    startInfo.RedirectStandardError = false;
                                    startInfo.CreateNoWindow = false;
                                    if (pin.silentCommand)
                                    {
                                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                        startInfo.CreateNoWindow = true;
                                    }
                                    process.StartInfo = startInfo;
                                    process.Start();
                                }

                                if (pin.matchNewWindow) // compare taskbar windows and check for change
                                {
                                    Thread.Sleep(1500);

                                    List<IntPtr> taskbarWindows2 = ToolsWindow.GetTaskbarWindows();
                                    List<IntPtr> foundWindows = [];

                                    foreach (IntPtr win2 in taskbarWindows2)
                                    {
                                        bool found = false;
                                        foreach (IntPtr win1 in taskbarWindows1)
                                        {
                                            if (win1 == win2)
                                            {
                                                found = true;
                                                break;
                                            }
                                        }

                                        if (!found)
                                        {
                                            foundWindows.Add(win2);
                                        }
                                    }

                                    if (foundWindows.Count == 1)
                                    {
                                        pin.window.Handle = foundWindows[0];
                                        pin.doubleClickCommand = false;
                                        ToolsWindow.SetWindowData(pin.window);
                                        pin.window.Type = "COMMAND";
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Program.Error("exception: " + ex.Message);
                            }
                        }
                    ),
                    new RunWorkerCompletedEventHandler(
                        delegate (object o, RunWorkerCompletedEventArgs args)
                        {

                        }
                    )
                );
            }
        }

        /********************************************************************************/




    }
}
