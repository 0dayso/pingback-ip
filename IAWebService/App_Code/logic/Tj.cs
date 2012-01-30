using System;
using System.Data;
using gs.DataBase;

namespace logic
{
	/// <summary>
	/// Tj 的摘要说明。
	/// </summary>
	public class Tj
	{
		public Tj()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public DataSet getTjRs(string p_strAgentId,string p_strIsAll)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "";
				if(p_strIsAll == "0")
				{
					strSql = "SELECT dbo.eg_PolicyCity.vcCityName AS vcSrc, eg_PolicyCity_1.vcCityName AS vcDst, dbo.eg_Tj.* FROM dbo.eg_Tj INNER JOIN dbo.eg_PolicyCity ON dbo.eg_Tj.vcSrcCode = dbo.eg_PolicyCity.vcCityCode INNER JOIN dbo.eg_PolicyCity eg_PolicyCity_1 ON dbo.eg_Tj.vcDstCode = eg_PolicyCity_1.vcCityCode where numAgentId='" + p_strAgentId.Trim() + "'";
				}

				if(p_strIsAll == "1")
				{
					strSql = "SELECT dbo.eg_PolicyCity.vcCityName AS vcSrc, eg_PolicyCity_1.vcCityName AS vcDst, dbo.eg_Tj.* FROM dbo.eg_Tj INNER JOIN dbo.eg_PolicyCity ON dbo.eg_Tj.vcSrcCode = dbo.eg_PolicyCity.vcCityCode INNER JOIN dbo.eg_PolicyCity eg_PolicyCity_1 ON dbo.eg_Tj.vcDstCode = eg_PolicyCity_1.vcCityCode where numAgentId='" + p_strAgentId.Trim() + "' and numShow=1";
				}

				if(p_strIsAll == "2")
				{
					strSql = "SELECT dbo.eg_PolicyCity.vcCityName AS vcSrc, eg_PolicyCity_1.vcCityName AS vcDst, dbo.eg_Tj.* FROM dbo.eg_Tj INNER JOIN dbo.eg_PolicyCity ON dbo.eg_Tj.vcSrcCode = dbo.eg_PolicyCity.vcCityCode INNER JOIN dbo.eg_PolicyCity eg_PolicyCity_1 ON dbo.eg_Tj.vcDstCode = eg_PolicyCity_1.vcCityCode where numAgentId='" + p_strAgentId.Trim() + "' and numShow=2";
				}
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("logic.Tj.getTjRs is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}
	}
}
