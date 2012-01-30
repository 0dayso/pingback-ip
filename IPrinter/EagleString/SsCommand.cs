using System;
using System.Collections.Generic;
using System.Text;

namespace EagleString
{
    /// <summary>
    /// 将SS或SD指令分解得到各个参数，方便传入IBE
    /// </summary>
    public class SsCommand
    {
        public SsCommand(AvResult avr, string sd)
        {
            string[] a = sd.ToLower().Split('\r');
            string s = a[0];
            s = egString.trim(s.Substring(2), "/: ");
            int id = int.Parse(s.Substring(0, 1));
            s = egString.trim(s.Substring(1), "/: ");
            for(int i=0;i<avr.si[id-1].fi.Length;i++)
            {
                m_flightno.Add(avr.si[id - 1].fi[i].info_Flight);
                m_bunk.Add(s.Substring(0, 1));
                m_date.Add(avr.FlightDate_DT);
                m_citypair.Add(avr.si[id - 1].fi[i].info_CityPair);
            }
            Init(sd);
        }
        public SsCommand(string ss)
        {
            Init(ss);
        }
        /// <summary>
        /// 一次性封口可用IBE直接调用，其中ssr指令直接加证件号，顺序和姓名顺序相同
        /// </summary>
        /// <param name="ss">ss:mu2501/l/22apr/wuhpvg/ll1\rnm1ce\rct123\tktl2300/./wuh128\rssr 123456\r@</param>
        private void Init(string ss)
        {
            string[] a = ss.ToLower().Split('\r');
            string s = a[0].Trim();
            
            for (int i = 1; i < a.Length; i++)
            {
                s = a[i].Trim();
                if (s.StartsWith("ss") && !s.StartsWith("ssr"))
                {
                    s = egString.trim(s.Substring(2), "/: ");
                    string[] b = s.Split('/');
                    m_flightno.Add( b[0]);
                    m_bunk.Add(b[1]);
                    m_date.Add(BaseFunc.str2datetime(b[2], true));
                    m_citypair.Add(b[3]);
                }
                else if (s.StartsWith("nm"))
                {
                    s = egString.trim(s.Substring(2), "/: ");
                    string[] nm = s.Split('1');
                    for (int j = 0; j < nm.Length; j++) m_names.Add(nm[j]);
                }
                else if (s.StartsWith("c"))
                {
                    s = egString.trim(s.Substring(1), "/: t");
                    m_ct = s;
                }
                else if (s.StartsWith("tktl"))
                {
                    s = egString.trim(s.Substring(4), "/: ");
                    string[] c = s.Split('/');
                    m_tkt = BaseFunc.str2datetime(c[1], true).AddHours(int.Parse(c[0]) / 100).AddMinutes(int.Parse(c[0]) % 100);
                }
                else if (s.StartsWith("ssr"))
                {
                    s = egString.trim(s.Substring(4), "/: ");
                    m_cards.Add(s);
                }
            }
            if (m_names.Count != m_cards.Count && m_cards.Count!=0)
            {
                throw new Exception("姓名数与证件数不一致！");
            }
            else if (m_cards.Count == 0)
            {
                for (int i = 0; i < m_names.Count; i++)
                {
                    m_cards.Add(i.ToString());
                }
            }

        }
        public List<string> m_citypair = new List<string>();
        public List<DateTime> m_date = new List<DateTime>();
        public List<string> m_flightno = new List<string>();
        public List<string> m_bunk = new List<string>();
        public List<string> m_names = new List<string>();
        public List<string> m_cards = new List<string>();
        public DateTime m_tkt = new DateTime();
        public string m_ct;
    }
}
