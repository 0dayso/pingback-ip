using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAClass.Issuing;
using IAClass.Entity;

namespace Renwox
{
    class Issuing : IIssuing
    {
        //config: username, password, insuranceCode
        static WebServiceForExternal ws = new WebServiceForExternal();

        public IssuingResultEntity Issue(IssueEntity entity)
        {
            IssuingResultEntity result = new IssuingResultEntity();
            PurchaseRequestEntity req = new PurchaseRequestEntity();
            req.customerBirth = entity.Birthday;
            req.customerGender = (Gender)entity.Gender;
            req.customerID = entity.ID;
            req.customerIDType = (IdentityType)entity.IDType;
            req.customerName = entity.Name;
            req.customerPhone = entity.PhoneNumber;
            req.flightDate = entity.EffectiveDate;
            req.flightNo = entity.FlightNo;

            string[] config = entity.IOC_Class_Parameters.Split(',');
            req.InsuranceCode = config[2];
            req.password = config[1];
            req.username = config[0];

            string xml = Common.XmlSerialize<PurchaseRequestEntity>(req);
            string ret = ws.Issue(xml);
            PurchaseResponseEntity resp = Common.XmlDeserialize<PurchaseResponseEntity>(ret);

            if (string.IsNullOrEmpty(resp.Trace.ErrorMsg))
            {
                result.PolicyNo = result.Trace.Detail = resp.PolicyNo;
            }
            else
            {
                result.Trace.Detail = resp.Trace.Detail;
                result.Trace.ErrorMsg = resp.Trace.ErrorMsg;
            }

            return result;
        }

        public TraceEntity Withdraw(WithdrawEntity entity)
        {
            TraceEntity result = new TraceEntity();

            WithdrawRequest req = new WithdrawRequest();
            string[] config = entity.IOC_Class_Parameters.Split(',');
            req.Username = config[0];
            req.Password = config[1];
            req.PolicyNo = entity.PolicyNo;
            req.CaseNo = entity.CaseNo;
            string xml = Common.XmlSerialize<WithdrawRequest>(req);
            string ret = ws.Withdraw(xml);
            result = Common.XmlDeserialize<TraceEntity>(ret);

            return result;
        }

        public TraceEntity Validate(IssueEntity entity)
        {
            TraceEntity result = new TraceEntity();
            return result;
        }
    }
}
