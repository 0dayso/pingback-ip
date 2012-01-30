using System;

namespace gs.util
{
	/// <summary>
	/// StringTool ��ժҪ˵����
	/// </summary>
	public class StringTool
	{
		public StringTool()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ���ַ��ܰ�һ�����ȿ���,Ȼ������ķ���
		/// </summary>
		/// <param name="p_strSrc">ԭ��</param>
		/// <param name="p_iLen">�೤�ַ�����</param>
		/// <param name="p_strBeInstedChar">���滻�ַ���</param>
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
				strRet = strRet.Replace("��"," ");
			}
			return strRet;

		}

	}
}
