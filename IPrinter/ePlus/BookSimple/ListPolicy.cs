using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections;

using System.ComponentModel;
using System.Data;
using System.Drawing;


using System.Data.OleDb;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;

namespace ePlus
{
    class ListPolicy
    {
        static public string avDate = "";
        public string xmlPolicy = "";
        public void SetAllPolicy(System.Windows.Forms.ListView lv,string date)
        {
            try
            {
                if (!Model.md.b_026) return;
                int iCount = lv.Items.Count;
                System.Windows.Forms.ListViewItem[] items = new System.Windows.Forms.ListViewItem[iCount];
                string flightnos = "";
                for (int i = 0; i < iCount; i++)
                {
                    string fn = lv.Items[i].SubItems[1].Text;
                    if (fn.Substring(0, 1) == "*") fn = fn.Substring(1);
                    flightnos += fn + (i == iCount - 1 ? "" : ",");
                }

                string cp = lv.Items[0].SubItems[3].Text;
                AVResult ar = new AVResult();
                ar.avResult = connect_4_Command.AV_String;
                ar.SetVar();
                //ar.SetToListview(lv,DateTime.Parse(date));
                Application.DoEvents();
                string day = ar.avDate.Substring(0, 2);
                string mon = ar.avDate.Substring(2, 3);
                int iDay = int.Parse(day);
                int iMon = int.Parse(EagleAPI.GetMonthInt(mon));
                int iYear = DateTime.Now.Year;
                if (iMon < DateTime.Now.Month) iYear++;

                string dd = iYear.ToString() + "-" + iMon.ToString() + "-" + iDay.ToString();
                ListPolicy.avDate = dd;
                EagleString.AvResult ar_new = new EagleString.AvResult(ar.avResult, 0, 0);
                ListPolicy.avDate = ar_new.FlightDate_DT.ToShortDateString();
                GlobalVar2.xmlPolicies = WebService.wsGetPolicies(flightnos, dd, cp.Substring(0, 3), cp.Substring(cp.Length - 3));
                Hashtable ht = GetHashTableFromXmlString(GlobalVar2.xmlPolicies);
                for (int i = 0; i < iCount; i++)
                {
                    items[i] = new System.Windows.Forms.ListViewItem();
                    items[i].Text = "";
                    items[i].SubItems.Add("返点");
                    items[i].SubItems.Add("");
                    items[i].SubItems.Add("政策");
                    items[i].SubItems.Add("");
                    items[i].SubItems.Add("");
                    items[i].SubItems.Add("");

                    int countLie = lv.Items[i].SubItems.Count;
                    for (int j = 7; j < countLie; j++)//23为3折舱,35为所有舱
                    {
                        Application.DoEvents();
                        try
                        {
                            if (lv.Items[i].SubItems[j].Text.Trim().Length != 2)
                                items[i].SubItems.Add("");
                            else
                            {
                                string p = GetOnePolicy(lv.Items[i].SubItems[3].Text,
                                                                    date,
                                                                    lv.Items[i].SubItems[1].Text,
                                                                    lv.Items[i].SubItems[j].Text[0].ToString(),
                                                                    ht
                                                                    );
                                items[i].SubItems.Add(p);

                            }
                        }
                        catch
                        {
                        }
                    }
                }
                for (int i = 0; i < iCount; i++)
                {
                    lv.Items.Insert(2 * i + 1, items[i]);
                }
                bool b = false;
                for (int i = 0; i < lv.Items.Count; i += 2)
                {
                    try
                    {
                        b = !b;
                        if (b)
                            lv.Items[i].BackColor = lv.Items[i + 1].BackColor = lv.Items[i].BackColor = System.Drawing.Color.Gainsboro;
                        else
                            lv.Items[i].BackColor = lv.Items[i + 1].BackColor = lv.Items[i].BackColor = System.Drawing.Color.White;
                    }
                    catch
                    {
                    }
                }
            }
            catch(Exception ex)
            {
                EagleAPI.LogWrite("SetAllPolicy:" + ex.Message);
            }
        }
        /// <summary>
        /// 从WEB服务中已经得到的政策信息中取对应舱位政策
        /// </summary>
        /// <param name="fromto"></param>
        /// <param name="date"></param>
        /// <param name="flightno"></param>
        /// <param name="bunk"></param>
        /// <returns></returns>
        public string GetOnePolicy(string fromto, string date,string flightno,string bunk,Hashtable hashtable)
        {
            if (fromto.Trim().Length != 6) return "";
            if (bunk == "") return "";
            return GetPolicyFormXmlString(hashtable, flightno, bunk);
        }
        public string GetPolicyFormXmlString(Hashtable hashtable, string flightno, string bunk)
        {
            try
            {
                string key = flightno.ToUpper()+"-"+bunk.ToUpper();
                if (hashtable.ContainsKey(key))
                {
                    PolicyInfomation pi = (PolicyInfomation)hashtable[key];
                    //if (key.Substring(0, 2) == "MU") MessageBox.Show(key + "/" + pi.usergain);
                    return float.Parse(pi.usergain).ToString() + "%";
                }
                else
                {
                    return float.Parse(defaultGain).ToString() + "%";
                }
            }
            catch
            {
                
            }
            return "-";
        }
        public static string defaultGain = "0";
        public Hashtable GetHashTableFromXmlString(string xmlstring)
        {
            Hashtable ht = new Hashtable();
            
            
            XmlDocument xd = new XmlDocument();
            try
            {
                xd.LoadXml(xmlstring);
                XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode("Promots");
                defaultGain = xd.SelectSingleNode("eg").SelectSingleNode("RetGain").InnerText;
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
                        ht.Add(strKey.ToUpper(), pi);
                    }
                    catch
                    { }
                }
            }
            catch { }
            return ht;
        }
        public class PolicyInfomation
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

    class EasyPrice
    {
        static public void insertPrice(float f_Bunk_Y, string distance,ListView lv)
        {
            try
            {
                if (lv.Items[0].SubItems[4].Text == "距离") lv.Items.RemoveAt(0);
            }
            catch
            {
                
            }
            ListViewItem li = new ListViewItem();
            li.BackColor = System.Drawing.Color.LightBlue;
            li.SubItems.Add("");
            li.SubItems.Add("");
            li.SubItems.Add("");
            li.SubItems.Add("距离");
            li.SubItems.Add(distance);
            li.SubItems.Add("价格");
            int price = 0;
            //price = (int)((1.5F * f_Bunk_Y + 5F) / 10) * 10;
            price = EagleString.egString.TicketPrice((int)f_Bunk_Y, 150);
            li.SubItems.Add(price.ToString());
            //price = (int)((1.3F * f_Bunk_Y + 5F) / 10) * 10;
            price = EagleString.egString.TicketPrice((int)f_Bunk_Y, 130);
            li.SubItems.Add(price.ToString());
            //for (float f = 1.0F; f > 0.29F; f = f - 0.05F)
            //for (float f = 100.0F; f > 29F; f = f - 5F)

            int dec = 5;
            if (DateTime.Compare(BookTicket.DateSearch, DateTime.Parse("2009-4-20")) >= 0)
                dec = 4;
            for (int i = 100; i > 29; i -= dec) 
            {
                //price = (int)((f * f_Bunk_Y + 500F) / 1000) * 10;
                price = EagleString.egString.TicketPrice((int)f_Bunk_Y,i);
                li.SubItems.Add(price.ToString());
            }
            lv.Items.Insert(0, li);
            avDS.init(lv, ListPolicy.avDate);
            EasyPrice.tt.initListWithDs(avDS, -1);
            
        }
        static public ePlus.Data.avDataSet avDS = new ePlus.Data.avDataSet ();
        static public ePlus.Data.TableTicket tt = new ePlus.Data.TableTicket ();
    }

    class EasyTax
    {
        string taxFile = Application.StartupPath + "\\tax.xml";
        XmlDocument xd = new XmlDocument();
        public EasyTax()
        {
            //xd = new XmlDocument();
            xd.Load(taxFile);
        }
        public string GetTaxFuel(string distance)
        {
            double d = 0;
            try
            {
                d = double.Parse(distance.Trim());
            }
            catch
            {
                return "";
            }
            XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode("local").SelectSingleNode("fuel");
            for (int i = 0; i < xn.ChildNodes.Count; i++)
            {
                string scope = xn.ChildNodes[i].InnerText;
                double a = double.Parse(scope.Split('-')[0]);
                double b = double.Parse(scope.Split('-')[1]);
                if (d >= a && d <= b) return xn.ChildNodes[i].Name.Substring(3);
            }
            return "";
        }
        public string GetTaxBuild(string airtype)
        {
            if (airtype == "") return "请上移鼠标！";
            XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode("local").SelectSingleNode("build");
            for (int i = 0; i < xn.ChildNodes.Count; i++)
            {
                string value = xn.ChildNodes[i].InnerText;
                if (value.IndexOf(airtype) >= 0)
                {
                    string r =xn.ChildNodes[i].Name.Substring(3);
                    //if (r == "10") MessageBox.Show(airtype);
                    return r;
                }
            }
            return "50";
        }
        public string GetTGQ(string al)
        {
            return GetTGQ(al, "ALL");
        }
        public string GetTGQ(string al, string bunk)
        {
            string ret = "";
            XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode("TGQ").SelectSingleNode("_" + al).SelectSingleNode(bunk);
            for (int i = 0; i < xn.ChildNodes.Count; i++)
            {
                switch (xn.ChildNodes[i].Name)
                {
                    case "T":
                        ret += "\r\n退票规定：";
                        break;
                    case "G":
                        ret += "\r\n变更规定：";
                        break;
                    case "Q":
                        ret += "\r\n订座规定：";
                        break;
                    default:
                        ret += "\r\n其它规定";
                        break;
                }
                ret += xn.ChildNodes[i].InnerText;
            }
            return ret;
        }
        public string GetSpecialRebate(string key)
        {
            XmlDocument xmldoc = new XmlDocument();
            Hashtable hashTable = new Hashtable();
            ListPolicy lp = new ListPolicy();
            hashTable = lp.GetHashTableFromXmlString(GlobalVar2.xmlPolicies);
            ListPolicy.PolicyInfomation pi = new ListPolicy.PolicyInfomation();
            pi = (ListPolicy.PolicyInfomation)hashTable[key];
            return pi.rebate;
        }
    }

    class ErpDll
    {
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]   //找子窗体   
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);     

        static public void SetErpInfo(
            string passenger,
            string sail, string prn,
            DateTime flightTime1,
            DateTime flightTime2,
            string flightNo1,
            string flightNo2,
            string bunk1,
            string bunk2)
        {
            IntPtr ParenthWnd = new IntPtr(0);
            IntPtr EdithWnd = new IntPtr(0);
            string lpszParentWindow = "机票单录入 - Microsoft Internet Explorer";
            ParenthWnd = FindWindow(null, lpszParentWindow);
            if (ParenthWnd.Equals(IntPtr.Zero)) MessageBox.Show("请先打开机票单录入窗口", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
            }

        }
    }

}
