using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using CoreInk;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace eLibrary
{
    public partial class WritingPad : UserControl
    {
        public WritingPad()
        {
            InitializeComponent();
            first = this.Height / 3;
            second = first * 2;
            ai = new AI();
            buffer = new Bitmap(Width, Height);
            ClearBuffer();
           // XRefresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            ClearBuffer();
        }

        protected override void OnResize(EventArgs e)
        {
            first = this.Height / 3;
            second = first * 2;
            buffer = new Bitmap(Width, Height);
            ClearBuffer();
            base.OnResize(e);
        }
        //ArrayList machs;
       // StreamWriter sw;
        //char ch = '\u1000';
        Image buffer;
        public void XRefresh()
        {
            //if (buffer == null)
            //    buffer = new Bitmap(Width, Height);
            //if (buffer.Width != Width || buffer.Height != Height)
            //    buffer = new Bitmap(Width, Height);
            //Graphics g = pictureBox1.CreateGraphics();
            //g.Clear(Color.White);
            //DrawLines(g, Height, Width);
           // first = this.Height / 3;
            //second = first * 2;
            //g.FillRectangle(new SolidBrushX(Color.LightBlue), new Rectangle(0, 0, this.Width, this.Height));
            //g.DrawLine(new Pen(Color.FromArgb(0,37,107)), 0, first, this.Width, first);
            //g.DrawLine(new Pen(Color.FromArgb(0, 37, 107)), 0, second, this.Width, second);
            //Invalidate();
        }

        public void ClearBuffer()
        {
            Graphics g = Graphics.FromImage(buffer);
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            LinearGradientBrush lgb = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), ForeColor, BackColor, LinearGradientMode.Vertical);
            g.FillRectangle(lgb, new Rectangle(0, 0, Width, Height));
            //g.Clear(Color.Black);
            string lmode = "";
            switch (language)
            {
                case 0:
                    lmode = "ကခဂ";
                    break;
                case 1:
                    lmode = "abc";
                    break;
                case 2:
                    lmode = "123";
                    break;
                case 3:
                    lmode = "၁၂၃";
                    break;
            }
            g.DrawString("ကခဂ", new Font("MyMyanmar", 9, FontStyle.Regular), new SolidBrush(Color.FromArgb(20, 20, 20)), 4, -1);
            g.DrawLine(new Pen(Color.FromArgb(60, 0, 0, 0)), 0, first, this.Width, first);
            g.DrawLine(new Pen(Color.FromArgb(60, 0, 0, 0)), 0, second, this.Width, second);
            g.DrawString("၁၂၃", new Font("MyMyanmar", 9, FontStyle.Regular), new SolidBrush(Color.FromArgb(20, 20, 20)), Width - 28, -1);
            int jx = (Width / 4) - 2;
            g.DrawLine(new Pen(Color.FromArgb(20, 0, 0, 0)), jx * 3, 0, jx * 3, Height);
            g.DrawRectangle(new Pen(Color.FromArgb(255, 0, 0, 0), 1), new Rectangle(0, 0, Width - 1, Height - 1));
            g.Dispose();
            delaydraw.Enabled = true;
        }

        private void DrawPoints(List<Point> points, Color c)
        {
            Graphics g = Graphics.FromImage(buffer);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (points.Count > 1)
                g.DrawLines(new Pen(c, 4), points.ToArray());
            else
            {
                foreach (Point p in points)
                {
                    g.FillEllipse(new SolidBrush(c), new Rectangle(p, new Size(2, 2)));
                }
            }
            delaydraw.Enabled = true;
        }

        private int[] getIntervals(Point a, Point b)
        {
            int xx = a.X - b.X;
            if (xx < 0) xx= xx * -1;
            int yy = a.Y - b.Y;
            if (yy < 0) yy = yy * -1;
            int average = (xx + yy) / 2;
            return null;
        }

        private void WriteMe_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.DrawImage(buffer, 0, 0);
        }

        private void WriteMe_Click(object sender, EventArgs e)
        {
            //XRefresh();
        }

        public void StartDrawing(int x, int y)
        {
            down = true;
            Start(x, y);
        }

        private bool down = false;
        private void WriteMe_MouseDown(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("down");
            down = true;
            Start(e.X,e.Y);
        }


        bool moving = false;
        private void Start(int x, int y)
        {
            moving = true;
            points = new List<Point>();
        }

        public void StopDrawing(int x, int y)
        {
            if (!down)
                Start(x,y);
            Stop(x, y);
            down = false;
        }

        public void MoveDrawing(int x, int y)
        {
            if (!down)
                StartDrawing(x, y);
            else
                XMove(x, y);
        }

        CoreInk.AI ai; 
        private void Stop(int x, int y)
        {
            moving = false;
            if (points.Count == 0) return;
            List<int> directions = ai.Points2Directions(points);
            timer1.Enabled = false;
            timer1.Enabled = true;
            if (OnWrittenEvent != null)
            {
                OnWrittenEvent.Invoke(points, directions,getQuadrants(),isAttached());
            }
            lastpoints = points;
        }

        private int isAttached()
        {
            List<Point> _base = getMaxMin(lastpoints);
            List<Point> _att = getMaxMin(points);
            if (_base[1].X > _att[0].X)
                return 0;
            if (_base[0].X < _att[1].X)
                return 0;
            if (_base[1].Y > _att[0].Y)
                return 0;
            if (_base[0].Y < _att[1].Y)
                return 0;
            return 1;
        }

        private List<Point> getMaxMin(List<Point> ps)
        {
            int max_x = 0;
            int min_x = this.Width;
            int max_y = 0;
            int min_y = this.Height;
            foreach (Point p in ps)
            {
                if (p.X > max_x)
                    max_x = p.X;
                if (p.Y > max_y)
                    max_y = p.Y;
                if (p.X < min_x)
                    min_x = p.X;
                if (p.Y < min_y)
                    min_y = p.Y;
            }
            List<Point> ls = new List<Point>();
            ls.Add(new Point(max_x,max_y));
            ls.Add(new Point(min_x,min_y));
            return ls;
        }

        public CoreInk.AI.Quadrant getQuadrants()
        {
            CoreInk.AI.Quadrant q = new AI.Quadrant();
            foreach (Point p in points)
            {
                if (p.Y < first - (first / 2))
                    q.first = true;
                if (p.Y > first + (first / 3) && p.Y < second - (first / 3))
                {
                    q.second = true;
                }
                if (p.Y > second + (first / 2))
                    q.third = true;
            }
            if (!q.first && !q.second && !q.third)
            {
                if (points.Count > 0)
                {
                    Point p = points[0];
                    if (p.Y < first)
                        q.first = true;
                    if (p.Y > first && p.Y < second)
                        q.second = true;
                    if (p.Y > second)
                        q.third = true;
                }
            }
            return q;
        }

        public int first, second;
        private List<Point> points = new List<Point>();
        private List<Point> lastpoints = new List<Point>();
        private void XMove(int x, int y)
        {
            if (moving)
            {
                if (y == 0)
                    return;
                //ClearBuffer();
                points.Add(new Point(x, y));
                DrawPoints(lastpoints, Color.Navy);
                DrawPoints(points, Color.White);
            }
        }

        public bool Xclimbing()
        {
            int count = 0;
            for (int i = points.Count; i <= 0; i--)
            {
                count++;
                if (count >= 3)
                    return false;
                if (i - 1 >= 0)
                {
                    Point a = (Point)points[i - 1];
                    Point b = (Point)points[i];
                    if (a.X >= b.X)
                        return false;
                }
            }
            return true;
        }

        public bool Xfalling()
        {
            int count = 0;
            for (int i = points.Count; i <= 0; i--)
            {
                count++;
                if (count >= 3)
                    return false;   
                if (i - 1 >= 0)
                {
                    Point a = (Point)points[i - 1];
                    Point b = (Point)points[i];
                    if (a.X <= b.X)
                        return false;
                }
            }
            return true;
        }

        public delegate bool OnWritten(List<Point> points, List<int> directions,CoreInk.AI.Quadrant q, int AttachMode);
        public OnWritten OnWrittenEvent;
        private void WriteMe_MouseUp(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("up");
            if (!down)
                Start(e.X, e.Y);
            Stop(e.X,e.Y);
            down = false;
        }

        private void WriteMe_MouseMove(object sender, MouseEventArgs e)
        {
            XMove(e.X, e.Y);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //timer1.Enabled = false;
            //XRefresh();
        }

        private void WritingPad_Resize(object sender, EventArgs e)
        {
            //XRefresh();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (moving)
            {
                timer1.Enabled = false;
                return;
            }
            timer1.Enabled = false;
            points = new List<Point>();
            lastpoints = new List<Point>();
            ClearBuffer();
        }

        private void delaydraw_Tick(object sender, EventArgs e)
        {
            try
            {
                delaydraw.Enabled = false;
                this.CreateGraphics().DrawImage(buffer, 0, 0);
            }
            catch { }
        }

        private int language = 0;
        internal void SetLanguage(int i)
        {
            language = i;
        }

        private void WritingPad_DoubleClick(object sender, EventArgs e)
        {

        }
    }
}
