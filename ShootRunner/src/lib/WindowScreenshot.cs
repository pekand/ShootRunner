using System.Drawing.Imaging;
using System.Runtime.InteropServices;

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

        public static Bitmap CaptureWindow(Window window, int resizeWidth = 0, int resizeHeight = 0, int wait = 0, Action callback = null)
        {
            try
            {            
                if (!GetWindowRect(window.Handle, out RECT rect))
                    return null;

                if (ToolsWindow.IsMinimalized(window))
                {
                    return null;
                }

                int width = rect.Right - rect.Left;
                int height = rect.Bottom - rect.Top;

                if (width <=0 || height<=0) {
                    return null;
                }

                using (Bitmap bmp = new Bitmap(width, height, PixelFormat.Format64bppArgb))
                {
                    if (wait>0) { 
                        Thread.Sleep(wait);
                    }

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
                        using (Bitmap resizedBmp = ResizeBitmap(bmp, resizeWidth, resizeHeight))
                        {

                            if (resizedBmp == null){
                                return null;
                            
                            }

                            if (IsSingleColor(resizedBmp))
                            {
                                return null;
                            }

                            if (window.screenshot != null)
                            {
                                window.screenshot.Dispose();
                            }
                            window.screenshot = (Bitmap)resizedBmp.Clone();

                            callback?.Invoke();

                            return window.screenshot;
                        }
                        
                    }

                    if (IsSingleColor(bmp))
                    {
                        return null;
                    }

                    if (window.screenshot != null) {
                        window.screenshot.Dispose();
                    }

                    window.screenshot = (Bitmap)bmp.Clone();

                    callback?.Invoke();

                    return window.screenshot;
                }

            }
            catch (Exception)
            {


            }

            return null;
        }

        public static void CaptureWindowTask(Window window, int resizeWidth = 0, int resizeHeight = 0, int wait = 0, Action callback = null)
        {
            Thread screenshotThread = new Thread(() => CaptureWindow(window, resizeWidth, resizeHeight, wait, callback));
            screenshotThread.SetApartmentState(ApartmentState.STA);
            screenshotThread.Start();
        }


        public static Bitmap ResizeBitmap(Bitmap originalImage, int width, int height)
        {

            using (Bitmap resizedImage = new Bitmap(width, height))
            {
                using (Graphics graphics = Graphics.FromImage(resizedImage))
                {
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    graphics.DrawImage(originalImage, 0, 0, width, height);
                }

                return (Bitmap)resizedImage.Clone();
            }
        }


        public static Icon ConvertToIcon(Bitmap bitmap, int width, int height)
        {
            using (Bitmap resizedBitmap = new Bitmap(bitmap, new Size(width, height)))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    resizedBitmap.Save(ms, ImageFormat.Png);
                    using (Bitmap pngBitmap = new Bitmap(ms))
                    {
                        return Icon.FromHandle(pngBitmap.GetHicon());
                    }
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


        public static void CaptureWindow3(Window window, int resizeWidth = 0, int resizeHeight = 0, int wait = 0, Action callback = null)
        {
            try
            {
                if (window.Handle == IntPtr.Zero)
                {
                    return;
                }

                if (!ToolsWindow.IsWindowValid(window))
                {
                    return;
                }

                if (ToolsWindow.IsMinimalized(window))
                {
                    return;
                }

                if (GetWindowRect(window.Handle, out RECT rect))
                {

                    int width = rect.Right - rect.Left;
                    int height = rect.Bottom - rect.Top;


                    Rectangle windowRectangle = new Rectangle(rect.Left, rect.Top, width, height);
                    Screen screen = Screen.FromRectangle(windowRectangle);


                    using (Bitmap screenshot = new Bitmap(screen.Bounds.Width, screen.Bounds.Height))
                    {
                        using (Graphics g = Graphics.FromImage(screenshot))
                        {
                            if (wait > 0)
                            {
                                Thread.Sleep(wait);
                            }

                            g.CopyFromScreen(screen.Bounds.X, screen.Bounds.Y, 0, 0, screenshot.Size, CopyPixelOperation.SourceCopy);
                        }

                        Rectangle windowRectRelativeToScreen = new Rectangle(
                            rect.Left - screen.Bounds.X,
                            rect.Top - screen.Bounds.Y,
                            width,
                            height
                        );

                        using (Bitmap resizedScreenshot = screenshot.Clone(windowRectRelativeToScreen, screenshot.PixelFormat))
                        {

                            if (resizeWidth > 0 && resizeHeight > 0)
                            {
                                using (Bitmap resizedBmp = ResizeBitmap(resizedScreenshot, resizeWidth, resizeHeight))
                                {

                                    if (resizedBmp == null)
                                    {
                                        return;

                                    }

                                    if (IsSingleColor(resizedBmp))
                                    {
                                        return;
                                    }

                                    if (window.screenshot != null)
                                    {
                                        window.screenshot.Dispose();
                                    }

                                    window.screenshot = (Bitmap)resizedBmp.Clone();

                                    callback?.Invoke();
                                    return;
                                }

                            }

                            if (window.screenshot != null)
                            {
                                window.screenshot.Dispose();
                            }

                            window.screenshot = (Bitmap)resizedScreenshot.Clone();

                            callback?.Invoke();
                            return;
                        }

                    }
                }
            } catch { 
            }

            return;
        }

        public static void CaptureWindow3Task(Window window, int resizeWidth = 0, int resizeHeight = 0, int wait = 0, Action callback = null)
        {
            Thread screenshotThread = new Thread(() => CaptureWindow3(window, resizeWidth, resizeHeight, wait, callback));
            screenshotThread.SetApartmentState(ApartmentState.STA);
            screenshotThread.Start();
        }
    }
}
