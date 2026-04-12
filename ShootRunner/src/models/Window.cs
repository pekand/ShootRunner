#nullable disable

#pragma warning disable IDE0130

namespace ShootRunner
{
    public class Window(IntPtr? Handle = null) : IDisposable
    {
        public String Type = ""; // WINDOW, COMMAND 

        public String Title = "";

        // HANDLE
        public IntPtr Handle = Handle ?? IntPtr.Zero;
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

        public Window CloneWindow()
        {
            Window newWindow = new();

            newWindow.Type = this.Type;
            newWindow.Title = this.Title;
            newWindow.Handle = this.Handle;
            newWindow.isDesktop = this.isDesktop;
            newWindow.isTaskbar = this.isTaskbar;
            newWindow.processId = this.processId;
            newWindow.className = this.className;
            newWindow.app = this.app;
            newWindow.directory = this.directory;
            newWindow.executable = this.executable;
            newWindow.command = this.command;
            newWindow.icon = Duplicate.FastClone(this.icon);
            newWindow.screenshot = Duplicate.FastClone(this.screenshot);
            newWindow.isCurentWindowScreensot = this.isCurentWindowScreensot;
            newWindow.locked = this.locked;
            newWindow.transparent = this.transparent;
            newWindow.mosttop = this.mosttop;
            newWindow.hidden = this.hidden;
            newWindow.disposed = this.disposed;

            return newWindow;
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
