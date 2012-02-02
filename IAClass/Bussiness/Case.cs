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
using System.Net;
using System.IO;
using IAClass.Entity;
using IAClass.Issuing;

//namespace IAClass
//{
    /// <summary>
    /// Case 的摘要说明
    /// </summary>
    public class Case
    {
        /// <summary>
        /// 实时投保，需要实时返回保单号
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static IssuingResultEntity Issue(IssueEntity entity)
        {
            IssuingFacade facade = new IssuingFacade();
            IssuingResultEntity result = facade.Issue(entity);

            if (string.IsNullOrEmpty(result.Trace.ErrorMsg))
            {
                string strSql = "";

                if (string.IsNullOrEmpty(result.PolicyNo))//没有保单号
                {
                    result.Trace.ErrorMsg = "投保失败，没有返回保单号！";
                }
                else
                {
                    //主键更新,不会阻塞  保存返回的正式保单号
                    strSql = "update t_case set certNo = '{0}', [isIssued] = 1 where caseNo = '{1}'";
                    strSql = string.Format(strSql, result.PolicyNo, entity.CaseNo);
                    if (entity.DbCommand != null)
                    {
                        entity.DbCommand.CommandText = strSql;
                        entity.DbCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlHelper.ExecuteNonQuery(Common.ConnectionString, CommandType.Text, strSql);
                    }
                }

                return result;
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// 延迟投保，可以追溯
        /// </summary>
        /// <param name="entityObj"></param>
        public static IssuingResultEntity IssueAsync(object entityObj)
        {
            string strSql = "";
            try
            {
                IssueEntity entity = (IssueEntity)entityObj;
                IssuingFacade facade = new IssuingFacade();
                IssuingResultEntity result = facade.Issue(entity);

                if (string.IsNullOrEmpty(result.Trace.ErrorMsg))
                {
                    if (string.IsNullOrEmpty(result.PolicyNo))//没有保单号
                    {
                        result.Trace.ErrorMsg = "投保失败，没有返回保单号！？";
                        strSql = "update t_case set IssuingFailed = @IssuingFailed where caseNo = @caseNo";
                        SqlHelper.ExecuteNonQuery(Common.ConnectionString, CommandType.Text, strSql,
                            new string[] { "@IssuingFailed", "@caseNo" },
                            new object[] { result.Trace.ErrorMsg, entity.CaseNo });
                    }
                    else
                    {
                        //主键更新,不会阻塞  保存返回的正式保单号
                        strSql = "update t_case set certNo = '{0}', [isIssued] = 1 where caseNo = '{1}'";
                        strSql = string.Format(strSql, result.PolicyNo, entity.CaseNo);
                        SqlHelper.ExecuteNonQuery(Common.ConnectionString, CommandType.Text, strSql);
                    }
                }
                else
                {
                    strSql = "update t_case set IssuingFailed = @IssuingFailed where caseNo = @caseNo";
                    SqlHelper.ExecuteNonQuery(Common.ConnectionString, CommandType.Text, strSql,
                        new string[] { "@IssuingFailed", "@caseNo" },
                        new object[] { result.Trace.ErrorMsg, entity.CaseNo });
                }

                return result;
            }
            catch(Exception e)
            {
                Common.LogIt("IssueAsync异常：" + Environment.NewLine + strSql + Environment.NewLine + e.ToString());
                throw;
            }
        }

        public static object TopEveryday(DateTime date, string productId)
        {
            string strSql = @"
select COUNT(caseNo) from t_Case with(nolock)
where datetime between '{0}' and '{1}'
and enabled = 1";

            if (productId == "0")//所有产品
                strSql = string.Format(strSql, date, date.AddDays(1).AddSeconds(-1));
            else
            {
                strSql += " and productID = '{2}'";
                strSql = string.Format(strSql, date, date.AddDays(1).AddSeconds(-1), productId);
            }

            return SqlHelper.ExecuteScalar(Common.ConnectionString, CommandType.Text, strSql);
        }

        public static DataSet TopToday(string dateStart, string dateEnd, string productId)
        {
            string strSql = @"
SELECT     TOP 5 b.displayname, COUNT(a.datetime) AS count
FROM         t_Case AS a with(nolock) INNER JOIN
                      t_User AS b with(nolock) ON a.caseOwner = b.username
WHERE     (a.enabled = 1) and (a.datetime between @date1 and @date2) {0}
GROUP BY b.displayname
ORDER BY count DESC";

            if (productId == "0")
                strSql = string.Format(strSql, string.Empty);
            else
                strSql = string.Format(strSql, " and a.productID = '" + productId + "'");

            DateTime dt1, dt2;

            if (string.IsNullOrEmpty(dateStart.Trim()))
                dt1 = DateTime.Today;
            else
                dt1 = DateTime.Parse(dateStart.Trim());

            if (string.IsNullOrEmpty(dateEnd.Trim()))
                dt2 = dt1.AddDays(1).AddSeconds(-1);
            else
                dt2 = DateTime.Parse(dateEnd.Trim()).AddDays(1).AddSeconds(-1);

            SqlCommand comm = new SqlCommand(strSql);
            comm.Parameters.Add(new SqlParameter("@date1", dt1));
            comm.Parameters.Add(new SqlParameter("@date2", dt2));

            DataSet ds = Common.DB.ExecuteDataSet(comm);
            return ds;
        }

        public static bool IsIssued(DateTime flightDate, string idNo)
        {
            //这里非主键select虽然用readpast依然阻塞 使用with(nolock)来躲开其他线程的更新锁等，以避免阻塞
            string strSql = @"
SELECT caseID
  FROM t_Case with(nolock)
  where customerFlightDate = '{0}'
  and customerID = '{1}'
  and enabled = 1";
            strSql = string.Format(strSql, flightDate, idNo);
            DataSet dsSerial = new DataSet();
            using (SqlConnection cnn = new SqlConnection(Common.ConnectionString))
            {
                SqlCommand cmm = new SqlCommand(strSql, cnn);
                SqlDataAdapter sda = new SqlDataAdapter(cmm);
                sda.Fill(dsSerial);
            }

            if (dsSerial.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 检查手机号在当天被使用的次数
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static int CountMobile(string mobile, string username)
        {
            DateTime dt1 = DateTime.Today;
            DateTime dt2 = dt1.AddDays(1);

            string strSql = @"
select COUNT(caseNo)
from t_Case with(nolock)
where customerPhone = '{0}'
and datetime between '{1}' and '{2}'
and caseOwner = '{3}'
and enabled = 1";
            strSql = string.Format(strSql, mobile, dt1, dt2, username);
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(Common.ConnectionString, CommandType.Text, strSql));
            return count;
        }

        public static TraceEntity Discard(string username, string caseNo)
        {
            TraceEntity response = new TraceEntity();

            string strSql = @"
SELECT caseID, customerFlightDate, CertNo,
		b.IsIssuingRequired, IOC_TypeName, WithdrawRatio, caseOwner, ParentPath
  FROM [t_Case] a with(nolock) inner join t_Product b
  on a.productID = b.productID
  where caseNo = '{0}'";
            strSql = string.Format(strSql, caseNo);
            DataSet ds = SqlHelper.ExecuteDataset(Common.ConnectionString, CommandType.Text, strSql);

            if (ds.Tables[0].Rows.Count == 0)
            {
                response.ErrorMsg = "单证号不存在！";
                return response;
            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                string caseOwner = dr["caseOwner"].ToString();
                if (username != caseOwner)
                {
                    response.ErrorMsg = "单证号不属于该用户！";
                    return response;
                }

                bool isIssuingRequired = Convert.ToBoolean(dr["IsIssuingRequired"]);
                //bool withdrawOnEffDateEnabled = Convert.ToBoolean(dr["WithdrawOnEffDateEnabled"]);//是否可以在生效当日（起飞前）进行退保操作
                decimal withdrawRatio = Convert.ToDecimal(dr["WithdrawRatio"]);
                DateTime dtFlightDate = Convert.ToDateTime(dr["customerFlightDate"]);
                DateTime dtNow = DateTime.Now;

                t_User user = UserClass.GetUser(username);
                if (user.CountConsumed > 100)
                {
                    decimal ratio = (decimal)user.CountWithdrawed / (decimal)user.CountConsumed;
                    if (ratio > withdrawRatio)
                    {
                        response.ErrorMsg = "系统检测到您目前的作废率过高，暂无法作废！";
                        return response;
                    }
                }

                if (dtFlightDate < dtNow)
                {
                    response.ErrorMsg = "已生效，无法作废！";
                    return response;
                }
                //else if (!withdrawOnEffDateEnabled)
                //{
                //    if (dtFlightDate.Date == dtNow.Date)
                //    {
                //        response.ErrorMsg = "乘机当日零点起已生效，无法作废！";
                //        return response;
                //    }
                //}

                //bool isOk = true;

                if (isIssuingRequired)
                {
                    WithdrawEntity entity = new WithdrawEntity();
                    entity.CaseNo = caseNo;
                    entity.PolicyNo = dr["CertNo"].ToString();

                    if (!string.IsNullOrEmpty(entity.PolicyNo))
                    {
                        TraceEntity ret;

                        try
                        {
                            IAClass.Issuing.IssuingFacade facade = new IAClass.Issuing.IssuingFacade();
                            ret = facade.Withdraw(entity, dr["IOC_TypeName"].ToString());
                        }
                        catch (Exception ee)
                        {
                            Common.LogIt(ee.ToString());
                            ret.ErrorMsg = ee.Message;
                        }
                        //若成功退保，则做下标记；若失败，仍然给客户退单
                        if (string.IsNullOrEmpty(ret.ErrorMsg))
                        {
                            Common.DB.Update(Tables.t_Case)
                                                .AddColumn(Tables.t_Case.isWithdrawed, true)
                                                .Where(Tables.t_Case.caseNo == caseNo)
                                                .Execute();
                        }
                    }
                }

                //if (isOk)
                {
                    Common.DB.Update(Tables.t_Case)
                        .AddColumn(Tables.t_Case.enabled, false)
                        .Where(Tables.t_Case.caseNo == caseNo)
                        .Execute();

                    string[] parentArray = dr["ParentPath"].ToString().Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                    if (parentArray.Length > 1)//形如：/admin/tianzhi/bcaaa/bcaaa0150/
                    {
                        Common.DB.Update(Tables.t_User)
                                .AddColumn(Tables.t_User.balance, Tables.t_User.balance + Tables.t_User.price)
                                .Where(Tables.t_User.username == parentArray[1])
                                .Execute();
                    }

                    //不要求完全精确，无需事务处理
                    Common.DB.Update(Tables.t_User)
                        .AddColumn(Tables.t_User.CountWithdrawed, Tables.t_User.CountWithdrawed + 1)
                        .Where(Tables.t_User.username == username)
                        .Execute();
                }

                return response;
            }
        }

        public static TraceEntity Discard(UserLoginResponse user, string caseNo)
        {
            TraceEntity response = new TraceEntity();

            string strSql = @"
SELECT caseID, customerFlightDate, CertNo,
		b.IsIssuingRequired, IOC_TypeName, WithdrawRatio, caseOwner, ParentPath
  FROM [t_Case] a with(nolock) inner join t_Product b
  on a.productID = b.productID
  where caseNo = '{0}'";
            strSql = string.Format(strSql, caseNo);
            DataSet ds = SqlHelper.ExecuteDataset(Common.ConnectionString, CommandType.Text, strSql);

            if (ds.Tables[0].Rows.Count == 0)
            {
                response.ErrorMsg = "单证号不存在！";
                return response;
            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                string caseOwner = dr["caseOwner"].ToString();
                if (user.Username != caseOwner)
                {
                    response.ErrorMsg = "单证号不属于该用户！";
                    return response;
                }

                bool isIssuingRequired = Convert.ToBoolean(dr["IsIssuingRequired"]);
                //bool withdrawOnEffDateEnabled = Convert.ToBoolean(dr["WithdrawOnEffDateEnabled"]);//是否可以在生效当日（起飞前）进行退保操作
                decimal withdrawRatio = Convert.ToDecimal(dr["WithdrawRatio"]);
                DateTime dtFlightDate = Convert.ToDateTime(dr["customerFlightDate"]);
                DateTime dtNow = DateTime.Now;

                if (user.CountConsumed > 100)
                {
                    decimal ratio = (decimal)user.CountWithdrawed / (decimal)user.CountConsumed;
                    if (ratio > withdrawRatio)
                    {
                        response.ErrorMsg = "系统检测到您目前的作废率过高，暂无法作废！";
                        return response;
                    }
                }

                if (dtFlightDate < dtNow)
                {
                    response.ErrorMsg = "已生效，无法作废！";
                    return response;
                }
                //else if (!withdrawOnEffDateEnabled)
                //{
                //    if (dtFlightDate.Date == dtNow.Date)
                //    {
                //        response.ErrorMsg = "乘机当日零点起已生效，无法作废！";
                //        return response;
                //    }
                //}

                //bool isOk = true;

                if (isIssuingRequired)
                {
                    WithdrawEntity entity = new WithdrawEntity();
                    entity.CaseNo = caseNo;
                    entity.PolicyNo = dr["CertNo"].ToString();

                    if (!string.IsNullOrEmpty(entity.PolicyNo))
                    {
                        TraceEntity ret;

                        try
                        {
                            IAClass.Issuing.IssuingFacade facade = new IAClass.Issuing.IssuingFacade();
                            ret = facade.Withdraw(entity, dr["IOC_TypeName"].ToString());
                        }
                        catch (Exception ee)
                        {
                            Common.LogIt(ee.ToString());
                            ret.ErrorMsg = ee.Message;
                        }
                        //若成功退保，则做下标记；若失败，仍然给客户退单
                        if (string.IsNullOrEmpty(ret.ErrorMsg))
                        {
                            Common.DB.Update(Tables.t_Case)
                                                .AddColumn(Tables.t_Case.isWithdrawed, true)
                                                .Where(Tables.t_Case.caseNo == caseNo)
                                                .Execute();
                        }
                    }
                }

                //if (isOk)
                {
                    Common.DB.Update(Tables.t_Case)
                        .AddColumn(Tables.t_Case.enabled, false)
                        .Where(Tables.t_Case.caseNo == caseNo)
                        .Execute();

                    string[] parentArray = dr["ParentPath"].ToString().Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                    if (parentArray.Length > 1)//形如：/admin/tianzhi/bcaaa/bcaaa0150/
                    {
                        Common.DB.Update(Tables.t_User)
                                .AddColumn(Tables.t_User.balance, Tables.t_User.balance + Tables.t_User.price)
                                .Where(Tables.t_User.username == parentArray[1])
                                .Execute();
                    }

                    //不要求完全精确，无需事务处理
                    Common.DB.Update(Tables.t_User)
                        .AddColumn(Tables.t_User.CountWithdrawed, Tables.t_User.CountWithdrawed + 1)
                        .Where(Tables.t_User.username == user.Username)
                        .Execute();
                }

                return response;
            }
        }

        public static t_Case Get(int caseID)
        {
            string strSql = @"
SELECT a.*, b.displayname
  FROM [t_Case] a with(nolock)
  inner join t_user b with(nolock) on a.caseOwner = b.username
  where a.caseId = '{0}'
  order by datetime desc";
            strSql = string.Format(strSql, caseID);
            DataSet ds = SqlHelper.ExecuteDataset(Common.ConnectionString, CommandType.Text, strSql);

            t_Case policy = NBear.Mapping.ObjectConvertor.ToObject<t_Case>(ds.Tables[0].Rows[0]);
            return policy;
        }

        public static Policy[] GetList(string username, int pageSize)
        {
            string strSql = @"
SELECT top {0} caseID, enabled, datetime,
  customerName, customerID, customerFlightNo, customerFlightDate,
  caseNo, CertNo
  FROM [t_Case] a with(nolock)
  where caseOwner = '{1}'
  order by datetime desc";
            strSql = string.Format(strSql, pageSize, username);
            DataSet ds = SqlHelper.ExecuteDataset(Common.ConnectionString, CommandType.Text, strSql);

            Policy[] list = NBear.Mapping.ObjectConvertor.ToArray<DataRow, Policy>(ds.Tables[0]);
            return list;
        }

        public static Policy[] GetPolicyListBetween(string username, DateTime dtStart, DateTime dtEnd)
        {
            string strSql = @"
SELECT caseID, a.enabled, datetime,
  customerName, customerID, customerFlightNo, customerFlightDate, caseNo, CertNo, b.productName
  FROM [t_Case] a with(nolock) inner join t_Product b with(nolock)
  on a.productID = b.productID
  where caseOwner = '{0}' and datetime between '{1}' and '{2}'
  order by datetime desc";
            strSql = string.Format(strSql, username, dtStart, dtEnd);
            DataSet ds = SqlHelper.ExecuteDataset(Common.ConnectionString, CommandType.Text, strSql);

            Policy[] list = NBear.Mapping.ObjectConvertor.ToArray<DataRow, Policy>(ds.Tables[0]);
            return list;
        }

        /// <summary>
        /// 查询指定保险公司指定日的出单量(不含作废)
        /// </summary>
        /// <param name="insurer"></param>
        /// <returns></returns>
        public static int CountEnableddByInsurer(string insurer, DateTime dtDay)
        {
            DateTime dtStart = dtDay.Date;
            DateTime dtEnd = dtStart.AddDays(1).AddSeconds(-1);

            object count = Common.DB.Select(Tables.t_Case, Tables.t_Case.caseNo.Count())
                                    .Where(Tables.t_Case.caseSupplier == insurer)
                                    .Where(Tables.t_Case.datetime.Between(dtStart, dtEnd))
                                    .Where(Tables.t_Case.enabled == true)
                                    .ToScalar();
            return Convert.ToInt32(count);
        }

        /// <summary>
        /// 导出需要手工作废的数据
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public static DataView ExportForAdmin_Discarded(int top, string productId, string txtStart, string txtEnd)
        {
            DateTime dtEnd = DateTime.Parse(txtEnd).AddDays(1).AddSeconds(-1);//只有日期部分，无需转换

            SqlConnection cnn = new SqlConnection(Common.ConnectionString);
            string sql = string.Format(@"select top {0} caseid,
                                [datetime], customerName, customerID,
                                CONVERT(VARCHAR(10),[customerFlightDate],120) as customerFlightDate,
                                enabled, null as userGroup, null as displayname,
                                null as caseNo, null as customerPhone, null as customerFlightNo, isSMSent,
                                customerBirth,customerGender
                                from t_case where productId ={1} and isIssued = 1 and enabled = 0 and [datetime] between '{2}' and '{3}'", top, productId, txtStart, dtEnd);
            SqlDataAdapter sda = new SqlDataAdapter(sql, cnn);
            DataSet ds = new DataSet();
            cnn.Open();
            sda.Fill(ds);

            DataView dv = ds.Tables[0].DefaultView;
            DateTime dtNow = DateTime.Now;
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //友好方式显示出单时间
                //DateTime timespan = Convert.ToDateTime(dr["datetime"]);
                //dr[1] = Common.GetTimeAgo(dtNow, timespan);

                sb.Append(dr[0].ToString() + ",");
            }

            if (sb.Length == 0)
                return dv;

            string sqlUpdate = "update t_case set isIssued = 0 where caseid in (" + sb.ToString().TrimEnd(',') + ")";
            SqlCommand cmm = new SqlCommand(sqlUpdate, cnn);
            cmm.ExecuteNonQuery();
            cnn.Close();

            dv.Sort = "datetime desc, customerFlightDate asc";
            return dv;
        }

        /// <summary>
        /// 导出需要手工投保的数据
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public static DataView ExportForAdmin_NotIssued(int top, string productId, string txtStart, string txtEnd)
        {
            DateTime dtEnd = DateTime.Parse(txtEnd).AddDays(1).AddSeconds(-1);//只有日期部分，无需转换

            SqlConnection cnn = new SqlConnection(Common.ConnectionString);
            string sql = string.Format(@"select top {0} caseid,
                                [datetime], customerName, customerID,
                                CONVERT(VARCHAR(10),[customerFlightDate],120) as customerFlightDate,
                                enabled, null as userGroup, null as displayname,
                                null as caseNo, null as customerPhone, null as customerFlightNo, isSMSent,
                                customerBirth,customerGender
                                from t_case where productId ={1} and isIssued = 0 and enabled = 1 and [datetime] between '{2}' and '{3}'", top, productId, txtStart, dtEnd);
            SqlDataAdapter sda = new SqlDataAdapter(sql, cnn);
            DataSet ds = new DataSet();
            cnn.Open();
            sda.Fill(ds);

            DataView dv = ds.Tables[0].DefaultView;
            DateTime dtNow = DateTime.Now;
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //友好方式显示出单时间
                //DateTime timespan = Convert.ToDateTime(dr["datetime"]);
                //dr[1] = Common.GetTimeAgo(dtNow, timespan);

                sb.Append(dr[0].ToString() + ",");
            }

            if (sb.Length == 0)
                return dv;

            string sqlUpdate = "update t_case set isIssued = 1 where caseid in (" + sb.ToString().TrimEnd(',') + ")";
            SqlCommand cmm = new SqlCommand(sqlUpdate, cnn);
            cmm.ExecuteNonQuery();
            cnn.Close();

            dv.Sort = "datetime desc, customerFlightDate asc";
            return dv;
        }

        /// <summary>
        /// 查询用户(含子级用户)的出单量(含作废的)
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static int CountConsumedIncludingChild(string username)
        {
            return CountConsumedIncludingChild(username, DateTime.Parse("1901-1-1"), DateTime.Parse("2099-1-1"));
        }

        /// <summary>
        /// 查询用户(含子级用户)的出单量(含作废的)
        /// </summary>
        /// <param name="username"></param>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public static int CountConsumedIncludingChild(string username, DateTime dtStart, DateTime dtEnd)
        {
            object count = Common.DB.StoredProcedure("CaseConsumedCount")
                .AddInputParameter("@username", DbType.String, username)
                .AddInputParameter("@startDate", DbType.DateTime, dtStart)
                .AddInputParameter("@endDate", DbType.DateTime, dtEnd)
                .ToScalar();
            return Convert.ToInt32(count);
        }

        /// <summary>
        /// 查询用户(含子级用户)指定日的出单量(不含作废)
        /// </summary>
        /// <param name="username"></param>
        /// <param name="dtDay"></param>
        /// <returns></returns>
        public static int CountEnabledIncludingChild(string username, DateTime dtDay)
        {
            DateTime dtStart = dtDay.Date;
            DateTime dtEnd = dtStart.AddDays(1).AddSeconds(-1);
            return CountEnabledIncludingChild(username, dtStart, dtEnd);
        }

        /// <summary>
        /// 查询用户(含子级用户)的出单量(不含作废)
        /// </summary>
        /// <param name="username"></param>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public static int CountEnabledIncludingChild(string username, DateTime dtStart, DateTime dtEnd)
        {
            object count = Common.DB.StoredProcedure("CaseEnabledCount")
                .AddInputParameter("@username", DbType.String, username)
                .AddInputParameter("@startDate", DbType.DateTime, dtStart)
                .AddInputParameter("@endDate", DbType.DateTime, dtEnd)
                .ToScalar();
            return Convert.ToInt32(count);
        }

        /// <summary>
        /// 查询用户(含子级用户)作废的单证数量
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static int CountDiscardedIncludingChild(string username)
        {
            return CountDiscardedIncludingChild(username, DateTime.Parse("1901-1-1"), DateTime.Parse("2099-1-1"));
        }

        /// <summary>
        /// 查询用户(含子级用户)作废的单证数量
        /// </summary>
        /// <param name="username"></param>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public static int CountDiscardedIncludingChild(string username, DateTime dtStart, DateTime dtEnd)
        {
            object count = Common.DB.StoredProcedure("CaseDiscardedCount")
                .AddInputParameter("@username", DbType.String, username)
                .AddInputParameter("@startDate", DbType.DateTime, dtStart)
                .AddInputParameter("@endDate", DbType.DateTime, dtEnd)
                .ToScalar();
            return Convert.ToInt32(count);
        }
    }
//}