using System;
using System.Collections.Generic;
using System.Text;
using WebService2;

namespace logic
{
    public class IBEAgent
    {
        /// <summary>
        /// IBE对象
        /// </summary>
        private WebService2.IBECOMService wsibe;
        /// <summary>
        /// 配置编号
        /// </summary>
        public static string _IpID = "10";
        public string IpID
        {
            get
            {
                return _IpID;
            }
            set
            {
                _IpID = value;
            }
        }
        public IBEAgent()
        {
            wsibe = new WebService2.IBECOMService();
        }
        /// <summary>
        /// 查航班
        /// </summary>
        /// <param name="startCity"></param>
        /// <param name="endCity"></param>
        /// <param name="flyDate"></param>
        /// <param name="airLine"></param>
        /// <returns></returns>
        public string getAV(string startCity, string endCity, string flyDate, string airLine)
        {
            return wsibe.getAV(_IpID, startCity, endCity, flyDate, airLine);

        }
        /// <summary>
        /// 查价格
        /// </summary>
        /// <param name="startCity"></param>
        /// <param name="endCity"></param>
        /// <param name="flyDate"></param>
        /// <returns></returns>
        public string getFDWS(string startCity, string endCity, string flyDate)
        {
            return wsibe.getFDWS(_IpID, startCity, endCity, flyDate);
        }
        /// <summary>
        /// 产生电脑号
        /// </summary>
        /// <param name="strXml"></param>
        /// <returns></returns>
        public string MakeEbtOrder(string strXml)
        {
            return wsibe.MakeEbtOrder(_IpID, strXml);
        }
        //public string getPrice()
        //{
        //}
        /// <summary>
        /// 做RT
        /// </summary>
        /// <param name="strXml"></param>
        /// <returns></returns>
        public string wsRT2(string strXml)
        {
            return wsibe.wsRT2(_IpID, strXml);
        }

        public string getRtInfo(string pnr)
        {
            return wsibe.getRtInfo(_IpID, pnr);
        }
        public string getAFlightInfo(string p_strFlight, string p_strBeginPort, string p_strEndPort, string p_strTime, string p_strAirCorpCode)
        {
            return wsibe.getAFlightInfo(_IpID, p_strFlight, p_strBeginPort, p_strEndPort, p_strTime, p_strAirCorpCode);
        }
        public string MakeOrder(string p_strXml)
        {
            return wsibe.MakeOrder(_IpID, p_strXml);
        }
        public string getRt(string p_strPnr)
        {
            return wsibe.getRt(_IpID, p_strPnr);
        }
        public string delPnr(string p_strPnr)
        {
            return wsibe.delPnr(_IpID, p_strPnr);
        }
    }
}
