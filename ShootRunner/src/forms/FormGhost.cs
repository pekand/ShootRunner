using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShootRunner.src.forms
{
    public partial class FormGhost : Form
    {
        private const long WS_POPUP = 0x80000000L;
        private const int GWL_STYLE = -16;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public Bitmap pic = null;
        public FormGhost(Bitmap pic)
        {
            this.pic = pic;
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.Size = this.pic.Size;
            this.TopMost = true;
            this.BackColor = Color.Magenta; // Transparency key color
            this.TransparencyKey = Color.Magenta; // Make the form transparent
            this.ShowInTaskbar = false;
            
            PictureBox pictureBox = new PictureBox
            {
                Image = this.pic,
                Size = this.Size,
                BackColor = Color.Transparent
            };
            this.Controls.Add(pictureBox);
        }

        private void FormGhost_Load(object sender, EventArgs e)
        {

        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            int style = GetWindowLong(this.Handle, GWL_STYLE);
            SetWindowLong(this.Handle, GWL_STYLE, (int)(style | WS_POPUP));
        }

    }
}
