namespace MyInput
{
    partial class test
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(test));
            this.button1 = new System.Windows.Forms.Button();
            this.glassButton24 = new Glass.GlassButton();
            this.glassButton44 = new Glass.GlassButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(109, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // glassButton24
            // 
            this.glassButton24.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.glassButton24.BackColor = System.Drawing.Color.DodgerBlue;
            this.glassButton24.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.glassButton24.Font = new System.Drawing.Font("MyMyanmar", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glassButton24.GlowColor = System.Drawing.Color.Black;
            this.glassButton24.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(20)))));
            this.glassButton24.Location = new System.Drawing.Point(44, 46);
            this.glassButton24.Name = "glassButton24";
            this.glassButton24.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton24.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(244)))), ((int)(((byte)(254)))));
            this.glassButton24.Size = new System.Drawing.Size(35, 23);
            this.glassButton24.TabIndex = 17;
            this.glassButton24.Text = "D";
            this.glassButton24.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.glassButton24.UseMnemonic = false;
            this.glassButton24.VKCode = "44";
            this.glassButton24.Click += new System.EventHandler(this.glassButton24_Click);
            // 
            // glassButton44
            // 
            this.glassButton44.BackColor = System.Drawing.Color.Red;
            this.glassButton44.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.glassButton44.Font = new System.Drawing.Font("MyMyanmar", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glassButton44.GlowColor = System.Drawing.Color.White;
            this.glassButton44.InnerBorderColor = System.Drawing.Color.Transparent;
            this.glassButton44.Location = new System.Drawing.Point(198, 15);
            this.glassButton44.Name = "glassButton44";
            this.glassButton44.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton44.Size = new System.Drawing.Size(19, 19);
            this.glassButton44.TabIndex = 65;
            this.glassButton44.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.glassButton44.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.glassButton44.UseMnemonic = false;
            this.glassButton44.VKCode = null;
            this.glassButton44.Click += new System.EventHandler(this.glassButton44_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.glassButton24);
            this.panel1.Controls.Add(this.glassButton44);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(235, 238);
            this.panel1.TabIndex = 66;
            // 
            // test
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "test";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "test";
            this.TopMost = true;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.test_MouseDown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private Glass.GlassButton glassButton24;
        private Glass.GlassButton glassButton44;
        private System.Windows.Forms.Panel panel1;
    }
}