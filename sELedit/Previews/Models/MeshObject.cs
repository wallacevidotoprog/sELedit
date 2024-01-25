using System.IO;
using System.Text;

namespace eELedit.Previews.Models
{
    public class MeshObject
	{
		public string Name
		{
			get;
			set;
		}

		public int TexIndex
		{
			get;
			set;
		}

		public int MatIndex
		{
			get;
			set;
		}

		public uint VertexCount
		{
			get;
			set;
		}

		public uint IndexCount
		{
			get;
			set;
		}

		public Vertex[] Vertexes
		{
			get;
			set;
		}

		public Face[] Faces
		{
			get;
			set;
		}

		public static MeshObject Read(BinaryReader r, int vertex_type = 0)
		{
			MeshObject meshObject = new MeshObject();
			int count = r.ReadInt32();
			byte[] bytes = r.ReadBytes(count);
			meshObject.Name = Encoding.GetEncoding("GB2312").GetString(bytes);
			meshObject.TexIndex = r.ReadInt32();
			meshObject.MatIndex = r.ReadInt32();
			if (vertex_type == 1)
			{
				r.ReadInt32();
			}
			meshObject.VertexCount = r.ReadUInt32();
			meshObject.IndexCount = r.ReadUInt32();
			meshObject.Vertexes = new Vertex[meshObject.VertexCount];
			int num = 0;
			while ((long)num < (long)((ulong)meshObject.VertexCount))
			{
				meshObject.Vertexes[num] = Vertex.Read(r, vertex_type);
				num++;
			}
			uint num2 = meshObject.IndexCount / 3u;
			meshObject.Faces = new Face[num2];
			int num3 = 0;
			while ((long)num3 < (long)((ulong)num2))
			{
				meshObject.Faces[num3] = Face.Read(r);
				num3++;
			}
			return meshObject;
		}

		public MeshObject()
		{

		}
	}
}
