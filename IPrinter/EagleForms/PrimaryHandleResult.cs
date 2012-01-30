using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using EagleControls;
using EagleProtocal;
using EagleExtension;
using EagleString;
namespace EagleForms
{
    public partial class Primary
    {
        private string pnrOperating;
        private int pnrstatOperating;
        /// <summary>
        /// 对后台反回结果t的处理
        /// </summary>
        /// <param name="t"></param>
        public void HandleResult_background(string t)
        {
            switch (commandPool_back.TYPE)
            {
                case ETERM_COMMAND_TYPE.RT:
                    commandPool_back.Clear();
                    uploadEticketInfo.Recv(new RtResult(t, commandPool_back.PNRing));
                    break;
            }
        }
        /// <summary>
        /// 对返回结果的处理
        /// </summary>
        public void HandleResult()
        {
            switch (commandPool.TYPE)
            {
                case ETERM_COMMAND_TYPE.AV:
                    HandleAV(mainStatusBar.SHOW_PROFIT, mainStatusBar.SHOW_GROUP, mainStatusBar.SHOW_SPECTICK);
                    break;
                case ETERM_COMMAND_TYPE .SS:
                    HandleSS(true);
                    break;
                case ETERM_COMMAND_TYPE.ETDZ:
                    HandleEtdz();
                    break;
                case ETERM_COMMAND_TYPE.PAT:
                    break;
                case ETERM_COMMAND_TYPE.RT:
                    HandleRt(dataHandler.COMMANDRESULT);
                    break;
                case ETERM_COMMAND_TYPE.SFC:
                    break;
                case ETERM_COMMAND_TYPE.TRFD:
                    new Thread(new ThreadStart(HandleTrfd)).Start();
                    string[] a = new string[] { "TKT# MISMATCH, REOPEN IT", "OPTION" };
                    for (int i = 0; i < a.Length; i++)
                    {
                        if (dataHandler.COMMANDRESULT.Contains(a[i])) AppendBlackWindow(a[i] + "\r\n>");
                    }
                    break;
                case ETERM_COMMAND_TYPE.TRFX:
                    HandleTrfx();
                    break;
                case ETERM_COMMAND_TYPE.TRFU:
                    HandleTrfu();
                    break;
                case ETERM_COMMAND_TYPE.XEPNR:
                    HandleXepnr();
                    break;
                case ETERM_COMMAND_TYPE.PN:
                    HandlePn();
                    break;
                case ETERM_COMMAND_TYPE .DETRF:
                    HandleDetrF();
                    break;
                case ETERM_COMMAND_TYPE .DETR:
                    HandleDetr();
                    break;
                case ETERM_COMMAND_TYPE.TPR:
                    break;
                case ETERM_COMMAND_TYPE .TSL:
                    break;
                case ETERM_COMMAND_TYPE.RECEIPT_PRINT:
                    HandleReceiptPrint();
                    break;
                case ETERM_COMMAND_TYPE.RECEIPT_CANCEL:
                    HandleReceiptCancel();
                    break;
                case ETERM_COMMAND_TYPE.ETDZ_ONEKEY_RT:
                    if (loginInfo.b2b.lr.AuthorityOfFunction("0DZ"))
                    {
                        AppendBlackWindow("需要打开一键出票权限！\r\n>");
                    }
                    else
                    {
                        HandleEtdzOneKeyRt();
                    }
                    break;
                case ETERM_COMMAND_TYPE.ETDZ_ONEKEY_PAT:
                    HandleEtdzOneKeyPat();
                    break;
                case ETERM_COMMAND_TYPE.PNR_ORDER_SUBMIT:
                    HandlePnrOrderSubmitRt();
                    break;
                case ETERM_COMMAND_TYPE.QUEUE_CLEAR_AUTO:
                    HandleQueueClear();
                    break;
                case ETERM_COMMAND_TYPE.RRT_AIRCODE2PNR:
                    HandleAirCode2Pnr();
                    break;
                case ETERM_COMMAND_TYPE.TPR_IMPORT:
                    new Thread(new ThreadStart(HandleTprImport)).Start();
                    break;
                case ETERM_COMMAND_TYPE.TOL_INCOMING:
                    HandleTolImport();
                    break;
                case ETERM_COMMAND_TYPE.SS_4PassengerAddForm:
                    HandleSS4PassengerAddForm();
                    break;
                case ETERM_COMMAND_TYPE.FD:
                    HandleFD();
                    break;
                case ETERM_COMMAND_TYPE.DETR_ExpiredTicketFind:
                    expireTicketFinder.Recv(dataHandler.COMMANDRESULT);
                    break;
                case ETERM_COMMAND_TYPE.DETR_GetReceiptNoFinance:
                    DetrFResult detrfr = new DetrFResult(dataHandler.COMMANDRESULT);
                    finance.SetReceiptNumber(detrfr.TKTN, detrfr.RECEIPTNO);
                    break;
            }
        }
        /// <summary>
        /// 处理AV返回结果(4次WebService:政策,价格,散客拼团,特殊舱申请)
        /// </summary>
        /// <param name="bPolicy">是否显示政策</param>
        /// <param name="bGroup">是否显示散拼</param>
        /// <param name="bSpecTick">是否显示特舱申请</param>
        private void HandleAV(bool bPolicy, bool bGroup, bool bSpecTick)
        {
            if (commandPool.TYPE == ETERM_COMMAND_TYPE.AV) lowestList.Items.Clear();
            AvResult TempAvres = new AvResult(dataHandler.COMMANDRESULT, m_avPrice, m_avDistance);
            if (commandPool.TYPE == ETERM_COMMAND_TYPE.PN)
            {
                if (TempAvres.FlightDate_DT != avResult.FlightDate_DT)//翻页并且到了第二天
                {
                    if (easyMain != null && tcMain.SelectedTab == tpEasy)
                    {
                        easyMain.AddResult(TempAvres, mainStatusBar.SHOW_PROFIT);
                    }
                    return;
                }
            }
            try
            {
                avResult = new AvResult(dataHandler.COMMANDRESULT, m_avPrice, m_avDistance);
                System.Data.OleDb.OleDbConnection cn = new System.Data.OleDb.OleDbConnection();
                ListView lview = (ListView)lowestList;
                //显示到最低价与返点列表
                EagleExtension.EagleExtension.AvResultToListView_Lowest(
                                bPolicy,
                                dataHandler.COMMANDRESULT,
                                cn,
                                "",
                                loginInfo.b2b.webservice,
                                lview,
                                loginInfo.b2b.username,
                                ref m_avPrice,
                                ref m_avDistance
                                );
                if (bGroup)
                {
                    lview = (ListView)groupList;
                    //显示散拼列表
                    EagleExtension.EagleExtension.GroupResultToListView_Group(
                                    loginInfo.b2b.username,
                                    'A',
                                    loginInfo.b2b.webservice,
                                    dataHandler.COMMANDRESULT,
                                    lview
                                    );
                }
                if (bSpecTick)
                {
                    lview = (ListView)specTickListFix;
                    ListView lview2 = (ListView)specTickListFlow;
                    //显示固定与浮动列表
                    EagleExtension.EagleExtension.SpecTickResultToListView_Spec(
                                    dataHandler.COMMANDRESULT,
                                    loginInfo.b2b.webservice,
                                    lview,
                                    lview2,
                                    m_avPrice
                                    );
                }
            }
            catch (Exception ex)
            {
                AppendBlackWindow("HandleAV : " + ex.Message + "\r\n>");
            }
            if (easyMain != null && tcMain.SelectedTab==tpEasy)
            {
                easyMain.AddResult(avResult, mainStatusBar.SHOW_PROFIT);
            }
        }
        /// <summary>
        /// 处理预定结果,并指定是否显示中文结果
        /// </summary>
        /// <param name="show">订票成功后是否显示中文结果</param>
        private void HandleSS(bool show)
        {
            try
            {
                ssResult = new SsResult(dataHandler.COMMANDRESULT);
                if (ssResult.SUCCEED)
                {
                    OuterHandleSsResult(ssResult);
                    ssResult.CreateDate = DateTime.Now;
                    SsResultList se = SsResultList.DeSerializeSsResults();
                    if (se == null) se = new SsResultList();
                    se.ls.Add(ssResult);
                    se.SerializeSsResults();
                    if (show)
                    {
                        AppendBlackWindow(ssResult.CHINESESTRING + "\r\n>");
                    }
                    pnrOperating = ssResult.PNR;
                    pnrstatOperating = 0;
                    Thread thread = new Thread(new ThreadStart(th_SubmitPnrState));
                    thread.Start();

                    

                }
                easyMain.RecvSS(dataHandler.COMMANDRESULT);
            }
            catch (Exception ex)
            {
                EagleString.EagleFileIO.LogWrite("HandleSS : " + ex.Message + ex.TargetSite+"\r\n>");
            }

        }
        /// <summary>
        /// 提交PNR的线程函数，先置pnrOperating,pnrstatOperating
        /// </summary>
        private void th_SubmitPnrState()
        {
            try
            {
                bool bWSflag = false;
                List<string> offices = new List<string>();
                for (int i = 0; i < loginInfo.b2b.lr.indexOfUsing.Count; ++i)
                {
                    int index = loginInfo.b2b.lr.indexOfUsing[i];
                    offices.Add(loginInfo.b2b.lr.m_ls_office[index].OFFICE_NO);
                }
                string officename = string.Join(",", offices.ToArray());
                while (!bWSflag)
                {
                    wserviceKernal.SubmitPnrState(
                                                loginInfo.b2b.username,
                                                pnrOperating,
                                                pnrstatOperating,
                                                officename,
                                                ref bWSflag
                                                );
                }
                EagleString.EagleFileIO.LogWrite(string.Format("WEBSERVICE THREAD SUBMITPNRSTATE SUCCEED! (PNR={0})(OFFICE={1})({2})",
                                                                pnrOperating,
                                                                officename,
                                                                pnrstatOperating
                                                                ));
            }
            catch
            {
            }
        }
        /// <summary>
        /// 处理Xepnr返回结果
        /// </summary>
        private void HandleXepnr()
        {
            try
            {
                xepnrResult = new XepnrResult(dataHandler.COMMANDRESULT);
                if (xepnrResult.SUCCEED)
                {
                    pnrstatOperating = 2;
                    pnrOperating = xepnrResult.PNR;
                    if (pnrOperating == "") pnrOperating = rtResult.PNR;
                    Thread thread = new Thread(new ThreadStart(th_SubmitPnrState));
                    thread.Start();
                }
            }
            catch (Exception ex)
            {
                AppendBlackWindow("HandleXepnr : " + ex.Message + "\r\n>");
            }
        }
        private void HandleRt(string rtstring)
        {
            try
            {
                rtResult = new RtResult(rtstring, commandPool.PNRing);
                OuterHandleRtResult(rtResult);//Outer配置互换
                pnrOperating = commandPool.PNRing;
                pnrstatOperating = 1;
                Thread thread = new Thread(new ThreadStart(th_SubmitPnrState));
                thread.Start();
                int total = 0;
                try
                {
                    receipt.SetControlsByRtResult(rtResult);
                }
                catch
                {
                }
                try
                {
                    Printer.Insurance.Instance.SetControlsByRtResult(rtResult);
                }
                catch
                {
                }
                try
                {
                    EagleExtension.EagleExtension.CalPnrsTotalPrice(rtResult, loginInfo.b2b.webservice, ref total);
                    EagleString.EagleFileIO.LogWrite("预计总价格(不含婴儿)：" + total.ToString() + "\r\n>");
                }
                catch
                {
                }
            }
            catch(Exception ex)
            {
                EagleString.EagleFileIO.LogWrite("HandleRt : " + ex.Message + "\r\n>");
            }
        }
        /// <summary>
        /// 处理翻面，如AV,RT的翻页
        /// </summary>
        private void HandlePn()
        {
            switch (commandPool.TYPELAST)
            {
                case ETERM_COMMAND_TYPE.AV:
                    HandleAV(mainStatusBar.SHOW_PROFIT, mainStatusBar.SHOW_GROUP, mainStatusBar.SHOW_SPECTICK);
                    break;
                case ETERM_COMMAND_TYPE.RT:
                    HandleRt(rtResult.TXT +"\r\n"+ dataHandler.COMMANDRESULT);
                    break;
            }
        }
        /// <summary>
        /// 处理detr返回结果给打印对话框(行程单,保险,等)
        /// </summary>
        private void HandleDetr()
        {
            try
            {
                detrResult = new DetrResult(dataHandler.COMMANDRESULT);
                try
                {
                    receipt.SetControlsByDetrResult(detrResult);
                }
                catch
                {
                }
                try
                {
                    Printer.Insurance.Instance.SetControlsByDetrResult(detrResult);
                }
                catch
                {
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// 处理detr,f结果给打印对话框(行程单,保险,等)
        /// </summary>
        private void HandleDetrF()
        {
            try
            {
                detrFResult = new DetrFResult(dataHandler.COMMANDRESULT);
                if(receipt!=null)
                    receipt.SetCardByDetrfResult(detrFResult);
                //保险打印也需要身份证
                if(Printer.Insurance.Instance!=null)
                    Printer.Insurance.Instance.SetCardByDetrfResult(detrFResult);
            }
            catch(Exception ex)
            {
                AppendBlackWindow("HandleDetrF : " + ex.Message + "\r\n>");
            }
        }
        /// <summary>
        /// 打印行程单操作的返回结果处理
        /// </summary>
        private void HandleReceiptPrint()
        {
            string errorReason = EagleString.egString.Between2String(dataHandler.COMMANDRESULT, "<ErrorReason>", "</ErrorReason>");
            
            if (errorReason == "")
            {
                receipt.Print();    
            }
            else
            {
                MessageBox.Show(errorReason);
            }
        }
        /// <summary>
        /// 作废行程单操作的返回结果处理
        /// </summary>
        private void HandleReceiptCancel()
        {
            string errorReason = EagleString.egString.Between2String(dataHandler.COMMANDRESULT, "<ErrorReason>", "</ErrorReason>");
            if (errorReason == "")
            {
                AppendBlackWindow("作废成功！\r\n>");
            }
            else
            {
                MessageBox.Show(errorReason);
            }
        }

        /// <summary>
        /// 一键出票RT结果处理
        /// </summary>
        private void HandleEtdzOneKeyRt()
        {
            try
            {
                rtResult = new RtResult(dataHandler.COMMANDRESULT, commandPool.PNRing);
                m_etdzonekey.rtres = rtResult;
                //TODO:此处增加预计金额控制
                {
                    float yue = EagleExtension.EagleExtension.BALANCE(loginInfo.b2b.username, loginInfo.b2b.webservice);
                    int dec = 0;
                    EagleExtension.EagleExtension.CalPnrsTotalPrice(rtResult, loginInfo.b2b.webservice, ref dec);
                    AppendBlackWindow(yue.ToString("f2") + "-" + dec.ToString() + "\r\n>");
                    //if ((float)dec > yue) return;
                }
                if (string.IsNullOrEmpty(m_etdzonekey.pat))
                {
                    string cmd = m_etdzonekey.CreateEtdzString();
                    //AppendBlackWindow(cmd + "    无PAT项生成的etdz串\r\n>");
                    if (string.IsNullOrEmpty(cmd))
                    {
                        commandPool.HandleCommand("i");
                    }
                    else
                    {
                        commandPool.SetType(ETERM_COMMAND_TYPE.ETDZ);
                        CheckBeforeSend();
                        socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.Multi);//发送ETDZ
                    }
                }
                else
                {
                    string cmd = commandPool.HandleCommand(m_etdzonekey.pat);
                    //AppendBlackWindow(cmd + "     有PAT项,发送\r\n>");
                    commandPool.SetType(ETERM_COMMAND_TYPE.ETDZ_ONEKEY_PAT);
                    socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.Multi);//发送PAT
                }
            }
            catch(Exception ex)
            {
                AppendBlackWindow("HandleEtdzOneKeyRt : " + ex.Message + "\r\n>");
            }
        }
        /// <summary>
        /// 一键出票PAT结果处理
        /// </summary>
        private void HandleEtdzOneKeyPat()
        {
            try
            {
                m_etdzonekey.patres = new PatResult(dataHandler.COMMANDRESULT);
                string cmd = m_etdzonekey.CreateEtdzString();
                //AppendBlackWindow(cmd + "    有PAT项生成的etdz串\r\n>");
                if (string.IsNullOrEmpty(cmd))
                {
                    commandPool.HandleCommand("i");
                }
                else
                {
                    commandPool.SetType(ETERM_COMMAND_TYPE.ETDZ);
                    CheckBeforeSend();
                    socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.Multi);//发送ETDZ
                }
            }
            catch (Exception ex)
            {
                AppendBlackWindow("HandleEtdzOneKeyPat : " + ex.Message + "\r\n>");
            }
        }
        /// <summary>
        /// 处理ETDZ结果
        /// </summary>
        private void HandleEtdz()
        {
            try
            {
                pnrOperating = commandPool.PNRing;
                pnrstatOperating = 3;

                Thread thread = new Thread(new ThreadStart(th_SubmitPnrState));
                thread.Start();

                EagleString.EtdzResult etdzResult = new EtdzResult(dataHandler.COMMANDRESULT);
                
                if (etdzResult.TOTAL > 0)
                {
                    bool suc = false;
                    float yue = 0F;
                    wserviceKernal.DecFee(commandPool.PNRing, etdzResult.TOTAL, ref suc, ref yue);
                    if (suc)
                    {
                        AppendBlackWindow(string.Format("扣款{0},余额{1}\r\n>", etdzResult.TOTAL, yue));
                    }
                }
            }
            catch (Exception ex)
            {
                AppendBlackWindow("HandleEtdz : " + ex.Message + "\r\n>");
            }

        }
        /// <summary>
        /// 处理TRFD结果
        /// </summary>
        private void HandleTrfd()
        {
            try
            {
                ToCommand.RefundTicket refundTicket1 = new ToCommand.RefundTicket(socket, commandPool, loginInfo);
                refundTicket1.SetControlsByTrfuString(dataHandler.COMMANDRESULT);
                refundTicket1.ShowDialog();
            }
            catch(Exception ex)
            {
                 EagleString.EagleFileIO.LogWrite("HandleTrfd : " + ex.Message + "\r\n>");
            }
        }
        /// <summary>
        /// 处理TRFX结果
        /// </summary>
        private void HandleTrfx()
        {
            //删除退票单成功，上传信息
        }
        /// <summary>
        /// 处理TRFU结果
        /// </summary>
        private void HandleTrfu()
        {
            //退票成功，上传退票信息
        }
        /// <summary>
        /// 提交PNR订单发出的RT返回，获取PNR信息，并提交   --待测
        /// </summary>
        private void HandlePnrOrderSubmitRt()
        {
            try
            {
                rtResult = new RtResult(dataHandler.COMMANDRESULT, commandPool.PNRing);
                if (rtResult.SEGMENG.Length == 0) throw new Exception("缺少有效航段");
                if (rtResult.CARDID == null || rtResult.CARDID.Length == 0) throw new Exception("缺少证件号码");
                int fare=0;
                double real = 0.0;
                int build = 0;
                int fuel = 0;
                double gain = 0.0;
                double lirun = 0.0;
                double total = 0.0;
                EagleExtension.EagleExtension.CalPnrSomePrice(rtResult, loginInfo, ref fare, ref real, ref build, ref fuel,
                    ref gain, ref lirun, ref  total);
                bool succeed = false;
                string passports = "";
                wserviceKernal.SubmitPnr(
                    loginInfo.b2b.username,
                    rtResult.PNR,
                    DateTime.Now,
                    "",
                    string.Join(";", rtResult.Name_CARDS),
                    EagleString.EagleFileIO.ValueOf("PNRORDERSUBMITPHONE"),
                    rtResult.PSGCOUNT,
                    rtResult.FLIGHTS,
                    rtResult.BUNKS,
                    rtResult.FLIGHTDATES,
                    rtResult.CITYPAIRS,
                    fare,
                    real,
                    build,
                    fuel,
                    gain,
                    lirun,
                    total,
                    ref succeed,
                    ref passports);
                if (succeed)
                {
                    {
                        rtResult.SubmittdDate = DateTime.Now;
                        RtResultList rrl = RtResultList.DeSerializeRtResults();
                        if (rrl == null) rrl = new RtResultList();
                        rrl.ls.Add(rtResult);
                        rrl.SerializeRtResults();
                        
                    }
                    if (passports.Length >= 32)//发送提示新订单消息
                    {
                        if (tcMain.SelectedTab == tpBlack)
                            AppendBlackWindow(string.Format("PNR订单提交成功，PNR={0}\r\n>", rtResult.PNR));
                        else
                            MessageBox.Show(string.Format("PNR订单提交成功，PNR={0}", rtResult.PNR));
                        socket.SendNewPnrOrder(passports.Split('~'));
                    }
                }
                else
                {
                    if (tcMain.SelectedTab == tpBlack)
                        AppendBlackWindow(string.Format("PNR订单提交失败，PNR={0}\r\n>", rtResult.PNR));
                    else
                        MessageBox.Show(string.Format("PNR订单提交失败，PNR={0}", rtResult.PNR));
                }
            }
            catch(Exception ex)
            {
                AppendBlackWindow("HandlePnrOrderSubmitRt : " + ex.Message);
                if (tcMain.SelectedTab != tpBlack)MessageBox.Show("HandlePnrOrderSubmitRt : " + ex.Message);
            }
        }
        private void HandleQueueClear()
        {
            try
            {
                if (queueClear == null)
                {
                    queueClear = new global::EagleForms.ToCommand.QueueClear(socket, commandPool);
                    queueClear.Show();
                }
                queueClear.setcontrol(dataHandler.COMMANDRESULT);
            }
            catch
            {
            }
        }
        private void HandleAirCode2Pnr()
        {
            try
            {
                airCode2Pnr.setcontrol(dataHandler.COMMANDRESULT);
            }
            catch
            {
            }
        }
        private void HandleTprImport()
        {
            try
            {
                finance.autoImport.TprRecv(dataHandler.COMMANDRESULT);
            }
            catch(Exception ex)
            {
                AppendBlackWindow("财务对帐平台 : " + ex.Message);
                AppendBlackWindow("TPR导入中断");
            }
            commandPool.Clear();
        }
        private void HandleTolImport()
        {
            try
            {
                finance.autoImport.TolRecv(dataHandler.COMMANDRESULT);
            }
            catch (Exception ex)
            {
                AppendBlackWindow("财务对帐平台 : " + ex.Message);
                AppendBlackWindow("入库中断");
            }
        }
        private void HandleSS4PassengerAddForm()
        {
            passengerAdd.RecvSS(dataHandler.COMMANDRESULT);
        }
        private void HandleFD()
        {
            FdResult fdresult = new FdResult(dataHandler.COMMANDRESULT);
            if (easyMain != null && tcMain.SelectedTab == tpEasy)
            {
                easyMain.AddResult(fdresult);
            }

            if (fdresult.PRICE > 0)
            {
                string[] citypair = EagleString.BaseFunc.CITYPAIR_ALLY(fdresult.CITYPAIR).ToArray();
                System.Collections.Hashtable ht = new System.Collections.Hashtable();
                for (int i = 0; i < citypair.Length; ++i)
                {
                    ht.Add(citypair[i], fdresult.PRICE.ToString() + "," + fdresult.DISTANCE.ToString());
                    ht.Add(citypair[i].Substring(3)+citypair[i].Substring(0,3), fdresult.PRICE.ToString() + "," + fdresult.DISTANCE.ToString());
                }
                EagleString.EagleFileIO.WiteHashTableToEagleDotTxt(ht, "", EagleString.EagleFileIO.File_Price);
            }
            //PriceFileSet(ls_city, ref temp_pos1, ref temp_pos2);
        }
        int temp_pos1 = 0;
        int temp_pos2 = 0;
        private void PriceFileSet(List<string> lsCity,ref int pos1,ref int pos2)
        {
            pos2++;
            if (pos2 >= lsCity.Count)
            {
                pos1++;
                pos2 = pos1 + 1;
            }
            if (pos1 >= lsCity.Count) return;
            string cmd = "FD:" + lsCity[pos1].Substring(0, 3) + lsCity[pos2].Substring(0, 3);
            commandPool.Clear();
            cmd = commandPool.HandleCommand(cmd);
            AppendBlackWindow(cmd);
            socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.Multi);
        }
    }
}
