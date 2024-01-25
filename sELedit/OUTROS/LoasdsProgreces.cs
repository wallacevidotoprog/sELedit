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
    public partial class LoasdsProgreces : Form
    {
        public LoasdsProgreces()
        {
            InitializeComponent();
        }
        public int ark { get; set; }
        public int ark_total { get; set; }
        public int preg { get; set; }
        public int preg_max { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            progressBar_read.Minimum = 0;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label_read.Text = String.Format($"READ ({ark_total}/{ark_total})");
            progressBar_read.Maximum = preg_max;
            progressBar_read.Value = preg;
        }
    }
}
