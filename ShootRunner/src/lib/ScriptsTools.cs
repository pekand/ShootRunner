﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootRunner
{
    public class ScriptsTools
    {
        public static string IsCommandAvailable(string cmd = "")
        {
            // Get the PATH environment variable
            string pathEnv = Environment.GetEnvironmentVariable("PATH");

            if (string.IsNullOrEmpty(pathEnv))
                return null;

            // Split the PATH variable into its individual directories
            string[] paths = pathEnv.Split(Path.PathSeparator);

            // Check each directory for pwsh.exe
            foreach (string path in paths)
            {
                string cmdPath = Path.Combine(path, cmd);

                if (File.Exists(cmdPath))
                {
                    return cmdPath;
                }
            }

            return null;
        }
    }
}
