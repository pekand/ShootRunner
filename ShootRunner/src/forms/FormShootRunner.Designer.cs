﻿namespace ShootRunner
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
            contextMenuStrip1 = new ContextMenuStrip(components);
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
            timer1 = new System.Windows.Forms.Timer(components);
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // notifyIconShootRunner
            // 
            notifyIconShootRunner.ContextMenuStrip = contextMenuStrip1;
            notifyIconShootRunner.Icon = (Icon)resources.GetObject("notifyIconShootRunner.Icon");
            notifyIconShootRunner.Text = "ShootRunner";
            notifyIconShootRunner.Visible = true;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { applicationToolStripMenuItem, commandsToolStripMenuItem1, pinsToolStripMenuItem, widgetsToolStripMenuItem, optionsToolStripMenuItem, hideAllToolStripMenuItem, showAllToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(149, 172);
            contextMenuStrip1.Opening += contextMenuStrip1_Opening;
            // 
            // applicationToolStripMenuItem
            // 
            applicationToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { errorLogToolStripMenuItem, consoleToolStripMenuItem, exitToolStripMenuItem });
            applicationToolStripMenuItem.Name = "applicationToolStripMenuItem";
            applicationToolStripMenuItem.Size = new Size(148, 24);
            applicationToolStripMenuItem.Text = "Application";
            // 
            // errorLogToolStripMenuItem
            // 
            errorLogToolStripMenuItem.Name = "errorLogToolStripMenuItem";
            errorLogToolStripMenuItem.Size = new Size(131, 24);
            errorLogToolStripMenuItem.Text = "Error log";
            errorLogToolStripMenuItem.Click += errorLogToolStripMenuItem_Click;
            // 
            // consoleToolStripMenuItem
            // 
            consoleToolStripMenuItem.Name = "consoleToolStripMenuItem";
            consoleToolStripMenuItem.Size = new Size(131, 24);
            consoleToolStripMenuItem.Text = "Console";
            consoleToolStripMenuItem.Click += consoleToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(131, 24);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // commandsToolStripMenuItem1
            // 
            commandsToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { editCommandsToolStripMenuItem, shortcutFormToolStripMenuItem });
            commandsToolStripMenuItem1.Name = "commandsToolStripMenuItem1";
            commandsToolStripMenuItem1.Size = new Size(148, 24);
            commandsToolStripMenuItem1.Text = "Commands";
            // 
            // editCommandsToolStripMenuItem
            // 
            editCommandsToolStripMenuItem.Name = "editCommandsToolStripMenuItem";
            editCommandsToolStripMenuItem.Size = new Size(172, 24);
            editCommandsToolStripMenuItem.Text = "Edit commands";
            editCommandsToolStripMenuItem.Click += commandsToolStripMenuItem_Click;
            // 
            // shortcutFormToolStripMenuItem
            // 
            shortcutFormToolStripMenuItem.Name = "shortcutFormToolStripMenuItem";
            shortcutFormToolStripMenuItem.Size = new Size(172, 24);
            shortcutFormToolStripMenuItem.Text = "Shortcut form";
            shortcutFormToolStripMenuItem.Click += shortcutFormToolStripMenuItem_Click;
            // 
            // pinsToolStripMenuItem
            // 
            pinsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newPinToolStripMenuItem });
            pinsToolStripMenuItem.Name = "pinsToolStripMenuItem";
            pinsToolStripMenuItem.Size = new Size(148, 24);
            pinsToolStripMenuItem.Text = "Pins";
            // 
            // newPinToolStripMenuItem
            // 
            newPinToolStripMenuItem.Name = "newPinToolStripMenuItem";
            newPinToolStripMenuItem.Size = new Size(128, 24);
            newPinToolStripMenuItem.Text = "New pin";
            newPinToolStripMenuItem.Click += newPinToolStripMenuItem_Click;
            // 
            // widgetsToolStripMenuItem
            // 
            widgetsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newWidgetToolStripMenuItem, createWidgetToolStripMenuItem, taskbarToolStripMenuItem1 });
            widgetsToolStripMenuItem.Name = "widgetsToolStripMenuItem";
            widgetsToolStripMenuItem.Size = new Size(148, 24);
            widgetsToolStripMenuItem.Text = "Widgets";
            // 
            // newWidgetToolStripMenuItem
            // 
            newWidgetToolStripMenuItem.Name = "newWidgetToolStripMenuItem";
            newWidgetToolStripMenuItem.Size = new Size(163, 24);
            newWidgetToolStripMenuItem.Text = "New widget";
            newWidgetToolStripMenuItem.Click += newWidgetToolStripMenuItem_Click;
            // 
            // createWidgetToolStripMenuItem
            // 
            createWidgetToolStripMenuItem.Name = "createWidgetToolStripMenuItem";
            createWidgetToolStripMenuItem.Size = new Size(163, 24);
            createWidgetToolStripMenuItem.Text = "Create widget";
            createWidgetToolStripMenuItem.Click += createWidgetToolStripMenuItem_Click;
            // 
            // taskbarToolStripMenuItem1
            // 
            taskbarToolStripMenuItem1.Name = "taskbarToolStripMenuItem1";
            taskbarToolStripMenuItem1.Size = new Size(163, 24);
            taskbarToolStripMenuItem1.Text = "Taskbar";
            taskbarToolStripMenuItem1.Click += taskbarToolStripMenuItem_Click;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { autorunToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(148, 24);
            optionsToolStripMenuItem.Text = "Options";
            optionsToolStripMenuItem.Click += optionsToolStripMenuItem_Click;
            // 
            // autorunToolStripMenuItem
            // 
            autorunToolStripMenuItem.Name = "autorunToolStripMenuItem";
            autorunToolStripMenuItem.Size = new Size(129, 24);
            autorunToolStripMenuItem.Text = "Autorun";
            autorunToolStripMenuItem.Click += autorunToolStripMenuItem_Click;
            // 
            // hideAllToolStripMenuItem
            // 
            hideAllToolStripMenuItem.Name = "hideAllToolStripMenuItem";
            hideAllToolStripMenuItem.Size = new Size(148, 24);
            hideAllToolStripMenuItem.Text = "Hide all";
            hideAllToolStripMenuItem.Click += hideAllToolStripMenuItem_Click;
            // 
            // showAllToolStripMenuItem
            // 
            showAllToolStripMenuItem.Name = "showAllToolStripMenuItem";
            showAllToolStripMenuItem.Size = new Size(148, 24);
            showAllToolStripMenuItem.Text = "Show all";
            showAllToolStripMenuItem.Visible = false;
            showAllToolStripMenuItem.Click += showAllToolStripMenuItem_Click;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // FormShootRunner
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Red;
            ClientSize = new Size(50, 50);
            ContextMenuStrip = contextMenuStrip1;
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
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
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
        private ToolStripMenuItem hideAllToolStripMenuItem;
        private ToolStripMenuItem showAllToolStripMenuItem;
        private ToolStripMenuItem taskbarToolStripMenuItem1;
        private ToolStripMenuItem consoleToolStripMenuItem;
        private ToolStripMenuItem createWidgetToolStripMenuItem;
    }
}

