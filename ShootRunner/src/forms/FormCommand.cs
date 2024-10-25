using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShootRunner
{
    public partial class FormCommand : Form
    {
        public Window window = null;
        public FormCommand(Window window)
        {
            this.window = window;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            window.command = textBox1.Text;
            if (window.command.Trim() != "")
            {
                if (window.Type == "WINDOW") {
                    window.doubleClickCommand = true;
                }

                window.Type = "COMMAND";
                Program.Update();
            }
            this.window.silentCommand = checkBox1.Checked;
            this.Close();
        }

        private void FormCommand_Load(object sender, EventArgs e)
        {
            textBox1.Text = TextTools.NormalizeLineEndings(window.command);
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.SelectionLength = 0;            
            this.ActiveControl = null;
            checkBox1.Checked = this.window.silentCommand;
        }
    }
}
