using System;
using System.Collections.Generic;
using System.Text;

namespace EagleString
{
    public class CommandCreate
    {
        /// <summary>
        /// names与cardnos对应，并且必须事先按eterm方式排序,若多个航段,各数组必须按航班日期排序
        /// </summary>
        /// <param name="flight"></param>
        /// <param name="bunk"></param>
        /// <param name="dtflight"></param>
        /// <param name="citypair"></param>
        /// <param name="peoplei"></param>
        /// <param name="names"></param>
        /// <param name="cardnos"></param>
        /// <param name="phone"></param>
        /// <param name="office"></param>
        /// <param name="remarks"></param>
        /// <returns></returns>
        public static string Create_SS_String(string[] flight, char[] bunk, DateTime[] dtflight, string[] citypair, int peoplei,
            string[] names, string[] cardnos, string phone, string office,string [] remarks)
        {
            //  ssCZ2565/C/20FEB/KHNCAN/NN1
            string newline = Convert.ToString('\xD');
            string g = "/";
            string ret = "";
            string al = "";
            int interva1 = 1;//缺口程中使用
            for (int i = 0; i < flight.Length; ++i)
            {
                if (flight[i] == "")
                {
                    continue;
                }
                string ss = "SS:" +
                    flight[i] + g + bunk[i].ToString() + g + dtflight[i].ToString("ddMMM", egString.dtFormat) + g + citypair[i] + g +
                    "LL" + peoplei.ToString();
                ret += (ss + "" + newline);

                //增加缺口程

                try
                {
                    for (int j = 1; j < 4; j++)
                    {
                        if (flight[i + j] != "")
                        {
                            interva1 = j;
                            break;
                        }
                    }
                    if (citypair[i].Substring(3).ToUpper() != citypair[i + interva1].Substring(0, 3).ToUpper())
                    {
                        string sa = "SA:"+ citypair[i].Substring(3).ToUpper()
                            + citypair[i + interva1].Substring(0, 3).ToUpper();
                        ret += (sa + newline);
                    }
                }
                catch
                {
                }

                al = flight[i].Substring(0, 2);
            }
            string nm = "";
            for (int i = 0; i < peoplei; ++i)
            {
                nm += "1" + names[i];
            }
            ret += ("NM" + nm + newline);
            ret += ("CT" + phone + newline);

            string limitday = "";
            string limittime = "";
            LimitTime(dtflight, ref limitday, ref limittime);

            ret += ("TKTL" + limittime + g + limitday + g + office + newline);
            for (int i = 0; i < peoplei; ++i)
            {
                string cardno = cardnos[i];
                ret += ("SSR FOID " + al + " HK/NI" + cardno + "/P" + Convert.ToString(i + 1) + newline);
            }
            ret += "@ik";
            return ret;
        }
        public static string Create_SS_String(string flight, char bunk, DateTime dtflight, string citypair, int peoplei,
            string[] names, string[] cardnos, string phone, string office, string[] remarks)
        {
            return
                Create_SS_String(new string[] { flight }, 
                new char[] { bunk }, 
                new DateTime[] { dtflight }, 
                new string[] { citypair },
                peoplei, names, cardnos, phone, office, remarks);
        }
        /// <summary>
        /// 生成AV指令串AV H WUHPVG
        /// </summary>
        /// <param name="from">出发城市三字码</param>
        /// <param name="to">目标城市三字码</param>
        /// <param name="dt">日期</param>
        /// <param name="time">时间(表示几点后的航班)</param>
        /// <param name="direct">是否直飞</param>
        /// <param name="airline">航空公司二字代码(所有为"")</param>
        /// <returns></returns>
        public static string Create_AV_String(string from, string to, DateTime dt, int time, bool direct, string airline)
        {
            string ret = "Av H ";
            ret += from + to;
            ret += dt.ToString("ddMMM", egString.dtFormat);
            ret += "/" + time.ToString("d4");
            if (direct)
                ret += "/d";
            if (airline.Length == 2)
                ret += "/" + airline;
            return ret;
        }

        /// <summary>
        /// 根据几个航段的日期，取出票时限日期及时间(同EagleControls.Operators.)
        /// </summary>
        /// <param name="date"></param>
        /// <param name="limitDay"></param>
        /// <param name="limitTime"></param>
        public static void LimitTime(string[] date, ref string limitDay, ref string limitTime)
        {
            DateTime dt = EagleString.BaseFunc.str2datetime(date[0], true);
            for (int i = 1; i < date.Length; ++i)
            {
                try
                {
                    if (date[i] == "") continue;
                    DateTime d = EagleString.BaseFunc.str2datetime(date[i], true);
                    if (d < dt) dt = d;
                }
                catch (Exception ex)
                {
                    EagleString.EagleFileIO.LogWrite("Operators.LimitTime : " + ex.Message);
                }
            }
            limitDay = dt.ToString("ddMMM", EagleString.egString.dtFormat);
            if (DateTime.Now > dt)
            {
                limitTime = DateTime.Now.AddHours(1).ToString("HHmm");
                //MessageBox.Show("注意:由于当天航班，请在一小时内出票！");
            }
            else limitTime = "0500";

        }
        public static void LimitTime(DateTime[] date, ref string limitDay, ref string limitTime)
        {
            string[] d = new string[date.Length];
            for (int i = 0; i < d.Length; ++i)
            {
                d[i] = date[i].ToString("ddMMM", EagleString.egString.dtFormat);
            }
            LimitTime(d, ref limitDay, ref limitTime);
        }
    }
}
