using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace ShootRunner
{
    public partial class FormShootRunner : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private const int WH_KEYBOARD_LL = 13;

        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;
        
        private const int WM_KEYUP = 0x0101;

        private const int VK_LWIN = 0x5B;
        private const int VK_RWIN = 0x5C;


        private static IntPtr _hookID = IntPtr.Zero;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static LowLevelKeyboardProc _proc = HookCallback;

        public FormShootRunner()
        {
            InitializeComponent();
            _hookID = SetHook(_proc);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormShootRunner_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Hide();
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }
        private static bool IsLWinPressed()
        {
            // GetKeyState returns a short where the most significant bit indicates if the key is down
            return (GetKeyState(VK_RWIN) & 0x8000) != 0;
        }

        private static bool IsRWinPressed()
        {
            // GetKeyState returns a short where the most significant bit indicates if the key is down
            return (GetKeyState(VK_LWIN) & 0x8000) != 0;
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Keys key = (Keys)vkCode;

                Keys ModifierKeys = Control.ModifierKeys;

                Shortcut shortcut = new Shortcut();
                shortcut.ctrl = (ModifierKeys & Keys.Control) == Keys.Control;
                shortcut.alt = (ModifierKeys & Keys.Alt) == Keys.Alt;
                shortcut.shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
                shortcut.apps = (ModifierKeys & Keys.Apps) == Keys.Apps;
                shortcut.lwin = IsLWinPressed();
                shortcut.rwin = IsRWinPressed();
                shortcut.win = shortcut.lwin || shortcut.rwin;
                shortcut.key = key.ToString();

                Program.log("KeyDOWN"+
                    " CTRL=" + (shortcut.ctrl ? "1" : "0") +
                    " ALT=" + (shortcut.alt ? "1" : "0") +
                    " SHIFT=" + (shortcut.shift ? "1" : "0") +
                    " LWIN=" +(shortcut.lwin ? "1":"0") + 
                    " RWIN=" + (shortcut.rwin ? "1" : "0") +
                    " win=" + (shortcut.win ? "1" : "0") +
                    " APPS=" + (shortcut.apps ? "1" : "0") +
                    " key=" + shortcut.key
                 );

                if (Program.formShootRunner.RunScript(shortcut)) {
                    return (IntPtr)1;
                }
                
            }

            if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Keys key = (Keys)vkCode;

                Shortcut shortcut = new Shortcut();
                shortcut.ctrl = (ModifierKeys & Keys.Control) == Keys.Control;
                shortcut.alt = (ModifierKeys & Keys.Alt) == Keys.Alt;
                shortcut.shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
                shortcut.apps = (ModifierKeys & Keys.Apps) == Keys.Apps;
                shortcut.lwin = IsLWinPressed();
                shortcut.rwin = IsRWinPressed();
                shortcut.win = shortcut.lwin || shortcut.rwin;
                shortcut.key = key.ToString();

                Program.log("KeyDOWN" +
                    " CTRL=" + (shortcut.ctrl ? "1" : "0") +
                    " ALT=" + (shortcut.alt ? "1" : "0") +
                    " SHIFT=" + (shortcut.shift ? "1" : "0") +
                    " LWIN=" + (shortcut.lwin ? "1" : "0") +
                    " RWIN=" + (shortcut.rwin ? "1" : "0") +
                    " win=" + (shortcut.win ? "1" : "0") +
                    " APPS=" + (shortcut.apps ? "1" : "0") +
                    " key=" + shortcut.key
                 );
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private void FormShootRunner_InputLanguageChanging(object sender, InputLanguageChangingEventArgs e)
        {
            UnhookWindowsHookEx(_hookID);
        }

        private string KeyToName(Keys key) {
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

        private bool  RunScript(Shortcut shortcut)
        {

            foreach (var command in Program.commands)
            {
                if (this.ParseShortcut(command.shortcut, shortcut))
                {
                    this.RunCommand(command);
                    return true;
                }
            }

            return false;
        }

        private bool ParseShortcut(string commandShortcut, Shortcut shortcut)
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

            if (((shortcut.ctrl && ctrl)||(!shortcut.ctrl && !ctrl)) &&
                ((shortcut.alt && alt) || (!shortcut.alt && !alt)) &&
                ((shortcut.shift && shift) || (!shortcut.shift && !shift)) &&
                ((shortcut.win && win) || (!shortcut.win && !win)) &&
                shortcut.key.ToUpper() == key)
            {
                return true;
            }

            return false;
        }

        

        private bool RunCommand(Command command)
        {
            if (command.open != null && command.open != "")
            {
                try
                {
                    Process.Start(new ProcessStartInfo(command.open) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }

                return true;
            } else if (command.command != null && command.command != "") {
                Process process = new Process();
                process.StartInfo.FileName = command.command;
                process.StartInfo.Arguments = command.parameters;
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.CreateNoWindow = true;

                try
                {
                    process.Start();
                    //process.WaitForExit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }

                return true;
            } else if (command.window != null && command.window != "")
            {
                return ToolsWindow.BringToFront(command.window);
            }

            return false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (File.Exists(Program.commandFielPath))
            {
                DateTime currentModificationTime = File.GetLastWriteTime(Program.commandFielPath);
                if (Program.commandFielPathLastChange != currentModificationTime)
                {
                    Program.loadCommands();
                }
            }
        }



        public bool IsAutoRunSet(string appName, string appPath)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);

            if (registryKey == null)
            {
                return false;
            }

            string path = "\"" + appPath + "\"";

            bool isSet = registryKey.GetValue(appName) == path;
            
            registryKey.Close();

            return isSet;
        }

        public void SetAutoRun(string appName, string appPath)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);

            if (registryKey == null)
            {
                return;
            }

            registryKey.SetValue(appName, "\"" + appPath + "\"");
            registryKey.Close();
        }

        public void RemoveAutoRun(string appName)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);

            if (registryKey == null)
            {
                return;
            }

            if (registryKey.GetValue(appName) != null)
            {
                registryKey.DeleteValue(appName);
            }

            registryKey.Close();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.IsAutoRunSet(Program.AppName, Application.ExecutablePath)) {
                Program.autorun = true;
            }
            autorunToolStripMenuItem.Checked = Program.autorun;
        }

        private void autorunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.autorun = !Program.autorun;
            if (Program.autorun)
            {
                this.SetAutoRun(Program.AppName, Application.ExecutablePath);
            }
            else
            {
                this.RemoveAutoRun(Program.AppName);

            }
        }
    }
}
