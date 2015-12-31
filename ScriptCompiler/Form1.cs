using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.IO.Compression;

namespace ScriptCompiler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                saveFileDialog1.FileName = openFileDialog1.FileName.Substring(0, openFileDialog1.FileName.Length - 4) + ".ikb";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    KeyboardLanguage.Parser par = new KeyboardLanguage.Parser(openFileDialog1.FileName, "98761197agde5d2g13asdh8wjktwa6f5");
                    ArrayList ENI = par.getENI();
                    ArrayList ENM = par.getENM();
                    ArrayList ENO = par.getENO();
                    FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                    System.IO.Compression.DeflateStream df = new DeflateStream(fs, CompressionMode.Compress);
                    StreamWriter sw = new StreamWriter(df, Encoding.Unicode);
                    for (int i = 0; i < ENI.Count; i++)
                    {
                        foreach (char c in ((string)ENI[i]).ToCharArray())
                        {
                            int y = ((int)c) - 317;
                            sw.Write((char)y);
                        }
                        sw.Write((char)5);
                        foreach (char c in ((string)ENM[i]).ToCharArray())
                        {
                            int y = ((int)c) - 317;
                            sw.Write((char)y);

                        }
                        sw.Write((char)5);
                        foreach (char c in ((string)ENO[i]).ToCharArray())
                        {
                            int y = ((int)c) - 317;
                            sw.Write((char)y);
                        }
                        sw.Write((char)5);
                    }
                    df.Close();
                    //sw.Close();
                    fs.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int c = 1000;
            int y = (((int)c) * 7) - 317;
            MessageBox.Show(y.ToString());
            int x = (y + 317) / 7;
            MessageBox.Show(x.ToString());
        }
    }
}