using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using MyInput.Keyboard_Language;
using System.Windows.Forms;

namespace MyInput.Keyboard_Classes
{
    public class KeyProcessor
    {
        public KeyProcessor(string Script)
        {
            if (Script == "NULL")
            {
                LeftContext = new ArrayList();
                Keys = new ArrayList();
                Output = new ArrayList();
            }
            else
            {
                string file = Directory.GetCurrentDirectory() + "\\Scripts\\" + Script;
                if (File.Exists(file + ".ikl"))
                {
                    KeyboardLanguage.Parser par = new KeyboardLanguage.Parser(file + ".ikl", "98761197agde5d2g13asdh8wjktwa6f5");
                    if (par.RES != "OK")
                    {
                        MessageBox.Show("Error in Input Script: " + file + ".ikl\r\n" + par.RES + "\r\nOn Line:" + par.line);
                        Application.Exit();
                    }
                    LeftContext = par.getENI();
                    Keys = par.getENM();
                    Output = par.getENO();
                }
                else if (File.Exists(file + ".ikb"))
                {
                    Keyboard_Language.Decryptor par = new Decryptor(file + ".ikb", "98761197agde5d2g13asdh8wjktwa6f5");
                    LeftContext = par.getENI();
                    Keys = par.getENM();
                    Output = par.getENO();
                }
                else
                {
                    MessageBox.Show(@"MyInput cannot read the keyboard script.
It may be locked or corrupted, or not exists anymore");
                    Application.Exit();
                }
            }
            buffer = new Buffer();
            sname = Script;
        }

        public string getscript()
        {
            return sname;
        }

        private string sname;
        public KeyProcessorReturn ProcessKey(string c)
        {
            KeyProcessorReturn kpr = new KeyProcessorReturn();
            string btmp = buffer.getBuffer();
            ArrayList matchs = new ArrayList();

            for (int i = 0; i < Keys.Count; i++)
            {
                string k = (string)Keys[i];
                string lcontext = (string)LeftContext[i];
                if (k == c)
                {
                    if (lcontext.Length == 0)
                    {
                        if (btmp.Length == 0)
                        {
                            kpr.key = c;
                            kpr.leftcontext = "";
                            kpr.output = (string)Output[i];
                            kpr.beep = processOutput(kpr.output);
                            kpr.output = kpr.output.Replace("_#_beep", "");
                            kpr.output = kpr.output.Replace("_#_null", "");
                            return kpr;
                        }
                        else
                        {
                            matchs.Add(i);
                        }
                    }
                    else
                    {
                        string samelenbuf = ProcessBuffer(lcontext, btmp);
                        if (lcontext.Equals(samelenbuf))
                        {
                            matchs.Add(i);
                        }
                    }
                }
            }

            if (matchs.Count > 0)
            {
                int kpid = (int)matchs[0];
                for (int i = 0; i < matchs.Count; i++)
                {
                    string a = (string)LeftContext[kpid];
                    int x = (int)matchs[i];
                    string b = (string)LeftContext[x];
                    if (b.Length > a.Length)
                        kpid = x;
                }

                kpr.key = c;
                kpr.leftcontext = (string)LeftContext[kpid];
                kpr.output = (string)Output[kpid];
                kpr.beep = processOutput(kpr.output);
                kpr.output = kpr.output.Replace("_#_beep", "");
                kpr.output = kpr.output.Replace("_#_null", "");
                return kpr;
            }
            return null;
        }

        private bool processOutput(string s)
        {
            if (s.IndexOf("_#_beep") != -1)
                return true;
            return false;                
        }

        private string ProcessBuffer(string tmp, string btmp)
        {
            if (tmp.Length == btmp.Length)
            {
                return btmp;
            }
            else if (btmp.Length > tmp.Length)
            {
                return btmp.Substring(btmp.Length - tmp.Length);
            }
            else
            {
                return null;
            }
        }

        private Keyboard_Classes.Buffer buffer;
        private ArrayList LeftContext;
        private ArrayList Keys;
        private ArrayList Output;
    }

    public class KeyProcessorReturn
    {
        public string leftcontext;
        public string key;
        public bool beep;
        public string output;
    }

}
