using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;
using MyInput.Keyboard_Classes;
using System.IO;
using MyInput.Utilities;
using System.Threading;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Xml;
using System.Drawing.Drawing2D;


namespace MyInput
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            LinearGradientBrush lgb = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), BackColor, ForeColor, LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(lgb, new Rectangle(0, 0, Width, Height));
        }

        //DictionaryProvider dp = new DictionaryProvider();
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
            int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int WM_NCLBUTTONUP = 0xa2;
        private void Form1_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("This version of MyInput Beta is for use by \r\nMyanmar Posts and Telecommunications\r\nfor demonstration purpose at ICT Week.\r\nAll other rights reserved.\r\nCopyright 2000-2010\r\nTechnomation Studios.");
            //UserInterface.Glass.ExtendGlassIntoClientArea(this, 0, 0, Width, Height);
            kp = new KeyProcessor("NULL");
            iop = new IOProcessor(kp);
            iop.SetMainHandle(this);
            imp = new IMEProcessor(kime, this, iop);
            //kl = new KeyboardLayout("NULL");
            dkstate = "none";
            cfg = new Config("MyInput\\");
            //cfg.Write("embed-crypt", EmbeddingControl.createPublicKey("589114946"));
            int xxx = Screen.GetWorkingArea(this).Width - this.Width + 1;
            int yyy = Screen.GetWorkingArea(this).Height - this.Height;
            string left = cfg.Read("mfx", xxx.ToString());
            string percent = cfg.Read("ypercent", Pixel2Percent(yyy, Screen.GetWorkingArea(this).Height).ToString());
            this.Top = Percent2Pixel(Convert.ToDouble(percent), Screen.GetWorkingArea(this).Height);
            this.Left = Convert.ToInt32(left);
            togglekey = Convert.ToInt32(cfg.Read("toggle", "119"));
            scrkey = Convert.ToInt32(cfg.Read("scriptshortcut", "122"));
            osk = Convert.ToInt32(cfg.Read("osk", "121"));
            enableenable = Convert.ToBoolean(cfg.Read("enableenable", "true"));
            toggleenable = Convert.ToBoolean(cfg.Read("toggleenable", "true"));
            screnable = Convert.ToBoolean(cfg.Read("screnable", "true"));
            autohide = Convert.ToBoolean(cfg.Read("autoshow", "true"));
            oskenable = Convert.ToBoolean(cfg.Read("oskenable", "true"));
            virtualize = Convert.ToBoolean(cfg.Read("virtualize", "true"));
            Layouts = new ArrayList();
            Scripts = new ArrayList();
            string[] s = System.IO.Directory.GetFiles("Layouts\\");
            foreach (string x in s)
            {
                if (x.EndsWith(".keylayout"))
                {
                    string ss = Directory.GetCurrentDirectory() + "\\" + x;
                    KeyboardLayout kl = new KeyboardLayout(ss);
                    if (kl.getname() != null)
                    {
                        Layout l = new Layout();
                        l.name = kl.getname();
                        l.isIME = false;
                        l.scripts = kl.getScripts();
                        Layouts.Add(l);
                    }
                    kl = null;
                }
                else if (x.EndsWith(".dll"))
                {
                    Layout l = new Layout();
                    l.name = x.Substring(x.LastIndexOf("\\") + 1);
                    l.name = l.name.Substring(0, l.name.IndexOf("."));
                    l.isIME = true;
                    l.scripts = null;
                    Layouts.Add(l);
                }
            }
            s = System.IO.Directory.GetFiles("Scripts\\");
            foreach (string x in s)
            {
                string ss = Directory.GetCurrentDirectory() + "\\" + x;
                if (ss.EndsWith(".ikl"))
                {
                    ss = ss.Substring(ss.LastIndexOf("\\") + 1);
                    ss = ss.Substring(0, ss.Length - 4);
                    if (!ScriptExists(ss))
                    {
                        Scripts.Add(ss);
                    }
                }
                else if (ss.EndsWith(".ikb"))
                {
                    ss = ss.Substring(ss.LastIndexOf("\\") + 1);
                    ss = ss.Substring(0, ss.Length - 4);
                    if (!ScriptExists(ss))
                    {
                        Scripts.Add(ss);
                    }
                }
            }

            foreach (string sx in Scripts)
            {
                ToolStripMenuItem i = new ToolStripMenuItem(sx);
                i.Click += ScriptClick;
                scripts_menu.Items.Add(i);
            }

            foreach (Layout l in Layouts)
            {
                ToolStripMenuItem i = new ToolStripMenuItem(l.name);
                i.Click += LayoutClick;
                layouts.Items.Add(i);
            }


            string lay = cfg.Read("layout", "Type Writer");
            string scr = cfg.Read("script", "MM Unicode");
            if (LayoutExists(lay))
            {
                laybtn.Text = lay;
            }
            else
            {
                laybtn.Text = "Type Writer";
            }
            if (ScriptExists(scr))
            {
                UpdateScript(scr);
            }
            else
            {
                UpdateScript("MM Unicode");
            }
            System.GC.Collect();
            if (!true)
            {
                try
                {
                    string sss = cfg.Read("embed-crypt");
                    if (EmbeddingControl.isValid(sss))
                    {
                        embedcrypt.Enabled = true;
                        actv.Enabled = false;
                        scrbtn.Enabled = false;
                        laybtn.Enabled = false;
                        //glassButton4.Enabled = false;
                    }
                    else
                    {
                        //internet();
                    }
                }
                catch (Exception ex)
                {
                    //internet();
                }
            }
            else
            {
                enablekey = Convert.ToInt32(cfg.Read("enable", "120"));
            }
            this.Refresh();
            Program.kh.SetHandle(this);
            Program.kh.Initiate();
        }
        bool internetactive = false;

        private bool ScriptExists(string s)
        {
            foreach (string x in Scripts)
            {
                if (s == x)
                    return true;
            }
            return false;
        }

        private bool LayoutExists(string s)
        {
            foreach (Layout l in Layouts)
            {
                if (s == l.name)
                    return true;
            }
            return false;
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

        internal const uint WM_MOUSEACTIVATE = 0x21;
        internal const uint MA_ACTIVATE = 1;
        internal const uint MA_ACTIVATEANDEAT = 2;
        internal const uint MA_NOACTIVATE = 3;
        internal const uint MA_NOACTIVATEANDEAT = 4;


        private Config cfg;
        private ArrayList Scripts;
        private ArrayList Layouts;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Activate();
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        public void closeVK()
        {
            toolStripMenuItem2_Click(this, new EventArgs());
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (laybtn.Text != "MyRoman")
            {
                if (vk == null)
                {
                    vk = new VKeyboard();
                    vk.setHandle(this);
                    vk.SetActiveLayout(kl);
                    vk.SetActiveScript(kp);
                    vk.Show();
                }
                else
                {
                    if (vk.Visible)
                    {
                        vk.Close();
                        vk.Dispose();
                        vk = null;
                    }
                    else
                    {
                        vk.SetActiveLayout(kl);
                        vk.SetActiveScript(kp);
                        vk.Show();
                    }
                }
            }
            else
            {
                WinExec("MyRoman Browser.exe", 4);
            }
        }

        [DllImport("kernel32.dll")]
        static extern uint WinExec(string lpCmdLine, uint uCmdShow);

        VKeyboard vk;
        HWPan hw;
        private KeyProcessor kp;
        private KeyboardLayout kl;
        internal KeyboardIME kime;
        public IOProcessor iop;
        public IOProcessor NULLiop;
        public KeyboardLayout NULLkl;
        public KeyProcessor NULLkp;
        private IMEProcessor imp;
        private void ScriptClick(object sender, EventArgs e)
        {
            ToolStripMenuItem g = (ToolStripMenuItem)sender;
            UpdateScript(g.Text);
        }

        private void UpdateScriptCB(IAsyncResult ar)
        {
            MethodInvoker mx = delegate
            {
                if (!embedcrypt.Enabled)
                    scrbtn.Enabled = true;
                UpdateLayout(laybtn.Text);
            };
            scrbtn.BeginInvoke(mx, null);
        }

        private void UpdateScript(string p)
        {
            MethodInvoker mk = delegate
            {
                kp = new KeyProcessor(p);
                iop.SetKPR(kp);
                if (kl != null)
                {
                    if (hw != null)
                        hw.SetActiveScript(kp);
                }
                else if (kime != null)
                {
                    imp.Reset(kime, iop);
                }
                cfg.Write("script", p);
            };
            scrbtn.Text = p;
            scrbtn.Enabled = false;
            mk.BeginInvoke(UpdateScriptCB, null);
            if (!donwarn)
            {
                if (p == "ZawGyi" || p == "XPartial")
                {

                    Warning w = new Warning();
                    w.SetText(p);
                    w.Show();
                    w.Top = this.Top - 28;
                    w.Left = this.Left;
                    w.Width = this.Width;
                }
            }
            else
            {
                donwarn = false;
            }
        }

        public void MouseMoved(object sender, MouseEventArgs e)
        {
            if (e.Clicks > 0)
            {
                MyInput.Keyboard_Classes.Buffer bf = new MyInput.Keyboard_Classes.Buffer();
                bf.Flush();
            }
        }

        private void LayoutClick(object sender, EventArgs e)
        {
            ToolStripMenuItem g = (ToolStripMenuItem)sender;
            UpdateLayout(g.Text);
        }

        private void UpdateLayout(string s)
        {
            foreach (Layout l in Layouts)
            {
                if (s == l.name)
                {
                    if (!l.isIME)
                    {
                        scrbtn.Enabled = true;
                        kl = new KeyboardLayout(s);
                        kime = null;
                        /*if (imp != null)
                        {
                            imp.Kill();
                            imp = null;
                        }*/
                        laybtn.Text = kl.getname();
                        cfg.Write("layout", s);
                        if (vk != null)
                            vk.SetActiveLayout(kl);
                        if (hw != null)
                            hw.SetActiveLayout(kl);
                        if (active)
                        {
                            /*
                            if (imp != null)
                            {
                                imp.Kill();
                                imp = null;
                            }*/
                        }
                    }
                    else
                    {
                        kl = null;
                        kime = new KeyboardIME(l.name);
                        cfg.Write("layout", s);
                        laybtn.Text = kime.getname();
                        if (l.name == "MyRoman")
                        {
                            // toolStripMenuItem2.Text = "MyRoman Browser";
                        }
                        else
                        {
                            //toolStripMenuItem2.Enabled = false;
                        }
                        if (vk != null)
                        {
                            vk.Hide();
                            vk.Dispose();
                            vk = null;
                        }
                        if (active)
                        {
                            //if (imp != null)
                            //    imp.Kill();
                            imp.Reset(kime, iop);// = new IMEProcessor(kime,this, iop);
                        }
                    }
                    System.GC.Collect();
                    return;
                }
            }
        }

        private void laybtn_Click(object sender, EventArgs e)
        {
            string name = ((Glass.GlassButton)sender).Text;
            for (int i = 0; i < Layouts.Count; i++)
            {
                if (((Layout)Layouts[i]).name == name)
                {
                    i++;
                    if (i == Layouts.Count)
                        i = 0;
                    UpdateLayout(((Layout)Layouts[i]).name);
                    return;
                }
            }
        }

        bool donwarn = true;
        private void scrbtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Scripts.Count; i++)
            {
                if (((string)Scripts[i]) == kp.getscript())
                {
                    i++;
                    if (i == Scripts.Count)
                        i = 0;
                    UpdateScript((string)Scripts[i]);
                    return;
                }
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //cfg.Write("mfx", this.Left.ToString());
            //cfg.Write("mfy", this.Top.ToString());

            //cfg.Write("screenheight", Screen.PrimaryScreen.WorkingArea.Height.ToString());
            cfg.Write("ypercent", Pixel2Percent(this.Top, Screen.GetWorkingArea(this).Height).ToString());
        }


        private double Pixel2Percent(int pixel, int size)
        {
            return ((double)pixel * 100) / size;
        }

        private int Percent2Pixel(double percent, int size)
        {
            return (int)((percent * size) / 100);
        }

        private void Main_Move(object sender, EventArgs e)
        {
            // need improvements
            //this.Left = Screen.GetWorkingArea(this).Width - this.Width + 1;
        }

        internal bool active;
        internal bool popenter;
        private void servicetimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!autohide)
                {
                    servicetimer.Enabled = false;
                    return;
                }
                // this.TopLevel = true;
                //this.TopMost = true;
                // this.BringToFront();
                int inv = 10;
                int apos = Screen.GetWorkingArea(this).Width - fwidth + 1;
                if (popenter && !active)
                {
                    apos = Screen.GetWorkingArea(this).Width - 78;
                }
                int napos = Screen.GetWorkingArea(this).Width - actv.Width + 1;
                if (active || popenter)
                {
                    if (this.Left > apos)
                    {
                        this.Left -= inv;
                        this.Width = Screen.GetWorkingArea(this).Width - this.Left + 1;
                        if (this.Left < apos)
                        {
                            this.Left = apos;
                            this.Width = Screen.GetWorkingArea(this).Width - this.Left + 1;
                            servicetimer.Enabled = false;
                        }
                    }
                    else if (this.Left < apos)
                    {
                        this.Left += inv;
                        this.Width = Screen.GetWorkingArea(this).Width - this.Left + 1;
                        if (this.Left > apos)
                        {
                            this.Left = apos;
                            this.Width = Screen.GetWorkingArea(this).Width - this.Left + 1;
                            servicetimer.Enabled = false;
                        }
                    }
                }
                else
                {
                    if (this.Left < napos)
                    {
                        this.Left += inv;
                        this.Width = Screen.GetWorkingArea(this).Width - this.Left;
                        if (this.Left > napos)
                        {
                            this.Left = napos;
                            this.Width = Screen.GetWorkingArea(this).Width - this.Left + 1;
                            servicetimer.Enabled = false;
                        }
                    }
                }
            }
            catch { }
        }

        private void actv_Click(object sender, EventArgs e)
        {
            active = !active;
            if (active)
            {
                //iop = new IOProcessor(kp);
                //iop.SetMainHandle(this);
                if (kl != null)
                {
                    //kime = null;
                    if (vk != null && autohide)
                        vk.Show();
                }
                else if (kime != null)
                {
                    imp.Reset(kime, iop);// = new IMEProcessor(kime,this, iop);
                }
                actv.BackColor = Color.DodgerBlue;
            }
            else
            {
                if (vk != null && autohide)
                    vk.Hide();
                if (hw != null && autohide)
                {
                    hw.Close();
                    hw.Dispose();
                    hw = null;
                }
                //iop = null;
                /*                if (imp != null)
                                {
                                    imp.Kill();
                                    imp = null;
                                }
                */
                actv.BackColor = Color.Black;
                System.GC.Collect();
            }
            cfg.Write("active", active.ToString());
            servicetimer.Enabled = true;
        }

        internal bool KeyEvent(int nCode, IntPtr wParam, GlobalHook.KBDLLHOOKSTRUCT lParam, bool shift, bool gis)
        {
            if (active)
            {
                log.write("IO-IN: " + lParam.vkCode.ToString());
                if (kl != null)
                {
                    if (nCode == 0x0d)
                    {
                        SystemKeyEvent();
                        return false;
                    }
                    return iop.Income(lParam.vkCode, gis, shift, kl);
                }
                else
                {
                    return !imp.Income(lParam.vkCode);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        internal void ChangeState(bool shf, bool alt)
        {
            if (vk != null)
                vk.ChangeState(shf, alt);
        }

        internal bool togglePressed()
        {
            bool x = Convert.ToBoolean(cfg.Read("toggleenable", "true"));
            if (x)
            {
                log.write("TOGGLED");
                laybtn_Click(laybtn, null);
                return true;
            }
            else
            {
                return false;
            }
        }
        internal bool oskPressed()
        {
            if (this.active && this.iop != null && oskenable)
            {
                toolStripMenuItem2_Click(this, null);
                return true;
            }
            return false;
        }

        internal bool scrPressed()
        {
            bool x = Convert.ToBoolean(cfg.Read("screnable", "true"));
            if (x)
            {
                log.write("ENABLED");
                scrbtn_Click(this, null);
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool enablePressed()
        {
            bool x = Convert.ToBoolean(cfg.Read("enableenable", "true"));
            if (x)
            {
                log.write("ENABLED");
                actv_Click(this, null);
                return true;
            }
            else
            {
                return false;
            }
        }
        internal string dkstate;
        internal Log log = new Log();
        internal bool toggleenable;
        internal bool enableenable;
        internal bool screnable;
        internal bool autohide;
        internal int togglekey;
        internal int scrkey;
        internal int enablekey;
        internal bool oskenable;
        internal bool virtualize;
        internal int osk;
        internal void SystemKeyEvent()
        {
            if (imp != null)
            {
                imp.Income(0x1B);
            }
            MyInput.Keyboard_Classes.Buffer bf = new MyInput.Keyboard_Classes.Buffer();
            if (vk != null)
            {
                if (vk.eventInitiated)
                {
                    vk.eventInitiated = false;
                }
                else
                {
                    bf.Flush();
                }
            }
            else if (hw != null)
            {
                if (hw.eventInitiated)
                {
                    hw.eventInitiated = false;
                }
                else
                {
                    bf.Flush();
                }
            }
            else
            {
                bf.Flush();
            }
            log.write("MFM: SYSKEY");
        }

        private void glassButton4_Click(object sender, EventArgs e)
        {
            Activate();
            Glass.GlassButton gl = (Glass.GlassButton)sender;
            mainmenu.Show(gl, new Point(0, 0), ToolStripDropDownDirection.Left);
        }

        private void exitMyInputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        internal void dkChange(string p)
        {
            dkstate = p;
            if (vk != null)
            {
                vk.dkstate = this.dkstate;
                vk.UpdateVisual();
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigForm cfm = new ConfigForm(this);
            cfm.ShowDialog();
        }

        private void aboutMyInputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Credits cr = new Credits();
            cr.ShowDialog();
            cr.Dispose();
        }

        private void Main_Activated(object sender, EventArgs e)
        {

        }

        private void cmdset_Tick(object sender, EventArgs e)
        {
            try
            {
                if (cfg == null)
                    cfg = new Config("MyInput\\");
                string cmd = cfg.Read("command");
                if (cmd == "layout")
                {
                    UpdateLayout(cfg.Read("layout"));
                }
                else if (cmd == "activation")
                {
                    actv_Click(this, null);
                }
                else if (cmd == "script")
                {
                    UpdateScript(cfg.Read("script"));
                }
                else if (cmd == "layout&script")
                {
                    UpdateScript(cfg.Read("script"));
                    UpdateLayout(cfg.Read("layout"));
                }
                else if (cmd == "layout&script&activate")
                {
                    active = false;
                    UpdateScript(cfg.Read("script"));
                    UpdateLayout(cfg.Read("layout"));
                    actv_Click(this, null);
                }
                cfg.Write("command", "");
            }
            catch (Exception ex)
            {
            }
        }

        private void exit_Tick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private int fwidth;
        private void scrbtn_TextChanged(object sender, EventArgs e)
        {
            /*
           Graphics gr = scrbtn.CreateGraphics();
           SizeF sz = gr.MeasureString(scrbtn.Text, scrbtn.Font);
           scrbtn.Width = (int)sz.Width + 15;
           laybtn.Left = scrbtn.Left + scrbtn.Width + 1;
           glassButton4.Left = laybtn.Left + laybtn.Width + 1;
           label1.Left = glassButton4.Left + glassButton4.Width + 1;

            
           // */
            this.Width = label1.Left + label1.Width + 3;
            fwidth = this.Width;
            servicetimer.Enabled = true;
        }

        private void laybtn_TextChanged(object sender, EventArgs e)
        {
            /*
            Graphics gr = scrbtn.CreateGraphics();
            SizeF sz = gr.MeasureString(laybtn.Text, laybtn.Font);
            laybtn.Width = (int)sz.Width + 15;
            glassButton4.Left = laybtn.Left + laybtn.Width + 1;
            label1.Left = glassButton4.Left + glassButton4.Width + 1;
            
            
            // */
            this.Width = label1.Left + label1.Width + 3;
            fwidth = this.Width;
            servicetimer.Enabled = true;
        }

        private void chkreg_Tick(object sender, EventArgs e)
        {
            chkreg.Enabled = false;
        }

        private void embedcrypt_Tick(object sender, EventArgs e)
        {
            string s = cfg.Read("embed-crypt");
            if (!EmbeddingControl.isValid(s))
            {
                Application.Exit();
            }
        }

        private void showHideHandwritingInputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hw == null)
            {
                hw = new HWPan();
                hw.setHandle(this);
                hw.SetActiveLayout(kl);
                hw.SetActiveScript(kp);
                hw.Show();
            }
            else
            {
                hw.Close();
                hw.Dispose();
                hw = null;
            }
        }

        private void mainmenu_Opening(object sender, CancelEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void mainmenu_Paint(object sender, PaintEventArgs e)
        {
            //LinearGradientBrush lgb = new LinearGradientBrush(new Rectangle(0, 0, e.ClipRectangle.Width, e.ClipRectangle.Height), Color.Black, Color.Cyan, LinearGradientMode.Vertical);
            //e.Graphics.FillRectangle(lgb, new Rectangle(0, 0, e.ClipRectangle.Width, e.ClipRectangle.Height));

        }

        private void mainmenu_PaintGrip(object sender, PaintEventArgs e)
        {
        }

        private void aboutMyInputToolStripMenuItem_Paint(object sender, PaintEventArgs e)
        {

            if (((ToolStripMenuItem)sender).Selected)
            {
                ((ToolStripMenuItem)sender).ForeColor = Color.Black;
            }
            else
            {
                ((ToolStripMenuItem)sender).ForeColor = Color.White;
            }
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void Main_MouseEnter(object sender, EventArgs e)
        {

        }

        private void actv_MouseEnter(object sender, EventArgs e)
        {

        }

        private void glassButton2_MouseLeave(object sender, EventArgs e)
        {
            g3e = false;
            CheckandDoHide();
        }

        private void CheckandDoHide()
        {
            Point p = new Point();
            GetCursorPos(ref p);
            p.X -= Left;
            p.Y -= Top;
            if (p.X < 0 || p.Y < 0)
            {
                popenter = false;
                servicetimer.Enabled = true;
            }
        }

        private void CheckandDoShow()
        {
            Point p = new Point();
            GetCursorPos(ref p);
            p.X -= Left;
            p.Y -= Top;
            if (p.X > 0 && p.Y > 0)
            {
                popenter = true;
                servicetimer.Enabled = true;
            }
        }

        [DllImport("user32.dll")]

        // GetCursorPos() makes everything possible

        static extern bool GetCursorPos(ref Point lpPoint);

        private void glassButton2_MouseLeave_1(object sender, EventArgs e)
        {
            g2e = false;
            CheckandDoHide();
        }

        private void glassButton1_MouseLeave(object sender, EventArgs e)
        {
            g1e = false;
            CheckandDoHide();
        }

        private bool g1e = false;
        private bool g2e = false;
        private bool g3e = false;
        private void glassButton1_MouseEnter(object sender, EventArgs e)
        {
            g1e = true;
        }

        private void glassButton2_MouseEnter(object sender, EventArgs e)
        {
            g2e = true;
        }

        private void actv_MouseLeave(object sender, EventArgs e)
        {
            g3e = false;
            CheckandDoHide();
        }

        private void glassButton2_MouseLeave_2(object sender, EventArgs e)
        {
            CheckandDoHide();
        }

        private void popout_Tick(object sender, EventArgs e)
        {
            popout.Enabled = false;
            CheckandDoShow();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"Docs\\HELP.pdf");
        }
    }


    class Layout
    {
        public string name;
        public bool isIME;
        public string[] scripts;
    }
}