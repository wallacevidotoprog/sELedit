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
    public partial class _listTasks : Form
    {
        public int ID { get; set; }
        public int LIST { get; set; }
        public int GET { get; set; }
        public string TYPE { get; set; }

        public _listTasks()
        {
            InitializeComponent();
        }

        private void _major_sub_Load(object sender, EventArgs e)
        {
            

            for (int i = 0; i < MainWindow.database.Tasks.Length; i++)
            {
                dataGridView_elems.Rows.Add(new object[] { MainWindow.database.Tasks[i].ID, MainWindow.database.Tasks[i].Name });
            }



            if (ID!= 0)
            {
                try
                {
                    foreach (DataGridViewRow item in dataGridView_elems.Rows)
                    {
                        if (item.Cells[0].Value.ToString() == ID.ToString())
                        {
                            dataGridView_elems.Rows[item.Index].Selected = true;
                            dataGridView_elems.CurrentCell = dataGridView_elems.Rows[item.Index].Cells[0];
                            break;
                        }
                    }
                }
                catch
                {


                }
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
