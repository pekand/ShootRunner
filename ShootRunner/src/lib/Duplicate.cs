using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootRunner
{
    public class Duplicate
    {
        public static Bitmap? FastClone(Bitmap src)
        {
            if (src == null) {
                return null;
            }

            unsafe
            {
                var dst = new Bitmap(src.Width, src.Height, src.PixelFormat);

                var r = new Rectangle(0, 0, src.Width, src.Height);

                var srcData = src.LockBits(r, ImageLockMode.ReadOnly, src.PixelFormat);
                var dstData = dst.LockBits(r, ImageLockMode.WriteOnly, src.PixelFormat);

                Buffer.MemoryCopy(
                    srcData.Scan0.ToPointer(),
                    dstData.Scan0.ToPointer(),
                    dstData.Stride * dst.Height,
                    srcData.Stride * src.Height
                );

                src.UnlockBits(srcData);
                dst.UnlockBits(dstData);

                return dst;
            }
           
        }

    }
}
