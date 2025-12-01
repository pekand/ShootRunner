using System.Runtime.InteropServices;
using System.Text;

#pragma warning disable IDE0079
#pragma warning disable CA1401
#pragma warning disable CA2101
#pragma warning disable SYSLIB1054
#pragma warning disable CA2211
#pragma warning disable IDE0130

namespace ShootRunner
{
    public class WinApi
    {
        // HOOK
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        // HOOK
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        // HOOK
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        // HOOK
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        // HOOK
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        // HOOK
        public const int WH_KEYBOARD_LL = 13;
        public const int WM_KEYDOWN = 0x0100;
        public const int WM_SYSKEYDOWN = 0x0104;
        public const int WM_KEYUP = 0x0101;
        public static IntPtr _hookID = IntPtr.Zero;
        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        public const int WM_WTSSESSION_CHANGE = 0x02B1;
        public const int NOTIFY_FOR_THIS_SESSION = 0;

        [DllImport("Wtsapi32.dll")]
        public static extern bool WTSRegisterSessionNotification(IntPtr hWnd, [MarshalAs(UnmanagedType.U4)] int dwFlags);

        [DllImport("Wtsapi32.dll")]
        public static extern bool WTSUnRegisterSessionNotification(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        public static extern int BitBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSrc, int xSrc, int ySrc, int RasterOp);

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hdc);

        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, uint nFlags);

        public const int SRCCOPY = 0x00CC0020;

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

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
        public const int WS_VISIBLE = 0x10000000;
        public const int WS_DISABLED = 0x08000000;

        public const int MIN_ALL = 0x1A3;
        public const int MIN_ALL_UNDO = 0x1A0;

        public const int HTCAPTION = 0x2;
        public const int HTBOTTOMRIGHT = 17;
        public const int HTTOPLEFT = 13;
        public const int HTTOPRIGHT = 14;
        public const int HTBOTTOMLEFT = 16;

        public const uint DWMWA_CLOAKED = 14;

        public const int WM_VSCROLL = 0x0115;
        public const int SB_BOTTOM = 7;

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, uint processId);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll")]
        public static extern bool ProcessIdToSessionId(uint processId, out uint sessionId);

        [DllImport("psapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, int nSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hWnd, uint dwAttribute, out int pvAttribute, int cbAttribute);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        // FOCUS WINDOW
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        // FOCUS WINDOW
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // FOCUS WINDOW
        [DllImport("user32.dll")]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        // FOCUS WINDOW
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        // FOCUS WINDOW
        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        // FOCUS WINDOW
        [DllImport("user32.dll")]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, UIntPtr dwExtraInfo);

        // RESTORE WINDOW
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        // RESTORE WINDOW
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr GetShellWindow();

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern bool IsZoomed(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool LockWorkStation();

        // Import necessary Windows API functions
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public static Dictionary<string, int> keysToByte = new()
        {
        {"KeyCode", 0xFFFF},
        {"Modifiers", -65536},
        {"None", 0},
        {"LButton", 1},
        {"RButton", 2},
        {"Cancel", 3},
        {"MButton", 4},
        {"XButton1", 5},
        {"XButton2", 6},
        {"Back", 8},
        {"Tab", 9},
        {"LineFeed", 0xA},
        {"Clear", 0xC},
        {"Return", 0xD},
        {"Enter", 0xD},
        {"ShiftKey", 0x10},
        {"ControlKey", 0x11},
        {"Menu", 0x12},
        {"Pause", 0x13},
        {"Capital", 0x14},
        {"CapsLock", 0x14},
        {"KanaMode", 0x15},
        {"HanguelMode", 0x15},
        {"HangulMode", 0x15},
        {"JunjaMode", 0x17},
        {"FinalMode", 0x18},
        {"HanjaMode", 0x19},
        {"KanjiMode", 0x19},
        {"Escape", 0x1B},
        {"IMEConvert", 0x1C},
        {"IMENonconvert", 0x1D},
        {"IMEAccept", 0x1E},
        {"IMEAceept", 0x1E},
        {"IMEModeChange", 0x1F},
        {"Space", 0x20},
        {"Prior", 0x21},
        {"PageUp", 0x21},
        {"Next", 0x22},
        {"PageDown", 0x22},
        {"End", 0x23},
        {"Home", 0x24},
        {"Left", 0x25},
        {"Up", 0x26},
        {"Right", 0x27},
        {"Down", 0x28},
        {"Select", 0x29},
        {"Print", 0x2A},
        {"Execute", 0x2B},
        {"Snapshot", 0x2C},
        {"PrintScreen", 0x2C},
        {"Insert", 0x2D},
        {"Delete", 0x2E},
        {"Help", 0x2F},
        {"D0", 0x30},
        {"D1", 0x31},
        {"D2", 0x32},
        {"D3", 0x33},
        {"D4", 0x34},
        {"D5", 0x35},
        {"D6", 0x36},
        {"D7", 0x37},
        {"D8", 0x38},
        {"D9", 0x39},
        {"A", 0x41},
        {"B", 0x42},
        {"C", 0x43},
        {"D", 0x44},
        {"E", 0x45},
        {"F", 0x46},
        {"G", 0x47},
        {"H", 0x48},
        {"I", 0x49},
        {"J", 0x4A},
        {"K", 0x4B},
        {"L", 0x4C},
        {"M", 0x4D},
        {"N", 0x4E},
        {"O", 0x4F},
        {"P", 0x50},
        {"Q", 0x51},
        {"R", 0x52},
        {"S", 0x53},
        {"T", 0x54},
        {"U", 0x55},
        {"V", 0x56},
        {"W", 0x57},
        {"X", 0x58},
        {"Y", 0x59},
        {"Z", 0x5A},
        {"LWin", 0x5B},
        {"RWin", 0x5C},
        {"Apps", 0x5D},
        {"Sleep", 0x5F},
        {"NumPad0", 0x60},
        {"NumPad1", 0x61},
        {"NumPad2", 0x62},
        {"NumPad3", 0x63},
        {"NumPad4", 0x64},
        {"NumPad5", 0x65},
        {"NumPad6", 0x66},
        {"NumPad7", 0x67},
        {"NumPad8", 0x68},
        {"NumPad9", 0x69},
        {"Multiply", 0x6A},
        {"Add", 0x6B},
        {"Separator", 0x6C},
        {"Subtract", 0x6D},
        {"Decimal", 0x6E},
        {"Divide", 0x6F},
        {"F1", 0x70},
        {"F2", 0x71},
        {"F3", 0x72},
        {"F4", 0x73},
        {"F5", 0x74},
        {"F6", 0x75},
        {"F7", 0x76},
        {"F8", 0x77},
        {"F9", 0x78},
        {"F10", 0x79},
        {"F11", 0x7A},
        {"F12", 0x7B},
        {"F13", 0x7C},
        {"F14", 0x7D},
        {"F15", 0x7E},
        {"F16", 0x7F},
        {"F17", 0x80},
        {"F18", 0x81},
        {"F19", 0x82},
        {"F20", 0x83},
        {"F21", 0x84},
        {"F22", 0x85},
        {"F23", 0x86},
        {"F24", 0x87},
        {"NumLock", 0x90},
        {"Scroll", 0x91},
        {"LShiftKey", 0xA0},
        {"RShiftKey", 0xA1},
        {"LControlKey", 0xA2},
        {"RControlKey", 0xA3},
        {"LMenu", 0xA4},
        {"RMenu", 0xA5},
        {"BrowserBack", 0xA6},
        {"BrowserForward", 0xA7},
        {"BrowserRefresh", 0xA8},
        {"BrowserStop", 0xA9},
        {"BrowserSearch", 0xAA},
        {"BrowserFavorites", 0xAB},
        {"BrowserHome", 0xAC},
        {"VolumeMute", 0xAD},
        {"VolumeDown", 0xAE},
        {"VolumeUp", 0xAF},
        {"MediaNextTrack", 0xB0},
        {"MediaPreviousTrack", 0xB1},
        {"MediaStop", 0xB2},
        {"MediaPlayPause", 0xB3},
        {"LaunchMail", 0xB4},
        {"SelectMedia", 0xB5},
        {"LaunchApplication1", 0xB6},
        {"LaunchApplication2", 0xB7},
        {"OemSemicolon", 0xBA},
        {"Oem1", 0xBA},
        {"Oemplus", 0xBB},
        {"Oemcomma", 0xBC},
        {"OemMinus", 0xBD},
        {"OemPeriod", 0xBE},
        {"OemQuestion", 0xBF},
        {"Oem2", 0xBF},
        {"Oemtilde", 0xC0},
        {"Oem3", 0xC0},
        {"OemOpenBrackets", 0xDB},
        {"Oem4", 0xDB},
        {"OemPipe", 0xDC},
        {"Oem5", 0xDC},
        {"OemCloseBrackets", 0xDD},
        {"Oem6", 0xDD},
        {"OemQuotes", 0xDE},
        {"Oem7", 0xDE},
        {"Oem8", 0xDF},
        {"OemBackslash", 0xE2},
        {"Oem102", 0xE2},
        {"ProcessKey", 0xE5},
        {"Packet", 0xE7},
        {"Attn", 0xF6},
        {"Crsel", 0xF7},
        {"Exsel", 0xF8},
        {"EraseEof", 0xF9},
        {"Play", 0xFA},
        {"Zoom", 0xFB},
        {"NoName", 0xFC},
        {"Pa1", 0xFD},
        {"OemClear", 0xFE},
        {"Shift", 0x10000},
        {"Control", 0x20000},
        {"Alt", 0x40000}
        };

        public const int VK_LWIN = 0x5B;
        public const int VK_CONTROL = 0x11;
        public const int VK_MENU = 0x12;
        public const int VK_SHIFT = 0x10;

        public const uint KEYEVENTF_KEYDOWN = 0x0000;
        public const uint KEYEVENTF_KEYUP = 0x0002;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern void keybd_event(int bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        [StructLayout(LayoutKind.Sequential)]
        public struct MSG
        {
            public IntPtr hwnd;
            public uint message;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public int pt_x;
            public int pt_y;
        }

        public const uint EVENT_OBJECT_CREATE = 0x8000;
        public const uint EVENT_OBJECT_DESTROY = 0x8001;
        public const uint EVENT_OBJECT_SHOW = 0x8002;
        public const uint EVENT_OBJECT_HIDE = 0x8003;
        public const uint EVENT_OBJECT_CLOAKED = 0x8017;
        public const uint EVENT_OBJECT_UNCLOAKED = 0x8018;

        public const uint WINEVENT_OUTOFCONTEXT = 0x0000;
        public const uint PM_REMOVE = 0x0001;

        public const int INFINITE = -1;
        public const int WAIT_OBJECT_0 = 0;
        public const int WAIT_TIMEOUT = 0x102;
        public const uint QS_ALLEVENTS = 0x04FF;

        public const int WM_QUIT = 0x0012;
        public const int WM_CUSTOM_STOP = 0x9001; // Custom message to stop

        public delegate void WinEventDelegate(
    IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild,
    uint dwEventThread, uint dwmsEventTime);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWinEventHook(
            uint eventMin, uint eventMax, IntPtr hmodWinEventProc,
            WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        [DllImport("user32.dll")]
        public static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        [DllImport("user32.dll")]
        public static extern int GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

        [DllImport("user32.dll")]
        public static extern bool PostThreadMessage(uint idThread, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        public static extern uint GetCurrentThreadId();

        [DllImport("user32.dll")]
        public static extern bool PeekMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

        [DllImport("user32.dll")]
        public static extern uint MsgWaitForMultipleObjects(uint nCount, IntPtr[] pHandles, bool bWaitAll, uint dwMilliseconds, uint dwWakeMask);

        [DllImport("user32.dll")]
        public static extern IntPtr DispatchMessage(ref MSG lpmsg);

        [DllImport("user32.dll")]
        public static extern IntPtr TranslateMessage(ref MSG lpmsg);
    }
}
