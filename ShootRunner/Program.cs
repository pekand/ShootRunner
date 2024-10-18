using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        public static string commandFielPath = "";
        public static string commandFielName = "commands.xml";
        public static DateTime commandFielPathLastChange;

        public static bool autorun = false;

        public static Shortcut shortcut = new Shortcut();
        public static int tick = 0;

        public static bool pause = false;

        public static List<FormPin> pins = new List<FormPin>();

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
                        XDocument xdoc = XDocument.Load(Program.commandFielPath);
                        var commands = xdoc.Descendants("command");
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

        public static void LoadConfig()
        {
            if (File.Exists(configFielPath))
            {
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(configFielPath);
                    XmlNodeList nodes = xmlDoc.DocumentElement.SelectNodes("/root/autorun");
                    foreach (XmlNode node in nodes)
                    {
                        Program.autorun = node.InnerText == "1";
                    }
                } catch(Exception ex) {
                    Program.error(ex.Message);
                }
            }
        }

        public static void SaveConfig()
        {
            if (!Directory.Exists(configPath))
            {
                Directory.CreateDirectory(configPath);
            }

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlElement root = xmlDoc.CreateElement("root");
                xmlDoc.AppendChild(root);
                XmlElement element = xmlDoc.CreateElement("autorun");
                element.InnerText = Program.autorun ? "1" : "0";
                root.AppendChild(element);
                xmlDoc.Save(configFielPath);
            }
            catch (Exception ex)
            {
                Program.error(ex.Message);
            }
        }



        [STAThread]
        static void Main()
        {
            if (ChecKDuplicateRun()){
                MessageBox.Show("Another instance of the application is already running.", "Application Already Running", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            Program.roamingAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Program.configPath = Path.Combine(Program.roamingAppDataPath, Program.AppName);
            if (!Directory.Exists(configPath))
            {
                Directory.CreateDirectory(configPath);
            }
            Program.configFielPath = Path.Combine(Program.configPath, (isDebug() ? "DEBUG." : "") + "config.xml");
            Program.commandFielPath = Path.Combine(Program.configPath, (isDebug() ? "DEBUG." : "") + Program.commandFielName);
            Program.errorLogPath = Path.Combine(Program.configPath, (isDebug() ? "DEBUG." : "") + "error.log");

            if (!File.Exists(Program.commandFielPath) || isDebug()) {
                try
                {
                    File.WriteAllText(commandFielPath, @"<root>
<commands>
    <command>
    <shortcut>WIN+W</shortcut>
    <enabled>1</enabled>
    <action>CreateWidget</action>
    </command>
</commands>
</root>");
                } catch { 
                }
            }

            ClearBigLog();

            Program.debug("Start");

            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
            SystemEvents.SessionSwitch -= new SessionSwitchEventHandler(SystemEvents_SessionSwitch);

            LoadConfig();

            Program.loadCommands();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            formShootRunner = new FormShootRunner();
            Application.Run(formShootRunner);

            SaveConfig();

            Program.debug("End");
        }
    }
}
