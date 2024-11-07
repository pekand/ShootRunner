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
        public String Type = ""; // WINDOW, COMMAND 
        public String Title = "";
        public IntPtr Handle = IntPtr.Zero;
        public string app = null;
        public string command = null;
        public bool silentCommand = true; 
        public bool doubleClickCommand = false; // to activate use doble click instead of one click
        public Bitmap icon = null;
        public bool isDesktop = false;
        public bool isTaskbar = false;
        public bool locked = false;
        public double transparent = 1.0;
        public bool mosttop = false;
        public List<WindowProperty> props = null;

    }
}
