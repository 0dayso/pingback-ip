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
        /// <summary>
        /// 投保接口,本接口对 XML 节点名称大小写敏感
        /// </summary>
        /// <param name="requestXML"></param>
        /// <returns></returns>
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
            PurchaseRequestEntity request;
            PurchaseResponseEntity resp;
            string ret;

            try
            {
                request = Common.XmlDeserialize<PurchaseRequestEntity>(requestXML);
            }
            catch(Exception e)
            {
                Common.LogIt(requestXML + Environment.NewLine + e.ToString());
                resp = new PurchaseResponseEntity();
                resp.Trace.ErrorMsg = "服务器异常,请稍后重试!";
                ret = Common.XmlSerialize<PurchaseResponseEntity>(resp);
                return ret;
            }

            resp = WebServiceClass.Purchase(request, true);
            ret = Common.XmlSerialize<PurchaseResponseEntity>(resp);
            return ret;
        }

        /// <summary>
        /// 退保接口,本接口对 XML 节点名称大小写敏感
        /// </summary>
        /// <param name="requestXML"></param>
        /// <returns></returns>
        public static string DiscardIt(string requestXML)
        {
            /*请求参数
<?xml version="1.0" encoding="utf-16"?>
<WithdrawRequest>
  <Username>feng</username>
  <Password>123456</password>
  <PolicyNo>PC00013234234</PolicyNo>
</WithdrawRequest>
             * 返回参数
<?xml version="1.0" encoding="utf-16"?>
<TraceEntity>
  <ErrorMsg />
  <Detail>成功</Detail>
</TraceEntity>
             */
            WithdrawRequest request;
            TraceEntity trace;
            string ret;

            try
            {
                request = Common.XmlDeserialize<WithdrawRequest>(requestXML);
            }
            catch (Exception e)
            {
                Common.LogIt(requestXML + Environment.NewLine + e.ToString());
                trace = new TraceEntity();
                trace.ErrorMsg = "服务器异常,请稍后重试!";
                ret = Common.XmlSerialize<TraceEntity>(trace);
                return ret;
            }

            trace = WebServiceClass.DiscardIt(request.Username, request.Password, request.PolicyNo);//此处只能使用正式保单号撤单
            if (string.IsNullOrEmpty(trace.ErrorMsg))
                trace.ErrorMsg = string.Empty;//置为空字符串,否则序列化时Null类型将被忽略

            ret = Common.XmlSerialize<TraceEntity>(trace);
            return ret;
        }
    }
}
