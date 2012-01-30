using System;

namespace gs.util
{
	/// <summary>
	/// StringTool 的摘要说明。
	/// </summary>
	public class StringTool
	{
		public StringTool()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 把字符窜按一定长度砍断,然后加入别的符号
		/// </summary>
		/// <param name="p_strSrc">原窜</param>
		/// <param name="p_iLen">多长字符砍断</param>
		/// <param name="p_strBeInstedChar">被替换字符窜</param>
		/// <returns></returns>
		public static string getCutString(string p_strSrc,int p_iLen,string p_strBeInstedChar)
		{
			string strRet = "";
			string strTitleBak = p_strSrc;
			string strTitle = p_strSrc;

			int iCutLen = p_iLen;
			if(strTitle.Length > iCutLen)
			{
				while(true)	
				{
					if(iCutLen> strTitleBak.Length)
					{
						strRet += strTitleBak;
						break;
					}
					string strPart = strTitleBak.Substring(0,iCutLen);
					strRet = strRet + strPart + p_strBeInstedChar;
					strTitleBak = strTitleBak.Substring(iCutLen);
				}
			}
			else
			{
				strRet = strTitle;
			}
			return strRet;
		}

		public static string getPubKey()
		{
			return "UuI6CX1zJ3HbIxUyOYCMUVgI9N5BBRUc";
		}

		public static string getMixKey()
		{
			return "+MtU0wDjHe4=";
		}

		public static string getSqlFilterStr(string p_str)
		{
			string strRet = " ";

            if(p_str != null && p_str.Trim() != "" )
			{
				strRet = p_str;
				strRet = strRet.Replace("'"," ");
				strRet = strRet.Replace("\""," ");
				strRet = strRet.Replace("."," ");
				strRet = strRet.Replace("("," ");
				strRet = strRet.Replace(")"," ");
				strRet = strRet.Replace("，"," ");
			}
			return strRet;

		}

	}
}
