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

        // ICON
        public Bitmap icon = null;

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
                    screenshot?.Dispose();
                }

                disposed = true;
            }
        }        
    }
}
