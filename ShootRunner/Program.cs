using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Win32;
using ShootRunner.src.forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


#nullable disable


namespace ShootRunner
{
    internal static class Program
    {
        // APPLICATION
        public static string AppName = "ShootRunner";
        private static int processId;
        private static Mutex mutex = null;
        public static bool pause = false;

        // PATH
        public static string roamingAppDataPath = "";
        public static string configPath = "";
        public static string errorLogPath = "";
        public static string configFielPath = "";
        public static string widgetsPath = "";
        public static string webview2Path = "";
        public static string commandFielPath = "";
        public static string commandFielName = "";
        public static string powershell = null;

        // CONFIG
        public static Config config = null;
        public static ConfigFile configFile = null;
        public static bool autorun = false;
        public static bool autosave = true;

        // COMMANDS
        public static DateTime commandFielPathLastChange;
        public static Shortcut shortcut = new Shortcut();
        public static int tick = 0;        
        public static List<Command> commands = new List<Command>();
        
        // FORMS
        public static FormShootRunner formShootRunner = null;
        public static List<FormPin> pins = new List<FormPin>();
        public static List<FormWindowInfo> windowInfoForms = new List<FormWindowInfo>();        
        public static FormConsole console = null;
        public static FormShortcut shortcutForm = null;
        public static List<Form> selectedForms = new List<Form>();

        // WIDGETS
        public static WidgetManager widgetManager = new WidgetManager();

        // LOG
        public static string text = "";
        
        // AUTOSAVE
        public static bool updated = false;
        public static DateTime updatedTime = new DateTime();
        public static System.Timers.Timer timer;

#if DEBUG
        [DllImport("kernel32.dll")]
        static extern bool AllocConsole();
#endif

        // DEBUG
        public static bool isDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }

        // DEBUG
        public static string GetDebugPrefix()
        {
#if DEBUG
            return "DEBUG.";
#else
            return "";
#endif            
        }

        // PATH
        public static void SetPath()
        {
            Program.roamingAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Program.configPath = Path.Combine(Program.roamingAppDataPath, Program.AppName);
            if (!Directory.Exists(configPath))
            {
                Directory.CreateDirectory(configPath);
            }

            Program.commandFielName = GetDebugPrefix() + "commands.xml";
            Program.configFielPath = Path.Combine(Program.configPath, GetDebugPrefix() + "config.xml");
            Program.commandFielPath = Path.Combine(Program.configPath, Program.commandFielName);
            Program.errorLogPath = Path.Combine(Program.configPath, GetDebugPrefix() + "error.log");
            Program.widgetsPath = Path.Combine(Program.configPath, GetDebugPrefix() + "widgets");
            Program.webview2Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Program.AppName, "WebView2UserData");
        }

        // PATH
        public static void FindPowershell()
        {
            powershell = ScriptsTools.IsCommandAvailable("pwsh.exe");

            if (powershell == null)
            {
                powershell = ScriptsTools.IsCommandAvailable("powershell.exe");
            }
        }

        // AUTOSAVE
        public static void Update() {
            Program.updated = true;
            Program.updatedTime = DateTime.Now;
        }

        // AUTOSAVE
        public static void StartTimer()
        {
            timer = new System.Timers.Timer(1000*60);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        // AUTOSAVE
        public static void OnTimedEvent(object sender, EventArgs e)
        {
            if (Program.autosave && Program.updated && (DateTime.Now - Program.updatedTime).TotalMinutes >= 2)
            {
                Program.updatedTime = DateTime.Now;
                Program.updated = false;
                Program.configFile.Save();
            }
        }

        // LOG
        public static void ClearBigLog()
        {

            FileInfo fileInfo = new FileInfo(errorLogPath);
            long maxFileSize = 10 * 1024 * 1024;

            if (fileInfo.Exists && fileInfo.Length > maxFileSize)
            {
                fileInfo.Delete(); // CLEAR 10MB LOG
            }
        }

        // LOG
        public static void debug(string message) {
#if DEBUG
            string unixTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            using (StreamWriter sw = new StreamWriter(errorLogPath, true))
            {
                string line = unixTime + " DEBUG: " + message;
                sw.WriteLine(line); // file
                Program.write(line); // console form
                Console.WriteLine(line); // standard output
            }
#endif
        }

        // LOG
        public static void info(string message)
        {

            string unixTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            using (StreamWriter sw = new StreamWriter(errorLogPath, true))
            {
                string line = unixTime + " INFO: " + message;
                sw.WriteLine(line);
                Program.write(line);
#if DEBUG
                Console.WriteLine(line);
#endif
            }
        }

        // LOG
        public static void error(string message)
        {
            string unixTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            using (StreamWriter sw = new StreamWriter(Program.errorLogPath, true))
            {
                string line = unixTime + " ERROR: " + message;
                sw.WriteLine(line);
                Program.write(line);
#if DEBUG
                Console.WriteLine(line);
#endif
            }
        }

        // LOG
        public static void write(string message)
        { 
            text += message+"\r\n";

            if (Program.console != null) { 
                Program.console.write(message);
            }
        }

        // LOG
        public static void message(string message)
        {
#if DEBUG
            Console.WriteLine(message);
#endif
        }

        // CONSOLE
        public static void ShowConsole()
        {
            if (console == null) {
                console = new FormConsole(Program.text);
            }

            console.Show();
            console.BringToFront();
        }

        // CONSOLE
        public static void CloseConsole()
        {
            Program.console = null;
        }

        // COMMANDS
        public static void loadCommands()
        {
            if (!File.Exists(Program.commandFielPath))
            {
                try
                {
                    string filePath = Program.commandFielPath;
                    string content = CommandsFileContent.GetContent();

                    string directoryPath = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    File.WriteAllText(filePath, content);
                }
                catch (Exception ex)
                {

                    Program.error(ex.Message);
                }
            }

            if (!File.Exists(Program.commandFielPath))
            {
                return;
            }

            try
            {
                int attemps = 10;
                bool error = false;
                do
                {
                    error = false;
                    try
                    {
                        Program.commands.Clear();
                        Program.commandFielPathLastChange = File.GetLastWriteTime(Program.commandFielPath);


                        string xml = File.ReadAllText(Program.commandFielPath);

                        try
                        {
                            XmlReaderSettings xws = new XmlReaderSettings
                            {
                                CheckCharacters = false
                            };

                            using (XmlReader xr = XmlReader.Create(new StringReader(xml), xws))
                            {

                                XElement root = XElement.Load(xr);

                                foreach (XElement item in root.Elements())
                                {
                                    string name = item.Name.ToString();
                                    if (item.Name.ToString() == "commands")
                                    {

                                        foreach (var commandElement in item.Elements())
                                        {
                                            if (commandElement.Name.ToString() == "command")
                                            {
                                                Command newCommand = new Command();
                                                Program.commands.Add(newCommand);

                                                newCommand.enabled = commandElement.Element("enabled")?.Value == "1" ? true : false;
                                                newCommand.shortcut = commandElement.Element("shortcut")?.Value;
                                                newCommand.open = commandElement.Element("open")?.Value;
                                                newCommand.command = commandElement.Element("command")?.Value;
                                                newCommand.parameters = commandElement.Element("parameters")?.Value;
                                                newCommand.window = commandElement.Element("window")?.Value;
                                                newCommand.currentwindow = commandElement.Element("currentwindow")?.Value;
                                                newCommand.workdir = commandElement.Element("workdir")?.Value;
                                                newCommand.keypress = commandElement.Element("keypress")?.Value;
                                                newCommand.action = commandElement.Element("action")?.Value;
                                                newCommand.process = commandElement.Element("process")?.Value;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Program.error(ex.Message);
                        }
 
                    }
                    catch (Exception ex)
                    {
                        error = true;
                        Thread.Sleep(50);
                        Program.error(ex.Message);
                    }

                    attemps--;
                } while (error && attemps > 0);
            }
            catch (Exception ex)
            {
                Program.error(ex.Message);
            }

        }
        
        // SHORTCUT FORM
        public static void ShowShortcutForm() {
            if (shortcutForm == null)
            {
                Program.shortcutForm = new FormShortcut();
            }
            Program.shortcutForm.Show();
        }

        // SHORTCUT FORM
        public static void ShowShortcutInShortcutForm(Shortcut shortcut)
        {
            if (shortcutForm == null)
            {
                return;
            }

            Program.shortcutForm.label1.Text = 
                (shortcut.win ? "WIN+" : "") +
                (shortcut.ctrl ? "CTRL+" : "") +
                (shortcut.alt ? "ALT+" : "") +
                (shortcut.shift ? "SHIFT+" : "") +
                (shortcut.apps ? "APPS+" : "") +
                (shortcut.key);
        }

        // SHORTCUT FORM
        public static void CloseShortcutForm()
        {
            if (shortcutForm != null)
            {
                Program.shortcutForm = null;
            }            
        }

        // PIN FORM
        public static void CreatePin(Window window = null)
        {
            FormPin pin = new FormPin(window);
            pins.Add(pin);
            pin.Show();
            pin.Center();

            Program.Update();
        }

        // PIN FORM
        public static void AddEmptyPin() {

            Window window = new Window();
            window.Type = "COMMAND";
            window.doubleClickCommand = true;

            CreatePin(window);

            Program.Update();
        }

        // PIN FORM
        public static void OpenPins()
        {
            
            foreach (var pin in Program.pins)
            {
                pin.Show();
            }
        }

        // PIPE SERVER
        public static void OnMessageReceived(string message)
        {
           Program.info("Message Received: " + message);
        }

        // PIPE SERVER
        public static void InitPipeServer() {
            PipeServer.MessageReceived += OnMessageReceived;
            PipeServer.SetPipeName(GetDebugPrefix() + Program.AppName);
            if (!PipeServer.StartServerAsync()) {
                Program.info("Pipe server exists");
            }
        }

        // FORMS 
        public static void HideAllForms()
        {
            if (formShootRunner != null) formShootRunner.Hide();
            if (console != null) console.Hide();
            if (shortcutForm != null) shortcutForm.Hide();

            foreach (var pin in pins) { 
                pin.Hide();
            }


            widgetManager.HideAllWidgets();

        }

        // FORMS
        public static void ShowAllForms()
        {
            if (formShootRunner != null) formShootRunner.Show();
            if (console != null) console.Show();
            if (shortcutForm != null) shortcutForm.Show();

            foreach (var pin in pins)
            {
                pin.Show();
            }

            widgetManager.ShowAllWidgets();

        }

        // FORMS
        public static void ToggleAllForms()
        {
            bool allHiden = false;

            if (formShootRunner.Visible) {
                allHiden = false;
            }

            if (console.Visible)
            {
                allHiden = false;
            }

            if (shortcutForm.Visible)
            {
                allHiden = false;
            }

            foreach (var pin in pins)
            {
                if (pin.Visible)
                {
                    allHiden = false;
                }
            }

            foreach (var widget in widgetManager.widgets)
            {
                if (widget.widgetForm.Visible)
                {
                    allHiden = false;
                }
            }

            if (allHiden)
            {
                HideAllForms();
            }
            else {
                ShowAllForms();
            }
        }

        // APPLICATION
        public static bool ChecKDuplicateRun()
        {
            bool createdNew;


            mutex = new Mutex(true, (isDebug() ? "DEBUG." : "") + Program.AppName, out createdNew);

            if (!createdNew)
            {
                return true;
            }
            return false;
        }

        // APPLICATION
        public static void Exit()
        {
            Program.configFile.Save();

            foreach (FormWindowInfo info in windowInfoForms)
            {
                info.Close();
            }

            Application.Exit();
        }

        public static void AddFormToSelected(Form form) {
            selectedForms.Add(form);
        }

        public static void RemoveFormFromSelected(Form form)
        {
            selectedForms.Remove(form);
        }

        public static void SelectAllPins()
        {
            foreach (var pin in pins) {
                if (selectedForms.Contains(pin)) { 
                    continue;
                }

                selectedForms.Add(pin);
                pin.SelectPin();
            }
            
        }

        public static void DeselectAllPins()
        {
            foreach (var form in selectedForms)
            {
                try
                {
                    FormPin pin = form as FormPin;
                    pin.UnselectPin();
                }
                catch (Exception)
                {

                
                }
                
            }

            selectedForms.Clear();
        }

        public static void SetDragStartToSelectedForms(Form activeForm) {
            foreach (Form form in selectedForms)
            {
                if (form == activeForm) {
                    continue;
                }

                try
                {
                    FormPin pin = form as FormPin;
                    pin.SetDragStartLocation();
                }
                catch
                {


                }
            }
        }

        public static void SetDragSelectedFormsPosition(Form activeForm)
        {
            foreach (Form form in selectedForms)
            {
                if (form == activeForm)
                {
                    continue;
                }

                try
                {
                    FormPin pin = form as FormPin;
                    pin.SetDragNewLocation();
                }
                catch
                {


                }
            }
        }

        public static void ClearSelectedForms()
        {
            foreach (Form form in selectedForms)
            {
                try
                {
                    FormPin pin = form as FormPin;
                    pin.selected = false;
                }
                catch 
                {

                    
                }
            }

            selectedForms.Clear();
        }

        public static void MoveSelected(Form activeForm, int X, int Y)
        {
            foreach (Form form in selectedForms) {
                if (form != activeForm) {
                    form.Left += X;
                    form.Top += Y;
                }
            }
        }

        // MAIN
        [STAThread]
        public static void Main()
        {

#if DEBUG
            AllocConsole();
#endif


            Program.message("Application Start");

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                Console.WriteLine($"Unhandled exception: {e.ExceptionObject}");

                var process = Process.GetCurrentProcess();
                Console.WriteLine($"Handles: {process.HandleCount}, Threads: {process.Threads.Count}, Memory: {process.PrivateMemorySize64}");

            };

            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                Console.WriteLine($"Unobserved task exception: {e.Exception}");
                e.SetObserved();

                var process = Process.GetCurrentProcess();
                Console.WriteLine($"Handles: {process.HandleCount}, Threads: {process.Threads.Count}, Memory: {process.PrivateMemorySize64}");
            };

            Program.processId = Process.GetCurrentProcess().Id;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Program.SetPath();

            try
            {

                ClearBigLog();

                if (ChecKDuplicateRun())
                {
                    Program.info("Duplicite run, app end");                
                    return;
                }


                Program.config = new Config();            
                Program.configFile = new ConfigFile(Program.config);
                Program.configFile.Load();

                Program.FindPowershell();

                Program.loadCommands();                
                Program.OpenPins();
                widgetManager.OpenWidgets();

                Program.StartTimer();

                formShootRunner = new FormShootRunner();
                Application.Run(formShootRunner);

            }
            catch (Exception ex)
            {
                Program.error(ex.Message);
            }

            Program.message("Application End");
        }

        
    }
}
