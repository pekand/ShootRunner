#nullable disable

#pragma warning disable IDE0130

namespace ShootRunner
{
    public class Pin : IDisposable
    {
        // HANDLE
        public bool useWindow = false;
        public Window window = null;

        // COMMAND
        public bool useCommand = false;
        public string command = null;
        public bool useWorkdir = true;
        public string workdir = null;
        public bool silentCommand = true;
        public bool matchNewWindow = true;
        public bool doubleClickCommand = false; // to activate use doble click instead of one click        
        public bool usePowershell = true;
        public bool useCmdshell = false;

        // FILE
        public bool useFilelink = false;
        public string filelink = null;

        // DIRECTORY
        public bool useDirectorylink = false;
        public string directorylink = null;

        // HYPERLINK
        public bool useHyperlink = false;
        public string hyperlink = null;

        // SCRIPT
        public bool useScript = false;
        public string script = null;

        // ICON
        public Bitmap customicon = null;

        // PROPERTIES
        public bool locked = false;
        public double transparent = 1.0;
        public double opacity = 1.0;
        public bool mosttop = true;
        public bool hidden = false;

        private bool disposed = false;

        ~Pin()
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
                    customicon?.Dispose();
                }

                disposed = true;
            }
        }
    }
}
