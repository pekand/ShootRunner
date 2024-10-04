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

namespace ShootRunner
{
    internal static class Program
    {
        public static string AppName = "ShootRunner";

        public static List<Command> commands = new List<Command>();

        public static FormShootRunner formShootRunner = null;

        public static string roamingAppDataPath = "";
        public static string directoryName = "";
        public static string configPath = "";
        public static string errorLogPath = "";
        public static string configFielPath = "";
        public static string commandFielPath = "";
        public static string commandFielName = "commands.xml";
        public static DateTime commandFielPathLastChange;

        public static bool autorun = false;

        public static void log(string message) {
#if DEBUG
            string filePath = "application.log";
            string unixTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(unixTime + " " + message);
                Console.WriteLine(unixTime + " " + message);
            }
#endif
        }

        public static void error(string message)
        {
            string filePath = "application.log";
            string unixTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            using (StreamWriter sw = new StreamWriter(Program.errorLogPath, true))
            {
                sw.WriteLine(unixTime + " " + message);
                Console.WriteLine(unixTime + " " + message);
            }
        }

        public static void loadCommands()
        {
            if (File.Exists(Program.commandFielPath))
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

                            newCommand.shortcut = commandElement.Element("shortcut")?.Value;
                            newCommand.open = commandElement.Element("open")?.Value;
                            newCommand.command = commandElement.Element("command")?.Value;
                            newCommand.parameters = commandElement.Element("parameters")?.Value;
                            newCommand.window = commandElement.Element("window")?.Value;
                            newCommand.workdir = commandElement.Element("workdir")?.Value;
                            newCommand.keypress = commandElement.Element("keypress")?.Value;
                            newCommand.action = commandElement.Element("action")?.Value;
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
        }
        
        [STAThread]
        static void Main()
        {
            Program.log("Start");
            Program.roamingAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Program.directoryName = "ShootRunner";
            Program.configPath = Path.Combine(Program.roamingAppDataPath, Program.directoryName);
            Program.configFielPath = Path.Combine(Program.configPath, "config.xml");
            Program.commandFielPath = Path.Combine(Program.configPath, Program.commandFielName);
            Program.errorLogPath = Path.Combine(Program.configPath, "error.log");

            if (File.Exists(configFielPath)) {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(configFielPath);
                XmlNodeList nodes = xmlDoc.DocumentElement.SelectNodes("/root/autorun");
                foreach (XmlNode node in nodes)
                {                    
                    Program.autorun = node.InnerText == "1";
                }
            }

            Program.loadCommands();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            formShootRunner = new FormShootRunner();
            Application.Run(formShootRunner);

            if (true) {
                if (!Directory.Exists(configPath))
                {
                    Directory.CreateDirectory(configPath);
                }

                XmlDocument xmlDoc = new XmlDocument();
                XmlElement root = xmlDoc.CreateElement("root");
                xmlDoc.AppendChild(root);
                XmlElement element = xmlDoc.CreateElement("autorun");
                element.InnerText = Program.autorun ? "1" : "0";
                root.AppendChild(element);
                xmlDoc.Save(configFielPath);
            }

            Program.log("End");
        }
    }
}
