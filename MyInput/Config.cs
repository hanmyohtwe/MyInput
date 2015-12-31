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

        private void glassButton1_KeyDown(object sender, KeyEventArgs e)
        {
            glassButton1.Text = e.KeyCode.ToString();
            cfg.Write("enable", e.KeyValue.ToString());
            mfm.enablekey = e.KeyValue;
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
            int x = Convert.ToInt32(cfg.Read("toggle", "119"));
            glassButton2.Text = ((Keys)x).ToString();
            x = Convert.ToInt32(cfg.Read("enable", "120"));
            glassButton1.Text = ((Keys)x).ToString();
            x = Convert.ToInt32(cfg.Read("scriptshortcut", "122"));
            glassButton3.Text = ((Keys)x).ToString();
            x = Convert.ToInt32(cfg.Read("osk", "121"));
            glassButton4.Text = ((Keys)x).ToString();
            checkBox2.Checked = Convert.ToBoolean(cfg.Read("autoshow", "true"));
            checkBox3.Checked = Convert.ToBoolean(cfg.Read("debug", "false"));
            oskenable.Checked = Convert.ToBoolean(cfg.Read("oskenable", "true"));
            checkBox5.Checked = Convert.ToBoolean(cfg.Read("virtualize", "true"));
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            cfg.Write("autoshow", checkBox2.Checked.ToString());
            mfm.autohide = checkBox2.Checked;
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

        private void checkBox5_CheckedChanged_1(object sender, EventArgs e)
        {
            cfg.Write("oskenable", oskenable.Checked.ToString());
            mfm.oskenable = oskenable.Checked;
        }

        private void glassButton4_KeyDown(object sender, KeyEventArgs e)
        {
            glassButton4.Text = e.KeyCode.ToString();
            cfg.Write("osk", e.KeyValue.ToString());
            mfm.osk = e.KeyValue;
        }

        private void checkBox5_CheckedChanged_2(object sender, EventArgs e)
        {
            cfg.Write("virtualize", checkBox5.Checked.ToString());
            mfm.virtualize = checkBox5.Checked;
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
    }
}