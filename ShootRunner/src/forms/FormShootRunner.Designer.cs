
#pragma warning disable IDE0130

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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormShootRunner));
            notifyIconShootRunner = new NotifyIcon(components);
            contextMenuStrip = new ContextMenuStrip(components);
            applicationToolStripMenuItem = new ToolStripMenuItem();
            errorLogToolStripMenuItem = new ToolStripMenuItem();
            consoleToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            commandsToolStripMenuItem1 = new ToolStripMenuItem();
            editCommandsToolStripMenuItem = new ToolStripMenuItem();
            shortcutFormToolStripMenuItem = new ToolStripMenuItem();
            pinsToolStripMenuItem = new ToolStripMenuItem();
            newPinToolStripMenuItem = new ToolStripMenuItem();
            widgetsToolStripMenuItem = new ToolStripMenuItem();
            newWidgetToolStripMenuItem = new ToolStripMenuItem();
            createWidgetToolStripMenuItem = new ToolStripMenuItem();
            taskbarToolStripMenuItem1 = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            autorunToolStripMenuItem = new ToolStripMenuItem();
            hideAllToolStripMenuItem = new ToolStripMenuItem();
            showAllToolStripMenuItem = new ToolStripMenuItem();
            timer = new System.Windows.Forms.Timer(components);
            contextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // notifyIconShootRunner
            // 
            notifyIconShootRunner.ContextMenuStrip = contextMenuStrip;
            notifyIconShootRunner.Icon = (Icon)resources.GetObject("notifyIconShootRunner.Icon");
            notifyIconShootRunner.Text = "ShootRunner";
            notifyIconShootRunner.Visible = true;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { applicationToolStripMenuItem, commandsToolStripMenuItem1, pinsToolStripMenuItem, widgetsToolStripMenuItem, optionsToolStripMenuItem, hideAllToolStripMenuItem, showAllToolStripMenuItem });
            contextMenuStrip.Name = "contextMenuStrip1";
            contextMenuStrip.Size = new Size(156, 172);
            contextMenuStrip.Opening += ContextMenuStrip1_Opening;
            // 
            // applicationToolStripMenuItem
            // 
            applicationToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { errorLogToolStripMenuItem, consoleToolStripMenuItem, exitToolStripMenuItem });
            applicationToolStripMenuItem.Name = "applicationToolStripMenuItem";
            applicationToolStripMenuItem.Size = new Size(155, 24);
            applicationToolStripMenuItem.Text = "Application";
            // 
            // errorLogToolStripMenuItem
            // 
            errorLogToolStripMenuItem.Name = "errorLogToolStripMenuItem";
            errorLogToolStripMenuItem.Size = new Size(136, 24);
            errorLogToolStripMenuItem.Text = "Error log";
            errorLogToolStripMenuItem.Click += ErrorLogToolStripMenuItem_Click;
            // 
            // consoleToolStripMenuItem
            // 
            consoleToolStripMenuItem.Name = "consoleToolStripMenuItem";
            consoleToolStripMenuItem.Size = new Size(136, 24);
            consoleToolStripMenuItem.Text = "Console";
            consoleToolStripMenuItem.Click += ConsoleToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(136, 24);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // commandsToolStripMenuItem1
            // 
            commandsToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { editCommandsToolStripMenuItem, shortcutFormToolStripMenuItem });
            commandsToolStripMenuItem1.Name = "commandsToolStripMenuItem1";
            commandsToolStripMenuItem1.Size = new Size(155, 24);
            commandsToolStripMenuItem1.Text = "Commands";
            // 
            // editCommandsToolStripMenuItem
            // 
            editCommandsToolStripMenuItem.Name = "editCommandsToolStripMenuItem";
            editCommandsToolStripMenuItem.Size = new Size(181, 24);
            editCommandsToolStripMenuItem.Text = "Edit commands";
            editCommandsToolStripMenuItem.Click += CommandsToolStripMenuItem_Click;
            // 
            // shortcutFormToolStripMenuItem
            // 
            shortcutFormToolStripMenuItem.Name = "shortcutFormToolStripMenuItem";
            shortcutFormToolStripMenuItem.Size = new Size(181, 24);
            shortcutFormToolStripMenuItem.Text = "Shortcut form";
            shortcutFormToolStripMenuItem.Click += ShortcutFormToolStripMenuItem_Click;
            // 
            // pinsToolStripMenuItem
            // 
            pinsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newPinToolStripMenuItem });
            pinsToolStripMenuItem.Name = "pinsToolStripMenuItem";
            pinsToolStripMenuItem.Size = new Size(155, 24);
            pinsToolStripMenuItem.Text = "Pins";
            // 
            // newPinToolStripMenuItem
            // 
            newPinToolStripMenuItem.Name = "newPinToolStripMenuItem";
            newPinToolStripMenuItem.Size = new Size(133, 24);
            newPinToolStripMenuItem.Text = "New pin";
            newPinToolStripMenuItem.Click += NewPinToolStripMenuItem_Click;
            // 
            // widgetsToolStripMenuItem
            // 
            widgetsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newWidgetToolStripMenuItem, createWidgetToolStripMenuItem, taskbarToolStripMenuItem1 });
            widgetsToolStripMenuItem.Name = "widgetsToolStripMenuItem";
            widgetsToolStripMenuItem.Size = new Size(155, 24);
            widgetsToolStripMenuItem.Text = "Widgets";
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
            // taskbarToolStripMenuItem1
            // 
            taskbarToolStripMenuItem1.Name = "taskbarToolStripMenuItem1";
            taskbarToolStripMenuItem1.Size = new Size(171, 24);
            taskbarToolStripMenuItem1.Text = "Taskbar";
            taskbarToolStripMenuItem1.Click += TaskbarToolStripMenuItem_Click;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { autorunToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(155, 24);
            optionsToolStripMenuItem.Text = "Options";
            optionsToolStripMenuItem.Click += OptionsToolStripMenuItem_Click;
            // 
            // autorunToolStripMenuItem
            // 
            autorunToolStripMenuItem.Name = "autorunToolStripMenuItem";
            autorunToolStripMenuItem.Size = new Size(131, 24);
            autorunToolStripMenuItem.Text = "Autorun";
            autorunToolStripMenuItem.Click += AutorunToolStripMenuItem_Click;
            // 
            // hideAllToolStripMenuItem
            // 
            hideAllToolStripMenuItem.Name = "hideAllToolStripMenuItem";
            hideAllToolStripMenuItem.Size = new Size(155, 24);
            hideAllToolStripMenuItem.Text = "Hide all";
            hideAllToolStripMenuItem.Click += HideAllToolStripMenuItem_Click;
            // 
            // showAllToolStripMenuItem
            // 
            showAllToolStripMenuItem.Name = "showAllToolStripMenuItem";
            showAllToolStripMenuItem.Size = new Size(155, 24);
            showAllToolStripMenuItem.Text = "Show all";
            showAllToolStripMenuItem.Visible = false;
            showAllToolStripMenuItem.Click += ShowAllToolStripMenuItem_Click;
            // 
            // timer
            // 
            timer.Enabled = true;
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            // 
            // FormShootRunner
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Red;
            ClientSize = new Size(50, 53);
            ContextMenuStrip = contextMenuStrip;
            ForeColor = Color.Black;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "FormShootRunner";
            ShowInTaskbar = false;
            Text = "ShootRunner";
            FormClosing += FormShootRunner_FormClosing;
            FormClosed += FormShootRunner_FormClosed;
            Load += FormShootRunner_Load;
            InputLanguageChanging += FormShootRunner_InputLanguageChanging;
            VisibleChanged += FormShootRunner_VisibleChanged;
            DragEnter += FormShootRunner_DragEnter;
            contextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIconShootRunner;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.Timer timer;
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
        private ToolStripMenuItem hideAllToolStripMenuItem;
        private ToolStripMenuItem showAllToolStripMenuItem;
        private ToolStripMenuItem taskbarToolStripMenuItem1;
        private ToolStripMenuItem consoleToolStripMenuItem;
        private ToolStripMenuItem createWidgetToolStripMenuItem;
    }
}

