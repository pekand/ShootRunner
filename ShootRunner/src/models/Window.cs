#nullable disable

namespace ShootRunner
{
    public class Window:IDisposable
    {
        public String Type = ""; // WINDOW, COMMAND 

        public String Title = "";

        // HANDLE
        public IntPtr Handle = IntPtr.Zero;
        public bool isDesktop = false;
        public bool isTaskbar = false;

        // WINDOW
        public uint processId = 0;
        public string className = null;
        public string app = null;
        public string directory = null;
        public string executable = null;

        // COMMAND
        public string command = null;
        public bool silentCommand = true; 
        public bool doubleClickCommand = false; // to activate use doble click instead of one click
       
        // ICON
        public Bitmap icon = null;
        public Bitmap customicon = null;

        // SCRENSHOT
        public Bitmap screenshot = null;
        public bool isCurentWindowScreensot = false;        

        // PROPERTIES
        public bool locked = false;
        public double transparent = 1.0;
        public bool mosttop = false;
        public bool hidden = false;

        private bool disposed = false;

        ~Window()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    icon?.Dispose();
                    customicon?.Dispose();
                    screenshot?.Dispose();
                }

                disposed = true;
            }
        }
    }
}
