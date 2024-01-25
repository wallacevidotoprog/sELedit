using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sELedit.NOVO
{
    public partial class ProcTypeGenerator : Form
    {
        public ProcTypeGenerator()
        {
            InitializeComponent();
        }

        public int input{get;set ;}
        public int GET { get; set; }


        private void Get_proc_type(string proc_type)
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
                        checkBox_0.Checked = true;
                        break;
                    case 1:
                        checkBox_1.Checked = true;
                        break;
                    case 2:
                        checkBox_2.Checked = true;
                        break;
                    case 3:
                        checkBox_3.Checked = true;
                        break;
                    case 4:
                        checkBox_4.Checked = true;
                        break;
                    case 5:
                        checkBox_5.Checked = true;
                        break;
                    case 7:
                        checkBox_7.Checked = true;
                        break;
                    case 8:
                        checkBox_8.Checked = true;
                        break;
                    case 9:
                        checkBox_9.Checked = true;
                        break;
                    case 10:
                        checkBox_10.Checked = true;
                        break;
                    case 11:
                        checkBox_11.Checked = true;
                        break;
                    case 12:
                        checkBox_12.Checked = true;
                        break;
                    case 14:
                        checkBox_14.Checked = true;
                        break;
                }
            }

         

        }

        private void ProcTypeGenerator_Load(object sender, EventArgs e)
        {
            checkBox_0.Text = Extensions.GetLocalization(3000).Replace("\n","");//proc_type_1;
            checkBox_1.Text = Extensions.GetLocalization(3001).Replace("\n","");//proc_type_2;
            checkBox_2.Text = Extensions.GetLocalization(3002).Replace("\n","");//proc_type_4;
            checkBox_3.Text = Extensions.GetLocalization(3003).Replace("\n","");//proc_type_8;
            checkBox_4.Text = Extensions.GetLocalization(3004).Replace("\n","");//proc_type_16;
            checkBox_5.Text = Extensions.GetLocalization(3005).Replace("\n","");//proc_type_32;
            checkBox_6.Text = "NULLL";
            checkBox_7.Text = Extensions.GetLocalization(3007).Replace("\n","");//proc_type_128;
            checkBox_8.Text = Extensions.GetLocalization(3008).Replace("\n","");//proc_type_256;
            checkBox_9.Text = Extensions.GetLocalization(3009).Replace("\n","");//proc_type_512;
            checkBox_10.Text = Extensions.GetLocalization(3010).Replace("\n","");//proc_type_1024;
            checkBox_11.Text = Extensions.GetLocalization(3011).Replace("\n","");//proc_type_2048;
            checkBox_12.Text = Extensions.GetLocalization(3012).Replace("\n","");//proc_type_4096;
            checkBox_13.Text ="NULL";
            checkBox_14.Text = Extensions.GetLocalization(3014).Replace("\n","");//proc_type_16384;

            Get_proc_type(input.ToString());
        }

        private void checkBox_0_CheckedChanged(object sender, EventArgs e)
        {
            int number = 0;
            if (checkBox_0.Checked) { number += 1; }
            if (checkBox_1.Checked) { number += 2; }
            if (checkBox_2.Checked) { number += 4; }
            if (checkBox_3.Checked) { number += 8; }
            if (checkBox_4.Checked) { number += 16; }
            if (checkBox_5.Checked) { number += 32; }
            if (checkBox_6.Checked) { number += 0; }
            if (checkBox_7.Checked) { number += 128; }
            if (checkBox_8.Checked) { number += 256; }
            if (checkBox_9.Checked) { number += 512; }
            if (checkBox_10.Checked) { number += 1024; }
            if (checkBox_11.Checked) { number += 2048; }
            if (checkBox_12.Checked) { number += 4096; }
            if (checkBox_13.Checked) { number += 0; }
            if (checkBox_14.Checked) { number += 16384; }
            GET = number;
            button1.Text = number.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GET = int.Parse(button1.Text);
            this.Close();
        }

        private void checkBox_1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
