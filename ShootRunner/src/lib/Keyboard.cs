using System.Diagnostics;
using System.Runtime.InteropServices;

#nullable disable

#pragma warning disable IDE0130

namespace ShootRunner
{
    public class Keyboard
    {
        public static string TransformKeyCombination(string combination)
        {

            Dictionary<string, string> keyMapping = new()
            {
                { "CTRL", "^" },
                { "SHIFT", "+" },
                { "ALT", "%" }
            };

            string[] keys = combination.Split('+');
            string result = "";

            foreach (string key in keys)
            {
                string trimmedKey = key.Trim().ToUpper();

                if (keyMapping.TryGetValue(trimmedKey, out string value))
                {
                    result += value;
                }
                else
                {
                    result += trimmedKey.ToLower();    
                }
            }

            return result;
        }

        // SHORTCUT
        public static bool ParseShortcut(string commandShortcut, Shortcut shortcut)
        {
            if (commandShortcut == null)
            {
                return false;
            }

            string[] keys = commandShortcut.Split('+');

            bool win = false;
            bool ctrl = false;
            bool alt = false;
            bool shift = false;
            string key = null;

            foreach (string value in keys)
            {
                switch (value.ToUpper())
                {
                    case "CTRL":
                        ctrl = true;
                        break;
                    case "ALT":
                        alt = true;
                        break;
                    case "SHIFT":
                        shift = true;
                        break;
                    case "WIN":
                        win = true;
                        break;
                    case "LWIN":
                        win = true;
                        break;
                    case "RWIN":
                        win = true;
                        break;

                    default:
                        key = value.ToUpper();
                        break;
                }
            }

            if (((shortcut.ctrl && ctrl) || (!shortcut.ctrl && !ctrl)) &&
                ((shortcut.alt && alt) || (!shortcut.alt && !alt)) &&
                ((shortcut.shift && shift) || (!shortcut.shift && !shift)) &&
                ((shortcut.win && win) || (!shortcut.win && !win)) &&
                shortcut.key.Equals(key, StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }

        public static void KeyPress(string shortcut) {
            SendKeys.SendWait(TransformKeyCombination(shortcut));
        }



        public static void KeyPress2(string shortcut)
        {
            string[] keys = shortcut.Split('+');

            bool win = false;
            bool ctrl = false;
            bool alt = false;
            bool shift = false;
            string key = null;

            foreach (string value in keys)
            {
                switch (value.ToUpper())
                {
                    case "CTRL":
                        ctrl = true;
                        break;
                    case "ALT":
                        alt = true;
                        break;
                    case "SHIFT":
                        shift = true;
                        break;
                    case "WIN":
                        win = true;
                        break;
                    case "LWIN":
                        win = true;
                        break;
                    case "RWIN":
                        win = true;
                        break;
                    default:
                        key = value;
                        break;
                }
            }


            if (win) WinApi.keybd_event(WinApi.VK_LWIN, 0, WinApi.KEYEVENTF_KEYDOWN, UIntPtr.Zero);
            if (ctrl) WinApi.keybd_event(WinApi.VK_CONTROL, 0, WinApi.KEYEVENTF_KEYDOWN, UIntPtr.Zero);
            if (alt) WinApi.keybd_event(WinApi.VK_MENU, 0, WinApi.KEYEVENTF_KEYDOWN, UIntPtr.Zero);
            if (shift) WinApi.keybd_event(WinApi.VK_SHIFT, 0, WinApi.KEYEVENTF_KEYDOWN, UIntPtr.Zero);


            WinApi.keybd_event(WinApi.keysToByte[key], 0, WinApi.KEYEVENTF_KEYDOWN, UIntPtr.Zero);
            WinApi.keybd_event(WinApi.keysToByte[key], 0, WinApi.KEYEVENTF_KEYUP, UIntPtr.Zero);


            if (shift) WinApi.keybd_event(WinApi.VK_SHIFT, 0, WinApi.KEYEVENTF_KEYUP, UIntPtr.Zero);
            if (alt) WinApi.keybd_event(WinApi.VK_MENU, 0, WinApi.KEYEVENTF_KEYUP, UIntPtr.Zero);
            if (ctrl) WinApi.keybd_event(WinApi.VK_CONTROL, 0, WinApi.KEYEVENTF_KEYUP, UIntPtr.Zero);
            if (win) WinApi.keybd_event(WinApi.VK_LWIN, 0, WinApi.KEYEVENTF_KEYUP, UIntPtr.Zero);

        }

        public static void SandKeyPressToProcess(string shortcut, string path)
        {
            string[] keys = shortcut.Split('+');

            bool win = false;
            bool ctrl = false;
            bool alt = false;
            bool shift = false;
            string key = null;

            foreach (string value in keys)
            {
                switch (value.ToUpper())
                {
                    case "CTRL":
                        ctrl = true;
                        break;
                    case "ALT":
                        alt = true;
                        break;
                    case "SHIFT":
                        shift = true;
                        break;
                    case "WIN":
                        win = true;
                        break;
                    case "LWIN":
                        win = true;
                        break;
                    case "RWIN":
                        win = true;
                        break;
                    default:
                        key = value;
                        break;
                }
            }

            List<Process> list = SystemTools.FindProcess(path);

            foreach (var process in list)
            {
                IntPtr hWnd = process.MainWindowHandle;

                if (win) WinApi.PostMessage(hWnd, WinApi.WM_KEYDOWN, (IntPtr)WinApi.VK_LWIN, IntPtr.Zero);
                if (ctrl) WinApi.PostMessage(hWnd, WinApi.WM_KEYDOWN, (IntPtr)WinApi.VK_CONTROL, IntPtr.Zero);
                if (alt) WinApi.PostMessage(hWnd, WinApi.WM_KEYDOWN, (IntPtr)WinApi.VK_MENU, IntPtr.Zero);
                if (shift) WinApi.PostMessage(hWnd, WinApi.WM_KEYDOWN, (IntPtr)WinApi.VK_SHIFT, IntPtr.Zero);

                WinApi.PostMessage(hWnd, WinApi.WM_KEYDOWN, (IntPtr)WinApi.keysToByte[key], IntPtr.Zero);
                WinApi.PostMessage(hWnd, WinApi.WM_KEYUP, (IntPtr)WinApi.keysToByte[key], IntPtr.Zero);

                if (shift) WinApi.PostMessage(hWnd, WinApi.WM_KEYUP, (IntPtr)WinApi.VK_SHIFT, IntPtr.Zero);
                if (alt) WinApi.PostMessage(hWnd, WinApi.WM_KEYUP, (IntPtr)WinApi.VK_MENU, IntPtr.Zero);
                if (ctrl) WinApi.PostMessage(hWnd, WinApi.WM_KEYUP, (IntPtr)WinApi.VK_CONTROL, IntPtr.Zero);
                if (win) WinApi.PostMessage(hWnd, WinApi.WM_KEYUP, (IntPtr)WinApi.VK_LWIN, IntPtr.Zero);
            }

            

        }

        public static void SandKeyPressToCurrentWindow(string shortcut)
        {
            IntPtr windowHandle = WinApi.GetForegroundWindow();
            SandKeyPressToWindow(shortcut, windowHandle);
        }

        public static void SandKeyPressToWindow(string shortcut, IntPtr windowHandle)
        {
            if (windowHandle == IntPtr.Zero)
            {
                return;
            }

            string[] keys = shortcut.Split('+');

            bool win = false;
            bool ctrl = false;
            bool alt = false;
            bool shift = false;
            string key = null;

            foreach (string value in keys)
            {
                switch (value.ToUpper())
                {
                    case "CTRL":
                        ctrl = true;
                        break;
                    case "ALT":
                        alt = true;
                        break;
                    case "SHIFT":
                        shift = true;
                        break;
                    case "WIN":
                        win = true;
                        break;
                    case "LWIN":
                        win = true;
                        break;
                    case "RWIN":
                        win = true;
                        break;
                    default:
                        key = value;
                        break;
                }
            }

            IntPtr hWnd = windowHandle;

            /*int counter = 10;
            while (--counter >0 && KeyIsPressed()) {
                Program.log("Sleep");
                Thread.Sleep(200);
            }*/

            if (win) WinApi.PostMessage(hWnd, WinApi.WM_KEYDOWN, (IntPtr)WinApi.VK_LWIN, IntPtr.Zero);
            if (ctrl) WinApi.PostMessage(hWnd, WinApi.WM_KEYDOWN, (IntPtr)WinApi.VK_CONTROL, IntPtr.Zero);
            if (alt) WinApi.PostMessage(hWnd, WinApi.WM_KEYDOWN, (IntPtr)WinApi.VK_MENU, IntPtr.Zero);
            if (shift) WinApi.PostMessage(hWnd, WinApi.WM_KEYDOWN, (IntPtr)WinApi.VK_SHIFT, IntPtr.Zero);

            WinApi.PostMessage(hWnd, WinApi.WM_KEYDOWN, (IntPtr)WinApi.keysToByte[key], IntPtr.Zero);
            WinApi.PostMessage(hWnd, WinApi.WM_KEYUP, (IntPtr)WinApi.keysToByte[key], IntPtr.Zero);

            if (shift) WinApi.PostMessage(hWnd, WinApi.WM_KEYUP, (IntPtr)WinApi.VK_SHIFT, IntPtr.Zero);
            if (alt) WinApi.PostMessage(hWnd, WinApi.WM_KEYUP, (IntPtr)WinApi.VK_MENU, IntPtr.Zero);
            if (ctrl) WinApi.PostMessage(hWnd, WinApi.WM_KEYUP, (IntPtr)WinApi.VK_CONTROL, IntPtr.Zero);
            if (win) WinApi.PostMessage(hWnd, WinApi.WM_KEYUP, (IntPtr)WinApi.VK_LWIN, IntPtr.Zero);
        }

        static bool KeyIsPressed()
        {
            for (int keyCode = 0x08; keyCode <= 0xFE; keyCode++)
            {
                if ((WinApi.GetAsyncKeyState(keyCode) & 0x8000) != 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
