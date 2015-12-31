namespace MyInput
{
    partial class WarningText
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WarningText));
            this.label1 = new System.Windows.Forms.Label();
            this.glassButton2 = new Glass.GlassButton();
            this.glassButton1 = new Glass.GlassButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("MyMyanmar", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(617, 401);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // glassButton2
            // 
            this.glassButton2.Location = new System.Drawing.Point(447, 9);
            this.glassButton2.Name = "glassButton2";
            this.glassButton2.OuterBorderColor = System.Drawing.Color.DimGray;
            this.glassButton2.Size = new System.Drawing.Size(88, 37);
            this.glassButton2.TabIndex = 2;
            this.glassButton2.Text = "More";
            this.glassButton2.VKCode = null;
            this.glassButton2.Click += new System.EventHandler(this.glassButton2_Click);
            this.glassButton2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glassButton2_MouseDown);
            // 
            // glassButton1
            // 
            this.glassButton1.Location = new System.Drawing.Point(541, 9);
            this.glassButton1.Name = "glassButton1";
            this.glassButton1.OuterBorderColor = System.Drawing.Color.DimGray;
            this.glassButton1.Size = new System.Drawing.Size(88, 37);
            this.glassButton1.TabIndex = 1;
            this.glassButton1.Text = "OK";
            this.glassButton1.VKCode = null;
            this.glassButton1.Click += new System.EventHandler(this.glassButton1_Click);
            this.glassButton1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glassButton1_MouseDown);
            // 
            // WarningText
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(641, 419);
            this.Controls.Add(this.glassButton2);
            this.Controls.Add(this.glassButton1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WarningText";
            this.Opacity = 0.9;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WarningText";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Glass.GlassButton glassButton1;
        private Glass.GlassButton glassButton2;
    }
}