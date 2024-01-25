using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sELedit.NOVO
{
    public partial class item_ext_desc : Form
    {
        int ID;
        public item_ext_desc(int idItem)
        {
            InitializeComponent();
            ID = idItem;
        }



        private void richTextBox_DESC_PRE_KeyPress(object sender, KeyPressEventArgs e)
        {
            //MessageBox.Show(e.KeyChar.ToString());
            if (e.KeyChar == (Int32)Keys.Enter)
            {
                e.Handled = true;
                int Explanation_location;
                Explanation_location = richTextBox_DESC_PRE.SelectionStart;
                var ADD = @"\r";
                richTextBox_DESC_PRE.Text = richTextBox_DESC_PRE.Text.Insert(Explanation_location, ADD);
                SendKeys.Send("{Backspace}");
                richTextBox_DESC_PRE.SelectionStart = Explanation_location;
                SendKeys.Send("{Right}"); SendKeys.Send("{Right}");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.FullOpen = true;
            try
            {
                colorDialog.Color = Color.FromArgb(int.Parse(textBox_ColorCod.Text.Substring(1, 6), NumberStyles.HexNumber));
            }
            catch
            {
                colorDialog.Color = Color.Black;
            }
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                var colorcod = ColorCod(colorDialog.Color);
                textBox_ColorCod.Text = colorcod;
                pictureBox_Color.BackColor = colorDialog.Color;
                //if (checkBox_ToClipboard.Checked)
                Clipboard.SetText(colorcod);
            }
        }
        public string ColorCod(Color color)
        {
            Convert.ToString((object)new int[3]
            {
        (int) color.R,
        (int) color.G,
        (int) color.B
            });
            return string.Format("^{0:x2}{1:x2}{2:x2}", (object)color.R, (object)color.G, (object)color.B).ToUpper();
        }
        private void button_SETCOR_Click(object sender, EventArgs e)
        {
            int Explanation_location;
            Explanation_location = richTextBox_DESC_PRE.SelectionStart;
            string COR = textBox_ColorCod.Text;
            richTextBox_DESC_PRE.Text = richTextBox_DESC_PRE.Text.Insert(Explanation_location, textBox_ColorCod.Text);
        }
        private void richTextBox_DESC_PRE_TextChanged(object sender, EventArgs e)
        {
            string text = this.richTextBox_DESC_PRE.Text;
            SetText(text);
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

        private void item_ext_desc_Load(object sender, EventArgs e)
        {
            if (MainWindow.database.item_ext_desc.ContainsKey(ID.ToString()))
            {
                richTextBox_DESC_PRE.AppendText(MainWindow.database.item_ext_desc[ID.ToString()].ToString());
            }
            
        }

        private void item_ext_desc_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MainWindow.database.item_ext_desc.ContainsKey(ID.ToString()))
            {
                string x = richTextBox_DESC_PRE.Text;
                MainWindow.database.item_ext_desc.Remove(ID.ToString());
                MainWindow.database.item_ext_desc.Add(ID.ToString(), x);
            }
            else
            {
                string x = richTextBox_DESC_PRE.Text;
                MainWindow.database.item_ext_desc.Add(ID.ToString(), x);
            }
        }

        private void richTextBox_DESC_POS_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox_color_Item_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.FullOpen = true;
            try
            {
                colorDialog.Color = Color.FromArgb(int.Parse(textBox_ColorCod.Text.Substring(1, 6), NumberStyles.HexNumber));
            }
            catch
            {
                colorDialog.Color = Color.Black;
            }
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                var colorcod = ColorCod(colorDialog.Color);
                textBox_ColorCod.Text = colorcod;
                pictureBox_Color.BackColor = colorDialog.Color;
                //if (checkBox_ToClipboard.Checked)
                Clipboard.SetText(colorcod);
            }
            int Explanation_location;
            Explanation_location = richTextBox_DESC_PRE.SelectionStart;
            string COR = textBox_ColorCod.Text;
            richTextBox_DESC_PRE.Text = richTextBox_DESC_PRE.Text.Insert(Explanation_location, textBox_ColorCod.Text);
        }
    }
}
