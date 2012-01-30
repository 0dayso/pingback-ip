using System;
using System.Collections.Generic;
using System.Text;

namespace EagleString
{
    public class XepnrResult
    {
        string m_txt;
        public bool SUCCEED
        {
            get
            {
                return (m_txt.ToUpper().IndexOf("PNR CANCELLED") >= 0);
            }
        }
        public string PNR
        {
            get
            {
                try
                {
                    string t = "PNR CANCELLED";
                    return m_txt.Substring(m_txt.IndexOf(t) + t.Length);
                }
                catch
                {
                    return "";
                }
            }
        }
        public XepnrResult(string str)
        {
            m_txt = egString.trim(str);
            m_txt = egString.trim(m_txt, '>');
        }
    }
}
