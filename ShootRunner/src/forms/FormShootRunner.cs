using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

#nullable disable


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
        private const int VK_APPS = 0x5D;
        private static IntPtr _hookID = IntPtr.Zero;
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static LowLevelKeyboardProc _proc = HookCallback;

        // FILE WATCH
        private FileSystemWatcher watcher = null;


        private const int WM_WTSSESSION_CHANGE = 0x02B1;
        private const int NOTIFY_FOR_THIS_SESSION = 0;

        [DllImport("Wtsapi32.dll")]
        private static extern bool WTSRegisterSessionNotification(IntPtr hWnd, [MarshalAs(UnmanagedType.U4)] int dwFlags);

        [DllImport("Wtsapi32.dll")]
        private static extern bool WTSUnRegisterSessionNotification(IntPtr hWnd);

        // CONSTRUCTOR
        public FormShootRunner()
        {
            InitializeComponent();

            Load += (s, e) => WTSRegisterSessionNotification(this.Handle, NOTIFY_FOR_THIS_SESSION);
            FormClosing += (s, e) => WTSUnRegisterSessionNotification(this.Handle);

            _hookID = SetHook(_proc);
            this.RegisterCommandFileWatch();
        }

        // LOAD
        private void FormShootRunner_Load(object sender, EventArgs e)
        {
            if (Program.isDebug())
            {
                this.notifyIconShootRunner.Icon = Pictures.CreateCustomIcon();
            }

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Hide();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_WTSSESSION_CHANGE)
            {
                int reason = m.WParam.ToInt32();
                switch (reason)
                {
                    case 0x7: // WTS_SESSION_LOCK
                        Program.pause = true;
                        Program.info("System locked");
                        break;
                    case 0x8: // WTS_SESSION_UNLOCK;
                        Program.pause = false;
                        Program.info("System unlocked");
                        break;
                }
            }

            base.WndProc(ref m);
        }

        // COMMAND
        private bool RunPinCommand(Command command)
        {
            if (command.action == "console") // OPEN CONSOLE
            {
                Program.ShowConsole();
                return true;
            }
            else if (command.action == "Close") // CLOSE APPLICATION
            {
                Program.Exit();
                return true;
            }
            else if (command.action == "HideAllForms") // HIDE ALL FORMS
            {
                Program.HideAllForms();
                return true;
            }
            else if (command.action == "ShowAllForms") // SHOW ALL FORMS
            {
                Program.ShowAllForms();
                return true;
            }
            else if (command.action == "ToggleAllForms") // TOGGLE ALL FORMS
            {
                Program.ToggleAllForms();
                return true;
            }
            else if (command.action == "CreatePin") // CREATE PIN
            {
                this.CreatPin();
                return true;
            }
            else if (command.action == "AddEmptyPin") // CREATE EMPTY PIN
            {
                Program.AddEmptyPin();
                return true;
            }
            else if (command.action == "cascade") // WINDOWS TO CASCADE
            {
                ToolsWindow.CascadeWindows();
                return true;
            }
            else if (command.action == "taskbar") // SHOW TASKBAR
            {
                Program.widgetManager.ShowTaskbarWidget(null);
                return true;
            }
            else
            if (command.action == "LockPc") // LOCK PC
            {
                SystemTools.LockPc();
                return true;
            }
            else if (command.action == "ShowDesktop") // SHOW DESKTOP
            {
                ToolsWindow.ShowDesktop();
                return true;
            }
            else if (command.keypress != null && command.keypress != "") // SIMULATE KEYPRESS
            {
                if (command.currentwindow != null) // SEND KEYPRESS TO CURENT WINDOW IF TITLE CONTAIN STRING
                {
                    IntPtr Handle = ToolsWindow.GetCurrentWindow();
                    string title = ToolsWindow.GetWindowTitle(Handle);
                    if (title.Contains(command.currentwindow))
                    {
                        // Keyboard.SandKeyPressToWindow(command.keypress, window.Handle);
                        Keyboard.KeyPress2(command.keypress);
                        return true;
                    }
                }
                else if (command.process != null) // SEND KEYPRESS TO ALL RUNNING PROCESSES WITH PATH
                {
                    Keyboard.SandKeyPressToProcess(command.keypress, command.process);
                    return true;
                }
                else // JUST KEYPRESS
                {
                    Keyboard.KeyPress2(command.keypress);
                    return true;
                }

            }
            else if (command.window != null && command.window != "") // BRING WINDOW TO FRONT
            {
                bool found = ToolsWindow.BringToFront(command.window);
                if (found)
                {
                    return true;
                }

                if (command.open != null && command.open != "") // OPEN APP IF NOT WINDOW FOUND
                {
                    JobTask.OpenFileInSystem(command.open);
                    return true;
                }
                else if (command.command != null && command.command != "") // RUN COMMAND IF NOT WINDOW FOUND
                {
                    JobTask.RunCommand(command.command, command.parameters, command.workdir);
                    return true;
                }

                return false;
            }
            else if (command.open != null && command.open != "") // OPEN URL OR DOCUMENT
            {
                if (command.currentwindow != null) // OPEN FILE OR URL  ONLY IF CURRENT WINDOW HAS TITLE
                {
                    IntPtr Handle = ToolsWindow.GetCurrentWindow();
                    string title = ToolsWindow.GetWindowTitle(Handle);
                    if (title.Contains(command.currentwindow))
                    {
                        JobTask.OpenFileInSystem(command.open);
                        return true;
                    }
                }
                else // JUST OPEN FILE
                {
                    JobTask.OpenFileInSystem(command.open);
                    return true;
                }
            }
            else if (command.command != null && command.command != "") // RUN COMMAND AS PROCESS WITH PARAMETERS
            {
                if (command.currentwindow != null) // RUN COMMAND AS PROCESS WITH PARAMETERS IF CURRENT WINDOW HAS TITLE
                {
                    IntPtr Handle = ToolsWindow.GetCurrentWindow();
                    string title = ToolsWindow.GetWindowTitle(Handle);
                    if (title.Contains(command.currentwindow))
                    {
                        JobTask.RunCommand(command.command, command.parameters, command.workdir);
                        return true;
                    }
                }
                else // RUN COMMAND AS PROCESS WITH PARAMETERS
                {
                    JobTask.RunCommand(command.command, command.parameters, command.workdir);
                    return true;
                }

            }

            return false;
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
            if (Program.pause)
            {
                return CallNextHookEx(_hookID, nCode, wParam, lParam);
            }

            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
            {
                Program.tick = 0;

                int vkCode = Marshal.ReadInt32(lParam);
                Keys key = (Keys)vkCode;

                Keys ModifierKeys = Control.ModifierKeys;


                Program.shortcut.key = key.ToString();
                switch (Program.shortcut.key)
                {
                    case "LControlKey":
                        Program.shortcut.ctrl = true;
                        break;
                    case "RControlKey":
                        Program.shortcut.ctrl = true;
                        break;
                    case "LMenu":
                        Program.shortcut.alt = true;
                        break;
                    case "RMenu":
                        Program.shortcut.alt = true;
                        break;
                    case "LShiftKey":
                        Program.shortcut.shift = true;
                        break;
                    case "RShiftKey":
                        Program.shortcut.shift = true;
                        break;
                    case "LWin":
                        Program.shortcut.win = true;
                        break;
                    case "RWin":
                        Program.shortcut.win = true;
                        break;
                    case "Apps":
                        Program.shortcut.apps = true;
                        break;
                }
                /*
                Program.debug("KeyDOWN" +
                   " CTRL=" + (Program.shortcut.ctrl ? "1" : "0") +
                   " ALT=" + (Program.shortcut.alt ? "1" : "0") +
                   " SHIFT=" + (Program.shortcut.shift ? "1" : "0") +
                   " LWIN=" + (Program.shortcut.lwin ? "1" : "0") +
                   " RWIN=" + (Program.shortcut.rwin ? "1" : "0") +
                   " win=" + (Program.shortcut.win ? "1" : "0") +
                   " APPS=" + (Program.shortcut.apps ? "1" : "0") +
                   " key=" + Program.shortcut.key
                );*/

                if (Program.shortcutForm != null)
                {
                    Program.ShowShortcutInShortcutForm(Program.shortcut);
                }

                if (Program.formShootRunner.RunScript(Program.shortcut))
                {
                    return (IntPtr)1;
                }

                return CallNextHookEx(_hookID, nCode, wParam, lParam);
            }

            if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP)
            {
                Program.tick = 0;

                int vkCode = Marshal.ReadInt32(lParam);
                Keys key = (Keys)vkCode;

                Program.shortcut.key = key.ToString();
                switch (Program.shortcut.key)
                {
                    case "LControlKey":
                        Program.shortcut.ctrl = false;
                        break;
                    case "RControlKey":
                        Program.shortcut.ctrl = false;
                        break;
                    case "LMenu":
                        Program.shortcut.alt = false;
                        break;
                    case "RMenu":
                        Program.shortcut.alt = false;
                        break;
                    case "LShiftKey":
                        Program.shortcut.shift = false;
                        break;
                    case "RShiftKey":
                        Program.shortcut.shift = false;
                        break;
                    case "LWin":
                        Program.shortcut.win = false;
                        break;
                    case "RWin":
                        Program.shortcut.win = false;
                        break;
                    case "Apps":
                        Program.shortcut.apps = false;
                        break;
                }
                /*
                Program.debug("KeyUP" +
                   " CTRL=" + (Program.shortcut.ctrl ? "1" : "0") +
                   " ALT=" + (Program.shortcut.alt ? "1" : "0") +
                   " SHIFT=" + (Program.shortcut.shift ? "1" : "0") +
                   " LWIN=" + (Program.shortcut.lwin ? "1" : "0") +
                   " RWIN=" + (Program.shortcut.rwin ? "1" : "0") +
                   " win=" + (Program.shortcut.win ? "1" : "0") +
                   " APPS=" + (Program.shortcut.apps ? "1" : "0") +
                   " key=" + Program.shortcut.key
                );*/

                // prevent Menu key to show context menu
                if (Program.shortcut.key == "Apps")
                {
                    return (IntPtr)1;
                }
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

        // SHORCUT APPS
        private static bool IsAppsPressed()
        {
            return (GetKeyState(VK_APPS) & 0x8000) != 0;
        }

        // COMMAND
        private bool TestRunScript(Shortcut shortcut)
        {

            foreach (var command in Program.commands)
            {
                if (this.ParseShortcut(command.shortcut, shortcut))
                {
                    return true;
                }
            }

            return false;
        }

        // COMMAND
        private bool RunScript(Shortcut shortcut)
        {
            bool foundOne = false;
            foreach (var command in Program.commands)
            {
                if (command.enabled && this.ParseShortcut(command.shortcut, shortcut))
                {
                    bool executed = this.RunPinCommand(command);
                    if (executed)
                    {
                        foundOne = true;
                    }
                }
            }

            return foundOne;
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
            bool ctrl = false;
            bool alt = false;
            bool shift = false;
            bool apps = false;
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
                    case "APPS":
                        apps = true;
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
                ((shortcut.apps && apps) || (!shortcut.apps && !apps)) &&
                shortcut.key.ToUpper() == key)
            {
                return true;
            }

            return false;
        }

        // PIN
        private void CreatPin()
        {
            IntPtr Handle = ToolsWindow.GetCurrentWindow();

            if (Handle != IntPtr.Zero)
            {
                Window window = new Window();
                window.Handle = Handle;
                ToolsWindow.SetWindowData(window);
                FormPin pin = new FormPin(window);
                pin.Show();
                pin.Center();
                Program.pins.Add(pin);
            }

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
        private void RegisterCommandFileWatch()
        {
            if (watcher != null)
            {
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

                Program.debug("FILE WATCH:" + ex.Message);
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
            Program.tick = Program.tick + 1;
            if (Program.tick > 10)
            {
                Program.shortcut.ctrl = false;
                Program.shortcut.alt = false;
                Program.shortcut.shift = false;
                Program.shortcut.lwin = false;
                Program.shortcut.rwin = false;
                Program.shortcut.win = false;
                Program.shortcut.apps = false;
                Program.shortcut.key = "";
                Program.tick = 0;
            }
        }

        // POPUP OPEN
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            Program.autorun = this.IsAutoRunSet(Program.AppName, Application.ExecutablePath);
            autorunToolStripMenuItem.Checked = Program.autorun;
        }

        //POPUP COMMANDS
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

        // POPUP ERRORLOG
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

        //POPUP OPTIONS
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //POPUP AUTORUN
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

            Program.Update();
        }

        // POPUP EXIT
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Exit();
        }

        // POPUP SHORTCUTFORM
        private void shortcutFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.ShowShortcutForm();

        }

        // EVENT CLOSED
        private void FormShootRunner_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void FormShootRunner_FormClosing(object sender, FormClosingEventArgs e)
        {


        }

        private void newPinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.AddEmptyPin();
        }

        private void newWidgetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.widgetManager.AddEmptyWidget();
        }

        private void taskbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.widgetManager.ShowTaskbarWidget(null);
        }

        private void consoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.ShowConsole();
        }

        private void hideAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.HideAllForms();
            showAllToolStripMenuItem.Visible = true;
            hideAllToolStripMenuItem.Visible = false;
        }

        private void showAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.ShowAllForms();
            showAllToolStripMenuItem.Visible = false;
            hideAllToolStripMenuItem.Visible = true;
        }

        private void createWidgetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.widgetManager.ShowCreateWidgetForm();
        }
    }
}
