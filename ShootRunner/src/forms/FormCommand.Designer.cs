namespace ShootRunner
{
    partial class FormCommand
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCommand));
            textBox1 = new TextBox();
            button1 = new Button();
            checkBox1 = new CheckBox();
            checkMatchWindow = new CheckBox();
            checkBoxDoubleclick = new CheckBox();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Dock = DockStyle.Top;
            textBox1.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(0, 0);
            textBox1.Margin = new Padding(4);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(1067, 280);
            textBox1.TabIndex = 0;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // button1
            // 
            button1.Location = new Point(16, 291);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(111, 47);
            button1.TabIndex = 1;
            button1.Text = "ok";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = CheckState.Checked;
            checkBox1.Location = new Point(135, 303);
            checkBox1.Margin = new Padding(4);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(283, 23);
            checkBox1.TabIndex = 2;
            checkBox1.Text = "Run PowerShell command without output";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkMatchWindow
            // 
            checkMatchWindow.AutoSize = true;
            checkMatchWindow.Checked = true;
            checkMatchWindow.CheckState = CheckState.Checked;
            checkMatchWindow.Location = new Point(436, 303);
            checkMatchWindow.Margin = new Padding(4);
            checkMatchWindow.Name = "checkMatchWindow";
            checkMatchWindow.Size = new Size(147, 23);
            checkMatchWindow.TabIndex = 3;
            checkMatchWindow.Text = "Match new window";
            checkMatchWindow.UseVisualStyleBackColor = true;
            checkMatchWindow.CheckedChanged += checkMatchWindow_CheckedChanged;
            // 
            // checkBoxDoubleclick
            // 
            checkBoxDoubleclick.AutoSize = true;
            checkBoxDoubleclick.Checked = true;
            checkBoxDoubleclick.CheckState = CheckState.Checked;
            checkBoxDoubleclick.Location = new Point(603, 303);
            checkBoxDoubleclick.Margin = new Padding(4);
            checkBoxDoubleclick.Name = "checkBoxDoubleclick";
            checkBoxDoubleclick.Size = new Size(158, 23);
            checkBoxDoubleclick.TabIndex = 4;
            checkBoxDoubleclick.Text = "Run with DoubleClick";
            checkBoxDoubleclick.UseVisualStyleBackColor = true;
            checkBoxDoubleclick.CheckedChanged += checkBoxDoubleclick_CheckedChanged;
            // 
            // FormCommand
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1067, 345);
            Controls.Add(checkBoxDoubleclick);
            Controls.Add(checkMatchWindow);
            Controls.Add(checkBox1);
            Controls.Add(button1);
            Controls.Add(textBox1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "FormCommand";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Custom Command";
            Load += FormCommand_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
        private CheckBox checkMatchWindow;
        private CheckBox checkBoxDoubleclick;
    }
}