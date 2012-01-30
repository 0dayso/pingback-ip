using System;
using System.Data;
using System.Xml;
using gs.para;
using gs.DataBase;

namespace logic
{
	/// <summary>
	/// Insurance 的摘要说明。
	/// </summary>
	public class Insurance
	{
		public Insurance()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		
		public bool SaveIns(string strUserID,string streNumber,string strIssueNumber,
			string strNameIssued,string strCardType,string strCardNumber,
			string strRemark,string strIssuePeriod,string strIssueBegin,
			string strIssueEnd,string strSolutionDisputed,string strNameBeneficiary,
			string strSignature,string strSignDate,string strInssuerName,string strPnr)
		{
			Conn cn = null;
			bool bRet = false;
			try
			{
				cn = new Conn();

				Top2 tp = new Top2(cn); 
				
				string strCurTime = System.DateTime.Now.ToString();
				tp.AddRow("UserID","c",strUserID);
				tp.AddRow("eNumber","c",streNumber);
				tp.AddRow("IssueNumber","c",strIssueNumber);

				tp.AddRow("NameIssued","c",strNameIssued);
				tp.AddRow("CardType","c",strCardType);
				tp.AddRow("CardNumber","c",strCardNumber);

				tp.AddRow("Remark","c",strRemark);
				tp.AddRow("IssuePeriod","c",strIssuePeriod);
				tp.AddRow("IssueBegin","dt",strIssueBegin);

				tp.AddRow("IssueEnd","dt",strIssueEnd);
				tp.AddRow("SolutionDisputed","c",strSolutionDisputed);
				tp.AddRow("NameBeneficiary","c",strNameBeneficiary);

				tp.AddRow("Signature","c",strSignature);
				tp.AddRow("SignDate","dt",strSignDate);
				tp.AddRow("InssuerName","c",strInssuerName);
				tp.AddRow("Pnr","c",strPnr);
											
				if(tp.AddNewRec("eg_insurance"))
				{
					bRet = true;
				}

			}
			catch(Exception ex)
			{
				gs.util.func.Write("SaveIns is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}

		
		public string GetUncheckedPNR(string vcLoginName )
		{ 
			string strPnr = "";
			string strSql = "";
			
			Conn cn = null;
			try
			{
				cn = new Conn();

				strSql = "SELECT * FROM eg_user where vcLoginName='"+vcLoginName+"'";
				DataSet dsUser = cn.GetDataSet(strSql);
				if (dsUser.Tables[0].Rows.Count != 0)
				{
					string strSqlPnr = "";
					if (dsUser.Tables[0].Rows[0]["numRoleId"].ToString() == "0")
					{
						strSqlPnr = "SELECT * FROM eg_eticket where userid ='"+vcLoginName+"' and (etNumber is null or ltrim(rtrim(etNumber))='')";
					}
					else if (dsUser.Tables[0].Rows[0]["numRoleId"].ToString().Trim() == "1")
					{
						//strSql = "SELECT * FROM eg_eticket where userID IN  " + 
						//	"(SELECT vcLoginName FROM eg_user WHERE   numAgentId IN (SELECT numAgentId FROM eg_user WHERE vcLoginName = '" + vcLoginName + "')) and DecFeeState='0' ";
						strSqlPnr = "SELECT eg_eticket.* FROM dbo.eg_eticket INNER JOIN  dbo.eg_user ON dbo.eg_eticket.UserID = dbo.eg_user.vcLoginName where eg_user.numAgentId like (select ltrim(rtrim(numAgentId)) from eg_user where vcLoginName='" + vcLoginName.Trim() + "') + '%' and (eg_eticket.etNumber is null or ltrim(rtrim(eg_eticket.etNumber))='')";
				
					}
					
					DataSet ds = cn.GetDataSet(strSqlPnr); 

					if (ds.Tables[0].Rows.Count > 0)
						strPnr = ds.Tables[0].Rows[0]["Pnr"].ToString().Trim();

				}
			}
			catch(Exception ex)
			{
				gs.util.func.Write("GetUncheckedPNR is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return strPnr;
		}

		/// <summary>
		/// 判断保单范围
		/// </summary>
		/// <param name="vcLoginName"></param>
		/// <returns></returns>
		public bool GetIncBound(string type,string IssueNumber)
		{ 
			bool bState = true;
			
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select * from eg_insurance where Remark='" + type + "' and IssueNumber='" + IssueNumber + "'";
//				string strSql = "select * from eg_IncBound where vcLoginName='" + vcLoginName + "'";
				DataSet ds = cn.GetDataSet(strSql);

				if (ds.Tables[0].Rows.Count != 0)
				{
					bState = false;
					
				}

//				if (ds.Tables[0].Rows.Count != 0)
//				{
//					Int64 issueNum = Int64.Parse(IssueNumber);
//
//					foreach (DataRow dr in ds.Tables[0].Rows)
//					{
//						Int64 bInc= Int64.Parse(dr["begInsurance"].ToString());
//						Int64 eInc= Int64.Parse(dr["endInsurance"].ToString());						
//
//						if (issueNum >= bInc && issueNum <= eInc)
//						{
//							bState = true;
//							break;
//						}
//					}
//				}
				
			}
			catch(Exception ex)
			{
				gs.util.func.Write("GetIncBound is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bState;
		}



		public bool DelETicket(string strPnr)
		{
			Conn cn = null;
			bool bRet = false;
			try
			{
				cn = new Conn();
				cn.Update("delete from eg_eticket where rtrim(ltrim(Pnr))='" + strPnr.Trim() + "' and datediff(day,OperateTime,getdate())>=-10 and  datediff(day,OperateTime,getdate())<=10 and (DecFeeState<>'1' or DecFeeState is null)");
				bRet = true;
			}
			catch(Exception ex)
			{
				gs.util.func.Write("DelETicket is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bRet;

		}

		public bool SaveETicket(string strUserID,string strPnr,string stretNumber,string 
			strFlightNumber1,string strBunk1,string strCityPair1,string 
			strDate1,string strFlightNumber2,string strBunk2,string 
			strCityPair2,string strDate2,string strTotalFC,string strDecFeeState,string strKey,string strIpId,string strBasePrc,string strOil,string strPassenger)
		{
			Conn cn = null;
			bool bRet = false;
			try
			{
				cn = new Conn();

				Top2 tp = new Top2(cn); 
				
				string strCurTime = System.DateTime.Now.ToString();

				if (strUserID != null)
					tp.AddRow("UserID","c",strUserID);
				if (strPnr != null)
					tp.AddRow("Pnr","c",strPnr);
				if (stretNumber != null)
					tp.AddRow("etNumber","c",stretNumber);
				if (strFlightNumber1 != null)
					tp.AddRow("FlightNumber1","c",strFlightNumber1);
				if (strBunk1 != null)
					tp.AddRow("Bunk1","c",strBunk1);
				if (strCityPair1 != null)
					tp.AddRow("CityPair1","c",strCityPair1);
				if (strDate1 != null)
					tp.AddRow("Date1","c",strDate1);
				if (strFlightNumber2 != null)
					tp.AddRow("FlightNumber2","c",strFlightNumber2);
				if (strBunk2 != null)
					tp.AddRow("Bunk2","c",strBunk2);
				if (strCityPair2 != null)
					tp.AddRow("CityPair2","c",strCityPair2);
				if (strDate2 != null)
					tp.AddRow("Date2","c",strDate2);
				
				//if (strDecFeeState != null)
				//	tp.AddRow("DecFeeState","c",strDecFeeState);

				if (strIpId != null && strIpId.Trim() != "")
					tp.AddRow("IpId","c",strIpId);//现在已经变成了OFFICE号

				//gs.util.func.Write("the strIpId=" + strIpId);

				if (strBasePrc != null && strBasePrc.Trim() != "")
					tp.AddRow("numBasePrc","d",strBasePrc);

				if (strOil != null && strOil.Trim() != "")
					tp.AddRow("numOilPrc","d",strOil);

				if (strPassenger != null && strPassenger.Trim() != "")
					tp.AddRow("Passenger","c",strPassenger);


				if (strKey == "RetPreSubmitETicket")
				{
					string ticketID = "";
					Insurance ins = new Insurance();
				
					

					string strReIns = "select * from eg_eticket where rtrim(ltrim(Pnr))='" + strPnr.Trim() + "' and datediff(day,OperateTime,getdate())<=5";
					DataSet dsReIns = cn.GetDataSet(strReIns);
					if(dsReIns.Tables[0].Rows.Count == 0)
					{
						tp.AddRow("DecFeeState","c","0");
						
						if(strTotalFC != null && strTotalFC.Trim() != "")
							tp.AddRow("TotalFC","c",strTotalFC);
						

						if(tp.AddNewRec("eg_eticket"))
						{
							bRet = true;
						}
					}
					else
					{
						ticketID = dsReIns.Tables[0].Rows[0]["TicketID"].ToString().Trim();
						string strNowFC = "";
						if(dsReIns.Tables[0].Rows[0]["TotalFC"] != null)
							strNowFC = dsReIns.Tables[0].Rows[0]["TotalFC"].ToString().Trim();
						double dbYe = 0;
						if(strNowFC != null && strNowFC.Trim() != "")
							dbYe = Double.Parse(strNowFC);

						if(strNowFC.Trim() == "" || dbYe == 0)
						{
							if(strTotalFC != null && strTotalFC.Trim() != "")
								tp.AddRow("TotalFC","c",strTotalFC);
							else
								tp.AddRow("TotalFC","c","0");
						}


						if(tp.Update("eg_eticket","TicketID",ticketID))
						{
							bRet = true;
						}
						
						gs.util.func.Write("SaveETicket is err 该电脑号" + strPnr + "已经存在,不允许重复新增");
					}
					
					/*ticketID = ins.GetEticketByUPD(strUserID,strPnr,strDecFeeState);
					if (ticketID == "")
					{				
						if(tp.AddNewRec("eg_eticket"))
						{
							bRet = true;
						}
					}
					else
					{
						if(tp.Update("eg_eticket","TicketID",ticketID))
						{
							bRet = true;
						}
					}*/
				}
				else if (strKey == "RetSubmitETicket")
				{
					string strReIns = "select * from eg_eticket where rtrim(ltrim(Pnr))='" + strPnr.Trim() + "' and datediff(day,OperateTime,getdate())<=5";
					DataSet dsReIns = cn.GetDataSet(strReIns);
					if(dsReIns.Tables[0].Rows.Count > 0)
					{
						string strNowFC = "";
						if(dsReIns.Tables[0].Rows[0]["TotalFC"] != null)
							strNowFC = dsReIns.Tables[0].Rows[0]["TotalFC"].ToString().Trim();

						double dbYe = 0;
						if(strNowFC.Trim() != "")
							dbYe = Double.Parse(strNowFC);

						if(strNowFC.Trim() == "" || dbYe == 0)
							tp.AddRow("TotalFC","c",strTotalFC);
					}

					if (tp.Update("eg_eticket"," rtrim(ltrim(Pnr))='" + strPnr + "' and datediff(day,OperateTime,getdate())<=5" ))
					{
						bRet = true;
					}
					else
					{
						gs.util.func.Write("SaveETicket is err 已经超过过票时限");
					}
				}


			}
			catch(Exception ex)
			{
				bRet = false;
				gs.util.func.Write("SaveETicket is err" + ex.Message);
				throw ex;
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}

		/*public string GetEticketByUPD(string strUserID,string strPnr,string strDecFeeState)
		{ 
			string strID = "";
			
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "SELECT * FROM eg_eticket where UserID= '" + strUserID + "' and Pnr= '" + strPnr + "' and DecFeeState = '" + strDecFeeState + "'"; 
					
				DataSet ds = cn.GetDataSet(strSql); 

				if (ds.Tables[0].Rows.Count > 0)
					strID = ds.Tables[0].Rows[0]["TicketID"].ToString();
	
	
			}
			catch(Exception ex)
			{
				gs.util.func.Write("GetEticketByUPD" + ex.Message);
				throw ex;
			}
			finally
			{
				cn.close();
			}
			return strID;
		}*/

		/// <summary>
		/// 设置保险的状态为作废
		/// </summary>
		/// <param name="p_strIncNo">保单号</param>
		/// <param name="p_strIncCmp">保险种类</param>
		/// <param name="p_strUserName">用户名</param>
		/// <returns></returns>
		public bool setIncIsCancel(string p_strIncNo,string p_strIncCmp,string p_strUserName)
		{ 
			bool bRet = false;
			Conn cn = null;
			try
			{
				cn = new Conn();
				//DataSet ds = cn.GetDataSet("select * from eg_insurance where ltrim(rtrim(userid))='" + p_strUserName.Trim() + "' and IssueNumber='" + p_strIncNo.Trim() + "' and ltrim(rtrim(remark))='" + p_strIncCmp + "' and IssueBegin>getdate()");
				//gs.util.func.Write("setIncIsCancel is err" + "select * from eg_insurance where ltrim(rtrim(userid))='" + p_strUserName.Trim() + "' and IssueNumber='" + p_strIncNo.Trim() + "' and ltrim(rtrim(remark))='" + p_strIncCmp + "' and IssueBegin>getdate()");
				//if(ds.Tables[0].Rows.Count > 0)
				//{
					//gs.util.func.Write("kao");
					string strSql = "update eg_insurance set isCancel='1' where userid='" + p_strUserName.Trim() + "' and IssueNumber='" + p_strIncNo.Trim() + "' and ltrim(rtrim(remark))='" + p_strIncCmp + "' and IssueBegin>getdate()" ;
					//gs.util.func.Write("kao2");
					cn.Update(strSql);
					//gs.util.func.Write("kao3" + strSql);
					bRet = true;
				//}
			}
			catch(Exception ex)
			{
				gs.util.func.Write("setIncIsCancel is err" + ex.Message);
				throw ex;
			}
			finally
			{
				cn.close();
			}
			return bRet;

		}
	}
}
