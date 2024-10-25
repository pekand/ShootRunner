using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootRunner
{
    public class Pictures
    {
        public static Icon CreateCustomIcon()
        {

            using (Bitmap bitmap = new Bitmap(64, 64))
            {

                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.Clear(Color.Transparent);  
                    using (Brush redBrush = new SolidBrush(Color.FromArgb(255,255,0,0)))
                    {
                        g.FillEllipse(redBrush, 10, 10, 44, 44); 
                    }
                }


                using (MemoryStream ms = new MemoryStream())
                {

                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                    ms.Seek(0, SeekOrigin.Begin);

                    using (Bitmap bmpForIcon = new Bitmap(ms))
                    {
                        Icon createdIcon = Icon.FromHandle(bmpForIcon.GetHicon());
                        return createdIcon;
                    }
                }
            }
        }

    }
}
