using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ShootRunner.src.forms
{
    public partial class FormWindowInfo : Form
    {
        Window window = null;
        public FormWindowInfo(Window window)
        {
            this.window = window;
            InitializeComponent();
            UpdateInfo();
        }

        public void clear()
        {
            console.Text = "";
        }

        public void write(string message)
        {
            console.Text += message + "\r\n";
        }

        private void FormWindowInfo_Load(object sender, EventArgs e)
        {
            textBox2.Text = window.Handle.ToString();
            textBox3.Text = window.processId.ToString();
            textBox4.Text = window.className?.ToString();
            textBox5.Text = window.app;
            textBox6.Text = window.directory;
            textBox7.Text = window.executable;
            textBox8.Text = window.command;

            pictureBox1.Image = window.icon;
            pictureBox3.Image = window.screenshot;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            UpdateInfo();
        }

        public void UpdateInfo() {

            textBox1.Text = ToolsWindow.GetWindowTitle(this.window);

            this.clear();

            this.write("Is minimalized: " + (ToolsWindow.IsMinimalized(window) ? "Yes" : "No"));
            this.write("Is maximalized: " + (ToolsWindow.IsMaximalized(window.Handle) ? "Yes" : "No"));
            this.write("Has position: " + (ToolsWindow.WindowHasPosition(window.Handle) ? "Yes" : "No"));
            
            Rectangle? rec = ToolsWindow.GetWindowPosition(window.Handle);
            if (rec != null)
            {
                this.write("Position X: " + rec.Value.X.ToString());
                this.write("Position Y: " + rec.Value.Y.ToString());
                this.write("Position Width: " + rec.Value.Width.ToString());
                this.write("Position Height: " + rec.Value.Height.ToString());
            }

            this.write("Is popup window: " + (ToolsWindow.IsWindowPopup(window.Handle) ? "Yes" : "No"));
            this.write("Is tool window: " + (ToolsWindow.IsToolWindow(window.Handle) ? "Yes" : "No"));
            this.write("Is visible v2: " + (ToolsWindow.IsWindowVisible2(window.Handle) ? "Yes" : "No"));
            this.write("Is UWP Window: " + (ToolsWindow.IsUWPWindow(window.Handle) ? "Yes" : "No"));
            this.write("Is cloaked: " + (ToolsWindow.IsWindowCloaked(window.Handle) ? "Yes" : "No"));
            this.write("Is GPU rendered: " + (ToolsWindow.IsGpuRenderedWindow(window) ? "Yes" : "No"));
            this.write("Is window no bitmap type: " + (ToolsWindow.IsWindowNoBitmapType(window) ? "Yes" : "No"));
            this.write("Is gpu library loaded: " + (ToolsWindow.IsGpuLibraryLoaded(window) ? "Yes" : "No"));
            this.write("Is taskbar: " + (ToolsWindow.IsTaskbarWindow(window) ? "Yes" : "No"));
            this.write("Is desktop: " + (ToolsWindow.IsDesktopWindow(window) ? "Yes" : "No"));
            this.write("SessionId: " + ToolsWindow.GetWindowSessionId(window.Handle).ToString());
        }
    }
}
