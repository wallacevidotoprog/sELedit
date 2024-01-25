using LBLIBRARY;
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
    public partial class addons : Form
    {
        public addons()
        {
            InitializeComponent();
        }
        public MainWindow _Main { get; set; }
        public DataGridView dgv { get; set; }
        List<ForLista> lista;
        private void addons_Load(object sender, EventArgs e)
        {
            

            string addons_1_id_addon = null;
            string addons_1_id_addon_ID = null;
            string addons_1_id_addon_type = null;

            string addons_1_probability_addon = null;
            string addons_1_probability_addon_value= null;
            string addons_1_probability_addon_value_type = null;
            int cont = 1;
            int line = 0;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells[0].Value.ToString() == "addons_"+ cont + "_id_addon")
                {
                    addons_1_id_addon = row.Cells[0].Value.ToString();// name
                    addons_1_id_addon_ID = row.Cells[2].Value.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0].Replace("[", "").Replace("]", "").Replace(" ", "");// value
                    addons_1_id_addon_type = row.Cells[1].Value.ToString();// value
                }
                if (row.Cells[0].Value.ToString() == "addons_" + cont + "_probability_addon")
                {
                    addons_1_probability_addon = row.Cells[0].Value.ToString();// name
                    addons_1_probability_addon_value = row.Cells[2].Value.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0].Replace("[", "").Replace("]", "").Replace(" ", "");// value
                    addons_1_probability_addon_value_type = row.Cells[1].Value.ToString();// value

                    if (int.Parse(addons_1_id_addon_ID) > 0)
                    {
                        addItem(addons_1_id_addon_ID, addons_1_id_addon_ID, addons_1_probability_addon_value, line);
                        line++;
                    }                    
                    cont++;
                }             

               

            }

            this.Text =(porcc().ToString());
           // floatTrackBar1.Value = float.Parse(porcc().ToString());
        }

        float porcc()
        {
            float x = 0;
            foreach (DataGridViewRow row in dataGridView_item_addons.Rows)
            {
                x+= float.Parse(row.Cells[2].Value.ToString());
            }

            return x;
        }

        void addItem(string a, string b, string c, int d)
        {
            dataGridView_item_addons.Rows.Add(new object[] { a, EQUIPMENT_ADDON.GetAddon(b), float.Parse(c.Replace(".", ",")), c });
            dataGridView_item_addons.Rows[d].HeaderCell.Value = d.ToString();
        }
        private Image Recip(int Id)
        {
            Image imgg = null;
            string c = null;
            try
            {
                string value = "";
                int pos_item = 0;
                int La = 69;

                for (int i = 0; i < MainWindow.eLC.Lists[La].elementFields.Length; i++)
                {
                    if (MainWindow.eLC.Lists[La].elementFields[i] == "targets_1_id_to_make")
                    {
                        pos_item = i;

                        break;
                    }
                }
                for (int ef = 0; ef < MainWindow.eLC.Lists[La].elementValues.Length; ef++)
                {
                    //value = MainWindow.eLC.GetValue(La, ef, pos_item);
                    int a = int.Parse(MainWindow.eLC.GetValue(La, ef, 0));
                    if (Id == a)
                    {
                        string image = MainWindow.eLC.GetValue(La, ef, pos_item);

                        imgg = Img(int.Parse(MainWindow.eLC.GetValue(La, ef, pos_item)));
                        break;
                    }
                }
                return imgg;

            }
            catch
            {
                imgg = Properties.Resources.unknown;
            }

            return imgg;
        }


        private Image Img(int Id)
        {
            int[] Legal = { 3, 6, 9, 12, 15, 17, 19, 21, 22, 23, 24, 26, 27, 28, 31, 33, 35, 38, 74, 83, 86, 89, 92, 95, 96, 98, 99, 106, 107, 112, 113, 114, 115, 116, 117, 118, 119, 121, 122, 123, 124, 125, 130, 113, 134, 135, 140, 139, 141, 151, 154, 230, 218, 215, 212, 201, 200, 199, 198, 197, 186, 184, 171, 182 };
            Image imgg = null;
            try
            {
                string value = "";
                int pos_item = 0;
                int La = 0;


                for (int L = 0; L < Legal.Length; L++)
                {
                    La = Legal[L];

                    for (int i = 0; i < MainWindow.eLC.Lists[La].elementFields.Length; i++)
                    {
                        if (MainWindow.eLC.Lists[La].elementFields[i] == "Name")
                        {
                            pos_item = i;
                            break;
                        }
                    }
                    for (int ef = 0; ef < MainWindow.eLC.Lists[La].elementValues.Length; ef++)
                    {
                        value = MainWindow.eLC.GetValue(La, ef, pos_item);

                        var a = int.Parse(MainWindow.eLC.GetValue(La, ef, 0));

                        if (Id == a || value.Contains(Id.ToString()))
                        {

                            for (int k = 0; k < MainWindow.eLC.Lists[La].elementFields.Length; k++)
                            {
                                if (MainWindow.eLC.Lists[La].elementFields[k] == "file_icon")
                                {
                                    string image = MainWindow.eLC.GetValue(La, ef, k);
                                    String path = Path.GetFileName(image);
                                    if (MainWindow.database.ContainsKey(path))
                                    {
                                        imgg = MainWindow.database.images(path);
                                    }
                                    else
                                    {
                                        imgg = MainWindow.database.images("unknown.dds");
                                    }

                                }
                            }
                            break;
                        }
                    }


                }


            }
            catch (Exception)
            {

                imgg = Properties.Resources.unknown;
            }

            return imgg;
        }

        private void dataGridView_item_addons_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           // floatTrackBar1.Value = float.Parse(((DataGridView)sender).Rows[e.RowIndex].Cells[2].Value.ToString()); 
        }

        private void floatTrackBar1_Scroll(object sender, EventArgs e)
        {
            //dataGridView_item_addons.Rows[dataGridView_item_addons.CurrentCell.RowIndex].Cells[2].Value = floatTrackBar1.Value.ToString();
        }

        //List<ForLista> L()
        //{
        //    lista = new List<ForLista>();

        //    foreach (DataGridViewRow row in dgv.Rows)
        //    {

        //    }



        //        return lista;
        //}

        ////    string a = null; string b = null; string c = null; string d = null;
        ////        foreach (DataGridViewRow row in dgv.Rows)
        ////        {
        ////             a = row.Cells[0].Value.ToString();// name
        ////             b = row.Cells[1].Value.ToString();// type
        ////             c = row.Cells[2].Value.ToString();// value
        ////             d = row.Cells[3].Value.ToString();// index

        ////            dataGridView_item_addons.Rows.Add(new object[] { a  , b, Properties.Resources.unknown, decimal.Parse(c.Replace(".",",")), c, d
        ////});

        ////        }

        //List<PWHelper.Desc> ItemExtDesc;
        //List<PWHelper.ShopIcon> ShopIcons;
        //PWHelper.Elements Elem;
        //ArchiveEngine SurfacesPck;
        //Thread ConfigThread;
        //Thread SurfacesThread;
        //Thread ElementsThread;

        //private List<LBLIBRARY.PWHelper.Desc> item_ext_desc;
        //public List<PWHelper.Desc> Item_ext_desc { get => item_ext_desc; set => item_ext_desc = value; }
        //void LoadConfig()
        //{
        //    ConfigThread = new Thread(delegate ()
        //    {
        //        ArchiveEngine PckConfig = new ArchiveEngine(MainWindow.XmlData.ConfigsPckPath);
        //        ItemExtDesc = LBLIBRARY.PWHelper.LoadItemExtDesc(PckConfig);
        //        Item_ext_desc = ItemExtDesc;
        //        PckConfig.Dispose();
        //    });
        //    ConfigThread.Start();
        //}
        //public Bitmap LinkedImage;
        //public List<LBLIBRARY.PWHelper.ShopIcon> Surfaces_images;
        //void LoadSurfaces()
        //{
        //    SurfacesThread = new Thread(delegate ()
        //    {


        //        SurfacesPck = new ArchiveEngine(MainWindow.XmlData.SurfacesPckPath);
        //        ShopIcons = PWHelper.ReadSurfacesIcons(SurfacesPck);
        //        ShopIcons = ShopIcons.OrderByDescending(z => z.Name).ToList();
        //        Surfaces_images = ShopIcons;
        //        //Main_form.RefreshCurrentShopImage();
        //        LinkedImage = LBLIBRARY.PWHelper.LinkImages(ShopIcons);
        //        //Main_form.CreateIconsForm();


        //    });
        //    SurfacesThread.Start();
        //}

        //private void dataGridView_item_addons_Click(object sender, EventArgs e)
        //{

        //}

        //private void dataGridView_item_addons_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    MessageBox.Show(dataGridView_item_addons.Rows[e.RowIndex].Cells[3].Value.ToString());
        //}





        //public static List<PWHelper.Icon> LoadIconList(PWHelper.Elements el, string PckPath)
        //{
        //    ArchiveEngine pck = new ArchiveEngine(PckPath);
        //    Bitmap img = PWHelper.LoadDDSImage(((IEnumerable<byte>)pck.ReadFile(pck.PckFile, pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(d => d.Path == "surfaces\\iconset\\iconlist_ivtrm.dds")).ElementAt<PCKFileEntry>(0))).ToArray<byte>());
        //    return PWHelper.LoadIconList(el, img, PWHelper.CreateLines(pck));
        //}
    }

    class ForLista
    {
        public int ID { get; set; }
        public string TYPE { get; set; }
        public string VALUE { get; set; }
    }
}
