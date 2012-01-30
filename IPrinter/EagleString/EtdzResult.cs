using System;
using System.Collections.Generic;
using System.Text;

namespace EagleString
{
    /// <summary>
    /// Etdz结果处理
    /// </summary>
    public class EtdzResult
    {
        string m_txt;
        bool m_succ;
        int total;
        List<string> tktnos = new List<string> ();
        string m_pnr; public string Pnr { get { return m_pnr; } }
        /// <summary>
        /// 源文本
        /// </summary>
        public string STRING { get { return m_txt; } }
        /// <summary>
        /// 是否出票成功
        /// </summary>
        public bool SUCCEED { get { return m_succ; } }
        /// <summary>
        /// 总金额
        /// </summary>
        public int TOTAL { get { return total; } }
        public EtdzResult(string resText)
        {
            resText = egString.trim(resText.Trim(), '>').Trim();
            m_txt = resText;

            if (m_txt.IndexOf("CNY") == 0 || 
                m_txt.IndexOf("ELECTRONIC TICKET ISSUED") >= 0 ||
                m_txt.IndexOf("ET PROCESSING...  PLEASE WAIT!")>=0)
            {
                m_succ = true;
            }
            if (m_succ)
            {
                if (m_txt.IndexOf("CNY") == 0)
                {
                    total = Convert.ToInt32(egString.Between2String(m_txt, "CNY", "."));
                    string temp = m_txt.Replace("\r", " ").Replace("\n", " ");
                    string[] a = temp.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    int index = 1;
                    if (BaseFunc.PnrValidate(a[1])) m_pnr = a[1];
                    else if (BaseFunc.PnrValidate(a[2]))
                    {
                        m_pnr = a[2];
                        index = 2;
                    }
                    try
                    {
                        for (int i = index + 1; i < a.Length; i++)
                        {
                            string t="";
                            if (BaseFunc.TicketNumberValidate(a[i], ref t))
                            {
                                tktnos.Add(a[i]);
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                else total = 0;
            }
            else
            {
                total = -1;
                switch (m_txt)
                {
                    case "STOCK":
                        throw new Exception("STOCK : 票池为空，请上票号");
                    case "DEVICE":
                        throw new Exception("DEVICE : 打票机号错误");
                    case "NO PNR":
                        throw new Exception("NO PNR : 未知错误，请重试");
                    case "FORMAT":
                        throw new Exception("FORMAT : 格式错误");
                    case "FUNCTION":
                        throw new Exception("FUNCTION : 未知指令");
                    case "INCOMPLETE PNR/FN":
                        throw new Exception("INCOMPLETE PNR/FN : 请先输入票价组");
                    case "MANUAL":
                        throw new Exception("MANUAL : 可能EI项或其它项过长");
                }
            }
        }
    }
}
/*SAMPLE TEXTS
 * 1.STOCK
 * 2.DEVICE
 * 3.NO PNR
 * 4.FORMAT
 * 5.FUNCTION
 * 6.CNY760.00       PB3ZS
 *   CNY3700.00      XBWEP
 * 7.ET PROCESSING...  PLEASE WAIT!
 * 8.MELECTRONIC TICKET ISSUED
 * 9.INCOMPLETE PNR/FN
 * 10.Manual
 * 代理人执行ETDZ指令之后,系统首先会返回金额和CRS PNR记录编号,然后出现"ET PROCESSING...PLEASE WAIT!"的提示,最终出票成功后系统返回信息提示"ELECTRONIC TICKET ISSUED". 电子客票出票成功的标志是"ELECTRONIC TICKET ISSUED"
*/