using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootRunner
{
    public  class WidgetManager
    {
        public List<Widget> widgets = new List<Widget>();
        public List<FormWidget> widgetForms = new List<FormWidget>();
        public List<WidgetType> widgeTypes = new List<WidgetType>();

        public void LoadWidgetTypes()
        {
            try
            {
                string[] subDirectories = Directory.GetDirectories(Program.widgetsPath);

                foreach (string directory in subDirectories)
                {
                    try
                    {
                        string htmlPath = Path.Combine(directory, "widget.html");
                        if (File.Exists(htmlPath)) { 
                            WidgetType widgetType = new WidgetType();
                            widgetType.name = Path.GetFileName(directory); ;
                            widgetType.path = directory;
                            widgetType.source = htmlPath;
                            widgetType.html = File.ReadAllText(htmlPath);

                            widgeTypes.Add(widgetType);

                        }
                    }
                    catch (Exception ex)
                    {
                        Program.error(ex.Message);
                    }
                }

            } catch (Exception ex) {
                Program.error(ex.Message);
            }
        }

        public void Add(Widget widget)
        {
            widgets.Add(widget);
        }

        public void AddEmptyWidget()
        {
            Widget widget = new Widget();
            FormWidget widgeForm = new FormWidget(widget);
            widgets.Add(widget);
            widgetForms.Add(widgeForm);
            widgeForm.TopMost = true;
            widgeForm.Show();
            widgeForm.Center();
            Program.Update();
        }

        public void ShowWidget(Widget widget)
        {
            widget.widgetType = this.FindWidgetType(widget.type);
            FormWidget widgeForm = new FormWidget(widget);
            widget.widgetForm = widgeForm;            
            widgetForms.Add(widgeForm);
            widgeForm.TopMost = widget.mosttop;
            widgeForm.Show();
            widgeForm.SetStartPosition();
            Program.Update();
        }

        public void RemoveWidget(FormWidget formWidget) {
            this.widgets.Remove(formWidget.widget);
            this.widgetForms.Remove(formWidget);
        }

        public WidgetType FindWidgetType(string name)
        {
            foreach (var type in widgeTypes) {
                if (type.name == name) { 
                    return type;
                }
            }
            return null;
        }

        public void OpenWidgets()
        {
            this.LoadWidgetTypes();

            foreach (var widget in widgets)
            {
                this.ShowWidget(widget);
            }
        }
    }
}
