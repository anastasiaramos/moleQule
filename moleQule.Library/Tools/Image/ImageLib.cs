using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.IO;

namespace moleQule.Library
{
    public class ImageLib
    {
        public static byte[] CreateTumbnailImageFromByteArray(byte[] pbytImageByteArray, int pintWidth)
        {
            MemoryStream memoryStream = new MemoryStream(pbytImageByteArray);
            System.Drawing.
            Image imgImageSource = System.Drawing.Image.FromStream(memoryStream);
            double dblOrgnWidth = imgImageSource.Width;
            double dblOrgnHeight = imgImageSource.Height;
            double dblRatio = (dblOrgnWidth / dblOrgnHeight) * 100;
            double dblScaledWidth = pintWidth;
            double dblScaledHeight = 0;
            dblScaledHeight = (dblScaledWidth / dblRatio) * 100;
            System.Drawing.
            Bitmap bitmapImage = new System.Drawing.Bitmap(System.Convert.ToInt32(dblScaledWidth), System.Convert.ToInt32(dblScaledHeight));
            bitmapImage.SetResolution(imgImageSource.HorizontalResolution, imgImageSource.VerticalResolution);
            System.Drawing.
            Graphics graphics = System.Drawing.Graphics.FromImage(bitmapImage);
            graphics.CompositingMode = System.Drawing.Drawing2D.
            CompositingMode.SourceCopy;
            graphics.CompositingQuality = System.Drawing.Drawing2D.
            CompositingQuality.HighQuality;
            graphics.InterpolationMode = System.Drawing.Drawing2D.
            InterpolationMode.High;
            graphics.SmoothingMode = System.Drawing.Drawing2D.
            SmoothingMode.HighQuality;
            ImageAttributes imageAttributes = new ImageAttributes();
            graphics.DrawImage(imgImageSource,
            new System.Drawing.Rectangle(0, 0, System.Convert.ToInt32(dblScaledWidth), System.Convert.ToInt32(dblScaledHeight)), 0, 0, System.Convert.ToInt32(dblOrgnWidth), System.Convert.ToInt32(dblOrgnHeight), System.Drawing.GraphicsUnit.Pixel);
            MemoryStream outputMemoryStream = new MemoryStream();
            bitmapImage.Save(outputMemoryStream, System.Drawing.Imaging.
            ImageFormat.Png);
            bitmapImage.GetThumbnailImage(System.
            Convert.ToInt32(dblScaledWidth), System.Convert.ToInt32(dblScaledHeight), null, IntPtr.Zero);
            imgImageSource.Dispose();
            bitmapImage.Dispose();
            graphics.Dispose();
            return outputMemoryStream.ToArray();
        }
    }
}
