using System.Runtime.InteropServices;

namespace ShootRunner
{
    public partial class FormConsole : Form
    {
        public FormConsole(string text)
        {
            InitializeComponent();

            textBox2.Text = text;
        }

        public void write(string message)
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

            Program.write(message);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                this.ProcessCommand(textBox1.Text);
                textBox1.Text = "";
            }
        }

        private const int WM_VSCROLL = 0x0115;
        private const int SB_BOTTOM = 7;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        public void ScrollToBottom()
        {
            SendMessage(this.textBox2.Handle, WM_VSCROLL, SB_BOTTOM, 0);
        }
    }
}
