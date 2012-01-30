using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace EagleWebService
{
    public class kernalFunc
    {
        public kernalFunc(string webaddr)
        {
            ws = new wsKernal(webaddr);
        }
        wsKernal ws;

        private void is_null_return(string strRet)
        {
            if (strRet == "")
                throw new Exception("��ȡwebservice���Ϊ�գ���ַ" + ws.Url);
        }
        private void is_not_expected_return(string retCmd, string expected)
        {
            if (retCmd != expected)
                throw new Exception("��ȡwebservice��������Ϊ" + retCmd + "����ַ" + ws.Url);
        }
        private void handle_exception(Exception ex,string wsCmd)
        {
            //MessageBox.Show(wsCmd + ":" + ex.Message);
            EagleString.EagleFileIO.LogWrite(wsCmd + ":" + ex.Message);
        }
        /// <summary>
        /// ȡһ����Ϣ��������PNR
        /// </summary>
        public string PnrUnchecked(string username)
        {
            string wsCmd = "GetUncheckedPNR";
            NewPara np = new NewPara();
            np.AddPara("cm", wsCmd);
            np.AddPara("UserID", username);
            string strReq = np.GetXML();
            string strRet = ws.getEgSoap(strReq);
            is_null_return(strRet);
            NewPara np1 = new NewPara(strRet);
            string retcmd = np1.FindTextByPath("//eg/cm");

            is_not_expected_return(retcmd, "RetGetUncheckedPNR");
            string retres = np1.FindTextByPath("//eg/Pnr");
            return retres;
        }
        public void SubmitEticketInfomation(EagleString.RtResult rtres, int totalfair, int pnrstate, int totalbuild, int totalfuel, int ipid, ref bool bFlag)
        {
            string etno = "";
            switch (rtres.FLAG_OF_PNR)
            {
                case EagleString.PNR_FLAG.CANCELLED:
                    bool flag = false;
                    SubmitEticketInfomation("CANCELLED", "", ' ', "", DateTime.Now, "", ' ', "", DateTime.Now, 0, 1, rtres.PNR, "", 0, 0, 0, ref flag);
                    return;
                    break;
                case EagleString.PNR_FLAG.ETICKET:
                    etno = string.Join(";", rtres.TKTNO);
                    break;
                case EagleString.PNR_FLAG.MARRIED:
                    etno = "WAIT FOR RESUBMIT1";
                    break;
                case EagleString.PNR_FLAG.NORMAL:
                    etno = "WAIT FOR RESUBMIT2";
                    break;
            }
            if (rtres.SEGMENG.Length > 1)
            {
                SubmitEticketInfomation(etno, rtres.SEGMENG[0].Flight, rtres.SEGMENG[0].Bunk, rtres.SEGMENG[0].Citypair,
                    rtres.SEGMENG[0].Date, rtres.SEGMENG[1].Flight, rtres.SEGMENG[1].Bunk, rtres.SEGMENG[1].Citypair, rtres.SEGMENG[1].Date, totalfair, pnrstate, rtres.PNR, string.Join(";", rtres.Name_CARDS), totalbuild, totalfuel, ipid, ref bFlag);   

            }
            else if (rtres.SEGMENG.Length == 1)
            {
                SubmitEticketInfomation(etno, rtres.SEGMENG[0].Flight, rtres.SEGMENG[0].Bunk, rtres.SEGMENG[0].Citypair,
                    rtres.SEGMENG[0].Date, "", ' ', "", DateTime.Now, totalfair, pnrstate, rtres.PNR, string.Join(";", rtres.Name_CARDS), totalbuild, totalfuel, ipid, ref bFlag);
            }
        }


        /// <summary>
        /// �ύ���ӿ�Ʊ��Ϣ.��fl2Ϊ""ʱ���ڶ����μ���Ч
        /// </summary>
        /// <param name="etno">�����;�</param>
        public void SubmitEticketInfomation(string etno, string fl, char bunk, string cp, DateTime dt, string fl2, char bunk2, string cp2, DateTime dt2, int totalfare, int pnrstate, string pnr, string psgs, int totalbuild, int totalfuel, int ipid,ref bool bFlag)
        {
            string wsCmd = "SubmitETicket";
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", wsCmd);
                np.AddPara("etNumber", etno);
                np.AddPara("FlightNumber1", fl);
                np.AddPara("Bunk1", bunk.ToString());
                np.AddPara("CityPair1", cp);
                np.AddPara("Date1", dt.ToString());
                if (fl2 == "")
                {
                    np.AddPara("FlightNumber2", "");
                    np.AddPara("Bunk2", "");
                    np.AddPara("CityPair2", "");
                    np.AddPara("Date2", "");
                }
                else
                {
                    np.AddPara("FlightNumber2", fl2);
                    np.AddPara("Bunk2", bunk2.ToString());
                    np.AddPara("CityPair2", cp2.ToString());
                    np.AddPara("Date2", dt2.ToString());
                }


                np.AddPara("TotalFC", totalfare.ToString());

                np.AddPara("State", pnrstate.ToString());
                np.AddPara("Pnr", pnr);
                //np.AddPara("DecFeeState",  "2");//������Ӧ�����Դ����ڿͻ���ȡ��

                np.AddPara("Passenger", psgs);

                np.AddPara("numBasePrc", totalbuild.ToString());
                np.AddPara("numFuel", totalfuel.ToString());
                np.AddPara("IpId", ipid.ToString());
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                is_null_return(strRet);

                NewPara np1 = new NewPara(strRet);
                string retcmd = np1.FindTextByPath("//eg/cm");

                is_not_expected_return(retcmd, "RetSubmitETicket");
                string retres = np1.FindTextByPath("//eg/OperationFlag");
                bFlag = (retres == "SaveSucc");
            }
            catch (Exception ex)
            {
                handle_exception(ex, wsCmd);
            }
        }

        /// <summary>
        /// service name = GetFCȡƱ�ۣ���ȡ���أ�����ȡ������������false
        /// </summary>
        /// <param name="citypair"></param>
        /// <returns></returns>
        public bool GetFC_CityPair(string citypair, string sqlstring, OleDbConnection cn, 
                                    out float bf, out float bc, out float by)
        {
            string wsCmd = "GetFC";
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
                else//ȡ�������۸�
                {
                    NewPara np = new NewPara();
                    np.AddPara("cm", wsCmd);
                    np.AddPara("FROM", citypair.Substring(0, 3));
                    np.AddPara("TO", citypair.Substring(3, 3));
                    string strReq = np.GetXML();
                    string strRet = ws.getEgSoap(strReq);

                    is_null_return(strRet);

                    NewPara np1 = new NewPara(strRet);
                    string retcmd = np1.FindTextByPath("//eg/cm");

                    is_not_expected_return(retcmd, "RetGetFC");

                    string priceF = np1.FindTextByPath("//eg/BUNKF");
                    string priceC = np1.FindTextByPath("//eg/BUNKC");
                    string priceY = np1.FindTextByPath("//eg/BUNKY");
                    if (!(priceF != "" && priceC != "" && priceY != ""))
                        throw new Exception("ȡƱ�۽������Ϊ�մ�ֵ");
                    try
                    {
                        by = float.Parse(priceY);
                        bf = float.Parse(priceF);
                        bc = float.Parse(priceC);
                        return true;
                    }
                    catch
                    {
                        by = bf = bc = 0F;
                        return false;

                    }
                }
            }
            catch(Exception ex)
            {
                handle_exception(ex, wsCmd);
            }
            return false;
        }
        /// <summary>
        /// ȡ��Ӧ����������֤������GetPassnger = GetCardID
        /// </summary>
        /// <param name="psgername">�˿�����</param>
        /// <returns>���֤�����ö��ŷָ�</returns>
        public string GetPassenger(string psgername)
        {
            return GetCardID(psgername);
        }
        public string GetCardID(string psgername)
        {
            string wsCmd = "GetPassenger";
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", wsCmd);
                np.AddPara("Passenger", psgername);
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                is_null_return(strRet);
                NewPara np1 = new NewPara(strRet);
                string retcmd = np1.FindTextByPath("//eg/cm");

                is_not_expected_return(retcmd, "RetGetPassenger");

                return np1.FindTextByPath("//eg/CardID");
            }
            catch(Exception ex)
            {
                handle_exception(ex, wsCmd);
            }
            return "";
        }

        /// <summary>
        /// ɢ��ƴ��
        /// </summary>
        /// <param name="username">����Ա�ʻ���</param>
        /// <param name="psgername">�˿�����</param>
        /// <param name="cardid">�˿�֤����</param>
        /// <param name="groupid">��id</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        public bool Group_Add(string username,string psgername,string cardid,string groupid)
        {
            string wsCmd = "AddToGroup";
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", wsCmd);
                np.AddPara("UserID", username);
                np.AddPara("Name", psgername);
                np.AddPara("CardID", cardid);
                np.AddPara("GroupTicketID", groupid);
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                is_null_return(strRet); 
                NewPara np1 = new NewPara(strRet);
                string retcmd = np1.FindTextByPath("//eg/cm");
                string retres = np1.FindTextByPath("//eg/OperationFlag");

                is_not_expected_return(retcmd, "RetAddToGroup");
                
                if (retres == "SaveSucc")
                {
                    return true;
                }

            }
            catch(Exception ex)
            {
                handle_exception(ex, wsCmd);
            }
            return false;
        }
        /// <summary>
        /// �г��Ŷ�Ʊ
        /// </summary>
        /// <param name="username">�û���</param>
        /// <param name="fromto">���ж�</param>
        /// <param name="date">����</param>
        /// <param name="userclass">�û�����</param>
        /// <returns>����WebService��XML��</returns>
        public string Group_List(string username,string fromto,DateTime date,char userclass)
        {
            string wsCmd = "ListGroupTicket";
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", wsCmd);
                np.AddPara("FromTo", fromto);
                np.AddPara("Date", date.ToShortDateString());
                np.AddPara("UserID", username);
                np.AddPara("RebateType", userclass.ToString());
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                is_null_return(strRet);
                NewPara np1 = new NewPara(strRet);
                string retcmd = np1.FindTextByPath("//eg/cm");

                is_not_expected_return(retcmd, "RetListGroupTicket");

                string retres = np1.FindTextByPath("//eg/Result").Trim();
                if (retres != "")
                {
                    return retres;
                }

            }
            catch(Exception ex)
            {
                handle_exception(ex, wsCmd);
            }
            return "";
        }

        /// <summary>
        /// ��������ʽ/����ʽ��Ϣ
        /// </summary>
        /// <param name="username">�������ʺ���</param>
        /// <param name="context">��������</param>
        /// <param name="dtBeg">��Ч����</param>
        /// <param name="dtEnd">ʧЧ����</param>
        /// <param name="NoticeType">��Ϣ���� (����ʽ/����ʽ)</param>
        public void Submit_Notice_Scroll(
            string username,string context,DateTime dtBeg,DateTime dtEnd,string NoticeType)
        {
            string wsCmd = "SubmitScrollString";

            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", wsCmd);
                np.AddPara("UserID", username);
                np.AddPara("Context", username + ":" + context);
                np.AddPara("BegTime", dtBeg.ToString());
                np.AddPara("EndTime", dtEnd.ToString());
                np.AddPara("NoticeType", NoticeType);
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);

                is_null_return(strRet);

                NewPara np1 = new NewPara(strRet);
                string retcmd = np1.FindTextByPath("//eg/cm");

                is_not_expected_return(retcmd, "RetSubmitScrollString");
                
                string retres = np1.FindTextByPath("//eg/OperationFlag");
                if (retres == "SaveSucc")
                {
                    MessageBox.Show("�����ɹ�");
                    return;
                }
            }
            catch(Exception ex)
            {
                handle_exception(ex, wsCmd);
            }
            MessageBox.Show("����ʧ��");
            return;
        }
        /// <summary>
        /// ȡ�������棬0:������1:���װ�
        /// </summary>
        public string Get_Notice_Scroll(string username,string noticeType)
        {
            string wsCmd = "GetCurrentScrollString";
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", wsCmd);
                np.AddPara("User", username);
                np.AddPara("NoticeType", noticeType);
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                is_null_return(strRet);
                NewPara np1 = new NewPara(strRet);
                string retcmd = np1.FindTextByPath("//eg/cm");

                is_not_expected_return(retcmd, "RetGetCurrentScrollString");
                                
                return np1.FindTextByPath("//eg/Context").Replace('\r', ' ').Replace('\n', ' ');
            }
            catch(Exception ex)
            {
                handle_exception(ex, wsCmd);
            }
            return "";
        }
        /// <summary>
        /// ԭsubmitlog
        /// </summary>
        public void WriteLogToServer(string username,string strSend,string strRecv,ref bool IsSucceed)
        {
            string wsCmd = "WriteLog";
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", wsCmd);
                np.AddPara("User", username);
                np.AddPara("Cmd", strSend);
                np.AddPara("ReturnResult", strRecv);
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);

                is_null_return(strRet);

                NewPara np1 = new NewPara(strRet);

                string retcmd = np1.FindTextByPath("//eg/cm");
                is_not_expected_return(retcmd, "RetWriteLog");

                string retres = np1.FindTextByPath("//eg/OperationFlag");
                if (retres == "SaveSucc")
                {
                    IsSucceed = true;
                    return;
                }
            }
            catch(Exception ex)
            {
                handle_exception(ex, wsCmd);
            }
            IsSucceed = false;
            return;
        }
        public void WriteLogToServer_LOGIN(string username, ref bool IsSucceed)
        {
            string txtout = "Login��/";
            string txtin = "Login��/��½ʱ��Ϊ  " + DateTime.Now.ToString();
            WriteLogToServer(username, txtout, txtin, ref IsSucceed);
        }
        public void WriteLogToServer_FN(
                    string username, string strSend, string strRecv,string pnr, ref bool IsSucceed)
        {
            if (strSend.Length < 10) return;
            if (strSend.Trim().Substring(0, 2).ToLower() != "fn") return;
            string txtout = "FN��/" + pnr;
            WriteLogToServer(username, txtout, strRecv, ref IsSucceed);
        }
        public void WriteLogToServer_ETDZ(
            string username, string strSend, string strRecv, string pnr, ref bool IsSucceed)
        {
            if (strSend.Length < 4) return;
            if (strSend.Trim().Substring(0, 4).ToLower() != "etdz") return;

            string str_analysis = strRecv;

            if (!(str_analysis.IndexOf("CNY") >= 0
                && str_analysis.IndexOf(".00") > 0
                && str_analysis.Length < 30)) return;
            string temp = str_analysis;
            while (temp[temp.Length - 1] == '\r' || temp[temp.Length - 1] == '\n')
            {
                temp = temp.Substring(0, temp.Length - 1);
                temp = temp.Trim();
            }
            string recogPnr = temp.Substring(temp.Length - 5);
            if (recogPnr.ToUpper() != pnr.ToUpper()) return;

            string txtout = "ETDZ��/" +pnr;
            string txtin = strRecv;
            WriteLogToServer(username, txtout, txtin, ref IsSucceed);
        }
        /// <summary>
        /// �ύ�û�����PNRʱ����״̬��0:����,1:����,2:ȡ��,3:��Ʊ
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pnr"></param>
        /// <param name="stat">0:����,1:����,2:ȡ��,3:��Ʊ</param>
        /// <param name="office"></param>
        /// <param name="IsSucceed"></param>
        public void SubmitPnrState(string username,string pnr,int stat,string office, ref bool IsSucceed)
        {
            string wsCmd = "SubmitPnrState";
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", "SubmitPnrState");
                np.AddPara("User", username);
                np.AddPara("PNR", pnr.ToUpper());
                np.AddPara("State", stat.ToString());
                np.AddPara("Office", office.Substring(0,6));
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                is_null_return(strRet);
                NewPara np1 = new NewPara(strRet);
                string retcmd = np1.FindTextByPath("//eg/cm");
                is_not_expected_return(retcmd, "RetSubmitPnrState");
                string retres = np1.FindTextByPath("//eg/OperationFlag");
                IsSucceed = (retres == "SaveSucc");
            }
            catch (Exception ex)
            {
                handle_exception(ex, wsCmd);
            }
        }


        /// <summary>
        /// �ǳ�
        /// </summary>
        /// <param name="username">�ǳ��û���</param>
        /// <param name="IsSucceed">�Ƿ�ɹ�</param>
        public void LogOut(string username,ref bool IsSucceed)
        {
            string wsCmd = "Logout";
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", wsCmd);
                np.AddPara("User", username);
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);

                is_null_return(strRet);
                NewPara np1 = new NewPara(strRet);
                string retcmd = np1.FindTextByPath("//eg/cm");
                is_not_expected_return(retcmd, "RetLogout");
                string retres = np1.FindTextByPath("//eg/LogoutStat");
                IsSucceed = (retres == "LogoutSucc");
            }
            catch (Exception ex)
            {
                handle_exception(ex, wsCmd);
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="username">�û�</param>
        /// <param name="password">����</param>
        /// <param name="outxml">����xml��</param>
        /// <param name="bFlag">�Ƿ�ɹ�����</param>
        public void LogIn(string username, string password,ref string outxml,ref bool bFlag)
        {
            string wsCmd = "Login";
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", wsCmd);
                np.AddPara("UserName", username);
                np.AddPara("PassWord", password);
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                is_null_return(strRet);
                NewPara np2 = new NewPara(strRet);
                string retcmd = np2.FindTextByPath("//eg/cm").Trim();
                is_not_expected_return(retcmd, "RetLogin");
                string retres = np2.FindTextByPath("//eg/LoginFlag").Trim();
                if (retres != "LoginSucc") throw new Exception("��¼ʧ��,�����û���������");
                outxml = strRet;
                bFlag = true;
            }
            catch (Exception ex)
            {
                handle_exception(ex, wsCmd);
                bFlag = false;
            }
        }

        /// <summary>
        /// �޸�����
        /// </summary>
        /// <param name="username">�û���</param>
        /// <param name="newpassword">������</param>
        /// <param name="IsSucceed">�Ƿ��޸ĳɹ�</param>
        public void ChangePassword(string username, string newpassword, ref bool IsSucceed)
        {
            string wsCmd = "ChgPassword";
            IsSucceed = false;
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", wsCmd);
                np.AddPara("UserName", username);
                np.AddPara("Password", newpassword);
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);

                is_null_return(strRet);

                NewPara np1 = new NewPara(strRet);

                string retcmd = np1.FindTextByPath("//eg/cm");

                is_not_expected_return(retcmd, "RetChgPassword");

                string retres = np1.FindTextByPath("//eg/ChgPWDStat");

                IsSucceed = (retres == "ChgPassSucc");
            }
            catch (Exception ex)
            {
                handle_exception(ex, wsCmd);
            }
        }

        /// <summary>
        /// ԭgetcurmoney,GetCloseBalanceȡ�û���ǰ���
        /// </summary>
        /// <param name="username"></param>
        public void GetCloseBalance(string username,ref bool IsSucceed,ref string yue)
        {
            string wsCmd = "GetCloseBalance";
            IsSucceed = false;
            try
            {
                NewPara np1 = new NewPara();
                np1.AddPara("cm", wsCmd);
                np1.AddPara("UserName", username);
                string strReq = np1.GetXML();
                string strRet = ws.getEgSoap(strReq);

                is_null_return(strRet);

                NewPara np = new NewPara(strRet);
                string retcmd = np.FindTextByPath("//eg/cm");

                is_not_expected_return(retcmd, "RetCloseBalance");

                string retres = np.FindTextByPath("//eg/UserYE");

                yue = retres;
            }
            catch (Exception ex)
            {
                handle_exception(ex, wsCmd);
            }
        }


        public void SubmitPnr(
            string username,     //user

            string pnr,         //pnr
            DateTime tl,          //time limit 
            string remark,      //remark
            string names,       //like "name-card;name2-card2"
            string phone,       //telephone
            int countp,         // passenger count
            string [] flightno,
            char [] bunk,
            DateTime [] date,
            string [] citypair,
            int fareTkt,        //ticket price (not include tax)
            double fareReal,    //the price where ticket price - lirun
            int taxBuild,       //airport build tax
            int taxFuel,        //oil tax
            double usergain,    //sales commission (0-100)
            double lirun,       //profit    (single)
            double fareTotal,    //(fareReal + tax) * countp
            
            ref bool IsSucceed,      //out: does submit succeed
            ref string passports //out : if submitted, inform the username's administrator , split with '~'
            )
        {
            string wsCmd = "SubmitPNR";
            try
            {
                NewPara np = new NewPara();

                XmlDocument doc = np.getRoot();

                np.AddPara("cm", wsCmd);

                np.AddPara("User", username);
                np.AddPara("PNR", pnr);
                np.AddPara("TL", tl.ToString());
                np.AddPara("Bz", remark);
                np.AddPara("Phone", phone);
                np.AddPara("PersonCount", countp.ToString());
                np.AddPara("Names", names);
                np.AddPara("numTkPrc", fareTkt.ToString());
                np.AddPara("numRealPrc", fareReal.ToString("f2"));
                np.AddPara("numBasePrc", taxBuild.ToString());
                np.AddPara("numOilPrc", taxFuel.ToString());
                np.AddPara("numPoint", usergain.ToString());
                np.AddPara("numGain", lirun.ToString("f2"));
                np.AddPara("numTotal", fareTotal.ToString());

                XmlNode nodeAtk = np.AddPara("ATK", "");

                for (int i = 0; i < flightno.Length; ++i)
                {
                    XmlNode recNode = doc.CreateNode(XmlNodeType.Element, "REC", "");
                    nodeAtk.AppendChild(recNode);//����һ��REC

                    XmlNode nodeFlight = doc.CreateNode(XmlNodeType.Element, "FlightNo", "");
                    nodeFlight.AppendChild(doc.CreateTextNode(flightno[i]));
                    recNode.AppendChild(nodeFlight);

                    XmlNode nodeBunk = doc.CreateNode(XmlNodeType.Element, "Bunk", "");
                    nodeBunk.AppendChild(doc.CreateTextNode(bunk[i].ToString()));
                    recNode.AppendChild(nodeBunk);

                    XmlNode nodeDate = doc.CreateNode(XmlNodeType.Element, "Date", "");
                    nodeDate.AppendChild(doc.CreateTextNode(date[i].ToString()));
                    recNode.AppendChild(nodeDate);

                    XmlNode nodeCityPair = doc.CreateNode(XmlNodeType.Element, "CityPair", "");
                    nodeCityPair.AppendChild(doc.CreateTextNode(citypair[i]));
                    recNode.AppendChild(nodeCityPair);
                }
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                is_null_return(strRet);
                NewPara np2 = new NewPara(strRet);
                string retcmd = np2.FindTextByPath("//eg/cm");
                is_not_expected_return(retcmd, "RetSubmitPNR");
                string retres = np2.FindTextByPath("//eg/OperationFlag");
                IsSucceed = (retres == "SaveSucc");
                if (IsSucceed)
                {
                    passports = np2.FindTextByPath("//eg/Passports").Trim();
                }
            }
            catch (Exception ex)
            {
                handle_exception(ex, wsCmd);
            }

        }
        /// <summary>
        /// �г̵����Ƿ���Ա���ӡ
        /// </summary>
        /// <param name="username"></param>
        /// <param name="rn"></param>
        /// <param name="on"></param>
        /// <param name="en"></param>
        /// <param name="IsSucceed"></param>
        public void CanPrint(string username,string rn, string on, string en,ref bool IsSucceed)
        {
            string wsCmd = "CanPrint";
            IsSucceed = false;
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", wsCmd);
                np.AddPara("User", username);
                np.AddPara("RecieptNumber", rn);
                np.AddPara("CfgNumber", on);
                np.AddPara("ETNumber", en);
                string send = np.GetXML();
                string recv = ws.getEgSoap(send);
                is_null_return(recv);
                NewPara n = new NewPara(recv);
                string retcmd = n.FindTextByPath("//eg/cm");
                is_not_expected_return(retcmd, "RetCanPrint");
                string retres = n.FindTextByPath("//eg/OperationFlag");
                IsSucceed = (retres == "TRUE");
            }
            catch (Exception ex)
            {
                handle_exception(ex, wsCmd);
            }
        }
        /// <summary>
        /// ȡ������
        /// </summary>
        /// <param name="user"></param>
        /// <param name="insurancetype"></param>
        /// <param name="insuranceno"></param>
        public void CancelInsurance(string username, string insurancetype, string insuranceno,ref bool IsSucceed)
        {
            string wsCmd = "setIncIsCancel";
            IsSucceed = false;
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", wsCmd);
                np.AddPara("UserName", username);
                np.AddPara("IncName", insurancetype);
                np.AddPara("IncNo", insuranceno);
                string send = np.GetXML();
                string recv = ws.getEgSoap(send);
                is_null_return(recv);
                NewPara n = new NewPara(recv);
                string retcmd = n.FindTextByPath("//eg/cm");
                is_not_expected_return(retcmd, "RetDelInsState");
                string retres = n.FindTextByPath("//eg/OperationFlag").ToLower();
                IsSucceed = ("savesucc"==retres);
            }
            catch (Exception ex)
            {
                handle_exception(ex, wsCmd);
            }
            IsSucceed = false;
        }
        /// <summary>
        /// ȡ����,webservice��ΪGetPromot������xml��
        /// </summary>
        /// <param name="flightnos">���ŷָ��Ķ������ţ���MU2501,CZ3344</param>
        /// <param name="flightdate">�����ڸ�ʽ��"2007-12-5"</param>
        /// <param name="citypair"></param>
        /// <returns></returns>
        public string GetPolicies(string username,string flightnos, string flightdate, string citypair)
        {
            string wsCmd = "GetPromot";
            string outxml = "";
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", wsCmd);
                np.AddPara("UserName", username);
                np.AddPara("Airs", flightnos);
                np.AddPara("Date", flightdate);
                np.AddPara("BeginCity", citypair.Substring(0,3));
                np.AddPara("EndCity", citypair.Substring(3));
                string strSent = np.GetXML();
                string strPolicy = ws.getEgSoap(strSent);
                is_null_return(strPolicy);
                NewPara np2 = new NewPara(strPolicy);
                string retcmd = np2.FindTextByPath("//eg/cm");
                is_not_expected_return(retcmd, "RetPormot");
                outxml = strPolicy;
            }
            catch (Exception ex)
            {
                handle_exception(ex, wsCmd);
                outxml = "";

            }
            return outxml;
        }
        public void getPubMes(string username,out string [] messages)
        {
            string wsCmd = "GetPubMes";
            messages = null;
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", wsCmd);
                np.AddPara("User", username);
                string send = np.GetXML();
                string recv = ws.getEgSoap(send);
                is_null_return(recv);
                NewPara n = new NewPara(recv);
                string retcmd = n.FindTextByPath("//eg/cm");
                is_not_expected_return(retcmd, "RetPubMes");
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(recv);
                XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode("NewsRecs");
                messages = new string[xn.ChildNodes.Count];
                for (int i = 0; i < xn.ChildNodes.Count; i++)
                {
                    XmlNode xnin = xn.ChildNodes[i];
                    for (int j = 0; j < xnin.ChildNodes.Count; j++)
                    {
                        if (j == 0) messages[i] = "\r�����ߣ�";
                        if (j == 1) messages[i] += "\r�������ݣ�";
                        if (j == 2) messages[i] += "\r����ʱ�䣺";
                        messages[i] += xnin.ChildNodes[j].InnerText;
                    }
                }

            }
            catch (Exception ex)
            {
                handle_exception(ex, wsCmd);
            }

        }
        public void SubmitHYX(
            string username,
            string eNumber, //electronic number
            string IssueNumber, //insurance number
            string NameIssued,//passenger name
            string CardType,
            string CardNumber,
            string Remark,
            string IssuePeriod,
            string IssueBegin,
            string IssueEnd,
            string SolutionDisputed,
            string NameBeneficiary,
            string Signature,
            string SignDate,
            string InssuerName,
            string Pnr,
            ref bool IsSucceed
            )
        {
            string wsCmd = "SubmitHYX";
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", "SubmitHYX");
                np.AddPara("UserID", username);
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
                is_null_return(strRet);
                NewPara np1 = new NewPara(strRet);
                string retcmd = np1.FindTextByPath("//eg/cm");
                is_not_expected_return(retcmd, "RetSubmitHYX");
                string retres = np1.FindTextByPath("//eg/OperationFlag").ToLower();
                IsSucceed = (retres == "savesucc");
                if (!IsSucceed) throw new Exception("������Ϣ����ʧ�ܣ�������");
            }
            catch (Exception ex)
            {
                handle_exception(ex, wsCmd);
            }
        }
        /// <summary>
        /// �޶�Ӧ������ȫ����0
        /// </summary>
        /// <param name="citypair">6 charactors</param>
        /// <param name="tf">actually tf is distance</param>
        /// <param name="tc"></param>
        /// <param name="ty">this value is useful</param>
        public void FC_Read(string citypair,ref int tf,ref int tc,ref int ty)
        {
            string wsCmd = "GetFC";
            tf = tc = ty = 0;
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", wsCmd);
                np.AddPara("FROM", citypair.Substring(0, 3));
                np.AddPara("TO", citypair.Substring(3, 3));
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                is_null_return(strRet);
                NewPara np1 = new NewPara(strRet);
                tf = (int)float.Parse(np1.FindTextByPath("//eg/BUNKF").Trim());
                tc = (int)float.Parse(np1.FindTextByPath("//eg/BUNKC").Trim());
                ty = (int)float.Parse(np1.FindTextByPath("//eg/BUNKY").Trim());
            }
            catch (Exception ex)
            {
                handle_exception(ex, wsCmd);
                tf = tc = ty = 0;
            }
        }
        /// <summary>
        /// �������д���Ӧ���жԼ۸�
        /// </summary>
        /// <param name="citypair">���ж�</param>
        /// <param name="tf">ͷ�Ȳռ۸�(��ʵΪ����)</param>
        /// <param name="tc">����ռ۸�(��ʵ����;)</param>
        /// <param name="ty">���ò�ȫ��(���м۸�ļ������)</param>
        /// <param name="IsSucceed">д��ɹ����</param>
        public void FC_Write(string citypair, int tf, int tc, int ty,ref bool IsSucceed)
        {
            string wsCmd = "SaveFC";
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", wsCmd);

                np.AddPara("FROM", citypair.Substring(0, 3));
                np.AddPara("TO", citypair.Substring(3));
                np.AddPara("BUNKF", tf.ToString()); //np.AddPara("BUNKF", f_Bunk_F.ToString());
                np.AddPara("BUNKC", tc.ToString("f2"));
                np.AddPara("BUNKY", ty.ToString("f2"));
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                is_null_return(strRet);
                NewPara np1 = new NewPara(strRet);
                string retcmd = np1.FindTextByPath("//eg/cm");
                is_not_expected_return(retcmd, "RecSaveFC");
                string retres = np1.FindTextByPath("//eg/OperationFlag").ToLower();
                IsSucceed = ("savesucc" == retres);
            }
            catch (Exception ex)
            {
                handle_exception(ex, wsCmd);
            }

        }
        /// <summary>
        /// GetPNRs��������lb_Submited��pnrs��;�ָ�
        /// state="0"Ϊδ����
        /// state="1"Ϊ�Ѵ���ͨ��
        /// 2Ϊ�Ѵ���δͨ��
        /// 3Ϊ�Ѿ�ɾ��,���װ��pnr״̬��ע������pnr�Ĳ���״̬
        /// </summary>
        /// <param name="state"></param>
        /// 
        /// <returns></returns>
        public void GetSubmittedPnrsWith(string username, int state, ref string pnrs)
        {
            string wsCmd = "GetPNRs";
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", "GetPNRs");
                np.AddPara("User", username);
                np.AddPara("PNRState", state.ToString());
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                is_null_return(strRet);
                NewPara np1 = new NewPara(strRet);
                string retcmd = np1.FindTextByPath("//eg/cm");

                is_not_expected_return(retcmd, "RetGetPNRs");

                pnrs = np1.FindTextByPath("//eg/PNR");
            }
            catch (Exception ex)
            {
                handle_exception(ex, wsCmd);
            }
        }
        public void SetPNRStateDelete(string username,string pnr, ref bool IsSucceed)
        {
            string wsCmd = "SetPNRStateDelete";
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", wsCmd);
                np.AddPara("User", username);
                np.AddPara("PNR", pnr.Trim());
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                is_null_return(strRet);
                NewPara np1 = new NewPara(strRet);
                string retcmd = np1.FindTextByPath("//eg/cm");
                is_not_expected_return(retcmd, "RetSetPNRStateDelete");
                string retres = np1.FindTextByPath("//eg/OperationFlag").Trim();
                IsSucceed = ("SaveSucc" == retres);
            }
            catch (Exception ex)
            {
                handle_exception(ex, wsCmd);
            }
        }
        /// <summary>
        /// ��PNR���пۿ�������Ƿ�ɹ������
        /// </summary>
        public void DecFee(string pnr, int totalFC,ref bool succeed,ref float balance)
        {
            string wsCmd = "DecFee";
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", wsCmd);
                np.AddPara("Pnr", pnr);
                np.AddPara("TicketPrice", totalFC.ToString());
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                is_null_return(strRet);
                NewPara np1 = new NewPara(strRet);
                string retcmd = np1.FindTextByPath("//eg/cm");
                is_not_expected_return(retcmd, "RetDecFee");
                string retres = np1.FindTextByPath("//eg/DecStat").Trim();
                succeed = ("DecSucc" == retres);
                balance = float.Parse(np1.FindTextByPath("//eg/NewUserYe"));
            }
            catch (Exception ex)
            {
                handle_exception(ex, wsCmd);
                succeed = false;
            }
        }
        /// <summary>
        /// etdzʱԤ�ύ,����DecFeestat =0
        /// </summary>
        public void PreSubmitEticketWhenEtdz(string username, string pnr, int Decfeestat, string office,ref bool succeed)
        {
            string wsCmd = "PreSubmitETicket";
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", wsCmd);
                np.AddPara("UserID",username);
                np.AddPara("Pnr", pnr);
                np.AddPara("DecFeeState", Decfeestat.ToString());
                np.AddPara("IpId", office);
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                is_null_return(strRet);
                NewPara np1 = new NewPara(strRet);
                string retcmd = np1.FindTextByPath("//eg/cm");
                is_not_expected_return(retcmd, "RetPreSubmitETicket");
                string retres = np1.FindTextByPath("//eg/OperationFlag").Trim();
                succeed = ("SaveSucc" == retres);
            }
            catch (Exception ex)
            {
                handle_exception(ex, wsCmd);
                succeed = false;
            }
        }
        public void updateutraffic(string username, int type, int amount, ref bool succeed)
        {
            string wsCmd = "updateutraffic";
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", wsCmd);
                np.AddPara("uid", username);
                np.AddPara("traffictype", type.ToString());
                np.AddPara("traffic", amount.ToString());
                np.AddPara("updatetime", DateTime.Now.ToString());

                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                is_null_return(strRet);

                NewPara np1 = new NewPara(strRet);
                string retcmd = np1.FindTextByPath("//eg/cm");
                is_not_expected_return(retcmd, "reupdateut");
                string retres = np1.FindTextByPath("//eg/rwsult").Trim();
                succeed = ("1" == retres);
            }
            catch (Exception ex)
            {
                handle_exception(ex, wsCmd);
                succeed = false;
            }
        }
        /// <summary>
        /// �г�����Ʊ��Ϣ:��10������֮�����Ϣ
        /// </summary>
        /// <param name="citypair"></param>
        /// <param name="date"></param>
        /// <param name="outxml"></param>
        public void SpecialTicketList(string citypair, DateTime date,ref string outxml)
        {
            string wsCmd = "GetKBunkInfo";
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", wsCmd);
                np.AddPara("fromto", citypair);
                np.AddPara("date", date.ToShortDateString());
                string send = np.GetXML();
                string recv = ws.getEgSoap(send);
                is_null_return(recv);
                NewPara np2 = new NewPara(recv);
                string retcmd = np2.FindTextByPath("//eg/cm");
                is_not_expected_return(retcmd, "ReplyKBunkInfo");
                outxml = recv;
            }
            catch (Exception ex)
            {
                handle_exception(ex, wsCmd);
                outxml = "";
            }
        }

        public void SpecialTicketRequest(string username, int dataid, DateTime flightdate,char bunk, int count, string pnr,
            string[] psgers, string[] cardnos, string[] phones ,ref bool ret,ref string[] passport)
        {
            string wsCmd = "ApplyKBunkInfo";
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", wsCmd);
                np.AddPara("applyinfo", "");
                string path = "//eg/applyinfo";
                np.AddPara(path,"applyuser", username);
                np.AddPara(path,"kpolicyid", dataid.ToString());
                np.AddPara(path,"date", flightdate.ToShortDateString());
                np.AddPara(path, "bunk", bunk.ToString());
                np.AddPara(path,"bunkamount", count.ToString());
                np.AddPara(path,"pnr", pnr);
                np.AddPara(path,"passengers", "");
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(np.GetXML());
                XmlNode xn = xd.SelectSingleNode(path + "/passengers");
                for (int i = 0; i < count; ++i)
                {
                    XmlNode root = xn;
                    XmlNode nodeNew;
                    nodeNew = xd.CreateNode(XmlNodeType.Element, "passenger", "");
                    nodeNew.AppendChild(xd.CreateTextNode(""));
                    root.AppendChild(nodeNew);

                    root = nodeNew;
                    nodeNew = xd.CreateNode(XmlNodeType.Element, "name", "");
                    nodeNew.AppendChild(xd.CreateTextNode(psgers[i]));
                    root.AppendChild(nodeNew);
                    nodeNew = xd.CreateNode(XmlNodeType.Element, "passport", "");
                    nodeNew.AppendChild(xd.CreateTextNode(cardnos[i]));
                    root.AppendChild(nodeNew);
                    nodeNew = xd.CreateNode(XmlNodeType.Element, "phone", "");
                    nodeNew.AppendChild(xd.CreateTextNode(phones[i]));
                    root.AppendChild(nodeNew);
                }
                string recv = ws.getEgSoap(xd.OuterXml);
                is_null_return(recv);
                NewPara np2 = new NewPara(recv);
                string retcmd = np2.FindTextByPath("//eg/cm");
                is_not_expected_return(retcmd, "ApplyKBunkInfo");
                string retres = np2.FindTextByPath("//eg/result").Trim();
                ret = (retres.ToLower()=="succeed");
                passport = null;
                if (ret)
                {
                    string ps = np2.FindTextByPath("//eg/passport").Trim();
                    if (ps != "")
                    {
                        ps = ps.Substring(0, ps.Length - 1);
                        passport = ps.Split(new char[] { ',', '|' });
                    }
                }
                //<eg><cm>ApplyKBunkInfo</cm><result>Succeed</result><passport>C03E031322404E29B57629A282B77CA4                  </passport></eg>
            }
            catch (Exception ex)
            {
                handle_exception(ex, wsCmd);
            }

        }

        /// <summary>
        /// ��ʾһ��δ����Ķ���
        /// </summary>
        /// <param name="outxml">���ؽ��</param>
        public void SpecialTicketAppliedNoHandleToDisplay(ref string outxml)
        {
            string wsCmd = "DisplayKBunkInfo";
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", wsCmd);
                np.AddPara("orderid", "0");
                string send = np.GetXML();
                string recv = ws.getEgSoap(send);
                is_null_return(recv);
                NewPara np2 = new NewPara(recv);
                string retcmd = np2.FindTextByPath("//eg/cm");
                is_not_expected_return(retcmd, "ReturnUnProcess");
                string res = np2.FindTextByPath("//eg/result");
                if (res != null && res == "error") outxml = "";
                else outxml = recv;
            }
            catch (Exception ex)
            {
                handle_exception(ex, wsCmd);
                outxml = "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <param name="date"></param>
        /// <param name="stat"></param>
        /// <param name="pnr"></param>
        /// <param name="flightno"></param>
        /// <param name="bunk"></param>
        /// <param name="handle_price"></param>
        /// <param name="original_price"></param>
        /// <param name="remark"></param>
        /// <param name="bFlag"></param>
        /// <param name="passport"></param>
        /// <param name="sendxml">�����xml�������ó����������÷�����</param>
        public void SpecialTicketAppliedHandle(int id, string username, DateTime date, int stat,
            string pnr, string flightno, char bunk, int handle_price, int original_price, string remark, string applyuser, ref bool bFlag, ref string passport,
            ref string sendxml)
        {
            string wsCmd = "ProcessKOrder";
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", wsCmd);
                np.AddPara("order_id", id.ToString());
                np.AddPara("process_user", username);
                np.AddPara("process_date", date.ToString());
                np.AddPara("process_state", stat.ToString());
                np.AddPara("PNR", pnr);
                np.AddPara("flight_number", flightno);
                np.AddPara("new_bunk", bunk.ToString());
                np.AddPara("process_price", handle_price.ToString());
                np.AddPara("original_price", original_price.ToString());
                np.AddPara("remark1", remark);
                np.AddPara("applyuser", applyuser);

                string send = np.GetXML();
                sendxml = send;
                string recv = ws.getEgSoap(send);
                is_null_return(recv);
                NewPara np2 = new NewPara(recv);
                string retcmd = np2.FindTextByPath("//eg/cm");
                is_not_expected_return(retcmd, "ReturnProcessOrder");
                string retres = np2.FindTextByPath("//eg/result") ;
                bFlag = (retres == "Succeed");
                passport = "";
                if (bFlag)
                {
                    passport = np2.FindTextByPath("//eg/passport").Trim();
                }
            }
            catch (Exception ex)
            {
                handle_exception(ex, wsCmd);
                bFlag = false;
            }
        }

        private void uploadSpecifyLog(object username)
        {
            return ;//ȡ�����ϴ�����
            //1.��ָ�����ڵ���־�ļ�
            string[] file = new string[] { "2008-12-30.log" };//��Ҫ�ֶ��޸ĵ�����1:��ʾҪѰ�ҵ���־�ļ�
            string[] included = new string[] { "VLDHJ" };//��Ҫ�ֶ��޸ĵ�����2
            for (int i = 0; i < file.Length; ++i)
            {
                string content = System.IO.File.ReadAllText(Application.StartupPath + "\\Log\\" + file[i], Encoding.UTF8);
                for (int j = 0; j < included.Length; ++j)
                {
                    int pos = content.IndexOf(included[j]);
                    if (pos < 0) continue;
                    bool b = false;
                    WriteLogToServer((string)username, included[j], content, ref b);
                }
            }
        }
        public void UploadSpecifyLog(string username)
        {
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(uploadSpecifyLog));
            thread.Start((object)username);
        }
        public string[] PnrNeedDeduct(string username)
        {
            string wsCmd = "GetUnPayPnr";
            try
            {
                NewPara np = new NewPara();
                np.AddPara("cm", wsCmd);
                np.AddPara("UserName", username);

                string send = np.GetXML();
                string recv = ws.getEgSoap(send);
                is_null_return(recv);
                NewPara np2 = new NewPara(recv);
                string retcmd = np2.FindTextByPath("//eg/cm");
                is_not_expected_return(retcmd, "RetUnPayPnr");
                string retres = np2.FindTextByPath("//eg/Pnrs");
                return retres.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            }
            catch (Exception ex)
            {
                handle_exception(ex, wsCmd);
            }
            return null;
        }
    }
}


