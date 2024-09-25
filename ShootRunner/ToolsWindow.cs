using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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
        public static bool  BringToFront(string appName)
        {

            IntPtr hWnd = FindWindowByPartialName(appName);

            if (hWnd != IntPtr.Zero)
            {
                SimulateMouseInput();
                ShowWindow(hWnd, SW_RESTORE);
                SetForegroundWindow(hWnd);
                SimulateMouseInput();
                return true;
            }

            return false;

        }

        private static IntPtr FindWindowByPartialName(string partialName)
        {
            IntPtr foundWindow = IntPtr.Zero;

            EnumWindows((hWnd, lParam) =>
            {

                StringBuilder windowText = new StringBuilder(256);
                GetWindowText(hWnd, windowText, windowText.Capacity);

                Program.log(windowText.ToString());

                if (windowText.ToString().ToUpper().Contains(partialName.ToUpper()))
                {
                    foundWindow = hWnd;
                    return false;
                }
                return true; 
            }, IntPtr.Zero);

            return foundWindow;
        }
    }
}
