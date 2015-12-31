using System;
using System.Collections.Generic;
using System.Text;
using cs_IniHandlerDevelop;
using System.Collections;

namespace MyInput.Keyboard_Classes
{
    public class KeyboardLayout
    {
        public KeyboardLayout(string name)
        {
            try
            {
                if (inis == null)
                {
                    inis = IniStructure.ReadIni("Layouts\\" + name + ".keylayout");
                    if (inis == null)
                        inis = IniStructure.ReadIni(name);
                    this.name = name;
                }
                if (inis.filename != "Layouts\\" + name + ".keylayout" && inis.filename != name)
                {
                    inis = null;
                    inis = IniStructure.ReadIni("Layouts\\" + name + ".keylayout");
                    if (inis == null)
                        inis = IniStructure.ReadIni(name);
                    this.name = name;
                }
            }
            catch
            {
                inis = new IniStructure();
            }
        }

        public Key ProcessKey(Key k, string state)
        {
            string ch = Convert.ToString(k.vkCode, 16);
            ch = ch.ToUpper();
            return ProcessKey(ch, k.shift, state);
        }

        public Key ProcessKey(Key k)
        {
            string ch = Convert.ToString(k.vkCode, 16);
            ch = ch.ToUpper();
            return ProcessKey(ch,k.shift,k.alt);
        }

        public string getname()
        {
            return inis.GetValue("MyInputKeyboardFile", "name");
        }

        public string getFont()
        {
            string s = inis.GetValue("MyInputKeyboardFile", "VKFont");
            if (s != null)
            {
                return s.Trim();
            }
            else
            {
                return "MyMyanmar";
            }
        }


        public string getFontSize()
        {
            string s = inis.GetValue("MyInputKeyboardFile", "VKFontSize");
            if (s != null)
            {
                return s.Trim();
            }
            else
            {
                return "9";
            }
        }

        private string name;
        public Key ProcessKey(string xch,bool shift,bool alt)
        {
            string s;
            if (shift)
            {
                if (alt)
                {
                    s = inis.GetValue("ALTSHIFT", xch);
                }
                else
                {
                    s = inis.GetValue("SHIFT", xch);
                }
            }
            else if (alt)
            {
                s = inis.GetValue("ALT", xch);
            }
            else
            {
                s = inis.GetValue("NORMAL", xch);
            }
            if (s == null) s = "";
            s = processValue(s);
            Key k = new Key();
            k.shift = shift;
            k.alt = alt;
            k.ch = s;
            return k;
        }

        private string processValue(string s)
        {
            int state = 0;
            string tmp = "";
            string cx = "";
            foreach (char c in s)
            {
                switch (state)
                {
                    case 0:
                        if (c == ' ' || c == '\t')
                        {
                        }
                        else if (c == 'U')
                        {
                            state = 1;
                        }
                        else
                        {
                            tmp += c;
                        }
                        break;
                    case 1:
                        if (c == '+')
                        {
                            state = 2;
                        }
                        else if (c == ' ' || c == '\t')
                        {
                            tmp += 'U';
                        }
                        else
                        {
                            tmp += 'U' + c;
                        }
                        break;
                    case 2:
                        if (cx.Length < 4)
                        {
                            cx += c;
                        }
                        else
                        {
                            tmp += (char)Convert.ToInt32(cx, 16);
                            state = 0;
                        }
                        break;
                }
            }
            if (cx.Length == 4)
            {
                tmp += (char)Convert.ToInt32(cx, 16);
            }
            return tmp;
        }

        public Key ProcessKey(string xch, bool shift, string state)
        {
            string s;
            if (shift)
            {
                s = inis.GetValue(state + "SHIFT", xch);
            }
            else
            {
                s = inis.GetValue(state, xch);
            }
            if (s == null) s = "";
            s = processValue(s);
            Key k = new Key();
            k.shift = shift;
            k.alt = false;
            k.ch = s;
            return k;
        }

        private static IniStructure inis;
        
        public static string ValidateLayout(string path)
        {
            IniStructure ini = IniStructure.ReadIni(path);
            string s = ini.GetValue("MyInputKeyboardFile", "name");
            ini = null;
            return s;
        }

        public string[] getScripts()
        {
            string s = inis.GetValue("Scripts", "names");
            return s.Split(";".ToCharArray(),StringSplitOptions.RemoveEmptyEntries);
        }
    }

    public class Key
    {
        public bool shift;
        public bool alt;
        public int vkCode;
        public string ch;
    }
}
