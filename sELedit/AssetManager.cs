using sELedit.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Data;
using System.Drawing.Imaging;

namespace sELedit
{
    public class AssetManager
    {
        public int DDSFORMAT = 6;
        internal delegate void UpdateProgressDelegate(string value, int min, int max);
        private SortedList<int, int> item_color;
        private int rows;
        private Bitmap sourceBitmap;
        private CacheSave database = new CacheSave();
        private bool firstLoad = true;
        private int cols;
        private SortedList<int, string> imagesx;
        private SortedList<string, Point> imageposition;
        private SortedList<int, string> item_desc;
        public SortedList item_ext_desc;

        public static SortedList _wepon;
		public static SortedList _armor;
		public static SortedList _decoration;
		public static SortedList _suite;

		private List<string> arrTheme;
        PCKs pck;
        LoasdsProgreces LP = new LoasdsProgreces();
		public static object anydata;

		
		public SortedList<int, Image> ImageTask;

		public bool load(ref ColorProgressBar.ColorProgressBar cpb2)
        {
            cpb2.Maximum = 10;
            cpb2.Minimum = 0;
            cpb2.Value = 0;

            firstLoad = true;
            
            if (sourceBitmap == null)
            {
               
                imageposition = LoadSurfaces();
                cpb2.Value++;
                loadItem_color();
                cpb2.Value++;
            }

            if (firstLoad)
            {
				imagsTask();
                cpb2.Value++;
                //LoadTheme();
                Application.DoEvents();
                cpb2.Value++;
                LoadLocalizationText();
                Application.DoEvents();
                //this.LoadInstanceList();
                //Application.DoEvents();
                cpb2.Value++;
                LoadBuffList();
                Application.DoEvents();
                cpb2.Value++;
                LoadItemExtDescList();
                Application.DoEvents();
                cpb2.Value++;
                LoadSkillList();
                Application.DoEvents();
                cpb2.Value++;
                LoadAddonList();
                cpb2.Value++;
                Application.DoEvents();
                firstLoad = false;
				addons_wac();
                cpb2.Value++;


            }
            
            MainWindow.database = database;
            
            return true;
        }

        public void imagsTask()
		{

			try
			{
				ImageList imageList1 = new ImageList();
				string[] arquivos = Directory.GetFiles(Application.StartupPath + @"\images", "*.png", SearchOption.TopDirectoryOnly);
				for (int fd = 0; fd < arquivos.Length; fd++)
				{
					imageList1.Images.Add(Image.FromFile(arquivos[fd]));

				}
				MainWindow.database.ImageTask = imageList1;
			}
			catch (Exception)
			{

			
			}
			
			

		}

		public void addons_wac()
		{
			string wp = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\opt\add_wepom.txt";
			string am = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\opt\add_armor.txt";
			string dec = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\opt\add_decoration.txt";
			string st = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\opt\add_suite.txt";
			string[] lines_w = File.ReadAllLines(wp);
			string[] lines_a = File.ReadAllLines(am);
			string[] lines_d = File.ReadAllLines(dec);
			string[] lines_e = File.ReadAllLines(st);
			_wepon = new SortedList(); _armor = new SortedList(); _decoration = new SortedList(); _suite = new SortedList();
			_wepon.Clear(); _armor.Clear(); _decoration.Clear(); _suite.Clear();
            
            for (int i = 0; i < lines_w.Length; i++)
			{
				if (lines_w[i] != "")
				{
					try
					{
						_wepon.Add(int.Parse(lines_w[i].Replace(" ", "")), EQUIPMENT_ADDON.GetAddon(lines_w[i].Replace(" ", "").ToString()));
					}
					catch (Exception)
					{

						
					}
					
				}
			}
			database._wepon = _wepon;
			for (int i = 0; i < lines_a.Length; i++)
			{
				if (lines_a[i] != "")
				{
					try
					{
						_armor.Add(int.Parse(lines_a[i].Replace(" ", "")), EQUIPMENT_ADDON.GetAddon(lines_a[i].Replace(" ", "").ToString()));
					}
					catch (Exception)
					{

						
					}
					
				}
			}
			database._armor = _armor;
			for (int i = 0; i < lines_d.Length; i++)
			{
				if (lines_d[i] != "")
				{
					try
					{
						_decoration.Add(int.Parse(lines_d[i].Replace(" ", "")), EQUIPMENT_ADDON.GetAddon(lines_d[i].Replace(" ", "").ToString()));
					}
					catch (Exception)
					{

						
					}
					
				}
			}

			database._decoration = _decoration;
			for (int i = 0; i < lines_e.Length; i++)
			{
				if (lines_e[i] != "")
				{
					try
					{
						_suite.Add(int.Parse(lines_e[i].Replace(" ", "").Split(new string[] { "\"" }, StringSplitOptions.None)[0]), lines_e[i]./*Replace(" ", "").*/Split(new string[] { "\"" }, StringSplitOptions.None)[1]);
					}
					catch (Exception)
					{


					}

				}
			}
			database._suite = _suite;

		}
        public void LoadTheme()
        {
            try
            {
                string line;
                arrTheme = new List<string>();
                string theme_list = Path.GetDirectoryName(Application.ExecutablePath) + "\\resources\\theme.txt";
                Encoding enc = Encoding.GetEncoding("GBK");
                int lines = File.ReadAllLines(theme_list).Length;
                StreamReader file = new StreamReader(theme_list, enc);
                Application.DoEvents();
                int count = 0;

                while ((line = file.ReadLine()) != null)
                {
                    if (line != null && line.Length > 0 && !line.StartsWith("#") && !line.StartsWith("/"))
                    {
                        arrTheme.Add(line);
                    }
                    count++;
                }
                file.Close();
                database.arrTheme = arrTheme;
            }
            catch
            {
                database.arrTheme = null;
            }
        }

        static public Bitmap getSkillIcon(int skillid)
        {
            Bitmap img = Properties.Resources.ResourceManager.GetObject("_" + skillid) as Bitmap;
            return img != null ? img : new Bitmap(new Bitmap(Resources.blank));
        }
       
        



		public void loadItem_color()
		{
            try
            {
                if (File.Exists(MainWindow.XmlData.ConfigsPckPath))
                {
                    item_color = new SortedList<int,int>();
                    pck = new PCKs(MainWindow.XmlData.ConfigsPckPath);
                    IEnumerable<PCKFileEntry> source = pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.StartsWith("configs\\item_color.txt")));
                    byte[] array = ((IEnumerable<byte>)pck.ReadFile(pck.PckFile, source.ElementAt<PCKFileEntry>(0))).ToArray<byte>();

                    string tempFileName = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\configs\item_color.txt";
                    File.WriteAllBytes(tempFileName, array);
                    var item_color_Read = File.ReadAllLines(tempFileName, Encoding.GetEncoding("GBK"));


                    for (int i = 0; i < item_color_Read.Length; i++)
                    {
                        if (item_color_Read[i] != null)
                        {
                            string[] data = item_color_Read[i].Split(null);
                            string v1 = data[0].ToString();
                            string v2 = data[1].ToString();
                            if (v1.Length > 0 && v2.Length > 0)
                            {
                                item_color.Add(int.Parse(v1), int.Parse(v2));
                            }
                            else
                            {
                                if (v1.Length > 0)
                                {
                                    item_color.Add(int.Parse(v1), 0);
                                }
                                if (v2.Length > 0)
                                {
                                    item_color.Add(0, int.Parse(v2));
                                }
                            }
                        }
                        else
                        {

                        }
                    }
					database.item_color = item_color;

					loaditem_desc();

				}
                else
                {
                    string line;
                    item_color = new SortedList<int, int>();
                    string iconlist_ivtrm = Path.GetDirectoryName(Application.ExecutablePath) + @"\resources\configs\item_color.txt";

                    string extension = Path.GetExtension(iconlist_ivtrm);
                    if (extension == ".txt")
                    {
                        Encoding enc = Encoding.GetEncoding("GBK");
                        int lines = File.ReadAllLines(iconlist_ivtrm).Length;
                        StreamReader file = new StreamReader(iconlist_ivtrm, enc);
                        int count = 0;
                        while ((line = file.ReadLine()) != null)
                        {
                            Application.DoEvents();
                            string[] data = line.Split(null);
                            try
                            {
                                string v1 = data[0].ToString();
                                string v2 = data[1].ToString();
                                if (v1.Length > 0 && v2.Length > 0)
                                {
                                    item_color.Add(int.Parse(v1), int.Parse(v2));
                                }
                                else
                                {
                                    if (v1.Length > 0)
                                    {
                                        item_color.Add(int.Parse(v1), 0);
                                    }
                                    if (v2.Length > 0)
                                    {
                                        item_color.Add(0, int.Parse(v2));
                                    }
                                }
                            }
                            catch (Exception) { }
                            count++;
                        }
                        file.Close();
                    }

					database.item_color = item_color;

					loaditem_desc();


				}
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public void loaditem_desc()
        {
            try
            {
                
                if (File.Exists(MainWindow.XmlData.ConfigsPckPath))
                {
                    pck = new PCKs(MainWindow.XmlData.ConfigsPckPath);
                    item_desc = new SortedList<int, string>();
                    IEnumerable<PCKFileEntry> source = pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.StartsWith("configs\\item_desc.txt")));
                    byte[] array = ((IEnumerable<byte>)pck.ReadFile(pck.PckFile, source.ElementAt<PCKFileEntry>(0))).ToArray<byte>();
                   // var sd = pck.
                    string tempFileName = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\configs\item_desc.txt";
                    File.WriteAllBytes(tempFileName, array);
                    var item_desc_Read = File.ReadAllLines(tempFileName, Encoding.GetEncoding("GBK"));

                    for (int i = 0; i < item_desc_Read.Length; i++)
                    {
                        
                            if (item_desc_Read[i] != null && item_desc_Read[i].Length > 0 && !item_desc_Read[i].StartsWith("#") && !item_desc_Read[i].StartsWith("/"))
                            {
                                string[] data = item_desc_Read[i].Split('"');
                                data = data.Where(a => a != "").ToArray();
                                try
                                {
                                    Application.DoEvents();
                                    item_desc.Add(i, data[0])/*, data[1].ToString().Replace('"', ' ')*/;
                                }
                            catch (Exception e) { MessageBox.Show(e.Message); }
                            }
                         
                    }
                }
                else
                {
                    string line;
                    item_desc = new SortedList<int, string>();
                    string iconlist_ivtrm = Path.GetDirectoryName(Application.ExecutablePath) + @"\resources\configs\item_desc.txt";
                    Encoding enc = Encoding.GetEncoding("GBK");
                    int lines = File.ReadAllLines(iconlist_ivtrm).Length;
                    StreamReader file = new StreamReader(iconlist_ivtrm, enc);
                    Application.DoEvents();
                    int count = 0;

                    while ((line = file.ReadLine()) != null)
                    {
                        if (line != null && line.Length > 0 && !line.StartsWith("#") && !line.StartsWith("/"))
                        {
                            string[] data = line.Split('"');
                            data = data.Where(a => a != "").ToArray();
                            try
                            {
                                Application.DoEvents();
                                item_desc.Add(count, data[0])/*, data[1].ToString().Replace('"', ' ')*/;
                            }
                            catch (Exception) { }
                        }
                        count++;
                    }
                    file.Close();
                    database.item_desc = item_desc;
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public void LoadItemExtDescList()
        {
            try
            {
                if (database.item_ext_desc != null)
                {
                    
                    //MainWindow.item_ext_desc_TXT = database.item_ext_desc;
                    return;
                }

                if (File.Exists(MainWindow.XmlData.ConfigsPckPath))
                {
                    pck = new PCKs(MainWindow.XmlData.ConfigsPckPath);
                    try
                    {
                        IEnumerable<PCKFileEntry> source = pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.StartsWith("configs\\item_ext_desc.txt")));
                        byte[] array = ((IEnumerable<byte>)pck.ReadFile(pck.PckFile, source.ElementAt<PCKFileEntry>(0))).ToArray<byte>();

                        string tempFileName = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\configs\item_ext_desc.txt";
                        File.WriteAllBytes(tempFileName, array);
                        var item_ext_desc_Read = File.ReadAllLines(tempFileName, Encoding.GetEncoding("GBK"));

                        string result = string.Join("\n", item_ext_desc_Read);
                        if (result != string.Empty)
                        {
                            try
                            {
                                item_ext_desc = new SortedList();
                                var arr_result = result.Split('\n');
                                int start_Arr = 0;

                                for (int i = 0; i < arr_result.Length; i++)
                                {
                                    if (!arr_result[i].StartsWith("/") && !arr_result[i].StartsWith("#") && arr_result[i] != string.Empty)
                                    {

                                        var desfragmento = arr_result[i].Split('\"');


                                        int num;
                                        bool res = int.TryParse(desfragmento[0].Replace(" ", "").Replace("\t", ""), out num);

                                        if (res)
                                        {
                                            if (!item_ext_desc.ContainsKey(desfragmento[0].Replace(" ", "").Replace("\t", "")))
                                            {
                                                item_ext_desc.Add(desfragmento[0].Replace(" ", "").Replace("\t", "").Trim(), desfragmento[1]);
                                            }

                                        }

                                    }

                                }



                                //MainWindow.item_ext_desc_TXT = result.Split(new char[] { '\"' });
                                //string[] temp = MainWindow.item_ext_desc_TXT[0].Split(new char[] { '\n' });
                                //MainWindow.item_ext_desc_TXT[0] = temp[temp.Length - 1];


                            }
                            catch (Exception e)
                            {
                                MessageBox.Show("ERROR LOADING ITEM DESCRIPTION LIST\n" + e.Message);
                            }
                        }
                        else
                        {
                            MessageBox.Show("NOT FOUND item_ext_desc.txt!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR LOADING ITEM DESCRIPTION LIST\n" + ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        string path = Path.GetDirectoryName(Application.ExecutablePath) + @"\resources\configs\item_ext_desc.txt";
                        string extension = Path.GetExtension(path);
                        if (File.Exists(path))
                        {
                            try
                            {
                                item_ext_desc = new SortedList();

                                using(StreamReader sr = new StreamReader(path, Encoding.Unicode))
                                while (!sr.EndOfStream)
                                {   
                                        string line = sr.ReadLine();
                                        if (!(line.StartsWith("#") || line.StartsWith("/") || string.IsNullOrEmpty(line)))
                                        {
                                            var xLine = line.Split('"').Where(x => !string.IsNullOrEmpty(x)).ToArray();
                                            item_ext_desc.Add(xLine[0].Replace("\t",null), xLine[1]);
                                        }
                                }

                            }
                            catch (Exception e)
                            {
                                MessageBox.Show("ERROR LOADING ITEM DESCRIPTION LIST\n" + e.Message);
                            }
                        }
                        else
                        {
                            MessageBox.Show("NOT FOUND item_ext_desc.txt!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR LOADING ITEM DESCRIPTION LIST\n" + ex.Message);
                    }
                }



                GC.Collect();
                database.item_ext_desc = item_ext_desc;
            }
            catch (Exception)
            {

                throw;
            }
			
        }

		private SortedList<string, Point> LoadSurfaces()
        {
           if (File.Exists(MainWindow.XmlData.SurfacesPckPath))
                {
                    pck = new PCKs(MainWindow.XmlData.SurfacesPckPath);
                    IEnumerable<PCKFileEntry> source_img = pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.StartsWith("surfaces\\iconset\\iconlist_ivtrm.dds")));
                    byte[] array_IMG = ((IEnumerable<byte>)pck.ReadFile(pck.PckFile, source_img.ElementAt<PCKFileEntry>(0))).ToArray<byte>();
                    
                    DDSReader.Utils.PixelFormat sti = (DDSReader.Utils.PixelFormat)DDSFORMAT;
                    Bitmap bmp2 = DDSReader.DDS.LoadImage(array_IMG, true, sti);

                    if (bmp2 != null)
                    {

                        if (bmp2 != null)
                        {
                            sourceBitmap = bmp2;
                            string tempFileName_img = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\surfaces\iconset\iconlist_ivtrm.dds";
                            if (File.Exists(tempFileName_img))
                            {
                                File.Delete(tempFileName_img);
                                bmp2.Save(tempFileName_img);
                                bmp2.Save(tempFileName_img.Replace(Path.GetExtension(tempFileName_img), ".png"), ImageFormat.Png);
                            }
                            else
                            {
                                bmp2.Save(tempFileName_img);
                                bmp2.Save(tempFileName_img.Replace(Path.GetExtension(tempFileName_img), ".png"), ImageFormat.Png);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Unable to load thumbnails...");
                            //sourceBitmap = (Bitmap)Image.FromFile(Path.GetDirectoryName(Application.ExecutablePath) + "\\resources\\surfaces\\iconset\\iconlist_ivtrm.png");
                        }
                    }

                    database.sourceBitmap = sourceBitmap;
                    SortedList<string, Bitmap> results = new SortedList<string, Bitmap>();
                    List<Bitmap> zxczxc = new List<Bitmap>();
                    List<string> fileNames = new List<string>();

                    imagesx = new SortedList<int, string>();
                    int w = 0; int h = 0;

                    IEnumerable<PCKFileEntry> source = pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.StartsWith("surfaces\\iconset\\iconlist_ivtrm.txt")));
                    byte[] array = ((IEnumerable<byte>)pck.ReadFile(pck.PckFile, source.ElementAt<PCKFileEntry>(0))).ToArray<byte>();

                    string tempFileName = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\surfaces\iconset\iconlist_ivtrm.txt";
                    File.WriteAllBytes(tempFileName, array);
                    var iconlist_ivtrm_Read = File.ReadAllLines(tempFileName, Encoding.GetEncoding("GBK"));

                    LP.preg_max = iconlist_ivtrm_Read.Length;

                    for (int i = 0; i < iconlist_ivtrm_Read.Length; i++)
                    {
                       
                        if (iconlist_ivtrm_Read[i] != null)
                        {
                            LP.preg = i;
                            switch (i)
                            {
                                case 0:
                                    w = int.Parse(iconlist_ivtrm_Read[i]);
                                    break;
                                case 1:
                                    h = int.Parse(iconlist_ivtrm_Read[i]);
                                    break;
                                case 2:
                                    rows = int.Parse(iconlist_ivtrm_Read[i]);
                                    database.rows = rows;
                                    break;
                                case 3:
                                    cols = int.Parse(iconlist_ivtrm_Read[i]);
                                    database.cols = cols;
                                    break;
                                default:
                                    fileNames.Add(iconlist_ivtrm_Read[i]);
                                    break;
                            }

                        }
                    }
                    imageposition = new SortedList<string, Point>();
                    int x, y = 0;

                    LP.preg_max = fileNames.Count;
                    for (int a = 0; a < fileNames.Count; a++)
                    {
                        Application.DoEvents();
                        y = a / cols;
                        x = a - y * cols;
                        x = x * w;
                        y = y * h;
                        try
                        {
                            LP.preg = a;
                            imagesx.Add(a, fileNames[a]);
                            imageposition.Add(fileNames[a], new Point(x, y));
                        }
                        catch (Exception) { }

                    }

                    database.imagesx = imagesx;
                    database.imageposition = imageposition;
                    return imageposition;
                }
                else
                {
                    string sourceFilename = Path.GetDirectoryName(Application.ExecutablePath) + "\\resources\\surfaces\\iconset\\iconlist_ivtrm.png";

                    string extension = Path.GetExtension(sourceFilename);
                    if (extension == ".dds")
                    {
                        DDSReader.Utils.PixelFormat st = (DDSReader.Utils.PixelFormat)DDSFORMAT;
                        Bitmap bmp = DDSReader.DDS.LoadImage(sourceFilename, true, st);
                        if (bmp != null)
                        {
                            sourceBitmap = bmp;
                        }
                        else
                        {
                            MessageBox.Show("Unable to load thumbnails...");
                            //sourceBitmap = (Bitmap)Image.FromFile(Path.GetDirectoryName(Application.ExecutablePath) + "\\resources\\surfaces\\iconset\\iconlist_ivtrm.png");
                        }
                    }
                    else
                    {
                        sourceBitmap = (Bitmap)Image.FromFile(sourceFilename);
                    }
                    if (sourceBitmap == null)
                    {
                        MessageBox.Show("Unable to load dds image...");
                        sourceBitmap = (Bitmap)Image.FromFile(Path.GetDirectoryName(Application.ExecutablePath) + "\\resources\\surfaces\\iconset\\iconlist_ivtrm.png");
                    }
                    database.sourceBitmap = sourceBitmap;
                    SortedList<string, Bitmap> results = new SortedList<string, Bitmap>();
                    List<Bitmap> zxczxc = new List<Bitmap>();
                    List<string> fileNames = new List<string>();

                    imagesx = new SortedList<int, string>();
                    int w = 0;
                    int h = 0;

                    int counter = 0;
                    string line;
                    string iconlist_ivtrm = Path.GetDirectoryName(Application.ExecutablePath) + "\\resources\\surfaces\\iconset\\iconlist_ivtrm.txt";
                    Encoding enc = Encoding.GetEncoding("GBK");
                    StreamReader file = null;
                    string extension2 = Path.GetExtension(iconlist_ivtrm);
                    file = new StreamReader(iconlist_ivtrm, enc);
                    LP.preg_max = iconlist_ivtrm.Length;
                    int ct = 0;
                    while ((line = file.ReadLine()) != null)
                    {
                        LP.preg = ct;ct++;
                        switch (counter)
                        {
                            case 0:
                                w = int.Parse(line);
                                break;
                            case 1:
                                h = int.Parse(line);
                                break;
                            case 2:
                                rows = int.Parse(line);
                                database.rows = rows;
                                break;
                            case 3:
                                cols = int.Parse(line);
                                database.cols = cols;
                                break;
                            default:
                                fileNames.Add(line);
                                break;
                        }
                        counter++;
                    }
                    file.Close();
                    imageposition = new SortedList<string, Point>();
                    int x, y = 0;
                    LP.preg_max = fileNames.Count;
                    for (int a = 0; a < fileNames.Count; a++)
                    {
                        Application.DoEvents();
                        y = a / cols;
                        x = a - y * cols;
                        x = x * w;
                        y = y * h;
                        try
                        {
                            LP.preg = a;
                            imagesx.Add(a, fileNames[a]);
                            imageposition.Add(fileNames[a], new Point(x, y));
                        }
                        catch (Exception) { }

                    }
                }

                database.imagesx = imagesx;
                database.imageposition = imageposition;
                return imageposition;
           
			

            
        }

        public void LoadSkillList()
        {
            try
            {
                if (File.Exists(MainWindow.XmlData.ConfigsPckPath))
                {
                    pck = new PCKs(MainWindow.XmlData.ConfigsPckPath);
                    IEnumerable<PCKFileEntry> source = pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.StartsWith("configs\\skillstr.txt")));
                    byte[] array = ((IEnumerable<byte>)pck.ReadFile(pck.PckFile, source.ElementAt<PCKFileEntry>(0))).ToArray<byte>();

                    string tempFileName = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\configs\skillstr.txt";
                    File.WriteAllBytes(tempFileName, array);
                    var skillstr_Read = File.ReadAllLines(tempFileName, Encoding.GetEncoding("GBK"));
                    string result = string.Join("\n", skillstr_Read);
                    
                    if (File.Exists(tempFileName))
                    {
                        try
                        {
                            MainWindow.skillstr = result.Split(new char[] { '\"' });
                            string[] temp = MainWindow.skillstr[0].Split(new char[] { '\n' });
                            MainWindow.skillstr[0] = temp[temp.Length - 1];
                            
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("ERROR LOADING SKILL LIST\n" + e.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("NOT FOUND localization\\skillstr.txt!");
                    }
                    database.skillstr = MainWindow.skillstr;
                }
                else
                {
                    if (database.skillstr != null)
                    {
                        MainWindow.skillstr = database.skillstr;
                        return;
                    }
                    String path = Path.GetDirectoryName(Application.ExecutablePath) + "\\resources\\configs\\skillstr.txt";
                    if (File.Exists(path))
                    {
                        try
                        {
                            StreamReader sr = new StreamReader(path, Encoding.Unicode);
                            MainWindow.skillstr = sr.ReadToEnd().Split(new char[] { '\"' });
                            string[] temp = MainWindow.skillstr[0].Split(new char[] { '\n' });
                            MainWindow.skillstr[0] = temp[temp.Length - 1];
                            sr.Close();
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("ERROR LOADING SKILL LIST\n" + e.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("NOT FOUND localization\\skillstr.txt!");
                    }
                    database.skillstr = MainWindow.skillstr;
                }

            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void LoadAddonList()
        {
            if (database.addonslist != null)
            {
                MainWindow.addonslist = database.addonslist;
                return;
            }
            String path = Path.GetDirectoryName(Application.ExecutablePath) + "\\resources\\data\\addon_table.txt";
            MainWindow.addonslist = new SortedList();
            if (File.Exists(path))
            {
                try
                {
                    StreamReader sr = new StreamReader(path, Encoding.Unicode);

                    char[] seperator = new char[] { '\t' };
                    string line;
                    string[] split;
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        if (line.Contains("\t") && line != "" && !line.StartsWith("/") && !line.StartsWith("#"))
                        {
                            split = line.Split(seperator);
                            MainWindow.addonslist.Add(split[0], split[1]);
                        }
                    }

                    sr.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show("ERROR LOADING ADDON LIST\n" + e.Message);
                }
            }
            else
            {
                MessageBox.Show("NOT FOUND! " + path);
            }
            database.addonslist = MainWindow.addonslist;
        }

        public void LoadLocalizationText()
        {
            MainWindow.LocalizationText = new SortedList();
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\resources\\data\\language_en.txt";
            if (File.Exists(path))
            {
                try
                {
                    StreamReader sr = new StreamReader(path, Encoding.Unicode);

                    char[] seperator = new char[] { '"' };
                    string line;
                    string[] split;
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        if (line != "" && !line.StartsWith("/") && !line.StartsWith("#"))
                        {
                            split = line.Split(seperator);
                            MainWindow.LocalizationText.Add(split[0].Trim(), split[1]);
                        }
                    }

                    sr.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show("ERROR LOADING LOCALIZATION\n" + e.Message);
                }
            }
            else
            {
                MessageBox.Show("NOT FOUND localization:" + path + "!");
            }
            database.LocalizationText = MainWindow.LocalizationText;
        }

        //public void LoadInstanceList()
        //{
        //    if (database.InstanceList != null)
        //    {
        //        MainWindow.InstanceList = database.InstanceList;
        //        return;
        //    }

        //    database.defaultMapsTemplate = new SortedList<int, Map>();
        //    MainWindow.InstanceList = new SortedList();
        //    String path = Path.GetDirectoryName(Application.ExecutablePath) + "\\configs\\instance_en.txt";
        //    if (File.Exists(path))
        //    {
        //        try
        //        {
        //            StreamReader sr = new StreamReader(path, Encoding.Unicode);

        //            char[] seperator = new char[] { '\t' };
        //            string line;
        //            string[] split;
        //            while (!sr.EndOfStream)
        //            {
        //                line = sr.ReadLine();
        //                if (line.Contains("\t") && line != "" && !line.StartsWith("/") && !line.StartsWith("#"))
        //                {
        //                    split = line.Split(seperator);
        //                    if (split.Length > 2)
        //                    {
        //                        MainWindow.InstanceList.Add(split[0], " [" + split[1] + "] [" + split[2] + "] " + split[3] + "");
        //                        Map map = new Map();
        //                        map.name = split[3];
        //                        map.realName = split[2];
        //                        database.defaultMapsTemplate.Add(Int32.Parse(split[0]), map);
        //                    }
        //                    else
        //                    {
        //                        MainWindow.InstanceList.Add(split[0], split[1]);
        //                    }
        //                }
        //            }

        //            sr.Close();
        //        }
        //        catch (Exception e)
        //        {
        //            MessageBox.Show("ERROR LOADING INSTANCE LIST\n" + e.Message);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("NOT FOUND localization:" + path + "!");
        //    }
        //    database.InstanceList = MainWindow.InstanceList;
        //}

        public void LoadBuffList()
        {

            try
            {
                if (database.buff_str != null)
                {
                    MainWindow.buff_str = database.buff_str;
                    return;
                }

                if (File.Exists(MainWindow.XmlData.ConfigsPckPath))
                {
                    pck = new PCKs(MainWindow.XmlData.ConfigsPckPath);
                    IEnumerable<PCKFileEntry> source = pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.StartsWith("configs\\buff_str.txt")));
                    byte[] array = ((IEnumerable<byte>)pck.ReadFile(pck.PckFile, source.ElementAt<PCKFileEntry>(0))).ToArray<byte>();

                    string tempFileName = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\configs\buff_str.txt";
                    File.WriteAllBytes(tempFileName, array);
                    var buff_str_Read = File.ReadAllLines(tempFileName, Encoding.GetEncoding("GBK"));
                    string result = string.Join("\n", buff_str_Read);

                    if (File.Exists(tempFileName))
                    {
                        try
                        {
                            MainWindow.buff_str = result.Split(new char[] { '\"' });
                            string[] temp = MainWindow.buff_str[0].Split(new char[] { '\n' });
                            MainWindow.buff_str[0] = temp[temp.Length - 1];
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("ERROR LOADING BUFF LIST\n" + e.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("NOT FOUND localization\\skillstr.txt!");
                    }
                    database.skillstr = MainWindow.skillstr;
                }
                else
                {
                    string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\resources\\configs\\buff_str.txt";
                    if (File.Exists(path))
                    {
                        try
                        {
                            StreamReader sr = new StreamReader(path, Encoding.Unicode);
                            MainWindow.buff_str = sr.ReadToEnd().Split(new char[] { '\"' });
                            string[] temp = MainWindow.buff_str[0].Split(new char[] { '\n' });
                            MainWindow.buff_str[0] = temp[temp.Length - 1];

                            sr.Close();
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("ERROR LOADING BUFF LIST\n" + e.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("NOT FOUND localization\\buff_str.txt!");
                    }
                    database.buff_str = MainWindow.buff_str;
                }

            }
            catch (Exception)
            {

                throw;
            }
            
            
        }
    }
}
