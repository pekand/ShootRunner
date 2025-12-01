using System.ComponentModel;
using System.Drawing.Drawing2D;

#nullable disable

#pragma warning disable IDE0079
#pragma warning disable IDE0130

namespace ShootRunner
{
    public partial class FormPin : Form
    {
        public int StartLeft = 100;
        public int StartTop = 100;
        public int StartWidth = 64;
        public int StartHeight = 64;

        public Pin pin = new();

        List<IntPtr> taskbarWindows = null;

        // Variables to track mouse movement
        public bool dragging = false;
        public Point dragCursorPoint;
        public Point dragFormPoint;

        public bool selected = false;

        /*************************************************************************/

        // CONSTRUCTOR
        public FormPin(Window window, bool newPin = false)
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.MinimumSize = new Size(32, 32);
            this.BackColor = Color.White;
            this.TopMost = this.pin.mosttop;

            this.BackColor = System.Drawing.Color.Black;

            this.pin.window = window;
            if (this.pin.window != null && newPin)
            {
                this.pin.useWindow = true;
                this.pin.doubleClickCommand = false;
            }

            this.ShowInTaskbar = false;
            this.AllowDrop = true;
        }

        /*************************************************************************/

        // ACTION HIDE IN TASKBAR
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

        // MESSAGGES LOOP
        protected override void WndProc(ref Message m)
        {

            if (m.Msg == WinApi.WM_NCHITTEST)
            {
                Point pos = PointToClient(Cursor.Position);
                Size size = this.ClientSize;

                // RESIZE RIGHT BOTTOM CORNER
                if (pos.X >= size.Width - 10 && pos.Y >= size.Height - 10)
                {
                    this.MakeSquery();
                    m.Result = (IntPtr)WinApi.HTBOTTOMRIGHT;
                    return;
                }
            }
            base.WndProc(ref m);
        }

        // FORM LOAD
        private void FormPin_Load(object sender, EventArgs e)
        {
            this.SetStartPosition();
            this.MakeRoundy();
            if (this.pin.window != null)
            {
                this.Opacity = this.pin.window.transparent < 0.2 ? 0.2 : this.pin.window.transparent;
            }
            dobleClickToActivateToolStripMenuItem.Checked = this.pin.doubleClickCommand;
            FindPinWindow();
        }

        // FORM
        private void FormPin_Shown(object sender, EventArgs e)
        {
            if (!this.TopMost && this.pin.mosttop)
            {
                this.TopMost = this.pin.mosttop;
            }
        }

        // FORM
        private void FormPin_Click(object sender, EventArgs e)
        {
            if (!this.TopMost && this.pin.mosttop)
            {
                this.TopMost = this.pin.mosttop;
            }
        }

        // FORM
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

        // FORM
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
                Program.Error(ex.Message);

            }
        }

        // FORM
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

        // FORM
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

        // FORM
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

        // FORM
        private void FormPin_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Color startColor = Color.FromArgb(255, 50, 50, 50);
            Color endColor = Color.FromArgb(255, 30, 30, 30);

            Rectangle rect = new(0, 0, this.Width, this.Height);

            using (LinearGradientBrush brush = new(rect, startColor, endColor, LinearGradientMode.Vertical))
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

        // FORM
        private void FormPin_Resize(object sender, EventArgs e)
        {
            int newSize = Math.Min(this.Width, this.Height);
            this.Size = new Size(newSize, newSize);
            Program.Update();
            this.Refresh();
        }

        // FORM
        private void FormPin_ResizeBegin(object sender, EventArgs e)
        {
            this.MakeSquery();
        }

        // FORM
        private void FormPin_ResizeEnd(object sender, EventArgs e)
        {
            this.MakeRoundy();
        }

        // FORM
        private void FormPin_DragEnter(object sender, DragEventArgs e)
        {
            Program.Info("Available formats:\r\n");
            foreach (var f in e.Data.GetFormats()) Program.Info("  " + f + "\r\n");

            bool hasContent = false;

            try
            {


                if (e.Data.GetData(DataFormats.FileDrop) != null)
                    hasContent = true;
                else if (e.Data.GetData(DataFormats.Text) != null)
                    hasContent = true;
                else if (e.Data.GetData(DataFormats.UnicodeText) != null)
                    hasContent = true;
                else if (e.Data.GetData(DataFormats.Html) != null)
                    hasContent = true;
                else if (e.Data.GetData(DataFormats.Bitmap) != null)
                    hasContent = true;

            }
            catch (Exception)
            {


            }

            e.Effect = hasContent ? DragDropEffects.Copy : DragDropEffects.None;


            if (hasContent)
            {
                if (this.pin.window != null)
                {
                    if (this.pin.useCommand && this.pin.command != null && this.pin.command.Trim() != "")
                    {
                        if (this.pin.matchNewWindow && this.pin.window.Handle != IntPtr.Zero && ToolsWindow.IsWindowValid(this.pin.window))
                        {
                            IntPtr currentWindow = ToolsWindow.GetCurrentWindow();
                            if (currentWindow != IntPtr.Zero && this.pin.window.Handle != currentWindow)
                            {
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
                                ToolsWindow.BringWindowToFront(this.pin.window);
                            }
                        }
                    }
                }
            }

        }

        // FORM
        private void FormPin_DragDrop(object sender, DragEventArgs e)
        {
            string text = null;
            string html = null;
            Bitmap image = null;
            string[] files = null;

            try
            {

                if (e.Data.GetData(DataFormats.FileDrop) != null)
                {
                    files = (string[])e.Data.GetData(DataFormats.FileDrop);
                }

                if (e.Data.GetData(DataFormats.Text) != null)
                {
                    text = (string)e.Data.GetData(DataFormats.Text);
                }

                if (e.Data.GetData(DataFormats.UnicodeText) != null)
                {
                    text = (string)e.Data.GetData(DataFormats.UnicodeText);
                }

                if (e.Data.GetData(DataFormats.Html) != null)
                {
                    html = (string)e.Data.GetData(DataFormats.Html);
                }

                if (e.Data.GetData(DataFormats.Bitmap) != null)
                {
                    image = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
                }

            }
            catch (Exception)
            {


            }



            if (files != null)
            {
                if (this.pin.useDirectorylink && Directory.Exists(this.pin.directorylink))
                {

                    foreach (var file in files)
                    {
                        Program.Info($"File: {file}\r\n");
                        if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                        {
                            Os.CopyOrMove(file, this.pin.directorylink, move: true);
                        }
                        else
                        {
                            Os.CopyOrMove(file, this.pin.directorylink, move: false);
                        }
                    }
                }
            }

            if (html != null)
            {

                if (Uri.IsWellFormedUriString(text.Trim(), UriKind.Absolute))
                {
                    Task.Run(async () =>
                    {
                        string url = text.Trim();
                        string title = await Os.GetPageTitleAsync(url);
                        Os.SaveInternetShortcut(this.pin.directorylink, url, title);
                    });

                }
                else if (this.pin.useDirectorylink && Directory.Exists(this.pin.directorylink))
                {
                    Os.SaveTextWithAutoRename(this.pin.directorylink, Os.ExtractHtmlFragment(html), "page", ".html");
                }

            }
            else
            if (text != null)
            {
                if (this.pin.useDirectorylink && Directory.Exists(this.pin.directorylink))
                {
                    if (Uri.IsWellFormedUriString(text.Trim(), UriKind.Absolute))
                    {
                        Task.Run(async () =>
                        {
                            string url = text.Trim();
                            string title = await Os.GetPageTitleAsync(url);
                            Os.SaveInternetShortcut(this.pin.directorylink, url, title);
                        });
                    }
                    else
                    {
                        Os.SaveTextWithAutoRename(this.pin.directorylink, text, "text", ".txt");
                    }
                }
            }

            if (image != null)
            {
                if (this.pin.useDirectorylink && Directory.Exists(this.pin.directorylink))
                {
                    var path = Path.Combine(this.pin.directorylink, "image_" + Guid.NewGuid() + ".png");
                    image.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                    Program.Info("Image saved to: " + path + "\r\n");
                }
            }
        }

        /*********************************************************************************/

        // CONTEXTMENU
        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            dobleClickToActivateToolStripMenuItem.Checked = this.pin.doubleClickCommand;
            topMostToolStripMenuItem.Checked = this.pin.mosttop;
            AddWindowsToContextMenu();
        }

        // CONTEXTMENU PIN
        private void SelectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* select window from oppened windows, is filled with code */
        }

        // CONTEXTMENU PIN
        private void SetCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCommand formCommand = new(this);
            formCommand.Show();
        }

        // CONTEXTMENU PIN
        private void NewPinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.AddEmptyPin();
        }

        // CONTEXTMENU PIN
        private async void DuplicateToolStripMenuItem_Click(object sender, EventArgs e)
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

        // CONTEXTMENU PIN
        private void MinimalizeToolStripMenuItem_Click_1(object sender, EventArgs e)
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
                Program.Error(ex.Message);
            }

        }

        // CONTEXTMENU PIN
        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.CloseForm();
        }

        // CONTEXTMENU PIN
        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SelectAllPins();
        }

        // CONTEXTMENU PIN
        private void DeselectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.DeselectAllPins();
        }

        // CONTEXTMENU OPTIONS
        private void SetIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string selectedFilePath = openFileDialog.FileName;
                    using (var image = Image.FromFile(selectedFilePath))
                    {
                        this.pin.customicon?.Dispose();
                        this.pin.customicon = new Bitmap(image);
                        this.Refresh();
                    }

                    Program.Update();
                }
                catch (Exception ex)
                {
                    Program.Error("Open image from file error: " + ex.Message);
                }
            }
        }

        // CONTEXTMENU OPTIONS
        private void DobleClickToActivateToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.pin.doubleClickCommand = !this.pin.doubleClickCommand;
            dobleClickToActivateToolStripMenuItem.Checked = this.pin.doubleClickCommand;
            Program.Update();
        }

        // CONTEXTMENU OPTIONS
        private void TransparentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTransparent form = new(null, this);
            form.trackBar1.Value = (int)(this.Opacity * 100);
            form.Show();
            this.pin.transparent = this.Opacity;
        }

        // CONTEXTMENU OPTIONS
        private void TopMostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.pin.mosttop = !this.pin.mosttop;
            this.TopMost = this.pin.mosttop;
        }

        // CONTEXTMENU WIDGET
        private void NewWidgetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.widgetManager.AddEmptyWidget();
        }

        // CONTEXTMENU WIDGET CREATE
        private void CreateWidgetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.widgetManager.ShowCreateWidgetForm();
        }

        // CONTEXTMENU WIDGET
        private void TaskbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.widgetManager.ShowTaskbarWidget(null);
        }

        // CONTEXTMENU
        private void SelectWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        // CONTEXTMENU APPLICATION
        private void ConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.ShowConsole();
        }

        // CONTEXTMENU APPLICATION EXIT
        private void CloseToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            Program.Exit();
        }

        /*********************************************************************************/

        // ACTION PIN DO ACTION
        public async void DoPinAction()
        {

            string wordir = null;

            if (this.pin.useWorkdir)
            {
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
                    if (this.pin.silentCommand)
                    {
                        await SystemTools.RunPowershellScriptWithTimeoutAsync(this.pin.script, wordir, this.pin.silentCommand); // TODO this.pin.silentCommand 
                    }
                    else
                    {
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
                            await SystemTools.RunPowershellScriptWithTimeoutAsync(this.pin.command, wordir, this.pin.silentCommand); // TODO this.pin.silentCommand 
                        }
                        else
                        {
                            await SystemTools.RunPowershellScriptVisibleWithTimeout(this.pin.command, wordir);
                        }
                    }
                    if (this.pin.useCmdshell)
                    {
                        await SystemTools.RunScriptWithTimeoutAsync(this.pin.command, wordir,this.pin.silentCommand);
                    }
                }
            }

            if (this.pin.useWindow && this.pin.window != null)
            {
                if (this.pin.window.Handle != IntPtr.Zero && ToolsWindow.IsWindowValid(this.pin.window) && WinApi.IsWindowVisible(this.pin.window.Handle))
                {
                    ToolsWindow.BringWindowToFront(this.pin.window);
                }
                else if (this.pin.window.app != null && this.pin.window.app.Trim() != "")
                {
                    Window window = await JobTask.StartProcessAndGetWindowHandleAsync(this.pin.window.app, null, null, false, true);

                    if (window != null && window.Handle != IntPtr.Zero)
                    {
                        this.pin.window.Handle = window.Handle;
                    }
                }
            }
        }

        // ACTION WINDOW FIND
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

        // ACTION ADD TASKBAR WINDOWS TO CONTEXTMENU
        public void AddWindowsToContextMenu()
        {
            this.taskbarWindows = ToolsWindow.GetTaskbarWindows();

            selectToolStripMenuItem.DropDownItems.Clear();
            foreach (IntPtr Handle in this.taskbarWindows)
            {
                Window window = new()
                {
                    Handle = Handle
                };
                ToolsWindow.SetWindowData(window);
                ToolStripMenuItem item = new(window.Title)
                {
                    Image = window.icon
                };
                item.Click += (sender, e) => SelectTasbarWindowInContextMenu(window);

                if (this.pin.window != null && this.pin.window.Handle == Handle)
                {
                    item.Checked = true;
                    item.Font = new Font(item.Font, FontStyle.Bold);
                }

                selectToolStripMenuItem.DropDownItems.Add(item);
            }
        }

        // ACTION
        public void SelectTasbarWindowInContextMenu(Window window)
        {
            ToolsWindow.SetWindowData(window);

            if (window == null || this.pin.window == window)
            {
                return;
            }

            this.pin.window?.Dispose();
            if (this.pin.window != window)
            {
                this.pin.useWindow = true;
                this.pin.window = window;
                this.Refresh();
            }
        }

        // ACTION
        public void RedrawPin()
        {
            this.Refresh();

        }

        // ACTION SET START POSITION
        public void SetStartPosition()
        {
            this.Left = this.StartLeft;
            this.Top = this.StartTop;
            this.Width = this.StartWidth;
            this.Height = this.StartHeight;

        }

        // ACTION CENTER PIN IN SCREEN
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

        // ACTION WINDOW SHAPE 
        public void MakeRoundy()
        {
            // Set the radius for the rounded corners
            int radius = 15;

            GraphicsPath path = new();

            // Define the rounded rectangle
            path.AddArc(0, 0, radius, radius, 180, 90); // Top-left corner
            path.AddArc(this.Width - radius, 0, radius, radius, 270, 90); // Top-right corner
            path.AddArc(this.Width - radius, this.Height - radius, radius, radius, 0, 90); // Bottom-right corner
            path.AddArc(0, this.Height - radius, radius, radius, 90, 90); // Bottom-left corner
            path.CloseAllFigures();

            this.Region = new Region(path);
        }

        // ACTION  WINDOW SHAPE 
        public void MakeSquery()
        {

            this.Region = null;
        }

        // ACTION DRAG
        public void SetDragStartLocation()
        {
            dragFormPoint = this.Location;
            dragCursorPoint = Cursor.Position;
        }

        // ACTION DRAG
        public void SetDragNewLocation()
        {
            Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
            Point sum = Point.Add(dragFormPoint, new Size(diff));
            this.Location = sum;
        }

        // ACTION SELECTION MARK AS UNSELECTED
        public void UnselectPin()
        {
            selected = false;
            this.Refresh();
        }

        // ACTION SELECTION MARK AS SELECTED
        public void SelectPin()
        {
            selected = true;
            this.Refresh();
        }

        // ACTION FORM CLOSE
        public void CloseForm()
        {
            Program.RemoveFormFromSelected(this);
            Program.pins.Remove(this);
            Program.Update();
            this.Close();
        }

        /*********************************************************************************/

    }
}
