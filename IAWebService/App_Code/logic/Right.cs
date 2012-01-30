using System;
using gs.DataBase;
using System.Data;

namespace logic
{
	/// <summary>
	/// Right 的摘要说明。
	/// </summary>
	public class Right
	{
		/// <summary>
		/// 检查是否合法用户
		/// </summary>
		/// <param name="p_strUserName"></param>
		/// <param name="p_strPwd"></param>
		/// <returns></returns>
		public static bool IsValidUser(string p_strUserName,string p_strPwd)
		{
			bool bRet = false;
			Conn cn = null;
			try
			{
				gs.util.DES des = new gs.util.DES();
				string strPwd = des.EncryptString(p_strPwd,gs.util.StringTool.getPubKey(),gs.util.StringTool.getMixKey());

				cn = new Conn();
				string strSql = "select * from eg_user where vcLoginName='" + p_strUserName + "' and vcPass='" + strPwd + "'";
				DataSet ds = cn.GetDataSet(strSql);
				if(ds.Tables[0].Rows.Count > 0)
				{
					bRet = true;
				}

			}
			finally
			{
				cn.close();
			}
			return bRet;
		}

		/// <summary>
		/// 得到用户的角色
		/// </summary>
		/// <param name="p_strUserName"></param>
		/// <returns></returns>
		public static int getRole(string p_strUserName)
		{
			int iRet = 0;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select * from eg_user where vcLoginName='" + p_strUserName + "'";
				DataSet ds = cn.GetDataSet(strSql);
				if(ds.Tables[0].Rows.Count > 0)
				{
					iRet = Int32.Parse(ds.Tables[0].Rows[0]["numRoleId"].ToString());
				}

			}
			finally
			{
				cn.close();
			}
			return iRet;
		}

        public static int getRole(Conn cn,string p_strUserName)
        {
            int iRet = 0;
            try
            {
                string strSql = "select * from eg_user where vcLoginName='" + p_strUserName + "'";
                DataSet ds = cn.GetDataSet(strSql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    iRet = Int32.Parse(ds.Tables[0].Rows[0]["numRoleId"].ToString());
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            return iRet;
        }

		/// <summary>
		/// 通过用户编号得到角色编号
		/// </summary>
		/// <param name="p_strUserId"></param>
		/// <returns></returns>
		public static int getRoleById(string p_strUserId)
		{
			int iRet = 0;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select * from eg_user where numUserId=" + p_strUserId;
				DataSet ds = cn.GetDataSet(strSql);
				if(ds.Tables[0].Rows.Count > 0)
				{
					iRet = Int32.Parse(ds.Tables[0].Rows[0]["numRoleId"].ToString());
				}

			}
			finally
			{
				cn.close();
			}
			return iRet;
		}

		/// <summary>
		/// 得到用户对应的代理商
		/// </summary>
		/// <param name="p_strUserName"></param>
		/// <returns></returns>
		public static string getAgent(string p_strUserName)
		{
			string strRet = "";
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select * from eg_user where vcLoginName='" + p_strUserName + "'";
				DataSet ds = cn.GetDataSet(strSql);
				if(ds.Tables[0].Rows.Count > 0)
				{
					strRet = ds.Tables[0].Rows[0]["numAgentId"].ToString().Trim();
				}

			}
			finally
			{
				cn.close();
			}
			return strRet;
		}

        /// <summary>
        /// 得到用户对应的代理商
        /// </summary>
        /// <param name="p_strUserName"></param>
        /// <returns></returns>
        public static string getAgent(Conn cn,string p_strUserName)
        {
            string strRet = "";
            
            try
            {
                string strSql = "select * from eg_user where vcLoginName='" + p_strUserName + "'";
                DataSet ds = cn.GetDataSet(strSql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    strRet = ds.Tables[0].Rows[0]["numAgentId"].ToString().Trim();
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            return strRet;
        }

		/// <summary>
		/// 根据帐号得到USERID
		/// </summary>
		/// <param name="p_strUserName"></param>
		/// <returns></returns>
		public static string getUserId(string p_strUserName)
		{
			string strRet = "";
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select * from eg_user where vcLoginName='" + p_strUserName + "'";
				DataSet ds = cn.GetDataSet(strSql);
				if(ds.Tables[0].Rows.Count > 0)
				{
					strRet = ds.Tables[0].Rows[0]["numUserId"].ToString().Trim();
				}

			}
			finally
			{
				cn.close();
			}
			return strRet;
		}

		public static string getEtkUserName(string pnr)
		{
			string strRet = "";
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select * from eg_eticket where Pnr='" + pnr + "' and datediff(day,OperateTime,getdate())<5";
				//gs.util.func.Write("getUserName is =" + strSql);
				DataSet ds = cn.GetDataSet(strSql);
				if(ds.Tables[0].Rows.Count > 0)
				{
					strRet = ds.Tables[0].Rows[0]["UserId"].ToString().Trim();
				}

			}
			catch(Exception ex)
			{
				gs.util.func.Write("Right.getEtkUserName is ett =" + ex.Message);
				throw ex;
			}
			finally
			{
				cn.close();
			}
			return strRet;
		}

		/// <summary>
		/// 得到用户信息
		/// </summary>
		/// <param name="p_strUserName"></param>
		/// <returns></returns>
		public static DataSet getUserInfo(string p_strUserName)
		{
			DataSet ds = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select * from eg_user where vcLoginName='" + p_strUserName + "'";
				ds = cn.GetDataSet(strSql);
			}
			finally
			{
				cn.close();
			}
			return ds;
		}

		public static string getUserNameById(string p_strUserId)
		{
			string strRet = "";
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select * from eg_user where numUserId=" + p_strUserId;
				DataSet ds = cn.GetDataSet(strSql);
				if(ds.Tables[0].Rows.Count > 0)
				{
					strRet = ds.Tables[0].Rows[0]["vcLoginName"].ToString().Trim();
				}

			}
			finally
			{
				cn.close();
			}
			return strRet;
		}
	}
}
