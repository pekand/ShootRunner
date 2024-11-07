using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootRunner
{
    public class WindowProperty
    {
        public string name = "";
        public string value = "";
        public WindowProperty(string name = "", string value = "")
        {
            this.name = name;
            this.value = value;
        }
    }
}
