using sELedit.configs;
using sELedit.DDSReader.Utils;
using sELedit.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace sELedit
{
    
    public partial class ToolTip : Form
	{
		public ToolTip()
		{
			InitializeComponent();
		}
		protected override bool ShowWithoutActivation
		{
			get
			{
				return true;
			}
		}

        public void ShowToolTip(IntPtr WindowHandle, int IdListRecipe, int lista = 0)
		{
			this.ShowToolTip(WindowHandle, IdListRecipe, lista, 0, -1.0, -1.0);
		}
		public void SetText(int IdListRecipe,int lista)
		{
            dataGridView1.Rows.Clear();
            Encoding enc = Encoding.GetEncoding("Unicode");

            switch (lista)
            {
                case 69:
                    try
                    {
                        int height_Grid = 0;
                        for (int k = 0; k < MainWindow.eLC.Lists[69].elementValues.Length; k++)
                        {
                            var id_RP = int.Parse(MainWindow.eLC.GetValue(69, k, 0));
                           
                            string id_upgrade_equip = MainWindow.eLC.GetValue(69, k, 86);
                            string itens = "";
                            if (id_RP == IdListRecipe)
                            {
                                if (int.Parse(id_upgrade_equip) > 0)
                                {
                                    dataGridView1.Rows.Add(new object[] { id_upgrade_equip, "UP" });
                                    dataGridView1.Rows.Add(new object[] { "", "" });
                                   
                                    height_Grid++;
                                    height_Grid++;
                                }

                                for (int i = 22; i <= 85; i++)
                                {
                                    if (int.Parse(MainWindow.eLC.GetValue(69, k, i)) > 0)
                                    {
                                        itens += MainWindow.eLC.GetValue(69, k, i) + "-" + MainWindow.eLC.GetValue(69, k, i + 1) + "|";
                                        i++;
                                    }

                                }
                                var subi = itens.Split('|');
                                subi = subi.Where(s => s != "").ToArray();




                                for (int i = 0; i < subi.Length; i++)
                                {

                                    var tta = subi[i].Split('-');
                                    dataGridView1.Rows.Add(new object[] { tta[0].Replace(" ", ""), tta[1].Replace(" ", "") });
                                    height_Grid++;
                                }
                                break;
                            }

                        }


                        Height = 32 * height_Grid + 2 +7;


                        dataGridView1.ClearSelection();


                    }
                    catch
                    {
                        //richTextBox1.Text = "Text parse error";
                    }
                    break;
                case 74:
                    try
                    {
                        int height_Grid = 0;
                        if (MainWindow.database.Tasks != null)
                        {
                            for (int t = 0; t < MainWindow.database.Tasks.Length; t++)
                            {
                                if (MainWindow.database.Tasks[t].ID == IdListRecipe)
                                {
                                    for (int m = 0; m < MainWindow.database.Tasks[t].m_Award_S.m_CandItems.Length; m++)
                                    {
                                        for (int i = 0; i < MainWindow.database.Tasks[t].m_Award_S.m_CandItems[m].m_AwardItems.Length; i++)
                                        {

                                            dataGridView1.Rows.Add(new object[] { MainWindow.database.Tasks[t].m_Award_S.m_CandItems[m].m_AwardItems[i].m_ulItemTemplId, "[ " + MainWindow.database.Tasks[t].m_Award_S.m_CandItems[m].m_AwardItems[i].m_ulItemNum + " UN", Convert.ToDecimal(MainWindow.database.Tasks[t].m_Award_S.m_CandItems[m].m_AwardItems[i].m_fProb * 100) + "%  " + " ]"});
                                            height_Grid++;
                                        }
                                    }
                                }

                            }
                        }


                        Height = 32 * height_Grid + 2 +7;


                        dataGridView1.ClearSelection();


                    }
                    catch
                    {
                        
                    }
                    break;
                default:
                    break;
            }

            this.BackgroundImage = configs.ImgsFiles.TrueStretchImage(Resources._base, this.Width, this.Height);

        }


        private void dataGridView_recipes_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            string value = "";
            int ID = Extensions.GetIdItemFromGDV(dgv.Rows[e.RowIndex].Cells[0].Value.ToString() ?? "0");           
            bool fi = false;
            if (ID != 0)
            {
                try
                {
                    
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

                            if (ID == int.Parse(MainWindow.eLC.GetValue(La, ef, 0))/* || value.Contains(b.ToString())*/)
                            {
                                string path = Path.GetFileName(value);
                                if (MainWindow.database.sourceBitmap != null && MainWindow.database.ContainsKey(path))
                                {
                                    if (MainWindow.database.ContainsKey(path))
                                    {
                                        ((TextAndImageCell)dgv.Rows[e.RowIndex].Cells[0]).Image = Extensions.ResizeImage(MainWindow.database.images(path), 32, 32);
                                        dgv.Rows[e.RowIndex].Cells[0].Value = "[" + ID + "] - " + MainWindow.eLC.GetValue(La, ef, posN);
                                        fi = true;

                                        Color clr = Color.White;
                                        try
                                        {
                                            
                                            if (MainWindow.eLC.GetValue(La, ef, posN).StartsWith("^"))
                                            {
                                                clr = Extensions.ColorHex(MainWindow.eLC.GetValue(La, ef, posN));

                                            }
                                            else
                                            {
                                                clr = Helper.getByID(MainWindow.database.item_color[ID]);
                                            }
                                        }
                                        catch (Exception)
                                        { clr = Color.White; }

                                        dgv.Rows[e.RowIndex].Cells[0].Style.ForeColor = clr;

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
                //}

            }
        }



        [DllImport("user32.dll")]
		public static extern bool GetCursorPos(out Point lpPoint);
		[DllImport("user32.dll")]
		public static extern IntPtr WindowFromPoint(Point p);
		[DllImport("user32.dll")]
		public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
		IntPtr OwnerHandle;
		Point InitialCursorPosition;
		bool IsItemToolTip;



		public void ShowToolTip(IntPtr WindowHandle, int IdListRecipe, int lista, int TimeOut, double LineHeihgt, double WordsMultiplier)
		{
			if ((Text == null) || (Text == ""))
			{
				return;
			}
			OwnerHandle = WindowHandle;
			GetCursorPos(out Point gpoint);
            if (WindowFromPoint(gpoint) != OwnerHandle)
            {
                return;
            }
            tmrHideMe.Enabled = false;
			Cursor cursor = Cursor;
			Point position = Cursor.Position;
			InitialCursorPosition = position;
			SetText(IdListRecipe, lista);
			//Size sz = TextRenderer.MeasureText("                         ",null);
			//Width =/* sz.Width */ 300;
			//Height =/* sz.Height +*/ 300;
			Cursor cursor2 = Cursor;
			int WWidth = Cursor.Position.X + 25;
			Cursor cursor3 = Cursor;
			int HHeight = Cursor.Position.Y + 25;
			bool flag = false;
			if (WWidth + Width > SystemInformation.VirtualScreen.Width)
			{
				WWidth = SystemInformation.VirtualScreen.Width - Width;
				flag = true;
			}
			Rectangle rectangle2 = SystemInformation.VirtualScreen;
			if ((HHeight + Height) > rectangle2.Height)
			{
				HHeight = SystemInformation.VirtualScreen.Height - Height;
			}
			else if (!flag)
			{
				goto Label_01DC;
			}
			Cursor cursor4 = Cursor;
			if (Cursor.Position.X >= WWidth)
			{
				Cursor cursor5 = Cursor;
				if (Cursor.Position.Y >= HHeight)
				{
					Cursor cursor6 = Cursor;
					WWidth = Cursor.Position.X + 20;
					Cursor cursor7 = Cursor;
					HHeight = Cursor.Position.Y + 15;
				}
			}
		Label_01DC:
			Left = WWidth;
			Top = HHeight;
			if (!Visible)
			{
				Left = 0x1388;
				Top = 0x1388;
				SetWindowPos(Handle, 1, 0, 0, 0, 0, 0x13);
				Show();
				Application.DoEvents();
				Left = WWidth;
				Top = HHeight;
				Application.DoEvents();
				SetWindowPos(Handle, -1, 0, 0, 0, 0, 0x13);
			}
			IsItemToolTip = false;
			if (TimeOut > 0)
			{
				tmrHideMe.Interval = TimeOut;
				tmrHideMe.Enabled = true;
			}
		}

		private void TmrHideMe_Tick(object sender, EventArgs e)
		{
			if (!IsItemToolTip)
			{
				Hide();
			}
			tmrHideMe.Enabled = false;
		}

        private void dataGridView1_RowHeightInfoPushed(object sender, DataGridViewRowHeightInfoPushedEventArgs e)
        {

        }
    }
}

