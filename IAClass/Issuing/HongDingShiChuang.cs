using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAClass.Issuing;
using IAClass.Entity;
using System.Configuration;

//宏鼎世创
namespace HongDingShiChuang
{
    class Issuing : IIssuing
    {
        //config: username, password, planCode

        public IssuingResultEntity Issue(IssueEntity entity)
        {
            string[] config = entity.IOC_Class_Parameters.Split(',');
            IssuingResultEntity result = new IssuingResultEntity();
            StringBuilder sbPost = new StringBuilder();
            sbPost.Append("username=").Append(config[0]);
            sbPost.Append("&password=").Append(config[1]);
            sbPost.Append("&planCode=").Append(config[2]);
            sbPost.Append("&method=1");
            sbPost.Append("&startTime=").Append(entity.EffectiveDate.ToString("yyyyMMddhhmmss"));
            sbPost.Append("&endTime=").Append(entity.ExpiryDate.ToString("yyyyMMddhhmmss"));
            //投标人信息
            sbPost.Append("&tName=").Append(System.Web.HttpUtility.UrlEncode(entity.Name));
            sbPost.Append("&tBirth=").Append(entity.Birthday.ToString("yyyyMMdd"));
            sbPost.Append("&tSex=").Append(entity.Gender == Gender.Female ? "F" : "M");
            sbPost.Append("&tType=").Append(System.Web.HttpUtility.UrlEncode(entity.IDType == IdentityType.身份证 ? "身份证" : "其他"));
            sbPost.Append("&tId=").Append(entity.ID);
            sbPost.Append("&tMobile=").Append(entity.PhoneNumber);
            //被保人信息
            sbPost.Append("&bName=").Append(System.Web.HttpUtility.UrlEncode(entity.Name));
            sbPost.Append("&bBirth=").Append(entity.Birthday.ToString("yyyyMMdd"));
            sbPost.Append("&bSex=").Append(entity.Gender == Gender.Female ? "F" : "M");
            sbPost.Append("&bType=").Append(System.Web.HttpUtility.UrlEncode(entity.IDType == IdentityType.身份证 ? "身份证" : "其他"));
            sbPost.Append("&bId=").Append(entity.ID);
            sbPost.Append("&bMobile=").Append(entity.PhoneNumber);

            //string post = System.Web.HttpUtility.UrlEncode(sbPost.ToString());
            byte[] data = Encoding.UTF8.GetBytes(sbPost.ToString());
            string ret = Common.HttpPost("http://113.11.197.237/byOther/pingan.do", data);
            if (ret.Contains("success"))
            {
                string key = "保单号：";
                result.PolicyNo = result.Trace.Detail = ret.Substring(ret.IndexOf(key) + key.Length);
            }
            else
            {
                Common.LogIt(sbPost.ToString());
                Common.LogIt(ret);
                result.Trace.ErrorMsg = ret;
            }
            return result;
        }

        public TraceEntity Withdraw(WithdrawEntity entity)
        {
            string[] config = entity.IOC_Class_Parameters.Split(',');
            TraceEntity result = new TraceEntity();
            StringBuilder sbPost = new StringBuilder();
            sbPost.Append("username=").Append(config[0]);
            sbPost.Append("&password=").Append(config[1]);
            sbPost.Append("&recodes=").Append(entity.PolicyNo);
            sbPost.Append("&method=0");

            //string post = System.Web.HttpUtility.UrlEncode(sbPost.ToString());
            byte[] data = Encoding.UTF8.GetBytes(sbPost.ToString());
            string ret = Common.HttpPost("http://113.11.197.237/byOther/pingan.do", data);

            if (!ret.Contains("success"))
            {
                Common.LogIt(ret);
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
