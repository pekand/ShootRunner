using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Reflection;
using System.IO;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections;

#nullable disable


namespace ShootRunner
{
    public class ConfigFile
    {
        Config config = null;

        public ConfigFile(Config config) { 
            this.config = config;
        }

        public void Load() {

            if (!File.Exists(Program.configFielPath)) {
                return;
            }

            XmlReaderSettings xws = new XmlReaderSettings
            {
                CheckCharacters = false
            };

            // load config file
            string xml = File.ReadAllText(Program.configFielPath);

            try
            {
                using (XmlReader xr = XmlReader.Create(new StringReader(xml), xws))
                {

                    XElement root = XElement.Load(xr);
                    this.LoadInnerXmlCommands(root);
                }
            }
            catch (Exception ex)
            {
                Program.error(ex.Message);
            }
        }

        public void Save()
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings xws = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                CheckCharacters = false,
                Indent = true
            };


            XElement root = new XElement("config");
            XElement items = new XElement("pins");
            root.Add(items);

            foreach (FormPin pin in Program.pins)
            {
                XElement item = new XElement("pin");
                items.Add(item);
                item.Add(new XElement("type", pin.window.Type));
                item.Add(new XElement("title", pin.window.Title));
                item.Add(new XElement("app", pin.window.app));
                item.Add(new XElement("command", pin.window.command));
                item.Add(new XElement("silentcommand", ConvertTo.BoolToString(pin.window.silentCommand)));
                item.Add(new XElement("doubleclickcommand", ConvertTo.BoolToString(pin.window.doubleClickCommand)));
                item.Add(new XElement("icon", ConvertTo.BitmapToString(pin.window.icon)));
                if (pin.window.customicon != null) {
                    item.Add(new XElement("customicon", ConvertTo.BitmapToString(pin.window.customicon)));
                }
                item.Add(new XElement("isDesktop", ConvertTo.BoolToString(pin.window.isDesktop)));
                item.Add(new XElement("isTaskbar", ConvertTo.BoolToString(pin.window.isTaskbar)));
                item.Add(new XElement("left", pin.Left.ToString()));
                item.Add(new XElement("top", pin.Top.ToString()));
                item.Add(new XElement("width", pin.Width.ToString()));
                item.Add(new XElement("height", pin.Height.ToString()));
                item.Add(new XElement("mosttop", ConvertTo.BoolToString(pin.window.mosttop)));
                item.Add(new XElement("locked", ConvertTo.BoolToString(pin.window.locked)));
                item.Add(new XElement("transparent", ConvertTo.DoubleToString(pin.window.transparent)));
            }

            XElement widgets = new XElement("widgets");
            root.Add(widgets);

            foreach (Widget w in Program.widgetManager.widgets)
            {

                if (w.type == "taskbar") {
                    continue;
                }

                XElement widget = new XElement("widget");
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


                XElement data = new XElement("data");
                widget.Add(data);

                foreach (KeyValuePair<string, string> entry in w.data)
                {
                    string key = entry.Key;
                    string value = entry.Value;
                    XElement item = new XElement("item");
                    data.Add(item);
                    item.Add(new XAttribute("key", key));
                    item.Value = value;
                }
            }

            if (Program.widgetManager.formTaskbar != null) {
                XElement widget = new XElement("widget");
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

                using (XmlWriter xw = XmlWriter.Create(sb, xws))
                {
                    root.WriteTo(xw);
                }

            }
            catch (Exception ex)
            {
                Program.error(ex.Message);
            }


            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(Program.configFielPath);
                file.Write(sb.ToString());
                file.Close();

            }
            catch (System.IO.IOException ex)
            {
                Program.error(ex.Message);
            }
            catch (Exception ex)
            {
                Program.error(ex.Message);
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
                                Window window = new Window();
                                FormPin formPin = new FormPin(window);
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

                                        if (el.Name.ToString() == "command")
                                        {
                                            window.command = TextTools.NormalizeLineEndings(el.Value);
                                        }

                                        if (el.Name.ToString() == "silentcommand")
                                        {
                                            window.silentCommand = ConvertTo.StringToBool(el.Value);
                                        }

                                        if (el.Name.ToString() == "doubleclickcommand")
                                        {
                                            window.doubleClickCommand = ConvertTo.StringToBool(el.Value);
                                        }

                                        if (el.Name.ToString() == "icon")
                                        {
                                            window.icon = ConvertTo.StringToBitmap(el.Value);
                                        }

                                        if (el.Name.ToString() == "customicon")
                                        {
                                            window.customicon = ConvertTo.StringToBitmap(el.Value);
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
                                            window.mosttop = ConvertTo.StringToBool(el.Value);
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
                                        Program.error(ex.Message);
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

                                Widget widget = new Widget();
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
                                        Program.error(ex.Message);
                                    }
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


        

    }
}
