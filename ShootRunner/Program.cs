using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Win32;


namespace ShootRunner
{
    internal static class Program
    {
        public static string AppName = "ShootRunner";

        public static List<Command> commands = new List<Command>();

        public static FormShootRunner formShootRunner = null;

        public static string roamingAppDataPath = "";
        public static string configPath = "";
        public static string errorLogPath = "";
        public static string configFielPath = "";
        public static string widgetsPath = "";
        public static string commandFielPath = "";
        public static string commandFielName = "commands.xml";
        public static DateTime commandFielPathLastChange;

        public static bool autorun = false;
        public static bool autosave = true;

        public static Shortcut shortcut = new Shortcut();
        public static int tick = 0;

        public static bool pause = false;

        public static List<FormPin> pins = new List<FormPin>();
        

        public static string powershell = null;

        public static bool updated = false;
        public static DateTime updatedTime = new DateTime();


        public static WidgetManager widgetManager = new WidgetManager();

        public static void Update() {
            Program.updated = true;
            Program.updatedTime = DateTime.Now;
            Program.debug("Update");
        }

        public static System.Timers.Timer timer;

        public static void StartTimer()
        {
            timer = new System.Timers.Timer(1000*60);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public static void OnTimedEvent(object sender, EventArgs e)
        {
            if (Program.autosave && Program.updated && (DateTime.Now - Program.updatedTime).TotalMinutes >= 2)
            {
                Program.updatedTime = DateTime.Now;
                Program.updated = false;
                Program.configFile.Save();
            }
        }

        public static void FindPowershell() {
            powershell = ScriptsTools.IsCommandAvailable("pwsh.exe");

            if (powershell == null) {
                powershell = ScriptsTools.IsCommandAvailable("powershell.exe");
            }
        }

        public static void ClearBigLog()
        {

            FileInfo fileInfo = new FileInfo(errorLogPath);
            long maxFileSize = 10 * 1024 * 1024;

            if (fileInfo.Exists && fileInfo.Length > maxFileSize)
            {
                fileInfo.Delete(); // CLEAR 10MB LOG
            }
        }

        public static bool isDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }


        public static void debug(string message) {
#if DEBUG
            string unixTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            using (StreamWriter sw = new StreamWriter(errorLogPath, true))
            {
                sw.WriteLine(unixTime + " " + message);
                Console.WriteLine(unixTime + " " + message);
            }
#endif
        }

        public static void error(string message)
        {
            string unixTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            using (StreamWriter sw = new StreamWriter(Program.errorLogPath, true))
            {
                sw.WriteLine(unixTime + " " + message);
                Console.WriteLine(unixTime + " " + message);
            }
        }

        public static void loadCommands()
        {
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


                        string xml = File.ReadAllText(Program.configFielPath);

                        /*if (Program.isDebug())
                        {
                            xml =  @"<root>
                            <commands>
                            <command>
                            <shortcut>F7</shortcut>
                            <enabled>1</enabled>
                            <action>CreatePin</action>
                            <parameters></parameters>y
                            </command>
                            <command>
                            <shortcut>F8</shortcut>
                            <enabled>1</enabled>
                            <action>Close</action>
                            <parameters></parameters>
                            </command>
                            </commands>
                            </root>";
                        }*/

                        try
                        {
                            XmlReaderSettings xws = new XmlReaderSettings
                            {
                                CheckCharacters = false
                            };

                            using (XmlReader xr = XmlReader.Create(new StringReader(xml), xws))
                            {

                                XElement root = XElement.Load(xr);
                                var commands = root.Descendants("command");
                                foreach (var commandElement in commands)
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
        
        private static Mutex mutex = null;

        public static bool ChecKDuplicateRun(){
            bool createdNew;

            
            mutex = new Mutex(true, (isDebug() ? "DEBUG.":"")+Program.AppName, out createdNew);

            if (!createdNew)
            {                
                return true;
            }
            return false;
        }


        // EVENT LOCKPC
        public static void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                Program.pause = true;
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                Program.pause = false;
            }
        }

        public static FormShortcut shortcutForm = null;

        public static void ShowShortcutForm() {
            if (shortcutForm == null)
            {
                Program.shortcutForm = new FormShortcut();
            }
            Program.shortcutForm.Show();
        }

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

        public static void CloseShortcutForm()
        {
            if (shortcutForm != null)
            {
                Program.shortcutForm = null;
            }            
        }

        public static Config config = null;
        public static ConfigFile configFile = null;

        public static void AddEmptyPin() { 

            Window window = new Window();
            window.Type = "COMMAND";
            window.doubleClickCommand = true;
            FormPin pin = new FormPin(window);
            pins.Add(pin);
            pin.Show();
            pin.Center();

            Program.Update();
        }

        public static void OpenPins()
        {
            
            foreach (var pin in Program.pins)
            {
                pin.Show();
            }
        }

        

        [STAThread]
        static void Main()
        {
            if (ChecKDuplicateRun()){
                MessageBox.Show("Another instance of the application is already running.", "Application Already Running", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            Program.roamingAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Program.configPath = Path.Combine(Program.roamingAppDataPath, Program.AppName);
            if (!Directory.Exists(configPath))
            {
                Directory.CreateDirectory(configPath);
            }
            Program.configFielPath = Path.Combine(Program.configPath, (isDebug() ? "DEBUG." : "") + "config.xml");
            Program.commandFielPath = Path.Combine(Program.configPath, (isDebug() ? "DEBUG." : "") + Program.commandFielName);
            Program.errorLogPath = Path.Combine(Program.configPath, (isDebug() ? "DEBUG." : "") + "error.log");

            Program.widgetsPath = Path.Combine(Program.configPath, (isDebug() ? "DEBUG." : "") + "widgets");

            ClearBigLog();

            Program.debug("Start");

            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
            SystemEvents.SessionSwitch -= new SessionSwitchEventHandler(SystemEvents_SessionSwitch);

            Program.config = new Config();            
            Program.configFile = new ConfigFile(Program.config);
            Program.configFile.Load();

            Program.loadCommands();
            Program.FindPowershell();
            Program.OpenPins();
            widgetManager.OpenWidgets();
            Program.StartTimer();

            formShootRunner = new FormShootRunner();
            Application.Run(formShootRunner);

            Program.configFile.Save();

            Program.debug("End");
        }
    }
}
