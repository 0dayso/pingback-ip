using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace EagleWebService
{
    public class IbeRtResult
    {

        string xml = "";
        public IbeRtResult(string rtxml)
        {
            xml = rtxml;
        }
        /// <summary>
        /// 取乘客信息
        /// </summary>
        /// <param name="iVal">0表示取姓名.1表示取身份证.2表示取电子票号</param>
        /// <returns></returns>
        public string[] getpeopleinfo(int iVal)//
        {
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(xml);
            XmlNode xn = xd.SelectSingleNode("ibe").SelectSingleNode("passenger");
            int count = xn.ChildNodes.Count;
            if (count == 0) return null;
            string[] ret = new string[count];
            for (int i = 0; i < count; i++)
            {
                XmlNode xmtmp = xn.ChildNodes[i];
                switch (iVal)
                {
                    case 0:
                        ret[i] = xmtmp.SelectSingleNode("name").InnerText;
                        break;
                    case 1:
                        ret[i] = xmtmp.SelectSingleNode("cardid").InnerText;
                        break;
                    case 2:
                        ret[i] = xmtmp.SelectSingleNode("tktno").InnerText;
                        break;
                    default:
                        ret[i] = "";
                        break;
                }
            }
            return ret;
        }
        /// <summary>
        /// flightno~bunk~fromto~flightdate~timeLeave~timeArrive
        /// </summary>
        /// <returns></returns>
        public string[] getflightsegsinfo()
        {
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(xml);
            XmlNode xn = xd.SelectSingleNode("ibe").SelectSingleNode("segment");
            int count = xn.ChildNodes.Count;
            if (count == 0) return null;
            string[] ret = new string[count];
            for (int i = 0; i < count; i++)
            {
                ret[i] = "";
                XmlNode xmtmp = xn.ChildNodes[i];
                ret[i] += xmtmp.SelectSingleNode("flightno").InnerText + "~";
                ret[i] += xmtmp.SelectSingleNode("bunk").InnerText + "~";
                ret[i] += xmtmp.SelectSingleNode("fromto").InnerText + "~";
                ret[i] += xmtmp.SelectSingleNode("flightdate").InnerText + "~";
                ret[i] += xmtmp.SelectSingleNode("timeLeave").InnerText + "~";
                ret[i] += xmtmp.SelectSingleNode("timeArrive").InnerText;
            }
            return ret;
        }
    }

}