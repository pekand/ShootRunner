#nullable disable

#pragma warning disable IDE0130

namespace ShootRunner
{
    public class Pictures
    {
        public static Icon CreateCustomIcon()
        {

            using Bitmap bitmap = new(64, 64);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.Transparent);
                using Brush redBrush = new SolidBrush(Color.FromArgb(255, 255, 0, 0));
                g.FillEllipse(redBrush, 10, 10, 44, 44);
            }


            using MemoryStream ms = new();

            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            ms.Seek(0, SeekOrigin.Begin);

            using Bitmap bmpForIcon = new(ms);
            Icon createdIcon = Icon.FromHandle(bmpForIcon.GetHicon());
            return createdIcon;
        }

    }
}
