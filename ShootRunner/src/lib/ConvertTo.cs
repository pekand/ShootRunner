using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

#nullable disable

namespace ShootRunner
{
    public static class ConvertTo
    {
        public static string BitmapToString(Bitmap icon)
        {
            if (icon == null) {
                return "";
            }

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    icon.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] bytes = ms.ToArray();
                    return Convert.ToBase64String(bytes);
                }
            }
            catch
            {
                return null;
            }
        }

        public static Bitmap StringToBitmap(string base64String)
        {
            if (base64String.Trim() == "")
            {
                return null;
            }

            try
            {
                byte[] bytes = Convert.FromBase64String(base64String);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    return new Bitmap(ms);
                }
            }
            catch
            {
                return null;
            }
        }

        public static string BoolToString(bool value, bool defaultValue = false)
        {
            return value.ToString();            
        }

        public static bool StringToBool(string value, bool defaultValue = false)
        {
            try
            {
                return bool.Parse(value);
            }
            catch
            {
                return defaultValue;
            }
            
        }

        public static double StringToDouble(string value, double defaultValue = 0.0)
        {
            try
            {
                return Double.Parse(value);
            }
            catch
            {
                return defaultValue;
            }

        }

        public static string DoubleToString(double value)
        {
            return value.ToString();
        }

        public static string IntToString(int value)
        {
            return value.ToString();
        }

        public static int StringToInt(string value, int defaultValue = 0)
        {
            try
            {
                return int.Parse(value);
            }
            catch
            {
                return defaultValue;
            }
        }

    }
}
