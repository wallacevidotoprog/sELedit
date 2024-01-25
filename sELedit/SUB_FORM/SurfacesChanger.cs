using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sELedit.NOVO
{
	public partial class SurfacesChanger : Form
    {
        
        public  string SET { get; set; }
        public  string GET { get; set; }
        public static GraphicsState State { get; set; }
        public static bool Selected { get; set; }
        public static int[] OldCoord { get; set; }
        public static string _text;
        public SurfacesChanger(/*string _Pat*/)
        {
            InitializeComponent();             
            Helper2.LoadSurfaces();

            var CursH = AdvancedCursorsFromEmbededResources.Create(Properties.Resources.Hand);

            Cursor = CursH;
            pictureBox1.Cursor = CursH;

        }

        private void DrowRect(int[] val, Graphics g)
        {
            {
                Pen pen = new Pen(Color.Chartreuse, 4);
                Brush brush = new SolidBrush(panel1.BackColor);

                g.DrawRectangle(pen, val[0], val[1], 32, 32);

                pen.Dispose();
            }
        }

        void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int mm = 4096 / 32; // Image Default size
                int x = e.X / 32;
                int y = e.Y / 32;
                int val = y == 0 ? x : x + (y * mm);
                var id = Helper2.FindCoord(Helper2._surfaces[val]);
                pictureBox2.Image = Graphic.CropImage(Helper2._img, new Rectangle(id[0], id[1], 32, 32));
                _text = Helper2._surfaces[val];
                OldCoord = new[] { x * 32, y * 32 };
                pictureBox1.Refresh();
            }
            catch (Exception)
            {

               
            }
            
        }

        private void SurfacesChanger_Load(object sender, EventArgs e)
        {           
            panel1.AutoScroll = true;
            pictureBox1.Paint += PictureBox1OnPaint;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.Image = Helper2._img;
            var id = Helper2.FindCoord(SET);
            if (id[1] !=0){panel1.VerticalScroll.Value = id[1] - 32;}            
            if (id[1] < 32)
                id[1] = 33;            
            if (id[1] != 0) { panel1.HorizontalScroll.Value = id[0] - 32; }
            //pictureBox2.Image = Graphic.CropImage(Helper2._img, new Rectangle(id[0], id[1], 32, 32));

            OldCoord = new[] { id[0], id[1] };
            textBox1.Text = SET;
        }

        private void PictureBox1OnPaint(object sender, PaintEventArgs paintEventArgs)
        {
            DrowRect(OldCoord, paintEventArgs.Graphics);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            _text = textBox1.Text;
            GET = _text;
            this.Close();
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                int mm = 4096 / 32; // Image Default size
                int x = e.X / 32;
                int y = e.Y / 32;
                int val = y == 0 ? x : x + (y * mm);
                var id = Helper2.FindCoord(Helper2._surfaces[val]);
                //pictureBox2.Image = Graphic.CropImage(Helper2._img, new Rectangle(id[0], id[1], 32, 32));
                _text = Helper2._surfaces[val];
                OldCoord = new[] { x * 32, y * 32 };
                pictureBox1.Refresh();
                //_text = textBox1.Text;
                GET = _text;
                //MessageBox.Show(_text+ "\n"+ GET);
                this.Close();
            }
            catch (Exception)
            {


            }
            
        }
    }
    class Helper2
    {
       
        public static Image _img;
        public static List<string> _surfaces;
        public static Dictionary<string, Image> _cropped;

        public static void CropImages()
        {
            _cropped = new Dictionary<string, Image>();
            foreach (var ll in _surfaces)
            {
                _cropped.Add(ll, Graphic.GetImage(_img, ll));
            }
            Graphic.bmpImage.Dispose();
            Graphic.bmpImage = null;
        }

        
        public static void LoadSurfaces()
        {
            Thread _CropImages;
            _surfaces = new List<string>();
            try
            {
                _img = Image.FromFile(Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\surfaces\iconset\iconlist_ivtrm.png");
                foreach (var line in File.ReadAllLines(Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\surfaces\iconset\iconlist_ivtrm.txt", Encoding.GetEncoding(936)))
                {
                    if (Path.GetExtension(line) == ".dds")
                        _surfaces.Add(line);
                }
               // _CropImages =new Thread(CropImages).Start(); 
                //_CropImages = new Thread(delegate () { CropImages(); }); _CropImages.Start(); _CropImages.Join();
                // CropImages();
            }
            catch (Exception m)
            {
                MessageBox.Show(m.ToString());
            }


        }

        public static int[] FindCoord(string val)
        {
            int i = 0;
            int x = 0;
            int y = 0;
            if (val == null)
                return new[] { 0, 0 };
            val = val.Replace("\0", "");
            if (i >= _surfaces.Count)
                return new[] { 0, 0 };
            while (Path.GetFileName(val) != _surfaces[i])
            {
                x += 32;
                if (x >= 4096)
                {
                    x = 0;
                    y += 32;
                }
                i++;
                if (i >= _surfaces.Count)
                {
                    return new[] { 32, 0 };
                }
            }
            return new[] { x, y };
        }
    }

    class Graphic
    {
        public static Bitmap bmpImage;
        public static Image CropImage(Image img, Rectangle cropArea)
        {
            if (bmpImage == null)
                bmpImage = new Bitmap(img);
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        }

        public static Image GetImage(Image img, string vals)
        {
            if (vals == null)
                return null;
            vals = vals.Replace("\0", "");
            vals = Path.GetFileName(vals);
            var id = Helper2.FindCoord(vals);

            return CropImage(Helper2._img, new Rectangle(id[0], id[1], 32, 32));
        }
    }





}
