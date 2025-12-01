using System.Diagnostics.Metrics;
using System.Reflection.Metadata;
using System.Text;
using System.Xml;
using System.Xml.Linq;

#nullable disable

#pragma warning disable IDE0079
#pragma warning disable IDE0130
#pragma warning disable CA1822

namespace ShootRunner
{
    public class ConfigFile(Config config)
    {
        readonly Config config = config;

        public void Load() {

            if (!File.Exists(Program.configFielPath)) {
                return;
            }

            XmlReaderSettings xws = new()
            {
                CheckCharacters = false
            };

            // load config file
            string xml = File.ReadAllText(Program.configFielPath);

            try
            {
                using XmlReader xr = XmlReader.Create(new StringReader(xml), xws);

                XElement root = XElement.Load(xr);
                this.LoadInnerXmlCommands(root);
            }
            catch (Exception ex)
            {
                Program.Error(ex.Message);
            }
        }

        public void Save()
        {
            StringBuilder sb = new();
            XmlWriterSettings xws = new()
            {
                OmitXmlDeclaration = true,
                CheckCharacters = false,
                Indent = true
            };


            XElement root = new("config");
            XElement items = new("pins");
            root.Add(items);

            foreach (FormPin pinForm in Program.pins)
            {
                XElement item = new("pin");
                items.Add(item);

                if (pinForm.pin.window != null) {
                    item.Add(new XElement("type", pinForm.pin.window.Type));
                    item.Add(new XElement("title", pinForm.pin.window.Title));
                    item.Add(new XElement("app", pinForm.pin.window.app));
                    item.Add(new XElement("icon", ConvertTo.BitmapToString(pinForm.pin.window.icon)));
                    item.Add(new XElement("isDesktop", ConvertTo.BoolToString(pinForm.pin.window.isDesktop)));
                    item.Add(new XElement("isTaskbar", ConvertTo.BoolToString(pinForm.pin.window.isTaskbar)));
                }


                item.Add(new XElement("usewindow", ConvertTo.BoolToString(pinForm.pin.useWindow)));
                item.Add(new XElement("usefilelink", ConvertTo.BoolToString(pinForm.pin.useFilelink)));
                item.Add(new XElement("filelink", pinForm.pin.filelink));
                item.Add(new XElement("usedirectorylink", ConvertTo.BoolToString(pinForm.pin.useDirectorylink)));
                item.Add(new XElement("directorylink", pinForm.pin.directorylink));
                item.Add(new XElement("usehyperlink", ConvertTo.BoolToString(pinForm.pin.useHyperlink)));
                item.Add(new XElement("hyperlink", pinForm.pin.hyperlink));
                item.Add(new XElement("usescript", ConvertTo.BoolToString(pinForm.pin.useScript)));
                item.Add(new XElement("script", pinForm.pin.script));

                item.Add(new XElement("usecommand", ConvertTo.BoolToString(pinForm.pin.useCommand)));
                item.Add(new XElement("command", pinForm.pin.command));
                item.Add(new XElement("useworkdir", ConvertTo.BoolToString(pinForm.pin.useWorkdir)));
                item.Add(new XElement("workdir", pinForm.pin.workdir));
                item.Add(new XElement("usepowershell", ConvertTo.BoolToString(pinForm.pin.usePowershell)));
                item.Add(new XElement("usecmdshell", ConvertTo.BoolToString(pinForm.pin.useCmdshell)));
                item.Add(new XElement("silentcommand", ConvertTo.BoolToString(pinForm.pin.silentCommand)));
                item.Add(new XElement("matchNewWindow", ConvertTo.BoolToString(pinForm.pin.matchNewWindow)));
                item.Add(new XElement("doubleclickcommand", ConvertTo.BoolToString(pinForm.pin.doubleClickCommand)));
                
                if (pinForm.pin.customicon != null) {
                    item.Add(new XElement("customicon", ConvertTo.BitmapToString(pinForm.pin.customicon)));
                }

                item.Add(new XElement("left", pinForm.Left.ToString()));
                item.Add(new XElement("top", pinForm.Top.ToString()));
                item.Add(new XElement("width", pinForm.Width.ToString()));
                item.Add(new XElement("height", pinForm.Height.ToString()));
                item.Add(new XElement("mosttop", ConvertTo.BoolToString(pinForm.pin.mosttop)));
                item.Add(new XElement("locked", ConvertTo.BoolToString(pinForm.pin.locked)));
                item.Add(new XElement("transparent", ConvertTo.DoubleToString(pinForm.pin.transparent)));
            }

            XElement widgets = new("widgets");
            root.Add(widgets);

            foreach (Widget w in Program.widgetManager.widgets)
            {

                if (w.type == "taskbar") {
                    continue;
                }

                XElement widget = new("widget");
                widgets.Add(widget);
                widget.Add(new XElement("type", w.type));
                if (w.widgetForm != null)
                {
                    widget.Add(new XElement("left", w.StartLeft.ToString()));
                    widget.Add(new XElement("top", w.StartTop.ToString()));
                    widget.Add(new XElement("width", w.StartWidth.ToString()));
                    widget.Add(new XElement("height", w.StartHeight.ToString()));
                    widget.Add(new XElement("mosttop", ConvertTo.BoolToString(w.widgetForm.TopMost)));
                }

                widget.Add(new XElement("locked", ConvertTo.BoolToString(w.locked)));
                widget.Add(new XElement("transparent", ConvertTo.DoubleToString(w.transparent)));

                widget.Add(new XElement("backgroundColor", ConvertTo.ColorToString(w.backgroundColor)));
                widget.Add(new XElement("useBigIcons", ConvertTo.BoolToString(w.useBigIcons)));
                widget.Add(new XElement("useScreenshots", ConvertTo.BoolToString(w.useScreenshots)));


                XElement data = new("data");
                widget.Add(data);

                foreach (KeyValuePair<string, string> entry in w.data)
                {
                    string key = entry.Key;
                    string value = entry.Value;
                    XElement item = new("item");
                    data.Add(item);
                    item.Add(new XAttribute("key", key));
                    item.Value = value;
                }
            }

            if (Program.widgetManager.formTaskbar != null) {
                XElement widget = new("widget");
                widgets.Add(widget);
                FormTaskbar formTaskbar = Program.widgetManager.formTaskbar;
                Widget taskbarWidget = formTaskbar.widget;

                widget.Add(new XElement("type", taskbarWidget.type));

                widget.Add(new XElement("left", taskbarWidget.StartLeft.ToString()));
                widget.Add(new XElement("top", taskbarWidget.StartTop.ToString()));
                widget.Add(new XElement("width", taskbarWidget.StartWidth.ToString()));
                widget.Add(new XElement("height", taskbarWidget.StartHeight.ToString()));
                widget.Add(new XElement("mosttop", ConvertTo.BoolToString(formTaskbar.TopMost)));


                widget.Add(new XElement("locked", ConvertTo.BoolToString(taskbarWidget.locked)));
                widget.Add(new XElement("transparent", ConvertTo.DoubleToString(taskbarWidget.transparent)));

                widget.Add(new XElement("backgroundColor", ConvertTo.ColorToString(taskbarWidget.backgroundColor)));
                widget.Add(new XElement("useBigIcons", ConvertTo.BoolToString(taskbarWidget.useBigIcons)));
                widget.Add(new XElement("useScreenshots", ConvertTo.BoolToString(taskbarWidget.useScreenshots)));
            }

            try
            {

                using XmlWriter xw = XmlWriter.Create(sb, xws);
                root.WriteTo(xw);

            }
            catch (Exception ex)
            {
                Program.Error(ex.Message);
            }


            try
            {
                using StreamWriter file = new(Program.configFielPath);
                file.Write(sb.ToString());
                file.Close();

            }
            catch (System.IO.IOException ex)
            {
                Program.Error(ex.Message);
            }
            catch (Exception ex)
            {
                Program.Error(ex.Message);
            }

        }

        public void LoadInnerXmlCommands(XElement root)
        {
            try {
                foreach (XElement item in root.Elements())
                {
                    if (item.Name.ToString() == "pins")
                    {
                        foreach (XElement pin in item.Elements())
                        {

                            if (pin.Name.ToString() == "pin")
                            {
                                Window window = new();
                                FormPin formPin = new(window);
                                Program.pins.Add(formPin);

                                foreach (XElement el in pin.Elements())
                                {
                                    try
                                    {

                                        if (el.Name.ToString() == "type")
                                        {
                                            window.Type = el.Value;
                                        }

                                        if (el.Name.ToString() == "title")
                                        {
                                            window.Title = el.Value;
                                        }

                                        if (el.Name.ToString() == "app")
                                        {
                                            window.app = el.Value;
                                        }

                                        if (el.Name.ToString() == "usewindow")
                                        {
                                            formPin.pin.useWindow = ConvertTo.StringToBool(el.Value);
                                        }

                                        if (el.Name.ToString() == "usefilelink")
                                        {
                                            formPin.pin.useFilelink = ConvertTo.StringToBool(el.Value);
                                        }

                                        if (el.Name.ToString() == "filelink")
                                        {
                                            formPin.pin.filelink = TextTools.NormalizeLineEndings(el.Value);
                                        }

                                        if (el.Name.ToString() == "usedirectorylink")
                                        {
                                            formPin.pin.useDirectorylink = ConvertTo.StringToBool(el.Value);
                                        }

                                        if (el.Name.ToString() == "directorylink")
                                        {
                                            formPin.pin.directorylink = TextTools.NormalizeLineEndings(el.Value);
                                        }

                                        if (el.Name.ToString() == "usehyperlink")
                                        {
                                            formPin.pin.useHyperlink = ConvertTo.StringToBool(el.Value);
                                        }

                                        if (el.Name.ToString() == "hyperlink")
                                        {
                                            formPin.pin.hyperlink = TextTools.NormalizeLineEndings(el.Value);
                                        }

                                        if (el.Name.ToString() == "usescript")
                                        {
                                            formPin.pin.useScript = ConvertTo.StringToBool(el.Value);
                                        }

                                        if (el.Name.ToString() == "script")
                                        {
                                            formPin.pin.script = TextTools.NormalizeLineEndings(el.Value);
                                        }

                                        if (el.Name.ToString() == "usecommand")
                                        {
                                            formPin.pin.useCommand = ConvertTo.StringToBool(el.Value);
                                        }

                                        if (el.Name.ToString() == "command")
                                        {
                                            formPin.pin.command = TextTools.NormalizeLineEndings(el.Value);
                                        }

                                        if (el.Name.ToString() == "useworkdir")
                                        {
                                            formPin.pin.useWorkdir = ConvertTo.StringToBool(el.Value);
                                        }

                                        if (el.Name.ToString() == "workdir")
                                        {
                                            formPin.pin.workdir = TextTools.NormalizeLineEndings(el.Value);
                                        }

                                        if (el.Name.ToString() == "usepowershell")
                                        {
                                            formPin.pin.usePowershell = ConvertTo.StringToBool(el.Value);
                                        }

                                        if (el.Name.ToString() == "usecmdshell")
                                        {
                                            formPin.pin.useCmdshell = ConvertTo.StringToBool(el.Value);
                                        }

                                        if (el.Name.ToString() == "silentcommand")
                                        {
                                            formPin.pin.silentCommand = ConvertTo.StringToBool(el.Value);
                                        }

                                        if (el.Name.ToString() == "matchNewWindow")
                                        {
                                            formPin.pin.matchNewWindow = ConvertTo.StringToBool(el.Value);
                                        }

                                        if (el.Name.ToString() == "doubleclickcommand")
                                        {
                                            formPin.pin.doubleClickCommand = ConvertTo.StringToBool(el.Value);
                                        }

                                        if (el.Name.ToString() == "icon")
                                        {
                                            window.icon = ConvertTo.StringToBitmap(el.Value);
                                        }

                                        if (el.Name.ToString() == "customicon")
                                        {
                                            formPin.pin.customicon = ConvertTo.StringToBitmap(el.Value);
                                        }

                                        if (el.Name.ToString() == "isDesktop")
                                        {
                                            window.isDesktop = ConvertTo.StringToBool(el.Value);
                                        }

                                        if (el.Name.ToString() == "isTaskbar")
                                        {
                                            window.isTaskbar = ConvertTo.StringToBool(el.Value);
                                        }

                                        if (el.Name.ToString() == "left")
                                        {
                                            formPin.StartLeft = ConvertTo.StringToInt(el.Value);
                                        }

                                        if (el.Name.ToString() == "top")
                                        {
                                            formPin.StartTop = ConvertTo.StringToInt(el.Value);
                                        }

                                        if (el.Name.ToString() == "width")
                                        {
                                            formPin.StartWidth = ConvertTo.StringToInt(el.Value);
                                        }

                                        if (el.Name.ToString() == "height")
                                        {
                                            formPin.StartHeight = ConvertTo.StringToInt(el.Value);
                                        }

                                        if (el.Name.ToString() == "mosttop")
                                        {
                                            formPin.pin.mosttop = ConvertTo.StringToBool(el.Value);
                                        }

                                        if (el.Name.ToString() == "locked")
                                        {
                                            window.locked = ConvertTo.StringToBool(el.Value);
                                        }

                                        if (el.Name.ToString() == "transparent")
                                        {
                                            window.transparent = ConvertTo.StringToDouble(el.Value);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        Program.Error(ex.Message);
                                    }
                                }

                            }
                        }
                    }

                    if (item.Name.ToString() == "widgets")
                    {
                        foreach (XElement widgetEl in item.Elements())
                        {

                            if (widgetEl.Name.ToString() == "widget")
                            {

                                Widget widget = new();
                                Program.widgetManager.widgets.Add(widget);

                                foreach (XElement el in widgetEl.Elements())
                                {
                                    try
                                    {

                                        if (el.Name.ToString() == "type")
                                        {
                                            widget.type = el.Value;
                                        }

                                        if (el.Name.ToString() == "left")
                                        {
                                            widget.StartLeft = ConvertTo.StringToInt(el.Value);
                                        }

                                        if (el.Name.ToString() == "top")
                                        {
                                            widget.StartTop = ConvertTo.StringToInt(el.Value);
                                        }

                                        if (el.Name.ToString() == "width")
                                        {
                                            widget.StartWidth = ConvertTo.StringToInt(el.Value);
                                        }

                                        if (el.Name.ToString() == "height")
                                        {
                                            widget.StartHeight = ConvertTo.StringToInt(el.Value);
                                        }

                                        if (el.Name.ToString() == "mosttop")
                                        {
                                            widget.mosttop = ConvertTo.StringToBool(el.Value);
                                        }

                                        if (el.Name.ToString() == "locked")
                                        {
                                            widget.locked = ConvertTo.StringToBool(el.Value);
                                        }

                                        if (el.Name.ToString() == "transparent")
                                        {
                                            widget.transparent = ConvertTo.StringToDouble(el.Value);
                                        }

                                        if (el.Name.ToString() == "backgroundColor")
                                        {
                                            widget.backgroundColor = ConvertTo.StringToColor(el.Value, Color.Black);
                                        }

                                        if (el.Name.ToString() == "useBigIcons")
                                        {
                                            widget.useBigIcons = ConvertTo.StringToBool(el.Value, false);
                                        }

                                        if (el.Name.ToString() == "useScreenshots")
                                        {
                                            widget.useScreenshots = ConvertTo.StringToBool(el.Value, false);
                                        }

                                        if (el.Name.ToString() == "data")
                                        {
                                            foreach (XElement el1 in el.Elements())
                                            {
                                                if (el1.Name.ToString() == "item")
                                                {
                                                    if (el1.Attribute("key") != null) {
                                                        widget.data.Add(el1.Attribute("key").Value, el1.Value);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Program.Error(ex.Message);
                                    }
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.Error(ex.Message);
            }
        }


        

    }
}
