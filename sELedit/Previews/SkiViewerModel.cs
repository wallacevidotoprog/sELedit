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
using WPF_SkiViewer = sELedit.Previews.SkiViewer;

namespace sELedit.Previews
{
    public partial class SkiViewerModel : Form
    {
        public string FILE { get; set; }
        public byte[] SKI { get; set; }
        public eELedit.Previews.Models.TexturesFromBytes[] _texturesBytes { get; set; }

        public SkiViewerModel(string _file)
        {
            FILE = FILE;
            InitializeComponent();

        }

        public void read()
        {
            WPF_SkiViewer WPF = elementHost_SKI.Child as WPF_SkiViewer;
            WPF.SKI = SKI;
            WPF._texturesBytes = _texturesBytes;
            WPF.Prepare();
        }

        public void ViewerSKI(string txt)
        {
            string caminho = $@"C:\xampp\htdocs\FTP CELULAR\Perfect World\Perfect World PWLoko\element\{txt.Split(new string[] { @"\" }, StringSplitOptions.None)[0].ToLower()}.pck";
            if (File.Exists(caminho))
            {
                try
                {
                    var pck = new PCKs(caminho);

                    using (StreamReader sr = new StreamReader(new MemoryStream(((IEnumerable<byte>)pck.ReadFile(pck.PckFile, (pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.StartsWith(txt.ToLower())))).ElementAt<PCKFileEntry>(0))).ToArray<byte>()), Encoding.GetEncoding("GBK")))
                    {
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            if (line.StartsWith("SkinModelPath") && line.ToUpper().EndsWith("SMD"))
                            {
                                line = line.Replace("SkinModelPath:", null).Replace(" ", null).ToLower();
                                var smd = new Previews.Structures.SmdFile(((IEnumerable<byte>)pck.ReadFile(pck.PckFile, (pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.StartsWith(line.ToLower())))).ElementAt<PCKFileEntry>(0))).ToArray<byte>());
                                if (smd.SkiFile is null)
                                {
                                    var sourceSKI = pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.EndsWith((Path.GetFileNameWithoutExtension(txt) + ".ski").ToLower())));
                                    SKI = ((IEnumerable<byte>)pck.ReadFile(pck.PckFile, sourceSKI.ElementAt<PCKFileEntry>(0))).ToArray<byte>();
                                    break;
                                }
                                else
                                {
                                    var sourceSKI = pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.EndsWith((smd.SkiFile).ToLower())));
                                    SKI = ((IEnumerable<byte>)pck.ReadFile(pck.PckFile, sourceSKI.ElementAt<PCKFileEntry>(0))).ToArray<byte>();
                                    break;
                                }
                                
                            }
                        }
                    }
                   var source = pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.StartsWith(txt.Replace(Path.GetFileName(txt), null).ToLower()) && i.Path.EndsWith("dds")));
                    _texturesBytes = new eELedit.Previews.Models.TexturesFromBytes[source.Count()];
                    int nun = 0;
                    foreach (var item in source)
                    {
                        _texturesBytes[nun] = new eELedit.Previews.Models.TexturesFromBytes();
                        _texturesBytes[nun]._name = Path.GetFileName(item.Path);
                        _texturesBytes[nun]._file = pck.ReadFile(pck.PckFile, source.ElementAt(nun)).ToArray();
                        nun++;
                    }

                    read();
                }
                catch (Exception EF)
                {
                    if (SKI == null)
                    {
                        
                        SKI = Properties.Resources.models_error_error;
                        _texturesBytes = null;
                        read();
                    }
                    else
                    {
                        read();
                    } 


                    //MessageBox.Show(EF.Message);
                }
                

            }
        }

        private void SkiViewerModel_Load(object sender, EventArgs e)
        {

            ViewerSKI(FILE);

        }
    }
}
