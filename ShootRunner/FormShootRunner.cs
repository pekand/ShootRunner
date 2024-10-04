﻿using System;
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
        // HOOK
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        // HOOK
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        // HOOK
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        // HOOK
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        // HOOK
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        // HOOK
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_KEYUP = 0x0101;
        private const int VK_LWIN = 0x5B;
        private const int VK_RWIN = 0x5C;
        private static IntPtr _hookID = IntPtr.Zero;
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static LowLevelKeyboardProc _proc = HookCallback;

        // FILE WATCH
        private FileSystemWatcher watcher = null;

        // CONSTRUCTOR
        public FormShootRunner()
        {
            InitializeComponent();
            _hookID = SetHook(_proc);
            this.RegisterCommandFileWatch();
        }

        // LOAD
        private void FormShootRunner_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Hide();
        }

        // HOOK
        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }
        
        // HOOK
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

        // HOOK
        private void FormShootRunner_InputLanguageChanging(object sender, InputLanguageChangingEventArgs e)
        {
            UnhookWindowsHookEx(_hookID);
        }

        // SHORCUT LWIN
        private static bool IsLWinPressed()
        {
            return (GetKeyState(VK_RWIN) & 0x8000) != 0;
        }

        // SHORCUT RWIN
        private static bool IsRWinPressed()
        {            
            return (GetKeyState(VK_LWIN) & 0x8000) != 0;
        }
       
        // COMMAND
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

        // SHORTCUT
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

        // COMMAND
        private bool RunCommand(Command command)
        {
            if (command.action == "LockPc") {
                SystemTools.LockPc();
                return true;
            } else if (command.action == "ShowDesktop")
            {
                SystemTools.ShowDesktop();
                return true;
            }
            else if (command.keypress != null && command.keypress != "")
            {
                Keyboard.KeyPress2(command.keypress);

            } else if (command.window != null && command.window != "")
            {
                bool found = ToolsWindow.BringToFront(command.window);
                if (!found) {
                    if (command.open != null && command.open != "")
                    {
                        Task.OpenFileInSystem(command.open);
                        return true;
                    }
                    else if (command.command != null && command.command != "")
                    {
                        Task.RunCommand(command.command, command.parameters, command.workdir);
                        return true;
                    }
                }

                return found;
            } else if (command.open != null && command.open != "")
            {
                Task.OpenFileInSystem(command.open);
                return true;
            } else if (command.command != null && command.command != "") {
                Task.RunCommand(command.command, command.parameters, command.workdir);
                return true;
            } 

            return false;
        }

        //AUTORUN
        public bool IsAutoRunSet(string appName, string appPath)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);

            if (registryKey == null)
            {
                return false;
            }

            string path = "\"" + appPath + "\"";

            string value = registryKey.GetValue(appName)?.ToString();
            bool isSet = value == path;
            
            registryKey.Close();

            return isSet;
        }

        //AUTORUN
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

        //AUTORUN
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

        // FILE WATCH
        private void RegisterCommandFileWatch() {
            if (watcher != null) {
                return;
            }
                        

            try
            {

                watcher = new FileSystemWatcher();
                watcher.Path = Program.configPath;
                watcher.Filter = Program.commandFielName;
                watcher.NotifyFilter = NotifyFilters.LastWrite;
                watcher.Changed += OnChanged;
                watcher.Created += OnChanged;
                watcher.Deleted += OnChanged;
                watcher.EnableRaisingEvents = true;
            }
            catch (Exception ex)
            {

                Program.log("FILE WATCH:" + ex.Message);
            }
            

        }

        // FILE WATCH
        private static void OnChanged(object source, FileSystemEventArgs e)
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

        // TIMER
        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        //POPUP
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        //POPUP
        private void autorunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.IsAutoRunSet(Program.AppName, Application.ExecutablePath))
            {
                Program.autorun = false;
                this.RemoveAutoRun(Program.AppName);
            }
            else
            {
                Program.autorun = true;
                this.SetAutoRun(Program.AppName, Application.ExecutablePath);
            }

            autorunToolStripMenuItem.Checked = Program.autorun;
        }

        //POPUP
        private void commandsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo(Program.commandFielPath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        // POPUP
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            Program.autorun = this.IsAutoRunSet(Program.AppName, Application.ExecutablePath);            
            autorunToolStripMenuItem.Checked = Program.autorun;
        }

        // POPUP
        private void errorLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo(Program.errorLogPath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
