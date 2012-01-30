using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
namespace EagleString
{
    public enum PNR_FLAG
    {
        /// <summary>
        /// 一般PNR.
        /// </summary>
        NORMAL,
        /// <summary>
        /// 带有已出电子客票标识的PNR.
        /// </summary>
        ETICKET,
        /// <summary>
        /// 带有已取消标识的PNR.
        /// </summary>
        CANCELLED,
        /// <summary>
        /// 带有已MARRIED标识的PNR.
        /// </summary>
        MARRIED
    }
    [Serializable]
    public class RtResult
    {

        public DateTime SubmittdDate = new DateTime();

        public PNR_FLAG FLAG_OF_PNR { get {
            if (TXT.IndexOf(etstring) >= 0) return PNR_FLAG.ETICKET;
            else if (TXT.IndexOf(xestring) >= 0) return PNR_FLAG.CANCELLED;
            else if (TXT.IndexOf(marriedstring) >= 0) return PNR_FLAG.MARRIED;
            else return PNR_FLAG.NORMAL;
        } }
        public string PNR { get { return m_pnr; } }
        /// <summary>
        /// 原文本
        /// </summary>
        
        public string TXT { get { return m_rtstring; } }
        /// <summary>
        /// 有效航段组，即K状态的航段
        /// </summary>
        public FLIGHT_SEGMENG[] SEGMENG { get { return m_seg; } }
        /// <summary>
        /// 总人数
        /// </summary>
        
        public int PSGCOUNT { get { return m_psg_count; } }
        /// <summary>
        /// 证件号码组
        /// </summary>
        public string[] CARDID { get { return m_cards; } }
        public string[] NAMES { get { return m_names; } set { m_names = value; } }

        public void CardIdSet(DetrFResult dr)
        {
            if(m_cards==null)m_cards = new string [m_psg_count];
            for(int i=0;i<m_names.Length;++i)
            {
                if (m_names[i] == dr.NAME)
                {
                    m_cards[i] = dr.CARDNO;
                }
            }
        }
        /// <summary>
        /// 用"-"分隔的姓名-证件号的数组
        /// </summary>
        public string[] Name_CARDS
        {
            get
            {
            string[] ret = new string[m_psg_count];
            for (int i = 0; i < m_psg_count; ++i)
            {
                try
                {
                    ret[i] = m_names[i] + "-" + m_cards[i];
                }
                catch
                {
                    ret[i] = m_names[i] + "-";
                }
            }
            return ret;
        } }
        /// <summary>
        /// 取航班数组
        /// </summary>
        public string[] FLIGHTS { get {
            string[] ret = new string[m_seg.Length];
            for (int i = 0; i < m_seg.Length; ++i)
            {
                ret[i] = m_seg[i].Flight;
            }
            return ret;
        } }
        /// <summary>
        /// 取舱位数组
        /// </summary>
        public char[] BUNKS { get {
            char[] ret = new char[m_seg.Length];
            for (int i = 0; i < m_seg.Length; ++i)
            {
                ret[i] = m_seg[i].Bunk;
            }
            return ret;
        } }
        /// <summary>
        /// 取有效航班日期数组
        /// </summary>
        public DateTime[] FLIGHTDATES { get {
            DateTime[] ret = new DateTime[m_seg.Length];
            for (int i = 0; i < m_seg.Length; ++i)
            {
                ret[i] = m_seg[i].Date;
            }
            return ret;
        } }
        /// <summary>
        /// 取航班的城市对数组
        /// </summary>
        public string[] CITYPAIRS { get {
            string[] ret = new string[m_seg.Length];
            for (int i = 0; i < m_seg.Length; ++i)
            {
                ret[i] = m_seg[i].Citypair;
            }
            return ret;
        } }

        public string EI { get { return m_ei; } }
        public string[] TKTNO { get { return m_tktnos; } set { m_tktnos = value; } }
        public string TKTNO4Name(string name)
        {
            for (int i = 0; i < m_names.Length; ++i)
            {
                if (name == m_names[i])
                {
                    return m_tktnos[i];
                }
            }
            return "";
        }
        public string TKTNO4Card(string card)
        {
            for (int i = 0; i < m_cards.Length; ++i)
            {
                if (card == m_cards[i])
                {
                    return m_tktnos[i];
                }
            }
            return "";
        }
        public PRICE_CALCULATE PRICE_CAL { get { return m_price; } }
            private string m_rtstring = "";
        private string m_pnr = "";
        private bool m_succeed;
        private bool m_et_flag;
        private string[] m_names;
        private string[] m_cards;
        private DateTime m_time_dz;
        private string m_city_dz;
        private bool[] m_tkt_stat;
        private string m_pnr_big;
        private bool m_auto_fare_flat;
        private int m_psg_count;
        private FLIGHT_SEGMENG[] m_seg;
        private PRICE_CALCULATE m_price;//主要取机建(航段信息中不包含机型)，票面价可由城市对取，并按成人价格，具体所有人总价要重新计算
        private string[] m_tktnos;
        private string m_fp;
        private string m_ei = "";
        private string m_office;
        private bool m_grouptkt;
        private int m_baby_count;

        private string chars_remove = "+- \r\n";//加减空格换行回车
        string etstring = "**ELECTRONIC TICKET PNR**";
        string xestring = "THIS PNR WAS ENTIRELY CANCELLED";
        string marriedstring = "MARRIED SEGMENT EXIST IN THE PNR";
        public RtResult(string rtstring,string pnr)
        {
            rtstring = egString.trim(rtstring);
            rtstring = egString.trim(rtstring, '>');
            pnr = pnr.ToUpper();
            if (rtstring.IndexOf(xestring) >= 0) return;

            m_rtstring = egString.trim(rtstring);
            m_succeed = (m_rtstring.IndexOf(pnr) >= 0);
            m_succeed = (BaseFunc.PnrValidate(pnr)&&m_succeed);
            if (!m_succeed) return;
            m_pnr = pnr;



            string[] line = m_rtstring.Split(Structs.SP_R_N, StringSplitOptions.RemoveEmptyEntries);
            m_et_flag = (egString.trim(line[0]) == etstring);

            string sname = m_rtstring.Replace(etstring, "");//sname为 etstring或起始位置到pnr之间有串,替换掉回车，换行，加号，减号
            sname = sname.Replace(marriedstring, "");
            string sflight = sname;//保存替换后的值
            sname = egString.trim(sname.Substring(0, sname.IndexOf(m_pnr)));
            sname = sname.Replace("\r", " ").Replace("\n", " ");//maybe there are char of newline
            sname = sname.Replace("+", " ").Replace("-", " ");//there are +/- if multi pages 

            namesfrom(sname);

            //string temp = Convert.ToString(m_psg_count + 1) + ".";
            //string sflight = m_rtstring.Replace(etstring,"");//egString.trim(m_rtstring.Substring(m_rtstring.IndexOf(temp) + temp.Length));


            string[] sp = Structs.SP_NUMBER((uint)(m_psg_count + 999), true, true);

            line = sflight.Split(sp, StringSplitOptions.RemoveEmptyEntries);

            segfromlines(line);

            ssrfoidfromlines(line);
            SSR_TKNE_fromlines(line);//added by king 2009.12.8
            ssradtkfromlines(line);

            tktstatusfromlines(line);

            pnrbigfromlines(line);

            m_auto_fare_flat = (m_rtstring.IndexOf("RMK AUTOMATIC FARE QUOTE") > 0);

            farefromlines(line);

            //tkt no
            if (m_tktnos == null || string.IsNullOrEmpty(m_tktnos[0]))//若ssr组没有取到电子客票号
                tktnofromlines(line);
            //fp
            fpfromlines(line);
            //ei
            eifromlines(line);
            m_office = m_rtstring.Substring(m_rtstring.Length - 6);
            //if (!BaseFunc.OfficeValidate(m_office)) m_office = "";//commentted by king
        }

        public int CHILDRENCOUNT
        {
            get
            {
                int ret = 0;
                foreach (string s in m_names)
                {
                    if (egString.right(s, 3) == "CHD" || s.IndexOf("CHD(") > 0 || s.IndexOf("CHD (") > 0)
                    {
                        ++ret;
                    }
                }
                return ret;
            }
        }
        public int ADULTCOUNT { get { return m_psg_count - CHILDRENCOUNT; } }




        /// <summary>
        /// 取结果中的姓名,按显示序号有序放置,如:1.陈桂元 4.申小爽 2.陈金祥 3.吕德山 6.张俊 5.许炳山 (英文名会有空格，不能分隔)
        /// </summary>
        /// <param name="line"></param>
        private void namesfrom(string line)
        {
            line = egString.trim(line);
            try
            {
                m_psg_count = line.Split('.').Length - 1;
                m_names = new string[m_psg_count];
                m_cards = new string[m_psg_count];
                int id = -1;
                string num = "";
                string str = "";
                string sname = line + "0";
                bool bkuohao = false;
                for (int i = 0; i < sname.Length; ++i)
                {
                    char c = sname[i];
                    if (char.IsDigit(c) && !bkuohao)
                    {
                        if (id != -1 && str != "")
                        {
                            m_names[id - 1] = egString.trim(str);
                            id = -1;
                        }
                        str = "";
                        num += c.ToString();
                    }
                    else if (c == '.')//遇到.则保存序号,并清掉num。下次再遇到数字时则id(不为-1)和str(不为空)确定了一个NAME
                    {
                        id = Convert.ToInt32(num);
                        num = "";
                        if (id == 0)//团队票
                        {
                            groupnamesfrom();
                            m_grouptkt = true;
                            return;
                        }
                    }
                    else if (c == '(')
                    {
                        bkuohao = true;
                        str += c.ToString();
                    }
                    else if (c == ')')
                    {
                        bkuohao = false;
                        str += c.ToString();
                    }
                    else
                    {

                        str += c.ToString();
                        if (id == -1)//没有. (有异常，举报该中文字)
                        {
                            id = Convert.ToInt32(num);
                            System.Windows.Forms.MessageBox.Show
                                (string.Format("发现不能识别的中文字第{0}人，请将该乘客姓名email:clawclaw@163.com", id));
                        }
                    }
                }
                m_grouptkt = false;
            }
            catch(Exception ex)
            {
                throw new Exception("在RT结果中取姓名组出错:" + ex.Message);
            }
        }
        private void groupnamesfrom()
        {
            try
            {
                string sname = egString.trim(m_rtstring.Replace(etstring, ""));
                string groupinfo = egString.Between2String(sname, "0.", m_pnr);
                m_psg_count = Convert.ToInt32(egString.AfterString(groupinfo, "NM"));
                m_names = new string[m_psg_count];
                sname = egString.Between2String(sname, m_pnr, Convert.ToString(m_psg_count + 1) + ".");
                namesfrom(sname);
            }
            catch (Exception ex)
            {
                throw new Exception("在RT结果中取团队票姓名组出错:" + ex.Message);
            }
        }
        /// <summary>
        /// 取结果中有航段信息->m_seg
        /// </summary>
        /// <param name="line">是用数字.分割后的数组，并不是真正的按换行符分割的line</param>
        private void segfromlines(string[] line)
        {
            try
            {
                List<string> m_seg_string = new List<string>();
                for (int i = 0; i < line.Length; ++i)
                {
                    string l = line[i];
                    if (egString.trim(l) == "") continue;
                    if (l.Length > 2 && l[0] == ' ' && (l[1] == ' '||l[1]=='*'))
                    {
                        m_seg_string.Add(egString.trim(l, "+- \r\n"));
                        //MU5901 G   WE24DEC  KMGJHG RR6   1825 1915          E
                        //8C8276 X   TU23DEC08XMNWUH RR19  1020 1130          E
                    }
                    else
                    {
                        if (m_seg_string.Count > 0) break;
                    }
                }
                m_seg = new FLIGHT_SEGMENG[m_seg_string.Count];
                for (int i = 0; i < m_seg_string.Count; ++i)
                {
                    m_seg[i].FromString2(m_seg_string[i]);
                }
                //检查行动代码
                int count = m_seg.Length;
                List<FLIGHT_SEGMENG> lsfs= new List<FLIGHT_SEGMENG>();
                for (int i = 0; i < count; ++i)
                {
                    if(string.IsNullOrEmpty(m_seg[i].Actioncode))//added by king
                        continue;
                    if (m_seg[i].Actioncode == "RR" || m_seg[i].Actioncode.IndexOf("K") >= 0)//有效航段
                    {
                        lsfs.Add(m_seg[i]);
                    }
                }
                m_seg = lsfs.ToArray();
            }
            catch(Exception ex)
            {
                throw new Exception("在RT结果中取航段组出错:" + ex.Message);
            }
        }
        /// <summary>
        /// 取身份证组到m_cards  (SSR FOID MU HK1 NI422202197307010111/P6)
        /// </summary>
        /// <param name="line"></param>
        private void ssrfoidfromlines(string[] line)
        {
            try
            {
                List<string> ls = getsomeline(line, "SSR FOID");
                if (m_names.Length > ls.Count) throw new Exception("乘客数量和 SSR FOID 行数不匹配，取身份证号失败");
                m_cards = new string[m_names.Length];
                for (int i = 0; i < ls.Count; ++i)
                {
                    string l = ls[i];
                    int pos = l.LastIndexOf("/P");// /P的位置
                    int index = 0;
                    if (pos < 0)//没有/P的情况，只有一个乘客
                    {
                        pos = l.LastIndexOf("NI");//NI的位置
                        if (pos > 0)
                        {
                            m_cards[0] = l.Substring(pos + 2);
                        }
                        else//没有找到NI，可能证件号被隐藏
                        {
                            //m_cards = null;
                            return;
                        }
                        break;
                    }
                    else
                    {
                        index = Convert.ToInt32(l.Substring(pos + 2)) - 1;
                        m_cards[index] = egString.Between2StringReverse(l, "/P", "NI");
                    }
                }
            }
            catch (Exception ex)
            {
                //throw new Exception("在RT结果中取证件号码组出错:" + ex.Message);
                EagleFileIO.LogWrite("RtResult.ssrfoidfromlines : " + ex.Message);
            }
        }
        /// <summary>
        /// 从 SSR 组取票号
        ///  如：SSR TKNE MU HK1 KMGLNJ 5963 Y08DEC 7813853644500/1/P1
        ///      SSR TKNE 航空公司代码 行动代码 城市对 航班号 舱位日期 票号/航段序号/旅客序号
        /// </summary>
        /// <param name="line"></param>
        public void SSR_TKNE_fromlines(string[] line)
        {
            try
            {
                List<string> ls = getsomeline(line, "SSR TKNE");
                if (m_names.Length > ls.Count) throw new Exception("乘客数量和 SSR TKNE 行数不匹配，取票号失败");
                m_tktnos = new string[m_names.Length];
                for (int i = 0; i < ls.Count; ++i)
                {
                    string l = ls[i];
                    int pos = l.LastIndexOf("/P");// /P的位置

                    int index = Convert.ToInt32(l.Substring(pos + 2)) - 1;//由旅客序号来确定数组的索引序号
                    m_tktnos[index] = egString.Between2StringReverse(l, "/", " ");//在最后一个空格和斜杠之间
                }
            }
            catch (Exception ex)
            {
                //throw new Exception("RtResult.SSR_TKNE_fromlines:" + ex.Message);
                EagleFileIO.LogWrite("RtResult.SSR_TKNE_fromlines : " + ex.Message);
            }
        }
        /// <summary>
        /// 取(SSR ADTK 1E BY WUH23DEC08/0823 OR CXL MU5901 G24DEC)
        /// </summary>
        private void ssradtkfromlines(string [] line)
        {
            try
            {
                List<string> ls = getsomeline(line, "SSR ADTK");
                m_city_dz = ls[0].Substring(15, 3);
                string d = ls[0].Substring(18, 7);
                string t = ls[0].Substring(26, 4);
                m_time_dz = BaseFunc.str2datetime(d, false);
                int h = int.Parse(t.Substring(0, 2));
                int m = int.Parse(t.Substring(2));
                m_time_dz = m_time_dz.AddHours(h).AddMinutes(m);
            }
            catch(Exception ex)
            {
            }

        }

        /// <summary>
        /// 取每张电子客票的状态HK为TRUE，其它为FALSE(SSR TKNE CZ HK1 WUHCTU 3441 N09JAN 7843189478638/1/P1)一个人也有/1/P1
        /// </summary>
        /// <param name="line"></param>
        private void tktstatusfromlines(string[] line)
        {
            try
            {
                m_tkt_stat = new bool[m_names.Length * m_seg.Length];
                List<string> ls = getsomeline(line, "SSR TKNE");
                int indexseg = 0;
                int indexp = 0;
                for (int i = 0; i < ls.Count; ++i)
                {
                    try
                    {
                        string l = ls[i];

                        indexp = Convert.ToInt32(l.Substring((l.LastIndexOf("/P") + 2))) - 1;
                        indexseg = Convert.ToInt32(egString.Between2StringReverse(l, "/P", "/")) - 1;
                        m_tkt_stat[indexseg * m_names.Length + indexp] = (l.Substring(12, 2) == "HK");
                    }
                    catch
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("在RT结果中取客票状态组出错:" + ex.Message);
            }
        }

        /// <summary>
        /// 取航空公司大编码:RMK CA/BCK1J
        /// </summary>
        /// <param name="line"></param>
        private void pnrbigfromlines(string[] line)
        {
            try
            {
                List<string> ls = getsomeline(line, "RMK CA/");
                string l = ls[0];
                m_pnr_big = l.Substring(7);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 取票价组,注意可能有2行。FN/A/FCNY160.00/SCNY160.00/C3.00/XCNY130.00/TCNY50.00CN/TCNY80.00YQ/        -
        ///        ACNY290.00
        /// </summary>
        /// <param name="line"></param>
        private void farefromlines(string[] line)
        {

            List<string> ls = getsomeline(line, "FN/");
            if (ls.Count == 0) return;
            try
            {
                int fare = (int)Convert.ToDouble(egString.Between2String(ls[0], "FCNY", "/"));
                int build = 0;
                try
                {
                    build = (int)Convert.ToDouble(egString.Between2String(ls[0], "TCNY", "CN"));
                }
                catch
                {
                }
                int fuel = 0;
                try
                {
                    fuel = (int)Convert.ToDouble(egString.Between2StringReverse(ls[0], "YQ", "TCNY"));
                }
                catch
                {
                }
                m_price = new PRICE_CALCULATE(fare, build, fuel);
            }
            catch (Exception ex)
            {
                EagleFileIO.LogWrite("FN项可能被隐藏:" + ex.Message);
            }
        }

        /// <summary>
        /// 取票号组
        /// </summary>
        /// <param name="line"></param>
        private void tktnofromlines(string[] line)
        {
            try
            {
                List<string> ls = getsomeline(line, "TN/");

                if (m_et_flag || ls.Count > 0)//在TN/项中取
                {
                    m_tktnos = new string[m_names.Length];

                    if (egString.AfterString(ls[0], "/P") == "" && m_names.Length == 1)
                    {
                        m_tktnos[0] = egString.AfterString(ls[0], "TN/");
                        return;
                    }
                    for (int i = 0; i < ls.Count; ++i)
                    {
                        ls[i] = ls[i].Replace("+", "").Replace("-", "").Replace(">","");
                        int index = Convert.ToInt32(egString.AfterString(ls[i], "/P")) - 1;
                        string tktno = egString.Between2String(ls[i], "TN/", "/P");
                        m_tktnos[index] = tktno;
                    }
                }
                else//在OSI项中取
                {
                    ls = getsomeline(line, "OSI 1E");
                    if (ls.Count == 0)
                        return;
                    m_tktnos = new string[m_names.Length];

                    for (int i = 0; i < ls.Count; ++i)
                    {
                        string py = egString.AfterString(ls[i], m_seg.Length.ToString());

                        string ph = egString.Between2String(ls[i], "TN/", " ");
                        if (ph == "") ph = egString.Between2String(ls[i], "TKNO/", " "); ;//added by king
                        if (ph == "" || py == "") continue;//不是所找的OSI项
                        int j = 0;
                        for (j = 0; j < m_names.Length; ++j)
                        {
                            if (egString.GetSpell(m_names[j]).ToUpper() == py)
                            {
                                //if (m_tktnos[j] != "") continue;
                                if (!string.IsNullOrEmpty(m_tktnos[j])) continue;//modified by king
                                else
                                {
                                    m_tktnos[j] = ph;
                                    break;
                                }
                            }
                        }
                        if (j == m_names.Length) throw new Exception("有中文字不能被识别成拼音！");
                    }
                }
            }
            catch(Exception ex)
            {
                EagleFileIO.LogWrite("RtResult.tktnofromlines : " + ex.Message);
            }
        }
        /// <summary>
        /// 取EI项
        /// </summary>
        /// <param name="line"></param>
        private void eifromlines(string[] line)
        {
            try
            {
                List<string> ls = getsomeline(line, "EI/");
                m_ei = egString.AfterString(ls[0], "EI/");
            }
            catch (Exception ex)
            {
                m_ei = "";
                //EagleFileIO.LogWrite("RtResult.eifromlines : " + ex.Message);
            }
        }
        /// <summary>
        /// 取FP项 :(FP/CASH,CNY)
        /// </summary>
        /// <param name="line"></param>
        private void fpfromlines(string[] line)
        {
            try
            {
                List<string> ls = getsomeline(line, "FP/");
                m_fp = egString.AfterString(ls[0], "FP/");
            }
            catch (Exception ex)
            {
                EagleFileIO.LogWrite("RtResult.fpfromlines : " + ex.Message);
            }
        }

        private void babycountfromlines(string[] line)
        {
            try
            {
                List<string> ls = getsomeline(line, "XN/IN/");
                m_baby_count = ls.Count;
            }
            catch (Exception ex)
            {
                EagleFileIO.LogWrite("RtResult.babycountfromlines : " + ex.Message);
            }
        }
        /// <summary>
        /// 取行头为head的行的集合，已经去掉了chars_remove
        /// </summary>
        /// <param name="line">行数组,用数字分割后的行(并不是用\n分隔的)</param>
        /// <param name="head">行头字符串</param>
        /// <returns></returns>
        private List<string> getsomeline(string[] line, string head)
        {
             List<string> ls = new List<string>();
             for (int i = 0; i < line.Length; ++i)
             {
                 string l = line[i];
                 if (l.IndexOf(head) == 0)
                 {
                     ls.Add(egString.trim(l, chars_remove));
                 }
                 else
                 {
                     if (ls.Count > 0) break;
                 }
             }
             return ls;
        }
    }
    [Serializable]
    public class RtResultList
    {
        public List<RtResult> ls = new List<RtResult>();

        public void SerializeRtResults()
        {
            using (FileStream fs = new FileStream(Application.StartupPath + "\\SubmittedPnr.txt", FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, this);
            }

        }
        public static RtResultList DeSerializeRtResults()
        {
            try
            {
                using (FileStream fs = new FileStream(Application.StartupPath + "\\SubmittedPnr.txt", FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (RtResultList)(formatter.Deserialize(fs));
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
/*SAMPLE STRING
**ELECTRONIC TICKET PNR**
 0.19WUHLX/AL NM19 X3E6E
 1.高志银 2.耿爱云 3.黄成圣 4.黄剑河 5.胡汉桥 6.胡一安   7.姜烈君 8.李XINZI 9.李程 10.刘亚兰 11.梅克锋
12.裴艳辉 13.钱桂兰 14.王靖 15.王秀珍 16.王又喜 17.魏青 18.曾小兵 19.周建秋
20.  8C8276 X   TU23DEC08XMNWUH RR19  1020 1130          E
21.WUH/T WUH/T 027-85773939/WUH HAN KOU CTS TRAFFIC SERVICE CO.,LTD/HE JIN
     SONG ABCDEFG
22.*
23.T/AT//WUHCZ
24.SSR FOID                                                                    +
25.SSR FOID                                                                    -
26.SSR FOID
27.SSR FOID
28.SSR FOID
29.SSR FOID
30.SSR FOID
31.SSR FOID
32.SSR FOID
33.SSR FOID
34.SSR FOID
35.SSR FOID
36.SSR FOID                                                                    +
37.SSR FOID                                                                    -
38.SSR FOID
39.SSR FOID
40.SSR FOID
41.SSR FOID
42.SSR FOID
43.SSR TKNE 8C HK1 WUHXMN 8275 X19DEC 8833189478634/1/P19
44.SSR TKNE 8C HK1 WUHXMN 8275 X19DEC 8833189478632/1/P18
45.SSR TKNE 8C HK1 WUHXMN 8275 X19DEC 8833189478631/1/P17
46.SSR TKNE 8C HK1 WUHXMN 8275 X19DEC 8833189478630/1/P16
47.SSR TKNE 8C HK1 WUHXMN 8275 X19DEC 8833189478629/1/P15
48.SSR TKNE 8C HK1 WUHXMN 8275 X19DEC 8833189478628/1/P14                      +
49.SSR TKNE 8C HK1 WUHXMN 8275 X19DEC 8833189478626/1/P13                      -
50.SSR TKNE 8C HK1 WUHXMN 8275 X19DEC 8833189478625/1/P12
51.SSR TKNE 8C HK1 WUHXMN 8275 X19DEC 8833189478624/1/P11
52.SSR TKNE 8C HK1 WUHXMN 8275 X19DEC 8833189478623/1/P10
53.SSR TKNE 8C HK1 WUHXMN 8275 X19DEC 8833189478622/1/P9
54.SSR TKNE 8C HK1 WUHXMN 8275 X19DEC 8833189478621/1/P8
55.SSR TKNE 8C HK1 WUHXMN 8275 X19DEC 8833189478620/1/P7
56.SSR TKNE 8C HK1 WUHXMN 8275 X19DEC 8833189478619/1/P6
57.SSR TKNE 8C HK1 WUHXMN 8275 X19DEC 8833189478618/1/P5
58.SSR TKNE 8C HK1 WUHXMN 8275 X19DEC 8833189478616/1/P4
59.SSR TKNE 8C HK1 WUHXMN 8275 X19DEC 8833189478615/1/P3
60.SSR TKNE 8C HK1 WUHXMN 8275 X19DEC 8833189478614/1/P2                       +
61.SSR TKNE 8C HK1 WUHXMN 8275 X19DEC 8833189478613/1/P1                       -
62.SSR TKNE 8C HK1 XMNWUH 8276 X23DEC 8833189478613/2/P1
63.SSR TKNE 8C HK1 XMNWUH 8276 X23DEC 8833189478614/2/P2
64.SSR TKNE 8C HK1 XMNWUH 8276 X23DEC 8833189478615/2/P3
65.SSR TKNE 8C HK1 XMNWUH 8276 X23DEC 8833189478616/2/P4
66.SSR TKNE 8C HK1 XMNWUH 8276 X23DEC 8833189478618/2/P5
67.SSR TKNE 8C HK1 XMNWUH 8276 X23DEC 8833189478619/2/P6
68.SSR TKNE 8C HK1 XMNWUH 8276 X23DEC 8833189478620/2/P7
69.SSR TKNE 8C HK1 XMNWUH 8276 X23DEC 8833189478621/2/P8
70.SSR TKNE 8C HK1 XMNWUH 8276 X23DEC 8833189478622/2/P9
71.SSR TKNE 8C HK1 XMNWUH 8276 X23DEC 8833189478623/2/P10
72.SSR TKNE 8C HK1 XMNWUH 8276 X23DEC 8833189478624/2/P11                      +
73.SSR TKNE 8C HK1 XMNWUH 8276 X23DEC 8833189478625/2/P12                      -
74.SSR TKNE 8C HK1 XMNWUH 8276 X23DEC 8833189478626/2/P13
75.SSR TKNE 8C HK1 XMNWUH 8276 X23DEC 8833189478628/2/P14
76.SSR TKNE 8C HK1 XMNWUH 8276 X23DEC 8833189478629/2/P15
77.SSR TKNE 8C HK1 XMNWUH 8276 X23DEC 8833189478630/2/P16
78.SSR TKNE 8C HK1 XMNWUH 8276 X23DEC 8833189478631/2/P17
79.SSR TKNE 8C HK1 XMNWUH 8276 X23DEC 8833189478632/2/P18
80.SSR TKNE 8C HK1 XMNWUH 8276 X23DEC 8833189478634/2/P19
81.RMK CA/Z4SRZ
82.RMK AUTOMATIC FARE QUOTE
83.FN/A/
84.TN/883-3189478613/P1                                                        +
85.TN/883-3189478614/P2                                                        -
86.TN/883-3189478615/P3
87.TN/883-3189478616/P4
88.TN/883-3189478618/P5
89.TN/883-3189478619/P6
90.TN/883-3189478620/P7
91.TN/883-3189478621/P8
92.TN/883-3189478622/P9
93.TN/883-3189478623/P10
94.TN/883-3189478624/P11
95.TN/883-3189478625/P12
96.TN/883-3189478626/P13                                                       +
97.TN/883-3189478628/P14                                                       -
98.TN/883-3189478629/P15
99.TN/883-3189478630/P16
100.TN/883-3189478631/P17
101.TN/883-3189478632/P18
102.TN/883-3189478634/P19
103.FP/CASH,CNY
104.WUH129
*/
/*
*THIS PNR WAS ENTIRELY CANCELLED*
004     HDQCA 9983 0341 08JAN /RLC3 
     X1.CE/SHI(001) PN27K   
001 X2.  MU2501 Y   FR09JAN  WUHPVG XX1   0830 0945          E --T1 
       NN(001)  DK(001)  HK(001)  XX(003)   
001 X3.WUH/T WUH/T 0714-6255100/HUANG SHI LAN XIANG TICKET CO.,LTD/CAOQING  
         ABCDEFG
001 X4.123  
001 X5.TL/2300/08JAN/WUH128 
002 X6.RMK CA/JM5BC 
001  7.WUH402                                                                  +
*/