using System;

namespace gs.util
{
	/// <summary>
	/// func 的摘要说明。
	/// </summary>
	public class func
	{
		private static string m_strLogFile = "";
		private static int iLog = 1;
		private static string m_strSmsUrl = "";
		private static string m_strIbeUrl = "";

		public func()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public static string getContextPath()
		{
			return System.Configuration.ConfigurationSettings.AppSettings["ContextPath"].ToString().Trim();
		}

		public static string getSmsUrl()
		{
			string strRet = "";
			if(m_strSmsUrl == "")
			{
				m_strSmsUrl = System.Configuration.ConfigurationSettings.AppSettings["SmsUrl"].ToString().Trim();
			}
			
			strRet = m_strSmsUrl;
			return strRet;
		}

		/// <summary>
		/// 输出调试信息
		/// </summary>
		/// <param name="p_StrMsg"></param>
		public static void Write(string p_strMsg)
		{
            Common.LogIt(p_strMsg);
		}

		public static string getIbeUrl()
		{
			string strRet = "";
			if(m_strIbeUrl == "")
			{
				m_strIbeUrl = System.Configuration.ConfigurationSettings.AppSettings["IBEUrl"].ToString().Trim();

			}
			strRet = m_strIbeUrl;
			return strRet;
		}
	}
}
