#nullable disable


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
                window.Type = "COMMAND";
                window.doubleClickCommand = checkBoxDoubleclick.Checked;
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
            checkMatchWindow.Checked = this.window.matchNewWindow;

            checkBoxDoubleclick.Checked = this.window.doubleClickCommand;
            if (window.Type == "WINDOW")
            {
                checkBoxDoubleclick.Checked = true;
            }
            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkMatchWindow_CheckedChanged(object sender, EventArgs e)
        {
            this.window.matchNewWindow = !this.window.matchNewWindow;
        }

        private void checkBoxDoubleclick_CheckedChanged(object sender, EventArgs e)
        {
            this.window.doubleClickCommand = !this.window.doubleClickCommand;
        }
    }
}
