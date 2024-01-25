using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Amib.Threading;

namespace sELedit.newPck
{
	public static class compress
	{
		public static int ProgressValue { get; private set; }
		public static int ProgressMax { get; private set; }
		public static string ProgressText { get; private set; }
		public static int CompressionLevel { get; private set; }
		const int ProcessorsFactor = 8;
		private static SmartThreadPool threadPool = new SmartThreadPool(30000, Environment.ProcessorCount * ProcessorsFactor);

		public static void _Compress(string dir)
		{

			string pck = dir.Replace(".files", "");
			if (File.Exists(pck))
				File.Delete(pck);
			if (File.Exists(pck.Replace(".pck", ".pkx")))
				File.Delete(pck.Replace(".pck", ".pkx"));
			//ProgressText = LocExtension.GetLocalizedValue<string>("FileList");
			string[] files = Directory.GetFiles(dir, "*", SearchOption.AllDirectories);
			PCKStream stream = new PCKStream(pck);
			stream.WriteInt32(stream.key.FSIG_1);
			stream.WriteInt32(0);
			stream.WriteInt32(stream.key.FSIG_2);
			ProgressMax = files.Length;
			MemoryStream FileTable = new MemoryStream();
			CountdownEvent events = new CountdownEvent(files.Length);
			for (ProgressValue = 0; ProgressValue < ProgressMax; ++ProgressValue)
			{
				string file = files[ProgressValue].Replace(dir, "").Replace("/", "\\").Remove(0, 1);
				//ProgressText = $"{LocExtension.GetLocalizedValue<string>("Compressing")} {ProgressValue}/{ProgressMax}: {file}";
				byte[] decompressed = File.ReadAllBytes(Path.Combine(dir, files[ProgressValue]));
				byte[] compressed = PCKZlib.Compress(decompressed, CompressionLevel);
				var entry = new PCKFileEntry()
				{
					Path = file,
					Offset = (uint)stream.Position,
					Size = decompressed.Length,
					CompressedSize = compressed.Length
				};
				stream.WriteBytes(compressed);
				threadPool.QueueWorkItem(x => {
					PCKFileEntry e = x as PCKFileEntry;
					byte[] buffer = e.Write(CompressionLevel);
					lock (FileTable)
					{
						FileTable.Write(BitConverter.GetBytes(buffer.Length ^ stream.key.KEY_1), 0, 4);
						FileTable.Write(BitConverter.GetBytes(buffer.Length ^ stream.key.KEY_2), 0, 4);
						FileTable.Write(buffer, 0, buffer.Length);
					}
					events.Signal();
				}, entry);
			}
			events.Wait();
			long FileTableOffset = stream.Position;
			stream.WriteBytes(FileTable.ToArray());
			stream.WriteInt32(stream.key.ASIG_1);//4
			stream.WriteInt16(2);//2
			stream.WriteInt16(2);//2
			stream.WriteUInt32((uint)(FileTableOffset ^ stream.key.KEY_1));//4
			stream.WriteInt32(0);//4
			stream.WriteBytes(Encoding.Default.GetBytes("Angelica File Package, Perfect World."));//37
			byte[] nuller = new byte[215];
			stream.WriteBytes(nuller);//215 - 268
			stream.WriteInt32(stream.key.ASIG_2);//4
			stream.WriteInt32(files.Length);//4
			stream.WriteInt16(2);//2
			stream.WriteInt16(2);//2
			stream.Seek(4, SeekOrigin.Begin);
			stream.WriteUInt32((uint)stream.GetLenght());
			stream.Dispose();
			//ProgressValue = 0;
			//ProgressText = LocExtension.GetLocalizedValue<string>("Ready");
			//CloseOnFinish?.Invoke();
		}

	}
}
