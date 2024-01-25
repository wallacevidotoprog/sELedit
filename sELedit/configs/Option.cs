using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using Microsoft.VisualBasic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using LBLIBRARY;

namespace sELedit
{
    public partial class Option : Form
    {
        public Option(MainWindow fm)
        {
            this.Main_form = fm;
            InitializeComponent();
        }
        MainWindow Main_form;
        public string SurfacesPath;
        public string ElementsPath;
        public string ConfigsPath;
        List<LBLIBRARY.PWHelper.Desc> ItemExtDesc;
        List<LBLIBRARY.PWHelper.ShopIcon> ShopIcons;
        LBLIBRARY.PWHelper.Elements Elem;
        LBLIBRARY.ArchiveEngine SurfacesPck;
        bool ShopIconsAreLoading;
        bool ElementsIsLoading;
        LBLIBRARY.PWHelper lb = new LBLIBRARY.PWHelper();
        Thread ConfigThread;
        Thread ElementsThread;
        Thread SurfacesThread;
        private void Elements_data_search_Click(object sender, EventArgs e)
        {
            if (Dialog_elements.ShowDialog() == DialogResult.OK)
                Elements_path_textbox.Text = Dialog_elements.FileName;
        }
        private void Surfaces_pck_search_Click(object sender, EventArgs e)
        {
            if (Dialog_surfaces.ShowDialog() == DialogResult.OK)
                Surfaces_path_textbox.Text = Dialog_surfaces.FileName;
        }
        private void Configs_search_Click(object sender, EventArgs e)
        {
            if (Dialog_configs.ShowDialog() == DialogResult.OK)
            {
                Configs_path.Text = Dialog_configs.FileName;
            }
        }
        private void Exit_options_button(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Accept_button_Click(object sender, EventArgs e)
        {
            if (File.Exists(this.Configs_path.Text))
            {
                LoadConfig();
                this.ConfigsPath = Configs_path.Text;
            }
            if (File.Exists(this.Elements_path_textbox.Text))
            {
                ElementsPath = Elements_path_textbox.Text;
                LoadElementsdata();
            }
            if (File.Exists(this.Surfaces_path_textbox.Text))
            {
                SurfacesPath = Surfaces_path_textbox.Text;
                LoadSurfaces();
            }
            this.Hide();

        }
        void LoadConfig()
        {
            ConfigThread = new Thread(delegate ()
            {
                ArchiveEngine PckConfig = new ArchiveEngine(ConfigsPath);
                ItemExtDesc = LBLIBRARY.PWHelper.LoadItemExtDesc(PckConfig);
                Main_form.Item_ext_desc = ItemExtDesc;
                PckConfig.Dispose();
            });
            ConfigThread.Start();
        }
        bool CheckInUse(string FilePath)
        {
            try
            {
                using (Stream stream = new FileStream(FilePath, FileMode.Open))
                {
                    stream.Close();
                }
                return false;
            }
            catch
            {
                return true;
            }
        }
        void LoadElementsdata()
        {
            ElementsThread = new Thread(delegate ()
            {
                if (CheckInUse(ElementsPath) == false)
                {
                    ElementsIsLoading = true;
                    Elem = LBLIBRARY.PWHelper.ReadElements(ElementsPath, Application.StartupPath, true);
                    Main_form.Elem = Elem;
                    Main_form.ElementsLoaded(false);
                    if (Elem != null && SurfacesPck != null)
                    {
                        List<LBLIBRARY.PWHelper.Icon> d = null;
                        while (d == null)
                        {
                            if (ShopIconsAreLoading == true)
                            {
                                continue;
                            }
                            d = LBLIBRARY.PWHelper.LoadIconList(Elem, SurfacesPck);
                            SurfacesPck.Dispose();
                        }
                        Main_form.ElementsLoaded(true);
                    }
                    ElementsIsLoading = false;
                }
                else
                {
                    MessageBox.Show("Elements.data is in use", "File is locked", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            });
            ElementsThread.Start();
        }
        void LoadSurfaces()
        {
            SurfacesThread = new Thread(delegate ()
            {
                if (ShopIcons == null || Reload_checkbox.Checked == true)
                {
                    ShopIconsAreLoading = true;
                    Main_form.IsLinking = true;
                    SurfacesPck = new ArchiveEngine(SurfacesPath);
                    ShopIcons = LBLIBRARY.PWHelper.ReadSurfacesIcons(SurfacesPck);
                    ShopIcons = ShopIcons.OrderByDescending(z => z.Name).ToList();
                    Main_form.Surfaces_images = ShopIcons;
                    //Main_form.RefreshCurrentShopImage();
                    Main_form.LinkedImage = LBLIBRARY.PWHelper.LinkImages(ShopIcons);
                    Main_form.CreateIconsForm();
                    ShopIconsAreLoading = false;
                    if (ElementsIsLoading == false)
                    {
                        SurfacesPck.Dispose();
                    }
                }
            });
            SurfacesThread.Start();
        }
        public void Setpath(string e, string s, string c)
        {
            Elements_path_textbox.Text = e;
            Surfaces_path_textbox.Text = s;
            Configs_path.Text = c;
            Accept_button_Click(null, null);
        }
        public void RefreshLanguage(int Language)
        {
            if (Language == 1 && Accept_button.Text != "Принять")
            {
                this.Text = "Настройки";
                Accept_button.Text = "Принять";
                Exit_button.Text = "Отмена";
                groupBox1.Text = "Дополнительно";
                Reload_checkbox.Text = "Перезагрузить Surfaces.pck";
            }
            else if (Language == 2 && Accept_button.Text != "Accept")
            {
                this.Text = "Option";
                Accept_button.Text = "Accept";
                Exit_button.Text = "Cancel";
                groupBox1.Text = "Extra";
                Reload_checkbox.Text = "Reload Surfaces.pck";
            }
        }
    }
}
