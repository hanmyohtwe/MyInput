using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.IO.Compression;
using System.Threading;

namespace MyInput.Keyboard_Language
{
    class Decryptor
    {
        public Decryptor(string filename, string key)
        {
            if (key != "98761197agde5d2g13asdh8wjktwa6f5")
                return;
            ENI = new ArrayList();
            ENO = new ArrayList();
            ENM = new ArrayList();

            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            DeflateStream df = new DeflateStream(fs, CompressionMode.Decompress);
            StreamReader sr = new StreamReader(df,Encoding.Unicode);
            string tmp = "";
            int state = 0;
            while ( !sr.EndOfStream )
            {
                int i = sr.Read();
                if (i == 5)
                {
                    switch (state)
                    {
                        case 0:
                            ENI.Add(tmp);
                            tmp = "";
                            state = 1;
                            break;
                        case 1:
                            ENM.Add(tmp);
                            tmp = "";
                            state = 2;
                            break;
                        case 2:
                            ENO.Add(tmp);
                            tmp = "";
                            state = 0;
                            break;
                    }
                }
                else
                {
                    i = (i + 317);
                    tmp += (char)i;
                }
            }
            fs.Close();
        }

        public ArrayList getENI()
        {
            return ENI;
        }

        public ArrayList getENM()
        {
            return ENM;
        }

        public ArrayList getENO()
        {
            return ENO;
        }
        private ArrayList ENI;
        private ArrayList ENO;
        private ArrayList ENM;
    }
}
