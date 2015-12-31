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

namespace MyInput
{
    public partial class ConfigForm : Form
    {
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
            int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;

        public ConfigForm(Main mf)
        {
            mfm = mf;
            cfg = new Config("MyInput\\");
            InitializeComponent();
        }

        Main mfm;
        Config cfg;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void glassButton40_Click(object sender, EventArgs e)
        {
            Close();
        }

        List<String> sequence = new List<string>();
        List<String> modifiers = new List<string>();
        String getfor = "";
        private void glassButton1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.ShiftKey
        || e.KeyCode == Keys.ControlKey
        || e.KeyCode == Keys.Menu)
            {
                modifiers.Add(e.KeyCode.ToString());
            }
            else
            {
                String res = "";
                foreach(String s in modifiers)
                    res += s + "+";
                res += e.KeyCode.ToString();

                modifiers.Clear();
                if (sequence.Count >= 2) return;
                sequence.Add(res);

                ((Glass.GlassButton)sender).Text = "";
                foreach (String s in sequence)
                {
                    ((Glass.GlassButton)sender).Text += "(" + s + ")" + " ";
                }
                cfg.Write("enable", ((Glass.GlassButton)sender).Text);
            }
        }

        private void glassButton1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey
        || e.KeyCode == Keys.ControlKey
        || e.KeyCode == Keys.Menu)
            {
                if (modifiers.Count > 0)
                {
                    String res = "";
                    foreach (String s in modifiers)
                        res += s + "+";

                    res = res.Remove(res.Length - 1);
                    modifiers.Clear();
                    if (sequence.Count >= 2) return;
                    sequence.Add(res);

                    ((Glass.GlassButton)sender).Text = "";
                    foreach (String s in sequence)
                    {
                        ((Glass.GlassButton)sender).Text += "(" + s + ")" + " ";
                    }
                    cfg.Write("enable", ((Glass.GlassButton)sender).Text);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            cfg.Write("toggleenable", checkBox1.Checked.ToString());
            mfm.toggleenable = checkBox1.Checked;
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = Convert.ToBoolean(cfg.Read("toggleenable", "true"));
            checkBox4.Checked = Convert.ToBoolean(cfg.Read("enableenable", "true"));
            screnable.Checked = Convert.ToBoolean(cfg.Read("screnable", "true"));
            int x = Convert.ToInt32(cfg.Read("toggle","119"));
            glassButton2.Text = ((Keys)x).ToString();
            x = Convert.ToInt32(cfg.Read("enable", "120"));
            glassButton1.Text = ((Keys)x).ToString();
            x = Convert.ToInt32(cfg.Read("scriptshortcut", "122"));
            glassButton3.Text = ((Keys)x).ToString();
            x = Convert.ToInt32(cfg.Read("osk", "121"));
           
           
            checkBox3.Checked = Convert.ToBoolean(cfg.Read("debug", "false"));
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            cfg.Write("debug", checkBox3.Checked.ToString());
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            cfg.Write("enableenable", checkBox1.Checked.ToString());
            mfm.enableenable = checkBox1.Checked;
        }

        private void glassButton2_KeyDown(object sender, KeyEventArgs e)
        {
            glassButton2.Text = e.KeyCode.ToString();
            cfg.Write("toggle", e.KeyValue.ToString());
            mfm.togglekey = e.KeyValue;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            cfg.Write("screnable", screnable.Checked.ToString());
            mfm.screnable = screnable.Checked;
        }

        private void glassButton3_KeyDown(object sender, KeyEventArgs e)
        {
            glassButton3.Text = e.KeyCode.ToString();
            cfg.Write("scriptshortcut", e.KeyValue.ToString());
            mfm.scrkey = e.KeyValue;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            LinearGradientBrush lgb = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), BackColor, ForeColor, LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(lgb, new Rectangle(0, 0, Width, Height));
        }

        private void glassButton44_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void glassButton44_MouseDown(object sender, MouseEventArgs e)
        {
            Close();
        }

        private void glassButton1_Click(object sender, EventArgs e)
        {
            getfor = "enable";
            sequence.Clear();
            modifiers.Clear();
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (getfor == "enable")
            {
                glassButton1.Text = "";
                foreach (String s in sequence)
                {
                    glassButton1.Text += "(" + s + ")" + " ";
                }
                //glassButton1.Text = sequence.t;
                timer1.Enabled = false;
            }
        }


    }
}