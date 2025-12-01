using System.Drawing.Imaging;

#nullable disable

#pragma warning disable IDE0130

namespace ShootRunner
{
    public class WindowScreenshot
    {
        public static Bitmap CaptureWindow(Window window, int resizeWidth = 0, int resizeHeight = 0, int wait = 0, Action callback = null)
        {
            try
            {            
                if (!WinApi.GetWindowRect(window.Handle, out WinApi.RECT rect))
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

                using Bitmap bmp = new(width, height, PixelFormat.Format64bppArgb);
                if (wait > 0)
                {
                    Thread.Sleep(wait);
                }

                using (Graphics gfxBmp = Graphics.FromImage(bmp))
                {
                    IntPtr hdcBitmap = gfxBmp.GetHdc();
                    IntPtr hdcWindow = WinApi.GetDC(window.Handle);
                    int result = WinApi.BitBlt(hdcBitmap, 0, 0, width, height, hdcWindow, 0, 0, WinApi.SRCCOPY);
                    gfxBmp.ReleaseHdc(hdcBitmap);
                    int result2 = WinApi.ReleaseDC(window.Handle, hdcWindow);
                }

                if (resizeWidth > 0 && resizeHeight > 0)
                {
                    using Bitmap resizedBmp = ResizeBitmap(bmp, resizeWidth, resizeHeight);

                    if (resizedBmp == null)
                    {
                        return null;

                    }

                    if (IsSingleColor(resizedBmp))
                    {
                        return null;
                    }

                    window.screenshot?.Dispose();
                    window.screenshot = (Bitmap)resizedBmp.Clone();

                    callback?.Invoke();

                    return window.screenshot;

                }

                if (IsSingleColor(bmp))
                {
                    return null;
                }

                window.screenshot?.Dispose();

                window.screenshot = (Bitmap)bmp.Clone();

                callback?.Invoke();

                return window.screenshot;

            }
            catch (Exception)
            {


            }

            return null;
        }

        public static void CaptureWindowTask(Window window, int resizeWidth = 0, int resizeHeight = 0, int wait = 0, Action callback = null)
        {
            Thread screenshotThread = new(() => CaptureWindow(window, resizeWidth, resizeHeight, wait, callback));
            screenshotThread.SetApartmentState(ApartmentState.STA);
            screenshotThread.Start();
        }


        public static Bitmap ResizeBitmap(Bitmap originalImage, int width, int height)
        {

            using Bitmap resizedImage = new(width, height);
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(originalImage, 0, 0, width, height);
            }

            return (Bitmap)resizedImage.Clone();
        }


        public static Icon ConvertToIcon(Bitmap bitmap, int width, int height)
        {
            using Bitmap resizedBitmap = new(bitmap, new Size(width, height));
            using MemoryStream ms = new();
            resizedBitmap.Save(ms, ImageFormat.Png);
            using Bitmap pngBitmap = new(ms);
            return Icon.FromHandle(pngBitmap.GetHicon());
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

                if (WinApi.GetWindowRect(window.Handle, out WinApi.RECT rect))
                {

                    int width = rect.Right - rect.Left;
                    int height = rect.Bottom - rect.Top;


                    Rectangle windowRectangle = new(rect.Left, rect.Top, width, height);
                    Screen screen = Screen.FromRectangle(windowRectangle);


                    using Bitmap screenshot = new(screen.Bounds.Width, screen.Bounds.Height);
                    using (Graphics g = Graphics.FromImage(screenshot))
                    {
                        if (wait > 0)
                        {
                            Thread.Sleep(wait);
                        }

                        g.CopyFromScreen(screen.Bounds.X, screen.Bounds.Y, 0, 0, screenshot.Size, CopyPixelOperation.SourceCopy);
                    }

                    Rectangle windowRectRelativeToScreen = new(
                        rect.Left - screen.Bounds.X,
                        rect.Top - screen.Bounds.Y,
                        width,
                        height
                    );

                    using Bitmap resizedScreenshot = screenshot.Clone(windowRectRelativeToScreen, screenshot.PixelFormat);

                    if (resizeWidth > 0 && resizeHeight > 0)
                    {
                        using Bitmap resizedBmp = ResizeBitmap(resizedScreenshot, resizeWidth, resizeHeight);

                        if (resizedBmp == null)
                        {
                            return;

                        }

                        if (IsSingleColor(resizedBmp))
                        {
                            return;
                        }

                        window.screenshot?.Dispose();

                        window.screenshot = (Bitmap)resizedBmp.Clone();

                        callback?.Invoke();
                        return;

                    }

                    window.screenshot?.Dispose();

                    window.screenshot = (Bitmap)resizedScreenshot.Clone();

                    callback?.Invoke();
                    return;
                }
            } catch { 
            }

            return;
        }

        public static void CaptureWindow3Task(Window window, int resizeWidth = 0, int resizeHeight = 0, int wait = 0, Action callback = null)
        {
            Thread screenshotThread = new(() => CaptureWindow3(window, resizeWidth, resizeHeight, wait, callback));
            screenshotThread.SetApartmentState(ApartmentState.STA);
            screenshotThread.Start();
        }
    }
}
