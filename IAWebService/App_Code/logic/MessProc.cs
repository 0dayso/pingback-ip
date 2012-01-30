using System;
using System.Data;
using System.Xml;
using gs.para;
using logic;
using gs.DataBase;
using logic.XmlEntity;


namespace logic
{
	/// <summary>
	/// MessProc 的摘要说明。
	/// </summary>
	public class MessProc
	{
		public MessProc()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 处理用户登入消息
		/// </summary>
		/// <param name="p_strUserName"></param>
		/// <param name="p_strPassWord"></param>
		/// <returns></returns>
		public string login(string p_strUserName,string p_strPassWord)
		{
			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();

			np.AddPara("cm","RetLogin");//返回消息说明命令解析不正常
			if(Right.IsValidUser(p_strUserName,p_strPassWord))
			{//登入成功
				string strCurUserId = Right.getUserId(p_strUserName);
				int iRole = Right.getRole(p_strUserName);
				if(iRole == 2)
				{
					np.AddPara("LoginFlag","InvalidRole");
				}
				else
				{
					np.AddPara("LoginFlag","LoginSucc");
					
					EgUser eu = new EgUser();
					string strPassPort = eu.RecUserLogin(p_strUserName);	//记录用户登入信息
					np.AddPara("PassPort",strPassPort);

					//DataSet ds = eu.getUserAgentInfo(p_strUserName);
					//np.AddPara("SrvIp",ds.Tables[0].Rows[0]["vcWebIp"].ToString().Trim());
					//np.AddPara("SrvDNS",ds.Tables[0].Rows[0]["vcDNS"].ToString().Trim());
					//np.AddPara("PassPort",strPassPort);
					//np.AddPara("TrimLen",ds.Tables[0].Rows[0]["vcTrimLen"].ToString().Trim());
					//np.AddPara("SrvPort",ds.Tables[0].Rows[0]["SrvPort"].ToString().Trim());

					DataSet dsAgent = eu.getUserAgentInfo(p_strUserName);
					np.AddPara("TrimLen",dsAgent.Tables[0].Rows[0]["vcTrimLen"].ToString().Trim());

					DataSet ds = eu.getUserLoginSrv(strCurUserId);

                    if (ds.Tables[0].Rows.Count == 0)//added by king 2009.09.09
                        throw new Exception("找不到该用户可用的配置信息！请检查是否分配了配置？");

					np.AddPara("SrvIp",ds.Tables[0].Rows[0]["vcLoginSrvIp"].ToString().Trim());
					np.AddPara("SrvDNS",ds.Tables[0].Rows[0]["vcLoginDNS"].ToString().Trim());
					np.AddPara("SrvPort",ds.Tables[0].Rows[0]["vcLoginSrvPort"].ToString().Trim());
					np.AddPara("SrvName",ds.Tables[0].Rows[0]["vcLoginSrvName"].ToString().Trim());

					//设置用户状态0为正常1为暂停,则不能登入了.
                    DataSet ds2 = eu.getUserInfo(strCurUserId);
					np.AddPara("UserStat",ds2.Tables[0].Rows[0]["numStat"].ToString().Trim());
					np.AddPara("InsuranceUserName",ds2.Tables[0].Rows[0]["vcInsUserName"].ToString().Trim());
					np.AddPara("InsurancePassWord",ds2.Tables[0].Rows[0]["vcInsPassWord"].ToString().Trim());

					//判断用户所处于的代理商是否正常
					np.AddPara("AgentStat",eu.getUserAgentStat(p_strUserName));

					//判断用户是否已经过期
					if(eu.isExpireUser(strCurUserId))
						np.AddPara("UserExpire","true");
					else
						np.AddPara("UserExpire","false");

					//增加可使用IP信息
					//XmlNode nodeIP = np.AddPara("IPS","");
					//DataSet dsIps = eu.getUserIps(strCurUserId);
					//for(int i=0;i<dsIps.Tables[0].Rows.Count;i++)
					//{
					//	string strIp = dsIps.Tables[0].Rows[i]["vcLocalIP"].ToString().Trim();
					//	XmlNode subNodeNew = doc.CreateNode(XmlNodeType.Element,"ip","");
					//	subNodeNew.AppendChild(doc.CreateTextNode(strIp));	//把变量值赋进去
					//	nodeIP.AppendChild(subNodeNew);	
					//}

					/*XmlNode nodeIP = np.AddPara("IPS","");
					for(int i=0;i<ds.Tables[0].Rows.Count;i++)
					{
						string strIp = ds.Tables[0].Rows[i]["vcLocalIP"].ToString().Trim() + "<eg66>" + ds.Tables[0].Rows[i]["IpId"].ToString().Trim();
						XmlNode subNodeNew = doc.CreateNode(XmlNodeType.Element,"ip","");
						subNodeNew.AppendChild(doc.CreateTextNode(strIp));	//把变量值赋进去
						nodeIP.AppendChild(subNodeNew);	
					}*/

					XmlNode nodeIP = np.AddPara("IPS","");
					for(int i=0;i<ds.Tables[0].Rows.Count;i++)
					{
						string strIp = ds.Tables[0].Rows[i]["vcLocalIP"].ToString().Trim() + "<eg66>" + ds.Tables[0].Rows[i]["IpId"].ToString().Trim();
						XmlNode subNodeNew = doc.CreateNode(XmlNodeType.Element,"AOFF","");

						XmlNode subIpNode = null;
						subIpNode = doc.CreateNode(XmlNodeType.Element,"ipid","");
						subIpNode.AppendChild(doc.CreateTextNode(ds.Tables[0].Rows[i]["IpId"].ToString().Trim()));
						subNodeNew.AppendChild(subIpNode);	//把变量值赋进去

						subIpNode = doc.CreateNode(XmlNodeType.Element,"ip","");
						subIpNode.AppendChild(doc.CreateTextNode(ds.Tables[0].Rows[i]["vcLocalIP"].ToString().Trim()));
						subNodeNew.AppendChild(subIpNode);	

						subIpNode = doc.CreateNode(XmlNodeType.Element,"SrvIp","");
						subIpNode.AppendChild(doc.CreateTextNode(ds.Tables[0].Rows[i]["vcLoginSrvIp"].ToString().Trim()));
						subNodeNew.AppendChild(subIpNode);	

						subIpNode = doc.CreateNode(XmlNodeType.Element,"SrvDNS","");
						subIpNode.AppendChild(doc.CreateTextNode(ds.Tables[0].Rows[i]["vcLoginDNS"].ToString().Trim()));
						subNodeNew.AppendChild(subIpNode);

						subIpNode = doc.CreateNode(XmlNodeType.Element,"SrvPort","");
						subIpNode.AppendChild(doc.CreateTextNode(ds.Tables[0].Rows[i]["vcLoginSrvPort"].ToString().Trim()));
						subNodeNew.AppendChild(subIpNode);

						subIpNode = doc.CreateNode(XmlNodeType.Element,"SrvName","");
						subIpNode.AppendChild(doc.CreateTextNode(ds.Tables[0].Rows[i]["vcLoginSrvName"].ToString().Trim()));
						subNodeNew.AppendChild(subIpNode);

						subIpNode = doc.CreateNode(XmlNodeType.Element,"PeiZhi","");
						subIpNode.AppendChild(doc.CreateTextNode(ds.Tables[0].Rows[i]["OfficeCode"].ToString().Trim()));
						subNodeNew.AppendChild(subIpNode);

						subIpNode = doc.CreateNode(XmlNodeType.Element,"vcOffNo","");
						subIpNode.AppendChild(doc.CreateTextNode(ds.Tables[0].Rows[i]["vcOffNo"].ToString().Trim()));
						subNodeNew.AppendChild(subIpNode);


						nodeIP.AppendChild(subNodeNew);	
					}


					//增加可使用指令信息
					XmlNode nodeCm = np.AddPara("UseCms","");
					DataSet dsCms = eu.getUserCms(strCurUserId);
					for(int i=0;i<dsCms.Tables[0].Rows.Count;i++)
					{
						string strCm = dsCms.Tables[0].Rows[i]["vcCm"].ToString().Trim();
						XmlNode subNodeNew = doc.CreateNode(XmlNodeType.Element,"usecm","");
						subNodeNew.AppendChild(doc.CreateTextNode(strCm));	//把变量值赋进去
						nodeCm.AppendChild(subNodeNew);	
					}

					//增加可销售票种类信息
					XmlNode nodeTk = np.AddPara("Tickets","");
					DataSet dsTks = eu.getUserTks(strCurUserId);
					for(int i=0;i<dsTks.Tables[0].Rows.Count;i++)
					{
						string strTk = dsTks.Tables[0].Rows[i]["vcTickTypeName"].ToString().Trim();
						XmlNode subNodeNew = doc.CreateNode(XmlNodeType.Element,"ticket","");
						subNodeNew.AppendChild(doc.CreateTextNode(strTk));	//把变量值赋进去
						nodeTk.AppendChild(subNodeNew);	
					}
                    //增加扣款类型2为先扣款numseerate 3为后扣款
                    DataSet dsRootAgent = eu.getUserRootAgentInfo(p_strUserName);
                    string strDecFeeType = dsRootAgent.Tables[0].Rows[0]["numSecRate"].ToString().Trim();
                    np.AddPara("DecFeeType", strDecFeeType);
				}
			}
			else
			{
				np.AddPara("LoginFlag","LoginFail");
			}
			string strRet = np.GetXML();
			return strRet;
		}

        public string GetIbeUrl(string username,string ibeIDUsing)
        {
            string strRet = "";
            Conn cn = new Conn();
            try
            {
                string strSql = "select top 1 ibeID, ibeURL from eg_IbeList where ibeID > " + ibeIDUsing + " order by ibeID";
                DataSet ds = cn.GetDataSet(strSql);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    string ibeUrl = ds.Tables[0].Rows[0]["ibeURL"].ToString().Trim();
                    string ibeID = ds.Tables[0].Rows[0]["ibeID"].ToString().Trim();

                    NewPara np = new NewPara();
                    np.AddPara("cm", "RetGetIbeUrl");//返回消息说明命令解析不正常
                    np.AddPara("ibeID", ibeID);
                    np.AddPara("ibeURL", ibeUrl);
                    strRet = np.GetXML();
                }
                else
                {
                    strSql = "select top 1 ibeID, ibeURL from eg_IbeList order by ibeID";
                    ds = cn.GetDataSet(strSql);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string ibeUrl = ds.Tables[0].Rows[0]["ibeURL"].ToString().Trim();
                        string ibeID = ds.Tables[0].Rows[0]["ibeID"].ToString().Trim();

                        NewPara np = new NewPara();
                        np.AddPara("cm", "RetGetIbeUrl");//返回消息说明命令解析不正常
                        np.AddPara("ibeID", ibeID);
                        np.AddPara("ibeURL", ibeUrl);
                        strRet = np.GetXML();
                    }
                }
            }
            catch (Exception ex)
            {
                gs.util.func.Write("GetIbeUrl is err:" + ex.Message);
                throw ex;
            }

            return strRet;
        }

		public string loginXyt(string p_strUserName,string p_strPassWord)
		{
			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();

			np.AddPara("cm","RetLogin");//返回消息说明命令解析不正常
			if(Right.IsValidUser(p_strUserName,p_strPassWord))
			{//登入成功
				np.AddPara("LoginFlag","LoginSucc");
			}
			else
			{
				np.AddPara("LoginFlag","LoginFail");
			}
			string strRet = np.GetXML();
			return strRet;
		}

		/// <summary>
		/// 处理用户余额查询
		/// </summary>
		/// <param name="p_strUserCode"></param>
		/// <returns></returns>
		public string getCloseBalance(string p_strUserCode)
		{
			string strCurUserId = Right.getUserId(p_strUserCode);
			EgFee fe = new EgFee();
			string strYe = fe.GetCloseBalance(strCurUserId);

			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();
			np.AddPara("cm","RetCloseBalance");//返回余额指令
			np.AddPara("UserYE",strYe);
			return np.GetXML();
		}

		/// <summary>
		/// 扣款
		/// </summary>
		/// <param name="p_strUserName"></param>
		/// <param name="p_strTicketVal">扣款金额</param>
		/// <returns></returns>
		public string DecFee(string p_strPnr,string p_strTicketVal)
		{
//gs.util.func.Write("df====1");
			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();
			np.AddPara("cm","RetDecFee");//返回余额指令

			
			string strCurActName = Right.getEtkUserName(p_strPnr);
//gs.util.func.Write("df====2 strCurActName=" + strCurActName);
			if(strCurActName == null || strCurActName.Trim() == "")
			{//如果没有对应的用户名则不扣款
				np.AddPara("DecStat","DecFail");
				np.AddPara("Pnr",p_strPnr.Trim());
				gs.util.func.Write("DecFee is Fail pnr=" + p_strPnr + " 该电子客票记录的用户帐号为空");
			}
			else
			{
//gs.util.func.Write("df====3 ");
				string strCurUserId = Right.getUserId(strCurActName);
//gs.util.func.Write("df====4 strCurUserId=" + strCurUserId);
				Conn cn = null;

				EgFee fe = new EgFee();

				try
				{
					cn = new Conn();
//gs.util.func.Write("df====4 ");
					cn.beginTrans();
					string strYe = fe.GetCloseBalance(cn,strCurUserId).Trim();
gs.util.func.Write("df====5 strYe=" + strYe);
					float dbYe = 0;
					float dbNewYe = 0;

					if(strYe != "")
					{
						dbYe = float.Parse(strYe);
					}
//gs.util.func.Write("df====6 ");
                    dbNewYe = dbYe;
					DataSet ds = cn.GetDataSet("select * from eg_eticket where Pnr='" + p_strPnr.Trim() + "' and DecFeeState=1 and datediff(day,OperateTime,getdate())>-5 and  datediff(day,OperateTime,getdate())<5");
                    if (ds.Tables[0].Rows.Count == 0)
                    {//判断是否已经扣过款,如果没扣过才可以扣款
                        //gs.util.func.Write("df====7 ");
                        if (fe.DecFee(cn, strCurUserId, p_strTicketVal, "system", "购买机票减款", "admin从" + strCurActName + "减款", "../HYX/OrderDetail.aspx?PnrId=" + p_strPnr.Trim(), ""))
                        {
                            //gs.util.func.Write("df====8 ");
                            dbNewYe = dbYe - float.Parse(p_strTicketVal);
                            cn.Update("update eg_eticket set DecFeeState='1',TotalFC=isnull(TotalFC,0)+" + p_strTicketVal + " where Pnr='" + p_strPnr + "' and datediff(day,OperateTime,getdate())>-5 and  datediff(day,OperateTime,getdate())<5");
gs.util.func.Write("df====9 dbNewYe=" + dbNewYe);
                        }
                       
                    }
                    
					
					cn.commit();
//gs.util.func.Write("df====10 ");
					np.AddPara("DecStat","DecSucc");
					np.AddPara("NewUserYe",dbNewYe.ToString());
					np.AddPara("Pnr",p_strPnr.Trim());
				}
				catch(Exception ex)
				{
					gs.util.func.Write("DecFee is Fail pnr=" + p_strPnr + " ex=" + ex.Message);
					np.AddPara("DecStat","DecFail");
					cn.rollback();
				}
				finally
				{
					cn.close();
				}
			}
gs.util.func.Write("df====11 np="+np.GetXML());
			return np.GetXML();
		}

		public string DecFee2(string p_strPnr,string p_strTicketVal)
		{	
			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();
			np.AddPara("cm","RetDecFee2");//返回余额指令

            string strCurActName = Right.getEtkUserName(p_strPnr);

			if(strCurActName == null || strCurActName.Trim() == "")
			{
				np.AddPara("DecStat","DecFail");
				np.AddPara("Pnr",p_strPnr.Trim());
				gs.util.func.Write("DecFee2 is Fail pnr=" + p_strPnr + " 该电子客票记录的用户帐号为空");
			}
			else
			{
				string strCurUserId = Right.getUserId(strCurActName);
				
				Conn cn = null;

				EgFee fe = new EgFee();

				try
				{
					cn = new Conn();

					cn.beginTrans();
					string strYe = fe.GetCloseBalance(cn,strCurUserId).Trim();
                    gs.util.func.Write("扣款前余额：" + strYe);
					float dbYe = 0;
					float dbNewYe = 0;
					if(strYe != "")
					{
						dbYe = float.Parse(strYe);
					}
					
					DataSet ds = cn.GetDataSet("select * from eg_eticket where Pnr='" + p_strPnr.Trim() + "' and DecFeeState=1 and datediff(day,OperateTime,getdate())>-5 and  datediff(day,OperateTime,getdate())<5");
                    if (ds.Tables[0].Rows.Count == 0)
                    {//判断是否已经扣过款,如果没扣过才可以扣款

                        if (fe.DecFee(cn, strCurUserId, p_strTicketVal, "system", "购买机票减款", "admin从" + strCurActName + "减款", "../HYX/OrderDetail.aspx?PnrId=" + p_strPnr.Trim(), ""))
                        {
                            dbNewYe = dbYe - float.Parse(p_strTicketVal);
                            cn.Update("update eg_eticket set DecFeeState=1,TotalFC=isnull(TotalFC,0)+" + p_strTicketVal + " where Pnr='" + p_strPnr + "' and datediff(day,OperateTime,getdate())>-5 and  datediff(day,OperateTime,getdate())<5");
                            gs.util.func.Write("扣款后余额：" + dbNewYe);
                        }
                    }
                    else
                    {
                        gs.util.func.Write("已扣过款，无需重复扣款");
                    }
					
					cn.commit();
					np.AddPara("DecStat","DecSucc");
					np.AddPara("NewUserYe",dbNewYe.ToString());
					np.AddPara("Pnr",p_strPnr.Trim());
				}
				catch(Exception ex)
				{
					gs.util.func.Write("DecFee2 is Fail pnr=" + p_strPnr + " ex=" + ex.Message);
					np.AddPara("DecStat","DecFail");
					cn.rollback();
				}
				finally
				{
					cn.close();
				}
			}

            return np.GetXML();
		}

        public string DecFee3(string p_strPnr, string p_strTicketVal, string p_strNumber)
        {
            NewPara np = new NewPara();
            XmlDocument doc = np.getRoot();
            np.AddPara("cm", "RetDecFee3");

            string strCurActName = Right.getEtkUserName(p_strPnr);

            if (strCurActName == null || strCurActName.Trim() == "")
            {
                np.AddPara("DecStat", "DecFail");
                np.AddPara("Pnr", p_strPnr.Trim());
                gs.util.func.Write("DecFee3 is Fail pnr=" + p_strPnr + " 该电子客票记录的用户帐号为空");
            }
            else
            {
                string strCurUserId = Right.getUserId(strCurActName);

                Conn cn = null;

                EgFee fe = new EgFee();

                try
                {
                    cn = new Conn();

                    cn.beginTrans();
                    string strYe = fe.GetCloseBalance(cn, strCurUserId).Trim();
                    float dbYe = 0;
                    float dbNewYe = 0;
                    if (strYe != "")
                    {
                        dbYe = float.Parse(strYe);
                    }

                    DataSet ds = cn.GetDataSet("select * from eg_eticket where Pnr='" + p_strPnr.Trim() + "' and DecFeeState=1 and datediff(day,OperateTime,getdate())>-5 and  datediff(day,OperateTime,getdate())<5");
                    if (ds.Tables[0].Rows.Count == 0)
                    {//判断是否已经扣过款,如果没扣过才可以扣款

                        if (fe.DecFee(cn, strCurUserId, p_strTicketVal, "system", "购买机票减款", "admin从" + strCurActName + "减款", "../HYX/OrderDetail.aspx?PnrId=" + p_strPnr.Trim(), ""))
                        {
                            dbNewYe = dbYe - float.Parse(p_strTicketVal);
                            //cn.Update("update eg_eticket set DecFeeState=1,TotalFC=isnull(TotalFC,0)+" + p_strTicketVal + " where Pnr='" + p_strPnr + "' and datediff(day,OperateTime,getdate())>-5 and  datediff(day,OperateTime,getdate())<5");
                            cn.Update("update eg_eticket set etNumber='" + p_strNumber + "',DecFeeState=1,TotalFC=isnull(TotalFC,0)+" + p_strTicketVal + " where Pnr='" + p_strPnr + "' and datediff(day,OperateTime,getdate())>-5 and  datediff(day,OperateTime,getdate())<5");
                        }

                    }
                    else
                    { //已经扣款成功的，写入电子客票号

                        cn.Update("update eg_eticket set etNumber='" + p_strNumber + "' where Pnr='" + p_strPnr + "' and datediff(day,OperateTime,getdate())>-5 and  datediff(day,OperateTime,getdate())<5");
                    }

                    cn.commit();
                    np.AddPara("DecStat", "DecSucc");
                    np.AddPara("NewUserYe", dbNewYe.ToString());
                    np.AddPara("Pnr", p_strPnr.Trim());
                }
                catch (Exception ex)
                {
                    gs.util.func.Write("DecFee3 is Fail pnr=" + p_strPnr + " ex=" + ex.Message);
                    np.AddPara("DecStat", "DecFail");
                    cn.rollback();
                }
                finally
                {
                    cn.close();
                }
            }

            return np.GetXML();
        }

		/// <summary>
		/// 用户退款服务
		/// </summary>
		/// <param name="p_strUserCode"></param>
		/// <param name="p_strTicketVal"></param>
		/// <returns></returns>
		public string BackFee(string p_strUserCode,string p_strTicketVal)
		{
            string strCurUserId = Right.getUserId(p_strUserCode);

			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();
			np.AddPara("cm","RetBackFee");//返回余额指令

			EgFee fe = new EgFee();
			float dbYe = 0;
			string strYe = fe.GetCloseBalance(strCurUserId).Trim();
			if(strYe != "")
			{
				dbYe = float.Parse(strYe);
			}

			bool bRet = fe.BackFee(strCurUserId,p_strTicketVal,"system");
			if(bRet)
			{
				float dbNewYe = dbYe+float.Parse(p_strTicketVal);
				np.AddPara("BackStat","BackSucc");
				np.AddPara("NewUserYe",dbNewYe.ToString());
			}
			else
			{
				np.AddPara("BackStat","BackFail");
			}
			return np.GetXML();
		}

		/// <summary>
		/// 用户刷新自己的在线时间
		/// </summary>
		/// <param name="p_strUserCode"></param>
		/// <returns></returns>
		public string RereshSelf(string p_strUserCode)
		{
			string strCurUserId = Right.getUserId(p_strUserCode);

			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();
			np.AddPara("cm","RetRefreshSelf");//用户刷新自己的在线时间.
			EgUser eu = new EgUser();
			if(eu.RefreshSelf(p_strUserCode))
			{//刷新成功
				np.AddPara("RefreshStat","RefreshSucc");
			}
			else
			{
				np.AddPara("RefreshStat","RefreshFail");
			}
			return np.GetXML();
		}

		/// <summary>
		/// 用户退出
		/// </summary>
		/// <param name="p_strUserCode"></param>
		/// <returns></returns>
		public string logout(string p_strUserCode)
		{
			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();
			np.AddPara("cm","RetLogout");//在线用户表中删除用户
			EgUser eu = new EgUser();
			if(eu.DelOnlineUser(p_strUserCode))
			{//刷新成功
				np.AddPara("LogoutStat","LogoutSucc");
			}
			else
			{
				np.AddPara("LogoutStat","LogoutFail");
			}
			return np.GetXML();
		}


		/// <summary>
		/// 用户修改密码
		/// </summary>
		/// <param name="p_strUserCode"></param>
		/// <param name="p_strPassword"></param>
		/// <returns></returns>
		public string ChgPassword(string p_strUserCode,string p_strPassword)
		{
			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();
			np.AddPara("cm","RetChgPassword");//在线用户表中删除用户
			EgUser eu = new EgUser();
			if(eu.ChgPassword(p_strUserCode,p_strPassword))
			{//刷新成功
				np.AddPara("ChgPWDStat","ChgPassSucc");
			}
			else
			{
				np.AddPara("ChgPWDStat","ChgPassFail");
			}
			return np.GetXML();
		}

		public string RegInsurance(string strUserID,string streNumber,string strIssueNumber,
			string strNameIssued,string strCardType,string strCardNumber,
			string strRemark,string strIssuePeriod,string strIssueBegin,
			string strIssueEnd,string strSolutionDisputed,string strNameBeneficiary,
			string strSignature,string strSignDate,string strInssuerName,string strPnr)
		{
			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();
			np.AddPara("cm","RetSubmitHYX");
			Insurance ins = new Insurance();

			//if (ins.GetIncBound(strRemark,strIssueNumber))
			//{
				if(ins.SaveIns(strUserID,streNumber,strIssueNumber,
					strNameIssued,strCardType,strCardNumber,
					strRemark,strIssuePeriod,strIssueBegin,
					strIssueEnd,strSolutionDisputed,strNameBeneficiary,
					strSignature,strSignDate,strInssuerName,strPnr))
				{//保存成功
					np.AddPara("OperationFlag","SaveSucc");
				}
				else
				{
					np.AddPara("OperationFlag","SaveFail");
				}
			/*}
			else
			{
				np.AddPara("OperationFlag","SaveFail");
			}*/

			return np.GetXML();
		}

		public string RegETicket(string strUserID,string strPnr,string stretNumber,
			string strFlightNumber1,string strBunk1,string strCityPair1,string strDate1,
			string strFlightNumber2,string strBunk2,string strCityPair2,
			string strDate2,string strTotalFC,string strDecFeeState,string strKey,string strIpId,string strBasePrc,string strOil,string strPassenger)
		{
			NewPara np = new NewPara();
			try
			{
				XmlDocument doc = np.getRoot();
			
				np.AddPara("cm",strKey);
				Insurance ins = new Insurance();
			
				if(ins.SaveETicket(strUserID,strPnr,stretNumber,
					strFlightNumber1,strBunk1,strCityPair1,
					strDate1,strFlightNumber2,strBunk2,
					strCityPair2,strDate2,strTotalFC,strDecFeeState,strKey,strIpId,strBasePrc,strOil,strPassenger))
				{//保存成功
					np.AddPara("OperationFlag","SaveSucc");
				}
				else
				{
					np.AddPara("OperationFlag","SaveFail");
				}
			}
			catch(Exception ex)
			{
				gs.util.func.Write("WS.logic.MessProc.RegETicket is err" + ex.Message);
				throw ex;
			}
			return np.GetXML();
		}

		public string DelETicket( string strPnr )
		{
			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();
			
			np.AddPara("cm","RetCancelPNR");
			Insurance ins = new Insurance();
			if(ins.DelETicket(strPnr))
			{//保存成功
				np.AddPara("OperationFlag","SaveSucc");
			}
			else
			{
				np.AddPara("OperationFlag","SaveFail");
			}
			return np.GetXML();
		}

		public string QueryETicket( string strUserID )
		{
			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();
			
			np.AddPara("cm","RetGetUncheckedPNR");
			Insurance ins = new Insurance();
			string strPnr = ins.GetUncheckedPNR(strUserID);
			
			np.AddPara("Pnr",strPnr);
			
			return np.GetXML();
		}

		/// <summary>
		/// 得到查询指定用户，指定状态的 PNRs串
		/// </summary>
		/// <param name="strUser"></param>
		/// <param name="strPNRState"></param>
		/// <returns></returns>
		public string GetStrPNRs(string strUser,string strPNRState)
		{
			DataSet ds;
			PnrTicket pnr = new PnrTicket();
			ds = pnr.GetPnrs(strUser,strPNRState);

			string strPnrs = "";

			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				strPnrs += dr["vcPnrNo"] + ";";
			}

			if (strPnrs != "")
			{
				strPnrs = strPnrs.Remove(strPnrs.Length -1,1);
			}

			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();
			np.AddPara("cm","RetGetPNRs");//返回余额指令
			np.AddPara("PNR",strPnrs);
			return np.GetXML();			
		}

		/// <summary>
		/// 请求将PNR状态设为删除状态
		/// </summary>
		/// <param name="strUser"></param>
		/// <param name="strPNR"></param>
		/// <returns></returns>
		public string SetPnrDelState(string strUser,string strPNR)
		{
			NewPara np = new NewPara();
			np.AddPara("cm","RetSetPNRStateDelete");

			PnrTicket pnr = new PnrTicket();
			
			//strPNR = strPNR.Replace(";","','");
			
			 
			//strPNR = "'" + strPNR + "'";

			if(pnr.SetPNRStateDel(strPNR))
			{//刷新成功
				np.AddPara("OperationFlag","SaveSucc");
			}
			else
			{
				np.AddPara("OperationFlag","SaveFail");
			}
			return np.GetXML();	
		}

		public string GetFC(string strFROM, string strTO)
		{
			DataTable dt;
			PnrTicket pnr = new PnrTicket();
			dt = pnr.GetDsFC(strFROM,strTO).Tables[0];
			
		 
			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();
			np.AddPara("cm","RetGetFC");//返回余额指令
			np.AddPara("BUNKF",dt.Rows[0]["BUNKF"].ToString());
			np.AddPara("BUNKC",dt.Rows[0]["BUNKC"].ToString());
			np.AddPara("BUNKY",dt.Rows[0]["BUNKY"].ToString());
			return np.GetXML();
		}

		public string GetPassenger(string name)
		{
			DataTable dtTicket;
			DataTable dtPnr;
			PnrTicket pnr = new PnrTicket();
			string strPassengers="";
			string[] aryPassengers;
			string[] aryPList;
			dtTicket = pnr.GetPassengerOfTicket(name).Tables[0];			
			dtPnr = pnr.GetPassengerOfPnr(name).Tables[0];		
	
			foreach(DataRow dr in dtTicket.Rows)
			{
				aryPList = dr["Passenger"].ToString().Split(';');

				for(int i=0;i< aryPList.Length;i++)
				{
					aryPassengers = aryPList[i].Split('-');

					if (aryPassengers[0].Trim() == name)
					{
						if (strPassengers.IndexOf(aryPassengers[1].Trim()) == -1)
							strPassengers += aryPassengers[1].Trim() + ",";
					}
				}
			}

			foreach(DataRow dr in dtPnr.Rows)
			{
				aryPList = dr["vcNames"].ToString().Split(';');

				for(int i=0;i< aryPList.Length;i++)
				{
					aryPassengers = aryPList[i].Split('-');

					if (aryPassengers[0].Trim() == name)
					{
						if (strPassengers.IndexOf(aryPassengers[1].Trim()) == -1)
							strPassengers += aryPassengers[1].Trim() + ",";
					}
				}
			}
		 
			if (strPassengers != "")
			{
				strPassengers = strPassengers.Remove(strPassengers.Length -1,1);
			}

			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();
			np.AddPara("cm","RetGetPassenger");
			np.AddPara("CardID",strPassengers);			
			return np.GetXML();
		}

		/// <summary>
		/// 请求保存价格
		/// </summary>
		/// <param name="strFROM"></param>
		/// <param name="strTO"></param>
		/// <param name="strBUNKF"></param>
		/// <param name="strBUNKC"></param>
		/// <param name="strBUNKY"></param>
		/// <returns></returns>
		public string SaveFC(string strFROM,string strTO,string strBUNKF,string strBUNKC,string strBUNKY)
		{
			NewPara np = new NewPara();
			np.AddPara("cm","RecSaveFC");

			PnrTicket pnr = new PnrTicket(); 
			 
			if(pnr.SaveFC(strFROM,strTO,strBUNKF,strBUNKC,strBUNKY))
			{//刷新成功
				np.AddPara("OperationFlag","SaveSucc");
			}
			else
			{
				np.AddPara("OperationFlag","SaveFail");
			}
			return np.GetXML();	
		}

		/// <summary>
		/// 用户命令日志
		/// </summary>
		/// <param name="p_strUserCode"></param>
		/// <param name="p_strCm"></param>
		/// <param name="p_strCmType"></param>
		/// <param name="p_strTime"></param>
		/// <returns></returns>
		public string SaveLogs(string p_strUserCode,string p_strCm,string p_strCmType,string p_strTime)
		{
			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();
			np.AddPara("cm","RetChgPassword");//在线用户表中删除用户
			EgUser eu = new EgUser();
			if(eu.SaveLogs(p_strUserCode,p_strCm,p_strCmType,p_strTime))
			{//保存成功
				np.AddPara("RetSaveOperations","SaveSucc");
			}
			else
			{
				np.AddPara("RetSaveOperations","SaveFail");
			}
			return np.GetXML();
		}


		public string SaveSysLogs(string strUser,string strCmd,string strReturnResult)
		{
			NewPara np = new NewPara();
			np.AddPara("cm","RetWriteLog");
			SysLogs logs = new SysLogs();
			if(logs.AddLogs(strUser,strCmd,strReturnResult))
			{//刷新成功
				np.AddPara("OperationFlag","SaveSucc");
			}
			else
			{
				np.AddPara("OperationFlag","SaveFail");
			}
			return np.GetXML();	
		}

		private string getAgentCode(string p_strAgentCode)
		{
			string strRet = p_strAgentCode;

			if(p_strAgentCode.Length > 8)
				strRet = strRet.Substring(0,8);

			return strRet;
		}

		/// <summary>
		/// 处理傻瓜版顶票信息
		/// </summary>
		/// <param name="p_strXML"></param>
		/// <returns></returns>
		public string NewPnr(string p_strXML)
		{
			NewPara np = new NewPara();
			np.AddPara("cm","RetSubmitPNR");//用户刷新自己的在线时间.
			PnrTicket pt = new PnrTicket();

			string strCurUserId = "";

			NewPara npRet = new NewPara(p_strXML.Trim());
			string strUserTitle = "",strAgentFullName = "",strAgentCode = "",strLmtJe = "0.00";
			string strUserCode = npRet.FindTextByPath("//eg/User").Trim();

			//string strCurUserId = Right.getUserId(strUserCode);

			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select eg_user.*,dbo.getAgentName(numAgentId) as vcNewAgentName from eg_user where vcLoginName='" + strUserCode + "'";
				DataSet ds = cn.GetDataSet(strSql);
				if(ds.Tables[0].Rows.Count > 0)
				{
					strCurUserId = ds.Tables[0].Rows[0]["numUserId"].ToString().Trim();
					strUserTitle = ds.Tables[0].Rows[0]["vcUserTitle"].ToString().Trim();
					strAgentFullName = ds.Tables[0].Rows[0]["vcNewAgentName"].ToString().Trim();
					strAgentCode = ds.Tables[0].Rows[0]["numAgentId"].ToString().Trim();
				}
				ds.Clear();
				
				string strSqlAg = "select * from eg_agent where numAgentId='" + getAgentCode(strAgentCode) + "'";
				DataSet dsAg = cn.GetDataSet(strSqlAg);
				if(dsAg.Tables[0].Rows[0]["numEasyLmt"] != null && dsAg.Tables[0].Rows[0]["numEasyLmt"].ToString().Trim() != "")
					strLmtJe = dsAg.Tables[0].Rows[0]["numEasyLmt"].ToString().Trim();
				dsAg.Clear();
			}
			finally
			{
				cn.close();
			}
			

			EgFee fe = new EgFee();
			string strYe = fe.GetCloseBalance(strCurUserId);

			if(System.Double.Parse(strYe) > System.Double.Parse(strLmtJe) )
			{

				string strPnr = npRet.FindTextByPath("//eg/PNR").Trim();
				string strRet = pt.NewTickte(p_strXML);
				/*string strSql = "select vcUserTitle,dbo.getAgentName(numAgentId) as vcNewAgentName from eg_user where vcLoginName='" + strUserCode + "'";
				Conn cn = null;
				try
				{
					cn = new Conn();
					DataSet ds = cn.GetDataSet(strSql);
					strUserTitle = ds.Tables[0].Rows[0]["vcUserTitle"].ToString().Trim();
					strAgentFullName = ds.Tables[0].Rows[0]["vcNewAgentName"].ToString().Trim();
			
				}
				catch(Exception ex)
				{
					gs.util.func.Write("NewPnr date operate is err" + ex.Message);
				}
				finally
				{
					cn.close();
				}*/


				if(strRet.Trim() != "")
				{//出票成功
					np.AddPara("OperationFlag","SaveSucc");
					np.AddPara("UserTitle",strUserTitle);
					np.AddPara("AgentFullName",strAgentFullName);
					np.AddPara("PNR",strPnr);
					np.AddPara("Passports",strRet);

				}
				else
				{
					np.AddPara("OperationFlag","SaveFail");
				}
			}
			else
			{
				np.AddPara("OperationFlag","SaveFail");
				gs.util.func.Write("用户" + strUserCode + "小于-500块,已经不能提交简易版定单");
			}
			return np.GetXML();
		}

		public string getEtkBound(string strUserID,string streNumber,string strCfgNumber)
		{
			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();
			np.AddPara("cm","RetCheckRecieptNumber");
			PnrTicket etk = new PnrTicket();

			if (etk.GetEtkBound(strUserID,streNumber,strCfgNumber))
			{		
				np.AddPara("OperationFlag","TRUE");
			}
			else
			{
				np.AddPara("OperationFlag","FALSE");
			}

			return np.GetXML();
		}

		public string setCanPrint(string strUserID,string strRecieptNumber,string strCfgNumber,string strETNumber)
		{
			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();
			np.AddPara("cm","RetCanPrint");
			PnrTicket etk = new PnrTicket();

			if (etk.GetOfficeBound(strRecieptNumber,strCfgNumber,strUserID) 
				&& etk.GetEtkBound(strUserID,strRecieptNumber,strCfgNumber) 
				&& etk.GetRptBound(strETNumber,strCfgNumber,strUserID))				
			{		
				np.AddPara("OperationFlag","TRUE");
			}
			else
			{
				np.AddPara("OperationFlag","FALSE");
			}

			return np.GetXML();
		}

		public string AddGroup(string strUserID,string strName,string strCardID,string strGroupTicketID)
		{
			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();
			
			np.AddPara("cm","RetAddToGroup");
			PnrTicket pt = new PnrTicket();
			
			if(pt.SaveGroup(strUserID,strName,strCardID,strGroupTicketID))
			{//保存成功
				np.AddPara("OperationFlag","SaveSucc");
			}
			else
			{
				np.AddPara("OperationFlag","SaveFail");
			}
			
			return np.GetXML();
		}

		public string GetListGroupTicket(string strFromTo,string strDate,string strUserID,string strType)
		{
			DataSet ds;
			DataSet dsContent;
			string count;
			string strRebate="";
			PnrTicket pnr = new PnrTicket();
			ds = pnr.GetGroupTicket(strFromTo.Trim(),strDate.Trim(),strUserID.Trim());

			string strGroup = "";

			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				dsContent = pnr.GetGroupContent(dr["ID"].ToString());

				if (dsContent.Tables[0].Rows.Count == 0 || dsContent == null)				
					count = "0";
				else
					count = dsContent.Tables[0].Rows.Count.ToString();

				if (strType.ToUpper() == "A")
					strRebate = dr["Rebate"].ToString();
				else if (strType.ToUpper() == "B")
					strRebate = dr["TypeB"].ToString();
				else if (strType.ToUpper() == "C")
					strRebate = dr["TypeC"].ToString();
				else if (strType.ToUpper() == "D")
					strRebate = dr["TypeD"].ToString();
				else if (strType.ToUpper() == "E")
					strRebate = dr["TypeE"].ToString();

				string strMark = dr["vcMark"].ToString().Trim();
				//strMark = strMark.Replace(","," ");

				strGroup += dr["ID"] + "<eg66>" + dr["FromTo"] + "<eg66>" + dr["FlightNo"] + "<eg66>" + dr["Date"] + "<eg66>" + strRebate + "<eg66>" + dr["Total"]  + "<eg66>" + dr["Pnr"]+ "<eg66>" + dr["vcPrcName"] + "<eg66>" + strMark + "<eg66>" + count + "<eg666>";

			}

			if (strGroup != "")
			{
				strGroup = strGroup.Remove(strGroup.Length -7,7);
			}

			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();
			np.AddPara("cm","RetListGroupTicket");//返回余额指令
			np.AddPara("Result",strGroup);
			return np.GetXML();			
		}
		public string AddScrollString(string strUserID,string strContext,string strBegTime,string strEndTime,string strNoticeType)
		{
			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();
			
			np.AddPara("cm","RetSubmitScrollString");
			PnrTicket pt = new PnrTicket();
			
			if(pt.SaveScrollString(strUserID,strContext,strBegTime,strEndTime,strNoticeType))
			{//保存成功
				np.AddPara("OperationFlag","SaveSucc");
			}
			else
			{
				np.AddPara("OperationFlag","SaveFail");
			}
			
			return np.GetXML();
		}

		public string GetScrollString(string strUserID,string strNoticeType)
		{
			//刷新在线用户
			EgUser eu = new EgUser();
			eu.RefreshSelf(strUserID);

			//
			DataSet ds;
			string strContext="";
			
			PnrTicket etk = new PnrTicket();
			ds = etk.GetScrollString(strUserID,strNoticeType);

			if (ds != null && ds.Tables[0].Rows.Count != 0)
			{
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					strContext += dr["Context"].ToString().Trim() + "|";
				}
			}
			
			ds = etk.GetScrollStringByUID(strUserID,strNoticeType);

			if (ds != null && ds.Tables[0].Rows.Count != 0)
			{
				if (ds.Tables[0].Rows[0]["EndTime"].Equals(System.DBNull.Value))
				{
					strContext += ds.Tables[0].Rows[0]["Context"].ToString().Trim() + "|";
				}
			}
			
			if (strContext != "")
			{
				strContext = strContext.Remove(strContext.Length -1,1);
			}

			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();
			np.AddPara("cm","RetGetCurrentScrollString");
			np.AddPara("Context",strContext);
			return np.GetXML();			
		}

		public string getETNumberBelong(string strETicketNumber)
		{
			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();
			np.AddPara("cm","RetRequestETNumberBelong");
			PnrTicket etk = new PnrTicket();
			np.AddPara("OfficeNumber",etk.GetEtBelong(strETicketNumber));
			return np.GetXML();
		}

		public string setPnrState(string strUser,string strPNR,string strState)
		{
			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();
			np.AddPara("cm","RetSubmitPnrState");
			PnrTicket pnr = new PnrTicket();

			if(pnr.savePnrState(strUser,strPNR,strState))
			{//保存成功
				np.AddPara("OperationFlag","SaveSucc");
			}
			else
			{
				np.AddPara("OperationFlag","SaveFail");
			}

			return np.GetXML();
		}

		public string getPubMes(string p_strUser)
		{//得到最新公告
			string strCurAgentId = Right.getAgent(p_strUser);
			EgMes em = new EgMes();
			DataSet ds = em.getNewMes(p_strUser,strCurAgentId);

			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();

			np.AddPara("cm","RetPubMes");//返回消息说明消息正常返回
			
			XmlNode nodeRec = np.AddPara("NewsRecs","");
			
			for(int i=0;i<ds.Tables[0].Rows.Count;i++)
			{
				XmlNode subNodeNew = doc.CreateNode(XmlNodeType.Element,"ARec","");
				subNodeNew.AppendChild(doc.CreateTextNode(""));	//做个小的ROOT
				nodeRec.AppendChild(subNodeNew);	

				//得到发信人
				string strUserId = ds.Tables[0].Rows[i]["vcUserTitle"].ToString().Trim();
				XmlNode nodeUserName = doc.CreateNode(XmlNodeType.Element,"UserName","");
				nodeUserName.AppendChild(doc.CreateTextNode(strUserId));	
				subNodeNew.AppendChild(nodeUserName);

				//公告内容
				string strContent = ds.Tables[0].Rows[i]["vcContext"].ToString().Trim();
				XmlNode nodeContent = doc.CreateNode(XmlNodeType.Element,"Content","");
				nodeContent.AppendChild(doc.CreateTextNode(strContent));	
				subNodeNew.AppendChild(nodeContent);

				//公告发出时间
				string strTime = ds.Tables[0].Rows[i]["dtOperateTime"].ToString().Trim();
				XmlNode nodeTime = doc.CreateNode(XmlNodeType.Element,"Time","");
				nodeTime.AppendChild(doc.CreateTextNode(strTime));	
				subNodeNew.AppendChild(nodeTime);
			
			}
			
			string strRet = np.GetXML();
			return strRet;
		}

		public string GetUserIpsAndAgents(string p_strUser)
		{//得到用户的IP和所有能看到的代理商

			string strCurUserId = Right.getUserId(p_strUser);

			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();

			np.AddPara("cm","RetGetUserIpsAndAgents");

			EgUser eu = new EgUser();
			DataSet ds = eu.getUserLoginSrv(strCurUserId);
			
			XmlNode nodeIP = np.AddPara("IPS","");
			for(int i=0;i<ds.Tables[0].Rows.Count;i++)
			{
				string strIp = ds.Tables[0].Rows[i]["OfficeCode"].ToString().Trim() + "<eg66>" + ds.Tables[0].Rows[i]["IpId"].ToString().Trim();
				XmlNode subNodeNew = doc.CreateNode(XmlNodeType.Element,"ip","");
				subNodeNew.AppendChild(doc.CreateTextNode(strIp));	//把变量值赋进去
				nodeIP.AppendChild(subNodeNew);	
			}

			ds = eu.getUserAgents(p_strUser);
			XmlNode nodeAg = np.AddPara("AGENTS","");
			for(int i=0;i<ds.Tables[0].Rows.Count;i++)
			{
				string strAg = ds.Tables[0].Rows[i]["numAgentId"].ToString().Trim() + "<eg66>" + ds.Tables[0].Rows[i]["vcAgentName"].ToString().Trim();
				XmlNode subNodeNew = doc.CreateNode(XmlNodeType.Element,"ag","");
				subNodeNew.AppendChild(doc.CreateTextNode(strAg));	//把变量值赋进去
				nodeAg.AppendChild(subNodeNew);	
			}

			string strRet = np.GetXML();
			return strRet;

		}

		private string getVal(DataSet ds,string p_strFieldName)
		{
			if(ds.Tables[0].Rows[0][p_strFieldName] != null)
			{
				return ds.Tables[0].Rows[0][p_strFieldName].ToString().Trim();
			}
			else
			{
				return "";
			}
		}

		public string GetTekInfoFromPnr(string p_strPnr)
		{//从电子客票里取得信息，提电脑号
			
			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();

			np.AddPara("cm","RetGetTekInfo");

			Order od = new Order();

			DataSet ds = od.getTekInfo(p_strPnr);

			if(ds.Tables[0].Rows.Count == 0)
				np.AddPara("TicketID","");
			else
			{
				np.AddPara("TicketID",getVal(ds,"TicketID"));
				np.AddPara("UserID",getVal(ds,"UserID"));
				np.AddPara("OperateTime",getVal(ds,"OperateTime"));
				np.AddPara("Pnr",getVal(ds,"Pnr"));
				np.AddPara("etNumber",getVal(ds,"etNumber"));
				np.AddPara("FlightNumber1",getVal(ds,"FlightNumber1"));
				np.AddPara("Bunk1",getVal(ds,"Bunk1"));
				np.AddPara("FlightNumber2",getVal(ds,"FlightNumber2"));
				np.AddPara("Bunk2",getVal(ds,"Bunk2"));
				np.AddPara("CityPair1",getVal(ds,"CityPair1"));
				np.AddPara("CityPair2",getVal(ds,"CityPair2"));
				np.AddPara("Date1",getVal(ds,"Date1"));
				np.AddPara("Date2",getVal(ds,"Date2"));
				np.AddPara("TotalFC",getVal(ds,"TotalFC"));
				np.AddPara("State",getVal(ds,"State"));
				np.AddPara("DecFeeState",getVal(ds,"DecFeeState"));
				np.AddPara("Passenger",getVal(ds,"Passenger"));
				np.AddPara("IpId",getVal(ds,"IpId"));
				np.AddPara("numRealPrc",getVal(ds,"numRealPrc"));
				np.AddPara("numBasePrc",getVal(ds,"numBasePrc"));
				np.AddPara("numOilPrc",getVal(ds,"numOilPrc"));
				np.AddPara("vcTkType",getVal(ds,"vcTkType"));
				np.AddPara("vcSenter",getVal(ds,"vcSenter"));
				np.AddPara("vcCusters",getVal(ds,"vcCusters"));
			}
			
			/*XmlNode nodeIP = np.AddPara("IPS","");
			for(int i=0;i<ds.Tables[0].Rows.Count;i++)
			{
				string strIp = ds.Tables[0].Rows[i]["vcLocalIP"].ToString().Trim() + "<eg66>" + ds.Tables[0].Rows[i]["IpId"].ToString().Trim();
				XmlNode subNodeNew = doc.CreateNode(XmlNodeType.Element,"ip","");
				subNodeNew.AppendChild(doc.CreateTextNode(strIp));	//把变量值赋进去
				nodeIP.AppendChild(subNodeNew);	
			}*/
			
			string strRet = np.GetXML();
			return strRet;

		}


		/// <summary>
		/// 作废保单
		/// </summary>
		/// <param name="p_strIncNo"></param>
		/// <param name="p_strIncCmp"></param>
		/// <param name="p_strUserName"></param>
		/// <returns></returns>
		public string cancelInsurance(string p_strIncNo,string p_strIncCmp,string p_strUserName)
		{
			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();
			np.AddPara("cm","RetDelInsState");
			Insurance ins = new Insurance();

			if(ins.setIncIsCancel(p_strIncNo,p_strIncCmp,p_strUserName))
			{//保存成功
				np.AddPara("OperationFlag","SaveSucc");
			}
			else
			{
				np.AddPara("OperationFlag","SaveFail");
			}

			return np.GetXML();
		}
        /// <summary>
        /// 查询可K位的舱位
        /// </summary>
        /// <param name="strFromTo"></param>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public string GetKBunkInfo(string strFromTo, string strDate)
        {
            KBunk kb = new KBunk();
            string strTemo = kb.QueryBunk(strFromTo, strDate);
            string strRet = string.Format("<eg><cm>ReplyKBunkInfo</cm>{0}</eg>",strTemo);
            //np.AddPara("cm", "ReplyKBunkInfo");
            //np.AddPara("bunkinfo", strTemo);
            return strRet;
        }
        /// <summary>
        /// 提交一个K位的申请
        /// </summary>
        /// <param name="xnApp"></param>
        /// <returns></returns>
        public string ApplyKBunkApplication(XmlNode xnApp)
        {
            XMLApplyInfo xmlap = new XMLApplyInfo();
            Type mytype = xmlap.GetType();
            System.Reflection.FieldInfo[] fields = mytype.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            for (int i = 0; i < fields.Length-1; i++)
            {
                fields[i].SetValue(xmlap,xnApp.ChildNodes[i].InnerText.Trim());
            }
            XmlNode xnPa = xnApp.ChildNodes[xnApp.ChildNodes.Count-1];
            XMLPassenger[] xmlpas=new XMLPassenger[xnPa.ChildNodes.Count];
            XMLPassenger tempPas = new XMLPassenger();
            Type mytype1 = tempPas.GetType();
            System.Reflection.FieldInfo[] fields1 = mytype1.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            for (int i = 0; i < xmlpas.Length; i++)
            {
                xmlpas[i] = new XMLPassenger();
                for (int j = 0; j < fields1.Length; j++)
                {
                    fields1[j].SetValue(xmlpas[i],xnPa.ChildNodes[i].ChildNodes[j].InnerText.Trim());
                }
            }
            xmlap.passengerss = xmlpas;
            KBunk kb=new KBunk();
            string strRec = kb.AddKOrder(xmlap);
            string[] rec = strRec.Split(',');
            strRec = string.Format(@"<eg><cm>ApplyKBunkInfo</cm><result>{0}</result><passport>{1}</passport></eg>",rec[0],rec[1]);
            return strRec;
        }
        /// <summary>
        /// 显示一个未处理的K座
        /// </summary>
        /// <returns></returns>
        public string DisplayKBunkInfo(string orderid)
        {
            KBunk kb = new KBunk();
            string strRet = kb.DisplayUnProcess(orderid);
            strRet = string.Format("<eg><cm>ReturnUnProcess</cm><result>{0}</result></eg>", strRet);
            return strRet;
        }
        public string ProcessKOrder(NewPara np)
        {
            KBunk kb = new KBunk();
            string strRet = kb.ProcessKOrder(np.GetXML());
            strRet = string.Format("<eg><cm>ReturnProcessOrder</cm>{0}</eg>", strRet);
            return strRet;
        }
        /// <summary>
        /// 判断电脑号是否被扣过款
        /// </summary>
        /// <param name="npRet"></param>
        /// <returns></returns>
        public string IsDecFee(NewPara npRet)
        {
            Conn co = new Conn();
            string strPnr = npRet.FindTextByPath("//eg/pnr");
            string strSql = "Select DecFeeState from eg_eticket where datediff(day,OperateTime,getdate())>-5 and  datediff(day,OperateTime,getdate())<5 and pnr='" + strPnr + "'";
            DataSet ds = co.GetDataSet(strSql);
            string strReIsDecFee = string.Empty;
            try
            {
                strReIsDecFee = ds.Tables[0].Rows[0]["DecFeeState"].ToString();
            }
            catch
            {
                strReIsDecFee = "not found";
            }
            NewPara np = new NewPara();
            np.AddPara("cm", "ReIsDecFee");
            np.AddPara("IsDecFee", strReIsDecFee);
            return np.GetXML();
        }
        public string GetEticketReport(string us, string startdate, string enddate)
        { 
            string strRet = string.Empty;
            try
            {
               
                Conn co = new Conn();
                DataSet ds = co.GetDataSet("select * from eg_user where vcLoginName='" + us + "'");
                string numAgentId = ds.Tables[0].Rows[0]["numAgentID"].ToString().Trim();
                string Sql = string.Format("select '{0}' as agentid, '' as receiptNumber, '-1' as TicketID,('你:' + convert(varchar,count(*)) + '条记录.' + convert(varchar,isnull(sum(dbo.getEtkCount(eg_eticket.etNumber)),0)) +  '张电子客票.总费用:' + convert(varchar,isnull(sum(eg_eticket.TotalFC),0)) + '.总基建:' + convert(varchar,isnull(sum(eg_eticket.numBasePrc),0)) + '.总燃油' + convert(varchar,isnull(sum(eg_eticket.numOilPrc),0)) ) as UserID ,'' as vcUserTitle,'' as OperateTime, '' as etNumber,'' as PNR,'' as FlightNumber1,'' as Bunk1,'' as CityPair1,'' as FlightNumber2,'' as Bunk2,'' as CityPair2,'' as Date1,'' as Date2, 0 as TotalFC,'' as State,'' as DecFeeState,'' as Passenger, '' as IpId,'' as vcLoginName,'' as OfficeCode,0 as numBasePrc,0 as numOilPrc, 0 as isOffline FROM dbo.eg_eticket INNER JOIN dbo.eg_user ON dbo.eg_eticket.UserID = dbo.eg_user.vcLoginName where eg_user.numAgentId like '{1}%' and eg_user.numAgentId like '{2}%' and eg_eticket.OperateTime > CONVERT(DATETIME, '{3}') AND  eg_eticket.OperateTime < CONVERT(DATETIME, '{4}')  union SELECT '{5}' as agentid, eg_eticket.receiptNumber, eg_eticket.TicketID,eg_eticket.UserID,eg_user.vcUserTitle as vcUserTitle,convert(varchar,eg_eticket.OperateTime,120) as OperateTime,eg_eticket.etNumber,eg_eticket.PNR,eg_eticket.FlightNumber1,eg_eticket.Bunk1,eg_eticket.CityPair1,eg_eticket.FlightNumber2, eg_eticket.Bunk2, eg_eticket.CityPair2, eg_eticket.Date1, eg_eticket.Date2, eg_eticket.TotalFC, eg_eticket.State, eg_eticket.DecFeeState, eg_eticket.Passenger, eg_eticket.IpId,dbo.eg_user.vcLoginName AS vcLoginName,dbo.eg_eticket.IpId AS OfficeCode,eg_eticket.numBasePrc,eg_eticket.numOilPrc ,eg_eticket.isOffline FROM dbo.eg_eticket INNER JOIN dbo.eg_user ON dbo.eg_eticket.UserID = dbo.eg_user.vcLoginName where eg_user.numAgentId like '{6}%' and eg_eticket.OperateTime > CONVERT(DATETIME, '{7}') AND  eg_eticket.OperateTime < CONVERT(DATETIME, '{8}') ", numAgentId, numAgentId, numAgentId, startdate, enddate, numAgentId, numAgentId, startdate, enddate);
                ds = co.GetDataSet(Sql);
                MyExcel me = new MyExcel();
                string path = "E:\\www\\Excel\\" + us + DateTime.Now.ToShortDateString() + DateTime.Now.Minute.ToString()+ ".xls";
                me.CreateExcel(ds, path);
                strRet = "<eg><cm>ReGetEticketExcel</cm><result>"+path+"</result></eg>";
            }
            catch(Exception ea)
            {
                strRet = ea.Message;
            }
            return strRet;
        }
        //得到服务器地址与端口号 wsl add
        public string setIPID(string ipid)
        {
            NewPara np = new NewPara();
            XmlDocument doc = np.getRoot();
            np.AddPara("cm", "ret_setIPID");
            string sql = "";
            Conn cn = null;
            string strRet = "";
            try
            {
                cn = new Conn();
                sql = "SELECT a.vcSignCode, b.vcSrvIP, b.vcSrvPort FROM eg_SrvIps a INNER JOIN eg_Srv b ON a.numSrvId = b.numSrvId WHERE (a.IpId = '" + ipid + "')";
                DataSet ds = cn.GetDataSet(sql);
                if (ds.Tables[0].Rows.Count == 1)
                {
                    strRet = (ds.Tables[0].Rows[0]["vcSignCode"].ToString().Trim() + ";");
                    strRet += (ds.Tables[0].Rows[0]["vcSrvIP"].ToString().Trim() + ";");
                    strRet += (ds.Tables[0].Rows[0]["vcSrvPort"].ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                gs.util.func.Write("setIPID is err sql=" + sql + ex.Message);
                throw ex;
            }
            finally
            {
                cn.close();
            }
            np.AddPara("getIPID", strRet);
            return np.GetXML();
        }
        //得到所有服务器地址与端口号 wsl add
        public string getIPID()
        {
            NewPara np = new NewPara();
            XmlDocument doc = np.getRoot();
            np.AddPara("cm", "ret_getIPID");
            string sql = "";
            Conn cn = null;
            string strRet = "";
            try
            {
                cn = new Conn();
                sql = "SELECT a.IpId,a.vcSignCode, b.vcSrvIP, b.vcSrvPort FROM eg_SrvIps a INNER JOIN eg_Srv b ON a.numSrvId = b.numSrvId ";
                DataSet ds = cn.GetDataSet(sql);
                for (int i = 0; i < ds.Tables[0].Rows.Count;i++ )
                {
                    strRet += (ds.Tables[0].Rows[i]["IpId"].ToString().Trim() + ";");
                    strRet += (ds.Tables[0].Rows[i]["vcSignCode"].ToString().Trim() + ";");
                    strRet += (ds.Tables[0].Rows[i]["vcSrvIP"].ToString().Trim() + ";");
                    strRet += (ds.Tables[0].Rows[i]["vcSrvPort"].ToString().Trim()+"#");
                }
            }
            catch (Exception ex)
            {
                gs.util.func.Write("getIPID is err sql=" + sql + ex.Message);
                throw ex;
            }
            finally
            {
                cn.close();
            }
            np.AddPara("getIPID", strRet);
            return np.GetXML();
        }


	}
}



