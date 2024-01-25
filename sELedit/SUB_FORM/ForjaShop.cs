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
    public partial class ForjaShop : Form
    {
        public Npc_MAKER[] Npc_MAKER { get; set; }
        List<Button> buttons;
        int BntSelect;
        Point mousePositionSelected;
        FlowLayoutPanel flp;
        Thread SelectRecipe;

        public ForjaShop(Npc_MAKER[] _Npc_MAKER)
        {
            InitializeComponent();
            Npc_MAKER = _Npc_MAKER;
            buttons = new List<Button>() { button1, button2, button3, button4, button5, button6, button7, button8 };
        }
        #region flowLayoutPanel_itens

        
        private void ForjaShop_Load(object sender, EventArgs e)
        {
            try
            {
                var Curs = AdvancedCursorsFromEmbededResources.Create(Properties.Resources.Game);
                Cursor = Curs;
                this.Cursor = Curs;

            }
            catch { }
            blocosS Blocos;
            flowLayoutPanel_itens.Controls.Clear();
            Blocos = new blocosS();
            for (int i = 0; i < Npc_MAKER.Length; i++)
            {
                if (Npc_MAKER[i].Title != string.Empty)
                {
                    buttons[i].Text = Npc_MAKER[i].Title;
                }
                if (i == 0)
                {
                    BntSelect = i;
                    for (int b = 0; b < Npc_MAKER[i].IdItem.Length; b++)
                    {
                        Blocos = new blocosS();
                        Blocos.ID = Npc_MAKER[i].IdItem[b];
                        Blocos.LIST = 69;
                        if (Npc_MAKER[i].IdItem[b] == 0)
                        {
                            Blocos.FOTO = Properties.Resources.bloco_a;
                        }
                        else
                        {
                            Blocos.FOTO = Extensions.IdImageRecipe(Npc_MAKER[i].IdItem[b], out int x);
                        }


                        flowLayoutPanel_itens.Controls.Add(Blocos);
                       

                    }
                }


            }
            Title_Selected(null, null);




        }

        private void Selected_Title(object sender, EventArgs e)
        {
            blocosS Blocos;
            Button btn = (Button)sender;

            if (btn.Text != "NULL")
            {
                flowLayoutPanel_itens.Controls.Clear();
                
                for (int i = 0; i < buttons.Count; i++)
                {
                    if (btn.Text == buttons[i].Text)
                    {
                        for (int b = 0; b < Npc_MAKER[i].IdItem.Length; b++)
                        {
                            Blocos = new blocosS();
                            Blocos.ID = Npc_MAKER[i].IdItem[b];
                            Blocos.LIST = 69;
                            if (Npc_MAKER[i].IdItem[b] == 0)
                            {
                                Blocos.FOTO = Properties.Resources.bloco_a;
                            }
                            else
                            {
                                Blocos.FOTO = Extensions.IdImageRecipe(Npc_MAKER[i].IdItem[b], out int x);
                            }


                            flowLayoutPanel_itens.Controls.Add(Blocos);
                            
                        }
                        BntSelect = i;
                        flowLayoutPanel_itemrecipe.Controls.Clear();
                        for (int x = 0; x < 8; x++)
                        {
                            recipeItem recipeItem = new recipeItem();
                            flowLayoutPanel_itemrecipe.Controls.Add(recipeItem);
                        }
                        
                        break;
                    }
                }
                


            }
            else
            {
                MessageBox.Show("Test");
            }

            Title_Selected(null, null);




        }

        

        void Title_Selected(object sender, EventArgs e)
        {
            //DimGray
            for (int i = 0; i < buttons.Count; i++)
            {
                if (i == BntSelect)
                {
                    buttons[i].BackColor = Color.DimGray;
                }
                else
                {
                    buttons[i].BackColor = Color.Black;
                }
            }
        }

        private void MyMouseDown(object sender, MouseEventArgs e)
        {
            Control source;
            source = (Control)sender;
            source.DoDragDrop(new MyWrapper(source), DragDropEffects.Move);
        }

        private void flowLayoutPanel_itens_DragEnter(object sender, DragEventArgs e)
        {
           if (e.Data.GetDataPresent(typeof(MyWrapper)))
                e.Effect = DragDropEffects.Move;
            //e.Effect = DragDropEffects.Move; 
            
            mousePositionSelected = flowLayoutPanel_itens.PointToClient(new Point(e.X, e.Y));
            SelectRecipe = new Thread(delegate () { RECIPEVIEW(mousePositionSelected); }); SelectRecipe.Start();
            //RECIPEVIEW(mousePositionSelected);
        }

       void RECIPEVIEW(Point XY)
       {
           
            flowLayoutPanel_itemrecipe.Invoke((MethodInvoker)delegate () {
                try
                {
                    NOVO.recipeItem recipeItem;
                    Control Trans = flowLayoutPanel_itens.GetChildAtPoint(XY);

                    int IdListRecipe = flowLayoutPanel_itens.Controls.GetChildIndex(Trans, false);
                    flowLayoutPanel_itemrecipe.Controls.Clear();

                    int getIndex = Npc_MAKER[BntSelect].IdItem[IdListRecipe];


                    for (int k = 0; k < MainWindow.eLC.Lists[69].elementValues.Length; k++)
                    {
                        var id_RP = int.Parse(MainWindow.eLC.GetValue(69, k, 0));

                        string id_upgrade_equip = MainWindow.eLC.GetValue(69, k, 86);


                        if (id_RP == getIndex)
                        {
                            if (int.Parse(id_upgrade_equip) > 0)
                            {
                                recipeItem = new recipeItem();
                                recipeItem.ID = int.Parse(id_upgrade_equip);
                                recipeItem.QTD = 1;
                                flowLayoutPanel_itemrecipe.Controls.Add(recipeItem);
                            }

                            for (int i = 22; i <= 85; i++)
                            {
                                if (int.Parse(MainWindow.eLC.GetValue(69, k, i)) > 0)
                                {

                                    recipeItem = new recipeItem();
                                    recipeItem.ID = int.Parse(MainWindow.eLC.GetValue(69, k, i));
                                    recipeItem.QTD = int.Parse(MainWindow.eLC.GetValue(69, k, i + 1));
                                    flowLayoutPanel_itemrecipe.Controls.Add(recipeItem);
                                    i++;
                                }

                            }
                            break;
                        }

                    }
                }
                catch (Exception)
                {

                    
                }
               


            });
            
        }

        private void flowLayoutPanel_itens_DragDrop(object sender, DragEventArgs e)
        {
            MyWrapper wrapper = (MyWrapper)e.Data.GetData(typeof(MyWrapper));
            Control source = wrapper.Control;         
           
            Point mousePosition = flowLayoutPanel_itens.PointToClient(new Point(e.X, e.Y));

            Control Trans = flowLayoutPanel_itens.GetChildAtPoint(mousePosition);

            int indexDestination = flowLayoutPanel_itens.Controls.GetChildIndex(Trans, false);
            int indexOrigem = flowLayoutPanel_itens.Controls.GetChildIndex(source, false);

                       
            if (indexOrigem != -1 && indexDestination != -1)
            {
                SetPosi(GetPosi(flowLayoutPanel_itens.Controls.IndexOf(source)), flowLayoutPanel_itens.Controls.IndexOf(source), GetPosi(flowLayoutPanel_itens.Controls.IndexOf(Trans)), flowLayoutPanel_itens.Controls.IndexOf(Trans));

                flowLayoutPanel_itens.Controls.SetChildIndex(Trans, indexOrigem);
                flowLayoutPanel_itens.Controls.SetChildIndex(source, indexDestination);
                flowLayoutPanel_itens.Refresh();
            }


        }

        void SetPosi(int id_o,int pos_o,int id_d,int pos_d)
        {
            //BntSelect
            if (pos_d !=-1)
            {
                Npc_MAKER[BntSelect].IdItem[pos_d] = id_o;
            }
            if (pos_o != -1)
            {
                Npc_MAKER[BntSelect].IdItem[pos_o] = id_d;
            }
            
            

        }

        private int GetPosi(int pos_o)
        {
            int x = -1;
            if (pos_o != -1)
            {
                return Npc_MAKER[BntSelect].IdItem[pos_o];
                x = Npc_MAKER[BntSelect].IdItem[pos_o];
            }          
            return x;

        }

        private void ForjaShop_FormClosing(object sender, FormClosingEventArgs e)
        {

        }


        #endregion

       
    }

    class MyWrapper
    {
        private Control control;

        public MyWrapper(Control control)
        {
            this.control = control;
        }

        public Control Control
        {
            get { return control; }
        }
    }

}
