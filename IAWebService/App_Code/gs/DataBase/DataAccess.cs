using System;
using System.Collections;
using System.Data.OleDb;
using System.Data;

namespace gs.DataBase
{
	/// <summary>
	/// ���ݿ���ʹ�����insert,update,delete��
	/// </summary>
	public class DataAccess
	{
		private string m_strConn = "";
		private string m_strTableName = "";
		private string m_strErr = "";
		private Conn m_Conn = null;
		private IList m_Ary = null;  
		private  OleDbDataAdapter da=null;//����������(insert,update,del)

		public DataAccess(string p_strConnStr)
		{
			m_Ary = null;
			m_strConn = p_strConnStr;
			m_Ary = new ArrayList();
		}
		public DataAccess(Conn p_Conn)
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
		/// <param name="p_strFieldTitle">�ֶεı�ʾ(c/string,i/int,d/double,dt/datetime)</param>
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
			
			OleDbConnection dbConn = m_Conn.GetConn();
			 
			int iFieldNum = 0;
			int iNum = 0;

			string strFieldName = "";

			string[] ary = null;
			string strVal = "";
			bool bRet = true;
			bool bExitFor = false;
			iFieldNum = m_Ary.Count;//�ֶθ���

			string strSelect="select ";//select���Ӿ�
			for(iNum=0;iNum<iFieldNum;iNum++)
			{
				ary = (string[])m_Ary[iNum];
				strFieldName = ary[0].Trim(); 
				if(iFieldNum==1)
				{
					strSelect+=strFieldName;//ֻ��һ���ֶ�
				}
				else if(iNum==iFieldNum-1)
				{
					strSelect+=strFieldName;//���һ���ֶ�
				}
				else
				{
					strSelect+=strFieldName+",";//�м���ֶ�
				}
			}
			da = new OleDbDataAdapter(strSelect+ "  from  "+p_strTableName+" ",dbConn);
			
			OleDbCommand cmd=new OleDbCommand();
			cmd.Connection=dbConn;
			string strInsertinto=strSelect.Replace("select"," ");//inser into���Ӿ�
			strInsertinto= "INSERT INTO  "+p_strTableName+"("+strInsertinto+") values(";

			for(iNum=0;iNum<iFieldNum;iNum++)
			{
				ary = (string[])m_Ary[iNum];
				if(iFieldNum==1)
				{
					strInsertinto+="?)";;//ֻ��һ���ֶ�
				}
				else if(iNum==iFieldNum-1)
				{
					strInsertinto+="?)";//���һ���ֶ�
				}
				else
				{
					strInsertinto+="?,";//�м���ֶ�
				}
			}
			cmd.CommandText =strInsertinto;

			DataSet ds=new DataSet();
			da.Fill(ds,p_strTableName);
  
			for(iNum=0;iNum<iFieldNum;iNum++)
			{
				ary = (string[])m_Ary[iNum];
				strVal = ary[2].Trim();
				strFieldName = ary[0].Trim(); 
				bExitFor = false;

				OleDbParameter myDataParameter = new OleDbParameter();
				switch(ary[1].Trim())
				{
					case "c":
						myDataParameter.ParameterName = "@"+strFieldName;
						myDataParameter.OleDbType = OleDbType.Char;
						myDataParameter.SourceColumn=strFieldName;
						cmd.Parameters.Add(myDataParameter);
						break;

					case "i":
						if(IsInt(strVal))
						{
							myDataParameter.ParameterName = "@"+strFieldName;
							myDataParameter.OleDbType = OleDbType.Integer;
							myDataParameter.SourceColumn=strFieldName;
							cmd.Parameters.Add(myDataParameter);
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
							myDataParameter.ParameterName = "@"+strFieldName;
							myDataParameter.OleDbType = OleDbType.Double;
							myDataParameter.SourceColumn=strFieldName;
							cmd.Parameters.Add(myDataParameter);
						}
						else
						{
							AddErr(strFieldName + "ӦΪ����" + "m_err");
							bRet = false;
							bExitFor = true;
						}
						break;

					case "dt":
					{
						myDataParameter.ParameterName = "@"+strFieldName;
						myDataParameter.OleDbType = OleDbType.Date;
						myDataParameter.SourceColumn=strFieldName;
						cmd.Parameters.Add(myDataParameter);
					}
						break;

					case "t":
						//??
						break;
				}

				if(bExitFor)
				{
					//break;
				}
			}
       
			if(bRet)
			{
				DataRow newrow=ds.Tables[p_strTableName].NewRow();
				for(iNum=0;iNum<iFieldNum;iNum++)
				{
					ary = (string[])m_Ary[iNum];
					strFieldName = ary[0].Trim();
					string strFieldVal = ary[2].Trim(); 
					switch(ary[1].Trim())
					{
						case "c":
							newrow[strFieldName]=strFieldVal;
							break;
						case "i":
							newrow[strFieldName]=int.Parse(strFieldVal);
							break;
						case "d":
							newrow[strFieldName]=double.Parse(strFieldVal);
							break;
						case "dt":
							newrow[strFieldName]=DateTime.Parse(strFieldVal);
							break;
					}
					
				}
				ds.Tables[p_strTableName].Rows.Add(newrow);
				// �����¼
				try
				{
				 
					da.InsertCommand = cmd;
					da.Update(ds.Tables[p_strTableName]);
					bRet=true;
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
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
		/// <param name="p_strTableName">����</param>
		/// <param name="p_strKeyName">�ؼ��ֵ�����</param>
		/// <param name="p_strKeyVal">�ؼ��ֵ�ֵ(string)</param>
		/// <returns></returns>
		public bool Update(string p_strTableName,string p_strKeyName,string p_strKeyVal)
		{
			return Update(p_strTableName,p_strKeyName + "='" + p_strKeyVal+"' ");
		}

		/// <summary>
		/// �޸�һ����¼
		/// </summary>
		/// <param name="p_strTableName">����</param>
		/// <param name="p_strKeyName">�ؼ��ֵ�����</param>
		/// <param name="p_strKeyVal">�ؼ��ֵ�ֵ(int)</param>
		/// <returns></returns>
		public bool Update(string p_strTableName,string p_strKeyName,int p_strKeyVal)
		{
			return Update(p_strTableName,p_strKeyName + "=" + p_strKeyVal+" ");
		}


		private bool Update(string p_strTableName,string p_strWhere)
		{
			
			OleDbConnection dbConn = m_Conn.GetConn();
			 
			int iFieldNum = 0;
			int iNum = 0;

			string strFieldName = "";
			string strFieldVal = "";

			string[] ary = null;
			string strVal = "";
			bool bRet = true;
			bool bExitFor = false;
			iFieldNum = m_Ary.Count;//�ֶθ���

			string strSelect="select ";//select���Ӿ�
			for(iNum=0;iNum<iFieldNum;iNum++)
			{
				ary = (string[])m_Ary[iNum];
				strFieldName = ary[0].Trim(); 
				if(iFieldNum==1)
				{
					strSelect+=strFieldName+ "  from "+p_strTableName;//ֻ��һ���ֶ�
				}
				else if(iNum==iFieldNum-1)
				{
					strSelect+=strFieldName+ "  from "+p_strTableName+" ";//���һ���ֶ�
				}
				else
				{
					strSelect+=strFieldName+",";//�м���ֶ�
				}
			}
			da = new OleDbDataAdapter(strSelect+" where "+p_strWhere,dbConn);
			
			OleDbCommand cmd=new OleDbCommand();
			cmd.Connection=dbConn;
			string strInsertinto="";//update���Ӿ�
			strInsertinto= "update    "+p_strTableName+"  set  ";

			for(iNum=0;iNum<iFieldNum;iNum++)
			{
				ary = (string[])m_Ary[iNum];
				strFieldName = ary[0].Trim();
				strFieldVal = ary[2].Trim();
				if(iFieldNum==1)
				{
					strInsertinto+=strFieldName+"=?";//ֻ��һ���ֶ�
				}
				else if(iNum==iFieldNum-1)
				{
					strInsertinto+=strFieldName+"=?";//���һ���ֶ�
				}
				else
				{
					strInsertinto+=strFieldName+"=?,";//�м���ֶ�
				}
			}
			strInsertinto+=" where "+p_strWhere;
			cmd.CommandText =strInsertinto;

			DataSet ds=new DataSet();
			da.Fill(ds,p_strTableName);
  
			for(iNum=0;iNum<iFieldNum;iNum++)
			{
				ary = (string[])m_Ary[iNum];
				strVal = ary[2].Trim();
				strFieldName = ary[0].Trim(); 
				bExitFor = false;

				OleDbParameter myDataParameter = new OleDbParameter();
				switch(ary[1].Trim())
				{
					case "c":
						myDataParameter.ParameterName = "@"+strFieldName;
						myDataParameter.OleDbType = OleDbType.Char;
						myDataParameter.SourceColumn=strFieldName;
						cmd.Parameters.Add(myDataParameter);
						break;

					case "i":
						if(IsInt(strVal))
						{
							myDataParameter.ParameterName = "@"+strFieldName;
							myDataParameter.OleDbType = OleDbType.Integer;
							myDataParameter.SourceColumn=strFieldName;
							cmd.Parameters.Add(myDataParameter);
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
							myDataParameter.ParameterName = "@"+strFieldName;
							myDataParameter.OleDbType = OleDbType.Double;
							myDataParameter.SourceColumn=strFieldName;
							cmd.Parameters.Add(myDataParameter);
						}
						else
						{
							AddErr(strFieldName + "ӦΪ����" + "m_err");
							bRet = false;
							bExitFor = true;
						}
						break;
					
					case "dt":
					{
						myDataParameter.ParameterName = "@"+strFieldName;
						myDataParameter.OleDbType = OleDbType.Date;
						myDataParameter.SourceColumn=strFieldName;
						cmd.Parameters.Add(myDataParameter);
					}
						break;

					case "t":
						//??
						break;
				}

				if(bExitFor)
				{
					//break;
				}
			}
       
			if(bRet)
			{
				DataRow newrow=ds.Tables[p_strTableName].Rows[0];
				for(iNum=0;iNum<iFieldNum;iNum++)
				{
					ary = (string[])m_Ary[iNum];
					strFieldName = ary[0].Trim();
					strFieldVal = ary[2].Trim(); 
					switch(ary[1].Trim())
					{
						case "c":
							newrow[strFieldName]=strFieldVal;
							break;
						case "i":
							newrow[strFieldName]=int.Parse(strFieldVal);
							break;
						case "d":
							newrow[strFieldName]=double.Parse(strFieldVal);
							break;
						case "dt":
							newrow[strFieldName]=DateTime.Parse(strFieldVal);
							break;
					}
				}
				// �޸ļ�¼
				try
				{
				 
					da.UpdateCommand = cmd;
					da.Update(ds.Tables[p_strTableName]);
					bRet=true;
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
			}
			else
			{
				throw new Exception(GetErr());
			}
			return bRet;
		}



		/// <summary>
		/// ɾ����¼
		/// </summary>
		/// <param name="p_strTableName">����</param>
		/// <param name="p_strKeyName">�ؼ��ֵ�����</param>
		/// <param name="p_strKeyVal">�ؼ��ֵ�ֵ</param>
		/// <returns></returns>
		public bool Delete(string p_strTableName,string p_strKeyName,int p_strKeyVal)
		{
			return Delete(p_strTableName,p_strKeyName + "=" + p_strKeyVal+" ");
		}
		/// <summary>
		/// ɾ����¼
		/// </summary>
		/// <param name="p_strTableName">����</param>
		/// <param name="p_strKeyName">�ؼ��ֵ�����</param>
		/// <param name="p_strKeyVal">�ؼ��ֵ�ֵ</param>
		/// <returns></returns>
		public bool Delete(string p_strTableName,string p_strKeyName,string p_strKeyVal)
		{
			return Delete(p_strTableName,p_strKeyName + "='" + p_strKeyVal+"' ");
		}
		/// <summary>
		/// ɾ����¼
		/// </summary>
		/// <param name="p_strTableName"></param>
		/// <param name="p_strWhere"></param>
		/// <returns></returns>
		private  bool Delete(string p_strTableName,string p_strWhere)
		{
			
			OleDbConnection dbConn = m_Conn.GetConn();
			bool bRet;
			string strDelete="";//delete���Ӿ�
			OleDbCommand cmd=new OleDbCommand();
			cmd.Connection=dbConn;
			
			strDelete= "delete    "+p_strTableName+"  where  "+p_strWhere;
			cmd.CommandText =strDelete;
			// delete��¼
			try
			{
				
				int a=cmd.ExecuteNonQuery();
				if(a>0)
					bRet=true;
				else
					bRet=false;
			}
			catch (Exception ex)
			{
				bRet=false;
				throw new Exception(ex.Message);
				
			}
			 
			return bRet;
		 
		}

		/// <summary>
		/// ���ؼ�¼��
		/// </summary>
		/// <param name="strSql">select���</param>
		/// <returns>DataSet</returns>
		public DataSet ExecuteDataSet(string strSql)
		{
			DataSet ds=new DataSet();
			try
			{
				OleDbConnection dbConn = m_Conn.GetConn();
				OleDbDataAdapter da=new OleDbDataAdapter(strSql,dbConn);
				da.Fill(ds);
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return ds;
			
		}
		/// <summary>
		/// ִ��sql���
		/// </summary>
		/// <param name="strSql"></param>
		public void ExecuteSqlstr(string strSql)
		{
 			try
			{	
				OleDbConnection dbConn = m_Conn.GetConn();
				OleDbCommand cmd=new OleDbCommand();
				cmd.Connection=dbConn;
				cmd.CommandText =strSql;
 				cmd.ExecuteNonQuery();
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
 		}
	}
}
