#nullable disable


namespace ShootRunner
{
    public partial class FormTransparent : Form
    {
        public FormWidget formWidget = null;
        public FormPin formPin = null;

        public FormTransparent(FormWidget formWidget, FormPin formPin)
        {
            InitializeComponent();
            this.formWidget = formWidget;
            this.formPin = formPin;
        }

        private void FormTransparent_Load(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (formWidget != null) {
                formWidget.Opacity = trackBar1.Value / 100.0;
            }

            if (formPin != null)
            {
                formPin.Opacity = trackBar1.Value / 100.0;
            }
        }
    }
}
