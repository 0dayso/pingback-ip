///Version 2.0
///Auther Eric.Chen
///先扣款再出票的扣款类，通过截获用户ETDZ指令来扣款
///2008.12.29  08:21
using System;
using System.Collections.Generic;
using System.Text;
using gs.para;
using System.Threading;
using System.Windows.Forms;
using System.Collections;

namespace ePlus
{
    public class NewDecFee2
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
        /// 成人总价格
        /// </summary>
        private float foTotalAdultFee;
        /// <summary>
        /// 一个成人价格
        /// </summary>
        private float foAdultFee;
        /// <summary>
        /// 成人总人数
        /// </summary>
        private int intAdultNum;
        /// <summary>
        /// 儿童总价格
        /// </summary>
        private float foTotalChildFee;
        /// <summary>
        /// 单张儿童价格
        /// </summary>
        private float foChildFee;
        /// <summary>
        /// 儿童人数
        /// </summary>
        private int intChildNum;
        /// <summary>
        /// 婴儿总价
        /// </summary>
        private float foTotalBabyFee;
        /// <summary>
        /// 单张婴儿价
        /// </summary>
        private float foBabyFee;
        /// <summary>
        /// 婴儿总人数
        /// </summary>
        private int intBabynNum;
        /// <summary>
        /// office号
        /// </summary>
        private string strOfficeID;
        /// <summary>
        /// 原余额
        /// </summary>
        private float foOriginallyBalance;
        /// <summary>
        /// 扣款后余额
        /// </summary>
        private float foCurrentBalance;
        /// <summary>
        /// 处理结果
        /// </summary>
        public string strProResult;
        /// <summary>
        /// 乘客总人数
        /// </summary>
        private int intPasNum;
        /// <summary>
        /// PNR是否就是小孩票
        /// </summary>
        private bool isChild;
        /// <summary>
        /// PNR中是否有小孩
        /// </summary>
        private bool bContainChild;
        /// <summary>
        /// 是否包含婴儿
        /// </summary>
        private bool bContainBaby;
        /// <summary>
        /// 总价格
        /// </summary>
        private float foMuPaFee;
        /// <summary>
        /// 取价格标志
        /// </summary>
        /// <remarks>1为在取成人2为在取儿童3在取婴儿</remarks>
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
                if (value != null || value != "")
                {
                    //strrtreturn += "\n" + Command.AV_String.Replace("\r\n","\n");
                    strrtreturn +=connect_4_Command.AV_String;
                    string strtemp = string.Empty;
                    string strtempa = strrtreturn;

                    if (strtempa.EndsWith("+\r\n\r\n") || strtempa.EndsWith("+\r\n") || strtempa.EndsWith("+") || strtempa.EndsWith("+\r\n\r\n\r\n") || strtempa.EndsWith("+\r\n\r\n\r\n\r\n"))//&& strtemp.IndexOf("*THIS PNR WAS ENTIRELY CANCELLED*") < 0
                    {
                        //发送PN指令
                        strrtreturn = strrtreturn.Replace('+', ' ')+"\r\n";
                        strrtreturn=strrtreturn.Replace('-',' ')+"\r\n";
                        if (frmMain.st_tabControl.InvokeRequired)
                        {
                            EventHandler eh = new EventHandler(SendPN);
                            TabControl tc = frmMain.st_tabControl;
                            frmMain.st_tabControl.Invoke(eh, new object[] { tc, EventArgs.Empty });
                        }
                    }
                    else
                    {
                        GlobalVar.b_rt = false;
                        if (!AnalyseRTReturn())
                        {
                            //throw new Exception(strProResult);
                            EagleAPI.LogWrite("\r\n"+strProResult+"\r\n");
                            DoEtdz();
                        }
                        else
                        {
                            DoDecFee();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 带一个参数的构造函数
        /// </summary>
        /// <param name="etcommand"></param>
        public NewDecFee2(string strcom)
        {
            foOriginallyBalance = float.Parse(GlobalVar.f_CurMoney);
            strETCommand = strcom;
            intAdultNum = intChildNum = intBabynNum =intPasNum=0;
            foAdultFee = foBabyFee = foChildFee = 0;
            isChild = false;
            bContainChild = false;
            bContainBaby = false;
            GlobalVar.b_NotDoDec = false;
            nGetFcFlag = 1;
            try
            {
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
                            if (strPnr == "" || strPnr == null || !EagleAPI.IsRtCode(strPnr))
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
            }
            catch
            {
                DoEtdz();
            }
            SendRT();
        }
        /// <summary>
        /// 1、发指令取是否出票和人数
        /// </summary>
        private void SendRT()
        {
            string rtCommand = "";
            rtCommand = "i~rt:n/" + strPnr.Trim();
            EagleAPI.CLEARCMDLIST(3);
            GlobalVar.b_rt = true;
            EagleAPI.EagleSendOneCmd(rtCommand);
        }
        static public void SendPN(object sender, EventArgs e)
        {
            WindowInfo wndInfo = frmMain.windowSwitch[0];
            wndInfo.SendData("pn");
        }
        /*
        >rtwbl03
1.LIU/XIANG 2.ZHU/JUN WBL03
 3.  MU2501 Y   SA24JAN  WUHPVG HK2   0830 0945          E --T1
 4.  MU564  Y   MO26JAN  PVGPEK HK2   0800 1010          E T1T2
 5.WUH/T WUH/T 0714-6255100/HUANG SHI LAN XIANG TICKET CO.,LTD/CAOQING ABCDEFG
 6.454545
 7.TL/2000/29DEC/WUH402
 8.FC/A/WUH MU PVG 810.00Y MU PEK 1130.00Y CNY1940.00END
 9.FC/IN/WUH MU PVG 80.00YIN10 MU PEK 110.00YIN10 CNY190.00END
10.SSR ADTK 1E BY WUH13JAN09/1221 OR CXL MU 564 Y26JAN
11.SSR INFT MU  KK1 WUHPVG 2501 Y24JAN BABY/CD 04MAR08/P2
12.SSR INFT MU  KK1 WUHPVG 2501 Y24JAN BABB/CD 04MAR08/P1
13.OSI YY 1INF BABB/CDINF/P1                                                   +
14.OSI YY 1INF BABY/CDINF/P2                                                   -
15.RMK CA/DL82W
16.RMK AUTOMATIC FARE QUOTE
17.FN/A/FCNY1940.00/SCNY1940.00/C3.00/XCNY160.00/TCNY100.00CN/TCNY60.00YQ/
    ACNY2100.00
18.FN/IN/FCNY190.00/SCNY190.00/C0.00/TEXEMPTCN/TEXEMPTYQ/ACNY190.00
19.FP/CASH,CNY
20.FP/IN/CASH,CNY
21.XN/IN/BABB/CDINF(MAR08)/P1
22.XN/IN/BABY/CDINF(MAR08)/P2
23.WUH402
         */
        /// <summary>
        /// 分析RT命令返回的数据
        /// </summary>
        /// <returns></returns>
        private bool AnalyseRTReturn()
        {
            EagleAPI.LogWrite("\r\n" + "扣款取总人数结果：" + strrtreturn + "\r\n");

            string temp = "";
            if (strrtreturn == null || strrtreturn == "")
            {

                strProResult = "\r\n未取到乘客人数信息，请重新出票！\r\n>";
                return false;


            }
            //if(strrtreturn.IndexOf("**ELECTRONIC TICKET PNR**")>-1)
            //{
            //    strProResult="\r\nPNR:"+strPnr+"已出过票，不允许再次出票\r\n>";
            //    return false;
            //}
            try
            {
                int num = strrtreturn.IndexOf(strPnr.ToUpper() + "\r\n");
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

                    intAdultNum=intPasNum = ipans;
                    EagleAPI.LogWrite("\r\n" + "扣款取总人数：" + intPasNum.ToString() + "\r\n");
                   //*以下为取儿童信息
                    string strtemp = ipans > 9 ? "" : " ";
                    int endofpassenger = strrtreturn.IndexOf(this.strPnr.ToUpper());
                    if (endofpassenger < 0)
                    {
                        strProResult = "\r\n取儿童人数信息非法，请重新出票\r\n>";
                        return false;
                    }
                    strtemp = strrtreturn.Substring(0, endofpassenger);
                    int nchild = strtemp.IndexOf("CHD");
                    if (nchild > 0)
                    {
                        bContainChild = true;
                        string strtemp1 = strtemp.Replace("CHD", "");
                        nchild = (strtemp.Length - strtemp1.Length) / 3;
                        intChildNum = nchild;
                        intAdultNum = ipans - nchild;
                        if (ipans < nchild)
                        {
                            strProResult = "\r\n取出的儿童人数信息出错，请重新出票\r\n>";
                            return false;
                        }
                        bContainChild = true;
                        isChild=intPasNum == intChildNum ? true : false;

                    }
                    else
                    {
                        bContainChild = false;
                        isChild = false;
                        intChildNum = 0;
                        intAdultNum = intPasNum;
                    }
                    // */
                    /////////////////////////以上判断是否成人，儿童票，取成人及儿童人数完成;下面开始取价格及婴儿项///////////////
                    string[] arrayPnrStructer = strrtreturn.Split(new string[] {"\r\n"},StringSplitOptions.RemoveEmptyEntries);
                    for(int j=0;j<arrayPnrStructer.Length;j++)
                    {

                        string arraystart = string.Empty;
                        if (arrayPnrStructer[j].Length > 3)
                        {
                            arraystart = arrayPnrStructer[j].Substring(0, 3);
                            if (!(arraystart[2] == '.' && (arraystart[0] == ' ' || arraystart[0] > '0' - 1 && arraystart[0] < '9' + 1) && (arraystart[1] > '0' - 1 && arraystart[1] < '9' + 1)))
                            {
                                if (j > 0)
                                {
                                    arrayPnrStructer[j - 1] += arrayPnrStructer[j];
                                }
                            }
                        }
                    }
                    ArrayList xn = new ArrayList();
                    ArrayList fn = new ArrayList();
                    foreach(string st in arrayPnrStructer)//取FN XN项
                    {
                        if (st.Length < 6)
                            continue;
                        if (st.Substring(3, 2) == "FN")
                        {
                            fn.Add(st);
                        }
                        if (st.Substring(3, 2) == "XN")
                        {
                            xn.Add(st);
                        }
                    }
                    if(fn.Count==0||(xn.Count>0&&(fn.Count<0||fn.Count<xn.Count)))
                    {
                        strProResult = "\r\n未做票价，或者票未做全，请先做完票价并封口后再出票！\r\n>";
                        return false;
                    }
                    for (int k = 0; k < fn.Count; k++)
                    {
                        string tempfnin = fn[k].ToString();
                        if (tempfnin.IndexOf("FN/A/FCNY") > 0 || tempfnin.IndexOf("FN/FCNY") > 0)
                        {
                            int fnins = tempfnin.IndexOf("ACNY") + 4;
                            if (fnins > 5)
                            {
                            int fnine = tempfnin.Length;
                            if (isChild)
                            {
                                foChildFee = float.Parse(tempfnin.Substring(fnins, fnine - fnins));
                            }
                            else
                            {
                                foAdultFee = float.Parse(tempfnin.Substring(fnins, fnine - fnins));
                            }
                            }
                            break;
                        }
                    }
                    if (foChildFee == 0.0 && foAdultFee == 0.0)
                    {
                        strProResult = "\r\n没有权限获取票价，可能是其它配置做的价格，请删除fn项重做票价！\r\n>";
                        return false;
                    }
                    //if (isChild&&foChildFee==0.0)
                    //{
                    //    for (int k = 0; k < fn.Count; k++)
                    //    {
                    //        string tempfnin = fn[k].ToString();
                    //        if (tempfnin.IndexOf("FN/FCNY") > 0)
                    //        {
                    //            int fnins = tempfnin.IndexOf("ACNY") + 4;
                    //            int fnine = tempfnin.Length;
                    //            foChildFee = float.Parse(tempfnin.Substring(fnins, fnine - fnins));
                    //            break;
                    //        }
                    //    }
                    //}
                    if (xn.Count != 0)
                    {
                        intBabynNum = xn.Count;
                        bContainBaby = true;
                        for (int k = 0; k < fn.Count; k++)
                        {
                            string tempfnin = fn[k].ToString();
                            if ( tempfnin.IndexOf("FN/A/IN/") > 0|| tempfnin.IndexOf("FN/IN/") > 0)
                            {
                                int fnins = tempfnin.IndexOf("ACNY") + 4;
                                int fnine = tempfnin.Length;
                                foBabyFee = float.Parse(tempfnin.Substring(fnins, fnine - fnins));
                                break;
                            }
                        }
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
                strProResult = "\r\n取扣款信息出错，请重新出票\r\n>";
                return false;
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
                if (foOriginallyBalance < foMuPaFee)
                {
                    throw new Exception("\r\n该用户余额不足，不能出票！\r\n>");
                }
                strRet = InvokeWS();
                //strRet = "<eg><cm>RetDecFee</cm><DecStat>DecSucc</DecStat><NewUserYe>300</NewUserYe></eg>";
            }
            else
            {
               //throw new Exception("\r\n计算多人总票价出错！\r\n>");
                EagleAPI.LogWrite("\r\n计算多人总票价出错！\r\n");
                DoEtdz();
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
                GlobalVar.f_CurMoney = money;
                foCurrentBalance = float.Parse(money);
                string agrs = "\r\n调用扣款服务" + "<eg66>" + strPnr;
                strProResult = "\r\n系统开始减款，用户原始余额：" + foOriginallyBalance.ToString("G")+ "扣款：" + foMuPaFee.ToString("G");
                strProResult += "\r\n" + "用户当前余额：" + foCurrentBalance.ToString("G") + "\r\n";
                
                EagleAPI.LogWrite(agrs + strProResult);
                GlobalVar.b_NotDoDec = true;//先扣款成功
                DoEtdz();
            }
            else
            {
                //if (cm == "RetDecFee" && decstat == "Deced")
                //{
                //}
                //else
                //{
                EagleAPI.LogWrite("cm:" + cm + " err:" + err + "\r\n>");
                DoEtdz();
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
            try
            {
                if (bContainChild && !isChild)
                {
                    strProResult = "\r\n儿童票不能和成人订在一个PNR中出票！\r\n>";
                    return false;
                }
                if (isChild)
                {
                    if (intChildNum != 0 && foChildFee != 0.0)
                    {
                        foTotalChildFee = foChildFee * intChildNum;
                        foMuPaFee = foTotalChildFee;
                        return true;
                    }
                    else
                    {
                        strProResult = "\r\n取得的儿童票价出错！\r\n>";
                        return false;
                    }
                }
                if (intBabynNum == 0)
                {
                    if (intAdultNum != 0 && foAdultFee != 0.0)
                    {
                        foTotalAdultFee = foAdultFee * intAdultNum;
                        foMuPaFee = foTotalAdultFee;
                        return true;
                    }
                    else
                    {
                        strProResult = "\r\n取得的成人票价出错！\r\n>";
                        return false;
                    }
                }
                else
                {
                    if (intAdultNum != 0 && foAdultFee != 0.0 && intBabynNum != 0 && foBabyFee != 0.0)
                    {
                        foTotalAdultFee = foAdultFee * intAdultNum;
                        foTotalBabyFee = foBabyFee * intBabynNum;
                        foMuPaFee = foTotalBabyFee + foTotalAdultFee;
                        return true;
                    }
                    else
                    {
                        strProResult = "\r\n取得的成人和婴儿票价出错，可能是未做婴儿票价！\r\n>";
                        return false;
                    }
                }
            }
            catch (Exception ea)
            {
                strProResult = "\r\n计算机票价出错！:"+ea.Message+"\r\n>";
                return false;
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
            np.AddPara("TicketPrice", foMuPaFee.ToString());
            string strReq = np.GetXML();
            string strRet = ws.getEgSoap(strReq);
            GlobalVar.f_Balance -= (decimal)(foMuPaFee);
            EagleAPI.LogWrite(strPnr + " DecFee 先扣款成功!\r\n");
            return strRet;
        }
        /// <summary>
        /// 5、出票
        /// </summary>
        public void DoEtdz()
        {
            GlobalVar.b_netdz = true;
            //EagleAPI.EagleSendOneCmd("^|_^i~rtN3BQC~etdz:9", 1);//测试用
            EagleAPI.EagleSendOneCmd(strETCommand, 3);
            EagleAPI.LogWrite("\r\n"+DateTime.Now.ToShortTimeString()+"NewDecFee向航信发出指令："+strETCommand);

        }

    }
}
