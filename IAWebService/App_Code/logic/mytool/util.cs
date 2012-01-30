using System;
using gs.DataBase;
using System.Data;

namespace logic.mytool
{
	/// <summary>
	/// util 的摘要说明。
	/// </summary>
	public class util
	{
		public util()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 得到主键
		/// </summary>
		/// <param name="p_strParaName"></param>
		/// <param name="p_strParaVal"></param>
		/// <returns></returns>
		public static string getId(string p_strParaName)
		{
			Conn cn = null;
			string strRet = "0";
			try
			{
				cn = new Conn();
				cn.beginTrans();
				string strSql = "select * from eg_ident where ParaName='" + p_strParaName + "'";
				DataSet ds = cn.GetDataSet(strSql);
				if(ds.Tables[0].Rows.Count > 0)
				{
					strSql = "update eg_ident set ParaVal=ParaVal+1 where ParaName='" + p_strParaName + "'";
					cn.Update(strSql);
					long iTmp = long.Parse(ds.Tables[0].Rows[0]["ParaVal"].ToString()) + 1;
					strRet = iTmp.ToString();
				}
				else
				{
					strSql = "insert into eg_ident(ParaName,ParaVal) values('" + p_strParaName + "',0)";
					cn.Update(strSql);
				}
				cn.commit();
			}
			catch(Exception ex)
			{
				cn.rollback();
				System.Console.Write(ex.Message);
			}
			finally
			{
				cn.close();
			}
			return strRet;
		}

		public static string getId(Conn cn,string p_strParaName)
		{
			
			string strRet = "0";
			try
			{

				string strSql = "select * from eg_ident where ParaName='" + p_strParaName + "'";
				DataSet ds = cn.GetDataSet(strSql);
				if(ds.Tables[0].Rows.Count > 0)
				{
					strSql = "update eg_ident set ParaVal=ParaVal+1 where ParaName='" + p_strParaName + "'";
					cn.Update(strSql);
					long iTmp = long.Parse(ds.Tables[0].Rows[0]["ParaVal"].ToString()) + 1;
					strRet = iTmp.ToString();
				}
				else
				{
					strSql = "insert into eg_ident(ParaName,ParaVal) values('" + p_strParaName + "',0)";
					cn.Update(strSql);
				}
			
			}
			catch(Exception ex)
			{
				
				System.Console.Write(ex.Message);
			}

			return strRet;
		}

		/// <summary>
		/// 得到SQL返回的信息
		/// </summary>
		/// <param name="p_strSql"></param>
		/// <returns></returns>
		public static DataSet getDs(string p_strSql)
		{
			Conn cn = null;
			DataSet dsRet = null;
			try
			{
				cn = new Conn();
				dsRet = cn.GetDataSet(p_strSql);
			}
			catch(Exception ex)
			{
				System.Console.Write("logic.mytool.util.getDs is error:" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return dsRet;
		}

		/// <summary>
		/// 执行SQL
		/// </summary>
		/// <param name="p_strSql"></param>
		/// <returns></returns>
		public static bool exeCm(string p_strSql)
		{
			Conn cn = null;
			bool bRet = false;
			try
			{
				cn = new Conn();
				cn.Update(p_strSql);
				bRet = true;
			}
			catch(Exception ex)
			{
				gs.util.func.Write("logic.mytool.util.exeCm is error:" + ex.Message);
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
