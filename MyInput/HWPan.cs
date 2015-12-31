using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MyInput.Utilities;
using System.Drawing.Drawing2D;
using CoreInk;
using MyInput.Keyboard_Classes;

using System.Threading;
using System.Collections;

namespace MyInput
{
    public partial class HWPan : Form
    {
        public HWPan()
        {
            InitializeComponent();
        }


        int offx = 0;
        public void NumberToggle()
        {
            if (language == 0)
            {
                writingPad1.SetLanguage(3);
                language = 3;
                offx = 300;
            }
            else
            {
                writingPad1.SetLanguage(0);
                language = 0;
                offx = 0;
            }
        }

        public void LanguageToggle()
        {

        }

        public void USBEvent(byte[] bytes)
        {
           int x = Convert.ToInt32(bytes[2]);
            int y = Convert.ToInt32(bytes[4]);
            x -= 120;
            y -= 130;

            int h = 886;
            int w = 937;
            x = x + (255 * bytes[3]);
            y = y + (255 * bytes[5]);
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            //USBHIDDRIVER.USBInterface.usbBuffer.Clear();

            
            int factorh = writingPad1.Height + (writingPad1.Height / 2);

            int neww = (factorh * 816) / 763;
            int xnew =  (neww * x) / 816;
            xnew += offx;

            int ynew = (factorh * y) / 763;
            ynew -= (writingPad1.Height / 4);


            bool press = bytes[1] == 128;

            MethodInvoker mkx = delegate
            {
                label2.Text = xnew.ToString();
                label3.Text = ynew.ToString();

            };

            this.Invoke(mkx);

            if (press)
            {
                MethodInvoker mk = delegate
                {
                    writingPad1.MoveDrawing(xnew + 30, ynew);
                };
                this.Invoke(mk);
            }
            else
            {
                MethodInvoker mk = delegate
                {
                    writingPad1.StopDrawing(xnew + 30, ynew);
                };
                this.Invoke(mk);
            }
        }

     //   USBHIDDRIVER.USBInterface usb = new USBInterface("vid_1267", "pid_0701");

        private void HWPan_MouseDown(object sender, MouseEventArgs e)
        {
            Activate();
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == WM_MOUSEACTIVATE)
            {
                m.Result = (IntPtr)MA_NOACTIVATE;
            }
            base.WndProc(ref m);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            LinearGradientBrush lgb = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.Black, Color.FromArgb(45, 45, 45), LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(lgb, new Rectangle(0, 0, Width, Height));
        }

        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
            int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;

        private const int WS_EX_TOOLWINDOW = 0x00000080;
        private const int WS_EX_NOACTIVATE = 0x08000000;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= (WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW);
                return cp;
            }
        }

        private Config cfg;
        internal const uint WM_MOUSEACTIVATE = 0x21;
        internal const uint MA_ACTIVATE = 1;
        internal const uint MA_ACTIVATEANDEAT = 2;
        internal const uint MA_NOACTIVATE = 3;
        internal const uint MA_NOACTIVATEANDEAT = 4;

        private string ScriptName;

        private void HWPan_Load(object sender, EventArgs e)
        {
            this.Manager = new CoreInk.Manager();
            writingPad1.OnWrittenEvent += new eLibrary.WritingPad.OnWritten(OnWritten);
            cfg = new Config("MyInput\\");
            int x = (Screen.GetWorkingArea(this).Width / 2) - (this.Width / 2);
            int y = (Screen.GetWorkingArea(this).Height - this.Height);
            string left = cfg.Read("hwkx", x.ToString());
            string top = cfg.Read("hwky", y.ToString());
            this.Top = Convert.ToInt32(top);
            this.Left = Convert.ToInt32(left);
        }

        private void glassButton44_Click(object sender, EventArgs e)
        {
            Close();
        }

        private int language = 0;
        private string laststrokeresult = "";
        List<Stroke> temp = new List<Stroke>();
        public CoreInk.Manager Manager;
        public bool OnWritten(List<Point> points, List<int> dirs, AI.Quadrant q, int AttachMode)
        {
            eventInitiated = true;
            string strstk = "";
            bool matched = false;
            foreach (int d in dirs)
                strstk += d.ToString();
            if (dirs.Count == 1 && dirs[0] == AI.Direction.W)
            {
                //textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
                pop(1);
                temp = new List<Stroke>();
                return true;
            }
            else if (dirs.Count == 1 && dirs[0] == AI.Direction.E && q.first == false)
            {
                Push(" ");
                temp = new List<Stroke>();
                return true;
            }
            List<Stroke> matching_strokes = Manager.MatchStroke(strstk, q, language);
            List<Stroke> attached_matches = StrokesAttached(temp, matching_strokes);
            if (attached_matches.Count == 0)
            {
                Stroke s = GetBestStroke(matching_strokes);
                if (s != null)
                {
                    String strokeresult = Manager.MatchCharacter(s.cid).charx;
                    if (q.second == true && q.third == false)
                    {
                        if (s.quadrant == "C")
                        {
                            strokeresult = getStacked(strokeresult);
                        }
                    }
                    if (strokeresult == "\u1001")
                    {
                        int a = Math.Abs(points[0].Y - writingPad1.first);
                        int b = Math.Abs(points[0].Y - writingPad1.second);
                        if (b < a)
                        {
                            strokeresult = "ေ";
                        }
                    }
                    Push(strokeresult);
                    laststrokeresult = strokeresult;
                }
                else
                {
                    laststrokeresult = "";
                }
                temp = StrokesNeedAttach(matching_strokes);
            }
            else
            {
                Stroke s = GetEndStroke(attached_matches);
                if (s != null)
                {
                    String strokeresult = Manager.MatchCharacter(s.cid).charx;
                    
                    pop(laststrokeresult.Length);
                    Push(strokeresult);
                    laststrokeresult = strokeresult;
                }
                else
                {
                    laststrokeresult = "";
                }
                temp = GetEndStrokeNeedsAttach(matching_strokes, attached_matches);
            }
            return matched;
        }

        public string getStacked(string con)
        {
            string consonants = "ကခဂဃစဆဇဈဍဌဋဎဏတထဒဓနပဖဗဘမလ";
            string stacks = "ၠၡၢၣၨၩၪၫၲၰၮၳၴၵၷၸၹၺၻၼၽၾၿႌ";
            int i = consonants.IndexOf(con);
            if (i != -1)
            {
                return stacks[i].ToString();
            }
            return con;
        }

        public void Push(string s)
        {
            foreach (char c in s)
                Push(c);
            //Redraw();
        }

        public void Push(char c)
        {
            iop.Income(c.ToString());
            /*
            intervalue = new StringBuilder(CoreInk.AI.Rule(intervalue.ToString().ToCharArray(), c));
            handle.inputting = intervalue.ToString();
             */
            //textBoxIP1.AddMMChar(c);
        }

        public void pop(int count)
        {
            for (int i = 0; i < count; i++)
            { iop.Income("delete"); }//textBoxIP1.Pop();
        }

        public Stroke GetEndStroke(List<Stroke> sts)
        {
            foreach (Stroke st in sts)
            {
                if (st.nextstroke == 0)
                    return st;
            }
            return null;
        }

        public List<Stroke> GetEndStrokeNeedsAttach(List<Stroke> match, List<Stroke> last)
        {
            List<Stroke> l = StrokesNeedAttach(match);
            foreach (Stroke st in last)
            {
                if (st.nextstroke != 0)
                    l.Add(st);
            }
            return l;
        }

        public Stroke GetBestStroke(List<Stroke> sts)
        {
            foreach (Stroke st in sts)
            {
                if (st.nextstroke == 0 && st.thisstroke == 0)
                    return st;
            }
            return null;
        }

        public List<Stroke> StrokesNeedAttach(List<Stroke> sts)
        {
            List<Stroke> l = new List<Stroke>();
            foreach (Stroke st in sts)
            {
                if (st.nextstroke != 0 && st.thisstroke == 0)
                    l.Add(st);
            }
            return l;
        }

        public List<Stroke> StrokesAttached(List<Stroke> _base, List<Stroke> match)
        {
            List<Stroke> l = new List<Stroke>();
            foreach (Stroke m in match)
            {
                foreach (Stroke b in _base)
                {
                    if (b.nextstroke == m.thisstroke && b.vid == m.vid)
                        l.Add(m);
                }
            }
            return l;
        }

        internal void SetActiveLayout(MyInput.Keyboard_Classes.KeyboardLayout kl)
        {
            
        }

        [DllImport("user32.dll")]

        // GetCursorPos() makes everything possible
        static extern bool GetCursorPos(ref Point lpPoint);
        public bool eventInitiated = false;
        IOProcessor iop;
        internal void SetActiveScript(MyInput.Keyboard_Classes.KeyProcessor kp)
        {
            ScriptName = kp.getscript();
            this.iop = mfm.iop;
        }

        private Main mfm;
        internal void setHandle(Main main)
        {
            mfm = main;
            iop = mfm.iop;
            iop.SetMainHandle(mfm);
        }

        private void writingPad1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X > 370)
            {
                writingPad1.SetLanguage(3);
                language = 3;
            }
            else
            {
                writingPad1.SetLanguage(0);
                language = 0;
            }
        }

        private void glassButton2_Click(object sender, EventArgs e)
        {
           
        }

        private void glassButton5_Click(object sender, EventArgs e)
        {
            
        }

        private void glassButton3_Click(object sender, EventArgs e)
        {
            
        }

        private void glassButton4_Click(object sender, EventArgs e)
        {
            
        }

        private void glassButton2_Click(object sender, MouseEventArgs e)
        {
            
        }

        private void glassButton2_MouseUp(object sender, MouseEventArgs e)
        {
            iop.Income("delete");
        }

        private void glassButton5_MouseUp(object sender, MouseEventArgs e)
        {
            iop.Income("\t");
        }

        private void glassButton3_MouseUp(object sender, MouseEventArgs e)
        {
            iop.Income(" ");
        }

        private void glassButton4_MouseUp(object sender, MouseEventArgs e)
        {
            iop.Income("\r");
        }

        private void glassButton44_MouseUp(object sender, MouseEventArgs e)
        {
            Close();
        }
    }
}
