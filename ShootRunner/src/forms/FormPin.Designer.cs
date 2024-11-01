namespace ShootRunner
{
    partial class FormPin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPin));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setCommandToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.widgetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newWidgetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setIconToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dobleClickToActivateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transparentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.newPinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pinToolStripMenuItem,
            this.commandToolStripMenuItem,
            this.widgetToolStripMenuItem,
            this.applicationToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(147, 124);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // pinToolStripMenuItem
            // 
            this.pinToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem,
            this.selectToolStripMenuItem,
            this.newPinToolStripMenuItem});
            this.pinToolStripMenuItem.Name = "pinToolStripMenuItem";
            this.pinToolStripMenuItem.Size = new System.Drawing.Size(146, 24);
            this.pinToolStripMenuItem.Text = "Pin";
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // selectToolStripMenuItem
            // 
            this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            this.selectToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.selectToolStripMenuItem.Text = "Select";
            this.selectToolStripMenuItem.Click += new System.EventHandler(this.selectToolStripMenuItem_Click);
            // 
            // commandToolStripMenuItem
            // 
            this.commandToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setCommandToolStripMenuItem1});
            this.commandToolStripMenuItem.Name = "commandToolStripMenuItem";
            this.commandToolStripMenuItem.Size = new System.Drawing.Size(146, 24);
            this.commandToolStripMenuItem.Text = "Command";
            // 
            // setCommandToolStripMenuItem1
            // 
            this.setCommandToolStripMenuItem1.Name = "setCommandToolStripMenuItem1";
            this.setCommandToolStripMenuItem1.Size = new System.Drawing.Size(162, 24);
            this.setCommandToolStripMenuItem1.Text = "Set command";
            this.setCommandToolStripMenuItem1.Click += new System.EventHandler(this.setCommandToolStripMenuItem_Click);
            // 
            // widgetToolStripMenuItem
            // 
            this.widgetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newWidgetToolStripMenuItem});
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
            // applicationToolStripMenuItem
            // 
            this.applicationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem1});
            this.applicationToolStripMenuItem.Name = "applicationToolStripMenuItem";
            this.applicationToolStripMenuItem.Size = new System.Drawing.Size(146, 24);
            this.applicationToolStripMenuItem.Text = "Application";
            // 
            // closeToolStripMenuItem1
            // 
            this.closeToolStripMenuItem1.Name = "closeToolStripMenuItem1";
            this.closeToolStripMenuItem1.Size = new System.Drawing.Size(180, 24);
            this.closeToolStripMenuItem1.Text = "Exit";
            this.closeToolStripMenuItem1.Click += new System.EventHandler(this.closeToolStripMenuItem1_Click_1);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setIconToolStripMenuItem1,
            this.dobleClickToActivateToolStripMenuItem,
            this.transparentToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(146, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // setIconToolStripMenuItem1
            // 
            this.setIconToolStripMenuItem1.Name = "setIconToolStripMenuItem1";
            this.setIconToolStripMenuItem1.Size = new System.Drawing.Size(211, 24);
            this.setIconToolStripMenuItem1.Text = "Set Icon";
            this.setIconToolStripMenuItem1.Click += new System.EventHandler(this.setIconToolStripMenuItem_Click);
            // 
            // dobleClickToActivateToolStripMenuItem
            // 
            this.dobleClickToActivateToolStripMenuItem.Name = "dobleClickToActivateToolStripMenuItem";
            this.dobleClickToActivateToolStripMenuItem.Size = new System.Drawing.Size(211, 24);
            this.dobleClickToActivateToolStripMenuItem.Text = "Doble click to activate";
            this.dobleClickToActivateToolStripMenuItem.Click += new System.EventHandler(this.dobleClickToActivateToolStripMenuItem_Click);
            // 
            // transparentToolStripMenuItem
            // 
            this.transparentToolStripMenuItem.Name = "transparentToolStripMenuItem";
            this.transparentToolStripMenuItem.Size = new System.Drawing.Size(211, 24);
            this.transparentToolStripMenuItem.Text = "Transparent";
            this.transparentToolStripMenuItem.Click += new System.EventHandler(this.transparentToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Image Files|*.ico;*.bmp;*.jpg;*.jpeg;*.png;";
            this.openFileDialog1.Title = "Select icon";
            // 
            // newPinToolStripMenuItem
            // 
            this.newPinToolStripMenuItem.Name = "newPinToolStripMenuItem";
            this.newPinToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.newPinToolStripMenuItem.Text = "New pin";
            this.newPinToolStripMenuItem.Click += new System.EventHandler(this.newWidgetToolStripMenuItem_Click);
            // 
            // FormPin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormPin";
            this.Load += new System.EventHandler(this.FormPin_Load);
            this.ResizeBegin += new System.EventHandler(this.FormPin_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.FormPin_ResizeEnd);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormPin_Paint);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FormPin_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormPin_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormPin_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormPin_MouseUp);
            this.Resize += new System.EventHandler(this.FormPin_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem commandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setCommandToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setIconToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem dobleClickToActivateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem widgetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newWidgetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transparentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newPinToolStripMenuItem;
    }
}