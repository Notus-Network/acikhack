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
        public enum ProtectionLevel
        {
            Low = 0,
            Medium = 1,
            High = 2
        }
        private static float[] ProtectionType(ProtectionLevel protectionLevel, bool isLight = false)
        {
            // FontSize, XSpace, YSpace, Opacity
            if (protectionLevel == ProtectionLevel.Low)
                return new float[] { 8, 50, 50, isLight ? 120 : 100 };
            if (protectionLevel == ProtectionLevel.Medium)
                return new float[] { 12, 40, 40, isLight ? 120 : 100 };
            if (protectionLevel == ProtectionLevel.High)
                return new float[] { 14, 40, 40, isLight ? 120 : 100 };

            return new float[] { 0, 0, 0, 0 };
        }
        public static bool AddWatermarkToImage(string sourceName, string destinationPath, string walletKey, ProtectionLevel protectionLevel, bool isJpeg, bool imageIsLight = false)
        {
            float[] values = ProtectionType(protectionLevel, imageIsLight);
            Font font = new Font("Arial", values[0]);

            try
            {
                using Bitmap bitmap = (Bitmap)System.Drawing.Image.FromFile(sourceName); 
                using Graphics graphic = Graphics.FromImage(bitmap);
                using Brush brush = new SolidBrush(Color.FromArgb(Convert.ToInt32(values[3]), !imageIsLight ? Color.White : Color.Black));

                SizeF textSize = graphic.MeasureString(walletKey, font);
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

                float t = 0, x = 0, y = 0;
                for (int j = 1; j <= 40; j++, y += values[2])
                {
                    x = 0;
                    if (j % 2 == 0)
                        t = (textSize.Width / 2);
                    for (int i = 1; i <= 40; i++, x += values[1])
                    {
                        graphic.DrawString(walletKey, font, brush, new PointF(x + t, y));
                        x += textSize.Width;
                    }
                    t = 0;
                }
                
                using (MemoryStream memory = new MemoryStream()) 
                {
                    using(FileStream fs = new FileStream(destinationPath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        bitmap.Save(memory, isJpeg ? System.Drawing.Imaging.ImageFormat.Jpeg : System.Drawing.Imaging.ImageFormat.Png);
                        byte[] bytes = memory.ToArray();
                        fs.Write(bytes, 0, bytes.Length);
                    }
                }

                bitmap.Save(destinationPath);
                return true;
            } catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }
    }
}
