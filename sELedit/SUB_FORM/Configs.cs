using sELedit.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace sELedit.configs
{
    public partial class Configs : Form
    {
        public Configs()
        {
            InitializeComponent();

           // this.BackgroundImage = configs.ImgsFiles.TrueStretchImage(Resources._base, this.Width, this.Height);
        }
        public string elementData { get; set; }
        public string configPCK { get; set; }
        public string surfacePCK { get; set; }
        public string tasksData { get; set; }
		public string gshop { get; set; }
		public string gshop1 { get; set; }
        public bool ATT { get; set; }


        Settings XmlData;
        // Tasks
        string caminho = Path.Combine(Application.StartupPath, "Settings.xml");

        private void Elements_data_search_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFileDialog.Filter = "elements.data|*.data|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    elementData = openFileDialog.FileName.ToString();
                    Elements_path_textbox.Text = elementData;
                }
            }
        }

        private void Surfaces_pck_search_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFileDialog.Filter = "surface.pck|*.pck|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    surfacePCK = openFileDialog.FileName.ToString();
                    Surfaces_path_textbox.Text = surfacePCK;
                }
            }
        }

        private void Configs_search_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFileDialog.Filter = "configs.pck|*.pck|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    configPCK = openFileDialog.FileName.ToString();
                    Configs_path.Text = configPCK;
                }
            }
        }

        private void Configs_Load(object sender, EventArgs e)
        {
				StreamReader reader = new StreamReader(caminho);			
				XmlSerializer deserializer = new XmlSerializer(typeof(Settings));
				object obj = deserializer.Deserialize(reader);
				XmlData = (Settings)obj;
				reader.Close();
				elementData = XmlData.ElementsDataPath; Elements_path_textbox.Text = elementData;
				configPCK = XmlData.ConfigsPckPath; Configs_path.Text = configPCK;
				surfacePCK = XmlData.SurfacesPckPath; Surfaces_path_textbox.Text = surfacePCK;
				tasksData = XmlData.TasksDataPath; textBox_Tasks.Text = tasksData;
				gshop = XmlData.GshopDataPath; textBox_gshop.Text = gshop;
				gshop1 = XmlData.Gshop1DataPath; textBox_gshop1.Text = gshop1;


		}

			private void Exit_button_Click(object sender, EventArgs e)
        {
            ATT = false;
            this.Close();
        }

        private void Accept_button_Click(object sender, EventArgs e)
        {
            elementData =Elements_path_textbox.Text ;
			surfacePCK =Surfaces_path_textbox.Text;
			configPCK =Configs_path.Text;
			tasksData = textBox_Tasks.Text;
			gshop = textBox_gshop.Text;
			gshop1 = textBox_gshop1.Text;

			XmlData.ElementsDataPath= elementData;
			XmlData.ConfigsPckPath= configPCK;
			XmlData.SurfacesPckPath= surfacePCK;
			XmlData.TasksDataPath = tasksData;
			XmlData.GshopDataPath = gshop;
			XmlData.Gshop1DataPath = gshop1;



			using (var writer = new StringWriter())
			{
				new XmlSerializer(XmlData.GetType()).Serialize(writer, XmlData);
				File.Delete(caminho);
				StreamWriter sw3 = new StreamWriter(caminho, true, Encoding.UTF8);
				sw3.Write(writer.ToString());
				sw3.Close();
			}
            ATT = true;
            this.Close();
            


        }

        private void button_Tasks_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFileDialog.Filter = "tasks.data|*.data|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                     tasksData= openFileDialog.FileName.ToString();
                    textBox_Tasks.Text = tasksData;
                }
            }
        }

		private void button_gshop_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				openFileDialog.Filter = "gshop.data|*.data|All files (*.*)|*.*";
				openFileDialog.FilterIndex = 1;
				openFileDialog.RestoreDirectory = true;

				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					gshop = openFileDialog.FileName.ToString();
					textBox_gshop.Text = gshop;
				}
			}
		}

		private void button_gshop1_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				openFileDialog.Filter = "gshop1.data|*.data|All files (*.*)|*.*";
				openFileDialog.FilterIndex = 1;
				openFileDialog.RestoreDirectory = true;

				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					gshop1 = openFileDialog.FileName.ToString();
					textBox_gshop1.Text = gshop1;
				}
			}
		}
	}
}
