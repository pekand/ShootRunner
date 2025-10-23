#nullable disable


using System.Windows.Forms;

namespace ShootRunner
{
    public partial class FormCommand : Form
    {
        public class ActionItem
        {
            public string Text { get; set; }
            public string Value { get; set; }

            public Bitmap Icon { get; set; }
            public Action Callback { get; set; }

            public override string ToString() => Text;
        }

        public FormPin pinForm = null;
        public Pin pin = null;
        public Window selectedWindow = null;
        public Bitmap customicon = null;

        List<IntPtr> taskbarWindows = null;

        public FormCommand(FormPin pinForm)
        {
            this.pinForm = pinForm;
            this.pin = pinForm.pin;
            InitializeComponent();
        }

        // BUTTON OK CLICK
        private void buttonOK_Click(object sender, EventArgs e)
        {

            this.pin.useWindow = this.checkBoxUseWindow.Checked;

            this.pin.useFilelink = this.checkBoxFile.Checked;
            this.pin.filelink = this.textBoxFile.Text;

            this.pin.useDirectorylink = this.checkBoxDirectory.Checked;
            this.pin.directorylink = this.textBoxDirectory.Text;

            this.pin.useHyperlink = this.checkBoxHyperlink.Checked;
            this.pin.hyperlink = this.textBoxHyperlink.Text;

            this.pin.useScript = this.checkBoxUseScript.Checked;
            this.pin.script = this.textBoxScript.Text;

            this.pin.useCommand = this.checkBoxCommand.Checked;
            this.pin.command = commandTextBox.Text;

            this.pin.useWorkdir = this.checkBoxWorkdir.Checked;
            this.pin.workdir = this.textBoxWorkdir.Text;

            this.pin.usePowershell = this.checkBoxPowerShell.Checked;
            this.pin.useCmdshell = this.checkBoxCmd.Checked;
            this.pin.matchNewWindow = this.checkMatchWindow.Checked;
            this.pin.silentCommand = checkBoxHideOutput.Checked;
            this.pin.doubleClickCommand = checkBoxDoubleclick.Checked;

            if (selectedWindow != this.pin.window)
            {
                if (this.pin.window != null)
                {
                    this.pin.window.Dispose();
                }
                this.pin.window = selectedWindow;
            }

            if (this.pin.customicon != customicon)
            {
                if (this.pin.customicon != null)
                {
                    this.pin.customicon.Dispose();
                }
                this.pin.customicon = customicon;
                this.pinForm.RefreshIcon();
            }

            this.Close();
        }

        private void FormCommand_Load(object sender, EventArgs e)
        {
            this.labelWindowApp.Text = "";

            this.checkBoxUseWindow.Checked = this.pin.useWindow;

            this.checkBoxFile.Checked = this.pin.useFilelink;
            this.textBoxFile.Text = this.pin.filelink;

            this.checkBoxDirectory.Checked = this.pin.useDirectorylink;
            this.textBoxDirectory.Text = this.pin.directorylink;

            this.textBoxHyperlink.Text = this.pin.hyperlink;
            this.checkBoxHyperlink.Checked = this.pin.useHyperlink;

            this.checkBoxUseScript.Checked = this.pin.useScript;
            this.textBoxScript.Text = this.pin.script;

            this.checkBoxCommand.Checked = this.pin.useCommand;
            this.commandTextBox.Text = TextTools.NormalizeLineEndings(pin.command);
            this.commandTextBox.SelectionStart = commandTextBox.Text.Length;
            this.commandTextBox.SelectionLength = 0;
            this.ActiveControl = null;

            this.checkBoxWorkdir.Checked = this.pin.useWorkdir;
            this.textBoxWorkdir.Text = this.pin.workdir;

            this.checkBoxPowerShell.Checked = this.pin.usePowershell;
            this.checkBoxCmd.Checked = this.pin.useCmdshell;
            this.checkMatchWindow.Checked = this.pin.matchNewWindow;
            this.checkBoxHideOutput.Checked = this.pin.silentCommand;
            this.checkBoxDoubleclick.Checked = this.pin.doubleClickCommand;

            if (this.pin.window != null)
            {
                this.selectedWindow = this.pin.window;
                this.pictureBoxIcon.Image = this.pin.window.icon;
                this.labelWindowApp.Text = this.pin.window.app;
            }

            if (this.pin.customicon != null)
            {
                this.pictureBoxIcon.Image = this.pin.customicon;
                this.customicon = this.pin.customicon;
            }

            AddWindowsToContextMenu();

        }

        public void AddWindowsToContextMenu()
        {


            this.taskbarWindows = ToolsWindow.GetTaskbarWindows();

            comboBoxWindow.Items.Clear();

            int Index = 0;
            int SelectedIndex = 0;

            ActionItem emptyItem = new ActionItem();
            emptyItem.Text = "None";
            emptyItem.Icon = null;
            emptyItem.Callback += () => SelectType(null);
            comboBoxWindow.Items.Add(emptyItem);

            foreach (IntPtr Handle in this.taskbarWindows)
            {
                Index++;

                Window window = new Window();
                window.Handle = Handle;
                ToolsWindow.SetWindowData(window);
                ActionItem item = new ActionItem();
                item.Icon = window.icon;
                item.Text = window.Title;
                item.Callback += () => SelectType(window);
                comboBoxWindow.Items.Add(item);

                if (this.pin.window != null && this.pin.window.Handle == Handle)
                {
                    SelectedIndex = Index;
                }
            }

            if (SelectedIndex != 0)
            {
                comboBoxWindow.SelectedIndex = SelectedIndex;
            }
            comboBoxWindow.SelectedIndexChanged += comboBoxWindow_SelectedIndexChanged;
        }

        public void SelectType(Window selectedWindowIncombobox)
        {
            if (selectedWindowIncombobox == null)
            {
                selectedWindow = null;
                pictureBoxIcon.Image = null;
            }
            else
            {
                ToolsWindow.SetWindowData(selectedWindowIncombobox);

                selectedWindow = selectedWindowIncombobox;
                this.labelWindowApp.Text = selectedWindow.app;

                if (this.customicon == null && selectedWindow.icon != null)
                {
                    pictureBoxIcon.Image = selectedWindow.icon;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkMatchWindow_CheckedChanged(object sender, EventArgs e)
        {
            this.pin.matchNewWindow = !this.pin.matchNewWindow;
        }

        private void checkBoxDoubleclick_CheckedChanged(object sender, EventArgs e)
        {
            this.pin.doubleClickCommand = !this.pin.doubleClickCommand;
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        // UNSELECT WINDOW
        private void buttonNoWindow_Click(object sender, EventArgs e)
        {
            comboBoxWindow.SelectedIndex = 0;
            checkBoxUseWindow.Checked = false;
        }

        // SELECT FILE
        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Select a file";
                dialog.Filter = "All files (*.*)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxFile.Text = dialog.FileName;
                    checkBoxFile.Checked = true;
                }
            }

        }

        // SELECT DIRECTORY
        private void buttonSelectDirectory_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select a folder";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxDirectory.Text = dialog.SelectedPath;
                    checkBoxDirectory.Checked = true;
                }
            }
        }

        private void buttonSelectWorkDir_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select a workdir";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxWorkdir.Text = dialog.SelectedPath;
                    checkBoxWorkdir.Checked = true;
                }
            }
        }

        private void checkBoxPowerShell_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPowerShell.Checked)
            {
                checkBoxCmd.Checked = false;
            }
        }

        private void checkBoxCmd_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCmd.Checked)
            {
                checkBoxPowerShell.Checked = false;
            }
        }

        private void textBoxHyperlink_TextChanged(object sender, EventArgs e)
        {
            if (textBoxHyperlink.Text.Trim() != "")
            {
                checkBoxHyperlink.Checked = true;
            }
        }

        private void comboBoxWindow_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxWindow.SelectedItem is ActionItem ai && ai.Callback != null)
                ai.Callback();
        }

        private void comboBoxWindow_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            e.DrawBackground();

            var item = (ActionItem)comboBoxWindow.Items[e.Index];
            int margin = 4;
            int iconSize = 16;
            int x = e.Bounds.X + margin;

            // Draw icon
            if (item.Icon != null)
            {
                e.Graphics.DrawImage(item.Icon, x, e.Bounds.Y + (e.Bounds.Height - iconSize) / 2, iconSize, iconSize);
                x += iconSize + margin;
            }

            // Draw text
            using (var brush = new SolidBrush(e.ForeColor))
                e.Graphics.DrawString(item.Text, e.Font, brush, x, e.Bounds.Y + 3);

            e.DrawFocusRectangle();
        }

        private void pictureBoxIcon_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Select an image";
                dialog.Filter = "Image Files|*.ico;*.png;*.jpg;*.jpeg;*.bmp;*.gif|All Files|*.*";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string selectedFilePath = dialog.FileName;
                        using (var image = Image.FromFile(selectedFilePath))
                        {
                            if (customicon != null)
                            {
                                customicon.Dispose();
                            }
                            customicon = new Bitmap(image);
                            this.pictureBoxIcon.Image = customicon;
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.error("Open image from file error: " + ex.Message);
                    }
                }
            }


        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonRemoveCustomIcon_Click(object sender, EventArgs e)
        {
            this.pictureBoxIcon.Image = null;

            if (this.customicon != null)
            {
                this.customicon.Dispose();
                this.customicon = null;
            }

            if (this.selectedWindow != null)
            {
                pictureBoxIcon.Image = this.selectedWindow.icon;
            }

        }

        private void checkBoxUseWindow_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
