using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace sELedit
{
	public partial class ClassMaskWindow : Form
	{
		bool lockCheckBox;
        public int input { get; set; }
        public string GET { get; set; }
        public ClassMaskWindow()
		{
			InitializeComponent();
            
        }

       



        private void Get_proc_type(string proc_type)
        {
            string line = string.Empty;
            uint proctypes;
            bool result = uint.TryParse(proc_type, out proctypes);
            List<uint> powers = new List<uint>(Extensions.GetPowers(proctypes));


            for (int p = 0; p < powers.Count; p++)
            {
                if (powers[p] == 0) continue;

                //switch (p)
                //{
                //    case 0:
                //        checkBox_BM.Checked = true;
                //        break;
                //    case 1:
                //        checkBox_1.Checked = true;
                //        break;
                //    case 2:
                //        checkBox_2.Checked = true;
                //        break;
                //    case 3:
                //        checkBox_3.Checked = true;
                //        break;
                //    case 4:
                //        checkBox_4.Checked = true;
                //        break;
                //    case 5:
                //        checkBox_5.Checked = true;
                //        break;
                //    case 7:
                //        checkBox_7.Checked = true;
                //        break;
                //    case 8:
                //        checkBox_8.Checked = true;
                //        break;
                //    case 9:
                //        checkBox_9.Checked = true;
                //        break;
                //    case 10:
                //        checkBox_10.Checked = true;
                //        break;
                //    case 11:
                //        checkBox_11.Checked = true;
                //        break;
                //    case 12:
                //        checkBox_12.Checked = true;
                //        break;
                //    case 14:
                //        checkBox_14.Checked = true;
                //        break;
                //}
            }



        }


        private void change_Dec(object sender, EventArgs e)
		{
			lockCheckBox = true;

			int number = Convert.ToInt32(numericUpDown_mask.Value);

			if (number / 2048 > 0)
			{
				number = number % 2048;
				checkBox_ST.Checked = true;
			}
			else
			{
				checkBox_ST.Checked = false;
			}

			if (number / 1024 > 0)
			{
				number = number % 1024;
				checkBox_DU.Checked = true;
			}
			else
			{
				checkBox_DU.Checked = false;
			}

			if (number / 512 > 0)
			{
				number = number % 512;
				checkBox_MY.Checked = true;
			}
			else
			{
				checkBox_MY.Checked = false;
			}

			if (number / 256 > 0)
			{
				number = number % 256;
				checkBox_SE.Checked = true;
			}
			else
			{
				checkBox_SE.Checked = false;
			}

			if (number / 128 > 0)
			{
				number = number % 128;
				checkBox_CLE.Checked = true;
			}
			else
			{
				checkBox_CLE.Checked = false;
			}

			if (number / 64 > 0)
			{
				number = number % 64;
				checkBox_AR.Checked = true;
			}
			else
			{
				checkBox_AR.Checked = false;
			}

			if (number / 32 > 0)
			{
				number = number % 32;
				checkBox_AS.Checked = true;
			}
			else
			{
				checkBox_AS.Checked = false;
			}

			if (number / 16 > 0)
			{
				number = number % 16;
				checkBox_BAR.Checked = true;
			}
			else
			{
				checkBox_BAR.Checked = false;
			}

			if (number / 8 > 0)
			{
				number = number % 8;
				checkBox_VEN.Checked = true;
			}
			else
			{
				checkBox_VEN.Checked = false;
			}

			if (number / 4 > 0)
			{
				number = number % 4;
				checkBox_PSY.Checked = true;
			}
			else
			{
				checkBox_PSY.Checked = false;
			}

			if (number / 2 > 0)
			{
				number = number % 2;
				checkBox_WIZ.Checked = true;
			}
			else
			{
				checkBox_WIZ.Checked = false;
			}

			if (number / 1 > 0)
			{
				number = number % 1;
				checkBox_BM.Checked = true;
			}
			else
			{
				checkBox_BM.Checked = false;
			}

			lockCheckBox = false;

		}

		private void change_Bin(object sender, EventArgs e)
		{
			if (!lockCheckBox)
			{
				int number = 0;
				if (checkBox_BM.Checked)
				{
					number += 1;
				}
				if (checkBox_WIZ.Checked)
				{
					number += 2;
				}
				if (checkBox_PSY.Checked)
				{
					number += 4;
				}
				if (checkBox_VEN.Checked)
				{
					number += 8;
				}
				if (checkBox_BAR.Checked)
				{
					number += 16;
				}
				if (checkBox_AS.Checked)
				{
					number += 32;
				}
				if (checkBox_AR.Checked)
				{
					number += 64;
				}
				if (checkBox_CLE.Checked)
				{
					number += 128;
				}
				if (checkBox_SE.Checked)
				{
					number += 256;
				}
				if (checkBox_MY.Checked)
				{
					number += 512;
				}
				if (checkBox_DU.Checked)
				{
					number += 1024;
				}
				if (checkBox_ST.Checked)
				{
					number += 2048;
				}

				numericUpDown_mask.Value = Convert.ToDecimal(number);
			}
		}

        private void button_sent_Click(object sender, EventArgs e)
        {
            GET = numericUpDown_mask.Value.ToString();
            this.Close();
        }

        private void ClassMaskWindow_Load(object sender, EventArgs e)
        {
            numericUpDown_mask.Value = input;
            Get_proc_type(input.ToString());
        }
    }
}
