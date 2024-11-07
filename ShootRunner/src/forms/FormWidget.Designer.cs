namespace ShootRunner
{
    partial class FormWidget
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWidget));
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.widgetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newWidgetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.typeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mostTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lockedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transparentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.widgetToolStripMenuItem,
            this.applicationToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(147, 76);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // widgetToolStripMenuItem
            // 
            this.widgetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newWidgetToolStripMenuItem,
            this.editToolStripMenuItem,
            this.reloadToolStripMenuItem,
            this.typeToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.widgetToolStripMenuItem.Name = "widgetToolStripMenuItem";
            this.widgetToolStripMenuItem.Size = new System.Drawing.Size(146, 24);
            this.widgetToolStripMenuItem.Text = "Widget";
            // 
            // newWidgetToolStripMenuItem
            // 
            this.newWidgetToolStripMenuItem.Name = "newWidgetToolStripMenuItem";
            this.newWidgetToolStripMenuItem.Size = new System.Drawing.Size(150, 24);
            this.newWidgetToolStripMenuItem.Text = "New widget";
            this.newWidgetToolStripMenuItem.Click += new System.EventHandler(this.newWidgetToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(150, 24);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(150, 24);
            this.reloadToolStripMenuItem.Text = "Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // typeToolStripMenuItem
            // 
            this.typeToolStripMenuItem.Name = "typeToolStripMenuItem";
            this.typeToolStripMenuItem.Size = new System.Drawing.Size(150, 24);
            this.typeToolStripMenuItem.Text = "Type";
            this.typeToolStripMenuItem.Click += new System.EventHandler(this.typeToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(150, 24);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeWidgetToolStripMenuItem_Click);
            // 
            // applicationToolStripMenuItem
            // 
            this.applicationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.applicationToolStripMenuItem.Name = "applicationToolStripMenuItem";
            this.applicationToolStripMenuItem.Size = new System.Drawing.Size(146, 24);
            this.applicationToolStripMenuItem.Text = "Application";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(99, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mostTopToolStripMenuItem,
            this.lockedToolStripMenuItem,
            this.transparentToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(146, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // mostTopToolStripMenuItem
            // 
            this.mostTopToolStripMenuItem.Name = "mostTopToolStripMenuItem";
            this.mostTopToolStripMenuItem.Size = new System.Drawing.Size(150, 24);
            this.mostTopToolStripMenuItem.Text = "Most top";
            this.mostTopToolStripMenuItem.Click += new System.EventHandler(this.mostTopToolStripMenuItem_Click);
            // 
            // lockedToolStripMenuItem
            // 
            this.lockedToolStripMenuItem.Name = "lockedToolStripMenuItem";
            this.lockedToolStripMenuItem.Size = new System.Drawing.Size(150, 24);
            this.lockedToolStripMenuItem.Text = "Locked";
            this.lockedToolStripMenuItem.Click += new System.EventHandler(this.lockedToolStripMenuItem_Click);
            // 
            // transparentToolStripMenuItem
            // 
            this.transparentToolStripMenuItem.Name = "transparentToolStripMenuItem";
            this.transparentToolStripMenuItem.Size = new System.Drawing.Size(150, 24);
            this.transparentToolStripMenuItem.Text = "Transparent";
            this.transparentToolStripMenuItem.Click += new System.EventHandler(this.transparentToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormWidget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormWidget";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Widget";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormWidget_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormWidget_FormClosed);
            this.Load += new System.EventHandler(this.FormWidget_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mostTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lockedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transparentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem widgetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newWidgetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem typeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
    }
}