// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.ImageResize
// Assembly: Q-Tech Bookkeeping, Version=1.0.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 23C9EC80-8A12-46C0-87B7-19563BD5EB8E
// Assembly location: D:\Program Files\Q-Tech Industrial Solutions\Q-Tech Bookkeeping\Q-Tech Bookkeeping.exe

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Q_Tech_Bookkeeping
{
  internal class ImageResize
  {
    public static Bitmap ResizeImage(Image image, int width, int height)
    {
      Rectangle destRect = new Rectangle(0, 0, width, height);
      Bitmap bitmap = new Bitmap(width, height);
      bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        graphics.CompositingMode = CompositingMode.SourceCopy;
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        using (ImageAttributes imageAttr = new ImageAttributes())
        {
          imageAttr.SetWrapMode(WrapMode.TileFlipXY);
          graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
        }
      }
      return bitmap;
    }
  }
}
