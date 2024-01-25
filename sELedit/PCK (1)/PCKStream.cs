using System;
using System.IO;

namespace sELedit.PCK
{
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
}
