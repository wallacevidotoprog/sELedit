using sELedit.configs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sELedit
{
    class Extensions
    {
        public static string ProbValueFormat;

        private static InfoTool ift;

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);
            try
            {

                destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

                using (var graphics = Graphics.FromImage(destImage))
                {
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    using (var wrapMode = new ImageAttributes())
                    {
                        wrapMode.SetWrapMode(WrapMode.TileFlipXY);

                        graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                    }
                }

                return destImage;
            }
            catch (Exception)
            {

                
            }
            return destImage;
        }

        public static Image IdImageRecipe(int Id,out int idiTEM, bool outID = false)
        {
            Image imgg = null;
            int IdItem = 0;
            try
            {                
                int pos_item = 0;
                int La = 69;

                for (int i = 0; i < MainWindow.eLC.Lists[La].elementFields.Length; i++)
                {
                    if (MainWindow.eLC.Lists[La].elementFields[i] == "targets_1_id_to_make")
                    {
                        pos_item = i;

                        break;
                    }
                }


                for (int ef = 0; ef < MainWindow.eLC.Lists[La].elementValues.Length; ef++)
                {
                    if (Id == int.Parse(MainWindow.eLC.GetValue(La, ef, 0)))
                    {                       
                        IdItem = int.Parse(MainWindow.eLC.GetValue(La, ef, pos_item));
                        if (outID)
                        {
                            break;
                        }
                        imgg = IdImageItem(int.Parse(MainWindow.eLC.GetValue(La, ef, pos_item)));
                        break;
                    }
                }

                idiTEM = IdItem;
                return imgg;

            }
            catch
            {
                imgg = Properties.Resources.unknown;
            }
            if (imgg ==null)
            {
                imgg = Properties.Resources.bloco;
            }

            idiTEM = IdItem;
            return imgg;           
        }

        public static Image IdImageItem(int Id)
        {
            Image imgg = Properties.Resources.bloco_a;
            try
            {               
                int pos_icon = 0;
               

                for (int L = 0; L < MainWindow.eLC.Lists.Length; L++)
                {
                    if (MainWindow.eLC.Lists[L].itemUse==true)
                    {                       

                        for (int i = 0; i < MainWindow.eLC.Lists[L].elementFields.Length; i++)
                        {
                            if (MainWindow.eLC.Lists[L].elementFields[i] == "file_icon")
                            {
                                pos_icon = i;
                                break;
                            }
                        }

                        for (int ef = 0; ef < MainWindow.eLC.Lists[L].elementValues.Length; ef++)
                        {
                            if (Id == int.Parse(MainWindow.eLC.GetValue(L, ef, 0)))
                            {
                                String path = Path.GetFileName(MainWindow.eLC.GetValue(L, ef, pos_icon));
                                if (MainWindow.database.ContainsKey(path))
                                {
                                    return MainWindow.database.images(path);
                                }
                                else
                                {
                                    return MainWindow.database.images("unknown.dds");
                                }


                            }
                        }
                    }
                }


            }
            catch (Exception)
            {

                imgg = Properties.Resources.bloco_a;
            }

            return imgg;

            #region old
            //Image imgg = Properties.Resources.bloco_a;
            //try
            //{
            //    string value = "";
            //    int pos_item = 0;
            //    int La = 0;


            //    for (int L = 0; L < MainWindow.database.ItemUse.Count; L++)
            //    {
            //        La = int.Parse(MainWindow.database.ItemUse.GetKey(L).ToString());

            //        for (int i = 0; i < MainWindow.eLC.Lists[La].elementFields.Length; i++)
            //        {
            //            if (MainWindow.eLC.Lists[La].elementFields[i] == "Name")
            //            {
            //                pos_item = i;
            //                break;
            //            }
            //        }
            //        bool tstm = false;
            //        for (int ef = 0; ef < MainWindow.eLC.Lists[La].elementValues.Length; ef++)
            //        {
            //            value = MainWindow.eLC.GetValue(La, ef, pos_item);

            //            var a = int.Parse(MainWindow.eLC.GetValue(La, ef, 0));

            //            if (Id == a)
            //            {

            //                for (int k = 0; k < MainWindow.eLC.Lists[La].elementFields.Length; k++)
            //                {
            //                    if (MainWindow.eLC.Lists[La].elementFields[k] == "file_icon")
            //                    {
            //                        string image = MainWindow.eLC.GetValue(La, ef, k);
            //                        String path = Path.GetFileName(image);
            //                        if (MainWindow.database.ContainsKey(path))
            //                        {
            //                            imgg = MainWindow.database.images(path);
            //                        }
            //                        else
            //                        {
            //                            imgg = MainWindow.database.images("unknown.dds");
            //                        }
            //                        tstm = true;
            //                        break;
            //                    }

            //                }

            //            }
            //            if (tstm)
            //            {
            //                break;
            //            }
            //        }
            //        if (tstm)
            //        {
            //            break;
            //        }

            //    }


            //}
            //catch (Exception)
            //{

            //    imgg = Properties.Resources.bloco_a;
            //}

            //return imgg;
            #endregion
        }

        public static string IdNameItem(int Id)
		{
			string value = "";
			int pos_item = 0;
			int La = 0;
			string Name = "";
			try
			{
                bool tstm = false;
                for (int L = 0; L < MainWindow.database.ItemUse.Count; L++)
				{
					La = int.Parse(MainWindow.database.ItemUse.GetKey(L).ToString());

					for (int i = 0; i < MainWindow.eLC.Lists[La].elementFields.Length; i++)
					{
						if (MainWindow.eLC.Lists[La].elementFields[i] == "Name")
						{
							pos_item = i;
							break;
						}
					}
					for (int ef = 0; ef < MainWindow.eLC.Lists[La].elementValues.Length; ef++)
					{
						value = MainWindow.eLC.GetValue(La, ef, pos_item);
						var a = int.Parse(MainWindow.eLC.GetValue(La, ef, 0));
						if (Id == a)
						{
							Name = value;
                            tstm = true;

                            break;
						}
					}
                    if (tstm)
                    {
                        break;
                    }

                }


			}
			catch (Exception)
			{

				Name = "Err";
			}
			return Name;
		}

        public static string IdNameItemRecipe(int idR)
        {
            string value = "";
            int pos_item = 0;
            int La = 69;
            try
            {
                for (int i = 0; i < MainWindow.eLC.Lists[La].elementFields.Length; i++)
                {
                    if (MainWindow.eLC.Lists[La].elementFields[i] == "targets_1_id_to_make")
                    {
                        pos_item = i;

                        break;
                    }
                }
                for (int ef = 0; ef < MainWindow.eLC.Lists[La].elementValues.Length; ef++)
                {
                    //value = MainWindow.eLC.GetValue(La, ef, pos_item);
                    int a = int.Parse(MainWindow.eLC.GetValue(La, ef, 0));
                    if (idR == a)
                    {
                        string Name = MainWindow.eLC.GetValue(La, ef, pos_item);
                        value = IdNameItem(int.Parse(Name));
                        break;
                    }
                }
            }
            catch (Exception)
            {
                value = "ERRO";


            }
            return value;


        }

        public static int GetIdItemFromGDV(string txt)
        {
            if (!string.IsNullOrEmpty(txt))
            {
                return int.Parse(txt?.Replace("[", "").Replace("]", "")?.Split(new string[] { "-" }, StringSplitOptions.None)[0].Replace(" ", null) ?? "");
            }
            else
            {
                return 0;
            }
            
        }

        public static string SetIdNameItemFromGDV(string id,string[] removes=null)
        {
            if (!string.IsNullOrEmpty(id))
            {
                if (removes is null)
                {
                    return "[" + id + "] - " + Extensions.IdNameItem(int.Parse(id)); ;
                }
                else
                {
                    string _ret = "[" + id + "] - " + Extensions.IdNameItem(int.Parse(id));
                    for (int i = 0; i < removes.Length; i++)
                    {
                        _ret = _ret.Replace(removes[i], null);
                    }
                    return _ret;
                }
                
            }
            else
            {
                return "";
            }

        }

        public static string SetIdNameRecipeFromGDV(string id, string[] removes = null)
        {
            if (!string.IsNullOrEmpty(id))
            {
                if (removes is null)
                {
                    return "[" + id + "] - " + Extensions.IdNameItemRecipe(int.Parse(id));
                }
                else
                {
                    string _ret = "[" + id + "] - " + Extensions.IdNameItemRecipe(int.Parse(id));
                    for (int i = 0; i < removes.Length; i++)
                    {
                        _ret = _ret.Replace(removes[i], null);
                    }
                    return _ret;
                }

            }
            else
            {
                return "";
            }

        }

        public static Color ColorHex(string txt)
        {

            return Color.FromArgb(int.Parse(txt.Replace("^", null).Substring(0, 6), NumberStyles.HexNumber));
           
            
        }

        public static (byte[], eELedit.Previews.Models.TexturesFromBytes[]) ViewerSKI(string txt)
        {
            string caminho = $@"C:\xampp\htdocs\FTP CELULAR\Perfect World\Perfect World PWLoko\element\{txt.Split(new string[] { @"\" }, StringSplitOptions.None)[0].ToLower()}.pck";
            if (File.Exists(caminho))
            {
                var stopwatch = new System.Diagnostics.Stopwatch(); stopwatch.Start();

                var pck = new PCKs(caminho);
                byte[] ski = null;
                using (StreamReader sr = new StreamReader( new MemoryStream(((IEnumerable<byte>)pck.ReadFile(pck.PckFile, (pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.StartsWith(txt.ToLower())))).ElementAt<PCKFileEntry>(0))).ToArray<byte>()), Encoding.GetEncoding("GBK")))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        if (line.StartsWith("SkinModelPath") && line.ToUpper().EndsWith("SMD"))
                        {
                            line = line.Replace("SkinModelPath:", null).Replace(" ", null);
                            var smd = new Previews.Structures.SmdFile(((IEnumerable<byte>)pck.ReadFile(pck.PckFile, (pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.StartsWith(line.ToLower())))).ElementAt<PCKFileEntry>(0))).ToArray<byte>());
                            IEnumerable<PCKFileEntry> sourceSKI = pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.EndsWith(smd.SkiFile.ToLower())));
                            ski = ((IEnumerable<byte>)pck.ReadFile(pck.PckFile, sourceSKI.ElementAt<PCKFileEntry>(0))).ToArray<byte>();
                            break;
                        }
                    }
                }
                //var ecm = new Previews.Structures.EcmFileStructure(((IEnumerable<byte>)pck.ReadFile(pck.PckFile, (pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.StartsWith(txt.ToLower())))).ElementAt<PCKFileEntry>(0))).ToArray<byte>());
                //var smd = new Previews.Structures.SmdFile(((IEnumerable<byte>)pck.ReadFile(pck.PckFile, (pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.StartsWith(ecm.Smd.ToLower())))).ElementAt<PCKFileEntry>(0))).ToArray<byte>());
                //IEnumerable<PCKFileEntry>  sourceSKI = pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.EndsWith(smd.SkiFile.ToLower())));
                
                var sds = txt.Replace(Path.GetFileName(txt), "textures");
                IEnumerable <PCKFileEntry> source = pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.StartsWith(txt.Replace(Path.GetFileName(txt),null).ToLower()) && i.Path.EndsWith("dds")));
                var texturesBytes = new eELedit.Previews.Models.TexturesFromBytes[source.Count()];
                int nun = 0;
                foreach (var item in source)
                {
                    texturesBytes[nun] = new eELedit.Previews.Models.TexturesFromBytes();
                    texturesBytes[nun]._name = Path.GetFileName(item.Path);
                    texturesBytes[nun]._file = pck.ReadFile(pck.PckFile, source.ElementAt(nun)).ToArray();
                    nun++;
                }
                stopwatch.Stop();
               // System.Windows.Forms.MessageBox.Show(stopwatch.Elapsed.ToString()); 00.01.777
                return (ski, texturesBytes);
            }
            return (null, null);
            
        }

        


        internal static string ByteArray_to_HexString(byte[] value)
        {
            return BitConverter.ToString(value);
        }
        internal static byte[] HexString_to_ByteArray(string value)
        {
            char[] chArray = new char[]
			{
				'-'
			};
            string[] strArray = value.Split(chArray);
            byte[] numArray = new byte[strArray.Length];
            for (int index = 0; index < strArray.Length; index++)
            {
                numArray[index] = Convert.ToByte(strArray[index], 16);
            }
            return numArray;
        }

        internal static string ByteArray_to_GbkString(byte[] text)
        {
            Encoding encoding = Encoding.GetEncoding("GBK");
            char[] array = new char[1];
            char[] chArray = array;
            return encoding.GetString(text).Split(chArray)[0];
        }
        internal static byte[] GbkString_to_ByteArray(string text, int length)
        {
            Encoding encoding = Encoding.GetEncoding("GBK");
            byte[] numArray = new byte[length];
            byte[] bytes = encoding.GetBytes(text);
            if (numArray.Length > bytes.Length)
            {
                Array.Copy(bytes, numArray, bytes.Length);
            }
            else
            {
                byte[] numArray2 = bytes;
                byte[] numArray3 = numArray;
                int length2 = numArray3.Length;
                Array.Copy(numArray2, numArray3, length2);
            }
            return numArray;
        }
        public static byte[] GbkString_to_ByteArray2(string text, int length)
        {
            Encoding enc = Encoding.GetEncoding("GBK");
            byte[] target = new byte[length];
            byte[] source = enc.GetBytes(text);
            if (target.Length > source.Length)
            {
                Array.Copy(source, target, source.Length);
            }
            else
            {
                Array.Copy(source, target, target.Length);
            }
            return target;
        }

        internal static string ByteArray_to_UnicodeString(byte[] text)
        {
            Encoding encoding = Encoding.GetEncoding("Unicode");
            char[] array = new char[1];
            char[] chArray = array;
            return encoding.GetString(text).Split(chArray)[0];
        }
        internal static byte[] UnicodeString_to_ByteArray(string text, int length)
        {
            Encoding encoding = Encoding.GetEncoding("Unicode");
            byte[] numArray = new byte[length];
            byte[] bytes = encoding.GetBytes(text);
            if (numArray.Length > bytes.Length)
            {
                Array.Copy(bytes, numArray, bytes.Length);
            }
            else
            {
                byte[] numArray2 = bytes;
                byte[] numArray3 = numArray;
                int length2 = numArray3.Length;
                Array.Copy(numArray2, numArray3, length2);
            }
            return numArray;
        }
        public static byte[] UnicodeString_to_ByteArray2(string text, int length)
        {
            Encoding enc = Encoding.GetEncoding("Unicode");
            byte[] target = new byte[length];
            byte[] source = enc.GetBytes(text);
            if (target.Length > source.Length)
            {
                Array.Copy(source, target, source.Length);
            }
            else
            {
                Array.Copy(source, target, target.Length);
            }
            return target;
        }

        public static string SecondsToString(uint time)
        {
            uint days = time / 86400;
            time = time - (days * 86400);
            uint hours = time / 3600;
            time = time - (hours * 3600); ;
            uint minutes = time / 60;
            uint seconds = time - (minutes * 60);
            return (days.ToString("D2") + "-" + hours.ToString("D2") + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2"));
        }
        public static uint StringToSecond(string time)
        {
            char[] chArray = new char[]
			{
				'-', ':'
			};
            string[] times = time.Split(chArray);
            return (86400 * Convert.ToUInt32(times[0])) + (3600 * Convert.ToUInt32(times[1])) + (60 * Convert.ToUInt32(times[2])) + Convert.ToUInt32(times[3]);
        }

        public static string ConvertToClientX(float x)
        {
            double cx = 400 + Math.Truncate(x * 0.1);
            return cx.ToString();
        }
        public static string ConvertToClientY(float y)
        {
            double cy = Math.Truncate(y * 0.1);
            return cy.ToString();
        }
        public static string ConvertToClientZ(float z)
        {
            double cz = 550 + Math.Truncate(z * 0.1);
            return cz.ToString();
        }

        

        public static string ItemDesc(int id)
        {
            string result;
            if (id != 0)
            {
                try
                {


                    if (MainWindow.database.item_ext_desc.ContainsKey(id.ToString()))
                    {
                        result = MainWindow.database.item_ext_desc[id.ToString()].ToString() ;
                    }
                    else
                    {
                        result = "";
                    }

                }
                catch(Exception ex)
                {
                    //System.Windows.Forms.MessageBox.Show(ex.ToString());
                    result = "";
                }
            }
            else
            {
                result = Extensions.GetLocalization(402);
            }

            return result.Replace("\\r", Environment.NewLine).Replace("\\t", "" + (char)9);
        }

        public static string SkillName(int id)
        {
            string result;
            string nameid = id.ToString() + "0";
            try
            {
                int index = -1;
                for (int num25 = 0; num25 < MainWindow.database.skillstr.Length - 1; num25 += 4)
                {
                    if (Convert.ToInt32(MainWindow.database.skillstr[num25 + 0]) == Convert.ToInt32(nameid))
                    {
                        index = num25 + 1;
                        break;
                    }
                }
                result = MainWindow.database.skillstr[index];
            }
            catch
            {
                result = Extensions.GetLocalization(404);
            }

            return result/*.Replace("\\n", Environment.NewLine).Replace("\\t", "" + (char)9)*/;
        }

        public static string SkillDesc(int id)
        {
            string result;
            string descid = id.ToString() + "1";
            try
            {
                int index = -1;
                for (int num25 = 0; num25 < MainWindow.database.skillstr.Length - 1; num25 += 4)
                {
                    if (Convert.ToInt32(MainWindow.database.skillstr[num25 + 2]) == Convert.ToInt32(descid))
                    {
                        index = num25 + 3;
                        break;
                    }
                }
                result = MainWindow.database.skillstr[index];
            }
            catch
            {
                result = Extensions.GetLocalization(404);
            }

            return result.Replace("%%", "%")/*.Replace("\\n", Environment.NewLine).Replace("\\t", "" + (char)9)*/;
        }

        public static string SkillText(int id)
        {
            string result;
            if (id != 0)
            {
                try
                {
                    string name = Extensions.SkillName(id);
                    string desc = Extensions.SkillDesc(id);
                    if (name != Extensions.GetLocalization(404) && desc != Extensions.GetLocalization(404))
                    {
                        if (name != "")
                        {
                            if (desc != "")
                            {
                                result = name + "\n\n" + ColorClean(desc);
                            }
                            else
                            {
                                result = name;
                            }
                        }
                        else
                        {
                            result = ColorClean(desc);
                        }
                    }
                    else
                    {
                        result = Extensions.GetLocalization(404);
                    }
                }
                catch
                {
                    result = Extensions.GetLocalization(404);
                }
            }
            else
            {
                result = Extensions.GetLocalization(402);
            }

            return result.Replace("\\n", Environment.NewLine).Replace("\\t", "" + (char)9);
        }

       

        public static string GetLocalization(int key)
        {
            string result;
            try
            {
                if (MainWindow.LocalizationText.ContainsKey(key.ToString()))
                {
                    result = MainWindow.LocalizationText[key.ToString()].ToString();
                }
                else
                {
                    result = "NOT FOUND KEY " + key;
                }
            }
            catch
            {
                result = "NOT FOUND KEY " + key;
            }
            return result.Replace("\\n", Environment.NewLine).Replace("\\t", "" + (char)9);
        }

        public static int DigitNumberToInt32(object value)
        {
            string result = Convert.ToString(value).Replace("" + (char)160, "").Replace("" + (char)32, "");
            return Convert.ToInt32(result);
        }

        public static float PercentNumberToSingle(object value, bool EnableShowPercents)
        {
            if (EnableShowPercents == true)
            {
                float result = Convert.ToSingle(Convert.ToString(value).Replace("%", ""));
                return Convert.ToSingle(result * 0.01);
            }
            else
            {
                float result = Convert.ToSingle(value);
                return result;
            }
        }

        public static string ColorClean(string line)
        {
            if (line == ""|| line.Length <= 1) { return ""; }
            string[] blocks = line.Split(new char[] { '^' });
            if (blocks.Length > 1)
            {
                string result = "";

                if (blocks[0] != "")
                {
                    result += blocks[0];
                }
                for (int i = 1; i < blocks.Length; i++)
                {
                    if (blocks[i] != "")
                    {
                        result += blocks[i].Substring(6);
                    }
                }

                return result;
            }
            else
            {
                return line;
            }
        }

        public static string Get_proc_type(string proc_type)
        {
            string line = string.Empty;
            uint proctypes;
            bool result = uint.TryParse(proc_type, out proctypes);
            List<uint> powers = new List<uint>(Extensions.GetPowers(proctypes));


            for (int p = 0; p < powers.Count; p++)
            {
                if (powers[p] == 0) continue;

                switch (p)
                {
                    case 0:
                        line += Extensions.GetLocalization(3000) + "  ";//proc_type_1
                        break;
                    case 1:
                        line += Extensions.GetLocalization(3001) + "  ";//proc_type_2
                        break;
                    case 2:
                        line += Extensions.GetLocalization(3002) + "  ";//proc_type_4
                        break;
                    case 3:
                        line += Extensions.GetLocalization(3003) + "  ";//proc_type_8
                        break;
                    case 4:
                        line += Extensions.GetLocalization(3004) + "  ";//proc_type_16
                        break;
                    case 5:
                        line += Extensions.GetLocalization(3005) + "  ";//proc_type_32
                        break;
                    case 7:
                        line += Extensions.GetLocalization(3007) + "  ";//proc_type_128
                        break;
                    case 8:
                        line += Extensions.GetLocalization(3008) + "  ";//proc_type_256
                        break;
                    case 9:
                        line += Extensions.GetLocalization(3009) + "  ";//proc_type_512
                        break;
                    case 10:
                        line += Extensions.GetLocalization(3010) + "  ";//proc_type_1024
                        break;
                    case 11:
                        line += Extensions.GetLocalization(3011) + "  ";//proc_type_2048
                        break;
                    case 12:
                        line += Extensions.GetLocalization(3012) + "  ";//proc_type_4096
                        break;
                    case 14:
                        line += Extensions.GetLocalization(3014) + "  ";//proc_type_16384
                        break;
                }
            }

            return line;

        }

        public static string GetItemProps(int Id, uint Period)
        {
            string line = "";
            uint proctypes;
            int l = 0;
            int pos_item = -1;
            int pos_proc_type = -1;
            bool Suc = false;
            try
            {
                for (int i = 0; i < MainWindow.database.task_items_list.Length; i += 2)
                {
                    if (MainWindow.eLC.Version >= Convert.ToInt32(MainWindow.database.task_items_list[i + 1]))
                    {
                        l = Convert.ToInt32(MainWindow.database.task_items_list[i]);
                        for (int t = 0; t < MainWindow.eLC.Lists[l].elementValues.Length; t++)
                        {
                            if (Convert.ToInt32(MainWindow.eLC.GetValue(l, t, 0)) == Id)
                            {
                                pos_item = t;
                                Suc = true;
                                break;
                            }
                        }
                        if (Suc == true) break;
                    }
                    if (pos_item == -1)
                    {
                        return line;
                    }
                    for (int k = 0; k < MainWindow.eLC.Lists[l].elementFields.Length; k++)
                    {
                        if (MainWindow.eLC.Lists[l].elementFields[k] == "Name")
                        {
                            line += MainWindow.eLC.GetValue(l, pos_item, k);
                            break;
                        }
                    }
                    if (Period != 0)
                    {
                        line += "\n" + Extensions.GetLocalization(7113) + Extensions.ItemPropsSecondsToString2(Period);
                    }
                    for (int k = 0; k < MainWindow.eLC.Lists[l].elementFields.Length; k++)
                    {
                        if (MainWindow.eLC.Lists[l].elementFields[k] == "proc_type")
                        {
                            pos_proc_type = k;
                            break;
                        }
                    }
                    bool result = uint.TryParse(MainWindow.eLC.GetValue(l, pos_item, pos_proc_type), out proctypes);
                    List<uint> powers = new List<uint>(Extensions.GetPowers(proctypes));
                    for (int p = 0; p < powers.Count; p++)
                    {
                        if (powers[p] == 0) continue;

                        switch (p)
                        {
                            case 6:
                                line += "\n" + Extensions.GetLocalization(3006);//proc_type_64
                                break;
                            case 15:
                                line += "\n" + Extensions.GetLocalization(3015);//proc_type_32768
                                break;
                        }
                    }
                    for (int p = 0; p < powers.Count; p++)
                    {
                        if (powers[p] == 0) continue;

                        switch (p)
                        {
                            case 13:
                                line += Extensions.GetLocalization(3013);//proc_type_8192
                                break;
                        }
                    }
                    if (l == 3) line += WEAPON_ESSENCE.GetProps(pos_item);
                    if (l == 6) line += ARMOR_ESSENCE.GetProps(pos_item);
                    if (l == 9) line += DECORATION_ESSENCE.GetProps(pos_item);
                    if (l == 12) line += MEDICINE_ESSENCE.GetProps(pos_item);
                    if (l == 17) line += DAMAGERUNE_ESSENCE.GetProps(pos_item);
                    if (l == 19) line += ARMORRUNE_ESSENCE.GetProps(pos_item);
                    if (l == 22) line += FLYSWORD_ESSENCE.GetProps(pos_item);
                    if (l == 23) line += WINGMANWING_ESSENCE.GetProps(pos_item);
                    if (l == 27) line += ELEMENT_ESSENCE.GetProps(pos_item);
                    if (l == 28) line += "\n" + Extensions.GetLocalization(7118);
                    if (l == 31) line += PROJECTILE_ESSENCE.GetProps(pos_item);
                    if (l == 83) line += FASHION_ESSENCE.GetProps(pos_item);
                    if (l == 89) line += FACEPILL_ESSENCE.GetProps(pos_item);
                    if (l == 95) line += PET_EGG_ESSENCE.GetProps(pos_item);
                    if (l == 96) line += PET_FOOD_ESSENCE.GetProps(pos_item);
                    if (l == 98) line += FIREWORKS_ESSENCE.GetProps(pos_item);
                    if (l == 106) line += SKILLMATTER_ESSENCE.GetProps(pos_item);
                    if (l == 107) line += REFINE_TICKET_ESSENCE.GetProps(pos_item);
                    if (l == 114) line += AUTOHP_ESSENCE.GetProps(pos_item);
                    if (l == 115) line += AUTOMP_ESSENCE.GetProps(pos_item);
                    if (l == 119) line += GOBLIN_ESSENCE.GetProps(pos_item);
                    if (l == 121) line += GOBLIN_EQUIP_ESSENCE.GetProps(pos_item);
                    if (l == 122) line += GOBLIN_EXPPILL_ESSENCE.GetProps(pos_item);
                    if (l == 123) line += SELL_CERTIFICATE_ESSENCE.GetProps(pos_item);
                    if (l == 124) line += TARGET_ITEM_ESSENCE.GetProps(pos_item);
                    if (l == 130) line += INC_SKILL_ABILITY_ESSENCE.GetProps(pos_item);
                    if (l == 133) line += WEDDING_BOOKCARD_ESSENCE.GetProps(pos_item);
                    if (l == 135) line += SHARPENER_ESSENCE.GetProps(pos_item);
                    if (l == 141) line += CONGREGATE_ESSENCE.GetProps(pos_item);
                    if (l == 151) line += FORCE_TOKEN_ESSENCE.GetProps(pos_item);
                    if (l == 184) line += POKER_ESSENCE.GetProps(pos_item);
                    if (l == 191) line += UNIVERSAL_TOKEN_ESSENCE.GetProps(pos_item);
                    if (l == 197) line += ASTROLABE_ESSENCE.GetProps(pos_item);
                    if (l == 212) line += FIREWORKS2_ESSENCE.GetProps(pos_item);
                    if (l == 218) line += HOME_FORMULAS_ITEM_ESSENCE.GetProps(pos_item);
                    if (l != 3 && l != 6 && l != 9 && l != 12 && l != 17 && l != 19 && l != 22 && l != 23 &&
                        l != 27 && l != 31 && l != 83 && l != 89 && l != 95 && l != 96 && l != 98 && l != 106 &&
                        l != 107 && l != 114 && l != 115 && l != 119 && l != 121 && l != 122 && l != 123 &&
                        l != 124 && l != 130 && l != 133 && l != 135 && l != 141 && l != 151 && l != 184 &&
                        l != 191 && l != 197 && l != 212 && l != 218)
                    {
                        for (int k = 0; k < MainWindow.eLC.Lists[l].elementFields.Length; k++)
                        {
                            if (MainWindow.eLC.Lists[l].elementFields[k] == "price")
                            {
                                string price = MainWindow.eLC.GetValue(l, pos_item, k);
                                if (price != "0")
                                {
                                    line += "\n" + Extensions.GetLocalization(7024) + " " + Convert.ToInt32(price).ToString("N0", CultureInfo.CreateSpecificCulture("zh-CN"));
                                }
                                break;
                            }
                        }
                    }
                    line += "\n\n";
                    for (int p = 0; p < powers.Count; p++)
                    {
                        if (powers[p] == 0) continue;

                        switch (p)
                        {
                            case 0:
                                line += Extensions.GetLocalization(3000);//proc_type_1
                                break;
                            case 1:
                                line += Extensions.GetLocalization(3001);//proc_type_2
                                break;
                            case 2:
                                line += Extensions.GetLocalization(3002);//proc_type_4
                                break;
                            case 3:
                                line += Extensions.GetLocalization(3003);//proc_type_8
                                break;
                            case 4:
                                line += Extensions.GetLocalization(3004);//proc_type_16
                                break;
                            case 5:
                                line += Extensions.GetLocalization(3005);//proc_type_32
                                break;
                            case 7:
                                line += Extensions.GetLocalization(3007);//proc_type_128
                                break;
                            case 8:
                                line += Extensions.GetLocalization(3008);//proc_type_256
                                break;
                            case 9:
                                line += Extensions.GetLocalization(3009);//proc_type_512
                                break;
                            case 10:
                                line += Extensions.GetLocalization(3010);//proc_type_1024
                                break;
                            case 11:
                                line += Extensions.GetLocalization(3011);//proc_type_2048
                                break;
                            case 12:
                                line += Extensions.GetLocalization(3012);//proc_type_4096
                                break;
                            case 14:
                                line += Extensions.GetLocalization(3014);//proc_type_16384
                                break;
                        }
                    }
                }
            }
            catch
            {
                line = "";
            }
            return line;
        }

        public static string GetNameItem(int b)
        {
            string ret = null;
            bool fi = false;
            string value;
			int La;
			for (int L = 0; L < MainWindow.database.ItemUse.Count; L++)
			{
				La = int.Parse(MainWindow.database.ItemUse.GetKey(L).ToString());

				 
                int pos = 0;
                int posN = 0;
                for (int i = 0; i < MainWindow.eLC.Lists[La].elementFields.Length; i++)
                {
                    if (MainWindow.eLC.Lists[La].elementFields[i] == "Name")
                    {
                        posN = i;
                        //break;
                    }
                   
                }
                for (int ef = 0; ef < MainWindow.eLC.Lists[La].elementValues.Length; ef++)
                {
                    value = MainWindow.eLC.GetValue(La, ef, pos);

                    if (b == int.Parse(MainWindow.eLC.GetValue(La, ef, 0)))
                    {

                            ret = MainWindow.eLC.GetValue(La, ef, posN);
                            fi = true;

                    }
                }

                if (fi == true)
                {
                    break;
                }
            }
            return ret;
        }

        public static  InfoTool GetItemPropsFromID(int Id) 
        { 
            ift = null;
            try
            {
                for (int i = 0; i < MainWindow.eLC.Lists.Length; i++)
                {
                    if (MainWindow.eLC.Lists[i].itemUse == true)
                    {
                        for (int ef = 0; ef < MainWindow.eLC.Lists[i].elementValues.Length; ef++)
                        {
                            var a = int.Parse(MainWindow.eLC.GetValue(i, ef, 0));
                            if (Id == a)
                            {
                                ift = GetItemPropsByILP(Id, 0, i, ef);
                                goto end;
                            }
                        }
                    }
                } 
            }
            catch { }
            end:
            return ift;
        } //Só ID


        public static InfoTool GetItemPropsByILP(int Id, uint Period, int l, int pos_item) /// PADRAO 
        {
           ift = new InfoTool();
            uint proctypes;
            int pos_proc_type = -1;
            ift.itemId = Id;

            try
            {

                for (int k = 0; k < MainWindow.eLC.Lists[l].elementFields.Length; k++)
                {
                    if (MainWindow.eLC.Lists[l].elementFields[k] == "Name")
                    {
                        ift.name = MainWindow.eLC.GetValue(l, pos_item, k);
                        break;
                    }
                }
                if (Period != 0)
                {
                    ift.time = "\n" + Extensions.GetLocalization(7113) + Extensions.ItemPropsSecondsToString2(Period);
                }
                for (int k = 0; k < MainWindow.eLC.Lists[l].elementFields.Length; k++)
                {
                    if (MainWindow.eLC.Lists[l].elementFields[k] == "proc_type")
                    {
                        pos_proc_type = k;
                        break;
                    }
                }
                bool result = uint.TryParse(MainWindow.eLC.GetValue(l, pos_item, pos_proc_type), out proctypes);
                List<uint> powers = new List<uint>(Extensions.GetPowers(proctypes));
                for (int p = 0; p < powers.Count; p++)
                {
                    if (powers[p] == 0) continue;

                    switch (p)
                    {
                        case 6:
                            ift.powers += "\n" + Extensions.GetLocalization(3006);//proc_type_64
                            break;
                        case 15:
                            ift.powers += "\n" + Extensions.GetLocalization(3015);//proc_type_32768
                            break;
                    }
                }

                for (int p = 0; p < powers.Count; p++)
                {
                    if (powers[p] == 0) continue;

                    switch (p)
                    {
                        case 13:
                            ift.powers += Extensions.GetLocalization(3013);//proc_type_8192
                            break;
                    }
                }
                
                if (l == 3) ift.addons += WEAPON_ESSENCE.GetProps(pos_item);
                if (l == 6) ift.addons += ARMOR_ESSENCE.GetProps(pos_item);
                if (l == 9) ift.addons += DECORATION_ESSENCE.GetProps(pos_item);
                if (l == 12) ift.addons += MEDICINE_ESSENCE.GetProps(pos_item);
                if (l == 17) ift.addons += DAMAGERUNE_ESSENCE.GetProps(pos_item);
                if (l == 19) ift.addons += ARMORRUNE_ESSENCE.GetProps(pos_item);
                if (l == 22) ift.addons += FLYSWORD_ESSENCE.GetProps(pos_item);
                if (l == 23) ift.addons += WINGMANWING_ESSENCE.GetProps(pos_item);
                if (l == 27) ift.addons += ELEMENT_ESSENCE.GetProps(pos_item);
                if (l == 28) ift.addons += "\n" + Extensions.GetLocalization(7118);
                if (l == 31) ift.addons += PROJECTILE_ESSENCE.GetProps(pos_item);
                if (l == 83) ift.addons += FASHION_ESSENCE.GetProps(pos_item);
                if (l == 89) ift.addons += FACEPILL_ESSENCE.GetProps(pos_item);
                if (l == 94) ift.addons += PET_EGG_ESSENCE.GetProps(pos_item);
                if (l == 95) ift.addons += PET_EGG_ESSENCE.GetProps(pos_item);
                if (l == 96) ift.addons += PET_FOOD_ESSENCE.GetProps(pos_item);
                if (l == 98) ift.addons += FIREWORKS_ESSENCE.GetProps(pos_item);
                if (l == 106) ift.addons += SKILLMATTER_ESSENCE.GetProps(pos_item);
                if (l == 107) ift.addons += REFINE_TICKET_ESSENCE.GetProps(pos_item);
                if (l == 114) ift.addons += AUTOHP_ESSENCE.GetProps(pos_item);
                if (l == 115) ift.addons += AUTOMP_ESSENCE.GetProps(pos_item);
                if (l == 119) ift.addons += GOBLIN_ESSENCE.GetProps(pos_item);
                if (l == 121) ift.addons += GOBLIN_EQUIP_ESSENCE.GetProps(pos_item);
                if (l == 122) ift.addons += GOBLIN_EXPPILL_ESSENCE.GetProps(pos_item);
                if (l == 123) ift.addons += SELL_CERTIFICATE_ESSENCE.GetProps(pos_item);
                if (l == 124) ift.addons += TARGET_ITEM_ESSENCE.GetProps(pos_item);
                if (l == 130) ift.addons += INC_SKILL_ABILITY_ESSENCE.GetProps(pos_item);
                if (l == 133) ift.addons += WEDDING_BOOKCARD_ESSENCE.GetProps(pos_item);
                if (l == 135) ift.addons += SHARPENER_ESSENCE.GetProps(pos_item);
                if (l == 141) ift.addons += CONGREGATE_ESSENCE.GetProps(pos_item);
                if (l == 151) ift.addons += FORCE_TOKEN_ESSENCE.GetProps(pos_item);
                if (l == 184) ift.addons += POKER_ESSENCE.GetProps(pos_item);
                if (l == 191) ift.addons += UNIVERSAL_TOKEN_ESSENCE.GetProps(pos_item);
                if (l == 197) ift.addons += ASTROLABE_ESSENCE.GetProps(pos_item);
                if (l == 212) ift.addons += FIREWORKS2_ESSENCE.GetProps(pos_item);
                if (l == 218) ift.addons += HOME_FORMULAS_ITEM_ESSENCE.GetProps(pos_item);
                if (l != 3 && l != 6 && l != 9 && l != 12 && l != 17 && l != 19 && l != 22 && l != 23 &&
                    l != 27 && l != 31 && l != 83 && l != 89 && l != 95 && l != 96 && l != 98 && l != 106 &&
                    l != 107 && l != 114 && l != 115 && l != 119 && l != 121 && l != 122 && l != 123 &&
                    l != 124 && l != 130 && l != 133 && l != 135 && l != 141 && l != 151 && l != 184 &&
                    l != 191 && l != 197 && l != 212 && l != 218)
                {
                    for (int k = 0; k < MainWindow.eLC.Lists[l].elementFields.Length; k++)
                    {
                        if (MainWindow.eLC.Lists[l].elementFields[k] == "price")
                        {
                            string price = MainWindow.eLC.GetValue(l, pos_item, k);
                            if (price != "0")
                            {
                                ift.price += "\n" + Extensions.GetLocalization(7024) + " " + Convert.ToInt32(price).ToString("N0", CultureInfo.CreateSpecificCulture("zh-CN"));
                            }
                            break;
                        }
                    }
                }
                if (ift.addons.Length > 1)
                {
                    ift.addons = ift.addons.Remove(0, 1) + "\n\n";
                }
                
                for (int p = 0; p < powers.Count; p++)
                {
                    if (powers[p] == 0) continue;

                    switch (p)
                    {
                        case 0:
                            ift.protect += "^87CEFA" + Extensions.GetLocalization(3000) ;//proc_type_1
                            break;
                        case 1:
                            ift.protect += "^87CEFA" + Extensions.GetLocalization(3001);//proc_type_2
                            break;
                        case 2:
                            ift.protect += "^87CEFA" + Extensions.GetLocalization(3002);//proc_type_4
                            break;
                        case 3:
                            ift.protect += "^87CEFA" + Extensions.GetLocalization(3003);//proc_type_8
                            break;
                        case 4:
                            ift.protect += "^87CEFA" + Extensions.GetLocalization(3004);//proc_type_16
                            break;
                        case 5:
                            ift.protect += "^87CEFA" + Extensions.GetLocalization(3005);//proc_type_32
                            break;
                        case 7:
                            ift.protect += "^87CEFA" + Extensions.GetLocalization(3007);//proc_type_128
                            break;
                        case 8:
                            ift.protect += "^87CEFA" + Extensions.GetLocalization(3008);//proc_type_256
                            break;
                        case 9:
                            ift.protect += "^87CEFA" + Extensions.GetLocalization(3009);//proc_type_512
                            break;
                        case 10:
                            ift.protect += "^87CEFA" + Extensions.GetLocalization(3010);//proc_type_1024
                            break;
                        case 11:
                            ift.protect += "^87CEFA" + Extensions.GetLocalization(3011);//proc_type_2048
                            break;
                        case 12:
                            ift.protect += "^87CEFA" + Extensions.GetLocalization(3012);//proc_type_4096
                            break;
                        case 14:
                            ift.protect += "^87CEFA" + Extensions.GetLocalization(3014);//proc_type_16384
                            break;
                    }
                }

                ift.protect += "\n^FFFFFF";

                for (int k = 0; k < MainWindow.eLC.Lists[l].elementFields.Length; k++)
                {
                    if (MainWindow.eLC.Lists[l].elementFields[k] == "file_icon")
                    {
                        string image = MainWindow.eLC.GetValue(l, pos_item, k);
                        String path = Path.GetFileName(image);
                        if (MainWindow.database.ContainsKey(path))
                        {
                            ift.img = MainWindow.database.images(path);
                        }
                        else
                        {
                            ift.img = MainWindow.database.images("unknown.dds");
                        }
                        break;
                    }
                }

                if (!(MainWindow.database.Tasks is null))
                {
                    int _limited = 0;
                    string tilte = "\n\n^ECEC00" + Extensions.GetLocalization(8000) + "\n";
                    string cabs = tilte;
                    for (int t = 0; t < MainWindow.database.Tasks.Length; t++)
                    {
                        for (int m = 0; m < MainWindow.database.Tasks[t].m_Award_S.m_CandItems.Length; m++)
                        {
                            for (int i = 0; i < MainWindow.database.Tasks[t].m_Award_S.m_CandItems[m].m_AwardItems.Length; i++)
                            {
                                if (MainWindow.database.Tasks[t].m_Award_S.m_CandItems[m].m_AwardItems[i].m_ulItemTemplId == Id)
                                {
                                    _limited++;
                                    if (_limited < 5)
                                    {
                                        cabs += "^80FF00[ " + MainWindow.database.Tasks[t].ID + " - " + MainWindow.database.Tasks[t].Name + " ] " + "^80FF00" + MainWindow.database.Tasks[t].m_Award_S.m_CandItems[m].m_AwardItems[i].m_ulItemNum + " UN - " + Convert.ToDecimal(MainWindow.database.Tasks[t].m_Award_S.m_CandItems[m].m_AwardItems[i].m_fProb * 100) + "%  "/*+MainWindow.database.Tasks[t].m_Award_S.m_CandItems[m].m_AwardItems[i].m_fProb.ToString(Extensions.ProbValueFormat) + "*/+ "\n";
                                    }
                                }
                            }
                        }

                    }
                    if (_limited > 5)
                    {
                        cabs += "^80FF00" + (_limited - 5) + "+";
                    }

                    if (cabs.Length > tilte.Length)
                    {
                        ift.TaskItem = cabs;
                    }
                }
                                              
            }
            catch
            {

            }

            if (ift.addons.Length > 1 || ift.img != null)
            {
                return ift;
            }
            else
            {
                return null;
            }
        } /// PADRAO 



        public static string DecodingCharacterComboId(string character_combo_id)
        {
            string line = "";
            if ((character_combo_id != "0" && character_combo_id != "255" && MainWindow.eLC.Version < 52) || (character_combo_id != "0" && character_combo_id != "4095"))
            {
                line += "\n" + Extensions.GetLocalization(7017);
                uint charactercomboids;
                bool result_character_combo_id = uint.TryParse(character_combo_id, out charactercomboids);
                List<uint> powers_character_combo_id = new List<uint>(Extensions.GetPowers(charactercomboids));
                for (int p = 0; p < powers_character_combo_id.Count; p++)
                {
                    if (powers_character_combo_id[p] == 0) continue;

                    switch (p)
                    {
                        case 0:
                            line += " | " + Extensions.GetLocalization(1120);//character_combo_id_1
                            break;
                        case 1:
                            line += " | " + Extensions.GetLocalization(1121);//character_combo_id_2
                            break;
                        case 2:
                            line += " | " + Extensions.GetLocalization(1122);//character_combo_id_4
                            break;
                        case 3:
                            line += " | " + Extensions.GetLocalization(1123);//character_combo_id_8
                            break;
                        case 4:
                            line += " | " + Extensions.GetLocalization(1124);//character_combo_id_16
                            break;
                        case 5:
                            line += " | " + Extensions.GetLocalization(1125);//character_combo_id_32
                            break;
                        case 6:
                            line += " | " + Extensions.GetLocalization(1126);//character_combo_id_64
                            break;
                        case 7:
                            line += " | " + Extensions.GetLocalization(1127);//character_combo_id_128
                            break;
                        case 8:
                            line += " | " + Extensions.GetLocalization(1128);//character_combo_id_256
                            break;
                        case 9:
                            line += " | " + Extensions.GetLocalization(1129);//character_combo_id_512
                            break;
                        case 10:
                            line += " | " + Extensions.GetLocalization(1130);//character_combo_id_1024
                            break;
                        case 11:
                            line += " | " + Extensions.GetLocalization(1131);//character_combo_id_2048
                            break;
                    }
                }
            }
            else
            {
                line += "\n" + Extensions.GetLocalization(7017);
                line += " ALL";
            }
            return line;
        }

        public static string DecodingFoodMask(string food_mask)
        {
            string line = "";
            if (food_mask != "0")
            {
                line += "\n" + Extensions.GetLocalization(7050);
                uint foodmasks;
                bool result_food_mask = uint.TryParse(food_mask, out foodmasks);
                List<uint> powers_food_mask = new List<uint>(Extensions.GetPowers(foodmasks));
                for (int p = 0; p < powers_food_mask.Count; p++)
                {
                    if (powers_food_mask[p] == 0) continue;

                    switch (p)
                    {
                        case 0:
                            line += " " + Extensions.GetLocalization(3050);//food_mask_1
                            break;
                        case 1:
                            line += " " + Extensions.GetLocalization(3051);//food_mask_2
                            break;
                        case 2:
                            line += " " + Extensions.GetLocalization(3052);//food_mask_4
                            break;
                        case 3:
                            line += " " + Extensions.GetLocalization(3053);//food_mask_8
                            break;
                        case 4:
                            line += " " + Extensions.GetLocalization(3054);//food_mask_16
                            break;
                    }
                }
            }
            return line;
        }

        public static string ItemPropsSecondsToString(uint time)
        {
            string result = "";
            uint time1 = time;
            uint days = time / 86400;
            time = time - (days * 86400);
            uint hours = time / 3600;
            time = time - (hours * 3600);
            uint minutes = time / 60;
            uint seconds = time - (minutes * 60);
            if (time1 == 60) seconds = 60;
            if (time1 == 3600) minutes = 60;
            if (time1 == 86400) hours = 60;
            if (time1 <= 60) result = seconds.ToString() + Extensions.GetLocalization(7091);
            if (time1 > 60 && time1 <= 3600) result = minutes.ToString() + Extensions.GetLocalization(7092) + " " + seconds.ToString() + Extensions.GetLocalization(7091);
            if (time1 > 3600 && time1 <= 86400) result = hours.ToString() + Extensions.GetLocalization(7093) + " " + minutes.ToString() + Extensions.GetLocalization(7092);
            if (time1 > 86400) result = days.ToString() + Extensions.GetLocalization(7094) + " " + hours.ToString() + Extensions.GetLocalization(7093);
            return result;
        }

        public static string ItemPropsSecondsToString2(uint time)
        {
            string result = "";
            uint time1 = time;
            uint days = time / 86400;
            time = time - (days * 86400);
            uint hours = time / 3600;
            time = time - (hours * 3600);
            uint minutes = time / 60;
            uint seconds = time - (minutes * 60);
            if (time1 == 60) seconds = 60;
            if (time1 == 3600) minutes = 60;
            if (time1 == 86400) hours = 60;
            if (time1 <= 60) result = seconds.ToString() + Extensions.GetLocalization(7114);
            if (time1 > 60 && time1 <= 3600) result = minutes.ToString() + Extensions.GetLocalization(7115) + " " + seconds.ToString() + Extensions.GetLocalization(7114);
            if (time1 > 3600 && time1 <= 86400) result = hours.ToString() + Extensions.GetLocalization(7116) + " " + minutes.ToString() + Extensions.GetLocalization(7115);
            if (time1 > 86400) result = days.ToString() + Extensions.GetLocalization(7117) + " " + hours.ToString() + Extensions.GetLocalization(7116);
            return result;
        }

        public static IEnumerable<uint> GetPowers(uint value)
        {
            uint v = value;
            while (v > 0)
            {
                yield return (v & 0x01);
                v >>= 1;
            }
        }





        #region Cod Old

        //public static string GetMonsterNPCMineProps(int Id)
        //{
        //    string line = "";
        //    int l = 0;
        //    int pos = -1;
        //    bool Suc = false;
        //    try
        //    {
        //        for (int i = 0; i < TaskEditor.monster_npc_minelists.Count; i++)
        //        {
        //            l = Convert.ToInt32(TaskEditor.monster_npc_minelists[i]);
        //            for (int t = 0; t < TaskEditor.eLC.Lists[l].elementValues.Count; t++)
        //            {
        //                if (Convert.ToInt32(TaskEditor.eLC.GetValue(l, t, 0)) == Id)
        //                {
        //                    pos = t;
        //                    Suc = true;
        //                    break;
        //                }
        //            }
        //            if (Suc == true) break;
        //        }
        //        for (int k = 0; k < TaskEditor.eLC.Lists[l].elementFields.Count; k++)
        //        {
        //            if (TaskEditor.eLC.Lists[l].elementFields[k] == "Name")
        //            {
        //                if (pos != -1)
        //                { 
        //                    line += TaskEditor.eLC.GetValue(l, pos, k);
        //                    break;
        //                }
        //            }
        //        }
        //        if (l == 38) line += MONSTER_ESSENCE.GetProps(pos);
        //        if (l == 57) line += NPC_ESSENCE.GetProps(pos);
        //        if (l == 79) line += MINE_ESSENCE.GetProps(pos);
        //    }
        //    catch
        //    {
        //        line = "";
        //    }
        //    return line;
        //}

        //public static string GetTitleProps(int Id)
        //{
        //    string line = "";
        //    try
        //    {
        //        for (int k = 0; k < TaskEditor.eLC.Lists[169].elementValues.Count; k++)
        //        {
        //            if (Convert.ToInt32(TaskEditor.eLC.GetValue(169, k, 0)) == Id)
        //            {
        //                line += TITLE_CONFIG.GetProps(k);
        //                break;
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        line = "";
        //    }
        //    return line;
        //}

        //public static string GetHomeItemProps(int Id)
        //{
        //    string line = "";
        //    try
        //    {
        //        for (int k = 0; k < TaskEditor.eLC.Lists[223].elementValues.Count; k++)
        //        {
        //            if (Convert.ToInt32(TaskEditor.eLC.GetValue(223, k, 0)) == Id)
        //            {
        //                line += HOME_ITEM_ENTITY.GetProps(k);
        //                break;
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        line = "";
        //    }
        //    return line;
        //}

        //public static string RelayStationName(int id)
        //{
        //    string result;
        //    if (id != 0)
        //    {
        //        try
        //        {
        //            int index = -1;
        //            for (int num25 = 0; num25 < ElementsEditor.database.world_targets.Length - 1; num25 += 5)
        //            {
        //                if (Convert.ToInt32(ElementsEditor.database.world_targets[num25 + 0]) == id)
        //                {
        //                    index = num25 + 1;
        //                    break;
        //                }
        //            }
        //            result = ElementsEditor.database.world_targets[index];
        //        }
        //        catch
        //        {
        //            result = Extensions.GetLocalization(404);
        //        }
        //    }
        //    else
        //    {
        //        result = Extensions.GetLocalization(402);
        //    }

        //    return result.Replace("\\n", Environment.NewLine).Replace("\\t", "" + (char)9);
        //}

        public static InfoTool GetItemPropsGetsX(int Id)  /// OLD
        {


            InfoTool ift = new InfoTool();
            uint proctypes;
            int pos_proc_type = -1;
            ift.itemId = Id;
            bool Test = false;
            try
            {
                string value = "";
                int pos_item = 0;
                int La = 0;


                for (int L = 0; L < MainWindow.database.ItemUse.Count; L++)
                {


                    if (Test == false)
                    {
                        La = int.Parse(MainWindow.database.ItemUse.GetKey(L).ToString());

                        if (La == 198)
                        {

                        }
                        for (int i = 0; i < MainWindow.eLC.Lists[La].elementFields.Length; i++)
                        {
                            if (MainWindow.eLC.Lists[La].elementFields[i] == "Name")
                            {
                                pos_item = i;
                                break;
                            }
                        }
                        for (int ef = 0; ef < MainWindow.eLC.Lists[La].elementValues.Length; ef++)
                        {
                            value = MainWindow.eLC.GetValue(La, ef, pos_item);
                            var a = int.Parse(MainWindow.eLC.GetValue(La, ef, 0));
                            if (Id == a || value.Contains(Id.ToString()))
                            {
                                //ift = Extensions.GetItemPropsGets(Id, La, pos);
                                ift.name = MainWindow.eLC.GetValue(La, ef, pos_item);
                                Test = true;

                                for (int k = 0; k < MainWindow.eLC.Lists[La].elementFields.Length; k++)
                                {
                                    if (MainWindow.eLC.Lists[La].elementFields[k] == "file_icon")
                                    {
                                        string image = MainWindow.eLC.GetValue(La, ef, k);
                                        String path = Path.GetFileName(image);
                                        if (MainWindow.database.ContainsKey(path))
                                        {
                                            ift.img = MainWindow.database.images(path);
                                        }
                                        else
                                        {
                                            ift.img = MainWindow.database.images("unknown.dds");
                                        }
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }



                }
                for (int k = 0; k < MainWindow.eLC.Lists[La].elementFields.Length; k++)
                {
                    if (MainWindow.eLC.Lists[La].elementFields[k] == "proc_type")
                    {
                        pos_proc_type = k;
                        break;
                    }
                }
                bool result = uint.TryParse(MainWindow.eLC.GetValue(La, pos_item, pos_proc_type), out proctypes);
                List<uint> powers = new List<uint>(Extensions.GetPowers(proctypes));
                for (int p = 0; p < powers.Count; p++)
                {
                    if (powers[p] == 0) continue;

                    switch (p)
                    {
                        case 6:
                            ift.powers += "\n" + Extensions.GetLocalization(3006);//proc_type_64
                            break;
                        case 15:
                            ift.powers += "\n" + Extensions.GetLocalization(3015);//proc_type_32768
                            break;
                    }
                }

                for (int p = 0; p < powers.Count; p++)
                {
                    if (powers[p] == 0) continue;

                    switch (p)
                    {
                        case 13:
                            ift.powers += Extensions.GetLocalization(3013);//proc_type_8192
                            break;
                    }
                }

                if (La == 3) ift.addons += WEAPON_ESSENCE.GetProps(pos_item);
                if (La == 6) ift.addons += ARMOR_ESSENCE.GetProps(pos_item);
                if (La == 9) ift.addons += DECORATION_ESSENCE.GetProps(pos_item);
                if (La == 12) ift.addons += MEDICINE_ESSENCE.GetProps(pos_item);
                if (La == 17) ift.addons += DAMAGERUNE_ESSENCE.GetProps(pos_item);
                if (La == 19) ift.addons += ARMORRUNE_ESSENCE.GetProps(pos_item);
                if (La == 22) ift.addons += FLYSWORD_ESSENCE.GetProps(pos_item);
                if (La == 23) ift.addons += WINGMANWING_ESSENCE.GetProps(pos_item);
                if (La == 27) ift.addons += ELEMENT_ESSENCE.GetProps(pos_item);
                if (La == 28) ift.addons += "\n" + Extensions.GetLocalization(7118);
                if (La == 31) ift.addons += PROJECTILE_ESSENCE.GetProps(pos_item);
                if (La == 83) ift.addons += FASHION_ESSENCE.GetProps(pos_item);
                if (La == 89) ift.addons += FACEPILL_ESSENCE.GetProps(pos_item);
                if (La == 94) ift.addons += PET_EGG_ESSENCE.GetProps(pos_item);
                if (La == 95) ift.addons += PET_EGG_ESSENCE.GetProps(pos_item);
                if (La == 96) ift.addons += PET_FOOD_ESSENCE.GetProps(pos_item);
                if (La == 98) ift.addons += FIREWORKS_ESSENCE.GetProps(pos_item);
                if (La == 106) ift.addons += SKILLMATTER_ESSENCE.GetProps(pos_item);
                if (La == 107) ift.addons += REFINE_TICKET_ESSENCE.GetProps(pos_item);
                if (La == 114) ift.addons += AUTOHP_ESSENCE.GetProps(pos_item);
                if (La == 115) ift.addons += AUTOMP_ESSENCE.GetProps(pos_item);
                if (La == 119) ift.addons += GOBLIN_ESSENCE.GetProps(pos_item);
                if (La == 121) ift.addons += GOBLIN_EQUIP_ESSENCE.GetProps(pos_item);
                if (La == 122) ift.addons += GOBLIN_EXPPILL_ESSENCE.GetProps(pos_item);
                if (La == 123) ift.addons += SELL_CERTIFICATE_ESSENCE.GetProps(pos_item);
                if (La == 124) ift.addons += TARGET_ITEM_ESSENCE.GetProps(pos_item);
                if (La == 130) ift.addons += INC_SKILL_ABILITY_ESSENCE.GetProps(pos_item);
                if (La == 133) ift.addons += WEDDING_BOOKCARD_ESSENCE.GetProps(pos_item);
                if (La == 135) ift.addons += SHARPENER_ESSENCE.GetProps(pos_item);
                if (La == 141) ift.addons += CONGREGATE_ESSENCE.GetProps(pos_item);
                if (La == 151) ift.addons += FORCE_TOKEN_ESSENCE.GetProps(pos_item);
                if (La == 184) ift.addons += POKER_ESSENCE.GetProps(pos_item);
                if (La == 191) ift.addons += UNIVERSAL_TOKEN_ESSENCE.GetProps(pos_item);
                if (La == 197) ift.addons += ASTROLABE_ESSENCE.GetProps(pos_item);
                if (La == 212) ift.addons += FIREWORKS2_ESSENCE.GetProps(pos_item);
                if (La == 218) ift.addons += HOME_FORMULAS_ITEM_ESSENCE.GetProps(pos_item);
                if (La != 3 && La != 6 && La != 9 && La != 12 && La != 17 && La != 19 && La != 22 && La != 23 &&
                    La != 27 && La != 31 && La != 83 && La != 89 && La != 95 && La != 96 && La != 98 && La != 106 &&
                    La != 107 && La != 114 && La != 115 && La != 119 && La != 121 && La != 122 && La != 123 &&
                    La != 124 && La != 130 && La != 133 && La != 135 && La != 141 && La != 151 && La != 184 &&
                    La != 191 && La != 197 && La != 212 && La != 218)
                {
                    for (int k = 0; k < MainWindow.eLC.Lists[La].elementFields.Length; k++)
                    {
                        if (MainWindow.eLC.Lists[La].elementFields[k] == "price")
                        {
                            string price = MainWindow.eLC.GetValue(La, pos_item, k);
                            if (price != "0")
                            {
                                ift.price += "\n" + Extensions.GetLocalization(7024) + " " + Convert.ToInt32(price).ToString("N0", CultureInfo.CreateSpecificCulture("zh-CN"));
                            }
                            break;
                        }
                    }
                }
                if (ift.addons.Length > 1)
                {
                    ift.addons = ift.addons.Remove(0, 1) + "\n\n";
                }
                for (int p = 0; p < powers.Count; p++)
                {
                    if (powers[p] == 0) continue;

                    switch (p)
                    {
                        case 0:
                            ift.protect += Extensions.GetLocalization(3000);//proc_type_1
                            break;
                        case 1:
                            ift.protect += Extensions.GetLocalization(3001);//proc_type_2
                            break;
                        case 2:
                            ift.protect += Extensions.GetLocalization(3002);//proc_type_4
                            break;
                        case 3:
                            ift.protect += Extensions.GetLocalization(3003);//proc_type_8
                            break;
                        case 4:
                            ift.protect += Extensions.GetLocalization(3004);//proc_type_16
                            break;
                        case 5:
                            ift.protect += Extensions.GetLocalization(3005);//proc_type_32
                            break;
                        case 7:
                            ift.protect += Extensions.GetLocalization(3007);//proc_type_128
                            break;
                        case 8:
                            ift.protect += Extensions.GetLocalization(3008);//proc_type_256
                            break;
                        case 9:
                            ift.protect += Extensions.GetLocalization(3009);//proc_type_512
                            break;
                        case 10:
                            ift.protect += Extensions.GetLocalization(3010);//proc_type_1024
                            break;
                        case 11:
                            ift.protect += Extensions.GetLocalization(3011);//proc_type_2048
                            break;
                        case 12:
                            ift.protect += Extensions.GetLocalization(3012);//proc_type_4096
                            break;
                        case 14:
                            ift.protect += Extensions.GetLocalization(3014);//proc_type_16384
                            break;
                    }
                }


                //if (MainWindow.database.item_desc.ContainsKey(ift.itemId))
                //{
                //    ift.description = MainWindow.database.item_desc[ift.itemId];
                //}

            }
            catch
            {

            }
            if (ift.addons.Length > 1 || ift.img != null)
            {
                return ift;
            }
            else
            {
                return null;
            }
        } // Old

        public static InfoTool GetItemProps2byIdDDD(int Id, uint Period)
        {
            InfoTool ift = new InfoTool();
            uint proctypes;
            int pos_proc_type = -1;
            ift.itemId = Id;
            int l = 0;
            int pos_item = 0;
            bool Suc = false;


            if (MainWindow.database.task_items.ContainsKey(Id))
            {
                pos_item = MainWindow.database.task_items[Id].index;
                l = MainWindow.database.task_items[Id].listID;
                ift.name = MainWindow.database.task_items[Id].name;
                ift.price = ift.price += "\n" + Extensions.GetLocalization(7024) + " " + MainWindow.database.task_items[Id].price;
                ift.test = MainWindow.database.task_items[Id];
                if (MainWindow.database.ContainsKey(MainWindow.database.task_items[Id].iconpath))
                {
                    ift.img = MainWindow.database.images(MainWindow.database.task_items[Id].iconpath);
                }
                else
                {
                    ift.img = MainWindow.database.images("unknown.dds");
                }
            }
            else
            {
                for (int i = 0; i < MainWindow.database.task_items_list.Length; i += 2)
                {
                    if (MainWindow.eLC.Version >= Convert.ToInt32(MainWindow.database.task_items_list[i + 1]))
                    {
                        l = Convert.ToInt32(MainWindow.database.task_items_list[i]);
                        for (int t = 0; t < MainWindow.eLC.Lists[l].elementValues.Length; t++)
                        {
                            if (Convert.ToInt32(MainWindow.eLC.GetValue(l, t, 0)) == Id)
                            {
                                pos_item = t;
                                Suc = true;
                                break;
                            }
                        }
                        if (Suc == true) break;
                    }
                }

                for (int k = 0; k < MainWindow.eLC.Lists[l].elementFields.Length; k++)
                {
                    if (MainWindow.eLC.Lists[l].elementFields[k] == "Name")
                    {
                        ift.name = MainWindow.eLC.GetValue(l, pos_item, k);
                        break;
                    }
                }

                for (int k = 0; k < MainWindow.eLC.Lists[l].elementFields.Length; k++)
                {
                    if (MainWindow.eLC.Lists[l].elementFields[k] == "file_icon")
                    {
                        ift.file_icon = MainWindow.eLC.GetValue(l, pos_item, k);
                        String path = Path.GetFileName(ift.file_icon);
                        Bitmap img = null;
                        if (MainWindow.database.ContainsKey(path))
                        {
                            img = MainWindow.database.images(path);
                        }
                        if (img != null)
                        {
                            ift.img = img;
                        }
                        break;
                    }
                }

                if (Period != 0)
                {
                    ift.time = "\n" + Extensions.GetLocalization(7113) + Extensions.ItemPropsSecondsToString2(Period);
                }
                for (int k = 0; k < MainWindow.eLC.Lists[l].elementFields.Length; k++)
                {
                    if (MainWindow.eLC.Lists[l].elementFields[k] == "proc_type")
                    {
                        pos_proc_type = k;
                        break;
                    }
                }
                for (int k = 0; k < MainWindow.eLC.Lists[l].elementFields.Length; k++)
                {
                    if (MainWindow.eLC.Lists[l].elementFields[k] == "file_icon")
                    {
                        string image = MainWindow.eLC.GetValue(l, pos_item, k);
                        String path = Path.GetFileName(image);
                        if (MainWindow.database.ContainsKey(path))
                        {
                            ift.img = MainWindow.database.images(path);
                        }
                        else
                        {
                            ift.img = MainWindow.database.images("unknown.dds");
                        }
                        break;
                    }
                }

                if (l != 3 && l != 6 && l != 9 && l != 12 && l != 17 && l != 19 && l != 22 && l != 23 &&
                    l != 27 && l != 31 && l != 83 && l != 89 && l != 95 && l != 96 && l != 98 && l != 106 &&
                    l != 107 && l != 114 && l != 115 && l != 119 && l != 121 && l != 122 && l != 123 &&
                    l != 124 && l != 130 && l != 133 && l != 135 && l != 141 && l != 151 && l != 184 &&
                    l != 191 && l != 197 && l != 212 && l != 218)
                {
                    for (int k = 0; k < MainWindow.eLC.Lists[l].elementFields.Length; k++)
                    {
                        if (MainWindow.eLC.Lists[l].elementFields[k] == "price")
                        {
                            string price = MainWindow.eLC.GetValue(l, pos_item, k);
                            if (price != "0")
                            {
                                ift.price += "\n" + Extensions.GetLocalization(7024) + " " + Convert.ToInt32(price).ToString("N0", CultureInfo.CreateSpecificCulture("zh-CN"));
                            }
                            break;
                        }
                    }
                }

            }
            bool result = uint.TryParse(MainWindow.eLC.GetValue(l, pos_item, pos_proc_type), out proctypes);
            List<uint> powers = new List<uint>(Extensions.GetPowers(proctypes));
            for (int p = 0; p < powers.Count; p++)
            {
                if (powers[p] == 0) continue;

                switch (p)
                {
                    case 6:
                        ift.powers += "\n" + Extensions.GetLocalization(3006);//proc_type_64
                        break;
                    case 15:
                        ift.powers += "\n" + Extensions.GetLocalization(3015);//proc_type_32768
                        break;
                }
            }

            for (int p = 0; p < powers.Count; p++)
            {
                if (powers[p] == 0) continue;

                switch (p)
                {
                    case 13:
                        ift.powers += Extensions.GetLocalization(3013);//proc_type_8192
                        break;
                }
            }
            if (l == 3) ift.addons += WEAPON_ESSENCE.GetProps(pos_item);
            if (l == 6) ift.addons += ARMOR_ESSENCE.GetProps(pos_item);
            if (l == 9) ift.addons += DECORATION_ESSENCE.GetProps(pos_item);
            if (l == 12) ift.addons += MEDICINE_ESSENCE.GetProps(pos_item);
            if (l == 17) ift.addons += DAMAGERUNE_ESSENCE.GetProps(pos_item);
            if (l == 19) ift.addons += ARMORRUNE_ESSENCE.GetProps(pos_item);
            if (l == 22) ift.addons += FLYSWORD_ESSENCE.GetProps(pos_item);
            if (l == 23) ift.addons += WINGMANWING_ESSENCE.GetProps(pos_item);
            if (l == 27) ift.addons += ELEMENT_ESSENCE.GetProps(pos_item);
            if (l == 28) ift.addons += "\n" + Extensions.GetLocalization(7118);
            if (l == 31) ift.addons += PROJECTILE_ESSENCE.GetProps(pos_item);
            if (l == 83) ift.addons += FASHION_ESSENCE.GetProps(pos_item);
            if (l == 89) ift.addons += FACEPILL_ESSENCE.GetProps(pos_item);
            if (l == 95) ift.addons += PET_EGG_ESSENCE.GetProps(pos_item);
            if (l == 96) ift.addons += PET_FOOD_ESSENCE.GetProps(pos_item);
            if (l == 98) ift.addons += FIREWORKS_ESSENCE.GetProps(pos_item);
            if (l == 106) ift.addons += SKILLMATTER_ESSENCE.GetProps(pos_item);
            if (l == 107) ift.addons += REFINE_TICKET_ESSENCE.GetProps(pos_item);
            if (l == 114) ift.addons += AUTOHP_ESSENCE.GetProps(pos_item);
            if (l == 115) ift.addons += AUTOMP_ESSENCE.GetProps(pos_item);
            if (l == 119) ift.addons += GOBLIN_ESSENCE.GetProps(pos_item);
            if (l == 121) ift.addons += GOBLIN_EQUIP_ESSENCE.GetProps(pos_item);
            if (l == 122) ift.addons += GOBLIN_EXPPILL_ESSENCE.GetProps(pos_item);
            if (l == 123) ift.addons += SELL_CERTIFICATE_ESSENCE.GetProps(pos_item);
            if (l == 124) ift.addons += TARGET_ITEM_ESSENCE.GetProps(pos_item);
            if (l == 130) ift.addons += INC_SKILL_ABILITY_ESSENCE.GetProps(pos_item);
            if (l == 133) ift.addons += WEDDING_BOOKCARD_ESSENCE.GetProps(pos_item);
            if (l == 135) ift.addons += SHARPENER_ESSENCE.GetProps(pos_item);
            if (l == 141) ift.addons += CONGREGATE_ESSENCE.GetProps(pos_item);
            if (l == 151) ift.addons += FORCE_TOKEN_ESSENCE.GetProps(pos_item);
            if (l == 184) ift.addons += POKER_ESSENCE.GetProps(pos_item);
            if (l == 191) ift.addons += UNIVERSAL_TOKEN_ESSENCE.GetProps(pos_item);
            if (l == 197) ift.addons += ASTROLABE_ESSENCE.GetProps(pos_item);
            if (l == 212) ift.addons += FIREWORKS2_ESSENCE.GetProps(pos_item);
            if (l == 218) ift.addons += HOME_FORMULAS_ITEM_ESSENCE.GetProps(pos_item);

            if (ift.addons.Length > 1)
            {
                ift.addons = ift.addons.Remove(0, 1) + "\n\n";
            }
            for (int p = 0; p < powers.Count; p++)
            {
                if (powers[p] == 0) continue;

                switch (p)
                {
                    case 0:
                        ift.protect += Extensions.GetLocalization(3000);//proc_type_1
                        break;
                    case 1:
                        ift.protect += Extensions.GetLocalization(3001);//proc_type_2
                        break;
                    case 2:
                        ift.protect += Extensions.GetLocalization(3002);//proc_type_4
                        break;
                    case 3:
                        ift.protect += Extensions.GetLocalization(3003);//proc_type_8
                        break;
                    case 4:
                        ift.protect += Extensions.GetLocalization(3004);//proc_type_16
                        break;
                    case 5:
                        ift.protect += Extensions.GetLocalization(3005);//proc_type_32
                        break;
                    case 7:
                        ift.protect += Extensions.GetLocalization(3007);//proc_type_128
                        break;
                    case 8:
                        ift.protect += Extensions.GetLocalization(3008);//proc_type_256
                        break;
                    case 9:
                        ift.protect += Extensions.GetLocalization(3009);//proc_type_512
                        break;
                    case 10:
                        ift.protect += Extensions.GetLocalization(3010);//proc_type_1024
                        break;
                    case 11:
                        ift.protect += Extensions.GetLocalization(3011);//proc_type_2048
                        break;
                    case 12:
                        ift.protect += Extensions.GetLocalization(3012);//proc_type_4096
                        break;
                    case 14:
                        ift.protect += Extensions.GetLocalization(3014);//proc_type_16384
                        break;
                }
            }

            //if (MainWindow.database.item_desc.ContainsKey(ift.itemId))
            //{
            //    ift.description = MainWindow.database.item_desc[ift.itemId];
            //}
            return ift;
        } // OLD

        //public static string ItemName(int id)
        //{
        //    string result;
        //    if (id != 0 && TaskEditor.LoadedElements == true)
        //    {
        //        try
        //        {
        //            if(ElementsEditor.database.task_items.ContainsKey(id))
        //            {
        //                return ElementsEditor.database.task_items[id].name;
        //            }else
        //            {
        //                return "Unknown("+ id + ")";
        //            }
        //        }
        //        catch
        //        {
        //            result = Extensions.GetLocalization(404);
        //        }
        //    }
        //    else
        //    {
        //        result = Extensions.GetLocalization(402);
        //    }

        //    return result/*.Replace("\\n", Environment.NewLine).Replace("\\t", "" + (char)9)*/;
        //}

        //public static string MonsterNPCMineName(int id)
        //{
        //    string result;
        //    if (id != 0 && TaskEditor.LoadedElements == true)
        //    {
        //        try
        //        {
        //            result = ElementsEditor.database.monsters_npcs_mines[id.ToString()].ToString();
        //        }
        //        catch
        //        {
        //            result = Extensions.GetLocalization(404);
        //        }
        //    }
        //    else
        //    {
        //        result = Extensions.GetLocalization(402);
        //    }

        //    return result/*.Replace("\\n", Environment.NewLine).Replace("\\t", "" + (char)9)*/;
        //}

        //public static string TitleName(int id)
        //{
        //    string result;
        //    if (id != 0 && TaskEditor.LoadedElements == true)
        //    {
        //        try
        //        {
        //            result = ElementsEditor.database.titles[id.ToString()].ToString();
        //        }
        //        catch
        //        {
        //            result = Extensions.GetLocalization(404);
        //        }
        //    }
        //    else
        //    {
        //        result = Extensions.GetLocalization(402);
        //    }

        //    return result/*.Replace("\\n", Environment.NewLine).Replace("\\t", "" + (char)9)*/;
        //}

        //public static string HomeItemName(int id)
        //{
        //    string result;
        //    if (id != 0 && TaskEditor.LoadedElements == true)
        //    {
        //        try
        //        {
        //            result = ElementsEditor.database.homeitems[id.ToString()].ToString();
        //        }
        //        catch
        //        {
        //            result = Extensions.GetLocalization(404);
        //        }
        //    }
        //    else
        //    {
        //        result = Extensions.GetLocalization(402);
        //    }

        //    return result/*.Replace("\\n", Environment.NewLine).Replace("\\t", "" + (char)9)*/;
        //}

        //public static string InstanceName(int id)
        //{
        //    string result;
        //    try
        //    {
        //        result = MainWindow.InstanceList[id.ToString()].ToString();
        //    }
        //    catch
        //    {
        //        result = Extensions.GetLocalization(404);
        //    }

        //    return result.Replace("\\n", Environment.NewLine).Replace("\\t", "" + (char)9);
        //}

        //public static string BuffDesc(int id)
        //{
        //    string result;
        //    if (id != 0)
        //    {
        //        try
        //        {
        //            int index = -1;
        //            for (int num25 = 0; num25 < ElementsEditor.database.buff_str.Length - 1; num25 += 2)
        //            {
        //                if (Convert.ToInt32(ElementsEditor.database.buff_str[num25 + 0]) == id)
        //                {
        //                    index = num25 + 1;
        //                    break;
        //                }
        //            }
        //            if (ElementsEditor.database.buff_str.Length > index && index != -1)
        //            {
        //                result = ElementsEditor.database.buff_str[index];
        //            }else
        //            {
        //                result = Extensions.GetLocalization(404);
        //            }
        //        }
        //        catch
        //        {
        //            result = Extensions.GetLocalization(404);
        //        }
        //    }
        //    else
        //    {
        //        result = Extensions.GetLocalization(402);
        //    }

        //    return result.Replace("\\n", Environment.NewLine).Replace("\\t", "" + (char)9);
        //}

        #endregion
    }
}
