using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ShootRunner
{
    public class ToolsWindow
    {
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

        const uint MOUSEEVENTF_MOVE = 0x0001;

        // RESTORE WINDOW
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        // RESTORE WINDOW
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        // RESTORE WINDOW
        private const int SW_SHOW = 5;
        private const int SW_RESTORE = 9;

        // FOCUS WINDOW
        static void SimulateMouseInput()
        {
            mouse_event(MOUSEEVENTF_MOVE, 0, 1, 0, UIntPtr.Zero);
            mouse_event(MOUSEEVENTF_MOVE, 0, -1, 0, UIntPtr.Zero);

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

        public static string GetWindowTitle(Window window) {
            StringBuilder windowText = new StringBuilder(256);
            GetWindowText(window.Handle, windowText, windowText.Capacity);
            return windowText.ToString();
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

        public static Window GetCurrentWindow()
        {
            Window window = new Window();

            IntPtr activeWindowHandle = ToolsWindow.GetForegroundWindow();
            if (activeWindowHandle != IntPtr.Zero) {
                window.Type = "WINDOW";
                window.Handle = activeWindowHandle;
                window.Title = ToolsWindow.GetWindowTitle(window);
                window.icon = ToolsWindow.GetWindowIcon(window);
                window.app = ToolsWindow.GetApplicationPathFromWindow(window);
                Bitmap appIcon = ToolsWindow.ExtractIconFromPath(window.app);
                if (window.icon == null || (appIcon!=null && window.icon != null && appIcon.Width > window.icon.Width)) {
                    window.icon = appIcon;
                }
                window.isDesktop = ToolsWindow.IsDesktopWindow(window);
                window.isTaskbar = ToolsWindow.IsTaskbarWindow(window);
            }
            
            return window;
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool IsWindow(IntPtr hWnd);

        public static bool IsWindowValid(Window window)
        {
            return IsWindow(window.Handle);
        }


        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool DestroyIcon(IntPtr hIcon);

        private const int WM_GETICON = 0x7F;
        private const int ICON_SMALL = 0;
        private const int ICON_BIG = 1;
        private const int ICON_SMALL2 = 2;

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

        // Import necessary functions from Windows API
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, uint processId);

        [DllImport("psapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, int nSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);

        // Process access rights
        const uint PROCESS_QUERY_INFORMATION = 0x0400;
        const uint PROCESS_VM_READ = 0x0010;

        public static string GetApplicationPathFromWindow(Window window)
        {
            uint processId;
            // Get process ID from the window handle
            GetWindowThreadProcessId(window.Handle, out processId);

            // Open the process
            IntPtr hProcess = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, false, processId);

            if (hProcess != IntPtr.Zero)
            {
                StringBuilder filePath = new StringBuilder(1024);
                // Get the file name (application path) of the process
                if (GetModuleFileNameEx(hProcess, IntPtr.Zero, filePath, filePath.Capacity) > 0)
                {
                    CloseHandle(hProcess);
                    return filePath.ToString();
                }

                CloseHandle(hProcess);
            }

            return null;
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

        // Import the IsIconic function from User32.dll
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsIconic(IntPtr hWnd);

        public static bool IsMinimalized(Window window)
        {
            if (window == null || !ToolsWindow.IsWindowValid(window))
            {
                return false;
            }

            bool isMinimized = IsIconic(window.Handle);
            return isMinimized;
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);


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


        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private const uint SWP_NOZORDER = 0x0004;
        private const uint SWP_NOSIZE = 0x0001;


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

        ///////////////////////////////////////////////////////////////////////////

        public static List<Window> GetTaskbarWindows()
        {
            List<Window> windowHandles = new List<Window>();

            EnumWindows((hWnd, lParam) =>
            {
                if (IsWindowVisible(hWnd))
                {
                    StringBuilder title = new StringBuilder(256);
                    GetWindowText(hWnd, title, 256);
                    if (!string.IsNullOrEmpty(title.ToString()))
                    {
                        Window window = new Window();
                        window.Type = "WINDOW";
                        window.Handle = hWnd;
                        window.Title = ToolsWindow.GetWindowTitle(window);
                        window.app = ToolsWindow.GetApplicationPathFromWindow(window);
                        windowHandles.Add(window);
                    }
                }
                return true;
            }, IntPtr.Zero);

            return windowHandles;
        }

        ///////////////////////////////////////////////////////////////////////////


        // Constants for ShowWindow
        private const int SW_MINIMIZE = 6;
        private const int WM_CLOSE = 0x0010;

        public static void MinimizeWindow(Window window)
        {
            if (window.Handle != IntPtr.Zero && ToolsWindow.IsWindowValid(window))
            {
                ShowWindow(window.Handle, SW_MINIMIZE);
            }
        }

        public static void CloseWindow(Window window)
        {
            SendMessage(window.Handle, WM_CLOSE, (int)IntPtr.Zero, (int)IntPtr.Zero);
        }

    }

    

}
