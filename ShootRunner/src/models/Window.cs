using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ShootRunner
{
    public class Window
    {
        public String Title = "";
        public IntPtr Handle = IntPtr.Zero;
        public string app = null;
        public Icon icon = null;
        public bool isDesktop = false;
        public bool isTaskbar = false;
    }
}
