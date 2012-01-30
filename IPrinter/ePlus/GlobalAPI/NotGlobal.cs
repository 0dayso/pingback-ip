using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace ePlus.GlobalAPI
{
    class NotGlobal
    {
        /// <summary>
        /// 判断是否为瞎按回车产生的指令。
        /// </summary>
        /// <param name="ls"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool CheckSameCommand(List<string> ls, string str)
        {
            string cmd = str.Trim().ToLower();
            if (cmd == "pn" || cmd == "pb") return false;
            if (ls.Count < 1) return false;
            if (ls[ls.Count - 1].ToLower().Trim() != "cmd") return false;
            return false;
        }

        /// <summary>
        /// 把所有列出来的配置得到分组，如WUH128-1,WUH128-2,WUH129-1,WUH129-2，则返回"WUH128组;WUH129组"，只有一个配置则不返组
        /// 组配置里必须放在同一个服务器上。不然可能会导致指令无返回，视服务程序而定
        /// </summary>
        /// <param name="lsCfg"></param>
        /// <returns></returns>
        public List<string> GetConfigGroupsBy(List<string> lsCfg)
        {
            List<string> ret = new List<string>();
            for (int i = 0; i < lsCfg.Count; i++)
            {
                lsCfg[i] = lsCfg[i].Substring(0, 6);
            }
            for (int i = 0; i < lsCfg.Count - 1; i++)
            {
                if (lsCfg[i].ToUpper() == lsCfg[i + 1].ToUpper())
                    lsCfg[i] = "";
            }
            for (int i = 0; i < lsCfg.Count; i++)
            {
                if (lsCfg[i] != "") ret.Add(lsCfg[i] + "集群");
            }
            return ret;
        }
        /// <summary>
        /// 取某个OFFICE的所有配置IP
        /// </summary>
        /// <param name="officenumber">6字节的OFFICE号</param>
        /// <returns></returns>
        public string GetConfigIPsGroupBy(string officenumber)
        {
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(GlobalVar.loginXml);
                XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode("IPS");
                string ret = "";
                for (int i = 0; i < xn.ChildNodes.Count; i++)
                {
                    XmlNode xncfg = xn.ChildNodes[i].SelectSingleNode("PeiZhi");
                    if (xncfg.InnerText.ToLower().IndexOf(officenumber.ToLower()) == 0)//这里的条件
                    {
                        //ret += xn.ChildNodes[i].SelectSingleNode("ip").InnerText + "~";
                        ret += xn.ChildNodes[i].SelectSingleNode("ipid").InnerText + "~";//ipid并删掉connectcfg.xml里的配置信息
                    }
                }
                if (ret.Length > 0)
                {
                    ret = ret.Substring(0, ret.Length - 1);
                    return ret;
                }
            }
            catch
            {
            }
            return "";
        }
    }

}
