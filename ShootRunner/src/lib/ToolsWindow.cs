using System.Diagnostics;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable


namespace ShootRunner
{
    public class ToolsWindow
    {

        public const uint PROCESS_QUERY_INFORMATION = 0x0400;
        public const uint PROCESS_VM_READ = 0x0010;

        public const uint MOUSEEVENTF_MOVE = 0x0001;

        public const uint SWP_NOZORDER = 0x0004;
        public const uint SWP_NOSIZE = 0x0001;

        public const int SW_SHOW = 5;
        public const int SW_MINIMIZE = 6;
        public const int SW_RESTORE = 9;
        public const int SW_SHOWMINIMIZED = 2;

        public const int ICON_SMALL = 0;
        public const int ICON_BIG = 1;
        public const int ICON_SMALL2 = 2;

        public const int WM_GETICON = 0x7F;
        public const int WM_CLOSE = 0x0010;
        public const int WM_COMMAND = 0x0111;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int WM_NCHITTEST = 0x84;

        public const int GWL_EXSTYLE = -20;
        public const int GWL_STYLE = -16;

        public const int WS_EX_TOOLWINDOW = 0x00000080;
        public const long WS_POPUP = 0x80000000L;
        public const int WS_CAPTION = 0x00C00000;
        public const int WS_EX_NOREDIRECTIONBITMAP = 0x00200000;

        public const int MIN_ALL = 0x1A3;
        public const int MIN_ALL_UNDO = 0x1A0;

        public const int HTCAPTION = 0x2;
        public const int HTBOTTOMRIGHT = 17;

        private const uint DWMWA_CLOAKED = 14;

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        /**********************************************************************/

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, uint processId);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);

        [DllImport("psapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, int nSize);

        [DllImport("dwmapi.dll")]
        private static extern int DwmGetWindowAttribute(IntPtr hWnd, uint dwAttribute, out int pvAttribute, int cbAttribute);        

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        // FOCUS WINDOW
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        // FOCUS WINDOW
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // FOCUS WINDOW
        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        // FOCUS WINDOW
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        // FOCUS WINDOW
        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        // FOCUS WINDOW
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, UIntPtr dwExtraInfo);

        // RESTORE WINDOW
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        // RESTORE WINDOW
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int processId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetShellWindow();

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        /**********************************************************************/
        public static Window SetWindowData(Window window)
        {
            if (window.Handle != IntPtr.Zero)
            {
                try
                {
                    window.Type = "WINDOW";
                    window.Title = ToolsWindow.GetWindowTitle(window);
                    
                    if (window.processId == 0) {
                        window.processId = ToolsWindow.GetWindowProcessId(window);
                    }

                    if (window.app == null)
                    {
                        window.app = ToolsWindow.GetApplicationPathFromWindow(window);
                        if (window.app != null)
                        {
                            window.directory = Path.GetDirectoryName(window.app);
                            window.executable = Path.GetFileName(window.app);
                        }
                    }
                    //window.command = window.app;
                    if (window.className == null) {
                        window.className = ToolsWindow.GetWindowClassName(window);
                    }

                    if (window.icon == null)
                    {
                        Bitmap windowIcon = ToolsWindow.GetWindowIcon(window);
                        Bitmap appIcon = ToolsWindow.ExtractIconFromPath(window.app);

                        if (windowIcon != null && appIcon != null)
                        {
                            if (appIcon.Width >= windowIcon.Width)
                            {
                                windowIcon.Dispose();
                                window.icon = appIcon;
                            }else if (appIcon.Width < windowIcon.Width)
                            {
                                appIcon.Dispose();
                                window.icon = windowIcon;
                            }
                        } else if (windowIcon == null && appIcon != null)  {
                            window.icon = appIcon;
                        }
                        else if (windowIcon != null && appIcon == null)
                        {
                            window.icon = windowIcon;
                        }
                    }

                    window.isDesktop = ToolsWindow.IsDesktopWindow(window);
                    window.isTaskbar = ToolsWindow.IsTaskbarWindow(window);
                    if (!window.isGpuRenderedChecked) {
                        window.isGpuRenderedChecked = true;
                        window.isGpuRendered = ToolsWindow.IsGpuRenderedWindow(window);
                    }
                }
                catch (Exception ex)
                {
                    Program.error(ex.Message);
                }

            }

            return window;
        }


        public static List<IntPtr> GetAlWindows()
        {
            List<IntPtr> windows = new List<IntPtr>();

            try
            {
                EnumWindows((hWnd, lParam) =>
                {
                    try
                    {

                        if (excludedWindows.Contains(hWnd)) {
                            return true;
                        }

                        windows.Add(hWnd);
                    }
                    catch (Exception ex)
                    {

                        Program.error(ex.Message);
                    }

                    return true;
                }, IntPtr.Zero);
            }
            catch (Exception ex)
            {

                Program.error(ex.Message);
            }

            return windows;
        }

        public static IntPtr shellWindow = IntPtr.Zero;
        public static IntPtr GetShellWindowHandle() {
            if (shellWindow == IntPtr.Zero) {
                shellWindow = GetShellWindow();
            }

            return shellWindow;
        }

        public static List<IntPtr> excludedWindows = new List<IntPtr>();

        public static List<IntPtr> GetTaskbarWindows()
        {
            List<IntPtr> taskbarWindows = new List<IntPtr>();

            try
            {
                List<IntPtr> windows = GetAlWindows();

                foreach (IntPtr Handle in windows)
                {
                    try
                    {
                        if (excludedWindows.Contains(Handle)) {
                            continue;
                        }

                        if (!IsWindowVisible(Handle))
                        {
                            excludedWindows.Add(Handle);
                            continue;
                        }

                        /*if (IsWindowPopup(window.Handle))
                        {
                            excludedWindows.Add(window.Handle);
                            continue;
                        }*/

                        if (IsToolWindow(Handle))
                        {
                            excludedWindows.Add(Handle);
                            continue;
                        }

                        if (Handle == GetShellWindowHandle())
                        {
                            excludedWindows.Add(Handle);
                            continue;
                        }

                        string title = ToolsWindow.GetWindowTitle(Handle);

                        if (string.IsNullOrEmpty(title))
                        {
                            excludedWindows.Add(Handle);
                            continue;
                        }

                        if (IsWindowCloaked(Handle))
                        {
                            excludedWindows.Add(Handle);
                            continue;
                        }

                        taskbarWindows.Add(Handle);
                    }
                    catch (Exception ex)
                    {

                        Program.error(ex.Message);
                    }

                }
            }
            catch (Exception ex)
            {

                Program.error(ex.Message);
            }


            return taskbarWindows;
        }

        public static string GetWindowClassName(Window window)
        {
            return GetWindowClassName(window.Handle);
        }

        public static string GetWindowClassName(IntPtr Handle)
        {
            StringBuilder className = new StringBuilder(256);
            GetClassName(Handle, className, className.Capacity);
            return className.ToString();
        }

        public static IntPtr GetCurrentWindow()
        {
            IntPtr activeWindowHandle = ToolsWindow.GetForegroundWindow();

            if (activeWindowHandle == IntPtr.Zero)
            {
                return IntPtr.Zero;
            }
            return activeWindowHandle;
        }

        static void SimulateMouseInput()
        {
            mouse_event(MOUSEEVENTF_MOVE, 0, 1, 0, UIntPtr.Zero);
            mouse_event(MOUSEEVENTF_MOVE, 0, -1, 0, UIntPtr.Zero);

        }

        private static IntPtr FindWindowByPartialName(string partialName)
        {
            IntPtr foundWindow = IntPtr.Zero;

            EnumWindows((hWnd, lParam) =>
            {

                StringBuilder windowText = new StringBuilder(256);
                GetWindowText(hWnd, windowText, windowText.Capacity);

                Program.debug(windowText.ToString());

                if (windowText.ToString().ToUpper().Contains(partialName.ToUpper()))
                {
                    foundWindow = hWnd;
                    return false;
                }
                return true; 
            }, IntPtr.Zero);

            return foundWindow;
        }

        public static Bitmap ExtractIconFromPath(string path) {
            if (!File.Exists(path)) {
                return null;
            }

            Icon icon = Icon.ExtractAssociatedIcon(path);

            if (icon == null) {
                return null;
            }

            Bitmap clonedIcon = ((Icon)icon.Clone()).ToBitmap();
            return clonedIcon;
        }

        public static void CascadeWindows(int offsetX = 100, int offsetY = 100)
        {
            List<IntPtr> windowHandles = new List<IntPtr>();

            // EnumWindows callback function to collect window handles
            EnumWindows((hWnd, lParam) =>
            {
                if (IsWindowVisible(hWnd))
                {
                    System.Text.StringBuilder windowText = new System.Text.StringBuilder(256);
                    GetWindowText(hWnd, windowText, windowText.Capacity);

                    if (!string.IsNullOrWhiteSpace(windowText.ToString())) // Filter empty window titles
                    {
                        windowHandles.Add(hWnd);
                    }
                }
                return true; // Continue enumeration
            }, IntPtr.Zero);

            // Cascade the windows
            int x = 0, y = 0;
            foreach (IntPtr hWnd in windowHandles)
            {
                SetWindowPos(hWnd, IntPtr.Zero, x, y, 600, 600, SWP_NOZORDER);
                x += offsetX;
                y += offsetY;
            }
        }

        public static string GetApplicationPathFromWindow(Window window)
        {
            return GetApplicationPathFromWindow(window.Handle);
        }
        public static string GetApplicationPathFromWindow(IntPtr Handle)
        {
            GetWindowThreadProcessId(Handle,out int processId);

            IntPtr hProcess = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, false, (uint)processId);

            if (hProcess != IntPtr.Zero)
            {
                StringBuilder filePath = new StringBuilder(1024);

                if (GetModuleFileNameEx(hProcess, IntPtr.Zero, filePath, filePath.Capacity) > 0)
                {
                    CloseHandle(hProcess);
                    return filePath.ToString();
                }

                CloseHandle(hProcess);
            }

            return null;
        }

        public static bool IsWindowValid(Window window)
        {
            return IsWindow(window.Handle);
        }

        public static Bitmap GetWindowIcon(Window window)
        {
            IntPtr hIcon = SendMessage(window.Handle, WM_GETICON, ICON_BIG, 0);

            if (hIcon == IntPtr.Zero)
            {
                hIcon = SendMessage(window.Handle, WM_GETICON, ICON_SMALL, 0);
            }

            if (hIcon == IntPtr.Zero)
            {
                hIcon = SendMessage(window.Handle, WM_GETICON, ICON_SMALL2, 0);
            }

            if (hIcon != IntPtr.Zero)
            {
                Icon icon = Icon.FromHandle(hIcon);

                Bitmap clonedIcon = ((Icon)icon.Clone()).ToBitmap(); ;

                DestroyIcon(hIcon);

                return clonedIcon;
            }

            return null;
        }

        public static bool IsMinimalized(Window window)
        {
            if (window == null || !ToolsWindow.IsWindowValid(window))
            {
                return false;
            }

            bool isMinimized = IsIconic(window.Handle);
            return isMinimized;
        }

        private static bool IsDesktopWindow(Window window)
        {
            // Class names for the desktop window
            string[] desktopClasses = { "Progman", "WorkerW" };
            StringBuilder className = new StringBuilder(256);

            // Get the class name of the window handle
            if (GetClassName(window.Handle, className, className.Capacity) > 0)
            {
                // Check if the class name matches the desktop window classes
                foreach (var desktopClass in desktopClasses)
                {
                    if (className.ToString().Equals(desktopClass, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool IsTaskbarWindow(Window window)
        {
            IntPtr foregroundWindow = GetForegroundWindow();
            StringBuilder className = new StringBuilder(256);
            GetClassName(window.Handle, className, className.Capacity);
            return className.ToString() == "Shell_TrayWnd";
        }

        public static bool IsWindowPopup(IntPtr hHandle)
        {
            const long WS_POPUP = 0x80000000L;
            long style = (long)GetWindowLongPtr(hHandle, -16);
            bool isPopup = ((style & WS_POPUP) != 0);
            return isPopup;
        }

        public static bool IsToolWindow(IntPtr hWnd)
        {
            IntPtr exStyle = GetWindowLongPtr(hWnd, GWL_EXSTYLE);
            return (exStyle.ToInt64() & WS_EX_TOOLWINDOW) == WS_EX_TOOLWINDOW;
        }

        public static void SetAsWindowPopup(Window window)
        {
            try
            {
                int style = GetWindowLong(window.Handle, GWL_STYLE);
                SetWindowLong(window.Handle, GWL_STYLE, (int)(style | WS_POPUP));
            }
            catch (Exception ex)
            {

                Program.error(ex.Message);
            }
        }

        public static void SetToolWindow(Window window)
        {
            try
            {
                int style = GetWindowLong(window.Handle, GWL_EXSTYLE);
                SetWindowLong(window.Handle, GWL_EXSTYLE, style | WS_EX_TOOLWINDOW);
            }
            catch (Exception ex)
            {

                Program.error(ex.Message);
            }
        }        

        public static void MinimizeWindow(Window window)
        {
            if (window.Handle != IntPtr.Zero && ToolsWindow.IsWindowValid(window))
            {
                ShowWindow(window.Handle, SW_MINIMIZE);
            }
        }

        public static bool BringWindowToFront(Window window)
        {

            if (window.Handle != IntPtr.Zero)
            {
                SimulateMouseInput();
                if (ToolsWindow.IsMinimalized(window))
                {
                    ShowWindow(window.Handle, SW_RESTORE);
                }
                SetForegroundWindow(window.Handle);
                SimulateMouseInput();
                return true;
            }

            return false;

        }

        public static bool BringToFront(string appName)
        {

            IntPtr hWnd = FindWindowByPartialName(appName);

            if (hWnd != IntPtr.Zero)
            {
                SimulateMouseInput();
                if (IsIconic(hWnd))
                {
                    ShowWindow(hWnd, SW_RESTORE);
                }
                SetForegroundWindow(hWnd);
                SimulateMouseInput();
                return true;
            }

            return false;

        }

        public static string GetWindowTitle(Window window)
        {
            StringBuilder windowText = new StringBuilder(256);
            GetWindowText(window.Handle, windowText, windowText.Capacity);
            return windowText.ToString();
        }

        public static string GetWindowTitle(IntPtr Handle)
        {            
            StringBuilder windowText = new StringBuilder(256);
            GetWindowText(Handle, windowText, windowText.Capacity);
            return windowText.ToString();
        }

        public static void CloseWindow(Window window)
        {
            SendMessage(window.Handle, WM_CLOSE, (int)IntPtr.Zero, (int)IntPtr.Zero);
        }

        public static void ShowDesktop(bool undo = false)
        {

            IntPtr hWnd = FindWindow("Shell_TrayWnd", null);
            if (hWnd != IntPtr.Zero)
            {
                if (undo)
                {

                    SendMessage(hWnd, WM_COMMAND, (IntPtr)MIN_ALL_UNDO, IntPtr.Zero);

                }
                else
                {

                    SendMessage(hWnd, WM_COMMAND, (IntPtr)MIN_ALL, IntPtr.Zero);

                }
            }

        }

        public static bool AllWindowsMinimalized()
        {
            bool allMinimized = true;

            try
            {
                EnumWindows((hWnd, lParam) =>
                {
                    if (IsWindowVisible(hWnd) && !IsIconic(hWnd))
                    {
                        allMinimized = false;
                        return false; 
                    }
                    return true;
                }, IntPtr.Zero);
            }
            catch (Exception ex)
            {

                Program.error(ex.Message);
            }

            return allMinimized;
        }

        public static void RemoveTitleBar(IntPtr hWnd)
        {
            int style = GetWindowLong(hWnd, GWL_STYLE);
            SetWindowLong(hWnd, GWL_STYLE, style & ~WS_CAPTION);
        }

        public static void AddTitleBar(IntPtr hWnd)
        {
            int style = GetWindowLong(hWnd, GWL_STYLE);
            SetWindowLong(hWnd, GWL_STYLE, style | WS_CAPTION);
        }

        public static List<Window> FindWindowByProcessId(int processId)
        {
            List<Window> windows = new List<Window>();

            try
            {

                EnumWindows((hWnd, lParam) =>
                {
                    try
                    {
                        GetWindowThreadProcessId(hWnd, out int windowProcessId);
                        if (windowProcessId == processId)
                        {
                            StringBuilder windowText = new StringBuilder(256);
                            GetWindowText(hWnd, windowText, 256);

                            if (string.IsNullOrWhiteSpace(windowText.ToString()))
                            {
                                return true;
                            }

                            if (!IsWindowVisible(hWnd))
                                return true;

                            if (IsWindowPopup(hWnd))
                            {
                                return true;
                            }

                            if (IsToolWindow(hWnd))
                            {
                                return true;
                            }

                            Window window = new Window();
                            window.Handle = hWnd;
                            window.Title = windowText.ToString();
                            windows.Add(window);
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.error(ex.Message);
                    }

                    return true;
                }, IntPtr.Zero);
            }
            catch (Exception ex)
            {

                Program.error(ex.Message);
            }

            return windows;
        }


        public static int GetWindowProcessId(Window window)
        {
            return GetWindowProcessId(window.Handle);
        }


        public static int GetWindowProcessId(IntPtr Handle) {

            try
            {
                GetWindowThreadProcessId(Handle, out int processId);
                return processId;
            }
            catch (Exception ex)
            {

                Program.error(ex.Message);
            }
            return 0;
        }

        public static bool IsGpuLibraryLoaded(Window window)
        {
            int processId = window.processId;
            if (processId == 0) {
                processId = ToolsWindow.GetWindowProcessId(window);
            }

            if (processId == 0)
            {
                return false;
            }

            try
            {
                Process process = Process.GetProcessById(window.processId);
                return process.Modules.Cast<ProcessModule>().Any(m => m.ModuleName.Contains("d3d") || m.ModuleName.Contains("opengl"));
            }
            catch (Exception ex)
            {
                Program.error(ex.Message);
            }

            return false;
        }

        public static bool IsWindowNoBitmapType(Window window)
        {
            try
            {
                IntPtr style = GetWindowLongPtr(window.Handle, GWL_EXSTYLE);
                return (style.ToInt64() & WS_EX_NOREDIRECTIONBITMAP) != 0;
            }
            catch (Exception ex)
            {
                Program.error(ex.Message);
            }

            return false;
        }

        public static bool IsGpuRenderedWindow(Window window)
        {
            return IsWindowNoBitmapType(window) || IsGpuLibraryLoaded(window);
        }

        /*************************************************************************/



        public static Rectangle? GetWindowPosition(IntPtr hWnd)
        {
            if (GetWindowRect(hWnd, out RECT rect))
            {
                return new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            }
            return null;
        }

        public static bool WindowHasPosition(IntPtr hWnd)
        {
            if (GetWindowRect(hWnd, out RECT rect))
            {
                if (rect.Left >= 0 &&
                    rect.Top >= 0 &&
                    (rect.Right - rect.Left) > 0 &&
                    (rect.Bottom - rect.Top) > 0
                    ) { 
                    return true;
                }
                
            }
            throw new InvalidOperationException("Unable to get window position.");
        }



        public static bool IsWindowCloaked(IntPtr hWnd)
        {
            if (DwmGetWindowAttribute(hWnd, DWMWA_CLOAKED, out int isCloaked, Marshal.SizeOf(typeof(int))) == 0)
            {
                return isCloaked != 0;
            }
            return false;
        }



    }



}
