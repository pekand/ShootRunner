using System.Runtime.InteropServices;

#pragma warning disable IDE0079
#pragma warning disable IDE0130

namespace ShootRunner
{
    public partial class FormConsole : Form
    {
        public FormConsole(string text)
        {
            InitializeComponent();

            textBox2.Text = text;
        }

        public void Write(string message)
        {
            this.Invoke(new Action(() =>
            {
                textBox2.Text += message + "\r\n";
                this.ScrollToBottom();
            }));
        }

        private void FormConsole_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void FormConsole_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.CloseConsole();            
        }
        public void ProcessCommand(string message) {
            message = message.Trim();

            if (message == "") {
                return;
            }

            if (message == "clear")
            {
                this.textBox2.Text = "";
                return;
            }

            if (message == "exit")
            {
                Program.Exit();
                return;
            }

            Program.Write(message);
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                this.ProcessCommand(textBox1.Text);
                textBox1.Text = "";
            }
        }

        public void ScrollToBottom()
        {
            WinApi.SendMessage(this.textBox2.Handle, WinApi.WM_VSCROLL, WinApi.SB_BOTTOM, 0);
        }
    }
}
