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
                result.PolicyNo = result.Trace.Detail = Common.InterceptNumber(ret);
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
            string[] config = entity.IOC_Class_Parameters.Split(',');
            string ret = string.Empty;

            try
            {
                ret = webService.policyCancel(config[0], config[1], entity.PolicyNo);

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

    public class Issuing_HuaXia : IAClass.Issuing.IIssuing
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
                ret = webService.alterableApproval_hx(config[0], config[1], birth, entity.ID, idType, entity.Name, gender,
                                                    entity.FlightNo, "", birth, entity.ID, idType, entity.Name, gender,
                                                    entity.PhoneNumber, entity.EffectiveDate.ToString("yyyy-MM-dd"), config[2]);

                if (string.IsNullOrEmpty(ret))
                    throw new Exception("西安奇易WebService返回为空！");
            }
            catch
            {
                Common.LogIt(webService.Url);
                throw;
            }

            //正常返回是：
/*
<?xml version="1.0" encoding="UTF-8"?>
<TranData>
  <Head>
    <TranCom>06</TranCom>
    <FuncFlag>0101</FuncFlag>
    <TranDate>2012-07-02</TranDate>
    <TranTime>11:44:51</TranTime>
    <TranNo>20120702114451</TranNo>
    <BankCode>06</BankCode>
    <TranSeq>ZZZZ</TranSeq>
    <Flag>0</Flag>
    <Desc />
  </Head>
  <Body>
    <ContNo>21300000440168</ContNo>
    <SignDate />
    <SignTime />
    <CValiDate>2012-08-29</CValiDate>
    <CValiTime>00:00:00</CValiTime>
  </Body>
</TranData>
 * */
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(ret);
            }
            catch
            {
                Common.LogIt("西安奇易Issuing_HuaXia返回：" + ret);
                throw;
            }

            if (doc.SelectSingleNode("TranData/Head/Flag").InnerText == "0")
            {
                result.PolicyNo = result.Trace.Detail = doc.SelectSingleNode("TranData/Body/ContNo").InnerText;
            }
            else
            {
                Common.LogIt("西安奇易Issuing_HuaXia返回：" + ret);
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
                ret = webService.policyCancel_hx(config[0], config[1], entity.PolicyNo);

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
