using ShootRunner.src.forms;
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
        public int IconWidth = 64;
        public int IconHeight = 64;
        public int Space = 20;

        Window lastWindow = null;

        public FormTaskbar(Widget widget)
        {
            this.DoubleBuffered = true;
            this.widget = widget;
            InitializeComponent();
        }

        private void FormTaskbar_Load(object sender, EventArgs e)
        {
            this.TopMost = this.widget.mosttop;
            this.Opacity = this.widget.transparent < 0.2 ? 0.2 : this.widget.transparent;
            this.ShowInTaskbar = false;
            this.BackColor = this.widget.backgroundColor;

            SwitchIconType();

            InitList();

            this.SetStartPosition();
        }

        public void SwitchIconType()
        {
            if (widget.useScreenshots)
            {
                if (widget.useBigIcons)
                {
                    IconWidth = 128;
                    IconHeight = 128;
                }
                else
                {
                    IconWidth = 64;
                    IconHeight = 64;
                }
            }
            else
            {
                if (widget.useBigIcons)
                {
                    IconWidth = 64;
                    IconHeight = 64;
                }
                else
                {
                    IconWidth = 32;
                    IconHeight = 32;
                }
            }
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
            this.Resize -= FormTaskbar_Resize;
            this.Move -= FormTaskbar_Move;
            this.Left = this.widget.StartLeft;
            this.Top = this.widget.StartTop;
            this.Width = this.widget.StartWidth;
            this.Height = this.widget.StartHeight;
            this.BackColor = this.widget.backgroundColor;
            this.Resize += FormTaskbar_Resize;
            this.Move += FormTaskbar_Move;
        }

        public void InitList()
        {

            windows = ToolsWindow.GetTaskbarWindows();

            foreach (var win in windows)
            {
                try
                {
                    if (win.Handle == this.Handle)
                    {
                        continue;
                    }

                    ToolsWindow.SetWindowData(win, true);
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

            Window currentWindow = ToolsWindow.GetCurrentWindow();

            if (widget.useScreenshots)
            {
                if (lastWindow == null && currentWindow != null)
                {
                    foreach (var win in taskbarWindows)
                    {
                        if (win.Handle == currentWindow.Handle)
                        {
                            lastWindow = win;
                            break;
                        }
                    }
                }

                if (currentWindow != null && lastWindow != null && currentWindow.Handle != lastWindow.Handle)
                {
                    foreach (var win in taskbarWindows)
                    {
                        if (win.Handle == currentWindow.Handle)
                        {
                            if (!ToolsWindow.IsMinimalized(lastWindow))
                            {
                                Bitmap screenshot = WindowScreenshot.CaptureWindow(lastWindow, 256, 256);
                                if (screenshot != null)
                                {
                                    lastWindow.screenshot = screenshot;
                                    changed = true;
                                }
                            }
                            lastWindow = win;
                            break;
                        }
                    }

                }
            }


            if (changed)
            {
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

        public void CloseForm()
        {
            Program.widgetManager.RemoveTaskbarWidget(this);
            Program.Update();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
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
                        if (widget.useScreenshots && window.screenshot != null)
                        {
                            e.Graphics.DrawImage(window.screenshot, new Rectangle(X, Y, W, H));

                            if (window.icon != null)
                            {
                                e.Graphics.DrawImage(window.icon, new Rectangle(X + W - W / 2, Y + H - H / 2, W / 2, H / 2));
                            }
                        }
                        else if (window.icon != null)
                        {

                            e.Graphics.DrawImage(window.icon, new Rectangle(X, Y, W, H));
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


        public Window GetWindowOnposition(int eX, int eY)
        {

            Window windowOnPosition = null;

            int X = StartX;
            int Y = StartY;
            int W = IconWidth;
            int H = IconHeight;
            int S = Space;

            foreach (Window window in taskbarWindows)
            {
                if (window.icon != null)
                {
                    if (X <= eX && eX <= (X + W) && Y <= eY && eY <= (Y + H))
                    {
                        windowOnPosition = window;
                        break;
                    }

                    X += W + S;

                    if (X + W > this.Width)
                    {
                        X = StartX;
                        Y += H + S;
                    }
                }

            }

            return windowOnPosition;
        }

        public int GetSpaceOnposition(int eX, int eY)
        {

            int spaceOnPosition = 0;

            int W = IconWidth;
            int H = IconHeight;
            int S = Space;


            int rX1 = 0;
            int rY1 = 0;
            int rX2 = StartX + IconWidth;
            int rY2 = StartY + IconHeight;

            int nX1 = rX1 + S + W;
            int nY1 = rY1;
            int nX2 = rX2 + S + W;
            int nY2 = rY2;

            int pos = 0;

            foreach (Window window in taskbarWindows)
            {
                if (rX1 <= eX && eX <= rX2 && rY1 <= eY && eY <= rY2)
                {
                    spaceOnPosition = pos;

                    break;
                }

                if (nX1 <= eX && eX <= nX2 && nY1 <= eY && eY <= nY2)
                {
                    spaceOnPosition = pos + 1;

                    break;
                }

                rX1 += S + W;
                rX2 += S + W;

                nX1 += S + W;
                nX2 += S + W;

                if (rX1 + S + W > this.Width)
                {
                    rX1 = 0;
                    rY1 += S + H;
                    rX2 = S + W;
                    rY2 += S + H;

                    nX1 = rX1 + S + W;
                    nY1 += S + H;
                    nX2 = rX2 + S + W;
                    nY2 += S + H;
                }


                pos++;
            }

            return spaceOnPosition;
        }

        public bool draggingItem = false;
        public Window draggingWindow = null;
        public int mouseDownPos = 0;
        public int mouseDownX = 0;
        public int mouseDownY = 0;
        public int mouseDownWindow = 0;
        private FormGhost ghostForm;

        private void FormTaskbar_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownX = e.X;
            mouseDownY = e.Y;

            if (!this.widget.locked && e.Y < 10 && e.Button == MouseButtons.Left)
            {
                dragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = this.Location;
            }
            else if (!dragging && e.Button == MouseButtons.Left)
            {
                draggingWindow = GetWindowOnposition(e.X, e.Y);
                if (draggingWindow != null)
                {
                    draggingItem = true;
                    mouseDownPos = GetSpaceOnposition(e.X, e.Y);
                }

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
            else if (draggingItem && this.Cursor != Cursors.Hand &&
                (Math.Abs(mouseDownX - e.X) > 10 || Math.Abs(mouseDownY - e.Y) > 10))
            {
                Cursor = Cursors.Hand;

                if (draggingWindow != null)
                {
                    Bitmap pic = draggingWindow.screenshot;
                    if (pic == null)
                    {
                        pic = draggingWindow.icon;
                    }

                    if (pic != null)
                    {
                        ghostForm = new FormGhost(pic);
                        Point screenPosition = Control.MousePosition;
                        ghostForm.Location = new Point(screenPosition.X + 10, screenPosition.Y + 10);
                        ghostForm.Show();


                    }
                }
            }

            if (draggingItem && ghostForm != null)
            {
                Point screenPosition = Control.MousePosition;
                ghostForm.Location = new Point(screenPosition.X + 10, screenPosition.Y + 10);

            }
        }

        private void FormTaskbar_MouseUp(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                dragging = false;
            }
            else if (draggingItem && e.Button == MouseButtons.Left)
            {
                int mouseDownUpPos = GetSpaceOnposition(e.X, e.Y);

                if (mouseDownPos != mouseDownUpPos)
                {
                    draggingItem = true;
                    ListMove.MoveItem(this.taskbarWindows, mouseDownPos, mouseDownUpPos);
                    this.Refresh();
                }
                else
                {
                    draggingItem = false;
                }
            }


            Window onWindow = this.GetWindowOnposition(e.X, e.Y);

            this.selectedWindow = null;

            if (onWindow != null)
            {
                this.selectedWindow = onWindow;

                if (!draggingItem && e.Button == MouseButtons.Left)
                {
                    ToolsWindow.BringWindowToFront(selectedWindow);
                }

                if (e.Button == MouseButtons.Middle)
                {
                    ToolsWindow.MinimizeWindow(onWindow);
                }

                if (e.Button == MouseButtons.Right)
                {

                }
            }

            if (dragging || draggingItem || Cursor != Cursors.Default)
            {
                dragging = false;
                draggingItem = false;
                Cursor = Cursors.Default;
            }

            if (ghostForm != null)
            {
                ghostForm?.Close();
                ghostForm = null;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            mostTopToolStripMenuItem.Checked = this.TopMost;
            lockToolStripMenuItem.Checked = this.widget.locked;
            useScreenshotsToolStripMenuItem.Checked = widget.useScreenshots;
            useBigIconsToolStripMenuItem.Checked = widget.useBigIcons;
        }

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
            Program.Exit();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CloseForm();
            this.Close();
        }

        private void windowToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void minimalizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.selectedWindow != null)
            {
                ToolsWindow.MinimizeWindow(this.selectedWindow);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.selectedWindow != null)
            {
                ToolsWindow.CloseWindow(this.selectedWindow);
            }
        }

        private void useScreenshotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            widget.useScreenshots = !widget.useScreenshots;
            useScreenshotsToolStripMenuItem.Checked = widget.useScreenshots;
            SwitchIconType();
            this.Refresh();
        }

        private void useBigIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            widget.useBigIcons = !widget.useBigIcons;
            useBigIconsToolStripMenuItem.Checked = widget.useScreenshots;
            SwitchIconType();
            this.Refresh();
        }

        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    Color selectedColor = colorDialog.Color;
                    this.widget.backgroundColor = selectedColor;
                    this.BackColor = selectedColor;
                    this.Refresh();
                }
            }
        }
        private void FormTaskbar_Resize(object sender, EventArgs e)
        {
            this.widget.StartLeft = this.Left;
            this.widget.StartTop = this.Top;
            this.widget.StartWidth = this.Width;
            this.widget.StartHeight = this.Height;
            Program.Update();
            this.Refresh();
        }

        private void FormTaskbar_Move(object sender, EventArgs e)
        {
            this.widget.StartLeft = this.Left;
            this.widget.StartTop = this.Top;
            this.widget.StartWidth = this.Width;
            this.widget.StartHeight = this.Height;
            Program.Update();
            this.Refresh();
        }
    }
}
