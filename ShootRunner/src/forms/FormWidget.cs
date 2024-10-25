using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShootRunner
{
    public partial class FormWidget : Form
    {
        public string Type = "";
        public int StartLeft = 100;
        public int StartTop = 100;
        public int StartWidth = 300;
        public int StartHeight = 300;
        public bool locked = false;
        public bool transparent = false;

        

        public void Center()
        {
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

        public FormWidget()
        {
            InitializeComponent();
        }

        private void FormWidget_Load(object sender, EventArgs e)
        {

            if (this.transparent)
            {
                this.Opacity = 0.8;
            }
            else
            {
                this.Opacity = 1.0;
            }

            this.MinimumSize = new Size(64, 64);
            this.SetStartPosition();
            this.ShowInTaskbar = false;

            this.Deactivate += new EventHandler(this.Form_Deactivate);
            this.Activated += new EventHandler(this.Form_Activated);
        }

        public void CloseForm()
        {
            Program.widgets.Remove(this);
            Program.Update();
        }

        // Import user32.dll to access window style methods
        [DllImport("user32.dll", SetLastError = true)]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        // Constants for window style
        const int GWL_STYLE = -16;
        const int WS_CAPTION = 0x00C00000;

        // Event handler when the form loses focus
        private void Form_Deactivate(object sender, EventArgs e)
        {
            RemoveTitleBar();
        }

        // Event handler when the form gains focus
        private void Form_Activated(object sender, EventArgs e)
        {
            AddTitleBar();
        }

        // Method to remove the title bar
        private void RemoveTitleBar()
        {
            int style = GetWindowLong(this.Handle, GWL_STYLE);
            SetWindowLong(this.Handle, GWL_STYLE, style & ~WS_CAPTION);
            this.Refresh();  // Redraw the window to apply changes
        }

        // Method to add the title bar back
        private void AddTitleBar()
        {
            int style = GetWindowLong(this.Handle, GWL_STYLE);
            SetWindowLong(this.Handle, GWL_STYLE, style | WS_CAPTION);
            this.Refresh();  // Redraw the window to apply changes
        }

        // Windows message constants
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        protected override void WndProc(ref Message m)
        {
            
            if (this.locked && ( m.Msg == WM_NCLBUTTONDOWN && m.WParam.ToInt32() == HTCAPTION))
            {
                return; 
            }

            base.WndProc(ref m);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            mostTopToolStripMenuItem.Checked = this.TopMost;
            lockedToolStripMenuItem.Checked = this.locked;
            transparentToolStripMenuItem.Checked = this.transparent;
        }

        private void transparentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.transparent = !this.transparent;
            transparentToolStripMenuItem.Checked = this.transparent;

            if (this.transparent)
            {
                this.Opacity = 0.8;
            }
            else
            {
                this.Opacity = 1.0;
            }
        }

        private void removeWidgetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newWidgetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.AddEmptyWidget();
        }

        private void mostTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
            mostTopToolStripMenuItem.Checked = this.TopMost;
        }

        private void lockedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.locked = !this.locked;
            lockedToolStripMenuItem.Checked = this.locked;
        }

        private void FormWidget_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseForm();
        }

        private void FormWidget_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
