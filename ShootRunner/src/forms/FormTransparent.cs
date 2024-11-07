using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#nullable disable


namespace ShootRunner.src.forms
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
