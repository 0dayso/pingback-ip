using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace EagleDllImport
{
    public class CtiFunction
    {
        /// <summary>
        /// 客户资料来电，格式:kld [telephone]
        /// </summary>
        /// <param name="s"></param>
        public static string kld(string s,string webservice)
        {
            char newline = '\xD';
            string ret = "";
            EagleWebService.wsYzpbtoc ws = new EagleWebService.wsYzpbtoc(webservice);
            string[] sp = new string[] { " ", ",", "，", ":", ";", "：", "；" };
            string[] a = s.Split(sp, StringSplitOptions.RemoveEmptyEntries);
            if (a.Length > 1)
            {
                ret = ws.GetCustomerByPhone(a[1]);
                if (ret.ToLower().Trim() == "none") throw new Exception(string.Format("不存在{0}的客户资料", a[1]));
                else if (ret.ToLower().Trim() == "failed") throw new Exception(string.Format("查询失败！"));
            }
            else
            {
                ///here to do: give callxml value.
                string callxml = "";
                if (string.IsNullOrEmpty(callxml)) throw new Exception("没有检测到客户来电记录！");
                System.Xml.XmlDocument xd = new System.Xml.XmlDocument ();
                xd.LoadXml(callxml);//不能载入，则会抛出异常
                if (xd.SelectSingleNode("NewCustomer") != null)throw new Exception("当前来电为新客户，请先保存该客户信息！");
                ret = callxml;
            }
            DataEntity.XMLSchema.Customers customer;
            customer = DataEntity.XMLSchema.xml_BaseClass.LoadXml<DataEntity.XMLSchema.Customers>(ret);

            if (customer.Passengers == null || customer.Passengers.Count == 0)
                throw new Exception("该来电客户目前没有常旅客！");
            string retString = "";
            foreach (DataEntity.XMLSchema.Passenger passenger in customer.Passengers)
            {
                retString += passenger.fet_ID + " "
                      + passenger.fet_Name + " "
                      + passenger.fet_CardID1 + newline.ToString();
            }
            return retString;
        }
        public static string egyd(string s, string kldinfo,string tktime,string tkdate,string officeno)
        {
            char newline = '\xD';
            string[] sp = new string[] { " ", ",", "，", ":", ";", "：", "；" };
            string[] arr = s.Split(sp, StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length < 2) throw new Exception("EGYD3:无参数！");                                       //错误3

            string strCmd = "sd" + arr[1] + string.Format("{0}", arr.Length - 2) + newline.ToString();
            List<string> lsNM = new List<string>();

            XmlDocument xd = new XmlDocument();
            string resKHZLLD = kldinfo;
            if (resKHZLLD == "") throw new Exception("EGYD4:无来电信息");                                      //错误4
            xd.LoadXml(resKHZLLD);
            XmlNode xn = xd.SelectSingleNode("//Customer/Passengers");
            for (int i = 2; i < arr.Length; i++)
            {
                for (int j = 0; j < xn.ChildNodes.Count; j++)
                {
                    if (xn.ChildNodes[j].SelectSingleNode("PassengerID").InnerText.Trim() == arr[i].Trim())
                    {
                        lsNM.Add(xn.ChildNodes[j].SelectSingleNode("PassengerName").InnerText + "-" + xn.ChildNodes[j].SelectSingleNode("CardID").InnerText);
                    }
                }
            }
            EagleString.egString.sortStringListByPinYin(lsNM);
            strCmd += "nm";
            for (int i = 0; i < lsNM.Count; i++)
            {
                strCmd += "1" + lsNM[i].Split('-')[0];
            }
            strCmd += newline.ToString();
            strCmd += "ct" + xd.SelectSingleNode("//Customer/Mobile").InnerText + xd.SelectSingleNode("//Customer/Mobile").InnerText
                + newline.ToString();
            strCmd += "tktl" + (tktime == "" ? "2300" : tktime)
                + "/" + (tkdate == "" ? "." : tkdate) + "/" + (officeno == "" ? "wuh128" : officeno) + newline.ToString();
            for (int i = 0; i < lsNM.Count; i++)
            {
                string tempstr = lsNM[i].Split('-')[1].Trim();
                if (tempstr == "") tempstr = "0";
                strCmd += "SSR FOID YY HK/NI" + tempstr + "/p" + string.Format("{0}", i + 1) + newline.ToString();
            }
            strCmd += "@";
            return strCmd;
        }
        /// <summary>
        /// 增加或修改对应PNR的航意险
        /// </summary>
        /// <param name="s">格式baoxian pnr 1</param>
        /// <param name="ht">key=pnr,value=baoxian</param>
        /// <returns>返回显示串</returns>
        public static string baoxian(string s, System.Collections.Hashtable ht)
        {
            string[] sp = new string[] { " ", ",", "，", ":", ";", "：", "；" };
            string[] arr = s.Split(sp, StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length != 3) throw new Exception("baoxian5:未指定保险的PNR及类型");                                 //错误5
            string retString = arr[1] + "-" + arr[2];
            if (ht.Contains(arr[1].ToUpper()))
                ht[arr[1].ToUpper()] = arr[2];
            else
                ht.Add(arr[1].ToUpper(), arr[2]);
            retString = "对PNR:" + arr[1].ToUpper() + "的保险改为" + arr[2];
            return retString;
        }
    }
}
