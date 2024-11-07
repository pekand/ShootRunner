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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

#nullable disable


namespace ShootRunner
{
    public partial class FormTaskbar : Form
    {
        public Widget widget = null;
        public List<Window> windows = new List<Window>();
        public List<Window> taskbarWindows = new List<Window>();
        Window selectedWindow = null;

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public int StartX = 20;
        public int StartY = 20;
        public int IconWidth = 32;
        public int IconHeight = 32;
        public int Space = 20;

        public FormTaskbar(Widget widget)
        {
            this.DoubleBuffered = true;
            this.widget = widget;
            InitializeComponent();
        }

        private void FormTaskbar_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.MinimumSize = new Size(32, 32);
            this.BackColor = Color.White;
            this.TopMost = this.widget.mosttop;
            this.Opacity = this.widget.transparent < 0.2 ? 0.2 : this.widget.transparent;

            this.BackColor = System.Drawing.Color.Black;
            this.ShowInTaskbar = false;
            this.Center();

            InitList();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_TOOLWINDOW = 0x00000080;
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_TOOLWINDOW; // Add the tool window style
                return cp;
            }
        }

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
            this.Left = this.widget.StartLeft;
            this.Top = this.widget.StartTop;
            this.Width = this.widget.StartWidth;
            this.Height = this.widget.StartHeight;
        }        

        public void InitList() {

            windows = ToolsWindow.GetTaskbarWindows();
            
            foreach (var win in windows)
            {
                try
                {
                    if (win.Handle == this.Handle) { 
                        continue;
                    }

                    ToolsWindow.SetWindowData(win);
                    if (win.icon == null)
                    {
                        continue;
                    }

                    taskbarWindows.Add(win);                    
                }
                catch (Exception ex)
                {
                    Program.error(ex.Message);

                }
                
            }             

        }

        public void UpdateList()
        {
            bool changed = false;

            List<Window> newWindows = ToolsWindow.GetTaskbarWindows();

            foreach (var newWin in newWindows)
            {

                if (newWin.Handle == this.Handle)
                {
                    continue;
                }

                bool found = false;

                foreach (var win in windows)
                {
                    if (newWin.Handle == win.Handle) { 
                        found = true;
                        break;
                    }
                }

                if (found) {
                    continue;
                }

                try
                {
                    ToolsWindow.SetWindowData(newWin);
                    if (newWin.icon == null)
                    {
                        continue;
                    }

                    taskbarWindows.Add(newWin);
                    windows.Add(newWin);
                    changed = true;
                }
                catch (Exception ex)
                {
                    Program.error(ex.Message);

                }

            }

            List<Window> toremove = new List<Window>();

            
           foreach (var win in windows)
           {
                bool found = false;

                foreach (var newWin in newWindows)
                {
                    if (newWin.Handle == win.Handle)
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    continue;
                }

                toremove.Add(win);
                changed = true;
            }

            foreach (var win in toremove)
            {
                windows.Remove(win);

                List<Window> listToremove = new List<Window>();

                bool found = false;

                foreach (Window item in taskbarWindows)
                {
                    if (item == win)
                    {
                        listToremove.Add(item);
                        break;
                    }
                }

                foreach (Window item in listToremove)
                {
                    taskbarWindows.Remove(item);
                }
  
            }

            if (changed) { 
                this.Refresh();
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            /*if (listView1.SelectedItems.Count > 0)
            {                
                ListViewItem selectedItem = listView1.SelectedItems[0];
                Window window = (Window)selectedItem.Tag;
                ToolsWindow.BringWindowToFront(window);
            
            }*/
        }

        private const int GWL_STYLE = -16;
        private const int WS_CAPTION = 0x00C00000;
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        [DllImport("user32.dll", SetLastError = true)]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        private void RemoveTitleBar()
        {
            /*initTop = this.Top;
            initHeight = this.Height;
            this.FormBorderStyle = FormBorderStyle.None;

            this.Top = initTop + initialCaptionHeight;
            this.Height = initHeight - initialCaptionHeight;*/


            int style = GetWindowLong(this.Handle, GWL_STYLE);
            SetWindowLong(this.Handle, GWL_STYLE, style & ~WS_CAPTION);
            this.Refresh();

            //titleBarHeight = SystemInformation.CaptionHeight;
            //ToggleBorder(FormBorderStyle.None);


        }

        private void AddTitleBar()
        {

            /*this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Top = initTop;
            this.Height = initHeight;*/

            int style = GetWindowLong(this.Handle, GWL_STYLE);
            SetWindowLong(this.Handle, GWL_STYLE, style | WS_CAPTION);
            this.Refresh();

            //ToggleBorder(FormBorderStyle.Sizable);
        }

        // Constants for resizing
        private const int WM_NCHITTEST = 0x84;

        private const int HTBOTTOMRIGHT = 17;
        protected override void WndProc(ref Message m)
        {
            if (!this.widget.locked && m.Msg == WM_NCHITTEST)
            {
                // Get mouse position relative to the form
                Point pos = PointToClient(Cursor.Position);
                Size size = this.ClientSize;

                // If mouse is near the bottom-right corner, trigger resize
                if (pos.X >= size.Width - 10 && pos.Y >= size.Height - 10)
                {
                    m.Result = (IntPtr)HTBOTTOMRIGHT;
                    return;
                }
            }

            if (this.widget.locked && (m.Msg == WM_NCLBUTTONDOWN && m.WParam.ToInt32() == HTCAPTION))
            {
                return;
            }

            base.WndProc(ref m);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////

        private void Form_Deactivate(object sender, EventArgs e)
        {
            //RemoveTitleBar();
        }

        private void Form_Activated(object sender, EventArgs e)
        {
            //AddTitleBar();
        }

        private void FormTaskbar_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseForm();
        }

        public void CloseForm()
        {
            Program.widgetManager.RemoveTaskbarWidget(this);
            Program.Update();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            mostTopToolStripMenuItem.Checked = this.TopMost;
            lockToolStripMenuItem.Checked = this.widget.locked;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private void mostTopToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.TopMost = !this.TopMost;
            mostTopToolStripMenuItem.Checked = this.TopMost;
            this.widget.mosttop = this.TopMost;
        }

        private void lockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.widget.locked = !this.widget.locked;
            lockToolStripMenuItem.Checked = this.widget.locked;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
        private void FormTaskbar_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            int X = StartX;
            int Y = StartY;
            int W = IconWidth;
            int H = IconHeight;
            int S = Space;

            try
            {
                foreach (Window window in taskbarWindows)
                {
                    if (window.icon != null)
                    {
                        e.Graphics.DrawImage(window.icon, new Rectangle(X, Y, W, H));
                        X += W + S;

                        if (X + W > this.Width) {
                            X = StartX;
                            Y += H + S;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Program.error(ex.Message);
            }
            
        }

        private void FormTaskbar_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void FormTaskbar_MouseUp(object sender, MouseEventArgs e)
        {

            if (!this.widget.locked && dragging && e.Button == MouseButtons.Left)
            {
                dragging = false;                
            }

            if (!dragging && e.Button == MouseButtons.Left) {
                int X = StartX;
                int Y = StartY;
                int W = IconWidth;
                int H = IconHeight;
                int S = Space;

                try
                {
                    foreach (Window window in taskbarWindows)
                    {
                        if (window.icon != null)
                        {
                            if (X <= e.X && e.X <= (X + W) && Y <= e.Y && e.Y <= (Y + H))
                            {
                                this.selectedWindow = window;
                                ToolsWindow.BringWindowToFront(selectedWindow);
                                return;
                            }

                            X += W + S;

                            if (X + W > this.Width)
                            {
                                X = StartX;
                                Y += H + S;
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

        private void FormTaskbar_MouseDown(object sender, MouseEventArgs e)
        {
            if (!this.widget.locked && e.Y < 10 && e.Button == MouseButtons.Left)
            {
                dragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = this.Location;
            }
        }

        private void FormTaskbar_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.widget.locked && dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(diff));
                Program.Update();
            }
        }

    }
}
