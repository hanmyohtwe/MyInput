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
    public partial class WarningText : Form
    {
        public WarningText()
        {
            InitializeComponent();
        }

        private void glassButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void glassButton2_Click(object sender, EventArgs e)
        {
            
        }

        private void glassButton1_MouseDown(object sender, MouseEventArgs e)
        {
            Close();
        }

        private void glassButton2_MouseDown(object sender, MouseEventArgs e)
        {
            Process.Start(@"Docs\\Myanmar Language Systems.pdf");
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
