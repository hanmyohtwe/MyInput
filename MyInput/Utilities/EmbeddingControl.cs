using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MyInput.Utilities
{
    static class EmbeddingControl
    {
        public static bool isValid(string key)
        {
            try
            {
                string tms = key.Substring(0, key.IndexOf("-"));
                string pvk = key.Substring(key.IndexOf("-") + 1, (key.LastIndexOf("-") - key.IndexOf("-")) - 1);
                string pbk = key.Substring(key.LastIndexOf("-") + 1);
                DateTime dt = DateTime.Now;
                long cur = dt.Ticks;
                long _tms = long.Parse(tms);
                long min = 50000000;
                long _cms = cur - min;
                if (_cms < _tms)
                {
                    if (ValidatePrivateKey(pvk))
                    {
                        string actualkey = ProcessPublicKey(pvk, tms);
                        if (actualkey == pbk)
                            return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        public static string ProcessPublicKey(string privateKey, string TimeStamp)
        {
            string a = TimeStamp.Substring(0, TimeStamp.Length / 2);
            string b = TimeStamp.Substring(TimeStamp.Length / 2);
            string c = Convert.ToString(Convert.ToInt32(a) + Convert.ToInt32(b) + Convert.ToInt32(privateKey.Substring(0, 4)));
            int[] ckeys = { 1212, 3431, 12454, 5641, 2345, 14236, 4453, 5665, 7812, 4422, 1242, 3276, 17722 };
            string result = "0";
            int j = 0;
            for (int i = 0; i < c.Length; i++)
            {
                long r = Convert.ToInt64(result);
                int y = Convert.ToInt32(c[i].ToString());
                y *= ckeys[j++];
                r += y;
                if (j >= ckeys.Length)
                    j = 0;
                result = (r*2).ToString();
            }
            return result;
        }

        public static string createPublicKey(string privateKey)
        {
            if (ValidatePrivateKey(privateKey))
            {
                string tms = DateTime.Now.Ticks.ToString();
                string pvt = privateKey;
                string pbk = ProcessPublicKey(privateKey, tms);
                return tms + "-" + pvt + "-" + pbk;
            }
            else
            {
                return "0-0-0";
            }
        }

        public static bool ValidatePrivateKey(string key)
        {
            string f = key.Substring(0, 4);
            int _1 = Int32.Parse(key[0].ToString());
            int _2 = Int32.Parse(key[1].ToString());
            int _3 = Int32.Parse(key[2].ToString());
            int _4 = Int32.Parse(key[3].ToString());
            int _5 = _1 + _3;
            int _6 = _2 + _4;
            int _7 = _1 + _2 + _3 + _4 + _5 + _6;
            string chk = key.Substring(4);
            if (chk == Convert.ToString(_5) + Convert.ToString(_6) + Convert.ToString(_7))
                return true;
            return false;
        }
    }
}
