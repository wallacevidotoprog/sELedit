using sELedit.configs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sELedit.NOVO
{
    public partial class RECIPES : Form
    {
        Encoding enc = Encoding.GetEncoding("Unicode");
        Thread AssetManagerLoad;
        public RECIPES()
        {
            InitializeComponent();

        }


        public int input { get; set; }
        public int GET { get; set; }

        private void RECIPES_Load(object sender, EventArgs ex)
        {
            AssetManagerLoad = new Thread(delegate () {

                    
                for (int e = 0; e < MainWindow.eLC.Lists[69].elementValues.Length; e++)
                {
                    dataGridView_elems.Invoke((MethodInvoker)delegate () {

                        dataGridView_elems.Rows.Add(new object[] { MainWindow.eLC.GetValue(69, e, 0), "", MainWindow.eLC.GetValue(69, e, 3) });

                        Text = "RECIPES ("+e+" - "+ MainWindow.eLC.Lists[69].elementValues.Length.ToString()+ " )";
                    });
                }


            }); AssetManagerLoad.Start();
        }


        public Image img(int IdRecipe, int idIndex)
        {
            Image ig = null;

            int id_1 = int.Parse(MainWindow.eLC.GetValue(69, idIndex, 8));
            bool fi = false;

            if (IdRecipe != 0)
            {
                try
                {
                    string value = "";
                    for (int L = 0; L < MainWindow.database.ItemUse.Count; L++)
                    {

                        int La = int.Parse(MainWindow.database.ItemUse.GetKey(L).ToString());
						int pos = 0;
                        int posN = 0;

                        for (int i = 0; i < MainWindow.eLC.Lists[La].elementFields.Length; i++)
                        {
                            if (MainWindow.eLC.Lists[La].elementFields[i] == "Name")
                            {
                                posN = i;
                                //break;
                            }
                            if (MainWindow.eLC.Lists[La].elementFields[i] == "file_icon")
                            {
                                pos = i;
                                break;
                            }

                        }
                        for (int ef = 0; ef < MainWindow.eLC.Lists[La].elementValues.Length; ef++)
                        {
                            value = MainWindow.eLC.GetValue(La, ef, pos);

                            if (id_1 == int.Parse(MainWindow.eLC.GetValue(La, ef, 0))/* || value.Contains(b.ToString())*/)
                            {
                                string path = Path.GetFileName(value);
                                if (MainWindow.database.sourceBitmap != null && MainWindow.database.ContainsKey(path))
                                {
                                    if (MainWindow.database.ContainsKey(path))
                                    {
                                        ig= Extensions.ResizeImage(MainWindow.database.images(path), 32, 32);
                                        
                                        fi = true;


                                        break;
                                    }
                                }
                            }
                        }

                        if (fi == true)
                        {
                            break;
                        }
                    }


                }
                catch (Exception edx)
                {

                    
                }
            }


            if (ig == null)
            {
                ig = Properties.Resources.unknown;
            }

            return ig;
        }

        private void dataGridView_elems_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int id = int.Parse(((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value.ToString());
            Image igg = img(int.Parse(((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value.ToString()), e.RowIndex);
            ((TextAndImageCell)((DataGridView)sender).Rows[e.RowIndex].Cells[1]).Image = igg;


        }

        private void dataGridView_elems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            GET = int.Parse(((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value.ToString());
            this.Close();
        }
    }
}
