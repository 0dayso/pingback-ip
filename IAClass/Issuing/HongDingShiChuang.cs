using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAClass.Issuing;
using IAClass.Entity;

//宏鼎世创
namespace HongDingShiChuang
{
    class Issuing : IIssuing
    {
        static string username = "LIMING";
        static string password = "99999";
        static string planCode = "P0122E11";

        public IssuingResultEntity Issue(IssueEntity entity)
        {
            IssuingResultEntity result = new IssuingResultEntity();
            StringBuilder sbPost = new StringBuilder();
            sbPost.Append("username=").Append(username);
            sbPost.Append("&password=").Append(password);
            sbPost.Append("&planCode=").Append(planCode);
            sbPost.Append("&method=1");
            sbPost.Append("&startTime=").Append(entity.EffectiveDate.ToString("yyyyMMddhhmmss"));
            sbPost.Append("&endTime=").Append(entity.ExpiryDate.ToString("yyyyMMddhhmmss"));
            //投标人信息
            sbPost.Append("&tName=").Append(entity.Name);
            sbPost.Append("&tSex=").Append(entity.Gender == Gender.Female ? "F" : "M");
            sbPost.Append("&tId=").Append(entity.ID);
            //被保人信息
            sbPost.Append("&bName=").Append(entity.Name);
            sbPost.Append("&bBirth=").Append(entity.Birthday.ToString("yyyyMMdd"));
            sbPost.Append("&bSex=").Append(entity.Gender == Gender.Female ? "F" : "M");
            sbPost.Append("&bType=").Append(entity.IDType == IdentityType.身份证 ? "身份证" : "其他");
            sbPost.Append("&bId=").Append(entity.ID);
            sbPost.Append("&bMobile =").Append(entity.PhoneNumber);

            string post = System.Web.HttpUtility.UrlEncode(sbPost.ToString());
            byte[] data = Encoding.UTF8.GetBytes(post);
            string ret = Common.HttpPost("http://113.11.197.237/byOther/pingan.do", data);
            if (ret.Contains("success"))
            {
                string key = "保单号：";
                result.PolicyNo = result.Trace.Detail = ret.Substring(ret.IndexOf(key) + key.Length);
            }
            else
            {
                result.Trace.ErrorMsg = ret;
            }
            return result;
        }

        public TraceEntity Withdraw(WithdrawEntity entity)
        {
            TraceEntity result = new TraceEntity();
            StringBuilder sbPost = new StringBuilder();
            sbPost.Append("username=").Append(username);
            sbPost.Append("&password=").Append(password);
            sbPost.Append("&recodes=").Append(entity.PolicyNo);
            sbPost.Append("&method=0");

            string post = System.Web.HttpUtility.UrlEncode(sbPost.ToString());
            byte[] data = Encoding.UTF8.GetBytes(post);
            string ret = Common.HttpPost("http://113.11.197.237/byOther/pingan.do", data);

            if (!ret.Contains("success"))
            {
                result.ErrorMsg = ret;
            }

            return result;
        }

        public TraceEntity Validate(IssueEntity entity)
        {
            TraceEntity result = new TraceEntity();
            return result;
        }
    }
}
