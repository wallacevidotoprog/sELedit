using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sELedit
{
    public partial class LoasdsProgreces : UserControl
    {
        public LoasdsProgreces()
        {
            InitializeComponent();
        }

        public int ark { get; set; }
        public int ark_total { get; set; }
        public int preg { get; set; }
        public int preg_max { get; set; }

        private void timerload_Tick(object sender, EventArgs e)
        {
            label_read.Text = String.Format($"READ ({ark_total}/{ark_total})");
            progressBar_read.Maximum = preg_max;
            progressBar_read.Value = preg;
        }

        private void LoasdsProgreces_Load(object sender, EventArgs e)
        {
            progressBar_read.Minimum = 0;
            timerload.Start();
        }
    }
}
