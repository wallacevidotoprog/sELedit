using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sELedit
{
    public partial class Select_id : Form
    {
        public Select_id(MainWindow f,LBLIBRARY.PWHelper.Elements ds,int[] Items_amount,bool IconsLoaded)
        {
            this.wp = ds;
            this.fm = f;
            this.Amount = Items_amount;
            this.IconsLoaded = IconsLoaded;
            InitializeComponent();
            SortList();
        }
        public int Decision;
        public LBLIBRARY.PWHelper.Elements wp;
        MainWindow fm;
        int[] Amount;
        bool IconsLoaded;
        public List<AddNewItems> NewItemsList;
        List<LBLIBRARY.PWHelper.Elements.Item> Search_list;
        int Search_position;
        private void SortList()
        {
           var vk = wp.ElementsLists.Select(v => v.ListName).ToArray();
                List_categories.Items.AddRange(vk);
        }
        private void List_categories_SelectedValueChanged(object sender, EventArgs e)
        {
            Bitmap bt = new Bitmap(1,1);
            Items_grid.ScrollBars = ScrollBars.None;
            Items_grid.Rows.Clear();
            if (List_categories.SelectedIndex == 21)
            {
                this.Width = 680;
                Items_grid.Columns[1].Width = 220;
                Items_grid.Columns[2].Width = 50;
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.Name = "stylemajortype";
                col.HeaderText = "Major";
                col.Width = 50;
                Items_grid.Columns.Add(col);

                col = new DataGridViewTextBoxColumn();
                col.Name = "stylesubtype";
                col.HeaderText = "Sub";
                col.Width = 50;
                Items_grid.Columns.Add(col);

                col = new DataGridViewTextBoxColumn();
                col.Name = "stylegender";
                col.HeaderText = "Gender";
                col.Width = 50;
                Items_grid.Columns.Add(col);
                int z = 10;
                if(wp.Version==6)
                {
                    z = 9;
                }
                foreach (var i in wp.ElementsLists[List_categories.SelectedIndex].Items)
                {
                    Items_grid.Rows.Add(i.IconImage, i.Name, i.Id,i.Values[0],i.Values[1],i.Values[z]);
                }
            }
            else
            {
                if(Items_grid.Columns.Count==6)
                {
                    Items_grid.Columns.RemoveAt(3);
                    Items_grid.Columns.RemoveAt(3);
                    Items_grid.Columns.RemoveAt(3);
                    this.Width = 582;
                    Items_grid.Columns[1].Width = 257;
                    Items_grid.Columns[2].Width = 70;
                }
                foreach (var i in wp.ElementsLists[List_categories.SelectedIndex].Items)
                {
                    Items_grid.Rows.Add(i.IconImage, i.Name, i.Id);
                }
            }
            Items_grid.ScrollBars = ScrollBars.Vertical;
        }
        private void Items_grid_DoubleClick(object sender, EventArgs e)
        {
            if (Decision == 0 || Decision == 1)
            {
                Image gf = (Image)Items_grid.CurrentRow.Cells[0].Value;
                if (gf.Size == new Size(1, 1))
                {
                    gf = Properties.Resources.SmallQuestionMark.ToBitmap();
                }
                Image bm = Properties.Resources._32x32_Question.ToBitmap();
                int f = wp.Items.FindIndex(i => i.Id == (int)Items_grid.CurrentRow.Cells[2].Value);
                if (f != -1)
                    bm = wp.Items[f].Standard_image;
                fm.SetNewID((int)Items_grid.CurrentRow.Cells[2].Value, Items_grid.CurrentRow.Cells[1].Value.ToString(), gf, bm, Decision);
                this.Hide();
            }
        }
        private void To_the_end_button_Click(object sender, EventArgs e)
        {
            Items_grid.CurrentCell = Items_grid.Rows[Items_grid.Rows.Count - 1].Cells[1];
        }
        public void Selectindex(int val,int id )
        {
            if (val > -1)
            {
                List_categories.SelectedIndex = val;
                foreach (DataGridViewRow k in Items_grid.Rows)
                {
                    if (k.Cells[2].Value.ToString().Contains(id.ToString()))
                    {
                        Items_grid.CurrentCell = k.Cells[1];
                        Items_grid.FirstDisplayedScrollingRowIndex = k.Index;
                        break;
                    }
                }
            }
        }
        private void HideForm_button_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void AddNewItems_button_Click(object sender, EventArgs e)
        {
            //if (Decision == 0 || Decision == 1)
            //{
            //    Image gf = (Image)Items_grid.CurrentRow.Cells[0].Value;
            //    if (gf.Size == new Size(1, 1))
            //        gf = Properties.Resources.SmallQuestionMark.ToBitmap();
            //    Image bm = Properties.Resources._32x32_Question.ToBitmap();
            //    int f = wp.Items.FindIndex(i => i.Id == (int)Items_grid.CurrentRow.Cells[2].Value);
            //    if (f != -1)
            //        bm = wp.Items[f].Standard_image;
            //    fm.SetNewID((int)Items_grid.CurrentRow.Cells[2].Value, Items_grid.CurrentRow.Cells[1].Value.ToString(), gf, bm, Decision);
            //}
            //else if (Decision == 2)
            //{
            //    NewItemsList = new List<AddNewItems>(Items_grid.SelectedRows.Count);
            //    var t = Items_grid.SelectedRows.Cast<DataGridViewRow>().OrderBy(g => g.Index);
            //    foreach(DataGridViewRow dg in t)
            //    {
            //        Image gf = (Image)dg.Cells[0].Value;
            //        if (gf.Size == new Size(1, 1))
            //            gf = Properties.Resources.SmallQuestionMark.ToBitmap();
            //        AddNewItems a = new AddNewItems();
            //        a.Im = gf;
            //        a.Name = dg.Cells[1].Value.ToString();
            //        a.Id = Convert.ToInt32(dg.Cells[2].Value);
            //        NewItemsList.Add(a);
            //    }
            //    fm.AddItemsElementsData(NewItemsList);
            //}
            //this.Hide();
        }
        public void RefreshLanguage(int Language)
        {
            if(Language==1 && this.Text != "Выбор элементов с Elements.data")
            {
                this.Text = "Выбор элементов с Elements.data";
                AddNewItems_button.Text = "Выбрать";
                HideForm_button.Text = "Отмена";
            }
            else if (Language==2 && this.Text != "Selecting items from Elements.data")
            {
                this.Text = "Selecting items from Elements.data";
                AddNewItems_button.Text = "Select";
                HideForm_button.Text = "Cancel";
            }
        }

        private void Search_textbox_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(Search_textbox.Text,out int GotId);
            Search_list = wp.Items.Where(i => i.Name.ToLower().Contains(Search_textbox.Text.ToLower()) ||i.Id == GotId).ToList();
            var d = wp.Items.FindIndex(i => i.Name.ToLower().Contains(Search_textbox.Text.ToLower()) || i.Id == GotId);
            if (d> -1)
            {
                Search_position = 0;
                var s = Array.FindIndex(Amount, i => i > d);
                Selectindex(s, wp.Items[d].Id);
            }
        }

        private void Continue_search_Click(object sender, EventArgs e)
        {
            if (Search_list != null)
            {
                if (Search_list.Count > Search_position + 1)
                {
                    Search_position++;
                    int d = wp.Items.IndexOf(Search_list[Search_position]);
                    if (d != -1)
                    {
                        var s = Array.FindIndex(Amount, i => i > d);
                        Selectindex(s, wp.Items[d].Id);
                    }
                }
            }
        }

        private void Select_id_Load(object sender, EventArgs e)
        {

        }
    }
    public class AddNewItems
    {
        public Image Im;
        public string Name;
        public int Id;
    }
}
