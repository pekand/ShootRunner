namespace ShootRunner
{
    partial class FormCommand
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCommand));
            commandTextBox = new TextBox();
            buttonOK = new Button();
            checkBoxHideOutput = new CheckBox();
            checkMatchWindow = new CheckBox();
            checkBoxDoubleclick = new CheckBox();
            labelWindow = new Label();
            checkBoxUseWindow = new CheckBox();
            comboBoxWindow = new ComboBox();
            textBoxWorkdir = new TextBox();
            buttonSelectWorkDir = new Button();
            checkBoxFile = new CheckBox();
            checkBoxHyperlink = new CheckBox();
            textBoxFile = new TextBox();
            textBoxHyperlink = new TextBox();
            buttonNoWindow = new Button();
            textBoxDirectory = new TextBox();
            checkBoxDirectory = new CheckBox();
            buttonSelectFile = new Button();
            buttonSelectDirectory = new Button();
            checkBoxCommand = new CheckBox();
            checkBoxWorkdir = new CheckBox();
            checkBoxPowerShell = new CheckBox();
            checkBoxCmd = new CheckBox();
            pictureBoxIcon = new PictureBox();
            labelIcon = new Label();
            buttonCancel = new Button();
            buttonRemoveCustomIcon = new Button();
            labelWindowApp = new Label();
            checkBoxUseScript = new CheckBox();
            textBoxScript = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxIcon).BeginInit();
            SuspendLayout();
            // 
            // commandTextBox
            // 
            commandTextBox.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            commandTextBox.Location = new Point(187, 336);
            commandTextBox.Margin = new Padding(6);
            commandTextBox.Multiline = true;
            commandTextBox.Name = "commandTextBox";
            commandTextBox.ScrollBars = ScrollBars.Vertical;
            commandTextBox.Size = new Size(1188, 521);
            commandTextBox.TabIndex = 0;
            commandTextBox.TextChanged += textBox1_TextChanged;
            // 
            // buttonOK
            // 
            buttonOK.Location = new Point(1288, 1000);
            buttonOK.Margin = new Padding(6);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(166, 42);
            buttonOK.TabIndex = 1;
            buttonOK.Text = "ok";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // checkBoxHideOutput
            // 
            checkBoxHideOutput.AutoSize = true;
            checkBoxHideOutput.Checked = true;
            checkBoxHideOutput.CheckState = CheckState.Checked;
            checkBoxHideOutput.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBoxHideOutput.Location = new Point(478, 1005);
            checkBoxHideOutput.Margin = new Padding(6);
            checkBoxHideOutput.Name = "checkBoxHideOutput";
            checkBoxHideOutput.Size = new Size(249, 34);
            checkBoxHideOutput.TabIndex = 2;
            checkBoxHideOutput.Text = "Hide Command Output";
            checkBoxHideOutput.UseVisualStyleBackColor = true;
            // 
            // checkMatchWindow
            // 
            checkMatchWindow.AutoSize = true;
            checkMatchWindow.Checked = true;
            checkMatchWindow.CheckState = CheckState.Checked;
            checkMatchWindow.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkMatchWindow.Location = new Point(754, 920);
            checkMatchWindow.Margin = new Padding(6);
            checkMatchWindow.Name = "checkMatchWindow";
            checkMatchWindow.Size = new Size(212, 34);
            checkMatchWindow.TabIndex = 3;
            checkMatchWindow.Text = "Match new window";
            checkMatchWindow.UseVisualStyleBackColor = true;
            checkMatchWindow.CheckedChanged += checkMatchWindow_CheckedChanged;
            // 
            // checkBoxDoubleclick
            // 
            checkBoxDoubleclick.AutoSize = true;
            checkBoxDoubleclick.Checked = true;
            checkBoxDoubleclick.CheckState = CheckState.Checked;
            checkBoxDoubleclick.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBoxDoubleclick.Location = new Point(754, 962);
            checkBoxDoubleclick.Margin = new Padding(6);
            checkBoxDoubleclick.Name = "checkBoxDoubleclick";
            checkBoxDoubleclick.Size = new Size(230, 34);
            checkBoxDoubleclick.TabIndex = 4;
            checkBoxDoubleclick.Text = "Run with DoubleClick";
            checkBoxDoubleclick.UseVisualStyleBackColor = true;
            checkBoxDoubleclick.CheckedChanged += checkBoxDoubleclick_CheckedChanged;
            // 
            // labelWindow
            // 
            labelWindow.AutoSize = true;
            labelWindow.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelWindow.Location = new Point(24, 33);
            labelWindow.Margin = new Padding(4, 0, 4, 0);
            labelWindow.Name = "labelWindow";
            labelWindow.Size = new Size(89, 30);
            labelWindow.TabIndex = 6;
            labelWindow.Text = "Window";
            // 
            // checkBoxUseWindow
            // 
            checkBoxUseWindow.AutoSize = true;
            checkBoxUseWindow.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBoxUseWindow.Location = new Point(51, 117);
            checkBoxUseWindow.Margin = new Padding(4);
            checkBoxUseWindow.Name = "checkBoxUseWindow";
            checkBoxUseWindow.Size = new Size(108, 34);
            checkBoxUseWindow.TabIndex = 7;
            checkBoxUseWindow.Text = "Window";
            checkBoxUseWindow.UseVisualStyleBackColor = true;
            checkBoxUseWindow.CheckedChanged += checkBoxUseWindow_CheckedChanged;
            // 
            // comboBoxWindow
            // 
            comboBoxWindow.DrawMode = DrawMode.OwnerDrawFixed;
            comboBoxWindow.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxWindow.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBoxWindow.FormattingEnabled = true;
            comboBoxWindow.ItemHeight = 32;
            comboBoxWindow.Location = new Point(187, 117);
            comboBoxWindow.Margin = new Padding(4);
            comboBoxWindow.Name = "comboBoxWindow";
            comboBoxWindow.Size = new Size(1186, 38);
            comboBoxWindow.TabIndex = 8;
            comboBoxWindow.DrawItem += comboBoxWindow_DrawItem;
            // 
            // textBoxWorkdir
            // 
            textBoxWorkdir.Location = new Point(187, 866);
            textBoxWorkdir.Name = "textBoxWorkdir";
            textBoxWorkdir.Size = new Size(1186, 35);
            textBoxWorkdir.TabIndex = 10;
            textBoxWorkdir.TextChanged += textBox1_TextChanged_1;
            // 
            // buttonSelectWorkDir
            // 
            buttonSelectWorkDir.Location = new Point(1382, 863);
            buttonSelectWorkDir.Name = "buttonSelectWorkDir";
            buttonSelectWorkDir.Size = new Size(86, 42);
            buttonSelectWorkDir.TabIndex = 11;
            buttonSelectWorkDir.Text = "Select";
            buttonSelectWorkDir.UseVisualStyleBackColor = true;
            buttonSelectWorkDir.Click += buttonSelectWorkDir_Click;
            // 
            // checkBoxFile
            // 
            checkBoxFile.AutoSize = true;
            checkBoxFile.Location = new Point(51, 164);
            checkBoxFile.Name = "checkBoxFile";
            checkBoxFile.Size = new Size(63, 34);
            checkBoxFile.TabIndex = 12;
            checkBoxFile.Text = "File";
            checkBoxFile.UseVisualStyleBackColor = true;
            // 
            // checkBoxHyperlink
            // 
            checkBoxHyperlink.AutoSize = true;
            checkBoxHyperlink.Location = new Point(51, 252);
            checkBoxHyperlink.Name = "checkBoxHyperlink";
            checkBoxHyperlink.Size = new Size(119, 34);
            checkBoxHyperlink.TabIndex = 13;
            checkBoxHyperlink.Text = "Hyperlink";
            checkBoxHyperlink.UseVisualStyleBackColor = true;
            // 
            // textBoxFile
            // 
            textBoxFile.Location = new Point(187, 162);
            textBoxFile.Name = "textBoxFile";
            textBoxFile.Size = new Size(1186, 35);
            textBoxFile.TabIndex = 14;
            // 
            // textBoxHyperlink
            // 
            textBoxHyperlink.Location = new Point(187, 250);
            textBoxHyperlink.Name = "textBoxHyperlink";
            textBoxHyperlink.Size = new Size(1186, 35);
            textBoxHyperlink.TabIndex = 15;
            textBoxHyperlink.TextChanged += textBoxHyperlink_TextChanged;
            // 
            // buttonNoWindow
            // 
            buttonNoWindow.Location = new Point(1379, 115);
            buttonNoWindow.Name = "buttonNoWindow";
            buttonNoWindow.Size = new Size(86, 40);
            buttonNoWindow.TabIndex = 16;
            buttonNoWindow.Text = "None";
            buttonNoWindow.UseVisualStyleBackColor = true;
            buttonNoWindow.Click += buttonNoWindow_Click;
            // 
            // textBoxDirectory
            // 
            textBoxDirectory.Location = new Point(187, 203);
            textBoxDirectory.Name = "textBoxDirectory";
            textBoxDirectory.Size = new Size(1186, 35);
            textBoxDirectory.TabIndex = 17;
            // 
            // checkBoxDirectory
            // 
            checkBoxDirectory.AutoSize = true;
            checkBoxDirectory.Location = new Point(52, 204);
            checkBoxDirectory.Name = "checkBoxDirectory";
            checkBoxDirectory.Size = new Size(116, 34);
            checkBoxDirectory.TabIndex = 18;
            checkBoxDirectory.Text = "Directory";
            checkBoxDirectory.UseVisualStyleBackColor = true;
            // 
            // buttonSelectFile
            // 
            buttonSelectFile.Location = new Point(1379, 160);
            buttonSelectFile.Name = "buttonSelectFile";
            buttonSelectFile.Size = new Size(86, 35);
            buttonSelectFile.TabIndex = 19;
            buttonSelectFile.Text = "Select";
            buttonSelectFile.UseVisualStyleBackColor = true;
            buttonSelectFile.Click += buttonSelectFile_Click;
            // 
            // buttonSelectDirectory
            // 
            buttonSelectDirectory.Location = new Point(1379, 204);
            buttonSelectDirectory.Name = "buttonSelectDirectory";
            buttonSelectDirectory.Size = new Size(86, 38);
            buttonSelectDirectory.TabIndex = 20;
            buttonSelectDirectory.Text = "Select";
            buttonSelectDirectory.UseVisualStyleBackColor = true;
            buttonSelectDirectory.Click += buttonSelectDirectory_Click;
            // 
            // checkBoxCommand
            // 
            checkBoxCommand.AutoSize = true;
            checkBoxCommand.Location = new Point(50, 334);
            checkBoxCommand.Name = "checkBoxCommand";
            checkBoxCommand.Size = new Size(128, 34);
            checkBoxCommand.TabIndex = 21;
            checkBoxCommand.Text = "Command";
            checkBoxCommand.UseVisualStyleBackColor = true;
            // 
            // checkBoxWorkdir
            // 
            checkBoxWorkdir.AutoSize = true;
            checkBoxWorkdir.Location = new Point(51, 866);
            checkBoxWorkdir.Name = "checkBoxWorkdir";
            checkBoxWorkdir.Size = new Size(104, 34);
            checkBoxWorkdir.TabIndex = 22;
            checkBoxWorkdir.Text = "Workdir";
            checkBoxWorkdir.UseVisualStyleBackColor = true;
            // 
            // checkBoxPowerShell
            // 
            checkBoxPowerShell.AutoSize = true;
            checkBoxPowerShell.Location = new Point(478, 922);
            checkBoxPowerShell.Name = "checkBoxPowerShell";
            checkBoxPowerShell.Size = new Size(172, 34);
            checkBoxPowerShell.TabIndex = 23;
            checkBoxPowerShell.Text = "Use PowerShell";
            checkBoxPowerShell.UseVisualStyleBackColor = true;
            checkBoxPowerShell.CheckedChanged += checkBoxPowerShell_CheckedChanged;
            // 
            // checkBoxCmd
            // 
            checkBoxCmd.AutoSize = true;
            checkBoxCmd.Location = new Point(478, 962);
            checkBoxCmd.Name = "checkBoxCmd";
            checkBoxCmd.Size = new Size(119, 34);
            checkBoxCmd.TabIndex = 24;
            checkBoxCmd.Text = "Use CMD";
            checkBoxCmd.UseVisualStyleBackColor = true;
            checkBoxCmd.CheckedChanged += checkBoxCmd_CheckedChanged;
            // 
            // pictureBoxIcon
            // 
            pictureBoxIcon.BackColor = Color.Gray;
            pictureBoxIcon.Location = new Point(248, 920);
            pictureBoxIcon.Name = "pictureBoxIcon";
            pictureBoxIcon.Size = new Size(128, 128);
            pictureBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxIcon.TabIndex = 25;
            pictureBoxIcon.TabStop = false;
            pictureBoxIcon.Click += pictureBoxIcon_Click;
            // 
            // labelIcon
            // 
            labelIcon.AutoSize = true;
            labelIcon.Location = new Point(189, 920);
            labelIcon.Name = "labelIcon";
            labelIcon.Size = new Size(53, 30);
            labelIcon.TabIndex = 26;
            labelIcon.Text = "Icon";
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(1110, 1002);
            buttonCancel.Margin = new Padding(6);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(166, 42);
            buttonCancel.TabIndex = 27;
            buttonCancel.Text = "Cancel";
            buttonCancel.TextAlign = ContentAlignment.BottomCenter;
            buttonCancel.UseMnemonic = false;
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // buttonRemoveCustomIcon
            // 
            buttonRemoveCustomIcon.Location = new Point(382, 920);
            buttonRemoveCustomIcon.Name = "buttonRemoveCustomIcon";
            buttonRemoveCustomIcon.Size = new Size(35, 40);
            buttonRemoveCustomIcon.TabIndex = 28;
            buttonRemoveCustomIcon.Text = "X";
            buttonRemoveCustomIcon.UseVisualStyleBackColor = true;
            buttonRemoveCustomIcon.Click += buttonRemoveCustomIcon_Click;
            // 
            // labelWindowApp
            // 
            labelWindowApp.AutoSize = true;
            labelWindowApp.Location = new Point(189, 79);
            labelWindowApp.Name = "labelWindowApp";
            labelWindowApp.Size = new Size(171, 30);
            labelWindowApp.TabIndex = 29;
            labelWindowApp.Text = "labelWindowApp";
            // 
            // checkBoxUseScript
            // 
            checkBoxUseScript.AutoSize = true;
            checkBoxUseScript.Location = new Point(52, 292);
            checkBoxUseScript.Name = "checkBoxUseScript";
            checkBoxUseScript.Size = new Size(84, 34);
            checkBoxUseScript.TabIndex = 30;
            checkBoxUseScript.Text = "Script";
            checkBoxUseScript.UseVisualStyleBackColor = true;
            // 
            // textBoxScript
            // 
            textBoxScript.Location = new Point(189, 292);
            textBoxScript.Name = "textBoxScript";
            textBoxScript.Size = new Size(1186, 35);
            textBoxScript.TabIndex = 31;
            // 
            // FormCommand
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1482, 1059);
            Controls.Add(textBoxScript);
            Controls.Add(checkBoxUseScript);
            Controls.Add(labelWindowApp);
            Controls.Add(buttonRemoveCustomIcon);
            Controls.Add(buttonCancel);
            Controls.Add(labelIcon);
            Controls.Add(pictureBoxIcon);
            Controls.Add(checkBoxCmd);
            Controls.Add(checkBoxPowerShell);
            Controls.Add(checkBoxWorkdir);
            Controls.Add(checkBoxCommand);
            Controls.Add(buttonSelectDirectory);
            Controls.Add(buttonSelectFile);
            Controls.Add(checkBoxDirectory);
            Controls.Add(textBoxDirectory);
            Controls.Add(buttonNoWindow);
            Controls.Add(textBoxHyperlink);
            Controls.Add(textBoxFile);
            Controls.Add(checkBoxHyperlink);
            Controls.Add(checkBoxFile);
            Controls.Add(buttonSelectWorkDir);
            Controls.Add(textBoxWorkdir);
            Controls.Add(comboBoxWindow);
            Controls.Add(checkBoxUseWindow);
            Controls.Add(labelWindow);
            Controls.Add(checkBoxDoubleclick);
            Controls.Add(checkMatchWindow);
            Controls.Add(checkBoxHideOutput);
            Controls.Add(buttonOK);
            Controls.Add(commandTextBox);
            Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(6);
            Name = "FormCommand";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Custom Command";
            Load += FormCommand_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxIcon).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox commandTextBox;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.CheckBox checkBoxHideOutput;
        private CheckBox checkMatchWindow;
        private CheckBox checkBoxDoubleclick;
        private Label labelWindow;
        private CheckBox checkBoxUseWindow;
        private ComboBox comboBoxWindow;
        private TextBox textBoxWorkdir;
        private Button buttonSelectWorkDir;
        private CheckBox checkBoxFile;
        private CheckBox checkBoxHyperlink;
        private TextBox textBoxFile;
        private TextBox textBoxHyperlink;
        private Button buttonNoWindow;
        private TextBox textBoxDirectory;
        private CheckBox checkBoxDirectory;
        private Button buttonSelectFile;
        private Button buttonSelectDirectory;
        private CheckBox checkBoxCommand;
        private CheckBox checkBoxWorkdir;
        private CheckBox checkBoxPowerShell;
        private CheckBox checkBoxCmd;
        private PictureBox pictureBoxIcon;
        private Label labelIcon;
        private Button buttonCancel;
        private Button buttonRemoveCustomIcon;
        private Label labelWindowApp;
        private CheckBox checkBoxUseScript;
        private TextBox textBoxScript;
    }
}