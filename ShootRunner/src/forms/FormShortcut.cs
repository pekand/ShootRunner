#nullable disable

#pragma warning disable IDE0130

namespace ShootRunner
{
    public partial class FormShortcut : Form
    {
        public FormShortcut()
        {
            InitializeComponent();
        }

        private void FormShortcut_Load(object sender, EventArgs e)
        {

        }

        private void FormShortcut_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.CloseShortcutForm();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(label1.Text);
        }
    }
}
