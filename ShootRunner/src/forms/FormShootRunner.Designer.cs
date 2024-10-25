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
            this.commandsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcutFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autorunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.newPinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.commandsToolStripMenuItem,
            this.newPinToolStripMenuItem,
            this.errorLogToolStripMenuItem,
            this.shortcutFormToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 170);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // commandsToolStripMenuItem
            // 
            this.commandsToolStripMenuItem.Name = "commandsToolStripMenuItem";
            this.commandsToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.commandsToolStripMenuItem.Text = "Commands";
            this.commandsToolStripMenuItem.Click += new System.EventHandler(this.commandsToolStripMenuItem_Click);
            // 
            // errorLogToolStripMenuItem
            // 
            this.errorLogToolStripMenuItem.Name = "errorLogToolStripMenuItem";
            this.errorLogToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.errorLogToolStripMenuItem.Text = "Error log";
            this.errorLogToolStripMenuItem.Click += new System.EventHandler(this.errorLogToolStripMenuItem_Click);
            // 
            // shortcutFormToolStripMenuItem
            // 
            this.shortcutFormToolStripMenuItem.Name = "shortcutFormToolStripMenuItem";
            this.shortcutFormToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.shortcutFormToolStripMenuItem.Text = "Shortcut form";
            this.shortcutFormToolStripMenuItem.Click += new System.EventHandler(this.shortcutFormToolStripMenuItem_Click);
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
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // newPinToolStripMenuItem
            // 
            this.newPinToolStripMenuItem.Name = "newPinToolStripMenuItem";
            this.newPinToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.newPinToolStripMenuItem.Text = "New pin";
            this.newPinToolStripMenuItem.Click += new System.EventHandler(this.newPinToolStripMenuItem_Click);
            // 
            // FormShootRunner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormShootRunner";
            this.Text = "Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormShootRunner_FormClosed);
            this.Load += new System.EventHandler(this.FormShootRunner_Load);
            this.InputLanguageChanging += new System.Windows.Forms.InputLanguageChangingEventHandler(this.FormShootRunner_InputLanguageChanging);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIconShootRunner;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autorunToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commandsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem errorLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shortcutFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newPinToolStripMenuItem;
    }
}

