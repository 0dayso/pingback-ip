using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using IAClass;
using IAClass.Entity;

namespace ChinaLife_GD
{
    /// <summary>
    /// 中国人寿广东分公司
    /// </summary>
    public class Issuing : IAClass.Issuing.IIssuing
    {
        static string IssuingURL = System.Configuration.ConfigurationManager.AppSettings["ChinaLife_GD_Issuing"];

        /// <summary>
        /// 投保
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IssuingResultEntity Issue(IssueEntity entity)
        {
            IssuingResultEntity result = new IssuingResultEntity();
            string url = GetIssuingURLWithParam(entity);
            string response = GetResponse(url);
            result = ScanIssuingResult(response);
            return result;
        }

        /// <summary>
        /// 退保（空函数，广州分公司在业务上不支持该功能）
        /// </summary>
        /// <param name="policyNo"></param>
        /// <returns></returns>
        public TraceEntity Withdraw(WithdrawEntity entity)
        {
            TraceEntity result = new TraceEntity();
            result.ErrorMsg = "该产品仅支持退单。";
            return result;
        }

        /// <summary>
        /// 解析返回的字符串
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static IssuingResultEntity ScanIssuingResult(string response)
        {
            /*返回格式：
成功、或者重复投保，均返回：
S$ a历史总投保数|b本日总投保数|c姓名|d证件号码|e起保日期|f出生日期|g性别|投保易编号
失败：
F$失败原因*/
            response = response.Trim();//滤掉换行回车等符号
            IssuingResultEntity result = new IssuingResultEntity();
            
            if (response.StartsWith("S$"))
                result.PolicyNo = response.Substring(response.LastIndexOf('|') + 1);
            else
            {
                result.Trace.ErrorMsg = response;
                Common.LogIt("广东国寿 投保：" + response);
            }

            return result;
        }

        /// <summary>
        /// 构造URL参数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static string GetIssuingURLWithParam(IssueEntity entity)
        {
            try
            {
                BirthAndGender birthAndGender;
                birthAndGender = Common.GetBirthAndSex(entity.ID);
                entity.Birthday = birthAndGender.Birth;
                entity.Gender = birthAndGender.Gender;
            }
            catch
            {
                entity.Birthday = DateTime.Parse("1901-1-1");
                entity.Gender = Gender.Male;
            }

            ////有限的行级排它锁,不会阻塞其他行的读取和主键更新
            //string strSql = "SELECT traceNo1 FROM t_TraceNo with(rowlock,xlock,readpast) where productID=" + entity.ProductId;
            //entity.DbCommand.CommandText = strSql;
            //int traceNo_Total = Convert.ToInt32(entity.DbCommand.ExecuteScalar()) + 1;//历史总投保流水号
            //strSql = "update t_TraceNo set traceNo1 = traceNo1 + 1 where productID=" + entity.ProductId;
            //entity.DbCommand.CommandText = strSql;
            //entity.DbCommand.ExecuteNonQuery();

            ////string strSql;
            ////int traceNo_Total = int.Parse(GetTotal());

            //int traceNo_Total_Today = 0;
            //DateTime dtToday = DateTime.Today;
            //strSql = "SELECT traceNo FROM t_TraceNo_Date with(rowlock,xlock,readpast) where productID=" + entity.ProductId
            //    + " and date='" + dtToday.ToShortDateString() + "'";
            //entity.DbCommand.CommandText = strSql;
            //object obj = entity.DbCommand.ExecuteScalar();//当日总投保数

            //if (obj == null)
            //{
            //    strSql = "insert into t_TraceNo_Date(productID,date,TraceNo) values({0}, '{1}', 0)";
            //    strSql = string.Format(strSql, entity.ProductId, dtToday);
            //    entity.DbCommand.CommandText = strSql;
            //    entity.DbCommand.ExecuteNonQuery();
            //    traceNo_Total_Today = 1;
            //}
            //else
            //{
            //    traceNo_Total_Today = Convert.ToInt32(obj) + 1;
            //}

            //strSql = "update t_TraceNo_Date set traceNo = traceNo + 1 where productID=" + entity.ProductId
            //    + " and date='" + dtToday.ToShortDateString() + "'";
            //entity.DbCommand.CommandText = strSql;
            //entity.DbCommand.ExecuteNonQuery();

            string url = "{0}?d=历史总投保数|本日总投保数|姓名|证件号码|起保日期|出生日期|性别";
            url = string.Format(url, IssuingURL);
            //url = url.Replace("历史总投保数", traceNo_Total.ToString());
            //url = url.Replace("本日总投保数", traceNo_Total_Today.ToString());
            url = url.Replace("历史总投保数", "1");
            url = url.Replace("本日总投保数", "1");
            url = url.Replace("姓名", EncodeName(entity.Name));
            url = url.Replace("证件号码", entity.ID);
            url = url.Replace("起保日期", entity.EffectiveDate.ToString("yyyy-MM-dd"));
            url = url.Replace("出生日期", entity.Birthday.ToString("yyyy-MM-dd"));
            url = url.Replace("性别", entity.Gender == Gender.Male ? "1" : "0");//"1" 男
            return url;
        }

        static string GetTotal()
        {
            string response = GetResponse("http://121.8.126.173:7788/WebRoot/ag_post_application.jsp").Trim();
            int index1 = response.IndexOf("$");
            int index2 = response.IndexOf("|");
            return response.Substring(index1 + 1, index1);
        }

        /// <summary>
        /// 乘客姓名需要用Server.UrlEncode（）格式化，若是winform则用下面函数格式化
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string EncodeName(string str)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str);//转成十进制ASCII码(字节码)
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));//再把二进制转成16进制
            }
            return (sb.ToString());
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        private static string GetResponse(string requestURL)
        {
            HttpWebRequest hwrequest = (HttpWebRequest)System.Net.HttpWebRequest.Create(requestURL);
            hwrequest.KeepAlive = true;
            hwrequest.ContentType = "text/xml";
            hwrequest.Method = "Get";
            hwrequest.AllowAutoRedirect = true;

            try
            {
                HttpWebResponse res = (HttpWebResponse)hwrequest.GetResponse();
                StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                string result = sr.ReadToEnd();
                res.Close();
                sr.Close();
                return result;
            }
            catch
            {
                Common.LogIt(requestURL);
                throw;
            }
        }
    }
}
