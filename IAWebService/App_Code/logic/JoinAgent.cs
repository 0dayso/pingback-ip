using System;
using System.Data;
using gs.DataBase;

namespace logic
{
	/// <summary>
	/// Agent 的摘要说明。
	/// </summary>
	public class JoinAgent
	{
		public JoinAgent()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 删除一个代理商
		/// </summary>
		/// <param name="p_strAgentId"></param>
		/// <returns></returns>
		public bool delAgent(string p_strAgentId)
		{
			bool bRet = false;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "delete from eg_agent where numAgentId=" + p_strAgentId;
				cn.Update(strSql);
				bRet = true;
			}
			catch(Exception ex)
			{
				gs.util.func.Write("delAgent is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}

		/// <summary>
		/// 检查是否有门市和用户对应，如果有则返回真
		/// </summary>
		/// <param name="p_strAgentId"></param>
		/// <returns></returns>
		public bool hasSalsAndUser(string p_strAgentId)
		{
			bool bRet = false;

			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select * from eg_user where numAgentId=" + p_strAgentId;
				DataSet ds = cn.GetDataSet(strSql);
				if(ds.Tables[0].Rows.Count > 0)
				{
					bRet = true;
				}

				strSql = "select * from eg_sals where numAgentId=" + p_strAgentId;
				ds = cn.GetDataSet(strSql);
				if(ds.Tables[0].Rows.Count > 0)
				{
					bRet = true;
				}

			}
			catch(Exception ex)
			{
				gs.util.func.Write("hasSalsAndUser is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}

		/// <summary>
		/// 增加一个供应商
		/// </summary>
		/// <param name="p_strAgentName"></param>
		/// <param name="p_strIp"></param>
		/// <param name="p_strDNS"></param>
		/// <param name="p_strUserCount"></param>
		/// <returns></returns>
		public bool newAgent(string p_strAgentName,string p_strIp,string p_strDNS,string p_strUserCount)
		{
			Conn cn = null;
			bool bRet = false;
			try
			{
				cn = new Conn();
				Top3 tp = new Top3(cn); 
				
				tp.AddRow("vcAgentName","c",p_strAgentName);
				tp.AddRow("vcWebIp","c",p_strIp);
				tp.AddRow("vcDNS","c",p_strDNS);
				tp.AddRow("numUserCount","i",p_strUserCount);
				tp.AddRow("dtRegDate","dt",System.DateTime.Now.ToLongTimeString());
			
				if(tp.AddNewRec("eg_agent"))
				{
					bRet = true;
				}

			}
			catch(Exception ex)
			{
				gs.util.func.Write("newAgent is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}

		/// <summary>
		/// 修改一个代理商的信息
		/// </summary>
		/// <param name="p_strAgentId"></param>
		/// <param name="p_strAgentName"></param>
		/// <param name="p_strIp"></param>
		/// <param name="p_strDNS"></param>
		/// <param name="p_strUserCount"></param>
		/// <returns></returns>
		public bool updateAgent(string p_strAgentId,string p_strAgentName,string p_strIp,string p_strDNS,string p_strUserCount)
		{
			Conn cn = null;
			bool bRet = false;
			try
			{
				cn = new Conn();
				Top3 tp = new Top3(cn); 
				
				tp.AddRow("vcAgentName","c",p_strAgentName);
				tp.AddRow("vcWebIp","c",p_strIp);
				tp.AddRow("vcDNS","c",p_strDNS);
				tp.AddRow("numUserCount","i",p_strUserCount);
				tp.AddRow("dtRegDate","dt",System.DateTime.Now.ToLongTimeString());
							
				if(tp.Update("eg_agent","numAgentId",p_strAgentId))
				{
					bRet = true;
				}

			}
			catch(Exception ex)
			{
				gs.util.func.Write("updateAgent is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}

		/// <summary>
		/// 代理商自己修改自己的信息
		/// </summary>
		/// <param name="p_strAgentId"></param>
		/// <param name="p_strAgentName"></param>
		/// <param name="p_strIp"></param>
		/// <param name="p_strDNS"></param>
		/// <returns></returns>
		public bool updateAgent(string p_strAgentId,string p_strAgentName,string p_strIp,string p_strDNS)
		{
			Conn cn = null;
			bool bRet = false;
			try
			{
				cn = new Conn();
				Top3 tp = new Top3(cn); 
				
				tp.AddRow("vcAgentName","c",p_strAgentName);
				tp.AddRow("vcWebIp","c",p_strIp);
				tp.AddRow("vcDNS","c",p_strDNS);
				tp.AddRow("dtRegDate","dt",System.DateTime.Now.ToLongTimeString());
							
				if(tp.Update("eg_agent","numAgentId",p_strAgentId))
				{
					bRet = true;
				}

			}
			catch(Exception ex)
			{
				gs.util.func.Write("updateAgent is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}

		/// <summary>
		/// 得到代理商信息
		/// </summary>
		/// <param name="p_strAgentId"></param>
		/// <returns></returns>
		public DataSet getAgentInfo(string p_strAgentId)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select * from eg_agent where numAgentId='" + p_strAgentId.Trim() + "'";
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("getAgentInfo is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}

		/// <summary>
		/// 为代理商p_strAgentId增加一个新的IPp_strIp
		/// </summary>
		/// <param name="p_strAgentId"></param>
		/// <param name="p_strIp"></param>
		/// <returns></returns>
		public bool newIP(string p_strAgentId,string p_strIp)
		{
			Conn cn = null;
			bool bRet = false;
			try
			{
				cn = new Conn();
				Top3 tp = new Top3(cn); 
				
				tp.AddRow("numAgentId","i",p_strAgentId);
				tp.AddRow("vcLocalIP","c",p_strIp);
				
				if(tp.AddNewRec("eg_agentIps"))
				{
					bRet = true;
				}

			}
			catch(Exception ex)
			{
				gs.util.func.Write("newIP is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}

		/// <summary>
		/// 删除代理商和民航的一个IP
		/// </summary>
		/// <param name="p_strIPId"></param>
		/// <returns></returns>
		public bool delAgentIP(string p_strIPId)
		{
			bool bRet = false;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "delete from eg_agentIps where IpId=" + p_strIPId;
				cn.Update(strSql);
				bRet = true;
			}
			catch(Exception ex)
			{
				gs.util.func.Write("delAgentIP is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}

		/// <summary>
		/// 得到IP的信息
		/// </summary>
		/// <param name="p_strIPId"></param>
		/// <returns></returns>
		public DataSet getIpInfo(string p_strIPId)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select * from eg_agentIps where IpId=" + p_strIPId;
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("getIpInfo is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}

		/// <summary>
		/// 修改代理商的IP
		/// </summary>
		/// <param name="p_strIPId"></param>
		/// <param name="p_strIp"></param>
		/// <returns></returns>
		public bool updateAgentIP(string p_strIPId,string p_strIp)
		{
			Conn cn = null;
			bool bRet = false;
			try
			{
				cn = new Conn();
				/*Top3 tp = new Top3(cn); 
				
				tp.AddRow("vcLocalIP","c",p_strIp);
							
				if(tp.Update("eg_agentIps","IpId",p_strIPId))
				{
					bRet = true;
				}*/
				cn.Update("update eg_agentIps set vcLocalIP='" + p_strIp + "' where IpId=" + p_strIPId);

			}
			catch(Exception ex)
			{
				gs.util.func.Write("newAgent is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}

		/// <summary>
		/// 得到带来商可以使用的指令
		/// </summary>
		/// <param name="p_strAgentId"></param>
		/// <returns></returns>
		public DataSet getAgentCms(string p_strAgentId)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select * from eg_agentCm where numAgentId=" + p_strAgentId;
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("getAgentCms is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}

		/// <summary>
		/// 得到代理商的所有可销售票种类
		/// </summary>
		/// <param name="p_strAgentId"></param>
		/// <returns></returns>
		public DataSet getAgentTks(string p_strAgentId)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select * from eg_agentTkType where numAgentId=" + p_strAgentId;
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("getAgentTks is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}

		/// <summary>
		/// 删除代理商的一个门市
		/// </summary>
		/// <param name="p_strSalId"></param>
		/// <returns></returns>
		public bool delSal(string p_strSalId)
		{
			Conn cn = null;
			bool bRet = false;
			try
			{
				cn = new Conn();
				
				cn.Update("delete from eg_sals where numSalsId=" + p_strSalId);
				bRet = true;

			}
			catch(Exception ex)
			{
				gs.util.func.Write("delSal is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}

		/// <summary>
		/// 得到门市的信息
		/// </summary>
		/// <param name="p_strSalId"></param>
		/// <returns></returns>
		public DataSet getSalInfo(string p_strSalId)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select * from eg_sals where numSalsId=" + p_strSalId;
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("getSalInfo is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}

		public bool newSal(string p_strAgentId,string p_strSalName)
		{
			Conn cn = null;
			bool bRet = false;
			try
			{
				cn = new Conn();
				Top3 tp = new Top3(cn); 
				
				tp.AddRow("numAgentId","i",p_strAgentId);
				tp.AddRow("vcSalsName","c",p_strSalName);
				
				if(tp.AddNewRec("eg_sals"))
				{
					bRet = true;
				}

			}
			catch(Exception ex)
			{
				gs.util.func.Write("newSal is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}

		/// <summary>
		/// 修改门市的资料
		/// </summary>
		/// <param name="p_strSalId"></param>
		/// <param name="p_strSalName"></param>
		/// <returns></returns>
		public bool updateSal(string p_strSalId,string p_strSalName)
		{
			Conn cn = null;
			bool bRet = false;
			try
			{
				cn = new Conn();
				Top3 tp = new Top3(cn); 
				
				tp.AddRow("vcSalsName","c",p_strSalName);
							
				if(tp.Update("eg_sals","numSalsId",p_strSalId))
				{
					bRet = true;
				}

			}
			catch(Exception ex)
			{
				gs.util.func.Write("updateSal is err" + ex.Message);
				throw ex;
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}

		/// <summary>
		/// 得到代理商可操作模块
		/// </summary>
		/// <param name="p_strAgentId"></param>
		/// <returns></returns>
		public DataSet getAgentRights(string p_strAgentId)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select * from eg_agentModu where numAgentId='" + p_strAgentId + "'";
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("getAgentRights is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}

		/// <summary>
		/// 检测代理是否还可以增加用户
		/// </summary>
		/// <param name="p_strAgentId"></param>
		/// <returns></returns>
		public bool getAgentOverUsercountLimt(Conn cn,string p_strAgentId)
		{
			DataSet rsRet = null;
			bool bRet = false;
			//Conn cn = null;
			try
			{
				/*cn = new Conn();
				string strSql = "select * from eg_agent where numAgentId='" + p_strAgentId + "'";
				rsRet = cn.GetDataSet(strSql);
				int iLimtCount = Int32.Parse(rsRet.Tables[0].Rows[0]["numUserCount"].ToString());
				strSql = "select * from eg_user where numAgentId like '" + p_strAgentId + "%'";
				rsRet = cn.GetDataSet(strSql);
				int iCount = rsRet.Tables[0].Rows.Count;
				if(iCount+1 <= iLimtCount)
				{//如果该代理商的当前用户量加一以后仍然不大于代理商用户限制,则允许增加新用户
					bRet = true;
				}*/
				
				//为配合二级代理商专门修改的，待测试
				//cn = new Conn();
				string strSql = "select * from eg_agent where numAgentId='" + p_strAgentId + "'";
				rsRet = cn.GetDataSet(strSql);
				int iLimtCount = Int32.Parse(rsRet.Tables[0].Rows[0]["numUserCount"].ToString());

				//计算出所有的已经使用的用户量＝本代理商分配的用户量＋下一级所有代理商的用户量配额
				strSql = "select (  (select count(*) from eg_user where numAgentId='" + p_strAgentId + "') + (select isnull(sum(numUserCount),0) from eg_agent where numAgentId like '" + p_strAgentId + "____' and numAgentId<>'" + p_strAgentId + "')  ) as cnt";
				rsRet = cn.GetDataSet(strSql);
				int iCount = Int32.Parse(rsRet.Tables[0].Rows[0]["cnt"].ToString());

				if(iCount < iLimtCount)
				{//如果该代理商的当前用户量加一以后仍然不大于代理商用户限制,则允许增加新用户
					bRet = true;
				}
				
			}
			catch(Exception ex)
			{
				gs.util.func.Write("getAgentMaxUserCount is err" + ex.Message);
				throw ex;
			}
			finally
			{
				//cn.close();
			}
			return bRet;
		}


		/// <summary>
		/// 根据代理商的DNS得到所有的服务器所有的IP信息和发出包信息与SIGNCODE信息
		/// </summary>
		/// <param name="p_strAgentDNS"></param>
		/// <returns></returns>
		public string getAgentSrvInfo(string p_strAgentId)
		{
			/*string strRet = "";
			Conn cn = null;
			try
			{
				DataSet rsRet = null;
				cn = new Conn();
				string strSql = "select * from eg_agentIps where numAgentId=" + p_strAgentId;
				rsRet = cn.GetDataSet(strSql);
				for(int iTmp=0;iTmp<rsRet.Tables[0].Rows.Count;iTmp++)
				{
					string strRow = rsRet.Tables[0].Rows[iTmp]["vcLocalIP"].ToString().Trim() + "~" + rsRet.Tables[0].Rows[iTmp]["vcSrvIP"].ToString().Trim() + "~" + rsRet.Tables[0].Rows[iTmp]["vcLocalPort"].ToString().Trim() + "~" + rsRet.Tables[0].Rows[iTmp]["vcSignCode"].ToString().Trim() + "~" + rsRet.Tables[0].Rows[iTmp]["vcSentPack"].ToString().Trim();
					strRet = strRet + strRow + "$";
				}
				strRet = strRet.Substring(0,strRet.Length-1);
				strRet = "$" + strRet;

			}
			catch(Exception ex)
			{
				gs.util.func.Write("getAgentCms is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return strRet;*/

			string strRet = "";
			Conn cn = null;
			try
			{
				DataSet rsRet = null;
				cn = new Conn();
				string strSql = "select * from eg_SrvIps where numSrvId=" + p_strAgentId + " and numStop=0";
				rsRet = cn.GetDataSet(strSql);
				for(int iTmp=0;iTmp<rsRet.Tables[0].Rows.Count;iTmp++)
				{
					//string strRow = rsRet.Tables[0].Rows[iTmp]["vcLocalIP"].ToString().Trim() + "~" + rsRet.Tables[0].Rows[iTmp]["vcSrvIP"].ToString().Trim() + "~" + rsRet.Tables[0].Rows[iTmp]["vcLocalPort"].ToString().Trim() + "~" + rsRet.Tables[0].Rows[iTmp]["vcSignCode"].ToString().Trim() + "~" + rsRet.Tables[0].Rows[iTmp]["vcSentPack"].ToString().Trim();
					string strRow = rsRet.Tables[0].Rows[iTmp]["IpId"].ToString().Trim() + "~" + rsRet.Tables[0].Rows[iTmp]["vcLocalIP"].ToString().Trim() + "~" + rsRet.Tables[0].Rows[iTmp]["vcSrvIP"].ToString().Trim() + "~" + rsRet.Tables[0].Rows[iTmp]["vcLocalPort"].ToString().Trim() + "~" + rsRet.Tables[0].Rows[iTmp]["vcSignCode"].ToString().Trim() + "~" + rsRet.Tables[0].Rows[iTmp]["vcSentPack"].ToString().Trim() + "~" + rsRet.Tables[0].Rows[iTmp]["vcActNo"].ToString().Trim();
					strRet = strRet + strRow + "$";
				}
				strRet = strRet.Substring(0,strRet.Length-1);
				strRet = "$" + strRet;

			}
			catch(Exception ex)
			{
				gs.util.func.Write("getAgentCms is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return strRet;

		}

		
	}
}
