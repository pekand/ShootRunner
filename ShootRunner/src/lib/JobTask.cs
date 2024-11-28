﻿using System.ComponentModel;
using System.Diagnostics;
using System.Management.Automation;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

#nullable disable


namespace ShootRunner
{
    public class JobTask
    {
        /*public static void RunCommand(string command, string parameters, string workdir = "") {
            Process process = new Process();
            process.StartInfo.FileName = command;
            process.StartInfo.Arguments = parameters;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            if (workdir != null && workdir != "") {
                process.StartInfo.WorkingDirectory = workdir;
            }

            try
            {
                process.Start();
                //process.WaitForExit();
            }
            catch (Exception ex)
            {
                Program.debug("An error occurred: " + ex.Message);
            }
        }*/

        public static void RunCommand(string cmd, string parameters, string workdir = null, bool silent = false) //b39d265706
        {
            Job.DoJob(
                new DoWorkEventHandler(
                    delegate (object o, DoWorkEventArgs args)
                    {
                        if (TextTools.IsURL(cmd) && parameters == null) {
                            Process.Start(new ProcessStartInfo(cmd) { UseShellExecute = true });
                        }
                        else if (Directory.Exists(cmd) && parameters == null)
                        {
                            System.Diagnostics.Process.Start("explorer.exe", cmd);
                        }
                        else if(File.Exists(cmd) && parameters == null)
                        {
                            System.Diagnostics.Process.Start(cmd);
                        }
                        else
                        {
                            try
                            {

                                Process process = new Process();
                                ProcessStartInfo startInfo = new ProcessStartInfo();

                                if (silent)
                                {
                                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                }


                                startInfo.FileName = cmd;
                                startInfo.Arguments = parameters;
                                startInfo.WorkingDirectory = workdir;
                                startInfo.UseShellExecute = true;
                                startInfo.RedirectStandardOutput = false;
                                startInfo.RedirectStandardError = false;
                                startInfo.CreateNoWindow = false;
                                process.StartInfo = startInfo;
                                process.Start();
                            }
                            catch (Exception ex)
                            {
                                Program.error("exception: " + ex.Message);
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

        public static string EscapeString(string input)
        {
            StringBuilder escaped = new StringBuilder();
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

        public static void RunPowerShellCommand(string cmd, string workdir = null, bool silent = false) //b39d265706
        {
            if (Program.powershell == null) {
                Program.error("Powershell not available. Install powershell.");
                return;
            }

            Job.DoJob(
                new DoWorkEventHandler(
                    delegate (object o, DoWorkEventArgs args)
                    {
                        var job = (BackgroundJob)args.Argument;

                        if (TextTools.IsURL(cmd))
                        {
                            Process.Start(new ProcessStartInfo(cmd) { UseShellExecute = true });
                        }
                        else if (Directory.Exists(cmd))
                        {
                            System.Diagnostics.Process.Start("explorer.exe", cmd);
                        }
                        else if (File.Exists(cmd))
                        {
                            System.Diagnostics.Process.Start(cmd);
                        }
                        else
                        {
                            try
                            {
                                if (silent)
                                {
                                    try
                                    {
                                        using (PowerShell ps = PowerShell.Create())
                                        {
                                            ps.AddScript(cmd);

                                            try
                                            {
                                                var result = ps.BeginInvoke();
                                                Program.write(result.ToString());
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
                                                Program.error(ex.Message);
                                            }
                                            
                                        }
                                    }
                                    catch (Exception ex)
                                    {

                                        Program.error(ex.Message);
                                    }
                                    
                                }
                                else {
                                    Process process = new Process();
                                    ProcessStartInfo startInfo = new ProcessStartInfo();

                                    startInfo.FileName = Program.powershell;
                                    string escapedCmd = EscapeString(cmd);
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
                                Program.error("exception: " + ex.Message);
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

        public static void OpenFileInSystem(string open)
        {
            try
            {
                Process.Start(new ProcessStartInfo(open) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                Program.debug("An error occurred: " + ex.Message);
            }
        }

        public static List<Process> FindProcess(string path)
        {
            List<Process> list = new List<Process>();

            var processes = Process.GetProcesses();

           foreach ( var process in processes )
            {
                try
                {
                    if (process.MainModule.FileName.Equals(path, StringComparison.OrdinalIgnoreCase)) { 
                        list.Add(process);
                    }
                }
                catch (Exception)
                {
                    
                }
            }

            return list;
        }

        public static  async Task<Window> StartProcessAndGetWindowHandleAsync(string cmd, string parameters, string workdir = null, bool silent = false, bool guesWindow = false)
        {
           
            try
            {
                List<IntPtr> taskbarWindows1 = null;                
                if (guesWindow) {
                    taskbarWindows1 = ToolsWindow.GetTaskbarWindows();
                }

                var process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                process.StartInfo = startInfo;
                startInfo.FileName = cmd;
                startInfo.Arguments = parameters;

                if (workdir != null)
                {
                    startInfo.WorkingDirectory = workdir;
                }

                if (silent)
                {
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.CreateNoWindow = true;
                }

                process.Start();

                if (!process.HasExited) {
                    await System.Threading.Tasks.Task.Run(() => process.WaitForInputIdle());
                }

                //  check if process have main window
                if (process.MainWindowHandle != IntPtr.Zero)
                {
                    Window window = new Window();
                    window.Handle = process.MainWindowHandle;
                    return window;
                }

                // get all process windows and try find wisible window
                List<Window> processWindows = ToolsWindow.FindWindowByProcessId((uint)process.Id);
                if (processWindows.Count == 1)
                {
                    Window window = new Window();
                    window.Handle = processWindows[0].Handle;
                    return window;

                }

                if (guesWindow) // compare taskbar windows and check for change
                {
                    List<IntPtr> taskbarWindows2 = ToolsWindow.GetTaskbarWindows();
                    List<IntPtr> foundWindows = new List<IntPtr>();
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

                    foreach (IntPtr win3 in foundWindows)
                    {
                        string path = ToolsWindow.GetApplicationPathFromWindow(win3);
                        if (path == cmd)
                        {
                            Window window = new Window();
                            window.Handle = win3;
                            return window;
                        }
                    }                        
                }

                
            }
            catch (Exception ex)
            {

                Program.error(ex.Message);
            }

            return null;
        }

        public static void RunPowerShellCommand(Window window) 
        {
            if (Program.powershell == null)
            {
                Program.error("Powershell not available. Install powershell.");
                return;
            }

            Job.DoJob(
                new DoWorkEventHandler(
                    delegate (object o, DoWorkEventArgs args)
                    {
                        var job = (BackgroundJob)args.Argument;

                        if (TextTools.IsURL(window.command))
                        {
                            Process.Start(new ProcessStartInfo(window.command) { UseShellExecute = true });
                        }
                        else if (Directory.Exists(window.command))
                        {
                            System.Diagnostics.Process.Start("explorer.exe", window.command);
                        }
                        else if (File.Exists(window.command))
                        {
                            System.Diagnostics.Process.Start(window.command);
                        }
                        else
                        {
                            try
                            {
                                List<IntPtr> taskbarWindows1 = null;

                                if (window.matchNewWindow)
                                {
                                    taskbarWindows1 = ToolsWindow.GetTaskbarWindows();
                                }

                                if (window.silentCommand)
                                {
                                    try
                                    {
                                        using (PowerShell ps = PowerShell.Create())
                                        {
                                            ps.AddScript(window.command);

                                            // string workingDirectory = @"C:\Your\Desired\Path";
                                            // ps.AddScript($"Set-Location -Path \"{workingDirectory}\"");

                                            try
                                            {
                                                var result = ps.BeginInvoke();
                                                Program.write(result.ToString());
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
                                                Program.error(ex.Message);
                                            }

                                        }
                                    }
                                    catch (Exception ex)
                                    {

                                        Program.error(ex.Message);
                                    }

                                }
                                else
                                {
                                    Process process = new Process();
                                    ProcessStartInfo startInfo = new ProcessStartInfo();

                                    startInfo.FileName = Program.powershell;
                                    string escapedCmd = EscapeString(window.command);
                                    if (window.silentCommand)
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
                                    if (window.silentCommand)
                                    {
                                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                        startInfo.CreateNoWindow = true;
                                    }
                                    process.StartInfo = startInfo;
                                    process.Start();
                                }

                                if (window.matchNewWindow) // compare taskbar windows and check for change
                                {
                                    Thread.Sleep(1500);

                                    List<IntPtr> taskbarWindows2 = ToolsWindow.GetTaskbarWindows();
                                    List<IntPtr> foundWindows = new List<IntPtr>();

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

                                    if (foundWindows.Count == 1) {
                                        window.Handle = foundWindows[0];
                                        window.doubleClickCommand = false;                                        
                                        ToolsWindow.SetWindowData(window);
                                        window.Type = "COMMAND";
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Program.error("exception: " + ex.Message);
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
    }
}
