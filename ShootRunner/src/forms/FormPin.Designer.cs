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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPin));
            contextMenuStrip1 = new ContextMenuStrip(components);
            pinToolStripMenuItem = new ToolStripMenuItem();
            removeToolStripMenuItem = new ToolStripMenuItem();
            newPinToolStripMenuItem = new ToolStripMenuItem();
            runToolStripMenuItem = new ToolStripMenuItem();
            minimalizeToolStripMenuItem = new ToolStripMenuItem();
            selectToolStripMenuItem = new ToolStripMenuItem();
            commandToolStripMenuItem = new ToolStripMenuItem();
            setCommandToolStripMenuItem1 = new ToolStripMenuItem();
            widgetToolStripMenuItem = new ToolStripMenuItem();
            newWidgetToolStripMenuItem = new ToolStripMenuItem();
            applicationToolStripMenuItem = new ToolStripMenuItem();
            closeToolStripMenuItem1 = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            setIconToolStripMenuItem1 = new ToolStripMenuItem();
            dobleClickToActivateToolStripMenuItem = new ToolStripMenuItem();
            transparentToolStripMenuItem = new ToolStripMenuItem();
            openFileDialog1 = new OpenFileDialog();
            timer1 = new System.Windows.Forms.Timer(components);
            taskbarToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { pinToolStripMenuItem, commandToolStripMenuItem, widgetToolStripMenuItem, applicationToolStripMenuItem, optionsToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(181, 146);
            contextMenuStrip1.Opening += contextMenuStrip1_Opening;
            // 
            // pinToolStripMenuItem
            // 
            pinToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { removeToolStripMenuItem, newPinToolStripMenuItem, runToolStripMenuItem, minimalizeToolStripMenuItem, selectToolStripMenuItem });
            pinToolStripMenuItem.Name = "pinToolStripMenuItem";
            pinToolStripMenuItem.Size = new Size(180, 24);
            pinToolStripMenuItem.Text = "Pin";
            // 
            // removeToolStripMenuItem
            // 
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            removeToolStripMenuItem.Size = new Size(180, 24);
            removeToolStripMenuItem.Text = "Remove";
            removeToolStripMenuItem.Click += removeToolStripMenuItem_Click;
            // 
            // newPinToolStripMenuItem
            // 
            newPinToolStripMenuItem.Name = "newPinToolStripMenuItem";
            newPinToolStripMenuItem.Size = new Size(180, 24);
            newPinToolStripMenuItem.Text = "New pin";
            newPinToolStripMenuItem.Click += newPinToolStripMenuItem_Click;
            // 
            // runToolStripMenuItem
            // 
            runToolStripMenuItem.Name = "runToolStripMenuItem";
            runToolStripMenuItem.Size = new Size(180, 24);
            runToolStripMenuItem.Text = "Duplicate";
            runToolStripMenuItem.Click += duplicateToolStripMenuItem_Click;
            // 
            // minimalizeToolStripMenuItem
            // 
            minimalizeToolStripMenuItem.Name = "minimalizeToolStripMenuItem";
            minimalizeToolStripMenuItem.Size = new Size(180, 24);
            minimalizeToolStripMenuItem.Text = "Minimalize";
            minimalizeToolStripMenuItem.Click += minimalizeToolStripMenuItem_Click_1;
            // 
            // selectToolStripMenuItem
            // 
            selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            selectToolStripMenuItem.Size = new Size(180, 24);
            selectToolStripMenuItem.Text = "Select";
            selectToolStripMenuItem.Click += selectToolStripMenuItem_Click;
            // 
            // commandToolStripMenuItem
            // 
            commandToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { setCommandToolStripMenuItem1 });
            commandToolStripMenuItem.Name = "commandToolStripMenuItem";
            commandToolStripMenuItem.Size = new Size(180, 24);
            commandToolStripMenuItem.Text = "Command";
            // 
            // setCommandToolStripMenuItem1
            // 
            setCommandToolStripMenuItem1.Name = "setCommandToolStripMenuItem1";
            setCommandToolStripMenuItem1.Size = new Size(180, 24);
            setCommandToolStripMenuItem1.Text = "Set command";
            setCommandToolStripMenuItem1.Click += setCommandToolStripMenuItem_Click;
            // 
            // widgetToolStripMenuItem
            // 
            widgetToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newWidgetToolStripMenuItem, taskbarToolStripMenuItem });
            widgetToolStripMenuItem.Name = "widgetToolStripMenuItem";
            widgetToolStripMenuItem.Size = new Size(180, 24);
            widgetToolStripMenuItem.Text = "Widget";
            // 
            // newWidgetToolStripMenuItem
            // 
            newWidgetToolStripMenuItem.Name = "newWidgetToolStripMenuItem";
            newWidgetToolStripMenuItem.Size = new Size(180, 24);
            newWidgetToolStripMenuItem.Text = "New widget";
            newWidgetToolStripMenuItem.Click += newWidgetToolStripMenuItem_Click;
            // 
            // applicationToolStripMenuItem
            // 
            applicationToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { closeToolStripMenuItem1 });
            applicationToolStripMenuItem.Name = "applicationToolStripMenuItem";
            applicationToolStripMenuItem.Size = new Size(180, 24);
            applicationToolStripMenuItem.Text = "Application";
            // 
            // closeToolStripMenuItem1
            // 
            closeToolStripMenuItem1.Name = "closeToolStripMenuItem1";
            closeToolStripMenuItem1.Size = new Size(180, 24);
            closeToolStripMenuItem1.Text = "Exit";
            closeToolStripMenuItem1.Click += closeToolStripMenuItem1_Click_1;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { setIconToolStripMenuItem1, dobleClickToActivateToolStripMenuItem, transparentToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(180, 24);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // setIconToolStripMenuItem1
            // 
            setIconToolStripMenuItem1.Name = "setIconToolStripMenuItem1";
            setIconToolStripMenuItem1.Size = new Size(211, 24);
            setIconToolStripMenuItem1.Text = "Set Icon";
            setIconToolStripMenuItem1.Click += setIconToolStripMenuItem_Click;
            // 
            // dobleClickToActivateToolStripMenuItem
            // 
            dobleClickToActivateToolStripMenuItem.Name = "dobleClickToActivateToolStripMenuItem";
            dobleClickToActivateToolStripMenuItem.Size = new Size(211, 24);
            dobleClickToActivateToolStripMenuItem.Text = "Doble click to activate";
            dobleClickToActivateToolStripMenuItem.Click += dobleClickToActivateToolStripMenuItem_Click;
            // 
            // transparentToolStripMenuItem
            // 
            transparentToolStripMenuItem.Name = "transparentToolStripMenuItem";
            transparentToolStripMenuItem.Size = new Size(211, 24);
            transparentToolStripMenuItem.Text = "Transparent";
            transparentToolStripMenuItem.Click += transparentToolStripMenuItem_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Filter = "Image Files|*.ico;*.bmp;*.jpg;*.jpeg;*.png;";
            openFileDialog1.Title = "Select icon";
            // 
            // taskbarToolStripMenuItem
            // 
            taskbarToolStripMenuItem.Name = "taskbarToolStripMenuItem";
            taskbarToolStripMenuItem.Size = new Size(180, 24);
            taskbarToolStripMenuItem.Text = "Taskbar";
            taskbarToolStripMenuItem.Click += taskbarToolStripMenuItem_Click;
            // 
            // FormPin
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1067, 658);
            ContextMenuStrip = contextMenuStrip1;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 4, 4, 4);
            Name = "FormPin";
            StartPosition = FormStartPosition.Manual;
            Text = "FormPin";
            Load += FormPin_Load;
            ResizeBegin += FormPin_ResizeBegin;
            ResizeEnd += FormPin_ResizeEnd;
            Paint += FormPin_Paint;
            MouseDoubleClick += FormPin_MouseDoubleClick;
            MouseDown += FormPin_MouseDown;
            MouseMove += FormPin_MouseMove;
            MouseUp += FormPin_MouseUp;
            Resize += FormPin_Resize;
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem minimalizeToolStripMenuItem;
        private ToolStripMenuItem taskbarToolStripMenuItem;
    }
}