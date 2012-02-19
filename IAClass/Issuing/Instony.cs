using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAClass.Entity;
using System.Xml;

namespace Instony
{
    class Issuing : IAClass.Issuing.IIssuing
    {
        static InstonyService ws = new InstonyService();
        static string key = System.Configuration.ConfigurationManager.AppSettings["InstonyIssuing"];

        public TraceEntity Validate(IssueEntity entity)
        {
            return new TraceEntity();
        }

        public IssuingResultEntity Issue(IssueEntity entity)
        {
            IssuingResultEntity result = new IssuingResultEntity();

            XmlDocument xmlDoc = IssuingXML.Issuing.Clone() as XmlDocument;
            xmlDoc.SelectSingleNode("Instony/PolicyInfo/StartTime").InnerText = entity.EffectiveDate.ToString("yyyyMMddHHmmss");
            xmlDoc.SelectSingleNode("Instony/PolicyInfo/EndTime").InnerText = entity.EffectiveDate.ToString("yyyyMMddHHmmss");
            xmlDoc.SelectSingleNode("Instony/PolicyInfo/CreateTime").InnerText = DateTime.Now.ToString("yyyyMMddHHmmss");
            xmlDoc.SelectSingleNode("Instony/PolicyInfo/NoCode").InnerText = entity.FlightNo;
            int i = 0;
            //投保人
            XmlNode xn = xmlDoc.SelectSingleNode("Instony/Holder");
        SetValue:
            i++;
            xn.SelectSingleNode("Name").InnerText = entity.Name;
            xn.SelectSingleNode("Sex").InnerText = entity.Gender == Gender.Female ? "F" : "M";
            xn.SelectSingleNode("Birthday").InnerText = entity.Birthday.ToString("yyyyMMdd");
            xn.SelectSingleNode("IDType").InnerText = GetIdType(entity.IDType);
            xn.SelectSingleNode("IDNum").InnerText = entity.ID;
            xn.SelectSingleNode("Mobile").InnerText = string.IsNullOrEmpty(entity.PhoneNumber) ? "18611586620" : entity.PhoneNumber;

            //被保人 内容同投保人节点，故用goto重复一次
            xn = xmlDoc.SelectSingleNode("Instony/Insureds/Insured");
            if (i < 2)
                goto SetValue;

            string response = "";
            try
            {
                response = ws.doSaleService(xmlDoc.OuterXml, key);
                if(string.IsNullOrEmpty(response))
                    throw new Exception("意时网投保WebService返回为空！");
            }
            catch
            {
                Common.LogIt(ws.Url);
                throw;
            }

            try
            {
                XmlDocument xmlDocRet = new XmlDocument();
                xmlDocRet.LoadXml(response);
                if (xmlDocRet.SelectSingleNode("Instony/RetData/Flag").InnerText == "1")
                {
                    result.PolicyNo = xmlDocRet.SelectSingleNode("Instony/PolicyInfo/PolicyNum").InnerText;
                    return result;
                }
                else
                {
                    Common.LogIt("投保参数" + xmlDoc.OuterXml + System.Environment.NewLine + "意时网投保返回：" + response);
                    result.Trace.ErrorMsg = xmlDocRet.SelectSingleNode("Instony/RetData/Message").InnerText;
                    return result;
                }
            }
            catch
            {
                Common.LogIt("投保参数" + xmlDoc.OuterXml + System.Environment.NewLine + "意时网投保返回：" + response);
                throw;
            }
        }

        public TraceEntity Withdraw(WithdrawEntity entity)
        {
            TraceEntity result = new TraceEntity();

            string response = "";
            try
            {
                response = ws.CannelPolicyByPolicyNos(entity.PolicyNo, key);
                if (string.IsNullOrEmpty(response))
                    throw new Exception("意时网退保WebService返回为空！");
            }
            catch
            {
                Common.LogIt(ws.Url);
                throw;
            }

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(response);

                if (xmlDoc.SelectSingleNode("Instony/RetData/Flag").InnerText == "1")
                    return result;
                else
                {
                    Common.LogIt("退保参数" + entity.PolicyNo + System.Environment.NewLine + "意时网投保返回：" + response);
                    result.ErrorMsg = xmlDoc.SelectSingleNode("Instony/RetData/Message").InnerText;
                    return result;
                }
            }
            catch
            {
                Common.LogIt("退保参数" + entity.PolicyNo + System.Environment.NewLine + "意时网投保返回：" + response);
                throw;
            }
        }

        static string GetIdType(IdentityType type)
        {
            switch (type)
            {
                case IdentityType.身份证:
                    return "I";
                case IdentityType.护照:
                    return "P";
                default:
                    return "O";//其他
            }
        }
    }

    class IssuingXML
    {
        static XmlDocument issuing;

        static public XmlDocument Issuing
        {
            get
            {
                if (issuing == null)
                {
                    issuing = new XmlDocument();
                    issuing.Load(System.IO.Path.Combine(Common.BaseDirectory, "App_Data/Instony/CreatePolicy.xml"));
                }

                return issuing;
            }
        }
    }
}
