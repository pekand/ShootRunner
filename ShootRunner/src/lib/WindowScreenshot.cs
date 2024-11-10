using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace ShootRunner
{
    public class WindowScreenshot
    {
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        private static extern int BitBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSrc, int xSrc, int ySrc, int RasterOp);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hdc);

        [DllImport("user32.dll")]
        private static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, uint nFlags);

        private const int SRCCOPY = 0x00CC0020;

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public static Bitmap CaptureWindow(Window window, int resizeWidth = 0, int resizeHeight = 0)
        {
            if (!GetWindowRect(window.Handle, out RECT rect))
                return null;

            if (ToolsWindow.IsMinimalized(window))
            {
                return null;
            }

            int width = rect.Right - rect.Left;
            int height = rect.Bottom - rect.Top;

            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format64bppArgb);
            using (Graphics gfxBmp = Graphics.FromImage(bmp))
            {
                IntPtr hdcBitmap = gfxBmp.GetHdc();
                IntPtr hdcWindow = GetDC(window.Handle);
                BitBlt(hdcBitmap, 0, 0, width, height, hdcWindow, 0, 0, SRCCOPY);
                gfxBmp.ReleaseHdc(hdcBitmap);
                ReleaseDC(window.Handle, hdcWindow);
            }

            if (resizeWidth > 0 && resizeHeight > 0)
            {
                bmp =  ResizeBitmap(bmp, resizeWidth, resizeHeight);
            }

            if (IsSingleColor(bmp)) {
                return null;
            }

            return bmp;
        }

        public static Bitmap CaptureWindow2(Window window, int resizeWidth = 0, int resizeHeight = 0)
        {
            if (!GetWindowRect(window.Handle, out RECT rect))
                return null;

            if (ToolsWindow.IsMinimalized(window))
            {
                return null;
            }

            int width = rect.Right - rect.Left;
            int height = rect.Bottom - rect.Top;

            using (Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    IntPtr hdc = graphics.GetHdc();
                    bool success = PrintWindow(window.Handle, hdc, 0);
                    graphics.ReleaseHdc(hdc);

                    if (!success)
                    {
                        return null;
                    }

                    
                    if (resizeWidth > 0 && resizeHeight > 0)
                    {
                        Bitmap resizedBitmap = ResizeBitmap(bitmap, resizeWidth, resizeHeight);

                        if (IsSingleColor(resizedBitmap))
                        {
                            return null;
                        }

                        return resizedBitmap;
                    }

                    return bitmap;

                }
            }
        }


        public static Bitmap ResizeBitmap(Bitmap bitmap, int width, int height)
        {
            Bitmap resizedBitmap = new Bitmap(bitmap, new Size(width, height));
            using (MemoryStream ms = new MemoryStream())
            {
                resizedBitmap.Save(ms, ImageFormat.Png);
                using (Bitmap pngBitmap = new Bitmap(ms))
                {
                    return (Bitmap)pngBitmap.Clone();
                }
            }
        }


        public static Icon ConvertToIcon(Bitmap bitmap, int width, int height)
        {
            Bitmap resizedBitmap = new Bitmap(bitmap, new Size(width, height));
            using (MemoryStream ms = new MemoryStream())
            {
                resizedBitmap.Save(ms, ImageFormat.Png);
                using (Bitmap pngBitmap = new Bitmap(ms))
                {
                    return Icon.FromHandle(pngBitmap.GetHicon());
                }
            }
        }

        public static bool IsSingleColor(Bitmap bitmap)
        {
            Color color = bitmap.GetPixel(0, 0); // Get the color of the first pixel

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    if (bitmap.GetPixel(x, y) != color)
                    {
                        return false; // Found a different color
                    }
                }
            }

            return true; // All pixels have the same color
        }

    }
}
