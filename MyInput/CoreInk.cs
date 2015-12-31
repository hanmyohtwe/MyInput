using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;

namespace CoreInk
{
    public class AI
    {
        public class State
        {
            int direction;
            char c;
        }

        public class Direction
        {
            public const int STATION = 0;
            public const int N = 1;
            public const int NW = 2;
            public const int W = 3;
            public const int SW = 4;
            public const int S = 5;
            public const int SE = 6;
            public const int E = 7;
            public const int NE = 7;
        }

        public class Quadrant
        {
            public bool first;
            public bool second;
            public bool third;
        }

        public const char bk = '\u200b';
        public static String Rule(char[] a, char b)
        {
            bool remed = false;
            if (a.Length < 1)
                return new String(a) + b;
            if (a[a.Length - 1] == bk)
            {
                a = RemoveChr(a);
                remed = true;
            }

            // Stakers rules
            if (a.Length >= 2)
            {
                if (a[a.Length - 1] == '\u1039')
                {
                    char[] normal = "\u1000\u1001\u1002\u1003\u1005\u1006\u1007\u1008\u100F\u1010\u1011\u1012\u1013\u1014\u1015\u1016\u1017\u1018\u1019\u101C\u100b".ToCharArray();
                    char[] stak = "\u1060\u1061\u1062\u1063\u1068\u1069\u106A\u106B\u1074\u1075\u1077\u1078\u1079\u107A\u107B\u107C\u107D\u107E\u107F\u108C\u106e".ToCharArray();
                    for (int i = 0; i < normal.Length; i++)
                    {
                        if (b == normal[i])
                        {
                            b = stak[i];
                        }
                    }
                }
            }

            //special stackers
            if (a.Length >= 3)
            {
                if (a[a.Length - 1] == '\u1039')
                {
                    if (a[a.Length - 2] == '\u100f')
                    {
                        if (b == '\u100c')
                        {
                            b = '\u1070';
                        }
                        if (b == '\u100d')
                        {
                            b = '\u1071';
                        }
                        if (b == '\u100b')
                        {
                            b = '\u106e';
                        }
                    }
                    else if (a[a.Length - 2] == '\u100b')
                    {
                        if (b == '\u100b')
                        {
                            b = '\u106f';
                        }
                    }
                    else if (a[a.Length - 2] == '\u100d')
                    {
                        if (b == '\u100d')
                        {
                            b = '\u1072';
                        }
                    }
                }
            }

            if (isByee(a[a.Length - 1]) && b == 'ႅ')
            {
                char c = a[a.Length - 1];
                a[a.Length - 1] = b;
                b = c;
            }
            //case big RARIT
            if (a[a.Length - 1] == '\u1085' || a[a.Length - 1] == '\u1084')
            {
                char[] byeeshay = "\u1000\u1003\u1006\u1010\u1011\u1018\u101C\u101E\u100A\u101A".ToCharArray();
                for (int i = 0; i < byeeshay.Length; i++)
                {
                    if (b == byeeshay[i])
                    {
                        a[a.Length - 1] = '\u1084';
                        break;
                    }
                    else
                    {
                        a[a.Length - 1] = '\u1085';
                    }
                }
            }

            // Mout Cha
            if (b == '\u102C')
            {
                char[] mbyees = "\u1001\u1002\u1004\u1012\u1015\u101D".ToCharArray();
                if (a.Length > 1)
                {
                    if (a[a.Length - 2] != '\u1085')
                    {
                        for (int i = 0; i < mbyees.Length; i++)
                        {
                            if (a[a.Length - 1] == mbyees[i])
                            {
                                b = '\u102b';
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < mbyees.Length; i++)
                    {
                        if (a[a.Length - 1] == mbyees[i])
                        {
                            b = '\u102b';
                        }
                    }
                }
            }

            // Shae Htao
            if (a[a.Length - 1] == '\u102b' && b == '\u103a')
            {
                b = '\u1057';        //HA HTO AND U
                //ichy trick used here!
            }
            if (a.Length > 1)
            {
                if (a[a.Length - 2] == '\u1092' && a[a.Length - 1] == '\u1092')
                {
                    a = RemoveChr(a);
                }
            }
            if (a[a.Length - 1] == '\u1090' && b == '\u102f')
            {
                a[a.Length - 1] = '\u1092';
                b = '\u1092';
            }
            if (a[a.Length - 1] == '\u1092' && b == '\u1030')
            {
                a[a.Length - 1] = '\u1090';
            }

            //Tall U and UU
            if (b == '\u102f' || b == '\u1030')
            {
                bool ch = false;
                if (a[a.Length - 1] == '\u100a')
                {
                    ch = true;
                }
                if (a[a.Length - 1] == '\u1080')
                {
                    ch = true;
                }
                if (a[a.Length - 1] == '\u1081')
                {
                    ch = true;
                }
                if (a[a.Length - 1] == '\u1082')
                {
                    ch = true;
                }
                if (a[a.Length - 1] == '\u1083')
                {
                    ch = true;
                }
                if (a[a.Length - 1] == '\u1090' && b == '\u1030')
                {
                    ch = true;
                }
                if (a.Length > 1)
                {
                    if (a[a.Length - 2] == '\u1039')
                    {
                        ch = true;
                    }
                    if (a[a.Length - 2] == '\u100a' && a[a.Length - 1] == '\u102d')
                    {
                        ch = true;
                    }
                    if (a[a.Length - 2] == '\u100a' && a[a.Length - 1] == '\u1090')
                    {
                        ch = true;
                    }
                    if (a[a.Length - 2] == '\u1085' || a[a.Length - 2] == '\u1084')
                    {
                        ch = true;
                    }
                    if (a[a.Length - 2] == '\u108d' && a[a.Length - 1] == '\u102d')
                    {
                        ch = true;
                    }
                    if (a[a.Length - 2] == '\u108d' && a[a.Length - 1] == '\u1036')
                    {
                        ch = true;
                    }
                    if (a[a.Length - 2] == '\u1080' && a[a.Length - 1] == '\u102d')
                    {
                        ch = true;
                    }
                    if (a[a.Length - 2] == '\u1080' && a[a.Length - 1] == '\u1036')
                    {
                        ch = true;
                    }
                    if (a[a.Length - 2] == '\u1082' && a[a.Length - 1] == '\u102d')
                    {
                        ch = true;
                    }
                    if (a[a.Length - 2] == '\u1082' && a[a.Length - 1] == '\u1036')
                    {
                        ch = true;
                    }
                }
                if (a.Length > 2)
                {
                    if (a[a.Length - 3] == '\u1039' && a[a.Length - 2] != bk)
                    {
                        ch = true;
                    }
                    if (a[a.Length - 3] == '\u100a' && a[a.Length - 2] == '\u1090' && a[a.Length - 1] == '\u102d')
                    {
                        ch = true;
                    }
                    if ((a[a.Length - 3] == '\u1085' || a[a.Length - 3] == '\u1084') && isByee(a[a.Length - 2]) && !isByee(a[a.Length - 1]))
                    {
                        ch = true;
                    }
                }

                if (ch)
                {
                    if (b == '\u102f')
                    {
                        b = '\u1033';
                    }
                    if (b == '\u1030')
                    {
                        b = '\u1034';
                    }
                }

            }
            // YAPIN WASWE
            if (a[a.Length - 1] == '\u1080')
            {
                if (b == '\u108d')
                {
                    a = RemoveChr(a);
                    b = '\u1081';
                }
                if (b == '\u1090')
                {
                    a = RemoveChr(a);
                    b = '\u1082';
                }
            }
            else if (a[a.Length - 1] == '\u1081')
            {
                if (b == '\u1090')
                {
                    a = RemoveChr(a);
                    b = '\u1083';
                }
            }
            else if (a[a.Length - 1] == '\u108d')
            {
                if (b == '\u1090')
                {
                    a = RemoveChr(a);
                    b = '\u108e';
                }
            }

            // UKM POSITIONING
            if (b == '\u1037')
            {
                bool ch = false;
                if (a[a.Length - 1] == '\u1014')
                {
                    ch = true;
                }
                if (a[a.Length - 1] == '\u102f')
                {
                    ch = true;
                }
                if (a[a.Length - 1] == '\u1030')
                {
                    ch = true;
                }
                if (a[a.Length - 1] == '\u1090')
                {
                    ch = true;
                }
                if (a[a.Length - 1] == '\u1033')
                {
                    ch = true;
                }
                if (a[a.Length - 1] == '\u1034')
                {
                    ch = true;
                }
                if (a[a.Length - 1] == '\u108d')
                {
                    ch = true;
                }
                if (a[a.Length - 1] == '\u1034')
                {
                    ch = true;
                }
                if (a[a.Length - 1] == '\u1080')
                {
                    ch = true;
                }
                if (a.Length > 1)
                {
                    if ((a[a.Length - 2] == '\u1085' || a[a.Length - 2] == '\u1084'))
                    {
                        ch = true;
                    }
                    if (a[a.Length - 2] == '\u1014' && a[a.Length - 1] == '\u1036')
                    {
                        ch = true;
                    }
                    if (a[a.Length - 2] == '\u1014' && a[a.Length - 1] == '\u1032')
                    {
                        ch = true;
                    }
                    if (a[a.Length - 2] == '\u103d' && a[a.Length - 1] == '\u1036')
                    {
                        ch = true;
                    }
                    if (a[a.Length - 2] == '\u1090' && a[a.Length - 1] == '\u1032')
                    {
                        ch = true;
                    }
                    if (a[a.Length - 2] == '\u1080' && a[a.Length - 1] == '\u1090')
                    {
                        ch = true;
                    }
                    if (a[a.Length - 2] == '\u1080' && a[a.Length - 1] == '\u1036')
                    {
                        ch = true;
                    }
                    if (a[a.Length - 2] == '\u108d' && a[a.Length - 1] == '\u1032')
                    {
                        ch = true;
                    }
                    if (a[a.Length - 2] == '\u108d' && a[a.Length - 1] == '\u1036')
                    {
                        ch = true;
                    }
                    if (a[a.Length - 2] == '\u1014' && a[a.Length - 1] == '\u103a')
                    {
                        ch = true;
                    }
                }

                if (a.Length > 2)
                {
                    if (a[a.Length - 3] == '\u1014' && a[a.Length - 2] == '\u1090' && a[a.Length - 1] == '\u1036')
                    {
                        ch = true;
                    }
                    if (a[a.Length - 3] == '\u1014' && a[a.Length - 2] == '\u1090' && a[a.Length - 1] == '\u1032')
                    {
                        ch = true;
                    }
                }

                //System.err.println(ch);
                if (ch)
                {
                    b = '\u1094';
                }
            }

            //NATO
            if (a[a.Length - 1] == '\u1014' || a[a.Length - 1] == '\u1059')
            {
                bool ch = false;
                if (b == '\u102f')
                {
                    ch = true;
                }
                if (b == '\u1030')
                {
                    ch = true;
                }
                if (b == '\u1080')
                {
                    ch = true;
                }
                if (b == '\u108d')
                {
                    ch = true;
                }
                if (b == '\u108e')
                {
                    ch = true;
                }
                if (b == '\u1090')
                {
                    ch = true;
                }
                if (ch)
                {
                    a[a.Length - 1] = '\u1059';
                }
                else
                {
                    a[a.Length - 1] = '\u1014';
                }
                if (ch)
                {
                    a[a.Length - 1] = '\u1059';
                }
                else
                {
                    a[a.Length - 1] = '\u1014';
                }
            }
            if (a[a.Length - 1] == '\u1085' && b == '\u1014')
            {
                b = '\u1059';
            }
            if (a.Length > 1)
            {
                if (a[a.Length - 2] == '\u1014' || a[a.Length - 2] == '\u1059')
                {
                    bool ch = false;
                    if (!isByee(a[a.Length - 1]))
                    {
                        if (b == '\u102f')
                        {
                            ch = true;
                        }
                        if (b == '\u1030')
                        {
                            ch = true;
                        }
                    }
                    if (a[a.Length - 1] == '\u1039')
                    {
                        ch = true;
                    }
                    if (!isByee(b))
                    {
                        if (ch)
                        {
                            a[a.Length - 2] = '\u1059';
                        }
                        //else a[a.Length-2] = '\u1014';
                    }
                }        //return whole string
            }
            String temp = new String(a);
            if (remed)
            {
                temp += bk;
            }
            temp += b;
            return temp;
        }

        public static bool isByee(char ch)
        {
            if ((int)ch > 4095 && (int)ch < 4130)
            {
                return true;
            }
            return false;
        }

        public static char[] RemoveChr(char[] c)
        {
            String s = new String(c);
            return s.Substring(0, s.Length - 1).ToCharArray();
        }

        public class AttachMode
        {
            public const int inside = 1;
            public const int right = 2;
            public const int left = 3;
        }

        public const int tolerance = 15;
        public List<int> Points2Directions(List<Point> points)
        {
            List<int> states = new List<int>();
            int xl = calculateDistance(points);
            if (xl < tolerance)
            {
                states.Add(Direction.STATION);
                return states;
            }
            Point lastpoint = points[0];
            bool bx = false;
            foreach (Point p in points)
            {
                if (!bx)
                {
                    lastpoint = p;
                    bx = true;
                }
                else
                {
                    int barring = getBarring4(lastpoint, p);
                    if (barring != Direction.STATION)
                    {
                        if (states.Count > 0 && states[states.Count - 1] != barring)
                        {
                            states.Add(barring);
                        }
                        else if (states.Count == 0)
                        {
                            states.Add(barring);
                        }
                        lastpoint = p;
                    }
                }
            }
            return states;
        }

        private int calculateDistance(List<Point> points)
        {
            if (points.Count > 2)
            {
                int px = 0;
                int lx = points[0].X;
                int ly = points[0].Y;
                for (int i = 1; i < points.Count; i++)
                {
                    Point p = points[i];
                    int x = Math.Abs(lx - p.X);
                    int y = Math.Abs(ly - p.Y);
                    lx = p.X;
                    ly = p.Y;
                    px += (x + y) / 2;
                }
                return px;
            }
            else
            {
                return 0;
            }
        }

        private int getBarring4(Point a, Point b)
        {
            int horizonal = b.X - a.X;
            int vertical = b.Y - a.Y;
            if (DeNegateNumber(horizonal) > DeNegateNumber(vertical))
            {
                if (horizonal > tolerance)
                {
                    return Direction.E;
                }
                else if (horizonal < -1 * tolerance)
                {
                    return Direction.W;
                }
                else
                {
                    return Direction.STATION;
                }
            }
            else
            {
                if (vertical > tolerance)
                {
                    return Direction.S;
                }
                else if (vertical < -1 * tolerance)
                {
                    return Direction.N;
                }
                else
                {
                    return Direction.STATION;
                }
            }
        }

        private int DeNegateNumber(int num)
        {
            if (num < 0)
                return -1 * num;
            else
                return num;
        }

        private int getBarring8(Point a, Point b)
        {
            int horizonal = b.X - a.X;
            int vertical = b.Y - a.Y;
            if (horizonal > tolerance)
            {
                if (vertical > tolerance)
                {
                    return Direction.SE;
                }
                else if (vertical < -1 * tolerance)
                {
                    return Direction.NE;
                }
                else
                {
                    return Direction.E;
                }
            }
            else if (horizonal < -1 * tolerance)
            {
                if (vertical > tolerance)
                {
                    return Direction.SW;
                }
                else if (vertical < -1 * tolerance)
                {
                    return Direction.NW;
                }
                else
                {
                    return Direction.W;
                }
            }
            else if (vertical > tolerance)
            {
                return Direction.S;
            }
            else if (vertical < -1 * tolerance)
            {
                return Direction.N;
            }
            else
            {
                return Direction.STATION;
            }
        }
    }
}
