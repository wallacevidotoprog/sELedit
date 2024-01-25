using DevIL;
using DevIL.Unmanaged;
using LBLIBRARY;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace sELedit.newPck
{
    public class PW_W
    {
        private static Bitmap Big;
        private static Bitmap Small;

        public PW_W()
        {
            Small = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("LBLIBRARY.Small.ico"));
            Big = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("LBLIBRARY.Standard.ico"));
        }

        public static Bitmap LoadDDSImage(byte[] ByteArray)
        {
            Bitmap bitmap = (Bitmap)null;
            IL.Initialize();
            IL.Enable(ILEnable.AbsoluteFormat);
            IL.SetDataFormat(DataFormat.BGRA);
            IL.Enable(ILEnable.AbsoluteType);
            IL.SetDataType(DataType.UnsignedByte);
            MemoryStream memoryStream = new MemoryStream(ByteArray);
            if (IL.LoadImageFromStream((Stream)memoryStream))
            {
                ImageInfo imageInfo = IL.GetImageInfo();
                bitmap = new Bitmap(imageInfo.Width, imageInfo.Height, PixelFormat.Format32bppArgb);
                Rectangle rect = new Rectangle(0, 0, imageInfo.Width, imageInfo.Height);
                BitmapData bitmapdata = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                IntPtr scan0 = bitmapdata.Scan0;
                IL.CopyPixels(0, 0, 0, imageInfo.Width, imageInfo.Height, 1, DataFormat.BGRA, DataType.UnsignedByte, scan0);
                bitmap.UnlockBits(bitmapdata);
            }
            IL.Shutdown();
            memoryStream.Close();
            return bitmap;
        }

        public static List<Icon> LoadIconList(
          Elements el,
          string Iconlist_dds,
          string iconlist_txt)
        {
            return LoadIconList(el, LoadDDSImage(File.ReadAllBytes(Iconlist_dds)), ((IEnumerable<string>)File.ReadAllLines(iconlist_txt, Encoding.GetEncoding(936))).ToList<string>());
        }

        public static List<Icon> LoadIconList(
          Elements el,
          Bitmap img,
          List<string> Text)
        {
            List<Icon> iconList = new List<Icon>();
            Text.RemoveRange(0, 4);
            int x = 0;
            int y = 0;
            for (int index = 0; index < Text.Count; ++index)
            {
                Icon icon = new Icon()
                {
                    IconName = Text[index],
                    StandardImage = img.Clone(new Rectangle(x, y, 32, 32), img.PixelFormat)
                };
                icon.ResizedImage = icon.StandardImage.ResizeImage(21, 21);
                iconList.Add(icon);
                x += 32;
                if (x == img.Width)
                {
                    y += 32;
                    x = 0;
                }
            }
            foreach (Elements.List elementsList in el.ElementsLists)
            {
                foreach (Elements.Item obj in elementsList.Items)
                {
                    string itn = obj.Icon;
                    if (obj.Icon != null)
                        itn = itn.ToLower();
                    int index = iconList.FindIndex((Predicate<Icon>)(z => z.IconName == itn));
                    if (index != -1)
                    {
                        obj.IconImage = (System.Drawing.Image)iconList[index].ResizedImage;
                        obj.Standard_image = (System.Drawing.Image)iconList[index].StandardImage;
                    }
                }
            }
            return iconList;
        }

        public static List<Icon> LoadIconList(Elements el, string PckPath)
        {
            ArchiveEngine pck = new ArchiveEngine(PckPath);
            Bitmap img = LoadDDSImage(((IEnumerable<byte>)pck.ReadFile(pck.PckFile, pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(d => d.Path == "surfaces\\iconset\\iconlist_ivtrm.dds")).ElementAt<PCKFileEntry>(0))).ToArray<byte>());
            return LoadIconList(el, img, CreateLines(pck));
        }

        public static List<Icon> LoadIconList(
          Elements el,
          ArchiveEngine pck)
        {
            Bitmap img = LoadDDSImage(((IEnumerable<byte>)pck.ReadFile(pck.PckFile, pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(d => d.Path == "surfaces\\iconset\\iconlist_ivtrm.dds")).ElementAt<PCKFileEntry>(0))).ToArray<byte>());
            return LoadIconList(el, img, CreateLines(pck));
        }

        public static List<ShopIcon> ReadSurfacesIcons(ArchiveEngine pck)
        {
            List<ShopIcon> shopIconList = new List<ShopIcon>();
            foreach (PCKFileEntry file in pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i =>
            {
                if (i.Path.StartsWith("surfaces\\百宝阁\\") && i.Path.Contains(".dds"))
                    return true;
                return i.Path.StartsWith("surfaces\\竞拍品\\") && i.Path.Contains(".dds");
            })).ToList<PCKFileEntry>())
                shopIconList.Add(new ShopIcon(file.Path, (System.Drawing.Image)LoadDDSImage(((IEnumerable<byte>)pck.ReadFile(pck.PckFile, file)).ToArray<byte>())));
            return shopIconList;
        }

        public static Bitmap LinkImages(List<ShopIcon> Surfaces_icons)
        {
            Bitmap bitmap = new Bitmap(1152, Surfaces_icons.Count * 128 / 9 + 55);
            Graphics graphics = Graphics.FromImage((System.Drawing.Image)bitmap);
            int x = 0;
            int y = 0;
            for (int index = 0; index < Surfaces_icons.Count; ++index)
            {
                graphics.DrawImage(Surfaces_icons[index].Icon, x, y);
                x += 128;
                if (bitmap.Width == x)
                {
                    y += 128;
                    x = 0;
                }
            }
            return bitmap;
        }

        public static Elements ReadElements(
          string FilePath,
          string ApplicationStartUpPath,
          bool RemoveNonItemLists)
        {
            Elements elements = new Elements();
            BinaryReader br = new BinaryReader((Stream)File.Open(FilePath, FileMode.Open));
            elements.Version = (int)br.ReadInt16();
            ReadConfigFile(ApplicationStartUpPath, elements.Version, elements);
            elements.NonObjectListBytesAmount = new Dictionary<int, int>();
            List<int> ls = new List<int>()
      {
        187,
        185,
        183,
        181,
        180,
        179,
        178,
        177,
        176,
        175,
        174,
        173,
        172,
        170,
        169,
        168,
        167,
        166,
        165,
        164,
        163,
        161,
        160,
        159,
        158,
        157,
        156,
        155,
        153,
        152,
        150,
        149,
        148,
        147,
        146,
        145,
        144,
        143,
        142,
        139,
        138,
        137,
        136,
        132,
        131,
        129,
        128,
        (int) sbyte.MaxValue,
        126,
        120,
        111,
        110,
        109,
        105,
        104,
        103,
        102,
        101,
        100,
        94,
        93,
        91,
        90,
        88,
        87,
        85,
        84,
        82,
        81,
        80,
        79,
        78,
        77,
        76,
        73,
        72,
        71,
        70,
        69,
        68,
        67,
        66,
        65,
        64,
        63,
        62,
        61,
        60,
        59,
        58,
        57,
        56,
        55,
        54,
        53,
        52,
        51,
        50,
        49,
        48,
        47,
        46,
        45,
        44,
        43,
        42,
        41,
        40,
        39,
        38,
        37,
        36,
        34,
        32,
        30,
        20,
        18,
        16,
        14,
        13,
        11,
        10,
        8,
        7,
        5,
        4,
        2,
        1,
        0
      };
            if (RemoveNonItemLists)
            {
                for (int index = 0; index < elements.ElementsLists.Count; ++index)
                {
                    if (ls.Contains(index))
                        elements.NonObjectListBytesAmount.Add(index, elements.ElementsLists[index].Types.SumBytes());
                }
            }
            for (int index = 0; index < elements.ElementsLists.Count; ++index)
            {
                if (index != 58)
                {
                    elements.ElementsLists[index].TypesNames.RemoveAt(0);
                    elements.ElementsLists[index].Types.RemoveAt(0);
                }
            }
            br.BaseStream.Seek(2L, SeekOrigin.Current);
            if (elements.Version >= 10)
                br.BaseStream.Seek(4L, SeekOrigin.Current);
            ReadElementLists(br, elements);
            br.Close();
            //if (RemoveNonItemLists)
            //    elements.RemoveNonObjectList(ls);
            elements.Items = elements.ElementsLists.SelectMany<Elements.List, Elements.Item>((Func<Elements.List, IEnumerable<Elements.Item>>)(z => (IEnumerable<Elements.Item>)z.Items)).ToList<Elements.Item>();
            elements.InListAmount = new int[elements.ElementsLists.Count];
            for (int index = 0; index < elements.ElementsLists.Count; ++index)
            {
                List<int> list = elements.ElementsLists.Select<Elements.List, int>((Func<Elements.List, int>)(v => v.ItemsAmount)).ToList<int>();
                list.RemoveRange(index + 1, elements.ElementsLists.Count - (index + 1));
                elements.InListAmount[index] = list.Sum();
            }
            return elements;
        }

        public static List<Desc> LoadItemExtDesc(ArchiveEngine pck)
        {
            IEnumerable<PCKFileEntry> source = pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.StartsWith("configs\\item_ext_desc")));
            byte[] array = ((IEnumerable<byte>)pck.ReadFile(pck.PckFile, source.ElementAt<PCKFileEntry>(0))).ToArray<byte>();
            StreamReader streamReader = new StreamReader((Stream)new MemoryStream(array), Encoding.GetEncoding(936));
            List<string> stringList = new List<string>();
            int num = 0;
            for (int index = 0; index < ((IEnumerable<byte>)array).Count<byte>(); ++index)
            {
                stringList.Add(streamReader.ReadLine());
                if (stringList[index] != null)
                    ++num;
                else
                    break;
            }
            stringList.RemoveAll((Predicate<string>)(v => v == null));
            List<Desc> descList = new List<Desc>();
            foreach (string str in stringList)
            {
                if (!str.StartsWith("/") && !str.StartsWith("#") && (!str.StartsWith("^") && str != "") && str.Contains("\""))
                {
                    string[] strArray = str.Split('"');
                    if (((IEnumerable<string>)strArray).Count<string>() > 1)
                    {
                        int result;
                        int.TryParse(strArray[0], out result);
                        if (result != 0 && result != -1)
                            descList.Add(new Desc(result, strArray[1]));
                    }
                }
            }
            return descList;
        }

        private static object ReadValue(BinaryReader br, string type)
        {
            if (type.Contains("int32") || type.Contains("link") || type.Contains("combo"))
                return (object)br.ReadInt32();
            if (type.Contains("float"))
                return (object)br.ReadSingle();
            if (type.Contains("byte"))
                return (object)br.ReadBytes(Convert.ToInt32(type.Split(':')[1]));
            if (type.Contains("wstring"))
                return (object)Encoding.Unicode.GetString(br.ReadBytes(Convert.ToInt32(type.Split(':')[1]))).ToString().Split(new char[1])[0];
            if (!type.Contains("string"))
                return (object)"";
            return (object)Encoding.GetEncoding("GBK").GetString(br.ReadBytes(Convert.ToInt32(type.Split(':')[1]))).ToString().Split(new char[1])[0];
        }

        private static void ReadConfigFile(string AppStart, int vers, Elements el)
        {
            StreamReader streamReader = new StreamReader(Directory.GetFiles(AppStart + "\\configs", "PW_*_v" + (object)vers + ".cfg")[0], Encoding.UTF8);
            el.ListsAmount = Convert.ToInt32(streamReader.ReadLine());
            el.DialogsListPosition = Convert.ToInt32(streamReader.ReadLine());
            for (int index = 0; index < el.ListsAmount; ++index)
            {
                string str = "";
                while (str == "")
                    str = streamReader.ReadLine();
                el.ElementsLists.Add(new Elements.List()
                {
                    ListName = str,
                    ListType = streamReader.ReadLine(),
                    TypesNames = ((IEnumerable<string>)streamReader.ReadLine().Split(new string[1]
                  {
            ";"
                  }, StringSplitOptions.RemoveEmptyEntries)).ToList<string>(),
                    Types = ((IEnumerable<string>)streamReader.ReadLine().Split(new string[1]
                  {
            ";"
                  }, StringSplitOptions.RemoveEmptyEntries)).ToList<string>()
                });
            }
            streamReader.Close();
        }

        private static void ReadElementLists(BinaryReader br, Elements el)
        {
            for (int index1 = 0; index1 < el.ListsAmount; ++index1)
            {
                if (index1 == 20 && el.Version >= 10)
                {
                    br.ReadBytes(4);
                    byte[] numArray = br.ReadBytes(4);
                    br.ReadBytes(BitConverter.ToInt32(numArray, 0));
                    br.ReadBytes(4);
                }
                if (index1 == 100 && el.Version >= 10)
                {
                    br.ReadBytes(4);
                    byte[] numArray = br.ReadBytes(4);
                    br.ReadBytes(BitConverter.ToInt32(numArray, 0));
                }
                if (el.NonObjectListBytesAmount.Keys.Contains<int>(index1))
                {
                    if (index1 == 57)
                    {
                        el.NpcsList.ItemsAmount = br.ReadInt32();
                        for (int index2 = 0; index2 < el.NpcsList.ItemsAmount; ++index2)
                        {
                            Elements.Item obj = new Elements.Item();
                            obj.Id = br.ReadInt32();
                            obj.Name = br.ReadBytes(64).ToString(Encoding.Unicode);
                            br.BaseStream.Seek((long)(el.NonObjectListBytesAmount[index1] - 68), SeekOrigin.Current);
                            el.NpcsList.Items.Add(obj);
                        }
                    }
                    else if (el.NonObjectListBytesAmount[index1] != -1)
                    {
                        br.BaseStream.Seek((long)(el.NonObjectListBytesAmount[index1] * br.ReadInt32()), SeekOrigin.Current);
                    }
                    else
                    {
                        byte[] bytes = Encoding.GetEncoding("GBK").GetBytes("facedata\\");
                        long position = br.BaseStream.Position;
                        int num = -72 - bytes.Length;
                        bool flag = true;
                        while (flag)
                        {
                            flag = false;
                            for (int index2 = 0; index2 < bytes.Length; ++index2)
                            {
                                ++num;
                                if ((int)br.ReadByte() != (int)bytes[index2])
                                {
                                    flag = true;
                                    break;
                                }
                            }
                        }
                        br.BaseStream.Position = position;
                        br.BaseStream.Seek((long)num, SeekOrigin.Current);
                    }
                }
                else
                {
                    el.ElementsLists[index1].ItemsAmount = br.ReadInt32();
                    el.ElementsLists[index1].Items = new List<Elements.Item>();
                    for (int index2 = 0; index2 < el.ElementsLists[index1].ItemsAmount; ++index2)
                    {
                        Elements.Item obj = new Elements.Item();
                        obj.Id = br.ReadInt32();
                        for (int index3 = 0; index3 < el.ElementsLists[index1].Types.Count; ++index3)
                        {
                            if (el.ElementsLists[index1].TypesNames[index3] == "Name" | el.ElementsLists[index1].TypesNames[index3] == "Иконка" | el.ElementsLists[index1].TypesNames[index3] == "file_icon" | el.ElementsLists[index1].TypesNames[index3] == "Кол-во в ячейке" | el.ElementsLists[index1].TypesNames[index3] == "pile_num_max")
                            {
                                if (el.ElementsLists[index1].TypesNames[index3] == "Name")
                                    obj.Name = br.ReadBytes(64).ToString(Encoding.Unicode);
                                else if (el.ElementsLists[index1].TypesNames[index3] == "Иконка" | el.ElementsLists[index1].TypesNames[index3] == "file_icon")
                                    obj.Icon = br.ReadBytes(128).ToString(Encoding.GetEncoding(936)).GetIconNameFromString();
                                else
                                    obj.MaxAmount = br.ReadInt32();
                            }
                            else
                                obj.Values.Add(ReadValue(br, el.ElementsLists[index1].Types[index3]));
                        }
                        el.ElementsLists[index1].Items.Add(obj);
                    }
                }
            }
        }

        private static List<string> CreateLines(ArchiveEngine pck)
        {
            byte[] array = ((IEnumerable<byte>)pck.ReadFile(pck.PckFile, pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path == "surfaces\\iconset\\iconlist_ivtrm.txt")).ElementAt<PCKFileEntry>(0))).ToArray<byte>();
            List<string> stringList = new List<string>();
            StreamReader streamReader = new StreamReader((Stream)new MemoryStream(array), Encoding.GetEncoding(936));
            int num = 0;
            for (int index = 0; index < ((IEnumerable<byte>)array).Count<byte>(); ++index)
            {
                stringList.Add(streamReader.ReadLine());
                if (stringList[index] != null)
                    ++num;
                else
                    break;
            }
            stringList.RemoveAll((Predicate<string>)(v => v == null));
            stringList.ForEach((Action<string>)(z => z.ToLower()));
            return stringList;
        }

        public class ShopIcon
        {
            public string Name;
            public System.Drawing.Image Icon;

            public ShopIcon(string d, System.Drawing.Image b)
            {
                this.Name = d;
                this.Icon = b;
            }
        }

        public class Elements
        {
            public List<Elements.List> ElementsLists = new List<Elements.List>();
            public List<Elements.Item> Items = new List<Elements.Item>();
            public int[] InListAmount;
            public int Version;
            public int ListsAmount;
            public int DialogsListPosition;
            public Elements.List NpcsList = new Elements.List();
            public Dictionary<int, int> NonObjectListBytesAmount;

            public class List
            {
                public int ItemsAmount;
                public List<Elements.Item> Items = new List<Elements.Item>();

                public string ListName { get; set; }

                public string ListType { get; set; }

                public List<string> TypesNames { get; set; }

                public List<string> Types { get; set; }
            }

            public class Item
            {
                public int Id;
                public string Name;
                public int MaxAmount;
                public string Icon;
                public List<object> Values = new List<object>();
                public System.Drawing.Image IconImage = (System.Drawing.Image)Small;
                public System.Drawing.Image Standard_image = (System.Drawing.Image)Big;
            }
        }

        public class Desc
        {
            public int Id;
            public string Description;

            public Desc(int i, string d)
            {
                this.Id = i;
                this.Description = d;
            }
        }

        public class Icon
        {
            public List<int> IDS;
            public string IconName;
            public Bitmap StandardImage;
            public Bitmap ResizedImage;
        }
    }
}
