namespace ShootRunner
{
    partial class FormShootRunner
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormShootRunner));
            this.notifyIconShootRunner = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.commandsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.editCommandsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcutFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pinsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newPinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.widgetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newWidgetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autorunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.taskbarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIconShootRunner
            // 
            this.notifyIconShootRunner.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIconShootRunner.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconShootRunner.Icon")));
            this.notifyIconShootRunner.Text = "ShootRunner";
            this.notifyIconShootRunner.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.commandsToolStripMenuItem1,
            this.pinsToolStripMenuItem,
            this.widgetsToolStripMenuItem,
            this.applicationToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.taskbarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 170);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // commandsToolStripMenuItem1
            // 
            this.commandsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editCommandsToolStripMenuItem,
            this.shortcutFormToolStripMenuItem});
            this.commandsToolStripMenuItem1.Name = "commandsToolStripMenuItem1";
            this.commandsToolStripMenuItem1.Size = new System.Drawing.Size(180, 24);
            this.commandsToolStripMenuItem1.Text = "Commands";
            // 
            // editCommandsToolStripMenuItem
            // 
            this.editCommandsToolStripMenuItem.Name = "editCommandsToolStripMenuItem";
            this.editCommandsToolStripMenuItem.Size = new System.Drawing.Size(172, 24);
            this.editCommandsToolStripMenuItem.Text = "Edit commands";
            this.editCommandsToolStripMenuItem.Click += new System.EventHandler(this.commandsToolStripMenuItem_Click);
            // 
            // shortcutFormToolStripMenuItem
            // 
            this.shortcutFormToolStripMenuItem.Name = "shortcutFormToolStripMenuItem";
            this.shortcutFormToolStripMenuItem.Size = new System.Drawing.Size(172, 24);
            this.shortcutFormToolStripMenuItem.Text = "Shortcut form";
            this.shortcutFormToolStripMenuItem.Click += new System.EventHandler(this.shortcutFormToolStripMenuItem_Click);
            // 
            // pinsToolStripMenuItem
            // 
            this.pinsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newPinToolStripMenuItem});
            this.pinsToolStripMenuItem.Name = "pinsToolStripMenuItem";
            this.pinsToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.pinsToolStripMenuItem.Text = "Pins";
            // 
            // newPinToolStripMenuItem
            // 
            this.newPinToolStripMenuItem.Name = "newPinToolStripMenuItem";
            this.newPinToolStripMenuItem.Size = new System.Drawing.Size(128, 24);
            this.newPinToolStripMenuItem.Text = "New pin";
            this.newPinToolStripMenuItem.Click += new System.EventHandler(this.newPinToolStripMenuItem_Click);
            // 
            // widgetsToolStripMenuItem
            // 
            this.widgetsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newWidgetToolStripMenuItem});
            this.widgetsToolStripMenuItem.Name = "widgetsToolStripMenuItem";
            this.widgetsToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.widgetsToolStripMenuItem.Text = "Widgets";
            // 
            // newWidgetToolStripMenuItem
            // 
            this.newWidgetToolStripMenuItem.Name = "newWidgetToolStripMenuItem";
            this.newWidgetToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.newWidgetToolStripMenuItem.Text = "New widget";
            this.newWidgetToolStripMenuItem.Click += new System.EventHandler(this.newWidgetToolStripMenuItem_Click);
            // 
            // applicationToolStripMenuItem
            // 
            this.applicationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.errorLogToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.applicationToolStripMenuItem.Name = "applicationToolStripMenuItem";
            this.applicationToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.applicationToolStripMenuItem.Text = "Application";
            // 
            // errorLogToolStripMenuItem
            // 
            this.errorLogToolStripMenuItem.Name = "errorLogToolStripMenuItem";
            this.errorLogToolStripMenuItem.Size = new System.Drawing.Size(131, 24);
            this.errorLogToolStripMenuItem.Text = "Error log";
            this.errorLogToolStripMenuItem.Click += new System.EventHandler(this.errorLogToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(131, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autorunToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // autorunToolStripMenuItem
            // 
            this.autorunToolStripMenuItem.Name = "autorunToolStripMenuItem";
            this.autorunToolStripMenuItem.Size = new System.Drawing.Size(129, 24);
            this.autorunToolStripMenuItem.Text = "Autorun";
            this.autorunToolStripMenuItem.Click += new System.EventHandler(this.autorunToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // taskbarToolStripMenuItem
            // 
            this.taskbarToolStripMenuItem.Name = "taskbarToolStripMenuItem";
            this.taskbarToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.taskbarToolStripMenuItem.Text = "Taskbar ";
            this.taskbarToolStripMenuItem.Click += new System.EventHandler(this.taskbarToolStripMenuItem_Click);
            // 
            // FormShootRunner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormShootRunner";
            this.Text = "Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormShootRunner_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormShootRunner_FormClosed);
            this.Load += new System.EventHandler(this.FormShootRunner_Load);
            this.InputLanguageChanging += new System.Windows.Forms.InputLanguageChangingEventHandler(this.FormShootRunner_InputLanguageChanging);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIconShootRunner;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autorunToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem widgetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newWidgetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commandsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pinsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shortcutFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editCommandsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem errorLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newPinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem taskbarToolStripMenuItem;
    }
}

