using System;
using System.Collections.Generic;
using System.Text;

namespace EagleString
{
    public class SkCommand
    {
        public string m_city1;
        public string m_city2;
        public DateTime m_date = DateTime.Now;
        public string m_airline = "ALL";
        public bool m_direct = false;
        public SkCommand(string txt)
        {
            string t = txt.Substring(2).Trim(" :/".ToCharArray());
            string[] a = t.Split(" /".ToCharArray());
            if (a[0].Length == 6)
            {
                m_city1 = a[0].Substring(0, 3);
                m_city2 = a[0].Substring(3);
            }
            else throw new Exception("∏Ò Ω,sk:wuhpvg/+/ca/d");
            for (int i = 1; i < a.Length; i++)
            {
                if (a[i].ToLower() == "d") m_direct = true;
                else if (a[i].Length == 2) m_airline = a[i].ToUpper();
                else m_date = EagleString.BaseFunc.str2datetime(a[i],true);
            }
        }
    }
}
