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
namespace sELedit.NOVO
{
    
    public partial class ToolTip_addons : Form
	{
		public ToolTip_addons()
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

        public void ShowToolTip(IntPtr WindowHandle, int IdListRecipe)
		{
			this.ShowToolTip(WindowHandle, IdListRecipe, 0, -1.0, -1.0);
		}
		public void SetText(int IdListRecipe)
		{


            string line = "";
            int cont = 0;
            listBox_adds.Items.Clear();
            int[] IdCombo = new int[12];
            bool Suc = false;
            int a = 1;
            for (int k = 0; k < MainWindow.eLC.Lists[90].elementValues.Length; k++)
            {
                var id_RP = int.Parse(MainWindow.eLC.GetValue(90, k, 0));

                if (id_RP == IdListRecipe)
                {

                    IdCombo[0] = int.Parse(MainWindow.eLC.GetValue(90, k, 15));
                    IdCombo[1] = int.Parse(MainWindow.eLC.GetValue(90, k, 16));
                    IdCombo[2] = int.Parse(MainWindow.eLC.GetValue(90, k, 17));
                    IdCombo[3] = int.Parse(MainWindow.eLC.GetValue(90, k, 18));
                    IdCombo[4] = int.Parse(MainWindow.eLC.GetValue(90, k, 19));
                    IdCombo[5] = int.Parse(MainWindow.eLC.GetValue(90, k, 20));
                    IdCombo[6] = int.Parse(MainWindow.eLC.GetValue(90, k, 21));
                    IdCombo[7] = int.Parse(MainWindow.eLC.GetValue(90, k, 22));
                    IdCombo[8] = int.Parse(MainWindow.eLC.GetValue(90, k, 23));
                    IdCombo[9] = int.Parse(MainWindow.eLC.GetValue(90, k, 24));
                    IdCombo[10] = int.Parse(MainWindow.eLC.GetValue(90, k, 25));

                    int con = 2; 
                    for (int i = 0; i < IdCombo.Length; i++)
                    {
                        if (IdCombo[i] != 0)
                        {
                            listBox_adds.Items.Add("(" + con + ") -" + IdCombo[i] + " - " + MainWindow.database._suite[IdCombo[i]]);
                            cont++;
                        }
                        
                        con++;
                    }
                }

                
                  
            }
            Height = 15 * cont+6;
            listBox_adds.Height = 15 * cont + 6;
            this.BackgroundImage = configs.ImgsFiles.TrueStretchImage(Resources._base, this.Width, this.Height);

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



		public void ShowToolTip(IntPtr WindowHandle, int IdListRecipe, int TimeOut, double LineHeihgt, double WordsMultiplier)
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
			SetText(IdListRecipe);
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

