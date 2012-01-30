using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.IO;
using ADOX;
using System.Data;
using System.Windows.Forms;
using System.Data.Odbc;

namespace EagleBase
{
    public class AccessOperation
    {
        private static string fn = Application.StartupPath + "\\ExpTick.mdb";
        private static string tn = "ticketsno";
        public static void CreateDatabase(string fn)
        {
            string ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fn;
            if (!File.Exists(fn))
            {
                ADOX.Catalog catalog = new Catalog();
                catalog.Create(ConnStr);
            }
        }
        public static void CreateTable(string fn, string tablename)
        {
            string ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fn;
            OleDbConnection conn = new OleDbConnection(ConnStr);
            conn.Open();
            string sql = "CREATE TABLE [" + tablename + "]";
            try
            {
                OleDbCommand cmd = new OleDbCommand(sql,conn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
            }
            conn.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fn"></param>
        /// <param name="tablename"></param>
        /// <param name="colstring">Like:ColumnName varchar(10)  , ColumnName Long 数字</param>
        public static void CreateColumn(string fn, string tablename, string colstring)
        {
            string ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fn;
            OleDbConnection conn = new OleDbConnection(ConnStr);
            conn.Open();
            try
            {
                string alter = "ALTER TABLE " + tablename + " ADD COLUMN " + colstring;
                OleDbCommand cmd = new OleDbCommand(alter, conn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
            }
            conn.Close();
        }
        public static void ExpTickDatabaseCreate()
        {
            string fn = AccessOperation.fn;
            string tn = AccessOperation.tn;
            CreateDatabase(fn);
            CreateTable(fn, tn);
            CreateColumn(fn, tn, "tktNo varchar(14)");
            CreateColumn(fn, tn, "sailDate varchar(10)");//日期2008-01-02
            CreateColumn(fn, tn, "flightDate varchar(10)");//日期2008-01-02
        }
        /// <summary>
        /// 从ExpTick.mdb中找对应的号
        /// </summary>
        /// <param name="sailBeg"></param>
        /// <param name="sailEnd"></param>
        /// <param name="and"></param>
        /// <param name="flightBeg"></param>
        /// <param name="flightEnd"></param>
        /// <returns></returns>
        public static List<string> ExpTickSelect(DateTime sailBeg, DateTime sailEnd, bool and, DateTime flightBeg, DateTime flightEnd)
        {
            string fn = AccessOperation.fn;
            string tn = AccessOperation.tn;
            string ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fn;
            OleDbConnection conn = new OleDbConnection(ConnStr);
            conn.Open();
            string sql = string.Format(
                //"select tktNo from {0} where (sailDate>='{1}' && sailDate<='{2}') {3} flightDate>='{4}' && flightDate<='{5}'"
                "select tktNo from {0} where (StrComp(sailDate,'{1}')>=0 and StrComp(sailDate,'{2}')<=0) {3} (StrComp(flightDate,'{4}')>=0 and StrComp(flightDate,'{5}')<=0)"
                
                , tn
                , sailBeg.ToString("yyyy-MM-dd")
                , sailEnd.ToString("yyyy-MM-dd")
                , and ? "and" : "or"
                , flightBeg.ToString("yyyy-MM-dd")
                , flightEnd.ToString("yyyy-MM-dd")
                );
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            List<string> ls = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ls.Add(dt.Rows[i][0].ToString());
            }
            return ls;
        }
        public static List<string> ExpTickSelectFromFinance(DateTime sailBeg, DateTime sailEnd, bool and, DateTime flightBeg, DateTime flightEnd)
        {
            string fn = Application.StartupPath + "\\database.mdb";
            string tn = "etickets";
            string ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fn;
            OleDbConnection conn = new OleDbConnection(ConnStr);
            conn.Open();
            string sql = string.Format(
                //"select tktNo from {0} where (sailDate>='{1}' && sailDate<='{2}') {3} flightDate>='{4}' && flightDate<='{5}'"
                "select [E-TKT-NUMBER] from {0} where (DATEOFSALE>=cdate('{1}') and DATEOFSALE<=cdate('{2}')) {3} (DATEOFSALE>=cdate('{4}') and DATEOFSALE<=cdate('{5}'))"

                , tn
                , sailBeg.ToString("yyyy-MM-dd")
                , sailEnd.ToString("yyyy-MM-dd")
                , and ? "and" : "or"
                , flightBeg.ToString("yyyy-MM-dd")
                , flightEnd.ToString("yyyy-MM-dd")
                );
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            List<string> ls = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ls.Add(dt.Rows[i][0].ToString());
            }
            return ls;
        }
        /// <summary>
        /// 从易格对帐平台数据库导入
        /// </summary>
        /// <param name="egFile">易格对帐平台的数据库 database.mdb</param>
        public static void ImportExpTickFromEagleFinance(object egFile)
        {
            ImportExpTickFromEagleFinance(egFile as string);
        }
        public static void ImportExpTickFromEagleFinance(string egFile)
        {
            string ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + egFile;
            OleDbConnection connEg = new OleDbConnection(ConnStr);
            connEg.Open();
            string sql = "select [E-TKT-NUMBER],[DATEOFSALE],[FLIGHTDATE] from etickets";
            OleDbCommand cmd = new OleDbCommand(sql, connEg);
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connEg.Close();
            string fn = Application.StartupPath + "\\ExpTick.mdb";
            string tn = "ticketsno";
            ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fn;
            connEg = new OleDbConnection(ConnStr);
            connEg.Open();
            EagleString.Imported.SendMessage(0xFFFF, (int)EagleString.Imported.egMsg.EXPIRE_TICKET_IMPORT_FROM_EAGLEFINANCE_TOTAL, 0, dt.Rows.Count);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //发出一个消息,当前第几条，共几条
                EagleString.Imported.SendMessage(0xFFFF, (int)EagleString.Imported.egMsg.EXPIRE_TICKET_IMPORT_FROM_EAGLEFINANCE_CURRENT, 0, i);
                string tktno = dt.Rows[i]["E-TKT-NUMBER"].ToString().Replace("-", "");
                DateTime date;
                DateTime date2;
                try
                {
                    date = DateTime.Parse(dt.Rows[i]["DATEOFSALE"].ToString());
                }
                catch
                {
                    continue;
                }
                try
                {
                    date2 = DateTime.Parse(dt.Rows[i]["FLIGHTDATE"].ToString());
                }
                catch
                {
                    date2 = new DateTime(date.Ticks);
                }

                sql = string.Format("select * from {0} where tktNo='{1}'", tn, tktno);
                cmd = new OleDbCommand(sql, connEg);
                adapter = new OleDbDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);
                if (table.Rows.Count == 0)
                {
                    sql = string.Format("insert into {0} (tktNo,sailDate,flightDate) values ('{1}','{2}','{3}')"
                        , tn, tktno, date.ToString("yyyy-MM-dd"), date2.ToString("yyyy-MM-dd"));
                    cmd = new OleDbCommand(sql, connEg);
                    cmd.ExecuteNonQuery();
                }
            }
            connEg.Close();
            MessageBox.Show("导入易格报表完毕！");
        }
        /// <summary>
        /// 摘要:从指定报表文件导入(起始票号终止票号在同１列中,或者单个票号,如781-1234567890-990,781-1234567890)
        /// </summary>
        /// <param name="xls"></param>
        /// <param name="tablename"></param>
        /// <param name="coltkt"></param>
        /// <param name="colsail"></param>
        /// <param name="colflight"></param>
        public static int ImportExpTickFrom(string xls, string tablename, int coltkt, int colsail, int colflight)
        {
            int ret = 0;
            int total2insert = 0;
            DataTable dt = new DataTable();
            OleDbDataAdapter adapt = new OleDbDataAdapter
                ("SELECT * FROM [" + tablename + "$]",
                "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + xls.Replace("/", "\\") + ";Extended Properties=Excel 8.0;");
            adapt.Fill(dt);

            string fn = AccessOperation.fn;
            string tn = AccessOperation.tn;
            string ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fn;
            OleDbConnection conn = new OleDbConnection(ConnStr);
            conn.Open();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                long startno = 0;
                long endno = 0;
                try
                {//主要在这里不同
                    string s = dt.Rows[i][coltkt].ToString().Replace("-", "").Trim();
                    if (s.Length == 13)
                    {
                        startno = long.Parse(s);
                        endno = long.Parse(s);
                    }
                    else if (s.Length > 13 && s.Length < 23)
                    {
                        startno = long.Parse(s.Substring(0, 13));
                        endno = long.Parse(s.Substring(0, 13 - (s.Length - 13))+s.Substring(13));
                    }
                    else throw new Exception("");
                }
                catch
                {
                    continue;
                }


                DateTime date;
                try
                {
                    date = DateTime.Parse(dt.Rows[i][colsail].ToString());
                }
                catch
                {
                    continue;
                }
                DateTime date2;
                try
                {
                    date2 = DateTime.Parse(dt.Rows[i][colflight].ToString());
                }
                catch
                {
                    date2 = new DateTime(date.Ticks);
                }
                for (long ln = startno; ln <= endno; ln++)
                {
                    total2insert++;
                    string sql = string.Format("select * from {0} where tktNo='{1}'", tn, ln.ToString());
                    OleDbCommand cmd = new OleDbCommand(sql, conn);
                    OleDbDataAdapter adapter2 = new OleDbDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter2.Fill(table);
                    if (table.Rows.Count == 0)
                    {
                        sql = string.Format("insert into {0} (tktNo,sailDate,flightDate) values ('{1}','{2}','{3}')"
                            , tn, ln.ToString(), date.ToString("yyyy-MM-dd"), date2.ToString("yyyy-MM-dd"));
                        cmd = new OleDbCommand(sql, conn);
                        ret += cmd.ExecuteNonQuery();
                    }
                }

            }
            if (total2insert == 0) MessageBox.Show("导入了0个记录！请确认文件！若是CSV文件，请转换为XLS文件后再尝试！");
            else MessageBox.Show("共找到" + total2insert.ToString() + "个记录，导入了" + ret.ToString() + "个记录！");

            conn.Close();
            return ret;
        }
        /// <summary>
        /// 从指定报表文件导入(起始票号终止票号分２列)
        /// </summary>
        /// <param name="xls">报表文件</param>
        /// <param name="tablename">表名</param>
        /// <param name="colStart">起始票号列</param>
        /// <param name="colEnd">终止票号列</param>
        /// <param name="colSail">销售日期列</param>
        /// <param name="colFlight">航班日期列</param>
        public static int ImportExpTickFrom(string xls, string tablename, int colStart, int colEnd, int colSail, int colFlight,bool autoGetTableName)
        {
            int ret = 0;
            int total2insert = 0;
            int insertedcount = 0;
            bool bCvs = xls.ToUpper().EndsWith("CSV");
            string extendedString="";
            if (xls.ToUpper().EndsWith("XLS")) extendedString = ";Extended Properties=Excel 8.0;";
            else if (bCvs) extendedString = ";Extended Properties='text;HDR=Yes;FMT=Delimited;'";
            else throw new Exception("未发现扩展名!请修改文件名后再尝试!");
            DataTable dt = new DataTable();
            if (!autoGetTableName) tablename = "[" + tablename + "$]";

            if (bCvs)
            {
                tablename = getTableNameOfExcel(xls);
                xls = xls.Substring(0, xls.LastIndexOf("\\") + 1);
                
            }

            string sqladapter = "SELECT * FROM " + tablename;
            string provider = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + xls.Replace("/", "\\") + extendedString;
            OleDbDataAdapter adapt = new OleDbDataAdapter(sqladapter, provider);
            if (bCvs) dt = GetDataTableFromCSV(xls, tablename);
            else adapt.Fill(dt);

            string fn = AccessOperation.fn;
            string tn = AccessOperation.tn;
            string ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fn;
            OleDbConnection conn = new OleDbConnection(ConnStr);
            conn.Open();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                long startno = 0;
                long endno = 0;
                try
                {
                    string s = dt.Rows[i][colStart].ToString().Replace("-", "").Trim();
                    if (!long.TryParse(s, out startno)) throw new Exception("startno");
                    s = dt.Rows[i][colEnd].ToString().Replace("-", "").Trim();
                    if (!long.TryParse(s, out endno)) throw new Exception("endno");
                }
                catch
                {
                    continue;
                }


                DateTime date;
                try
                {
                    date = DateTime.Parse(dt.Rows[i][colSail].ToString());
                }
                catch
                {
                    continue;
                }
                DateTime date2;
                try
                {
                    date2 = DateTime.Parse(dt.Rows[i][colFlight].ToString());
                }
                catch
                {
                    date2 = new DateTime(date.Ticks);
                }
                for (long ln = startno; ln <= endno; ln++)
                {
                    
                    string sql = string.Format("select * from {0} where tktNo='{1}'", tn, ln.ToString());
                    OleDbCommand cmd = new OleDbCommand(sql, conn);
                    OleDbDataAdapter adapter2 = new OleDbDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter2.Fill(table);
                    total2insert++;
                    if (table.Rows.Count == 0)
                    {
                        sql = string.Format("insert into {0} (tktNo,sailDate,flightDate) values ('{1}','{2}','{3}')"
                            , tn, ln.ToString(), date.ToString("yyyy-MM-dd"), date2.ToString("yyyy-MM-dd"));
                        cmd = new OleDbCommand(sql, conn);
                        ret += cmd.ExecuteNonQuery();
                    }
                }

            }
            conn.Close();
            if (total2insert == 0) MessageBox.Show("导入了0个记录！请确认文件！若是CSV文件，请转换为XLS文件后再尝试！");
            else MessageBox.Show("共找到"+total2insert.ToString()+"个记录，导入了" + ret.ToString() + "个记录！");
            return ret;
        }
        /// <summary>
        /// 从MU的网支报表导入xls格式,sheet:"销售明细",column:0(起始票号),1(终止票号),15(销售日期)列
        /// </summary>
        /// <param name="muFile"></param>
        public static void ImportExpTickFromMu(string muFile)
        {
            ImportExpTickFrom(muFile, "销售明细", 0, 1, 15, 15,false);
        }
        public static void ImportExpTickFromEu(string euFile)
        {
            ImportExpTickFrom(euFile, "销售报表", 10,10, 12, 12,false);
        }
        public static void ImportExpTickFromCa(string caFile)
        {
            ImportExpTickFrom(caFile, "sheet1", 1, 4, 8);
        }
        public static void ImportExpTickFromFm(string fmFile)
        {
            ImportExpTickFrom(fmFile, "sheet1", 2, 4, 4);//没有销售日期，就取航班日期为销售日期
        }
        public static void ImportExpTickFromZh(string zhFile)
        {
            ImportExpTickFrom(zhFile, "第一页", 4, 5, 1, 14,false);
        }
        public static void ImportExpTickFromSc(string scFile)
        {
            string tablename = getTableNameOfExcel(scFile);
            
            ImportExpTickFrom(scFile, tablename, 0, 1, 16, 16,true);
        }
        public static void ImportExpTickFromHu(string huFile)
        {
            string tablename = getTableNameOfExcel(huFile);

            ImportExpTickFrom(huFile, tablename, 1, 2, 25, 10, true);
        }
        public static string getTableNameOfExcel(string filename)
        {
            string xls = filename.Replace("/", "\\");
            string extendedString = "";
            if (xls.ToUpper().EndsWith("XLS")) extendedString = ";Extended Properties=Excel 8.0;";
            else if (xls.ToUpper().EndsWith("CSV"))
            {
                extendedString = ";Extended Properties=text;";//HDR=Yes;FMT=Delimited;'
                ///CSV中文件名即为表名
                string ret2= xls.Substring(xls.LastIndexOf("\\") + 1);
                return ret2;//.Substring(0, ret2.Length - 4);
            }
            else throw new Exception("未发现扩展名!请修改文件名后再尝试!");
            string ret = "";
            string ConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename.Replace("/", "\\") + extendedString;
            OleDbConnection tempcn = new OleDbConnection();
            tempcn.ConnectionString = ConnString;
            tempcn.Open();
            DataTable tables = tempcn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            ret = "[" + tables.Rows[0]["TABLE_NAME"].ToString().Trim() + "]";
            tempcn.Close();
            return ret;
        }
        private static DataTable GetDataTableFromCSV(string filePath, string fileName)
        {
            string strConn = @"Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=";
            strConn += filePath;                                                         //filePath, For example: C:\
            strConn += ";Extensions=asc,csv,tab,txt;";
            OdbcConnection objConn = new OdbcConnection(strConn);
//            DataSet dsCSV = new DataSet();
            DataTable dsCSV = new DataTable();
            try
            {
                string strSql = "select * from [" + fileName +"]";                      //fileName, For example: 1.csv
                OdbcDataAdapter odbcCSVDataAdapter = new OdbcDataAdapter(strSql, objConn);
                odbcCSVDataAdapter.Fill(dsCSV);
                return dsCSV;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
