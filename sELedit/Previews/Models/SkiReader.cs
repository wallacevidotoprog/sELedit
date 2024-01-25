using HelixToolkit.Wpf;
using sELedit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace eELedit.Previews.Models
{
	public class SkiReader
	{
        
        public TexturesFromBytes[] texturesBytes
        {
            get;
            set;
        }

        public string[] Bips;

		public string[] Textures;

		public string ModelFilePath
		{
			get;
			set;
		}

		public byte[] Signature
		{
			get;
			set;
		}

		public uint SkiType
		{
			get;
			set;
		}

		public uint[] MeshCount
		{
			get;
			set;
		}

		public uint TexturesCount
		{
			get;
			set;
		}

		public uint MaterialsCount
		{
			get;
			set;
		}

		public uint NumBips
		{
			get;
			set;
		}

		public uint Unknow2
		{
			get;
			set;
		}

		public uint TypeMask
		{
			get;
			set;
		}

		public byte[] UnknowBytes
		{
			get;
			set;
		}

		public SkiMaterial[] Materials
		{
			get;
			set;
		}

		public MeshObject[] Object
		{
			get;
			set;
		}

		public SkiReader(byte[] file)
		{
			this.Bips = new string[0];
			this.Textures = new string[0];
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(file)))
			{
				this.Signature = binaryReader.ReadBytes(8);
				if (this.Signature[0] == 77)
				{
					if (this.Signature[7] == 65)
					{
						this.SkiType = binaryReader.ReadUInt32();
						this.MeshCount = new uint[4];
						for (int i = 0; i < 4; i++)
						{
							this.MeshCount[i] = binaryReader.ReadUInt32();
						}
						this.TexturesCount = binaryReader.ReadUInt32();
						this.MaterialsCount = binaryReader.ReadUInt32();
						this.NumBips = binaryReader.ReadUInt32();
						this.Unknow2 = binaryReader.ReadUInt32();
						this.TypeMask = binaryReader.ReadUInt32();
						this.UnknowBytes = binaryReader.ReadBytes(60);
						if (this.SkiType == 9u)
						{
							this.Bips = new string[this.NumBips];
							int num = 0;
							while ((long)num < (long)((ulong)this.NumBips))
							{
								int count = binaryReader.ReadInt32();
								byte[] bytes = binaryReader.ReadBytes(count);
								this.Bips[num] = Encoding.GetEncoding("GB2312").GetString(bytes);
								num++;
							}
						}
						this.Textures = new string[this.TexturesCount];
						int num2 = 0;
						while ((long)num2 < (long)((ulong)this.TexturesCount))
						{
							int count2 = binaryReader.ReadInt32();
							byte[] bytes2 = binaryReader.ReadBytes(count2);
							this.Textures[num2] = Encoding.GetEncoding("GB2312").GetString(bytes2).Replace(".DDS", ".dds");
							num2++;
						}
						this.Materials = new SkiMaterial[this.MaterialsCount];
						int num3 = 0;
						while ((long)num3 < (long)((ulong)this.MaterialsCount))
						{
							this.Materials[num3] = SkiMaterial.Read(binaryReader);
							num3++;
						}
						if (this.MeshCount[0] != 0u)
						{
							this.Object = new MeshObject[this.MeshCount[0]];
							int num4 = 0;
							while ((long)num4 < (long)((ulong)this.MeshCount[0]))
							{
								this.Object[num4] = MeshObject.Read(binaryReader, 0);
								num4++;
							}
						}
						else
						{
							this.Object = new MeshObject[1];
							this.Object[0] = MeshObject.Read(binaryReader, 1);
						}
						return;
					}
				}
				throw new Exception("Its no ski format");
			}
		}

		private Material GetTexture(string name)
		{
            if (texturesBytes != null)
            {


                var texture = texturesBytes.Where(x => x._name.StartsWith(name)).ToArray();
                //var texture = Manager.Files.Where(x => x.Path.StartsWith(ModelFilePath.RemoveFirstSeparator()) && x.Path.EndsWith(name));
                if (texture.Count() > 0)
                    return MaterialHelper.CreateImageMaterial(texturesBytes[0]._file.ToImage(), 1.0);
               
            }
            return HelixToolkit.Wpf.Materials.Orange;
        }

		public Model3DGroup GetModel()
		{
			Model3DGroup model3DGroup = new Model3DGroup();
			MeshObject[] @object = this.Object;
			for (int i = 0; i < @object.Length; i++)
			{
				MeshObject meshObject = @object[i];
				List<Point3D> list = new List<Point3D>();
				List<Vector3D> list2 = new List<Vector3D>();
				List<Point> list3 = new List<Point>();
				Vertex[] vertexes = meshObject.Vertexes;
				for (int j = 0; j < vertexes.Length; j++)
				{
					Vertex vertex = vertexes[j];
					list.Add(vertex.Position);
					list2.Add(vertex.Normal);
					list3.Add(vertex.UVCoords);
				}
				List<int> list4 = new List<int>();
				Face[] faces = meshObject.Faces;
				for (int k = 0; k < faces.Length; k++)
				{
					Face face = faces[k];
					list4.Add((int)face.VertIndexs[0]);
					list4.Add((int)face.VertIndexs[1]);
					list4.Add((int)face.VertIndexs[2]);
				}
				MeshGeometry3D geometry = new MeshGeometry3D
				{
					Normals = new Vector3DCollection(list2),
					Positions = new Point3DCollection(list),
					TextureCoordinates = new PointCollection(list3),
					TriangleIndices = new Int32Collection(list4)
				};
				Material material = HelixToolkit.Wpf.Materials.Red;
				if (this.TexturesCount > 0u && (long)meshObject.TexIndex < (long)((ulong)this.TexturesCount))
				{
					material = GetTexture(this.Textures[meshObject.TexIndex]);
				}
				GeometryModel3D value = new GeometryModel3D
				{
					Geometry = geometry,
					Material = material,
					BackMaterial = material
				};
				model3DGroup.Children.Add(value);
			}
			return model3DGroup;
		}
       
    }

    public class TexturesFromBytes
    {
        public string _name;
        public byte[] _file;

    }
   
    public static class StaticConverter
    {
        public static BitmapImage ToImage(this byte[] array)
        {
            sELedit.DDSReader.Utils.PixelFormat sti = (sELedit.DDSReader.Utils.PixelFormat)6;
            System.Drawing.Bitmap bmp2 = sELedit.DDSReader.DDS.LoadImage(array, true, sti);

            using (MemoryStream ms = new MemoryStream())
            {
                bmp2.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return BI(ms.ToArray());
            }
            //return ToImage2(bmp2)
        }

        public static BitmapImage BI(this byte[] array)
        {
            //File.WriteAllBytes(@"C:\Users\LOFT261\Desktop\img.dds", array);
            using (var ms = new MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();

                //Save(image, @"C:\Users\LOFT261\Desktop\img.png");
                return image;
            }
        }

        public static void Save(this BitmapImage image, string filePath)
        {
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));

            using (var fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }
    }
    
}
