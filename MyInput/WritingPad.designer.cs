namespace eLibrary
{
    partial class WritingPad
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WritingPad));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.delaydraw = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(210, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(204, 135);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WriteMe_MouseMove);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WriteMe_MouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WriteMe_MouseUp);
            // 
            // delaydraw
            // 
            this.delaydraw.Interval = 30;
            this.delaydraw.Tick += new System.EventHandler(this.delaydraw_Tick);
            // 
            // WritingPad
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.pictureBox1);
            this.Name = "WritingPad";
            this.Size = new System.Drawing.Size(204, 135);
            this.DoubleClick += new System.EventHandler(this.WritingPad_DoubleClick);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.WriteMe_Paint);
            this.Click += new System.EventHandler(this.WriteMe_Click);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WriteMe_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WriteMe_MouseDown);
            this.Resize += new System.EventHandler(this.WritingPad_Resize);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WriteMe_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer delaydraw;

    }
}
