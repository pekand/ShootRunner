#nullable disable

#pragma warning disable IDE0130

namespace ShootRunner
{
    public class Pin : IDisposable
    {
        public const double DefaultMaxCommandExecutionTime = 60.0;

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
        public double maxExecTime = DefaultMaxCommandExecutionTime;

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

        public Pin ClonePin()
        {
            Pin newPin = new();

            newPin.useWindow = this.useWindow;
            newPin.window = this.window.CloneWindow();
            newPin.useCommand = this.useCommand;
            newPin.command = this.command;
            newPin.useWorkdir = this.useWorkdir;
            newPin.workdir = this.workdir;
            newPin.silentCommand = this.silentCommand;
            newPin.matchNewWindow = this.matchNewWindow;
            newPin.doubleClickCommand = this.doubleClickCommand;
            newPin.usePowershell = this.usePowershell;
            newPin.useCmdshell = this.useCmdshell;
            newPin.useFilelink = this.useFilelink;
            newPin.filelink = this.filelink;
            newPin.useDirectorylink = this.useDirectorylink;
            newPin.directorylink = this.directorylink;
            newPin.useHyperlink = this.useHyperlink;
            newPin.hyperlink = this.hyperlink;
            newPin.useScript = this.useScript;
            newPin.script = this.script;
            newPin.customicon = Duplicate.FastClone(this.customicon);
            newPin.locked = this.locked;
            newPin.transparent = this.transparent;
            newPin.opacity = this.opacity;
            newPin.mosttop = this.mosttop;
            newPin.hidden = this.hidden;
            newPin.disposed = this.disposed;

            return newPin;
        }
    }
}
