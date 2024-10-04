using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Shell32;

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

        public static void ShowDesktop()
        {
            Shell32.ShellClass objShel = new Shell32.ShellClass();
            objShel.ToggleDesktop();
        }
    }
}
