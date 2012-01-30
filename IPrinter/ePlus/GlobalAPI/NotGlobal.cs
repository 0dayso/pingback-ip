using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace ePlus.GlobalAPI
{
    class NotGlobal
    {
        /// <summary>
        /// �ж��Ƿ�ΪϹ���س�������ָ�
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
        /// �������г��������õõ����飬��WUH128-1,WUH128-2,WUH129-1,WUH129-2���򷵻�"WUH128��;WUH129��"��ֻ��һ�������򲻷���
        /// ��������������ͬһ���������ϡ���Ȼ���ܻᵼ��ָ���޷��أ��ӷ���������
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
                if (lsCfg[i] != "") ret.Add(lsCfg[i] + "��Ⱥ");
            }
            return ret;
        }
        /// <summary>
        /// ȡĳ��OFFICE����������IP
        /// </summary>
        /// <param name="officenumber">6�ֽڵ�OFFICE��</param>
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
                    if (xncfg.InnerText.ToLower().IndexOf(officenumber.ToLower()) == 0)//���������
                    {
                        //ret += xn.ChildNodes[i].SelectSingleNode("ip").InnerText + "~";
                        ret += xn.ChildNodes[i].SelectSingleNode("ipid").InnerText + "~";//ipid��ɾ��connectcfg.xml���������Ϣ
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
