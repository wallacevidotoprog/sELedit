// Decompiled with JetBrains decompiler
// Type: LBLIBRARY.PCKFileEntry
// Assembly: LBLIBRARY, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D28A3931-4306-4D62-9FB9-15C3C8BA0540
// Assembly location: F:\google drive loft\gerencial\wallace\pw\GShopEditorByLuka-master\GShopEditorByLuka\LBLIBRARY.dll

using System;
using System.IO;
using System.Text;

namespace sELedit.PCK
{
    public class PCKFileEntry
    {
        public string Path { get; set; }

        public uint Offset { get; set; }

        public int Size { get; set; }

        public int CompressedSize { get; set; }

        public PCKFileEntry()
        {
        }

        public PCKFileEntry(byte[] bytes) => this.Read(bytes);

        public void Read(byte[] bytes)
        {
            if (bytes.Length < 276)
                bytes = PCKZlib.Decompress(bytes, 276);
            BinaryReader binaryReader = new BinaryReader((Stream)new MemoryStream(bytes));
            this.Path = Encoding.GetEncoding(936).GetString(binaryReader.ReadBytes(260)).Split(new string[1]
            {
        "\0"
            }, StringSplitOptions.RemoveEmptyEntries)[0].Replace("/", "\\").ToLower();
            this.Offset = binaryReader.ReadUInt32();
            this.Size = binaryReader.ReadInt32();
            this.CompressedSize = binaryReader.ReadInt32();
            binaryReader.Close();
        }

        public byte[] Write(int CompressionLevel)
        {
            byte[] numArray1 = new byte[276];
            BinaryWriter binaryWriter = new BinaryWriter((Stream)new MemoryStream(numArray1));
            binaryWriter.Write(Encoding.GetEncoding("GB2312").GetBytes(this.Path.Replace("/", "\\")));
            binaryWriter.BaseStream.Seek(260L, SeekOrigin.Begin);
            binaryWriter.Write(this.Offset);
            binaryWriter.Write(this.Size);
            binaryWriter.Write(this.CompressedSize);
            binaryWriter.Write(0);
            binaryWriter.BaseStream.Seek(0L, SeekOrigin.Begin);
            binaryWriter.Close();
            byte[] numArray2 = PCKZlib.Compress(numArray1, CompressionLevel);
            return numArray2.Length >= 276 ? numArray1 : numArray2;
        }
    }
}
