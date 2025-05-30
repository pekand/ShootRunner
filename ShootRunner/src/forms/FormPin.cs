﻿using Microsoft.CodeAnalysis.Operations;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

#nullable disable


namespace ShootRunner
{
    public partial class FormPin : Form
    {
        public int StartLeft = 100;
        public int StartTop = 100;
        public int StartWidth = 64;
        public int StartHeight = 64;

        public Window window = null;

        List<IntPtr> taskbarWindows = null;

        // Variables to track mouse movement
        public bool dragging = false;
        public Point dragCursorPoint;
        public Point dragFormPoint;

        public bool selected = false;

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
            this.AllowDrop = true;
        }

        private void FormPin_Load(object sender, EventArgs e)
        {
            this.SetStartPosition();
            this.makeRoundy();
            this.Opacity = this.window.transparent < 0.2 ? 0.2 : this.window.transparent;
            dobleClickToActivateToolStripMenuItem.Checked = this.window.doubleClickCommand;
        }

        public void CloseForm()
        {
            Program.RemoveFormFromSelected(this);
            Program.pins.Remove(this);
            Program.Update();
            this.Close();
        }

        protected override void WndProc(ref Message m)
        {

            if (m.Msg == ToolsWindow.WM_NCHITTEST)
            {
                Point pos = PointToClient(Cursor.Position);
                Size size = this.ClientSize;

                // RESIZE RIGHT BOTTOM CORNER
                if (pos.X >= size.Width - 10 && pos.Y >= size.Height - 10)
                {
                    this.makeSquery();
                    m.Result = (IntPtr)ToolsWindow.HTBOTTOMRIGHT;
                    return;
                }
            }
            base.WndProc(ref m);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_TOOLWINDOW = 0x00000080;
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_TOOLWINDOW;
                return cp;
            }
        }

        public async void DoPinAction()
        {
            if (this.window.isDesktop)
            {
                ToolsWindow.ShowDesktop();
            }
            else if (this.window.Type == "COMMAND" && this.window.command != null && this.window.command.Trim() != "")
            {
                if (this.window.matchNewWindow && this.window.Handle != IntPtr.Zero && ToolsWindow.IsWindowValid(this.window))
                {
                    ToolsWindow.BringWindowToFront(this.window);
                }
                else
                {

                    JobTask.RunPowerShellCommand(window);
                }
            }
            else if (this.window.Type == "WINDOW")
            {
                if (this.window.Handle != IntPtr.Zero && ToolsWindow.IsWindowValid(this.window))
                {
                    ToolsWindow.BringWindowToFront(this.window);
                }
                else if (this.window.app != null && this.window.app.Trim() != "")
                {
                    this.window.Handle = IntPtr.Zero;

                    bool foundWindow = false;
                    List<IntPtr> taskbarWindows = ToolsWindow.GetTaskbarWindows();
                    foreach (IntPtr Handle in taskbarWindows)
                    {
                        if (this.window.Title == ToolsWindow.GetWindowTitle(Handle))
                        {
                            this.window.Handle = Handle;
                            foundWindow = true;
                            break;
                        }
                    }

                    if (!foundWindow)
                    {
                        foreach (IntPtr Handle in taskbarWindows)
                        {
                            if (this.window.app != null && this.window.app == ToolsWindow.GetApplicationPathFromWindow(Handle))
                            {
                                this.window.Handle = Handle;
                                foundWindow = true;
                                break;
                            }
                        }
                    }

                    if (foundWindow)
                    {
                        ToolsWindow.BringWindowToFront(this.window);
                    }
                    else
                    {
                        if (this.window.app != null && this.window.app.Trim() != "")
                        {
                            Window window = await JobTask.StartProcessAndGetWindowHandleAsync(this.window.app, null, null, false, true);

                            if (window != null && window.Handle != IntPtr.Zero)
                            {
                                this.window.Handle = window.Handle;
                            }
                        }
                    }
                }
            }
        }

        /*************************************************************************/

        private void FormPin_MouseUp(object sender, MouseEventArgs e)
        {
            if (dragging && e.Button == MouseButtons.Left)
            {
                dragging = false;

                if (dragCursorPoint == Cursor.Position && !this.window.doubleClickCommand)
                {
                    this.DoPinAction();
                }

                if (dragCursorPoint != Cursor.Position)
                {
                    Program.Update();
                }
            }

            if (e.Button == MouseButtons.Middle)
            {
                if (this.window.isDesktop)
                {
                    ToolsWindow.ShowDesktop(true);
                }
                else if (this.window.Type == "WINDOW" && this.window.Handle != IntPtr.Zero && ToolsWindow.IsWindowValid(this.window))
                {

                    ToolsWindow.MinimizeWindow(this.window);
                }
                else if (this.window.Type == "COMMAND" && this.window.matchNewWindow && this.window.Handle != IntPtr.Zero && ToolsWindow.IsWindowValid(this.window))
                {

                    ToolsWindow.MinimizeWindow(this.window);
                }
            }
        }


        public void SetDragStartLocation()
        {
            dragFormPoint = this.Location;
            dragCursorPoint = Cursor.Position;
        }

        public void SetDragNewLocation()
        {
            Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
            Point sum = Point.Add(dragFormPoint, new Size(diff));
            this.Location = sum;
        }

        public void UnselectPin()
        {
            selected = false;
            this.Refresh();
        }

        public void SelectPin()
        {
            selected = true;
            this.Refresh();
        }

        private void FormPin_MouseDown(object sender, MouseEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift && e.Button == MouseButtons.Left)
            {
                selected = !selected;
                if (selected)
                {
                    Program.AddFormToSelected(this);
                }
                else
                {
                    Program.RemoveFormFromSelected(this);
                }
                this.Refresh();
            }
            else if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = this.Location;
                if (selected)
                {
                    Program.SetDragStartToSelectedForms(this);
                }
            }
        }

        private void FormPin_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                Point sum = Point.Add(dragFormPoint, new Size(diff));
                this.Location = sum;
                if (this.selected)
                {
                    Program.SetDragSelectedFormsPosition(this);
                }
            }
        }

        private void FormPin_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.window.doubleClickCommand)
                {
                    this.DoPinAction();
                }
            }
            catch (Exception ex)
            {
                Program.error(ex.Message);

            }
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

            Color startColor = Color.FromArgb(255, 50, 50, 50);
            Color endColor = Color.FromArgb(255, 30, 30, 30);

            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);

            using (LinearGradientBrush brush = new LinearGradientBrush(rect, startColor, endColor, LinearGradientMode.Vertical))
            {
                g.FillRectangle(brush, rect);

            }

            if (this.window.customicon != null)
            {
                e.Graphics.DrawImage(this.window.customicon, new Rectangle(0, 0, this.Width, this.Height));
            }
            else if (this.window.icon != null)
            {
                e.Graphics.DrawImage(this.window.icon, new Rectangle(0, 0, this.Width, this.Height));
            }

            using var pen = new Pen(Color.LightGray, 5);
            if (selected)
            {

                e.Graphics.DrawRectangle(pen, rect);
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

        /*********************************************************************************/

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            dobleClickToActivateToolStripMenuItem.Checked = this.window.doubleClickCommand;
            AddWindowsToContextMenu();
        }

        public void AddWindowsToContextMenu()
        {
            this.taskbarWindows = ToolsWindow.GetTaskbarWindows();

            selectToolStripMenuItem.DropDownItems.Clear();
            foreach (IntPtr Handle in this.taskbarWindows)
            {
                Window window = new Window();
                window.Handle = Handle;
                ToolsWindow.SetWindowData(window);
                ToolStripMenuItem item = new ToolStripMenuItem(window.Title);
                item.Image = window.icon;
                item.Click += (sender, e) => SelectType(window);
                selectToolStripMenuItem.DropDownItems.Add(item);
            }
        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* select window from oppened windows, is filled with code */
        }

        public void SelectType(Window window)
        {
            ToolsWindow.SetWindowData(window);

            if (window == null)
            {
                return;
            }

            window.customicon = (Bitmap)this.window.customicon?.Clone();
            this.window.Dispose();
            this.window = window;

            this.Refresh();

        }

        private void setCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCommand formCommand = new FormCommand(this.window);
            formCommand.Show();
        }

        private void setIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string selectedFilePath = openFileDialog.FileName;
                    using (var image = Image.FromFile(selectedFilePath))
                    {
                        if (this.window.customicon != null)
                        {
                            this.window.customicon.Dispose();
                        }
                        this.window.customicon = new Bitmap(image);
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

        private void newPinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.AddEmptyPin();
        }

        private void closeToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            Program.Exit();
        }

        private void newWidgetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.widgetManager.AddEmptyWidget();
        }

        private void transparentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTransparent form = new FormTransparent(null, this);
            form.trackBar1.Value = (int)(this.Opacity * 100);
            form.Show();
            this.window.transparent = this.Opacity;
        }

        private void selectWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.CloseForm();
        }

        private void dobleClickToActivateToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.window.doubleClickCommand = !this.window.doubleClickCommand;
            dobleClickToActivateToolStripMenuItem.Checked = this.window.doubleClickCommand;
            Program.Update();
        }

        private async void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.window.Type == "COMMAND")
            {
                JobTask.RunPowerShellCommand(this.window.command, null, this.window.silentCommand);
            }

            if (this.window.Type == "WINDOW")
            {
                if (this.window.app != null && this.window.app.Trim() != "")
                {
                    Window window = await JobTask.StartProcessAndGetWindowHandleAsync(this.window.app, null, null, this.window.silentCommand);

                    if (window != null && window.Handle != IntPtr.Zero)
                    {
                        ToolsWindow.SetWindowData(window);
                        Program.CreatePin(window);
                    }
                }
            }
        }

        private void minimalizeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (this.window.Type == "WINDOW" && this.window.Handle != IntPtr.Zero)
                {
                    ToolsWindow.MinimizeWindow(this.window);
                }
            }
            catch (Exception ex)
            {
                Program.error(ex.Message);
            }

        }

        private void taskbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.widgetManager.ShowTaskbarWidget(null);
        }

        /*********************************************************************************/

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

        public void makeRoundy()
        {
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

        private void consoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.ShowConsole();
        }

        private void FormPin_DragEnter(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop) ||
                e.Data.GetDataPresent(DataFormats.Text) ||
                e.Data.GetDataPresent(DataFormats.UnicodeText) ||
                e.Data.GetDataPresent(DataFormats.Bitmap)
                )
            {
                e.Effect = DragDropEffects.Copy;

                if (this.window != null)
                {
                    if (this.window.Type == "COMMAND" && this.window.command != null && this.window.command.Trim() != "")
                    {
                        if (this.window.matchNewWindow && this.window.Handle != IntPtr.Zero && ToolsWindow.IsWindowValid(this.window))
                        {
                            IntPtr currentWindow = ToolsWindow.GetCurrentWindow();
                            if (currentWindow != IntPtr.Zero && this.window.Handle != currentWindow)
                            {
                                Program.info("front");
                                ToolsWindow.BringWindowToFront(this.window);
                            }

                        }
                    }
                    else if (this.window.Type == "WINDOW")
                    {
                        if (this.window.Handle != IntPtr.Zero && ToolsWindow.IsWindowValid(this.window))
                        {
                            IntPtr currentWindow = ToolsWindow.GetCurrentWindow();
                            if (currentWindow != IntPtr.Zero && this.window.Handle != currentWindow)
                            {
                                Program.info("front");
                                ToolsWindow.BringWindowToFront(this.window);
                            }
                        }
                    }
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void FormPin_KeyDown(object sender, KeyEventArgs e)
        {
            int moveAmount = e.Shift ? 1 : 10;

            switch (e.KeyCode)
            {
                case Keys.Up:
                    Top -= moveAmount;
                    Program.MoveSelected(this, 0, -moveAmount);
                    break;
                case Keys.Down:
                    Top += moveAmount;
                    Program.MoveSelected(this, 0, +moveAmount);
                    break;
                case Keys.Left:
                    Left -= moveAmount;
                    Program.MoveSelected(this, -moveAmount, 0);
                    break;
                case Keys.Right:
                    Left += moveAmount;
                    Program.MoveSelected(this, +moveAmount, 0);
                    break;
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SelectAllPins();
        }

        private void deselectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.DeselectAllPins();
        }

        /*********************************************************************************/

    }
}
