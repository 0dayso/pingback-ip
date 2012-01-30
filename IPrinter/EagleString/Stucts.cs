using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace EagleString
{
    public class Structs
    {
        /// <summary>
        /// �ָ���(���л�س�������+�س����س�+����)
        /// </summary>
        public static string[] SP_R_N = new string[] { "\r", "\n", "\r\n" ,"\n\r"};
        /// <summary>
        /// ���մ�ӡ�����б�
        /// </summary>
        public static string[] INSURANCE_LIST = new string[] {
            "001","005","007", "009","B01","B02","B03","B04","B05","B06","B07","B08","B09","B0A","B0B","B0C","B0D"
        };

        public static Hashtable INSURANCE_HASH = new Hashtable();
        /// <summary>
        /// �ָ���(���֡���.�����з�)
        /// </summary>
        /// <param name="num">1-num,num�������0</param>
        /// <param name="dot">Ϊtrue��Ϊ1.-num.</param>
        /// <param name="newline">Ϊtrue��Ϊ\n1-\nnum</param>
        /// <returns></returns>
        public static string[] SP_NUMBER(uint num, bool dot, bool newline)
        {
            string[] ret = new string[num];
            string a = (newline ? "\n" : "");
            string b = (dot ? "." : "");
            for (int i = 0; i < num; ++i)
            {
                string t = i.ToString();
                if (t.Length < 2) t = t.PadLeft(2, ' ');
                ret[i] = a + t + b;
            }
            return ret;
        }
        /// <summary>
        /// һ����Ʊ�࣬ע������һ����Ʊ�ĶԻ�����
        /// </summary>
        public class ETDZONEKEY
        {
            public string pnr;
            public string pat;
            public RtResult rtres;
            public PatResult patres;
            public LoginInfo li;
            /// <summary>
            /// ��PNR����Ʊ�۵Ĵ�����
            /// </summary>
            /// <param name="p"></param>
            /// <param name="f"></param>
            public ETDZONEKEY(string p, string f,LoginInfo l)
            {
                li = l;
                SET(p, f);
            }
            public void SET(string p, string f)
            {
                pnr = p;
                pat = f;
            }
            /// <summary>
            /// ����rtResult��patResult�Զ�����etdz��.ע��:�ж���sfcʱ������ѡ���
            /// </summary>
            public string CreateEtdzString()
            {
                try
                {
                    List<string> ls = new List<string>();
                    ls.Add("i");
                    ls.Add("rt" + pnr);


                    if (!string.IsNullOrEmpty(pat))//�ȼ���Ʊ����
                    {
                        if (patres.PATTYPE == PatResult.PAT_TYPE.PAT_A || patres.PATTYPE == PatResult.PAT_TYPE.PAT_IN)
                        {
                            if (patres.HAS_PAT_A)
                            {
                                string sfc = "";
                                if (patres.LS_SFC.Count > 1)//����ѡ���(���SFC)
                                {
                                    SfcForm dlg = new SfcForm(patres.STRING,patres.LS_SFC);
                                    if (dlg.ShowDialog() == DialogResult.OK)
                                    {
                                        sfc = dlg.SFCITEM;
                                    }
                                    else
                                    {
                                        throw new Exception("δѡ��SFC��");
                                    }
                                }
                                else
                                {
                                    sfc = "sfc:01";
                                }
                                ls.Add(pat);
                                ls.Add(sfc);
                            }
                            else
                            {
                                throw new Exception("PAT:Aû�з����������˼�");
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(patres.PAT_M))
                            {
                                ls.Add(pat);
                                ls.Add(patres.PAT_M.Replace("\r\n", "\n").Replace("\r", "\n"));
                            }
                            else
                            {
                                throw new Exception("PATʱ������δ֪����");
                            }
                        }
                    }
                    for (int i = 0; i < rtres.SEGMENG.Length; ++i)
                    {
                        ls.Add(Convert.ToString(i + 1 + rtres.PSGCOUNT) + "RR");                //������
                    }
                    ls.Add("XE" + Convert.ToString(rtres.PSGCOUNT + rtres.SEGMENG.Length + 3)); //ʱ����
                    string EI = "";
                    if (rtres.SEGMENG.Length == 0) throw new Exception("ȱʧ��Ч������");
                    int rebate = EagleFileIO.RebateOf(rtres.SEGMENG[0].Bunk, rtres.SEGMENG[0].Flight);
                    if (rebate >= 100)
                    {
                        EI = "";
                    }
                    else if (rebate >= 85)
                    {
                        EI = "EI:����ǩת";
                    }
                    else if (rebate >= 40)
                    {
                        EI = "EI:����ǩת����";
                    }
                    else
                    {
                        EI = "EI:����ǩת������Ʊ";
                    }
                    ls.Add(EI);

                    int printerno = EagleFileIO.EtdzPrinterNumber(li.b2b.lr.IpidUsingIsSameOffice());
                    if (printerno == 0)
                    {
                        throw new Exception("������ʹ�ö��ֲ�ͬ���ã���ϵͳδ¼�뵱ǰ���õĴ�Ʊ���ţ����ܳ�Ʊ��");
                    }
                    else
                    {
                        ls.Add("etdz:" + printerno.ToString());
                    }
                    return string.Join("~", ls.ToArray());
                }
                catch (Exception ex)
                {
                    throw new Exception ("Structs.ETDZONEKEY.CreateEtdzString : " + ex.Message);
                }
            }
        }
        /// <summary>
        /// �����Զ�����TPR�������Ϣ
        /// </summary>
        public class TPR_AUTO_INFO
        {

            public int IPID;
            public string OFFICE;
            public string COMMAND;
            public bool STAT;
            
            public void ToListViewItem(ListViewItem lvi)
            {
                lvi.Text = OFFICE;
                lvi.SubItems.Add(IPID.ToString());
                lvi.SubItems.Add(COMMAND);
                lvi.SubItems.Add(STAT ? "��" : "��");
            }
            public void FromListViewItem(ListViewItem lvi)
            {
                OFFICE = lvi.Text;
                IPID = int.Parse(lvi.SubItems[1].Text);
                COMMAND = lvi.SubItems[2].Text;
                STAT = (lvi.SubItems[3].Text == "��");
            }
        }
    }
    //MU2501  I MO22DEC  WUHPVG DK1   0830 0945
    /// <summary>
    /// ���л��Ľṹ��(һ���������Ϣ)
    /// </summary>
    [Serializable]
    public struct FLIGHT_SEGMENG
    {
        /// <summary>
        /// �����
        /// </summary>
        public string Flight;
        /// <summary>
        /// ��λ
        /// </summary>
        public char Bunk;
        /// <summary>
        /// ��������MO22DEC
        /// </summary>
        public DateTime Date;
        /// <summary>
        /// ���ж�
        /// </summary>
        public string Citypair;
        /// <summary>
        /// �ж�����
        /// </summary>
        public string Actioncode;
        /// <summary>
        /// ����
        /// </summary>
        public int Number;
        /// <summary>
        /// ���ʱ��
        /// </summary>
        public int Time;
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public int Time2;
        /// <summary>
        /// �Ӵ��г�ʼ���ṹ�����
        /// </summary>
        /// <param name="seg">(����PNR��Ĵ�)�磺"MU2501  I MO22DEC  WUHPVG DK1   0830 0945"������Trim</param>
        public void FromString(string seg)
        {
            string[] a = seg.Split(' ');
            List<string> ls = new List<string>();
            for (int i = 0; i < a.Length; ++i)
            {
                if (a[i] != "") ls.Add(a[i]);
            }
            Flight = ls[0];
            Bunk = ls[1][0];
            Date = BaseFunc.str2datetime(ls[2],true);
            Citypair = ls[3];
            Actioncode = ls[4].Substring(0, 2);
            Number = Convert.ToInt32(ls[4].Substring(2));
            Time = Convert.ToInt32(ls[5]);
            Time2 = Convert.ToInt32(ls[6]);
        }
        /// <summary>
        /// 01234567890123456789012345678901234567890123456789
        /// 8C8276 X   TU23DEC08XMNWUH RR19  1020 1130          E --T2
        /// MU5901 G   WE24DEC  KMGJHG RR6   1825 1915          E
        /// ZH9706 Y   TU06JAN09XFNSZXUN1  2215 2355          ES
        ///*MU6737 H1  FR13FEB  CTUPVGNO6  1340 1605          E --T1 OP-MU5408
        /// </summary>
        /// <param name="seg"></param>
        public void FromString2(string seg)
        {
            string[] a = seg.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            List<string> ls = new List<string>();
            for (int i = 0; i < a.Length; ++i)
            {
                if (a[i] != "") ls.Add(a[i]);
            }
            Flight = ls[0];
            Bunk = ls[1][0];
            try
            {
                if (ls[2].Length > 7)//����һ��
                {
                    Time = Convert.ToInt32(ls[4]);
                    //Time2 = Convert.ToInt32(ls[5]);
                    Time2 = Convert.ToInt32(ls[5].Substring(0,4));//modified by king

                    Date = BaseFunc.str2datetime(ls[2].Substring(0, 9), true);
                    Citypair = ls[2].Substring(9, 6);
                    Actioncode = ls[3].Substring(0, 2);
                    Number = Convert.ToInt32(ls[3].Substring(2));

                }
                else//����һ��
                {
                    Date = BaseFunc.str2datetime(ls[2], true);
                    if (ls[3].Length == 6)
                    {
                        Time = Convert.ToInt32(ls[5]);
                        //Time2 = Convert.ToInt32(ls[6]);
                        Time2 = Convert.ToInt32(ls[6].Substring(0, 4));//modified by king ����ɻ��ִ�ʱ���Ǵ����賿����ʾ�磺0040+1����ת������

                        Citypair = ls[3];
                        Actioncode = ls[4].Substring(0, 2);
                        Number = Convert.ToInt32(ls[4].Substring(2));
                    }
                    else if (ls[3].Length > 6)
                    {
                        Time = Convert.ToInt32(ls[4]);
                        //Time2 = Convert.ToInt32(ls[5]);
                        Time2 = Convert.ToInt32(ls[5].Substring(0,4));//modified by king

                        Citypair = ls[3].Substring(0, 6);
                        Actioncode = ls[3].Substring(6, 2);
                        Number = Convert.ToInt32(ls[3].Substring(8));
                    }

                }
            }
            catch
            {
                //��Ҫ���Ǻ���źͲ�λ��������Ϣ���Ժ���
            }
        }

        public string ToChineseString()
        {
            string ret = "";
            string n = "\r\n";
            ret += "  �����:" + Flight + n;
            ret += "    ��λ:" + Bunk.ToString() + n;
            ret += "  �˻���:" + Date.ToShortDateString() + n;
            ret += "��ɳ���:" + EagleFileIO.CityCnName(Citypair.Substring(0, 3)) + n;
            ret += "�������:" + EagleFileIO.CityCnName(Citypair.Substring(3)) + n;
            ret += "    ����:" + Number.ToString() + n;
            ret += "���ʱ��:" + Time.ToString("d4") + n;
            ret += "����ʱ��:" + Time2.ToString("d4") + n;

            return ret;
        }

        int WeightByBunk(char bunk)
        {
            switch (bunk)
            {
                case 'F':
                    return 40;
                case 'C':
                    return 30;
                default:
                    return 20;
            }
        }

        public void ToTextBoxArrayLikeReceipt(TextBox[] tb)
        {
            try
            {
                if (tb.Length != 14) return;
                tb[0].Text = EagleFileIO.CityCnName(Citypair.Substring(0,3));
                tb[1].Text = Citypair.Substring(0,3);
                tb[2].Text = Flight.Substring(0,2);
                tb[3].Text = Flight.Substring(2);
                tb[4].Text = Bunk.ToString();
                tb[5].Text = Date.ToString("ddMMMyy", egString.dtFormat).ToUpper();
                tb[6].Text = Time.ToString("d4");
                tb[7].Text = Actioncode;
                tb[8].Text = "Y" + EagleFileIO.RebateOf(Bunk, Flight).ToString();
                tb[9].Text = "";// DATE_YES.ToString("ddMMMyy", egString.dtFormat).ToUpper();
                tb[10].Text = "";// DATE_NO.ToString("ddMMMyy", egString.dtFormat).ToUpper();
                tb[11].Text = WeightByBunk(Bunk).ToString() + "K";
                tb[12].Text = EagleFileIO.CityCnName(Citypair.Substring(3));
                tb[13].Text = Citypair.Substring(3);
            }
            catch
            {
                throw new Exception("�����������ؼ�ʱ��������:FLIGHT_SEGMENT.ToTextBoxArrayLikeReceipt");
            }
        }
    }
    [Serializable]
    public class PRICE_CALCULATE
    {
        private int m_fare =0;
        private int m_build =0;
        private int m_fuel=0;
        public int FARE { get { return m_fare; } set { m_fare = value; } }
        public int BUILD { get { return m_build; } set { m_build = value; } }
        public int FUEL { get { return m_fuel; } set { m_fuel = value; } }
        public PRICE_CALCULATE(int fare, int build, int fuel)
        {
            m_fare = fare;
            m_build = build;
            m_fuel = fuel;
        }
        public int TOTAL { get { return m_fuel + m_build + m_fare; } }
        public int TAX_TOTAL { get { return m_build + m_fuel; } }

    }
    public enum CtiTypeEnum:byte
    {
        /// <summary>
        /// ����忨�ͺ������� Ĭ������
        /// </summary>
        EGPlug = 0,
        /// <summary>
        /// ���潻�����ͺ�������
        /// </summary>
        EGSwitch = 1,
        /// <summary>
        /// ����usbС���Ӻ�������
        /// </summary>
        EGUSB = 2
    }
}
