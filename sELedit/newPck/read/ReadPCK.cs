using DevIL;
using DevIL.Unmanaged;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sELedit.newPck.read
{
    class ReadPCK
    {
        public void /*static List<Icon> */LoadIconList(string PckPath)
        {
            ArchiveEngine pck = new ArchiveEngine(PckPath);
            Bitmap img = LoadDDSImage(((IEnumerable<byte>)pck.ReadFile(pck.PckFile, pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(d => d.Path == "surfaces\\iconset\\iconlist_ivtrm.dds")).ElementAt<PCKFileEntry>(0))).ToArray<byte>());
            // return PWHelper.LoadIconList(el, img, PWHelper.CreateLines(pck));
        }
        public static Bitmap LoadDDSImage(byte[] ByteArray)
        {
            Bitmap bitmap = (Bitmap)null;
            IL.Initialize();
            IL.Enable(ILEnable.AbsoluteFormat);
            IL.SetDataFormat(DataFormat.BGRA);
            IL.Enable(ILEnable.AbsoluteType);
            IL.SetDataType(DataType.UnsignedByte);
            MemoryStream memoryStream = new MemoryStream(ByteArray);
            if (IL.LoadImageFromStream((Stream)memoryStream))
            {
                ImageInfo imageInfo = IL.GetImageInfo();
                bitmap = new Bitmap(imageInfo.Width, imageInfo.Height, PixelFormat.Format32bppArgb);
                Rectangle rect = new Rectangle(0, 0, imageInfo.Width, imageInfo.Height);
                BitmapData bitmapdata = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                IntPtr scan0 = bitmapdata.Scan0;
                IL.CopyPixels(0, 0, 0, imageInfo.Width, imageInfo.Height, 1, DataFormat.BGRA, DataType.UnsignedByte, scan0);
                bitmap.UnlockBits(bitmapdata);
            }
            IL.Shutdown();
            memoryStream.Close();
            return bitmap;
        }

















    }
}
