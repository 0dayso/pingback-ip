using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAClass;
using IAClass.Entity;

namespace XiAnQiYi
{
    public class Issuing : IAClass.Issuing.IIssuing
    {
        static Service webService = new Service();

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

            string ret = string.Empty;
            try
            {
                ret = webService.alterableApproval("13888092959", "123456", birth, entity.ID, idType, entity.Name, gender, "",
                                                    "", "", "",
                                                    entity.FlightNo, "302", birth, entity.ID, idType, entity.Name, gender,
                                                    entity.PhoneNumber, entity.EffectiveDate.ToString("yyyy-MM-dd HH:mm:ss"), "DZHK02_07");

                if (string.IsNullOrEmpty(ret))
                    throw new Exception("西安奇易WebService返回为空！");
            }
            catch
            {
                Common.LogIt(webService.Url);
                throw;
            }

            //正常返回是：“投保成功！保单号：020471140216954”
            if (ret.Contains("投保成功"))
            {
                result.PolicyNo = Common.InterceptNumber(ret);
            }
            else
            {
                Common.LogIt("西安奇易Issue：" + ret);
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

            string ret = string.Empty;
            try
            {
                ret = webService.policyCancel("13888092959", "123456", entity.PolicyNo);

                if (string.IsNullOrEmpty(ret))
                    throw new Exception("西安奇易WebService返回为空！");
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
                Common.LogIt("西安奇易Withdraw：" + ret);
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
                case IdentityType.港澳通行证:
                    return "4";
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
