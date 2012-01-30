using System;
using System.Data;
using System.Xml;

using gs.DataBase;
using gs.para;

namespace logic
{
	/// <summary>
	/// PnrTicket 的摘要说明。
	/// </summary>
	public class PnrTicket
	{
		public PnrTicket()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 客户端生成了一张票据信息,对于傻瓜版
		/// </summary>
		/// <param name="p_strXML"></param> 具体说明见代码末尾
		/// <returns></returns>
		public string NewTickte(string p_strXML)
		{
			Conn cn = null;
			string strRet = "";
			string strCustInfo = "";
			string strOper = "";
			try
			{
				string strPnrId = logic.mytool.util.getId("eg_pnr");
				NewPara npRet = new NewPara(p_strXML.Trim());
				string strUserCode = npRet.FindTextByPath("//eg/User").Trim();

				cn = new Conn();
				cn.beginTrans();
				Top2 tp = new Top2(cn); 
				
				string strBz = npRet.FindTextByPath("//eg/Bz").Trim();
				strBz = gs.util.StringTool.getSqlFilterStr(strBz);

				tp.AddRow("numPnrId","c",strPnrId);
				tp.AddRow("vcPnrNo","c",npRet.FindTextByPath("//eg/PNR").Trim());
				tp.AddRow("vcLoginName","c",gs.util.StringTool.getSqlFilterStr(npRet.FindTextByPath("//eg/User").Trim()));
				strOper = gs.util.StringTool.getSqlFilterStr(npRet.FindTextByPath("//eg/User").Trim());
				tp.AddRow("vcPhone","c",gs.util.StringTool.getSqlFilterStr(npRet.FindTextByPath("//eg/Phone").Trim()));
				tp.AddRow("numPersonCount","i",npRet.FindTextByPath("//eg/PersonCount").Trim());
				strCustInfo = gs.util.StringTool.getSqlFilterStr(npRet.FindTextByPath("//eg/Names").Trim());
				tp.AddRow("vcNames","c",strCustInfo);
				tp.AddRow("vcTL","c",npRet.FindTextByPath("//eg/TL").Trim());

				tp.AddRow("numTkPrc","d",npRet.FindTextByPath("//eg/numTkPrc").Trim()); //票面价
				tp.AddRow("numRealPrc","d",npRet.FindTextByPath("//eg/numRealPrc").Trim()); //实收价
				tp.AddRow("numBasePrc","d",npRet.FindTextByPath("//eg/numBasePrc").Trim()); //基建
				tp.AddRow("numOilPrc","d",npRet.FindTextByPath("//eg/numOilPrc").Trim()); //燃油
				tp.AddRow("numPoint","d",npRet.FindTextByPath("//eg/numPoint").Trim()); //返点
				tp.AddRow("numGain","d",npRet.FindTextByPath("//eg/numGain").Trim()); //利润
				tp.AddRow("numTotal","d",npRet.FindTextByPath("//eg/numTotal").Trim()); //合计
				
				tp.AddRow("numStat","i","0");
				//tp.AddRow("dtApply","dt",System.DateTime.Now.ToLongTimeString());
				tp.AddRow("dtApply","t","getdate()");
				tp.AddRow("vcBz","c",strBz);
				
							
				if(tp.AddNewRec("eg_pnr"))
				{
					XmlNode nds = npRet.FindNodeByPath("//eg/ATK");
					for(int i=0;i<nds.ChildNodes.Count;i++)
					{
						XmlNode nodeFlight = nds.ChildNodes[i].ChildNodes[0];
						string strFlightNo = nodeFlight.ChildNodes[0].Value;

						XmlNode nodeBunk = nds.ChildNodes[i].ChildNodes[1];
						string strBunk = nodeBunk.ChildNodes[0].Value;

						XmlNode nodeDate = nds.ChildNodes[i].ChildNodes[2];
						string strDate = nodeDate.ChildNodes[0].Value;

						XmlNode nodeCityPairt = nds.ChildNodes[i].ChildNodes[3];
						string strCityPair = nodeCityPairt.ChildNodes[0].Value;

						string strIns = "insert into eg_pnrtks(numPnrId,vcFlightNo,vcBunk,vcDate,vcCityPair) values(" + strPnrId + 
							",'" + strFlightNo + "','" + strBunk + "','" + strDate + "','" + strCityPair + "')";
						cn.Update(strIns);
					}
					strRet = getOnlineManager(cn,strUserCode,npRet.FindTextByPath("//eg/PNR").Trim());
				}
				cn.commit();
			}
			catch(Exception ex)
			{
				cn.rollback();
				gs.util.func.Write("NewTickte is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}

			/*try
			{
				cn = new Conn();
				saveCustInfo(cn,strOper,strCustInfo);//保存客户资料
			}
			catch(Exception ex)
			{
				gs.util.func.Write("NewTickte saveCustInfo is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}*/

			return strRet;
		}

		private void saveCustInfo(Conn cn,string p_strOper,string p_strCustInfo)
		{
			try
			{
				string strRet = "";
				if(p_strCustInfo.Trim() != "")
				{
					strRet = p_strCustInfo.Trim();
				
					if(strRet.IndexOf(";")>1)
					{
						string[] aryCusts = strRet.Split(';');
						
						for(int i=0;i<aryCusts.Length;i++)
						{
							string strACust = aryCusts[i];
							if(strACust.IndexOf(";")>1)
							{
								string[] ary = strACust.Split('-');
								if(ary.Length > 2)
								{
									NewPara npCust = new NewPara();

									npCust.AddPara("cm","SaveCust");//操作类型说明
									npCust.AddPara("vcInpEgUser",p_strOper);
									npCust.AddPara("vcIdentCard",ary[1]);
									npCust.AddPara("vcCustName",ary[0]);
									npCust.AddPara("vcMobile",ary[2]);
									npCust.AddPara("numMemSrc","2");
						
									Custer cu = new Custer();
									cu.SaveCust(cn,npCust.GetXML());
			
								}
							}
						}
					}
					else
					{
						if(strRet.IndexOf(";")>1)
						{
							string[] ary = strRet.Split('-');
							if(ary.Length > 2)
							{
								NewPara npCust = new NewPara();

								npCust.AddPara("cm","SaveCust");//操作类型说明
								npCust.AddPara("vcInpEgUser",p_strOper);
								npCust.AddPara("vcIdentCard",ary[1]);
								npCust.AddPara("vcCustName",ary[0]);
								npCust.AddPara("vcMobile",ary[2]);
								npCust.AddPara("numMemSrc","2");
						
								Custer cu = new Custer();
								string strRetCust = cu.SaveCust(cn,npCust.GetXML());
			
								//NewPara npRet = new NewPara(strRetCust);
								//string strCustId = npRet.FindTextByPath("//eg/CustId");


							}
						}
					}
				}
			}
			catch(Exception ex)
			{
				gs.util.func.Write("PnrTicket.saveCustInfo is err" + ex.Message);
			}
		}

		/// <summary>
		/// 检查当前用户的所有在线上线管理员对应的Passport
		/// </summary>
		/// <param name="p_cn"></param>
		/// <param name="strUserName"></param>
		/// <param name="p_strPnr"></param>
		/// <returns></returns>
		private string getOnlineManager(Conn p_cn,string strUserName,string p_strPnr)
		{
			string strRet = "";
			string strSql = "";
			try
			{
				strSql = "select numAgentId from eg_user where vcLoginName='" + strUserName.Trim() + "'";
				DataSet ds = p_cn.GetDataSet(strSql);
				string strAgentId = ds.Tables[0].Rows[0]["numAgentId"].ToString().Trim();
				ds.Clear();

				string strRootAgentId = strAgentId;
				if(strAgentId.Length > 8)
					strRootAgentId = strAgentId.Substring(0,8);

				string strSqlAgent = "";
				int iBegin = strRootAgentId.Length,iEnd = strAgentId.Length;
				for(int i=iBegin;i<=iEnd;i+=4)
				{
					string strTmpAgentId = strAgentId.Substring(0,i);
					strSqlAgent = strSqlAgent + " ltrim(rtrim(eg_user.numAgentId))='" + strTmpAgentId + "' or";
				}
				strSqlAgent = strSqlAgent.Substring(0,strSqlAgent.Length-3);
                
				//strSql = "select eg_onlineUser.vcPassPort from dbo.eg_user INNER JOIN dbo.eg_onlineUser ON dbo.eg_user.vcLoginName = dbo.eg_onlineUser.vcLoginName where ltrim(rtrim(eg_user.numAgentId)) like '" + strRootAgentId + "%' and len(ltrim(rtrim(eg_user.numAgentId)))<=len('" + strAgentId + "') and eg_user.numRoleId=1";
				strSql = "select eg_onlineUser.vcPassPort from dbo.eg_user INNER JOIN dbo.eg_onlineUser ON dbo.eg_user.vcLoginName = dbo.eg_onlineUser.vcLoginName where eg_user.numRoleId=1 and (" + strSqlAgent + ")";

				ds = p_cn.GetDataSet(strSql);
				for(int iTmp=0;iTmp<ds.Tables[0].Rows.Count;iTmp++)
				{
					strRet = strRet + "~" + ds.Tables[0].Rows[iTmp]["vcPassPort"].ToString().Trim();
				}
			}
			catch(Exception ex)
			{
				gs.util.func.Write("getOnlineManager is err" + ex.Message + strSql);
				throw ex;
			}
			return strRet;
		}

		//查询相应状态的PNR
		public DataSet GetPnrs(string strUserID,string strPnrState)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "SELECT vcPnrNo FROM dbo.eg_pnr where numStat=" + strPnrState + " and vcLoginName='" + strUserID +"'";
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("getAgentUsers is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}

		public DataSet GetPassengerOfTicket(string strPassenger)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "SELECT * FROM dbo.eg_eticket where Passenger like '%" + strPassenger + "-%'";
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("getAgentUsers is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}
		public DataSet GetPassengerOfPnr(string strPassenger)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try	
			{
				cn = new Conn();
				string strSql = "SELECT * FROM dbo.eg_pnr where vcNames like '%" + strPassenger + "-%'";
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("getAgentUsers is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}

		/// <summary>
		/// 请求将PNR状态设为删除状态
		/// </summary>
		/// <param name="strPNR"></param>
		/// <returns></returns>
		public bool SetPNRStateDel(string strPNR)
		{
			Conn cn = null;
			bool bRet = false;
			try
			{
				cn = new Conn();
				string strUpdate = "update eg_pnr set numstat=3 where rtrim(ltrim(vcPnrNo))='" + strPNR.Trim() + "' and datediff(day,dtApply,getdate())>=-10 and  datediff(day,dtApply,getdate())<=10";
				//string strSql = "update eg_pnr set numstat=3 where vcPnrNo in (" + strPNR + ")";
				bRet = cn.Update(strUpdate);

			}
			catch(Exception ex)
			{
				gs.util.func.Write("SetPNRStateDel is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}

		/// <summary>
		/// 票价的查找
		/// </summary>
		/// <param name="strFROM"></param>
		/// <param name="strTO"></param>
		/// <returns></returns>
		public DataSet GetDsFC(string strFROM,string strTO)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "SELECT * FROM eg_ticketInfo where BFROM='" + strFROM.Trim() + "' and ETO='" + strTO.Trim() +"'";
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("GetDsFC is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}
		/// <summary>
		///  检查行程单号是否属于User
		/// </summary>
		/// <param name="vcLoginName"></param>
		/// <param name="IssueNumber"></param>
		/// <returns></returns>
		public bool GetEtkBound(string vcLoginName,string IssueNumber,string strCfgNumber)
		{ 
			bool bState = false;
			
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select * from eg_eticketBound  where userid='" + vcLoginName + "'";
				DataSet ds = cn.GetDataSet(strSql);

				if (ds.Tables[0].Rows.Count != 0)
				{
					Int64 issueNum = Int64.Parse(IssueNumber);

					foreach (DataRow dr in ds.Tables[0].Rows)
					{
						Int64 bEtk= Int64.Parse(dr["begNumber"].ToString());
						Int64 eEtk= Int64.Parse(dr["endNumber"].ToString());						

						if (issueNum >= bEtk && issueNum <= eEtk)
						{
							bState = true;
							break;
						}
					}
				}
				
			}
			catch(Exception ex)
			{
				gs.util.func.Write(" err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bState;
		}

		
		/// <summary>
		/// 检查行程单号是否属于OFFICE号
		/// </summary>
		/// <param name="vcLoginName"></param>
		/// <param name="IssueNumber"></param>
		/// <param name="strCfgNumber"></param>
		/// <returns></returns>
		public bool GetOfficeBound(string strRecieptNumber,string strCfgNumber,string strUserID)
		{ 
			bool bState = false;
			string strSql= "";
			string agentID = "0";
			Conn cn = null;
			try
			{
				cn = new Conn();
				strSql = "select * from eg_user where vcLoginName='" + strUserID  + "'";
				DataSet dsUser = cn.GetDataSet(strSql);
				
				if (dsUser.Tables[0].Rows.Count > 0 )
				{
					agentID = dsUser.Tables[0].Rows[0]["numAgentId"].ToString();
				}

				strSql = "select a.BegNumber,a.EndNumber from eg_OfficeNumber as a join eg_agentips as b on a.OfficeCode = b.OfficeCode  where a.OfficeCode='" + strCfgNumber + "' and b.numAgentId='" + agentID+"'";
				DataSet ds = cn.GetDataSet(strSql);

				if (ds.Tables[0].Rows.Count != 0)
				{
					Int64 issueNum = Int64.Parse(strRecieptNumber);

					foreach (DataRow dr in ds.Tables[0].Rows)
					{
						Int64 bEtk= Int64.Parse(dr["BegNumber"].ToString());
						Int64 eEtk= Int64.Parse(dr["EndNumber"].ToString());						

						if (issueNum >= bEtk && issueNum <= eEtk)
						{
							bState = true;
							break;
						}
					}
				}
				
			}
			catch(Exception ex)
			{
				gs.util.func.Write(" err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bState;
		}

		/// <summary>
		/// 检查电子客票号是否属于OFFICE号
		/// </summary>
		/// <param name="vcLoginName"></param>
		/// <param name="IssueNumber"></param>
		/// <param name="strCfgNumber"></param>
		/// <returns></returns>
		public bool GetRptBound(string strETNumber,string strCfgNumber,string strUserID)
		{ 
			
			bool bState = false;
			string strSql= "";
			string agentID = "0";
			Conn cn = null;
			try
			{
				cn = new Conn();
				strSql = "select * from eg_user where vcLoginName='" + strUserID  + "'";
				DataSet dsUser = cn.GetDataSet(strSql);
				
				if (dsUser.Tables[0].Rows.Count > 0 )
				{
					agentID = dsUser.Tables[0].Rows[0]["numAgentId"].ToString();
				}

				strSql = "select a.BegNumber,a.EndNumber from eg_OfficeEticket as a join eg_agentips as b on a.OfficeCode = b.OfficeCode  where a.OfficeCode='" + strCfgNumber + "' and b.numAgentId='" + agentID+"'";
				DataSet ds = cn.GetDataSet(strSql);
				
				if (ds.Tables[0].Rows.Count != 0)
				{
					Int64 issueNum = Int64.Parse(strETNumber);

					foreach (DataRow dr in ds.Tables[0].Rows)
					{
						Int64 bEtk= Int64.Parse(dr["BegNumber"].ToString());
						Int64 eEtk= Int64.Parse(dr["EndNumber"].ToString());						

						if (issueNum >= bEtk && issueNum <= eEtk)
						{
							bState = true;
							break;
						}
					}
				}
				
			}
			catch(Exception ex)
			{
				gs.util.func.Write(" err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bState;
		}

		public bool SaveFC(string strFROM,string strTO,string strBUNKF,string strBUNKC,string strBUNKY)
		{
			Conn cn = null;
			bool bRet = false;
			try
			{
				cn = new Conn();

				Top2 tp = new Top2(cn); 

				DataSet ds = cn.GetDataSet("select * from eg_ticketInfo where bfrom='" + strFROM.Trim() + "' and ETO='" + strTO.Trim() + "'");
								
				tp.AddRow("BFROM","c",strFROM);
				tp.AddRow("ETO","c",strTO);
				tp.AddRow("BUNKF","c",strBUNKF);
				tp.AddRow("BUNKC","c",strBUNKC);
				tp.AddRow("BUNKY","c",strBUNKY);			 
								
				if(ds.Tables[0].Rows.Count > 0)
				{
					if(tp.AddNewRec("eg_ticketInfo"))
					{
						bRet = true;
					}

				}
				else
				{
					if(tp.Update("eg_ticketInfo"," bfrom='" + strFROM.Trim() + "' and ETO='" + strTO.Trim() + "'"))
					{
						bRet = true;
					}
				}


			}
			catch(Exception ex)
			{
				gs.util.func.Write("SaveFC is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}


		public bool savePnrState(string strUser,string strPNR,string strState)
		{
			Conn cn = null;
			bool bRet = false;
			try
			{
				cn = new Conn();
				string strRunSql = string.Format("insert into eg_pnrState ([user],pnr,state) values('{0}','{1}','{2}')",strUser,strPNR,strState);
				
				string strSql = "";

				if (strState == "2")
				{
					strSql="select * from eg_pnrState where state=0 and pnr='"+ strPNR+"'";
					DataSet ds = cn.GetDataSet(strSql);
					
					if (ds.Tables[0].Rows.Count != 0 )
					{
						string user = ds.Tables[0].Rows[0]["user"].ToString();

						if (strUser == user||Right.getRole(cn,strUser) == 2)
						{
							bRet = cn.Update(strRunSql);
						}
						else
						{
							if (Right.getAgent(cn,user) == Right.getAgent(cn,strUser) && Right.getRole(cn,user) ==0 && Right.getRole(cn,strUser) == 1)
							{
								bRet = cn.Update(strRunSql);
							}						
						}
					}
					else
						bRet = cn.Update(strRunSql);
				}
				else
				{
					bRet = cn.Update(strRunSql);
				}
			}
			catch(Exception ex)
			{
				gs.util.func.Write("savePnrState is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}


		public bool SaveGroup(string strUserID,string strName,string strCardID,string strGroupTicketID)
		{
			Conn cn = null;
			bool bRet = false;
			try
			{
				cn = new Conn();

				Top2 tp = new Top2(cn); 
								
				tp.AddRow("UserID","c",strUserID);
				tp.AddRow("Name","c",strName);
				tp.AddRow("CardID","c",strCardID);
				tp.AddRow("GroupTicketID","c",strGroupTicketID);
						 

				string strSql = "select * from eg_ToGroup where id='"+strGroupTicketID+"'";
				DataSet dsGroup = cn.GetDataSet(strSql);

				if (dsGroup.Tables[0].Rows.Count!=0)
				{
					int totalNum = int.Parse(dsGroup.Tables[0].Rows[0]["total"].ToString());
					strSql = "select count(*) from eg_ToGroupContent where GroupTicketID = '" + strGroupTicketID +"'";
					DataSet dsGroupConten = cn.GetDataSet(strSql);
					int existNum = int.Parse(dsGroupConten.Tables[0].Rows[0][0].ToString());

					if (totalNum > existNum)
					{
						if(tp.AddNewRec("eg_ToGroupContent"))
						{
							bRet = true;
						}
					}
				}
			}
			catch(Exception ex)
			{
				gs.util.func.Write("SaveGroup is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}

		public DataSet GetGroupTicket(string strFromTo,string strDate,string strUserID)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string agentId = Right.getAgent(strUserID).ToString().Trim();
				int iLen = agentId.Length;
				/*if(iLen > 8)
					agentId = agentId.Substring(0,8);
				string strSql = "SELECT * FROM dbo.eg_ToGroup where RTrim(FromTo)='" + strFromTo + "' and RTrim([Date])='" + strDate +"' and RTrim(AGENTID) = '" + agentId+ "' AND (STATE=0 OR STATE IS NULL)";*/
				//string strSql = "SELECT * FROM dbo.eg_ToGroup where RTrim(FromTo)='" + strFromTo + "' and convert(varchar,convert(datetime,[Date]),111)=convert(varchar,convert(datetime,'" + strDate.Trim() + "'),111) and (STATE=0 OR STATE IS NULL)";
				string strSql = "SELECT * FROM dbo.eg_ToGroup where RTrim(FromTo)='" + strFromTo + "' and rtrim(ltrim([Date]))='" + strDate.Trim() + "' and (STATE=0 OR STATE IS NULL)";
//gs.util.func.Write("GetGroupTicket is err" + strSql);
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("GetGroupTicket is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}

		public DataSet GetGroupContent(string ID)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "SELECT * FROM dbo.eg_ToGroupContent where GroupTicketID='" + ID + "'";
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("getAgentUsers is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}

		
		public bool SaveScrollString(string strUserID,string strContext,string strBegTime,string strEndTime,string strNoticeType)
		{
			Conn cn = null;
			bool bRet = false;
			try
			{
				cn = new Conn();

				Top3 tp = new Top3(cn); 
								
				tp.AddRow("UserID","c",strUserID);
				tp.AddRow("Context","c",strContext);

				if (strBegTime == "")
					strBegTime = DateTime.Now.ToLongTimeString();

				tp.AddRow("BegTime","dt",strBegTime);
				tp.AddRow("NoticeType","c",strNoticeType);

				if (strEndTime == "")
					cn.Update("update eg_announce set EndTime=getdate() where EndTime is null");
				else
					tp.AddRow("EndTime","dt",strEndTime);

				if(tp.AddNewRec("eg_announce"))
				{
					bRet = true;
				}		 
			}
			catch(Exception ex)
			{
				gs.util.func.Write("SaveScrollString is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}

		
		public DataSet GetScrollString(string strUserID,string strNoticeType)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				//RTrim(UserID)='" + strUserID + "' and
				string strSql = "SELECT * FROM dbo.eg_announce where  " +
				" RTrim(UserID) in (select vcLoginName from eg_user where numagentid like (select substring(ltrim(rtrim(numagentid)),1,8)+'%' from eg_user where RTrim(vcLoginName)='"+strUserID+"'))"
					+ " and EndTime Is Not Null and ((GETDATE() >= CONVERT(DateTime, BegTime)) AND (GETDATE() < CONVERT(DateTime, EndTime))) and NoticeType = '" + strNoticeType + "'";
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("getAgentUsers is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}

		public DataSet GetScrollStringByUID(string strUserID,string strNoticeType)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "SELECT * FROM dbo.eg_announce where RTrim(UserID) in (select vcLoginName from eg_user where numagentid in (select numagentid from eg_user where RTrim(vcLoginName)='"+strUserID+"'))"
					+" and (OperateTime IN (SELECT MAX(OperateTime) FROM eg_announce )) and NoticeType = '" + strNoticeType + "'";
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("getAgentUsers is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}

		public string  GetEtBelong(string strETicketNumber)
		{ 
			DataSet rsRet = null;
			string rsOfficeNum = "";
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select * from eg_OfficeNumber  where " + strETicketNumber + " BETWEEN BegNumber AND EndNumber";
				rsRet = cn.GetDataSet(strSql);

				if (rsRet.Tables[0].Rows.Count != 0)				
				{
					rsOfficeNum = rsRet.Tables[0].Rows[0]["OfficeCode"].ToString();
				}
			}
			catch(Exception ex)
			{
				gs.util.func.Write(" err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsOfficeNum;
		}

	}
}


/* p_strXML的参数说明

<eg>

    <cm>SubmitPNR</cm>  <!--返回指令类型,对登入系统指令的应答-->

    <User>jm<User><!—操作用户名，即登陆帐号-->

    <PNR>EMMR3</PNR><!—订座记录号-->

       <ATK>
            <FlightNo></FlightNo><!—航班号1-->

            <Bunk></Bunk><!—舱位1-->

            <Date></Date><!—航班日期1-->

            <CityPair></CityPair><!—城市对1-->

 

            <FlightNo></FlightNo><!—航班号2-->

            <Bunk></Bunk><!—舱位2-->

            <Date></Date><!—航班日期2-->

            <CityPair></CityPair><!—城市对2-->
       <ATK>

 

        <Phone></Phone><!—联系电话-->

        <PersonCount></PersonCount><!—人数-->

        <Names></Names><!—姓名，多个人姓名用~割开-->

        <TL></TL><!—出票时限-->

</PnrInfo ><!—记录号具体信息-->  

</eg>


 
 * */