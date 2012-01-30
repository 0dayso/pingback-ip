using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Net.Sockets;
using System.Net;

namespace ePlus.GlobalAPI
{
    class Hosts
    {
        static public void setDefault()
        {
            return;
            setHostWrite();
            deleteEagleHost();
            try
            {
                FileStream fs = new FileStream(System.Environment.SystemDirectory + "\\drivers\\etc\\hosts", FileMode.Append  , FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                
                sw.WriteLine("127.0.0.1       localhost");
                //sw.WriteLine("219.139.240.93       8b4642b69c074b2");
                sw.Close();
                fs.Close();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("不能写入Hosts文件");
                deleteHosts_CreateHosts();
            }
        }
        static public void deleteEagleHost()
        {
            return;
            FileStream fs = new FileStream(System.Environment.SystemDirectory + "\\drivers\\etc\\hosts", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs);
            List<string> ls = new List<string>();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (line.Contains("localhost")) continue;
                if (line.Contains("8b4642b69c074b2")) continue;
                if (line.Contains("www.eg66.com")) continue;
                if (line.Contains("yinge.eg66.com")) continue;
                if (line.Contains("download.eg66.com")) continue;
                if (line.Contains("wangtong.eg66.com")) continue;
                if (line.Contains("downwangtong.eg66.com")) continue;
                ls.Add(line);
            }

            sr.Close();
            fs.Close();
            fs = new FileStream(System.Environment.SystemDirectory + "\\drivers\\etc\\hosts", FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < ls.Count; i++)
            {
                sw.WriteLine(ls[i]);
            }
            sw.Close();
            fs.Close();
        }
        static public void setHost(string ip_dn)
        {
            setHostWrite();
            //if (ip_dn == "wuheg66.vicp.net")
            //{
            //    try
            //    {
            //        setDefault();
            //        deleteEagleHost();
            //        FileStream fn = new FileStream(System.Windows.Forms.Application.StartupPath + "\\wangtongip.txt", FileMode.Open);
            //        StreamReader sr = new StreamReader(fn);
            //        string wangtongip = sr.ReadLine();
            //        sr.Close ();
            //        fn.Close();


            //        FileStream fs = new FileStream(System.Environment.SystemDirectory + "\\drivers\\etc\\hosts", FileMode.Append, FileAccess.Write);
            //        StreamWriter sw = new StreamWriter(fs);
            //        sw.WriteLine("127.0.0.1       localhost");
            //        //sw.WriteLine("219.139.240.93       8b4642b69c074b2");
            //        sw.WriteLine(wangtongip, "    8b4642b69c074b2");
            //        sw.WriteLine(wangtongip + " www.eg66.com");
            //        sw.WriteLine(wangtongip + " yinge.eg66.com");
            //        sw.WriteLine(wangtongip + " download.eg66.com");
            //        sw.WriteLine("221.122.60.171" + " wangtong.eg66.com");
            //        sw.WriteLine("221.122.60.171" + " downwangtong.eg66.com");
            //        sw.Close();
            //        fs.Close();
            //    }
            //    catch
            //    {
            //        System.Windows.Forms.MessageBox.Show("不能写入Hosts文件");
            //        deleteHosts_CreateHosts();
            //    }
            //    return;
            //}
            string ip = DNS2IP(ip_dn);
            try
            {
                setDefault();
                deleteEagleHost();
                FileStream fs = new FileStream(System.Environment.SystemDirectory + "\\drivers\\etc\\hosts", FileMode.Append , FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("127.0.0.1       localhost");
                if (ip_dn == "eg66.noip.cn")                //网通
                    sw.WriteLine(ip+"    8b4642b69c074b2");
                else
                    sw.WriteLine("219.139.240.93       8b4642b69c074b2");//电信 长江数据
                sw.WriteLine(ip + " www.eg66.com");
                sw.WriteLine(ip + " yinge.eg66.com");
                sw.WriteLine(ip + " download.eg66.com");
                sw.WriteLine("221.122.60.171" + " wangtong.eg66.com");
                sw.WriteLine("221.122.60.171" + " downwangtong.eg66.com");
                sw.Close();
                fs.Close();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("不能写入Hosts文件");
                deleteHosts_CreateHosts();
            }

        }
        static public void setHostWrite()
        {
            try
            {
                string f = System.Environment.SystemDirectory + "\\drivers\\etc\\hosts";
                File.SetAttributes(f, File.GetAttributes(f) & ~FileAttributes.ReadOnly);
                //
                
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("setHostWrite()" + e.Message);
            }
        }
        static public void deleteHosts_CreateHosts()
        {
            try
            {
                File.Delete(System.Environment.SystemDirectory + "\\drivers\\etc\\hosts");
                File.Create(System.Environment.SystemDirectory + "\\drivers\\etc\\hosts");
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show("deleteHosts_CreateHosts()" + e.Message);
            }
        }
        static public string DNS2IP(string dns)
        {
            string ret;
            bool bIsIp = false;
            try
            {
                System.Net.IPAddress.Parse(dns);
                bIsIp = true;
            }
            catch (Exception ex)
            {
                bIsIp = false;
                System.Console.Write(ex.Message);
            }
            try
            {
                if (!bIsIp)
                {
                    System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(dns);
                    System.Net.IPAddress[] addr = ipEntry.AddressList;
                    ret = addr[0].ToString();
                }
                else
                    ret = dns;
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(dns + e.Message);
                ret = dns;
            }
            return ret;

        }
    }

    class SwitchConfigBetweenServer
    {
        /// <summary>
        /// 取得将要切换的目标配置的服务器
        /// </summary>
        /// <param name="config">切换配置所选择的text</param>
        /// <param name="SrvIp">out服务器地址</param>
        /// <param name="SrvPort">out服务器端口</param>
        static public string GetDestServerInfoAfterSwitch(string config,out string SrvIp,out string SrvPort )
        {
            SrvIp = SrvPort = "";
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(GlobalVar.loginXml);
            XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode("IPS");
            for (int i = 0; i < xn.ChildNodes.Count; i++)
            {
                if (config.Contains("集群"))
                {
                    if (xn.ChildNodes[i].SelectSingleNode("PeiZhi").InnerText.ToLower().Contains(config.Substring(0,6).ToLower()))
                    {
                        SrvIp = xn.ChildNodes[i].SelectSingleNode("SrvIp").InnerText;
                        if (LogonForm.isZzInternet)
                            SrvIp = GlobalVar.gbZzInternetIP;
                        SrvPort = xn.ChildNodes[i].SelectSingleNode("SrvPort").InnerText;
                        return xn.ChildNodes[i].SelectSingleNode("SrvName").InnerText;
                    }
                }
                else
                {
                    if (xn.ChildNodes[i].SelectSingleNode("PeiZhi").InnerText == config)
                    {
                        SrvIp = xn.ChildNodes[i].SelectSingleNode("SrvIp").InnerText;
                        if (LogonForm.isZzInternet)
                            SrvIp = GlobalVar.gbZzInternetIP;
                        SrvPort = xn.ChildNodes[i].SelectSingleNode("SrvPort").InnerText;
                        return xn.ChildNodes[i].SelectSingleNode("SrvName").InnerText;
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// 切换配置成功后，执行此函数，修改loginXml
        /// </summary>
        /// <param name="config">切换配置所选择的text</param>
        /// <returns>无</returns>
        static public string ModifyLoginXmlAfterSwitch(string config)
        {
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(GlobalVar.loginXml);
            XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode("IPS");
            for (int i = 0; i < xn.ChildNodes.Count; i++)
            {
                string peizhi = xn.ChildNodes[i].SelectSingleNode("PeiZhi").InnerText;
                bool bjiqun = false;
                if (config.Contains("集群"))
                {

                    bjiqun = peizhi.ToLower().Contains(config.Substring(0, 6).ToLower());
                }
                if (peizhi == config || bjiqun)
                {
                    GlobalVar.loginLC.SrvIP = xd.SelectSingleNode("eg").SelectSingleNode("SrvIp").InnerText = xn.ChildNodes[i].SelectSingleNode("SrvIp").InnerText;
                    GlobalVar.loginLC.SrvDNS = xd.SelectSingleNode("eg").SelectSingleNode("SrvDNS").InnerText = xn.ChildNodes[i].SelectSingleNode("SrvDNS").InnerText;
                    xd.SelectSingleNode("eg").SelectSingleNode("SrvPort").InnerText = xn.ChildNodes[i].SelectSingleNode("SrvPort").InnerText;
                    try
                    {
                        GlobalVar.loginLC.SrvPort = int.Parse(xd.SelectSingleNode("eg").SelectSingleNode("SrvPort").InnerText.Trim());
                    }
                    catch
                    {
                        GlobalVar.loginLC.SrvPort = 10000;
                    }
                    GlobalVar.loginLC.SrvName = xd.SelectSingleNode("eg").SelectSingleNode("SrvName").InnerText = xn.ChildNodes[i].SelectSingleNode("SrvName").InnerText;
                    
                    break;
                }
            }
            GlobalVar.loginXml = xd.InnerXml;
            return "";

        }

        public static void ReplaceGlobalSocket(string srvip, string port)
        {
            Socket sk = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sk.Connect(srvip, int.Parse(port));
            Options.GlobalVar.socketGlobal = sk;
        }
    }
}
