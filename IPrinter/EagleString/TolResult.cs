using System;
using System.Collections.Generic;
using System.Text;

namespace EagleString
{
    public class TolResult
    {
        public string TXT { get { return m_txt; } }
        public bool SUCCEED { get { return m_succeed; } }
        public string OFFICE { get { return m_office; } }
        string m_txt;
        bool m_succeed;
        string m_office;
        public List<long> ls_tktArrangeStart = new List<long>();
        public List<long> ls_tktArrangeEnd = new List<long>();
        public TolResult(string t)
        {
            t = egString.trim(t.Trim());
            m_txt = t;
            m_succeed = (t.IndexOf("TICKET STORE/USE  REPORT") > 0);
            string[] a = t.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            m_office = egString.Between2String(t, "OFFICE :", "IATA NO.:");
            for (int i = 0; i < a.Length; ++i)
            {
                string temp = a[i];
                string[] b = temp.Split('-');
                if (b.Length == 3)
                {
                    if (b[0].Length == 3 && b[1].Length == 7 && b[2].Length == 7)
                    {
                        long start = Convert.ToInt64(b[0] + b[1]);
                        long end = Convert.ToInt64(b[0] + b[2]);
                        ls_tktArrangeStart.Add(start);
                        ls_tktArrangeEnd.Add(end);
                    }
                }
            }
        }
    }
}
/*
>********************************************************************************
*                       TICKET STORE/USE  REPORT                               *
*   AGENT  :   39076                             AIRLINE : BSP                 *
*   OFFICE :  WUH169                             IATA NO.: 08025194            *
*   DATE   : 16JAN09                             TIME   : 10:29                *
--------------------------------------------------------------------------------
        Form First     Last        Granted  Granted By  Granted  Ticket Allo
 Office Code TKT no.    TKT    Qua  By OFF   Agent/Pid    Date   T/Tp/M Tp/ST   
------------------------------------------------------------------------------- 
 System       Ticket Range     Qua  Start/End Date  Office   Agent/Pid  Dev Use 
------------------------------------------------------------------------------- 
 WUH169  334-2749096-2749215    120 BJS636    9940/  711 08JAN09 D/DC/E TK/UO  +
>pn
>CRS      2749096-2749215    120 09JAN09/10JAN09   WUH169 39076/47984   2 TK-
         334-3710136-3710395    260 BJS636    9940/  711 12JAN09 D/DC/E TK/UO   
    CRS      3710136-3710395    260 12JAN09/14JAN09   WUH169 39076/47984   2 TK 
         334-5022746-5022865    120 BJS636    9940/  711 15JAN09 D/DC/E TK/IU   
    CRS      5022746-5022865    120 16JAN09/          WUH169 39076/47984   2
 WUH169  END
*=============================================================================* 
TOTAL TICKETS :        500   TOTAL IN USE:        500   TOTAL STORE:          0 
*   
>
*/