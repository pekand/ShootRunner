using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

#nullable disable


namespace ShootRunner
{
    public class JobTask
    {
        public static void RunCommand(string command, string parameters, string workdir = "") {
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
        }

        public static void RunCommand(string cmd, string workdir = null, bool silent = false) //b39d265706
        {
            Job.DoJob(
                new DoWorkEventHandler(
                    delegate (object o, DoWorkEventArgs args)
                    {
                        if (TextTools.IsURL(cmd)) {
                            Process.Start(new ProcessStartInfo(cmd) { UseShellExecute = true });
                        }
                        else if (Directory.Exists(cmd))
                        {
                            System.Diagnostics.Process.Start("explorer.exe", cmd);
                        }
                        else if(File.Exists(cmd))
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


                                startInfo.FileName = "cmd.exe";
                                startInfo.Arguments = "/C " + cmd;

                                startInfo.WorkingDirectory = workdir;
                                startInfo.UseShellExecute = true;
                                startInfo.RedirectStandardOutput = false;
                                startInfo.RedirectStandardError = false;
                                startInfo.CreateNoWindow = false;
                                process.StartInfo = startInfo;
                                process.Start();
                                /*string output = process.StandardOutput.ReadToEnd();
                                string error = process.StandardError.ReadToEnd();
                                process.WaitForExit();
                                Program.log.Write("output: " + output);
                                Program.log.Write("error: " + error);*/
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
                                /*

    Get-Process
    Write-Host 'Second Command Executed'
    Get-Service
    Write-Host "Script paused. Press Enter to continue..."
    Read-Host

                                 */
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
                                                Console.WriteLine($"Error: {ex.Message}");
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
                                    /*string output = process.StandardOutput.ReadToEnd();
                                    string error = process.StandardError.ReadToEnd();
                                    process.WaitForExit();
                                    Program.log.Write("output: " + output);
                                    Program.log.Write("error: " + error);*/
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

        public static  async Task<Window> StartProcessAndGetWindowHandleAsync(string cmd, string workdir = null, bool silent = false)
        {
            Window window = new Window();

            /*  IntPtr handle = await StartProcessAndGetWindowHandleAsync("notepad.exe"); */

            var process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            process.StartInfo = startInfo;
            startInfo.FileName = cmd;

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

            await System.Threading.Tasks.Task.Delay(2000);

            await System.Threading.Tasks.Task.Run(() => process.WaitForInputIdle());

            window.Handle = process.MainWindowHandle;

            return window;
        }
    }
}
