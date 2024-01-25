using System;
using System.Collections;
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
    public partial class set_ADDONS : Form
    {
        public set_ADDONS()
        {
            InitializeComponent();
        }
        public static SortedList _wepon;
        public static SortedList _armor;
        public static SortedList _decoration;
		public static SortedList _suite;

		public int gINDEX { get; set; }
        public int input { get; set; }
        public int GET { get; set; }




        private void set_ADDONS_Load(object sender, EventArgs e)
        {
            _wepon = MainWindow.database._wepon;
            _armor = MainWindow.database._armor;
            _decoration = MainWindow.database._decoration;
			_suite = MainWindow.database._suite;

			listBox1.Items.Clear();
            listBox1.Items.Add("WEAPON_ESSENCE"); listBox1.Items.Add("ARMOR_ESSENCE"); listBox1.Items.Add("DECORATION_ESSENCE"); listBox1.Items.Add("SUITE_ESSENCE");
			if (input!= null)
            {
				listBox1.SelectedIndex = 0;
			}
            else
            {
                listBox1.SelectedIndex = 0;
            }
            


        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int x = listBox1.SelectedIndex;
            dataGridView_adds.Rows.Clear();
            switch (x)
            {
                case 0:
                    foreach (DictionaryEntry pair in _wepon)
                    {
                        dataGridView_adds.Rows.Add(new object[] { pair.Key, EQUIPMENT_ADDON.GetAddon(pair.Key.ToString()) });
                    }
                    break;
                case 1:
                    foreach (DictionaryEntry pair in _armor)
                    {
						dataGridView_adds.Rows.Add(new object[] { pair.Key, EQUIPMENT_ADDON.GetAddon(pair.Key.ToString()) });
					}
                    break;
                case 2:
                    foreach (DictionaryEntry pair in _decoration)
                    {
						dataGridView_adds.Rows.Add(new object[] { pair.Key, EQUIPMENT_ADDON.GetAddon(pair.Key.ToString()) });
					}
                    break;
				case 3:
					foreach (DictionaryEntry pair in _suite)
					{
						dataGridView_adds.Rows.Add(new object[] { pair.Key, pair.Value });
					}
					break;
				default:
                    break;
            }
            //if (input != null)
            //{
            //    foreach (DataGridViewRow item in dataGridView_adds.Rows)
            //    {
            //        if (item.Cells[0].Value.ToString() == input.ToString())
            //        {

            //            dataGridView_adds.Rows[item.Index].Selected = true;
            //            dataGridView_adds.CurrentCell = dataGridView_adds.Rows[item.Index].Cells[0];
                        
            //            break;
            //        }
            //    }
            //}
        }

		private void dataGridView_adds_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			
				GET = int.Parse(((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value.ToString());
				this.Close();
			
		}
	}
}
