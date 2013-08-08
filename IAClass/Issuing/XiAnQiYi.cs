using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAClass;
using IAClass.Entity;
using System.Xml;

namespace XiAnQiYi
{
    public class Issuing : IAClass.Issuing.IIssuing
    {
        static Service webService = new Service();

        public TraceEntity Validate(IssueEntity entity)
        {
            return new TraceEntity();
        }

        /// <summary>
        /// 投保
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IssuingResultEntity Issue(IssueEntity entity)
        {
            IssuingResultEntity result = new IssuingResultEntity();

            string birth = entity.Birthday.ToString("yyyy-MM-dd");
            string idType = GetIdType(entity.IDType);
            string gender = GetGender(entity.Gender);
            string[] config = entity.IOC_Class_Parameters.Split(',');

            string ret = string.Empty;
            try
            {
                ret = webService.alterableApproval(config[0], config[1], birth, entity.ID, idType, entity.Name, gender, "",
                                                    "", "", "",
                                                    entity.FlightNo, "302", birth, entity.ID, idType, entity.Name, gender,
                                                    entity.PhoneNumber, entity.EffectiveDate.ToString("yyyy-MM-dd HH:mm:ss"), config[2]);

                if (string.IsNullOrEmpty(ret))
                    throw new Exception("西安奇易alterableApproval返回为空！");
            }
            catch
            {
                Common.LogIt(webService.Url);
                throw;
            }

            //正常返回是：“投保成功！保单号：020471140216954”
            if (ret.Contains("投保成功"))
            {
                result.PolicyNo = result.Trace.Detail = StringHelper.InterceptNumber(ret);
            }
            else if (ret.Contains("份数超限"))
            {
                result.PolicyNo = entity.CaseNo;
                result.Trace.Detail = ret;
            }
            else
            {
                //Common.LogIt(entity.Title + "西安奇易Issue：" + ret);
                result.Trace.ErrorMsg = ret;
            }

            return result;
        }

        /// <summary>
        /// 退保
        /// </summary>
        /// <param name="policyNo"></param>
        /// <returns></returns>
        public TraceEntity Withdraw(WithdrawEntity entity)
        {
            TraceEntity result = new TraceEntity();
            string[] config = entity.IOC_Class_Parameters.Split(',');
            string ret = string.Empty;

            try
            {
                ret = webService.policyCancel(config[0], config[1], entity.PolicyNo);

                if (string.IsNullOrEmpty(ret))
                    throw new Exception("西安奇易policyCancel返回为空！");
            }
            catch
            {
                Common.LogIt(webService.Url);
                throw;
            }

            if (ret.Contains("撤销成功"))
                return result;
            else
            {
                Common.LogIt("西安奇易policyCancel：" + ret);
                result.ErrorMsg = ret;
                return result;
            }
        }

        static string GetIdType(IdentityType type)
        {
            switch (type)
            {
                case IdentityType.身份证:
                    return "0";
                case IdentityType.护照:
                    return "2";
                case IdentityType.军官证:
                    return "1";
                default:
                    return "4";//其他
            }
        }

        static string GetGender(Gender gender)
        {
            switch (gender)
            {
                case Gender.Female:
                    return "2";
                default:
                    return "1";
            }
        }
    }

    public class Issuing_Xinhua : IAClass.Issuing.IIssuing
    {
        static Service webService = new Service();

        public TraceEntity Validate(IssueEntity entity)
        {
            return new TraceEntity();
        }

        /// <summary>
        /// 投保
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IssuingResultEntity Issue(IssueEntity entity)
        {
            IssuingResultEntity result = new IssuingResultEntity();

            string birth = entity.Birthday.ToString("yyyy-MM-dd");
            string idType = "5";// GetIdType(entity.IDType);
            string gender = entity.Gender == Gender.Female ? "0" : "1";
            if (string.IsNullOrEmpty(entity.PhoneNumber))
                entity.PhoneNumber = "13888092959";
            string[] config = entity.IOC_Class_Parameters.Split(',');

            string ret = string.Empty;
            try
            {
                ret = webService.alterableApproval_LB(config[0], config[1], birth, entity.ID, idType, entity.Name, gender,
                                                    "", entity.ID, idType, entity.Name, entity.FlightNo, "01", birth, entity.ID, idType, entity.Name, gender,
                                                    entity.PhoneNumber, entity.EffectiveDate.ToString("yyyy-MM-dd"), entity.ExpiryDate.ToString("yyyy-MM-dd"), config[2]);

                if (string.IsNullOrEmpty(ret))
                    throw new Exception("西安奇易alterableApproval_LB返回为空！");
            }
            catch
            {
                Common.LogIt(webService.Url);
                throw;
            }

            //正常返回是：
            /*
            投保成功！保单号：662206804374
             * */
            if (ret.Contains("投保成功"))
            {
                result.PolicyNo = result.Trace.Detail = StringHelper.InterceptNumber(ret);
            }
            else if (ret.Contains("份数超限"))
            {
                result.PolicyNo = entity.CaseNo;
                result.Trace.Detail = ret;
            }
            else
            {
                Common.LogIt(entity.Title + "西安奇易alterableApproval_LB：" + ret);
                result.Trace.ErrorMsg = ret;
            }

            return result;
        }

        /// <summary>
        /// 退保
        /// </summary>
        /// <param name="policyNo"></param>
        /// <returns></returns>
        public TraceEntity Withdraw(WithdrawEntity entity)
        {
            TraceEntity result = new TraceEntity();
            string[] config = entity.IOC_Class_Parameters.Split(',');
            string ret = string.Empty;

            try
            {
                ret = webService.policyCancel_LB(config[0], config[1], entity.PolicyNo);

                if (string.IsNullOrEmpty(ret))
                    throw new Exception("西安奇易policyCancel_LB返回为空！");
            }
            catch
            {
                Common.LogIt(webService.Url);
                throw;
            }

            if (ret.Contains("撤销成功"))
                return result;
            else
            {
                Common.LogIt("西安奇易policyCancel_LB：" + ret);
                result.ErrorMsg = ret;
                return result;
            }
        }

        static string GetIdType(IdentityType type)
        {
            switch (type)
            {
                case IdentityType.身份证:
                    return "0";
                case IdentityType.护照:
                    return "2";
                case IdentityType.军官证:
                    return "1";
                default:
                    return "4";//其他
            }
        }

        static string GetGender(Gender gender)
        {
            switch (gender)
            {
                case Gender.Female:
                    return "2";
                default:
                    return "1";
            }
        }
    }
}
