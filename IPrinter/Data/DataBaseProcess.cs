using System;
using System.Data;
using System.Data.OleDb;  
using System.Collections.Generic;
using System.Text;

namespace ePlus.Data
{
    public class DataBaseProcess : IDisposable 
    {
        public DataBaseProcess(string connectionString)
        {
            m_Connection = new OleDbConnection(connectionString);
        }

        #region 提供的方法
        /// <summary>
        /// 执行查询SQL语句
        /// </summary>
        /// <param name="sqlStatement">待执行的SQL语句</param>
        /// <returns>数据集 DataTable</returns>
        public DataTable ExcuteQuery(string sqlStatement)
        {
            if (m_Connection == null)
                throw new Exception("未创建与数据库连接的实例。");

            OleDbCommand cmd = new OleDbCommand(sqlStatement, m_Connection);

            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            DataSet dataSet = null;
            try
            {

                if (m_Connection != null && m_Connection.State == ConnectionState.Closed)
                {
                    m_Connection.Open();
                }

                dataSet = new DataSet();
                adapter.Fill(dataSet);
                return dataSet.Tables[0];  
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("win2000用户请注意：请下载补丁http://download.eg66.com/mdac_typ28.exe");
                throw new Exception("执行SQL失败。");
            }
            finally
            {
                //System.Windows.Forms.MessageBox.Show("数据库格式不支持！");
            }
            return null;
        }

        /// <summary>
        /// 执行数据控制SQL语句
        /// </summary>
        /// <param name="sqlStatement">待执行的SQL语句</param>
        /// <returns>受影响的数据行数</returns>
        public int ExcuteNonQuery(string sqlStatement)
        {
            if (m_Connection == null)
                throw new Exception("未创建与数据库连接的实例。");

            OleDbCommand cmd = new OleDbCommand(sqlStatement, m_Connection);

            try
            {
                if (m_Connection != null && m_Connection.State == ConnectionState.Closed)
                {
                    m_Connection.Open();
                }

                return cmd.ExecuteNonQuery(); 
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("win2000用户请注意：请下载补丁http://download.eg66.com/mdac_typ28.exe");
                throw new Exception("执行SQL失败。");
            }
            return 0;
        }

        public static string GetConnectionString(string fileName)
        {
            OleDbConnectionStringBuilder connectionStringBuilder = new OleDbConnectionStringBuilder();
            connectionStringBuilder.Provider = "Microsoft.Jet.OLEDB.4.0";
            connectionStringBuilder.DataSource = fileName;

            return connectionStringBuilder.ConnectionString; 
        }
        #endregion

        #region Private Members
        private OleDbConnection m_Connection = null;
        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            if (m_Connection != null && m_Connection.State == ConnectionState.Open)
            {
                m_Connection.Close();  
            }
        }

        #endregion
    }
}
