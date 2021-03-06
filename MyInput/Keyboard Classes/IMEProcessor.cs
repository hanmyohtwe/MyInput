using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using MyInput.Utilities;
using System.Drawing;
using System.Threading;
using System.Diagnostics;

namespace MyInput.Keyboard_Classes
{
    public class IMEProcessor
    {
        public IMEProcessor(KeyboardIME kime, Main mfm, IOProcessor iop)
        {
            this.mfm = mfm;
            this.kime = kime;
            this.iop = iop;
            imf = new IMEForm();
            tmp = new StringBuilder();
            imf.SetText("Loading...");
            Point p = FindCaret();
            BackupState();
            imf.Opacity = 0;
            imf.Show();
            RestoreState();
            imf.Hide();
            imf.Opacity = 0.85;
        }

        public void Reset(KeyboardIME kime, IOProcessor iop)
        {
            this.kime = kime;
            this.iop = iop;
         //   imf = new IMEForm();
            tmp = new StringBuilder();
        }

        IOProcessor iop;
        Main mfm;
        public void Kill()
        {
            imf.Hide();
            imf.Dispose();
        }
        
        StringBuilder tmp;
        KeyboardIME kime;
        public bool Income(int vkCode)
        {
            try
            {
                Buffer bf = new Buffer();
                string input = ((Keys)vkCode).ToString();
                int num = isNumber(vkCode);
                if (num > 0 && KeyCaches != null && num <= KeyCaches.Count)
                {
                    imf.Hide();
                    KeyCacher kch = (KeyCacher)KeyCaches[num - 1];
                    SendCH.Del(kch.match.Length);
                    KeyCacher sch = CaculatePossibleStaking(bf, kch.value);
                    //List<KeyCacher> suggestions = CaculatePossibleSuggestions(bf, kch.value);
                    if (sch != null)
                    {
                        KeyCaches = new ArrayList();
                        KeyCacher lx = new KeyCacher();
                        lx.num = 1;
                        lx.value = sch.value;
                        lx.match = sch.match;
                        KeyCaches.Add(lx);
                        imf.SetText("1. " + sch.value);
                        FindCaretAndShow();
                        //bf.Append(kch.value);
                        tmp = new StringBuilder();
                        //SendCH.Send(kch.value);
                        foreach (char c in kch.value)
                        {
                            if (c != '\u200b')
                                iop.Income(c.ToString());
                        }
                        return false;
                    }
                    else
                    {
                        //bf.Append(kch.value);
                        tmp = new StringBuilder();
                        string res = Preprocess(kch.value);
                        foreach (char c in res)
                        {
                            if (c != '\u200b')
                            {
                                foreach (string ch in iop.CompatibilityDecompose(c.ToString()))
                                {
                                    iop.Income(ch);
                                }
                            }
                        }
                        //SendCH.Send(kch.value);
                        KeyCaches = null;
                        return false;
                    }
                }
                else if (vkCode == 0x20 || vkCode == 0x0d)
                {
                    if (KeyCaches != null)
                    {
                        imf.Hide();
                        KeyCacher kch = (KeyCacher)KeyCaches[0];
                        SendCH.Del(kch.match.Length);
                        KeyCacher sch = CaculatePossibleStaking(bf, kch.value);
                        if (sch != null)
                        {
                            KeyCaches = new ArrayList();
                            KeyCacher lx = new KeyCacher();
                            lx.num = 1;
                            lx.value = sch.value;
                            lx.match = sch.match;
                            KeyCaches.Add(lx);
                            imf.SetText("1. " + sch.value);
                            //imf.Show();
                            FindCaretAndShow();
                            //bf.Append(kch.value);
                            tmp = new StringBuilder();
                            //SendCH.Send(kch.value);
                            foreach (char c in kch.value)
                            {
                                if (c != '\u200b')
                                    iop.Income(c.ToString());
                            }
                            return false;
                        }
                        else
                        {
                            //bf.Append(kch.value);
                            tmp = new StringBuilder();
                            //SendCH.Send(kch.value);
                            string res = Preprocess(kch.value);
                            foreach (char c in res)
                            {
                                if (c != '\u200b')
                                {
                                    foreach (string ch in iop.CompatibilityDecompose(c.ToString()))
                                    {
                                        iop.Income(ch);
                                    }
                                }
                            }
                            KeyCaches = null;
                            return false;
                        }
                    }
                    if (vkCode == 0x0d)
                        mfm.SystemKeyEvent();
                    tmp = new StringBuilder();
                    return true;
                }
                else if (vkCode == 0x1b)
                {
                    KeyCaches = null;
                    tmp = new StringBuilder();
                    imf.Hide();
                    return false;
                }
                else
                {
                    if (vkCode == 0x08)
                    {


                        if (tmp.Length > 0)
                            tmp.Remove(tmp.Length - 1, 1);
                        else if (tmp.Length == 0)
                        {
                            imf.Hide();
                            string buffer = bf.getBuffer();
                            if (buffer.Length > 0)
                            {
                                int x = 0;
                                for (int i = buffer.Length - 2; i >= 0; i--)
                                {
                                    x++;
                                    if (buffer[i] == 0x200b)
                                    {
                                        SendCH.Del(x);
                                        bf.PopChars(x);
                                        return false;
                                    }
                                }
                                if (buffer[buffer.Length - 1] == '\u200b')
                                {
                                    bf.PopChars(buffer.Length);
                                    SendCH.Del(buffer.Length);
                                }
                                else
                                {
                                    bf.PopChars(1);
                                    SendCH.Del(1);
                                }

                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (input.Length > 1)
                        {
                            if (input == "Oemtilde")
                                tmp.Append("`");
                            else if (input.StartsWith("D") && input.Length == 2)
                            {
                                tmp.Append(input[1]);
                            }
                            else if (input == "Oem1")
                            {
                                tmp.Append(";");
                            }
                            else if (input == "Oemcomma")
                            {
                                tmp.Append(",");
                            }
                            else if (input == "OemPeriod")
                            {
                                tmp.Append(".");
                            }
                            else if (input == "OemQuestion")
                            {
                                tmp.Append("/");
                            }
                            else
                            {
                                tmp.Append('-');
                            }
                        }
                        else
                        {
                            tmp.Append(input);
                        }
                    }
                    ArrayList al = new ArrayList();
                    IMEData s = kime.getData(tmp.ToString());
                    if (s != null)
                    {
                        KeyCaches = new ArrayList();
                        string str = "";
                        for (int i = 0; i < s.Values.Count; i++)
                        {
                            KeyCacher kch = new KeyCacher();
                            kch.num = i + 1;
                            kch.value = s.Values[i].ToString();
                            kch.match = s.key;
                            KeyCaches.Add(kch);
                            str += (i + 1).ToString() + ". " + s.Values[i].ToString() + "  ";
                        }
                        imf.SetText(str);
                        FindCaretAndShow();
                        /*Point p = FindCaret();
                        BackupState();
                            imf.ShowFormAt(p.X, p.Y - imf.Height);
                        RestoreState();
                         //*/
                    }
                    else
                    {
                        imf.Hide();
                        KeyCaches = null;
                    }
                    return true;
                }
            }
            catch { return true; }
        }

        private List<KeyCacher> CaculatePossibleSuggestions(Buffer bf, string p)
        {
            return null;
        }

        private string Preprocess(string p)
        {
            p = p.Replace("ိႀ", "ႀိ");
            p = p.Replace("ဳံ", "ံဳ");
            if (iop.Script == "XPartial")
                return p;
            if (iop.Script == "MM Unicode")
            {
                p = p.Replace("ၗ", "\u103a");
                p = p.Replace("ဥ်", "ဉ်");
                if (p.IndexOf('\u1039') > 0)
                {
                    if (p.IndexOf('\u103a') > 0)
                    {
                        p = p.Replace("\u1004\u103a\u1039", "ၤ");
                    }
                    else
                    {
                        int k = p.IndexOf('\u1039');
                        string byees = "ကခဂဃစဆဇဈဋဌဍဏတထဒဓနပဖဗဘမ";
                        string stacked = "ၠၡၢၣၨၩၪၫၮၰၲၴၵၷၸၹၺၻၼၽၾၿ";
                        int a = stacked.Length;
                        for (int i = 0; i < byees.Length; i++)
                        {
                            p = p.Replace("" + p[k - 1] + p[k] + byees[i], "" + p[k - 1] + stacked[i]);
                        }
                    }
                }
            }
            if (iop.Script == "ZawGyi")
            {
                p = p.Replace("ါၗ", "ၗ");
            }
            if(p.Length > 1)
                if (p[0] == 'ႅ' || p[0] == 'ႄ')
                {
                    for (char c = 'က'; c <= 'အ'; c++)
                    {
                        if (p[0] == 'ႅ' && p[1] == c)
                        {
                            string s = p.Replace("ႅ" + c, c + "ႅ");
                            return s;
                        }
                        else if (p[0] == 'ႄ' && p[1] == c)
                        {
                            return p.Replace("ႄ" + c, c + "ႅ");
                        }
                    }
                }
            if (p.Length > 2)
            {
                if (p[0] == 'ေ' && (p[1] == 'ႅ' || p[1] == 'ႄ'))
                {
                    p = p.Replace("" + p[0] + p[1] + p[2], "" + p[0] + p[2] + 'ႅ');
                }
            }
            return p;
        }

        private KeyCacher CaculatePossibleStaking(Buffer bf, string p)
        {
            KeyCacher kch = new KeyCacher();
            string b = bf.getBuffer();
            if (b.Length > 0 && b[b.Length - 1]==(char)0x200b)
            {
                b = b.Substring(0, b.Length - 1);
                int ee = b.LastIndexOf((char)0x200b);
                if (ee > 0 && ee != b.Length - 1)
                    b = b.Substring(ee + 1);
                b += (char)0x200b;
            }
            if (b.Contains("\u1039"))
            {
            }else if (b.Length > 1)
            {
                if (b[b.Length - 1] == 0x200b)
                {
                    if (isConsonant(b[b.Length - 2]))
                    {
                        if (isConsonant(p[0]))
                        {
                            if (doesStack(b[b.Length - 2], p[0]))
                            {
                                string left = b;
                                left = left.Substring(0, left.Length - 1);
                                int i = left.LastIndexOf((char)0x200b);
                                if(i>0)
                                    left = left.Substring(i+1);
                                kch.value = left + (char)0x1039 +  p;
                                kch.match = b + p;
                                return kch;
                            }
                        }
                    }
                    else if (b.Length > 2)
                    {
                        if (isConsonant(b[b.Length - 3]))
                        {
                            if (b[b.Length - 3] == 'င')
                            {
                                if (isConsonant(p[0]))
                                {
                                    if (doesStack(b[b.Length - 3], p[0]))
                                    {
                                        string left = b.ToString();
                                        left = left.Substring(0, left.Length - 1);
                                        int i = left.LastIndexOf((char)0x200b);
                                        if (i > 0)
                                            left = left.Substring(i + 1);
                                        kch.value = left + (char)0x1039 + p;
                                        kch.match = b + p;
                                        return kch;
                                    }
                                }
                            }
                            else
                            {
                                if (b[b.Length - 2] == (char)0x103a)
                                {
                                    if (isConsonant(p[0]))
                                    {
                                        if (doesStack(b[b.Length - 3], p[0]))
                                        {
                                            string left = b.ToString();
                                            left = left.Substring(0, left.Length - 2);
                                            int i = left.LastIndexOf((char)0x200b);
                                            if (i > 0)
                                                left = left.Substring(i + 1);
                                            kch.value = left + (char)0x1039 + p;
                                            kch.match = b + p;
                                            return kch;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        private bool isConsonant(char ch)
        {
            int i = (int)ch;
            if (i >= (int)'က' && i <= 'အ')
                return true;
            return false;
        }

        private bool doesStack(char up, char down)
        {
            switch (up)
            {
                case 'က':
                    if (down == 'က' || down == 'ခ')
                        return true;
                    break;
                case 'ဂ':
                    if (down == 'ဂ' || down == 'ဃ')
                        return true;
                    break;
                case 'င':
                    if (down == 'က' || down == 'ခ' || down == 'ဂ' || down == 'ဃ')
                        return true;
                    break;
                case 'စ':
                    if (down == 'စ' || down == 'ဆ')
                        return true;
                    break;
                case 'ဇ':
                    if (down == 'ဇ' || down == 'ဈ')
                        return true;
                    break;
                case 'ဉ':
                    if (down == 'စ' || down == 'ဆ' || down == 'ဇ' || down == 'ဈ')
                        return true;
                    break;
                case 'ဏ':
                    if (down == 'ဌ' || down == 'ဍ' || down == 'ဎ' || down == 'ဏ')
                        return true;
                    break;
                case 'တ':
                    if (down == 'တ' || down == 'ထ')
                        return true;
                    break;
                case 'ဒ':
                    if (down == 'ဒ' || down == 'ဓ')
                        return true;
                    break;
                case 'န':
                    if (down == 'တ' || down == 'ထ' || down == 'ဒ' || down == 'ဓ' || down == 'န')
                        return true;
                    break;
                case 'ပ':
                    if (down == 'ပ' || down == 'ဖ')
                        return true;
                    break;
                case 'ဗ':
                    if (down == 'ဗ' || down == 'ဘ')
                        return true;
                    break;
                case 'မ':
                    if (down == 'ပ' || down == 'ဖ' || down == 'ဗ' || down == 'ဘ' || down =='မ')
                        return true;
                    break;
                case 'လ':
                    if (down == 'လ')
                        return true;
                    break;
                default:
                    return false;
            }
            return false;
        }
        private IntPtr ForeGroundWindow;
        private IntPtr ActiveWindow;
        private IntPtr Focus;
        private void BackupState()
        {
            //ForeGroundWindow = Native.GetForegroundWindow();
            ActiveWindow = Native.GetActiveWindow();
            //Focus = Native.GetFocus();
        }

        private void RestoreState()
        {
            //Native.SetActiveWindow(ForeGroundWindow);
            Native.SetForegroundWindow(ActiveWindow);
            //Native.SetFocus(Focus);
        }


        private void FindCaretAndShow()
        {
            MethodInvoker mk = delegate
            {
                Point p = FindCaret();
                BackupState();
                MethodInvoker mx = delegate
                {
                    imf.ShowFormAt(p.X, p.Y - imf.Height);
                };
                try
                {
                    imf.Invoke(mx);
                }
                catch
                {
                }
                RestoreState();
            };
            mk.BeginInvoke(null, null);
        }

        private void FCSCB(IAsyncResult ar)
        {
        }

        private Point FindCaret()
        {
            Point p = new Point();
            uint id;
            IntPtr hFocus = Native.GetFocus();
            if (hFocus != IntPtr.Zero)
            {
                Native.GetCaretPos(ref p);
                Native.ClientToScreen(hFocus, ref p);
            }
            else
            {
                
                Native.AttachThreadInput(Native.GetWindowThreadProcessId(Native.GetForegroundWindow(), out id),
                Native.GetCurrentThreadId(), true);
                Native.GetCaretPos(ref p);
                Native.ClientToScreen(hFocus, ref p);

            }
            return p;
            //return new Point(100,100);
        }

        private static IMEForm imf;
        private int isNumber(int vk)
        {
            if (vk >= 0x30 && vk <= 0x39)
            {
                return vk - 0x30;
            }
            else
            {
                return -1;
            }
        }

        ArrayList KeyCaches;
    }

    class KeyCacher
    {
        public int num;
        public string value;
        public string match;
    }
}
