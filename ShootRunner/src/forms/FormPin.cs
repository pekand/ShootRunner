using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShootRunner
{
    public partial class FormPin : Form
    {
        public int StartLeft = 100;
        public int StartTop = 100;
        public int StartWidth = 64;
        public int StartHeight = 64;
        

        public Window window = null;

        public FormPin(Window window)
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.MinimumSize = new Size(32, 32);            
            this.BackColor = Color.White;
            this.TopMost = true;

            
            this.BackColor = System.Drawing.Color.Black;
            //this.TransparencyKey = System.Drawing.Color.Lime;

            this.window = window;
            this.ShowInTaskbar = false;

        }

        private void FormPin_Load(object sender, EventArgs e)
        {
            this.SetStartPosition();
            this.makeRoundy();

            if (this.window.transparent)
            {
                this.Opacity = 0.8;
            }
            else {
                this.Opacity = 1.0;
            }


            dobleClickToActivateToolStripMenuItem.Checked = this.window.doubleClickCommand;
        }

        public void Center() {
            Screen currentScreen = Screen.FromPoint(Cursor.Position);
            Rectangle screenBounds = currentScreen.WorkingArea;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(
                screenBounds.Left + (screenBounds.Width - this.Width) / 2,
                screenBounds.Top + (screenBounds.Height - this.Height) / 2
            );
        }

        public void SetStartPosition()
        {
            this.Left = this.StartLeft;
            this.Top = this.StartTop;
            this.Width = this.StartWidth;
            this.Height = this.StartHeight;
        }

        public void makeRoundy() {
            // Set the radius for the rounded corners
            int radius = 15;

            GraphicsPath path = new GraphicsPath();

            // Define the rounded rectangle
            path.AddArc(0, 0, radius, radius, 180, 90); // Top-left corner
            path.AddArc(this.Width - radius, 0, radius, radius, 270, 90); // Top-right corner
            path.AddArc(this.Width - radius, this.Height - radius, radius, radius, 0, 90); // Bottom-right corner
            path.AddArc(0, this.Height - radius, radius, radius, 90, 90); // Bottom-left corner
            path.CloseAllFigures();

            this.Region = new Region(path);
        }

        public void makeSquery()
        {

            this.Region = null;
        }

        public void CloseForm() {
            Program.pins.Remove(this);
            Program.Update();
            this.Close();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CloseForm();
        }

        // Variables to track mouse movement
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void FormPin_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = false;

                if (dragCursorPoint == Cursor.Position && !this.window.doubleClickCommand) {
                    this.FormPin_MouseDoubleClick(sender, e);
                }
            }
        }

        private void FormPin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = this.Location;
            }
        }

        private void FormPin_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(diff));
                Program.Update();
            }
        }

        private void FormPin_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.window.isDesktop)
            {
                SystemTools.ShowDesktop();
            }
            else if (this.window.Type == "COMMAND" && this.window.command != null && this.window.command.Trim() != "")
            {
                // Task.RunCommand(this.window.command, null, this.window.silentCommand);
                Task.RunPowerShellCommand(this.window.command, null, this.window.silentCommand);
            }
            else if (this.window.Type == "WINDOW")
            {
                if (this.window.Handle != IntPtr.Zero && ToolsWindow.IsWindowValid(this.window)) {
                    ToolsWindow.BringWindowToFront(this.window);
                }
                else if (this.window.app != null && this.window.app.Trim() != "")
                {
                    bool foundWindow = false;
                    List<Window> taskbarWindows = ToolsWindow.GetTaskbarWindows();
                    foreach (Window win in taskbarWindows)
                    {
                        if (this.window.Title == win.Title) {
                            this.window.Handle = win.Handle;
                            foundWindow = true;
                            break;
                        }
                    }

                    if (!foundWindow) {
                        foreach (Window win in taskbarWindows)
                        {
                            if (this.window.app == win.app)
                            {
                                this.window.Handle = win.Handle;
                                foundWindow = true;
                                break;
                            }
                        }
                    }

                    if (foundWindow) {
                        ToolsWindow.BringWindowToFront(this.window);
                    } else {
                        if (this.window.app != null && this.window.app.Trim() != ""){
                            Task.RunCommand(this.window.app, null, this.window.silentCommand);
                        }
                    }
                }
            }
            
            
        }

        // Import native methods for resizing
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool ReleaseCapture();

        // Constants for resizing
        private const int WM_NCHITTEST = 0x84;
        private const int HTBOTTOMRIGHT = 17;

        protected override void WndProc(ref Message m)
        {
            
            if (m.Msg == WM_NCHITTEST)
            {
                // Get mouse position relative to the form
                Point pos = PointToClient(Cursor.Position);
                Size size = this.ClientSize;

                // If mouse is near the bottom-right corner, trigger resize
                if (pos.X >= size.Width - 10 && pos.Y >= size.Height - 10)
                {
                    this.makeSquery();
                    m.Result = (IntPtr)HTBOTTOMRIGHT;
                    return;
                }
            }
            base.WndProc(ref m);
        }

        private void FormPin_Resize(object sender, EventArgs e)
        {
            int newSize = Math.Min(this.Width, this.Height);
            this.Size = new Size(newSize, newSize);
            Program.Update();
            this.Refresh();
        }

        private void FormPin_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Color startColor = Color.FromArgb(255,50,50,50);
            Color endColor = Color.FromArgb(255, 30, 30, 30);

            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);

            using (LinearGradientBrush brush = new LinearGradientBrush(rect, startColor, endColor, LinearGradientMode.Vertical))
            {
                g.FillRectangle(brush, rect);

            }

            if (this.window.icon != null)
            {
                e.Graphics.DrawImage(this.window.icon,new Rectangle(0,0,this.Width,this.Height));
            }
        }

        private void FormPin_ResizeBegin(object sender, EventArgs e)
        {
            this.makeSquery();
        }

        private void FormPin_ResizeEnd(object sender, EventArgs e)
        {
            this.makeRoundy();
        }

        private void setCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCommand formCommand = new FormCommand(this.window);
            formCommand.ShowDialog();
        }

        private void setIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string selectedFilePath = openFileDialog1.FileName;
                    using (var image = Image.FromFile(selectedFilePath))
                    {
                        this.window.icon = new Bitmap(image);
                        this.Refresh();
                    }

                    Program.Update();
                }
                catch (Exception ex)
                {
                    Program.error("Open image from file error: " + ex.Message);
                }
            }
        }

        private void minimalizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolsWindow.MinimizeWindow(this.window);
        }

        private void closeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolsWindow.CloseWindow(this.window);
        }

        private void dobleClickToActivateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.window.doubleClickCommand = !this.window.doubleClickCommand;
            dobleClickToActivateToolStripMenuItem.Checked = this.window.doubleClickCommand;
            Program.Update();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            dobleClickToActivateToolStripMenuItem.Checked = this.window.doubleClickCommand;
            transparentToolStripMenuItem.Checked = this.window.transparent;
        }

        private void newPinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.AddEmptyPin();
        }

        private void closeToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newWidgetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.AddEmptyWidget();
        }

        private void transparentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.window.transparent = !this.window.transparent;
            transparentToolStripMenuItem.Checked = this.window.transparent;

            if (this.window.transparent)
            {
                this.Opacity = 0.8;
            }
            else
            {
                this.Opacity = 1.0;
            }
        }
    }
}
