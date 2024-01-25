using System.IO;
using System.Windows;
using System.Windows.Media.Media3D;

namespace eELedit.Previews.Models
{
    public class Vertex
	{
		public Point3D Position
		{
			get;
			set;
		}

		public float[] VertexWeight
		{
			get;
			set;
		}

		public byte[] BoneIndex
		{
			get;
			set;
		}

		public Vector3D Normal
		{
			get;
			set;
		}

		public Point UVCoords
		{
			get;
			set;
		}

		public static Vertex Read(BinaryReader r, int vertex_type = 0)
		{
            Vertex vertex = new Vertex
            {
                Position = new Point3D
                {
                    X = (double)r.ReadSingle(),
                    Y = (double)r.ReadSingle(),
                    Z = (double)r.ReadSingle()
                }
            };
            if (vertex_type == 0)
			{
				vertex.VertexWeight = new float[3];
				for (int i = 0; i < 3; i++)
				{
					vertex.VertexWeight[i] = r.ReadSingle();
				}
				vertex.BoneIndex = new byte[4];
				for (int j = 0; j < 4; j++)
				{
					vertex.BoneIndex[j] = r.ReadByte();
				}
			}
			vertex.Normal = new Vector3D
			{
				X = (double)r.ReadSingle(),
				Y = (double)r.ReadSingle(),
				Z = (double)r.ReadSingle()
			};
			vertex.UVCoords = new Point
			{
				X = (double)r.ReadSingle(),
				Y = (double)r.ReadSingle()
			};
			return vertex;
		}

		public Vertex()
		{

		}
	}
}
