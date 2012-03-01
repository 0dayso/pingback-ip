using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

//namespace IAClass.Bussiness
//{
    /// <summary>
    /// 承保接口的相关数据统计
    /// </summary>
    public class InterfaceStat
    {
        /// <summary>
        /// 已投保
        /// </summary>
        /// <returns></returns>
        public static int CountIssued(DateTime dtStart, DateTime dtEnd, int interfaceId)
        {
            dtStart = dtStart.Date;
            dtEnd = dtEnd.Date.AddDays(1);

            string strSql = @"
Select COUNT(caseNo) from t_Case with(nolock)
where datetime between '{0}' and '{1}' and interface_Id = {2} and isIssued = 1 and isWithdrawed = 0";
            strSql = string.Format(strSql, dtStart, dtEnd.AddSeconds(-1), interfaceId);
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(Common.ConnectionString, CommandType.Text, strSql));
            return count;
        }

        /// <summary>
        /// 已撤销
        /// </summary>
        /// <returns></returns>
        public static int CountWithdrawed(DateTime dtStart, DateTime dtEnd, int interfaceId)
        {
            dtStart = dtStart.Date;
            dtEnd = dtEnd.Date.AddDays(1);

            string strSql = @"
Select COUNT(caseNo) from t_Case with(nolock)
where datetime between '{0}' and '{1}' and interface_Id = {2} and isWithdrawed = 1";
            strSql = string.Format(strSql, dtStart, dtEnd.AddSeconds(-1), interfaceId);
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(Common.ConnectionString, CommandType.Text, strSql));
            return count;
        }

        /// <summary>
        /// 已生效
        /// </summary>
        /// <returns></returns>
        public static int CountDone(DateTime dtStart, DateTime dtEnd, int interfaceId)
        {
            dtStart = dtStart.Date;
            dtEnd = dtEnd.Date.AddDays(1);

            string strSql = @"
Select COUNT(caseNo) from t_Case with(nolock)
where datetime between '{0}' and '{1}' and interface_Id = {2} and isIssued = 1 and isWithdrawed = 0 and customerFlightDate < '{3}'";
            strSql = string.Format(strSql, dtStart, dtEnd.AddSeconds(-1), interfaceId, dtEnd);
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(Common.ConnectionString, CommandType.Text, strSql));
            return count;
        }
    }
//}
