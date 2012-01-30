using System;
using System.Xml;
using gs.para;

namespace logic
{
	/// <summary>
	/// CMM ��ժҪ˵����
	/// </summary>
	public class CMM
	{
		public CMM()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public static string procCmm(string p_str)
		{
			string strRet = "";
			try
			{
				//gs.util.func.Write("xml is =" + p_str);

				NewPara npRet = new NewPara(p_str.Trim());
				string strCmm = npRet.FindTextByPath("//eg/cm").Trim();
				if(strCmm != "")
				{
					MessProc mp = new MessProc();
					
					if(strCmm == "Login")
					{
						string strUserName = npRet.FindTextByPath("//eg/UserName").Trim();
						string strPassWord = npRet.FindTextByPath("//eg/PassWord").Trim();
						strRet = mp.login(strUserName,strPassWord);
					}

                    if (strCmm == "GetIbeUrl")
                    {
                        string strUserName = npRet.FindTextByPath("//eg/UserName").Trim();
                        string ibeID = npRet.FindTextByPath("//eg/ibeID").Trim();
                        strRet = mp.GetIbeUrl(strUserName, ibeID);
                    }

					#region
					if(strCmm == "GetCloseBalance") //�õ��ʻ����
					{
						string strUserName = npRet.FindTextByPath("//eg/UserName").Trim();
						strRet = mp.getCloseBalance(strUserName);
					}
					
					if(strCmm == "DecFee")         
					{//���ѿۿ�
						string strPnr = npRet.FindTextByPath("//eg/Pnr").Trim();
						string strVal = npRet.FindTextByPath("//eg/TicketPrice").Trim();
						gs.util.func.Write("Dec Fee Pnr====(" + strPnr + ")(" + strVal + ")");
						strRet = mp.DecFee(strPnr,strVal);
					}

					if(strCmm == "DecFee2")         
					{//���ѿۿ�2
						string strPnr = npRet.FindTextByPath("//eg/Pnr").Trim();
						string strVal = npRet.FindTextByPath("//eg/TicketPrice").Trim();
                        gs.util.func.Write("Dec Fee2 Pnr====(" + strPnr + ")(" + strVal + ")");
						strRet = mp.DecFee2(strPnr,strVal);
                    }
                    if (strCmm == "DecFee3")
                    {//���ѿۿ�3д����ӿ�Ʊ��
                        string strPnr = npRet.FindTextByPath("//eg/Pnr").Trim();
                        string strVal = npRet.FindTextByPath("//eg/TicketPrice").Trim();
                        string strNumber = npRet.FindTextByPath("//eg/TicketNumber").Trim();
                        gs.util.func.Write("Dec Fee3 Pnr====(" + strPnr + ")(" + strVal + ")");
                        strRet = mp.DecFee3(strPnr, strVal, strNumber);

                    }

					if(strCmm == "BackFee")         
					{//���ѿۿ�
						string strUserName = npRet.FindTextByPath("//eg/UserName").Trim();
						string strVal = npRet.FindTextByPath("//eg/TicketPrice").Trim();
						strRet = mp.BackFee(strUserName,strVal);
					}

					if(strCmm == "RefreshSelf")         
					{//ˢ���û�����ʱ��
						string strUserName = npRet.FindTextByPath("//eg/UserName").Trim();
						strRet = mp.RereshSelf(strUserName);
					}

					if(strCmm == "Logout")         
					{//�û��ǳ�ϵͳ,ɾ�������û�
						string strUserName = npRet.FindTextByPath("//eg/UserName").Trim();//bug���ͻ��˴������Ľڵ������ǡ�user�������ǡ�UserName�������¸ù�����ͬ����
						strRet = mp.logout(strUserName);
					}

					if(strCmm == "CmpPassport")      //��ʱ������.   
					{//�������̷���������������ݿ��и��û��Ƿ��Ѿ�����ϵͳ,ͨ����֤
						string strUserName = npRet.FindTextByPath("//eg/UserName").Trim();
						string strPassPort = npRet.FindTextByPath("//eg/Passport").Trim();
						EgUser eu = new EgUser();
						strRet = eu.CmpPassport(strPassPort);   //���YANG�޸���һ��,ȥ���û���
					}

					if(strCmm == "ChgPassword")         
					{//�û��޸�����
						string strUserName = npRet.FindTextByPath("//eg/UserName").Trim();
						string strPassword = npRet.FindTextByPath("//eg/Password").Trim();
						strRet = mp.ChgPassword(strUserName,strPassword);
					}

					if(strCmm == "SaveOperations")         
					{//�û��޸�����
						string strUserName = npRet.FindTextByPath("//eg/User").Trim();
						string strCm = npRet.FindTextByPath("//eg/OperationString").Trim();
						string strCmType = npRet.FindTextByPath("//eg/Send_Recieve").Trim();
						string strTime = npRet.FindTextByPath("//eg/OperationTime").Trim();
						strRet = mp.SaveLogs(strUserName,strCm,strCmType,strTime);
					}

					if(strCmm == "SubmitPNR")
					{//����ɵ�ϰ��PNR��Ʊ��Ϣ
                        gs.util.func.Write("pnr:" + npRet.FindTextByPath("//eg/PNR").Trim() + " cm:" + strCmm);
						strRet = mp.NewPnr(p_str.Trim());
					}

					if(strCmm == "SubmitHYX")         
					{//�����ձ���
						string strUserID = npRet.FindTextByPath("//eg/UserID").Trim();
						string streNumber = npRet.FindTextByPath("//eg/eNumber").Trim();
						string strIssueNumber = npRet.FindTextByPath("//eg/IssueNumber").Trim();
						string strNameIssued = npRet.FindTextByPath("//eg/NameIssued").Trim();
						string strCardType = npRet.FindTextByPath("//eg/CardType").Trim();

						string strCardNumber = npRet.FindTextByPath("//eg/CardNumber").Trim();
						string strRemark = npRet.FindTextByPath("//eg/Remark").Trim();
						string strIssuePeriod = npRet.FindTextByPath("//eg/IssuePeriod").Trim();
						string strIssueBegin = npRet.FindTextByPath("//eg/IssueBegin").Trim();
						string strIssueEnd = npRet.FindTextByPath("//eg/IssueEnd").Trim();

						string strSolutionDisputed = npRet.FindTextByPath("//eg/SolutionDisputed").Trim();
						string strNameBeneficiary = npRet.FindTextByPath("//eg/NameBeneficiary").Trim();
						string strSignature = npRet.FindTextByPath("//eg/Signature").Trim();
						string strSignDate = npRet.FindTextByPath("//eg/SignDate").Trim();
						string strInssuerName = npRet.FindTextByPath("//eg/InssuerName").Trim();//

						string strPnr = npRet.FindTextByPath("//eg/Pnr").Trim();

						strRet = mp.RegInsurance(strUserID,streNumber,strIssueNumber,
								strNameIssued,strCardType,strCardNumber,
								strRemark,strIssuePeriod,strIssueBegin,
								strIssueEnd,strSolutionDisputed,strNameBeneficiary,
								strSignature,strSignDate,strInssuerName,strPnr);
					}

					if (strCmm == "PreSubmitETicket")
					{
                        gs.util.func.Write("pnr:" + npRet.FindTextByPath("//eg/PNR").Trim() + " cm:" + strCmm);
						string strUserID = npRet.FindTextByPath("//eg/UserID").Trim();
						string strPnr = npRet.FindTextByPath("//eg/Pnr").Trim();
						string strDecFeeState = npRet.FindTextByPath("//eg/DecFeeState").Trim();
						string strIpId = npRet.FindTextByPath("//eg/IpId").Trim();
						strRet = mp.RegETicket(strUserID,strPnr,							
							null,null,null,null,null,null,null,
							null,null,null,strDecFeeState,"RetPreSubmitETicket",strIpId,null,null,null
							);
					}

					if(strCmm == "SubmitETicket")         
					{//���ӿ�Ʊ�ϴ�
                        gs.util.func.Write("pnr:" + npRet.FindTextByPath("//eg/PNR").Trim() + " cm:" + strCmm);
//						string strUserID = npRet.FindTextByPath("//eg/UserID").Trim();
						string strPnr = npRet.FindTextByPath("//eg/Pnr").Trim();
						string stretNumber = npRet.FindTextByPath("//eg/etNumber").Trim();
						string strFlightNumber1 = npRet.FindTextByPath("//eg/FlightNumber1").Trim();

						string strBunk1 = npRet.FindTextByPath("//eg/Bunk1").Trim();
						string strCityPair1 = npRet.FindTextByPath("//eg/CityPair1").Trim();
						string strDate1 = npRet.FindTextByPath("//eg/Date1").Trim();
						string strFlightNumber2 = npRet.FindTextByPath("//eg/FlightNumber2").Trim();

						string strBunk2 = npRet.FindTextByPath("//eg/Bunk2").Trim();
						string strCityPair2 = npRet.FindTextByPath("//eg/CityPair2").Trim();
						string strDate2 = npRet.FindTextByPath("//eg/Date2").Trim();
						string strTotalFC = npRet.FindTextByPath("//eg/TotalFC").Trim();
						string strDecFeeState = npRet.FindTextByPath("//eg/DecFeeState").Trim();
						//string strIpId = npRet.FindTextByPath("//eg/IpId").Trim();
						string strIpId ="";
						string strBasePrc = npRet.FindTextByPath("//eg/numBasePrc").Trim();
						string strOil = npRet.FindTextByPath("//eg/numFuel").Trim();
						string strPassenger = npRet.FindTextByPath("//eg/Passenger").Trim();

						strRet = mp.RegETicket(null,strPnr,stretNumber,
							strFlightNumber1,strBunk1,strCityPair1,
							strDate1,strFlightNumber2,strBunk2,
							strCityPair2,strDate2,strTotalFC,strDecFeeState,"RetSubmitETicket",strIpId,strBasePrc,strOil,strPassenger
							);
					}

					if (strCmm == "CancelPNR")
					{
						string strPnr = npRet.FindTextByPath("//eg/Pnr").Trim();
						strRet = mp.DelETicket(strPnr);
					}

					if (strCmm == "GetUncheckedPNR")
					{						
						string strUserID = npRet.FindTextByPath("//eg/UserID").Trim();
						strRet = mp.QueryETicket(strUserID);
					}

					if(strCmm == "GetPNRs")         
					{//
						string strUserName = npRet.FindTextByPath("//eg/User").Trim();
						string strPNRState = npRet.FindTextByPath("//eg/PNRState").Trim();
						strRet = mp.GetStrPNRs(strUserName,strPNRState);
					}

					if(strCmm == "SetPNRStateDelete")         
					{//
						string strUserName = npRet.FindTextByPath("//eg/User").Trim();
						string strPNRs = npRet.FindTextByPath("//eg/PNR").Trim();
						strRet = mp.SetPnrDelState(strUserName,strPNRs);
					}

					if(strCmm == "GetFC")         
					{//
						string strFROM = npRet.FindTextByPath("//eg/FROM").Trim();
						string strTO = npRet.FindTextByPath("//eg/TO").Trim();
						strRet = mp.GetFC(strFROM,strTO);
					}

					if(strCmm == "SaveFC")         
					{//
						string strFROM = npRet.FindTextByPath("//eg/FROM").Trim();
						string strTO = npRet.FindTextByPath("//eg/TO").Trim();
						string strBUNKF = npRet.FindTextByPath("//eg/BUNKF").Trim();
						string strBUNKC = npRet.FindTextByPath("//eg/BUNKC").Trim();
						string strBUNKY = npRet.FindTextByPath("//eg/BUNKY").Trim();
						strRet = mp.SaveFC(strFROM,strTO,strBUNKF,strBUNKC,strBUNKY);
					}

					if(strCmm == "WriteLog")         
					{//
						string strUser = npRet.FindTextByPath("//eg/User").Trim();
						string strCmd = npRet.FindTextByPath("//eg/Cmd").Trim();
						string strReturnResult = npRet.FindTextByPath("//eg/ReturnResult").Trim();
						
						strRet = mp.SaveSysLogs(strUser,strCmd,strReturnResult);
						//strRet = "";
					}

					if (strCmm == "GetPassenger")
					{
						string strPassenger = npRet.FindTextByPath("//eg/Passenger").Trim();
						strRet = mp.GetPassenger(strPassenger);
					}

//					if (strCmm == "CheckReceiptNumber")
//					{
//						string strUser = npRet.FindTextByPath("//eg/User").Trim();
//						string strRecieptNumber = npRet.FindTextByPath("//eg/RecieptNumber").Trim();
//						string strCfgNumber = npRet.FindTextByPath("//eg/CfgNumber").Trim();
//						strRet = mp.getEtkBound(strUser,strRecieptNumber,strCfgNumber);
//					}
					if (strCmm == "CanPrint")
					{
						string strUser = npRet.FindTextByPath("//eg/User").Trim();
						string strRecieptNumber = npRet.FindTextByPath("//eg/RecieptNumber").Trim();
						string strCfgNumber = npRet.FindTextByPath("//eg/CfgNumber").Trim();
						string strETNumber = npRet.FindTextByPath("//eg/ETNumber").Trim();
						strRet = mp.setCanPrint(strUser,strRecieptNumber,strCfgNumber,strETNumber);
					}

					if (strCmm == "AddToGroup")
					{
						string strUserID = npRet.FindTextByPath("//eg/UserID").Trim();
						string strName = npRet.FindTextByPath("//eg/Name").Trim();
						string strCardID = npRet.FindTextByPath("//eg/CardID").Trim();
						string strGroupTicketID = npRet.FindTextByPath("//eg/GroupTicketID").Trim();
						
						strRet = mp.AddGroup(strUserID,strName,strCardID,strGroupTicketID);
					}

					if (strCmm == "ListGroupTicket")
					{
						string strFromTo = npRet.FindTextByPath("//eg/FromTo").Trim();
						string strDate = npRet.FindTextByPath("//eg/Date").Trim();
						string strUserID = npRet.FindTextByPath("//eg/UserID").Trim();
						string strType = npRet.FindTextByPath("//eg/RebateType").Trim();
						
						strRet = mp.GetListGroupTicket(strFromTo,strDate,strUserID,strType);
					}

					if (strCmm == "SubmitScrollString")
					{
						string strUserID = npRet.FindTextByPath("//eg/UserID").Trim();
						string strContext = npRet.FindTextByPath("//eg/Context").Trim();
						string strBegTime = npRet.FindTextByPath("//eg/BegTime").Trim();
						string strEndTime = npRet.FindTextByPath("//eg/EndTime").Trim();
						string strNoticeType = npRet.FindTextByPath("//eg/NoticeType").Trim();						
						
						strRet = mp.AddScrollString(strUserID,strContext,strBegTime,strEndTime,strNoticeType);
					}

					if (strCmm == "GetCurrentScrollString")
					{
						string strUserID = npRet.FindTextByPath("//eg/User").Trim();									
						string strNoticeType = npRet.FindTextByPath("//eg/NoticeType").Trim();	
							
						
						strRet = mp.GetScrollString(strUserID,strNoticeType);
					}

					if (strCmm == "RequestETNumberBelong")
					{
						string strETicketNumber = npRet.FindTextByPath("//eg/ETicketNumber").Trim();														
						
						strRet = mp.getETNumberBelong(strETicketNumber);
					}
					if (strCmm == "SubmitPnrState")
					{
                        gs.util.func.Write("pnr:" + npRet.FindTextByPath("//eg/PNR").Trim() + " cm:" + strCmm);
						string strUser = npRet.FindTextByPath("//eg/User").Trim();
						string strPNR = npRet.FindTextByPath("//eg/PNR").Trim();
						string strState = npRet.FindTextByPath("//eg/State").Trim();											
						
						strRet = mp.setPnrState(strUser,strPNR,strState);
					}
					if(strCmm == "GetPubMes")
					{//�õ����µĹ�����Ϣ
						string strUser = npRet.FindTextByPath("//eg/User").Trim();
						
						strRet = mp.getPubMes(strUser);
					}

					if(strCmm == "GetUserIpsAndAgents")
					{
						string strUser = npRet.FindTextByPath("//eg/User").Trim();
						strRet = mp.GetUserIpsAndAgents(strUser);
					}

					if(strCmm == "GetTekInfo")
					{//�õ�һ�ŵ��ӿ�Ʊ����Ϣ
						string strPnr = npRet.FindTextByPath("//eg/Pnr").Trim();
						strRet = mp.GetTekInfoFromPnr(strPnr);
					}

					if(strCmm == "setIncIsCancel")
					{//���ñ��յ�״̬Ϊ����
						string strIncUserName = npRet.FindTextByPath("//eg/UserName").Trim();
						string strIncCmp = npRet.FindTextByPath("//eg/IncName").Trim();
						string strIncNo = npRet.FindTextByPath("//eg/IncNo").Trim();
						strRet = mp.cancelInsurance(strIncNo,strIncCmp,strIncUserName);
					}

					if(strCmm == "GetPromot")
					{//�õ����߹�����Ϣ
						string strPormotUserName = npRet.FindTextByPath("//eg/UserName").Trim();
						string strAirs = npRet.FindTextByPath("//eg/Airs").Trim();	//��Ҫ��ѯ�ĺ����б�,��,�ָ�
						string strDate = npRet.FindTextByPath("//eg/Date").Trim();
						string strBegin = npRet.FindTextByPath("//eg/BeginCity").Trim();
						string strEnd = npRet.FindTextByPath("//eg/EndCity").Trim();
						//strRet = mp.cancelInsurance(strIncNo,strIncCmp,strIncUserName);
						Policy pl = new Policy();
						strRet = pl.getPromote(strPormotUserName,strAirs,strDate,strBegin,strEnd);
					}

					if(strCmm == "GetUnPayPnr")
					{//�õ�δ���֧����PNR
						string strUnPayUserName = npRet.FindTextByPath("//eg/UserName").Trim();
						
						EgFee ef = new EgFee();
						strRet = ef.GetUnPayPnr(strUnPayUserName);
					}
                    if (strCmm == "GetTNumberNullPnr")
                    {//�õ����ӿ�Ʊ��Ϊ�պ�δ���֧����PNR
                        string strUnPayUserName = npRet.FindTextByPath("//eg/UserName").Trim();

                        EgFee ef = new EgFee();
                        strRet = ef.GetTNumberNullPnr(strUnPayUserName);
                    }
                    if (strCmm == "GetTNNullPnrByIPID")
                    {//�õ����ӿ�Ʊ��Ϊ�պ�δ���֧����PNR
                        string strUnPayIPID = npRet.FindTextByPath("//eg/IPID").Trim();

                        EgFee ef = new EgFee();
                        strRet = ef.GetTNNullPnrByIPID(strUnPayIPID);
                    }

					if(strCmm == "ExistUser")
					{//����û����Ƿ��ظ�
						string strExistUser = npRet.FindTextByPath("//eg/UserName").Trim();
						EgUser eu = new EgUser();

						NewPara np = new NewPara();
						np.AddPara("cm","RetExistUser");

						if(eu.isExistUser(strExistUser))
						{
							np.AddPara("stat","ReUserName");
						}
						else
						{
							np.AddPara("stat","GoodUserName");
						}
						
						strRet = np.GetXML();

					}

					if(strCmm == "AddNewCUser")
					{// �������û��Ƿ�ɹ�
						NewPara npPara = new NewPara();
						npPara.AddPara("MerchId",npRet.FindTextByPath("//eg/MerchId").Trim());
						npPara.AddPara("User",npRet.FindTextByPath("//eg/UserName").Trim());
						npPara.AddPara("UserTitle",npRet.FindTextByPath("//eg/UserTitle").Trim());
						npPara.AddPara("UserPassWord",npRet.FindTextByPath("//eg/UserPassWord").Trim());
						npPara.AddPara("Sex",npRet.FindTextByPath("//eg/Sex").Trim());
						npPara.AddPara("Birthday",npRet.FindTextByPath("//eg/Birthday").Trim());
						npPara.AddPara("CrosAdr",npRet.FindTextByPath("//eg/CrosAdr").Trim());
						npPara.AddPara("PostCode",npRet.FindTextByPath("//eg/PostCode").Trim());
						npPara.AddPara("Email",npRet.FindTextByPath("//eg/Email").Trim());
						npPara.AddPara("Tel",npRet.FindTextByPath("//eg/Tel").Trim());
						npPara.AddPara("vcUserType",npRet.FindTextByPath("//eg/vcUserType").Trim());
						
						EgUser eu = new EgUser();
						//gs.util.func.Write("AddNewCUser=" + npPara.GetXML());
						string strAddCUserStat = eu.NewCUser(npPara.GetXML());

						NewPara npRetAddCUser = new NewPara();
						npRetAddCUser.AddPara("cm","RetAddNewCUser");
						npRetAddCUser.AddPara("AddCUserStat",strAddCUserStat);
						strRet = npRetAddCUser.GetXML();

					}

					if(strCmm == "CUserFirstPage")
					{// 
						
						string strAgentCode = npRet.FindTextByPath("//eg/AgentCode").Trim();
						EgUser eu = new EgUser();

						strRet = eu.getCUserPageXml(strAgentCode);

					}

					if(strCmm == "AV")
					{// 
						
						string strAvUserName = npRet.FindTextByPath("//eg/UserName").Trim();
						string strAvAirCode = npRet.FindTextByPath("//eg/AirCode").Trim();
						string strAvFlyDate = npRet.FindTextByPath("//eg/FlyDate").Trim();
						string strAvFlyTime = npRet.FindTextByPath("//eg/FlyTime").Trim();
						string strAvOrgCityCode = npRet.FindTextByPath("//eg/OrgCityCode").Trim();
						string strAvDstCityCode = npRet.FindTextByPath("//eg/DstCityCode").Trim();
						Policy epAv = new Policy();
						strRet = epAv.getAv(strAvUserName,strAvAirCode,strAvFlyDate,strAvFlyTime,strAvOrgCityCode,strAvDstCityCode);

					}

					if(strCmm == "SentOrder")
					{// 
						
						string strOrderSent = npRet.FindTextByPath("//eg/OrderPara").Trim();
						Order iob = new Order();
						strRet = iob.Submit185Order(strOrderSent);
					}

					if(strCmm == "GetNewOrderMes")
					{// �õ���վ���µĶ�������
						Order iob2 = new Order();
						strRet = iob2.getNewOrderList();
					}

					if(strCmm == "LoginXyt")
					{
						string strUserName = npRet.FindTextByPath("//eg/UserName").Trim();
						string strPassWord = npRet.FindTextByPath("//eg/PassWord").Trim();
						strRet = mp.loginXyt (strUserName,strPassWord);
					}

					if(strCmm == "SaveCust")
					{//����һ���ͻ�
						Custer cu = new Custer();
						strRet = cu.SaveCust(npRet.GetXML());
					}

					if(strCmm == "UpdateCust")
					{//�޸Ŀͻ�����
						Custer cu = new Custer();
						strRet = cu.UpdateCust(npRet.GetXML());
					}

					if(strCmm == "CustAirFee")
					{//�����ͻ����Ѽ�¼
						Custer cu = new Custer();
						strRet = cu.NewCustAirFee(npRet.GetXML());
					}

					if(strCmm == "GetCustInfoByMobile")
					{//�õ��ͻ�����
						Custer cu = new Custer();
						strRet = cu.getCustInfoByMobile(npRet.FindTextByPath("//eg/MobileNo").Trim());
					}

					if(strCmm == "NewCustCallRec")
					{//�����ͻ�����
						Custer cu = new Custer();
						strRet = cu.NewCustCallRec(npRet.GetXML());
					}

					if(strCmm == "GetCustLastCalls")
					{//�õ��ͻ����10������
						Custer cu = new Custer();
						strRet = cu.GetCustLastCalls(npRet.FindTextByPath("//eg/numCustId").Trim());
					}

					//��ӡ,�ѻ���ӡ,�г̵��ŵļ�¼ added by chenqj
					if(strCmm == "ReceiptNumberLog")
					{
						NewPara np = new NewPara();
						np.AddPara("cm","RetReceiptNumberLog");

						try
						{
							string username = npRet.FindTextByPath("//eg/username").Trim();
							string pnr = npRet.FindTextByPath("//eg/pnr").Trim();
							string receiptNumber = npRet.FindTextByPath("//eg/receiptNumber").Trim();
							string isOffline = npRet.FindTextByPath("//eg/isOffline").Trim();

							eTicket.LogIt(username, pnr, receiptNumber,isOffline);
							np.AddPara("err", string.Empty);
						}
						catch(Exception ee)
						{
							np.AddPara("err", ee.Message);
						}

						strRet = np.GetXML();
					}

                    if (strCmm == "Check443")
                    {
                        string strKey = npRet.FindTextByPath("//eg/Key").Trim();
                        NewPara np443 = new NewPara();
                        np443.AddPara("cm", "RetCheck443");
                        if (strKey == "eg66aeg66a")
                        {
                            np443.AddPara("Flag", "true");
                        }
                        else 
                        {
                            np443.AddPara("Flag", "false");
                        }

                        strRet = np443.GetXML();
                    }
                    //����ǲ�ѯK����Ϣ
                    if (strCmm == "GetKBunkInfo")
                    {
                        string strFromTo = npRet.FindTextByPath("//eg/fromto").Trim();
                        string strDate = npRet.FindTextByPath("//eg/date").Trim();
                        strRet = mp.GetKBunkInfo(strFromTo, strDate);
                    }
                    //������ύK������
                    if (strCmm == "ApplyKBunkInfo")
                    {
                        //string strFromTo = npRet.FindTextByPath("//eg/fromto").Trim();
                        XmlNode xn = npRet.FindNodeByPath("//eg/applyinfo");
                        strRet = mp.ApplyKBunkApplication(xn);
                    }
                    if (strCmm == "DisplayKBunkInfo")
                    {
                        //string strFromTo = npRet.FindTextByPath("//eg/fromto").Trim();
                        ///XmlNode xn = npRet.FindNodeByPath("//eg/applyinfo");
                        string orderid = npRet.FindTextByPath("//eg/orderid").Trim();
                        strRet = mp.DisplayKBunkInfo(orderid);
                    }
                    if (strCmm == "ProcessKOrder")
                    {
                        //string strFromTo = npRet.FindTextByPath("//eg/fromto").Trim();
                        ///XmlNode xn = npRet.FindNodeByPath("//eg/applyinfo");
                        strRet = mp.ProcessKOrder(npRet);
                    }
                    if (strCmm == "IsDecFee")
                    {
                        gs.util.func.Write("pnr:" + npRet.FindTextByPath("//eg/PNR").Trim() + " cm:" + strCmm);
                        strRet = mp.IsDecFee(npRet);
                    }
                    //cityTicketInfo   wsl add
                    //�õ����еĳ���
                    if (strCmm == "GetAllPolicyCity")
                    {
                        CityTicketInfo cti = new CityTicketInfo();
                        strRet = cti.getAllPolicyCity();
                    }
                    //����Ʊ����Ϣ
                    if (strCmm == "UpdateTicketInfo")
                    {
                        string bfrom = npRet.FindTextByPath("//eg/bfrom").Trim();
                        string eto = npRet.FindTextByPath("//eg/eto").Trim();
                        string bunkf = npRet.FindTextByPath("//eg/bunkf").Trim();
                        string bunky = npRet.FindTextByPath("//eg/bunky").Trim();
                        CityTicketInfo cti = new CityTicketInfo();
                        strRet = cti.updateTicketInfo(bfrom, eto, bunkf, bunky);
                    }
                    //���Ʊ����Ϣ
                    if (strCmm == "InsertTicketInfo")
                    {
                        string bfrom = npRet.FindTextByPath("//eg/bfrom").Trim();
                        string eto = npRet.FindTextByPath("//eg/eto").Trim();
                        string bunkf = npRet.FindTextByPath("//eg/bunkf").Trim();
                        string bunky = npRet.FindTextByPath("//eg/bunky").Trim();
                        CityTicketInfo cti = new CityTicketInfo();
                        strRet = cti.insertTicketInfo(bfrom, eto, bunkf, bunky);
                    }
                    //�õ�Ʊ����Ϣ
                    if (strCmm == "GetTicketInfo")
                    {
                        string bfrom = npRet.FindTextByPath("//eg/bfrom").Trim();
                        string eto = npRet.FindTextByPath("//eg/eto").Trim();
                        CityTicketInfo cti = new CityTicketInfo();
                        strRet = cti.getTicketInfo(bfrom, eto);
                    }
                    if (strCmm == "GetEticketExcel")
                    {
                        string strUser = npRet.FindTextByPath("//eg/user").Trim();
                        string stardate = npRet.FindTextByPath("//eg/startdate").Trim();
                        string enddate = npRet.FindTextByPath("//eg/enddate").Trim();
                        strRet = mp.GetEticketReport(strUser,stardate,enddate);
                    }
                    //�õ�������ip��ַ��˿ں� wsl add
                    if (strCmm == "setIPID")
                    {
                        string ipid = npRet.FindTextByPath("//eg/ipid").Trim();
                        strRet = mp.setIPID(ipid);
                    }
                    //�õ����еķ�����ip��ַ��˿ں� wsl add
                    if (strCmm == "getIPID")
                    {                       
                        strRet = mp.getIPID();
                    }
					#endregion
					
				}
				else
				{//�������Ϊ��,˵���������
					NewPara np = new NewPara();
					np.AddPara("cm","CmNotFound");//������Ϣ˵����������û�ҵ���ָ������Ϊ��
					strRet = np.GetXML();
				}
			}
			catch(Exception ex)
			{
				gs.util.func.Write("requestXML=[" + p_str + "] exception=[" + ex.Message + "]");
				NewPara np = new NewPara();
				np.AddPara("cm","CmErr");//������Ϣ˵���������������
				np.AddPara("err",ex.Message);
				strRet = np.GetXML();
			}
			//logic.GlobalFunc.iGlobalUserNum--;
			return strRet;
		}
	}
}
