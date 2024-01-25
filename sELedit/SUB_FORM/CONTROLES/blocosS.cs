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
    public partial class blocosS : UserControl
    {
        public blocosS()
        {
            InitializeComponent();
        }

        [Category("ITEM")]
        public Image FOTO { get; set; }

        [Category("ITEM")]
        public int ID { get; set; }

        [Category("ITEM")]
        public int LIST { get; set; }

        private void blocosS_Load(object sender, EventArgs e)
        {
            pictureBox_item.BackgroundImage = FOTO;
            pictureBox_item.Image = null;
            
        }
        private IToolType customTooltype;

        private void pictureBox_item_MouseEnter(object sender, EventArgs e)
        {
            pictureBox_item.Image = Resources.bloco_select; IntPtr handle = ((Control)sender).Handle;

            try
            {
                if (customTooltype != null)
                    customTooltype.Close();
            }
            catch { }
            if (ID > 0)
            {
                InfoTool ift = null;
                try
                {
                   
                        if (ID > 0)
                        {
                            
                            if (LIST == 69)
                            {
                                 int idx;
                                 Extensions.IdImageRecipe(ID, out idx);
                                ift = Extensions.GetItemPropsFromID(idx);
                            }
                            else if (LIST == 40)
                            {
                            
                            ift = Extensions.GetItemPropsFromID(ID);
                            }

                           
                        }
                        if (ift == null)
                        {
                            string text = Extensions.GetItemProps(ID, 0);
                            text += Extensions.ItemDesc(ID);
                        }
                        else
                        {
                            ift.description = Extensions.ColorClean(Extensions.ItemDesc(ID));
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
            pictureBox_item.Image = null;
            if (customTooltype != null)
                customTooltype.Close();

        }

        private void pictureBox_item_MouseDown(object sender, MouseEventArgs e)
        {
            //pictureBox_item.DoDragDrop(new MyWrapper(pictureBox_item), DragDropEffects.Move);
            this.DoDragDrop(new MyWrapper(this), DragDropEffects.Move);
            //pictureBox_item.Image = Properties.Resources.bloco_select;
        }

        private void blocosS_MouseDown(object sender, MouseEventArgs e)
        {
            this.DoDragDrop(new MyWrapper(this), DragDropEffects.Move);
        }
    }
}
