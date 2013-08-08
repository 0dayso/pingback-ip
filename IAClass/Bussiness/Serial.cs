using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data.SqlClient;
using IAClass;

//namespace IAClass.Bussiness
//{
    public class Serial
    {
        public static int GetBalance(string username)
        {
            string strSql = "select COUNT(caseNo) from t_Serial with(nolock) where caseOwner = @username";
            object s = SqlHelper.ExecuteScalar(Common.ConnectionString, CommandType.Text, strSql, new SqlParameter("@username", username));
            return Convert.ToInt32(s);
        }

        /// <summary>
        /// 创建号段
        /// </summary>
        /// <param name="pipelineStart"></param>
        /// <param name="pipelineEnd"></param>
        /// <param name="insurer"></param>
        /// <param name="insurerDisplayName"></param>
        /// <param name="locationInclude"></param>
        /// <param name="locationExclude"></param>
        /// <returns></returns>
        public static int Create(string pipelineStart, string pipelineEnd, string insurer, string insurerDisplayName,
            string locationInclude, string locationExclude, string locationComment)
        {
            string[] arrangedNumber = Common.GetArranged(pipelineStart, pipelineEnd);

            if (arrangedNumber.Length == 0)
            {
                throw new Exception("创建失败，请正确填写起止单证号！");
            }

            object countDB = Common.DB.Select(Tables.t_Serial, Tables.t_Serial.caseNo.Count())
                                        .Where(Tables.t_Serial.caseNo.Between(pipelineStart, pipelineEnd))
                                        .ToScalar();
            if (Convert.ToInt32(countDB) > 0)
            {
                throw new Exception("该号段已在数据库中存在，请正确填写起止单证号！");
            }

            countDB = Common.DB.Select(Tables.t_Case, Tables.t_Case.caseNo.Count())
                                        .Where(Tables.t_Case.caseNo.Between(pipelineStart, pipelineEnd))
                                        .ToScalar();
            if (Convert.ToInt32(countDB) > 0)
            {
                throw new Exception("该号段已被使用，请正确填写起止单证号！");
            }

            int count = arrangedNumber.Length;
            string strPipelineNumberStart = StringHelper.InterceptNumber(pipelineStart);
            string strPipelinePrefix = pipelineStart.Replace(strPipelineNumberStart, string.Empty);

            /*2010.08.07
             * 使用组合所有Insert语句,变成一个超长SQL一次性提交给数据库执行的方式,大大提高了大批量插入的效率!!!
             * 但是该方法对web和database不在一个机器,如跨公网的情况可能未必有效(字符串过于庞大)
            */
            StringBuilder sqlBatch = new StringBuilder("BEGIN TRANSACTION; ");
            string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            foreach (string number in arrangedNumber)
            {
                string sql = "Insert into t_Serial(caseNo, caseOwner, caseSupplier, datetime, serialPrefix, locationInclude, locationExclude, locationComment) values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}'); ";
                sql = string.Format(sql, number,
                    HttpContext.Current.User.Identity.Name, insurer, datetime, strPipelinePrefix, locationInclude, locationExclude, locationComment);

                sqlBatch.Append(sql);
            }

            sqlBatch.Append("COMMIT  TRANSACTION;");
            string connStr = Common.ConnectionString;
            using (SqlConnection cnn = new SqlConnection(connStr))
            {
                SqlCommand cmm = new SqlCommand("", cnn);
                cmm.CommandText = sqlBatch.ToString();
                cnn.Open();
                cmm.ExecuteNonQuery();
                cnn.Close();
            }

            Common.DB.Insert(Tables.t_Log)
                     .AddColumn(Tables.t_Log.LogContent, "创建 " + insurerDisplayName + "(" + pipelineStart + "-" + pipelineEnd + ") 共 " + count + " 个单证号")
                     .AddColumn(Tables.t_Log.LogType, "创建")
                     .AddColumn(Tables.t_Log.LogOwner, HttpContext.Current.User.Identity.Name)
                     .AddColumn(Tables.t_Log.datetime, DateTime.Now)
                     .AddColumn(Tables.t_Log.amount, count)
                     .AddColumn(Tables.t_Log.IP,System.Web.HttpContext.Current.Request.UserHostAddress)
                     .Execute();
            return count;
        }

        /// <summary>
        /// 分配号段
        /// </summary>
        /// <param name="pipelineStart"></param>
        /// <param name="pipelineEnd"></param>
        /// <param name="fromUser"></param>
        /// <param name="toUser"></param>
        /// <returns></returns>
        public static int Assign(string pipelineStart, string pipelineEnd, string userParent, string userChild)
        {
            object parent = Common.DB.Select(Tables.t_User, Tables.t_User.parent)
                            .Where(Tables.t_User.username == userChild)
                            .ToScalar();

            if (parent == null || parent.ToString().ToUpper() != HttpContext.Current.User.Identity.Name.ToUpper())
            {
                throw new Exception("您无权分配该用户的号段！");
            }

            string[] arrangedNumber = Common.GetArranged(pipelineStart, pipelineEnd);

            if (arrangedNumber.Length == 0)
            {
                throw new Exception("创建失败，请正确填写起止单证号！");
            }

            int count = arrangedNumber.Length;
            object countDB = Common.DB.Select(Tables.t_Serial, Tables.t_Serial.caseNo.Count())
                                        .Where(Tables.t_Serial.caseNo.Between(pipelineStart, pipelineEnd))
                                        .Where(Tables.t_Serial.caseOwner == userParent)
                                        .ToScalar();
            if (Convert.ToInt32(countDB) != count)
            {
                throw new Exception("号段有误，请检查！");
            }

            string strPipelineNumberStart = StringHelper.InterceptNumber(pipelineStart);
            string strPipelinePrefix = pipelineStart.Replace(strPipelineNumberStart, string.Empty);

            StringBuilder sqlBatch = new StringBuilder("BEGIN TRANSACTION; ");
            string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            foreach (string number in arrangedNumber)
            {
                string sql = "update t_Serial set caseOwner = '{0}' where caseNo = '{1}' and caseOwner = '{2}'; ";
                sql = string.Format(sql, userChild, number, userParent);

                sqlBatch.Append(sql);
            }

            sqlBatch.Append("COMMIT  TRANSACTION;");
            string connStr = Common.ConnectionString;
            using (SqlConnection cnn = new SqlConnection(connStr))
            {
                SqlCommand cmm = new SqlCommand("", cnn);
                cmm.CommandText = sqlBatch.ToString();
                cnn.Open();
                cmm.ExecuteNonQuery();
                cnn.Close();
            }

            string userDisplayname = Common.DB.Select(Tables.t_User, Tables.t_User.displayname)
                                                    .Where(Tables.t_User.username == userChild)
                                                    .ToScalar().ToString();

            Common.DB.Insert(Tables.t_Log)
                .AddColumn(Tables.t_Log.LogContent, "分配 " + pipelineStart + "－" + pipelineEnd + " 共 " + count + " 个单证号给 " + userDisplayname + "(" + userChild + ")")
                .AddColumn(Tables.t_Log.LogType, "分配")
                .AddColumn(Tables.t_Log.LogOwner, userParent)
                .AddColumn(Tables.t_Log.datetime, datetime)
                .AddColumn(Tables.t_Log.amount, count)
                .AddColumn(Tables.t_Log.IP, System.Web.HttpContext.Current.Request.UserHostAddress)
                .Execute();

            Common.DB.Insert(Tables.t_Log)
                .AddColumn(Tables.t_Log.LogContent, "获得 " + pipelineStart + "－" + pipelineEnd + " 共 " + count + " 个单证号")
                .AddColumn(Tables.t_Log.LogType, "获得")
                .AddColumn(Tables.t_Log.LogOwner, userChild)
                .AddColumn(Tables.t_Log.datetime, datetime)
                .AddColumn(Tables.t_Log.amount, count)
                .AddColumn(Tables.t_Log.IP, System.Web.HttpContext.Current.Request.UserHostAddress)
                .Execute();

            return count;
        }

        /// <summary>
        /// 收回号段
        /// </summary>
        /// <param name="pipelineStart"></param>
        /// <param name="pipelineEnd"></param>
        /// <param name="fromUser"></param>
        /// <param name="toUser"></param>
        /// <returns></returns>
        public static int Withdrawal(string pipelineStart, string pipelineEnd, string userParent, string userChild)
        {
            object parent = Common.DB.Select(Tables.t_User, Tables.t_User.parent)
                            .Where(Tables.t_User.username == userChild)
                            .ToScalar();

            if (parent == null || parent.ToString().ToUpper() != HttpContext.Current.User.Identity.Name.ToUpper())
            {
                throw new Exception("您无权收回该用户的号段！");
            }

            string[] arrangedNumber = Common.GetArranged(pipelineStart, pipelineEnd);

            if (arrangedNumber.Length == 0)
            {
                throw new Exception("收回失败，请正确填写起止单证号！");
            }

            int count = arrangedNumber.Length;
            string strPipelineNumberStart = StringHelper.InterceptNumber(pipelineStart);
            string strPipelinePrefix = pipelineStart.Replace(strPipelineNumberStart, string.Empty);

            StringBuilder sqlBatch = new StringBuilder("BEGIN TRANSACTION; ");
            string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            foreach (string number in arrangedNumber)
            {
                string sql = "update t_Serial set caseOwner = '{0}' where caseNo = '{1}' and caseOwner = '{2}'; ";
                sql = string.Format(sql, userParent, number, userChild);

                sqlBatch.Append(sql);
            }

            sqlBatch.Append("COMMIT  TRANSACTION;");
            string connStr = Common.ConnectionString;
            using (SqlConnection cnn = new SqlConnection(connStr))
            {
                SqlCommand cmm = new SqlCommand("", cnn);
                cmm.CommandText = sqlBatch.ToString();
                cnn.Open();
                cmm.ExecuteNonQuery();
                cnn.Close();
            }

            string userDisplayname = Common.DB.Select(Tables.t_User, Tables.t_User.displayname)
                                                    .Where(Tables.t_User.username == userChild)
                                                    .ToScalar().ToString();

            Common.DB.Insert(Tables.t_Log)
                .AddColumn(Tables.t_Log.LogContent, "从用户 " + userDisplayname + "(" + userChild + ")" + " 收回 " + pipelineStart + "－" + pipelineEnd + " 共 " + count + " 个单证号！ ")
                .AddColumn(Tables.t_Log.LogType, "收回")
                .AddColumn(Tables.t_Log.LogOwner, userParent)
                .AddColumn(Tables.t_Log.datetime, datetime)
                .AddColumn(Tables.t_Log.amount, count)
                .AddColumn(Tables.t_Log.IP, System.Web.HttpContext.Current.Request.UserHostAddress)
                .Execute();

            Common.DB.Insert(Tables.t_Log)
                .AddColumn(Tables.t_Log.LogContent, "被收回 " + pipelineStart + "－" + pipelineEnd + " 共 " + count + " 个单证号！")
                .AddColumn(Tables.t_Log.LogType, "被收回")
                .AddColumn(Tables.t_Log.LogOwner, userChild)
                .AddColumn(Tables.t_Log.datetime, datetime)
                .AddColumn(Tables.t_Log.amount, count)
                .AddColumn(Tables.t_Log.IP, System.Web.HttpContext.Current.Request.UserHostAddress)
                .Execute();

            return count;
        }
    }
//}
