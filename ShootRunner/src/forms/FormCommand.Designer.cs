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
            textBoxScript = new TextBox();
            buttonOK = new Button();
            checkBoxHideOutput = new CheckBox();
            checkMatchWindow = new CheckBox();
            checkBoxDoubleclick = new CheckBox();
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
            checkBoxUseScript = new CheckBox();
            textBoxCommand = new TextBox();
            labelWindowApp = new TextBox();
            webViewHelp = new Microsoft.Web.WebView2.WinForms.WebView2();
            buttonCopyWinApp = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webViewHelp).BeginInit();
            SuspendLayout();
            // 
            // textBoxScript
            // 
            textBoxScript.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxScript.Location = new Point(162, 267);
            textBoxScript.Margin = new Padding(6);
            textBoxScript.Multiline = true;
            textBoxScript.Name = "textBoxScript";
            textBoxScript.ScrollBars = ScrollBars.Vertical;
            textBoxScript.Size = new Size(1188, 521);
            textBoxScript.TabIndex = 0;
            textBoxScript.TextChanged += textBox1_TextChanged;
            // 
            // buttonOK
            // 
            buttonOK.Location = new Point(1263, 931);
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
            checkBoxHideOutput.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBoxHideOutput.Location = new Point(453, 936);
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
            checkMatchWindow.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkMatchWindow.Location = new Point(729, 851);
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
            checkBoxDoubleclick.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBoxDoubleclick.Location = new Point(729, 893);
            checkBoxDoubleclick.Margin = new Padding(6);
            checkBoxDoubleclick.Name = "checkBoxDoubleclick";
            checkBoxDoubleclick.Size = new Size(230, 34);
            checkBoxDoubleclick.TabIndex = 4;
            checkBoxDoubleclick.Text = "Run with DoubleClick";
            checkBoxDoubleclick.UseVisualStyleBackColor = true;
            checkBoxDoubleclick.CheckedChanged += checkBoxDoubleclick_CheckedChanged;
            // 
            // checkBoxUseWindow
            // 
            checkBoxUseWindow.AutoSize = true;
            checkBoxUseWindow.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBoxUseWindow.Location = new Point(26, 48);
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
            comboBoxWindow.Location = new Point(162, 48);
            comboBoxWindow.Margin = new Padding(4);
            comboBoxWindow.Name = "comboBoxWindow";
            comboBoxWindow.Size = new Size(1186, 38);
            comboBoxWindow.TabIndex = 8;
            comboBoxWindow.DrawItem += comboBoxWindow_DrawItem;
            // 
            // textBoxWorkdir
            // 
            textBoxWorkdir.Location = new Point(162, 797);
            textBoxWorkdir.Name = "textBoxWorkdir";
            textBoxWorkdir.Size = new Size(1186, 35);
            textBoxWorkdir.TabIndex = 10;
            textBoxWorkdir.TextChanged += textBox1_TextChanged_1;
            // 
            // buttonSelectWorkDir
            // 
            buttonSelectWorkDir.Location = new Point(1357, 794);
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
            checkBoxFile.Location = new Point(26, 95);
            checkBoxFile.Name = "checkBoxFile";
            checkBoxFile.Size = new Size(63, 34);
            checkBoxFile.TabIndex = 12;
            checkBoxFile.Text = "File";
            checkBoxFile.UseVisualStyleBackColor = true;
            // 
            // checkBoxHyperlink
            // 
            checkBoxHyperlink.AutoSize = true;
            checkBoxHyperlink.Location = new Point(26, 183);
            checkBoxHyperlink.Name = "checkBoxHyperlink";
            checkBoxHyperlink.Size = new Size(119, 34);
            checkBoxHyperlink.TabIndex = 13;
            checkBoxHyperlink.Text = "Hyperlink";
            checkBoxHyperlink.UseVisualStyleBackColor = true;
            // 
            // textBoxFile
            // 
            textBoxFile.Location = new Point(162, 93);
            textBoxFile.Name = "textBoxFile";
            textBoxFile.Size = new Size(1186, 35);
            textBoxFile.TabIndex = 14;
            // 
            // textBoxHyperlink
            // 
            textBoxHyperlink.Location = new Point(162, 181);
            textBoxHyperlink.Name = "textBoxHyperlink";
            textBoxHyperlink.Size = new Size(1186, 35);
            textBoxHyperlink.TabIndex = 15;
            textBoxHyperlink.TextChanged += textBoxHyperlink_TextChanged;
            // 
            // buttonNoWindow
            // 
            buttonNoWindow.Location = new Point(1354, 46);
            buttonNoWindow.Name = "buttonNoWindow";
            buttonNoWindow.Size = new Size(86, 40);
            buttonNoWindow.TabIndex = 16;
            buttonNoWindow.Text = "None";
            buttonNoWindow.UseVisualStyleBackColor = true;
            buttonNoWindow.Click += buttonNoWindow_Click;
            // 
            // textBoxDirectory
            // 
            textBoxDirectory.Location = new Point(162, 134);
            textBoxDirectory.Name = "textBoxDirectory";
            textBoxDirectory.Size = new Size(1186, 35);
            textBoxDirectory.TabIndex = 17;
            // 
            // checkBoxDirectory
            // 
            checkBoxDirectory.AutoSize = true;
            checkBoxDirectory.Location = new Point(27, 135);
            checkBoxDirectory.Name = "checkBoxDirectory";
            checkBoxDirectory.Size = new Size(116, 34);
            checkBoxDirectory.TabIndex = 18;
            checkBoxDirectory.Text = "Directory";
            checkBoxDirectory.UseVisualStyleBackColor = true;
            // 
            // buttonSelectFile
            // 
            buttonSelectFile.Location = new Point(1354, 91);
            buttonSelectFile.Name = "buttonSelectFile";
            buttonSelectFile.Size = new Size(86, 35);
            buttonSelectFile.TabIndex = 19;
            buttonSelectFile.Text = "Select";
            buttonSelectFile.UseVisualStyleBackColor = true;
            buttonSelectFile.Click += buttonSelectFile_Click;
            // 
            // buttonSelectDirectory
            // 
            buttonSelectDirectory.Location = new Point(1354, 135);
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
            checkBoxCommand.Location = new Point(26, 223);
            checkBoxCommand.Name = "checkBoxCommand";
            checkBoxCommand.Size = new Size(128, 34);
            checkBoxCommand.TabIndex = 21;
            checkBoxCommand.Text = "Command";
            checkBoxCommand.UseVisualStyleBackColor = true;
            // 
            // checkBoxWorkdir
            // 
            checkBoxWorkdir.AutoSize = true;
            checkBoxWorkdir.Location = new Point(26, 797);
            checkBoxWorkdir.Name = "checkBoxWorkdir";
            checkBoxWorkdir.Size = new Size(104, 34);
            checkBoxWorkdir.TabIndex = 22;
            checkBoxWorkdir.Text = "Workdir";
            checkBoxWorkdir.UseVisualStyleBackColor = true;
            // 
            // checkBoxPowerShell
            // 
            checkBoxPowerShell.AutoSize = true;
            checkBoxPowerShell.Location = new Point(453, 853);
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
            checkBoxCmd.Location = new Point(453, 893);
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
            pictureBoxIcon.Location = new Point(223, 851);
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
            labelIcon.Location = new Point(164, 851);
            labelIcon.Name = "labelIcon";
            labelIcon.Size = new Size(53, 30);
            labelIcon.TabIndex = 26;
            labelIcon.Text = "Icon";
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(1085, 933);
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
            buttonRemoveCustomIcon.Location = new Point(357, 851);
            buttonRemoveCustomIcon.Name = "buttonRemoveCustomIcon";
            buttonRemoveCustomIcon.Size = new Size(35, 40);
            buttonRemoveCustomIcon.TabIndex = 28;
            buttonRemoveCustomIcon.Text = "X";
            buttonRemoveCustomIcon.UseVisualStyleBackColor = true;
            buttonRemoveCustomIcon.Click += buttonRemoveCustomIcon_Click;
            // 
            // checkBoxUseScript
            // 
            checkBoxUseScript.AutoSize = true;
            checkBoxUseScript.Location = new Point(27, 265);
            checkBoxUseScript.Name = "checkBoxUseScript";
            checkBoxUseScript.Size = new Size(84, 34);
            checkBoxUseScript.TabIndex = 30;
            checkBoxUseScript.Text = "Script";
            checkBoxUseScript.UseVisualStyleBackColor = true;
            // 
            // textBoxCommand
            // 
            textBoxCommand.Location = new Point(162, 221);
            textBoxCommand.Name = "textBoxCommand";
            textBoxCommand.Size = new Size(1186, 35);
            textBoxCommand.TabIndex = 31;
            // 
            // labelWindowApp
            // 
            labelWindowApp.BorderStyle = BorderStyle.None;
            labelWindowApp.Location = new Point(198, 13);
            labelWindowApp.Name = "labelWindowApp";
            labelWindowApp.ReadOnly = true;
            labelWindowApp.Size = new Size(1150, 28);
            labelWindowApp.TabIndex = 32;
            labelWindowApp.Text = "Window app";
            labelWindowApp.TextChanged += labelWindowApp_TextChanged;
            labelWindowApp.DoubleClick += labelWindowApp_DoubleClick;
            // 
            // webViewHelp
            // 
            webViewHelp.AllowExternalDrop = true;
            webViewHelp.CreationProperties = null;
            webViewHelp.DefaultBackgroundColor = Color.White;
            webViewHelp.Location = new Point(1465, 48);
            webViewHelp.Name = "webViewHelp";
            webViewHelp.Size = new Size(321, 931);
            webViewHelp.TabIndex = 33;
            webViewHelp.ZoomFactor = 1D;
            // 
            // buttonCopyWinApp
            // 
            buttonCopyWinApp.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonCopyWinApp.Location = new Point(164, 17);
            buttonCopyWinApp.Name = "buttonCopyWinApp";
            buttonCopyWinApp.Size = new Size(28, 28);
            buttonCopyWinApp.TabIndex = 34;
            buttonCopyWinApp.Text = "C";
            buttonCopyWinApp.UseVisualStyleBackColor = true;
            buttonCopyWinApp.Click += buttonCopyWinApp_Click;
            // 
            // FormCommand
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1798, 1000);
            Controls.Add(buttonCopyWinApp);
            Controls.Add(webViewHelp);
            Controls.Add(labelWindowApp);
            Controls.Add(textBoxCommand);
            Controls.Add(checkBoxUseScript);
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
            Controls.Add(checkBoxDoubleclick);
            Controls.Add(checkMatchWindow);
            Controls.Add(checkBoxHideOutput);
            Controls.Add(buttonOK);
            Controls.Add(textBoxScript);
            Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(6);
            Name = "FormCommand";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Custom Command";
            Load += FormCommand_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)webViewHelp).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox textBoxScript;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.CheckBox checkBoxHideOutput;
        private CheckBox checkMatchWindow;
        private CheckBox checkBoxDoubleclick;
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
        private CheckBox checkBoxUseScript;
        private TextBox textBoxCommand;
        private TextBox labelWindowApp;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewHelp;
        private Button buttonCopyWinApp;
    }
}