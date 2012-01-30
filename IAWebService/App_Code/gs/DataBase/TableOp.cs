using System;
using System.Collections;

namespace gs.DataBase
{
	/// <summary>
	/// TableOp 的摘要说明。
	/// </summary>
	public class TableOp
	{
		private string m_strConn = "";
		private string m_strTableName = "";
		private string m_strErr = "";
		private Conn m_Conn = null;
		private IList m_Ary = null;  
        
		/// <summary>
		/// 构造体
		/// </summary>
		/// <param name="p_strConnStr">数据库连接字符窜</param>
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
		/// 加入错误严正内容
		/// </summary>
		/// <param name="p_strErr">错误原因</param>
		private void AddErr(string p_strErr)
		{
			m_strErr += p_strErr.Trim();
		}

		public string GetErr()
		{
			return m_strErr;
		}

		/// <summary>
		/// 是否为整数
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
		/// 是否为实数
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
		


		//检查是否为整数
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
	
		//检查是否为数字
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
		
			//如果多于一个小数点则为错误
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
		/// 设置被操作的表名
		/// </summary>
		/// <param name="p_strTableName">表名</param>
		public void SetTableName(string p_strTableName)
		{
			m_strTableName = p_strTableName;
		}

		/// <summary>
		/// 增加一行
		/// </summary>
		/// <param name="p_strFieldName">字段的名称</param>
		/// <param name="p_strFieldTitle">字段的标示</param>
		/// <param name="p_strFieldLen">字段长度</param>
		/// <param name="p_strFieldType">字段类型</param>
		/// <param name="p_strFieldValue">字段内容</param>
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
		/// 增加一条记录到数据库中
		/// </summary>
		/// <returns>如果验证正常返回真,否则返回假</returns>
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
								AddErr(strFieldTitle + "超长" + "m_err");
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
								AddErr(strFieldTitle + "超长" + "m_err");
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
							AddErr(strFieldTitle + "应为整数" + "m_err");
							bRet = false;
							bExitFor = true;
						}
						break;

					case "d":
						if(IsNumeric(strVal))
						{
							if(strVal.Length > iFieldLen)
							{
								AddErr(strFieldTitle + "超长" + "m_err");
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
							AddErr(strFieldTitle + "应为数字" + "m_err");
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
		/// 修改一条记录
		/// </summary>
		/// <param name="p_strKeyName">关键字的名称</param>
		/// <param name="p_strKeyVal">关键字的值</param>
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
								AddErr(strFieldTitle + "超长" + "m_err");
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
								AddErr(strFieldTitle + "超长" + "m_err");
							}
							else
							{
								strFields = strFields + strFieldName + "=" + strVal + ",";
							}
						}
						else
						{
							AddErr(strFieldTitle + "应为整数" + "m_err");
							bRet = false;
							bExitFor = true;
						}
						break;

					case "d":
						if(IsNumeric(strVal))
						{
							if(strVal.Length > iFieldLen)
							{
								AddErr(strFieldTitle + "超长" + "m_err");
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
							AddErr(strFieldTitle + "应为数字" + "m_err");
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
