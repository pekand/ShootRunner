using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShootRunner
{
    

    public class Keyboard
    {
        public static string TransformKeyCombination(string combination)
        {

            Dictionary<string, string> keyMapping = new Dictionary<string, string>
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

                if (keyMapping.ContainsKey(trimmedKey))
                {
                    result += keyMapping[trimmedKey];
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
            bool lwin = false;
            bool rwin = false;
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
                shortcut.key.ToUpper() == key)
            {
                return true;
            }

            return false;
        }

        public static void KeyPress(string shortcut) {
            SendKeys.SendWait(TransformKeyCombination(shortcut));
        }

        const byte VK_LWIN = 0x5B;
        const byte VK_CONTROL = 0x11;
        const byte VK_MENU = 0x12;
        const byte VK_SHIFT = 0x10;

        
        const uint KEYEVENTF_KEYDOWN = 0x0000; 
        const uint KEYEVENTF_KEYUP = 0x0002;

        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(int bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        public static void KeyPress2(string shortcut)
        {
            string[] keys = shortcut.Split('+');

            bool win = false;
            bool lwin = false;
            bool rwin = false;
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


            if (win) keybd_event(VK_LWIN, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero);
            if (ctrl) keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero);
            if (alt) keybd_event(VK_MENU, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero);
            if (shift) keybd_event(VK_SHIFT, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero);


            keybd_event(Keyboard.keysToByte[key], 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero); 
            keybd_event(Keyboard.keysToByte[key], 0, KEYEVENTF_KEYUP, UIntPtr.Zero);


            if (shift) keybd_event(VK_SHIFT, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
            if (alt) keybd_event(VK_MENU, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
            if (ctrl) keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
            if (win) keybd_event(VK_LWIN, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);

        }

        public static Dictionary<string, int> keysToByte = new Dictionary<string, int>
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

        // SHORTCUT
        public static string KeyToName(Keys key)
        {
            string keyName = "";

            if (key == Keys.KeyCode) keyName = "KeyCode";
            if (key == Keys.Modifiers) keyName = "Modifiers";
            if (key == Keys.None) keyName = "None";
            if (key == Keys.LButton) keyName = "LButton";
            if (key == Keys.RButton) keyName = "RButton";
            if (key == Keys.Cancel) keyName = "Cancel";
            if (key == Keys.MButton) keyName = "MButton";
            if (key == Keys.XButton1) keyName = "XButton1";
            if (key == Keys.XButton2) keyName = "XButton2";
            if (key == Keys.Back) keyName = "Back";
            if (key == Keys.Tab) keyName = "Tab";
            if (key == Keys.LineFeed) keyName = "LineFeed";
            if (key == Keys.Clear) keyName = "Clear";
            if (key == Keys.Return) keyName = "Return";
            if (key == Keys.Enter) keyName = "Enter";
            if (key == Keys.ShiftKey) keyName = "ShiftKey";
            if (key == Keys.ControlKey) keyName = "ControlKey";
            if (key == Keys.Menu) keyName = "Menu";
            if (key == Keys.Pause) keyName = "Pause";
            if (key == Keys.Capital) keyName = "Capital";
            if (key == Keys.CapsLock) keyName = "CapsLock";
            if (key == Keys.KanaMode) keyName = "KanaMode";
            if (key == Keys.HanguelMode) keyName = "HanguelMode";
            if (key == Keys.HangulMode) keyName = "HangulMode";
            if (key == Keys.JunjaMode) keyName = "JunjaMode";
            if (key == Keys.FinalMode) keyName = "FinalMode";
            if (key == Keys.HanjaMode) keyName = "HanjaMode";
            if (key == Keys.KanjiMode) keyName = "KanjiMode";
            if (key == Keys.Escape) keyName = "Escape";
            if (key == Keys.IMEConvert) keyName = "IMEConvert";
            if (key == Keys.IMENonconvert) keyName = "IMENonconvert";
            if (key == Keys.IMEAccept) keyName = "IMEAccept";
            if (key == Keys.IMEAceept) keyName = "IMEAceept";
            if (key == Keys.IMEModeChange) keyName = "IMEModeChange";
            if (key == Keys.Space) keyName = "Space";
            if (key == Keys.Prior) keyName = "Prior";
            if (key == Keys.PageUp) keyName = "PageUp";
            if (key == Keys.Next) keyName = "Next";
            if (key == Keys.PageDown) keyName = "PageDown";
            if (key == Keys.End) keyName = "End";
            if (key == Keys.Home) keyName = "Home";
            if (key == Keys.Left) keyName = "Left";
            if (key == Keys.Up) keyName = "Up";
            if (key == Keys.Right) keyName = "Right";
            if (key == Keys.Down) keyName = "Down";
            if (key == Keys.Select) keyName = "Select";
            if (key == Keys.Print) keyName = "Print";
            if (key == Keys.Execute) keyName = "Execute";
            if (key == Keys.Snapshot) keyName = "Snapshot";
            if (key == Keys.PrintScreen) keyName = "PrintScreen";
            if (key == Keys.Insert) keyName = "Insert";
            if (key == Keys.Delete) keyName = "Delete";
            if (key == Keys.Help) keyName = "Help";
            if (key == Keys.D0) keyName = "D0";
            if (key == Keys.D1) keyName = "D1";
            if (key == Keys.D2) keyName = "D2";
            if (key == Keys.D3) keyName = "D3";
            if (key == Keys.D4) keyName = "D4";
            if (key == Keys.D5) keyName = "D5";
            if (key == Keys.D6) keyName = "D6";
            if (key == Keys.D7) keyName = "D7";
            if (key == Keys.D8) keyName = "D8";
            if (key == Keys.D9) keyName = "D9";
            if (key == Keys.A) keyName = "A";
            if (key == Keys.B) keyName = "B";
            if (key == Keys.C) keyName = "C";
            if (key == Keys.D) keyName = "D";
            if (key == Keys.E) keyName = "E";
            if (key == Keys.F) keyName = "F";
            if (key == Keys.G) keyName = "G";
            if (key == Keys.H) keyName = "H";
            if (key == Keys.I) keyName = "I";
            if (key == Keys.J) keyName = "J";
            if (key == Keys.K) keyName = "K";
            if (key == Keys.L) keyName = "L";
            if (key == Keys.M) keyName = "M";
            if (key == Keys.N) keyName = "N";
            if (key == Keys.O) keyName = "O";
            if (key == Keys.P) keyName = "P";
            if (key == Keys.Q) keyName = "Q";
            if (key == Keys.R) keyName = "R";
            if (key == Keys.S) keyName = "S";
            if (key == Keys.T) keyName = "T";
            if (key == Keys.U) keyName = "U";
            if (key == Keys.V) keyName = "V";
            if (key == Keys.W) keyName = "W";
            if (key == Keys.X) keyName = "X";
            if (key == Keys.Y) keyName = "Y";
            if (key == Keys.Z) keyName = "Z";
            if (key == Keys.LWin) keyName = "LWin";
            if (key == Keys.RWin) keyName = "RWin";
            if (key == Keys.Apps) keyName = "Apps";
            if (key == Keys.Sleep) keyName = "Sleep";
            if (key == Keys.NumPad0) keyName = "NumPad0";
            if (key == Keys.NumPad1) keyName = "NumPad1";
            if (key == Keys.NumPad2) keyName = "NumPad2";
            if (key == Keys.NumPad3) keyName = "NumPad3";
            if (key == Keys.NumPad4) keyName = "NumPad4";
            if (key == Keys.NumPad5) keyName = "NumPad5";
            if (key == Keys.NumPad6) keyName = "NumPad6";
            if (key == Keys.NumPad7) keyName = "NumPad7";
            if (key == Keys.NumPad8) keyName = "NumPad8";
            if (key == Keys.NumPad9) keyName = "NumPad9";
            if (key == Keys.Multiply) keyName = "Multiply";
            if (key == Keys.Add) keyName = "Add";
            if (key == Keys.Separator) keyName = "Separator";
            if (key == Keys.Subtract) keyName = "Subtract";
            if (key == Keys.Decimal) keyName = "Decimal";
            if (key == Keys.Divide) keyName = "Divide";
            if (key == Keys.F1) keyName = "F1";
            if (key == Keys.F2) keyName = "F2";
            if (key == Keys.F3) keyName = "F3";
            if (key == Keys.F4) keyName = "F4";
            if (key == Keys.F5) keyName = "F5";
            if (key == Keys.F6) keyName = "F6";
            if (key == Keys.F7) keyName = "F7";
            if (key == Keys.F8) keyName = "F8";
            if (key == Keys.F9) keyName = "F9";
            if (key == Keys.F10) keyName = "F10";
            if (key == Keys.F11) keyName = "F11";
            if (key == Keys.F12) keyName = "F12";
            if (key == Keys.F13) keyName = "F13";
            if (key == Keys.F14) keyName = "F14";
            if (key == Keys.F15) keyName = "F15";
            if (key == Keys.F16) keyName = "F16";
            if (key == Keys.F17) keyName = "F17";
            if (key == Keys.F18) keyName = "F18";
            if (key == Keys.F19) keyName = "F19";
            if (key == Keys.F20) keyName = "F20";
            if (key == Keys.F21) keyName = "F21";
            if (key == Keys.F22) keyName = "F22";
            if (key == Keys.F23) keyName = "F23";
            if (key == Keys.F24) keyName = "F24";
            if (key == Keys.NumLock) keyName = "NumLock";
            if (key == Keys.Scroll) keyName = "Scroll";
            if (key == Keys.LShiftKey) keyName = "LShiftKey";
            if (key == Keys.RShiftKey) keyName = "RShiftKey";
            if (key == Keys.LControlKey) keyName = "LControlKey";
            if (key == Keys.RControlKey) keyName = "RControlKey";
            if (key == Keys.LMenu) keyName = "LMenu";
            if (key == Keys.RMenu) keyName = "RMenu";
            if (key == Keys.BrowserBack) keyName = "BrowserBack";
            if (key == Keys.BrowserForward) keyName = "BrowserForward";
            if (key == Keys.BrowserRefresh) keyName = "BrowserRefresh";
            if (key == Keys.BrowserStop) keyName = "BrowserStop";
            if (key == Keys.BrowserSearch) keyName = "BrowserSearch";
            if (key == Keys.BrowserFavorites) keyName = "BrowserFavorites";
            if (key == Keys.BrowserHome) keyName = "BrowserHome";
            if (key == Keys.VolumeMute) keyName = "VolumeMute";
            if (key == Keys.VolumeDown) keyName = "VolumeDown";
            if (key == Keys.VolumeUp) keyName = "VolumeUp";
            if (key == Keys.MediaNextTrack) keyName = "MediaNextTrack";
            if (key == Keys.MediaPreviousTrack) keyName = "MediaPreviousTrack";
            if (key == Keys.MediaStop) keyName = "MediaStop";
            if (key == Keys.MediaPlayPause) keyName = "MediaPlayPause";
            if (key == Keys.LaunchMail) keyName = "LaunchMail";
            if (key == Keys.SelectMedia) keyName = "SelectMedia";
            if (key == Keys.LaunchApplication1) keyName = "LaunchApplication1";
            if (key == Keys.LaunchApplication2) keyName = "LaunchApplication2";
            if (key == Keys.OemSemicolon) keyName = "OemSemicolon";
            if (key == Keys.Oem1) keyName = "Oem1";
            if (key == Keys.Oemplus) keyName = "Oemplus";
            if (key == Keys.Oemcomma) keyName = "Oemcomma";
            if (key == Keys.OemMinus) keyName = "OemMinus";
            if (key == Keys.OemPeriod) keyName = "OemPeriod";
            if (key == Keys.OemQuestion) keyName = "OemQuestion";
            if (key == Keys.Oem2) keyName = "Oem2";
            if (key == Keys.Oemtilde) keyName = "Oemtilde";
            if (key == Keys.Oem3) keyName = "Oem3";
            if (key == Keys.OemOpenBrackets) keyName = "OemOpenBrackets";
            if (key == Keys.Oem4) keyName = "Oem4";
            if (key == Keys.OemPipe) keyName = "OemPipe";
            if (key == Keys.Oem5) keyName = "Oem5";
            if (key == Keys.OemCloseBrackets) keyName = "OemCloseBrackets";
            if (key == Keys.Oem6) keyName = "Oem6";
            if (key == Keys.OemQuotes) keyName = "OemQuotes";
            if (key == Keys.Oem7) keyName = "Oem7";
            if (key == Keys.Oem8) keyName = "Oem8";
            if (key == Keys.OemBackslash) keyName = "OemBackslash";
            if (key == Keys.Oem102) keyName = "Oem102";
            if (key == Keys.ProcessKey) keyName = "ProcessKey";
            if (key == Keys.Packet) keyName = "Packet";
            if (key == Keys.Attn) keyName = "Attn";
            if (key == Keys.Crsel) keyName = "Crsel";
            if (key == Keys.Exsel) keyName = "Exsel";
            if (key == Keys.EraseEof) keyName = "EraseEof";
            if (key == Keys.Play) keyName = "Play";
            if (key == Keys.Zoom) keyName = "Zoom";
            if (key == Keys.NoName) keyName = "NoName";
            if (key == Keys.Pa1) keyName = "Pa1";
            if (key == Keys.OemClear) keyName = "OemClear";
            if (key == Keys.Shift) keyName = "Shift";
            if (key == Keys.Control) keyName = "Control";
            if (key == Keys.Alt) keyName = "Alt";

            return keyName;
        }

    }
}
