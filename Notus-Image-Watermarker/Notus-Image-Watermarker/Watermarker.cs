using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Threading.Tasks;

namespace Notus.Image.Watermarker
{
    public class Watermarker
    {
        public static bool AddWatermarkToImage(string sourceName, string destinationPath, string walletKey)
        {
            Font font = new Font("Arial", 12);

            try
            {
                using Bitmap bitmap = (Bitmap)System.Drawing.Image.FromFile(sourceName);
                using Graphics graphic = Graphics.FromImage(bitmap);
                using Brush brush = new SolidBrush(Color.FromArgb(Convert.ToInt32(120)));

                SizeF textSize = graphic.MeasureString(walletKey, font);
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

                float t = 0, x = 0, y = 0;
                for (int j = 1; j <= 20; j++, y += 40)
                {
                    x = 0;
                    if (j % 2 == 0)
                        t = (textSize.Width / 2);
                    for (int i = 1; i <= 10; i++, x += 40)
                    {
                        graphic.DrawString(walletKey, font, brush, new PointF(x + t, y));
                        x += textSize.Width;
                    }
                    t = 0;
                }


                bitmap.Save(destinationPath);
                return true;
            }
            catch { return false; }
        }
    }
}
