using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable

#pragma warning disable IDE0079
#pragma warning disable IDE0130
#pragma warning disable CA2211

namespace ShootRunner
{
    public class ToolsWindow
    {
        public static IntPtr shellWindow = IntPtr.Zero;

        public static List<IntPtr> excludedWindows = [];
        public static List<IntPtr> includedWindows = [];

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
                    window.className ??= ToolsWindow.GetWindowClassName(window);

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
                }
                catch (Exception ex)
                {
                    Program.Error(ex.Message);
                }

            }

            return window;
        }

        public static List<IntPtr> GetAlWindows()
        {
            List<IntPtr> windows = [];

            try
            {
                WinApi.EnumWindows((hWnd, lParam) =>
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

                        Program.Error(ex.Message);
                    }

                    return true;
                }, IntPtr.Zero);
            }
            catch (Exception ex)
            {

                Program.Error(ex.Message);
            }

            return windows;
        }

        public static IntPtr GetShellWindowHandle() {
            if (shellWindow == IntPtr.Zero) {
                shellWindow = WinApi.GetShellWindow();
            }

            return shellWindow;
        }

        public static List<IntPtr> GetTaskbarWindows(bool allowExclude = false)
        {
            List<IntPtr> taskbarWindows = [];

            try
            {
                List<IntPtr> windows = GetAlWindows();

                foreach (IntPtr Handle in windows)
                {
                    try
                    {
                        /*Window w = new Window(); //debug
                        w.Handle = Handle;
                        ToolsWindow.SetWindowData(w);*/

                        if (excludedWindows.Contains(Handle)) {
                            continue;
                        }

                        if (!WinApi.IsWindowVisible(Handle))
                        {
                            if(allowExclude) excludedWindows.Add(Handle);
                            continue;
                        }

                        if (!IsWindowVisible2(Handle))
                        {
                            if (allowExclude) excludedWindows.Add(Handle);
                            continue;
                        }

                        /*if (IsWindowPopup(window.Handle))
                        {
                            excludedWindows.Add(window.Handle);
                            continue;
                        }*/

                        if (IsUWPWindow(Handle) && IsWindowCloaked(Handle))
                        {
                            if (allowExclude) excludedWindows.Add(Handle);
                            continue;
                        }

                        if (includedWindows.Contains(Handle))
                        {
                            taskbarWindows.Add(Handle);
                            continue;
                        }

                        if (IsToolWindow(Handle))
                        {
                            if (allowExclude) excludedWindows.Add(Handle);
                            continue;
                        }

                        if (Handle == GetShellWindowHandle()) // is explorer window
                        {
                            if (allowExclude) excludedWindows.Add(Handle);
                            continue;
                        }

                        string title = ToolsWindow.GetWindowTitle(Handle);

                        if (string.IsNullOrEmpty(title))
                        {
                            if (allowExclude) excludedWindows.Add(Handle);
                            continue;
                        }

                        string className = GetWindowClassName(Handle); 
                        if (className == "Windows.UI.Core.CoreWindow") {  // is container window
                            if (allowExclude) excludedWindows.Add(Handle);
                            continue;
                        }

                        if (GetWindowSessionId(Handle) <= 0)
                        {
                            if (allowExclude) excludedWindows.Add(Handle);
                            continue;
                        }

                        includedWindows.Add(Handle);

                        taskbarWindows.Add(Handle);
                    }
                    catch (Exception ex)
                    {
                        excludedWindows.Add(Handle);
                        Program.Error(ex.Message);
                    }

                }
            }
            catch (Exception ex)
            {

                Program.Error(ex.Message);
            }


            return taskbarWindows;
        }

        public static string GetWindowClassName(Window window)
        {
            return GetWindowClassName(window.Handle);
        }

        public static string GetWindowClassName(IntPtr Handle)
        {
            try
            {
                StringBuilder className = new(256);
                int result = WinApi.GetClassName(Handle, className, className.Capacity);
                return className.ToString();
            }
            catch (Exception)
            {

                return "";
            }
            
        }

        public static IntPtr GetCurrentWindow()
        {
            IntPtr activeWindowHandle = WinApi.GetForegroundWindow();

            if (activeWindowHandle == IntPtr.Zero)
            {
                return IntPtr.Zero;
            }
            return activeWindowHandle;
        }

        static void SimulateMouseInput()
        {
            WinApi.mouse_event(WinApi.MOUSEEVENTF_MOVE, 0, 1, 0, UIntPtr.Zero);
            WinApi.mouse_event(WinApi.MOUSEEVENTF_MOVE, 0, -1, 0, UIntPtr.Zero);

        }

        private static IntPtr FindWindowByPartialName(string partialName)
        {
            IntPtr foundWindow = IntPtr.Zero;

            WinApi.EnumWindows((hWnd, lParam) =>
            {

                StringBuilder windowText = new(256);
                int result = WinApi.GetWindowText(hWnd, windowText, windowText.Capacity);

                Program.Debug(windowText.ToString());

                if (windowText.ToString().Contains(partialName, StringComparison.CurrentCultureIgnoreCase))
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
            List<IntPtr> windowHandles = [];

            // EnumWindows callback function to collect window handles
            WinApi.EnumWindows((hWnd, lParam) =>
            {
                if (WinApi.IsWindowVisible(hWnd))
                {
                    StringBuilder windowText = new(256);
                    int result = WinApi.GetWindowText(hWnd, windowText, windowText.Capacity);

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
                WinApi.SetWindowPos(hWnd, IntPtr.Zero, x, y, 600, 600, WinApi.SWP_NOZORDER);
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
            uint result = WinApi.GetWindowThreadProcessId(Handle,out uint processId);

            IntPtr hProcess = WinApi.OpenProcess(WinApi.PROCESS_QUERY_INFORMATION | WinApi.PROCESS_VM_READ, false, processId);

            if (hProcess != IntPtr.Zero)
            {
                StringBuilder filePath = new(1024);

                if (WinApi.GetModuleFileNameEx(hProcess, IntPtr.Zero, filePath, filePath.Capacity) > 0)
                {
                    WinApi.CloseHandle(hProcess);
                    return filePath.ToString();
                }

                WinApi.CloseHandle(hProcess);
            }

            return null;
        }

        public static bool IsWindowValid(Window window)
        {
            return WinApi.IsWindow(window.Handle);
        }

        public static Bitmap GetWindowIcon(Window window)
        {
            IntPtr hIcon = WinApi.SendMessage(window.Handle, WinApi.WM_GETICON, WinApi.ICON_BIG, 0);

            if (hIcon == IntPtr.Zero)
            {
                hIcon = WinApi.SendMessage(window.Handle, WinApi.WM_GETICON, WinApi.ICON_SMALL, 0);
            }

            if (hIcon == IntPtr.Zero)
            {
                hIcon = WinApi.SendMessage(window.Handle, WinApi.WM_GETICON, WinApi.ICON_SMALL2, 0);
            }

            if (hIcon != IntPtr.Zero)
            {
                Icon icon = Icon.FromHandle(hIcon);

                Bitmap clonedIcon = ((Icon)icon.Clone()).ToBitmap(); ;

                WinApi.DestroyIcon(hIcon);

                return clonedIcon;
            }

            return null;
        }

        public static bool IsMaximalized(IntPtr hWnd)
        {
            if (WinApi.IsZoomed(hWnd))
            {
                return true;
            }

            return false;
        }

        public static bool IsMinimalized(Window window)
        {
            if (window == null || !ToolsWindow.IsWindowValid(window))
            {
                return false;
            }

            bool isMinimized = WinApi.IsIconic(window.Handle);
            return isMinimized;
        }

        public static bool IsDesktopWindow(Window window)
        {
            // Class names for the desktop window
            string[] desktopClasses = ["Progman", "WorkerW"];
            StringBuilder className = new(256);

            // Get the class name of the window handle
            if (WinApi.GetClassName(window.Handle, className, className.Capacity) > 0)
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

        public static bool IsTaskbarWindow(Window window)
        {
            StringBuilder className = new(256);
            int result = WinApi.GetClassName(window.Handle, className, className.Capacity);
            return className.ToString() == "Shell_TrayWnd";
        }

        public static bool IsWindowPopup(IntPtr hHandle)
        {
            const long WS_POPUP = 0x80000000L;
            long style = (long)WinApi.GetWindowLongPtr(hHandle, -16);
            bool isPopup = ((style & WS_POPUP) != 0);
            return isPopup;
        }

        public static bool IsToolWindow(IntPtr hWnd)
        {
            IntPtr exStyle = WinApi.GetWindowLongPtr(hWnd, WinApi.GWL_EXSTYLE);
            return (exStyle.ToInt64() & WinApi.WS_EX_TOOLWINDOW) == WinApi.WS_EX_TOOLWINDOW;
        }

        public static void SetAsWindowPopup(Window window)
        {
            try
            {
                int style = WinApi.GetWindowLong(window.Handle, WinApi.GWL_STYLE);
                int result = WinApi.SetWindowLong(window.Handle, WinApi.GWL_STYLE, (int)(style | WinApi.WS_POPUP));
            }
            catch (Exception ex)
            {

                Program.Error(ex.Message);
            }
        }

        public static void SetToolWindow(Window window)
        {
            try
            {
                int style = WinApi.GetWindowLong(window.Handle, WinApi.GWL_EXSTYLE);
                int result = WinApi.SetWindowLong(window.Handle, WinApi.GWL_EXSTYLE, style | WinApi.WS_EX_TOOLWINDOW);
            }
            catch (Exception ex)
            {

                Program.Error(ex.Message);
            }
        }        

        public static void MinimizeWindow(Window window)
        {
            if (window.Handle != IntPtr.Zero && ToolsWindow.IsWindowValid(window))
            {
                WinApi.ShowWindow(window.Handle, WinApi.SW_MINIMIZE);
            }
        }

        public static bool BringWindowToFront(Window window)
        {            
            if (window.Handle != IntPtr.Zero)
            {
                SimulateMouseInput();
                if (ToolsWindow.IsMinimalized(window))
                {
                    WinApi.ShowWindow(window.Handle, WinApi.SW_RESTORE);
                }
                WinApi.SetForegroundWindow(window.Handle);
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
                if (WinApi.IsIconic(hWnd))
                {
                    WinApi.ShowWindow(hWnd, WinApi.SW_RESTORE);
                }
                WinApi.SetForegroundWindow(hWnd);
                SimulateMouseInput();
                return true;
            }

            return false;

        }

        public static string GetWindowTitle(Window window)
        {
            StringBuilder windowText = new(256);
            int result = WinApi.GetWindowText(window.Handle, windowText, windowText.Capacity);
            return windowText.ToString();
        }

        public static string GetWindowTitle(IntPtr Handle)
        {            
            StringBuilder windowText = new(256);
            int result = WinApi.GetWindowText(Handle, windowText, windowText.Capacity);
            return windowText.ToString();
        }

        public static void CloseWindow(Window window)
        {
            WinApi.SendMessage(window.Handle, WinApi.WM_CLOSE, (int)IntPtr.Zero, (int)IntPtr.Zero);
        }

        public static void ShowDesktop(bool undo = false)
        {

            IntPtr hWnd = WinApi.FindWindow("Shell_TrayWnd", null);
            if (hWnd != IntPtr.Zero)
            {
                if (undo)
                {

                    WinApi.SendMessage(hWnd, WinApi.WM_COMMAND, (IntPtr)WinApi.MIN_ALL_UNDO, IntPtr.Zero);

                }
                else
                {

                    WinApi.SendMessage(hWnd, WinApi.WM_COMMAND, (IntPtr)WinApi.MIN_ALL, IntPtr.Zero);

                }
            }

        }

        public static bool AllWindowsMinimalized()
        {
            bool allMinimized = true;

            try
            {
                WinApi.EnumWindows((hWnd, lParam) =>
                {
                    if (WinApi.IsWindowVisible(hWnd) && !WinApi.IsIconic(hWnd))
                    {
                        allMinimized = false;
                        return false; 
                    }
                    return true;
                }, IntPtr.Zero);
            }
            catch (Exception ex)
            {

                Program.Error(ex.Message);
            }

            return allMinimized;
        }

        public static void RemoveTitleBar(IntPtr hWnd)
        {
            int style = WinApi.GetWindowLong(hWnd, WinApi.GWL_STYLE);
            int result = WinApi.SetWindowLong(hWnd, WinApi.GWL_STYLE, style & ~WinApi.WS_CAPTION);
        }

        public static void AddTitleBar(IntPtr hWnd)
        {
            int style = WinApi.GetWindowLong(hWnd, WinApi.GWL_STYLE);
            int result = WinApi.SetWindowLong(hWnd, WinApi.GWL_STYLE, style | WinApi.WS_CAPTION);
        }

        public static List<Window> FindWindowByProcessId(uint processId)
        {
            List<Window> windows = [];

            try
            {

                WinApi.EnumWindows((hWnd, lParam) =>
                {
                    try
                    {
                        uint result = WinApi.GetWindowThreadProcessId(hWnd, out uint windowProcessId);
                        if (windowProcessId == processId)
                        {
                            StringBuilder windowText = new(256);
                            int result2 = WinApi.GetWindowText(hWnd, windowText, 256);

                            if (string.IsNullOrWhiteSpace(windowText.ToString()))
                            {
                                return true;
                            }

                            if (!WinApi.IsWindowVisible(hWnd))
                                return true;

                            if (IsWindowPopup(hWnd))
                            {
                                return true;
                            }

                            if (IsToolWindow(hWnd))
                            {
                                return true;
                            }

                            Window window = new()
                            {
                                Handle = hWnd,
                                Title = windowText.ToString()
                            };
                            windows.Add(window);
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.Error(ex.Message);
                    }

                    return true;
                }, IntPtr.Zero);
            }
            catch (Exception ex)
            {

                Program.Error(ex.Message);
            }

            return windows;
        }

        public static uint GetWindowProcessId(Window window)
        {
            return GetWindowProcessId(window.Handle);
        }

        public static uint GetWindowProcessId(IntPtr Handle) {

            try
            {
                uint result = WinApi.GetWindowThreadProcessId(Handle, out uint processId);
                return processId;
            }
            catch (Exception ex)
            {

                Program.Error(ex.Message);
            }
            return 0;
        }

        public static bool IsGpuLibraryLoaded(Window window)
        {
            uint processId = window.processId;
            if (processId == 0) {
                processId = ToolsWindow.GetWindowProcessId(window);
            }

            if (processId == 0)
            {
                return false;
            }

            try
            {
                Process process = Process.GetProcessById((int)window.processId);
                return process.Modules.Cast<ProcessModule>().Any(m => m.ModuleName.Contains("d3d") || m.ModuleName.Contains("opengl"));
            }
            catch (Exception ex)
            {
                Program.Error(ex.Message);
            }

            return false;
        }

        public static bool IsWindowNoBitmapType(Window window)
        {
            try
            {
                IntPtr style = WinApi.GetWindowLongPtr(window.Handle, WinApi.GWL_EXSTYLE);
                return (style.ToInt64() & WinApi.WS_EX_NOREDIRECTIONBITMAP) != 0;
            }
            catch (Exception ex)
            {
                Program.Error(ex.Message);
            }

            return false;
        }

        public static bool IsGpuRenderedWindow(Window window)
        {
            return IsWindowNoBitmapType(window) || IsGpuLibraryLoaded(window);
        }

        public static Rectangle? GetWindowPosition(IntPtr hWnd)
        {
            if (WinApi.GetWindowRect(hWnd, out WinApi.RECT rect))
            {
                return new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            }
            return null;
        }

        public static bool WindowHasPosition(IntPtr hWnd)
        {
            if (WinApi.GetWindowRect(hWnd, out WinApi.RECT rect))
            {
                if (rect.Left >= 0 &&
                    rect.Top >= 0 &&
                    (rect.Right - rect.Left) > 0 &&
                    (rect.Bottom - rect.Top) > 0
                    ) { 
                    return true;
                }
                
            }

            return false;
        }

        public static bool IsWindowCloaked(IntPtr hWnd)
        {
            if (WinApi.DwmGetWindowAttribute(hWnd, WinApi.DWMWA_CLOAKED, out int isCloaked, Marshal.SizeOf(typeof(int))) == 0)
            {
                return isCloaked != 0;
            }
            return false;
        }

        public static bool IsUWPWindow(IntPtr Handle) {

            return GetWindowClassName(Handle) == "ApplicationFrameWindow";
        }

        public static bool IsWindowVisible2(IntPtr hWnd)
        {
            int style = WinApi.GetWindowLong(hWnd, WinApi.GWL_STYLE);
            return ((style & WinApi.WS_VISIBLE) != 0) || ((style & WinApi.WS_DISABLED) != 0);
        }

        public static uint GetWindowSessionId(IntPtr hWnd)
        {
            try
            {

                if (WinApi.GetWindowThreadProcessId(hWnd, out uint processId) == 0)
                {
                    return 0;
                }


                if (!WinApi.ProcessIdToSessionId(processId, out uint sessionId))
                {
                    return 0;
                }

                return sessionId;
            }
            catch
            {
            }

            return 0;
        }

        private const long WS_CHILD = 0x40000000;
        public static bool IsChild(IntPtr Handle) {
            long style = WinApi.GetWindowLongPtr(Handle, WinApi.GWL_STYLE).ToInt64();
            if ((style & WS_CHILD) == WS_CHILD) return true;

            return false;
        }

        public static bool HaveParent(IntPtr Handle) {
            return WinApi.GetParent(Handle) != IntPtr.Zero;
        }
    }



}
