using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.XPath;

namespace sELedit.ups
{
    public partial class BuscaAddonsSuite : Form
    {
        public BuscaAddonsSuite()
        {
            InitializeComponent();
        }
        SortedList _add_suite;

        string URL = "https://www.pwdatabase.com/br/items/";

        public string[] BUSCA(string id)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8;
            string pagina = wc.DownloadString(URL + id);
            var Document = new HtmlAgilityPack.HtmlDocument();
            Document.OptionOutputAsXml = true;
            Document.OptionReadEncoding = true;
            Document.OptionFixNestedTags = true;
            Document.OptionDefaultStreamEncoding = Encoding.UTF8;
            Document.LoadHtml(pagina);

            string valores=null; string[] arr_valores = null;
            try
            {
                foreach (HtmlNode node in Document.DocumentNode.SelectNodes("//div[@class='addons hidden']/table"))
                {
                    var ds = node.InnerHtml;
                    var Document2 = new HtmlAgilityPack.HtmlDocument();
                    Document2.LoadHtml(ds);

                    foreach (HtmlNode node2 in Document2.DocumentNode.SelectNodes("//tr/td"))
                    {
                        var ds2 = node2.InnerHtml;

                        if (ds2.Contains("strong"))
                        {
                            var ds3 = node2.InnerText;
                            valores += HtmlAgilityPack.HtmlEntity.DeEntitize(ds3) + "\n";
                        }

                    }
                }

                arr_valores = valores.Split('\n');
                arr_valores = arr_valores.Where(arr => arr != "").ToArray();
            }
            catch (Exception)
            {

                
            }
            

            return arr_valores;
        }

        private void BuscaAddonsSuite_Load(object sender, EventArgs ex)
        {
            _add_suite = new SortedList();

            int l = 90;
            if (l != MainWindow_OLD.eLC.ConversationListIndex)
            {
                for (int e = 0; e < MainWindow_OLD.eLC.Lists[l].elementValues.Length; e++)
                {
                    listBox_list.Items.Add("["+e+"] "+MainWindow_OLD.eLC.GetValue(l, e, 0) + " - " +MainWindow_OLD.eLC.GetValue(l, e, 1) );
                }
                listBox_list.SelectedIndex = 0;
            }
        }

        private void listBox_list_SelectedIndexChanged(object sender, EventArgs ex)
        {

            int l = 90;
            int e = listBox_list.SelectedIndex;
            int scroll = dataGridView_item.FirstDisplayedScrollingRowIndex;
            dataGridView_item.SuspendLayout();
            dataGridView_item.Rows.Clear();
            int[] IdCombo = new int[11];
            try
            {
                if (l != MainWindow_OLD.eLC.ConversationListIndex)
                {
                    if (e > -1)
                    {
                       
                        for (int f = 0; f < MainWindow_OLD.eLC.Lists[l].elementValues[e].Length; f++)
                        {
                            var a = MainWindow_OLD.eLC.Lists[l].elementFields[f];
                            var b = MainWindow_OLD.eLC.Lists[l].elementTypes[f];
                            var c = MainWindow_OLD.eLC.GetValue(l, e, f);

                                dataGridView_item.Rows.Add(new string[] { a, b, c, f.ToString() });
                                dataGridView_item.Rows[f].HeaderCell.Value = f.ToString();
                               
                        }
                        IdCombo[0] = int.Parse(MainWindow_OLD.eLC.GetValue(90, e, 15));
                        IdCombo[1] = int.Parse(MainWindow_OLD.eLC.GetValue(90, e, 16));
                        IdCombo[2] = int.Parse(MainWindow_OLD.eLC.GetValue(90, e, 17));
                        IdCombo[3] = int.Parse(MainWindow_OLD.eLC.GetValue(90, e, 18));
                        IdCombo[4] = int.Parse(MainWindow_OLD.eLC.GetValue(90, e, 19));
                        IdCombo[5] = int.Parse(MainWindow_OLD.eLC.GetValue(90, e, 20));
                        IdCombo[6] = int.Parse(MainWindow_OLD.eLC.GetValue(90, e, 21));
                        IdCombo[7] = int.Parse(MainWindow_OLD.eLC.GetValue(90, e, 22));
                        IdCombo[8] = int.Parse(MainWindow_OLD.eLC.GetValue(90, e, 23));
                        IdCombo[9] = int.Parse(MainWindow_OLD.eLC.GetValue(90, e, 24));
                        IdCombo[10] = int.Parse(MainWindow_OLD.eLC.GetValue(90, e, 25));
                    }
                }
              
                if (scroll > -1)
                {
                    dataGridView_item.FirstDisplayedScrollingRowIndex = scroll;
                }
            }
            catch (Exception x) {/* MessageBox.Show(x.Message);  */         }

            var idT = int.Parse(MainWindow_OLD.eLC.GetValue(90, e, 3));
            var idCombo = IdCombo.Where(a => a != 0).ToArray();
            string[] comb = null;
            if (idT != 0)
            {
                 comb = BUSCA(MainWindow_OLD.eLC.GetValue(90, e, 3));
            }
            


            for (int i = 0; i < idCombo.Length; i++)
            {

                if (comb != null)
                {
                    
                    if (!_add_suite.ContainsKey(idCombo[i]))
                    {
                        richTextBox_adds.AppendText(idCombo[i] + " \"" + comb[i] + "\"" + "\n");
                        _add_suite.Add(idCombo[i], comb[i]);
                    }
                }
                else
                {
                    
                    if (!_add_suite.ContainsKey(idCombo[i]))
                    {
                        richTextBox_adds.AppendText(idCombo[i] + " \"" + "\"" + "\n");
                        _add_suite.Add(idCombo[i], comb[i]);
                    }
                }
            }


        }

        private void iNICIARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox_adds.Clear();
            for (int i = 0; i < listBox_list.Items.Count; i++)
            {
                listBox_list.SelectedIndex = i;
            }
            string wp = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\opt\add_suite.txt";

            if (File.Exists(wp)){File.Delete(wp);}

            StreamWriter salvar1 = new StreamWriter(Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\opt\add_suite.txt");
            salvar1.WriteLine(richTextBox_adds.Text);
            salvar1.Close();

            Close();





        }
    }
}
