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
            timer = new System.Windows.Forms.Timer(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
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
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // timer
            // 
            timer.Enabled = true;
            timer.Interval = 1000;
            timer.Tick += timer_Tick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { taskbarToolStripMenuItem, windowToolStripMenuItem, optionsToolStripMenuItem, toolStripMenuItem1, applicationToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(147, 106);
            contextMenuStrip1.Opening += contextMenuStrip1_Opening;
            // 
            // taskbarToolStripMenuItem
            // 
            taskbarToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { removeToolStripMenuItem, showAllHiddenToolStripMenuItem });
            taskbarToolStripMenuItem.Name = "taskbarToolStripMenuItem";
            taskbarToolStripMenuItem.Size = new Size(146, 24);
            taskbarToolStripMenuItem.Text = "Taskbar";
            // 
            // removeToolStripMenuItem
            // 
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            removeToolStripMenuItem.Size = new Size(174, 24);
            removeToolStripMenuItem.Text = "Remove";
            removeToolStripMenuItem.Click += removeToolStripMenuItem_Click;
            // 
            // showAllHiddenToolStripMenuItem
            // 
            showAllHiddenToolStripMenuItem.Name = "showAllHiddenToolStripMenuItem";
            showAllHiddenToolStripMenuItem.Size = new Size(174, 24);
            showAllHiddenToolStripMenuItem.Text = "Show all hidden";
            showAllHiddenToolStripMenuItem.Click += showAllHiddenToolStripMenuItem_Click;
            // 
            // windowToolStripMenuItem
            // 
            windowToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { minimalizeToolStripMenuItem, infoToolStripMenuItem, showDesktopToolStripMenuItem, hiddeToolStripMenuItem, closeToolStripMenuItem });
            windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            windowToolStripMenuItem.Size = new Size(146, 24);
            windowToolStripMenuItem.Text = "Window";
            windowToolStripMenuItem.Click += windowToolStripMenuItem_Click;
            // 
            // minimalizeToolStripMenuItem
            // 
            minimalizeToolStripMenuItem.Name = "minimalizeToolStripMenuItem";
            minimalizeToolStripMenuItem.Size = new Size(164, 24);
            minimalizeToolStripMenuItem.Text = "Minimalize";
            minimalizeToolStripMenuItem.Click += minimalizeToolStripMenuItem_Click;
            // 
            // infoToolStripMenuItem
            // 
            infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            infoToolStripMenuItem.Size = new Size(164, 24);
            infoToolStripMenuItem.Text = "Info";
            infoToolStripMenuItem.Click += infoToolStripMenuItem_Click;
            // 
            // showDesktopToolStripMenuItem
            // 
            showDesktopToolStripMenuItem.Name = "showDesktopToolStripMenuItem";
            showDesktopToolStripMenuItem.Size = new Size(164, 24);
            showDesktopToolStripMenuItem.Text = "Show desktop";
            showDesktopToolStripMenuItem.Click += showDesktopToolStripMenuItem_Click;
            // 
            // hiddeToolStripMenuItem
            // 
            hiddeToolStripMenuItem.Name = "hiddeToolStripMenuItem";
            hiddeToolStripMenuItem.Size = new Size(164, 24);
            hiddeToolStripMenuItem.Text = "Hidde";
            hiddeToolStripMenuItem.Click += hiddeToolStripMenuItem_Click;
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(164, 24);
            closeToolStripMenuItem.Text = "Close";
            closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mostTopToolStripMenuItem, lockToolStripMenuItem, useScreenshotsToolStripMenuItem, useBigIconsToolStripMenuItem, backgroundColorToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(146, 24);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // mostTopToolStripMenuItem
            // 
            mostTopToolStripMenuItem.Name = "mostTopToolStripMenuItem";
            mostTopToolStripMenuItem.Size = new Size(185, 24);
            mostTopToolStripMenuItem.Text = "Most top";
            mostTopToolStripMenuItem.Click += mostTopToolStripMenuItem_Click;
            // 
            // lockToolStripMenuItem
            // 
            lockToolStripMenuItem.Name = "lockToolStripMenuItem";
            lockToolStripMenuItem.Size = new Size(185, 24);
            lockToolStripMenuItem.Text = "Locked";
            lockToolStripMenuItem.Click += lockToolStripMenuItem_Click;
            // 
            // useScreenshotsToolStripMenuItem
            // 
            useScreenshotsToolStripMenuItem.Name = "useScreenshotsToolStripMenuItem";
            useScreenshotsToolStripMenuItem.Size = new Size(185, 24);
            useScreenshotsToolStripMenuItem.Text = "Use screenshots";
            useScreenshotsToolStripMenuItem.Click += useScreenshotsToolStripMenuItem_Click;
            // 
            // useBigIconsToolStripMenuItem
            // 
            useBigIconsToolStripMenuItem.Name = "useBigIconsToolStripMenuItem";
            useBigIconsToolStripMenuItem.Size = new Size(185, 24);
            useBigIconsToolStripMenuItem.Text = "Use big icons";
            useBigIconsToolStripMenuItem.Click += useBigIconsToolStripMenuItem_Click;
            // 
            // backgroundColorToolStripMenuItem
            // 
            backgroundColorToolStripMenuItem.Name = "backgroundColorToolStripMenuItem";
            backgroundColorToolStripMenuItem.Size = new Size(185, 24);
            backgroundColorToolStripMenuItem.Text = "Background color";
            backgroundColorToolStripMenuItem.Click += backgroundColorToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(143, 6);
            // 
            // applicationToolStripMenuItem
            // 
            applicationToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { consoleToolStripMenuItem, exitToolStripMenuItem });
            applicationToolStripMenuItem.Name = "applicationToolStripMenuItem";
            applicationToolStripMenuItem.Size = new Size(146, 24);
            applicationToolStripMenuItem.Text = "Application";
            // 
            // consoleToolStripMenuItem
            // 
            consoleToolStripMenuItem.Name = "consoleToolStripMenuItem";
            consoleToolStripMenuItem.Size = new Size(127, 24);
            consoleToolStripMenuItem.Text = "Console";
            consoleToolStripMenuItem.Click += consoleToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(127, 24);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // FormTaskbar
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(50, 50);
            ContextMenuStrip = contextMenuStrip1;
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MinimumSize = new Size(50, 50);
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
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
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