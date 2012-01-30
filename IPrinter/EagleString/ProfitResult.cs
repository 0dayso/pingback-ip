using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;

namespace EagleString
{
    public class ProfitResult
    {
        private string m_main = "";
        private Hashtable m_ht;
        private string m_default_gain = "0";
        /// <summary>
        /// it should be from the webservice "GetPromot"
        /// </summary>
        /// <param name="profitstring"></param>
        public ProfitResult(string profitstring)
        {
            m_main = profitstring;
            set_hash_table();
        }
        private void set_hash_table()
        {
            if (m_ht == null) m_ht = new Hashtable();
            XmlDocument xd = new XmlDocument();
            try
            {
                xd.LoadXml(m_main);
                XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode("Promots");
                m_default_gain = xd.SelectSingleNode("eg").SelectSingleNode("RetGain").InnerText;
                for (int i = 0; i < xn.ChildNodes.Count; i++)
                {
                    try
                    {
                        PolicyInfomation pi = new PolicyInfomation();
                        XmlNode nodePolicy = xn.ChildNodes[i];
                        string strKey = nodePolicy.ChildNodes[9].ChildNodes[0].Value.ToString().Trim();
                        pi.policyid = nodePolicy.ChildNodes[0].ChildNodes[0].Value.ToString().Trim();
                        pi.airgain = nodePolicy.ChildNodes[1].ChildNodes[0].Value.ToString().Trim();
                        pi.gainid = nodePolicy.ChildNodes[2].ChildNodes[0].Value.ToString().Trim();
                        pi.rebate = nodePolicy.ChildNodes[3].ChildNodes[0].Value.ToString().Trim();
                        pi.usergain = nodePolicy.ChildNodes[4].ChildNodes[0].Value.ToString().Trim();
                        pi.bunk = nodePolicy.ChildNodes[5].ChildNodes[0].Value.ToString().Trim();
                        pi.agentid = nodePolicy.ChildNodes[6].ChildNodes[0].Value.ToString().Trim();
                        pi.agentname = nodePolicy.ChildNodes[7].ChildNodes[0].Value.ToString().Trim();
                        pi.pubusername = nodePolicy.ChildNodes[8].ChildNodes[0].Value.ToString().Trim();
                        pi.outergain = nodePolicy.ChildNodes[10].ChildNodes[0].Value.ToString().Trim();
                        pi.policybegin = nodePolicy.ChildNodes[11].ChildNodes[0].Value.ToString().Trim();
                        pi.policyend = nodePolicy.ChildNodes[12].ChildNodes[0].Value.ToString().Trim();
                        m_ht.Add(strKey.ToUpper(), pi);
                    }
                    catch
                    { }
                }
            }
            catch { }
        }
        /// <summary>
        /// 返回带百分号的字符串
        /// </summary>
        /// <param name="flightno"></param>
        /// <param name="bunk"></param>
        /// <returns></returns>
        public string ProfitWithFlightAndBunk(string flightno, char bunk)
        {
            string key = flightno.ToUpper() + "-" + bunk.ToString().ToUpper();
            if (m_ht.ContainsKey(key))
            {
                PolicyInfomation pi = (PolicyInfomation)m_ht[key];
                //if (key.Substring(0, 2) == "MU") MessageBox.Show(key + "/" + pi.usergain);
                return pi.usergain + "%";
            }
            else
            {
                return m_default_gain + "%";
            }
            return "-";
        }
        private class PolicyInfomation
        {
            public string policyid = "";
            public string airgain = "";//<AirRetGain>
            public string gainid = "";
            public string rebate = "";
            public string usergain = "";//<用户返点RetGain>
            public string bunk = "";
            public string agentid = "";
            public string agentname = "";
            public string pubusername = "";
            public string key = "";
            public string outergain = "";//<对外返点PubRetGain>
            public string policybegin = "";
            public string policyend = "";
        }
    }
}
