using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Xml;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using gs.para;
using System.Runtime.InteropServices;
using System.ComponentModel;
namespace ePlus
{
    class EagleAPI2
    {
        static public bool ExistFont(string fontname)
        {
            InstalledFontCollection insFont = new InstalledFontCollection(); 
            FontFamily[] families = insFont.Families;
            foreach (FontFamily family in families)
            {
                if (family.Name.ToUpper() == fontname.ToUpper()) return true;
            }
            return false;
        }
        static public void FixFont()
        {
            if (ExistFont("TEC1"))
            {
                MessageBox.Show("字体已经存在");
            }
            else
            {
                string fn = Environment.SystemDirectory;
                fn = fn.Substring(0, fn.LastIndexOf("\\"))+"\\fonts\\tec.ttf";
                string batfile = "c:\\fixfont.cmd";
                FileStream fs = new FileStream(batfile, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("copy " + fn + " c:\\tec.ttf");
                sw.WriteLine("del " + fn);
                sw.WriteLine("copy c:\\tec.ttf " + fn);
                sw.Close();
                fs.Close();
                PrintTicket.RunProgram("c:\\fixfont.cmd", "");
            }
        }
        public static void printchinesechar(string ch, System.Drawing.Printing.PrintPageEventArgs e, float x, float y)
        {
            float cInter = 5F;
            float eInter = 2.6F;
            Brush ptBrush = Brushes.Black;
            Font ptFontCn = new Font("TEC", 18.5F, System.Drawing.FontStyle.Regular);
            Font ptFontEn = null;
            if (EagleAPI2.ExistFont("MingLiU"))
                ptFontEn = new Font("MingLiU", 11.0F, System.Drawing.FontStyle.Regular);
            else if (EagleAPI2.ExistFont("楷体_GB2312"))
                ptFontEn = new Font("楷体_GB2312", 12.0F, System.Drawing.FontStyle.Regular);
            else if (EagleAPI2.ExistFont("Courier New"))
                ptFontEn = new Font("Courier New", 12.0F, System.Drawing.FontStyle.Regular);
            else if (EagleAPI2.ExistFont("Courier"))
                ptFontEn = new Font("Courier", 12.0F, System.Drawing.FontStyle.Regular);
            else
                ptFontEn = new Font("System", 12.0F, System.Drawing.FontStyle.Regular);
            Font ptFontNo = null;
            if (EagleAPI2.ExistFont("MingLiU"))
                ptFontNo = new Font("MingLiU", 11.0F, System.Drawing.FontStyle.Regular);

            ptFontEn = new Font("TEC", 18.0F, System.Drawing.FontStyle.Regular);
            ptFontNo = new Font("TEC", 18.0F, System.Drawing.FontStyle.Regular);

            if (ch.CompareTo("zzzzzzzzzzzzzzz") > 0)
            {
                string lkxm = ch;
                float xpos = x;
                for (int i = 0; i < lkxm.Length; i++)
                {
                    if (lkxm[i] > 'z')
                    {
                        
                        e.Graphics.DrawString(lkxm[i].ToString(), ptFontCn, ptBrush, xpos, y);//Z文
                        xpos += cInter;
                    }
                    else
                    {
                        e.Graphics.DrawString(lkxm[i].ToString(), ptFontEn, ptBrush, xpos, y + 2.5F -1F);//E文  -1F为TEC字体
                        xpos += eInter;
                    }
                }
            }
            else
                e.Graphics.DrawString(ch, ptFontEn, ptBrush, x, y);

        }
        static public void printenglisghchar(string ch, System.Drawing.Printing.PrintPageEventArgs e, float x, float y)
        {
            float cInter = 5F;//中文间距
            float eInter = 2.6F;//英文数字间距

            Brush ptBrush = Brushes.Black;
            Font ptFontCn = new Font("TEC", 18.5F, System.Drawing.FontStyle.Regular);
            Font ptFontEn = null;
            if (EagleAPI2.ExistFont("MingLiU"))
                ptFontEn = new Font("MingLiU", 11.0F, System.Drawing.FontStyle.Regular);
            else if (EagleAPI2.ExistFont("楷体_GB2312"))
                ptFontEn = new Font("楷体_GB2312", 12.0F, System.Drawing.FontStyle.Regular);
            else if (EagleAPI2.ExistFont("Courier New"))
                ptFontEn = new Font("Courier New", 12.0F, System.Drawing.FontStyle.Regular);
            else if (EagleAPI2.ExistFont("Courier"))
                ptFontEn = new Font("Courier", 12.0F, System.Drawing.FontStyle.Regular);
            else
                ptFontEn = new Font("System", 12.0F, System.Drawing.FontStyle.Regular);
            Font ptFontNo = null;
            if (EagleAPI2.ExistFont("MingLiU"))
                ptFontNo = new Font("MingLiU", 11.0F, System.Drawing.FontStyle.Regular);

            ptFontEn = new Font("TEC", 18.0F, System.Drawing.FontStyle.Regular);
            ptFontNo = new Font("TEC", 18.0F, System.Drawing.FontStyle.Regular);
            string lkxm = ch;
            for (int i = 0; i < lkxm.Length; i++)
            {
                if (lkxm[i] == '-')
                    e.Graphics.DrawString(lkxm[i].ToString(), ptFontCn, ptBrush, x + eInter * i - 0.3F, y - 1F);
                else if (lkxm[i] > '9')
                    e.Graphics.DrawString(lkxm[i].ToString(), ptFontEn, ptBrush, x + eInter * i, y - 1F);//-1F为TEC字体
                else
                    e.Graphics.DrawString(lkxm[i].ToString(), ptFontNo, ptBrush, x + eInter * i, y - 1F);//-1F为TEC字体
            }
        }
        /// <summary>
        /// 提示新订单，同个电脑客户端共用，不分用户名
        /// </summary>
        /// <returns></returns>
        static public bool initNewOrder()
        {
            XmlDocument xd = new XmlDocument();
            try
            {
                xd.Load(System.Windows.Forms.Application.StartupPath + "\\options.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开配置文件 options.xml 失败，可能需要重新安装。"
                    + System.Environment.NewLine + ex.Message);
                return false;
            }
            XmlNode xn = xd.SelectSingleNode("eg");
            try
            {
                xn = xn.SelectSingleNode("neworder");
                if (xn == null) throw new Exception();
            }
            catch
            {
                XmlElement xe;
                xe = xd.CreateElement("neworder");
                xe.InnerText = "0";
                if (xn == null) xn = xd.SelectSingleNode("eg");
                xn.AppendChild(xe);
                xd.Save(System.Windows.Forms.Application.StartupPath + "\\options.xml");
            }
            xn = xd.SelectSingleNode("eg").SelectSingleNode("neworder");
            bool ret= (xn.InnerText.Trim() == "1");
            GlobalVar.b_提示新订单 = ret;
            return ret;
        }
        /// <summary>
        /// 在XML文档中插入新项
        /// </summary>
        /// <param name="filename">xml文件</param>
        /// /// <param name="firstnode">被插入项的路径eg~node1~childnode1中的第一个节点eg</param>
        /// <param name="xmlpath">被插入项的路径node1~childnode1,不包含第一个节点</param>
        /// <param name="intVar">该项的innertext初始值</param>
        /// <returns></returns>
        static public string initOneXmlItem(string filename, string firstnode,string xmlpath, string intVar)
        {
            string innertext = "";
            XmlDocument xd = new XmlDocument();
            xd.Load(filename);
            XmlNode xn =xd.SelectSingleNode(firstnode);
            string[] nodes = xmlpath.Split('~');
            for (int i = 0; i < nodes.Length; i++)
            {
                XmlNode tempxn;
                tempxn = xn.SelectSingleNode(nodes[i]);
                if (tempxn == null)
                {
                    XmlElement xe;
                    xe = xd.CreateElement(nodes[i]);
                    if (i + 1 == nodes.Length)//最后一个
                    {
                        xe.InnerText = intVar;
                        
                    }
                    xn.AppendChild(xe);
                }
                xn = xn.SelectSingleNode(nodes[i]);
            }
            xd.Save(filename);
            innertext = xn.InnerText;
            return innertext;
        }
        /// <summary>
        /// 在姓名和航段之前少个换行符的情况：自动添加一个换行
        /// </summary>
        /// <param name="rtResult"></param>
        /// <returns></returns>
        static public string NewLineBetweenNameAndFlight(string rtResult)
        {
            string ret = rtResult;
            string pnr = EagleAPI.etstatic.Pnr;
            if (!EagleAPI.IsRtCode(pnr)) return ret;
            if (GlobalVar.b_rtCommand)
            {
                string limitStr = "15.SSR FOID                                                                    +";
                int limitInt = limitStr.Length + 1;//可能要多个"\r"
                try
                {
                    string[] lines = rtResult.Split('\r');
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string lineString = lines[i];
                        int bytelenth = System.Text.Encoding.Default.GetBytes(lineString).Length;
                        if (bytelenth > limitInt)
                        {
                            int posLine = lineString.IndexOf(pnr.ToUpper());
                            if (posLine > -1)
                            {
                                posLine += 5;
                                int pos = 0;
                                for (int j = 0; j < i; j++)
                                {
                                    pos += lines[j].Length + 1;
                                }
                                pos += posLine;
                                while (ret[pos + 1] == ' ')
                                {
                                    pos += 1;
                                }
                                bool addSpace = false;
                                if (ret.IndexOf('.', pos) - pos == 1) addSpace = true; ;
                                int posr = ret.IndexOf('\r', pos);
                                int posn = ret.IndexOf('\n', pos);

                                //if ((posr >= 0 && posr - pos <= 5) || (posn >= 0 && posn - pos <= 5)) break;
                                ret = ret.Insert(pos, addSpace?" \r":"\r");
                                break;
                            }
                        }
                    }
                }
                catch
                {
                }
            }

            return ret;
        }
        /// <summary>
        /// 修正本地数据库，可能增加列
        /// </summary>
        static public void fixLocalDatabase()
        {
            OleDbConnection cn = new OleDbConnection();
            try
            {
                string ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.Windows.Forms.Application.StartupPath + "\\data.mdb;";
                cn.ConnectionString = ConnStr;
                cn.Open();
                try//增加Remark1列(票面,机建,燃油,返点),返点以0-100的数
                {
                    string sel = "select Remark1 from t_SimpleBookPNR";
                    DataTable dt = new DataTable();
                    OleDbCommand cmd = new OleDbCommand(sel, cn);
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    string alter = "ALTER TABLE t_SimpleBookPNR ADD COLUMN Remark1 CHAR(200)";
                    OleDbCommand cmd = new OleDbCommand(alter, cn);
                    cmd.ExecuteNonQuery();
                }
                cn.Close();
            }
            catch
            {
                if (cn.State == ConnectionState.Open) cn.Close();
            }
        }
        /// <summary>
        /// 防止重复提交！而导致占用配置，在提交电子客票信息线程中使用！
        /// </summary>
        /// <param name="pnr"></param>
        /// <returns></returns>
        static public bool CheckSubmitedPnrInHashTable(string pnr)
        {
            try
            {
                if (GlobalVar2.gbHashTableOfTheSubmittingPnr.ContainsKey(pnr))
                {
                    DateTime dt = (DateTime)(GlobalVar2.gbHashTableOfTheSubmittingPnr[pnr]);
                    TimeSpan ts = DateTime.Now - dt;
                    int inteval = ts.Minutes;
                    if (inteval > 10)
                    {
                        
                        GlobalVar2.gbHashTableOfTheSubmittingPnr.Remove(pnr);
                        GlobalVar2.gbHashTableOfTheSubmittingPnr.Add(pnr, DateTime.Now);
                        return false;
                    }
                    EagleAPI.LogWrite("CATCH:    CheckSubmitedPnrInHashTable存在相同PNR在HASHTABLE中");
                    return true;
                }
                else
                {
                    GlobalVar2.gbHashTableOfTheSubmittingPnr.Add(pnr, DateTime.Now);
                    return false;
                }
            }
            catch
            {
                EagleAPI.LogWrite("CATCH:    CheckSubmitedPnrInHashTable");
                return false;
            }
        }
        /// <summary>
        /// 取航班及舱位对应折扣及返点"70~8",基数为100
        /// </summary>
        /// <param name="flightno"></param>
        /// <param name="bunk"></param>
        /// <returns></returns>

        static public string getRebateFromPolicyXml(string flightno, string bunk)
        {
            string xmlstring = GlobalVar2.xmlPolicies;
            XmlDocument xd = new XmlDocument();
            try
            {
                xd.LoadXml(xmlstring);
                XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode("Promots");
                for (int i = 0; i < xn.ChildNodes.Count; i++)
                {
                    try
                    {
                        ListPolicy.PolicyInfomation pi = new ListPolicy.PolicyInfomation();
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

                        if (strKey.ToUpper() == flightno + "-" + bunk)
                        {
                            return pi.rebate + "~" + pi.usergain;
                        }
                    }
                    catch
                    { }
                }
            }
            catch { }
            return "";
        }
        /// <summary>
        /// 取本地計算機上對應票價"fareY~distance"
        /// </summary>
        /// <param name="fromto"></param>
        /// <returns></returns>
        static public string getFareLocal(string fromto)
        {
            string ret = "";
            try
            {
                OleDbConnection mycon = new OleDbConnection();
                mycon.ConnectionString =
                    "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\data.mdb;";
                mycon.Open();
                OleDbCommand cmd = new OleDbCommand
                    ("select * from t_fc where [From]='" + fromto.Substring(0, 3) + "'" + "and [To]='" + fromto.Substring(3) + "'", mycon);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                DataTable dtTmp = new DataTable();
                adapter.Fill(dtTmp);
                if (dtTmp.Rows.Count > 0)
                {
                    ret =
                        dtTmp.Rows[0]["BunkY"].ToString() + "~" + dtTmp.Rows[0]["BunkF"].ToString();
                }
                mycon.Close();
            }
            catch
            {
            }
            return ret;
        }
        /// <summary>
        /// 取eagle服務器上的對應票價"fareY~distance"
        /// </summary>
        /// <param name="fromto"></param>
        /// <returns></returns>
        static public string getFareServer(string fromto)
        {
            return EagleString.EagleFileIO.PriceOf(fromto).ToString() + "~" + EagleString.EagleFileIO.DistanceOf(fromto);
            string ret = "";
            try
            {
                EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
                NewPara np = new NewPara();
                np.AddPara("cm", "GetFC");
                np.AddPara("FROM", fromto.Substring(0, 3));
                np.AddPara("TO", fromto.Substring(3, 3));
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                if (strRet != "")
                {
                    NewPara np1 = new NewPara(strRet);
                    if (np1.FindTextByPath("//eg/cm") == "RetGetFC")
                    {
                        string tf = np1.FindTextByPath("//eg/BUNKF");
                        string tc = np1.FindTextByPath("//eg/BUNKC");
                        string ty = np1.FindTextByPath("//eg/BUNKY");
                        if (tf != "" && tc != "" && ty != "")
                        {
                            try
                            {
                                return ty + "~" + tf;
                            }
                            catch
                            {

                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return ret;
        }
        /// <summary>
        /// 取對應航班及艙位的折扣信息
        /// </summary>
        /// <param name="airline"></param>
        /// <param name="bunk"></param>
        /// <returns></returns>
        static public string getRebateByAirlineAndBunk(string airline,string bunk)
        {
            string ret = "0";
            //string[] rebates =
            //    new string[] { "150", "130", "100", "95", "90", "85", "80", "75", "70", "65", "60", "55", "50", "45", "40", "35", "30" };
            FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath + "\\bunks.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            while (!sr.EndOfStream)
            {
                string linestring = sr.ReadLine();
                if (linestring.Substring(0, 2).ToUpper() == airline.Substring(0,2).ToUpper())
                {
                    int pos = linestring.Substring(3).IndexOf(bunk);
                    if (pos < 0)
                    {
                        ret = "0";
                        break;
                    }
                    else
                    {
                        //ret = rebates[pos];//commentted by chenqj
                        ret = EagleString.EagleFileIO.arrayOfRebate2[pos].ToString();
                        break;
                    }
                }
            }
            sr.Close();
            fs.Close();
            return ret;
        }
        //提交人~ＰＮＲ~提交时间
        static public bool IsPnrSubmitted(string pnr)
        {
            try
            {
                FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath + "\\submitted.rnp",
                    FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                while (!sr.EndOfStream)
                {
                    string lineString = sr.ReadLine();
                    string[] str = lineString.Split('~');
                    if (str.Length != 3) continue;
                    if (pnr.ToUpper().Trim() == str[1].ToUpper().Trim())
                    {
                        DateTime dt = DateTime.Parse(str[2].Trim());
                        TimeSpan ts = DateTime.Now - dt;
                        if (ts.TotalDays < 7)
                        {
                            MessageBox.Show("您在本次登陆已经提交过一次该PNR\n\n提交者:  " + str[0] + "\n\n提交时间:" + str[2],
                                "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //return true;
                        }
                    }
                }
                sr.Close();
                fs.Close();

            }
            catch { }
            return false;
        }
        //将已经提交的pnr保存到列表文件中
        static public void SaveSubmittedPnr(string pnr)
        {
            try
            {
                FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath + "\\submitted.rnp",
        FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(GlobalVar.loginName + "~" + pnr + "~" + DateTime.Now.ToString());
                sw.Close();
                fs.Close();
            }
            catch
            {
            }
        }
        //删除pnr列表文件
        static public void DeleteSubmittedPnr()
        {
            try
            {
                string fn = System.Windows.Forms.Application.StartupPath + "\\submitted.rnp";
                if(File.Exists(fn)) File.Delete(fn);
            }
            catch
            {
            }
        }
        //检查订票平台安装文件是否齐全，若不全，则自动修正
        static public bool ValidateFiles()
        {
            bool haveLost = false;//是否有需要重新下载的文件
            string lostfileIndex = "";
            string[] files = new string[] { "py.dat", "a4print.jpg", "gs.jpg", "citycode.mdb", "Data.mdb", "database.mdb", "EagleLog.mdb",
                "Quenes.mdb","CityCode.mp3","CmdList.mp3","ptconfig.mp3","restictions.mp3","DiamondBlue.ssk","EagleSkin.ssk","app.config",
            "config.xml","ConnectCFG.xml","Options.xml","PTReceipt.XML","tax.xml","tmp.xml","update.xml","EagleIns.ini","AutoUpdatePath.txt",
            "bunks.txt","citycode.txt","eticketManagerExportSort.txt","etmconfig.txt","AutoUpdate.exe","Eagle.exe","EagleWangtong.exe",
            "Data.DLL","ErpPlugin.dll","htmldoc.dll","IrisSkin2.dll","LogoPicture.dll","MovableLabel.DLL","Options.DLL","TestDll.dll","zip.dll"};
            for (int i = 0; i < files.Length; i++)
            {
                if (!File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + files[i])) lostfileIndex += i.ToString() + ",";
            }
            if (lostfileIndex.Length > 0)
            {
                //MessageBox.Show("您的订票系统中某些文件丢失，将自动修复更新文件！若修复失败，请重新安装！");
            }
            else return false;
            XmlDocument xd = new XmlDocument();
            xd.Load(System.Windows.Forms.Application.StartupPath + "\\" + "update.xml");
            XmlNode xn = xd.SelectSingleNode("Files");
            string[] lostfiles = lostfileIndex.Substring(0,lostfileIndex.Length-1).ToLower().Split(',');
            for (int i = 0; i < xn.ChildNodes.Count; i++)
            {
                XmlNode cn = xn.ChildNodes[i];
                string fn = cn.SelectSingleNode("FileName").InnerText.Trim().ToLower();
                for (int j = 0; j < lostfiles.Length; j++)
                {
                    if (fn == files[int.Parse(lostfiles[j])].ToLower())
                    {
                        cn.SelectSingleNode("Date").InnerText = "1999-9-9";
                        haveLost = true;
                        break;
                    }
                }
            }

            if (haveLost)
            {
                if (MessageBox.Show("您的系统中某些文件丢失，是否自动更新？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    xd.Save(System.Windows.Forms.Application.StartupPath + "\\update.xml");
                    System.Diagnostics.Process.Start(Application.StartupPath + "\\AutoUpdate.exe");
                }
                else
                    return false;
            }

            return haveLost;
            //MessageBox.Show("更新文件修复完毕，请重新启动订票程序！");
        }
        static public string USAS2GB(byte c1, byte c2)
        {
            byte usas1, usas2, gb1, gb2, tmp;
            if (c1 >= 0x80 || c2 >= 0x80)
                return System.Text.Encoding.ASCII.GetString(new byte[] { c1, c2 });
            usas1 = c1;
            usas2 = c2;
            if (usas1 >= 0x25 && usas1 <= 0x28)
            {
                tmp = usas1;
                usas1 = usas2;
                usas2 = (byte)(tmp + 10);
            }
            if (usas1 > 0x24)
                gb1 = (byte)(usas1 - 0x20 + 0xa0);
            else
                gb1 = (byte)(usas1 - 0x20 + 14 + 0xa0);
            gb2 = (byte)(usas2 - 0x20 + 0xa0);
            c1 = gb1;
            c2 = gb2;
            string ret = System.Text.Encoding.Default.GetString(new byte[] { c1, c2 });
            return ret;
        }


    }
}
//Mu7g$(
