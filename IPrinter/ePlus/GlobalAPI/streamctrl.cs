using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using gs.para;

namespace ePlus
{
    enum streamctrl_enum {XE_FAIL,XEPNR_FAIL , NOT_ENOUGH_MONEY , PRE_SUBMIT_FAIL ,NONE, SD_SS,TEST_ACCOUNT,HAS_NO_PNR};
    class streamctrl
    {
        public static streamctrl_enum send(string content, int msgtype)
        {
            try
            {
                if (GlobalVar.loginName.ToLower() == "bb" && content.ToLower().Contains("etdz")) return streamctrl_enum.TEST_ACCOUNT;

                #region xepnr操作权限控制
                {
                    string t_xepnr = content.ToLower().Trim();
                    if (t_xepnr.IndexOf("xepnr") == 0 || t_xepnr.IndexOf("~xepnr") >= 0 || t_xepnr.IndexOf("xe") == 0 || t_xepnr.IndexOf("~xe") >= 0)
                    {//并且要在不为ETDZ的情况下，不然在ETDZ的时候，还没有判断就返回了XEPNR:即在ETDZ时不做以下判断
                        if (EagleAPI.substring(content, 0, 4).ToLower() == "etdz" || content.ToLower().IndexOf("~etdz") >= 0) ;
                        else
                        {
                            string t_pnr = EagleAPI.etstatic.Pnr;
                            pnr_statistics ps = new pnr_statistics();
                            ps.pnr = t_pnr;
                            ps.state = "2";
                            if (!Model.md.b_00C && t_xepnr.Contains("xepnr"))//不能取消他人PNR，则进行提交判断
                            {
                                if ((!ps.submit()))//取消失败，则返回失败
                                {
                                    if (t_xepnr.IndexOf("xepnr") == 0 || t_xepnr.IndexOf("~xepnr") >= 0)
                                        return streamctrl_enum.XEPNR_FAIL;
                                    else
                                        return streamctrl_enum.XE_FAIL;
                                }
                            }
                        }
                    }
                }
                #endregion
                if (EagleAPI.substring(content, 0, 4).ToLower() == "etdz" || content.ToLower().IndexOf("~etdz") >= 0)
                {
                    GlobalVar.b_etdz = true;
                }

                GlobalVar.b_pat = (EagleAPI.substring(content, 0, 4).ToLower() == "pat:");

                GlobalVar.b_cmd_trfd_AM = ((EagleAPI.substring(content, 0, 7).ToLower() == "trfd:am") || (EagleAPI.substring(content, 0, 7).ToLower() == "trfd am"));
                GlobalVar.b_cmd_trfd_M = ((EagleAPI.substring(content, 0, 6).ToLower() == "trfd:m") || (EagleAPI.substring(content, 0, 6).ToLower() == "trfd m"));
                GlobalVar.b_cmd_trfd_L = ((EagleAPI.substring(content, 0, 6).ToLower() == "trfd:l") || (EagleAPI.substring(content, 0, 6).ToLower() == "trfd l"));

                if (EagleAPI.substring(content, 0, 4).ToLower() == "trfx")
                {
                    content = content.Replace('*', (char)0x1A);
                    content = content.Replace("/", "");
                }

                if (content == "i" || content == "ig")
                {
                    GlobalVar.b_etdz = false;
                    GlobalVar.b_enoughMoney = false;
                    GlobalVar.b_endbook = false;
                }

                //if(EagleAPI.substring(content,0,1)=="@" || EagleAPI.substring(content,0,1)=="\\")//0926
                //bool bEnough = false;
                #region etdz金额不足控制
                if (GlobalVar.b_etdz)
                {
                    try
                    {
                        bool bIsDecFee = false;//是否扣过款了
                        if (GlobalVar.newEticket.peaplecount < 1) GlobalVar.newEticket.peaplecount = 1;
                        string srvUrl = "";
                        if (GlobalVar.serverAddr == GlobalVar.ServerAddr.Eagle)
                        {
                            //if (GlobalVar2.gbConnectType == 1) srvUrl = "http://yinge.eg66.com/WS3/egws.asmx";
                            //if (GlobalVar2.gbConnectType == 2) srvUrl = "http://wangtong.eg66.com/WS3/egws.asmx";
                        }
                        //else
                            srvUrl = GlobalVar.WebServer;
                        EagleWebService.wsKernal ws = new EagleWebService.wsKernal(srvUrl);

                        NewPara np = new NewPara();
                        np.AddPara("cm", "IsDecFee");
                        np.AddPara("pnr", EagleAPI.etstatic.Pnr);
                        string strSent = np.GetXML();
                        string strreturn ="";
                        if (GlobalVar.serverAddr == GlobalVar.ServerAddr.Eagle)
                        {
                            strreturn = ws.getEgSoap(strSent);
                        }
                        if (!string.IsNullOrEmpty(strreturn.Trim()))
                        {
                            np = new NewPara(strreturn);
                            string strCm = np.FindTextByPath("//eg/cm");
                            string strIsDecFee = np.FindTextByPath("//eg/IsDecFee");
                            if (strCm == "ReIsDecFee" && strIsDecFee.Trim() == "1")
                            {
                                EagleAPI.LogWrite(EagleAPI.etstatic.Pnr + "已经扣过款，不再进行余额检查和扣款!\r\n");
                                bIsDecFee = true;
                            }
                        }

                        if (!bIsDecFee && (float.Parse(GlobalVar.f_CurMoney) < GlobalVar.f_limitMoneyPerTicket * GlobalVar.newEticket.peaplecount) && float.Parse(GlobalVar.f_CurMoney) < 0)//&& 1==0)float.Parse(GlobalVar.f_CurMoney) < GlobalVar.f_fc || 
                        {
                            //余额不足
                            EagleAPI.LogWrite("<计算是否够金额出票>当前余额为" + GlobalVar.f_CurMoney + ",预计扣款额为" + GlobalVar.f_limitMoneyPerTicket.ToString() + "*" + GlobalVar.newEticket.peaplecount.ToString());
                            GlobalVar.b_etdz = false;
                            GlobalVar.b_enoughMoney = false;
                            GlobalVar.b_endbook = false;
                            content = "i";
                            return streamctrl_enum.NOT_ENOUGH_MONEY;
                        }
                        else
                        {

                            string[] cmdarray = content.Split('~');
                            bool bHasRtPnrCmd = false;
                            foreach (string c in cmdarray)
                            {
                                if (c.ToLower().Length >= 7 && c.ToLower().IndexOf("rt") == 0)
                                {
                                    bHasRtPnrCmd = true;
                                    break;
                                }
                            }
                            if ((!bHasRtPnrCmd) && (!GlobalVar.bUsingConfigLonely))
                            {
                                //return streamctrl_enum.HAS_NO_PNR;
                            }
                            GlobalVar.b_enoughMoney = true;
                            content = content + "";
                            //初步提交，状态0，预提交
                            ePlus.eTicket.etPreSubmit etmp = new ePlus.eTicket.etPreSubmit();
                            bool bsubed = false;
                            for (int iSubmit = 0; iSubmit < 3; iSubmit++)
                            {
                                if (etmp.submitinfo())
                                {
                                    if (GlobalVar.serverAddr == GlobalVar.ServerAddr.KunMing)
                                    {
                                        int price = EagleString.EagleFileIO.PriceOfPnrInFile(EagleAPI.etstatic.Pnr);
                                        GlobalVar.f_Balance -= price;
                                    }

                                    bsubed = true;
                                    break;
                                }
                            }

                            if (!bsubed)
                            {
                                GlobalVar.b_etdz = false;
                                return streamctrl_enum.PRE_SUBMIT_FAIL;
                            }
                            //预提交成功，则同时提交PNR到PNR统计里
                            try
                            {
                                pnr_statistics ps = new pnr_statistics();
                                ps.pnr = EagleAPI.etstatic.Pnr;
                                ps.state = "3";
                                Thread thSubmitPnr = new Thread(new ThreadStart(ps.submit1));
                                thSubmitPnr.Start();
                            }
                            catch { };
                        }
                    }
                    catch(Exception ex2)
                    {
                        throw new Exception("Ex2 :" + ex2.Message);
                    }
                }

                #endregion

                #region rtxxxxx取当前操作的PNR，注意msgtype=3
                {
                    if (msgtype == 3)
                    {
                        string[] arrayTemp = content.Split('~');
                        for (int i = 0; i < arrayTemp.Length; i++)
                        {
                            if (EagleAPI.substring(arrayTemp[i], 0, 2).ToLower() == "rt" && content.Length >= 7)
                            {
                                EagleAPI.CLEARCMDLIST(msgtype);
                                EagleAPI.etstatic.Pnr = mystring.right(arrayTemp[i].Trim(), 5);
                            }
                        }
                    }

                }
                #endregion


                content = content.Replace((char)0x0A, (char)0x0D);//发送中换行\r转换成\n




                log.strSend = content;//同Command.SendString
                connect_4_Command.SendString = content;
                GlobalVar.b_querryCommand = false;
                GlobalVar.b_otherCommand = false;


                return streamctrl_enum.NONE;
            }
            catch(Exception ex)
            {
                throw new Exception("StreamControl : " + ex.Message);
            }
        }
        /// <summary>
        /// 发送结束后，指示全局变量GlobalVar.b_pnCommand,GlobalVar.b_rtCommand,GlobalVar.b_etdz,GlobalVar.b_etdz_A
        /// </summary>
        /// <param name="content"></param>
        public static void send_end(string content,int msgtype)
        {
            GlobalVar.b_pnCommand = (EagleAPI.substring(content, 0, 2).ToUpper() == "PN");
            if (connect_4_Command.Qorder.Count < 1) GlobalVar.b_rtCommand = false;
            else
                GlobalVar.b_rtCommand = (EagleAPI.substring(connect_4_Command.Qorder[connect_4_Command.Qorder.Count - 1], 0, 2).ToUpper() == "RT");
            if (GlobalVar.b_etdz && EagleAPI.etstatic.Pnr.Trim().Length == 5)
            {
                EagleAPI.CLEARCMDLIST(msgtype);
                GlobalVar.b_etdz = false;
                GlobalVar.b_etdz_A = true;
            }

        }
        public static void switch_office(string cmd, string currentoffice,string specifiedoffice)
        {
            return;
            if (GlobalVar.loginLC.IPsString_backup.Split('~').Length <= 1) return;
            string specifyCmd = "da~ddi~detr~di~da~dz~etdz~etrf~etry~stn~te~ti~tipb~tipn~tn~to~trfd~trfx~trfu~tsl~vt~xc~xi~xo~tss~vof~exit";
            specifyCmd += "~qc~qd~qe~qn~qr~qs~qt";
            specifyCmd += "~si~so~adm~an~asr~siff~cvof";
            if (EagleAPI.GetCmdName(cmd, specifyCmd) !="")
            {
                //使用指定配置
                if (currentoffice == specifiedoffice) return;
                else
                {
                    GlobalVar.bSwichConfigAuto = true;
                    GlobalVar.bSwichConfigByManual = true;
                    EagleAPI.SpecifyCFG(specifiedoffice);
                }
            }
            else
            {
                //使用全部配置
                if (currentoffice == GlobalVar.loginLC.IPsString_backup) return;
                else
                {
                    GlobalVar.bSwichConfigAuto = true;
                    GlobalVar.bSwichConfigByManual = true;
                    EagleAPI.SpecifyCFG(GlobalVar.loginLC.IPsString_backup);
                }
            }
        }
        /// <summary>
        /// 不是etdz指令返回真，是etdz并且以rtxxxxx开头也返回真
        /// </summary>
        /// <param name="cmdString"></param>
        /// <returns></returns>
        public static bool IsEtdzStartWithRtCommand(string cmdString)
        {

            string[] strArray = cmdString.Split('~');
            bool etdz = false;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i].ToLower().IndexOf("etdz") == 0)
                {
                    etdz = true;
                    break;
                }
            }
            bool startwithrt = false;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i].Length >= 7 && strArray[i].ToLower().Substring(0, 2) == "rt")
                {
                    startwithrt = true;
                }
            }
            bool cangoon = true;
            if (!etdz) cangoon = true;
            else if (etdz && startwithrt) cangoon= true;
            else cangoon = false;
            return cangoon;
        }

        /// <summary>
        /// 是否使用指定配置
        /// </summary>
        /// <param name="msgtype">协议类型，只针对0x0003及0x0007</param>
        /// <param name="cmdStrings">含~的指令串</param>
        /// <returns></returns>
        public static bool IsSpecifyConfigCommand(int msgtype,string cmdStrings)
        {
            switch (msgtype)
            {
                case 7:
                    return false;
                case 3:
                    string[] cmdarray = cmdStrings.Split('~');
                    for (int i = 0; i < cmdarray.Length; i++)
                    {
                        string temp = cmdarray[i].ToLower().Trim();
                        string [] cmds = {"da","ddi","di","detr","dq","dz","ec","ei","etdz","etrf","etry","stn","te","ti","tipb","tipn","tn","to","trfd",
                            "trfx","trfu","tsl","vt","xc","xi","xo","tss","vof","exit",
                            "ab","adm","an","asr","cndz","cp","cs","fk","si","siif","so","trp","nfi",
                            "rt",""
                        };
                        for (int j = 0; j < cmds.Length; j++)
                            if (temp.IndexOf(cmds[j]) == 0) return true;
                    }
                    return false;
                default:
                    return false;
            }
        }
        public static bool cmdListOperating = false;
        /// <summary>
        /// 清空原指令表，并将指定msgtype的列表复制给原指令表
        /// </summary>
        /// <param name="msgtype"></param>
        
        public static void UseTheCommandQueneListBegin(int msgtype)
        {
            while (cmdListOperating)
            {
                Thread.Sleep(100);
                EagleAPI.LogWrite("指令列表线程冲突，等待100耗秒");
            }
            cmdListOperating = true;
            EagleAPI.CLEARCMDLIST(0);
            switch (msgtype)
            {
                case 3:
                    for (int i = 0; i < connect_4_Command.Qall0003.Count; i++)
                    {
                        connect_4_Command.Qall.Add(connect_4_Command.Qall0003[i]);
                    }
                    for (int i = 0; i < connect_4_Command.Qorder0003.Count; i++)
                    {
                        connect_4_Command.Qorder.Add(connect_4_Command.Qorder0003[i]);
                    }
                    for (int i = 0; i < connect_4_Command.Qquery0003.Count; i++)
                    {
                        connect_4_Command.Qquery.Add(connect_4_Command.Qquery0003[i]);
                    }
                    for (int i = 0; i < connect_4_Command.Qsend0003.Count; i++)
                    {
                        connect_4_Command.Qsend.Add(connect_4_Command.Qsend0003[i]);
                    }
                    break;
                case 7:
                    for (int i = 0; i < connect_4_Command.Qall0007.Count; i++)
                    {
                        connect_4_Command.Qall.Add(connect_4_Command.Qall0007[i]);
                    }
                    for (int i = 0; i < connect_4_Command.Qorder0007.Count; i++)
                    {
                        connect_4_Command.Qorder.Add(connect_4_Command.Qorder0007[i]);
                    }
                    for (int i = 0; i < connect_4_Command.Qquery0007.Count; i++)
                    {
                        connect_4_Command.Qquery.Add(connect_4_Command.Qquery0007[i]);
                    }
                    for (int i = 0; i < connect_4_Command.Qsend0007.Count; i++)
                    {
                        connect_4_Command.Qsend.Add(connect_4_Command.Qsend0007[i]);
                    }
                    break;
            }
        }
        /// <summary>
        /// 清空msgtype的列表后，并将原指令列表复制进去，以保存
        /// </summary>
        /// <param name="msgtype"></param>
        public static void UseTheCommandQueneListEnd(int msgtype)
        {
            EagleAPI.CLEARCMDLIST(msgtype);
            switch (msgtype)
            {
                case 3:
                    
                    for (int i = 0; i < connect_4_Command.Qall.Count; i++)
                    {
                        connect_4_Command.Qall0003.Add(connect_4_Command.Qall[i]);
                    }
                    for (int i = 0; i < connect_4_Command.Qorder.Count; i++)
                    {
                        connect_4_Command.Qorder0003.Add(connect_4_Command.Qorder[i]);
                    }
                    for (int i = 0; i < connect_4_Command.Qquery.Count; i++)
                    {
                        connect_4_Command.Qquery0003.Add(connect_4_Command.Qquery[i]);
                    }
                    for (int i = 0; i < connect_4_Command.Qsend.Count; i++)
                    {
                        connect_4_Command.Qsend0003.Add(connect_4_Command.Qsend[i]);
                    }
                    break;
                case 7:
                    for (int i = 0; i < connect_4_Command.Qall.Count; i++)
                    {
                        connect_4_Command.Qall0007.Add(connect_4_Command.Qall[i]);
                    }
                    for (int i = 0; i < connect_4_Command.Qorder.Count; i++)
                    {
                        connect_4_Command.Qorder0007.Add(connect_4_Command.Qorder[i]);
                    }
                    for (int i = 0; i < connect_4_Command.Qquery.Count; i++)
                    {
                        connect_4_Command.Qquery0007.Add(connect_4_Command.Qquery[i]);
                    }
                    for (int i = 0; i < connect_4_Command.Qsend.Count; i++)
                    {
                        connect_4_Command.Qsend0007.Add(connect_4_Command.Qsend[i]);
                    }
                    break;
            }
            cmdListOperating = false;
        }
        /// <summary>
        /// 是否与配置有关指令
        /// </summary>
        /// <returns></returns>
        public static bool IsCommandHasRelationWithConfig(string cmdstrings)
        {
            string[] cmdarray = mystring.trim(cmdstrings.ToLower(),'~').Split('~');
            string[] cmdnorelation = "sd~ss~nm~tk~t:~ct~c:~av~fd~pn~pg~pb~pf~cd~cntd~co~cv~date~dsm~help~tim~time~wf~i".Split('~');
            if (cmdstrings.IndexOf("@") >= 0 || cmdstrings.IndexOf("\\") >= 0) return true;
            for (int i = 0; i < cmdarray.Length; i++)
            {
                for (int j = 0; j < cmdnorelation.Length; j++)
                {
                    if (cmdarray[i].IndexOf(cmdnorelation[j]) == 0) break;
                    if (j + 1 == cmdnorelation.Length) return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 回溯法精简指令
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string OptimizeCommandStrings(string content)
        {
            try
            {
                string[] cmdarray = content.ToLower().Split(new char[] { '~' }, StringSplitOptions.RemoveEmptyEntries);
                int thesame = -1;
                string ret = "";
                for (int i = 0; i < cmdarray.Length - 1; i++)
                {
                    if ((cmdarray[i] == cmdarray[i + 1]
                        || (cmdarray[i].Length >= 7 
                            && cmdarray[i + 1].Length >= 7 
                            && cmdarray[i].IndexOf("rt") == 0 
                            && cmdarray[i + 1].IndexOf("rt") == 0))
                        && cmdarray[i] != "pn"
                        && cmdarray[i] != "pb"
                        && cmdarray[i] != "xe")
                    {
                        thesame = i;
                        break;
                    }
                }
                if (thesame > -1)
                {
                    for (int i = 0; i < cmdarray.Length; i++)
                    {
                        if (i == thesame) continue;
                        ret += cmdarray[i] + "~";
                    }
                    return streamctrl.OptimizeCommandStrings(ret);
                }
                else
                {
                    for (int i = 0; i < cmdarray.Length; i++)
                    {
                        ret += cmdarray[i] + "~";
                    }
                    return ret.Substring(0, ret.Length - 1);
                }
            }
            catch
            {
                return content;
            }
        }
        /// <summary>
        /// 快速出票，适用于散客票，团队票可选择国际票方式或者读秒方式
        /// 对rtReturnString(虚拟实际发送得到指令得到的结果)，经过modifyCommand后，虚拟返回结果。
        /// 若指令需要被发送，则返回true，否则返回false并Append内容rtReturnString到黑屏
        /// 置于指令组合，与指令发送之间
        /// 注：在03下使用
        /// </summary>
        /// <param name="rtRetrunString"></param>
        /// <param name="modifyCommand"></param>
        /// <returns></returns>
        public static bool MemoryOperation(ref string rtReturnString, string modifyCommand)
        {
            bool ret = true;
            if (GlobalVar.commandSendtype == GlobalVar.CommandSendType.B) return true;//国际票方式直接返回true
            try
            {
                if (rtReturnString == "") return true;
                if (mystring.right(modifyCommand, 2).ToLower() == "rr" && int.Parse(mystring.left(modifyCommand, 1)) > 0)
                {
                    ret = false;
                    string[] lines = rtReturnString.Split('\n');
                    int offset = 0;
                    for (int i = 0; i < lines.Length; i++)
                    {
                        offset += lines[i].Length;
                        if (lines[i].Trim().Length >= 5)

                            if (lines[i].Substring(3, 2) == "  " || lines[i].Substring(3, 2) == " *")
                                if (lines[i].Trim().IndexOf(mystring.trim(modifyCommand.Substring(0, modifyCommand.Length - 2), '/')) == 0)
                                {
                                    string rep = rtReturnString.Substring(offset + 33 - lines[i].Length, 2);
                                    //rtReturnString = rtReturnString.Replace(rep, "RR");
                                    //lines[i] = lines[i].Replace(rep, "RR");
                                    lines[i] = lines[i].Remove(32, 2);
                                    lines[i] = lines[i].Insert(32, "RR");

                                    break;
                                }
                    }
                    string combine = "";
                    for (int j = 0; j < lines.Length; j++)
                    {
                        if (lines[j] != "")
                        {
                            combine += (lines[j] + "\n");
                        }
                    }
                    rtReturnString = combine;
                }
                if (modifyCommand.ToLower().IndexOf("ei") == 0)
                {
                    ret = false;
                    string[] lines = mystring.trim(rtReturnString).Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    int start = 0;
                    int iwhich = lines.Length - 1;
                    if (lines[lines.Length - 1].Length < 3) iwhich--;
                    start = rtReturnString.IndexOf(lines[iwhich]) + 3;

                    rtReturnString = rtReturnString.Substring(0, start) + modifyCommand;                    
                }
                //if (modifyCommand.ToLower().IndexOf("xe") == 0 && modifyCommand.ToLower().IndexOf("xepnr") != 0)
                //{
                //    ret = false;
                //    string[] lines = rtReturnString.Split('\n');
                //    for (int i = 0; i < lines.Length; i++)
                //    {
                //        if(lines[i].Trim().IndexOf(modifyCommand.Substring(2))==0 
                //            &&lines[i].Substring(2,1)==".")
                //        {
                //            lines[i] = "";
                            
                //        }
                //        try
                //        {
                //            int xeNumber = int.Parse(modifyCommand.Substring(2));
                //            int decNumber = int.Parse(lines[i].Trim().Substring(0, lines[i].Trim().IndexOf(".")));
                //            if (decNumber > xeNumber)
                //            {
                //                lines[i] = lines[i].Replace(decNumber.ToString() + ".", string.Format("{0}.", decNumber - 1));
                //            }
                //        }
                //        catch
                //        {
                //            //ret = true;
                //            //break;
                //        }
                //    }
                //    string combine = "";
                //    for (int i = 0; i < lines.Length; i++)
                //    {
                //        if (lines[i] != "")
                //        {
                //            combine += (lines[i] + "\n");
                //        }
                //    }
                //    rtReturnString = combine;
                //}
                if (modifyCommand.ToLower().IndexOf("at/") > 0 
                    && int.Parse(modifyCommand.Substring(0, modifyCommand.ToLower().IndexOf("at/"))) > 0)
                {
                    ret = false;
                    string[] lines = rtReturnString.Split('\n');
                    for (int i = 0; i < lines.Length; i++)
                    {
                        try
                        {
                            int xeNumber = int.Parse(modifyCommand.Substring(0, modifyCommand.ToLower().IndexOf("at/")));
                            int decNumber = int.Parse(lines[i].Trim().Substring(0, lines[i].Trim().IndexOf(".")));
                            if (decNumber == xeNumber)
                            {
                                lines[i] = lines[i].Substring(0, lines[i].IndexOf(".") + 1) + "AT//"
                                    + modifyCommand.Substring(modifyCommand.ToLower().IndexOf("at/") + 3).ToUpper();
                                break;
                            }
                        }
                        catch
                        {
                            //ret = true;
                            //break;
                        }
                    }
                    string combine = "";
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i] != "")
                        {
                            combine += (lines[i] + "\n");
                        }
                    }
                    rtReturnString = combine;
                }
            }
            catch { ret = true; }
            return ret;
        }

        public static void limitCommandNumber(string content)
        {
            string [] cmds = content.Split('~');
            int num = 0;
            for (int i = 0; i < cmds.Length; i++)
            {
                if (cmds[i].Length > 0 && cmds[i].ToLower()[0] == 'p')
                    num++;
            }
            if (num > 10)
            {
                //System.Windows.Forms.MessageBox.Show("超过10条连续翻页指令!延时5秒发送!或者请使用独占配置方式!");
                Thread.Sleep(5000);
            }
            else if (num > 5)
            {
                //System.Windows.Forms.MessageBox.Show("超过5条连续翻页指令!延时3秒发送!或者请使用独占配置方式!");
                Thread.Sleep(3000);
            }
        }

        

    }
    public class UseOnceClass
    {
        static public string LastBuff = "";
        public void AddLastBuff(byte[] CurrBuff)
        {
        }
    }
}
