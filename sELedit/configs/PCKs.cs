using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zlib;

namespace sELedit
{
    public class PCKs : IDisposable
    {
        private bool disposed;
        public PCKStream PckFile;
        public List<PCKFileEntry> Files;
        private int _CompressionLevel = 9;

        public int CompressionLevel
        {
            get => this._CompressionLevel;
            set
            {
                if (this._CompressionLevel == value)
                    return;
                this._CompressionLevel = value;
            }
        }

        public void Dispose()
        {
            this.Files = (List<PCKFileEntry>)null;
            this.PckFile.Dispose();
            GC.SuppressFinalize((object)this);
        }

        ~PCKs() => this.CleanUp(true);

        private void CleanUp(bool clean)
        {
            if (!this.disposed && clean)
                this.PckFile.Dispose();
            this.disposed = true;
        }

        public int GetFilesCount(PCKStream stream)
        {
            stream.Seek(-8L, SeekOrigin.End);
            return stream.ReadInt32();
        }

        public PCKFileEntry[] ReadFileTable(PCKStream stream)
        {
            stream.Seek(-8L, SeekOrigin.End);
            int length = stream.ReadInt32();
            stream.Seek(-272L, SeekOrigin.End);
            long offset = (long)(uint)((ulong)stream.ReadUInt32() ^ (ulong)stream.key.KEY_1);
            PCKFileEntry[] pckFileEntryArray = new PCKFileEntry[length];
            stream.Seek(offset, SeekOrigin.Begin);
            for (int index = 0; index < pckFileEntryArray.Length; ++index)
            {
                int count = stream.ReadInt32() ^ stream.key.KEY_1;
                stream.ReadInt32();
                pckFileEntryArray[index] = new PCKFileEntry(stream.ReadBytes(count));
            }
            return pckFileEntryArray;
        }

        public PCKs(string path)
        {
            this.PckFile = new PCKStream(path);
            this.Files = ((IEnumerable<PCKFileEntry>)this.ReadFileTable(this.PckFile)).ToList<PCKFileEntry>();
        }

        public byte[] ReadFile(PCKStream stream, PCKFileEntry file)
        {
            if (file == null)
                return new byte[0];
            stream.Seek((long)file.Offset, SeekOrigin.Begin);
            byte[] bytes = stream.ReadBytes(file.CompressedSize);
            return file.CompressedSize >= file.Size ? bytes : PCKZlib.Decompress(bytes, file.Size);
        }
    }

    public class PCKStream : IDisposable
    {
        protected BufferedStream pck;
        protected BufferedStream pkx;
        private string path = "";
        public long Position;
        public PCKKey key = new PCKKey();
        private const uint PCK_MAX_SIZE = 2147483392;
        private const int BUFFER_SIZE = 33554432;

        public PCKStream(string path, PCKKey key = null)
        {
            this.path = path;
            if (key != null)
                this.key = key;
            this.pck = new BufferedStream((Stream)new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite), 33554432);
            if (!File.Exists(path.Replace(".pck", ".pkx")) || !(Path.GetExtension(path) != ".cup"))
                return;
            this.pkx = new BufferedStream((Stream)new FileStream(path.Replace(".pck", ".pkx"), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite), 33554432);
        }

        public void Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    this.Position = offset;
                    break;
                case SeekOrigin.Current:
                    this.Position += offset;
                    break;
                case SeekOrigin.End:
                    this.Position = this.GetLenght() + offset;
                    break;
            }
            if (this.Position < this.pck.Length)
                this.pck.Seek(this.Position, SeekOrigin.Begin);
            else
                this.pkx.Seek(this.Position - this.pck.Length, SeekOrigin.Begin);
        }

        public long GetLenght() => this.pkx == null ? this.pck.Length : this.pck.Length + this.pkx.Length;

        public byte[] ReadBytes(int count)
        {
            byte[] buffer = new byte[count];
            int num = 0;
            if (this.Position < this.pck.Length)
            {
                int offset = this.pck.Read(buffer, 0, count);
                if (offset < count && this.pkx != null)
                {
                    this.pkx.Seek(0L, SeekOrigin.Begin);
                    num = offset + this.pkx.Read(buffer, offset, count - offset);
                }
            }
            else if (this.Position > this.pck.Length && this.pkx != null)
                num = this.pkx.Read(buffer, 0, count);
            this.Position += (long)count;
            return buffer;
        }

        public void WriteBytes(byte[] array)
        {
            if (this.Position + (long)array.Length < 2147483392L)
                this.pck.Write(array, 0, array.Length);
            else if (this.Position + (long)array.Length > 2147483392L)
            {
                if (this.pkx == null)
                    this.pkx = new BufferedStream((Stream)new FileStream(this.path.Replace(".pck", ".pkx"), FileMode.Create, FileAccess.ReadWrite), 33554432);
                if (this.Position > 2147483392L)
                {
                    this.pkx.Write(array, 0, array.Length);
                }
                else
                {
                    if (this.pkx == null)
                        this.pkx = new BufferedStream((Stream)new FileStream(this.path.Replace(".pck", ".pkx"), FileMode.Create, FileAccess.ReadWrite), 33554432);
                    this.pck.Write(array, 0, (int)(2147483392L - this.Position));
                    this.pkx.Write(array, (int)(2147483392L - this.Position), array.Length - (int)(2147483392L - this.Position));
                }
            }
            this.Position += (long)array.Length;
        }

        public uint ReadUInt32() => BitConverter.ToUInt32(this.ReadBytes(4), 0);

        public int ReadInt32() => BitConverter.ToInt32(this.ReadBytes(4), 0);

        public void WriteUInt32(uint value) => this.WriteBytes(BitConverter.GetBytes(value));

        public void WriteInt32(int value) => this.WriteBytes(BitConverter.GetBytes(value));

        public void WriteInt16(short value) => this.WriteBytes(BitConverter.GetBytes(value));

        public void Dispose()
        {
            this.pck?.Close();
            this.pkx?.Close();
        }
    }

    public class PCKKey
    {
        public int KEY_1 = -1466731422;
        public int KEY_2 = -240896429;
        public int ASIG_1 = -33685778;
        public int ASIG_2 = -267534609;
        public int FSIG_1 = 1305093103;
        public int FSIG_2 = 1453361591;

        public PCKKey()
        {
        }

        public PCKKey(int key1, int key2)
        {
            this.KEY_1 = key1;
            this.KEY_2 = key2;
        }

        public PCKKey(int key1, int key2, int asig1, int asig2, int fsig1, int fsig2)
        {
            this.KEY_1 = key1;
            this.KEY_2 = key2;
            this.ASIG_1 = asig1;
            this.ASIG_2 = asig2;
            this.FSIG_1 = fsig1;
            this.FSIG_2 = fsig2;
        }
    }

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
