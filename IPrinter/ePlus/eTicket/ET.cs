using System;
using System.Collections.Generic;
using System.Text;
using gs.para;
using System.IO;
using System.Xml;
using System.Threading;
using System.Windows.Forms;

namespace ePlus.eTicket
{
    //第一步
    /// <summary>
    /// PreSubmitETicket预提交，etdz时，将当前PNR,用户提交
    /// </summary>
    class etPreSubmit
    {
        public bool submitinfo()
        {
            try
            {
                EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
                NewPara np = new NewPara();
                np.AddPara("cm", "PreSubmitETicket");
                np.AddPara("UserID", GlobalVar.loginName);
                np.AddPara("Pnr", EagleAPI.etstatic.Pnr);
                np.AddPara("DecFeeState", "0");
                string ipid = mystring.substring(GlobalVar.officeNumberCurrent, 0, 6);

                
                if (ipid.Length != 6)
                {
                    if (!GlobalVar.loginLC.bSameAllConfig)
                        throw new Exception("—预提交—配置号错误？配置号为：" + ipid);
                    else ipid = GlobalVar.loginLC.thesameipid;
                }
                np.AddPara("IpId", ipid);
                if (EagleAPI.etstatic.Pnr.Trim().Length != 5) throw new Exception("—预提交—PNR错误？PNR为："+EagleAPI.etstatic.Pnr);
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
               
                
                if (strRet != "")
                {
                    NewPara np1 = new NewPara(strRet);
                    if (np1.FindTextByPath("//eg/cm") == "RetPreSubmitETicket" && np1.FindTextByPath("//eg/OperationFlag") == "SaveSucc")
                    {
                        EagleAPI.LogWrite(EagleAPI.etstatic.Pnr + " PreSubmitETicket 预提交成功!\r\n");
                        return true;
                    }
                    else if (np1.FindTextByPath("//eg/cm") != "CmErr")
                    {
                        System.Windows.Forms.MessageBox.Show(strReq);
                    }
                    else return true;
                }
                else
                {
                    MessageBox.Show("PreSubmitETicket与服务器数据库连接失败！","易格服务器错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Source + ex.Message);
            }
            return false;
        }
    }
    //第二步a扣款
    /// <summary>
    /// DecFee
    /// </summary>
    class etDecFee
    {
        public string Pnr = "";
        public string TotalFC = "";
        public bool submitinfo()
        {
            try
            {
                if (float.Parse(TotalFC) < 10.0F) return false;
                EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
                NewPara np = new NewPara();
                np.AddPara("cm", "DecFee");
                np.AddPara("Pnr", Pnr);
                np.AddPara("TicketPrice", TotalFC);
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                //DecFee_Class dc = new DecFee_Class(strRet);
                if(strRet == null || strRet == "")
                    throw new Exception("调用 WebService 返回不正常！xml:" + strRet + Environment.NewLine);

                NewPara np1 = new NewPara(strRet);
                string cm = np1.FindTextByPath("//eg/cm");
                string decstat = np1.FindTextByPath("//eg/DecStat");
                string money = np1.FindTextByPath("//eg/NewUserYe");
                string err = np1.FindTextByPath("//eg/err");

                if (cm == "RetDecFee" && decstat == "DecSucc")
                {
                    GlobalVar.f_CurMoney = money;
                    string agrs = "调用扣款服务" + "<eg66>" + Pnr + TotalFC;
                    Thread th = new Thread(new ParameterizedThreadStart(LogServerWrite));
                    th.Start(agrs);
                    return true;
                }
                else
                {
                    throw new Exception("cm:" + cm + " err:" + err + Environment.NewLine);
                }
            }
            catch
            { 
                throw;
            }
        }
        public static void LogServerWrite(object args)
        {
            try
            {
                string[] obj = ((string)args).Split(new string[] { "<eg66>" }, StringSplitOptions.RemoveEmptyEntries);
                string cmd = obj[0];
                string rr = obj[1];

                EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
                NewPara np = new NewPara();
                np.AddPara("cm", "WriteLog");
                np.AddPara("User", GlobalVar.loginName);
                np.AddPara("Cmd", cmd);
                np.AddPara("ReturnResult", rr);
                string strReq = np.GetXML();
                while (true)
                {
                    string strRet = ws.getEgSoap(strReq);
                    if (strRet != "")
                    {
                        NewPara np1 = new NewPara(strRet);
                        if (np1.FindTextByPath("//eg/cm") == "RetWriteLog" && np1.FindTextByPath("//eg/OperationFlag") == "SaveSucc")
                        {

                            return;
                        }
                    }
                }
            }
            catch
            {
                //MessageBox.Show("FN项提交失败");
            }
        }
    }
    //第二步b提交电子客票信息
    /// <summary>
    /// SubmitETicket
    /// </summary>
    public partial class etStatic
    {
        public string UserID = "";
        public string Pnr = "";
        public string etNumber = "";
        public string FlightNumber1 = "";
        public string Bunk1 = "";
        public string FlightNumber2 = "";
        public string Bunk2 = "";
        public string CityPair1 = "";
        public string CityPair2 = "";
        public string Date1 = "";
        public string Date2 = "";
        public string TotalFC = "";
        public string State = "1";//退废票状态
        public string DecFeeState = "0";
        public string Passengers = "";

        public string TotalFare = "";//暂时不用
        public string TotalTaxBuild = "";
        public string TotalTaxFuel = "";
        public string TerminalNumber = "0";//使用IP的ID

        public bool SubmitInfo()
        {
            try
            {
                EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
                NewPara np = new NewPara();
                np.AddPara("cm", "SubmitETicket");
                //if (UserID != "") np.AddPara("UserID", GlobalVar.loginName);
                np.AddPara("etNumber", etNumber);
                np.AddPara("FlightNumber1", FlightNumber1);
                np.AddPara("Bunk1", Bunk1);
                np.AddPara("CityPair1", CityPair1);
                np.AddPara("Date1", Date1);

                np.AddPara("FlightNumber2", FlightNumber2);
                np.AddPara("Bunk2", Bunk2);
                np.AddPara("CityPair2", CityPair2);
                np.AddPara("Date2", Date2);

                np.AddPara("TotalFC", TotalFC);

                np.AddPara("State", State);
                np.AddPara("Pnr", Pnr);
                //np.AddPara("DecFeeState",  "2");//服务器应不予以处理，在客户端取消

                np.AddPara("Passenger", Passengers);

                np.AddPara("numBasePrc", TotalTaxBuild);
                np.AddPara("numFuel", TotalTaxFuel);
                //np.AddPara("IpId", TerminalNumber);
                np.AddPara("IpId", "");
                
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                if (strRet != "")
                {
                    NewPara np1 = new NewPara(strRet);
                    if (np1.FindTextByPath("//eg/cm") == "RetSubmitETicket" && np1.FindTextByPath("//eg/OperationFlag") == "SaveSucc")
                    {
                        return true;
                    }
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show("SubmitETicket与服务器数据库连接失败！");
                    MessageBox.Show("SubmitETicket与服务器数据库连接失败！", "易格服务器错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch
            {
            }
            return false;
        }

        public void GetPnrString(string cmd)
        {
            if (cmd.Trim().Length >= 7)
            {
                if (cmd.Trim().Substring(0, 2).ToUpper() == "RT")
                {
                    Pnr = mystring.right(cmd.Trim(), 5);
                }
            }
        }
    }
    //第二步c电子客票没有出成功
    /// <summary>
    /// CancelPNR
    /// </summary>
    class etCreateFailed
    {
        public string Pnr = "";
        static public List<string> lsValidate = new List<string>();//用于可能提取错误PNR而导致取消记录行，而重复扣款。
        public bool submitinfo()
        {
            try
            {
                lsValidate.Add(Pnr.ToUpper());
                int countGetThisPnr = 0;
                //for (int i = 0; i < lsValidate.Count; i++)
                //{
                //    if (lsValidate[i] == Pnr.ToUpper()) countGetThisPnr++;
                //}
                //if (countGetThisPnr < 5) return false;
                EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
                NewPara np = new NewPara();
                np.AddPara("cm", "CancelPNR");
                np.AddPara("Pnr", Pnr);
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                if (strRet != "")
                {
                    NewPara np1 = new NewPara(strRet);
                    if (np1.FindTextByPath("//eg/cm") == "RetCancelPNR" && np1.FindTextByPath("//eg/OperationFlag") == "SaveSucc")
                    {
                        lsValidate.Clear();
                        string agrs = "调用取消记录服务" + "<eg66>" + Pnr;
                        Thread th = new Thread(new ParameterizedThreadStart(etDecFee.LogServerWrite));
                        th.Start(agrs);
//                        Thread.Sleep(10000);
                        return true;
                    }
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show("CancelPNR与服务器数据库连接失败！");
                    MessageBox.Show("CancelPNR与服务器数据库连接失败！", "易格服务器错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                string s = ex.Message;
            }
            return false;
        }
    }
    //第三步请求出票状态为0的PNR.DECFEESTATE=0的ＰＮＲ?etNumber为空?
    /// <summary>
    /// GetUncheckedPNR
    /// </summary>
    class etGetUncheckedPnr
    {
        public string getpnr()
        {
            try
            {
                EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
                NewPara np = new NewPara();
                np.AddPara("cm", "GetUncheckedPNR");
                np.AddPara("UserID", GlobalVar.loginName);
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                if (strRet != "")
                {
                    NewPara np1 = new NewPara(strRet);
                    if (np1.FindTextByPath("//eg/cm") == "RetGetUncheckedPNR" && np1.FindTextByPath("//eg/Pnr") != "")
                    {
                        return np1.FindTextByPath("//eg/Pnr");
                    }
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show("GetUncheckedPNR与服务器数据库连接失败！");
                }

            }
            catch
            {
            }
            return "";
        }
    }

    /// <summary>
    /// 按电子客票号提取的类
    /// </summary>
    class etInfomation
    {
        public string ISSUEDBY = "";
        public string ORGDST = "";
        public string ER = "";
        public string TOURCODE = "";
        public string PASSENGER = "";
        public string EXCH = "";
        public string CONJTKT = "";
        public string BigCode = "";
        public string SmallCode = "";

        public string FC = "";
        public string FARE = "";
        public string FOP = "";
        public string TAX1 = "";
        public string TAX2 = "";
        public string TOTAL = "";
        public string OI = "";
        public string FROM = "";
        public string TO1 = "";
        public string TO2 = "";
        public void SetVar(string rs)
        {
            string FlagStart;
            FlagStart = "ISSUED BY:";
            int iStart = rs.IndexOf(FlagStart);
            string FlagEnd = "ORG/DST:";
            int iEnd = rs.IndexOf(FlagEnd);
            if (iStart > -1 && iEnd > (iStart + FlagStart.Length))
            {
                ISSUEDBY = rs.Substring(iStart + FlagStart.Length, iEnd - (iStart + FlagStart.Length)).Trim();
            }

            FlagStart = FlagEnd;
            FlagEnd = "BSP-D";
            iStart = iEnd;
            {
                ORGDST = EagleAPI.substring(rs, iStart + FlagStart.Length, 8).Trim();
            }
            FlagStart = "E/R:";
            iStart = rs.IndexOf(FlagStart);
            iEnd = rs.IndexOf('\r', iStart >= 0 ? iStart : 0);
            int itemp = rs.IndexOf('\n', iStart >= 0 ? iStart : 0);
            if(itemp<iEnd) iEnd = itemp;
            if (iStart > 0 && iEnd > iStart + FlagStart.Length)
            {
                ER = rs.Substring(iStart + FlagStart.Length, iEnd - (iStart + FlagStart.Length)).Trim();
            }
            FlagStart = "TOUR CODE:";
            iStart = rs.IndexOf(FlagStart);
            iEnd = rs.IndexOf('\r', iStart >= 0 ? iStart : 0);
             itemp = rs.IndexOf('\n', iStart >= 0 ? iStart : 0);
            if (itemp < iEnd) iEnd = itemp;
            if (iStart > 0 && iEnd > iStart + FlagStart.Length)
            {
                TOURCODE = rs.Substring(iStart + FlagStart.Length, iEnd - (iStart + FlagStart.Length)).Trim();
            }
            FlagStart = "PASSENGER:";
            iStart = rs.IndexOf(FlagStart);
            iEnd = rs.IndexOf('\r', iStart >= 0 ? iStart : 0);
            itemp = rs.IndexOf('\n', iStart >= 0 ? iStart : 0);
            if (itemp < iEnd) iEnd = itemp;
            if (iStart > 0 && iEnd > iStart + FlagStart.Length)
            {
                PASSENGER = rs.Substring(iStart + FlagStart.Length, iEnd - (iStart + FlagStart.Length)).Trim();
            }
            FlagStart = "EXCH:";
             iStart = rs.IndexOf(FlagStart);
             FlagEnd = "CONJ TKT:";
             iEnd = rs.IndexOf(FlagEnd);
            if (iStart > -1 && iEnd > (iStart + FlagStart.Length))
            {
                EXCH = rs.Substring(iStart + FlagStart.Length, iEnd - (iStart + FlagStart.Length)).Trim();
            }
            FlagStart = "CONJ TKT:";
            iStart = rs.IndexOf(FlagStart);
            iEnd = rs.IndexOf('\r', iStart >= 0 ? iStart : 0);
             itemp = rs.IndexOf('\n', iStart >= 0 ? iStart : 0);
            if (itemp < iEnd) iEnd = itemp;
            if (iStart > 0 && iEnd > iStart + FlagStart.Length)
            {
                CONJTKT = rs.Substring(iStart + FlagStart.Length, iEnd - (iStart + FlagStart.Length)).Trim();
            }
            FlagStart = "FC:";
            iStart = rs.IndexOf(FlagStart);
            iEnd = rs.IndexOf('\r', iStart >= 0 ? iStart : 0);
             itemp = rs.IndexOf('\n', iStart >= 0 ? iStart : 0);
            if (itemp < iEnd) iEnd = itemp;
            if (iStart > 0 && iEnd > iStart + FlagStart.Length)
            {
                FC = rs.Substring(iStart + FlagStart.Length, iEnd - (iStart + FlagStart.Length)).Trim();
            }
            FlagStart = "FARE:";
             iStart = rs.IndexOf(FlagStart);
             FlagEnd = "|FOP:";
             iEnd = rs.IndexOf(FlagEnd);
            if (iStart > -1 && iEnd > (iStart + FlagStart.Length))
            {
                FARE = rs.Substring(iStart + FlagStart.Length, iEnd - (iStart + FlagStart.Length)).Trim();
            }
            FlagStart = "TAX:";
             iStart = rs.IndexOf(FlagStart);
             FlagEnd = "|OI:";
             iEnd = rs.IndexOf(FlagEnd);
            if (iStart > -1 && iEnd > (iStart + FlagStart.Length))
            {
                TAX1 = rs.Substring(iStart + FlagStart.Length, iEnd - (iStart + FlagStart.Length)).Trim();
            }
            FlagStart = "TAX:";
             iStart = rs.LastIndexOf(FlagStart);
             FlagEnd = "|";
             iEnd = rs.IndexOf(FlagEnd,iStart >= 0 ? iStart : 0);
            if (iStart > -1 && iEnd > (iStart + FlagStart.Length))
            {
                TAX2 = rs.Substring(iStart + FlagStart.Length, iEnd - (iStart + FlagStart.Length)).Trim();
            }
            FlagStart = "TOTAL:";
             iStart = rs.IndexOf(FlagStart);
             FlagEnd = "|TKTN:";
             iEnd = rs.IndexOf(FlagEnd);
            if (iStart > -1 && iEnd > (iStart + FlagStart.Length))
            {
                TOTAL = rs.Substring(iStart + FlagStart.Length, iEnd - (iStart + FlagStart.Length)).Trim();
            }
            FlagStart = "|FOP:";
            iStart = rs.IndexOf(FlagStart);
            iEnd = rs.IndexOf('\r', iStart >= 0 ? iStart : 0);
             itemp = rs.IndexOf('\n', iStart >= 0 ? iStart : 0);
            if (itemp < iEnd) iEnd = itemp;
            if (iStart > 0 && iEnd > iStart + FlagStart.Length)
            {
                FOP = rs.Substring(iStart + FlagStart.Length, iEnd - (iStart + FlagStart.Length)).Trim();
            }
            FlagStart = "|OI:";
            iStart = rs.IndexOf(FlagStart);
            iEnd = rs.IndexOf('\r', iStart >= 0 ? iStart : 0);
             itemp = rs.IndexOf('\n', iStart >= 0 ? iStart : 0);
            if (itemp < iEnd) iEnd = itemp;
            if (iStart > 0 && iEnd > iStart + FlagStart.Length)
            {
                FOP = rs.Substring(iStart + FlagStart.Length, iEnd - (iStart + FlagStart.Length)).Trim();
            }
            FlagStart = "O FM:1";
            iStart = rs.IndexOf(FlagStart);
            iEnd = rs.IndexOf('\r', iStart >= 0 ? iStart : 0);
            itemp = rs.IndexOf('\n', iStart >= 0 ? iStart : 0);
            if (itemp < iEnd) iEnd = itemp;
            if (iStart > 0 && iEnd > iStart + FlagStart.Length)
            {
                FROM = rs.Substring(iStart + FlagStart.Length, iEnd - (iStart + FlagStart.Length)).Trim();
            }
            FlagStart = "O TO:2";
            iStart = rs.IndexOf(FlagStart);
            iEnd = rs.IndexOf('\r', iStart >= 0 ? iStart : 0);
            itemp = rs.IndexOf('\n', iStart >= 0 ? iStart : 0);
            if (itemp < iEnd) iEnd = itemp;
            if (iStart > 0 && iEnd > iStart + FlagStart.Length)
            {
                TO1 = rs.Substring(iStart + FlagStart.Length, iEnd - (iStart + FlagStart.Length)).Trim();
            }
            FlagStart = "  TO: ";
            iStart = rs.IndexOf(FlagStart);
            iEnd = rs.IndexOf('\r', iStart >= 0 ? iStart : 0);
            itemp = rs.IndexOf('\n', iStart >= 0 ? iStart : 0);
            if (itemp < iEnd) iEnd = itemp;
            if (iStart > 0 && iEnd > iStart + FlagStart.Length)
            {
                TO2 = rs.Substring(iStart + FlagStart.Length, iEnd - (iStart + FlagStart.Length)).Trim();
            }
            if (TO1 == "")
            {
                TO1 = TO2;
                TO2 = "";
            }
            FlagStart = "RL:";
            iStart = rs.IndexOf(FlagStart);
            iEnd = rs.IndexOf('/', iStart >= 0 ? iStart : 0);
            //itemp = rs.IndexOf('\n', iStart >= 0 ? iStart : 0);
            //if (itemp < iEnd) iEnd = itemp;
            if (iStart > 0 && iEnd > iStart + FlagStart.Length)
            {
                BigCode = rs.Substring(iStart + FlagStart.Length, iEnd - (iStart + FlagStart.Length)).Trim();
                SmallCode = rs.Substring(iEnd + 1, 5);
            }

        }

    }

    /// <summary>
    /// CheckReceiptNumber核对是否可以打印
    /// </summary>
    public partial class ReceiptCheck
    {
        public string varreceiptnumber = "";

        public bool submitinfo()
        {
            try
            {
                EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
                NewPara np = new NewPara();
                np.AddPara("cm", "CheckReceiptNumber");
                np.AddPara("User", GlobalVar.loginName);
                np.AddPara("RecieptNumber", varreceiptnumber);
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                if (strRet != "")
                {
                    NewPara np1 = new NewPara(strRet);
                    if (np1.FindTextByPath("//eg/cm") == "RetCheckRecieptNumber" && np1.FindTextByPath("//eg/OperationFlag") == "TRUE")
                    {
                        return true;
                    }
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show("CheckReceiptNumber与服务器数据库连接失败！");
                    MessageBox.Show("CheckReceiptNumber与服务器数据库连接失败！", "易格服务器错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
            }
            return false;

        }
    }



    //存取行程单号:1.load时，取号码，并置验证吗.2.打印时，存入号码 3.打印结束后，号码加1，并存入号码，再置验证码
    class AutoSetReceiptNumber
    {
        string filename = System.Windows.Forms.Application.StartupPath + "\\PTReceipt.XML";
        public string GetReceiptNumberFromXML()
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            gs.para.NewPara np = new NewPara(temp);
            return np.FindTextByPath("//eg/Number");
        }
        public void SaveReceiptNumberToXML(string number)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);
            XmlNode xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("Number");
            xn.InnerText = number;
            xd.Save(filename);
        }
        public string GetReceiptSignatureFromXML()
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            gs.para.NewPara np = new NewPara(temp);
            return np.FindTextByPath("//eg/Signature");

        }
        public string GetReceiptSaleCodeFromXML()
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            gs.para.NewPara np = new NewPara(temp);
            return np.FindTextByPath("//eg/SaleCode");

        }

        public void SaveReceiptSignatureToXML(string number)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);
            XmlNode xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("Signature");
            xn.InnerText = number;
            xd.Save(filename);
        }
        public void SaveReceiptSaleCodeToXML(string number)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);
            XmlNode xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("SaleCode");
            xn.InnerText = number;
            xd.Save(filename);
        }
        public string GetXiaoShouDaiHao(string on)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            gs.para.NewPara np = new NewPara(temp);
            return np.FindTextByPath("//eg/" + on);

        }
    }
    class 过票
    {
        
        public void exec()
        {
            GlobalVar.etProcessing = true;
            ////PrintReceipt pr = new PrintReceipt();
            ////pr.Text = "电子客票确认及信息提交";
            ////pr.textBox_订座号.Text = EagleAPI.etstatic.Pnr;
            ////pr.textBox_订座号.ReadOnly = true;
            ////pr.Window_CheckET();
            ////string txtAppend = "";
            ////if (pr.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            ////    txtAppend = ("电子客票数据处理完成！\r\n>");
            ////else
            ////    txtAppend = ("电子客票数据可能未提交，请点菜单->电子客票->电子客票核对！\r\n>");
            ////GlobalVar.stdRichTB.AppendText(txtAppend);
            //if (PrintReceipt.b0007) return;

            //PrintReceipt.b0007 = true;
            //PrintReceipt pr = new PrintReceipt();
            //pr.display过票();
            //pr.Window_ManageET();
            //pr.Text = "电子客票后台核查管理";
            ////pr.Visible = false;
            //pr.setToSubmitForm();
            //pr.Show();
            //pr.Hide();
            Thread et = new Thread(new ThreadStart(eTicket.etrun.runthread));
            et.Start();


            GlobalVar.etProcessing = false;
            EagleAPI.CLEARCMDLIST(3);
            //用于在黑屏上再次显示。。。
            //EagleAPI.EagleSendOneCmd("i~rT:" + EagleAPI.etstatic.Pnr,3);
        }
    }
}
