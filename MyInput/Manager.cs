using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.SQLite;

namespace CoreInk
{
    public class Manager
    {
        System.Data.SQLite.SQLiteConnection conn;
        public List<Character> chars = new List<Character>();
        public void addCharacter(Character c)
        {
            foreach (Character ch in chars)
            {
                if (c.charx == ch.charx)
                    return;
            }
            chars.Add(c);
        }

        public void removeCharacter(Character c)
        {
            chars.Remove(c);
        }
        public List<Stroke> MatchStroke(string stroke, AI.Quadrant q)
        {
            return MatchStroke(stroke, q, 0);
        }

        public List<Stroke> MatchStroke(string stroke, AI.Quadrant q, int language)
        {
            bool b = true;
        REDO:
            conn = new System.Data.SQLite.SQLiteConnection(@"Data Source=CoreInkLib.dll");
            conn.Open();
            SQLiteCommand sqc = new SQLiteCommand("SELECT * FROM strokes WHERE directions = '" + stroke + "' AND quadrant='" + Quadrant2Str(q) + "' AND lang = " + language.ToString() + ";", conn);
            SQLiteDataReader sqr = sqc.ExecuteReader();
            List<Stroke> l = new List<Stroke>();
            while (sqr.Read())
            {
                Stroke st = new Stroke();
                st.sid = Convert.ToInt32(sqr[0]);
                st.vid = Convert.ToInt32(sqr[1]);
                st.cid = Convert.ToInt32(sqr[2]);
                st.thisstroke = Convert.ToInt32(sqr[5]);
                st.quadrant = sqr[4].ToString();
                sqc = new SQLiteCommand("SELECT ifnull(phase,0) FROM strokes WHERE vid = " + st.vid.ToString() + " AND phase >" + st.thisstroke.ToString() + " AND lang=" + language.ToString() + ";", conn);
                st.nextstroke = Convert.ToInt32(sqc.ExecuteScalar());
                foreach (char c in sqr[3].ToString())
                {
                    st.directions.Add(Convert.ToInt32(c.ToString()));
                }
                if (b == false)
                    st.quadrant = "C";
                l.Add(st);
            }
            conn.Close();
            if (q.third && l.Count == 0 && b)
            {
                q.third = false; q.second = true; b = false;
                goto REDO;
            }
            return l;
        }

        public string Quadrant2Str(AI.Quadrant q)
        {
            string s = "";
            if (q.first)
                s += "A";
            if (q.second)
                s += "B";
            if (q.third)
                s += "C";
            return s;
        }
        public Character MatchCharacter(int charid)
        {
            conn = new System.Data.SQLite.SQLiteConnection(@"Data Source=CoreInkLib.dll");
            conn.Open();
            SQLiteCommand sqc = new SQLiteCommand("SELECT * FROM character WHERE char_id = " + charid+ ";", conn);
            SQLiteDataReader sqr = sqc.ExecuteReader();
            sqr.Read();
            Character c = new Character();
            c.lang = Convert.ToInt32(sqr[2]);
            c.charx = sqr[1].ToString();
            conn.Close();
            return c;
        }

        public void LoadData()
        {
            chars = new List<Character>();
            conn = new System.Data.SQLite.SQLiteConnection(@"Data Source=CoreInkLib.dll");
            conn.Open();
            SQLiteCommand sqc = new SQLiteCommand("SELECT * FROM character ORDER BY charx", conn);
            SQLiteDataReader sqr = sqc.ExecuteReader();
            while (sqr.Read())
            {
                Character c = new Character();
                c.charx = sqr[1].ToString();
                SQLiteCommand sqc2 = new SQLiteCommand("SELECT * FROM variant WHERE char_id = " + sqr[0].ToString(), conn);
                SQLiteDataReader sqr2 = sqc2.ExecuteReader();
                while (sqr2.Read())
                {
                    Variant v = new Variant();
                    SQLiteCommand sqc3 = new SQLiteCommand("SELECT * FROM strokes WHERE vid = " + sqr2[0].ToString(), conn);
                    SQLiteDataReader sqr3 = sqc3.ExecuteReader();
                    while (sqr3.Read())
                    {
                        Stroke s = Int2Stroke(sqr3[3].ToString());
                        s.quadrant = sqr3[4].ToString();
                        v.Strokes.Add(s);
                    }
                    c.variants.Add(v);
                }
                chars.Add(c);
            }
            conn.Close();
        }

        public void Clear()
        {
            conn = new System.Data.SQLite.SQLiteConnection(@"Data Source=CoreInkLib.dll");
            conn.Open();
            SQLiteCommand sqc = new SQLiteCommand("DELETE FROM character", conn);
            sqc.ExecuteNonQuery();
            sqc = new SQLiteCommand("DELETE FROM strokes", conn);
            sqc.ExecuteNonQuery();
            sqc = new SQLiteCommand("DELETE FROM variant", conn);
            sqc.ExecuteNonQuery();
            conn.Close();
        }

        public void SaveData()
        {
            conn = new System.Data.SQLite.SQLiteConnection(@"Data Source=CoreInkLib.dll");
            conn.Open();
            SQLiteCommand sqc = new SQLiteCommand("DELETE FROM character",conn);
            sqc.ExecuteNonQuery();
            sqc = new SQLiteCommand("DELETE FROM strokes", conn);
            sqc.ExecuteNonQuery();
            sqc = new SQLiteCommand("DELETE FROM variant", conn);
            sqc.ExecuteNonQuery();
            foreach (Character c in chars)
            {
                sqc = new SQLiteCommand("SELECT ifnull(max(char_id),0) FROM character", conn);
                int id = Convert.ToInt32(sqc.ExecuteScalar());
                id++;
                sqc = new SQLiteCommand("INSERT INTO character VALUES (" + id.ToString() + ",'" + c.charx + "'," + c.lang.ToString() + ")", conn);
                sqc.ExecuteNonQuery();
                foreach (Variant v in c.variants)
                {
                    sqc = new SQLiteCommand("SELECT ifnull(max(vid),0) FROM variant", conn);
                    int vid = Convert.ToInt32(sqc.ExecuteScalar());
                    vid++;
                    sqc = new SQLiteCommand("INSERT INTO variant VALUES (" + vid.ToString() + "," + id.ToString() + ")", conn);
                    sqc.ExecuteNonQuery();
                    int sti = 0;
                    foreach (Stroke st in v.Strokes)
                    {
                        sqc = new SQLiteCommand("SELECT ifnull(max(s_id),0) FROM strokes", conn);
                        int sid = Convert.ToInt32(sqc.ExecuteScalar());
                        sid++;
                        sqc = new SQLiteCommand("INSERT INTO strokes VALUES (" + sid.ToString() + "," + vid.ToString() + "," + id.ToString() + ",'" + Stroke2Int(st).ToString() + "','" + st.quadrant + "'," + sti.ToString() + ")", conn);
                        sqc.ExecuteNonQuery();
                        sti++;
                    }
                }
            }
            conn.Clone();
        }

        private Stroke Int2Stroke(string s)
        {
            Stroke st = new Stroke();
            foreach (char c in s)
            {
                st.directions.Add(Convert.ToInt32(c.ToString()));
            }
            return st;
        }

        private string Stroke2Int(Stroke s)
        {
            string st = "";
            foreach (int i in s.directions)
            {
                st += i.ToString();
            }
            return (st);
        }
    }

    public class Character
    {
        public int charid;
        public string charx;
        public List<Variant> variants = new List<Variant>();
        public int lang = 0;
    }

    public class Variant
    {
        public int vid;
        public List<Stroke> Strokes = new List<Stroke>();
    }

    public class Stroke
    {
        public int sid;
        public int vid;
        public int cid;
        public List<int> directions = new List<int>();
        public string quadrant = "B";
        public int nextstroke = 0;
        public int thisstroke = 0;
    }
}
