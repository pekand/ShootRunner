namespace ShootRunner
{
    partial class FormCommandEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCommandEditor));
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            webViewHelp = new Microsoft.Web.WebView2.WinForms.WebView2();
            textBoxCommand = new TextBox();
            panel1 = new Panel();
            labelError = new Label();
            panel2 = new Panel();
            buttonExternalEditor = new Button();
            buttonPretty = new Button();
            buttonOK = new Button();
            panel3 = new Panel();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabelPosition = new ToolStripStatusLabel();
            openFileDialog1 = new OpenFileDialog();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webViewHelp).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 0, 1);
            tableLayoutPanel1.Controls.Add(panel2, 0, 2);
            tableLayoutPanel1.Controls.Add(panel3, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 93.00792F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 6.9920845F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tableLayoutPanel1.Size = new Size(800, 845);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 61.4609566F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38.5390434F));
            tableLayoutPanel2.Controls.Add(webViewHelp, 1, 0);
            tableLayoutPanel2.Controls.Add(textBoxCommand, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(794, 699);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // webViewHelp
            // 
            webViewHelp.AllowExternalDrop = true;
            webViewHelp.CreationProperties = null;
            webViewHelp.DefaultBackgroundColor = Color.White;
            webViewHelp.Dock = DockStyle.Fill;
            webViewHelp.Location = new Point(491, 3);
            webViewHelp.Name = "webViewHelp";
            webViewHelp.Size = new Size(300, 693);
            webViewHelp.TabIndex = 0;
            webViewHelp.ZoomFactor = 1D;
            // 
            // textBoxCommand
            // 
            textBoxCommand.BackColor = Color.FromArgb(48, 56, 65);
            textBoxCommand.Dock = DockStyle.Fill;
            textBoxCommand.Font = new Font("Consolas", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxCommand.ForeColor = Color.FromArgb(216, 222, 233);
            textBoxCommand.Location = new Point(3, 3);
            textBoxCommand.Multiline = true;
            textBoxCommand.Name = "textBoxCommand";
            textBoxCommand.ScrollBars = ScrollBars.Both;
            textBoxCommand.Size = new Size(482, 693);
            textBoxCommand.TabIndex = 1;
            textBoxCommand.WordWrap = false;
            textBoxCommand.TextChanged += textBox1_TextChanged;
            textBoxCommand.KeyDown += textBox1_KeyDown;
            textBoxCommand.MouseDown += textBox1_MouseDown;
            textBoxCommand.Resize += textBox1_Resize;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(255, 192, 192);
            panel1.Controls.Add(labelError);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 708);
            panel1.Name = "panel1";
            panel1.Size = new Size(794, 47);
            panel1.TabIndex = 1;
            // 
            // labelError
            // 
            labelError.AutoSize = true;
            labelError.Location = new Point(9, 15);
            labelError.Name = "labelError";
            labelError.Size = new Size(65, 20);
            labelError.TabIndex = 3;
            labelError.Text = "No Error";
            // 
            // panel2
            // 
            panel2.Controls.Add(buttonExternalEditor);
            panel2.Controls.Add(buttonPretty);
            panel2.Controls.Add(buttonOK);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 761);
            panel2.Name = "panel2";
            panel2.Size = new Size(794, 46);
            panel2.TabIndex = 2;
            // 
            // buttonExternalEditor
            // 
            buttonExternalEditor.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonExternalEditor.Location = new Point(90, 4);
            buttonExternalEditor.Name = "buttonExternalEditor";
            buttonExternalEditor.Size = new Size(167, 39);
            buttonExternalEditor.TabIndex = 7;
            buttonExternalEditor.Text = "External editor";
            buttonExternalEditor.UseVisualStyleBackColor = true;
            buttonExternalEditor.Click += buttonExternalEditor_Click;
            // 
            // buttonPretty
            // 
            buttonPretty.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonPretty.Location = new Point(9, 3);
            buttonPretty.Name = "buttonPretty";
            buttonPretty.Size = new Size(75, 39);
            buttonPretty.TabIndex = 6;
            buttonPretty.Text = "Pretty";
            buttonPretty.UseVisualStyleBackColor = true;
            buttonPretty.Click += buttonPretty_Click;
            // 
            // buttonOK
            // 
            buttonOK.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonOK.Location = new Point(710, 3);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(75, 39);
            buttonOK.TabIndex = 5;
            buttonOK.Text = "Save";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // panel3
            // 
            panel3.Controls.Add(statusStrip1);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(3, 813);
            panel3.Name = "panel3";
            panel3.Size = new Size(794, 29);
            panel3.TabIndex = 3;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabelPosition });
            statusStrip1.Location = new Point(0, 4);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(794, 25);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelPosition
            // 
            toolStripStatusLabelPosition.Name = "toolStripStatusLabelPosition";
            toolStripStatusLabelPosition.Size = new Size(61, 20);
            toolStripStatusLabelPosition.Text = "Position";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // FormCommandEditor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 845);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormCommandEditor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Commands Editor";
            Load += FormConfigEditor_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webViewHelp).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewHelp;
        private OpenFileDialog openFileDialog1;
        private TextBox textBoxCommand;
        private Panel panel1;
        private Label labelError;
        private Panel panel2;
        private Button buttonOK;
        private Panel panel3;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabelPosition;
        private Button buttonPretty;
        private Button buttonExternalEditor;
    }
}