using System;
using System.Collections.Generic;
using System.Text;

namespace EagleString
{
    public class AvCommand
    {
        public AvCommand(string localcity, string cmd)
        {
            m_date = DateTime.Now;
            string error = "av使用格式:av h wuhpvg01may/ca/d";
            string s = cmd.ToLower().Trim();
            if (s.StartsWith("av")) s = s.Substring(2).Trim();
            s = egString.trim(s, '/').Trim();
            m_avh = (s.StartsWith("h/") || s.StartsWith("h "));
            if (m_avh) s = s.Substring(2);
            if (s.Length == 3)
            {
                m_city1 = localcity;
                m_city2 = s.Substring(0,3);
                m_date = DateTime.Now;
                m_airline = "ALL";
                m_direct = false;
                return;
            }
            else if (s.Length == 4)
            {
                m_city1 = localcity;
                m_city2 = s.Substring(0, 3);
                m_direct = (s.Length == 4 && s[3] == 'd');
                m_airline = "ALL";
                if (s[3] == '+') m_date = DateTime.Now.AddDays(1);
                if (s[3] == '.') m_date = DateTime.Now;
                return;
            }
            else if (s.Length > 4)
            {
                char c = s[3];
                if (c >= 'a' && c <= 'z')
                {
                    m_city1 = s.Substring(0, 3);
                    m_city2 = s.Substring(3, 3);
                    s = s.Substring(6);
                }
                else
                {
                    m_city1 = localcity;
                    m_city2 = s.Substring(0, 3);
                    s = s.Substring(3);
                }
                s = egString.trim(s, '/').Trim();
                string[] b = s.Split(new string[] { "/", " " }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string t in b)
                {
                    if (t.Trim() == "d")
                    {
                        m_direct = true;
                        break;
                    }
                    else if (t.Length == 2)
                    {
                        m_airline = t;
                    }
                    else if (t == ".")
                    {

                    }
                    else if (t == "+")
                    {
                        m_date = DateTime.Now.AddDays(1);
                    }
                    else if (t.Length == 4)
                    {
                        m_date = EagleString.BaseFunc.str2datetime(t, true);
                    }
                    else if (t.Length == 5)
                    {
                        if (t[1] > '9')
                        {
                            m_date = EagleString.BaseFunc.str2datetime(t.Substring(0, 4), true);
                            m_direct = true;
                        }
                        else
                        {
                            m_date = EagleString.BaseFunc.str2datetime(t, true);
                        }
                    }
                    else if (t.Length == 6)
                    {
                        m_date = EagleString.BaseFunc.str2datetime(t.Substring(0, 5), true);
                        m_direct = true;
                    }
                }

            }
        }
        bool m_avh; public bool IsAvh { get { return m_avh; } }
        string m_city1; public string City1 { get { return m_city1; } }
        string m_city2; public string City2 { get { return m_city2; } }
        DateTime m_date = new DateTime(); public DateTime Date { get { return m_date; } }
        string m_airline="ALL"; 
        /// <summary>
        /// 为全部航班时返回"all"
        /// </summary>
        public string AirLine { get { return m_airline; } }
        bool m_direct; public bool Direct { get { return m_direct; } }

    }
}
