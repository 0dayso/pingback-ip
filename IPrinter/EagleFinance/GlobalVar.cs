using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Net.Sockets;
using System.Net;
using System.Collections;

namespace EagleFinance
{
    public enum AGENTS {EAGLE,ZHENGZHOU };
    class GlobalVar
    {
        
        public static bool bModified = false;
        public static string ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\database.mdb;";
        public static OleDbConnection cn = new OleDbConnection(ConnStr);
        public static string gbAgentPolicy = "2";
        public static string gbMaxPolicy = "3";
        public static int countStat = 0;
        public static Hashtable ht = new Hashtable();
        public static AGENTS agent = AGENTS.EAGLE;//modified by chenqj
    }
        class hashkey
        {
            public string username = "";
            public string flightno = "";
            public string bunk = "";
            public string date = "";
        }
        class hashvalue
        {
            public string maxGain = "";
            public string userGain = "";
        }

    class GlobalApi
    {
        public static void Incoming(string office, long numbeg, long numend)
        {
            for (long i = numbeg; i <= numend; i++)
            {
                try
                {
                    //FormMain.LABEL.Text 
                    //    = string.Format("正在入库--第{0}张/共{1}张,票号{2}", i - numbeg, numend - numbeg, i);
                    //Application.DoEvents();
                    string cmString = "insert into etickets (OFFICE,TKTNUMBER,IMPORTCOUNT) values ('{0}','{1}',0)";
                    cmString = string.Format(cmString, office.Trim(), i.ToString());
                    OleDbCommand cmd = new OleDbCommand(cmString, GlobalVar.cn);
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception(i.ToString() + "不能入库，可能有重复");
                }
            }
        }

        public static string getOffice(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            string content = sr.ReadToEnd();
            sr.Close();
            string str0 = "OFFICE : ";
            string str1 = "IATA NUMBER : ";
            int pos0 = content.IndexOf(str0);
            int pos1 = content.IndexOf(str1);
            if (pos0 < 0 || pos1 < 0 || pos1 <= pos0) throw new Exception("取OFFICE文件错误！");
            else return content.Substring(pos0 + str0.Length, pos1 - pos0 - str0.Length).Trim();
            
        }
        public static string getDateOfSale(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            string content = sr.ReadToEnd();
            sr.Close();
            string str0 = "DATE   : ";
            string str1 = "AIRLINE:   ";
            int pos0 = content.IndexOf(str0);
            int pos1 = content.IndexOf(str1);
            if (pos0 < 0 || pos1 < 0 || pos1 <= pos0) throw new Exception("取销售日期文件错误！");
            else
            {
                string ttt = content.Substring(pos0 + str0.Length, pos1 - pos0 - str0.Length).Trim();
                return convertDateString(ttt);
            }

        }
        public static string convertDateString(string datestring)
        {
            int day = int.Parse(datestring.Substring(0, 2));
            string smonth = datestring.Substring(2, 3);
            int year = int.Parse("20" + 
                (datestring.Substring(5).Length == 2 ? datestring.Substring(5) : DateTime.Now.Year.ToString().Substring(2)));
            int month = 0;
            switch (smonth.ToUpper())
            {
                case "JAN":
                    month = 1;
                    break;
                case "FEB":
                    month = 2;
                    break;
                case "MAR":
                    month = 3;
                    break;
                case "APR":
                    month = 4;
                    break;
                case "MAY":
                    month = 5;
                    break;
                case "JUN":
                    month = 6;
                    break;
                case "JUL":
                    month = 7;
                    break;
                case "AUG":
                    month = 8;
                    break;
                case "SEP":
                    month = 9;
                    break;
                case "OCT":
                    month = 10;
                    break;
                case "NOV":
                    month = 11;
                    break;
                case "DEC":
                    month = 12;
                    break;
                default:
                    throw new Exception("读取日期错误！");                    
            }
            DateTime dt = new DateTime(year, month, day);
            return dt.ToShortDateString();
        }
        public static string getTotalTicket(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            string content = sr.ReadToEnd();
            sr.Close();
            string str0 = "TOTAL TICKETS:";
            string str1 = "(";
            int pos0 = content.IndexOf(str0);
            int pos1 = content.IndexOf(str1,pos0);
            if (pos0 < 0 || pos1 < 0 || pos1 <= pos0) throw new Exception("取总票数文件错误！");
            else return content.Substring(pos0 + str0.Length, pos1 - pos0 - str0.Length).Trim();

        }
        public static string getNORMALTICKETS(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            string content = sr.ReadToEnd();
            sr.Close();
            string str0 = "NORMAL TICKETS";
            string str1 = "*===================================================";
            int pos0 = content.IndexOf(str0);
            int pos1 = content.IndexOf(str1, pos0);
            if (pos0 < 0 || pos1 < 0 || pos1 <= pos0) throw new Exception("取统计结果文件错误！");
            else return content.Substring(pos0 + str0.Length, pos1 - pos0 - str0.Length).Trim();

        }
        public static string getTprContent(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            string content = sr.ReadToEnd();
            sr.Close();
            return content;
        }
        public static void importOneLineFromTpr(string line,string filename)
        {
            try
            {

                if (line.Trim().Length < 63) return;
                if (line[0] > '9' || line[0] < '0') return;
                string str = line.Trim().ToUpper();
                string tktnumber = str.Substring(0, 14).Trim();
                string orig_dest = str.Substring(18, 9).Trim();
                string collection = str.Substring(27, 10).Trim();
                string tax = str.Substring(37, 10).Trim();
                string comm = str.Substring(47, 11).Trim();//没导入
                string pnr = str.Substring(58, 6).Trim();
                if (pnr.Length != 5)
                {
                    pnr = str.Substring(58);
                    string[] p = pnr.Split(' ');
                    for (int i = 0; i < p.Length; i++)
                    {
                        if (p[i].Trim().Length == 5)
                        {
                            pnr = p[i].Trim();
                            break;
                        }
                    }
                }
                string dateofsale = GlobalApi.getDateOfSale(filename);
                //判断票号是否已经入库
                string number = tktnumber.Substring(4);
                {
                    string cmdString = string.Format("select * from etickets where TKTNUMBER='{0}'", number);
                    OleDbCommand cmd = new OleDbCommand(cmdString, GlobalVar.cn);
                    DataTable dt = new DataTable();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    adapter.Fill(dt);
                    if (dt.Rows.Count != 1) throw new Exception(number + "票号未入库");
                }
                //更新数据库
                {
                    FormMain.LABEL.Text = string.Format("正在导入tpr报表:票号{0}", tktnumber);
                    Application.DoEvents();
                    string cmdString = "update etickets set ";
                    if (tktnumber.Length == 14) 
                        cmdString += string.Format("[E-TKT-NUMBER]='{0}'", tktnumber);
                    if (orig_dest.Contains("REFUND")) 
                        cmdString += string.Format(", [ORIG-DEST]='{0}',[TKT-FLAG]='4'", orig_dest);//cmdString += ", [TKT-FLAG]='4'";
                    else if (orig_dest.Contains("VOID")) 
                        cmdString += string.Format(", [ORIG-DEST]='{0}',[TKT-FLAG]='2'", orig_dest);//cmdString += ", [TKT-FLAG]='2'";
                    else if (orig_dest.Length == 7)
                        cmdString += string.Format(", [ORIG-DEST]='{0}',[TKT-FLAG]='1'", orig_dest);//cmdString += ", [TKT-FLAG]='1'";
                    if (dateofsale != "") 
                        cmdString += string.Format(", DATEOFSALE='{0}'", dateofsale);
                    if (collection.Length != 0 && collection.IndexOf('.')>0) 
                        cmdString += string.Format(", COLLECTION={0}", int.Parse(collection.Split('.')[0]));
                    if (tax.Length != 0 && tax.IndexOf('.')>0) 
                        cmdString += string.Format(", TAXS={0}", int.Parse(tax.Split('.')[0]));
                    if (pnr.Length == 5) 
                        cmdString += string.Format(", PNR='{0}'", pnr);
                    cmdString += string.Format(", IMPORTCOUNT=IMPORTCOUNT+1");
                    cmdString += string.Format(" where TKTNUMBER='{0}'", number);
                    OleDbCommand cmd = new OleDbCommand(cmdString, GlobalVar.cn);
                    cmd.ExecuteNonQuery();
                    GlobalVar.countStat++;
                }
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
                GlobalApi.appenderrormessage("importOneLineFromTpr: " +ex.Message);
            }
        }
        public static void importAllLineFromTpr(string filename)
        {
            GlobalVar.bModified = true;
            string content = GlobalApi.getTprContent(filename);
            string[] lines = content.Split('\n');
            GlobalVar.countStat = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                try
                {
                    GlobalApi.importOneLineFromTpr(lines[i],filename);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            FormMain.LABEL.Text = string.Format("导入TPR报表完成");
            GlobalApi.appenderrormessage(string.Format ("导入TPR报表完成，文件名为" + filename + "共{0}张电子客票",GlobalVar.countStat));
            GlobalVar.countStat = 0;
        }
        public static string getTableNameOfExcel(string filename)
        {
            string ret = "";
            string ConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename.Replace("/", "\\") + ";Extended Properties=Excel 8.0;";
            OleDbConnection tempcn = new OleDbConnection();
            tempcn.ConnectionString = ConnString;
            tempcn.Open();
            DataTable tables = tempcn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            ret = "[" + tables.Rows[0]["TABLE_NAME"].ToString().Trim() + "]";
            tempcn.Close();
            return ret;

        }
        public static void importAbmsReport(string filename)
        {
            GlobalVar.bModified = true;
            int importsucc = 0;
            string eName = getTableNameOfExcel(filename);
            DataTable dt = new DataTable();
            OleDbDataAdapter adapt = new OleDbDataAdapter
                ("SELECT * FROM " + eName,
                "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename.Replace("/", "\\") + ";Extended Properties=Excel 8.0;");
            adapt.Fill(dt);
            string saleDate = "";
            string username = "";
            string useraccount = "";
            string pnr = "";
            string flightnumber = "";
            string flightbunk = "";
            string flightdate = "";
            string flightcity = "";
            string eticketnumber = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    string temp = dt.Rows[i][0].ToString();
                    if (temp != "")
                        saleDate = temp.Split(new string[] { "at" }, StringSplitOptions.RemoveEmptyEntries)[0];

                    temp = dt.Rows[i][1].ToString();
                    if (temp != "")
                        username = temp.Trim();

                    temp = dt.Rows[i][2].ToString();
                    if (temp != "")
                        useraccount = temp.Trim();

                    temp = dt.Rows[i][4].ToString();
                    if (temp != "")
                        pnr = temp.Trim();

                    temp = dt.Rows[i][5].ToString();
                    if (temp != "")
                        flightnumber = temp.Trim();
                    temp = dt.Rows[i][6].ToString();
                    if (temp != "")
                        flightbunk = temp.Trim();
                    temp = dt.Rows[i][7].ToString();
                    if (temp != "")
                        try { flightdate = DateTime.Parse(temp.Trim()).ToShortDateString().Substring(5); }
                        catch { flightdate = temp.Trim(); }
                    temp = dt.Rows[i][9].ToString();
                    if (temp != "")
                        flightcity = temp.Trim();
                    eticketnumber = dt.Rows[i][10].ToString().Trim();
                    //判断票号是否已经存在
                    {
                        //string cmdString = string.Format("select * from etickets where TKTNUMBER='{0}'", ets[j].Substring(4).Trim());
                        string cmdString = string.Format
                            ("select * from etickets where PNR='{0}' and DATEOFSALE>=cdate('{1}') and DATEOFSALE<cdate('{2}')", 
                            pnr, saleDate, DateTime.Parse(saleDate).AddDays(1).ToShortDateString());
                        OleDbCommand cmd = new OleDbCommand(cmdString, GlobalVar.cn);
                        DataTable dt1 = new DataTable();
                        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                        adapter.Fill(dt1);
                        if (dt1.Rows.Count < 1) throw new Exception(dt1.Rows[i][3].ToString().Trim() + "票号未入库或者未从TPR报表中导入");
                    }
                    //更新表
                    {
                        FormMain.LABEL.Text = string.Format("正在导入ABMS报表{1}/{2}:票号{0}", eticketnumber,importsucc,dt.Rows.Count);
                        Application.DoEvents();
                        string cmdString = string.Format
                            ("update etickets set USERNAME='{0}',USERACCOUNT='{1}',"
                            + "FLIGHTNUMBER='{5}',FLIGHTBUNK='{6}',FLIGHTDATE='{7}',FLIGHTCITY='{8}'"
                            + " where PNR='{2}' and DATEOFSALE>=cdate('{3}') and DATEOFSALE<cdate('{4}')",
                            username,//theAgentName,
                            useraccount,
                            pnr, DateTime.Parse(saleDate).AddDays(-3).ToShortDateString()
                            , DateTime.Parse(saleDate).AddDays(1).ToShortDateString()
                            //5,6,7,8
                            , flightnumber, flightbunk, flightdate, flightcity
                            );
                        OleDbCommand cmd = new OleDbCommand(cmdString, GlobalVar.cn);
                        cmd.ExecuteNonQuery();
                        importsucc++;
                    }
                }
                catch (Exception ex)
                {
                    GlobalApi.appenderrormessage(ex.Message);
                }
            }
            FormMain.LABEL.Text = string.Format("导入ABMS报表完成,共{0}张,其中成功导入{1}张  ", dt.Rows.Count,importsucc);
            GlobalApi.appenderrormessage(FormMain.LABEL.Text + filename);
        }
        public static void importEagleReport(string filename)
        {
            GlobalVar.bModified = true;
            int importSucc = 0;
            string theAgentName = "";
            string eName = getTableNameOfExcel(filename);
            DataTable dt = new DataTable();
            OleDbDataAdapter adapt = new OleDbDataAdapter
                ("SELECT * FROM "+eName, 
                "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename.Replace("/", "\\") + ";Extended Properties=Excel 8.0;");
            adapt.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    //if (i == 0) continue;
                    string temp = dt.Rows[i][2].ToString();
                    if (dt.Rows[i][2] == null || dt.Rows[i][2].ToString().Trim() == "")
                    {//第二列为空,则表示该行为代理商名称行
                        theAgentName = dt.Rows[i][0].ToString().Split(':')[0];
                        continue;
                    }
                    //用户	名称	操作时间	PNR	行程单号 脱机打印 电子票号	航班1	舱位1	航班2	舱位2	城市对1	城市对2	乘机日1	乘机日2	总价	客票状态	燃油	基建	票价

                    string receiptNumber = dt.Rows[i]["行程单号"].ToString().Trim();
                    string isOffline = dt.Rows[i]["脱机打印"].ToString().Trim();
                    if (isOffline == "是")
                        isOffline = "1";
                    else
                        isOffline = "0";

                    string useraccount = dt.Rows[i]["用户"].ToString().Trim();
                    string username = dt.Rows[i]["名称"].ToString().Trim();
                    string pnr = dt.Rows[i]["PNR"].ToString().Trim();
                    string dateofsale = DateTime.Parse(dt.Rows[i]["操作时间"].ToString().Trim()).ToShortDateString();
                    string etickets = dt.Rows[i]["电子票号"].ToString().Trim();
                    string[] ets = null;// etickets.Split(';');
                    string flightnumber = dt.Rows[i]["航班1"].ToString().Trim()
                        + (dt.Rows[i]["航班2"].ToString().Trim() == "VOID" ? "" : ("/" + dt.Rows[i]["航班2"].ToString().Trim()));
                    string flightbunk = dt.Rows[i]["舱位1"].ToString().Trim()
                        + (dt.Rows[i]["舱位2"].ToString().Trim() == "" ? "" : ("/" + dt.Rows[i]["舱位2"].ToString().Trim()));
                    string flightcity = dt.Rows[i]["城市对1"].ToString().Trim()
                        + (dt.Rows[i]["城市对2"].ToString().Trim() == "" ? "" : ("/" + dt.Rows[i]["城市对2"].ToString().Trim()));
                    string tt1 = dt.Rows[i]["乘机日1"].ToString().Trim();
                    string tt2 = dt.Rows[i]["乘机日2"].ToString().Trim();
                    try
                    {
                        tt1 = DateTime.Parse(tt1).ToShortDateString().Substring(5);
                        if (!(tt2 == "" || tt2 == null))
                            tt2 = DateTime.Parse(tt2).ToShortDateString().Substring(5);
                    }
                    catch
                    {
                    }
                    string flightdate = tt1
                        + (tt2 == "" ? "" : ("/" + tt2));

                    if (etickets.Length > 0) ets = etickets.Split(';');
                    //for (int j = 0; j < ets.Length; j++)//根据票号
                    {
                        //判断票号是否已经存在
                        {
                            //string cmdString = string.Format("select * from etickets where TKTNUMBER='{0}'", ets[j].Substring(4).Trim());
                            string cmdString = string.Format(
                                "select * from etickets where PNR='{0}' and DATEOFSALE>=cdate('{1}') and DATEOFSALE<cdate('{2}')", 
                                pnr,
                                DateTime.Parse(dateofsale).AddDays(-1).ToShortDateString(), 
                                DateTime.Parse(dateofsale).AddDays(2).ToShortDateString()
                                );
                            OleDbCommand cmd = new OleDbCommand(cmdString, GlobalVar.cn);
                            DataTable dt1 = new DataTable();
                            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                            adapter.Fill(dt1);
                            if (dt1.Rows.Count < 1) throw new Exception(dt1.Rows[i][3].ToString().Trim() + "票号未入库或者未从TPR报表中导入");
                        }
                        //更新表
                        {
                            FormMain.LABEL.Text = string.Format("正在导入Eagle报表{1}/{2}:票号{0}", etickets,importSucc,dt.Rows.Count);
                            Application.DoEvents();
                            //string cmdString = string.Format("update etickets set AGENTNAME='{0}',USERACCOUNT='{1}' where TKTNUMBER='{2}'",
                            //    theAgentName,
                            //    useraccount,
                            //    ets[j].Substring(4).Trim());
                            string cmdString = string.Format
                                ("update etickets set AGENTNAME='{0}',USERACCOUNT='{1}',"
                                +"FLIGHTNUMBER='{5}',FLIGHTBUNK='{6}',FLIGHTDATE='{7}',FLIGHTCITY='{8}',USERNAME='{9}',"
                                + "receiptNumber = '{10}', IsOffline = '{11}'"
                                +" where PNR='{2}' and DATEOFSALE>=cdate('{3}') and DATEOFSALE<=cdate('{4}')",
                                theAgentName,
                                useraccount,
                                pnr, 
                                DateTime.Parse(dateofsale).AddDays(-3).ToShortDateString()
                                ,DateTime.Parse(dateofsale).AddDays(2).ToShortDateString()
                                //5,6,7,8
                                ,flightnumber,flightbunk,flightdate,flightcity,username,receiptNumber,isOffline
                                );
                            OleDbCommand cmd = new OleDbCommand(cmdString, GlobalVar.cn);
                            cmd.ExecuteNonQuery();
                            importSucc++;
                        }
                    }
                }
                catch(Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    GlobalApi.appenderrormessage(ex.Message);
                }
            }
            FormMain.LABEL.Text = string.Format("导入Eagle报表完成,共{0}张，其中成功导入{1}张    ", dt.Rows.Count,importSucc);
            GlobalApi.appenderrormessage(FormMain.LABEL.Text + filename);
        }

        public static void importEagleEasyReport(string filename)
        {
            string eName = getTableNameOfExcel(filename);
            DataTable dt = new DataTable();
            OleDbDataAdapter adapt = new OleDbDataAdapter
                ("SELECT * FROM " + eName,
                "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename.Replace("/", "\\") + ";Extended Properties=Excel 8.0;");
            adapt.Fill(dt);
            string pnr = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    string newpnr = dt.Rows[i]["PNR"].ToString().Trim();
                    if (pnr == newpnr) continue;
                    else pnr = newpnr;
                    if (pnr.Length != 5) continue;
                    string agentname = dt.Rows[i][31].ToString().Trim();
                    string useraccount = dt.Rows[i][32].ToString().Trim();
                    string username = dt.Rows[i][33].ToString().Trim();
                    string dateofsale = dt.Rows[i][7].ToString().Trim();//"销售日期"
                    string cmdString = string.Format
                                ("update etickets set AGENTNAME='{0}',USERACCOUNT='{4}',USERNAME='{5}'"

                                + " where PNR='{1}' and DATEOFSALE>=cdate('{2}') and DATEOFSALE<cdate('{3}')",
                                agentname, pnr, dateofsale, DateTime.Parse(dateofsale).AddDays(3).ToShortDateString(),
                                useraccount,username);
                    OleDbCommand cmd = new OleDbCommand(cmdString, GlobalVar.cn);
                    int ups = cmd.ExecuteNonQuery();
                    GlobalApi.appenderrormessage("导入订单状态：PNR为" + pnr + "对应导入" + ups.ToString() + "张电子客票！");


                }
                catch
                {
                }
            }
        }

        public static bool checkUsingEagleBookSystem()
        {
            TcpClient client;
            try
            {
                client = new TcpClient(Dns.GetHostName(), 51888);
                return true;

            }
            catch
            {
                MessageBox.Show("与服务器建立连接失败，请检查是否打开eagle订票系统的财务接口");
                return false;
            }
        }
        public static void appenderrormessage(string str)
        {
            try
            {
                FormMain.errorForm.lb.Items.Add(DateTime.Now.ToString() + "  " + str);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static bool canStartGetPolicy()
        {
            try
            {
                EagleWebService.IbeFunc fc = new EagleWebService.IbeFunc();
                
                DateTime serverTime = fc.ServerTime();
                if (serverTime.Hour < 8 || serverTime.Hour >= 20) return true;
                else if (serverTime.Hour >= 12 && serverTime.Hour < 14) return true;
                else
                {
                    MessageBox.Show("由于在高峰时期计算返点政策可能导致服务器崩溃，可使用时间:0点-8点,12点-14点,20点-24点"
                        , "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("不能得到服务器时间，请重试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
