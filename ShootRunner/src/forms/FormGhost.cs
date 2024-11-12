using System.Runtime.InteropServices;

#nullable disable

namespace ShootRunner
{
    public partial class FormGhost : Form
    {


        public Bitmap pic = null;
        public FormGhost(Bitmap pic)
        {
            this.pic = pic;

            InitializeComponent();

            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.Size = this.pic.Size;         
            
            
            this.TopMost = true;
            this.BackColor = Color.Magenta;
            this.TransparencyKey = Color.Magenta;         
            this.Opacity = 0.8;
            
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
    }
}
