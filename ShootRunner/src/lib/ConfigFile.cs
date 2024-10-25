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
                item.Add(new XElement("isDesktop", ConvertTo.BoolToString(pin.window.isDesktop)));
                item.Add(new XElement("isTaskbar", ConvertTo.BoolToString(pin.window.isTaskbar)));
                item.Add(new XElement("left", pin.Left.ToString()));
                item.Add(new XElement("top", pin.Top.ToString()));
                item.Add(new XElement("width", pin.Width.ToString()));
                item.Add(new XElement("height", pin.Height.ToString()));
                item.Add(new XElement("mosttop", ConvertTo.BoolToString(pin.window.mosttop)));
                item.Add(new XElement("locked", ConvertTo.BoolToString(pin.window.locked)));
                item.Add(new XElement("transparent", ConvertTo.BoolToString(pin.window.locked)));
            }

            XElement widgets = new XElement("widgets");
            root.Add(widgets);

            foreach (FormWidget w in Program.widgets)
            {
                XElement widget = new XElement("widget");
                widgets.Add(widget);
                widget.Add(new XElement("type", w.Type));
                widget.Add(new XElement("left", w.Left.ToString()));
                widget.Add(new XElement("top", w.Top.ToString()));
                widget.Add(new XElement("width", w.Width.ToString()));
                widget.Add(new XElement("height", w.Height.ToString()));
                widget.Add(new XElement("mosttop", ConvertTo.BoolToString(w.TopMost)));
                widget.Add(new XElement("locked", ConvertTo.BoolToString(w.locked)));
                widget.Add(new XElement("transparent", ConvertTo.BoolToString(w.locked)));
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
                                            window.transparent = ConvertTo.StringToBool(el.Value);
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
                        foreach (XElement widget in item.Elements())
                        {

                            if (widget.Name.ToString() == "widget")
                            {

                                FormWidget widgetForm = new FormWidget();
                                Program.widgets.Add(widgetForm);

                                foreach (XElement el in widget.Elements())
                                {
                                    try
                                    {

                                        if (el.Name.ToString() == "type")
                                        {
                                            widgetForm.Type = el.Value;
                                        }

                                        if (el.Name.ToString() == "left")
                                        {
                                            widgetForm.StartLeft = ConvertTo.StringToInt(el.Value);
                                        }

                                        if (el.Name.ToString() == "top")
                                        {
                                            widgetForm.StartTop = ConvertTo.StringToInt(el.Value);
                                        }

                                        if (el.Name.ToString() == "width")
                                        {
                                            widgetForm.StartWidth = ConvertTo.StringToInt(el.Value);
                                        }

                                        if (el.Name.ToString() == "height")
                                        {
                                            widgetForm.StartHeight = ConvertTo.StringToInt(el.Value);
                                        }

                                        if (el.Name.ToString() == "mosttop")
                                        {
                                            widgetForm.TopMost = ConvertTo.StringToBool(el.Value);
                                        }

                                        if (el.Name.ToString() == "locked")
                                        {
                                            widgetForm.locked = ConvertTo.StringToBool(el.Value);
                                        }

                                        if (el.Name.ToString() == "transparent")
                                        {
                                            widgetForm.transparent = ConvertTo.StringToBool(el.Value);
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
