using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace sELedit.configs
{
    public static class ImgsFiles
    {

        public static Image TrueStretchImage(Image sourceImage, int targetWidth, int targetHeight)
        {
            var targetImage = new BitmapImage();
            var img = new Bitmap((int)targetWidth, (int)targetHeight);

            using (var g = Graphics.FromImage(img))
            {

                var topLeftAngle = new Rectangle(0, 0, sourceImage.Width / 2, sourceImage.Height / 2 + 1);
                var topRightAngle = new Rectangle(sourceImage.Width / 2, 0, sourceImage.Width / 2, sourceImage.Height / 2);
                var bottomLeftAngle = new Rectangle(0, sourceImage.Height / 2, sourceImage.Width / 2, sourceImage.Height / 2);
                var bottomRighttAngle = new Rectangle(sourceImage.Width / 2, sourceImage.Height / 2, sourceImage.Width / 2, sourceImage.Height / 2);
                var dstTopLeftAngle = topLeftAngle;
                var dstTopRightAngle = new Rectangle((int)targetWidth - sourceImage.Width / 2, 0, sourceImage.Width / 2, sourceImage.Height / 2);
                var dstBottomLeftAngle = new Rectangle(0, (int)targetHeight - sourceImage.Height / 2, sourceImage.Width / 2, sourceImage.Height / 2);
                var dstBottomRightAngle = new Rectangle((int)targetWidth - sourceImage.Width / 2, (int)targetHeight - sourceImage.Height / 2, sourceImage.Width / 2, sourceImage.Height / 2);

                g.DrawImage(sourceImage, dstTopLeftAngle, topLeftAngle, GraphicsUnit.Pixel);
                g.DrawImage(sourceImage, dstTopRightAngle, topRightAngle, GraphicsUnit.Pixel);
                g.DrawImage(sourceImage, dstBottomLeftAngle, bottomLeftAngle, GraphicsUnit.Pixel);
                g.DrawImage(sourceImage, dstBottomRightAngle, bottomRighttAngle, GraphicsUnit.Pixel);

                var topTextureImg = new Bitmap(1, sourceImage.Height == 1 ? 1 : sourceImage.Height / 2);
                using (var tg = Graphics.FromImage(topTextureImg))
                {
                    tg.DrawImage(
                        sourceImage,
                        new Rectangle(0, 0, 1, sourceImage.Height / 2),
                        new Rectangle(sourceImage.Width / 2, 0, sourceImage.Width / 2 + sourceImage.Width % 2, sourceImage.Height / 2),
                        GraphicsUnit.Pixel
                        );
                }
                var topBrush = new TextureBrush(topTextureImg);
                var topBorderRect = new Rectangle(sourceImage.Width / 2, 0, (int)targetWidth - sourceImage.Width + 2, sourceImage.Height / 2);
                g.FillRectangle(topBrush, topBorderRect);

                var bottomTextureImg = new Bitmap(1, sourceImage.Height == 1 ? 1 : sourceImage.Height / 2);
                using (var tg = Graphics.FromImage(bottomTextureImg))
                {
                    tg.DrawImage(
                        sourceImage,
                        new Rectangle(0, 0, 1, sourceImage.Height / 2),
                        new Rectangle(sourceImage.Width / 2, sourceImage.Height / 2, 1, sourceImage.Height / 2),
                        GraphicsUnit.Pixel
                        );
                }
                for (var x = sourceImage.Width / 2; x < (int)targetWidth - sourceImage.Width / 2; x++)
                {
                    var bottomBorderRect = new Rectangle(x, (int)targetHeight - sourceImage.Height / 2, 1, sourceImage.Height / 2);
                    g.DrawImage(bottomTextureImg, bottomBorderRect);
                }

                var leftTextureImg = new Bitmap(sourceImage.Width == 1 ? 1 : sourceImage.Width / 2, 1);
                using (var tg = Graphics.FromImage(leftTextureImg))
                {
                    tg.DrawImage(
                        sourceImage,
                        new Rectangle(0, 0, sourceImage.Width / 2, 1),
                        new Rectangle(0, sourceImage.Height / 2, sourceImage.Width / 2, sourceImage.Height / 2 + 1),
                        GraphicsUnit.Pixel
                        );
                }
                var leftBrush = new TextureBrush(leftTextureImg);
                var leftBorderRect = new Rectangle(0, sourceImage.Height / 2, sourceImage.Width / 2, (int)targetHeight - sourceImage.Height);
                g.FillRectangle(leftBrush, leftBorderRect);

                var rightTextureImg = new Bitmap(sourceImage.Width == 1 ? 1 : sourceImage.Width / 2, 1);
                using (var tg = Graphics.FromImage(rightTextureImg))
                {
                    tg.DrawImage(
                        sourceImage,
                        new Rectangle(0, 0, sourceImage.Width / 2, 1),
                        new Rectangle(sourceImage.Width / 2, sourceImage.Height / 2, sourceImage.Width / 2, sourceImage.Height / 2),
                        GraphicsUnit.Pixel
                        );
                }
                for (var y = sourceImage.Height / 2; y < (int)targetHeight - sourceImage.Height / 2; y++)
                {
                    var rightBorderRect = new Rectangle((int)targetWidth - sourceImage.Width / 2, y, sourceImage.Width / 2, 1);
                    g.DrawImage(rightTextureImg, rightBorderRect);
                }
            }
            using (var ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Png);
                ms.Position = 0;
                targetImage.BeginInit();
                targetImage.StreamSource = ms;
                targetImage.CacheOption = BitmapCacheOption.OnLoad;
                targetImage.EndInit();
                ms.Close();
            }

            MemoryStream MSI = new MemoryStream();
            var encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(targetImage as BitmapSource));
            encoder.Save(MSI);
            MSI.Flush();
            return Image.FromStream(MSI);


        }       
    }
  
}
