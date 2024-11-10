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
            timer1 = new System.Windows.Forms.Timer(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            windowToolStripMenuItem = new ToolStripMenuItem();
            minimalizeToolStripMenuItem = new ToolStripMenuItem();
            closeToolStripMenuItem = new ToolStripMenuItem();
            taskbarToolStripMenuItem = new ToolStripMenuItem();
            removeToolStripMenuItem = new ToolStripMenuItem();
            applicationToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            mostTopToolStripMenuItem = new ToolStripMenuItem();
            lockToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Tick += timer1_Tick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { windowToolStripMenuItem, taskbarToolStripMenuItem, applicationToolStripMenuItem, optionsToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(147, 100);
            contextMenuStrip1.Opening += contextMenuStrip1_Opening;
            // 
            // windowToolStripMenuItem
            // 
            windowToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { minimalizeToolStripMenuItem, closeToolStripMenuItem });
            windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            windowToolStripMenuItem.Size = new Size(146, 24);
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
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(180, 24);
            closeToolStripMenuItem.Text = "Close";
            closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
            // 
            // taskbarToolStripMenuItem
            // 
            taskbarToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { removeToolStripMenuItem });
            taskbarToolStripMenuItem.Name = "taskbarToolStripMenuItem";
            taskbarToolStripMenuItem.Size = new Size(146, 24);
            taskbarToolStripMenuItem.Text = "Taskbar";
            // 
            // removeToolStripMenuItem
            // 
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            removeToolStripMenuItem.Size = new Size(127, 24);
            removeToolStripMenuItem.Text = "Remove";
            removeToolStripMenuItem.Click += removeToolStripMenuItem_Click;
            // 
            // applicationToolStripMenuItem
            // 
            applicationToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exitToolStripMenuItem });
            applicationToolStripMenuItem.Name = "applicationToolStripMenuItem";
            applicationToolStripMenuItem.Size = new Size(146, 24);
            applicationToolStripMenuItem.Text = "Application";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(99, 24);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mostTopToolStripMenuItem, lockToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(146, 24);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // mostTopToolStripMenuItem
            // 
            mostTopToolStripMenuItem.Name = "mostTopToolStripMenuItem";
            mostTopToolStripMenuItem.Size = new Size(135, 24);
            mostTopToolStripMenuItem.Text = "Most top";
            mostTopToolStripMenuItem.Click += mostTopToolStripMenuItem_Click;
            // 
            // lockToolStripMenuItem
            // 
            lockToolStripMenuItem.Name = "lockToolStripMenuItem";
            lockToolStripMenuItem.Size = new Size(135, 24);
            lockToolStripMenuItem.Text = "Locked";
            lockToolStripMenuItem.Click += lockToolStripMenuItem_Click;
            // 
            // FormTaskbar
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1155, 140);
            ContextMenuStrip = contextMenuStrip1;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "FormTaskbar";
            StartPosition = FormStartPosition.Manual;
            Text = "Taskbar";
            TopMost = true;
            Activated += Form_Activated;
            Deactivate += Form_Deactivate;
            FormClosing += FormTaskbar_FormClosing;
            Load += FormTaskbar_Load;
            Paint += FormTaskbar_Paint;
            MouseDown += FormTaskbar_MouseDown;
            MouseMove += FormTaskbar_MouseMove;
            MouseUp += FormTaskbar_MouseUp;
            Resize += FormTaskbar_Resize;
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Timer timer1;
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
    }
}