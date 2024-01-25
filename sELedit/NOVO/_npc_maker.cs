using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sELedit.NOVO
{
    public partial class _npc_maker : Form
    {
        public int ID { get; set; }
        public int LIST { get; set; }
        public int GET { get; set; }
        public string TYPE { get; set; }
        Thread AssetManagerLoad;
        public _npc_maker()
        {
            InitializeComponent();
        }

        private void _major_sub_Load(object sender, EventArgs ee)
        {

            AssetManagerLoad = new Thread(delegate () {


                for (int e = 0; e < MainWindow.eLC.Lists[54].elementValues.Length; e++)
                {
                    dataGridView_elems.Invoke((MethodInvoker)delegate () {

                        dataGridView_elems.Rows.Add(new object[] { MainWindow.eLC.GetValue(54, e, 0), MainWindow.eLC.GetValue(54, e, 1) });

                        Text = "NPC MAKER (" + e + " - " + MainWindow.eLC.Lists[54].elementValues.Length.ToString() + " )";
                    });
                }


                if (ID != 0)
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

            }); AssetManagerLoad.Start();

           
           
            
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
