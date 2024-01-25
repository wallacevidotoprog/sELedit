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
    public partial class _major_sub : Form
    {
        public int ID { get; set; }
        public int LIST { get; set; }
        public int GET { get; set; }
        public string TYPE { get; set; }

        public _major_sub()
        {
            InitializeComponent();
        }

        private void _major_sub_Load(object sender, EventArgs e)
        {
            if (TYPE == "id_major_type")
            {
                this.Text = "MAJOR TYPE";
                bool fini = false;
                for (int l = 0; l < MainWindow.eLC.Lists.Length; l++)
                {
                    string major = MainWindow.eLC.Lists[LIST].listName.Split(new string[] { " - " }, StringSplitOptions.None)[1].Replace("ESSENCE", "MAJOR_TYPE");

                    string conf = MainWindow.eLC.Lists[l].listName.Split(new string[] { " - " }, StringSplitOptions.None)[1];
                    if (major == conf)
                    {
                        for (int m = 0; m < MainWindow.eLC.Lists[l].elementValues.Length; m++)
                        {
                            dataGridView_elems.Rows.Add(new object[] { MainWindow.eLC.GetValue(l, m, 0), MainWindow.eLC.GetValue(l, m, 1) });

                        }
                        if (fini)
                        {
                            break;
                        }

                    }
                }
            }
            if (TYPE == "id_sub_type")
            {
                this.Text = "SUB TYPE";
                bool fini = false;
                for (int l = 0; l < MainWindow.eLC.Lists.Length; l++)
                {
                    string major = MainWindow.eLC.Lists[LIST].listName.Split(new string[] { " - " }, StringSplitOptions.None)[1].Replace("ESSENCE", "SUB_TYPE");

                    string conf = MainWindow.eLC.Lists[l].listName.Split(new string[] { " - " }, StringSplitOptions.None)[1];
                    if (major == conf)
                    {
                        for (int m = 0; m < MainWindow.eLC.Lists[l].elementValues.Length; m++)
                        {
                            dataGridView_elems.Rows.Add(new object[] { MainWindow.eLC.GetValue(l, m, 0), MainWindow.eLC.GetValue(l, m, 1) });

                        }
                        if (fini)
                        {
                            break;
                        }

                    }
                }
            }

            try
            {
                foreach (DataGridViewRow item in dataGridView_elems.Rows)
                {
                    if (item.Cells[0].Value.ToString() == ID.ToString())
                    {
                        dataGridView_elems.Rows[item.Index].Selected = true;
                        dataGridView_elems.FirstDisplayedScrollingRowIndex = dataGridView_elems.RowCount - 1;
                        break;
                    }
                }
            }
            catch 
            {

                
            }
        }

        private void dataGridView_elems_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                string a = dataGridView_elems.Rows[dataGridView_elems.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                string b = dataGridView_elems.Rows[dataGridView_elems.SelectedCells[0].RowIndex].Cells[1].Value.ToString();
                GET = int.Parse(a);
                Close();
            }
            catch (Exception)
            {

                
            }
        }
    }
}
