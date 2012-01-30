using System;
using System.Data;
using gs.DataBase;

namespace logic
{
	/// <summary>
	/// EgMes 的摘要说明。
	/// </summary>
	public class EgMes
	{
		public EgMes()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 得到当前登入系统用户的最新消息
		/// </summary>
		/// <param name="p_strUserId">用户ID,不是帐户名</param>
		/// <param name="p_strAgentId">当前用户所在代理商</param>
		/// <returns></returns>
		public DataSet getNewMes(string p_strUserId,string p_strAgentId)
		{
			/*SELECT *
			FROM eg_PubMes
			where ((getdate()>dtBegTime and dtEndTime is null) or (getdate()>dtBegTime and getdate()<dtEndTime)) 
			and ( (rtrim(ltrim(vcObjAgents))='' and rtrim(ltrim(vcObjUsers))='') or (','+ltrim(rtrim(vcObjAgents)) like '%,0001001300030001,%') or (','+ltrim(rtrim(vcObjUsers))+',' like '%,502,%'))
			*/

			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "SELECT dbo.eg_PubMes.*, dbo.eg_user.vcUserTitle AS vcUserTitle FROM dbo.eg_user INNER JOIN  dbo.eg_PubMes ON dbo.eg_user.numUserId = dbo.eg_PubMes.numUserID " + 
				" where ((getdate()>eg_PubMes.dtBegTime and eg_PubMes.dtEndTime is null) or (getdate()>eg_PubMes.dtBegTime and getdate()<eg_PubMes.dtEndTime)) " +
				" and ( (rtrim(ltrim(eg_PubMes.vcObjAgents))='' and rtrim(ltrim(eg_PubMes.vcObjUsers))='') or (','+ltrim(rtrim(eg_PubMes.vcObjAgents)) like '%," + p_strAgentId.Trim() + ",%') or (','+ltrim(rtrim(eg_PubMes.vcObjUsers))+',' like '%," + p_strUserId.Trim() + ",%')) order by eg_PubMes.numMesID Desc";
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("WS.logic.EgMes.getNewMes is err" + ex.Message);
				throw ex;
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}
	}
}
