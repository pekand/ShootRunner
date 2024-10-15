using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootRunner
{
    public class Task
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
    }
}
