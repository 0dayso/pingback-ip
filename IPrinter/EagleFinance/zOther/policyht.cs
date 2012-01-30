using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace EagleFinance.zOther
{
    class policyht
    {
        static public int i网络类型 = 0;//郑州:0为内网,1为外网

        public static void hashadd(hashkey key, hashvalue value)
        {
            GlobalVar.ht.Add(key, value);
        }
        public static void hashadd(string username, string flightno, string bunk, string date, string maxGain, string userGain)
        {
            hashkey key = new hashkey();
            key.username = username; key.flightno = flightno; key.bunk = bunk; key.date = date;
            hashvalue value = new hashvalue();
            value.maxGain = maxGain; value.userGain = userGain;
            GlobalVar.ht.Add(key, value);
        }
        public static hashvalue getvaluefromhashtable(hashkey key)
        {
            try
            {
                hashkey[] hk = new hashkey[GlobalVar.ht.Count];
                GlobalVar.ht.CopyTo(hk, 0);
                for (int i = 0; i < GlobalVar.ht.Count; i++)
                {
                    if (hk[i].date == key.date && hk[i].bunk == key.bunk && hk[i].flightno == key.flightno && hk[i].username == key.username)
                        return (hashvalue)GlobalVar.ht[key];
                }
            }
            catch
            {
            }
            return null;
            //if (GlobalVar.ht.Contains(key)) return (hashvalue)GlobalVar.ht[key];
            //else return null;
        }
        public static hashvalue getvaluefromhashtable(string username, string flightno, string bunk, string date)
        {
            hashkey key = new hashkey();
            key.username = username; key.flightno = flightno; key.bunk = bunk; key.date = date;
            if (GlobalVar.ht.Contains(key)) return (hashvalue)GlobalVar.ht[key];
            else return null;
        }
        public static hashvalue getvaluefromwebserverandsave(hashkey key, string from, string to, int netroute)
        {
            hashvalue value = new hashvalue();
            string maxgain = "";
            string usergain = "";
            try
            {
                string srvUrl = "";
                if (netroute == 1) srvUrl = "http://yinge.eg66.com/WS3/egws.asmx";
                if (netroute == 2) srvUrl = "http://wangtong.eg66.com/WS3/egws.asmx";
                if (GlobalVar.agent == AGENTS.ZHENGZHOU)
                {
                    if (i网络类型 == 0)
                        srvUrl = "http://10.2.1.23/ws/egws.asmx";
                    else if (i网络类型 == 1)
                        srvUrl = "http://www.zza96666.cn/ws/egws.asmx";
                }
                WS.egws ws = new WS.egws(srvUrl);
                gs.para.NewPara np = new gs.para.NewPara();
                np.AddPara("cm", "GetPromot");
                np.AddPara("UserName", key.username);
                np.AddPara("Airs", key.flightno);
                np.AddPara("Date", key.date);
                np.AddPara("BeginCity", from);
                np.AddPara("EndCity", to);
                string strSent = np.GetXML();
                string strPolicy = ws.getEgSoap(strSent);
                if (strPolicy == "") throw new Exception("取政策服务返回空值");
                string defaultpolicy = "";
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(strPolicy);
                defaultpolicy = xd.SelectSingleNode("eg").SelectSingleNode("RetGain").InnerText;
                usergain = defaultpolicy;
                
                XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode("Promots");
                if (xn.ChildNodes.Count <= 0 && defaultpolicy != "") maxgain = GlobalVar.gbMaxPolicy;
                if (defaultpolicy == "") defaultpolicy = GlobalVar.gbAgentPolicy;
                for (int i = 0; i < xn.ChildNodes.Count; i++)
                {
                    try
                    {
                        XmlNode nodePolicy = xn.ChildNodes[i];
                        string userpolicy = nodePolicy.ChildNodes[4].ChildNodes[0].Value.ToString().Trim();
                        string key1 = nodePolicy.ChildNodes[9].ChildNodes[0].Value.ToString().Trim();
                        if (key1 == key.flightno + "-" + key.bunk)
                        {
                            maxgain = nodePolicy.ChildNodes[1].ChildNodes[0].Value.ToString().Trim();
                            if (maxgain == "") maxgain = GlobalVar.gbMaxPolicy;
                            if (userpolicy == "") userpolicy = defaultpolicy;
                            usergain = userpolicy;
                            break;
                        }

                    }
                    catch
                    {
                        throw new Exception("2");
                    }
                }
            }
            catch
            {
                value.maxGain = GlobalVar.gbMaxPolicy;
                value.userGain = GlobalVar.gbAgentPolicy;
                return value;
            }
            
            value.maxGain = maxgain;
            value.userGain = usergain;
            hashadd(key, value);
            return value;
        }
    }
}
