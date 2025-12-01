#pragma warning disable IDE0079
#pragma warning disable IDE0130

namespace ShootRunner
{
    public partial class FormWindowInfo : Form
    {
        readonly Window? window = null;
        public FormWindowInfo(Window window)
        {
            this.window = window;
            InitializeComponent();
            UpdateInfo();
        }

        public void Clear()
        {
            console.Text = "";
        }

        public void Write(string message)
        {
            console.Text += message + "\r\n";
        }

        private void FormWindowInfo_Load(object sender, EventArgs e)
        {
            textBox2.Text = window?.Handle.ToString();
            textBox3.Text = window?.processId.ToString();
            textBox4.Text = window?.className?.ToString();
            textBox5.Text = window?.app;
            textBox6.Text = window?.directory;
            textBox7.Text = window?.executable;
            textBox8.Text = window?.command;

            pictureBox1.Image = window?.icon;
            pictureBox3.Image = window?.screenshot;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateInfo();
        }

        public void UpdateInfo() {

            textBox1.Text = ToolsWindow.GetWindowTitle(this.window);

            this.Clear();

            if (window != null)
            {
                this.Write("Is minimalized: " + (ToolsWindow.IsMinimalized(window) ? "Yes" : "No"));
                this.Write("Is maximalized: " + (ToolsWindow.IsMaximalized(window.Handle) ? "Yes" : "No"));
                this.Write("Has position: " + (ToolsWindow.WindowHasPosition(window.Handle) ? "Yes" : "No"));


                Rectangle? rec = ToolsWindow.GetWindowPosition(window.Handle);
                if (rec != null)
                {
                    this.Write("Position X: " + rec.Value.X.ToString());
                    this.Write("Position Y: " + rec.Value.Y.ToString());
                    this.Write("Position Width: " + rec.Value.Width.ToString());
                    this.Write("Position Height: " + rec.Value.Height.ToString());
                }

                this.Write("Is popup window: " + (ToolsWindow.IsWindowPopup(window.Handle) ? "Yes" : "No"));
                this.Write("Is tool window: " + (ToolsWindow.IsToolWindow(window.Handle) ? "Yes" : "No"));
                this.Write("Is visible v2: " + (ToolsWindow.IsWindowVisible2(window.Handle) ? "Yes" : "No"));
                this.Write("Is UWP Window: " + (ToolsWindow.IsUWPWindow(window.Handle) ? "Yes" : "No"));
                this.Write("Is cloaked: " + (ToolsWindow.IsWindowCloaked(window.Handle) ? "Yes" : "No"));
                this.Write("Is GPU rendered: " + (ToolsWindow.IsGpuRenderedWindow(window) ? "Yes" : "No"));
                this.Write("Is window no bitmap type: " + (ToolsWindow.IsWindowNoBitmapType(window) ? "Yes" : "No"));
                this.Write("Is gpu library loaded: " + (ToolsWindow.IsGpuLibraryLoaded(window) ? "Yes" : "No"));
                this.Write("Is taskbar: " + (ToolsWindow.IsTaskbarWindow(window) ? "Yes" : "No"));
                this.Write("Is desktop: " + (ToolsWindow.IsDesktopWindow(window) ? "Yes" : "No"));
                this.Write("SessionId: " + ToolsWindow.GetWindowSessionId(window.Handle).ToString());
            }
        }
    }
}
