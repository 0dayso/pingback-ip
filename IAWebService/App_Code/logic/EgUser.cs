using System;
using System.Data;
using System.Xml;
using gs.para;
using gs.DataBase;

namespace logic
{
	/// <summary>
	/// User 的摘要说明。
	/// </summary>
	public class EgUser
	{
		public EgUser()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 得到代理商的所有用户
		/// </summary>
		/// <param name="p_strAgentId"></param>
		/// <param name="p_strUserName"></param>
		/// <returns></returns>
		public DataSet getAgentUsers(string p_strAgentId,string p_strQry)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "";
				if(p_strQry.Trim() == "")
					strSql = "SELECT eg_user.*,eg_sals.vcSalsName as vcSalsName,(CASE eg_user.numRoleId WHEN 0 THEN '普通用户' WHEN 1 THEN '代理商管理员' END) as vcRoleName  FROM eg_user left OUTER JOIN  eg_sals ON eg_user.numSalsId =eg_sals.numSalsId where eg_user.numAgentId=" + p_strAgentId + " and (eg_user.numRoleId=1 or eg_user.numRoleId=0)";
				else
					strSql = "SELECT eg_user.*,eg_sals.vcSalsName as vcSalsName,(CASE eg_user.numRoleId WHEN 0 THEN '普通用户' WHEN 1 THEN '代理商管理员' END) as vcRoleName  FROM eg_user left OUTER JOIN  eg_sals ON eg_user.numSalsId =eg_sals.numSalsId where eg_user.numAgentId=" + p_strAgentId + " and (eg_user.numRoleId=1 or eg_user.numRoleId=0) and (eg_user.vcUserTitle like '%" + p_strQry + "%' or eg_user.vcLoginName like '%" + p_strQry + "%' or eg_sals.vcSalsName like '%" + p_strQry + "%')";
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
		/// 删除一个用户
		/// </summary>
		/// <param name="p_strUserId"></param>
		/// <returns></returns>
		public bool delUser(string p_strUserId)
		{
			Conn cn = null;
			bool bRet = false;
			try
			{
				cn = new Conn();
				
				cn.Update("delete from eg_user where numUserId=" + p_strUserId);
				bRet = true;

			}
			catch(Exception ex)
			{
				gs.util.func.Write("delUser is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public DataSet getUserInfo(string p_strUserId)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select * from eg_user where numUserId=" + p_strUserId;
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("getUserInfo is err" + ex.Message);
				throw ex;
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}

		/// <summary>
		/// 检测这个登录名是否有人用过.如果有人用返回TRUE
		/// </summary>
		/// <param name="p_strLoginName"></param>
		/// <returns></returns>
		public bool isExistUser(string p_strLoginName)
		{
			bool bRet = false;

			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select * from eg_user where vcLoginName='" + p_strLoginName + "'";
				DataSet ds = cn.GetDataSet(strSql);
				if(ds.Tables[0].Rows.Count > 0)
				{
					bRet = true;
				}

			}
			catch(Exception ex)
			{
				gs.util.func.Write("isExistUser is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}

		/// <summary>
		/// 检测这个登录名是否有人用过.如果有人用返回TRUE
		/// </summary>
		/// <param name="p_strLoginName"></param>
		/// <returns></returns>
		public bool isExistUser(Conn cn,string p_strLoginName)
		{
			bool bRet = false;

			//Conn cn = null;
			try
			{
				//cn = new Conn();
				string strSql = "select * from eg_user where vcLoginName='" + p_strLoginName + "'";
				DataSet ds = cn.GetDataSet(strSql);
				if(ds.Tables[0].Rows.Count > 0)
				{
					bRet = true;
				}

			}
			catch(Exception ex)
			{
				gs.util.func.Write("isExistUser is err" + ex.Message);
				throw ex;
			}
			finally
			{
				//cn.close();
			}
			return bRet;
		}

		/// <summary>
		/// 得到用户所有的IP
		/// </summary>
		/// <param name="p_strUserId"></param>
		/// <returns></returns>
		public DataSet getUserIps(string p_strUserId)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "SELECT eg_userIps.numUserId, eg_userIps.IpId, eg_agentIps.vcLocalIP FROM eg_userIps INNER JOIN   eg_agentIps ON eg_userIps.IpId = eg_agentIps.IpId where eg_userIps.numUserId=" + p_strUserId;
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("getUserIps is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}


		/// <summary>
		/// 得到用户可以使用的指令集
		/// </summary>
		/// <param name="p_strUserId"></param>
		/// <returns></returns>
		public DataSet getUserCms(string p_strUserId)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "SELECT eg_userCm.numUserId, eg_userCm.numCmId, eg_Command.vcCm,eg_Command.vcCmDesc FROM eg_userCm INNER JOIN eg_Command ON eg_userCm.numCmId = eg_Command.numCmId where numUserId=" + p_strUserId;
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("getUserCms is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}

		/// <summary>
		/// 得到用户的可以出票种类.
		/// </summary>
		/// <param name="p_strUserId"></param>
		/// <returns></returns>
		public DataSet getUserTks(string p_strUserId)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "SELECT eg_userTks.numTickId, eg_userTks.numUserId,eg_ticketType.vcTickTypeName FROM eg_userTks INNER JOIN eg_ticketType ON eg_userTks.numTickId = eg_ticketType.numTickId where eg_userTks.numUserId=" + p_strUserId;
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("getUserTks is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}
		/// <summary>
		/// 得到用户可以操作的所有模块
		/// </summary>
		/// <param name="p_strUserId"></param>
		/// <returns></returns>
		public DataSet getUserModus(string p_strUserId)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select * from eg_userModu where numUserId=" + p_strUserId;
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("getUserModus is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}

		/// <summary>
		/// 得到所有代理商用户,系统管理员角色时使用
		/// </summary>
		/// <param name="p_strQry"></param>
		/// <returns></returns>
		public DataSet getSysUsers(string p_strQry)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "";
				if(p_strQry.Trim() == "")
					strSql = "SELECT dbo.eg_user.*, dbo.eg_sals.vcSalsName AS vcSalsName,(CASE eg_user.numRoleId WHEN 2 THEN '系统管理员' WHEN 1 THEN '代理商管理员' END) AS vcRoleName,dbo.eg_agent.vcAgentName AS vcAgentName FROM dbo.eg_user INNER JOIN  dbo.eg_agent ON dbo.eg_user.numAgentId = dbo.eg_agent.numAgentId LEFT OUTER JOIN dbo.eg_sals ON dbo.eg_user.numSalsId = dbo.eg_sals.numSalsId where (eg_user.numRoleId=1 or eg_user.numRoleId=2)";
				else
					strSql = "SELECT dbo.eg_user.*, dbo.eg_sals.vcSalsName AS vcSalsName,(CASE eg_user.numRoleId WHEN 2 THEN '系统管理员' WHEN 1 THEN '代理商管理员' END) AS vcRoleName,dbo.eg_agent.vcAgentName AS vcAgentName FROM dbo.eg_user INNER JOIN  dbo.eg_agent ON dbo.eg_user.numAgentId = dbo.eg_agent.numAgentId LEFT OUTER JOIN dbo.eg_sals ON dbo.eg_user.numSalsId = dbo.eg_sals.numSalsId where (eg_user.numRoleId=1 or eg_user.numRoleId=2) and (eg_user.vcUserTitle like '%" + p_strQry + "%' or eg_user.vcLoginName like '%" + p_strQry + "%' or eg_sals.vcSalsName like '%" + p_strQry + "%')";
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("getSysUsers is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}

		/// <summary>
		/// 得到用户所属于角色信息
		/// </summary>
		/// <param name="p_strUserCode"></param>
		/// <returns></returns>
		public DataSet getUserAgentInfo(string p_strUserCode)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "SELECT eg_agent.* FROM eg_agent INNER JOIN  eg_user ON eg_agent.numAgentId = eg_user.numAgentId where eg_user.vcLoginName='" + p_strUserCode + "'";
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("getUserAgentInfo is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}

        /// <summary>
        /// 得到用户顶级代理商信息
        /// </summary>
        /// <param name="p_strUserCode"></param>
        /// <returns></returns>
        public DataSet getUserRootAgentInfo(string p_strUserCode)
        {
            DataSet rsRet = null;
            Conn cn = null;
            try
            {
                cn = new Conn();
                string strSql = "SELECT eg_agent.* FROM eg_agent INNER JOIN  eg_user ON eg_agent.numAgentId = left(eg_user.numAgentId,8) where eg_user.vcLoginName='" + p_strUserCode + "'";
                rsRet = cn.GetDataSet(strSql);
            }
            catch (Exception ex)
            {
                gs.util.func.Write("getUserAgentInfo is err" + ex.Message);
            }
            finally
            {
                cn.close();
            }
            return rsRet;
        }

		/// <summary>
		/// 记录用户登入时间,用于后面刷新用户自己的初始化数据,同时生成PASSPORT
		/// </summary>
		/// <param name="p_strLoginName"></param>
		/// <returns>PASSPORT</returns>
		public string RecUserLogin(string p_strLoginName)
		{
			Conn cn = null;
			string strRet = "";
			
			try
			{
				cn = new Conn();
				string strSql = "";

				string strRetTmp = System.Guid.NewGuid().ToString();
				strRetTmp = strRetTmp.Replace("-","").ToUpper();

				strSql = "Select * from eg_onlineUser where vcLoginName='" + p_strLoginName.Trim() + "'";
				DataSet dsPort = cn.GetDataSet(strSql);

				if (dsPort.Tables[0].Rows.Count > 0)
				{
					strRetTmp = dsPort.Tables[0].Rows[0]["vcPassPort"].ToString();
				}

				strSql = "delete from eg_onlineUser where vcLoginName='" + p_strLoginName.Trim() + "'";
				cn.Update(strSql);

				Top3 tp = new Top3(cn); 
				
				
				string strCurTime = System.DateTime.Now.ToString();
				tp.AddRow("vcLoginName","c",p_strLoginName.Trim());
				tp.AddRow("dtLoginTime","dt",strCurTime);
				tp.AddRow("vcPassPort","c",strRetTmp);
							
				if(tp.AddNewRec("eg_onlineUser"))
				{
					strRet = strRetTmp;
				}

			}
			catch(Exception ex)
			{
				gs.util.func.Write("RecUserLogin is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return strRet;
		}

		/// <summary>
		/// 刷新自己的登入时间
		/// </summary>
		/// <param name="p_strLoginName"></param>
		/// <returns></returns>
		public bool RefreshSelf(string p_strLoginName)
		{
			Conn cn = null;
			bool bRet = false;
			try
			{
				cn = new Conn();
				
				string strCurTime = System.DateTime.Now.ToString();
				string strSql = "delete from eg_onlineUser where DATEDIFF(second,dtLoginTime,convert(datetime,'" + strCurTime + "'))>1800";
				cn.Update(strSql);

				string strUpdate = "update eg_onlineUser set dtLoginTime=convert(datetime,'" + strCurTime + "') where vcLoginName='" + p_strLoginName.Trim() + "'";
				cn.Update(strUpdate);
				bRet = true;
				/*Top3 tp = new Top3(cn); 
				
				tp.AddRow("dtLoginTime","dt",System.DateTime.Now.ToString());
							
				if(tp.Update("eg_onlineUser","vcLoginName",p_strLoginName.Trim()))
				{
					bRet = true;
				}*/

			}
			catch(Exception ex)
			{
				gs.util.func.Write("RefreshSelf is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}


		/// <summary>
		/// 用户退出删除用户
		/// </summary>
		/// <param name="p_strLoginName"></param>
		/// <returns></returns>
		public bool DelOnlineUser(string p_strLoginName)
		{
			Conn cn = null;
			bool bRet = false;
			try
			{
				cn = new Conn();
				
				string strSql = "delete from eg_onlineUser where vcLoginName='" + p_strLoginName.Trim() + "'";
				cn.Update(strSql);
				bRet = true;
			}
			catch(Exception ex)
			{
				gs.util.func.Write("DelExpireUser is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}

		/// <summary>
		/// 比较改用户的PASSPORT是否存在并且是正确的
		/// </summary>
		/// <param name="p_strUserName"></param>
		/// <param name="p_strPassport"></param>
		/// <returns></returns>
		/*public string CmpPassport(string p_strPassport)
		{
			
			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();
			np.AddPara("cm","RetCmpPassport");//在线用户表中删除用户
			
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select * from eg_onlineUser where vcPassPort='" + p_strPassport.Trim() + "'";
				DataSet ds = cn.GetDataSet(strSql);
				if(ds.Tables[0].Rows.Count > 0)
				{//比较成功,通过登入
					np.AddPara("CmpPassportStat","CmpSucc");
				}
				else
				{
					np.AddPara("CmpPassportStat","CmpFail");
				}

			}
			catch(Exception ex)
			{
				gs.util.func.Write("CmpPassport is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return np.GetXML();
		}*/

		public string CmpPassport(string p_strPassport)
		{
			string strRet = "f";
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select * from eg_onlineUser where vcPassPort='" + p_strPassport.Trim() + "'";
				DataSet ds = cn.GetDataSet(strSql);
				if(ds.Tables[0].Rows.Count > 0)
				{//比较成功,通过登入
					strRet = "s";
				}

			}
			catch(Exception ex)
			{
				gs.util.func.Write("CmpPassport is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return strRet;
		}

		/// <summary>
		/// 用户修改密码
		/// </summary>
		/// <param name="p_strLoginName"></param>
		/// <param name="p_strNewPassword"></param>
		/// <returns></returns>
		public bool ChgPassword(string p_strLoginName,string p_strNewPassword)
		{
			Conn cn = null;
			bool bRet = false;
			try
			{
				gs.util.DES des = new gs.util.DES();
				string strPwd = des.EncryptString(p_strNewPassword,gs.util.StringTool.getPubKey(),gs.util.StringTool.getMixKey());

				cn = new Conn();
				
				string strSql = "update eg_user set vcPass='" + strPwd + "' where vcLoginName='" + p_strLoginName + "'";
				cn.Update(strSql);
				bRet = true;
			}
			catch(Exception ex)
			{
				gs.util.func.Write("ChgPassword is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}

		/// <summary>
		/// 保存用户操作记录，可供管理员查询
		/// </summary>
		/// <param name="p_strUserCode"></param>
		/// <param name="p_strCm"></param>
		/// <param name="strCmType"></param>
		/// <param name="p_strTime"></param>
		/// <returns></returns>
		public bool SaveLogs(string p_strUserCode,string p_strCm,string p_strCmType,string p_strTime)
		{
			Conn cn = null;
			bool bRet = false;
			try
			{
				cn = new Conn();

				Top3 tp = new Top3(cn); 
				
				//string strCurTime = System.DateTime.Now.ToString();
				/*tp.AddRow("vcLoginName","c",p_strUserCode.Trim());
				tp.AddRow("vcOperStr","c",p_strCm);
				tp.AddRow("vcType","c",p_strCmType);
				tp.AddRow("dtTime","dt",p_strTime);
							
				if(tp.AddNewRec("eg_userLogs"))
				{
					bRet = true;
				}*/

				//string strIns = "insert into eg_userLogs(vcLoginName,vcOperStr,vcType,dtTime) values('" + p_strUserCode.Trim() + "','" + p_strCm + "','" + p_strCmType + "','" + p_strTime + "')";
				//cn.Update(strIns);
				bRet = true;

			}
			catch(Exception ex)
			{
				gs.util.func.Write("SaveLogs is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}

		/// <summary>
		/// 得到用户状态
		/// </summary>
		/// <param name="p_strUserId"></param>
		/// <returns></returns>
		public string getUserStat(string p_strUserId)
		{
			DataSet ds = getUserInfo(p_strUserId);
			return ds.Tables[0].Rows[0]["numStat"].ToString().Trim();
		}

        
		/// <summary>
		/// 检测用户是否已经过期
		/// </summary>
		/// <param name="p_strUserId"></param>
		/// <returns></returns>
		public bool isExpireUser(string p_strUserId)
		{
			bool bRet = false;
			try
			{
			
				DataSet ds = getUserInfo(p_strUserId);
            			
				if(ds.Tables[0].Rows[0]["dtExpire"] == null || ds.Tables[0].Rows[0]["dtExpire"].ToString().Trim() == "")
				{
					bRet = false;
				}
				else
				{
					DateTime dtExpire = System.DateTime.Parse(ds.Tables[0].Rows[0]["dtExpire"].ToString());
					if(DateTime.Now < dtExpire)
						bRet = false;
					else
						bRet = true;
				}
			}
			catch(Exception ex)
			{
				gs.util.func.Write("isExpireUser is err" + ex.Message);
				throw ex;
			}
			return bRet;
		}

		/// <summary>
		/// 得到用户所在代理商的状态,如果检查到用户的当前或上级代理商被暂停了,则该用户也不能登入系统
		/// </summary>
		/// <param name="?"></param>
		/// <returns></returns>
		public string getUserAgentStat(string p_strUser)
		{
			string strRet = "0";
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strAgentId = Right.getAgent(p_strUser).Trim();
				int iLen = strAgentId.Length / 4;
				
				for(int i=0;i<iLen;i++)
				{
					string strTmpAgentId = strAgentId.Substring(0,4*(i+1));
					string strSql = "select * from eg_agent where numAgentId='" + strTmpAgentId + "'";
					rsRet = cn.GetDataSet(strSql);

					string strStat = rsRet.Tables[0].Rows[0]["numStat"].ToString().Trim();
					if(strStat == "1")
					{//经过检查,可能代理商的某个上级或本级代理商状态被设为停止
						strRet = "1";
						break;
					}
				}

			}
			catch(Exception ex)
			{
				strRet = "1";
				gs.util.func.Write("getAgentInfo is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return strRet;
            //return "0";
		}

		/// <summary>
		/// 得到用户的配置和相关服务器信息
		/// </summary>
		/// <param name="p_strUserCode"></param>
		/// <returns></returns>
		public DataSet getUserLoginSrv(string p_strUserID)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "SELECT dbo.eg_SrvIps.*, dbo.eg_Srv.vcSrvName AS vcLoginSrvName,dbo.eg_Srv.vcSrvIP AS vcLoginSrvIp, dbo.eg_Srv.vcSrvDNS AS vcLoginDNS,dbo.eg_Srv.vcSrvPort AS vcLoginSrvPort " +
					" FROM dbo.eg_userIps INNER JOIN dbo.eg_SrvIps ON dbo.eg_userIps.IpId = dbo.eg_SrvIps.IpId INNER JOIN  dbo.eg_Srv ON dbo.eg_SrvIps.numSrvId = dbo.eg_Srv.numSrvId " +
					" where eg_userIps.numUserId=" + p_strUserID;
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("WS.EgUser.getUserLoginSrv is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}

		/// <summary>
		/// 得到用户能看到的所有代理商
		/// </summary>
		/// <param name="p_strUserName"></param>
		/// <returns></returns>
		public DataSet getUserAgents(string p_strUserName)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "SELECT * FROM eg_agent where numAgentId like (select ltrim(rtrim(numAgentId)) from eg_user where vcLoginName='" + p_strUserName.Trim() + "') + '%' order by numAgentId";
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("WS.EgUser.getUserAgents is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}

		/// <summary>
		/// 生成一个新的C客户
		/// </summary>
		/// <param name="p_strXml"></param>
		/// <returns></returns>
		public string NewCUser(string p_strXml)
		{
			string strRet = "";

			NewPara npGet = new NewPara(p_strXml.Trim());
			string strMerchId = npGet.FindTextByPath("//eg/MerchId").Trim();
			string strUserCode = npGet.FindTextByPath("//eg/User").Trim();
			string strUserTitle = npGet.FindTextByPath("//eg/UserTitle").Trim();
			string strUserPassWord = npGet.FindTextByPath("//eg/UserPassWord").Trim();

			string strSex = npGet.FindTextByPath("//eg/Sex").Trim();
			string strBirthday = npGet.FindTextByPath("//eg/Birthday").Trim();
			string strCrosAdr = npGet.FindTextByPath("//eg/CrosAdr").Trim();
			string strPostCode = npGet.FindTextByPath("//eg/PostCode").Trim();
			string strEmail = npGet.FindTextByPath("//eg/Email").Trim();
			string strTel = npGet.FindTextByPath("//eg/Tel").Trim();
			string strUserType = npGet.FindTextByPath("//eg/vcUserType").Trim();
			
			//NewPara npRet = new NewPara();
			//XmlDocument doc = npRet.getRoot();
			//npRet.AddPara("Flag","OverUserLimted");

			Conn cn = null;
			try
			{
				cn = new Conn();
				cn.beginTrans();
				
				//得到C客户设置信息
				DataSet dsMerch = cn.GetDataSet("select * from eg_AgentCUser where numAgentId='" + strMerchId + "'");
				string strCurAgentId = "";
				string strUserClass = "";
				string strNewUserId = "";
				if(dsMerch.Tables[0].Rows.Count > 0)
				{
					strCurAgentId = dsMerch.Tables[0].Rows[0]["numCurAgentId"].ToString().Trim();
					strUserClass = dsMerch.Tables[0].Rows[0]["vcUserClass"].ToString().Trim();
				}

				//计算用户名是否合法
				JoinAgent ag = new JoinAgent();
				bool bCanAdd = ag.getAgentOverUsercountLimt(cn,strCurAgentId);//检测是否超过最大用户数量
				bool bExistLoginName = isExistUser(cn,strUserCode);

				if(bExistLoginName)
				{//如果用户名存在
					strRet = "ReUserName";
				}
				else
				{
					if(bCanAdd)
					{
						gs.util.DES des = new gs.util.DES();
						string strPwd = des.EncryptString(strUserPassWord,gs.util.StringTool.getPubKey(),gs.util.StringTool.getMixKey());

						strNewUserId = logic.mytool.util.getId(cn,"eg_user");
						Top3 tp = new Top3(cn);                                                                  
			
						tp.AddRow("numUserId","i",strNewUserId);
						tp.AddRow("vcLoginName","c",strUserCode);
						tp.AddRow("vcPass","c",strPwd);
						tp.AddRow("vcUserTitle","c",strUserTitle);
						if(strSex != "")
							tp.AddRow("vcSex","c","男");
						else
							tp.AddRow("vcSex","c",strSex);
						tp.AddRow("numRoleId","i","0");
						tp.AddRow("numAgentId","c",strCurAgentId);
						tp.AddRow("vcTel","c",strTel);
						if(strBirthday != "")
							tp.AddRow("vcBirthday","c",strBirthday);
						else
							tp.AddRow("vcBirthday","c","2000-1-1");

						tp.AddRow("vcCrosAdr","c",strCrosAdr);
						tp.AddRow("vcPostCode","c",strPostCode);
						tp.AddRow("vcEmail","c",strEmail);
						tp.AddRow("dtRegTime","dt",System.DateTime.Now.ToLongTimeString());
						tp.AddRow("dtLastAcsTime","dt",System.DateTime.Now.ToLongTimeString());
						tp.AddRow("numUserStat","i","0");
						if(strUserType != "")
						{
							tp.AddRow("vcUserType","c",strUserType);
						}
						else
						{
							tp.AddRow("vcUserType","c","c");
						}
						
						tp.AddRow("numVal","d","0");
						tp.AddRow("numStat","i","0");
						
						if(tp.AddNewRec("eg_user"))
						{
							string strClassSql = "insert into eg_userCm(numUserId,numCmId) values(" + strNewUserId + ",'" + strUserClass + "')";
							cn.Update(strClassSql);
							string strRightSql = "insert into eg_userModu(numUserId,numModuId) select " + strNewUserId + ",numModuId from eg_AgentCUserModus where numAgentId='" + strMerchId + "'";
							cn.Update(strRightSql);

							strRet = "AddSucc";
						}
						else
						{
							strRet = "AddFail";
						}
					}
					else
					{
						strRet = "OverUserLimted";
					}
				}
				cn.commit();
			}
			catch(Exception ex)
			{
				gs.util.func.Write("NewCUser is err" + ex.Message);
				cn.rollback();
			}
			finally
			{
				cn.close();
			}

			return strRet;
		}


		/// <summary>
		/// C客户得到自己首页的一些参数
		/// </summary>
		/// <param name="p_strUserName"></param>
		/// <returns></returns>
		public string getCUserPageXml(string p_strAgentCode)
		{
			NewPara npRet = new NewPara();
			XmlDocument doc = npRet.getRoot();

			npRet.AddPara("cm","RetCUserFirstPage");
			XmlNode nodeAirs = npRet.AddPara("AirList","");
			XmlNode nodeCitys = npRet.AddPara("CityList","");

			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select '全部' as vcAirName,'' as vcAirCode union select vcAirName,vcAirCode from eg_PolicyAir order by vcAirCode";
				DataSet ds = cn.GetDataSet(strSql);

				for(int i=0;i<ds.Tables[0].Rows.Count;i++)
				{
					XmlNode subNodeNew = doc.CreateNode(XmlNodeType.Element,"AAIR","");

					XmlNode subIpNode = null;
					subIpNode = doc.CreateNode(XmlNodeType.Element,"AirName","");
					subIpNode.AppendChild(doc.CreateTextNode(ds.Tables[0].Rows[i]["vcAirName"].ToString().Trim()));
					subNodeNew.AppendChild(subIpNode);	//把变量值赋进去

					subIpNode = doc.CreateNode(XmlNodeType.Element,"AirCode","");
					subIpNode.AppendChild(doc.CreateTextNode(ds.Tables[0].Rows[i]["vcAirCode"].ToString().Trim()));
					subNodeNew.AppendChild(subIpNode);	

					nodeAirs.AppendChild(subNodeNew);	
				}

				strSql = "select vcCityPy + '-' + vcCityName as vcCityName,vcCityCode from eg_PolicyCity order by vcCityPy";
				ds = cn.GetDataSet(strSql);

				for(int i=0;i<ds.Tables[0].Rows.Count;i++)
				{
					XmlNode subNodeNew = doc.CreateNode(XmlNodeType.Element,"ACITY","");

					XmlNode subIpNode = null;
					subIpNode = doc.CreateNode(XmlNodeType.Element,"CityName","");
					subIpNode.AppendChild(doc.CreateTextNode(ds.Tables[0].Rows[i]["vcCityName"].ToString().Trim()));
					subNodeNew.AppendChild(subIpNode);	//把变量值赋进去

					subIpNode = doc.CreateNode(XmlNodeType.Element,"CityCode","");
					subIpNode.AppendChild(doc.CreateTextNode(ds.Tables[0].Rows[i]["vcCityCode"].ToString().Trim()));
					subNodeNew.AppendChild(subIpNode);	

					nodeCitys.AppendChild(subNodeNew);	
				}

				strSql = "select vcWebCUser from eg_AgentCUser where numAgentId='" + p_strAgentCode + "'";
				ds = cn.GetDataSet(strSql);
				string strWebUser = "";
				if(ds.Tables[0].Rows.Count > 0)
				{
					strWebUser = ds.Tables[0].Rows[0]["vcWebCUser"].ToString().Trim();
				}
				npRet.AddPara("WebUser",strWebUser);

				//计算当前用户所在代理商所在城市
				string strAgentCode = "";
				if(p_strAgentCode.Length > 8)
				{
					strAgentCode = p_strAgentCode.Substring(0,8);
				}
				else
				{
					strAgentCode = p_strAgentCode;
				}

				string strSqlCity = "SELECT vcCityCode FROM eg_agent where numAgentId ='" + strAgentCode + "'";
				//gs.util.func.Write("strSqlCity=" + strSqlCity);
				DataSet rsRet = cn.GetDataSet(strSqlCity);
				string strCityCode = rsRet.Tables[0].Rows[0]["vcCityCode"].ToString().Trim();	//得到用户所在城市
				rsRet.Clear();

				npRet.AddPara("InCity",strCityCode);


				
			}
			catch(Exception ex)
			{
				gs.util.func.Write("WS.EgUser.getCUserPageXml is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			
			return npRet.GetXML();
		}

		/// <summary>
		/// 通过用户名得到用户基本信息
		/// </summary>
		/// <param name="p_strUserName"></param>
		/// <returns></returns>
		public DataSet getUserInfoByUserName(string p_strUserName)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select * from eg_user where vcLoginName='" + p_strUserName.Trim() + "'";
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("getUserInfoByUserName is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}

	}
}




