using System;
using System.Collections.Generic;
using System.Text;

namespace EagleString
{
    /// <summary>
    /// Etdz�������
    /// </summary>
    public class EtdzResult
    {
        string m_txt;
        bool m_succ;
        int total;
        List<string> tktnos = new List<string> ();
        string m_pnr; public string Pnr { get { return m_pnr; } }
        /// <summary>
        /// Դ�ı�
        /// </summary>
        public string STRING { get { return m_txt; } }
        /// <summary>
        /// �Ƿ��Ʊ�ɹ�
        /// </summary>
        public bool SUCCEED { get { return m_succ; } }
        /// <summary>
        /// �ܽ��
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
                        throw new Exception("STOCK : Ʊ��Ϊ�գ�����Ʊ��");
                    case "DEVICE":
                        throw new Exception("DEVICE : ��Ʊ���Ŵ���");
                    case "NO PNR":
                        throw new Exception("NO PNR : δ֪����������");
                    case "FORMAT":
                        throw new Exception("FORMAT : ��ʽ����");
                    case "FUNCTION":
                        throw new Exception("FUNCTION : δָ֪��");
                    case "INCOMPLETE PNR/FN":
                        throw new Exception("INCOMPLETE PNR/FN : ��������Ʊ����");
                    case "MANUAL":
                        throw new Exception("MANUAL : ����EI������������");
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
 * ������ִ��ETDZָ��֮��,ϵͳ���Ȼ᷵�ؽ���CRS PNR��¼���,Ȼ�����"ET PROCESSING...PLEASE WAIT!"����ʾ,���ճ�Ʊ�ɹ���ϵͳ������Ϣ��ʾ"ELECTRONIC TICKET ISSUED". ���ӿ�Ʊ��Ʊ�ɹ��ı�־��"ELECTRONIC TICKET ISSUED"
*/