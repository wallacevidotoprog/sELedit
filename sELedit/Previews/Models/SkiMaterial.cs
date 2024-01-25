using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace eELedit.Previews.Models
{
	public class SkiMaterial
	{
		public byte TrailZero
		{
			get;
			set;
		}

		public float[] MaterialParamsA
		{
			get;
			set;
		}

		public float[] MaterialParamsB
		{
			get;
			set;
		}

		public float[] MaterialParamsC
		{
			get;
			set;
		}

		public float[] MaterialParamsD
		{
			get;
			set;
		}

		public float Scale
		{
			get;
			set;
		}

		public byte Clothing
		{
			get;
			set;
		}

		public static SkiMaterial Read(BinaryReader r)
		{
			r.ReadBytes(10);
            SkiMaterial skiMaterial = new SkiMaterial
            {
                TrailZero = r.ReadByte(),
                MaterialParamsA = new float[4],
                MaterialParamsB = new float[4],
                MaterialParamsC = new float[4],
                MaterialParamsD = new float[4]
            };
            for (int i = 0; i < 4; i++)
			{
				skiMaterial.MaterialParamsA[i] = r.ReadSingle();
			}
			for (int j = 0; j < 4; j++)
			{
				skiMaterial.MaterialParamsB[j] = r.ReadSingle();
			}
			for (int k = 0; k < 4; k++)
			{
				skiMaterial.MaterialParamsC[k] = r.ReadSingle();
			}
			for (int l = 0; l < 4; l++)
			{
				skiMaterial.MaterialParamsD[l] = r.ReadSingle();
			}
			skiMaterial.Scale = r.ReadSingle();
			skiMaterial.Clothing = r.ReadByte();
			return skiMaterial;
		}

		public SkiMaterial()
        {

		}
	}
}
