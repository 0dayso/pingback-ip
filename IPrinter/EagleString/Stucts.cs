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
        /// 分隔符(换行或回车、换行+回车、回车+换行)
        /// </summary>
        public static string[] SP_R_N = new string[] { "\r", "\n", "\r\n" ,"\n\r"};
        /// <summary>
        /// 保险打印功能列表
        /// </summary>
        public static string[] INSURANCE_LIST = new string[] {
            "001","005","007", "009","B01","B02","B03","B04","B05","B06","B07","B08","B09","B0A","B0B","B0C","B0D"
        };

        public static Hashtable INSURANCE_HASH = new Hashtable();
        /// <summary>
        /// 分隔符(数字、点.、换行符)
        /// </summary>
        /// <param name="num">1-num,num必须大于0</param>
        /// <param name="dot">为true则为1.-num.</param>
        /// <param name="newline">为true则为\n1-\nnum</param>
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
        /// 一键出票类，注意区别一键出票的对话框类
        /// </summary>
        public class ETDZONEKEY
        {
            public string pnr;
            public string pat;
            public RtResult rtres;
            public PatResult patres;
            public LoginInfo li;
            /// <summary>
            /// 用PNR与做票价的串构造
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
            /// 利用rtResult和patResult自动生成etdz串.注意:有多项sfc时，弹出选择框
            /// </summary>
            public string CreateEtdzString()
            {
                try
                {
                    List<string> ls = new List<string>();
                    ls.Add("i");
                    ls.Add("rt" + pnr);


                    if (!string.IsNullOrEmpty(pat))//先加入票价组
                    {
                        if (patres.PATTYPE == PatResult.PAT_TYPE.PAT_A || patres.PATTYPE == PatResult.PAT_TYPE.PAT_IN)
                        {
                            if (patres.HAS_PAT_A)
                            {
                                string sfc = "";
                                if (patres.LS_SFC.Count > 1)//弹出选择框(多个SFC)
                                {
                                    SfcForm dlg = new SfcForm(patres.STRING,patres.LS_SFC);
                                    if (dlg.ShowDialog() == DialogResult.OK)
                                    {
                                        sfc = dlg.SFCITEM;
                                    }
                                    else
                                    {
                                        throw new Exception("未选择SFC项");
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
                                throw new Exception("PAT:A没有符合条件的运价");
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
                                throw new Exception("PAT时发生了未知错误");
                            }
                        }
                    }
                    for (int i = 0; i < rtres.SEGMENG.Length; ++i)
                    {
                        ls.Add(Convert.ToString(i + 1 + rtres.PSGCOUNT) + "RR");                //航段项
                    }
                    ls.Add("XE" + Convert.ToString(rtres.PSGCOUNT + rtres.SEGMENG.Length + 3)); //时限项
                    string EI = "";
                    if (rtres.SEGMENG.Length == 0) throw new Exception("缺失有效航段组");
                    int rebate = EagleFileIO.RebateOf(rtres.SEGMENG[0].Bunk, rtres.SEGMENG[0].Flight);
                    if (rebate >= 100)
                    {
                        EI = "";
                    }
                    else if (rebate >= 85)
                    {
                        EI = "EI:不得签转";
                    }
                    else if (rebate >= 40)
                    {
                        EI = "EI:不得签转更改";
                    }
                    else
                    {
                        EI = "EI:不得签转更改退票";
                    }
                    ls.Add(EI);

                    int printerno = EagleFileIO.EtdzPrinterNumber(li.b2b.lr.IpidUsingIsSameOffice());
                    if (printerno == 0)
                    {
                        throw new Exception("您可能使用多种不同配置，或系统未录入当前配置的打票机号！不能出票！");
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
        /// 在做自动导入TPR报表的信息
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
                lvi.SubItems.Add(STAT ? "是" : "否");
            }
            public void FromListViewItem(ListViewItem lvi)
            {
                OFFICE = lvi.Text;
                IPID = int.Parse(lvi.SubItems[1].Text);
                COMMAND = lvi.SubItems[2].Text;
                STAT = (lvi.SubItems[3].Text == "是");
            }
        }
    }
    //MU2501  I MO22DEC  WUHPVG DK1   0830 0945
    /// <summary>
    /// 序列化的结构体(一个航班的信息)
    /// </summary>
    [Serializable]
    public struct FLIGHT_SEGMENG
    {
        /// <summary>
        /// 航班号
        /// </summary>
        public string Flight;
        /// <summary>
        /// 舱位
        /// </summary>
        public char Bunk;
        /// <summary>
        /// 航班日期MO22DEC
        /// </summary>
        public DateTime Date;
        /// <summary>
        /// 城市对
        /// </summary>
        public string Citypair;
        /// <summary>
        /// 行动代码
        /// </summary>
        public string Actioncode;
        /// <summary>
        /// 数量
        /// </summary>
        public int Number;
        /// <summary>
        /// 起飞时间
        /// </summary>
        public int Time;
        /// <summary>
        /// 到达时间
        /// </summary>
        public int Time2;
        /// <summary>
        /// 从串中初始化结构体变量
        /// </summary>
        /// <param name="seg">(生成PNR后的串)如："MU2501  I MO22DEC  WUHPVG DK1   0830 0945"，必须Trim</param>
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
                if (ls[2].Length > 7)//上面一种
                {
                    Time = Convert.ToInt32(ls[4]);
                    //Time2 = Convert.ToInt32(ls[5]);
                    Time2 = Convert.ToInt32(ls[5].Substring(0,4));//modified by king

                    Date = BaseFunc.str2datetime(ls[2].Substring(0, 9), true);
                    Citypair = ls[2].Substring(9, 6);
                    Actioncode = ls[3].Substring(0, 2);
                    Number = Convert.ToInt32(ls[3].Substring(2));

                }
                else//下面一种
                {
                    Date = BaseFunc.str2datetime(ls[2], true);
                    if (ls[3].Length == 6)
                    {
                        Time = Convert.ToInt32(ls[5]);
                        //Time2 = Convert.ToInt32(ls[6]);
                        Time2 = Convert.ToInt32(ls[6].Substring(0, 4));//modified by king 如果飞机抵达时间是次日凌晨，表示如：0040+1，将转换出错

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
                //重要的是航班号和舱位，其它信息可以忽略
            }
        }

        public string ToChineseString()
        {
            string ret = "";
            string n = "\r\n";
            ret += "  航班号:" + Flight + n;
            ret += "    舱位:" + Bunk.ToString() + n;
            ret += "  乘机日:" + Date.ToShortDateString() + n;
            ret += "起飞城市:" + EagleFileIO.CityCnName(Citypair.Substring(0, 3)) + n;
            ret += "到达城市:" + EagleFileIO.CityCnName(Citypair.Substring(3)) + n;
            ret += "    人数:" + Number.ToString() + n;
            ret += "起飞时间:" + Time.ToString("d4") + n;
            ret += "到达时间:" + Time2.ToString("d4") + n;

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
                throw new Exception("将变量赋给控件时发生错误:FLIGHT_SEGMENT.ToTextBoxArrayLikeReceipt");
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
        /// 兰奇板卡型呼叫中心 默认类型
        /// </summary>
        EGPlug = 0,
        /// <summary>
        /// 兰奇交换机型呼叫中心
        /// </summary>
        EGSwitch = 1,
        /// <summary>
        /// 兰奇usb小盒子呼叫中心
        /// </summary>
        EGUSB = 2
    }
}
