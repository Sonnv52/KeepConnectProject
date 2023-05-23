using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Helper.Extentions
{
    public static class ResizeImageExtentions
    {
        public static byte[] Resize(this byte[] imageData, int newWidth, int newHeight)
        {
            using (var ms = new MemoryStream(imageData))
            {
                using (var image = Image.FromStream(ms))
                {
                    using (var resizedImage = new Bitmap(newWidth, newHeight))
                    {
                        using (var graphics = Graphics.FromImage(resizedImage))
                        {
                            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                            // Vẽ hình ảnh mới từ hình ảnh gốc
                            graphics.DrawImage(image, new Rectangle(0, 0, newWidth, newHeight));

                            // Lưu hình ảnh mới vào một mảng byte
                            using (var outputMs = new MemoryStream())
                            {
                                resizedImage.Save(outputMs, image.RawFormat);
                                return outputMs.ToArray();
                            }
                        }
                    }
                }
            }
        }
    }
}
