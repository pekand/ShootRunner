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
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 50);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.closeToolStripMenuItem.Text = "Remove pin";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // FormPin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPin";
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
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    }
}