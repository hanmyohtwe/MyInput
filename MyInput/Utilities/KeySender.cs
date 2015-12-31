using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;

namespace MyInput.Utilities
{
    class SendCH
    {
        public static unsafe void Send(char c)
        {
            INPUT structInput;
            structInput = new INPUT();
            structInput.type = Win32Consts.INPUT_KEYBOARD;
            // Key down shift, ctrl, and/or alt
            structInput.ki.wScan = (short)c;
            structInput.ki.wVk = 0;
            structInput.ki.time = 0;
            structInput.ki.dwFlags = Win32Consts.UNICODE;

            structInput.ki.dwExtraInfo = GetMessageExtraInfo();

            uint i = SendInput(1, ref structInput, (int)sizeof(INPUT));
            //if (i <= 0) MessageBox.Show(i.ToString());
        }

        public static unsafe void Send(int vk)
        {
            INPUT structInput;
            structInput = new INPUT();
            structInput.type = Win32Consts.INPUT_KEYBOARD;

            // Key down shift, ctrl, and/or alt
            structInput.ki.wScan = 0;
            structInput.ki.wVk = (short)vk;
            structInput.ki.time = 0;
            structInput.ki.dwFlags = 0;
            structInput.ki.dwExtraInfo = GetMessageExtraInfo();
            uint i = SendInput(1, ref structInput, (int)sizeof(INPUT));
        }

        public static unsafe void Send(string ch)
        {
            foreach (char c in ch.ToCharArray())
            {
                Thread.Sleep(1);
                Send(c);
            }
        }
        public static unsafe void Del()
        {
            INPUT[] structInput;
            structInput = new INPUT[2];
            structInput[0] = new INPUT();
            structInput[1] = new INPUT();
            structInput[0].type = Win32Consts.INPUT_KEYBOARD;
            structInput[1].type = Win32Consts.INPUT_KEYBOARD;
            // Key down shift, ctrl, and/or alt
            structInput[0].ki.wScan = 0;
            structInput[1].ki.wScan = 0;
            structInput[0].ki.wVk = 0x08;
            structInput[1].ki.wVk = 0x08;
            
            structInput[0].ki.time = 0;
            structInput[1].ki.time = 0;
            structInput[0].ki.dwFlags = 0x00;
            structInput[1].ki.dwFlags = 0x02;
            structInput[0].ki.dwExtraInfo = GetMessageExtraInfo();
            structInput[1].ki.dwExtraInfo = GetMessageExtraInfo();
            uint i = SendInput(2, structInput, (int)sizeof(INPUT));
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetMessageExtraInfo();


        public class Win32Consts
        {
            public const int INPUT_MOUSE = 0;
            public const int INPUT_KEYBOARD = 1;
            public const int UNICODE= 4;
        }    

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);
        [StructLayout(LayoutKind.Sequential)]
        struct MOUSEINPUT
        {
            int dx;
            int dy;
            int mouseData;
            int dwFlags;
            int time;
            IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT
        {
            public short wVk;
            public short wScan;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct HARDWAREINPUT
        {
            int uMsg;
            short wParamL;
            short wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct INPUT
        {
            [FieldOffset(0)]
            public int type;
            [FieldOffset(4)]
            public MOUSEINPUT mi;
            [FieldOffset(4)]
            public KEYBDINPUT ki;
            [FieldOffset(4)]
            public HARDWAREINPUT hi;
        }

        internal static void Del(int x)
        {
            for (int i = 0; i < x; i++)
            {
                Del();
            }
        }
    }







}
