using System;
using System.Data.OleDb;
using System.Data;

namespace gs.DataBase
{
	/// <summary>
	/// Conn 的摘要说明。
	/// </summary>
	public class Conn
	{
		/// <summary>
		/// Dbase 的摘要说明。
		/// </summary>
		public static string m_strConnStr = "";
		private OleDbConnection myConn = null;
		private bool bolTrans = false;
		private static bool bBeConn = false;
		private OleDbTransaction myTrans;

		public bool IsBolTrans()
		{
			return bolTrans;
		}

		public OleDbTransaction getTrans()
		{
			return myTrans;
		}

		public Conn()
		{
			//m_strConnStr = "Provider=SQLOLEDB.1;Password=sa;Persist Security Info=True;User ID=sa;Initial Catalog=gsvhost;Data Source=192.168.80.234;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;Workstation ID=SEA;Use Encryption for Data=False;Tag with column collation when possible=False";
			if(!bBeConn)
			{
				//commented by chenqj
				//热！ 谁加的啊这里 导致 2008-10-10 晚上服务器登陆异常！！！
//				if(System.DateTime.Now > System.DateTime.Parse("2008-10-10"))
//				{
//					gs.util.func.Write("refuse access");
//				}
//				else
				{
					m_strConnStr = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToString().Trim();
					bBeConn = true;
				}
			}
			
			try
			{
				myConn = new OleDbConnection(m_strConnStr);
				myConn.Open();
			}
			catch(Exception ex)
			{
				gs.util.func.Write("数据库连接未能打开" + ex.Message);
				gs.util.func.Write(m_strConnStr);
			}
		}

		public Conn(string p_strConnStr)
		{
			//m_strConnStr = p_strConnStr;
			//myConn = new OleDbConnection(m_strConnStr);
			//myConn.Open();
			if(!bBeConn)
			{
				m_strConnStr = System.Configuration.ConfigurationSettings.AppSettings["connstring"].ToString().Trim();
				bBeConn = true;
			}
			myConn = new OleDbConnection(m_strConnStr);
			myConn.Open();
		}

		public OleDbConnection	GetConn()
		{
			return myConn;
		}

		public bool TestSQL(string p_strSql)
		{
			bool bSucc = false;
			//OleDbConnection myConn = new OleDbConnection(m_strConnStr);
			//myConn.Open();
			try
			{
				OleDbCommand mycommand = new OleDbCommand(p_strSql,myConn);
				mycommand.ExecuteReader();
				bSucc = true;
			}
			catch(OleDbException ex)
			{
				Console.WriteLine(ex.Message); 
				bSucc = false;
			}
			/*finally
			{
				myConn.Close();
			}*/
			return bSucc;
		}

		public bool Update(string p_strSql)
		{
			bool bSucc = false;
			/*OleDbConnection myConn = new OleDbConnection(m_strConnStr);
			myConn.Open();*/
			try
			{
				OleDbCommand mycommand = new OleDbCommand(p_strSql,myConn);
				if(bolTrans)
				{
					mycommand.Transaction = myTrans;
				}
				mycommand.ExecuteNonQuery();
				bSucc = true;
			}
			catch(OleDbException ex)
			{
				bSucc = false;
				gs.util.func.Write("Update=" + p_strSql + ex.Message);
				throw ex; 
			}
			/*finally
			{
				myConn.Close();
			}*/
			return bSucc;
		}

		/*public void Save(string p_strSql,DataSet p_ds)
			{
				OleDbConnection myConn = new OleDbConnection(m_strConnStr);
				OleDbDataAdapter myDataAdapter = new OleDbDataAdapter();
				myDataAdapter.SelectCommand = new OleDbCommand(p_strSql, myConn);
				OleDbCommandBuilder custCB = new OleDbCommandBuilder(myDataAdapter);
				myConn.Open();

				//DataSet custDS = new DataSet();
				//myDataAdapter.Fill(custDS);

				//code to modify data in dataset here

				//Without the OleDbCommandBuilder this line would fail
				myDataAdapter.Update(p_ds.Tables[0]);
				myConn.Close();
			}*/

		public DataView Query(String p_strSql,String p_strTable)
		{
			DataView dv;
			//OleDbConnection myConn = new OleDbConnection(m_strConnStr);
			//myConn.Open();
			try
			{
				DataSet ds=new DataSet();
				OleDbDataAdapter adp = new OleDbDataAdapter(p_strSql,myConn);
				if(bolTrans)
				{
					adp.SelectCommand.Transaction = myTrans;
				}
				adp.Fill(ds,p_strTable); 
				dv = ds.Tables[p_strTable].DefaultView;
			}
			catch(OleDbException ex)
			{
				gs.util.func.Write("Query=" + p_strSql + ex.Message);
				throw ex; 
			}
			/*finally
			{
				myConn.Close();
			}*/
			return dv;
		}
		
		public DataSet GetDataSet(String p_strSql,String p_strTable)
		{
			//OleDbConnection myConn = new OleDbConnection(m_strConnStr);
			DataSet ds;
			try
			{
				OleDbDataAdapter adp = new OleDbDataAdapter(p_strSql,myConn);
				adp.SelectCommand = new OleDbCommand(p_strSql, myConn);
				if(bolTrans)
				{
					adp.SelectCommand.Transaction = myTrans;
				}
				OleDbCommandBuilder custCB = new OleDbCommandBuilder(adp);
				ds=new DataSet();
				adp.Fill(ds,p_strTable); 

				/*ds = new DataSet();
				OleDbDataAdapter adp = new OleDbDataAdapter(p_strSql,myConn);
				adp.Fill(ds,p_strTable); */
			}
			catch(OleDbException ex)
			{
				gs.util.func.Write("GetDataSet(" + p_strSql + ")" + ex.Message);
				throw ex; 
			}
			finally
			{
				//myConn.Close();
			} 
			return ds;
		}

		public DataSet GetDataSet(String p_strSql)
		{
			//OleDbConnection myConn = new OleDbConnection(m_strConnStr);
			DataSet ds;
			try
			{
				OleDbDataAdapter adp = new OleDbDataAdapter(p_strSql,myConn);
				adp.SelectCommand = new OleDbCommand(p_strSql, myConn);
				if(bolTrans)
				{
					adp.SelectCommand.Transaction = myTrans;
				}
				OleDbCommandBuilder custCB = new OleDbCommandBuilder(adp);
				ds=new DataSet();
				adp.Fill(ds,"temp"); 

				/*ds = new DataSet();
				OleDbDataAdapter adp = new OleDbDataAdapter(p_strSql,myConn);
				adp.Fill(ds,p_strTable); */
			}
			catch(OleDbException ex)
			{
				gs.util.func.Write("GetDataSet(" + p_strSql + ")" + ex.Message);
				throw ex; 
			}
			finally
			{
				//myConn.Close();
			} 
			return ds;
		}

		public OleDbDataReader Query(String p_strSql)
		{
			//OleDbConnection myConn=new OleDbConnection(m_strConnStr);
			//myConn.Open();
			OleDbCommand myComm=new OleDbCommand(p_strSql,myConn);
			OleDbDataReader myRs=myComm.ExecuteReader();
			//myConn.Close();
			return myRs;
		}
        /// <summary>
        /// 使用存储过程进行查询
        /// </summary>
        /// <param name="p_strSql">待执行的Sql语句</param>
        /// <param name="arParms">存储过程的参数</param>
        /// <returns>查询生成的视图</returns>
        public DataView ProcQuery(String p_strSql, OleDbParameter[] arParms)
        {
            DataView dv;
            //OleDbConnection myConn = new OleDbConnection(m_strConnStr);
            //myConn.Open();
            try
            {
                DataSet ds = new DataSet();
                OleDbDataAdapter adp = new OleDbDataAdapter(p_strSql,myConn);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                for(int i=0;i<arParms.GetLength(0);i++)
                {
                    adp.SelectCommand.Parameters.Add(arParms[i]);
                }
                if (bolTrans)
                {
                    adp.SelectCommand.Transaction = myTrans;
                }
                adp.Fill(ds);
                dv = ds.Tables[0].DefaultView;
            }
            catch (OleDbException ex)
            {
                gs.util.func.Write("Query=" + p_strSql + ex.Message);
                throw ex;
            }
            /*finally
            {
                myConn.Close();
            }*/
            return dv;
        }
        public string ProcExecuteScalar(String p_strSql, OleDbParameter[] arParms)
        {
            //DataView dv;
            //OleDbConnection myConn = new OleDbConnection(m_strConnStr);
            //myConn.Open();
            string strRet = string.Empty;
            try
            {
                //DataSet ds = new DataSet();
                //OleDbDataAdapter adp = new OleDbDataAdapter(p_strSql, myConn);
                OleDbCommand ocm=new OleDbCommand(p_strSql, myConn);
                ocm.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < arParms.GetLength(0); i++)
                {
                    ocm.Parameters.Add(arParms[i]);
                }
                if (bolTrans)
                {
                    ocm.Transaction = myTrans;
                }
                object ob = ocm.ExecuteScalar();
                if (ob != null)
                    strRet = ob.ToString();
                else
                    strRet = "";
            }
            catch (OleDbException ex)
            {
                gs.util.func.Write("Query=" + p_strSql + ex.Message);
                throw ex;
            }
            /*finally
            {
                myConn.Close();
            }*/
            return strRet;
        }

		public void close()
		{
			if(this != null && myConn != null)
			{
				myConn.Close(); 
			}
		}

		public void beginTrans()
		{
			bolTrans = true;
			myTrans = myConn.BeginTransaction();
		}

		public void commit()
		{

			myTrans.Commit(); 
		}

		public void rollback()
		{
			myTrans.Rollback(); 
		}
	}
}
