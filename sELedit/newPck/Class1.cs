// Decompiled with JetBrains decompiler
// Type: LBLIBRARY.ArchiveEngine
// Assembly: LBLIBRARY, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D28A3931-4306-4D62-9FB9-15C3C8BA0540
// Assembly location: F:\google drive loft\gerencial\wallace\pw\GShopEditorByLuka-master\GShopEditorByLuka\LBLIBRARY.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace sELedit.newPck
{
    public class ArchiveEngine : IDisposable
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

        ~ArchiveEngine() => this.CleanUp(true);

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

        public ArchiveEngine(string path)
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
}
