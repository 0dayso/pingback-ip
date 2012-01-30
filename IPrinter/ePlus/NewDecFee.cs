///Version 1.0
///Auther Eric.Chen
///先扣款再出票的扣款类，通过截获用户ETDZ指令来扣款
///2008.11.18  14:21
using System;
using System.Collections.Generic;
using System.Text;
using gs.para;
using System.Threading;

namespace ePlus
{
    public class NewDecFee
    {
        /// <summary>
        /// 电脑号
        /// </summary>
        private string strPnr;
        /// <summary>
        /// 用户所发的ETDZ指令
        /// </summary>
        private string strETCommand;
        /// <summary>
        /// 用户名
        /// </summary>
        private string strUserName;
        /// <summary>
        /// 大人总价格
        /// </summary>
        private string strTotalAdultFee;
        /// <summary>
        /// 大人总价格
        /// </summary>
        private string strAdultFee;
        /// <summary>
        /// 大人总人数
        /// </summary>
        private string strAdultNum;
        /// <summary>
        /// 儿童总价格
        /// </summary>
        private string strTotalChildFee;
        /// <summary>
        /// 单张儿童价格
        /// </summary>
        private string strChildFee;
        /// <summary>
        /// 儿童人数
        /// </summary>
        private string strChildNum;
        /// <summary>
        /// office号
        /// </summary>
        private string strOfficeID;
        /// <summary>
        /// 原余额
        /// </summary>
        private string strOriginallyBalance;
        /// <summary>
        /// 扣款后余额
        /// </summary>
        private string strCurrentBalance;
        /// <summary>
        /// 处理结果
        /// </summary>
        public string strProResult;
        /// <summary>
        /// 乘客总人数
        /// </summary>
        private string strPasNum;
        /// <summary>
        /// 是否小孩票
        /// </summary>
        private bool isChild;
        /// <summary>
        /// 总价格
        /// </summary>
        private string strMuPaFee;
        /// <summary>
        /// 取价格标志
        /// </summary>
        /// <remarks>1为在取成人2为在取儿童</remarks>
        public int nGetFcFlag;
        ///<summary>
        /// RT返回数据
        /// </summary>
        private string strrtreturn;
           public string strRTReturn
        {
            get
            {
                return strrtreturn;
            }
            set
            {
                GlobalVar.b_rt = false;
                strrtreturn = value.ToUpper();
                if (AnalyseRTReturn())
                {
                    try
                    {
                        SendPAT();
                    }
                    catch (Exception ea)
                    {
                        throw new Exception(ea.Message);
                    }
                }
                else
                {
                    throw new Exception(strProResult);
                }
            }
        }
        /// <summary>
        /// RT并PAT返回的数据
        /// </summary>
        private string strpatreturn;
        public string strPATReturn
        {
            get
            {
                return strpatreturn;
            }
            set
            {
                if (nGetFcFlag == 1)
                {
                    GlobalVar.b_getfc = false;
                }
                if (nGetFcFlag == 2)
                {
                    GlobalVar.b_getchildfc = false;
                }
                strpatreturn = value;
                if (AnalysePATReturn())
                {
                    try
                    {
                        DoDecFee();
                    }
                    catch (Exception ea)
                    {
                        throw new Exception(ea.Message);
                    }
                }
                else
                {
                    throw new Exception(strProResult);
                }
            }
        }
        /// <summary>
        /// 带一个参数的构造函数
        /// </summary>
        /// <param name="etcommand"></param>
        public NewDecFee(string strcom)
        {
            
            strOriginallyBalance = GlobalVar.f_CurMoney;
            strETCommand = strcom;
            strPasNum=string.Empty;
            strMuPaFee =string.Empty;
            isChild = false;
            nGetFcFlag = 1;
            if (!GlobalVar.bUsingConfigLonely)
            {
                int iRt = strETCommand.IndexOf("~rt");
                if (iRt > -1)
                {
                    try
                    {
                        iRt += 3;
                        strPnr = strETCommand.Substring(iRt);
                        strPnr = strPnr.Substring(0, strPnr.IndexOf("~")).Trim();
                        if (strPnr == "" || strPnr == null||!EagleAPI.IsRtCode(strPnr))
                        {
                            throw new Exception("\r\n没有正确PNR\r\n>");
                        }
                    }
                    catch
                    {
                        throw new Exception("\r\n没有PNR\r\n>");
                    }
                }
                else
                {
                    throw new Exception("\r\n出票指令有误\r\n>");
                }
            }
            else
            {
                strPnr = EagleAPI.etstatic.Pnr;
                if (strPnr == "" || strPnr == null)
                {
                    throw new Exception("\r\n独占配置出票出错！\r\n>");
                }
            }
            
           SendRT();
        }
        /// <summary>
        /// 1、发指令取是否出票和人数
        /// </summary>
        private void SendRT()
        {

            string rtCommand = "";
            rtCommand = "i~rt" + strPnr.Trim();
            GlobalVar.b_rt = true;
            EagleAPI.EagleSendOneCmd(rtCommand);
        }
        /// <summary>
        /// 分析RT命令返回的数据
        /// </summary>
        /// <returns></returns>
        private bool AnalyseRTReturn()
        {
            EagleAPI.LogWrite("\r\n"+"扣款取总人数结果："+strrtreturn+"\r\n");
           
            string temp = "";
            if (strrtreturn == null || strrtreturn == "")
            {

                //if (strPnr.ToLower().Trim() == EagleAPI.etstatic.Pnr.ToLower().Trim())
                //{
                //    strPasNum = EagleAPI.etstatic.Passengers;
                //    try
                //    {
                //        int ipans = System.Convert.ToInt32(strPasNum);
                //        if (ipans >= 1 && ipans < 600)
                //        {
                //            return true;
                //        }
                //    }
                //    catch
                //    {
                //        strProResult = "\r\n没有乘客人数信息，请重新出票！\r\n>";
                //        return false;
                //    }
                //}
               
                    strProResult = "\r\n未取到乘客人数信息，请重新出票！\r\n>";
                    return false;
                
                
            }
           
               
           
            //if(strrtreturn.IndexOf("**ELECTRONIC TICKET PNR**")>-1)
            //{
            //    strProResult="\r\nPNR:"+strPnr+"已出过票，不允许再次出票\r\n>";
            //    return false;
            //}
            else
            {
                try
                {
                    int num = strrtreturn.IndexOf(strPnr.ToUpper()+"\r\n");
                    if (num == -1)
                    {
                        strProResult = "\r\n取出的乘客人数信息出错，请重新出票\r\n>";
                        return false;
                    }
                    temp = strrtreturn.Substring(num + 7);
                    string pasn = temp.Substring(34, 3);
                    int ipans = System.Convert.ToInt32(pasn);
                    if (ipans >= 1 && ipans < 600)
                    {
                        
                        strPasNum = pasn;
                        EagleAPI.LogWrite("\r\n" + "扣款取总人数：" + strPasNum + "\r\n");
                        string strtemp = ipans > 9 ? "" : " ";
                        int endofpassenger = strrtreturn.IndexOf("\r\n" + strtemp + ipans.ToString());
                        if (endofpassenger < 0)
                        {
                            strProResult = "\r\n取儿童人数信息非法，请重新出票\r\n>";
                            return false;
                        }
                        strtemp = strrtreturn.Substring(0, endofpassenger);
                        int nchild = strtemp.IndexOf("CHD");
                        if (nchild > 0)
                        {
                            isChild = true;
                            string strtemp1 = strtemp.Replace("CHD","");
                            nchild = (strtemp1.Length - strtemp.Length) / 3;
                            strChildNum = nchild.ToString();
                            strAdultNum = (ipans - nchild).ToString();
                            if (ipans < nchild)
                            {
                                strProResult = "\r\n取出的儿童人数信息出错，请重新出票\r\n>";
                                return false;
                            }
                        }
                        else
                        {
                            isChild = false;
                            strChildNum = "0";
                            strAdultNum = strPasNum;
                        }

                        return true;
                    }
                    else
                    {
                        strProResult = "\r\n取出的乘客人数信息非法，请重新出票\r\n>";
                        return false;
                    }
                    
                }
                catch
                {
                    strProResult = "\r\n取乘客人数信息出错，请重新出票\r\n>";
                    return false;
                }
            }
        }
        /// <summary>
        /// 发指令取总价格串
        /// </summary>
        /// <returns></returns>
        private void SendPAT()
        {
            string patCommand = "";
            if (!isChild)
            {
                patCommand = "I~RT" + strPnr + "~PAT:A";
                GlobalVar.b_getfc = true;
                EagleAPI.EagleSendOneCmd(patCommand);
            }
            else
            {
                if (this.strPasNum == this.strChildNum)
                {
                    patCommand = patCommand = "I~RT" + strPnr + "~PAT:*CH";
                    GlobalVar.b_getchildfc = true;
                    EagleAPI.EagleSendOneCmd(patCommand);
                }
                else
                {
                    patCommand = "I~RT" + strPnr + "~PAT:A";
                    GlobalVar.b_getfc = true;
                    EagleAPI.EagleSendOneCmd(patCommand);
                    patCommand = patCommand = "I~RT" + strPnr + "~PAT:*CH";
                    GlobalVar.b_getchildfc = true;
                    EagleAPI.EagleSendOneCmd(patCommand);
                }
            }
        }
        /// <summary>
        /// 分析PAT价格返回的价格信息
        /// </summary>
        private bool AnalysePATReturn()
        {
            EagleAPI.LogWrite("\r\n"+"扣款取价格结果："+strpatreturn+"\r\n");
            string temp = "";
            if (strpatreturn == null || strpatreturn == "")
            {
                strProResult = "\r\n未取到机票价格，请重新出票！\r\n>";
                return false;
            }
            else
            {
                try
                {
                    int iTstart = strpatreturn.IndexOf("TOTAL:");
                    if (iTstart > -1)
                    {
                        iTstart += 6;
                        temp = strpatreturn.Substring(iTstart);
                        int iRstart = temp.IndexOf("\r\n");
                        if (iRstart > -1)
                        {
                            temp = temp.Substring(0,iRstart);
                            double dfc = System.Convert.ToDouble(temp);
                            if (dfc <10.0)
                            {
                                strProResult = "\r\n取机票价格有误，请再重新出票！\r\n>";
                                return false;
                            }
                            else
                            {
                                if (nGetFcFlag==1)
                                {
                                    strAdultFee = temp;
                                }
                                if (nGetFcFlag == 2)
                                {
                                    strChildFee = temp;
                                }
                                return true;
                            }
                        }
                        else
                        {
                            strProResult = "\r\n未取到正确机票价格，请重新出票，如果未做价格请先做价格封口后，再重新出票！\r\n>";
                            return false;
                        }

                    }
                    else
                    {
                        strProResult = "\r\n未取到机票价格，如果未做价格请先做价格封口后，再重新出票！\r\n>";
                        return false;
                    }
                }
                catch (Exception ea)
                {
                    strProResult = "\r\n取机票价格出错，如果未做价格请先做价格封口后，再重新出票！\r\n>";
                    return false;
                }
            }
        }
        /// <summary>
        /// 执行扣款
        /// </summary>
        /// <returns></returns>
        private void DoDecFee()
        {
            string strRet = "";
            if (CalcFee())
            {
                if (Convert.ToInt32(this.strOriginallyBalance) < Convert.ToInt32(this.strMuPaFee))
                {
                    throw new Exception("\r\n该用户余额不足，不能出票！\r\n>");
                }
                strRet = InvokeWS();
            }
            else
            {
                throw new Exception("\r\n计算多人总票价出错！\r\n>");
            }
            if (strRet == null || strRet == "")
                throw new Exception("\r\n调用 WebService 返回不正常！xml:" + strRet + "\r\n>");
            NewPara np1 = new NewPara(strRet);
            string cm = np1.FindTextByPath("//eg/cm");
            string decstat = np1.FindTextByPath("//eg/DecStat");
            string money = np1.FindTextByPath("//eg/NewUserYe");
            string err = np1.FindTextByPath("//eg/err");

            if (cm == "RetDecFee" && decstat == "DecSucc")
            {
                strCurrentBalance=GlobalVar.f_CurMoney = money;
                string agrs = "\r\n调用扣款服务" + "<eg66>" + strPnr;
                strProResult = "\r\n系统开始减款，用户原始余额：" + strOriginallyBalance + "扣款："+strMuPaFee;
                strProResult += "\r\n" + "用户当前余额：" + strCurrentBalance + "\r\n";
                GlobalVar.b_netdz = true;
                DoEtdz();
                EagleAPI.LogWrite(agrs+strProResult);
            }
            else
            {
                //if (cm == "RetDecFee" && decstat == "Deced")
                //{
                //}
                //else
                //{
                    throw new Exception("cm:" + cm + " err:" + err + Environment.NewLine);
                //}
            }
        }
        /// <summary>
        /// 计算机扣款额 
        /// </summary>
        /// <returns>计算机成功或失败</returns>
        private bool CalcFee()
        {
            //EagleAPI.LogWrite("\r\n" + "扣款取人数2："+strPasNum + "\r\n");
            if (!isChild)
            {
                if (strAdultNum != null && strAdultNum != "" && strAdultFee != null && strAdultFee != "")
                {
                    try
                    {
                        strMuPaFee = (System.Convert.ToInt32(strAdultNum) * System.Convert.ToDouble(strAdultFee)).ToString("f");
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (strPasNum == strChildNum)
                {
                    if (strChildNum != null && strChildNum != "" && strChildFee != null && strChildFee != "")
                    {
                        try
                        {
                            strMuPaFee = (System.Convert.ToInt32(strChildNum) * System.Convert.ToDouble(strChildFee)).ToString("f");
                            return true;
                        }
                        catch
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (strChildNum != null && strChildNum != "" && strChildFee != null && strChildFee != ""&&strAdultNum != null && strAdultNum != "" && strAdultFee != null && strAdultFee != "")
                    {
                        try
                        {
                            strTotalAdultFee = (System.Convert.ToInt32(strChildNum) * System.Convert.ToDouble(strChildFee)).ToString("f");
                            strTotalChildFee= (System.Convert.ToInt32(strChildNum) * System.Convert.ToDouble(strChildFee)).ToString("f");
                            strMuPaFee = (Convert.ToInt32(strTotalChildFee) + Convert.ToInt32(strTotalAdultFee)).ToString();
                            return true;
                        }
                        catch
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        /// <summary>
        /// 调用扣款Web服务
        /// </summary>
        private string InvokeWS()
        {
            EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
            NewPara np = new NewPara();
            np.AddPara("cm", "DecFee");
            np.AddPara("Pnr", strPnr);
            np.AddPara("TicketPrice",strMuPaFee);
            string strReq = np.GetXML();
            string strRet = ws.getEgSoap(strReq);
            return strRet;
        }
        /// <summary>
        /// 5、出票
        /// </summary>
        public void DoEtdz()
        {
            //EagleAPI.EagleSendOneCmd("^|_^i~rtN3BQC~etdz:9", 1);//测试用
             EagleAPI.EagleSendOneCmd(strETCommand,3);
            
        }
    }
}
