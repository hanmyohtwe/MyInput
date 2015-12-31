using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MyInput
{
    class DictionaryProvider
    {
        List<string> words = new List<string>();
        public DictionaryProvider()
        {
            StreamReader sr = new StreamReader("wordlist.txt");
            while (!sr.EndOfStream)
            {
                words.Add(sr.ReadLine());
            }
            sr.Close();
        }

        public List<string> getSuggestion(string word)
        {
            List<string> sugs = new List<string>();
            foreach (string s in words)
            {
                if (s.StartsWith(word))
                {
                    sugs.Add(s);
                    if (sugs.Count > 9)
                        return sugs;
                }
            }
            return sugs;
        }
    }
}
