namespace MyInput
{
    partial class ConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.screnable = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.glassButton3 = new Glass.GlassButton();
            this.glassButton2 = new Glass.GlassButton();
            this.glassButton1 = new Glass.GlassButton();
            this.glassButton44 = new Glass.GlassButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.glassButton44);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(503, 33);
            this.panel1.TabIndex = 64;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Options";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.ForeColor = System.Drawing.Color.White;
            this.checkBox1.Location = new System.Drawing.Point(12, 51);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(86, 18);
            this.checkBox1.TabIndex = 66;
            this.checkBox1.Text = "ON/OFF Key";
            this.toolTip1.SetToolTip(this.checkBox1, "enabling this option will let you on and off MyInput by pressing a hotkey.");
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.BackColor = System.Drawing.Color.Transparent;
            this.checkBox3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox3.ForeColor = System.Drawing.Color.White;
            this.checkBox3.Location = new System.Drawing.Point(427, 333);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(57, 18);
            this.checkBox3.TabIndex = 73;
            this.checkBox3.Text = "Debug";
            this.toolTip1.SetToolTip(this.checkBox3, "Only for developers. See MyInput SDK for more information. enabling this option m" +
                    "ay consume large amount of memory, cpu and disk space.");
            this.checkBox3.UseVisualStyleBackColor = false;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.BackColor = System.Drawing.Color.Transparent;
            this.checkBox4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox4.ForeColor = System.Drawing.Color.White;
            this.checkBox4.Location = new System.Drawing.Point(12, 94);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(115, 18);
            this.checkBox4.TabIndex = 75;
            this.checkBox4.Text = "Layout Toggle Key";
            this.toolTip1.SetToolTip(this.checkBox4, "enabling this option will let you switch keyboard layout by pressing a hot key.");
            this.checkBox4.UseVisualStyleBackColor = false;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // screnable
            // 
            this.screnable.AutoSize = true;
            this.screnable.BackColor = System.Drawing.Color.Transparent;
            this.screnable.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.screnable.ForeColor = System.Drawing.Color.White;
            this.screnable.Location = new System.Drawing.Point(12, 134);
            this.screnable.Name = "screnable";
            this.screnable.Size = new System.Drawing.Size(110, 18);
            this.screnable.TabIndex = 79;
            this.screnable.Text = "Script Toggle Key";
            this.toolTip1.SetToolTip(this.screnable, "enabling this option will let you switch keyboard layout by pressing a hot key.");
            this.screnable.UseVisualStyleBackColor = false;
            this.screnable.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(239, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 14);
            this.label7.TabIndex = 77;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(239, 139);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 14);
            this.label8.TabIndex = 81;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(239, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 14);
            this.label3.TabIndex = 69;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // glassButton3
            // 
            this.glassButton3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.glassButton3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glassButton3.GlowColor = System.Drawing.Color.Lime;
            this.glassButton3.Location = new System.Drawing.Point(139, 131);
            this.glassButton3.Name = "glassButton3";
            this.glassButton3.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton3.Size = new System.Drawing.Size(268, 28);
            this.glassButton3.TabIndex = 80;
            this.glassButton3.Text = "Set";
            this.glassButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.glassButton3, "hot key to switch keyboard layout. Press the button and press any key.");
            this.glassButton3.UseMnemonic = false;
            this.glassButton3.VKCode = null;
            this.glassButton3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glassButton3_KeyDown);
            // 
            // glassButton2
            // 
            this.glassButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.glassButton2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glassButton2.GlowColor = System.Drawing.Color.Lime;
            this.glassButton2.Location = new System.Drawing.Point(139, 91);
            this.glassButton2.Name = "glassButton2";
            this.glassButton2.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton2.Size = new System.Drawing.Size(268, 28);
            this.glassButton2.TabIndex = 76;
            this.glassButton2.Text = "Set";
            this.glassButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.glassButton2, "hot key to switch keyboard layout. Press the button and press any key.");
            this.glassButton2.UseMnemonic = false;
            this.glassButton2.VKCode = null;
            this.glassButton2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glassButton2_KeyDown);
            // 
            // glassButton1
            // 
            this.glassButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.glassButton1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glassButton1.GlowColor = System.Drawing.Color.Lime;
            this.glassButton1.Location = new System.Drawing.Point(139, 45);
            this.glassButton1.Name = "glassButton1";
            this.glassButton1.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton1.Size = new System.Drawing.Size(268, 28);
            this.glassButton1.TabIndex = 68;
            this.glassButton1.Text = "Set";
            this.glassButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.glassButton1, "hot key to on or off MyInput. Press the button and press any key.");
            this.glassButton1.UseMnemonic = false;
            this.glassButton1.VKCode = null;
            this.glassButton1.Click += new System.EventHandler(this.glassButton1_Click);
            this.glassButton1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glassButton1_KeyDown);
            this.glassButton1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.glassButton1_KeyUp);
            // 
            // glassButton44
            // 
            this.glassButton44.BackColor = System.Drawing.Color.Red;
            this.glassButton44.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.glassButton44.Font = new System.Drawing.Font("MyMyanmar", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glassButton44.GlowColor = System.Drawing.Color.Black;
            this.glassButton44.InnerBorderColor = System.Drawing.Color.Transparent;
            this.glassButton44.Location = new System.Drawing.Point(474, 6);
            this.glassButton44.Name = "glassButton44";
            this.glassButton44.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton44.Size = new System.Drawing.Size(17, 17);
            this.glassButton44.TabIndex = 65;
            this.glassButton44.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.glassButton44.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.glassButton44.UseMnemonic = false;
            this.glassButton44.VKCode = null;
            this.glassButton44.Click += new System.EventHandler(this.glassButton44_Click);
            this.glassButton44.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glassButton44_MouseDown);
            // 
            // ConfigForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(503, 363);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.glassButton3);
            this.Controls.Add(this.screnable);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.glassButton2);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.glassButton1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("MyMyanmar", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigForm";
            this.Opacity = 0.9D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Config";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private Glass.GlassButton glassButton1;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private Glass.GlassButton glassButton2;
        private System.Windows.Forms.ToolTip toolTip1;
        private Glass.GlassButton glassButton3;
        private System.Windows.Forms.CheckBox screnable;
        private Glass.GlassButton glassButton44;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer1;
    }
}