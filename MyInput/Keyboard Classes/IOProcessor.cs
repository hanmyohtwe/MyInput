using System;
using System.Collections.Generic;
using System.Text;
using System.Media;
using MyInput.Utilities;
using System.Collections;

namespace MyInput.Keyboard_Classes
{
    public class IOProcessor
    {
        public IOProcessor(KeyProcessor kpr)
        {
            SetKPR(kpr);
            bf = new Buffer();
            log = new Log();
            cfg = new Config("MyInput\\");
        }

        public void SetKPR(KeyProcessor kpr)
        {
            kp = kpr;
            Script = kpr.getscript();
        }

        internal Log log;
        internal Config cfg;
        public bool Income(int vkCode,bool gis,bool shf,KeyboardLayout kl)
        {
            Key k = new Key();
            k.alt = gis;
            k.shift = shf;
            k.vkCode = vkCode;
            if (k.vkCode == 8)
                k.ch = "delete";
            else
            {
                if (mfm != null)
                {
                    if (mfm.dkstate != "none")
                    {
                        k = kl.ProcessKey(k, mfm.dkstate);
                        k.ch = k.ch.Replace("◌", "");
                    }
                    else
                    {
                        k = kl.ProcessKey(k);
                        k.ch = k.ch.Replace("◌", "");
                    }
                }
            }
            if (k.ch.StartsWith("[") && k.ch.EndsWith("]"))
            {
                if (mfm != null)
                {
                    mfm.dkChange(k.ch.Replace("[", "").Replace("]", ""));
                    return true;
                }
            }
            else
            {
                mfm.dkChange("none");
            }
            bool eat = false;
            foreach (string s in CompatibilityDecompose(k.ch))
            {
                eat = eat | Income(s);
            }
            // what will we do with ear?
            return eat;
        }

        public void SetMainHandle(Main mfm)
        {
            this.mfm = mfm;
        }

        public List<string> CompatibilityDecompose(string ch)
        {
            List<string> al = new List<string>();
            if (kp.getscript() == "MM Unicode" && mfm.virtualize)
            {
                switch (ch)
                {
                    case "\u1081":
                        al.Add("\u1080");
                        al.Add("\u108d");
                        break;
                    case "\u1082":
                        al.Add("\u1080");
                        al.Add("\u1090");
                        break;
                    case "\u1083":
                        al.Add("\u1080");
                        al.Add("\u108d");
                        al.Add("\u1090");
                        break;
                    case "\u108e":
                        al.Add("\u108d");
                        al.Add("\u1090");
                        break;
                    case "\u1091":
                        al.Add("\u1090");
                        break;
                    case "\u1084":
                        al.Add("ႅ");
                        break;
                    case "\u1092":
                        al.Add("\u1090");
                        al.Add("ု");
                        break;
                    case "\u1094":
                    case "\u1095":
                        al.Add("\u1037");
                        break;
                    case "\u1033":
                        al.Add("ု");
                        break;
                    case "\u1034":
                        al.Add("ူ");
                        break;
                    case "\u1058":
                        al.Add("\u100c");
                        al.Add("\u1070");
                        break;
                    case "\u1035":
                        al.Add("ိ");
                        al.Add("ံ");
                        break;
                    case "\u1065":
                    case "\u1066":
                    case "\u1067":
                        al.Add("{BEEP}");
                        break;
                    case "ၙ":
                        al.Add("န");
                        break;
                    default:
                        al.Add(ch);
                        break;
                }
            }
            else
            {
                al.Add(ch);
            }
            return al;
        }


        private Main mfm;
        public string Script;
        private KeyProcessor kp;
        private Buffer bf;
        public bool Income(string chr)
        {
            if (kp == null)
                return false;
            if (chr == "") return false;
            KeyProcessorReturn x = kp.ProcessKey(chr);
            if (x == null)
            {
                if (chr == "delete")
                {
                    Output("{BS}");
                    bf.PopChars(1);
                    return true;
                }
                else if (chr == "{BEEP}")
                {
                    beep();
                    return true;
                }
                else
                {
                    Output(chr);
                    bf.Append(chr);
                    return true;
                }
            }
            else
            {
                if (x.beep)
                    beep();
                // Better Optimized Method Needed
                for (int i = 0; i < x.leftcontext.Length; i++)
                {
                    Output("{BS}");
                }
                bf.PopChars(x.leftcontext.Length);
                bf.Append(x.output);
                Output(x.output);
                return true;
            }
        }

        private void beep()
        {
            SystemSounds.Beep.Play();
            log.write("IO-Output: Beep");
        }

        private void Output(string ch)
        {
            if (ch.Length > 0)
            {
                if (ch == "{BS}")
                {
                    log.write("IO-Output: Delete Char");
                    //SendCH.Send('\u0008');
                    SendCH.Del();
                }
                else
                {
                    log.write("IO-Output: " + ch);
                    SendCH.Send(ch);
                }
            }
            //System.Windows.Forms.SendKeys.Send(ch);
        }
    }
}
