namespace MyInput
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.mainmenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.aboutMyInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMyInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scripts_menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.layouts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.servicetimer = new System.Windows.Forms.Timer(this.components);
            this.cmdset = new System.Windows.Forms.Timer(this.components);
            this.exittmr = new System.Windows.Forms.Timer(this.components);
            this.chkreg = new System.Windows.Forms.Timer(this.components);
            this.embedcrypt = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.actv = new Glass.GlassButton();
            this.glassButton1 = new Glass.GlassButton();
            this.glassButton2 = new Glass.GlassButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.popout = new System.Windows.Forms.Timer(this.components);
            this.scrbtn = new Glass.GlassButton();
            this.label1 = new Glass.GlassButton();
            this.laybtn = new Glass.GlassButton();
            this.mainmenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainmenu
            // 
            this.mainmenu.BackColor = System.Drawing.Color.Black;
            this.mainmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutMyInputToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.exitMyInputToolStripMenuItem});
            this.mainmenu.Name = "contextMenuStrip1";
            this.mainmenu.ShowImageMargin = false;
            this.mainmenu.Size = new System.Drawing.Size(92, 92);
            this.mainmenu.Opening += new System.ComponentModel.CancelEventHandler(this.mainmenu_Opening);
            this.mainmenu.PaintGrip += new System.Windows.Forms.PaintEventHandler(this.mainmenu_PaintGrip);
            this.mainmenu.Paint += new System.Windows.Forms.PaintEventHandler(this.mainmenu_Paint);
            // 
            // aboutMyInputToolStripMenuItem
            // 
            this.aboutMyInputToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.aboutMyInputToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.aboutMyInputToolStripMenuItem.Name = "aboutMyInputToolStripMenuItem";
            this.aboutMyInputToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
            this.aboutMyInputToolStripMenuItem.Text = "About";
            this.aboutMyInputToolStripMenuItem.Click += new System.EventHandler(this.aboutMyInputToolStripMenuItem_Click);
            this.aboutMyInputToolStripMenuItem.Paint += new System.Windows.Forms.PaintEventHandler(this.aboutMyInputToolStripMenuItem_Paint);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.settingsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            this.settingsToolStripMenuItem.Paint += new System.Windows.Forms.PaintEventHandler(this.aboutMyInputToolStripMenuItem_Paint);
            // 
            // exitMyInputToolStripMenuItem
            // 
            this.exitMyInputToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.exitMyInputToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.exitMyInputToolStripMenuItem.Name = "exitMyInputToolStripMenuItem";
            this.exitMyInputToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
            this.exitMyInputToolStripMenuItem.Text = "Exit";
            this.exitMyInputToolStripMenuItem.Click += new System.EventHandler(this.exitMyInputToolStripMenuItem_Click);
            this.exitMyInputToolStripMenuItem.Paint += new System.Windows.Forms.PaintEventHandler(this.aboutMyInputToolStripMenuItem_Paint);
            // 
            // scripts_menu
            // 
            this.scripts_menu.Name = "contextMenuStrip1";
            this.scripts_menu.ShowImageMargin = false;
            this.scripts_menu.Size = new System.Drawing.Size(36, 4);
            // 
            // layouts
            // 
            this.layouts.Name = "contextMenuStrip1";
            this.layouts.ShowImageMargin = false;
            this.layouts.Size = new System.Drawing.Size(36, 4);
            // 
            // servicetimer
            // 
            this.servicetimer.Enabled = true;
            this.servicetimer.Interval = 10;
            this.servicetimer.Tick += new System.EventHandler(this.servicetimer_Tick);
            // 
            // cmdset
            // 
            this.cmdset.Enabled = true;
            this.cmdset.Tick += new System.EventHandler(this.cmdset_Tick);
            // 
            // exittmr
            // 
            this.exittmr.Interval = 18000;
            this.exittmr.Tick += new System.EventHandler(this.exit_Tick);
            // 
            // chkreg
            // 
            this.chkreg.Enabled = true;
            this.chkreg.Interval = 3000;
            this.chkreg.Tick += new System.EventHandler(this.chkreg_Tick);
            // 
            // embedcrypt
            // 
            this.embedcrypt.Interval = 1000;
            this.embedcrypt.Tick += new System.EventHandler(this.embedcrypt_Tick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.actv);
            this.panel1.Controls.Add(this.glassButton1);
            this.panel1.Controls.Add(this.glassButton2);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(78, 33);
            this.panel1.TabIndex = 9;
            // 
            // actv
            // 
            this.actv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.actv.ContextMenuStrip = this.mainmenu;
            this.actv.Font = new System.Drawing.Font("MyMyanmar", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actv.GlowColor = System.Drawing.Color.DodgerBlue;
            this.actv.Location = new System.Drawing.Point(0, 0);
            this.actv.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.actv.Name = "actv";
            this.actv.OuterBorderColor = System.Drawing.Color.Transparent;
            this.actv.Size = new System.Drawing.Size(27, 32);
            this.actv.TabIndex = 2;
            this.actv.Text = "က";
            this.actv.VKCode = null;
            this.actv.Click += new System.EventHandler(this.actv_Click);
            // 
            // glassButton1
            // 
            this.glassButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.glassButton1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glassButton1.GlowColor = System.Drawing.Color.Cyan;
            this.glassButton1.Image = ((System.Drawing.Image)(resources.GetObject("glassButton1.Image")));
            this.glassButton1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.glassButton1.Location = new System.Drawing.Point(25, 0);
            this.glassButton1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.glassButton1.Name = "glassButton1";
            this.glassButton1.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton1.Size = new System.Drawing.Size(27, 32);
            this.glassButton1.TabIndex = 12;
            this.glassButton1.VKCode = null;
            this.glassButton1.Click += new System.EventHandler(this.showHideHandwritingInputToolStripMenuItem_Click);
            this.glassButton1.MouseLeave += new System.EventHandler(this.glassButton2_MouseLeave_2);
            // 
            // glassButton2
            // 
            this.glassButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.glassButton2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glassButton2.GlowColor = System.Drawing.Color.Cyan;
            this.glassButton2.Image = ((System.Drawing.Image)(resources.GetObject("glassButton2.Image")));
            this.glassButton2.Location = new System.Drawing.Point(50, 0);
            this.glassButton2.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.glassButton2.Name = "glassButton2";
            this.glassButton2.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton2.Size = new System.Drawing.Size(27, 32);
            this.glassButton2.TabIndex = 13;
            this.glassButton2.VKCode = null;
            this.glassButton2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            this.glassButton2.MouseLeave += new System.EventHandler(this.glassButton2_MouseLeave_2);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(102, -2);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 14);
            this.label2.TabIndex = 10;
            this.label2.Text = "save as";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(192, -2);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 14);
            this.label3.TabIndex = 11;
            this.label3.Text = "write with";
            // 
            // popout
            // 
            this.popout.Interval = 500;
            this.popout.Tick += new System.EventHandler(this.popout_Tick);
            // 
            // scrbtn
            // 
            this.scrbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.scrbtn.ContextMenuStrip = this.scripts_menu;
            this.scrbtn.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scrbtn.GlowColor = System.Drawing.Color.Cyan;
            this.scrbtn.Location = new System.Drawing.Point(77, 9);
            this.scrbtn.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.scrbtn.Name = "scrbtn";
            this.scrbtn.OuterBorderColor = System.Drawing.Color.Transparent;
            this.scrbtn.Size = new System.Drawing.Size(97, 22);
            this.scrbtn.TabIndex = 0;
            this.scrbtn.Text = "Unicode";
            this.scrbtn.VKCode = null;
            this.scrbtn.TextChanged += new System.EventHandler(this.scrbtn_TextChanged);
            this.scrbtn.Click += new System.EventHandler(this.scrbtn_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.ContextMenuStrip = this.mainmenu;
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.GlowColor = System.Drawing.Color.Cyan;
            this.label1.Location = new System.Drawing.Point(268, -6);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.label1.Name = "label1";
            this.label1.OuterBorderColor = System.Drawing.Color.Transparent;
            this.label1.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Size = new System.Drawing.Size(56, 46);
            this.label1.TabIndex = 14;
            this.label1.Text = "MyInput";
            this.label1.VKCode = null;
            this.label1.Click += new System.EventHandler(this.glassButton4_Click);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            // 
            // laybtn
            // 
            this.laybtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.laybtn.ContextMenuStrip = this.layouts;
            this.laybtn.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laybtn.GlowColor = System.Drawing.Color.Cyan;
            this.laybtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.laybtn.Location = new System.Drawing.Point(173, 9);
            this.laybtn.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.laybtn.Name = "laybtn";
            this.laybtn.OuterBorderColor = System.Drawing.Color.Transparent;
            this.laybtn.Size = new System.Drawing.Size(95, 22);
            this.laybtn.TabIndex = 4;
            this.laybtn.Text = "Type Writer";
            this.laybtn.VKCode = null;
            this.laybtn.TextChanged += new System.EventHandler(this.laybtn_TextChanged);
            this.laybtn.Click += new System.EventHandler(this.laybtn_Click);
            // 
            // Main
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(329, 32);
            this.Controls.Add(this.scrbtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.laybtn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.Name = "Main";
            this.ShowInTaskbar = false;
            this.Text = "Form1";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseEnter += new System.EventHandler(this.Main_MouseEnter);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.Move += new System.EventHandler(this.Main_Move);
            this.mainmenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Glass.GlassButton scrbtn;
        private Glass.GlassButton actv;
        private System.Windows.Forms.ContextMenuStrip mainmenu;
        private Glass.GlassButton laybtn;
        private System.Windows.Forms.ContextMenuStrip scripts_menu;
        private System.Windows.Forms.ContextMenuStrip layouts;
        private System.Windows.Forms.Timer servicetimer;
        private System.Windows.Forms.ToolStripMenuItem exitMyInputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMyInputToolStripMenuItem;
        private System.Windows.Forms.Timer cmdset;
        private System.Windows.Forms.Timer exittmr;
        private System.Windows.Forms.Timer chkreg;
        private System.Windows.Forms.Timer embedcrypt;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Glass.GlassButton glassButton1;
        private Glass.GlassButton glassButton2;
        private Glass.GlassButton label1;
        private System.Windows.Forms.Timer popout;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;

    }
}

