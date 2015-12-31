using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MyInput.Utilities
{
    class Log
    {

        private static StreamWriter sw;
        public Log()
        {
            Config cfg = new Config("MyInput\\");
            if (Convert.ToBoolean(cfg.Read("debug", "false")))
            {
                if (sw == null)
                    sw = new StreamWriter("keylog.log");
            }
            else
            {
                if (sw != null)
                    sw.Dispose();
                sw = null;
            }
        }

        public void write(string s)
        {
            if(sw != null){
                sw.WriteLine(s);
                sw.Flush();
            }
        }

    }
}
