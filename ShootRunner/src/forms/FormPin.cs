using Microsoft.CodeAnalysis.Operations;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

#nullable disable


namespace ShootRunner
{
    public partial class FormPin : Form
    {
        public int StartLeft = 100;
        public int StartTop = 100;
        public int StartWidth = 64;
        public int StartHeight = 64;

        public Pin pin = new Pin();

        List<IntPtr> taskbarWindows = null;

        // Variables to track mouse movement
        public bool dragging = false;
        public Point dragCursorPoint;
        public Point dragFormPoint;

        public bool selected = false;

        public FormPin(Window window, bool newPin = false)
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.MinimumSize = new Size(32, 32);
            this.BackColor = Color.White;
            this.TopMost = true;

            this.BackColor = System.Drawing.Color.Black;
            //this.TransparencyKey = System.Drawing.Color.Lime;

            this.pin.window = window;
            if (this.pin.window != null && newPin) {
                this.pin.useWindow = true;
                this.pin.doubleClickCommand = false;                
            }

            this.ShowInTaskbar = false;
            this.AllowDrop = true;
        }

        private void FormPin_Load(object sender, EventArgs e)
        {
            this.SetStartPosition();
            this.makeRoundy();
            if (this.pin.window != null) {
                this.Opacity = this.pin.window.transparent < 0.2 ? 0.2 : this.pin.window.transparent;
            }
            dobleClickToActivateToolStripMenuItem.Checked = this.pin.doubleClickCommand;
            FindPinWindow();
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

        public void FindPinWindow()
        {
            if (this.pin.window != null && this.pin.window.app != null && this.pin.window.app.Trim() != "")
            {
                bool foundWindow = false;
                List<IntPtr> taskbarWindows = ToolsWindow.GetTaskbarWindows();
                foreach (IntPtr Handle in taskbarWindows)
                {
                    if (this.pin.window.Title == ToolsWindow.GetWindowTitle(Handle) && (
                    this.pin.window.app == null || this.pin.window.app == ToolsWindow.GetApplicationPathFromWindow(Handle)))
                    {
                        this.pin.window.Handle = Handle;
                        foundWindow = true;
                        break;
                    }
                }

                if (!foundWindow)
                {
                    foreach (IntPtr Handle in taskbarWindows)
                    {
                        if (this.pin.window.app != null && this.pin.window.app == ToolsWindow.GetApplicationPathFromWindow(Handle))
                        {
                            this.pin.window.Handle = Handle;
                            foundWindow = true;
                            break;
                        }
                    }
                }              
            }
        }

        public async void DoPinAction()
        {

            string wordir = null;

            if (this.pin.useWorkdir) {
                wordir = this.pin.workdir;
            }

            if (this.pin.window != null && this.pin.window.isDesktop)
            {
                ToolsWindow.ShowDesktop();
            }

            if (this.pin.useFilelink)
            {
                SystemTools.OpenFile(this.pin.filelink, wordir);
            }

            if (this.pin.useDirectorylink)
            {
                SystemTools.OpenDirectory(this.pin.directorylink, wordir);
            }

            if (this.pin.useHyperlink)
            {
                SystemTools.OpenHyperlink(this.pin.hyperlink, wordir);
            }

            if (this.pin.useScript)
            {
                //SystemTools.RunScript(this.pin.script, wordir, this.pin.silentCommand);
                if (this.pin.usePowershell)
                {
                    if (this.pin.silentCommand) {
                        await SystemTools.RunPowershellScriptWithTimeoutAsync(this.pin.script, wordir, this.pin.silentCommand);
                    } else {
                        await SystemTools.RunPowershellScriptVisibleWithTimeout(this.pin.script, wordir);
                    }
                }
                if (this.pin.useCmdshell)
                {
                    await SystemTools.RunScriptWithTimeoutAsync(this.pin.script, wordir, this.pin.silentCommand);
                }
            }

            if (this.pin.useCommand && this.pin.command != null && this.pin.command.Trim() != "")
            {
                if (this.pin.matchNewWindow && this.pin.window.Handle != IntPtr.Zero && ToolsWindow.IsWindowValid(this.pin.window))
                {
                    ToolsWindow.BringWindowToFront(this.pin.window);
                }
                else
                {
                    if (this.pin.usePowershell)
                    {
                        if (this.pin.silentCommand)
                        {
                            await SystemTools.RunPowershellScriptWithTimeoutAsync(this.pin.command, wordir, this.pin.silentCommand);
                        }
                        else {
                            await SystemTools.RunPowershellScriptVisibleWithTimeout(this.pin.command, wordir);
                        }
                    }
                    if (this.pin.useCmdshell) {
                        await SystemTools.RunScriptWithTimeoutAsync(this.pin.command, wordir, this.pin.silentCommand);
                    }
                }
            }

            if (this.pin.useWindow && this.pin.window != null)
            {
                if (this.pin.window.Handle != IntPtr.Zero && ToolsWindow.IsWindowValid(this.pin.window) && ToolsWindow.IsWindowVisible(this.pin.window.Handle))
                {
                    ToolsWindow.BringWindowToFront(this.pin.window);
                }
                else if(this.pin.window.app != null && this.pin.window.app.Trim() != "")
                    {
                    Window window = await JobTask.StartProcessAndGetWindowHandleAsync(this.pin.window.app, null, null, false, true);

                    if (window != null && window.Handle != IntPtr.Zero)
                    {
                        this.pin.window.Handle = window.Handle;
                    }
                }
            }
        }

        /*************************************************************************/
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

        private void FormPin_MouseUp(object sender, MouseEventArgs e)
        {
            if (dragging && e.Button == MouseButtons.Left)
            {
                dragging = false;

                if (dragCursorPoint == Cursor.Position && !this.pin.doubleClickCommand)
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
                if (this.pin.window.isDesktop)
                {
                    ToolsWindow.ShowDesktop(true);
                }
                else if (this.pin.useWindow && this.pin.window.Handle != IntPtr.Zero && ToolsWindow.IsWindowValid(this.pin.window))
                {

                    ToolsWindow.MinimizeWindow(this.pin.window);
                }
                else if (this.pin.useCommand && this.pin.matchNewWindow && this.pin.window.Handle != IntPtr.Zero && ToolsWindow.IsWindowValid(this.pin.window))
                {

                    ToolsWindow.MinimizeWindow(this.pin.window);
                }
            }
        }

        private void FormPin_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.pin.doubleClickCommand)
                {
                    this.DoPinAction();
                }
            }
            catch (Exception ex)
            {
                Program.error(ex.Message);

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

            if (this.pin.customicon != null)
            {
                e.Graphics.DrawImage(this.pin.customicon, new Rectangle(0, 0, this.Width, this.Height));
            }
            else if (this.pin.window != null && this.pin.window.icon != null)
            {
                e.Graphics.DrawImage(this.pin.window.icon, new Rectangle(0, 0, this.Width, this.Height));
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
            dobleClickToActivateToolStripMenuItem.Checked = this.pin.doubleClickCommand;
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

                if (this.pin.window != null && this.pin.window.Handle == Handle) {
                    item.Checked = true;
                    item.Font = new Font(item.Font, FontStyle.Bold);
                }

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

            if (window == null || this.pin.window == window)
            {
                return;
            }

            if (this.pin.window != null) {
                this.pin.window.Dispose();
            }
            if (this.pin.window != window) {
                this.pin.useWindow = true;
                this.pin.window = window;                
                this.Refresh();
            }
        }

        private void setCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCommand formCommand = new FormCommand(this);
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
                        if (this.pin.customicon != null)
                        {
                            this.pin.customicon.Dispose();
                        }
                        this.pin.customicon = new Bitmap(image);
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
            ToolsWindow.MinimizeWindow(this.pin.window);
        }

        private void closeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolsWindow.CloseWindow(this.pin.window);
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
            this.pin.transparent = this.Opacity;
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

            this.pin.doubleClickCommand = !this.pin.doubleClickCommand;
            dobleClickToActivateToolStripMenuItem.Checked = this.pin.doubleClickCommand;
            Program.Update();
        }

        private async void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.pin.useCommand)
            {
                JobTask.RunPowerShellCommand(this.pin.command, null, this.pin.silentCommand);
            }

            if (this.pin.useWindow)
            {
                if (this.pin.window.app != null && this.pin.window.app.Trim() != "")
                {
                    Window window = await JobTask.StartProcessAndGetWindowHandleAsync(this.pin.window.app, null, null, this.pin.silentCommand);

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
                if (this.pin.useWindow && this.pin.window.Handle != IntPtr.Zero)
                {
                    ToolsWindow.MinimizeWindow(this.pin.window);
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

                if (this.pin.window != null)
                {
                    if (this.pin.useCommand && this.pin.command != null && this.pin.command.Trim() != "")
                    {
                        if (this.pin.matchNewWindow && this.pin.window.Handle != IntPtr.Zero && ToolsWindow.IsWindowValid(this.pin.window))
                        {
                            IntPtr currentWindow = ToolsWindow.GetCurrentWindow();
                            if (currentWindow != IntPtr.Zero && this.pin.window.Handle != currentWindow)
                            {
                                Program.info("front");
                                ToolsWindow.BringWindowToFront(this.pin.window);
                            }

                        }
                    }
                    else if (this.pin.useWindow)
                    {
                        if (this.pin.window.Handle != IntPtr.Zero && ToolsWindow.IsWindowValid(this.pin.window))
                        {
                            IntPtr currentWindow = ToolsWindow.GetCurrentWindow();
                            if (currentWindow != IntPtr.Zero && this.pin.window.Handle != currentWindow)
                            {
                                Program.info("front");
                                ToolsWindow.BringWindowToFront(this.pin.window);
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

        private void FormPin_DragDrop(object sender, DragEventArgs e)
        {

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

        public void RefreshIcon() {
            this.Refresh();

        }

        /*********************************************************************************/

    }
}
