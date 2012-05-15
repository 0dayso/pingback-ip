using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAClass.Issuing;
using IAClass.Entity;

namespace ziyoux
{
    public class Issuing : IIssuing
    {
        static Insure_WebService ws = new Insure_WebService();

        public IssuingResultEntity Issue(IssueEntity entity)
        {
            IssuingResultEntity result = new IssuingResultEntity();
            if (string.IsNullOrEmpty(entity.PhoneNumber))
                entity.PhoneNumber = "18611586620";
            if (entity.Birthday.Date == DateTime.Today)
                entity.Birthday = DateTime.Parse("1980-01-01");
            //agent,sesscode,productno,productname 用户名,密码,产品代码,产品名称
            string[] config = entity.IOC_Class_Parameters.Split(',');
            string rep = string.Empty;
            try
            {
                rep = ws.IWebService(DESEncrypt(entity.ID), config[0], config[1], config[2], config[3],
                    entity.EffectiveDate.ToString("yyyy-MM-dd"), entity.ExpiryDate.ToString("yyyy-MM-dd"),
                    entity.FlightNo, entity.EffectiveDate.ToString("yyyy-MM-dd HH:mm"),
                    "1", entity.Name, GetIdType(entity.IDType), entity.ID,
                    entity.Gender == Gender.Female ? "0" : "1",
                    entity.Birthday.ToString("yyyy-MM-dd"), entity.PhoneNumber, "1", "", "", false);

                if (string.IsNullOrEmpty(rep))
                {
                    string err = "接口未按约定返回:返回为空!";
                    Common.LogIt(err);
                    result.Trace.ErrorMsg = err;
                    return result;
                }
            }
            catch
            {
                Common.LogIt(ws.Url);
                throw;
            }

            string[] array = rep.Split('|');
            if (array.Length > 1)
            {
                if (array[0] == "1")
                    result.PolicyNo = array[1];
                else
                {
                    Common.LogIt(rep);
                    result.Trace.ErrorMsg = rep;
                }
            }
            else
            {
                Common.LogIt(rep);
                result.Trace.ErrorMsg = rep;
            }

            return result;
        }

        public TraceEntity Validate(IssueEntity entity)
        {
            return new TraceEntity();
        }

        public TraceEntity Withdraw(WithdrawEntity entity)
        {
            TraceEntity result = new TraceEntity();
            //agent,sesscode,productno,productname 用户名,密码,产品代码,产品名称
            string[] config = entity.IOC_Class_Parameters.Split(',');
            t_Case caseEntity = Case.Get(entity.CaseNo);
            string rep = ws.CannelPolicyById(
                DESEncrypt(caseEntity.customerID), config[0], config[1], config[2], entity.PolicyNo, caseEntity.customerID);

            string[] array = rep.Split('|');
            if (array.Length > 1)
            {
                if (array[0] != "1")
                {
                    Common.LogIt(rep);
                    result.ErrorMsg = rep;
                }
            }
            else
            {
                Common.LogIt(rep);
                result.ErrorMsg = rep;
            }

            return result;
        }

        public static string DESEncrypt(string idno)
        {
            StringBuilder desncr = new StringBuilder();
            string company = "Intercontinentallinkage", returnstr = string.Empty;
            for (int d = 0; d < idno.Length; ++d)
            {
                desncr.Append(idno.Substring(0, d));
                desncr.Append(company.Substring(0, d));
            }
            returnstr = desncr.ToString().Substring(0, 10);
            returnstr += desncr.ToString().Substring(desncr.ToString().Length - 10, 10);
            return returnstr;
        }

        static string GetIdType(IdentityType type)
        {
            switch (type)
            {
                case IdentityType.身份证:
                    return "身份证";
                case IdentityType.护照:
                    return "护照";
                case IdentityType.军官证:
                    return "军官证";
                default:
                    return "护照";//其他
            }
        }
    }
}
