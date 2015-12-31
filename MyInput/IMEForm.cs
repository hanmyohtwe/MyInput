using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace MyInput
{
    public partial class IMEForm : Form
    {
        public IMEForm()
        {
            InitializeComponent();
        }

        public void SetText(string s)
        {
            label1.Text = s;
            this.Width = label1.Width + 3;
        }

        public void ShowFormAt(int x, int y)
        {
            this.Top = y;
            this.Left = x;
            int w = Screen.GetWorkingArea(this).Width;
            if ((this.Left + this.Width) > w)
            {
                this.Left -= ((this.Left + this.Width) - Screen.GetWorkingArea(this).Width);
            }
            ShowNoActivate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            LinearGradientBrush lgb = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), BackColor, ForeColor, LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(lgb, new Rectangle(0, 0, Width, Height));
        }

        public void ShowNoActivate()
        {
            Native.ShowWindow(Handle, 4);//SW_SHOWNOACTIVATE
        }


        public void Hide()
        {
            Native.ShowWindow(Handle, 0);
        }
        /*
        protected override void SetVisibleCore(bool value)
        {
            if (value)
            {
                // AttachThreadInput(GetWindowThreadProcessId(GetForegroundWindow(), out id),
                //    GetCurrentThreadId(), true);
                Native.ShowWindow(Handle, 4);//SW_SHOWNOACTIVATE
            }
            else base.SetVisibleCore(value);
        }*/
    }


    public class Native
    {
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        public static extern bool GetCaretPos(ref Point lpPoint);
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref Point lpPoint);
        [DllImport("user32.dll")]
        public static extern IntPtr GetFocus();
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo,
           bool fAttach);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        [DllImport("kernel32.dll")]
        public static extern uint GetCurrentThreadId();
        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();
        [DllImport("user32.dll")]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);
        [DllImport("kernel32.dll")]
        public static extern void ExitThread(uint dwExitCode);
    }

}