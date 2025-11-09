namespace ShootRunner
{
    partial class FormTaskbar
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTaskbar));
            contextMenuStripTaskbar = new ContextMenuStrip(components);
            taskbarToolStripMenuItem = new ToolStripMenuItem();
            removeToolStripMenuItem = new ToolStripMenuItem();
            showAllHiddenToolStripMenuItem = new ToolStripMenuItem();
            windowToolStripMenuItem = new ToolStripMenuItem();
            minimalizeToolStripMenuItem = new ToolStripMenuItem();
            infoToolStripMenuItem = new ToolStripMenuItem();
            showDesktopToolStripMenuItem = new ToolStripMenuItem();
            hiddeToolStripMenuItem = new ToolStripMenuItem();
            closeToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            mostTopToolStripMenuItem = new ToolStripMenuItem();
            lockToolStripMenuItem = new ToolStripMenuItem();
            useScreenshotsToolStripMenuItem = new ToolStripMenuItem();
            useBigIconsToolStripMenuItem = new ToolStripMenuItem();
            backgroundColorToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            applicationToolStripMenuItem = new ToolStripMenuItem();
            consoleToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStripTaskbar.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStripTaskbar
            // 
            contextMenuStripTaskbar.Items.AddRange(new ToolStripItem[] { taskbarToolStripMenuItem, windowToolStripMenuItem, optionsToolStripMenuItem, toolStripMenuItem1, applicationToolStripMenuItem });
            contextMenuStripTaskbar.Name = "contextMenuStrip1";
            contextMenuStripTaskbar.Size = new Size(181, 128);
            contextMenuStripTaskbar.Opening += contextMenuStrip1_Opening;
            // 
            // taskbarToolStripMenuItem
            // 
            taskbarToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { removeToolStripMenuItem, showAllHiddenToolStripMenuItem });
            taskbarToolStripMenuItem.Name = "taskbarToolStripMenuItem";
            taskbarToolStripMenuItem.Size = new Size(180, 24);
            taskbarToolStripMenuItem.Text = "Taskbar";
            // 
            // removeToolStripMenuItem
            // 
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            removeToolStripMenuItem.Size = new Size(184, 24);
            removeToolStripMenuItem.Text = "Remove taskbar";
            removeToolStripMenuItem.Click += removeToolStripMenuItem_Click;
            // 
            // showAllHiddenToolStripMenuItem
            // 
            showAllHiddenToolStripMenuItem.Name = "showAllHiddenToolStripMenuItem";
            showAllHiddenToolStripMenuItem.Size = new Size(184, 24);
            showAllHiddenToolStripMenuItem.Text = "Show all hidden";
            showAllHiddenToolStripMenuItem.Click += showAllHiddenToolStripMenuItem_Click;
            // 
            // windowToolStripMenuItem
            // 
            windowToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { minimalizeToolStripMenuItem, infoToolStripMenuItem, showDesktopToolStripMenuItem, hiddeToolStripMenuItem, closeToolStripMenuItem });
            windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            windowToolStripMenuItem.Size = new Size(180, 24);
            windowToolStripMenuItem.Text = "Window";
            windowToolStripMenuItem.Click += windowToolStripMenuItem_Click;
            // 
            // minimalizeToolStripMenuItem
            // 
            minimalizeToolStripMenuItem.Name = "minimalizeToolStripMenuItem";
            minimalizeToolStripMenuItem.Size = new Size(180, 24);
            minimalizeToolStripMenuItem.Text = "Minimalize";
            minimalizeToolStripMenuItem.Click += minimalizeToolStripMenuItem_Click;
            // 
            // infoToolStripMenuItem
            // 
            infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            infoToolStripMenuItem.Size = new Size(180, 24);
            infoToolStripMenuItem.Text = "Info";
            infoToolStripMenuItem.Click += infoToolStripMenuItem_Click;
            // 
            // showDesktopToolStripMenuItem
            // 
            showDesktopToolStripMenuItem.Name = "showDesktopToolStripMenuItem";
            showDesktopToolStripMenuItem.Size = new Size(180, 24);
            showDesktopToolStripMenuItem.Text = "Show desktop";
            showDesktopToolStripMenuItem.Click += showDesktopToolStripMenuItem_Click;
            // 
            // hiddeToolStripMenuItem
            // 
            hiddeToolStripMenuItem.Name = "hiddeToolStripMenuItem";
            hiddeToolStripMenuItem.Size = new Size(180, 24);
            hiddeToolStripMenuItem.Text = "Hidde";
            hiddeToolStripMenuItem.Click += hiddeToolStripMenuItem_Click;
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(180, 24);
            closeToolStripMenuItem.Text = "Close";
            closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mostTopToolStripMenuItem, lockToolStripMenuItem, useScreenshotsToolStripMenuItem, useBigIconsToolStripMenuItem, backgroundColorToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(180, 24);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // mostTopToolStripMenuItem
            // 
            mostTopToolStripMenuItem.Name = "mostTopToolStripMenuItem";
            mostTopToolStripMenuItem.Size = new Size(195, 24);
            mostTopToolStripMenuItem.Text = "Most top";
            mostTopToolStripMenuItem.Click += mostTopToolStripMenuItem_Click;
            // 
            // lockToolStripMenuItem
            // 
            lockToolStripMenuItem.Name = "lockToolStripMenuItem";
            lockToolStripMenuItem.Size = new Size(195, 24);
            lockToolStripMenuItem.Text = "Locked";
            lockToolStripMenuItem.Click += lockToolStripMenuItem_Click;
            // 
            // useScreenshotsToolStripMenuItem
            // 
            useScreenshotsToolStripMenuItem.Name = "useScreenshotsToolStripMenuItem";
            useScreenshotsToolStripMenuItem.Size = new Size(195, 24);
            useScreenshotsToolStripMenuItem.Text = "Use screenshots";
            useScreenshotsToolStripMenuItem.Click += useScreenshotsToolStripMenuItem_Click;
            // 
            // useBigIconsToolStripMenuItem
            // 
            useBigIconsToolStripMenuItem.Name = "useBigIconsToolStripMenuItem";
            useBigIconsToolStripMenuItem.Size = new Size(195, 24);
            useBigIconsToolStripMenuItem.Text = "Use big icons";
            useBigIconsToolStripMenuItem.Click += useBigIconsToolStripMenuItem_Click;
            // 
            // backgroundColorToolStripMenuItem
            // 
            backgroundColorToolStripMenuItem.Name = "backgroundColorToolStripMenuItem";
            backgroundColorToolStripMenuItem.Size = new Size(195, 24);
            backgroundColorToolStripMenuItem.Text = "Background color";
            backgroundColorToolStripMenuItem.Click += backgroundColorToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(177, 6);
            // 
            // applicationToolStripMenuItem
            // 
            applicationToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { consoleToolStripMenuItem, exitToolStripMenuItem });
            applicationToolStripMenuItem.Name = "applicationToolStripMenuItem";
            applicationToolStripMenuItem.Size = new Size(180, 24);
            applicationToolStripMenuItem.Text = "Application";
            // 
            // consoleToolStripMenuItem
            // 
            consoleToolStripMenuItem.Name = "consoleToolStripMenuItem";
            consoleToolStripMenuItem.Size = new Size(131, 24);
            consoleToolStripMenuItem.Text = "Console";
            consoleToolStripMenuItem.Click += consoleToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(131, 24);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // FormTaskbar
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(50, 53);
            ContextMenuStrip = contextMenuStripTaskbar;
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MinimumSize = new Size(50, 53);
            Name = "FormTaskbar";
            StartPosition = FormStartPosition.Manual;
            Text = "Taskbar";
            TopMost = true;
            Activated += Form_Activated;
            Deactivate += Form_Deactivate;
            FormClosing += FormTaskbar_FormClosing;
            FormClosed += FormTaskbar_FormClosed;
            Load += FormTaskbar_Load;
            Paint += FormTaskbar_Paint;
            MouseDown += FormTaskbar_MouseDown;
            MouseMove += FormTaskbar_MouseMove;
            MouseUp += FormTaskbar_MouseUp;
            contextMenuStripTaskbar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTaskbar;
        private System.Windows.Forms.ToolStripMenuItem applicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mostTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lockToolStripMenuItem;
        private ToolStripMenuItem taskbarToolStripMenuItem;
        private ToolStripMenuItem removeToolStripMenuItem;
        private ToolStripMenuItem windowToolStripMenuItem;
        private ToolStripMenuItem minimalizeToolStripMenuItem;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem useScreenshotsToolStripMenuItem;
        private ToolStripMenuItem useBigIconsToolStripMenuItem;
        private ToolStripMenuItem backgroundColorToolStripMenuItem;
        private ToolStripMenuItem showDesktopToolStripMenuItem;
        private ToolStripMenuItem hiddeToolStripMenuItem;
        private ToolStripMenuItem infoToolStripMenuItem;
        private ToolStripMenuItem showAllHiddenToolStripMenuItem;
        private ToolStripMenuItem consoleToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
    }
}