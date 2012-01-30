/*SAMPLE TEXT
NAME: ÔøÑÇÕä   TKTN:7843343173815   
RCPT:   
  1         RP4042707182
  2         NI420107198204251026

*/
using System;
using System.Collections.Generic;
using System.Text;

namespace EagleString
{
    public class DetrFResult
    {
        string m_txt;
        string m_receiptno = "";
        string m_cardno = "";
        string m_name = "";
        string m_tktn = "";
        public string RECEIPTNO{get { return m_receiptno; }}
        public string CARDNO { get { return m_cardno; } }
        public string NAME { get { return m_name; } }
        public string TKTN { get { return m_tktn; } }
        public DetrFResult(string txt)
        {
            txt = egString.trim(txt.Trim(),'>').Replace("\r","\n")+"\n";
            m_txt = txt;
            try
            {
                string temp = m_txt.Split(new string[] { "RCPT:" }, StringSplitOptions.RemoveEmptyEntries)[1];
                m_tktn = egString.Between2String(txt, "TKTN:", "\n");
                m_receiptno = egString.Between2String(temp, "RP", "\n");
                m_cardno = egString.Between2String(temp, "NI", "\n");
                m_name = egString.Between2String(txt, "NAME:", "TKTN:");
            }
            catch
            {
            }

        }
    }
}
