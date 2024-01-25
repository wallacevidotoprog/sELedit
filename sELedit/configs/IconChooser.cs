using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sELedit
{
    public partial class IconChooser : Form
    {
        public IconChooser(MainWindow fm,Bitmap bm, List<LBLIBRARY.PWHelper.ShopIcon> ss,bool b)
        {
            this.ImageLoaded = b;
            this.Surfaces_icons = ss;
            this.LinkedImage = bm;
            this.Main_form = fm;
            InitializeComponent();
        }
        bool ImageLoaded;
        List<LBLIBRARY.PWHelper.ShopIcon> Surfaces_icons;
        int Last_index;
        Bitmap LinkedImage;
        MainWindow Main_form;
        int Index;
        public void FindIcon(int ind)
        {
            if (pictureBox1.Image != null)
            {
                int Y = (ind / 9) * 128;
                int X = ind - ((Y / 128) * 9);
                int f = pictureBox1.Size.Width - Y;
                panel1.VerticalScroll.Value = Y;
                pictureBox2.Location = new Point((X * 128) + 3, Y + 125);
                pictureBox2.Visible = true;
            }
        }
        private void Images_picturebox_MouseMove(object sender, MouseEventArgs e)
        {
            Index = (e.X / 128) + (9 * (e.Y / 128));
            int X_index = (int)(e.X / 128) * 128;
            int Y_index = (int)((e.Y / 128) * 128);
            if (Index != Last_index)
            {
                if (Surfaces_icons.Count >= Index + 1)
                {
                    X1.Location = new Point(X_index, Y_index);
                    X2.Location = new Point(X_index, Y_index+125);
                    Y1.Location = new Point(X_index, Y_index);
                    Y2.Location = new Point(X_index+127, Y_index);
                    Last_index = Index;
                }
                else
                {
                    Last_index = -1;
                }
            }
        }

        private void Images_picturebox_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }
        private void Images_picturebox_Click(object sender, EventArgs e)
        {
           if (Surfaces_icons.Count >= Index + 1)
           {
               Main_form.SetShopIconImage(Surfaces_icons[Index].Name, Surfaces_icons[Index].Icon);
               this.Hide();
           }
        }
        private void IconChooser_Load(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null && ImageLoaded == false)
            {
                pictureBox1.Image = LinkedImage;
                ImageLoaded = true;
                pictureBox2.Parent = pictureBox1;
                X1.Parent = pictureBox1;
                X2.Parent = pictureBox1;
                Y1.Parent = pictureBox1;
                Y2.Parent = pictureBox1;
            }
        }
    }
}
