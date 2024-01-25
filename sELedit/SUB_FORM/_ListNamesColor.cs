using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sELedit.configs;
using sELedit.DDSReader.Utils;

namespace sELedit.NOVO
{
    public partial class _ListNamesColor : Form
    {
        public string name { get; set; }
        public string get { get; set; }
        public int comboBox_lists { get; set; }
        public int idItem { get; set; }
        public _ListNamesColor(string _name,Color _cList,int _comboBox_lists,int _Width,Point XY,int _idItem)
        {
            InitializeComponent();
            listBox_names.BackColor = _cList;
            name = _name;
            comboBox_lists = _comboBox_lists;
            _Width = Width;
            idItem = _idItem;
            //Left = XY.X;
            //Top = XY.Y;

            Location = new Point(XY.X-10, XY.Y-10);
        }

        private void load(object sender, EventArgs e)
        {
            for (int c = 0; c < 9; c++)
            {
                ColoredItem coloredItem = new ColoredItem { Color = Helper.getByID(c), Text = name };
                listBox_names.Items.Add(coloredItem);
            }
        }

        private void listBox_names_MouseLeave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox_names_DrawItem(object sender, DrawItemEventArgs e)
        {
            var item = listBox_names.Items[e.Index] as ColoredItem;

            if (item != null)
            {
                e.Graphics.DrawString(
                    item.Text,
                    e.Font,
                    new SolidBrush(item.Color),
                    e.Bounds);
            }
        }

        private void listBox_names_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MainWindow.database.item_color.ContainsKey(idItem))
            {
                MainWindow.database.item_color.Remove(idItem);
                MainWindow.database.item_color.Add(idItem, listBox_names.SelectedIndex);

                
            }
            else
            {
                MainWindow.database.item_color.Add(idItem, listBox_names.SelectedIndex);

                
            }

            Close();
            
        }
    }

    class ColoredItem
    {
        public string Text;
        public Color Color;
    };
}
