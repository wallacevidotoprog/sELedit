using System.IO;

namespace eELedit.Previews.Models
{
    public class Face
	{
		public short[] VertIndexs
		{
			get;
			set;
		}

		public static Face Read(BinaryReader r)
		{
            Face face = new Face
            {
                VertIndexs = new short[3]
            };
            for (int i = 0; i < 3; i++)
			{
				face.VertIndexs[i] = r.ReadInt16();
			}
			return face;
		}

		public Face()
		{

		}
	}
}
