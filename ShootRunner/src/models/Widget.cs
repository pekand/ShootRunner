using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace ShootRunner
{
    public class Widget
    {
        public WidgetType widgetType = new WidgetType();
        public FormWidget widgetForm = null;
        public string type = "";
        public string uid = "";
        public int StartLeft = 100;
        public int StartTop = 100;
        public int StartWidth = 300;
        public int StartHeight = 300;
        public bool locked = false;
        public double transparent = 1.0;
        public bool mosttop = false;
        public Dictionary<string, string> data = new Dictionary<string, string>();

        // TASKBAR
        public bool useScreenshots = true;
        public bool useBigIcons = false;
        public Color backgroundColor = Color.Black;
    }
}
