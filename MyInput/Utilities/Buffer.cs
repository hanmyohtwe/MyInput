using System;
using System.Collections.Generic;
using System.Text;
using MyInput.Utilities;

namespace MyInput.Keyboard_Classes
{
    class Buffer
    {
        private static StringBuilder temp;
        public Buffer()
        {
            if (temp == null)
                temp = new StringBuilder();
        }

        public void Append(String s)
        {
            temp.Append(s);
            ChkBuf();
        }

        public void ChkBuf()
        {
            if (temp.Length >= 100)
            {
                temp = temp.Remove(0, temp.Length / 2);
                Log l = new Log();
                l.write("Buffer Divided:" + temp.ToString());
            }
        }

        public void Change(String s)
        {
            temp = new StringBuilder(s);
            ChkBuf();
        }

        public string getBuffer()
        {
            return temp.ToString();
        }

        public StringBuilder getBufferSB()
        {
            return temp;
        }

        public void PopChars(int num)
        {
            try
            {
                temp = temp.Remove(temp.Length - num, num);
            }
            catch (Exception e)
            {
            }
        }

        internal void Flush()
        {
            Log l = new Log();
            l.write("Buffer Flushed");
            temp = new StringBuilder();
        }
    }
}
