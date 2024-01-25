using System;
using System.Collections;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using sELedit.configs;
using sELedit.DDSReader.Utils;
using System.Globalization;
using sELedit.NOVO;
using Newtonsoft.Json.Utilities;
using tasks;
using System.Threading.Tasks;
using sELedit.gShop;


namespace sELedit
{
    
    public partial class MainWindow_OLD : Form
    {
        [DllImport("Kernel32.dll")]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, int lpNumberOfBytesRead);
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        public static eListCollection eLC;
        public eListConversation conversationList;
        string[][] xrefs;
        private Point mouseMoveCheck;
        public bool EnableSelectionList = true;
        public bool EnableSelectionItem = true;
        string ElementsPath = "";
        public Bitmap raw_img;
        public static string[] buff_str;
        public static string[] skillstr;
        public static SortedList addonslist;
        public static SortedList InstanceList;
        public static CacheSave database = null;
        private int proctypeLocation = 0;
        private int proctypeLocationvak = 0;
        private IToolType customTooltype;
        ToolTip t = new ToolTip();
        ToolTip_addons t2 = new ToolTip_addons();
        public static SortedList LocalizationText;
        string textBox_offset = null;
        XmlSerializer deserializer;
        TextReader reader;
        object obj;
        public static Settings XmlData;
        string caminho = Path.Combine(Application.StartupPath, "Settings.xml");
        List<NOVO.ITEM> ITEM;
        public AssetManager asm;
        Thread AssetManagerLoad;
        Thread _loads;
        RECIPES rr;
        SurfacesChanger _SurfacesChanger;






        public MainWindow_OLD()
        {
          
            InitializeComponent();



            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            label_Version.Text = "Wrechid Was Here... v" + fileVersionInfo.ProductVersion;
            ReadXML();

            asm = new AssetManager();

            //Enabled = true;
            //AssetManagerLoad = new Thread(delegate () {   asm.load(cpb2);  Loads(); }); AssetManagerLoad.Start();
            cpb2.Value = 0;
            
            mouseMoveCheck = new Point(0, 0);
        }














        public class MyRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                if (database.arrTheme != null)
                {
                    if (!e.Item.Selected)
                    {
                        base.OnRenderMenuItemBackground(e);
                        e.Item.BackColor = Color.FromName(database.arrTheme[7]);
                    }
                    else
                    {
                        Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
                        Brush myBrush = new SolidBrush(Color.FromName(database.arrTheme[3]));
                        e.Graphics.FillRectangle(myBrush, rc);
                        Pen myPen = new Pen(Color.FromName(database.arrTheme[2]));
                        e.Graphics.DrawRectangle(myPen, 1, 0, rc.Width - 2, rc.Height - 1);
                        e.Item.BackColor = Color.FromName(database.arrTheme[3]);
                    }
                }
            }
            protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
            {
                if (database.arrTheme != null)
                {
                    Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
                    Brush myBrush = new SolidBrush(Color.FromName(database.arrTheme[3]));
                    e.Graphics.FillRectangle(myBrush, rc);
                    Pen myPen = new Pen(Color.FromName(database.arrTheme[2]));
                    e.Graphics.DrawRectangle(myPen, 1, 0, rc.Width - 2, rc.Height - 1);
                    e.Item.BackColor = Color.FromName(database.arrTheme[3]);
                }
            }
            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                if (database.arrTheme != null)
                {
                    base.OnRenderItemText(e);
                    if (!e.Item.Selected)
                    {
                        e.Item.ForeColor = Color.FromName(database.arrTheme[4]);
                    }
                    else
                    {
                        e.Item.ForeColor = Color.FromName(database.arrTheme[5]);
                    }
                }
            }
        }

        private void comboBoxDb_DrawItem(object sender, DrawItemEventArgs e)
        {
            //Image img = Properties.Resources._32x32_Question.ToBitmap();
            //if (e.Index < 0) return;

            //e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            //e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;


            //e.Graphics.DrawImage(img,new Rectangle(e.Bounds.Location,new Size(e.Bounds.Height - 2, e.Bounds.Height - 2)));


        }
        
        private void click_load(object sender, EventArgs e)
        {
            OpenFileDialog eLoad = new OpenFileDialog();
            eLoad.Filter = "Elements File (*.data)|*.data|All Files (*.*)|*.*";
            if (eLoad.ShowDialog() == DialogResult.OK && File.Exists(eLoad.FileName))
            {
                try
                {
                    Cursor = Cursors.AppStarting;
                    //progressBar_progress.Style = ProgressBarStyle.Continuous;

                    eLC = new eListCollection(eLoad.FileName, ref cpb2);

                    this.exportContainerToolStripMenuItem.DropDownItems.Clear();

                    // search for available export rules
                    if (eLC.ConfigFile != null)
                    {
                        this.exportContainerToolStripMenuItem.DropDownItems.Add(new ToolStripLabel("Select a valid Conversation Rules Set"));
                        this.exportContainerToolStripMenuItem.DropDownItems[0].Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        this.exportContainerToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
                        string[] files = Directory.GetFiles(Application.StartupPath + "\\rules", "PW_v" + eLC.Version.ToString() + "*.rules");
                        for (int i = 0; i < files.Length; i++)
                        {
                            files[i] = files[i].Replace("=", "=>");
                            files[i] = files[i].Replace(".rules", "");
                            files[i] = files[i].Replace(Application.StartupPath + "\\rules\\", "");
                            this.exportContainerToolStripMenuItem.DropDownItems.Add(files[i], null, new EventHandler(this.click_export));
                        }
                    }
                    // load cross references list
                    if (eLC.ConfigFile != null)//Вроде работает
                    {
                        string[] referencefiles = Directory.GetFiles(Application.StartupPath + "\\configs", "references.txt");
                        if (referencefiles.Length > 0)
                        {
                            StreamReader sr = new StreamReader(referencefiles[0]);
                            char[] chars = { ';', ',' };
                            string[] x;
                            xrefs = new string[eLC.Lists.Length][];
                            string line;
                            while (!sr.EndOfStream)
                            {
                                line = sr.ReadLine();
                                if (!line.StartsWith("#"))
                                {
                                    x = line.Split(chars);
                                    if (int.Parse(x[0]) < eLC.Lists.Length)
                                    {
                                        xrefs[int.Parse(x[0])] = x;
                                    }
                                }
                            }
                            this.toolStripSeparator6.Visible = true;
                            //this.xrefItemToolStripMenuItem.Visible = true;
                        }
                    }

                    if (eLC.ConversationListIndex > -1 && eLC.Lists.Length > eLC.ConversationListIndex)
                    {
                        conversationList = new eListConversation((byte[])eLC.Lists[eLC.ConversationListIndex].elementValues[0][0]);
                    }

                    dataGridView_item.Rows.Clear();
                    comboBox_lists.Items.Clear();
                    for (int l = 0; l < eLC.Lists.Length; l++)
                    {
                        comboBox_lists.Items.Add("[" + l + "] " + eLC.Lists[l].listName.Split(new string[] { " - " }, StringSplitOptions.None)[1] + " (" + eLC.Lists[l].elementValues.Length + ")");
                    }
                    string timestamp = "";
                    if (eLC.Lists[0].listOffset.Length > 0)
                        timestamp = ", Timestamp: " + timestamp_to_string(BitConverter.ToUInt32(eLC.Lists[0].listOffset, 0));
                    this.Text = " sELedit++ 2.0(" + eLoad.FileName + " [Version: " + eLC.Version.ToString() + timestamp + "])";
                    ElementsPath = eLoad.FileName;

                    cpb2.Value = 0;
                    //progressBar_progress.Style = ProgressBarStyle.Continuous;

                    comboBox_lists.SelectedIndex = 0;

                    Cursor = Cursors.Default;
                }
                catch
                {
                    //MessageBox.Show(eListCollection.SStat[0].ToString() + "\n" + eListCollection.SStat[1].ToString() + "\n" + eListCollection.SStat[2].ToString());
                    MessageBox.Show("LOADING ERROR!\n\nThis error usually occurs if incorrect configuration, structure, or encrypted elements.data file...\nIf you are using elements.list.count trying to decrypt, its likely the last list item count is incorrect... \nUse details below to assist... \n\nRead Failed at this point :\n" + eListCollection.SStat[0].ToString() + " - List #\n" + eListCollection.SStat[1].ToString() + " - # Items This List\n" + eListCollection.SStat[2].ToString() + " - Item ID");
                    //progressBar_progress.Style = ProgressBarStyle.Continuous;
                    Cursor = Cursors.Default;
                }
            }
        }

        private void click_save(object sender, EventArgs e)
        {

            SaveFileDialog eSave = new SaveFileDialog();
            eSave.InitialDirectory = Environment.CurrentDirectory;
            eSave.Filter = "Elements File (*.data)|*.data|All Files (*.*)|*.*";
            if (eSave.ShowDialog() == DialogResult.OK && eSave.FileName != "")
            {
                try
                {
                    Cursor = Cursors.AppStarting;
                    //progressBar_progress.Style = ProgressBarStyle.Marquee;
                    //File.Copy(eSave.FileName, eSave.FileName + ".bak", true);
                    if (eLC.ConversationListIndex > -1 && eLC.Lists.Length > eLC.ConversationListIndex)
                    {
                        eLC.Lists[eLC.ConversationListIndex].elementValues[0][0] = conversationList.GetBytes();
                    }
                    eLC.Save(eSave.FileName);
                    //progressBar_progress.Style = ProgressBarStyle.Continuous;
                    Cursor = Cursors.Default;
                }
                catch
                {
                    MessageBox.Show("SAVING ERROR!\nThis error mostly occurs of configuration and elements.data mismatch");
                    //progressBar_progress.Style = ProgressBarStyle.Continuous;
                    Cursor = Cursors.Default;
                }
            }
        }

        //private void click_save2(object sender, EventArgs e)
        //{
        //    if (ElementsPath != "" && File.Exists(ElementsPath))
        //    {
        //        try
        //        {
        //            Cursor = Cursors.AppStarting;
        //            //progressBar_progress.Style = ProgressBarStyle.Marquee;
        //            File.Copy(ElementsPath, ElementsPath + ".bak", true);
        //            if (eLC.ConversationListIndex > -1 && eLC.Lists.Length > eLC.ConversationListIndex)
        //            {
        //                eLC.Lists[eLC.ConversationListIndex].elementValues[0][0] = conversationList.GetBytes();
        //            }
        //            eLC.Save(ElementsPath);
        //            //progressBar_progress.Style = ProgressBarStyle.Continuous;
        //            Cursor = Cursors.Default;
        //        }
        //        catch
        //        {
        //            MessageBox.Show("SAVING ERROR!\nThis error mostly occurs of configuration and elements.data mismatch");
        //            //progressBar_progress.Style = ProgressBarStyle.Continuous;
        //            Cursor = Cursors.Default;
        //        }
        //    }
        //}

        private void click_export(object sender, EventArgs e)
        {
            SaveFileDialog eSave = new SaveFileDialog();
            eSave.Filter = "Elements File (*.data)|*.data|All Files (*.*)|*.*";
            if (eSave.ShowDialog() == DialogResult.OK && eSave.FileName != "")
            {
                try
                {
                    int start = ((ToolStripMenuItem)sender).Text.IndexOf(" ==> ") + 5;

                    Cursor = Cursors.WaitCursor;
                    //progressBar_progress.Style = ProgressBarStyle.Marquee;
                    string rulesFile = Application.StartupPath + "\\rules\\" + ((ToolStripMenuItem)sender).Text.Replace("=>", "=") + ".rules";
                    eLC.Export(rulesFile, eSave.FileName);
                    //progressBar_progress.Style = ProgressBarStyle.Continuous;
                    Cursor = Cursors.Default;
                }
                catch
                {
                    MessageBox.Show("EXPORTING ERROR!\nThis error mostly occurs if selected rules fileset is invalid");
                    //progressBar_progress.Style = ProgressBarStyle.Continuous;
                    Cursor = Cursors.Default;
                }
            }
        }





        private void change_offset(object sender, EventArgs e)
        {
            eLC.SetOffset(comboBox_lists.SelectedIndex, textBox_offset);
        }

        private void change_value(object sender, DataGridViewCellEventArgs ea)
        {

        }

        private void click_search(object sender, EventArgs ea)
        {
            string id = textBox_search.Text;
            if (!checkBox_SearchMatchCase.Checked)
                id = id.ToLower();
            string value = "";
            int l = comboBox_lists.SelectedIndex;
            if (l < 0) { l = 0; }
            int f = 0;
            if (dataGridView_item.CurrentCell != null)
                f = dataGridView_item.CurrentCell.RowIndex + 1;
            if (f < 0) { f = 0; }
            if (eLC != null && eLC.Lists != null)
            {
                EnableSelectionItem = false;
                int ftmp = f;
                if (checkBox_SearchAll.Checked)
                {
                    int e = dataGridView_elems.CurrentCell.RowIndex;
                    if (e < 0) { e = 0; }
                    int etmp = e;
                    for (int lf = l; lf < eLC.Lists.Length; lf++)
                    {
                        for (int ef = etmp; ef < eLC.Lists[lf].elementValues.Length; ef++)
                        {
                            for (int ff = ftmp; ff < eLC.Lists[lf].elementFields.Length; ff++)
                            {
                                if (checkBox_SearchExactMatching.Checked)
                                {
                                    if (eLC.GetValue(lf, ef, ff) == id)
                                    {
                                        comboBox_lists.SelectedIndex = lf;
                                        dataGridView_elems.ClearSelection();
                                        EnableSelectionItem = true;
                                        dataGridView_elems.CurrentCell = dataGridView_elems[0, ef];
                                        dataGridView_elems.Rows[ef].Selected = true;
                                        dataGridView_elems.FirstDisplayedScrollingRowIndex = ef;
                                        dataGridView_item.CurrentCell = dataGridView_item.Rows[ff].Cells[2];
                                        return;
                                    }
                                }
                                else
                                {
                                    value = eLC.GetValue(lf, ef, ff);
                                    if (!checkBox_SearchMatchCase.Checked)
                                        value = value.ToLower();
                                    if (value.Contains(id))
                                    {
                                        comboBox_lists.SelectedIndex = lf;
                                        dataGridView_elems.ClearSelection();
                                        EnableSelectionItem = true;
                                        dataGridView_elems.CurrentCell = dataGridView_elems[0, ef];
                                        dataGridView_elems.Rows[ef].Selected = true;
                                        dataGridView_elems.FirstDisplayedScrollingRowIndex = ef;
                                        dataGridView_item.CurrentCell = dataGridView_item.Rows[ff].Cells[2];
                                        return;
                                    }
                                }
                            }
                            ftmp = 0;
                        }
                        etmp = 0;
                    }
                    etmp = e;
                    ftmp = f;
                    for (int lf = 0; lf < eLC.Lists.Length && lf <= l; lf++)
                    {
                        for (int ef = 0; ef < eLC.Lists[lf].elementValues.Length; ef++)
                        {
                            for (int ff = 0; ff < eLC.Lists[lf].elementFields.Length; ff++)
                            {
                                if (checkBox_SearchExactMatching.Checked)
                                {
                                    if (eLC.GetValue(lf, ef, ff) == id)
                                    {
                                        comboBox_lists.SelectedIndex = lf;
                                        dataGridView_elems.ClearSelection();
                                        EnableSelectionItem = true;
                                        dataGridView_elems.CurrentCell = dataGridView_elems[0, ef];
                                        dataGridView_elems.Rows[ef].Selected = true;
                                        dataGridView_elems.FirstDisplayedScrollingRowIndex = ef;
                                        dataGridView_item.CurrentCell = dataGridView_item.Rows[ff].Cells[2];
                                        return;
                                    }
                                }
                                else
                                {
                                    value = eLC.GetValue(lf, ef, ff);
                                    if (!checkBox_SearchMatchCase.Checked)
                                        value = value.ToLower();
                                    if (value.Contains(id))
                                    {
                                        comboBox_lists.SelectedIndex = lf;
                                        dataGridView_elems.ClearSelection();
                                        EnableSelectionItem = true;
                                        dataGridView_elems.CurrentCell = dataGridView_elems[0, ef];
                                        dataGridView_elems.Rows[ef].Selected = true;
                                        dataGridView_elems.FirstDisplayedScrollingRowIndex = ef;
                                        dataGridView_item.CurrentCell = dataGridView_item.Rows[ff].Cells[2];
                                        return;
                                    }
                                }
                            }
                            ftmp = 0;
                        }
                        etmp = 0;
                    }
                }
                else
                {
                    int e = dataGridView_elems.CurrentCell.RowIndex + 1;
                    if (e < 0) { e = 0; }
                    int etmp = e;
                    for (int lf = l; lf < eLC.Lists.Length; lf++)
                    {
                        int pos = 0;
                        for (int i = 0; i < eLC.Lists[lf].elementFields.Length; i++)
                        {
                            if (eLC.Lists[lf].elementFields[i] == "Name")
                            {
                                pos = i;
                                break;
                            }
                        }
                        for (int ef = etmp; ef < eLC.Lists[lf].elementValues.Length; ef++)
                        {
                            if (checkBox_SearchExactMatching.Checked)
                            {
                                if (id == eLC.GetValue(lf, ef, 0) || eLC.GetValue(lf, ef, pos) == id)
                                {
                                    comboBox_lists.SelectedIndex = lf;
                                    dataGridView_elems.ClearSelection();
                                    EnableSelectionItem = true;
                                    dataGridView_elems.CurrentCell = dataGridView_elems[0, ef];
                                    dataGridView_elems.Rows[ef].Selected = true;
                                    dataGridView_elems.FirstDisplayedScrollingRowIndex = ef;
                                    return;
                                }
                            }
                            else
                            {
                                value = eLC.GetValue(lf, ef, pos);
                                if (!checkBox_SearchMatchCase.Checked)
                                    value = value.ToLower();
                                if (id == eLC.GetValue(lf, ef, 0) || value.Contains(id))
                                {
                                    comboBox_lists.SelectedIndex = lf;
                                    dataGridView_elems.ClearSelection();
                                    EnableSelectionItem = true;
                                    dataGridView_elems.CurrentCell = dataGridView_elems[0, ef];
                                    dataGridView_elems.Rows[ef].Selected = true;
                                    dataGridView_elems.FirstDisplayedScrollingRowIndex = ef;
                                    return;
                                }
                            }
                        }
                        etmp = 0;
                    }
                    etmp = e;
                    for (int lf = 0; lf < eLC.Lists.Length && lf <= l; lf++)
                    {
                        int pos = 0;
                        for (int i = 0; i < eLC.Lists[lf].elementFields.Length; i++)
                        {
                            if (eLC.Lists[lf].elementFields[i] == "Name")
                            {
                                pos = i;
                                break;
                            }
                        }
                        for (int ef = 0; ef < eLC.Lists[lf].elementValues.Length; ef++)
                        {
                            if (checkBox_SearchExactMatching.Checked)
                            {
                                if (id == eLC.GetValue(lf, ef, 0) || eLC.GetValue(lf, ef, pos) == id)
                                {
                                    comboBox_lists.SelectedIndex = lf;
                                    dataGridView_elems.ClearSelection();
                                    EnableSelectionItem = true;
                                    dataGridView_elems.CurrentCell = dataGridView_elems[0, ef];
                                    dataGridView_elems.Rows[ef].Selected = true;
                                    dataGridView_elems.FirstDisplayedScrollingRowIndex = ef;
                                    return;
                                }
                            }
                            else
                            {
                                value = eLC.GetValue(lf, ef, pos);
                                if (!checkBox_SearchMatchCase.Checked)
                                    value = value.ToLower();
                                if (id == eLC.GetValue(lf, ef, 0) || value.Contains(id))
                                {
                                    comboBox_lists.SelectedIndex = lf;
                                    dataGridView_elems.ClearSelection();
                                    EnableSelectionItem = true;
                                    dataGridView_elems.CurrentCell = dataGridView_elems[0, ef];
                                    dataGridView_elems.Rows[ef].Selected = true;
                                    dataGridView_elems.FirstDisplayedScrollingRowIndex = ef;
                                    return;
                                }
                            }
                        }
                        etmp = 0;
                    }
                }
                EnableSelectionItem = true;
                MessageBox.Show("Search reached End without Result!");
            }
        }

        private void click_deleteItem(object sender, EventArgs ea)
        {
            int l = comboBox_lists.SelectedIndex;
            int[] selIndices = gridSelectedIndices(dataGridView_elems);
            if (dataGridView_elems.RowCount > 0 && selIndices.Length > 0)
            {
                if (selIndices.Length < dataGridView_elems.RowCount)
                {
                    if (l != eLC.ConversationListIndex)
                    {
                        EnableSelectionList = false;
                        EnableSelectionItem = false;
                        for (int i = selIndices.Length - 1; i > -1; i--)
                        {
                            eLC.Lists[l].RemoveItem(selIndices[i]);
                            dataGridView_elems.Rows.RemoveAt(selIndices[i]);
                        }
                        comboBox_lists.Items[l] = "[" + l + "]: " + eLC.Lists[l].listName + " (" + eLC.Lists[l].elementValues.Length + ")";
                        EnableSelectionList = true;
                        EnableSelectionItem = true;
                        change_item(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Operation not supported in List " + eLC.ConversationListIndex.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Cannot delete all items in list!");
                }
            }
        }

        private void click_cloneItem(object sender, EventArgs ea)
        {
            int l = comboBox_lists.SelectedIndex;
            int IDITEM;

            if (dataGridView_elems.RowCount > 0)
            {
                if (l != eLC.ConversationListIndex)
                {
                    int[] selIndices = gridSelectedIndices(dataGridView_elems);
                    EnableSelectionList = false;
                    EnableSelectionItem = false;
                    int NewSelectedCount = 0;

                    IDITEM = int.Parse(dataGridView_elems.Rows[dataGridView_elems.RowCount - 1].Cells[0].Value.ToString());
                    for (int i = 0; i < selIndices.Length; i++)
                    {
                        object[] o = new object[eLC.Lists[l].elementValues[selIndices[i]].Length];
                        eLC.Lists[l].elementValues[selIndices[i]].CopyTo(o, 0);
                        o[0] = IDITEM + 1;


                        eLC.Lists[l].AddItem(o);
                        dataGridView_elems.Rows.Add(new object[]
                        {
                            dataGridView_elems.Rows[selIndices[i]].Cells[0].Value,
                            dataGridView_elems.Rows[selIndices[i]].Cells[1].Value,
                            dataGridView_elems.Rows[selIndices[i]].Cells[2].Value
                        });
                        NewSelectedCount++;
                    }
                    //change_list(sender, ea);
                    comboBox_lists.Items[l] = "[" + l + "]: " + eLC.Lists[l].listName + " (" + eLC.Lists[l].elementValues.Length + ")";
                    dataGridView_elems.ClearSelection();
                    for (int i = NewSelectedCount; i > 0; i--)
                    {
                        dataGridView_elems.Rows[dataGridView_elems.RowCount - i].Selected = true;
                        dataGridView_elems.FirstDisplayedScrollingRowIndex = dataGridView_elems.RowCount - 1;
                    }
                    EnableSelectionList = true;
                    EnableSelectionItem = true;
                    change_item(null, null);



                }
                else
                {
                    MessageBox.Show("Operation not supported in List " + eLC.ConversationListIndex.ToString());
                }
            }
        }


        private void click_logicReplace(object sender, EventArgs e)
        {
            if (eLC != null)
            {
                (new ReplaceWindow(eLC)).ShowDialog();
                int itemIndex = dataGridView_elems.CurrentCell.RowIndex;
                change_list(null, null);
                dataGridView_elems.Rows[itemIndex].Selected = true;
            }
            else
            {
                MessageBox.Show("No File Loaded!");
            }
        }

        private void click_fieldReplace(object sender, EventArgs e)
        {
            if (eLC != null)
            {
                (new FieldReplaceWindow(eLC, conversationList, ref cpb2)).ShowDialog();
            }
            else
            {
                MessageBox.Show("No File Loaded!");
            }
        }

        private void click_info(object sender, EventArgs e)
        {
            if (eLC != null)
            {
                //(gcnew InfoWindow(eLC))->ShowDialog();
            }
            else
            {
                MessageBox.Show("No File Loaded!");
            }
        }

        private void click_version(object sender, EventArgs e)
        {
            OpenFileDialog eLoad = new OpenFileDialog();
            eLoad.Filter = "Elements File (*.data)|*.data|All Files (*.*)|*.*";
            if (eLoad.ShowDialog() == DialogResult.OK && File.Exists(eLoad.FileName))
            {
                FileStream fs = File.OpenRead(eLoad.FileName);
                BinaryReader br = new BinaryReader(fs);
                short version = br.ReadInt16();
                short signature = br.ReadInt16();
                int timestamp = 0;
                string stimestamp = "";
                if (version >= 10)
                    timestamp = br.ReadInt32();
                if (timestamp != 0)
                    stimestamp = "\nTimestamp: " + timestamp_to_string((uint)timestamp);
                br.Close();
                fs.Close();

                MessageBox.Show("File: " + eLoad.FileName + "\n\nVersion: " + version.ToString() + "\nSignature: " + signature.ToString() + stimestamp);
            }
            else
            {
                //MessageBox::Show("No File!");
            }
        }

        private void click_config(object sender, EventArgs e)
        {
            (new ConfigWindow()).Show();
        }

        private void click_exportItem(object sender, EventArgs ea)
        {
            if (dataGridView_elems.RowCount > 0)
            {
                int l = comboBox_lists.SelectedIndex;
                int[] selIndices = gridSelectedIndices(dataGridView_elems);
                if (l != eLC.ConversationListIndex)
                {
                    if (dataGridView_elems.RowCount > 0 && selIndices.Length > 0)
                    {
                        FolderBrowserDialog eSave = new FolderBrowserDialog();
                        if (eSave.ShowDialog() == DialogResult.OK && Directory.Exists(eSave.SelectedPath))
                        {
                            try
                            {
                                Cursor = Cursors.AppStarting;
                                //progressBar_progress.Style = ProgressBarStyle.Continuous;
                                cpb2.Maximum = selIndices.Length;
                                for (int i = 0; i < selIndices.Length; i++)
                                {
                                    eLC.Lists[l].ExportItem(eSave.SelectedPath + "\\" + selIndices[i], selIndices[i]);
                                    cpb2.Value++;
                                }
                                Cursor = Cursors.Default;
                                MessageBox.Show("Export complete!");
                            }
                            catch
                            {
                                MessageBox.Show("EXPORT ERROR!\nExporting item to unicode text file failed!");
                                Cursor = Cursors.Default;
                            }
                            cpb2.Value = 0;
                            //progressBar_progress.Style = ProgressBarStyle.Continuous;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Operation not supported in List " + eLC.ConversationListIndex.ToString());
                }
            }
        }

        private void click_importItem(object sender, EventArgs ea)
        {
            int l = comboBox_lists.SelectedIndex;
            int e = dataGridView_elems.CurrentRow.Index;
            if (l != eLC.ConversationListIndex)
            {
                if (l > -1 && e > -1)
                {
                    OpenFileDialog eLoad = new OpenFileDialog();
                    eLoad.Filter = "All Files (*.*)|*.*";
                    if (eLoad.ShowDialog() == DialogResult.OK && File.Exists(eLoad.FileName))
                    {
                        try
                        {
                            Cursor = Cursors.AppStarting;
                            eLC.Lists[l].ImportItem(eLoad.FileName, e);
                            change_list(null, null);
                            dataGridView_elems.Rows[e].Selected = true;
                            Cursor = Cursors.Default;
                        }
                        catch
                        {
                            MessageBox.Show("IMPORT ERROR!\nCheck if the item version matches the elements.data version and is imported to the correct list!");
                            Cursor = Cursors.Default;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Operation not supported in List " + eLC.ConversationListIndex.ToString());
            }
        }

        private void click_addItems(object sender, EventArgs ea)
        {
            int l = comboBox_lists.SelectedIndex;
            if (dataGridView_elems.RowCount >= 1)
            {
                if (l != eLC.ConversationListIndex)
                {
                    string[] fileNames = null;
                    OpenFileDialog eLoad = new OpenFileDialog();
                    eLoad.Filter = "All Files (*.*)|*.*";
                    eLoad.Multiselect = true;
                    if (eLoad.ShowDialog() == DialogResult.OK && File.Exists(eLoad.FileName))
                    {
                        EnableSelectionList = false;
                        EnableSelectionItem = false;
                        fileNames = eLoad.FileNames;
                        try
                        {
                            Cursor = Cursors.AppStarting;
                            //progressBar_progress.Style = ProgressBarStyle.Continuous;
                            cpb2.Maximum = fileNames.Length;
                            int pos = -1;
                            int pos2 = -1;
                            for (int i = 0; i < eLC.Lists[l].elementFields.Length; i++)
                            {
                                if (eLC.Lists[l].elementFields[i] == "Name")
                                {
                                    pos = i;
                                }
                                if (eLC.Lists[l].elementFields[i] == "file_icon" || eLC.Lists[l].elementFields[i] == "file_icon1")
                                {
                                    pos2 = i;
                                }
                                if (pos != -1 && pos2 != -1)
                                {
                                    break;
                                }
                            }
                            for (int i = 0; i < fileNames.Length; i++)
                            {
                                int e = dataGridView_elems.RowCount - 1;
                                object[] o = new object[eLC.Lists[l].elementValues[e].Length];
                                eLC.Lists[l].elementValues[e].CopyTo(o, 0);
                                eLC.Lists[l].AddItem(o);
                                eLC.Lists[l].ImportItem(fileNames[i], e + 1);
                                if (eLC.Lists[l].elementFields[0] == "ID")
                                {
                                    Bitmap img = Properties.Resources.blank;
                                    string path = Path.GetFileName(eLC.GetValue(l, e, pos2));
                                    if (database.sourceBitmap != null && database.ContainsKey(path))
                                    {
                                        if (database.ContainsKey(path))
                                        {
                                            img = database.images(path);
                                        }
                                    }
                                    dataGridView_elems.Rows.Add(new object[] { eLC.GetValue(l, e, 0), img, eLC.GetValue(l, e, pos) });
                                }
                                else
                                {
                                    dataGridView_elems.Rows.Add(new object[] { 0, Properties.Resources.unknown, eLC.GetValue(l, e, pos) });
                                }
                                cpb2.Value++;
                            }
                            Cursor = Cursors.Default;
                        }
                        catch
                        {
                            MessageBox.Show("IMPORT ERROR!\nCheck if the item version matches the elements.data version and is imported to the correct list!");
                            Cursor = Cursors.Default;
                        }
                        comboBox_lists.Items[l] = "[" + l + "]: " + eLC.Lists[l].listName + " (" + eLC.Lists[l].elementValues.Length + ")";
                        cpb2.Value = 0;
                        //progressBar_progress.Style = ProgressBarStyle.Continuous;
                        dataGridView_elems.ClearSelection();
                        EnableSelectionList = true;
                        EnableSelectionItem = true;
                        dataGridView_elems.Rows[dataGridView_elems.RowCount - 1].Selected = true;
                        dataGridView_elems.FirstDisplayedScrollingRowIndex = dataGridView_elems.RowCount - 1;
                        change_item(null, null);
                    }
                }
                else
                {
                    MessageBox.Show("Operation not supported in List " + eLC.ConversationListIndex.ToString());
                }
            }
        }



        private void click_npcExport(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.InitialDirectory = Environment.CurrentDirectory;
            save.Filter = "Text File (*.txt)|*.txt|All Files (*.*)|*.*";
            if (save.ShowDialog() == DialogResult.OK && save.FileName != "")
            {
                try
                {
                    Cursor = Cursors.AppStarting;
                    //progressBar_progress.Style = ProgressBarStyle.Marquee;

                    StreamWriter sw = new StreamWriter(save.FileName, false, Encoding.Unicode);

                    for (int i = 0; i < eLC.Lists[38].elementValues.Length; i++)
                    {
                        sw.WriteLine(eLC.GetValue(38, i, 0) + "\t" + eLC.GetValue(38, i, 2));
                    }

                    for (int i = 0; i < eLC.Lists[57].elementValues.Length; i++)
                    {
                        sw.WriteLine(eLC.GetValue(57, i, 0) + "\t" + eLC.GetValue(57, i, 1));
                    }

                    sw.Close();

                    //progressBar_progress.Style = ProgressBarStyle.Continuous;
                    Cursor = Cursors.Default;
                }
                catch
                {
                    MessageBox.Show("SAVING ERROR!");
                    //progressBar_progress.Style = ProgressBarStyle.Continuous;
                    Cursor = Cursors.Default;
                }
            }
        }

        private void click_joinEL(object sender, EventArgs e)
        {
            JoinWindow eJoin = new JoinWindow();
            if (eJoin.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(eJoin.FileName))
                {
                    if (eJoin.LogDirectory == "" || !Directory.Exists(eJoin.LogDirectory))
                    {
                        eJoin.LogDirectory = eJoin.FileName + ".JOIN";
                        Directory.CreateDirectory(eJoin.LogDirectory);
                    }

                    if (eJoin.BackupNew)
                    {
                        Directory.CreateDirectory(eJoin.LogDirectory + "\\added.backup");
                    }
                    if (eJoin.BackupChanged)
                    {
                        Directory.CreateDirectory(eJoin.LogDirectory + "\\replaced.backup");
                    }
                    if (eJoin.BackupMissing)
                    {
                        Directory.CreateDirectory(eJoin.LogDirectory + "\\removed.backup");
                    }

                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        //progressBar_progress.Style = ProgressBarStyle.Continuous;
                        eListCollection neLC = new eListCollection(eJoin.FileName, ref cpb2);
                        if (eLC.ConfigFile != neLC.ConfigFile)
                        {
                            MessageBox.Show("You're going to join two different element.data versions. The merged file will become invalid!", " WARNING");
                        }
                        if (eLC.ConversationListIndex > -1 && neLC.Lists.Length > eLC.ConversationListIndex)
                        {
                            conversationList = new eListConversation((byte[])neLC.Lists[eLC.ConversationListIndex].elementValues[0][0]);
                        }
                        StreamWriter sw = new StreamWriter(eJoin.LogDirectory + "\\LOG.TXT", false, Encoding.Unicode);

                        ArrayList report;
                        for (int l = 0; l < eLC.Lists.Length; l++)
                        {
                            if (l != eLC.ConversationListIndex)
                            {
                                report = eLC.Lists[l].JoinElements(neLC.Lists[l], l, eJoin.AddNew, eJoin.BackupNew, eJoin.ReplaceChanged, eJoin.BackupChanged, eJoin.RemoveMissing, eJoin.BackupMissing, eJoin.LogDirectory + "\\added.backup", eJoin.LogDirectory + "\\replaced.backup", eJoin.LogDirectory + "\\removed.backup");
                                report.Sort();
                                if (report.Count > 0)
                                {
                                    sw.WriteLine("List " + l + ": " + report.Count + " Item(s) Affected");
                                    sw.WriteLine();

                                    for (int n = 0; n < report.Count; n++)
                                    {
                                        sw.WriteLine((string)report[n]);
                                    }

                                    sw.WriteLine();
                                }

                                comboBox_lists.Items[l] = "[" + l + "]: " + eLC.Lists[l].listName + " (" + eLC.Lists[l].elementValues.Length + ")";
                            }
                        }

                        sw.Close();

                        if (comboBox_lists.SelectedIndex > -1)
                        {
                            change_list(null, null);
                        }

                        cpb2.Value = 0;
                        //cpb2.Style = ProgressBarStyle.Continuous;
                        Cursor = Cursors.Default;
                    }
                    catch
                    {
                        MessageBox.Show("LOADING ERROR!\nThis error mostly occurs of configuration and elements.data mismatch");
                        //progressBar_progress.Style = ProgressBarStyle.Continuous;
                        Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void click_npcAIexport(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.InitialDirectory = Environment.CurrentDirectory;
            save.Filter = "Text File (*.txt)|*.txt|All Files (*.*)|*.*";
            if (save.ShowDialog() == DialogResult.OK && save.FileName != "")
            {
                try
                {
                    Cursor = Cursors.AppStarting;
                    //progressBar_progress.Style = ProgressBarStyle.Marquee;

                    StreamWriter sw = new StreamWriter(save.FileName, false, Encoding.Unicode);

                    for (int i = 0; i < eLC.Lists[38].elementValues.Length; i++)
                    {
                        sw.WriteLine(eLC.GetValue(38, i, 0) + "\t" + eLC.GetValue(38, i, 2) + "\t" + eLC.GetValue(38, i, 64));
                    }

                    sw.Close();

                    //progressBar_progress.Style = ProgressBarStyle.Continuous;
                    Cursor = Cursors.Default;
                }
                catch
                {
                    MessageBox.Show("SAVING ERROR!");
                    //progressBar_progress.Style = ProgressBarStyle.Continuous;
                    Cursor = Cursors.Default;
                }
            }
        }

        private void click_skillValidate(object sender, EventArgs e)
        {
            if (eLC != null)
            {
                ArrayList mobSkills = new ArrayList();

                string skill;

                // check all monster skills (list 38 fields 119, 121, 123, 125, 127, 129)
                for (int n = 0; n < eLC.Lists[38].elementValues.Length; n++)
                {
                    for (int f = 119; f < 130; f += 2)
                    {
                        skill = eLC.GetValue(38, n, f);

                        if (Convert.ToInt32(skill) > 846)
                        {
                            mobSkills.Add("Invalid Skill: " + skill + " (Monster: " + eLC.GetValue(38, n, 0) + ")");
                        }
                    }
                }

                if (mobSkills.Count == 0)
                {
                    MessageBox.Show("OK, no invalid skills found!");
                }
                else
                {
                    string message = "";
                    for (int i = 0; i < mobSkills.Count; i++)
                    {
                        message += (string)mobSkills[i] + "\r\n";
                    }
                    new DebugWindow("Invalid Skills", message);
                }
            }
        }

        private void click_propertyValidate(object sender, EventArgs e)
        {
            if (eLC != null)
            {
                ArrayList properties = new ArrayList();

                string attribute;

                // weapons (list 3, fields 43-201, +=2)
                for (int n = 0; n < eLC.Lists[3].elementValues.Length; n++)
                {
                    for (int f = 43; f < 202; f += 2)
                    {
                        attribute = eLC.GetValue(3, n, f);

                        if (Convert.ToInt32(attribute) > 1909)
                        {
                            properties.Add("Invalid Property: " + attribute + " (Weapon: " + eLC.GetValue(3, n, 0) + ")");
                        }
                    }
                }

                // armors (list 6, fields 55-179, +=2)
                for (int n = 0; n < eLC.Lists[6].elementValues.Length; n++)
                {
                    for (int f = 55; f < 180; f += 2)
                    {
                        attribute = eLC.GetValue(6, n, f);

                        if (Convert.ToInt32(attribute) > 1909)
                        {
                            properties.Add("Invalid Property: " + attribute + " (Armor: " + eLC.GetValue(6, n, 0) + ")");
                        }
                    }
                }

                // ornaments (list 9, fields 44-160, +=2)
                for (int n = 0; n < eLC.Lists[9].elementValues.Length; n++)
                {
                    for (int f = 44; f < 161; f += 2)
                    {
                        attribute = eLC.GetValue(9, n, f);

                        if (Convert.ToInt32(attribute) > 1909)
                        {
                            properties.Add("Invalid Property: " + attribute + " (Ornament: " + eLC.GetValue(9, n, 0) + ")");
                        }
                    }
                }

                // soulgems (list 35, fields 11-12, +=1)
                for (int n = 0; n < eLC.Lists[35].elementValues.Length; n++)
                {
                    for (int f = 11; f < 13; f++)
                    {
                        attribute = eLC.GetValue(35, n, f);

                        if (Convert.ToInt32(attribute) > 1909)
                        {
                            properties.Add("Invalid Property: " + attribute + " (Soulgem: " + eLC.GetValue(35, n, 0) + ")");
                        }
                    }
                }

                // complect boni (list 90, fields 15-19, +=1)
                for (int n = 0; n < eLC.Lists[90].elementValues.Length; n++)
                {
                    for (int f = 15; f < 20; f++)
                    {
                        attribute = eLC.GetValue(90, n, f);

                        if (Convert.ToInt32(attribute) > 1909)
                        {
                            properties.Add("Invalid Property: " + attribute + " (Complect Bonus: " + eLC.GetValue(90, n, 0) + ")");
                        }
                    }
                }

                if (properties.Count == 0)
                {
                    MessageBox.Show("OK, no invalid properties found!");
                }
                else
                {
                    string message = "";
                    for (int i = 0; i < properties.Count; i++)
                    {
                        message += (string)properties[i] + "\r\n";
                    }
                    new DebugWindow("Invalid Properties", message);
                }
            }
        }

        private void click_tomeValidate(object sender, EventArgs e)
        {
            if (eLC != null)
            {
                ArrayList properties = new ArrayList();

                string attribute;

                for (int n = 0; n < eLC.Lists[112].elementValues.Length; n++)
                {
                    for (int f = 4; f < 14; f++)
                    {
                        attribute = eLC.GetValue(112, n, f);

                        if (Convert.ToInt32(attribute) > 1909)
                        {
                            properties.Add("Invalid Property: " + attribute + " (Tome: " + eLC.GetValue(112, n, 0) + ")");
                        }
                    }
                }

                if (properties.Count == 0)
                {
                    MessageBox.Show("OK, no invalid tome properties found!");
                }
                else
                {
                    string message = "";
                    for (int i = 0; i < properties.Count; i++)
                    {
                        message += (string)properties[i] + "\r\n";
                    }
                    new DebugWindow("Invalid Tome Properties", message);
                }
            }
        }

        private void click_skillReplace(object sender, EventArgs e)
        {
            if (eLC != null)
            {
                OpenFileDialog load = new OpenFileDialog();
                load.InitialDirectory = Application.StartupPath + "\\replace";
                load.Filter = "Skill Replace File (*.txt)|*.txt|All Files (*.*)|*.*";
                if (load.ShowDialog() == DialogResult.OK && File.Exists(load.FileName))
                {
                    SortedList skillTable = new SortedList();

                    StreamReader sr = new StreamReader(load.FileName);

                    string line;
                    string[] pair;
                    string[] seperator = new string[] { "=" };
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        if (!line.StartsWith("#") && line.Contains("="))
                        {
                            pair = line.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                            if (pair.Length == 2)
                            {
                                skillTable.Add(pair[0], pair[1]);
                            }
                        }
                    }

                    sr.Close();
                    /*
					ArrayList^ mobSkills = gcnew ArrayList();
					*/
                    string skill;

                    // change all monster skills (list 38 fields 119, 121, 123, 125, 127, 129)
                    for (int n = 0; n < eLC.Lists[38].elementValues.Length; n++)
                    {
                        for (int f = 119; f < 130; f += 2)
                        {
                            skill = eLC.GetValue(38, n, f);
                            /*
							if(!mobSkills->Contains(skill))
							{
								mobSkills->Add(skill);
							}
							*/
                            if (skillTable.ContainsKey(skill))
                            {
                                eLC.SetValue(38, n, f, (string)skillTable[skill]);
                            }
                        }
                    }
                    /*
					int debug = 1;
					*/
                }
            }
        }

        private void click_propertyReplace(object sender, EventArgs e)
        {
            if (eLC != null)
            {
                OpenFileDialog load = new OpenFileDialog();
                load.InitialDirectory = Application.StartupPath + "\\replace";
                load.Filter = "Property Replace File (*.txt)|*.txt|All Files (*.*)|*.*";
                if (load.ShowDialog() == DialogResult.OK && File.Exists(load.FileName))
                {
                    SortedList propertyTable = new SortedList();

                    StreamReader sr = new StreamReader(load.FileName);

                    string line;
                    string[] pair;
                    string[] seperator = new string[] { "=" };
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        if (!line.StartsWith("#") && line.Contains("="))
                        {
                            pair = line.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                            if (pair.Length == 2)
                            {
                                propertyTable.Add(pair[0], pair[1]);
                            }
                        }
                    }

                    sr.Close();
                    /*
					ArrayList^ weaponProps = gcnew ArrayList();
					ArrayList^ armorProps = gcnew ArrayList();
					ArrayList^ ornamentProps = gcnew ArrayList();
					ArrayList^ gemProps = gcnew ArrayList();
					ArrayList^ complectProps = gcnew ArrayList();
					*/

                    string attribute;

                    // weapons (list 3, fields 43-201, +=2)
                    for (int n = 0; n < eLC.Lists[3].elementValues.Length; n++)
                    {
                        for (int f = 43; f < 202; f += 2)
                        {
                            attribute = eLC.GetValue(3, n, f);
                            /*
							if(!weaponProps->Contains(attribute))
							{
								weaponProps->Add(attribute);
							}
							*/
                            if (propertyTable.ContainsKey(attribute))
                            {
                                eLC.SetValue(3, n, f, (string)propertyTable[attribute]);
                            }
                        }
                    }

                    // armors (list 6, fields 55-179, +=2)
                    for (int n = 0; n < eLC.Lists[6].elementValues.Length; n++)
                    {
                        for (int f = 55; f < 180; f += 2)
                        {
                            attribute = eLC.GetValue(6, n, f);
                            /*
							if(!armorProps->Contains(attribute))
							{
								armorProps->Add(attribute);
							}
							*/
                            if (propertyTable.ContainsKey(attribute))
                            {
                                eLC.SetValue(6, n, f, (string)propertyTable[attribute]);
                            }
                        }
                    }

                    // ornaments (list 9, fields 44-160, +=2)
                    for (int n = 0; n < eLC.Lists[9].elementValues.Length; n++)
                    {
                        for (int f = 44; f < 161; f += 2)
                        {
                            attribute = eLC.GetValue(9, n, f);
                            /*
							if(!ornamentProps->Contains(attribute))
							{
								ornamentProps->Add(attribute);
							}
							*/
                            if (propertyTable.ContainsKey(attribute))
                            {
                                eLC.SetValue(9, n, f, (string)propertyTable[attribute]);
                            }
                        }
                    }

                    // soulgems (list 35, fields 11-12, +=1)
                    for (int n = 0; n < eLC.Lists[35].elementValues.Length; n++)
                    {
                        for (int f = 11; f < 13; f++)
                        {
                            attribute = eLC.GetValue(35, n, f);
                            /*
							if(!gemProps->Contains(attribute))
							{
								gemProps->Add(attribute);
							}
							*/
                            if (propertyTable.ContainsKey(attribute))
                            {
                                eLC.SetValue(35, n, f, (string)propertyTable[attribute]);

                                if ((string)propertyTable[attribute] == "1515")
                                {
                                    eLC.SetValue(35, n, f + 2, "Vit. +20");
                                }
                                if ((string)propertyTable[attribute] == "1517")
                                {
                                    eLC.SetValue(35, n, f + 2, "Critical +2%");
                                }
                                if ((string)propertyTable[attribute] == "1518")
                                {
                                    eLC.SetValue(35, n, f + 2, "Channel -6%");
                                }
                            }
                        }
                    }

                    // complect boni (list 90, fields 15-19, +=1)
                    for (int n = 0; n < eLC.Lists[90].elementValues.Length; n++)
                    {
                        for (int f = 15; f < 20; f++)
                        {
                            attribute = eLC.GetValue(90, n, f);
                            /*
							if(!complectProps->Contains(attribute))
							{
								complectProps->Add(attribute);
							}
							*/
                            if (propertyTable.ContainsKey(attribute))
                            {
                                eLC.SetValue(90, n, f, (string)propertyTable[attribute]);
                            }
                        }
                    }
                    /*
					int debug = 1;
					*/
                }
            }
        }

        private void click_tomeReplace(object sender, EventArgs e)
        {
            if (eLC != null)
            {
                OpenFileDialog load = new OpenFileDialog();
                load.InitialDirectory = Application.StartupPath + "\\replace";
                load.Filter = "Tome Replace File (*.txt)|*.txt|All Files (*.*)|*.*";
                if (load.ShowDialog() == DialogResult.OK && File.Exists(load.FileName))
                {
                    SortedList propertyTable = new SortedList();

                    StreamReader sr = new StreamReader(load.FileName);

                    string line;
                    string[] pair;
                    string[] seperator = new string[] { "=" };
                    string[] divider = new string[] { "," };
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        if (!line.StartsWith("#") && line.Contains("="))
                        {
                            pair = line.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                            if (pair.Length == 2)
                            {
                                propertyTable.Add(pair[0], pair[1].Split(divider, StringSplitOptions.RemoveEmptyEntries));
                            }
                        }
                    }

                    sr.Close();
                    /*
					ArrayList^ tomeProps = gcnew ArrayList();
					*/
                    string attribute;
                    string[] attributes;
                    ArrayList attributesOrgiginal = new ArrayList();
                    ArrayList attributesReplaced = new ArrayList();

                    // weapons (list 3, fields 43-201, +=2)
                    for (int n = 0; n < eLC.Lists[112].elementValues.Length; n++)
                    {
                        attributesOrgiginal.Clear();
                        attributesReplaced.Clear();

                        for (int f = 4; f < 14; f++)
                        {
                            attribute = eLC.GetValue(112, n, f);
                            /*
							if(!tomeProps->Contains(attribute))
							{
								tomeProps->Add(attribute);
							}
							*/
                            if (attribute != "0")
                            {
                                if (propertyTable.ContainsKey(attribute))
                                {
                                    attributes = (string[])propertyTable[attribute];
                                    for (int a = 0; a < attributes.Length; a++)
                                    {
                                        attributesReplaced.Add(attributes[a]);
                                    }
                                }
                                else
                                {
                                    // add the attribute without changes
                                    attributesReplaced.Add(attribute);
                                }
                            }
                        }

                        if (attributesReplaced.Count > 10)
                        {
                            MessageBox.Show("Tome Attribute Overflow: " + n + "\nAttributes Truncated");
                        }

                        // add the new attribute list to the current tome
                        for (int f = 4; f < 14; f++)
                        {
                            if (f - 4 < attributesReplaced.Count)
                            {
                                // add the replaced attribute
                                attribute = (string)attributesReplaced[f - 4];
                                eLC.SetValue(112, n, f, attribute);
                            }
                            else
                            {
                                eLC.SetValue(112, n, f, "0");
                            }
                        }
                    }
                    /*
					int debug = 1;
					*/
                }
            }
        }

        private void click_probabilityValidate(object sender, EventArgs e)
        {
            if (eLC != null)
            {
                ArrayList probabilities = new ArrayList();
                double attribute;

                // weapons (list 3)
                for (int n = 0; n < eLC.Lists[3].elementValues.Length; n++)
                {
                    // weapon drop sockets count(fields 32-34, +=1)

                    attribute = 0;

                    for (int f = 32; f < 35; f++)
                    {
                        attribute += Convert.ToDouble(eLC.GetValue(3, n, f));
                    }

                    if (Math.Round(attribute, 6) != 1)
                    {
                        probabilities.Add("Suspicious Socket Drop Probability (sum != 1.0): " + attribute.ToString() + " (Weapon: " + eLC.GetValue(3, n, 0) + ")");
                    }

                    // weapon craft sockets count(fields 35-37, +=1)

                    attribute = 0;

                    for (int f = 35; f < 38; f++)
                    {
                        attribute += Convert.ToDouble(eLC.GetValue(3, n, f));
                    }

                    if (Math.Round(attribute, 6) != 1)
                    {
                        probabilities.Add("Suspicious Socket Craft Probability (sum != 1.0): " + attribute.ToString() + " (Weapon: " + eLC.GetValue(3, n, 0) + ")");
                    }

                    // weapon addons count(fields 38-41, +=1)

                    attribute = 0;

                    for (int f = 38; f < 42; f++)
                    {
                        attribute += Convert.ToDouble(eLC.GetValue(3, n, f));
                    }

                    if (Math.Round(attribute, 6) != 1)
                    {
                        probabilities.Add("Suspicious Addon Count Probability (sum != 1.0): " + attribute.ToString() + " (Weapon: " + eLC.GetValue(3, n, 0) + ")");
                    }

                    // weapon drop (fields 44-106, +=2)

                    attribute = 0;

                    for (int f = 44; f < 107; f += 2)
                    {
                        attribute += Convert.ToDouble(eLC.GetValue(3, n, f));
                    }

                    if (Math.Round(attribute, 6) != 1)
                    {
                        probabilities.Add("Suspicious Drop Attriutes Probability (sum != 1.0): " + attribute.ToString() + " (Weapon: " + eLC.GetValue(3, n, 0) + ")");
                    }

                    // weapon craft (fields 108-170, +=2)

                    attribute = 0;

                    for (int f = 108; f < 171; f += 2)
                    {
                        attribute += Convert.ToDouble(eLC.GetValue(3, n, f));
                    }

                    if (Math.Round(attribute, 6) != 1)
                    {
                        probabilities.Add("Suspicious Craft Attributes Probability (sum != 1.0): " + attribute.ToString() + " (Weapon: " + eLC.GetValue(3, n, 0) + ")");
                    }

                    // weapons unique (fields 172-202, +=2)

                    attribute = 0;

                    for (int f = 172; f < 203; f += 2)
                    {
                        attribute += Convert.ToDouble(eLC.GetValue(3, n, f));
                    }

                    if (Math.Round(attribute, 6) != 1)
                    {
                        probabilities.Add("Suspicious Unique Attributes Probability (sum != 1.0): " + attribute.ToString() + " (Weapon: " + eLC.GetValue(3, n, 0) + ")");
                    }
                }

                // armors (list 6)
                for (int n = 0; n < eLC.Lists[6].elementValues.Length; n++)
                {
                    // armor drop sockets count(fields 41-45, +=1)

                    attribute = 0;

                    for (int f = 41; f < 46; f++)
                    {
                        attribute += Convert.ToDouble(eLC.GetValue(6, n, f));
                    }

                    if (Math.Round(attribute, 6) != 1)
                    {
                        probabilities.Add("Suspicious Socket Drop Probability (sum != 1.0): " + attribute.ToString() + " (Armor: " + eLC.GetValue(6, n, 0) + ")");
                    }

                    // armor craft sockets count(fields 46-50, +=1)

                    attribute = 0;

                    for (int f = 46; f < 51; f++)
                    {
                        attribute += Convert.ToDouble(eLC.GetValue(6, n, f));
                    }

                    if (Math.Round(attribute, 6) != 1)
                    {
                        probabilities.Add("Suspicious Socket Craft Probability (sum != 1.0): " + attribute.ToString() + " (Armor: " + eLC.GetValue(6, n, 0) + ")");
                    }

                    // armor addons count(fields 51-54, +=1)

                    attribute = 0;

                    for (int f = 51; f < 55; f++)
                    {
                        attribute += Convert.ToDouble(eLC.GetValue(6, n, f));
                    }

                    if (Math.Round(attribute, 6) != 1)
                    {
                        probabilities.Add("Suspicious Addon Count Probability (sum != 1.0): " + attribute.ToString() + " (Armor: " + eLC.GetValue(6, n, 0) + ")");
                    }

                    // armor drop (fields 56-118, +=2)

                    attribute = 0;

                    for (int f = 56; f < 119; f += 2)
                    {
                        attribute += Convert.ToDouble(eLC.GetValue(6, n, f));
                    }

                    if (Math.Round(attribute, 6) != 1)
                    {
                        probabilities.Add("Suspicious Drop Attriutes Probability (sum != 1.0): " + attribute.ToString() + " (Armor: " + eLC.GetValue(6, n, 0) + ")");
                    }

                    // armor craft (fields 120-180, +=2)

                    attribute = 0;

                    for (int f = 120; f < 181; f += 2)
                    {
                        attribute += Convert.ToDouble(eLC.GetValue(6, n, f));
                    }

                    if (Math.Round(attribute, 6) != 1)
                    {
                        probabilities.Add("Suspicious Craft Attributes Probability (sum != 1.0): " + attribute.ToString() + " (Armor: " + eLC.GetValue(6, n, 0) + ")");
                    }
                }

                // ornaments (list 9)
                for (int n = 0; n < eLC.Lists[9].elementValues.Length; n++)
                {
                    // ornament addons count(fields 40-43, +=1)

                    attribute = 0;

                    for (int f = 40; f < 44; f++)
                    {
                        attribute += Convert.ToDouble(eLC.GetValue(9, n, f));
                    }

                    if (Math.Round(attribute, 6) != 1)
                    {
                        probabilities.Add("Suspicious Addon Count Probability (sum != 1.0): " + attribute.ToString() + " (Ornament: " + eLC.GetValue(9, n, 0) + ")");
                    }

                    // ornament drop (fields 45-107, +=2)

                    attribute = 0;

                    for (int f = 45; f < 108; f += 2)
                    {
                        attribute += Convert.ToDouble(eLC.GetValue(9, n, f));
                    }

                    if (Math.Round(attribute, 6) != 1)
                    {
                        probabilities.Add("Suspicious Drop Attriutes Probability (sum != 1.0): " + attribute.ToString() + " (Ornament: " + eLC.GetValue(9, n, 0) + ")");
                    }

                    // ornament craft (fields 109-161, +=2)

                    attribute = 0;

                    for (int f = 109; f < 162; f += 2)
                    {
                        attribute += Convert.ToDouble(eLC.GetValue(9, n, f));
                    }

                    if (Math.Round(attribute, 6) != 1)
                    {
                        probabilities.Add("Suspicious Craft Attributes Probability (sum != 1.0): " + attribute.ToString() + " (Ornament: " + eLC.GetValue(9, n, 0) + ")");
                    }
                }

                if (probabilities.Count == 0)
                {
                    MessageBox.Show("OK, no invalid probabilities found!");
                }
                else
                {
                    string message = "";
                    for (int i = 0; i < probabilities.Count; i++)
                    {
                        message += (string)probabilities[i] + "\r\n";
                    }
                    new DebugWindow("Invalid Probabilities", message);
                }
            }
        }

        private void click_TaskOverflowCheck(object sender, EventArgs e)
        {
            if (eLC != null)
            {
                string value;
                bool isAddedElement;

                LoseQuestWindow questWindow = new LoseQuestWindow();



                // list 45 recive quests
                for (int n = 0; n < eLC.Lists[45].elementValues.Length; n++)
                {
                    isAddedElement = false;
                    for (int f = 34; f < eLC.Lists[45].elementFields.Length; f++)
                    {
                        value = eLC.GetValue(45, n, f);
                        if (value != "0")
                        {
                            if (!isAddedElement)
                            {
                                questWindow.listBox_Receive.Items.Add("+++++ " + eLC.GetValue(45, n, 0) + " - " + eLC.GetValue(45, n, 1) + " +++++");
                                isAddedElement = true;
                            }
                            questWindow.listBox_Receive.Items.Add(value);
                        }
                    }
                }

                // list 46 activate quests
                for (int n = 0; n < eLC.Lists[46].elementValues.Length; n++)
                {
                    isAddedElement = false;
                    for (int f = 34; f < eLC.Lists[46].elementFields.Length; f++)
                    {
                        value = eLC.GetValue(46, n, f);
                        if (value != "0")
                        {
                            if (!isAddedElement)
                            {
                                questWindow.listBox_Activate.Items.Add("+++++ " + eLC.GetValue(46, n, 0) + " - " + eLC.GetValue(46, n, 1) + " +++++");
                                isAddedElement = true;
                            }
                            questWindow.listBox_Activate.Items.Add(value);
                        }
                    }
                }

                questWindow.Show();
            }
        }

        private void click_classMask(object sender, EventArgs e)
        {
            ClassMaskWindow eClassMask = new ClassMaskWindow();
            eClassMask.Show();
        }

        private void cellMouseMove_ToolTip(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (comboBox_lists.SelectedIndex == 0 && dataGridView_elems.CurrentCell.RowIndex != -1 && e.ColumnIndex == 2 && e.RowIndex > 2 && e.RowIndex < 6)
            {
                dataGridView_item.ShowCellToolTips = false;
                string text = "Float: " + (BitConverter.ToSingle(BitConverter.GetBytes((int)(eLC.Lists[0].elementValues[dataGridView_elems.CurrentCell.RowIndex][e.RowIndex])), 0)).ToString("F6");

                // not working on first mouse over (still shows previous value on first mouse over)
                //dataGridView_item->Rows[e->RowIndex]->Cells[e->ColumnIndex]->ToolTipText = text;

                // only draw on real move to prevent flickering in windows 7
                if (mouseMoveCheck.X != e.X || mouseMoveCheck.Y != e.Y)
                {
                    toolTip.SetToolTip((Control)sender, text);
                    mouseMoveCheck.X = e.X;
                    mouseMoveCheck.Y = e.Y;
                }
            }
            else if (e.RowIndex > -1 && dataGridView_item.Rows[e.RowIndex].Cells[0].Value.ToString() == "shop_price" && comboBox_lists.SelectedIndex > -1 && dataGridView_elems.CurrentCell.RowIndex > -1)
            {
                dataGridView_item.ShowCellToolTips = false;
                int shop_price = Convert.ToInt32(eLC.GetValue(comboBox_lists.SelectedIndex, dataGridView_elems.CurrentCell.RowIndex, e.RowIndex));
                double tmp = 0;
                double tmp1 = 0;
                tmp1 = shop_price * 0.05;
                if (shop_price >= 10)
                    tmp1 = Math.Round(tmp1, MidpointRounding.AwayFromZero);
                else
                    tmp1 = Math.Round(tmp1);
                tmp = shop_price + tmp1;
                if (tmp >= 100 && tmp < 1000)
                {
                    tmp = tmp * 0.1;
                    tmp = Math.Ceiling(tmp);
                    tmp = tmp * 10;
                }
                if (tmp >= 1000)
                {
                    tmp = tmp * 0.01;
                    tmp = Math.Ceiling(tmp);
                    tmp = tmp * 100;
                }
                string text = "In Game Price: " + tmp;
                if (mouseMoveCheck.X != e.X || mouseMoveCheck.Y != e.Y)
                {
                    toolTip.SetToolTip((Control)sender, text);
                    mouseMoveCheck.X = e.X;
                    mouseMoveCheck.Y = e.Y;
                }
            }
            else
            {
                toolTip.Hide((Control)sender);
                dataGridView_item.ShowCellToolTips = true;
            }
        }

        private void click_diffEL(object sender, EventArgs e)
        {
            RulesWindow eRules = new RulesWindow(ref cpb2);
            eRules.Show();
        }

        private void listBox_items_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int l = comboBox_lists.SelectedIndex;
            int k = dataGridView_elems.CurrentCell.RowIndex;
            if (l != eLC.ConversationListIndex)
            {
                if (l > -1 && k > -1)
                {
                    int pos = -1;
                    for (int i = 0; i < eLC.Lists[l].elementFields.Length; i++)
                    {
                        if (eLC.Lists[l].elementFields[i] == "Name")
                        {
                            pos = i;
                            break;
                        }
                    }
                    if (pos > -1)
                    {
                        Clipboard.SetDataObject(eLC.GetValue(l, k, 0) + "	" + eLC.GetValue(l, k, pos), true);
                    }
                    else
                    {
                        MessageBox.Show("Config Error: cannot find Name field");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid List");
                }
            }
            else
            {
                MessageBox.Show("Operation not supported in List " + eLC.ConversationListIndex.ToString());
            }
        }

        private void click_xrefItem(object sender, EventArgs ea)
        {
            int l = comboBox_lists.SelectedIndex;
            int e = dataGridView_elems.CurrentCell.RowIndex;
            if (l != eLC.ConversationListIndex)
            {
                if (l > -1 && e > -1)
                {
                    ReferencesWindow eXRef = new ReferencesWindow();
                    char[] chars = { '-' };
                    int results = 0;

                    for (int j = 1; j < xrefs[l].Length; j++)
                    {
                        string[] x = xrefs[l][j].Split(chars);
                        for (int m = 1; m < eLC.Lists[int.Parse(x[0])].elementValues.Length; m++)
                        {
                            for (int k = 1; k < x.Length; k++)
                            {
                                if (eLC.GetValue(int.Parse(x[0]), m, int.Parse(x[k])) == eLC.GetValue(l, e, 0))
                                {
                                    results++;
                                    int pos = -1;
                                    for (int i = 0; i < eLC.Lists[l].elementFields.Length; i++)
                                    {
                                        if (eLC.Lists[int.Parse(x[0])].elementFields[i] == "Name")
                                        {
                                            pos = i;
                                            break;
                                        }
                                    }
                                    eXRef.dataGridView.Rows.Add(new string[] { x[0], eLC.Lists[int.Parse(x[0])].listName, eLC.GetValue(int.Parse(x[0]), m, 0), eLC.GetValue(int.Parse(x[0]), m, pos), x[k] + " - " + eLC.Lists[int.Parse(x[0])].elementFields[int.Parse(x[k])] });
                                }
                            }
                        }
                    }
                    if (results > 0)
                    {
                        eXRef.Show();
                    }
                    else
                    {
                        eXRef.Close();
                        MessageBox.Show("No results found");
                    }
                }
            }
            else
            {
                MessageBox.Show("Operation not supported in List " + eLC.ConversationListIndex.ToString());
            }
        }

        private void click_SetValue(object sender, EventArgs e)
        {
            if (dgV != null)
            {
                string valueSet = string.Empty;

                if (typeItem.StartsWith("int"))
                {
                    valueSet = Convert.ToInt32(numericUpDownEx_value.Value.ToString()).ToString(); ;
                }
                if (typeItem.StartsWith("float"))
                {
                    valueSet = numericUpDownEx_value.Value.ToString();

                }
                if (typeItem.StartsWith("string") || typeItem.StartsWith("wstring"))
                {
                    valueSet = textBox_SetValue.Text;
                }

                int l = comboBox_lists.SelectedIndex;
                if (l > -1 && l != eLC.ConversationListIndex)
                {
                    ArrayList SelectedCellsIndexes = new ArrayList();
                    int[] selIndices = gridSelectedIndices(dataGridView_elems);
                    for (int i = 0; i < dgV.SelectedCells.Count; i++)
                    {
                        bool check = true;
                        for (int k = 0; k < SelectedCellsIndexes.Count; k++)
                        {
                            if ((int)SelectedCellsIndexes[k] == dgV.SelectedCells[i].RowIndex)
                            {
                                check = false;
                                break;
                            }
                        }
                        if (check)
                            SelectedCellsIndexes.Add(dgV.SelectedCells[i].RowIndex);
                    }
                    SelectedCellsIndexes.Sort();
                    for (int i = 0; i < SelectedCellsIndexes.Count; i++)
                    {

                        dgV.Rows[(int)SelectedCellsIndexes[i]].Cells[2].Value = valueSet;
                    }
                    for (int i = 0; i < selIndices.Length; i++)
                    {
                        for (int f = 0; f < SelectedCellsIndexes.Count; f++)
                        {

                            eLC.SetValue(l, selIndices[i], int.Parse(dgV.Rows[(int)SelectedCellsIndexes[i]].Cells[3].Value.ToString()), valueSet);

                            // (int)SelectedCellsIndexes[f]


                            if (dgV.Rows[(int)SelectedCellsIndexes[f]].Cells[0].Value.ToString() == "Name")
                            {
                                int pos = -1;
                                int pos2 = -1;
                                for (int p = 0; p < eLC.Lists[l].elementFields.Length; p++)
                                {
                                    if (eLC.Lists[l].elementFields[p] == "Name")
                                    {
                                        pos = p;
                                    }
                                    if (eLC.Lists[l].elementFields[p] == "file_icon" || eLC.Lists[l].elementFields[p] == "file_icon1")
                                    {
                                        pos2 = p;
                                    }
                                    if (pos != -1 && pos2 != -1)
                                    {
                                        break;
                                    }
                                }
                                if (eLC.Lists[l].elementFields[0] == "ID")
                                {
                                    Bitmap img = Properties.Resources.unknown;
                                    string path = Path.GetFileName(eLC.GetValue(l, selIndices[i], pos2));
                                    if (database.sourceBitmap != null && database.ContainsKey(path))
                                    {
                                        if (database.ContainsKey(path))
                                        {
                                            img = database.images(path);
                                        }
                                    }
                                    dataGridView_elems.Rows[selIndices[i]].Cells[0].Value = eLC.GetValue(l, selIndices[i], 0);
                                    dataGridView_elems.Rows[selIndices[i]].Cells[1].Value = img;
                                    dataGridView_elems.Rows[selIndices[i]].Cells[2].Value = eLC.GetValue(l, selIndices[i], pos);
                                }
                                else
                                {
                                    dataGridView_elems.Rows[selIndices[i]].Cells[0].Value = "";
                                    dataGridView_elems.Rows[selIndices[i]].Cells[1].Value = Properties.Resources.unknown;
                                    dataGridView_elems.Rows[selIndices[i]].Cells[2].Value = eLC.GetValue(l, selIndices[i], pos);
                                }
                            }
                        }
                    }
                }
            }

        }

        private void listBox_items_KeyDown(object sender, KeyEventArgs e)
        {
            //if (ModifierKeys == Keys.Control && listBox_items.SelectedIndices.Count > 0 && comboBox_lists.SelectedIndex != eLC.ConversationListIndex)
            //{
            //	if (e.KeyCode == Keys.Up)
            //	{
            //		if (listBox_items.SelectedIndices[0] > 0)
            //		{
            //			EnableSelectionItem = false;
            //			int[] SelectedIndices = new int[listBox_items.SelectedIndices.Count];
            //			for (int i = 0; i < listBox_items.SelectedIndices.Count; i++)
            //			{
            //				SelectedIndices[i] = listBox_items.SelectedIndices[i];
            //			}
            //			int pos = -1;
            //			for (int i = 0; i < eLC.Lists[comboBox_lists.SelectedIndex].elementFields.Length; i++)
            //			{
            //				if (eLC.Lists[comboBox_lists.SelectedIndex].elementFields[i] == "Name")
            //				{
            //					pos = i;
            //					break;
            //				}
            //			}
            //			for (int i = 0; i < listBox_items.SelectedIndices.Count; i++)
            //			{
            //				object[][] temp = new object[eLC.Lists[comboBox_lists.SelectedIndex].elementValues.Length][];
            //				Array.Copy(eLC.Lists[comboBox_lists.SelectedIndex].elementValues, 0, temp, 0, listBox_items.SelectedIndices[i] - 1);
            //				Array.Copy(eLC.Lists[comboBox_lists.SelectedIndex].elementValues, listBox_items.SelectedIndices[i], temp, listBox_items.SelectedIndices[i] - 1, 1);
            //				Array.Copy(eLC.Lists[comboBox_lists.SelectedIndex].elementValues, listBox_items.SelectedIndices[i] - 1, temp, listBox_items.SelectedIndices[i], 1);
            //				if (listBox_items.SelectedIndices[i] < listBox_items.Items.Count - 1)
            //					Array.Copy(eLC.Lists[comboBox_lists.SelectedIndex].elementValues, listBox_items.SelectedIndices[i] + 1, temp, listBox_items.SelectedIndices[i] + 1, listBox_items.Items.Count - listBox_items.SelectedIndices[i] - 1);
            //				eLC.Lists[comboBox_lists.SelectedIndex].elementValues = temp;
            //				int ei = SelectedIndices[i] - 1;
            //				int ei2 = SelectedIndices[i];
            //				if (eLC.Lists[comboBox_lists.SelectedIndex].elementFields[0] == "ID")
            //				{
            //					listBox_items.Items[ei] = "[" + ei + "]: " + eLC.GetValue(comboBox_lists.SelectedIndex, ei, 0) + " - " + eLC.GetValue(comboBox_lists.SelectedIndex, ei, pos);
            //					listBox_items.Items[ei2] = "[" + ei2 + "]: " + eLC.GetValue(comboBox_lists.SelectedIndex, ei2, 0) + " - " + eLC.GetValue(comboBox_lists.SelectedIndex, ei2, pos);
            //				}
            //				else
            //				{
            //					listBox_items.Items[ei] = "[" + ei + "]: " + eLC.GetValue(comboBox_lists.SelectedIndex, ei, pos);
            //					listBox_items.Items[ei2] = "[" + ei2 + "]: " + eLC.GetValue(comboBox_lists.SelectedIndex, ei2, pos);
            //				}
            //			}
            //			listBox_items.SelectedIndex = -1;
            //			listBox_items.SelectionMode = SelectionMode.MultiSimple;
            //			for (int i = 0; i < SelectedIndices.Length; i++)
            //			{
            //				listBox_items.SelectedIndex = SelectedIndices[i] - 1;
            //			}
            //			listBox_items.SelectionMode = SelectionMode.MultiExtended;
            //			EnableSelectionItem = true;
            //			change_item(null, null);
            //		}
            //	}
            //	else if (e.KeyCode == Keys.Down)
            //	{
            //		if (listBox_items.SelectedIndices[listBox_items.SelectedIndices.Count - 1] < listBox_items.Items.Count - 1)
            //		{
            //			EnableSelectionItem = false;
            //			int[] SelectedIndices = new int[listBox_items.SelectedIndices.Count];
            //			for (int i = 0; i < listBox_items.SelectedIndices.Count; i++)
            //			{
            //				SelectedIndices[i] = listBox_items.SelectedIndices[i];
            //			}
            //			int pos = -1;
            //			for (int i = 0; i < eLC.Lists[comboBox_lists.SelectedIndex].elementFields.Length; i++)
            //			{
            //				if (eLC.Lists[comboBox_lists.SelectedIndex].elementFields[i] == "Name")
            //				{
            //					pos = i;
            //					break;
            //				}
            //			}
            //			for (int i = listBox_items.SelectedIndices.Count - 1; i > -1; i--)
            //			{
            //				object[][] temp = new object[eLC.Lists[comboBox_lists.SelectedIndex].elementValues.Length][];
            //				Array.Copy(eLC.Lists[comboBox_lists.SelectedIndex].elementValues, 0, temp, 0, listBox_items.SelectedIndices[i]);
            //				Array.Copy(eLC.Lists[comboBox_lists.SelectedIndex].elementValues, listBox_items.SelectedIndices[i] + 1, temp, listBox_items.SelectedIndices[i], 1);
            //				Array.Copy(eLC.Lists[comboBox_lists.SelectedIndex].elementValues, listBox_items.SelectedIndices[i], temp, listBox_items.SelectedIndices[i] + 1, 1);
            //				if (listBox_items.SelectedIndices[i] < listBox_items.Items.Count - 2)
            //					Array.Copy(eLC.Lists[comboBox_lists.SelectedIndex].elementValues, listBox_items.SelectedIndices[i] + 2, temp, listBox_items.SelectedIndices[i] + 2, listBox_items.Items.Count - listBox_items.SelectedIndices[i] - 2);
            //				eLC.Lists[comboBox_lists.SelectedIndex].elementValues = temp;
            //				int ei = SelectedIndices[i] + 1;
            //				int ei2 = SelectedIndices[i];
            //				if (eLC.Lists[comboBox_lists.SelectedIndex].elementFields[0] == "ID")
            //				{
            //					listBox_items.Items[ei] = "[" + ei + "]: " + eLC.GetValue(comboBox_lists.SelectedIndex, ei, 0) + " - " + eLC.GetValue(comboBox_lists.SelectedIndex, ei, pos);
            //					listBox_items.Items[ei2] = "[" + ei2 + "]: " + eLC.GetValue(comboBox_lists.SelectedIndex, ei2, 0) + " - " + eLC.GetValue(comboBox_lists.SelectedIndex, ei2, pos);
            //				}
            //				else
            //				{
            //					listBox_items.Items[ei] = "[" + ei + "]: " + eLC.GetValue(comboBox_lists.SelectedIndex, ei, pos);
            //					listBox_items.Items[ei2] = "[" + ei2 + "]: " + eLC.GetValue(comboBox_lists.SelectedIndex, ei2, pos);
            //				}
            //			}
            //			listBox_items.SelectedIndex = -1;
            //			listBox_items.SelectionMode = SelectionMode.MultiSimple;
            //			for (int i = 0; i < SelectedIndices.Length; i++)
            //			{
            //				listBox_items.SelectedIndex = SelectedIndices[i] + 1;
            //			}
            //			listBox_items.SelectionMode = SelectionMode.MultiExtended;
            //			EnableSelectionItem = true;
            //			change_item(null, null);
            //		}
            //	}
            //}
        }

        private void click_moveItemsToTop(object sender, EventArgs ea)
        {
            int l = comboBox_lists.SelectedIndex;
            int[] selIndices = gridSelectedIndices(dataGridView_elems);
            if (selIndices[0] > 0 && selIndices.Length > 0 && l != eLC.ConversationListIndex)
            {
                EnableSelectionItem = false;
                object[][] temp = new object[eLC.Lists[l].elementValues.Length][];
                for (int i = 0; i < selIndices.Length; i++)
                {
                    Array.Copy(eLC.Lists[l].elementValues, selIndices[i], temp, i, 1);
                }
                Array.Copy(eLC.Lists[l].elementValues, 0, temp, selIndices.Length, selIndices[0]);
                for (int i = selIndices.Length - 1; i > -1; i--)
                {
                    eLC.Lists[l].RemoveItem(selIndices[i]);
                }
                Array.Copy(eLC.Lists[l].elementValues, 0, temp, selIndices.Length, eLC.Lists[l].elementValues.Length);
                eLC.Lists[l].elementValues = temp;

                change_list(null, null);

                dataGridView_elems.ClearSelection();
                for (int i = 0; i < selIndices.Length; i++)
                {
                    dataGridView_elems.Rows[i].Selected = true;
                    dataGridView_elems.FirstDisplayedScrollingRowIndex = i;
                }
                EnableSelectionItem = true;
            }
        }

        private void click_moveItemsToEnd(object sender, EventArgs ea)
        {
            int l = comboBox_lists.SelectedIndex;
            int[] selIndices = gridSelectedIndices(dataGridView_elems);
            if (selIndices[0] < dataGridView_elems.RowCount - 1 && selIndices.Length > 0 && l != eLC.ConversationListIndex)
            {
                EnableSelectionItem = false;
                object[][] temp = new object[eLC.Lists[l].elementValues.Length][];
                for (int i = 0; i < selIndices.Length; i++)
                {
                    Array.Copy(eLC.Lists[l].elementValues, selIndices[i], temp, dataGridView_elems.RowCount - selIndices.Length + i, 1);
                }
                Array.Copy(eLC.Lists[l].elementValues, 0, temp, selIndices.Length, selIndices[0]);
                for (int i = selIndices.Length - 1; i > -1; i--)
                {
                    eLC.Lists[l].RemoveItem(selIndices[i]);
                }
                Array.Copy(eLC.Lists[l].elementValues, 0, temp, 0, eLC.Lists[l].elementValues.Length);
                eLC.Lists[l].elementValues = temp;

                change_list(null, null);

                dataGridView_elems.ClearSelection();
                for (int i = dataGridView_elems.RowCount - selIndices.Length; i < dataGridView_elems.RowCount; i++)
                {
                    dataGridView_elems.Rows[i].Selected = true;
                    dataGridView_elems.FirstDisplayedScrollingRowIndex = i;
                }
                EnableSelectionItem = true;
            }
        }

        private void click_fieldCompare(object sender, EventArgs e)
        {
            if (eLC != null)
            {
                (new FieldCompare(eLC, conversationList, ref cpb2)).Show();
            }
            else
            {
                MessageBox.Show("No File Loaded!");
            }
        }

        string timestamp_to_string(uint timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            origin = origin.AddSeconds(timestamp);
            return origin.ToString("yyyy-MM-dd HH:mm:ss");
        }

       

        //public Bitmap CropBitmap(Bitmap bitmap, int cropX, int cropY, int cropWidth, int cropHeight)
        //{
        //    Rectangle rect = new Rectangle(cropX, cropY, cropWidth, cropHeight);
        //    Bitmap cropped = bitmap.Clone(rect, bitmap.PixelFormat);
        //    return cropped;
        //}

        public Bitmap ddsIcon(Bitmap rawImg, string rawTxt, string icoName)
        {
            try
            {
                int counter = 0;
                string line;
                int W = 0;
                int H = 0;
                double col = 0;
                double imgNum = -1;
                //X = horizonal
                double X;
                //Y = vertical
                double Y;
                Bitmap cImage = null;

                StreamReader file = new StreamReader(rawTxt, Encoding.GetEncoding("GB2312"));
                while ((line = file.ReadLine()) != null)
                {
                    if (counter == 0) { W = Int32.Parse(line); }
                    if (counter == 1) { H = Int32.Parse(line); }
                    if (counter == 3) { col = Int32.Parse(line); }

                    if (line == icoName)
                    {
                        imgNum = counter - 3;
                        break;
                    }
                    counter++;
                }

                if (imgNum != -1)
                {
                    X = Math.Floor(((imgNum * W) - W) / (col * W)) * W;
                    Y = ((imgNum * W) - W) - (((col * W) * X) / W);

                    Rectangle rect = new Rectangle(Convert.ToInt32(Y), Convert.ToInt32(X), W, H);
                    cImage = rawImg.Clone(rect, rawImg.PixelFormat);
                    //Bitmap cropped = rawImg.Clone(rect, rawImg.PixelFormat);

                    //cImage = CropBitmap(rawImg, Convert.ToInt32(Y), Convert.ToInt32(X), W, H);
                }
                return cImage;
            }
            catch
            {
                return Properties.Resources.QMark;
            }
        }

        public int[] gridSelectedIndices(DataGridView grd)
        {
            List<int> inx = new List<int>();
            Int32 selectedRowCount = grd.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    inx.Add(grd.SelectedRows[i].Index);
                }
            }
            inx.Sort();
            int[] arr = inx.ToArray();
            return arr;
        }

        private void textBox_search_enter(object sender, EventArgs e)
        {
            if (textBox_search.Text == "ID or NAME")
            {
                textBox_search.Clear();
            }
        }

        private void textBox_search_leave(object sender, EventArgs e)
        {
            if (textBox_search.Text == "")
            {
                textBox_search.Text = "ID or NAME";
            }
        }

        private void textBox_value_enter(object sender, EventArgs e)
        {
            if (textBox_SetValue.Text == "Set Value")
            {
                textBox_SetValue.Clear();
            }
        }

        private void textBox_value_leave(object sender, EventArgs e)
        {
            if (textBox_SetValue.Text == "")
            {
                textBox_SetValue.Text = "Set Value";
            }
        }

        private void listBox_items_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (customTooltype != null)
                    customTooltype.Close();
            }
            catch { }
            if (e.ColumnIndex >= 0 && e.ColumnIndex == 1 && e.RowIndex > -1)
            {
                InfoTool ift = null;
                try
                {
                    int l = comboBox_lists.SelectedIndex;
                    int xe = e.RowIndex;
                    int Id = Convert.ToInt32(this.dataGridView_elems.Rows[e.RowIndex].Cells[0].Value);
                    if (Id > 0)
                    {
                        ift = Extensions.GetItemPropsByILP(Id, 0, l, xe);
                    }
                    if (ift == null)
                    {
                        string text = Extensions.GetItemProps(Id, 0);
                        text += Extensions.ItemDesc(Id);
                        this.dataGridView_elems.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = text;
                    }
                    else
                    {
                        ift.description = Extensions.ItemDesc(Id);// Extensions.ColorClean(Extensions.ItemDesc(Id));
                        customTooltype = new IToolType(ift);
                        customTooltype.Show(this);
                    }
                }
                catch
                {
                }
            }
        }

        private void createListWithCountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.FileName = "elements.list.count";
            saveFileDialog1.Title = "Save List Count File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                if (File.Exists(saveFileDialog1.FileName)) { File.Delete(saveFileDialog1.FileName); }
                using (StreamWriter file = new StreamWriter(saveFileDialog1.FileName))
                {
                    file.WriteLine("ver=" + eLC.Version);
                    for (int l = 0; l < eLC.Lists.Length; l++)
                    {
                        file.WriteLine(l + "=" + eLC.Lists[l].elementValues.Length);
                    }
                }
            }
        }
        void Loads()
        {
            ITEM = new List<NOVO.ITEM>();

            try
            {
                tabControl_INFO.Invoke((MethodInvoker)delegate ()
                {
                    this.Enabled = true;
                    tabControl_INFO.TabPages.Remove(tabPage_addons);
                    tabControl_INFO.TabPages.Remove(tabPage_rands);
                    tabControl_INFO.TabPages.Remove(tabPage_uniques);
                    tabControl_INFO.TabPages.Remove(tabPage_materials);
                });
            }
            catch (Exception)
            {

                //throw;
            }



            bool r = ReadXML();

            if (r == true)
            {
                if (File.Exists(XmlData.ElementsDataPath))
                {
                    var a = MessageBox.Show("Deseja carregar as Configurações salvas ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (a == DialogResult.Yes)
                    {
                        Thread element = new Thread(new ThreadStart(LoadElementData));
                        element.Start();
                    }
                }
                if (File.Exists(XmlData.TasksDataPath))
                {
                    Thread tasks = new Thread(new ThreadStart(ReadTask));
                    tasks.Start();
                    
                }
				if (File.Exists(XmlData.GshopDataPath))
				{
					Thread gshop = new Thread(new ThreadStart(ReadShops));
					gshop.Start();

				}
				
			}
            bool el = false; bool cf = false; bool sf = false; bool tk = false; bool g1 = false; bool g2 = false;
			if (File.Exists(XmlData.ElementsDataPath)) { el = true; }
            if (File.Exists(XmlData.ConfigsPckPath)) { cf = true; }
            if (File.Exists(XmlData.SurfacesPckPath)) { sf = true; }
			if (File.Exists(XmlData.TasksDataPath)) { tk = true; }
			if (File.Exists(XmlData.GshopDataPath)) { g1 = true; }
			if (File.Exists(XmlData.Gshop1DataPath)) { g2 = true; }
			if (el == true && cf == true && sf == true && tk == true && g1 == true && g2 == true)
            {
                toolStripButton_Config.Image = Properties.Resources.icon_3_0_alpha;
            }
            else
            {
                toolStripButton_Config.Image = Properties.Resources.icon_3_1_alpha;
            }
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is MdiClient)
                {
                    control.BackColor = Color.FromArgb(28, 28, 28);
                    break;
                }
            }
            try
            {
                var Curs = AdvancedCursorsFromEmbededResources.Create(Properties.Resources.Game);
                Cursor = Curs;
                this.Cursor = Curs;
                dataGridView_item.Cursor = Curs;
                dataGridView_item_addons.Cursor = Curs;
                dataGridView_item_rands.Cursor = Curs;
                dataGridView_item_uniques.Cursor = Curs;
                dataGridView_item_materials.Cursor = Curs;
                dataGridView_elems.Cursor = Curs;
                comboBox_lists.Cursor = Curs;
                tabControl_INFO.Cursor = Curs;

            }
            catch { }
        }

        private void toolStripButton_Config_Click(object sender, EventArgs e)
        {
            (new configs.Configs()).ShowDialog(this);
            (new MainWindow_OLD()).Show();
            this.Hide();
        }



        //----------------------------------------------- NEW -------------------------------------------------------------





        private void InfoItemDataGrid(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGrid = (DataGridView)sender;
            try
            {
                if (customTooltype != null)
                    customTooltype.Close();
            }
            catch { }
            if (e.ColumnIndex >= 0 && e.ColumnIndex == 2 && e.RowIndex > -1)
            {
                InfoTool ift = null;
                try
                {
                    int l = comboBox_lists.SelectedIndex;
                    int xe = e.RowIndex;
                    var tt_a = dataGrid.Rows[e.RowIndex].Cells[2].Value.ToString().Replace("[", "").Replace("]", "").Split('-');
                    int Id = Convert.ToInt32(tt_a[0]);


                    string a = dataGrid.Rows[e.RowIndex].Cells[0].Value.ToString();

                    if (a != ("character_combo_id") && a.EndsWith("id") || a.EndsWith("id_to_make") || a.StartsWith("id_upgrade_equip"))
                    {

                        if (Id > 0)
                        {
                            //ift = Extensions.GetItemPropsGets(Id);
                        }
                        if (ift == null)
                        {
                            string text = Extensions.GetItemProps(Id, 0);
                            text += Extensions.ItemDesc(Id);
                            dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = text;
                        }
                        else
                        {
                            ift.description = Extensions.ColorClean(Extensions.ItemDesc(Id));
                            customTooltype = new IToolType(ift);
                            customTooltype.Show(this);
                        }


                    }

                }
                catch
                {
                }
            }
        }





        private void searchItem(object sender, DataGridViewCellEventArgs e, DataGridView dataGrid)
        {

            try
            {
                if (customTooltype != null)
                    customTooltype.Close();
            }
            catch { }
            if (e.ColumnIndex >= 0 && e.ColumnIndex == 2 && e.RowIndex > -1)
            {
                InfoTool ift = null;
                try
                {
                    int l = comboBox_lists.SelectedIndex;
                    int xe = e.RowIndex;
                    int Id = Convert.ToInt32(dataGrid.Rows[e.RowIndex].Cells[2].Value);
                    if (Id > 0)
                    {
                        string value = "";
						for (int L = 0; L < MainWindow_OLD.database.ItemUse.Count; L++)
						{
                            int La = int.Parse(MainWindow_OLD.database.ItemUse.GetKey(L).ToString());
							int pos = 0;
                            for (int i = 0; i < eLC.Lists[La].elementFields.Length; i++)
                            {
                                if (eLC.Lists[La].elementFields[i] == "Name")
                                {
                                    pos = i;
                                    break;
                                }
                            }
                            for (int ef = 0; ef < eLC.Lists[La].elementValues.Length; ef++)
                            {
                                value = eLC.GetValue(La, ef, pos);

                                if (Id == int.Parse(eLC.GetValue(La, ef, 0)) || value.Contains(Id.ToString()))
                                {
                                    //ift = Extensions.GetItemPropsGets(Id);

                                }
                            }


                        }


                    }
                    if (ift == null)
                    {
                        string text = Extensions.GetItemProps(Id, 0);
                        text += Extensions.ItemDesc(Id);
                        dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = text;
                    }
                    else
                    {
                        ift.description = Extensions.ColorClean(Extensions.ItemDesc(Id));
                        customTooltype = new IToolType(ift);
                        customTooltype.Show();
                    }
                }
                catch
                {
                }
            }



        }


        bool ReadXML()
        {
            try
            {
                deserializer = new XmlSerializer(typeof(Settings));
                reader = new StreamReader(caminho);
                obj = deserializer.Deserialize(reader);
                XmlData = (Settings)obj;
                reader.Close();

                return true;
            }
            catch (Exception)
            {
                reader.Close();
                return false;
            }

        }
        void WriteXML()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void change_item(object sender, EventArgs ea)
        {
            tabControl_INFO.TabPages.Remove(tabPage_addons); bool tabPage_addonsB = false;
            tabControl_INFO.TabPages.Remove(tabPage_rands); bool tabPage_randsB = false;
            tabControl_INFO.TabPages.Remove(tabPage_uniques); bool tabPage_uniquesB = false;
            tabControl_INFO.TabPages.Remove(tabPage_materials); bool tabPage_materialsB = false;
            tabControl_INFO.TabPages.Remove(tabPage_return); bool tabPage_returnB = false;

            textBox_NAME.Clear(); numericUpDownEx_ID.Value = 0; pictureBox_icon.Image = Properties.Resources.unknown;


            bool icon = false;
            bool name = false;

            int pd = 0; int add = 0; int rd = 0; int uq = 0; int mt = 0;

            if (EnableSelectionItem)
            {
                int l = comboBox_lists.SelectedIndex;
                if (dataGridView_elems.CurrentCell == null) { return; }

                #region grids
                int e = dataGridView_elems.CurrentCell.RowIndex;

                int scroll = dataGridView_item.FirstDisplayedScrollingRowIndex;
                dataGridView_item.SuspendLayout();
                dataGridView_item.Rows.Clear();

                int scrollA = dataGridView_item_addons.FirstDisplayedScrollingRowIndex;
                dataGridView_item_addons.SuspendLayout();
                dataGridView_item_addons.Rows.Clear();

                int scrollR = dataGridView_item_rands.FirstDisplayedScrollingRowIndex;
                dataGridView_item_rands.SuspendLayout();
                dataGridView_item_rands.Rows.Clear();

                int scrollU = dataGridView_item_uniques.FirstDisplayedScrollingRowIndex;
                dataGridView_item_uniques.SuspendLayout();
                dataGridView_item_uniques.Rows.Clear();

                int scrollM = dataGridView_item_materials.FirstDisplayedScrollingRowIndex;
                dataGridView_item_materials.SuspendLayout();
                dataGridView_item_materials.Rows.Clear();



                
                richTextBox_DESC_POS.Clear();
                #endregion

                proctypeLocation = 0;
                proctypeLocationvak = 0;

                string teste = "";
                try
                {
                    if (l != eLC.ConversationListIndex)
                    {
                        if (e > -1)
                        {
                            dataGridView_item.Enabled = false;
                            for (int f = 0; f < eLC.Lists[l].elementValues[e].Length; f++)
                            {
                                var a = eLC.Lists[l].elementFields[f];
                                var b = eLC.Lists[l].elementTypes[f];
                                var c = eLC.GetValue(l, e, f);

                                if (a.StartsWith("addons") || a.StartsWith("skills_") || a.StartsWith("after_death") || a.StartsWith("skill_hp"))
                                {
                                    dataGridView_item_addons.Rows.Add(new string[] { a, b, c, f.ToString() });
                                    dataGridView_item_addons.Rows[add].HeaderCell.Value = add.ToString();
                                    add++;
                                    if (tabPage_addonsB == false) { tabPage_addonsB = true; tabControl_INFO.TabPages.Add(tabPage_addons); }
                                    if (a.StartsWith("addons"))
                                    {
                                        tabPage_materials.Text = "Addons";
                                    }
                                    if (a.StartsWith("skills_"))
                                    {
                                        tabPage_materials.Text = "Skills";
                                    }

                                }
                                else if (a.StartsWith("rands"))
                                {
                                    dataGridView_item_rands.Rows.Add(new string[] { a, b, c, f.ToString() });
                                    dataGridView_item_rands.Rows[rd].HeaderCell.Value = rd.ToString();
                                    rd++;
                                    if (tabPage_randsB == false) { tabPage_randsB = true; tabControl_INFO.TabPages.Add(tabPage_rands); }
                                }
                                else if (a.StartsWith("uniques"))
                                {
                                    dataGridView_item_uniques.Rows.Add(new string[] { a, b, c, f.ToString() });
                                    dataGridView_item_uniques.Rows[uq].HeaderCell.Value = uq.ToString();
                                    uq++;
                                    if (tabPage_uniquesB == false) { tabPage_uniquesB = true; tabControl_INFO.TabPages.Add(tabPage_uniques); }
                                }
                                else if (a.StartsWith("materials") || a.StartsWith("drop_matters_"))
                                {
                                    dataGridView_item_materials.Rows.Add(new string[] { a, b, c, f.ToString() });
                                    dataGridView_item_materials.Rows[mt].HeaderCell.Value = mt.ToString();
                                    mt++;
                                    if (tabPage_materialsB == false) { tabPage_materialsB = true; tabControl_INFO.TabPages.Add(tabPage_materials); }
                                    if (a.StartsWith("materials"))
                                    {
                                        tabPage_materials.Text = "Materials";
                                    }
                                    if (a.StartsWith("drop_matters_"))
                                    {
                                        tabPage_materials.Text = "Drop Matters";
                                    }
                                }

                                else
                                {

                                    dataGridView_item.Rows.Add(new string[] { a, b, c, f.ToString() });
                                    dataGridView_item.Rows[pd].HeaderCell.Value = pd.ToString();
                                    pd++;

                                }





                            }
                            //if (tabPage_returnB == false) { tabPage_returnB = true; tabControl_INFO.TabPages.Add(tabPage_return); }
                            
                            dataGridView_item.Enabled = true;
                            dataGridView_item.PerformLayout();
                            dataGridView_item.ResumeLayout();
                        }
                    }
                    else
                    {
                        if (e > -1)
                        {
                            dataGridView_item.Rows.Add(new string[] { "id_talk", "int32", conversationList.talk_procs[e].id_talk.ToString() });
                            dataGridView_item.Rows.Add(new string[] { "text", "wstring:128", conversationList.talk_procs[e].GetText() });
                            for (int q = 0; q < conversationList.talk_procs[e].num_window; q++)
                            {
                                dataGridView_item.Rows.Add(new string[] { "window_" + q + "_id", "int32", conversationList.talk_procs[e].windows[q].id.ToString() });
                                dataGridView_item.Rows.Add(new string[] { "window_" + q + "_id_parent", "int32", conversationList.talk_procs[e].windows[q].id_parent.ToString() });
                                dataGridView_item.Rows.Add(new string[] { "window_" + q + "_talk_text", "wstring:" + conversationList.talk_procs[e].windows[q].talk_text_len, conversationList.talk_procs[e].windows[q].GetText() });
                                for (int c = 0; c < conversationList.talk_procs[e].windows[q].num_option; c++)
                                {
                                    dataGridView_item.Rows.Add(new string[] { "window_" + q + "_option_" + c + "_param", "int32", conversationList.talk_procs[e].windows[q].options[c].param.ToString() });
                                    dataGridView_item.Rows.Add(new string[] { "window_" + q + "_option_" + c + "_text", "wstring:128", conversationList.talk_procs[e].windows[q].options[c].GetText() });
                                    dataGridView_item.Rows.Add(new string[] { "window_" + q + "_option_" + c + "_id", "int32", conversationList.talk_procs[e].windows[q].options[c].id.ToString() });
                                }
                            }
                        }
                    }
                    if (scroll > -1)
                    {
                        dataGridView_item.FirstDisplayedScrollingRowIndex = scroll;
                        dataGridView_item_addons.FirstDisplayedScrollingRowIndex = scrollA;
                        dataGridView_item_rands.FirstDisplayedScrollingRowIndex = scrollR;
                        dataGridView_item_uniques.FirstDisplayedScrollingRowIndex = scrollU;
                    }
                }
                catch (Exception x) {/* MessageBox.Show(x.Message);  */         }
            }
           
            DataGrid(null, null, dataGridView_item);
            DataGridView dgv = (DataGridView)sender;
            int idid = 0;
            try
            {
                idid = int.Parse(dgv.Rows[dgv.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
            }
            catch (Exception)
            {

                //throw;
            }

            add_Returne(idid);
            conf();
			_MATS = new Thread(delegate () { dataGridView_item_materials_RowPrePaint(); }); _MATS.Start();
		}

		private void conf()
		{
			int recipe = int.Parse(dataGridView_recipes.Rows.Count.ToString());
			int suite = int.Parse(dataGridView_SUITE.Rows.Count.ToString());
			int npc = int.Parse(dataGridView_npcs.Rows.Count.ToString());
			int tasks = int.Parse(dataGridView_tasks.Rows.Count.ToString());
			int gshop = int.Parse(dataGridView_gshop.Rows.Count.ToString());
			int desc = 0;
			if (richTextBox_DESC_POS.Text.Length > 0)
			{
				desc = 1;
			}

			string name = tabPage_return.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0];

			tabPage_return.Text = tabPage_return.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0] + " - (" + (recipe + suite + npc + desc + tasks+ gshop) + ")";

			if (eLC.Lists[comboBox_lists.SelectedIndex].listName.ToString().EndsWith("_ESSENCE"))
			{
				tabControl_INFO.TabPages.Add(tabPage_return);
			}
			else
			{
				tabControl_INFO.TabPages.Remove(tabPage_return);
			}
		}
		private void change_list(object sender, EventArgs ea)
        {
            pictureBox_icon.Image = database.images("unknown.dds");

            for (int xx = 0; xx < MainWindow_OLD.database.ItemUse.Count; xx++)
            {
                if (int.Parse(MainWindow_OLD.database.ItemUse.GetKey(xx).ToString()) == comboBox_lists.SelectedIndex)
                {
                    addItemRecipeToolStripMenuItem.Visible = true;
                    break;
                }
                else
                {
                    addItemRecipeToolStripMenuItem.Visible = false;
                }

            }

            bool test = false;
            for (int i = 0; i < MainWindow_OLD.database.ItemUse.Count; i++)
            {
                if (int.Parse(MainWindow_OLD.database.ItemUse.GetKey(i).ToString()) == comboBox_lists.SelectedIndex)
                {
                    test = true;
                    break;
                }
                else
                {
                    test = false;
                }

            }
            int IndexOfRow = 0;
            if (comboBox_lists.SelectedIndex > -1 && EnableSelectionList)
            {
                int l = comboBox_lists.SelectedIndex;
                dataGridView_elems.Rows.Clear();
                textBox_offset = eLC.GetOffset(l);

                dataGridView_item.Rows.Clear();
                dataGridView_item_addons.Rows.Clear();
                dataGridView_item_rands.Rows.Clear();
                dataGridView_item_uniques.Rows.Clear();
                dataGridView_item_materials.Rows.Clear();


                if (l != eLC.ConversationListIndex)
                {
                    // Find Position for Name
                    int pos = -1;
                    int pos2 = -1;

                    for (int i = 0; i < eLC.Lists[l].elementFields.Length; i++)
                    {
                        if (eLC.Lists[l].elementFields[i] == "Name")
                        {
                            pos = i;
                        }
                        if (eLC.Lists[l].elementFields[i] == "file_icon" || eLC.Lists[l].elementFields[i] == "file_icon1")
                        {
                            pos2 = i;
                        }
                        if (pos != -1 && pos2 != -1) { break; }
                    }
                    bool fim = false;
                    for (int e = 0; e < eLC.Lists[l].elementValues.Length; e++)
                    {

                        if (eLC.Lists[l].elementFields[0] == "ID")
                        {

                            Bitmap img = Properties.Resources.blank;
                            string path = Path.GetFileName(eLC.GetValue(l, e, pos2));
                            if (database.sourceBitmap != null && database.ContainsKey(path))
                            {
                                if (database.ContainsKey(path))
                                {
                                    img = database.images(path);
                                }
                            }


                            dataGridView_elems.Rows.Add(new object[] { eLC.GetValue(l, e, 0), img, eLC.GetValue(l, e, pos) });



                        }
                        else
                        {

                            dataGridView_elems.Rows.Add(new object[] { 0, Properties.Resources.unknown, eLC.GetValue(l, e, pos) });

                        }

                        // button_SETS.Text = comboBox_lists.SelectedIndex.ToString() + " | "+ eLC.Lists[l].elementValues.Length + " | " + e+1;





                    }

                }
                else
                {
                    for (int e = 0; e < conversationList.talk_proc_count; e++)
                    {
                        dataGridView_elems.Rows.Add(new object[] { conversationList.talk_procs[e].id_talk, Properties.Resources.unknown, conversationList.talk_procs[e].id_talk + " - Dialog" });
                    }
                }


                if (comboBox_lists.SelectedIndex == 54 || comboBox_lists.SelectedIndex == 40)
                {
                    pictureBox_BOX.Visible = true;
                }
                else
                {
                    pictureBox_BOX.Visible = false;
                }
                change_item(null, null);
                //    });
                //}); LoadElemtnts.Start();

            }
        }
        void ReadTask()
        {
            if (File.Exists(MainWindow_OLD.XmlData.TasksDataPath))
            {
                ATaskTempl[] Tasks =null;

                 FileStream input = File.OpenRead(MainWindow_OLD.XmlData.TasksDataPath);
                BinaryReader binaryStream = new BinaryReader(input);
                TASK_PACK_HEADER tph = new TASK_PACK_HEADER(binaryStream);
                if (tph.magic == -1819966623 || tph.magic == 0)
                {
                    if (!GlobalData.Versions.Contains(tph.version))
                    {
                        binaryStream.Close();
                        input.Close();


                    }
                    else
                    {

                        GlobalData.NewID = 0;
                        GlobalData.version = tph.version;

                        int[] pOffs = new int[tph.item_count];
                        for (int i = 0; i < tph.item_count; i++)
                        {
                            pOffs[i] = binaryStream.ReadInt32();
                        }
                        Tasks = new ATaskTempl[tph.item_count];

                        IProgress<int> progress = new Progress<int>(value =>
                        {

                        });
                        if (true)
                        {
                            var p = 0;
                            Parallel.For(0, tph.item_count, i =>
                            {
                                byte[] bytes = null;
                                lock (binaryStream)
                                {
                                    binaryStream.BaseStream.Seek(pOffs[i], SeekOrigin.Begin);
                                    int count = ((i < pOffs.Length - 1)
                                                    ? pOffs[i + 1]
                                                    : (int)binaryStream.BaseStream.Length) - pOffs[i];
                                    bytes = binaryStream.ReadBytes(count);
                                }

                                using (var ms = new MemoryStream(bytes))
                                using (var br = new BinaryReader(ms))
                                {
                                    if (true)
                                    {
                                        try
                                        {
                                            Tasks[i] = new ATaskTempl(tph.version, br);
                                        }
                                        catch (Exception e)
                                        {
                                            //MessageBox.Show(String.Format(GetLocalization(521), i));
                                            //if (Debug)
                                            //    Extensions.WriteLog("Error load task! Task index: " + i);
                                        }
                                    }
                                    else
                                        Tasks[i] = new ATaskTempl(tph.version, br);
                                }

                                Interlocked.Increment(ref p);
                                if (p % 100 == 0)
                                {
                                    progress.Report(p / 2);
                                    Application.DoEvents();
                                }
                            }); //3.5 мс для 146 версии
                        }
                        else
                        {
                            for (int i = 0; i < tph.item_count; i++)
                            {
                                // Tasks[i] = new ATaskTempl(tph.version, binaryStream, pOffs[i]);
                                if (i % 100 == 0)
                                {
                                    progress.Report(i / 2);
                                    Application.DoEvents();
                                }
                            }
                        }

                        binaryStream.Close();
                        input.Close();

                        Tasks = Tasks.Where(it => it != null).ToArray();

                        void add_node(ATaskTempl[] tasks, TreeNodeCollection nodes, int GMIconIndex)
                        {
                            for (var i = 0; i < tasks.Length; i++)
                            {
                                tasks[i].AddNode(nodes, GMIconIndex);
                                if (tasks[i].pSub.Length > 0)
                                    add_node(tasks[i].pSub, nodes[i].Nodes, GMIconIndex);
                            }
                        }



                    }
                }
                else
                {
                    binaryStream.Close();
                    input.Close();
                }

                database.Tasks = Tasks;
            }
            else
            {
                database.Tasks = null;
            }
        }
		void ReadShops()
		{
			if (File.Exists(MainWindow_OLD.XmlData.GshopDataPath))
			{
				try
				{
					FileGshop FileGshop = new FileGshop();
					FileGshop.ReadFile(XmlData.GshopDataPath, 0);
					database.Gshop = FileGshop;
				}
				catch (Exception)
				{

					
				}
			}
			if (File.Exists(MainWindow_OLD.XmlData.Gshop1DataPath))
			{
				try
				{
					FileGshop FileGshop1 = new FileGshop();
					FileGshop1.ReadFile(XmlData.Gshop1DataPath, 0);
					database.GshopEvent = FileGshop1;
				}
				catch (Exception)
				{


				}
			}
		}
		void LoadElementData()
        {

            string file = XmlData.ElementsDataPath;
            comboBox_lists.Invoke((MethodInvoker)delegate
            {

                try
                {
                    //Cursor = Cursors.AppStarting;
                    //progressBar_progress.Style = ProgressBarStyle.Continuous;

                    eLC = new eListCollection(file, ref cpb2);

					SortedList ItemUse = new SortedList();
					for (int i = 0; i < eLC.Lists.Length; i++)
					{
						if (eLC.Lists[i].itemUse==true)
						{
							if (!ItemUse.ContainsKey(i))
							{
								ItemUse.Add(i, i);
							}
							
						}
						
					}
					database.ItemUse = ItemUse;

					this.exportContainerToolStripMenuItem.DropDownItems.Clear();

                    // search for available export rules
                    if (eLC.ConfigFile != null)
                    {
                        this.exportContainerToolStripMenuItem.DropDownItems.Add(new ToolStripLabel("Select a valid Conversation Rules Set"));
                        this.exportContainerToolStripMenuItem.DropDownItems[0].Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        this.exportContainerToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
                        string[] files = Directory.GetFiles(Application.StartupPath + "\\rules", "PW_v" + eLC.Version.ToString() + "*.rules");
                        for (int i = 0; i < files.Length; i++)
                        {
                            files[i] = files[i].Replace("=", "=>");
                            files[i] = files[i].Replace(".rules", "");
                            files[i] = files[i].Replace(Application.StartupPath + "\\rules\\", "");
                            this.exportContainerToolStripMenuItem.DropDownItems.Add(files[i], null, new EventHandler(this.click_export));
                        }
                    }
                    // load cross references list
                    if (eLC.ConfigFile != null)//Вроде работает
                    {
                        string[] referencefiles = Directory.GetFiles(Application.StartupPath + "\\configs", "references.txt");
                        if (referencefiles.Length > 0)
                        {
                            StreamReader sr = new StreamReader(referencefiles[0]);
                            char[] chars = { ';', ',' };
                            string[] x;
                            xrefs = new string[eLC.Lists.Length][];
                            string line;
                            while (!sr.EndOfStream)
                            {
                                line = sr.ReadLine();
                                if (!line.StartsWith("#"))
                                {
                                    x = line.Split(chars);
                                    if (int.Parse(x[0]) < eLC.Lists.Length)
                                    {
                                        xrefs[int.Parse(x[0])] = x;
                                    }
                                }
                            }
                            this.toolStripSeparator6.Visible = true;
                            // this.xrefItemToolStripMenuItem.Visible = true;
                        }
                    }

                    if (eLC.ConversationListIndex > -1 && eLC.Lists.Length > eLC.ConversationListIndex)
                    {
                        conversationList = new eListConversation((byte[])eLC.Lists[eLC.ConversationListIndex].elementValues[0][0]);
                    }

                    dataGridView_item.Rows.Clear();
                    comboBox_lists.Items.Clear();
                    comboBox_ref.Items.Clear();

                    for (int l = 0; l < eLC.Lists.Length; l++)
                    {

                        comboBox_lists.Items.Add("[" + l + "] " + eLC.Lists[l].listName.Split(new string[] { " - " }, StringSplitOptions.None)[1] + " (" + eLC.Lists[l].elementValues.Length + ")");

                        if (eLC.Lists[l].listName.Split(new string[] { " - " }, StringSplitOptions.None)[1].EndsWith("ESSENCE"))
                        {
                            comboBox_ref.Items.Add("[" + l + "] - " + eLC.Lists[l].listName.Split(new string[] { " - " }, StringSplitOptions.None)[1]);
                        }
                        //imageCombo1.Items.Add(new ImageCombo( "[" + l + "] " + eLC.Lists[l].listName.Split(new string[] { " - " }, StringSplitOptions.None)[1] + " (" + eLC.Lists[l].elementValues.Length + ")" ));
                    }
                    string timestamp = "";
                    if (eLC.Lists[0].listOffset.Length > 0)
                        timestamp = ", Timestamp: " + timestamp_to_string(BitConverter.ToUInt32(eLC.Lists[0].listOffset, 0));
                    this.Text = " sELedit++ 2.0(" + file + " [Version: " + eLC.Version.ToString() + timestamp + "])";
                    ElementsPath = file;

                    cpb2.Value = 0;
                    //progressBar_progress.Style = ProgressBarStyle.Continuous;

                    comboBox_lists.SelectedIndex = 54;

                    //Cursor = Cursors.Default;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + "\n\n\n" + e);
                    //throw;
                    //MessageBox.Show(eListCollection.SStat[0].ToString() + "\n" + eListCollection.SStat[1].ToString() + "\n" + eListCollection.SStat[2].ToString());
                    //   MessageBox.Show("LOADING ERROR!\n\nThis error usually occurs if incorrect configuration, structure, or encrypted elements.data file...\nIf you are using elements.list.count trying to decrypt, its likely the last list item count is incorrect... \nUse details below to assist... \n\nRead Failed at this point :\n" + eListCollection.SStat[0].ToString() + " - List #\n" + eListCollection.SStat[1].ToString() + " - # Items This List\n" + eListCollection.SStat[2].ToString() + " - Item ID");
                    //progressBar_progress.Style = ProgressBarStyle.Continuous;
                    //   Cursor = Cursors.Default;
                }
            });


            //  LoadsAdds();

           
           






        }
       
            
           


        private void change_value_NEW(object sender, DataGridViewCellEventArgs ea)
        {
            DataGridView grid = (DataGridView)sender;
            try
            {
                if (eLC != null && ea.ColumnIndex > -1)
                {
                    int l = comboBox_lists.SelectedIndex;
                    int f = int.Parse(grid.Rows[ea.RowIndex].Cells[3].Value.ToString());//ea.RowIndex;
                    int r = dataGridView_elems.CurrentCell.RowIndex;
                    string _set = string.Empty;

                    if (grid.Rows[ea.RowIndex].Cells[2].Value.ToString().Contains("[") && grid.Rows[ea.RowIndex].Cells[2].Value.ToString().Contains("]"))
                    {
                        var _set_v = Convert.ToString(grid.Rows[ea.RowIndex].Cells[2].Value.ToString()).Replace("[", "").Replace("] ", "").Split('-');
                        _set = _set_v[0].Replace(" ", "");
                    }
                    else
                    {
                        _set = Convert.ToString(grid.Rows[ea.RowIndex].Cells[2].Value.ToString());
                    }


                    if (true)
                    {

                    }
                    if (l != eLC.ConversationListIndex)
                    {
                        EnableSelectionItem = false;
                        int[] selIndices = gridSelectedIndices(dataGridView_elems);
                        for (int e = 0; e < selIndices.Length; e++)
                        {
                            if (f == 23)
                            {

                            }
                            eLC.SetValue(l, selIndices[e], f, _set);

                            if (grid == dataGridView_item)
                            {
                                for (int a = 0; a < selIndices.Length; a++)
                                {
                                    if (dataGridView_item.Rows[7].Cells[0].Value.ToString() == "ID" || dataGridView_item.Rows[a].Cells[0].Value.ToString() == "Name" || dataGridView_item.Rows[a].Cells[0].Value.ToString() == "file_icon" || dataGridView_item.Rows[a].Cells[0].Value.ToString() == "file_icon1")
                                    {
                                        // change the values in the listbox depending on new name & id

                                        // Find Position for Name
                                        int pos = -1;
                                        int pos2 = -1;
                                        for (int i = 0; i < eLC.Lists[l].elementFields.Length; i++)
                                        {
                                            if (eLC.Lists[l].elementFields[i] == "Name")
                                            {
                                                pos = i;
                                            }
                                            if (eLC.Lists[l].elementFields[i] == "file_icon" || eLC.Lists[l].elementFields[i] == "file_icon1")
                                            {
                                                pos2 = i;
                                            }
                                            if (pos != -1 && pos2 != -1)
                                            {
                                                break;
                                            }
                                        }
                                        Bitmap img = Properties.Resources.blank;
                                        string path = Path.GetFileName(eLC.GetValue(l, selIndices[e], pos2));
                                        if (database.sourceBitmap != null && database.ContainsKey(path))
                                        {
                                            if (database.ContainsKey(path))
                                            {
                                                img = database.images(path);
                                            }
                                        }

                                        dataGridView_elems.Rows[selIndices[e]].Cells[0].Value = eLC.GetValue(l, selIndices[e], 0);
                                        dataGridView_elems.Rows[selIndices[e]].Cells[1].Value = img;
                                        dataGridView_elems.Rows[selIndices[e]].Cells[2].Value = eLC.GetValue(l, selIndices[e], pos);
                                    }

                                }

                            }
                        }
                        EnableSelectionItem = true;
                        
                    }
                    else
                    {
                        //TALK_PROCs check which item was changed by field name
                        string fieldName = dataGridView_item[0, ea.RowIndex].Value.ToString();

                        if (fieldName == "id_talk")
                        {
                            conversationList.talk_procs[r].id_talk = Convert.ToInt32(dataGridView_item.CurrentCell.Value);
                            return;
                        }
                        if (fieldName == "text")
                        {
                            conversationList.talk_procs[r].SetText(dataGridView_item.CurrentCell.Value.ToString());
                            return;
                        }
                        if (fieldName.StartsWith("window_") && fieldName.EndsWith("_id"))
                        {
                            int q = Convert.ToInt32(fieldName.Replace("window_", "").Replace("_id", ""));
                            conversationList.talk_procs[r].windows[q].id = Convert.ToInt32(dataGridView_item.CurrentCell.Value);
                            return;
                        }
                        if (fieldName.StartsWith("window_") && fieldName.Contains("option_") && fieldName.EndsWith("_param"))
                        {
                            string[] s = fieldName.Replace("window_", "").Replace("_option_", ";").Replace("_param", "").Split(new char[] { ';' });
                            int q = Convert.ToInt32(s[0]);
                            int c = Convert.ToInt32(s[1]);
                            conversationList.talk_procs[r].windows[q].options[c].param = Convert.ToInt32(dataGridView_item.CurrentCell.Value);
                            return;
                        }
                        if (fieldName.StartsWith("window_") && fieldName.Contains("option_") && fieldName.EndsWith("_text"))
                        {
                            string[] s = fieldName.Replace("window_", "").Replace("_option_", ";").Replace("_text", "").Split(new char[] { ';' });
                            int q = Convert.ToInt32(s[0]);
                            int c = Convert.ToInt32(s[1]);
                            conversationList.talk_procs[r].windows[q].options[c].SetText(dataGridView_item.CurrentCell.Value.ToString());
                            return;
                        }
                        if (fieldName.StartsWith("window_") && fieldName.Contains("option_") && fieldName.EndsWith("_id"))
                        {
                            string[] s = fieldName.Replace("window_", "").Replace("_option_", ";").Replace("_id", "").Split(new char[] { ';' });
                            int q = Convert.ToInt32(s[0]);
                            int c = Convert.ToInt32(s[1]);
                            conversationList.talk_procs[r].windows[q].options[c].id = Convert.ToInt32(dataGridView_item.CurrentCell.Value);
                            return;
                        }
                        if (fieldName.StartsWith("window_") && fieldName.EndsWith("_id_parent"))
                        {
                            int q = Convert.ToInt32(fieldName.Replace("window_", "").Replace("_id_parent", ""));
                            conversationList.talk_procs[r].windows[q].id_parent = Convert.ToInt32(dataGridView_item.CurrentCell.Value);
                            return;
                        }
                        if (fieldName.StartsWith("window_") && fieldName.EndsWith("_talk_text"))
                        {
                            int q = Convert.ToInt32(fieldName.Replace("window_", "").Replace("_talk_text", ""));
                            conversationList.talk_procs[r].windows[q].SetText(dataGridView_item.CurrentCell.Value.ToString());
                            dataGridView_item[1, ea.RowIndex].Value = "wstring:" + conversationList.talk_procs[r].windows[q].talk_text_len;
                            return;
                        }
                    }
                    _MATS = new Thread(delegate () { dataGridView_item_materials_RowPrePaint(); }); _MATS.Start();
                }
            }
            catch (Exception exs)
            {
                throw;
                MessageBox.Show("CHANGING ERROR!\nFailed changing value, this value seems to be invalid.");
            }


        }


       
        private void pictureBox_BOX_Click(object sender, EventArgs e)
        {
           

            if (comboBox_lists.SelectedIndex==54)
            {
                NOVO.ForjaShop shop54;
             
                #region shop54

                Npc_MAKER[] _Npc_MAKER = new Npc_MAKER[8];
                Itens_Npc_MAKER[] _Itens_Npc_MAKER = new Itens_Npc_MAKER[32];
                int lineI = 0; int lineT = 0;

                int _lineI = 1; int _lineT = 1;
                int[] listItem;
               

                for (int i = 0; i < dataGridView_item.Rows.Count; i++)
                {
                    string name = dataGridView_item.Rows[i].Cells[0].Value.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0].Replace("[", "").Replace("]", "").Replace(" ", "");
                    string type = dataGridView_item.Rows[i].Cells[1].Value.ToString();
                    string value = dataGridView_item.Rows[i].Cells[2].Value.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0].Replace("[", "").Replace("]", "").Replace(" ", "");
                    
                    if (name.StartsWith("pages_" + _lineT) && name.EndsWith("_page_title"))
                    {
                        _Npc_MAKER[lineT] = new Npc_MAKER();
                        _Npc_MAKER[lineT].Title = value;
                        _Npc_MAKER[lineT].IdItem = listItem = new int[32]; 

                    }
                    if (name.StartsWith("pages_" + _lineT + "_id_goods_" + _lineI))
                    {
                        _Npc_MAKER[lineT].IdItem[_lineI-1]= int.Parse(value);
                       
                        lineI++;
                        _lineI++;

                        if (_lineI == 32)
                        {
                            _lineI = 1;
                            lineT++;
                            _lineT++;
                        }
                    }



                }

                if (_Npc_MAKER!= null)
                {
                    shop54 = new ForjaShop(_Npc_MAKER);
                    shop54.ShowDialog(this);
                    _Npc_MAKER = shop54.Npc_MAKER;


                     lineI = 0;  lineT = 0;

                     _lineI = 1;  _lineT = 1;

                    for (int i = 0; i < dataGridView_item.Rows.Count; i++)
                    {
                        string name = dataGridView_item.Rows[i].Cells[0].Value.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0].Replace("[", "").Replace("]", "").Replace(" ", "");
                        string type = dataGridView_item.Rows[i].Cells[1].Value.ToString();
                        string value = dataGridView_item.Rows[i].Cells[2].Value.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0].Replace("[", "").Replace("]", "").Replace(" ", "");

                        if (name.StartsWith("pages_" + _lineT) && name.EndsWith("_page_title"))
                        {
                            dataGridView_item.Rows[i].Cells[2].Value = _Npc_MAKER[lineT].Title;
                        }
                        if (name.StartsWith("pages_" + _lineT + "_id_goods_" + _lineI))
                        {
                            dataGridView_item.Rows[i].Cells[2].Value = _Npc_MAKER[lineT].IdItem[_lineI - 1];

                            lineI++;
                            _lineI++;

                            if (_lineI == 32)
                            {
                                _lineI = 1;
                                lineT++;
                                _lineT++;
                            }
                        }



                    }
                    //_MATS = new Thread(delegate () { dataGridView_item_materials_RowPrePaint(); }); _MATS.Start();
                }

                #endregion
               
               
                
                



            }

            if (comboBox_lists.SelectedIndex == 40)
            {
                NOVO.SELL shop40;

               

                Npc_MAKER[] _Npc_MAKER = new Npc_MAKER[8];
                Itens_Npc_MAKER[] _Itens_Npc_MAKER = new Itens_Npc_MAKER[32];
                int lineI = 0; int lineT = 0;

                int _lineI = 1; int _lineT = 1;
                int[] listItem;


                for (int i = 0; i < dataGridView_item.Rows.Count; i++)
                {
                    string name = dataGridView_item.Rows[i].Cells[0].Value.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0].Replace("[", "").Replace("]", "").Replace(" ", "");
                    string type = dataGridView_item.Rows[i].Cells[1].Value.ToString();
                    string value = dataGridView_item.Rows[i].Cells[2].Value.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0].Replace("[", "").Replace("]", "").Replace(" ", "");

                    if (name.StartsWith("pages_" + _lineT) && name.EndsWith("_page_title"))
                    {
                        _Npc_MAKER[lineT] = new Npc_MAKER();
                        _Npc_MAKER[lineT].Title = value;
                        _Npc_MAKER[lineT].IdItem = listItem = new int[32];

                    }
                    if (name.StartsWith("pages_" + _lineT) && name.EndsWith(_lineI +"_id"))
                    {
                        _Npc_MAKER[lineT].IdItem[_lineI - 1] = int.Parse(value);

                        lineI++;
                        _lineI++;

                        if (_lineI == 32)
                        {
                            _lineI = 1;
                            lineT++;
                            _lineT++;
                        }
                    }



                }

                if (_Npc_MAKER != null)
                {
                    shop40 = new SELL(_Npc_MAKER);
                    shop40.ShowDialog(this);
                    _Npc_MAKER = shop40.Npc_MAKER;


                    lineI = 0; lineT = 0;

                    _lineI = 1; _lineT = 1;

                    for (int i = 0; i < dataGridView_item.Rows.Count; i++)
                    {
                        string name = dataGridView_item.Rows[i].Cells[0].Value.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0].Replace("[", "").Replace("]", "").Replace(" ", "");
                        string type = dataGridView_item.Rows[i].Cells[1].Value.ToString();
                        string value = dataGridView_item.Rows[i].Cells[2].Value.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0].Replace("[", "").Replace("]", "").Replace(" ", "");

                        if (name.StartsWith("pages_" + _lineT) && name.EndsWith("_page_title"))
                        {
                            dataGridView_item.Rows[i].Cells[2].Value = _Npc_MAKER[lineT].Title;
                        }
                        if (name.StartsWith("pages_" + _lineT) && name.EndsWith(_lineI + "_id"))
                        {
                            dataGridView_item.Rows[i].Cells[2].Value = _Npc_MAKER[lineT].IdItem[_lineI - 1];

                            lineI++;
                            _lineI++;

                            if (_lineI == 32)
                            {
                                _lineI = 1;
                                lineT++;
                                _lineT++;
                            }
                        }



                    }
                    //_MATS = new Thread(delegate () { dataGridView_item_materials_RowPrePaint(); }); _MATS.Start();
                }
            }  
		}

		void ReadAdds(DataGridView dataGrid)
		{			
			ProbabilityEditorWindow addonss;
			ElementProbability[] Elements = null;
			Elements = new ElementProbability[dataGrid.Rows.Count];
			int line = 0;
			for (int i = 0; i < dataGrid.Rows.Count; i++)
			{
				string name = dataGrid.Rows[i].Cells[0].Value.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0].Replace("[", "").Replace("]", "").Replace(" ", "");
				string type = dataGrid.Rows[i].Cells[1].Value.ToString();
				string value = dataGrid.Rows[i].Cells[2].Value.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0].Replace("[", "").Replace("]", "").Replace(" ", "");
				string valueP = dataGrid.Rows[i+1].Cells[2].Value.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0].Replace("[", "").Replace("]", "").Replace(" ", "");

				if (type.Contains("int32"))
				{
					if (value!="0")
					{
						Elements[line] = new ElementProbability();
						Elements[line].Id = int.Parse(value);
						Elements[line].Probability = float.Parse(valueP);
						
						line++;
					}
					i++;
				}
							   				 
			}
			if (Elements != null)
			{
				Elements = Elements.Where(a => a != null).ToArray();
				addonss = new NOVO.ProbabilityEditorWindow(Elements);
				addonss.ShowDialog();
				line = 0;
				Elements = addonss.Elements;
				for (int i = 0; i < dataGrid.Rows.Count; i++)
				{
					string type = dataGrid.Rows[i].Cells[1].Value.ToString();

					if (type.Contains("int32"))
					{
						if (line < Elements.Length)
						{
							dataGrid.Rows[i].Cells[2].Value = Elements[line].Id;
							dataGrid.Rows[i + 1].Cells[2].Value = Elements[line].Probability;
							line++;
						}
						
					}
					i++;
				}
			}



			//dataGridView_Elements.Rows[i].Cells[3].Value 
		}



		
		
		private void pictureBox_addons_Click(object sender, EventArgs e)
        {
			ReadAdds(dataGridView_item_addons);	

			//string addons_1_id_addon = null;
			//string addons_1_id_addon_ID = null;
			//string addons_1_id_addon_type = null;

			//string addons_1_probability_addon = null;
			//string addons_1_probability_addon_value = null;
			//string addons_1_probability_addon_value_type = null;
			//int cont = 1;
			//int line = 0;
			//ElementProbability[] Elements = null;
			//Elements = new ElementProbability[dataGridView_item_addons.Rows.Count/2];
			//foreach (DataGridViewRow row in dataGridView_item_addons.Rows)
			//{
			//	if (row.Cells[0].Value.ToString() == "addons_" + cont + "_id_addon")
			//	{
			//		addons_1_id_addon = row.Cells[0].Value.ToString();// name
			//		addons_1_id_addon_ID = row.Cells[2].Value.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0].Replace("[", "").Replace("]", "").Replace(" ", "");// value
			//		addons_1_id_addon_type = row.Cells[1].Value.ToString();// value
			//	}
			//	if (row.Cells[0].Value.ToString() == "addons_" + cont + "_probability_addon")
			//	{
			//		addons_1_probability_addon = row.Cells[0].Value.ToString();// name
			//		addons_1_probability_addon_value = row.Cells[2].Value.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0].Replace("[", "").Replace("]", "").Replace(" ", "");// value
			//		addons_1_probability_addon_value_type = row.Cells[1].Value.ToString();// value

			//		if (int.Parse(addons_1_id_addon_ID) > 0)
			//		{
			//			Elements[line] = new ElementProbability();
			//			Elements[line].Id = int.Parse(addons_1_id_addon_ID);
			//			Elements[line].Probability = float.Parse(addons_1_probability_addon_value); //(new ElementProbability {Id= int.Parse(addons_1_id_addon_ID) , Probability=   float.Parse(addons_1_probability_addon_value) });

			//			//addItem(addons_1_id_addon_ID, addons_1_id_addon_ID, addons_1_probability_addon_value, line);
			//			line++;
			//		}
			//		cont++;
			//	}
			//}

			//if (Elements != null)
			//{

			//	Elements  = Elements.Where(a => a != null).ToArray();
			//	addonss = new NOVO.ProbabilityEditorWindow(Elements);
			//	addonss.ShowDialog();
			//}

		}



        private void comboBoxA1_SelectedIndexChanged(object sender, EventArgs ex)
        {
            string[] txt = comboBox_ref.Text.Replace("[", "").Replace("]", "").Split('-');
            comboBox_lists.SelectedIndex = int.Parse(txt[0]);
        }

        LISTAS l;
        private void dataGridView_elems_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs ee)
        {
            l = new LISTAS();

            for (int i = 0; i < l.Legal.Length; i++)
            {
                if (comboBox_lists.SelectedIndex == l.Legal[i])
                {
                    Color a;
                    try
                    { a = Helper.getByID(database.item_color[int.Parse(dataGridView_elems.Rows[ee.RowIndex].Cells[0].Value.ToString())]); }
                    catch (Exception)
                    { a = Color.White; }

                    dataGridView_elems.Rows[ee.RowIndex].Cells[2].Style.ForeColor = a;
                }
            }
            

        }


        public void DataGrid(object sender, DataGridViewCellEventArgs e, DataGridView gridView)
        {

            foreach (DataGridViewRow item in gridView.Rows)
            {
                Color cl;
                switch (item.Cells[0].Value.ToString())
                {
                    case "ID":
                        numericUpDownEx_ID.Value = decimal.Parse(item.Cells[2].Value.ToString());
                        break;
                    case "Name":
                        try
                        { cl = Helper.getByID(database.item_color[int.Parse(numericUpDownEx_ID.Value.ToString())]); }
                        catch (Exception)
                        { cl = Color.White; }
                        textBox_NAME.Text = item.Cells[2].Value.ToString();
                        textBox_NAME.ForeColor = cl;
                        break;
                    case "file_icon":
                        if (database.ContainsKey(Path.GetFileName(item.Cells[2].Value.ToString())))
                        { pictureBox_icon.Image = database.images(Path.GetFileName(item.Cells[2].Value.ToString()));  }
                        else { pictureBox_icon.Image = database.images("unknown.dds"); }
                        break;

                    default:
                        break;
                }





            }

        }



        string typeItem; DataGridView dgV;
        private void dataGridView_GET_SET(object sender, EventArgs e)
        {

            numericUpDownEx_value.Value = 0; textBox_SetValue.Text = "Set Value";
            DataGridView dgv = (DataGridView)sender;
            dgV = dgv;
            string value = "";
            string type = "";
            string name = "";

            if (dgv.SelectedRows.Count > 0)
            {

                name = dgv.Rows[dgv.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                string[] a = dgv.Rows[dgv.SelectedCells[0].RowIndex].Cells[1].Value.ToString().Split(':');
                type = a[0];
                typeItem = type;
            }

            //GetVaclue
            var tt_1 = dgv.Rows[dgv.SelectedCells[0].RowIndex].Cells[2].Value.ToString().Replace("[", "").Replace("]", "").Split('-');
            value = tt_1[0];

            if (type.StartsWith("int"))
            {
                numericUpDownEx_value.SetDecimalPlaces = 0;
                numericUpDownEx_value.SetThousandsSeparator = false;
                numericUpDownEx_value.MaximalValue = Int32.MaxValue;
                numericUpDownEx_value.MinimalValue = 0;
                numericUpDownEx_value.Increment = 1;
                numericUpDownEx_value.Value = decimal.Parse(value);
                //2 8
                
            }
            if (type.StartsWith("float"))
            {
                numericUpDownEx_value.SetDecimalPlaces = 6;
                numericUpDownEx_value.SetThousandsSeparator = true;
                numericUpDownEx_value.MaximalValue = 1;
                numericUpDownEx_value.MinimalValue = decimal.Parse("0,000000");
                numericUpDownEx_value.Increment = 0.25M;
                numericUpDownEx_value.Value = decimal.Parse(value);

            }
            if (type.StartsWith("string") || type.StartsWith("wstring"))
            {
                textBox_SetValue.Text = value.ToString();
            }


        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (typeItem.Contains("int"))
                {
                    numericUpDownEx_value.Value = decimal.Parse((sender as Button).Text);
                }
            }
            catch (Exception)
            {

                //throw;
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            DataGridView gridView = new DataGridView();
            gridView = dataGridView_item_materials;

            MessageBox.Show(gridView.Rows[gridView.SelectedCells[0].RowIndex].Cells[2].Value.ToString());

        }


		private void dataGridView_item_materials_RowPrePaint()
		{
			List<DataGridView> dataGrids = new List<DataGridView>(); dataGrids.Add(dataGridView_item); dataGrids.Add(dataGridView_item_materials);
            bool xds = false;
            bool xds2 = false;

            comboBox_lists.Invoke((MethodInvoker)delegate () {

                xds = comboBox_lists.SelectedIndex != 54;
                xds2 = comboBox_lists.SelectedIndex != 40;


            });

            if (xds==xds2)
            {

                for (int dg = 0; dg < dataGrids.Count; dg++)
                {
                    DataGridView dgv = dataGrids[dg];
                    if (dgv.Rows.Count > 0)
                    {
                        Bitmap bitmap;

                        foreach (DataGridViewRow e in dgv.Rows)
                        {
                            try
                            {

                                string a, tt; int b = 0;
                                var tt_1 = dgv.Rows[e.Index].Cells[2].Value.ToString().Replace("[", "").Replace("]", "").Split('-');
                                a = dgv.Rows[e.Index].Cells[0].Value.ToString();
                                int ret;
                                tt = tt_1[0];
                                if (int.TryParse(tt, out ret)) { b = int.Parse(tt_1[0].Replace(" ", "")); }
                                int cds;
                                var cfd = a.Split(new string[] { "_" }, StringSplitOptions.None);
                                int linha = 0;



                                if (b != 0)
                                {
                                    bool fi = false;
                                    if (a != ("character_combo_id") && a.EndsWith("id") || a.EndsWith("id_to_make") || a.StartsWith("id_upgrade_equip"))
                                    {
                                        if (b != 0)
                                        {
                                            try
                                            {
                                                string value = "";
                                                for (int L = 0; L < database.ItemUse.Count; L++)
                                                {

                                                    int La = int.Parse(database.ItemUse.GetKey(L).ToString());// Extensions.Legal[L];
                                                    int pos = 0;
                                                    int posN = 0;
                                                    for (int i = 0; i < eLC.Lists[La].elementFields.Length; i++)
                                                    {
                                                        if (eLC.Lists[La].elementFields[i] == "Name")
                                                        {
                                                            posN = i;
                                                            //break;
                                                        }
                                                        if (eLC.Lists[La].elementFields[i] == "file_icon")
                                                        {
                                                            pos = i;
                                                            break;
                                                        }

                                                    }
                                                    for (int ef = 0; ef < eLC.Lists[La].elementValues.Length; ef++)
                                                    {
                                                        value = eLC.GetValue(La, ef, pos);

                                                        if (b == int.Parse(eLC.GetValue(La, ef, 0))/* || value.Contains(b.ToString())*/)
                                                        {
                                                            string path = Path.GetFileName(value);
                                                            if (database.sourceBitmap != null && database.ContainsKey(path))
                                                            {
                                                                if (database.ContainsKey(path))
                                                                {
                                                                    if (dgv.Rows[e.Index].Cells[2].Value.ToString() == "0")
                                                                    {
                                                                        ((TextAndImageCell)dgv.Rows[e.Index].Cells[2]).Image = null;
                                                                    }
                                                                    else
                                                                    {
                                                                        ((TextAndImageCell)dgv.Rows[e.Index].Cells[2]).Image = Extensions.ResizeImage(database.images(path), 18, 18);
                                                                    }

                                                                    dgv.Rows[e.Index].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + eLC.GetValue(La, ef, posN);
                                                                    fi = true;

                                                                    Color clr;
                                                                    try
                                                                    { clr = Helper.getByID(database.item_color[int.Parse(b.ToString())]); }
                                                                    catch (Exception)
                                                                    { clr = Color.White; }

                                                                    dgv.Rows[e.Index].Cells[2].Style.ForeColor = clr;

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
                                            catch (Exception ex)
                                            {

                                                //MessageBox.Show(ex.Message + "\n" + linha);
                                            }
                                        }

                                    }
                                    if (a == "character_combo_id")
                                    {
                                        for (int k = 0; k < MainWindow_OLD.eLC.Lists[3].elementFields.Length; k++)
                                        {
                                            if (MainWindow_OLD.eLC.Lists[3].elementFields[k] == "character_combo_id")
                                            {
                                                dgv.Rows[e.Index].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + Extensions.DecodingCharacterComboId(tt_1[0].Replace(" ", ""));
                                                break;
                                            }
                                        }
                                    }
                                    if (a == "proc_type")
                                    {
                                        for (int k = 0; k < MainWindow_OLD.eLC.Lists[3].elementFields.Length; k++)
                                        {
                                            if (MainWindow_OLD.eLC.Lists[3].elementFields[k] == "proc_type")
                                            {
                                                dgv.Rows[e.Index].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + Extensions.Get_proc_type(tt_1[0].Replace(" ", ""));
                                                break;
                                            }
                                        }
                                    }
                                    if (a == "id_major_type")
                                    {
                                        bool fini = false;
                                        for (int l = 0; l < eLC.Lists.Length; l++)
                                        {
                                            string major = eLC.Lists[comboBox_lists.SelectedIndex].listName.Split(new string[] { " - " }, StringSplitOptions.None)[1].Replace("ESSENCE", "MAJOR_TYPE");
                                            string conf = eLC.Lists[l].listName.Split(new string[] { " - " }, StringSplitOptions.None)[1];
                                            if (major == conf)
                                            {
                                                for (int m = 0; m < eLC.Lists[l].elementValues.Length; m++)
                                                {
                                                    if (int.Parse(eLC.GetValue(l, m, 0)) == int.Parse(tt_1[0].Replace(" ", "")))
                                                    {
                                                        dgv.Rows[e.Index].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + eLC.GetValue(l, m, 1);
                                                        fini = true;
                                                    }


                                                    if (fini)
                                                    {
                                                        break;
                                                    }
                                                }
                                                if (fini)
                                                {
                                                    break;
                                                }

                                            }
                                        }

                                    }
                                    if (a == "id_sub_type")
                                    {
                                        bool fini = false;
                                        for (int l = 0; l < eLC.Lists.Length; l++)
                                        {
                                            string major = eLC.Lists[comboBox_lists.SelectedIndex].listName.Split(new string[] { " - " }, StringSplitOptions.None)[1].Replace("ESSENCE", "SUB_TYPE");
                                            string conf = eLC.Lists[l].listName.Split(new string[] { " - " }, StringSplitOptions.None)[1];
                                            if (major == conf)
                                            {
                                                for (int m = 0; m < eLC.Lists[l].elementValues.Length; m++)
                                                {
                                                    if (int.Parse(eLC.GetValue(l, m, 0)) == int.Parse(tt_1[0].Replace(" ", "")))
                                                    {
                                                        dgv.Rows[e.Index].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + eLC.GetValue(l, m, 1);
                                                        fini = true;
                                                    }

                                                    if (fini)
                                                    {
                                                        break;
                                                    }
                                                }
                                                if (fini)
                                                {
                                                    break;
                                                }

                                            }
                                        }
                                    }
                                    if (a.StartsWith("addon_") && !a.EndsWith("rate"))
                                    {

                                        dgv.Rows[e.Index].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + EQUIPMENT_ADDON.GetAddon(tt_1[0].Replace(" ", ""));

                                    }
                                    if (a.StartsWith("pages_") && int.TryParse(cfd[cfd.Length - 1], out cds) == true)
                                    {

                                        ((TextAndImageCell)dgv.Rows[e.Index].Cells[2]).Image = Extensions.ResizeImage(Extensions.IdImageRecipe(int.Parse(tt_1[0].Replace(" ", "")), out int x), 18, 18);
                                        dgv.Rows[e.Index].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + Extensions.IdNameItem(int.Parse(tt_1[0].Replace(" ", "")));
                                    }
                                    if (a.StartsWith("id_tasks_"))
                                    {
                                        for (int i = 0; i < database.Tasks.Length; i++)
                                        {
                                            if (database.Tasks[i].ID == int.Parse(tt_1[0].Replace(" ", "")))
                                            {
                                                dgv.Rows[e.Index].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + database.Tasks[i].Name;
                                                break;
                                            }

                                        }


                                    }
                                    if (a.StartsWith("id_make_service"))
                                    {
                                        for (int xe = 0; xe < eLC.Lists[54].elementValues.Length; xe++)
                                        {
                                            if (eLC.GetValue(54, xe, 0) == tt_1[0].Replace(" ", ""))
                                            {
                                                dgv.Rows[e.Index].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + eLC.GetValue(54, xe, 1);
                                                break;
                                            }




                                        }
                                    }
                                    if (a.StartsWith("id_buy_service"))
                                    {
                                        for (int xe = 0; xe < eLC.Lists[41].elementValues.Length; xe++)
                                        {
                                            if (eLC.GetValue(41, xe, 0) == tt_1[0].Replace(" ", ""))
                                            {
                                                dgv.Rows[e.Index].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + eLC.GetValue(41, xe, 1); break;
                                            }




                                        }
                                    }
                                    if (a.StartsWith("id_sell_service"))
                                    {
                                        for (int xe = 0; xe < eLC.Lists[40].elementValues.Length; xe++)
                                        {
                                            if (eLC.GetValue(40, xe, 0) == tt_1[0].Replace(" ", ""))
                                            {
                                                dgv.Rows[e.Index].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + eLC.GetValue(40, xe, 1);
                                                break;
                                            }




                                        }
                                    }
                                    if (a.StartsWith("id_repair_service"))
                                    {
                                        for (int xe = 0; xe < eLC.Lists[42].elementValues.Length; xe++)
                                        {
                                            if (eLC.GetValue(42, xe, 0) == tt_1[0].Replace(" ", ""))
                                            {
                                                dgv.Rows[e.Index].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + eLC.GetValue(42, xe, 1);
                                                break;
                                            }
                                        }
                                    }
                                    if (a.StartsWith("id_install_service"))
                                    {
                                        for (int xe = 0; xe < eLC.Lists[43].elementValues.Length; xe++)
                                        {
                                            if (eLC.GetValue(43, xe, 0) == tt_1[0].Replace(" ", ""))
                                            {
                                                dgv.Rows[e.Index].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + eLC.GetValue(43, xe, 1);
                                                break;
                                            }
                                        }
                                    }
                                    if (a.StartsWith("id_uninstall_service"))
                                    {
                                        for (int xe = 0; xe < eLC.Lists[44].elementValues.Length; xe++)
                                        {
                                            if (eLC.GetValue(44, xe, 0) == tt_1[0].Replace(" ", ""))
                                            {
                                                dgv.Rows[e.Index].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + eLC.GetValue(44, xe, 1);
                                                break;
                                            }
                                        }
                                    }
                                    if (a.StartsWith("id_task_out_service"))
                                    {
                                        for (int xe = 0; xe < eLC.Lists[46].elementValues.Length; xe++)
                                        {
                                            if (eLC.GetValue(46, xe, 0) == tt_1[0].Replace(" ", ""))
                                            {
                                                dgv.Rows[e.Index].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + eLC.GetValue(46, xe, 1);
                                                break;
                                            }
                                        }
                                    }
                                    if (a.StartsWith("id_task_in_service"))
                                    {
                                        for (int xe = 0; xe < eLC.Lists[45].elementValues.Length; xe++)
                                        {
                                            if (eLC.GetValue(45, xe, 0) == tt_1[0].Replace(" ", ""))
                                            {
                                                dgv.Rows[e.Index].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + eLC.GetValue(45, xe, 1);
                                                break;
                                            }
                                        }
                                    }
                                    if (a.StartsWith("id_task_matter_service"))
                                    {
                                        for (int xe = 0; xe < eLC.Lists[47].elementValues.Length; xe++)
                                        {
                                            if (eLC.GetValue(47, xe, 0) == tt_1[0].Replace(" ", ""))
                                            {
                                                dgv.Rows[e.Index].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + eLC.GetValue(47, xe, 1);
                                                break;
                                            }
                                        }
                                    }
                                    if (a.StartsWith("id_skill_service"))
                                    {
                                        for (int xe = 0; xe < eLC.Lists[48].elementValues.Length; xe++)
                                        {
                                            if (eLC.GetValue(48, xe, 0) == tt_1[0].Replace(" ", ""))
                                            {
                                                dgv.Rows[e.Index].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + eLC.GetValue(48, xe, 1);
                                                break;
                                            }
                                        }
                                    }
                                    if (a.StartsWith("id_heal_service"))
                                    {
                                        for (int xe = 0; xe < eLC.Lists[49].elementValues.Length; xe++)
                                        {
                                            if (eLC.GetValue(49, xe, 0) == tt_1[0].Replace(" ", ""))
                                            {
                                                dgv.Rows[e.Index].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + eLC.GetValue(49, xe, 1);
                                                break;
                                            }
                                        }
                                    }
                                    if (a.StartsWith("id_transmit_service"))
                                    {
                                        for (int xe = 0; xe < eLC.Lists[50].elementValues.Length; xe++)
                                        {
                                            if (eLC.GetValue(50, xe, 0) == tt_1[0].Replace(" ", ""))
                                            {
                                                dgv.Rows[e.Index].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + eLC.GetValue(50, xe, 1);
                                                break;
                                            }
                                        }
                                    }
                                    if (a.StartsWith("id_proxy_service"))
                                    {
                                        for (int xe = 0; xe < eLC.Lists[52].elementValues.Length; xe++)
                                        {
                                            if (eLC.GetValue(52, xe, 0) == tt_1[0].Replace(" ", ""))
                                            {
                                                dgv.Rows[e.Index].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + eLC.GetValue(52, xe, 1);
                                                break;
                                            }
                                        }
                                    }
                                    if (a.StartsWith("id_storage_service"))
                                    {
                                        for (int xe = 0; xe < eLC.Lists[53].elementValues.Length; xe++)
                                        {
                                            if (eLC.GetValue(53, xe, 0) == tt_1[0].Replace(" ", ""))
                                            {
                                                dgv.Rows[e.Index].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + eLC.GetValue(53, xe, 1);
                                                break;
                                            }
                                        }
                                    }
                                    if (a == "id_type")
                                    {
                                        bool fini = false;
                                        for (int l = 0; l < eLC.Lists.Length; l++)
                                        {
                                            string major = eLC.Lists[comboBox_lists.SelectedIndex].listName.Split(new string[] { " - " }, StringSplitOptions.None)[1].Replace("ESSENCE", "TYPE");
                                            string conf = eLC.Lists[l].listName.Split(new string[] { " - " }, StringSplitOptions.None)[1];
                                            if (major == conf)
                                            {
                                                for (int m = 0; m < eLC.Lists[l].elementValues.Length; m++)
                                                {
                                                    if (int.Parse(eLC.GetValue(l, m, 0)) == int.Parse(tt_1[0].Replace(" ", "")))
                                                    {
                                                        dgv.Rows[e.Index].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + eLC.GetValue(l, m, 1);
                                                        fini = true;
                                                    }

                                                    if (fini)
                                                    {
                                                        break;
                                                    }
                                                }
                                                if (fini)
                                                {
                                                    break;
                                                }

                                            }
                                        }
                                    }
                                }

                                //task_lists_1_id	

                                if (a.StartsWith("task_lists_") && a.EndsWith("_id"))
                                {
                                    for (int i = 0; i < database.Tasks.Length; i++)
                                    {
                                        if (database.Tasks[i].ID == int.Parse(tt_1[0].Replace(" ", "")))
                                        {

                                            ImageList imageList1 = new ImageList();
                                            string[] arquivos = Directory.GetFiles(Application.StartupPath + @"\images", "*.png", SearchOption.TopDirectoryOnly);
                                            for (int fd = 0; fd < arquivos.Length; fd++)
                                            {
                                                imageList1.Images.Add(Image.FromFile(arquivos[fd]));

                                            }



                                            string asds = database.Tasks[i].m_ulType.ToString();
                                            ((TextAndImageCell)dgv.Rows[e.Index].Cells[2]).Image = Extensions.ResizeImage(imageList1.Images[5], 10, 10);
                                            //MessageBox.Show(database.Tasks[i].m_ulType);
                                            dgv.Rows[e.Index].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + database.Tasks[i].Name;
                                            break;
                                        }

                                    }

                                }



                            }
                            catch (Exception exd)
                            {


                            }
                        }


                    }

                }
            }

		}


        private void click_addItemRecipe(object sender, EventArgs ea)
        {
            MessageBox.Show(sender.ToString());
            DataGridView gridView = new DataGridView();
            gridView = dataGridView_elems;

            int idItem = int.Parse(gridView.Rows[gridView.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
            int idListItem = comboBox_lists.SelectedIndex;

            int idListReceipe = 0;

            for (int i = 0; i < eLC.Lists.Length; i++)
            {
                //RECIPE_ESSENCE
                if (eLC.Lists[i].listName.EndsWith("RECIPE_ESSENCE"))
                {
                    idListReceipe = i;
                    break;
                }
            }

            int idClone = int.Parse(eLC.GetValue(idListReceipe, eLC.Lists[idListReceipe].elementValues.Length - 1, 0));

            string NameClone = gridView.Rows[gridView.SelectedCells[2].RowIndex].Cells[2].Value.ToString();

            Encoding enc = Encoding.GetEncoding("Unicode");
            string type = "wstring:64";
            /// Encoding enc = Encoding.GetEncoding("Unicode");
            byte[] target = new byte[Convert.ToInt32(type.Substring(8))];
            byte[] source = enc.GetBytes(NameClone);
            if (target.Length > source.Length)
            {
                Array.Copy(source, target, source.Length);
            }
            else
            {
                Array.Copy(source, target, target.Length);
            }

            int IDITEM;

            if (idItem != 0)
            {
                if (dataGridView_elems.RowCount > 0)
                {
                    if (idListReceipe != eLC.ConversationListIndex)
                    {
                        int[] selIndices = gridSelectedIndices(dataGridView_elems);
                        EnableSelectionList = false;
                        EnableSelectionItem = false;
                        int NewSelectedCount = 0;

                        IDITEM = int.Parse(dataGridView_elems.Rows[dataGridView_elems.RowCount - 1].Cells[0].Value.ToString());
                        for (int i = 0; i < selIndices.Length; i++)
                        {
                            object[] o = new object[eLC.Lists[idListReceipe].elementValues[0].Length];
                            eLC.Lists[idListReceipe].elementValues[0].CopyTo(o, 0);

                            o[0] = idClone + 1;
                            o[3] = target;
                            o[8] = idItem;
                            o[9] = float.Parse("1");
                            for (int jj = 10; jj < eLC.Lists[69].elementTypes.Length; jj++)
                            {
                                string ts = eLC.Lists[69].elementTypes[jj].ToString();
                                if (ts == "int32")
                                {
                                    o[jj] = int.Parse("0");
                                }
                                if (ts == "float")
                                {
                                    o[jj] = float.Parse("0");
                                }

                            }
                            eLC.Lists[idListReceipe].AddItem(o);

                            break;
                        }
                        EnableSelectionList = true;
                        EnableSelectionItem = true;
                       
                        if (MessageBox.Show("Deseja Ir a "+ eLC.Lists[69].listName + " ?","",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation)==DialogResult.Yes)
                        {
                            comboBox_lists.SelectedIndex = 69;
                            dataGridView_elems.CurrentCell = dataGridView_elems.Rows[dataGridView_elems.Rows.Count - 1].Cells[0];
                            change_item(null, null);
                        }
                        

                    }
                    else
                    {
                        MessageBox.Show("Operation not supported in List " + eLC.ConversationListIndex.ToString());
                    }
                }
            }

            int idid = 0;
            try
            {
                idid = int.Parse(gridView.Rows[gridView.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
            }
            catch (Exception)
            {

                //throw;
            }

            add_Returne(idid);
        }

        private void dataGridView_elems_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    var hti = dataGridView_elems.HitTest(e.X, e.Y);
                    dataGridView_elems.ClearSelection();
                    dataGridView_elems.Rows[hti.RowIndex].Selected = true;

                }
            }
            catch (Exception)
            {


            }
        }

        private void tabControl_INFO_Selected(object sender, TabControlEventArgs e)
        {
            //dataGridView_elems.ClearSelection();
            //dataGridView_item.ClearSelection();
            //dataGridView_item_addons.ClearSelection();
            //dataGridView_item_materials.ClearSelection();
            //dataGridView_item_rands.ClearSelection();
        }

        private void dataGridView_item_DoubleClick(object sender, EventArgs e)
        {
            DataGridView gridView = (DataGridView)sender;
            NOVO.Select_id select = new NOVO.Select_id();
            ClassMaskWindow eClassMask = new ClassMaskWindow();
            RECIPES rec = new RECIPES();
            ProcTypeGenerator procType = new ProcTypeGenerator();
            set_ADDONS setADD = new set_ADDONS();
            _major_sub major_Sub = new _major_sub();


            string retur = string.Empty;
            try
            {
                string a = gridView.Rows[gridView.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                string b = gridView.Rows[gridView.SelectedCells[0].RowIndex].Cells[1].Value.ToString();
                string c = gridView.Rows[gridView.SelectedCells[0].RowIndex].Cells[2].Value.ToString();
                string d = "";
                if (a.EndsWith("_id_to_make"))
                {
                    var mid = c.Replace("[", "").Replace("]", "").Split('-');

                   // select.eLC = eLC;
                   // select.database = database;
                    select.input = int.Parse(mid[0].Replace(" ", ""));
                    select.ShowDialog();
                    retur = select.retorn.ToString();

                }
                if (a.StartsWith("materials_") && a.EndsWith("_id"))
                {
                    var mid = c.Replace("[", "").Replace("]", "").Split('-');
                   // select.eLC = eLC;
                  //  select.database = database;
                    select.input = int.Parse(mid[0].Replace(" ", ""));
                    select.ShowDialog(this);
                    retur = select.retorn.ToString();
                }
                if (a.StartsWith("element_id"))
                {
                    var mid = c.Replace("[", "").Replace("]", "").Split('-');
                  //  select.eLC = eLC;
                 //   select.database = database;
                    select.input = int.Parse(mid[0].Replace(" ", ""));
                    select.ShowDialog(this);
                    retur = select.retorn.ToString();
                }
                if (a.StartsWith("id_upgrade_equip"))
                {
                    var mid = c.Replace("[", "").Replace("]", "").Split('-');
                  //  select.eLC = eLC;
                  //  select.database = database;
                    select.input = int.Parse(mid[0].Replace(" ", ""));
                    select.ShowDialog(this);
                    retur = select.retorn.ToString();
                }
                if (comboBox_lists.SelectedIndex == 90 && a.StartsWith("equipments_"))
                {
                    var mid = c.Replace("[", "").Replace("]", "").Split('-');
                  //  select.eLC = eLC;
                  //  select.database = database;
                    select.input = int.Parse(mid[0].Replace(" ", ""));
                    select.ShowDialog(this);
                    retur = select.retorn.ToString();
                }               
                if (a.StartsWith("character_combo_id"))
                {
                    eClassMask.ShowDialog(this);
                    retur = eClassMask.GET.ToString();
                }
                if (a.Contains("_id_goods_"))
                {
                    var mid = c.Replace("[", "").Replace("]", "").Split('-');
                    rec.input = int.Parse(mid[0].Replace(" ", ""));
                    rec.ShowDialog(this);
                    retur = rec.GET.ToString();
                }
                if (a == "proc_type")
                {
                    var mid = c.Replace("[", "").Replace("]", "").Split('-');
                    procType.input = int.Parse(mid[0].Replace(" ", ""));
                    procType.ShowDialog(this);
                    retur = procType.GET.ToString();
                }
                if (a.EndsWith("_id_rand") || a.EndsWith("_id_addon") || a.EndsWith("_id_unique"))
                {
                    var mid = c.Replace("[", "").Replace("]", "").Split('-');
                    setADD.input = int.Parse(mid[0].Replace(" ", ""));
                    setADD.gINDEX = comboBox_lists.SelectedIndex;
                    setADD.ShowDialog(this);
                    retur = setADD.GET.ToString();
                }
                if (a.StartsWith("drop_matters_") && a.EndsWith("_id"))
                {
                    var mid = c.Replace("[", "").Replace("]", "").Split('-');
                   
                    select.input = int.Parse(mid[0].Replace(" ", ""));
                    select.ShowDialog(this);
                    retur = select.retorn.ToString();
                }
                if (a.EndsWith("addon_"))
                {
                    var mid = c.Replace("[", "").Replace("]", "").Split('-');
                    setADD.input = int.Parse(mid[0].Replace(" ", ""));
                    setADD.gINDEX = comboBox_lists.SelectedIndex;
                    setADD.ShowDialog(this);
                    retur = setADD.GET.ToString();
                }
                if (a.StartsWith("file_matter"))
                {                    
                    string url_ski = c.Replace(Path.GetExtension(c), ".ski");
                    string name = Path.GetFileNameWithoutExtension(url_ski);
                    string file = Path.GetDirectoryName(url_ski);
                    string name_ext = Path.GetFileName(url_ski);
                }
                if (a.StartsWith("id_major_type") || a.StartsWith("id_sub_type"))
                {
                    var mid = c.Replace("[", "").Replace("]", "").Split('-');
                    major_Sub.ID = int.Parse(mid[0].Replace(" ", ""));
                    major_Sub.LIST = comboBox_lists.SelectedIndex;
                    major_Sub.TYPE = a;
                    major_Sub.ShowDialog(this);
                    retur = major_Sub.GET.ToString();
                }





                if (int.Parse(retur) != 0)
                {
                    gridView.Rows[gridView.SelectedCells[0].RowIndex].Cells[2].Value = retur;
                }
            }
            catch (Exception)
            {


            }
        }



        private void add_Returne(int ID)
        {
            if (ID != 0)
            {
                #region RECIPE
                Encoding enc = Encoding.GetEncoding("Unicode");
                dataGridView_recipes.Rows.Clear();
                bool Suc1 = false;
                int cont = 0;
                string id_RP = null;
                string ids_RP = null;
                for (int k = 0; k < eLC.Lists[69].elementValues.Length; k++)
                {
                    id_RP = eLC.GetValue(69, k, 0);
                    byte[] id_name = enc.GetBytes(eLC.GetValue(69, k, 3));
                    var id_1 = eLC.GetValue(69, k, 8);
                    var id_2 = eLC.GetValue(69, k, 10);
                    var id_3 = eLC.GetValue(69, k, 12);
                    var id_4 = eLC.GetValue(69, k, 14);
                    //string itens = "";
                    if (ID == int.Parse(id_1) || ID == int.Parse(id_2) || ID == int.Parse(id_3))
                    {
                        if (int.Parse(id_1) != 0)
                        {
                            dataGridView_recipes.Rows.Add(new object[] { id_RP, enc.GetString(id_name), id_1, id_2, id_3, id_4 });

                        }
                    }

                }
                // NPC
                // maker 
                dataGridView_npcs.Rows.Clear();
                foreach (DataGridViewRow item in dataGridView_recipes.Rows)
                {
                    var IdForMaker = dataGridView_recipes.Rows[item.Index].Cells[0].Value.ToString();
                    if (IdForMaker != null)
                    {

                        for (int k = 0; k < eLC.Lists[54].elementValues.Length; k++)
                        {
                            for (int f = 0; f < eLC.Lists[54].elementValues[k].Length; f++)
                            {
                                var a = eLC.Lists[54].elementFields[f];

                                if (a.Contains("_id_goods"))
                                {
                                    var c = eLC.GetValue(54, k, f);
                                    if (c == IdForMaker)
                                    {
                                        var idMk = eLC.GetValue(54, k, 0);
                                        for (int kk = 0; kk < eLC.Lists[57].elementValues.Length; kk++)
                                        {
                                            if (eLC.GetValue(57, kk, 26) == idMk)
                                            {
                                                dataGridView_npcs.Rows.Add(new object[] { eLC.GetValue(57, kk, 0), eLC.GetValue(57, kk, 1), "MAKER", IdForMaker });
                                            }

                                        }

                                    }
                                }

                            }
                        }
                    }
                }


                //SELL
                //var IdForMaker = dataGridView_recipes.Rows[item.Index].Cells[0].Value.ToString();

                if (ID != null || (ID != 0))
                {

                    for (int k = 0; k < eLC.Lists[40].elementValues.Length; k++)
                    {
                        for (int f = 0; f < eLC.Lists[40].elementValues[k].Length; f++)
                        {
                            var a = eLC.Lists[40].elementFields[f];

                            if (a.EndsWith("_id"))
                            {
                                var c = eLC.GetValue(40, k, f);

                                if (c == ID.ToString())
                                {
                                    var idMk = eLC.GetValue(40, k, 0);
                                    for (int kk = 0; kk < eLC.Lists[57].elementValues.Length; kk++)
                                    {
                                        if (eLC.GetValue(57, kk, 12) == idMk)
                                        {
                                            dataGridView_npcs.Rows.Add(new object[] { eLC.GetValue(57, kk, 0), eLC.GetValue(57, kk, 1), "SELL", ID });
                                        }

                                    }

                                }
                            }

                        }
                    }
                }







                #endregion

                #region SUITE
                string line = "";
                dataGridView_SUITE.Rows.Clear();
                int[] IdCombo = new int[12];
                bool Suc = false;
                for (int k = 0; k < MainWindow_OLD.eLC.Lists[90].elementValues.Length; k++)
                {
                    for (int a = 1; a < 13; a++)
                    {
                        for (int t = 0; t < MainWindow_OLD.eLC.Lists[90].elementFields.Length; t++)
                        {
                            if (MainWindow_OLD.eLC.Lists[90].elementFields[t] == "equipments_" + a + "_id")
                            {
                                if (Convert.ToInt32(MainWindow_OLD.eLC.GetValue(90, k, t)) == Convert.ToInt32(ID/*MainWindow.eLC.GetValue(3, pos_item, 0)*/))
                                {


                                    IdCombo[0] = int.Parse(eLC.GetValue(90, k, 3));
                                    IdCombo[1] = int.Parse(eLC.GetValue(90, k, 4));
                                    IdCombo[2] = int.Parse(eLC.GetValue(90, k, 5));
                                    IdCombo[3] = int.Parse(eLC.GetValue(90, k, 6));
                                    IdCombo[4] = int.Parse(eLC.GetValue(90, k, 7));
                                    IdCombo[5] = int.Parse(eLC.GetValue(90, k, 8));
                                    IdCombo[6] = int.Parse(eLC.GetValue(90, k, 9));
                                    IdCombo[7] = int.Parse(eLC.GetValue(90, k, 10));
                                    IdCombo[8] = int.Parse(eLC.GetValue(90, k, 11));
                                    IdCombo[9] = int.Parse(eLC.GetValue(90, k, 12));
                                    IdCombo[10] = int.Parse(eLC.GetValue(90, k, 13));
                                    IdCombo[11] = int.Parse(eLC.GetValue(90, k, 14));

                                    Suc = true;
                                    string id = "";
                                    string name = "";
                                    string max_equips = "0";

                                    for (int n = 0; n < MainWindow_OLD.eLC.Lists[90].elementFields.Length; n++)
                                    {
                                        if (MainWindow_OLD.eLC.Lists[90].elementFields[n] == "Name")
                                        {
                                            name = MainWindow_OLD.eLC.GetValue(90, k, n);
                                            id = MainWindow_OLD.eLC.GetValue(90, k, 0);
                                            break;
                                        }
                                    }
                                    for (int n = 0; n < MainWindow_OLD.eLC.Lists[90].elementFields.Length; n++)
                                    {
                                        if (MainWindow_OLD.eLC.Lists[90].elementFields[n] == "max_equips")
                                        {
                                            max_equips = MainWindow_OLD.eLC.GetValue(90, k, n);
                                            break;
                                        }
                                    }
                                    line += id + " - " + name + " (" + max_equips + ")";
                                    dataGridView_SUITE.Rows.Add(new object[] {
                                        line,
                                        LoadImg(IdCombo[0]),
                                        LoadImg(IdCombo[1]),
                                        LoadImg(IdCombo[2]),
                                        LoadImg(IdCombo[3]),
                                        LoadImg(IdCombo[4]),
                                        LoadImg(IdCombo[5]),
                                        LoadImg(IdCombo[6]),
                                        LoadImg(IdCombo[7]),
                                        LoadImg(IdCombo[8]),
                                        LoadImg(IdCombo[9]),
                                        LoadImg(IdCombo[10]),
                                        LoadImg(IdCombo[11])

                                    });




                                    break;
                                }
                                break;
                            }
                            if (Suc == true) break;
                        }
                        if (Suc == true) break;
                    }
                    if (Suc == true) break;
                }

                #endregion

                #region desc

                for (int i = 0; i < MainWindow_OLD.database.ItemUse.Count; i++)
                {
                    if (int.Parse(MainWindow_OLD.database.ItemUse.GetKey(i).ToString()) == comboBox_lists.SelectedIndex)
                    {
                        if (database.item_ext_desc.ContainsKey(ID.ToString()))
                        {
                            SetText(database.item_ext_desc[ID.ToString()].ToString());
                            
                        }
                       
                    }
                }
                #endregion

                #region Task
                dataGridView_tasks.Rows.Clear();
				if (database.Tasks != null)
				{                    
					for (int t = 0; t < database.Tasks.Length; t++)
					{
						for (int m = 0; m < database.Tasks[t].m_Award_S.m_CandItems.Length; m++)
						{
							for (int i = 0; i < database.Tasks[t].m_Award_S.m_CandItems[m].m_AwardItems.Length; i++)
							{
								if (database.Tasks[t].m_Award_S.m_CandItems[m].m_AwardItems[i].m_ulItemTemplId == ID)
								{

									dataGridView_tasks.Rows.Add(new object[] { database.Tasks[t].ID, database.Tasks[t].Name, database.Tasks[t].m_Award_S.m_CandItems[m].m_AwardItems[i].m_ulItemNum + " UN", Convert.ToDecimal(MainWindow_OLD.database.Tasks[t].m_Award_S.m_CandItems[m].m_AwardItems[i].m_fProb * 100) + "%  " });
								}
							}
						}
					}
				}


				#endregion

				#region GSHOP
				dataGridView_gshop.Rows.Clear();
				if (database.Gshop!= null)
				{
					for (int i = 0; i < database.Gshop.List_items.Count; i++)
					{
						if (database.Gshop.List_items[i].Id == ID)
						{
                            try
                            {
                                dataGridView_gshop.Rows.Add(new object[] { "GSHOP", database.Gshop.List_categories[database.Gshop.List_items[i].Item_category].Category_name, database.Gshop.List_categories[database.Gshop.List_items[i].Item_category].Sub_categories[database.Gshop.List_items[i].Item_sub_category], database.Gshop.List_items[i].Amount, Convert.ToDecimal(database.Gshop.List_items[i].Sales[0].Price) / 100 });

                            }
                            catch (Exception)
                            {

                                
                            }
							
						}
					}
				}
				if (database.GshopEvent != null)
				{
					for (int i = 0; i < database.GshopEvent.List_items.Count; i++)
					{
						if (database.GshopEvent.List_items[i].Id == ID)
						{
                            try
                            {
                                dataGridView_gshop.Rows.Add(new object[] { "GSHOP EVENT", database.GshopEvent.List_categories[database.GshopEvent.List_items[i].Item_category].Category_name, database.GshopEvent.List_categories[database.GshopEvent.List_items[i].Item_category].Sub_categories[database.GshopEvent.List_items[i].Item_sub_category], database.GshopEvent.List_items[i].Amount, Convert.ToDecimal(database.GshopEvent.List_items[i].Sales[0].Price) / 100 });

                            }
                            catch (Exception)
                            {

                               
                            }
							
						}
					}
				}
				#endregion
			}

		}

        public Image LoadImg(int id)
        {
            Image imgg = null;
            if (id != 0)
            {
                bool fi = false;

                try
                {
                    string value = "";
                    for (int L = 0; L < MainWindow_OLD.database.ItemUse.Count; L++)
                    {

                        int La = int.Parse(MainWindow_OLD.database.ItemUse.GetKey(L).ToString());
                        int pos = 0;
                        int posN = 0;
                        for (int i = 0; i < eLC.Lists[La].elementFields.Length; i++)
                        {
                            if (eLC.Lists[La].elementFields[i] == "Name")
                            {
                                posN = i;
                                //break;
                            }
                            if (eLC.Lists[La].elementFields[i] == "file_icon")
                            {
                                pos = i;
                                break;
                            }

                        }
                        for (int ef = 0; ef < eLC.Lists[La].elementValues.Length; ef++)
                        {
                            value = eLC.GetValue(La, ef, pos);

                            if (id == int.Parse(eLC.GetValue(La, ef, 0))/* || value.Contains(b.ToString())*/)
                            {
                                string path = Path.GetFileName(value);
                                if (database.sourceBitmap != null && database.ContainsKey(path))
                                {
                                    if (database.ContainsKey(path))
                                    {
                                        imgg = database.images(path);
                                        //((TextAndImageCell)dgv.Rows[e.RowIndex].Cells[1]).Image = Extensions.ResizeImage(database.images(path), 32, 32);
                                        //dgv.Rows[e.RowIndex].Cells[1].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + eLC.GetValue(La, ef, posN);
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
                catch (Exception ex)
                {
                    imgg = Properties.Resources.unknown;
                }




            }

            if (imgg == null)
            {
                imgg = Properties.Resources.blank;
            }

            return imgg;
        }

        private void dataGridView_recipes_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender; Bitmap bitmap;
            string tt;
            int b = 0;
            int c = 0;
            int d = 0;
            int ee = 0;
            var tt_1 = dgv.Rows[e.RowIndex].Cells[2].Value.ToString().Replace("[", "").Replace("]", "").Split('-');
            var tt_2 = dgv.Rows[e.RowIndex].Cells[3].Value.ToString().Replace("[", "").Replace("]", "").Split('-');
            var tt_3 = dgv.Rows[e.RowIndex].Cells[4].Value.ToString().Replace("[", "").Replace("]", "").Split('-');
            var tt_4 = dgv.Rows[e.RowIndex].Cells[5].Value.ToString().Replace("[", "").Replace("]", "").Split('-');

            int ret;
            tt = tt_1[0];
            if (int.TryParse(tt, out ret))
            {
                b = int.Parse(tt_1[0].Replace(" ", ""));
            }
            if (int.TryParse(tt, out ret))
            {
                c = int.Parse(tt_2[0].Replace(" ", ""));
            }
            if (int.TryParse(tt, out ret))
            {
                d = int.Parse(tt_3[0].Replace(" ", ""));
            }
            if (int.TryParse(tt, out ret))
            {
                ee = int.Parse(tt_4[0].Replace(" ", ""));
            }

            int linha = 0;
            bool fi = false;
            if (b != 0)
            {
                try
                {
                    string value = "";
                    for (int L = 0; L < MainWindow_OLD.database.ItemUse.Count; L++)
                    {

                        int La = int.Parse(MainWindow_OLD.database.ItemUse.GetKey(L).ToString());
                        int pos = 0;
                        int posN = 0;
                        for (int i = 0; i < eLC.Lists[La].elementFields.Length; i++)
                        {
                            if (eLC.Lists[La].elementFields[i] == "Name")
                            {
                                posN = i;
                                //break;
                            }
                            if (eLC.Lists[La].elementFields[i] == "file_icon")
                            {
                                pos = i;
                                break;
                            }

                        }
                        for (int ef = 0; ef < eLC.Lists[La].elementValues.Length; ef++)
                        {
                            value = eLC.GetValue(La, ef, pos);

                            if (b == int.Parse(eLC.GetValue(La, ef, 0))/* || value.Contains(b.ToString())*/)
                            {
                                string path = Path.GetFileName(value);
                                if (database.sourceBitmap != null && database.ContainsKey(path))
                                {
                                    if (database.ContainsKey(path))
                                    {
                                        ((TextAndImageCell)dgv.Rows[e.RowIndex].Cells[2]).Image = Extensions.ResizeImage(database.images(path), 32, 32);
                                        dgv.Rows[e.RowIndex].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + eLC.GetValue(La, ef, posN);
                                        fi = true;

                                        Color clr;
                                        try
                                        { clr = Helper.getByID(database.item_color[int.Parse(b.ToString())]); }
                                        catch (Exception)
                                        { clr = Color.White; }

                                        dgv.Rows[e.RowIndex].Cells[2].Style.ForeColor = clr;

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
                catch (Exception ex)
                {

                    //MessageBox.Show(ex.Message + "\n" + linha);
                }


            }
            bool fi2 = false;
            if (c != 0)
            {
                try
                {
                    string value = "";
                    for (int L = 0; L < MainWindow_OLD.database.ItemUse.Count; L++)
                    {

						int La = int.Parse(MainWindow_OLD.database.ItemUse.GetKey(L).ToString());

						int pos = 0;
                        int posN = 0;
                        for (int i = 0; i < eLC.Lists[La].elementFields.Length; i++)
                        {
                            if (eLC.Lists[La].elementFields[i] == "Name")
                            {
                                posN = i;
                                //break;
                            }
                            if (eLC.Lists[La].elementFields[i] == "file_icon")
                            {
                                pos = i;
                                break;
                            }

                        }
                        for (int ef = 0; ef < eLC.Lists[La].elementValues.Length; ef++)
                        {
                            value = eLC.GetValue(La, ef, pos);

                            if (c == int.Parse(eLC.GetValue(La, ef, 0))/* || value.Contains(b.ToString())*/)
                            {
                                string path = Path.GetFileName(value);
                                if (database.sourceBitmap != null && database.ContainsKey(path))
                                {
                                    if (database.ContainsKey(path))
                                    {
                                        ((TextAndImageCell)dgv.Rows[e.RowIndex].Cells[3]).Image = Extensions.ResizeImage(database.images(path), 32, 32);
                                        dgv.Rows[e.RowIndex].Cells[3].Value = "[" + tt_2[0].Replace(" ", "") + "] - " + eLC.GetValue(La, ef, posN);
                                        fi2 = true;

                                        Color clr;
                                        try
                                        { clr = Helper.getByID(database.item_color[int.Parse(c.ToString())]); }
                                        catch (Exception)
                                        { clr = Color.White; }

                                        dgv.Rows[e.RowIndex].Cells[3].Style.ForeColor = clr;

                                        break;
                                    }
                                }
                            }
                        }

                        if (fi2 == true)
                        {
                            break;
                        }
                    }


                }
                catch (Exception ex)
                {

                    //MessageBox.Show(ex.Message + "\n" + linha);
                }


            }
            bool fi3 = false;
            if (d != 0)
            {
                try
                {
                    string value = "";
                    for (int L = 0; L < MainWindow_OLD.database.ItemUse.Count; L++)
                    {

                        int La = int.Parse(MainWindow_OLD.database.ItemUse.GetKey(L).ToString());
						int pos = 0;
                        int posN = 0;
                        for (int i = 0; i < eLC.Lists[La].elementFields.Length; i++)
                        {
                            if (eLC.Lists[La].elementFields[i] == "Name")
                            {
                                posN = i;
                                //break;
                            }
                            if (eLC.Lists[La].elementFields[i] == "file_icon")
                            {
                                pos = i;
                                break;
                            }

                        }
                        for (int ef = 0; ef < eLC.Lists[La].elementValues.Length; ef++)
                        {
                            value = eLC.GetValue(La, ef, pos);

                            if (d == int.Parse(eLC.GetValue(La, ef, 0))/* || value.Contains(b.ToString())*/)
                            {
                                string path = Path.GetFileName(value);
                                if (database.sourceBitmap != null && database.ContainsKey(path))
                                {
                                    if (database.ContainsKey(path))
                                    {
                                        ((TextAndImageCell)dgv.Rows[e.RowIndex].Cells[4]).Image = Extensions.ResizeImage(database.images(path), 32, 32);
                                        dgv.Rows[e.RowIndex].Cells[4].Value = "[" + tt_3[0].Replace(" ", "") + "] - " + eLC.GetValue(La, ef, posN);
                                        fi3 = true;

                                        Color clr;
                                        try
                                        { clr = Helper.getByID(database.item_color[int.Parse(d.ToString())]); }
                                        catch (Exception)
                                        { clr = Color.White; }

                                        dgv.Rows[e.RowIndex].Cells[4].Style.ForeColor = clr;

                                        break;
                                    }
                                }
                            }
                        }

                        if (fi3 == true)
                        {
                            break;
                        }
                    }


                }
                catch (Exception ex)
                {

                    //MessageBox.Show(ex.Message + "\n" + linha);
                }



            }
            bool fi4 = false;
            if (ee != 0)
            {
                try
                {
                    string value = "";
                    for (int L = 0; L < MainWindow_OLD.database.ItemUse.Count; L++)
                    {

                        int La = int.Parse(MainWindow_OLD.database.ItemUse.GetKey(L).ToString());
						int pos = 0;
                        int posN = 0;
                        for (int i = 0; i < eLC.Lists[La].elementFields.Length; i++)
                        {
                            if (eLC.Lists[La].elementFields[i] == "Name")
                            {
                                posN = i;
                                //break;
                            }
                            if (eLC.Lists[La].elementFields[i] == "file_icon")
                            {
                                pos = i;
                                break;
                            }

                        }
                        for (int ef = 0; ef < eLC.Lists[La].elementValues.Length; ef++)
                        {
                            value = eLC.GetValue(La, ef, pos);

                            if (ee == int.Parse(eLC.GetValue(La, ef, 0))/* || value.Contains(b.ToString())*/)
                            {
                                string path = Path.GetFileName(value);
                                if (database.sourceBitmap != null && database.ContainsKey(path))
                                {
                                    if (database.ContainsKey(path))
                                    {
                                        ((TextAndImageCell)dgv.Rows[e.RowIndex].Cells[5]).Image = Extensions.ResizeImage(database.images(path), 32, 32);
                                        dgv.Rows[e.RowIndex].Cells[5].Value = "[" + tt_4[0].Replace(" ", "") + "] - " + eLC.GetValue(La, ef, posN);
                                        fi4 = true;

                                        Color clr;
                                        try
                                        { clr = Helper.getByID(database.item_color[int.Parse(ee.ToString())]); }
                                        catch (Exception)
                                        { clr = Color.White; }

                                        dgv.Rows[e.RowIndex].Cells[5].Style.ForeColor = clr;

                                        break;
                                    }
                                }
                            }
                        }

                        if (fi4 == true)
                        {
                            break;
                        }
                    }


                }
                catch (Exception ex)
                {

                    //MessageBox.Show(ex.Message + "\n" + linha);
                }


            }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            //treeView1.BeginUpdate();
            //treeView1.Nodes.Add("Parent");
            //treeView1.Nodes[0].Nodes.Add("Child 1");
            //treeView1.Nodes[0].Nodes.Add("Child 2");
            //treeView1.Nodes[0].Nodes[1].Nodes.Add("Grandchild");
            //treeView1.Nodes[0].Nodes[1].Nodes[0].Nodes.Add("Great Grandchild");
            //treeView1.EndUpdate();
        }

       



        public void SetText(string Text)
        {
            try
            {
                string output;
                output = Text.Replace("\\r", "\n");
                output = output.Replace("\\", "\n");
                List<string> colors = new List<string>();
                List<int> Symbol = new List<int>();
                for (int Index = 0; Index < output.Length; ++Index)
                {
                    int b = output.IndexOf("^", Index);
                    if (b >= 0)
                    {
                        colors.Add(output.Substring(b + 1, 6));
                        output = output.Remove(b, 7);
                        Symbol.Add(b);
                    }
                }
                richTextBox_DESC_POS.Text = output;
                for (int b = 0; b < Symbol.Count; ++b)
                {
                    richTextBox_DESC_POS.Select(Symbol[b], richTextBox_DESC_POS.Text.Length);
                    Color col = ColorTranslator.FromHtml("#" + colors[b]);
                    richTextBox_DESC_POS.SelectionColor = col;
                }

            }
            catch
            {
                richTextBox_DESC_POS.Text = "Text parse error";
            }
        }

        



        private void dataGridView_recipes_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int IdListRecipe = int.Parse(dataGridView_recipes.Rows[e.RowIndex].Cells[0].Value.ToString());


                //var tt_1 = dataGridView_recipes.Rows[e.RowIndex].Cells[1].Value.ToString().Replace("[", "").Replace("]", "").Split('-');

                IntPtr handle = ((Control)sender).Handle;
                this.t.ShowToolTip(handle, IdListRecipe);
            }
            catch (Exception)
            {


            }
        }

        private void dataGridView_recipes_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if ((t != null) && t.Visible)
            {
                this.t.Hide();
            }
        }

        private void dataGridView_recipes_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int idRecipe = int.Parse(dataGridView_recipes.Rows[e.RowIndex].Cells[0].Value.ToString());

            comboBox_lists.SelectedIndex = 69;
            dataGridView_item.Rows.Clear();
            
            foreach (DataGridViewRow item in dataGridView_elems.Rows)
            {
                var a = item.Cells[0].Value;
                if (item.Cells[0].Value.ToString() == idRecipe.ToString())
                {
                    
                    dataGridView_elems.Rows[item.Index].Selected = true;
                    dataGridView_elems.CurrentCell = dataGridView_elems.Rows[item.Index].Cells[0];
                    change_item(null, null);
                    break;
                }
            }


        }

        private void dataGridView_SUITE_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int idRecipe = int.Parse(dataGridView_SUITE.Rows[e.RowIndex].Cells[0].Value.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0].Replace(" ", ""));

            comboBox_lists.SelectedIndex = 90;

            foreach (DataGridViewRow item in dataGridView_elems.Rows)
            {
                var a = item.Cells[0].Value;
                if (item.Cells[0].Value.ToString() == idRecipe.ToString())
                {
                    dataGridView_item.Rows.Clear();
                    dataGridView_elems.Rows[item.Index].Selected = true;
                    dataGridView_elems.CurrentCell = dataGridView_elems.Rows[item.Index].Cells[0];
                    change_item(null, null);
                    break;
                }
            }
        }

        private void dataGridView_SUITE_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if ((t2 != null) && t2.Visible)
            {
                this.t2.Hide();
            }
        }

        private void dataGridView_SUITE_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int IdListRecipe = int.Parse(dataGridView_SUITE.Rows[e.RowIndex].Cells[0].Value.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0].Replace(" ", ""));
                IntPtr handle = ((Control)sender).Handle;
                this.t2.ShowToolTip(handle, IdListRecipe);
            }
            catch (Exception)
            {


            }
        }

        private void dataGridView_npcs_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int idRecipe = int.Parse(dataGridView_npcs.Rows[e.RowIndex].Cells[0].Value.ToString());

            comboBox_lists.SelectedIndex = 57;

            foreach (DataGridViewRow item in dataGridView_elems.Rows)
            {
                var a = item.Cells[0].Value;
                if (item.Cells[0].Value.ToString() == idRecipe.ToString())
                {
                    dataGridView_item.Rows.Clear();
                    dataGridView_elems.Rows[item.Index].Selected = true;
                    dataGridView_elems.CurrentCell = dataGridView_elems.Rows[item.Index].Cells[0];
                    change_item(null, null);
                    break;
                }
            }
        }

        #region drag
        private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;

        private void dataGridView_item_materials_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if (dragBoxFromMouseDown != Rectangle.Empty &&
                    !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {

                    // Proceed with the drag and drop, passing in the list item.                    
                    DragDropEffects dropEffect = dataGridView_item_materials.DoDragDrop(
                    dataGridView_item_materials.Rows[rowIndexFromMouseDown],
                    DragDropEffects.Move);
                }
            }
        }

        private void dataGridView_item_materials_MouseDown(object sender, MouseEventArgs e)
        {
            // Get the index of the item the mouse is below.
            rowIndexFromMouseDown = dataGridView_item_materials.HitTest(e.X, e.Y).RowIndex;
            if (rowIndexFromMouseDown != -1)
            {
                // Remember the point where the mouse down occurred. 
                // The DragSize indicates the size that the mouse can move 
                // before a drag event should be started.                
                Size dragSize = SystemInformation.DragSize;

                // Create a rectangle using the DragSize, with the mouse position being
                // at the center of the rectangle.
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                                                               e.Y - (dragSize.Height / 2)),
                                    dragSize);
            }
            else
                // Reset the rectangle if the mouse is not over an item in the ListBox.
                dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void dataGridView_item_materials_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dataGridView_item_materials_DragDrop(object sender, DragEventArgs e)
        {
            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            Point clientPoint = dataGridView_item_materials.PointToClient(new Point(e.X, e.Y));

            // Get the row index of the item the mouse is below. 
            rowIndexOfItemUnderMouseToDrop =
                dataGridView_item_materials.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            // If the drag operation was a move then remove and insert the row.
            if (e.Effect == DragDropEffects.Move)
            {
                DataGridViewRow rowToMove = e.Data.GetData(
                    typeof(DataGridViewRow)) as DataGridViewRow;
                dataGridView_item_materials.Rows.RemoveAt(rowIndexFromMouseDown);
                dataGridView_item_materials.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);

            }
        }

        #endregion


        private void dataGridView_item_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //try
            //{
            //    DataGridView gridView = (DataGridView)sender;

            //    //MessageBox.Show(gridView.Name);

            //    if (e.Button == MouseButtons.Right)
            //    {
            //        var hti = gridView.HitTest(e.X, e.Y);
            //        //gridView.ClearSelection();
            //        gridView.Rows[hti.RowIndex].Selected = true;

            //    }
            //}
            //catch (Exception)
            //{


            //}
        }

        private void dataGridView_item_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs ee)
        {
            //if (comboBox_lists.SelectedIndex == 69)
            //{
            //    rr = new RECIPES();
            //    int id = int.Parse(((DataGridView)sender).Rows[ee.RowIndex].Cells[0].Value.ToString());
            //    Image igg = rr.img(int.Parse(((DataGridView)sender).Rows[ee.RowIndex].Cells[0].Value.ToString()), ee.RowIndex);
            //    ((DataGridViewImageCell)((DataGridView)sender).Rows[ee.RowIndex].Cells[1]).Value = igg;
            //}
        }

        private void dataGridView_item_addons_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var tt_1 = ((DataGridView)sender).Rows[e.RowIndex].Cells[2].Value.ToString().Replace("[", "").Replace("]", "").Split('-');
            var a = ((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value.ToString();
            var b = ((DataGridView)sender).Rows[e.RowIndex].Cells[1].Value.ToString();



            if ((a.EndsWith("_id_addon")||a.StartsWith("skills_") || a.StartsWith("after_death") || a.StartsWith("skill_hp")) && b.StartsWith("int32") )
            {
                ((DataGridView)sender).Rows[e.RowIndex].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + EQUIPMENT_ADDON.GetAddon(tt_1[0].Replace(" ",""));
            }
            if (a.EndsWith("_id_unique"))
            {
                ((DataGridView)sender).Rows[e.RowIndex].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + EQUIPMENT_ADDON.GetAddon(tt_1[0].Replace(" ", ""));
            }

            if (a.EndsWith("_id_rand"))
            {
                ((DataGridView)sender).Rows[e.RowIndex].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + EQUIPMENT_ADDON.GetAddon(tt_1[0].Replace(" ", ""));
            }
            
            if (a.StartsWith("addons_") && a.EndsWith("_id"))
            {
                if (database._suite.ContainsKey(int.Parse(tt_1[0].Replace(" ", ""))))
                {
                    ((DataGridView)sender).Rows[e.RowIndex].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + database._suite[int.Parse(tt_1[0].Replace(" ", ""))];
                }
                
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
           
        }
        _ListNamesColor nn;
		private Thread _MATS;

		private void textBox_NAME_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < l.Legal.Length; i++)
            {
                if (comboBox_lists.SelectedIndex == l.Legal[i])
                {

                    (new _ListNamesColor(textBox_NAME.Text, Color.Black, comboBox_lists.SelectedIndex, textBox_NAME.Width, Cursor.Position, int.Parse(numericUpDownEx_ID.Value.ToString()))).ShowDialog(this);
                    break;


                }
            
                  
                    
                
            }
            DataGrid(null, null, dataGridView_item);

        }


        private void attAddonsItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new ups.exyt()).Show(this);
        }

        private void attAddonsSuiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new ups.BuscaAddonsSuite()).ShowDialog(this);
        }
        
        private void pictureBox_icon_DoubleClick(object sender, EventArgs e)
        {
            string input = "";bool have=false;
            foreach (DataGridViewRow item in dataGridView_item.Rows)
            {
                string a = item.Cells[0].Value.ToString();
                if (a.StartsWith("file_icon"))
                {
                    input = Path.GetFileName(item.Cells[2].Value.ToString());
                    have = true;
                    break;
                }
            }

            if (have)
            {
                _SurfacesChanger = new SurfacesChanger();
                _SurfacesChanger.SET = input;
                _SurfacesChanger.ShowDialog(this);

                string retur = _SurfacesChanger.GET;
                if (retur != null)
                {
                    foreach (DataGridViewRow item in dataGridView_item.Rows)
                    {
                        string a = item.Cells[0].Value.ToString();
                        if (a.StartsWith("file_icon"))
                        {
                            item.Cells[2].Value = retur;
                            DataGrid(null, null, dataGridView_item);

                            break;
                        }
                    }
                }
                

            }

            


        }

        private void textBox_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                click_search(null, null);
            }
        }


        private void richTextBox_DESC_POS_DoubleClick(object sender, EventArgs e)
        {
            (new NOVO.item_ext_desc(int.Parse(dataGridView_item.Rows[0].Cells[2].Value.ToString()))).ShowDialog(this);
            richTextBox_DESC_POS.Clear();
            if (database.item_ext_desc.ContainsKey(int.Parse(dataGridView_item.Rows[0].Cells[2].Value.ToString()).ToString()))
            {

                SetText(database.item_ext_desc[dataGridView_item.Rows[0].Cells[2].Value.ToString()].ToString());
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {


            //string send = @"C:\Users\LOFT261\Desktop\Nova pasta\Produtos.dat";
            //string algo = @"C:\xampp\htdocs\FTP CELULAR\teste.pck";

            //PCKs pck = new PCKs(algo);
            //StreamReader file = new StreamReader(send);
            //byte[] arrayImg =  File.ReadAllBytes(send);
            
            ////((IEnumerable<byte>)pck.Files.Add(arrayImg).ToArray<byte>();
            //PCKZlib.Compress(arrayImg, pck.CompressionLevel);

        }

        private void contextMenuStrip_items_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private void goToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

        }

        private void saveElementedataToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ElementsPath != "" && File.Exists(ElementsPath))
            {
                try
                {
                    Cursor = Cursors.AppStarting;
                    //progressBar_progress.Style = ProgressBarStyle.Marquee;
                    File.Copy(ElementsPath, ElementsPath + ".bak", true);
                    if (eLC.ConversationListIndex > -1 && eLC.Lists.Length > eLC.ConversationListIndex)
                    {
                        eLC.Lists[eLC.ConversationListIndex].elementValues[0][0] = conversationList.GetBytes();
                    }
                    eLC.Save(ElementsPath);
                    //progressBar_progress.Style = ProgressBarStyle.Continuous;
                    Cursor = Cursors.Default;
                }
                catch
                {
                    MessageBox.Show("SAVING ERROR!\nThis error mostly occurs of configuration and elements.data mismatch");
                    //progressBar_progress.Style = ProgressBarStyle.Continuous;
                    Cursor = Cursors.Default;
                }
            }
        }

        private void saveGshop12ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (database.Gshop != null)
            {
                SalveShops.SaveGshopData(database.Gshop, XmlData.GshopDataPath);
                SalveShops.SaveGshopSevData(database.Gshop, XmlData.GshopDataPath);
            }
            if (database.GshopEvent != null)
            {
                SalveShops.SaveGshopData(database.GshopEvent, XmlData.Gshop1DataPath);
                SalveShops.SaveGshopSevData(database.GshopEvent, XmlData.Gshop1DataPath);
            }
            
            
        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveElementedataToolStripMenuItem_Click(null, null);
            saveGshop12ToolStripMenuItem_Click(null,null);
        }

        private void ForjasAlt(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if(comboBox_lists.SelectedIndex==54 || comboBox_lists.SelectedIndex == 40)
            {
                DataGridView dgv = (DataGridView)sender;
                if (dgv.Rows.Count > 0)
                {
                    Bitmap bitmap;


                    try
                    {

                        string a, tt; int b = 0;
                        var tt_1 = dgv.Rows[e.RowIndex].Cells[2].Value.ToString().Replace("[", "").Replace("]", "").Split('-');
                        a = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();
                        int ret;
                        tt = tt_1[0];
                        if (int.TryParse(tt, out ret)) { b = int.Parse(tt_1[0].Replace(" ", "")); }
                        int cds;
                        var cfd = a.Split(new string[] { "_" }, StringSplitOptions.None);
                        int linha = 0;



                        if (b != 0)
                        {
                            bool fi = false;


                            if (a.StartsWith("pages_") && int.TryParse(cfd[cfd.Length - 1], out cds) == true)
                            {

                                ((TextAndImageCell)dgv.Rows[e.RowIndex].Cells[2]).Image = Extensions.ResizeImage(Extensions.IdImageRecipe(int.Parse(tt_1[0].Replace(" ", "")), out int x), 18, 18);
                                dgv.Rows[e.RowIndex].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + Extensions.IdNameItemRecipe(int.Parse(tt_1[0].Replace(" ", "")));
                            }
                            if (a.StartsWith("pages_") && a.EndsWith("_id"))
                            {
                               
                              ((TextAndImageCell)dgv.Rows[e.RowIndex].Cells[2]).Image = Extensions.ResizeImage(Extensions.IdImageItem(int.Parse(tt_1[0].Replace(" ", ""))), 18, 18);
                                

                                dgv.Rows[e.RowIndex].Cells[2].Value = "[" + tt_1[0].Replace(" ", "") + "] - " + Extensions.IdNameItem(int.Parse(tt_1[0].Replace(" ", "")));
                                
                            }

                        }
                    }
                   
                    catch (Exception exd)
                    {


                    }
                    


                }

            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (AssetManagerLoad.ThreadState== ThreadState.Running)
            //{
            //    AssetManagerLoad.Interrupt();
            //}
            
        }
    }
    
}





















        
    

       
    
