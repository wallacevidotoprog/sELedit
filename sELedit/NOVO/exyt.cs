using System;
using System.Collections;
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
    public partial class exyt : Form
    {
        string aText;
        int totalP;
        int moveP;


        int FILEs;
        int ark;

        Thread AssetManagerLoad;
        public exyt()
        {
            InitializeComponent();
        }
        public SortedList _wepon;
        public SortedList _armor;
        public SortedList _decoration;

        void LoadsAdds()
        {
            _wepon = new SortedList(); _armor = new SortedList(); _decoration = new SortedList();
            string aa = ""; string bb = ""; string cc ="";
            int[] adds = { 3, 6, 9 };


            for (int l = 0; l < adds.Length; l++)
            {
                aText = "(" + l + "/2) INDEX=" + adds[l].ToString() ;
                totalP = MainWindow.eLC.Lists[adds[l]].elementValues.Length;
                moveP = 0;
                for (int e = 0; e < MainWindow.eLC.Lists[adds[l]].elementValues.Length; e++)
                {
                    moveP++;
                    FILEs = MainWindow.eLC.Lists[adds[l]].elementValues[e].Length;
                    ark = 0;
                    for (int f = 46; f < MainWindow.eLC.Lists[adds[l]].elementValues[e].Length; f++)
                    {
                        var a = MainWindow.eLC.Lists[adds[l]].elementFields[f]; //var b = eLC.Lists[adds[l]].elementTypes[f]; 
                        ark++;

                        if (a.EndsWith("_id_addon") | a.EndsWith("_id_rand") | a.EndsWith("_id_unique"))
                        {
                            int c = int.Parse(MainWindow.eLC.GetValue(adds[l], e, f));

                            switch (adds[l])
                            {
                                case 3:
                                    try
                                    {
                                        if (c != 0)
                                        {
                                            _wepon.Add(c, c);
                                            aa += c + "\n";
                                        }
                                    }
                                    catch (Exception)
                                    {


                                    }
                                    break;
                                case 6:
                                    try
                                    {
                                        if (c != 0)
                                        {
                                            _armor.Add(c, c);
                                            bb += c + "\n";
                                        }
                                    }
                                    catch (Exception)
                                    {


                                    }
                                    break;
                                case 9:
                                    try
                                    {
                                        if (c != 0)
                                        {
                                            _decoration.Add(c, c);
                                            cc += c + "\n";
                                        }
                                    }
                                    catch (Exception)
                                    {


                                    }
                                    break;

                                default:
                                    break;
                            }
                           

                        }


                    }
                }


            }


            StreamWriter salvar1 = new StreamWriter(Path.GetDirectoryName(Application.ExecutablePath) + @"\" +@"resources\opt\add_wepom.txt");
            salvar1.WriteLine(aa);
            salvar1.Close();

            StreamWriter salvar2 = new StreamWriter(Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\opt\add_armor.txt");
            salvar2.WriteLine(bb);
            salvar2.Close();

            StreamWriter salvar3 = new StreamWriter(Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\opt\add_decoration.txt");
            salvar3.WriteLine(cc);
            salvar3.Close();

            Close();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Text = aText;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = totalP;
            progressBar1.Value = moveP - 1;


            progressBar2.Minimum = 0;
            progressBar2.Maximum = FILEs;
            progressBar2.Value = ark - 1;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void exyt_Load(object sender, EventArgs e)
        {
            AssetManagerLoad = new Thread(delegate () { LoadsAdds(); }); AssetManagerLoad.Start();
        }
    }
}
