using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using tasks;

namespace sELedit.ups
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        string FileName = @"C:\xampp\htdocs\FTP CELULAR\data\tasks.data";

        //int Tasks;
        public static ATaskTempl[] Tasks;
        private void Form1_Load(object sender, EventArgs ee)
        {

            FileStream input = File.OpenRead(FileName);
            BinaryReader binaryStream = new BinaryReader(input);
            TASK_PACK_HEADER tph = new TASK_PACK_HEADER(binaryStream);
            if (tph.magic == -1819966623 || tph.magic == 0)
            {
                if (!GlobalData.Versions.Contains(tph.version))
                {
                    binaryStream.Close();
                    input.Close();
                    Cursor = Cursors.Default;
                   
                }
                else
                {
                   
                    GlobalData.NewID = 0;
                    GlobalData.version = tph.version;
                    
                    int[] pOffs = new int[tph.item_count];
                    for (int i = 0; i < tph.item_count; i++)
                    {
                        pOffs[i] = binaryStream.ReadInt32();
                    }
                    Tasks = new ATaskTempl[tph.item_count];

                    IProgress<int> progress = new Progress<int>(value =>
                    {
                        
                    });
                    if (true)
                    {
                        var p = 0;
                        Parallel.For(0, tph.item_count, i =>
                        {
                            byte[] bytes = null;
                            lock (binaryStream)
                            {
                                binaryStream.BaseStream.Seek(pOffs[i], SeekOrigin.Begin);
                                int count = ((i < pOffs.Length - 1)
                                                ? pOffs[i + 1]
                                                : (int)binaryStream.BaseStream.Length) - pOffs[i];
                                bytes = binaryStream.ReadBytes(count);
                            }

                            using (var ms = new MemoryStream(bytes))
                            using (var br = new BinaryReader(ms))
                            {
                                if (true)
                                {
                                    try
                                    {
                                        Tasks[i] = new ATaskTempl(tph.version, br);
                                    }
                                    catch (Exception e)
                                    {
                                        //MessageBox.Show(String.Format(GlobalProgramData.GetLocalization(521), i));
                                        //if (GlobalProgramData.Debug)
                                        //    Extensions.WriteLog("Error load task! Task index: " + i);
                                    }
                                }
                                else
                                    Tasks[i] = new ATaskTempl(tph.version, br);
                            }

                            Interlocked.Increment(ref p);
                            if (p % 100 == 0)
                            {
                                progress.Report(p / 2);
                                Application.DoEvents();
                            }
                        }); //3.5 мс для 146 версии
                    }
                    else
                    {
                        for (int i = 0; i < tph.item_count; i++)
                        {
                           // GlobalProgramData.Tasks[i] = new ATaskTempl(tph.version, binaryStream, pOffs[i]);
                            if (i % 100 == 0)
                            {
                                progress.Report(i / 2);
                                Application.DoEvents();
                            }
                        }
                    }

                    binaryStream.Close();
                    input.Close();


                    //if (skipBrokenTasks)
                        Tasks = Tasks.Where(it => it != null).ToArray();


                    var sdsd = Tasks[0].m_Award_F;
                    var sds3 = Tasks[0].m_Award_FPointer;

                    var sdsd1 = Tasks[0].m_Award_S;

                    int sdsdd = sdsd1.m_CandItems[0].m_AwardItems[0].m_ulItemTemplId;


                    void add_node(ATaskTempl[] tasks, TreeNodeCollection nodes, int GMIconIndex)
                    {
                        for (var i = 0; i < tasks.Length; i++)
                        {
                            tasks[i].AddNode(nodes, GMIconIndex);
                            if (tasks[i].pSub.Length > 0)
                                add_node(tasks[i].pSub, nodes[i].Nodes, GMIconIndex);
                        }
                    }

                    
                    
                }
            }
            else
            {
                binaryStream.Close();
                input.Close();
            }

        }
    }
}
