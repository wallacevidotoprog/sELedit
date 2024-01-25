using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace sELedit
{
    public partial class teste : Form
    {
        public teste()
        {
            InitializeComponent();

            
        }

        string im = @"C:\xampp\htdocs\FTP CELULAR\elemente 1.5.3\surfaces\卡牌\边框.png";

        private void teste_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = configs.ImgsFiles.TrueStretchImage(Image.FromFile(im), this.Width, this.Height);
        }

    }

   
       

        
    
}
