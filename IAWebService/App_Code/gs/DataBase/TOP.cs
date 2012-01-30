using System;
using System.Collections;

namespace gs.DataBase
{
	/// <summary>
	/// TableOp ��ժҪ˵����
	/// </summary>
	public class TOP
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
		public TOP(string p_strConnStr)
		{
			m_Ary = null;
			m_strConn = p_strConnStr;
			m_Ary = new ArrayList();
		}

		public TOP(Conn p_Conn)
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
		public void AddRow(string p_strFieldName,string p_strFieldType,string p_strFieldValue)
		{
			string[] ary = new string[3];
			ary[0] = p_strFieldName;
			ary[1] = p_strFieldType;
			ary[2] = p_strFieldValue;
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
				strVal = ary[2].Trim();
				strFieldName = ary[0].Trim(); 
				bExitFor = false;
				
				switch(ary[1].Trim())
				{
					case "c":
						strFields = strFields + strFieldName + ",";

						if(strVal==null || strVal == "")
						{
							strVal = " ";
						}
						strVals = strVals + "'" + strVal + "',";
						break;

					case "i":
						if(IsInt(strVal))
						{
							strFields = strFields + strFieldName + ",";
							strVals = strVals + strVal + ",";
						}
						else
						{
							AddErr(strFieldName + "ӦΪ����" + "m_err");
							bRet = false;
							bExitFor = true;
						}
						break;

					case "d":
						if(IsNumeric(strVal))
						{
							strFields = strFields + strFieldName + ",";
							strVals = strVals + strVal + ",";
						}
						else
						{
							AddErr(strFieldName + "ӦΪ����" + "m_err");
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
				bRet = m_Conn.Update(strSql);
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
			return Update(p_strTableName,p_strKeyName + "=" + p_strKeyVal);
		}

		public bool Update(string p_strTableName,string p_strWhere)
		{
			string strSql = "";
			string strFields = "";
			int iNum = 0;
			int iFieldNum = 0;

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
				strVal = ary[2].Trim();
				strFieldName = ary[0].Trim(); 
				bExitFor = false;
				
				switch(ary[1].Trim())
				{
					case "c":
						
						if(strVal == "")
						{
							strVal = " ";
						}
						strFields = strFields + strFieldName + "='" + strVal + "',";
						break;

					case "i":
						if(IsInt(strVal))
						{
							strFields = strFields + strFieldName + "=" + strVal + ",";
						}
						else
						{
							AddErr(strFieldName + "ӦΪ����" + "m_err");
							bRet = false;
							bExitFor = true;
						}
						break;

					case "d":
						if(IsNumeric(strVal))
						{
							strFields = strFields + strFieldName + "=" + strVal + ",";
						}
						else
						{
							AddErr(strFieldName + "ӦΪ����" + "m_err");
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
				strSql = strSql + strFields + " where " + p_strWhere;
				bRet = m_Conn.Update(strSql);
			}
			else
			{
				throw new Exception(GetErr());
			}
			return bRet;
		}
	}
}
