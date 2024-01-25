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
    public partial class loads : Form
    {
        public loads()
        {
            InitializeComponent();
            this.BackColor = Color.Magenta; // ou qualquer outra cor muito incomum (de preferência uma que não exista na imagem)
            this.TransparencyKey = Color.Magenta; // igual à cor de fundo do Form
            this.FormBorderStyle = FormBorderStyle.None; // se quiser remover as bordas do Form
        }
    }
}
