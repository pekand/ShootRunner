using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShootRunner.src.forms
{
    public partial class FormWindowInfo : Form
    {
        Window window = null;
        public FormWindowInfo(Window window)
        {
            this.window = window;
            InitializeComponent();
        }

        private void FormWindowInfo_Load(object sender, EventArgs e)
        {
            textBox1.Text = window.Title;
            textBox2.Text = window.Handle.ToString();
            textBox3.Text = window.processId.ToString();
            textBox4.Text = window.className?.ToString();
            textBox5.Text = window.app;
            textBox6.Text = window.directory;
            textBox7.Text = window.executable;
            textBox8.Text = window.command;

            pictureBox1.Image = window.icon;
            pictureBox2.Image = window.customicon;
            pictureBox3.Image = window.screenshot;
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            this.Close();
        }
    }
}
