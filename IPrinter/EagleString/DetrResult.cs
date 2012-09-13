/*sample string
DETR:TN/784-3337051667,AIR/CZ
ISSUED BY:                           ORG/DST: ZUH/WUH                 BSP-D
E/R: 不得签转
TOUR CODE:
PASSENGER: 郭正雄
EXCH:                               CONJ TKT:
O FM:1ZUH CZ    6438  L 22DEC 1340 OK Y60        22DEC8/22DEC9 20K STATUS = B
          RL:V88KC   /BRMR3 CA
  TO: WUH
FC:
FARE:                      |FOP:
TAX:                       |OI:
TOTAL:                     |TKTN: 784-3337051667
*/

/*
DETR:TN/859-3337051670
ISSUED BY:                           ORG/DST: WUH/LJG                 BSP-D
E/R: 不得签转退票
TOUR CODE:
PASSENGER: 曾庆合
EXCH:                               CONJ TKT:
O FM:1WUH 8L    9936  N 24DEC 1330 OK Y30                      20K OPEN FOR USE
          RL:BHP2C   /P04EZ 1E
O TO:2KMG 8L    9955  E 24DEC 1830 OK Y40                      20K OPEN FOR USE
          RL:BHP2C   /P04EZ 1E
  TO: LJG
FC:
FARE:                      |FOP:
TAX:                       |OI:
TOTAL:                     |TKTN: 859-3337051670
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace EagleString
{
    public class DetrResult
    {
        private string m_string;
        /// <summary>
        /// detr指令返回的结果
        /// </summary>
        public string STRING
        {
            get
            {
                return m_string;
            }
        }
        public DetrResult(string str)
        {
            str = egString.trim(str.Trim(), '>');
            m_string = egString.trim(str);
            FromString(m_string);
        }
        public bool ISCHILD { get { return (m_passenger.IndexOf("CHD") > 0); } }
        public bool SUCCEED     { get { return m_succeed; } }
        public string DETR { get { return m_detr; } }
        public string ISSUEDBY { get { return m_issuedby; } }
        public string FROM { get { return m_from; } }
        public string TO { get { return m_to; } }
        public string TKTYPE { get { return m_tktype; } }
        public string EI { get { return m_ei; } }
        public string TOURCODE { get { return m_tourcode; } }
        public string PASSENGER { get { return m_passenger; } }
        public string EXCH { get { return m_exch; } }
        public string CONJ_TKT { get { return m_conj_tkt; } }
        public int SEG_COUNT { get { return m_nsegs; } }
        public List<SEG_DETR> LS_SEG_DETR { get { return m_ls_seg; } }
        public string FC { get { return m_fc; } }
        public string FOP { get { return m_fop; } }
        public string FARE { get { return m_fare; } }
        public string TAX { get { return m_tax; } }
        public string OI { get { return m_oi; } }
        /// <summary>
        /// 合计总票价
        /// </summary>
        public string TOTAL { get { return m_total; } }
        /// <summary>
        /// 电子客票号
        /// </summary>
        public string TKTN { get { return m_tktn; } }
        private bool m_succeed;
        private string m_detr;
        private string m_issuedby;
        private string m_from;
        private string m_to;
        private string m_tktype;
        private string m_ei;
        private string m_tourcode;
        private string m_passenger;
        private string m_exch;
        private string m_conj_tkt;
        private int m_nsegs;
        private List<SEG_DETR> m_ls_seg = new List<SEG_DETR>();
        private string m_fc;
        private string m_fare;
        private string m_fop;
        private string m_tax;
        private string m_oi;
        private string m_total;
        private string m_tktn;
        private bool m_printed = false;
        private void FromString(string m)
        {
            m = m.Replace("\r", "\n");
            string[] lines = m.Split(Structs.SP_R_N, StringSplitOptions.RemoveEmptyEntries);
            m_detr = lines[0];
            m_succeed = (m_detr.IndexOf("DETR") >= 0);
            if (!m_succeed) return;
            string t = "";
            string t2 = "";

            t = "ISSUED BY:";
            t2 = "ORG/DST:";
            m_issuedby = egString.Between2String(m, t, t2);

            t = t2;
            t2 = "/";
            m_from = egString.Between2String(m, t, t2);
            m_to = m.Substring(m.IndexOf(t2, m.IndexOf(t) + t.Length) + t2.Length, 3);

            t = m_from + "/" + m_to;
            t2 = "\n";
            m_tktype = egString.Between2String(m, t, t2);

            t = "E/R:";
            t2 = "\n";
            m_ei = egString.Between2String(m, t, t2);

            t = "TOUR CODE:";
            t2 = "\n";
            m_tourcode = egString.Between2String(m, t, t2);
            m_printed = (m_tourcode.Contains("RECEIPT PRINTED"));
            if(m_printed)m_tourcode = m_tourcode.Replace("RECEIPT PRINTED","").Trim();

            t = "PASSENGER:";
            t2 = "\n";
            m_passenger = egString.Between2String(m, t, t2);
            //---added by king
            if (string.IsNullOrEmpty(m_passenger))
                m_passenger = egString.Between2String(m, "PASSENGER: ", "   ");
            //---

            t = "EXCH:";
            t2 = "CONJ TKT:";
            m_exch = egString.Between2String(m, t, t2);

            t = t2;
            t2 = "\n";
            m_conj_tkt = egString.Between2String(m, t, t2);


            m_nsegs = 0;
            t = "TO:";
            for (int i = 0; (i = m.IndexOf(t, i)) >= 0; i++)
            {
                m_nsegs++;
            }
            string[] a = m.Split(new string[] { "FM:", "TO:" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < m_nsegs; ++i)
            {
                SEG_DETR seg = new SEG_DETR();
                seg.FromString(a[i+1] + a[i+2].Substring(0, 4));
                m_ls_seg.Add(seg);
            }
            t = "FC:";
            t2 = "FARE:";
            m_fc = egString.Between2String(m, t, t2);

            t = t2;
            t2 = "|FOP:";
            m_fare = egString.Between2String(m, t, t2);

            t = t2;
            t2 = "TAX:";
            m_fop = egString.Between2String(m, t, t2);

            t = t2;
            t2 = "|OI:";
            m_tax = egString.Between2String(m, t, t2);

            t = t2;
            t2 = "TOTAL:";
            m_oi = egString.Between2String(m, t, t2);

            t = t2;
            t2 = "|TKTN:";
            m_total = egString.Between2String(m, t, t2);

            m_tktn = m.Substring(m.IndexOf(t2) + t2.Length).Trim();

        }



        /// <summary>
        /// detr结果中的航段信息
        /// </summary>
        public struct SEG_DETR
        {
            /// <summary>
            /// 该航段的出发城市三字码
            /// </summary>
            public string FROM;
            /// <summary>
            /// 该航段的到过城市三字码
            /// </summary>
            public string TO;
            /// <summary>
            /// 航空公司二字
            /// </summary>
            public string AIRLINE;
            /// <summary>
            /// 航班号数字
            /// </summary>
            public string NUMBER;
            /// <summary>
            /// 舱位
            /// </summary>
            public char BUNK;
            /// <summary>
            /// 乘机日
            /// </summary>
            public DateTime DATE;
            /// <summary>
            /// 离港时间
            /// </summary>
            public int TIME;
            /// <summary>
            /// 行动代码，如OK
            /// </summary>
            public string ACTIONCODE;
            /// <summary>
            /// 票价级别，如Y30
            /// </summary>
            public string TKTCLASS;
            /// <summary>
            /// 生效日期
            /// </summary>
            public DateTime DATE_YES;
            /// <summary>
            /// 失效日期
            /// </summary>
            public DateTime DATE_NO;
            /// <summary>
            /// 行李重量上限
            /// </summary>
            public string WEIGHT;
            /// <summary>
            /// 票状态
            /// </summary>
            public string STATE;
            /// <summary>
            /// 小编码
            /// </summary>
            public string PNR;
            /// <summary>
            /// 大编码
            /// </summary>
            public string PNRBIG;

            /// <summary>
            /// 1TAO CZ    OPEN  X OPEN          Y40        19DEC8/19DEC9 20K REFUNDED
            ///           RL:
            ///  WUH

            /// 
            /// 
            /// 1YIH HU    7110  Y 25DEC 1700 OK Y                        20K USED/FLOWN

            /// 1WUH 8L    9936  N 24DEC 1330 OK Y30                      20K OPEN FOR USE
            ///           RL:BHP2C   /P04EZ 1E
            /// O  KMG
            /// 
            /// 
            /// 1ZUH CZ    6438  L 22DEC 1340 OK Y60        22DEC8/22DEC9 20K STATUS = B
            ///           RL:V88KC   /BRMR3 CA
            ///  WUH
            /// </summary>
            /// <param name="m"></param>
            public void FromString(string m)
            {
                FROM = m.Substring(1, 3);
                AIRLINE = m.Substring(5, 2);
                NUMBER = m.Substring(11, 4).Trim();
                BUNK = m.Substring(17)[0];
                try
                {
                    string dtstring = m.Substring(19, 5).Trim();
                    if(dtstring!="OPEN")
                        DATE = BaseFunc.str2datetime(m.Substring(19, 5), true);
                }
                catch
                {
                }
                try
                {
                    TIME = Convert.ToInt32(m.Substring(25, 4));
                }
                catch
                {
                }
                ACTIONCODE = m.Substring(30, 2);
                TKTCLASS = m.Substring(33, 3).Trim();
                string t = m.Substring(37, 21).Trim();//string t = m.Substring(36, 21).Trim();//2009.12.08发现bug：m值变成“5       30JAN0/30JAN0”，前面多截取了一个数字 by king
                string[] a = t.Split('/');
                if (a.Length == 2)
                {
                    //发现一个票号提取后得字符串：1TNA SC    1187  E 19NOV 1110 OK E/TNA11N94 19NOV1/19NOV1 20K OPEN FOR USE
                    //导致上面的变量 t 取值不正确
                    //考虑到以下仅为行程单的【EagleString\DetrResult.cs/SetControlsByDetrResult/ToTextBoxArrayLikeReceipt】方法服务，故注释掉以防出错 by king
                    //DATE_YES = BaseFunc.str2datetime(a[0], false);
                    //DATE_NO = BaseFunc.str2datetime(a[1], true);
                }
                WEIGHT = m.Substring(58, 3);
                a = m.Split(Structs.SP_R_N, StringSplitOptions.RemoveEmptyEntries);
                STATE = a[0].Substring(62);
                try
                {
                    string codes = egString.Between2String(m, "RL:", "\n");
                    string[] b = codes.Split('/');

                    string code1 = b[0].Trim();
                    string code2 = b[1].Trim();

                    string CA = "";
                    if (code2.Length > 5) CA = egString.substring(b[1], b[1].Length - 2, 2);
                    if (CA == "CA")
                    {
                        PNR = code1;
                        PNRBIG = code2;
                    }
                    else if (CA == "1E")
                    {
                        PNR = code2;
                        PNRBIG = code1;
                    }
                    else
                    {
                        PNR = PNRBIG = code1;
                    }
                }
                catch
                {
                    PNR = PNRBIG = "";
                }
                if (PNR.Length > 5) PNR = PNR.Substring(0, 5);
                if (PNRBIG.Length > 5) PNRBIG = PNRBIG.Substring(0, 5);
                TO = a[a.Length - 1].Substring(1).Trim();
            }

            public void ToTextBoxArrayLikeReceipt(System.Windows.Forms.TextBox[] tb)
            {
                try
                {
                    if (tb.Length != 14) return;
                    tb[0].Text = EagleFileIO.CityCnName(FROM);
                    tb[1].Text = FROM;
                    tb[2].Text = AIRLINE;
                    tb[3].Text = NUMBER;
                    tb[4].Text = BUNK.ToString();
                    tb[5].Text = DATE.ToString("ddMMMyy", egString.dtFormat).ToUpper();
                    tb[6].Text = TIME.ToString("d4");
                    tb[7].Text = ACTIONCODE;
                    tb[8].Text = TKTCLASS;
                    if (DATE_YES < new DateTime(2009, 1, 1))
                        tb[9].Text = "";
                    else
                        tb[9].Text = DATE_YES.ToString("ddMMMyy", egString.dtFormat).ToUpper();
                    if (DATE_NO < new DateTime(2009, 1, 1))
                        tb[10].Text = "";
                    else
                        tb[10].Text = DATE_NO.ToString("ddMMMyy", egString.dtFormat).ToUpper();
                    tb[11].Text = WEIGHT;
                    tb[12].Text = EagleFileIO.CityCnName(TO);
                    tb[13].Text = TO;
                }
                catch
                {
                    throw new Exception("将变量赋给控件时发生错误:ToTextBoxArrayLikeReceipt");
                }
            }
        }
    }
}
