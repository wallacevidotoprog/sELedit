using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sELedit.Properties;

namespace sELedit.NOVO
{
    public partial class recipeItem : UserControl
    {
        public recipeItem()
        {
            InitializeComponent();
        }
        public int ID { get; set; }
        public int QTD { get; set; }
       

        private void recipeItem_Load(object sender, EventArgs e)
        {
                button_qtd.Text = QTD.ToString();
            if (ID > 0)
            {
                pictureBox1.BackgroundImage = Extensions.IdImageItem(ID);
                pictureBox1.Image = null;
            }
                
            
        }
        private IToolType customTooltype;
        private void pictureBox_item_MouseEnter(object sender, EventArgs e)
        {
            IntPtr handle = ((Control)sender).Handle;
            if (ID > 0)
            {
                pictureBox1.Image = Resources.bloco_select;
            }
            try
            {
                if (customTooltype != null)
                    customTooltype.Close();
            }
            catch { }
            if (ID >= 0 )
            {
                InfoTool ift = null;
                try
                {
                    int Id = ID; ;
                    if (Id > 0)
                    {
                        ift = Extensions.GetItemPropsFromID(Id);
                    }
                    if (ift == null)
                    {
                        string text = Extensions.GetItemProps(Id, 0);
                        text += Extensions.ItemDesc(Id);
                        //dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = text;
                    }
                    else
                    {
                        ift.description = Extensions.ColorClean(Extensions.ItemDesc(Id));
                        customTooltype = new IToolType(ift);
                        customTooltype.Show(this);
                    }
                }
                catch
                {
                }
            }
        }

        private void pictureBox_item_MouseLeave(object sender, EventArgs e)
        {
            if (ID > 0)
            {
                pictureBox1.Image = null;
            }

            if (customTooltype != null)
                customTooltype.Close();
        }






    }
}
