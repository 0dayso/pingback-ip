using System;
using System.Collections;

namespace gs.DataBase
{
	/// <summary>
	/// TableOp ��ժҪ˵����
	/// </summary>
	public class TableOp
	{
		private string m_strConn = "";
		private string m_strTableName = "";
		private string m_strErr = "";
		private Conn m_Conn = null;
		private IList m_Ary = null;  
        
		/// <summary>
		/// ������
		/// </summary>
		/// <param name="p_strConnStr">���ݿ������ַ���</param>
		public TableOp(string p_strConnStr)
		{
			m_Ary = null;
			m_strConn = p_strConnStr;
			m_Ary = new ArrayList();
		}

		/// <summary>
		/// TMEP
		/// </summary>
		public TableOp()
		{
			m_Ary = null;
			m_Ary = new ArrayList();
		}

		public TableOp(Conn p_Conn)
		{
			m_Conn = p_Conn;
			m_Ary = null;
			m_Ary = new ArrayList();
		}

		/// <summary>
		/// ���������������
		/// </summary>
		/// <param name="p_strErr">����ԭ��</param>
		private void AddErr(string p_strErr)
		{
			m_strErr += p_strErr.Trim();
		}

		public string GetErr()
		{
			return m_strErr;
		}

		/// <summary>
		/// �Ƿ�Ϊ����
		/// </summary>
		/// <param name="p_str"></param>
		/// <returns></returns>
		/*private bool IsInt(string p_str)
		{
			bool bRet = false;
			try
			{
				Int32.Parse(p_str);
				bRet = true;
			}
			catch(System.Exception ex)
			{
				bRet = false;
			}
			return bRet;
		}*/

		/// <summary>
		/// �Ƿ�Ϊʵ��
		/// </summary>
		/// <param name="p_str"></param>
		/// <returns></returns>
		/*private bool IsNumeric(string p_str)
		{
			bool bRet = false;
			try
			{
				Double.Parse(p_str);
				bRet = true;
			}
			catch(System.Exception ex)
			{
				bRet = false;
			}
			return bRet;
		}*/
		


		//����Ƿ�Ϊ����
		private bool IsInt(String p_str)
		{
			int iTmp = 0;
			bool bPass = false;
			int iLength = p_str.Length;
		
			if( iLength == 0)
			{
				return false;
			}
		
			bPass = true;
			char[] ary = p_str.ToCharArray();
			for(iTmp=0;iTmp<iLength;iTmp++)
			{
				char str = ary[iTmp];
				if(str<'0' || str>'9') 
				{
					bPass = false;
					break;
				}
			}
			return bPass;
		}
	
		//����Ƿ�Ϊ����
		private bool IsNumeric(string p_str)
		{
			int iTmp,iPoint;
			char str;
			bool bPass = false;
				
			bPass = true;
			iPoint = 0;
		
			if(p_str.Length==0)
			{
				return false;
			}
		
            if((p_str.Length == 1) && (p_str.Substring(0,1) == "."))
				return false;
			
			char[] ary = p_str.ToCharArray();
			for(iTmp=0;iTmp<p_str.Length;iTmp++)
			{
				str = ary[iTmp];
				if(str<'0' || str>'9')
				{
					if(str == '.')
					{
						iPoint++;
					}
					else
					{
						bPass = false;
						break;
					}
				}
			}
		
			//�������һ��С������Ϊ����
			if(bPass)
			{
				if(iPoint > 1)
				{
					bPass = false;
				}
				else
				{
					bPass = true;
				}
			}	
			return bPass;
		}



		/// <summary>
		/// ���ñ������ı���
		/// </summary>
		/// <param name="p_strTableName">����</param>
		public void SetTableName(string p_strTableName)
		{
			m_strTableName = p_strTableName;
		}

		/// <summary>
		/// ����һ��
		/// </summary>
		/// <param name="p_strFieldName">�ֶε�����</param>
		/// <param name="p_strFieldTitle">�ֶεı�ʾ</param>
		/// <param name="p_strFieldLen">�ֶγ���</param>
		/// <param name="p_strFieldType">�ֶ�����</param>
		/// <param name="p_strFieldValue">�ֶ�����</param>
		public void AddRow(string p_strFieldName,string p_strFieldTitle,string p_strFieldLen,string p_strFieldType,string p_strFieldValue)
		{
			string[] ary = new string[5];
			ary[0] = p_strFieldName;
			ary[1] = p_strFieldTitle;
			ary[2] = p_strFieldLen;
			ary[3] = p_strFieldType;
			ary[4] = p_strFieldValue;
			m_Ary.Add(ary); 
		}

		/// <summary>
		/// ����һ����¼�����ݿ���
		/// </summary>
		/// <returns>�����֤����������,���򷵻ؼ�</returns>
		public bool AddNewRec(string p_strTableName)
		{
			string strSql = "";
			string strFields = "";
			string strVals = "";
			int iFieldNum = 0;
			int iNum = 0;

			int iFieldLen = 0;
			string strFieldTitle = "";
			string strFieldName = "";

			string[] ary = null;
			string strVal = "";
			bool bRet = true;
			bool bExitFor = false;
    
			strSql = "insert into " + p_strTableName + "(";
			iFieldNum = m_Ary.Count;
 
			for(iNum=0;iNum<iFieldNum;iNum++)
			{
				ary = (string[])m_Ary[iNum];
				strVal = ary[4].Trim();
				iFieldLen = Int32.Parse(ary[2]);
				strFieldTitle = ary[1].Trim(); 
				strFieldName = ary[0].Trim(); 
				bExitFor = false;
				
				switch(ary[3].Trim())
				{
					case "c":
						strFields = strFields + strFieldName + ",";

						if(strVal == "")
						{
							strVal = " ";
						}
						else
						{
							if(strVal.Length > iFieldLen)
							{
								AddErr(strFieldTitle + "����" + "m_err");
								bRet = false;
								bExitFor = true;
							}
						}
						strVals = strVals + "'" + strVal + "',";
						break;

					case "i":
						if(IsInt(strVal))
						{
							if(strVal.Length > iFieldLen)
							{
								AddErr(strFieldTitle + "����" + "m_err");
								bRet = false;
								bExitFor = true;
							}
							else
							{
								strFields = strFields + strFieldName + ",";
								strVals = strVals + strVal + ",";
							}
						}
						else
						{
							AddErr(strFieldTitle + "ӦΪ����" + "m_err");
							bRet = false;
							bExitFor = true;
						}
						break;

					case "d":
						if(IsNumeric(strVal))
						{
							if(strVal.Length > iFieldLen)
							{
								AddErr(strFieldTitle + "����" + "m_err");
								bRet = false;
								bExitFor = true;
							}
							else
							{
								strFields = strFields + strFieldName + ",";
								strVals = strVals + strVal + ",";
							}
						}
						else
						{
							AddErr(strFieldTitle + "ӦΪ����" + "m_err");
							bRet = false;
							bExitFor = true;
						}
						break;

					case "t":
						strFields = strFields + strFieldName + ",";
						strVals = strVals + strVal + ",";
						break;
				}

				if(bExitFor)
				{
					//break;
				}
			}
       
			if(bRet)
			{
				strFields = strFields.Substring(0,strFields.Length-1);
				strVals = strVals.Substring(0,strVals.Length-1);
				strSql = strSql + strFields + ") values(" + strVals + ")";
				Conn conn = new Conn();
				bRet = conn.Update(strSql);
			}
			else
			{
				throw new Exception(GetErr());
			}
			return bRet;
		}

		/// <summary>
		/// �޸�һ����¼
		/// </summary>
		/// <param name="p_strKeyName">�ؼ��ֵ�����</param>
		/// <param name="p_strKeyVal">�ؼ��ֵ�ֵ</param>
		/// <returns></returns>
		public bool Update(string p_strTableName,string p_strKeyName,string p_strKeyVal)
		{
			string strSql = "";
			string strFields = "";
			int iFieldNum = 0;
			int iNum = 0;

			int iFieldLen = 0;
			string strFieldTitle = "";
			string strFieldName = "";

			string[] ary = null;
			string strVal = "";
			bool bRet = true;
			bool bExitFor = false;
    
			strSql = "update " + p_strTableName + " set ";
			iFieldNum = m_Ary.Count;
 
			for(iNum=0;iNum<iFieldNum;iNum++)
			{
				ary = (string[])m_Ary[iNum];
				strVal = ary[4].Trim();
				iFieldLen = Int32.Parse(ary[2]);
				strFieldTitle = ary[1].Trim(); 
				strFieldName = ary[0].Trim(); 
				bExitFor = false;
				
				switch(ary[3].Trim())
				{
					case "c":
						
						if(strVal == "")
						{
							strVal = " ";
						}
						else
						{
							if(strVal.Length > iFieldLen)
							{
								AddErr(strFieldTitle + "����" + "m_err");
								bRet = false;
								bExitFor = true;
							}
						}
						strFields = strFields + strFieldName + "='" + strVal + "',";
						break;

					case "i":
						if(IsInt(strVal))
						{
							if(strVal.Length > iFieldLen)
							{
								AddErr(strFieldTitle + "����" + "m_err");
							}
							else
							{
								strFields = strFields + strFieldName + "=" + strVal + ",";
							}
						}
						else
						{
							AddErr(strFieldTitle + "ӦΪ����" + "m_err");
							bRet = false;
							bExitFor = true;
						}
						break;

					case "d":
						if(IsNumeric(strVal))
						{
							if(strVal.Length > iFieldLen)
							{
								AddErr(strFieldTitle + "����" + "m_err");
								bRet = false;
								bExitFor = true;
							}
							else
							{
								strFields = strFields + strFieldName + "=" + strVal + ",";
							}
						}
						else
						{
							AddErr(strFieldTitle + "ӦΪ����" + "m_err");
							bRet = false;
							bExitFor = true;
						}
						break;

					case "t":
						strFields = strFields + strFieldName + "=" + strVal + ",";
						break;
				}

				if(bExitFor)
				{
					//break;
				}
			}
       
			if(bRet)
			{
				strFields = strFields.Substring(0,strFields.Length-1);
				strSql = strSql + strFields + " where " + p_strKeyName.Trim() + "=" + p_strKeyVal.Trim(); 
				Conn conn = new Conn();
				bRet = conn.Update(strSql);
			}
			else
			{
				throw new Exception(GetErr());
			}
			return bRet;
		}
	}
}
