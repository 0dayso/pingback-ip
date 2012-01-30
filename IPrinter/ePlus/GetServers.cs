using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using gs.para;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace ePlus
{
    /*
    /// <summary>
    /// GetFC取票价
    /// </summary>
    /// 
    class BookAPI
    {
        /// <summary>
        /// 取票价，先取本地，无则取服务器，无则false
        /// </summary>
        /// <param name="citypair"></param>
        /// <returns></returns>
        static public bool GetFC_CityPair(string citypair, string sqlstring, OleDbConnection cn, out float bf, out float bc, out float by)
        {
            by = bf = bc = 0F;
            if (citypair.Length != 6)
            {
                return false;
            }
            try
            {
                OleDbCommand cmd = new OleDbCommand(sqlstring, cn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                DataTable dtTmp = new DataTable();
                adapter.Fill(dtTmp);
                if (dtTmp.Rows.Count != 0)
                {
                    bf = float.Parse(dtTmp.Rows[0]["BunkF"].ToString());
                    bc = float.Parse(dtTmp.Rows[0]["BunkC"].ToString());
                    by = float.Parse(dtTmp.Rows[0]["BunkY"].ToString());
                    return true;
                }
                else//取服务器价格
                {
                    WS.egws ws = new WS.egws(GlobalVar.WebServer);
                    NewPara np = new NewPara();
                    np.AddPara("cm", "GetFC");
                    np.AddPara("FROM", citypair.Substring(0, 3));
                    np.AddPara("TO", citypair.Substring(3, 3));
                    string strReq = np.GetXML();
                    string strRet = ws.getEgSoap(strReq);
                    if (strRet != "")
                    {
                        NewPara np1 = new NewPara(strRet);
                        if (np1.FindTextByPath("//eg/cm") == "RetGetFC")
                        {
                            if (np1.FindTextByPath("//eg/BUNKF") != "" && np1.FindTextByPath("//eg/BUNKC") != "" && np1.FindTextByPath("//eg/BUNKY") != "")
                            {
                                try
                                {
                                    by = float.Parse(np1.FindTextByPath("//eg/BUNKY"));
                                    bf = float.Parse(np1.FindTextByPath("//eg/BUNKF"));
                                    bc = float.Parse(np1.FindTextByPath("//eg/BUNKC"));
                                    return true;
                                }
                                catch
                                {
                                    by = bf = bc = 0F;
                                    return false;

                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("读取本地数据库票价表失败");
                return false;
            }
            return false;
        }

    }
    
    /// <summary>
    /// GetPassenger取乘客身份证信息(用于选择身份证号)
    /// </summary>
    /// 

    class Name_CardID
    {
        public string name = "";
        public string GetCardID()
        {
            try
            {
                WS.egws ws = new WS.egws(GlobalVar.WebServer);
                NewPara np = new NewPara();
                np.AddPara("cm", "GetPassenger");
                np.AddPara("Passenger", name);
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                if (strRet != "")
                {
                    NewPara np1 = new NewPara(strRet);
                    if (np1.FindTextByPath("//eg/cm") == "RetGetPassenger")
                    {
                        return np1.FindTextByPath("//eg/CardID");
                    }
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show("GetPassenger与服务器数据库连接失败！");
                }
            }
            catch
            {
            }
            return "";
        }
    }
    */
    /// <summary>
    /// 分析AVSTRING并取团体票
    /// </summary>
    
    class PassengerToGroup
    {
        public string avstring = "";
        public string fromto = "";
        public string date = "";
        public void execute()
        {
            if (avstring.Length <= 2) return;
            if (EagleAPI.substring(avstring, 0, 2).ToLower() != "av") return;
            string avsub = avstring.Substring(2).ToUpper();
            if (avsub[0] == ' ') avsub = avsub.Substring(1);
            #region 分析avstring,取fromto,date
            while (avsub.Length >= 6)
            {
                string citypair = avsub.Substring(0, 6);
                bool b_ABC = true;//全是字母
                for (int i = 0; i < citypair.Length; i++)
                {
                    if (citypair[i] < 'A' || citypair[i] > 'Z')
                    {
                        b_ABC = false;
                        break;
                    }
                }
                if (!b_ABC)
                {
                    avsub = avsub.Substring(1);
                    continue;
                }
                else
                {

                    if (EagleAPI.GetCityCn("", citypair.Substring(0, 3)) == "" || EagleAPI.GetCityCn("", citypair.Substring(3)) == "")
                    {
                        avsub = avsub.Substring(1);
                        continue;
                    }
                    else
                    {

                        fromto = citypair;

                        break;
                    }
                }
            }
            #endregion
            if (fromto == "")
            {
                avsub = avstring.Substring(2).ToUpper().Trim();
                string cityend;
                while (avsub.Length >= 3)
                {
                    cityend = avsub.Substring(0, 3);
                    if (EagleAPI.GetCityCn("", cityend.Substring(0, 3)) == "")
                    {
                        avsub = avsub.Substring(1);
                    }
                    else
                    {
                        fromto = GlobalVar.LocalCityCode + cityend;
                        break;
                    }
                }

            }
            GlobalVar2.gbFromto = fromto;



            try
            {
                char[] ch = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                string datestring = avstring;
                if (datestring.IndexOf("+") > -1)
                {
                    date = System.DateTime.Now.AddDays(1).ToShortDateString();
                }
                else if (datestring.IndexOf("-") > -1)
                {
                    date = System.DateTime.Now.AddYears(-1000).ToShortDateString();
                }
                else if (datestring.IndexOfAny(ch) >= 0)
                {
                    int start = datestring.IndexOfAny(ch);
                    datestring = datestring.Substring(start);
                    string mm = "";
                    string dd = "";
                    if (datestring[1] >= '0' && datestring[1] <= '9')
                    {
                        mm = datestring.Substring(2, 3);
                        dd = datestring.Substring(0, 2);
                    }
                    else
                    {
                        mm = datestring.Substring(1, 3);
                        dd = datestring.Substring(0, 1);
                    }
                    if (dd[0] == '0') dd = dd.Substring(1);
                    string imm = EagleAPI.GetMonthInt(mm.ToUpper());
                    date = System.DateTime.Now.Year.ToString() + "-" + imm + "-" + dd.ToString();

                    DateTime dateExpire = System.DateTime.Now.AddMonths(-3);
                    DateTime dateDest = DateTime.Parse(date + " 23:59:59");
                    if (dateExpire > dateDest)
                    {
                        date = System.DateTime.Now.AddYears(1).Year.ToString() + "-" + imm.ToString() + "-" + dd.ToString();
                    }

                }
                else
                {
                    date = System.DateTime.Now.ToShortDateString();
                }

            }
            catch
            {
                date = System.DateTime.Now.ToShortDateString();
            }
            if (fromto == "" || date == "")
            {
            }
            else
            {
                CSListGroupTicket gt = new CSListGroupTicket();
                gt.fromto = fromto;
                //gt.date = DateTime.Parse(date).Year.ToString() + "-" + DateTime.Parse(date).Month.ToString("d2") + "-" + DateTime.Parse(date).Day.ToString("d2");
                gt.date = DateTime.Parse(date).Year.ToString() + "-" + DateTime.Parse(date).Month.ToString() + "-" + DateTime.Parse(date).Day.ToString();

                List<string> lsFromTo = getFromtoArray(fromto.Substring(0,3).ToUpper(),fromto.Substring(3).ToUpper());

                string[] result = new string[lsFromTo.Count];
                for (int i = 0; i < lsFromTo.Count; i++)
                {
                    gt.fromto = lsFromTo[i];
                    result [i]= gt.listgroupticket();
                }
                string temp = "";
                for (int i = 0; i < result.Length; i++)
                {
                    if (result[i].Trim() == "") continue;
                    temp += result[i] + "<eg666>";
                }
                if (temp.Length > 0) temp = temp.Substring(0, temp.Length - 7);
                if (temp != "")
                {
                    BookSimple.ToGroup tg = new BookSimple.ToGroup();
                    tg.listcontent = temp;
                    tg.fromto = fromto;
                    tg.date = date;
                    tg.TopMost = true;
                    //tg.Location = new System.Drawing.Point(GlobalVar2.bookTicket.Location.X, GlobalVar2.bookTicket.Location.Y + GlobalVar2.bookTicket.Height);
                    tg.ShowDialog();
                    
                }

            }

        }
        List<string> getFromtoArray(string from,string to)
        {
            string[] doublecity = { "SHA","PVG","PEK","BJS","SEL","ICN","SIA","XIY" };
            List<string> lsRet = new List<string>();
            string[] af = new string[2];
            af[0] = from;
            af[1] = "";
            for (int i = 0; i < doublecity.Length; i++)
            {
                if (from == doublecity[i])
                {
                    if (i % 2 == 0) af[1] = doublecity[i + 1];
                    else af[1] = doublecity[i - 1];
                }
            }

            string[] at = new string[2];
            at[0] = to;
            at[1] = "";
            for (int i = 0; i < doublecity.Length; i++)
            {
                if (to == doublecity[i])
                {
                    if (i % 2 == 0) at[1] = doublecity[i + 1];
                    else at[1] = doublecity[i - 1];
                }
            }
            for (int i = 0; i < 2; i++)
            {
                if (af[i] == "") continue;
                for (int j = 0; j < 2; j++)
                {
                    if (at[j] == "") continue;
                    lsRet.Add(af[i] + at[j]);
                }
            }
            return lsRet;
        }
    }
    /// <summary>
    /// AddToGroup入团
    /// </summary>
    class CSAddToGroup
    {
        public string name = "";
        public string cardid = "";
        public string groupid = "";
        public bool addtogroup()
        {
            try
            {
                EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
                NewPara np = new NewPara();
                np.AddPara("cm", "AddToGroup");
                np.AddPara("UserID", GlobalVar.loginName);
                np.AddPara("Name", name);
                np.AddPara("CardID", cardid);
                np.AddPara("GroupTicketID", groupid);
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                if (strRet != "")
                {
                    NewPara np1 = new NewPara(strRet);
                    if (np1.FindTextByPath("//eg/cm") == "RetAddToGroup" && np1.FindTextByPath("//eg/OperationFlag") == "SaveSucc")
                    {
                        return true;
                    }
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show("AddToGroup与服务器数据库连接失败！");
                }
            }
            catch
            {
            }
            return false;
        }
    }

    /// <summary>
    /// ListGroupTicket取对应团体票内容
    /// </summary>
    class CSListGroupTicket
    {
        public string fromto = "";
        public string date = "";
        public string listgroupticket()
        {
            try
            {
                date = DateTime.Parse(date).Year.ToString() + "-" + DateTime.Parse(date).Month.ToString() + "-" + DateTime.Parse(date).Day.ToString();
                EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
                NewPara np = new NewPara();
                np.AddPara("cm", "ListGroupTicket");
                np.AddPara("FromTo", fromto);
                np.AddPara("Date", date);
                np.AddPara("UserID", GlobalVar.loginName);
                string Retype = "";


                if (Model.md.b_048) Retype = "E";
                else if (Model.md.b_038) Retype = "D";
                else if (Model.md.b_028) Retype = "C";
                else if (Model.md.b_018) Retype = "B";
                else if (Model.md.b_008) Retype = "A";
                else return "";
                np.AddPara("RebateType", Retype);
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                if (strRet != "")
                {
                    
                    NewPara np1 = new NewPara(strRet);
                    if (np1.FindTextByPath("//eg/cm") == "RetListGroupTicket" && np1.FindTextByPath("//eg/Result").Trim() != "")
                    {
                        string ret= np1.FindTextByPath("//eg/Result").Trim();
                        return ret;
                    }
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show("ListGroupTicket与服务器数据库连接失败！");
                }
            }
            catch
            {
            }
            return "";
        }
    }

    /// <summary>
    /// SubmitScrollString,GetCurrentScrollString提交公告，取公告
    /// </summary>
    class Notice
    {
        public string context = "";
        public string dtBeg = "";
        public string dtEnd = "";
        public string strType = "";
        static public string NOTICESCROLL = "";
        public void submit_notice_scroll()
        {
            try
            {
                EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
                NewPara np = new NewPara();
                np.AddPara("cm", "SubmitScrollString");
                np.AddPara("UserID", GlobalVar.loginName);
                np.AddPara("Context", GlobalVar.loginName + ":" +context);
                np.AddPara("BegTime", dtBeg);
                np.AddPara("EndTime", dtEnd);
                np.AddPara("NoticeType", strType);
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                if (strRet != "")
                {
                    NewPara np1 = new NewPara(strRet);
                    if (np1.FindTextByPath("//eg/cm") == "RetSubmitScrollString" && np1.FindTextByPath("//eg/OperationFlag") == "SaveSucc")
                    {
                        MessageBox.Show("发布成功");
                        return ;
                    }
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show("与服务器数据库连接失败！");
                }
            }
            catch
            {
            }
            MessageBox.Show("发布失败");
            return;
        }
        public string get_notice_scroll(string noticeType)
        {
            string ret = File.ReadAllText("notice.txt", Encoding.Default);
            return ret;
            //try
            //{
            //    EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
            //    NewPara np = new NewPara();
            //    np.AddPara("cm", "GetCurrentScrollString");
            //    np.AddPara("User", GlobalVar.loginName);
            //    np.AddPara("NoticeType", noticeType);
            //    string strReq = np.GetXML();
            //    string strRet = ws.getEgSoap(strReq);
            //    if (strRet != "")
            //    {
            //        NewPara np1 = new NewPara(strRet);
            //        if (np1.FindTextByPath("//eg/cm") == "RetGetCurrentScrollString")
            //        {
            //            return np1.FindTextByPath("//eg/Context").Replace('\r', ' ').Replace('\n', ' '); ;
            //        }
            //    }
            //    else
            //    {
            //        //System.Windows.Forms.MessageBox.Show("与服务器数据库连接失败！");
            //    }
            //}
            //catch
            //{

            //}
            //return "";
        }

    }
    /// <summary>
    /// WriteLog写服务器日志
    /// </summary>
    class log
    {
        static public string strSend = "";
        static public string strRecv = "";
        public bool b_submit = false;
        public void submitlog()
        {
            try
            {
                EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
                NewPara np = new NewPara();
                np.AddPara("cm", "WriteLog");
                np.AddPara("User", GlobalVar.loginName);
                np.AddPara("Cmd", strSend);
                np.AddPara("ReturnResult", strRecv);
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                if (strRet != "")
                {
                    NewPara np1 = new NewPara(strRet);
                    if (np1.FindTextByPath("//eg/cm") == "RetWriteLog" && np1.FindTextByPath("//eg/OperationFlag") == "SaveSucc")
                    {
                        b_submit = true;
                        return;
                    }
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show("WriteLog与服务器数据库连接失败！");
                }
            }
            catch
            {
            }
            b_submit = false;
            return;
        }
        public void submitlog(string send, string recv)
        {
            try
            {
                EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
                NewPara np = new NewPara();
                np.AddPara("cm", "WriteLog");
                np.AddPara("User", GlobalVar.loginName);
                np.AddPara("Cmd", send);
                np.AddPara("ReturnResult", recv);
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                if (strRet != "")
                {
                    NewPara np1 = new NewPara(strRet);
                    if (np1.FindTextByPath("//eg/cm") == "RetWriteLog" && np1.FindTextByPath("//eg/OperationFlag") == "SaveSucc")
                    {
                        b_submit = true;
                        return;
                    }
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show("WriteLog与服务器数据库连接失败！");
                }
            }
            catch
            {
            }
            b_submit = false;
            return;
        }
        static public void submitServerLogItem()
        {
            if (log.strSend.Length < 10) return;
            if (log.strSend.Trim().Substring(0, 2).ToLower() == "fn")
            {
                try
                {
                    EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
                    NewPara np = new NewPara();
                    np.AddPara("cm", "WriteLog");
                    np.AddPara("User", GlobalVar.loginName);
                    np.AddPara("Cmd", "Login项/" + GlobalVar.loginLC.PassPort);
                    np.AddPara("ReturnResult", "Login项/登陆时间为  " + DateTime.Now.ToString());
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
                }
            }
        }
        static public void submitFNpnr()
        {
            if (log.strSend.Length < 10) return;
            if (log.strSend.Trim().Substring(0, 2).ToLower() == "fn")
            {
                try
                {
                    EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
                    NewPara np = new NewPara();
                    np.AddPara("cm", "WriteLog");
                    np.AddPara("User", GlobalVar.loginName);
                    np.AddPara("Cmd", "FN项/" + EagleAPI.etstatic.Pnr);
                    np.AddPara("ReturnResult", log.strSend);
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
            //else submitETDZ();

        }
        static public void submitETDZ()
        {
            if (log.strSend.Length < 4) return;
            if (log.strSend.Trim().Substring(0, 4).ToLower() != "etdz") return;
            string str_analysis = log.strRecv;
            if (str_analysis.IndexOf("CNY") >= 0 && str_analysis.IndexOf(".00") > 0 && str_analysis.Length < 30)
            {
                string temp = str_analysis;
                while (temp[temp.Length - 1] == '\r' || temp[temp.Length - 1] == '\n')
                {
                    temp = temp.Substring(0, temp.Length - 1);
                }
                temp = temp.Trim();
                temp = EagleAPI.substring(temp, temp.Length - 5, 5);
                if (temp.ToUpper() != EagleAPI.etstatic.Pnr.ToUpper()) return;
            }

            try
            {
                EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
                NewPara np = new NewPara();
                np.AddPara("cm", "WriteLog");
                np.AddPara("User", GlobalVar.loginName);
                np.AddPara("Cmd", "ETDZ项/" + EagleAPI.etstatic.Pnr);
                np.AddPara("ReturnResult", log.strRecv);
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
    /// <summary>
    /// SubmitPnrState先对返回结果进行分析，并利用服务进行统计
    /// </summary>
    class pnr_statistics
    {
        public string str_analysis = "";
        public string pnr = "";
        public string state = "";
        string analysis1()//PNR CANCELLED S2M2Y
        {
            int pos = str_analysis.IndexOf("PNR CANCELLED");
            try
            {
                if (pos >= 0 && str_analysis.Length < 30)
                {
                    string temp = str_analysis.Substring(pos + 13).Trim().Substring(0, 5);
                    return temp;
                }
            }
            catch { }
            return "";
        }
        /*
QGG4V -EOT SUCCESSFUL, BUT ASR UNUSED FOR 1 OR MORE SEGMENTS
  ZH9897  Y FR22DEC  WUHSHE DK1   2010 2220 
  航空公司使用自动出票时限, 请检查PNR
         * */
        string analysis2()//订票返回结果分析
        {
            string[] rows = str_analysis.Split('\r');
            string lockstring = "";

            int itemp = -1;
            if ((itemp = str_analysis.IndexOf("-EOT SUCCESSFUL")) >= 0)
            {
                int itempbeg = -1;
                itempbeg = str_analysis.LastIndexOf("\r", itemp);
                if (itempbeg < 0)
                    itempbeg = str_analysis.LastIndexOf("\n", itemp);
                if (itempbeg < 0) itempbeg = 0;
                lockstring = str_analysis.Substring(itempbeg, itemp - itempbeg).Trim();
                if (EagleAPI.IsRtCode(lockstring)) return lockstring;
                else lockstring = "";
            }
            for (int i = rows.Length - 1; i >= 0; i--)
            {
                if (rows[i].Length >= 5)
                {
                    lockstring = rows[i];
                    break;
                }
            }
            if (lockstring.Length > 0 && lockstring[0] == '\n') lockstring = lockstring.Substring(1);
            if (string.Compare(EagleAPI.substring(lockstring, 5, 1), " ") > 0) return "";
            lockstring = mystring.trim(EagleAPI.substring(lockstring, 0, 6),'-').Trim();
            for (int i = 0; i < lockstring.Length; i++)
            {
                if (lockstring[i] < '0' || lockstring[i] > 'Z' || (lockstring[i] > '9' && lockstring[i] < 'A'))
                    return "";
            }
            return lockstring;
        }
        string analysis3()//etdz结果
        {
            if (str_analysis.IndexOf("CNY") >= 0 && str_analysis.IndexOf(".00") > 0 && str_analysis.Length < 30)
            {
                string temp = str_analysis;
                while (temp[temp.Length - 1] == '\r' || temp[temp.Length - 1] == '\n')
                {
                    temp = temp.Substring(0, temp.Length - 1);
                }
                temp = temp.Trim();
                temp = EagleAPI.substring(temp, temp.Length - 5, 5);
                return temp;
            }
            return "";
        }
        public void submit1()
        {
            for (int i = 0; i < 3; i++)
            {
                if (submit()) return;
            }
        }
        public bool submit()
        {
            if (pnr == "")
            {
                string temp = "";
                if ((temp = analysis1()) != "") { pnr = temp; state = (state == "" ? "2" : state); return false; }
                
                else if ((temp = analysis3()) != "") { pnr = temp; state = (state == "" ? "3" : state); }
                else if ((temp = analysis2()) != "") { pnr = temp; state = (pnr.ToUpper() == EagleAPI.etstatic.Pnr.ToUpper() ? "1" : "0"); }
            }
            if (pnr == "") return false;
            if (!EagleAPI.IsRtCode(pnr)) return false;
            try
            {
                EagleAPI.etstatic.Pnr = pnr;
                EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
                NewPara np = new NewPara();
                np.AddPara("cm", "SubmitPnrState");
                np.AddPara("User", GlobalVar.loginName);
                np.AddPara("PNR", pnr.ToUpper());
                np.AddPara("State", state);
                np.AddPara("Office", mystring.substring(GlobalVar.officeNumberCurrent, 0, 6));
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                if (strRet != "")
                {
                    NewPara np1 = new NewPara(strRet);
                    if (np1.FindTextByPath("//eg/cm") == "RetSubmitPnrState" && np1.FindTextByPath("//eg/OperationFlag") == "SaveSucc")
                    {
                        try
                        {
                            if (state == "0" && GlobalVar2.gbUserModel == 1 && Options.GlobalVar.B2cCanCreateNewOrder == "1")//如果是产生PNR并且是BtoC用户登陆，那么生成订单
                            {
                                //1.PNR提交窗口，并设置大小1*1
                                Options.GlobalVar.B2CIsAutoSubmit = true;
                                BookSimple.SubmitPnr dlg = new ePlus.BookSimple.SubmitPnr();
                                dlg.Size = new System.Drawing.Size(dlg.Size.Width, 1);
                                dlg.ControlBox = false;
                                dlg.Show();
                                dlg.正常提交按钮(pnr.ToUpper(), "95161");
                                
                            }
                        }
                        catch
                        {
                        }
                        return true;
                    }

                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("SubmitPnrState与服务器数据库连接失败！");
                }

            }
            catch
            {
            }
            return false;

        }

    }
    /// <summary>
    /// Logout退出登陆
    /// </summary>
    class ExitSystem
    {
        public void logout()
        {
            bool b_succ = false;
            try
            {
                EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
                NewPara np = new NewPara();
                np.AddPara("cm", "Logout");
                np.AddPara("User", GlobalVar.loginName);
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                if (strRet != "")
                {
                    NewPara np1 = new NewPara(strRet);
                    if (np1.FindTextByPath("//eg/cm") == "RetLogout" && np1.FindTextByPath("//eg/LogoutStat") == "LogoutSucc")
                    {
                        b_succ = true;
                    }
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show("Logout与服务器数据库连接失败！");
                }
            }
            catch
            {
            }
            //log.strSend = "Logout";
            //log.strRecv = (b_succ ? "Success" : "Fail");
            //log ml = new ePlus.log();
            //ml.submitlog();
            return;
        }
    }

    /// <summary>
    /// Login登陆系统，返回XML串
    /// </summary>
    class Login
    {
        string strReq;
        string strRet;
        string errorMsg;

        private void GetIt()
        {
            EagleWebService.wsKernal ws = new EagleWebService.wsKernal("");
            try
            {
                strRet = ws.getEgSoap(strReq);
            }
            catch (System.Threading.ThreadAbortException)
            { }
            catch (Exception e)
            {
                EagleString.EagleFileIO.LogWrite(e.ToString());
                this.errorMsg = e.Message;
            }
        }

        public string login()
        {
            NewPara np = new NewPara();
            np.AddPara("cm", "Login");
            np.AddPara("UserName", GlobalVar.loginName);//jm
            np.AddPara("PassWord", GlobalVar.loginPassword);//jm
            strReq = np.GetXML();

            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(GetIt));
            th.Start();
            if (!th.Join(System.Threading.Timeout.Infinite))
            {
                th.Abort();
                throw new TimeoutException("连接超时，请稍后重试！");
            }
            else if (!string.IsNullOrEmpty(this.errorMsg))
                throw new Exception(this.errorMsg);

            if (strRet.ToLower().IndexOf("hak345") > 0 && strRet.IndexOf("美尔达") > 0) GlobalVar.bHakUser = true;
            if (strRet.ToLower().IndexOf("北京广顺") > 0) GlobalVar.bPekGuangShunUser = true;
            return strRet;
        }
    }
    public partial class Login_Classes
    {
        string xmlString = "";
        NewPara np;

        public string CommandType = "";
        public string LoginFlag = "";
        public string IPsString = "";
        public string VisuableCommand = "";
        public string SailTicketType = "";
        public string PassPort = "";
        public string SrvIP = "";
        public int SrvPort = 10000;
        public string SrvDNS = "";
        public string SrvName = "";
        public string IPsString_backup = "";
        public string TrimLen = "";

        public string agentState = "";
        public string userState = "";
        public string UserExpire = "";

        public string InsUserAccount = "";
        public string InsUserPassword = "";//<InsuranceUserName></InsuranceUserName><InsurancePassWord></InsurancePassWord>

        public bool bSameAllConfig = false;
        public string thesameipid = "";

        public Login_Classes()
        {
        }
        public int configCount
        {
            get
            {
                return GetIPsString().Split('~').Length;
            }
        }
        public Login_Classes(Login_Classes lc)
        {
            this.CommandType = lc.CommandType;
            this.LoginFlag = lc.LoginFlag;
            this.IPsString = lc.IPsString;
            this.VisuableCommand = lc.VisuableCommand;
            this.SailTicketType = lc.SailTicketType;
            this.PassPort = lc.PassPort;
            this.SrvIP = lc.SrvIP;
            this.SrvDNS = lc.SrvDNS;
            this.IPsString_backup = lc.IPsString_backup;
            this.TrimLen = lc.TrimLen;
            this.SrvPort = lc.SrvPort;

            this.agentState = lc.agentState;
            this.userState = lc.userState;
            this.UserExpire = lc.UserExpire;
            this.SrvName = lc.SrvName;
            GlobalVar2.bxUserAccount = lc.InsUserAccount;
            GlobalVar2.bxPassWord = lc.InsUserPassword;
            this.bSameAllConfig = lc.bSameAllConfig;
        }
        public Login_Classes(string xmlstring)
        {
            xmlString = xmlstring;
            try
            {
                np = new NewPara(xmlString);
                CommandType = GetCommandType();
                LoginFlag = GetLoginFlag();
                IPsString = GetIPsString();
                VisuableCommand = GetVisuableCommand();
                SailTicketType = GetSailTicketType();
                PassPort = GetPassport();
                SrvIP = GetSrvIP();
                IPsString_backup = IPsString;
                TrimLen = GetTrimLen();
                SrvPort = (GetSrvPort() == 0 ? 10000 : GetSrvPort());

                agentState = (np.FindTextByPath("//eg/AgentStat").Trim());
                userState = (np.FindTextByPath("//eg/UserStat").Trim());
                UserExpire = np.FindTextByPath("//eg/UserExpire").Trim();
                SrvName = np.FindTextByPath("//eg/SrvName").Trim();
                GlobalVar2.bxUserAccount = np.FindTextByPath("//eg/InsuranceUserName");
                GlobalVar2.bxPassWord = np.FindTextByPath("//eg/InsurancePassWord");
                SrvDNS = GetSrvDNS();
                if (GlobalVar.serverAddr == GlobalVar.ServerAddr.ZhenZhouJiChang)
                {
                    if (LogonForm.isZzInternet)
                    {
                        SrvIP = GetSrvDNS();
                        SrvDNS = GetSrvIP();
                    }
                }
            }
            catch
            {
            }
        }
        public bool LoginState()
        {
            if (this.CommandType == "RetLogin" && this.LoginFlag == "LoginSucc")//登陆成功
                return true;
            return false;
        }

        int GetSrvPort()
        {
            try
            {
                return int.Parse(np.FindTextByPath("//eg/SrvPort").Trim());
            }
            catch
            {
            }
            return 0;
        }
        /// <summary>
        /// 返回RetLogin
        /// </summary>
        /// <returns></returns>
        string GetCommandType()
        {
            if (xmlString == "") return "";
            return np.FindTextByPath("//eg/cm").Trim();
        }
        /// <summary>
        /// 登入成功,返回LoginSucc,不成功LoginFail
        /// </summary>
        /// <returns></returns>
        string GetLoginFlag()
        {
            if (xmlString == "") return "";
            return np.FindTextByPath("//eg/LoginFlag").Trim();
        }
        /// <summary>
        /// 返回PassPort,32位
        /// </summary>
        /// <returns></returns>
        string GetPassport()
        {
            if (xmlString == "") return "";
            return np.FindTextByPath("//eg/PassPort").Trim();
        }
        string GetTrimLen()
        {
            if (xmlString == "") return "";
            return np.FindTextByPath("//eg/TrimLen").Trim();
        }
        /// <summary>
        /// 返回可用IPs串，多个IP用~割开
        /// </summary>
        /// <returns></returns>
        string GetIPsString()
        {
            string ret = "";
            try
            {
                XmlNode tempxn = np.FindNodeByPath("//eg/IPS/AOFF");
                if (tempxn.Name != "AOFF")
                {
                    XmlNode nodeIPS = np.FindNodeByPath("//eg/IPS");
                    string[] sp = { "<eg66>" };
                    for (int i = 0; i < nodeIPS.ChildNodes.Count; i++)
                    {
                        XmlNode tmpNode = nodeIPS.ChildNodes[i];
                        string tempip = tmpNode.ChildNodes[0].Value.Split(sp, StringSplitOptions.RemoveEmptyEntries)[0];
                        ret += DNS2IP(tempip) + ((i == nodeIPS.ChildNodes.Count - 1) ? "" : "~");
                        GlobalVar.ipListId.Add(tmpNode.ChildNodes[0].Value);
                    }
                }
                else
                {
                    tempxn = np.FindNodeByPath("//eg/IPS");
                    string peizhi = "";
                    bSameAllConfig = true;
                    for (int i = 0; i<tempxn.ChildNodes.Count; i++)
                    {
                        for (int j = 0; j < tempxn.ChildNodes[i].ChildNodes.Count; j++)
                        {
                            if (tempxn.ChildNodes[i].ChildNodes[j].Name.ToUpper () == "PEIZHI")
                            {
                                try
                                {
                                    if (peizhi == "")
                                    {
                                        thesameipid = peizhi = tempxn.ChildNodes[i].ChildNodes[j].InnerText.ToUpper().Substring(0, 6);
                                    }
                                    else
                                    {
                                        if ((peizhi == tempxn.ChildNodes[i].ChildNodes[j].InnerText.ToUpper().Substring(0, 6)) && bSameAllConfig)
                                        {

                                        }
                                        else
                                        {
                                            bSameAllConfig = false;
                                            thesameipid = "";
                                        }
                                    }
                                }
                                catch
                                {
                                    MessageBox.Show("请在后台管理中正确设置OFFICE或配置：如WUH128");
                                    continue;
                                }
                            }
                            if (tempxn.ChildNodes[i].ChildNodes[j].Name == "ip")
                            {
                                try
                                {
                                    string tempip = tempxn.ChildNodes[i].ChildNodes[j].InnerText;
                                    tempip = DNS2IP(tempip);
                                    //if (ret.IndexOf(tempip) >= 0) break;
                                    ret += (tempip) + "~";
                                }
                                catch
                                {
                                }
                                
                            }
                        }
                    }
                    if (ret.Length > 0) ret = ret.Substring(0, ret.Length - 1);
                }
            }
            catch(Exception ex)
            {
                   
            }
            //航以网
            if (ret == "" && GlobalVar.serverAddr == GlobalVar.ServerAddr.HangYiWang)
            {
                try
                {

                    XmlNode nodeIPS = np.FindNodeByPath("//eg/IPS");
                    string sp = "<eg66>";
                    for (int i = 0; i < nodeIPS.ChildNodes.Count; i++)
                    {
                        XmlNode tmpNode = nodeIPS.ChildNodes[i];
                        ret += tmpNode.ChildNodes[0].Value + ((i == nodeIPS.ChildNodes.Count - 1) ? "" : "~");
                        //XmlNode tmpNode = nodeIPS.ChildNodes[i];
                        //string tempip = tmpNode.ChildNodes[0].Value.Substring(0, tmpNode.ChildNodes[0].Value.IndexOf(sp));
                        //ret += DNS2IP(tempip) + ((i == nodeIPS.ChildNodes.Count - 1) ? "" : "~");
                    }


                }
                catch(Exception ee)
                {
                    return "";
                }
            }
            return ret;

        }
        string DNS2IP(string dns)
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

            if (!bIsIp)
            {
                System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(dns);
                System.Net.IPAddress[] addr = ipEntry.AddressList;
                ret = addr[0].ToString();
            }
            else
                ret = dns;
            return ret;

        }
        public static string dns2ip_static(string dns)
        {
            try
            {
                string ret;
                bool bIsIp = false;
                try
                {
                    System.Net.IPAddress.Parse(dns.Trim());
                    bIsIp = true;
                }
                catch (Exception ex)
                {
                    bIsIp = false;
                    System.Console.Write(ex.Message);
                }

                if (!bIsIp)
                {
                    System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(dns);
                    System.Net.IPAddress[] addr = ipEntry.AddressList;
                    ret = addr[0].ToString();
                }
                else
                    ret = dns;
                return ret;
            }
            catch
            {
                return dns;
            }
        }
        /// <summary>
        /// 返回当前用户可用的指令集，多个指令用~割开
        /// </summary>
        /// <returns></returns>
        string GetVisuableCommand()
        {
            string ret = "";
            try
            {

                XmlNode nodeIPS = np.FindNodeByPath("//eg/UseCms");
                for (int i = 0; i < nodeIPS.ChildNodes.Count; i++)
                {
                    XmlNode tmpNode = nodeIPS.ChildNodes[i];
                    ret += tmpNode.ChildNodes[0].Value + ((i == nodeIPS.ChildNodes.Count - 1) ? "" : "~");
                }

            }
            catch
            {
                return "";
            }
            return ret;
        }
        /// <summary>
        /// 返回当前用户销售机票的类型，多个用~割开
        /// </summary>
        /// <returns></returns>
        string GetSailTicketType()
        {
            string ret = "";
            try
            {

                XmlNode nodeIPS = np.FindNodeByPath("//eg/Tickets");
                for (int i = 0; i < nodeIPS.ChildNodes.Count; i++)
                {
                    XmlNode tmpNode = nodeIPS.ChildNodes[i];
                    ret += tmpNode.ChildNodes[0].Value + ((i == nodeIPS.ChildNodes.Count - 1) ? "" : "~");
                }

            }
            catch
            {
                return "";
            }
            return ret;
        }
        /// <summary>
        /// 返回该用户的代理商服务器的DOMAIN NAME
        /// </summary>
        /// <returns></returns>
        string GetSrvIP()
        {
            if (xmlString == "") return "";
            return np.FindTextByPath("//eg/SrvIp").Trim();
        }
        /// <summary>
        /// 返回该用户的代理商服务器所在IP
        /// </summary>
        /// <returns></returns>
        string GetSrvDNS()
        {
            if (xmlString == "") return "";
            return np.FindTextByPath("//eg/SrvDNS").Trim();
        }
    }
    /// <summary>
    /// ChgPassword修改密码
    /// </summary>    
    class ChangePWD
    {
        public bool changepwd(string newpwd)
        {
            try
            {
                EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
                NewPara np = new NewPara();
                np.AddPara("cm", "ChgPassword");
                np.AddPara("UserName", GlobalVar.loginName);
                np.AddPara("Password", newpwd);
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                if (strRet == "") return false;
                NewPara np1 = new NewPara(strRet);
                if (np1.FindTextByPath("//eg/cm") == "RetChgPassword" && np1.FindTextByPath("//eg/ChgPWDStat") == "ChgPassSucc")
                    return true;
            }
            catch
            {
            }
            return false;
        }
    }

    /// <summary>
    /// GetCloseBalance取用户当前余额，取错误则返回字符A
    /// </summary>
    class GetRemainMoney
    {
        public string getcurmoney()
        {
            try
            {
                EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
                NewPara np1 = new NewPara();
                np1.AddPara("cm", "GetCloseBalance");
                np1.AddPara("UserName", GlobalVar.loginName);
                string strReq = np1.GetXML();
                string strRet = ws.getEgSoap(strReq);
                if (strRet == "") return "A";
                NewPara np = new NewPara(strRet);
                if (np.FindTextByPath("//eg/cm") == "RetCloseBalance")
                    return np.FindTextByPath("//eg/UserYE");
            }
            catch
            {
            }
            return "A";
        }
    }


    /// <summary>
    /// SubmitPNR简易版提交PNR请求审核，
    /// </summary>
    class EasySubmitPnr
    {
        /// <summary>
        /// 用于黑屏提交
        /// </summary>
        public bool submit_easy_pnr(string pnr, string tl, string remark,string names,string phone,string personcount,
            string [] flightno,string []bunk,string[]date,string[] citypair,
            string fareTkt,string fareReal,string taxBuild,string taxFuel,string usergain,string lirun,string fareTotal)
        {


            if (EagleAPI2.IsPnrSubmitted(pnr)) return false;
            EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
            NewPara np = new NewPara();

            XmlDocument doc = np.getRoot();

            np.AddPara("cm", "SubmitPNR");
            np.AddPara("User", GlobalVar.loginName);
            np.AddPara("PNR", pnr);
            np.AddPara("TL", tl);
            np.AddPara("Bz", remark);
            np.AddPara("Phone", phone);
            np.AddPara("PersonCount", personcount);
            string tmpName = "";
            if (GlobalVar2.gbPassegersInEasyVersion == "")
            {
                np.AddPara("Names", names);
                tmpName = names;
            }
            else
            {
                np.AddPara("Names", GlobalVar2.gbPassegersInEasyVersion);
                tmpName = GlobalVar2.gbPassegersInEasyVersion;
            }
            GlobalVar2.gbPassegersInEasyVersion = tmpName;
            //***************************2008-7-16.置全局变量gbYzpOrder中的bPassINFO:姓名及证件**************************开始
            //if(GlobalVar2.gbYzpOrder==null)
            //    GlobalVar2.gbYzpOrder = new YzpBtoC.YZPnewOrder();
            string[] psg = GlobalVar2.gbPassegersInEasyVersion.Split(';');//例：测试四-P0;测试五-P0
            //if (GlobalVar2.gbYzpOrder.rPASSINFO == null)
            //    GlobalVar2.gbYzpOrder.rPASSINFO = new YzpBtoC.YZPpassINFO[psg.Length];
            if (GlobalVar2.gbYzpOrder == null) GlobalVar2.gbYzpOrder = new YzpBtoC.YZPnewOrder();
            GlobalVar2.gbYzpOrder.lPNR = pnr;
            GlobalVar2.gbYzpOrder.lAGENTID = Options.GlobalVar.B2CCorpID;
            GlobalVar2.gbYzpOrder.lSENDPART = Options.GlobalVar.B2CDepartmentID;

            if (GlobalVar2.gbYzpOrder.rPASSINFO == null)
            {
                GlobalVar2.gbYzpOrder.rPASSINFO = new YzpBtoC.YZPpassINFO[psg.Length];
                for (int i = 0; i < psg.Length; i++)
                {
                    GlobalVar2.gbYzpOrder.rPASSINFO[i] = new YzpBtoC.YZPpassINFO();
                }
            }
            string [] namecard;
            try
            {
                //namecard = File.ReadAllLines("c:\\namecard.txt");
                namecard = Options.GlobalVar.PassengersArray;
            }
            catch
            {
                namecard = null;
            }
            for (int i = 0; i < psg.Length; i++)
            {
                GlobalVar2.gbYzpOrder.rPASSINFO[i].bPASSNAME = psg[i].Split('-')[0];
                try
                {//紧急提交没有"-"
                    string cardno = psg[i].Split('-')[1];

                    if (cardno[0] == 'P')
                    {
                        //数组 psg[] 的值全部是P0 导致下面语句没有准确取得身份证号码。因乘客姓名已被拼音排序，故直接用i值对应亦可 //commentted by chenqj
                        //int index = int.Parse (cardno.Substring(1));
                        //if (index > 0) index -= 1;
                        //if (psg[i].Split('-')[0].Trim().ToUpper() == namecard[index].Split('-')[0].Trim().ToUpper())
                        //{
                        //    cardno = namecard[index].Split('-')[1].Trim();
                        //}
                        if (psg[i].Split('-')[0].Trim().ToUpper() == namecard[i].Split('-')[0].Trim().ToUpper())
                        {
                            cardno = namecard[i].Split('-')[1].Trim();
                        }
                    }
                    GlobalVar2.gbYzpOrder.rPASSINFO[i].bCARDNO =cardno ;
                }
                catch
                {
                }
                if (psg[i].Split('-')[0].IndexOf("(CHD)") > 0)
                {
                    GlobalVar2.gbYzpOrder.rPASSINFO[i].bPASSTYPE = "2";
                }
                else
                {
                    GlobalVar2.gbYzpOrder.rPASSINFO[i].bPASSTYPE = "1";
                }
                try
                {//紧急提交没有"-"
                    if (psg[i].Split('-')[1].Length == 15 || psg[i].Split('-')[1].Length == 18)
                    {
                        GlobalVar2.gbYzpOrder.rPASSINFO[i].bCARDTYPE = "1";
                    }
                    else
                    {
                        GlobalVar2.gbYzpOrder.rPASSINFO[i].bCARDTYPE = "3";
                    }
                }
                catch
                {
                }
            }
            //***************************2008-7-16.置全局变量gbYzpOrder中的bPassINFO:姓名及证件**************************结束

            //****************************************************2008-7-15提交到BtoC后台***************************************开始

            if (GlobalVar2.gbUserModel == 1)//提交到BtoC后台
            {

             //   return submit_yzp_pnr(pnr, tl, remark, tmpName, phone, personcount,
             //flightno, bunk, date, citypair,
             //fareTkt, fareReal, taxBuild, taxFuel, usergain, lirun, fareTotal);
                YzpBtoC.SubmitPnrBtoC btoc = new YzpBtoC.SubmitPnrBtoC(GlobalVar2.gbYzpOrder );

                if (Options.GlobalVar.B2CIsAutoSubmit)
                {
                    btoc.Size = new System.Drawing.Size(1, 1);
                    btoc.ShowInTaskbar = false;
                }
                btoc.ShowDialog();
                GlobalVar2.gbPassegersInEasyVersion = "";
                //MessageBox.Show("提交订单到BtoC后台完成！");
                throw new Exception("BTOC");
                
            }
            //*****************************************************2008-7-15提交到BtoC后台***************************************结束

            np.AddPara("numTkPrc", fareTkt);//票面
            np.AddPara("numRealPrc", fareReal);//实收
            np.AddPara("numBasePrc", taxBuild);//机建
            np.AddPara("numOilPrc", taxFuel);//燃油
            np.AddPara("numPoint", usergain);//返点 0-100整数
            np.AddPara("numGain", lirun);//利润：具体金额
            np.AddPara("numTotal", fareTotal);//合计(人数＊（实收+机建+燃油））

            XmlNode nodeAtk = np.AddPara("ATK", "");
            int ni = 2;


            if (flightno[0] == "") ni = 0;
            else if (flightno[1] == "")
            {
                ni = 1;
            }
            else ni = 2;

            for (int i = 0; i < ni; i++)
            {
                XmlNode recNode = doc.CreateNode(XmlNodeType.Element, "REC", "");
                nodeAtk.AppendChild(recNode);//生成一个REC

                XmlNode nodeFlight = doc.CreateNode(XmlNodeType.Element, "FlightNo", "");
                nodeFlight.AppendChild(doc.CreateTextNode(flightno[i]));
                recNode.AppendChild(nodeFlight);

                XmlNode nodeBunk = doc.CreateNode(XmlNodeType.Element, "Bunk", "");
                nodeBunk.AppendChild(doc.CreateTextNode(bunk[i]));
                recNode.AppendChild(nodeBunk);

                XmlNode nodeDate = doc.CreateNode(XmlNodeType.Element, "Date", "");
                nodeDate.AppendChild(doc.CreateTextNode(date[i]));
                recNode.AppendChild(nodeDate);

                XmlNode nodeCityPair = doc.CreateNode(XmlNodeType.Element, "CityPair", "");
                nodeCityPair.AppendChild(doc.CreateTextNode(citypair[i]));
                recNode.AppendChild(nodeCityPair);
            }
            string strReq = np.GetXML();
            string strRet = ws.getEgSoap(strReq);

            if (strRet != "")
            {
                NewPara np1 = new NewPara(strRet);
                if (strRet != "" && np1.FindTextByPath("//eg/cm") == "RetSubmitPNR" && np1.FindTextByPath("//eg/OperationFlag") == "SaveSucc")
                {//提交成功
                    //MessageBox.Show("提交成功！");
                    //发送有新订单消息
                    string[] pspt = np1.FindTextByPath("//eg/Passports").Split('~');
                    string body = "";
                    for (int i = 0; i < pspt.Length; i++)
                    {
                        body += pspt[i];
                    }
                    if (body.Trim() != "")
                    {
                        int outlength = 0;
                        EagleProtocol ep = new EagleProtocol();
                        ep.MsgType = 6;
                        ep.MsgBody = body;
                        ep.SetMsgLength();
                        char[] sendstring = ep.ConvertToString(out outlength);
                        EagleAPI.EagleSend(sendstring, outlength);
                    }
                    EagleAPI2.SaveSubmittedPnr(pnr);
                    GlobalVar2.gbPassegersInEasyVersion = "";
                    return true;
                }
            }
            else
            {//提交失败
            }
            GlobalVar2.gbPassegersInEasyVersion = "";
            return false;
        
        }
        /// <summary>
        /// 用于原简版提交
        /// </summary>
        public bool submit_easy_pnr(string pnr,string tl,DataTable dtTmp,string remark,
            string fareTkt,string fareReal,string taxBuild,string taxFuel,string usergain,string lirun,string fareTotal)
        {
            if (EagleAPI2.IsPnrSubmitted(pnr)) return false;
            EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
            NewPara np = new NewPara();

            XmlDocument doc = np.getRoot();

            np.AddPara("cm", "SubmitPNR");
            np.AddPara("User", GlobalVar.loginName);
            np.AddPara("PNR", pnr);
            np.AddPara("TL", tl);
            np.AddPara("Bz", remark);

            //下面值传入时为"0",若dtTmp为空，则为0
            if (dtTmp != null)
            {
                try
                {
                    string remark1 = dtTmp.Rows[0]["Remark1"].ToString().Trim();
                    string str1 = remark1.Split(';')[0];
                    string str2 = remark1.Split(';')[1];
                    string fare1 = str1.Split('~')[0];
                    string fare2 = str2.Split('~')[0];
                    string taxb1 = str1.Split('~')[1];
                    string taxb2 = str2.Split('~')[1];
                    string taxf1 = str1.Split('~')[2];
                    string taxf2 = str2.Split('~')[2];
                    string gain1 = str1.Split('~')[3];
                    string gain2 = str2.Split('~')[3];
                    fareTkt = string.Format("{0}", int.Parse(fare1) + int.Parse(fare2));
                    taxBuild = string.Format("{0}", int.Parse(taxb1) + int.Parse(taxb2));
                    taxFuel = string.Format("{0}", int.Parse(taxf1) + int.Parse(taxf2));
                    double dlirun =
                        (double.Parse(fare1) * double.Parse(gain1) / 100.0 + double.Parse(fare2) * double.Parse(gain2) / 100.0);
                    lirun = dlirun.ToString("f0");
                    double dfareReal = double.Parse(fareTkt) - dlirun;
                    fareReal = dfareReal.ToString("f0");

                    if (gain1 == gain2 || (int.Parse(gain2) == 0 && int.Parse(fare2) == 0)) usergain = gain1;
                    else
                    {
                        double tempfloat = (dlirun) / double.Parse(fareTkt) * 100.0;
                        usergain = tempfloat.ToString("f2");
                    }
                    string pCount = dtTmp.Rows[0]["PersonCount"].ToString();
                    int ifareTotal =
                        (int.Parse(fareReal) + int.Parse(taxBuild) + int.Parse(taxFuel)) * int.Parse(pCount);
                    fareTotal= ifareTotal.ToString();
                }
                catch
                {
                }
            }
            np.AddPara("numTkPrc", fareTkt);//票面
            np.AddPara("numRealPrc", fareReal);//实收
            np.AddPara("numBasePrc", taxBuild);//机建
            np.AddPara("numOilPrc", taxFuel);//燃油
            np.AddPara("numPoint", usergain);//返点 0-100整数
            np.AddPara("numGain", lirun);//利润：具体金额
            np.AddPara("numTotal", fareTotal);//合计(人数＊（实收+机建+燃油））

            GetRemainMoney rm = new GetRemainMoney();
            rm.getcurmoney();
            string cmoney = rm.getcurmoney();
            if (cmoney != "A")
            {
                GlobalVar.f_CurMoney = cmoney;
                //GlobalVar.f_CurMoney = EagleAPI.substring(GlobalVar.f_CurMoney, 0, GlobalVar.f_CurMoney.Length - 2) + "00";
                if (float.Parse(GlobalVar.f_CurMoney) <= -500F)
                {
                    //MessageBox.Show("对不起，您的余额不足！");
                    MessageBox.Show("抱歉，您的余额不足！请及时冲款", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if (GlobalVar2.gbUserModel == 0)//added by chenqj
                        return false;
                }
                float yujiYe = float.Parse(GlobalVar.f_CurMoney) - float.Parse(fareTotal);
                if (yujiYe < 0)
                    MessageBox.Show("严重警告，本笔金额预计将超支,请及时冲款", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (yujiYe < 500)
                    MessageBox.Show("警告:本笔金额扣款后预计余额将少于500,请及时冲款", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (yujiYe < 1000)
                    MessageBox.Show("提示:本笔金额扣款后预计余额将少于1000,请及时冲款", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //MessageBox.Show("获取余额失败！");
                MessageBox.Show("抱歉，获取余额失败！请尝试重新提交", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }


            if (dtTmp != null)
            {
                string NAMES = "";
                string[] arr_name = dtTmp.Rows[0]["Names"].ToString().Split(';');
                string[] arr_cardid = dtTmp.Rows[0]["CardNumbers"].ToString().Split(';');
                for (int i = 0; i < arr_name.Length; i++)
                {
                    NAMES += ";" + arr_name[i] + "-" + arr_cardid[i];
                }
                NAMES = NAMES.Substring(1);

                np.AddPara("Phone", dtTmp.Rows[0]["Phone"].ToString());
                np.AddPara("PersonCount", dtTmp.Rows[0]["PersonCount"].ToString());
                np.AddPara("Names", NAMES);

                XmlNode nodeAtk = np.AddPara("ATK", "");
                int ni = 2;

                string[] flightno = new string[ni];
                flightno[0] = dtTmp.Rows[0]["FlightNumber1"].ToString();
                flightno[1] = dtTmp.Rows[0]["FlightNumber2"].ToString();
                string[] bunk = new string[ni];
                bunk[0] = dtTmp.Rows[0]["Bunk1"].ToString();
                bunk[1] = dtTmp.Rows[0]["Bunk2"].ToString();
                string[] date = new string[ni];
                date[0] = dtTmp.Rows[0]["Date1"].ToString();
                date[1] = dtTmp.Rows[0]["Date2"].ToString();
                string[] citypair = new string[ni];
                citypair[0] = dtTmp.Rows[0]["CityPair1"].ToString();
                citypair[1] = dtTmp.Rows[0]["CityPair2"].ToString();
                if (flightno[0] == "") ni = 0;
                else if (flightno[1] == "")
                {
                    ni = 1;
                }
                else ni = 2;

                for (int i = 0; i < ni; i++)
                {
                    XmlNode recNode = doc.CreateNode(XmlNodeType.Element, "REC", "");
                    nodeAtk.AppendChild(recNode);//生成一个REC

                    XmlNode nodeFlight = doc.CreateNode(XmlNodeType.Element, "FlightNo", "");
                    nodeFlight.AppendChild(doc.CreateTextNode(flightno[i]));
                    recNode.AppendChild(nodeFlight);

                    XmlNode nodeBunk = doc.CreateNode(XmlNodeType.Element, "Bunk", "");
                    nodeBunk.AppendChild(doc.CreateTextNode(bunk[i]));
                    recNode.AppendChild(nodeBunk);

                    XmlNode nodeDate = doc.CreateNode(XmlNodeType.Element, "Date", "");
                    nodeDate.AppendChild(doc.CreateTextNode(date[i]));
                    recNode.AppendChild(nodeDate);

                    XmlNode nodeCityPair = doc.CreateNode(XmlNodeType.Element, "CityPair", "");
                    nodeCityPair.AppendChild(doc.CreateTextNode(citypair[i]));
                    recNode.AppendChild(nodeCityPair);
                }
            }
            else
            {
                np.AddPara("Phone", "");
                np.AddPara("PersonCount", "0");
                np.AddPara("Names", "");
                XmlNode nodeAtk = np.AddPara("ATK", "");
                XmlNode recNode = doc.CreateNode(XmlNodeType.Element, "REC", "");
                nodeAtk.AppendChild(recNode);//生成一个REC

                XmlNode nodeFlight = doc.CreateNode(XmlNodeType.Element, "FlightNo", "");
                nodeFlight.AppendChild(doc.CreateTextNode("黑屏"));
                recNode.AppendChild(nodeFlight);

                XmlNode nodeBunk = doc.CreateNode(XmlNodeType.Element, "Bunk", "");
                nodeBunk.AppendChild(doc.CreateTextNode("黑屏"));
                recNode.AppendChild(nodeBunk);

                XmlNode nodeDate = doc.CreateNode(XmlNodeType.Element, "Date", "");
                nodeDate.AppendChild(doc.CreateTextNode("黑屏"));
                recNode.AppendChild(nodeDate);

                XmlNode nodeCityPair = doc.CreateNode(XmlNodeType.Element, "CityPair", "");
                nodeCityPair.AppendChild(doc.CreateTextNode("黑屏"));
                recNode.AppendChild(nodeCityPair);
            }
            string strReq = np.GetXML();
            string strRet = ws.getEgSoap(strReq);

            if (strRet != "")
            {
                NewPara np1 = new NewPara(strRet);
                if (strRet != "" && np1.FindTextByPath("//eg/cm") == "RetSubmitPNR" && np1.FindTextByPath("//eg/OperationFlag") == "SaveSucc")
                {//提交成功
                    //if (dtTmp != null) MessageBox.Show("提交成功！");
                    //发送有新订单消息
                    string[] pspt = np1.FindTextByPath("//eg/Passports").Split('~');
                    string body = "";
                    for (int i = 0; i < pspt.Length; i++)
                    {
                        body += pspt[i];
                    }
                    if (body.Trim() != "")
                    {
                        int outlength = 0;
                        EagleProtocol ep = new EagleProtocol();
                        ep.MsgType = 6;
                        ep.MsgBody = body;
                        ep.SetMsgLength();
                        char[] sendstring = ep.ConvertToString(out outlength);
                        EagleAPI.EagleSend(sendstring, outlength);
                    }
                    EagleAPI2.SaveSubmittedPnr(pnr);
                    return true;

                }
            }
            else
            {//提交失败
            }
            return false;
        }
    }

    class WebService
    {   
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rn">行程单号</param>
        /// <param name="on">OFFICE号</param>
        /// <param name="en">电子客票号</param>
        /// <returns></returns>
        static public string CanPrint(string rn,string on,string en)
        {
            try
            {
                EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
                NewPara np = new NewPara();
                np.AddPara("cm", "CanPrint");
                np.AddPara("User", GlobalVar.loginName);
                np.AddPara("RecieptNumber", rn);
                np.AddPara("CfgNumber", on);
                np.AddPara("ETNumber", en);
                string send = np.GetXML();
                string recv = ws.getEgSoap(send);
                if (recv == "") return "FAILED";
                NewPara n = new NewPara(recv);
                if (n.FindTextByPath("//eg/cm") == "RetCanPrint")
                    return n.FindTextByPath("//eg/OperationFlag");
            }
            catch { }
            return "FALSE";
        }
        /// <summary>
        /// 取消保单号(作废保单号)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="insurancetype">保单类型</param>
        /// <param name="insuranceno">保单号</param>
        /// <returns></returns>
        static public bool CancelInsurance(string user, string insurancetype, string insuranceno)
        {
            try
            {
                EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
                NewPara np = new NewPara();
                np.AddPara("cm", "setIncIsCancel");
                np.AddPara("UserName", user);
                np.AddPara("IncName", insurancetype);
                np.AddPara("IncNo", insuranceno);
                string send = np.GetXML();
                string recv = ws.getEgSoap(send);
                if (recv == "") return false;
                NewPara n = new NewPara(recv);
                if (n.FindTextByPath("//eg/cm") == "RetDelInsState")
                   if("savesucc" == n.FindTextByPath("//eg/OperationFlag").ToLower())
                       return true;
            }
            catch { }
            return false;
        }
        /// <summary>
        /// 取政策。返回xml串
        /// </summary>
        /// <param name="flightnos"></param>
        /// <param name="flightdate">短日期格式串"2007-12-5"</param>
        /// <param name="citybeg"></param>
        /// <param name="cityend"></param>
        /// <returns></returns>
        static public string wsGetPolicies(string flightnos, string flightdate, string citybeg, string cityend)
        {
            try
            {
                string srvUrl = "";
                if (GlobalVar.serverAddr == GlobalVar.ServerAddr.Eagle)
                {
                    //if (GlobalVar2.gbConnectType == 1) srvUrl = "http://yinge.eg66.com/WS3/egws.asmx";
                    //if (GlobalVar2.gbConnectType == 2) srvUrl = "http://wangtong.eg66.com/WS3/egws.asmx";
                }
                //else
                    srvUrl = GlobalVar.WebServer;
                EagleWebService.wsKernal ws = new EagleWebService.wsKernal(srvUrl);

                //WS.egws ws = new WS.egws(srvUrl);
                NewPara np = new NewPara();
                np.AddPara("cm", "GetPromot");
                np.AddPara("UserName", GlobalVar.loginName);
                np.AddPara("Airs", flightnos);
                np.AddPara("Date", flightdate);
                np.AddPara("BeginCity", citybeg);
                np.AddPara("EndCity", cityend);
                string strSent = np.GetXML();
                string strPolicy = ws.getEgSoap(strSent);
                return strPolicy;
            }
            catch { }
            return "";
        }
    }
    /// <summary>
    /// 取弹出式公告
    /// </summary>
    class GetServer
    {
        static public string [] getPubMes()
        {

            EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
            NewPara np = new NewPara();
            np.AddPara("cm", "GetPubMes");
            np.AddPara("User", GlobalVar.loginName);
            string send = np.GetXML();
            string recv = ws.getEgSoap(send);
            if (recv == "") return null;
            NewPara n = new NewPara(recv);
            if (n.FindTextByPath("//eg/cm") == "RetPubMes")
            {
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(recv);
                XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode("NewsRecs");
                string[] ret = new string[xn.ChildNodes.Count];
                for (int i = 0; i < xn.ChildNodes.Count; i++)
                {
                    XmlNode xnin = xn.ChildNodes[i];
                    for (int j = 0; j < xnin.ChildNodes.Count; j++)
                    {
                        if (j == 0) ret[i] = "\r发布者：";
                        if (j == 1) ret[i] += "\r发布内容：";
                        if (j == 2) ret[i] += "\r发布时间：";
                        ret[i] += xnin.ChildNodes[j].InnerText;
                    }
                }
                return ret;
            }
            return null;
        }
    }
}

namespace ePlus.LocalOperation
{
    /// <summary>
    /// 写入本地日志，在关闭程序时传至服务器
    /// </summary>
    public class LogOperation
    {
        static public bool b_OK = false;
        public void writelocallog(string send,string recv)
        {
            string ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\EagleLog.mdb;";
            OleDbConnection cn = new OleDbConnection();
            try
            {
                cn.ConnectionString = ConnStr;
                cn.Open();
            }
            catch
            {
                return;//系统环境无法打开Access库，直接跳过
                //MessageBox.Show("本地数据库连接失败，请检查文件是否存在");
            }
            string cmString = "insert into LocalLog (STRINGSEND,STRINGRECV) Values ('{0}','{1}')";
            cmString = string.Format(cmString, send, recv);
            OleDbCommand cmd = new OleDbCommand(cmString, cn);
            try
            {
                if (mystring.substring(send, 0, 2).ToLower() == "rt")
                {
                    cmd.CommandText = "delete from LocalLog where stringsend='" + send + "'";
                    cmd.ExecuteNonQuery();
                }


                cmd.CommandText = cmString;
                if (cmd.ExecuteNonQuery() != 1) MessageBox.Show("保存本地数据失败");

            }
            catch { };
            cn.Close();    
            
        }
        public static void staticwritelocallog(string send, string recv)
        {
            LocalOperation.LogOperation lo = new ePlus.LocalOperation.LogOperation();
//            lo.writelocallog(send, GlobalVar.loginName + "于" + System.DateTime.Now.ToString() + "执行指令<br>" + "<font color=Red>" + send + "</font>" + "<br>" + "返回<font color=Blue>" + recv + "</font>");
            lo.writelocallog(send, GlobalVar.loginName + "于" + System.DateTime.Now.ToString() + "执行" + send + "\r\n返回：" + recv + "\r\n");
        }
        /// <summary>
        /// 关闭程序时，将日志保存到服务器中
        /// </summary>
        public void writeserverlog()
        {
            string ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\EagleLog.mdb;";
            OleDbConnection cn = new OleDbConnection();
            try
            {
                cn.ConnectionString = ConnStr;
                cn.Open();
            }
            catch
            {
                b_OK = true;
                return;//系统环境无法打开Access库，直接跳过
                //MessageBox.Show("本地数据库连接失败，请检查文件是否存在");
            }
            //上传有用日志
            OleDbCommand cmd = new OleDbCommand("select * from LocalLog where stringsend like '%etdz%'"
                + " or stringsend like 'vt%'"
                + " or stringsend like 'trfd%'"
                + " or stringsend like 'trfx%'"
                + " or stringsend like 'trfu%'"
                + " or stringsend like 'fn%'"
                //+" or stringsend like 'rt%'"
                + " or stringsend like '%xepnr%'"
                + " or stringsend like '%@%'"
                + " or stringsend like 'ss%'"
                + " or stringsend like 'sd%'"
                + " or stringsend like 'nm%'"
                + " or stringsend like '打印行程单%'"
                + " or stringsend like 'i~rt%'"
                + " or stringsend like 'xe%'"
                + " or stringsend like 'rt%'"
                + " or stringsend like 'rrt%'"
                + " or stringsend like '%\\%'", cn);
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            adapter.Fill(dt);
            string sendstr = "";
            string recvstr = "";
            log ml = new log();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //sendstr += "<br>[" + i.ToString("d2") + "]" + dt.Rows[i]["STRINGSEND"];

                    //recvstr += "<br>[" + i.ToString("d2") + "]" + dt.Rows[i]["STRINGRECV"];
                    sendstr += "\r\n[" + i.ToString("d2") + "]" + dt.Rows[i]["STRINGSEND"];
                    recvstr += "\r\n[" + i.ToString("d2") + "]" + dt.Rows[i]["STRINGRECV"];
                }
                
                if (sendstr != "" || recvstr != "")
                {
                    sendstr = sendstr.Substring(2);
                    recvstr = recvstr.Substring(2);
                    ml.submitlog(sendstr, recvstr);
                    if (ml.b_submit)
                    {
                        cmd.CommandText = "delete * from LocalLog";
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            string than = "";
            if (GlobalVar.PackageNumberSend > GlobalVar.PackageNumberRecv) than = ">";
            else if (GlobalVar.PackageNumberSend < GlobalVar.PackageNumberRecv) than = "<";
            else than = "=";
            if(GlobalVar.PackageNumberSend>5)
                ml.submitlog("包数目" + than, string.Format("发出包{0}，收到包{1}", GlobalVar.PackageNumberSend, GlobalVar.PackageNumberRecv));
            cn.Close();
            b_OK = true;
        }
        public void writeserverlogpart()//上传指令条数
        {
            int rocs = 200;
            string ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\EagleLog.mdb;";
            OleDbConnection cn = new OleDbConnection();
            try
            {
                cn.ConnectionString = ConnStr;
                cn.Open();
            }
            catch
            {
                return;//系统环境无法打开Access库，直接跳过
                //MessageBox.Show("本地数据库连接失败，请检查文件是否存在");
            }

            //OleDbCommand cmd = new OleDbCommand("select top " + rocs.ToString() + " * from LocalLog order by id desc", cn);
            OleDbCommand cmd = new OleDbCommand("select * from LocalLog where stringsend like 'etdz%' "
                + "or stringsend like 'fn%' or stringsend like 'rt%' or stringsend like 'xepnr%' or stringsend like '@%' or stringsend like '\\%'", cn);
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            adapter.Fill(dt);
            string sendstr = "";
            string recvstr = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sendstr += "<br>[" + i.ToString("d2") + "]" + dt.Rows[i]["STRINGSEND"];
                recvstr += "<br>[" + i.ToString("d2") + "]" + dt.Rows[i]["STRINGRECV"];
            }
            log ml = new log();
            if (sendstr != "" || recvstr != "")
            {
                sendstr = sendstr.Substring(4);
                recvstr = recvstr.Substring(4);
                ml.submitlog(sendstr, recvstr);
                if (ml.b_submit)
                {
                    //cmd.CommandText = "delete from LocalLog where id in (select top "+rocs.ToString()+" id from LocalLog order by id desc)";
                    cmd.CommandText = "delete * from LocalLog";
                    cmd.ExecuteNonQuery();
                }
            }
            cn.Close();
        }
    }

}

namespace ePlus.PrintHyx
{
    /// <summary>
    /// SubmitHYX上传保险数据的类
    /// </summary>
    class HyxStructs
    {
        public string UserID;
        //public string OperateTime;
        public string eNumber;
        public string IssueNumber;
        public string NameIssued;
        public string CardType = "";
        public string CardNumber;
        public string Remark;
        public string IssuePeriod;
        public string IssueBegin;
        public string IssueEnd;
        public string SolutionDisputed = "";
        public string NameBeneficiary = "";
        public string Signature = "";
        public string SignDate = "";
        public string InssuerName = "";
        public string Pnr = "";
        public bool SubmitInfo()
        {
            try
            {
                EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
                NewPara np = new NewPara();
                np.AddPara("cm", "SubmitHYX");
                np.AddPara("UserID", UserID);
                np.AddPara("eNumber", eNumber);
                np.AddPara("IssueNumber", IssueNumber);
                np.AddPara("NameIssued", NameIssued);
                np.AddPara("CardType", CardType);
                np.AddPara("CardNumber", CardNumber);
                np.AddPara("Remark", Remark);
                np.AddPara("IssuePeriod", IssuePeriod);
                np.AddPara("IssueBegin", IssueBegin);
                np.AddPara("IssueEnd", IssueEnd);
                np.AddPara("SolutionDisputed", SolutionDisputed);
                np.AddPara("NameBeneficiary", NameBeneficiary);
                np.AddPara("Signature", Signature);
                np.AddPara("SignDate", SignDate);
                np.AddPara("InssuerName", InssuerName);
                np.AddPara("Pnr", Pnr);
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                if (strRet != "")
                {
                    NewPara np1 = new NewPara(strRet);
                    if (np1.FindTextByPath("//eg/cm") == "RetSubmitHYX" && np1.FindTextByPath("//eg/OperationFlag") == "SaveSucc")
                    {
                        return true;
                    }
                    else
                    {
                        if (np1.FindTextByPath("//eg/OperationFlag") != "SaveSucc")
                            MessageBox.Show("数据提交失败！请检查保单号是否已被使用，或网络是否正常！");
                        else
                            MessageBox.Show("保存失败，服务指令不可用");
                    }
                }
                else
                {
                    MessageBox.Show("服务器繁忙，不能提供服务，请重试");                    
                }
            }
            catch
            {
                MessageBox.Show("服务器繁忙，请重试");                
            }
            return false;
        }
    }


}