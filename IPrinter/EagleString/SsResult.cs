using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
namespace EagleString
{
    [Serializable]
    public class SsResult
    {
        public DateTime CreateDate;
        public List<FLIGHT_SEGMENG> SEGS ;
        private string m_pnr;
        private string m_result;
        private bool m_succeed = true;
        /// <summary>
        /// The PNR
        /// </summary>
        public string PNR{get{return m_pnr;}}
        /// <summary>
        /// Indicate that Create Pnr Successfully or Not.
        /// </summary>
        public bool SUCCEED{get{return m_succeed;}}
        public string STRING{get{return m_result;}}
        public string CHINESESTRING
        {
            get
            {
                string show = "";
                for (int i = 0; i < SEGS.Count; ++i)
                {
                    show += SEGS[i].ToChineseString();
                }
                return show;
            }
        }

        public SsResult(string ssres)
        {
            SEGS = new List<FLIGHT_SEGMENG>();
            m_result = ssres;
            m_succeed = true;
            //todo: init segs , m_pnr
            string all = egString.trim(ssres);
            all = egString.trim(all, '>');
            string[] a = all.Split(Structs.SP_R_N, StringSplitOptions.RemoveEmptyEntries);
            List<string> ls = new List<string>();
            foreach (string s in a)
            {
                if (s != "") ls.Add(s);
            }
            if(ls.Count<2)
            {
                m_succeed = false;
                return;//小于2行，表示生成PNR失败
            }
            foreach (string s in ls)
            {
                string t = egString.trim(s);
                if (t.IndexOf("-EOT SUCCESSFUL") > 0)
                {
                    m_pnr = t.Substring(0, 5);
                    continue;
                }
                else if (t.Length == 5)
                {
                    m_pnr = t;
                    continue;
                }
                else if (t.IndexOf("航空公司") > 0)
                {
                    m_pnr = t.Substring(0, 5);
                    continue;
                }
                else if (t.IndexOf("航空公司") == 0)
                {
                    continue;
                }
                else
                {
                    FLIGHT_SEGMENG seg = new FLIGHT_SEGMENG();
                    seg.FromString(t);
                    SEGS.Add(seg);
                }
            }
        }
    }
    [Serializable]
    public class SsResultList
    {
        public List<SsResult> ls = new List<SsResult>();
        public void SerializeSsResults()
        {
            using (FileStream fs = new FileStream(Application.StartupPath + "\\CreatedPnr.txt", FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, this);
            }

        }
        public static SsResultList DeSerializeSsResults()
        {
            try
            {
                using (FileStream fs = new FileStream(Application.StartupPath + "\\CreatedPnr.txt", FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (SsResultList)(formatter.Deserialize(fs));
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
/*
  MU2501  I MO22DEC  WUHPVG DK1   0830 0945
TN06T -   航空公司使用自动出票时限, 请检查PNR
*/
/*

  FM9450  Z MO22DEC  WUHPVG DW1   1100 1230
TN0CW
*/
/*
QGG4V -EOT SUCCESSFUL, BUT ASR UNUSED FOR 1 OR MORE SEGMENTS
ZH9897  Y FR22DEC  WUHSHE DK1   2010 2220 
航空公司使用自动出票时限, 请检查PNR
*/