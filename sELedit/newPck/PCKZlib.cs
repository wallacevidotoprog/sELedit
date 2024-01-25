using System;
using System.IO;
using zlib;

namespace sELedit.newPck
{
    public static class PCKZlib
    {
        public static byte[] Decompress(byte[] bytes, int size)
        {
            byte[] buffer = new byte[size];
            ZOutputStream zoutputStream = new ZOutputStream((Stream)new MemoryStream(buffer));
            try
            {
                PCKZlib.CopyStream((Stream)new MemoryStream(bytes), (Stream)zoutputStream, size);
            }
            catch
            {
                Console.WriteLine("Bad zlib data");
            }
            return buffer;
        }

        public static byte[] Compress(byte[] bytes, int CompressionLevel)
        {
            MemoryStream memoryStream = new MemoryStream();
            ZOutputStream zoutputStream = new ZOutputStream((Stream)memoryStream, CompressionLevel);
            PCKZlib.CopyStream((Stream)new MemoryStream(bytes), (Stream)zoutputStream, bytes.Length);
            zoutputStream.finish();
            return memoryStream.ToArray().Length >= bytes.Length ? bytes : memoryStream.ToArray();
        }

        public static void CopyStream(Stream input, Stream output, int Size)
        {
            byte[] buffer = new byte[Size];
            int count;
            while ((count = input.Read(buffer, 0, Size)) > 0)
                output.Write(buffer, 0, count);
            output.Flush();
        }
    }
}
