using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace MyInput
{
    public partial class Warning : Form
    {
        public Warning()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Close();
        }

        internal void SetText(string p)
        {
            label1.Text = label1.Text.Replace("%FF", p);
        }

        private void glassButton1_Click(object sender, EventArgs e)
        {
            Process.Start(@"\\Docs\\Myanmar Language Systems.pdf");
        }

        private void glassButton1_MouseDown(object sender, MouseEventArgs e)
        {
            timer1.Enabled = false;
            WarningText wt = new WarningText();
            wt.ShowDialog();
            Close();
        }
    }
}
