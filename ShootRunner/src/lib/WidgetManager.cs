#nullable disable

#pragma warning disable IDE0079
#pragma warning disable IDE0130
#pragma warning disable CA1822

namespace ShootRunner
{
    public  class WidgetManager
    {

        public FormTaskbar formTaskbar = null;

        public List<Widget> widgets = [];
        public List<FormWidget> widgetForms = [];
        public List<WidgetType> widgeTypes = [];
        public FormWidgetCreate formWidgetCreate = null;

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
                            WidgetType widgetType = new() { name = Path.GetFileName(directory) };
                            ;
                            widgetType.path = directory;
                            widgetType.source = htmlPath;
                            widgetType.html = File.ReadAllText(htmlPath);

                            widgeTypes.Add(widgetType);

                        }
                    }
                    catch (Exception ex)
                    {
                        Program.Error(ex.Message);
                    }
                }

            } catch (Exception ex) {
                Program.Error(ex.Message);
            }
        }

        public void Add(Widget widget)
        {
            widgets.Add(widget);
        }

        public void AddEmptyWidget()
        {
            Widget widget = new();
            FormWidget widgeForm = new(widget);
            widgets.Add(widget);
            widgetForms.Add(widgeForm);
            widgeForm.TopMost = true;
            widgeForm.Show();
            widgeForm.Center();
            Program.Update();
        }

        

        public void ShowWidget(Widget widget)
        {
            if (widget.type == "taskbar") {
                this.ShowTaskbarWidget(widget);
            } else {
                widget.widgetType = this.FindWidgetType(widget.type);
                FormWidget widgeForm = new(widget);
                widget.widgetForm = widgeForm;
                widgetForms.Add(widgeForm);
                widgeForm.TopMost = widget.mosttop;
                widgeForm.Show();
                widgeForm.SetStartPosition();
                Program.Update();
            }
        }

        public void RemoveWidget(FormWidget formWidget) {
            this.widgets.Remove(formWidget.widget);
            this.widgetForms.Remove(formWidget);
        }


        public void ShowTaskbarWidget(Widget widget)
        {
            if (widget == null)
            {
                widget = new Widget
                {
                    type = "taskbar"
                };
                widgets.Add(widget);
            }

            if (formTaskbar == null){
                this.formTaskbar = new FormTaskbar(widget);
            }

            this.formTaskbar.Show();            
            Program.Update();
        }

        public void RemoveTaskbarWidget(FormTaskbar formTaskbar)
        {
            this.widgets.Remove(formTaskbar.widget);
            this.formTaskbar = null;
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

        
        public void HideAllWidgets()
        {
            if (this.widgets != null)
            {
                foreach (var widget in widgets)
                {
                    widget.widgetForm?.Hide();                    
                }
            }
            
        }

        public void ShowAllWidgets()
        {
            if (this.widgets != null)
            {
                foreach (var widget in widgets)
                {
                    widget.widgetForm?.Show();
                }
            }
        }

        public void CreateWidget(string name = "", WidgetType type = null)
        {

            if (name.Trim() == "") {
                return;
            }

            try
            {
                string widgetPath = Path.Combine(Program.widgetsPath, name);
                string widgetHtmlPath = Path.Combine(widgetPath, "widget.html");
                Directory.CreateDirectory(widgetPath);
                File.WriteAllText(widgetHtmlPath, type.html);
            }
            catch (Exception)
            {

                
            }
        }

        public  void ShowCreateWidgetForm()
        {

            if (this.formWidgetCreate == null || this.formWidgetCreate.IsDisposed) {
              this.formWidgetCreate = new FormWidgetCreate();
            }

            this.formWidgetCreate.Show();
        }
    }
}
