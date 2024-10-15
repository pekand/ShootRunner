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
        private Window window = null;

        public FormPin(Window window)
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.MinimumSize = new Size(32, 32);
            this.Size = new Size(64, 64);            
            this.BackColor = Color.White;
            this.TopMost = true;

            this.Opacity = 0.8;  // 80% opaque, 20% transparent
            this.BackColor = System.Drawing.Color.Black;
            //this.TransparencyKey = System.Drawing.Color.Lime;


            this.window = window;
            this.ShowInTaskbar = false;
        }

        private void FormPin_Load(object sender, EventArgs e)
        {
            Screen currentScreen = Screen.FromPoint(Cursor.Position);
            Rectangle screenBounds = currentScreen.WorkingArea;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(
                screenBounds.Left + (screenBounds.Width - this.Width) / 2,
                screenBounds.Top + (screenBounds.Height - this.Height) / 2
            );

            this.makeRoundy();
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

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
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

                if (dragCursorPoint == Cursor.Position) {
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
            }
        }

        private void FormPin_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.window.isDesktop)
            {
                SystemTools.ShowDesktop();
            } else if (ToolsWindow.IsWindowValid(this.window))
            {
                ToolsWindow.BringWindowToFront(this.window);
            }
            else { 
                this.Close();
            }
        }

        private Point lastMousePos;

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
                e.Graphics.DrawIcon(this.window.icon,new Rectangle(0,0,this.Width,this.Height));
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

        
    }
}
