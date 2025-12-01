#pragma warning disable IDE0130

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
            contextMenuStrip = new ContextMenuStrip(components);
            pinToolStripMenuItem = new ToolStripMenuItem();
            selectToolStripMenuItem = new ToolStripMenuItem();
            commandToolStripMenuItem = new ToolStripMenuItem();
            newPinToolStripMenuItem = new ToolStripMenuItem();
            runToolStripMenuItem = new ToolStripMenuItem();
            minimalizeToolStripMenuItem = new ToolStripMenuItem();
            removeToolStripMenuItem = new ToolStripMenuItem();
            selectAllToolStripMenuItem = new ToolStripMenuItem();
            deselectAllToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            setIconToolStripMenuItem1 = new ToolStripMenuItem();
            dobleClickToActivateToolStripMenuItem = new ToolStripMenuItem();
            transparentToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            widgetToolStripMenuItem = new ToolStripMenuItem();
            newWidgetToolStripMenuItem = new ToolStripMenuItem();
            createWidgetToolStripMenuItem = new ToolStripMenuItem();
            taskbarToolStripMenuItem = new ToolStripMenuItem();
            applicationToolStripMenuItem = new ToolStripMenuItem();
            consoleToolStripMenuItem = new ToolStripMenuItem();
            closeToolStripMenuItem1 = new ToolStripMenuItem();
            openFileDialog = new OpenFileDialog();
            timer = new System.Windows.Forms.Timer(components);
            topMostToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { pinToolStripMenuItem, optionsToolStripMenuItem, toolStripMenuItem1, widgetToolStripMenuItem, applicationToolStripMenuItem });
            contextMenuStrip.Name = "contextMenuStrip1";
            contextMenuStrip.Size = new Size(181, 128);
            contextMenuStrip.Opening += ContextMenuStrip1_Opening;
            // 
            // pinToolStripMenuItem
            // 
            pinToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { selectToolStripMenuItem, commandToolStripMenuItem, newPinToolStripMenuItem, runToolStripMenuItem, minimalizeToolStripMenuItem, removeToolStripMenuItem, selectAllToolStripMenuItem, deselectAllToolStripMenuItem });
            pinToolStripMenuItem.Name = "pinToolStripMenuItem";
            pinToolStripMenuItem.Size = new Size(180, 24);
            pinToolStripMenuItem.Text = "Pin";
            // 
            // selectToolStripMenuItem
            // 
            selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            selectToolStripMenuItem.Size = new Size(155, 24);
            selectToolStripMenuItem.Text = "Select";
            selectToolStripMenuItem.Click += SelectToolStripMenuItem_Click;
            // 
            // commandToolStripMenuItem
            // 
            commandToolStripMenuItem.Name = "commandToolStripMenuItem";
            commandToolStripMenuItem.Size = new Size(155, 24);
            commandToolStripMenuItem.Text = "Command";
            commandToolStripMenuItem.Click += SetCommandToolStripMenuItem_Click;
            // 
            // newPinToolStripMenuItem
            // 
            newPinToolStripMenuItem.Name = "newPinToolStripMenuItem";
            newPinToolStripMenuItem.Size = new Size(155, 24);
            newPinToolStripMenuItem.Text = "New pin";
            newPinToolStripMenuItem.Click += NewPinToolStripMenuItem_Click;
            // 
            // runToolStripMenuItem
            // 
            runToolStripMenuItem.Name = "runToolStripMenuItem";
            runToolStripMenuItem.Size = new Size(155, 24);
            runToolStripMenuItem.Text = "Duplicate";
            runToolStripMenuItem.Click += DuplicateToolStripMenuItem_Click;
            // 
            // minimalizeToolStripMenuItem
            // 
            minimalizeToolStripMenuItem.Name = "minimalizeToolStripMenuItem";
            minimalizeToolStripMenuItem.Size = new Size(155, 24);
            minimalizeToolStripMenuItem.Text = "Minimalize";
            minimalizeToolStripMenuItem.Click += MinimalizeToolStripMenuItem_Click_1;
            // 
            // removeToolStripMenuItem
            // 
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            removeToolStripMenuItem.Size = new Size(155, 24);
            removeToolStripMenuItem.Text = "Remove";
            removeToolStripMenuItem.Click += RemoveToolStripMenuItem_Click;
            // 
            // selectAllToolStripMenuItem
            // 
            selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            selectAllToolStripMenuItem.Size = new Size(155, 24);
            selectAllToolStripMenuItem.Text = "Select all";
            selectAllToolStripMenuItem.Click += SelectAllToolStripMenuItem_Click;
            // 
            // deselectAllToolStripMenuItem
            // 
            deselectAllToolStripMenuItem.Name = "deselectAllToolStripMenuItem";
            deselectAllToolStripMenuItem.Size = new Size(155, 24);
            deselectAllToolStripMenuItem.Text = "Deselect all";
            deselectAllToolStripMenuItem.Click += DeselectAllToolStripMenuItem_Click;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { setIconToolStripMenuItem1, dobleClickToActivateToolStripMenuItem, transparentToolStripMenuItem, topMostToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(180, 24);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // setIconToolStripMenuItem1
            // 
            setIconToolStripMenuItem1.Name = "setIconToolStripMenuItem1";
            setIconToolStripMenuItem1.Size = new Size(226, 24);
            setIconToolStripMenuItem1.Text = "Set Icon";
            setIconToolStripMenuItem1.Click += SetIconToolStripMenuItem_Click;
            // 
            // dobleClickToActivateToolStripMenuItem
            // 
            dobleClickToActivateToolStripMenuItem.Name = "dobleClickToActivateToolStripMenuItem";
            dobleClickToActivateToolStripMenuItem.Size = new Size(226, 24);
            dobleClickToActivateToolStripMenuItem.Text = "Doble click to activate";
            dobleClickToActivateToolStripMenuItem.Click += DobleClickToActivateToolStripMenuItem_Click;
            // 
            // transparentToolStripMenuItem
            // 
            transparentToolStripMenuItem.Name = "transparentToolStripMenuItem";
            transparentToolStripMenuItem.Size = new Size(226, 24);
            transparentToolStripMenuItem.Text = "Transparent";
            transparentToolStripMenuItem.Click += TransparentToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(177, 6);
            // 
            // widgetToolStripMenuItem
            // 
            widgetToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newWidgetToolStripMenuItem, createWidgetToolStripMenuItem, taskbarToolStripMenuItem });
            widgetToolStripMenuItem.Name = "widgetToolStripMenuItem";
            widgetToolStripMenuItem.Size = new Size(180, 24);
            widgetToolStripMenuItem.Text = "Widget";
            // 
            // newWidgetToolStripMenuItem
            // 
            newWidgetToolStripMenuItem.Name = "newWidgetToolStripMenuItem";
            newWidgetToolStripMenuItem.Size = new Size(171, 24);
            newWidgetToolStripMenuItem.Text = "New widget";
            newWidgetToolStripMenuItem.Click += NewWidgetToolStripMenuItem_Click;
            // 
            // createWidgetToolStripMenuItem
            // 
            createWidgetToolStripMenuItem.Name = "createWidgetToolStripMenuItem";
            createWidgetToolStripMenuItem.Size = new Size(171, 24);
            createWidgetToolStripMenuItem.Text = "Create widget";
            createWidgetToolStripMenuItem.Click += CreateWidgetToolStripMenuItem_Click;
            // 
            // taskbarToolStripMenuItem
            // 
            taskbarToolStripMenuItem.Name = "taskbarToolStripMenuItem";
            taskbarToolStripMenuItem.Size = new Size(171, 24);
            taskbarToolStripMenuItem.Text = "Taskbar";
            taskbarToolStripMenuItem.Click += TaskbarToolStripMenuItem_Click;
            // 
            // applicationToolStripMenuItem
            // 
            applicationToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { consoleToolStripMenuItem, closeToolStripMenuItem1 });
            applicationToolStripMenuItem.Name = "applicationToolStripMenuItem";
            applicationToolStripMenuItem.Size = new Size(180, 24);
            applicationToolStripMenuItem.Text = "Application";
            // 
            // consoleToolStripMenuItem
            // 
            consoleToolStripMenuItem.Name = "consoleToolStripMenuItem";
            consoleToolStripMenuItem.Size = new Size(131, 24);
            consoleToolStripMenuItem.Text = "Console";
            consoleToolStripMenuItem.Click += ConsoleToolStripMenuItem_Click;
            // 
            // closeToolStripMenuItem1
            // 
            closeToolStripMenuItem1.Name = "closeToolStripMenuItem1";
            closeToolStripMenuItem1.Size = new Size(131, 24);
            closeToolStripMenuItem1.Text = "Exit";
            closeToolStripMenuItem1.Click += CloseToolStripMenuItem1_Click_1;
            // 
            // openFileDialog
            // 
            openFileDialog.FileName = "openFileDialog1";
            openFileDialog.Filter = "Image Files|*.ico;*.bmp;*.jpg;*.jpeg;*.png;";
            openFileDialog.Title = "Select icon";
            // 
            // topMostToolStripMenuItem
            // 
            topMostToolStripMenuItem.Name = "topMostToolStripMenuItem";
            topMostToolStripMenuItem.Size = new Size(226, 24);
            topMostToolStripMenuItem.Text = "Top most";
            topMostToolStripMenuItem.Click += TopMostToolStripMenuItem_Click;
            // 
            // FormPin
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1067, 693);
            ContextMenuStrip = contextMenuStrip;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "FormPin";
            StartPosition = FormStartPosition.Manual;
            Text = "FormPin";
            Load += FormPin_Load;
            Shown += FormPin_Shown;
            ResizeBegin += FormPin_ResizeBegin;
            ResizeEnd += FormPin_ResizeEnd;
            Click += FormPin_Click;
            DragDrop += FormPin_DragDrop;
            DragEnter += FormPin_DragEnter;
            Paint += FormPin_Paint;
            KeyDown += FormPin_KeyDown;
            MouseDoubleClick += FormPin_MouseDoubleClick;
            MouseDown += FormPin_MouseDown;
            MouseMove += FormPin_MouseMove;
            MouseUp += FormPin_MouseUp;
            Resize += FormPin_Resize;
            contextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
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
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem minimalizeToolStripMenuItem;
        private ToolStripMenuItem taskbarToolStripMenuItem;
        private ToolStripMenuItem consoleToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem commandToolStripMenuItem;
        private ToolStripMenuItem selectAllToolStripMenuItem;
        private ToolStripMenuItem deselectAllToolStripMenuItem;
        private ToolStripMenuItem createWidgetToolStripMenuItem;
        private ToolStripMenuItem topMostToolStripMenuItem;
    }
}