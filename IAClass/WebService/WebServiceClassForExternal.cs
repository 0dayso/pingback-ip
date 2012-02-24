using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAClass.WebService;
using IAClass.Entity;

namespace IAClass.WebService
{
    public class WebServiceClassForExternal
    {
        public static string Issue(string requestXML)
        {
            /* 请求参数
<?xml version="1.0" encoding="utf-16"?>
<PurchaseRequestEntity>
  <InsuranceCode>pd001</InsuranceCode>
  <username>user001</username>
  <password>pass001</password>
  <flightDate>2012-03-15T00:00:00</flightDate>
  <flightNo>HU3213</flightNo>
  <customerGender>Male</customerGender>
  <customerBirth>1982-01-11T00:00:00</customerBirth>
  <customerIDType>身份证</customerIDType>
  <customerID>352224198201110013</customerID>
  <customerName>张山</customerName>
  <customerPhone>13888888888</customerPhone>
</PurchaseRequestEntity>
             * 返回参数
<?xml version="1.0" encoding="utf-16"?>
<PurchaseResponseEntity>
  <PolicyNo>PC00013234234</PolicyNo>
  <SerialNo />
  <CaseNo />
  <AgentName>测试代理商</AgentName>
  <ValidationPhoneNumber />
  <Trace>
    <ErrorMsg />
    <Detail />
  </Trace>
</PurchaseResponseEntity>
             */
            PurchaseRequestEntity request = Common.XmlDeserialize<PurchaseRequestEntity>(requestXML);
            PurchaseResponseEntity resp = WebServiceClass.Purchase(request, true);
            string ret = Common.XmlSerialize<PurchaseResponseEntity>(resp);
            return ret;
        }

        public static string DiscardIt(string requestXML)
        {
            /*
<?xml version="1.0" encoding="utf-16"?>
<TraceEntity>
  <ErrorMsg />
  <Detail>成功</Detail>
</TraceEntity>
             */
            WithdrawRequest request = Common.XmlDeserialize<WithdrawRequest>(requestXML);
            TraceEntity trace = WebServiceClass.DiscardIt(request.Username, request.Password, request.PolicyNo);//此处只能使用正式保单号撤单
            if (string.IsNullOrEmpty(trace.ErrorMsg))
                trace.ErrorMsg = string.Empty;//置为空字符串,否则序列化时Null类型将被忽略

            string ret = Common.XmlSerialize<TraceEntity>(trace);
            return ret;
        }
    }
}
