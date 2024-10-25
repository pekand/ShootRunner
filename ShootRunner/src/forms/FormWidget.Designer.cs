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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeWidgetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newWidgetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mostTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lockedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transparentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeWidgetToolStripMenuItem,
            this.newWidgetToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(173, 76);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // removeWidgetToolStripMenuItem
            // 
            this.removeWidgetToolStripMenuItem.Name = "removeWidgetToolStripMenuItem";
            this.removeWidgetToolStripMenuItem.Size = new System.Drawing.Size(172, 24);
            this.removeWidgetToolStripMenuItem.Text = "Remove widget";
            this.removeWidgetToolStripMenuItem.Click += new System.EventHandler(this.removeWidgetToolStripMenuItem_Click);
            // 
            // newWidgetToolStripMenuItem
            // 
            this.newWidgetToolStripMenuItem.Name = "newWidgetToolStripMenuItem";
            this.newWidgetToolStripMenuItem.Size = new System.Drawing.Size(172, 24);
            this.newWidgetToolStripMenuItem.Text = "New widget";
            this.newWidgetToolStripMenuItem.Click += new System.EventHandler(this.newWidgetToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mostTopToolStripMenuItem,
            this.lockedToolStripMenuItem,
            this.transparentToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(172, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // mostTopToolStripMenuItem
            // 
            this.mostTopToolStripMenuItem.Name = "mostTopToolStripMenuItem";
            this.mostTopToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.mostTopToolStripMenuItem.Text = "Most top";
            this.mostTopToolStripMenuItem.Click += new System.EventHandler(this.mostTopToolStripMenuItem_Click);
            // 
            // lockedToolStripMenuItem
            // 
            this.lockedToolStripMenuItem.Name = "lockedToolStripMenuItem";
            this.lockedToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.lockedToolStripMenuItem.Text = "Locked";
            this.lockedToolStripMenuItem.Click += new System.EventHandler(this.lockedToolStripMenuItem_Click);
            // 
            // transparentToolStripMenuItem
            // 
            this.transparentToolStripMenuItem.Name = "transparentToolStripMenuItem";
            this.transparentToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.transparentToolStripMenuItem.Text = "Transparent";
            this.transparentToolStripMenuItem.Click += new System.EventHandler(this.transparentToolStripMenuItem_Click);
            // 
            // FormWidget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormWidget";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Widget";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormWidget_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormWidget_FormClosed);
            this.Load += new System.EventHandler(this.FormWidget_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem removeWidgetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newWidgetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mostTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lockedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transparentToolStripMenuItem;
    }
}