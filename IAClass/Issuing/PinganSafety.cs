﻿using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Xml.XPath;
using System.Collections.Generic;
using IAClass.Entity;
using IAClass.Issuing;
using IAClass;

namespace Pingan
{
    public static class PinganString
    {
        private static object mutex = new object();
        private static XmlDocument m_print;
        private static XmlDocument m_policycert;
        private static XmlDocument m_revoke;
        public static XmlDocument print
        {
            get
            {
                if (m_print == null)
                {
                    m_print = new XmlDocument();
                    m_print.Load(System.IO.Path.Combine(Common.BaseDirectory, "App_Data/PingAn/print.xml"));
                }

                return m_print;

            }
        }
        public static XmlDocument PolicyCert
        {
            get
            {
                lock (mutex)
                {
                    if (m_policycert == null)//非线程安全!! 原因:在 new 完成但还未 Load 的时候,线程即可成功越过该 if 使用了该属性,从而导致非常隐秘的Bug!!
                    {
                        m_policycert = new XmlDocument();
                        m_policycert.Load(System.IO.Path.Combine(Common.BaseDirectory, "App_Data/PingAn/policyCert.xml"));
                    }
                }

                return m_policycert;
            }
        }
        public static XmlDocument Revoke
        {
            get
            {
                if (m_revoke == null)
                {
                    m_revoke = new XmlDocument();
                    m_revoke.Load(System.IO.Path.Combine(Common.BaseDirectory, "App_Data/PingAn/revokePolicy.xml"));
                }

                return m_revoke;
            }

        }
    }
    public struct PinganPrint
    {
        public string certNo;
        //public string productNo;
    }
    public struct PolicyCert
    {
        public string name;
        public string idType;
        public string idNo;
        public string birthDate;
        public string gender;
        public string units;
        public string effDate;
        public string matuDate;
        public string mobile;
    }
    public struct RevokePolicy
    {
        public string certNo;
        //public string productNo;
    }
    public struct PolicyResult
    {
        // 保单号
        public string certNo;
        // 价格
        public string totPrem;
        // 总单号
        public string policyNo;
        // 状态码
        public string certsts;
        // 投报人姓名
        public string name;
        // 投报类型
        public string productNo;
        // 证件号码
        public string idNo;
        
        public void GetData(XmlNode node)
        {
            foreach (XmlNode cnode in node.ChildNodes)
            {
                switch (cnode.Attributes["name"].Value)
                {
                    case "certNo": certNo = cnode.InnerText; break;
                    case "totPrem": totPrem = cnode.InnerText; break;
                    case "policyNo": policyNo = cnode.InnerText; break;
                    case "certsts": certsts = cnode.InnerText; break;
                    case "name": name = cnode.InnerText; break;
                    case "productNo": productNo = cnode.InnerText; break;
                    default: break;
                }
            }
        }
    }
    public struct PrintResult
    {
        // 性别
        public string sex;
        //
        public string period;
        // 订单号
        public string polNo;
        // 保单号
        public string certNo;
        public string totPrem;
        public string productName;
        // 姓名
        public string name;
        // 生日
        public string birthday;
        public string idNo;
        public void GetData(XmlNode node)
        {
            foreach (XmlNode cnode in node.ChildNodes)
            {
                switch (cnode.Attributes["name"].Value)
                {
                    case "sex": sex = cnode.InnerText; break;
                    case "period": period = cnode.InnerText; break;
                    case "polNo": polNo = cnode.InnerText; break;
                    case "certNo": certNo = cnode.InnerText; break;
                    case "totPrem": totPrem = cnode.InnerText; break;
                    case "productName": productName = cnode.InnerText; break;
                    case "name": name = cnode.InnerText; break;
                    case "birthday": birthday = cnode.InnerText; break;
                    case "idNo": idNo = cnode.InnerText; break;
                    default: break;
                }
            }
        }
    }
    public struct RevokeResult
    {
        public string certNo;
        public string totPrem;
        public string policyNo;
        public string certsts;
        public string name;
        public string productNo;
        public void GetData(XmlNode node)
        {
            foreach (XmlNode cnode in node.ChildNodes)
            {
                switch (cnode.Attributes["name"].Value)
                {
                    case "certNo": certNo = cnode.InnerText; break;
                    case "totPrem": totPrem = cnode.InnerText; break;
                    case "policyNo": policyNo = cnode.InnerText; break;
                    case "certsts": certsts = cnode.InnerText; break;
                    case "name": name = cnode.InnerText; break;
                    case "productNo": productNo = cnode.InnerText; break;
                    default: break;
                }
            }
        }
    }
    public class Issuing : IIssuing
    {
        private string filename = Path.Combine(Common.BaseDirectory, "App_Data/PingAn/EXV_BIS_IFRONT_EGIS_ABBS_JNJRD_001_PRD.pfx");//System.Configuration.ConfigurationManager.AppSettings["PACertFilename"];
        private string password = "Pa888888";//System.Configuration.ConfigurationManager.AppSettings["PACertPassword"];
        private string url = "https://eairiis-prddmz.paic.com.cn/invoke/wm.tn/receive";//System.Configuration.ConfigurationManager.AppSettings["PACertUrl"];
        //https://eairiis-stgdmz.paic.com.cn/invoke/wm.tn/receive
        private string GetPrintstring(PinganPrint[] printpar)
        {
            
            XmlDocument print = PinganString.print.Clone() as XmlDocument;
            XmlNode para = print.SelectSingleNode("abbsParamXml/Request/policyPrinting/paramList/parameter");
            foreach (PinganPrint pr in printpar)
            {
                XmlNode parameter = para.Clone();
                parameter.SelectSingleNode("certNo").InnerText = pr.certNo;
                //parameter.SelectSingleNode("productNo").InnerText = pr.productNo;
                para.ParentNode.AppendChild(parameter);
                
                
            }
            para.ParentNode.RemoveChild(para);
            return print.OuterXml;

        }
        /// <summary>
        /// 根据投保结构体数组获取投保字符串参数
        /// </summary>
        /// <param name="policycert"></param>
        /// <returns></returns>
        private string GetPolicycert(PolicyCert[] policycert)
        {
            XmlDocument policy = PinganString.PolicyCert.Clone() as XmlDocument;
            XmlNode para = policy.SelectSingleNode("abbsParamXml/Request/policyIssuing/paramList/parameter");
            foreach (PolicyCert pc in policycert)
            {
                XmlNode parameter = para.Clone();
                parameter.SelectSingleNode("name").InnerText = pc.name;
               // parameter.SelectSingleNode("productNo").InnerText = pc.productNo;
                parameter.SelectSingleNode("idType").InnerText = pc.idType;
                parameter.SelectSingleNode("idNo").InnerText = pc.idNo;
                parameter.SelectSingleNode("gender").InnerText = pc.gender;
                parameter.SelectSingleNode("birthDate").InnerText = pc.birthDate;
                parameter.SelectSingleNode("effDate").InnerText = pc.effDate;
                parameter.SelectSingleNode("matuDate").InnerText = pc.matuDate;
                parameter.SelectSingleNode("units").InnerText = pc.units;
                parameter.SelectSingleNode("mobile").InnerText = pc.mobile;
                para.ParentNode.AppendChild(parameter);
            }
            para.ParentNode.RemoveChild(para);
            return policy.OuterXml;
        }
        private string GetRevokePolicy(RevokePolicy[] revokepolicy)
        {
            XmlDocument revoke = PinganString.Revoke.Clone() as XmlDocument;
            XmlNode para = revoke.SelectSingleNode("abbsParamXml/Request/policyWithdraw/paramList/parameter");

            foreach (RevokePolicy pr in revokepolicy)
            {
                XmlNode parameter = para.Clone();
                parameter.SelectSingleNode("certNo").InnerText = pr.certNo;
                //parameter.SelectSingleNode("productNo").InnerText = pr.productNo;
                para.ParentNode.AppendChild(parameter);
            }
            para.ParentNode.RemoveChild(para);
            return revoke.OuterXml;

        }
        /// <summary>
        /// 获取结果
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        private string GetResult(string para)
        {
            string result = "";
            byte[] data = Encoding.GetEncoding("gbk").GetBytes(para);
            //2012.01.31 新增X509KeyStorageFlags参数，以解决忽然出现的异常“CryptographicException: 出现了内部错误”
            X509Certificate2 cer2 = new X509Certificate2(filename, password, X509KeyStorageFlags.MachineKeySet);
            System.Net.ServicePointManager.Expect100Continue = false;

            result = Common.HttpPost(url, data, cer2);
            return result;
        }

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        /// <summary>
        /// 投保 --白振峰
        /// </summary>
        /// <param name="policycert">投保参数 结构体数组可以多人同时投保</param>
        /// <returns>执行结果XML格式字符串</returns>
        public string Issue(PolicyCert[] policycert)
        {
            return GetResult(GetPolicycert(policycert));
        }

        public TraceEntity Validate(IssueEntity entity)
        {
            return new TraceEntity();
        }

        //若是中文名,去除*CHD等字样
        private string GetChineseName(string source)
        {
            string cname = StringHelper.MiscelHelper.InterceptChinese(source);
            if (string.IsNullOrEmpty(cname))
                return source;//英文名字
            else
                return cname;
        }

        public IssuingResultEntity Issue(IssueEntity entity)
        {
            IssuingResultEntity result = new IssuingResultEntity();

            if (entity.EffectiveDate.Date == entity.ExpiryDate.Date)
                entity.ExpiryDate = entity.ExpiryDate.AddDays(1);//"满期日期必须晚于生效日期"

            PolicyCert[] cert = new PolicyCert[1];
            cert[0].birthDate = entity.Birthday.ToString("yyyyMMdd");
            cert[0].effDate = entity.EffectiveDate.ToString("yyyyMMdd");
            cert[0].gender = entity.Gender == Gender.Female ? "F" : "M";
            cert[0].idNo = entity.ID;
            cert[0].idType = GetIdType(entity.IDType);
            cert[0].matuDate = entity.ExpiryDate.ToString("yyyyMMdd");
            cert[0].name = GetChineseName(entity.Name);
            cert[0].units = "1";
            cert[0].mobile = entity.PhoneNumber;
            string xmlRet = "";

            try
            {
                xmlRet = Issue(cert);

                if (string.IsNullOrEmpty(xmlRet))
                    throw new Exception("平安投保返回为空!");

                XmlDocument xd = new XmlDocument();
                xd.LoadXml(xmlRet);
                XmlNodeList nodes = xd.SelectNodes("Values/value");

                if (GetValue(nodes, "processFlag") == "1")
                {
                    nodes = xd.SelectNodes("Values/array/record/value");
                    string billNo = GetValue(nodes, "certNo");
                    result.PolicyNo = result.Trace.Detail = billNo;
                    return result;
                }
                else
                {
                    Common.LogIt("平安投保返回:" + xmlRet);
                    //平安接口315暂停
                    string error = GetValue(nodes, "processMessage");
                    if (string.IsNullOrEmpty(error))
                        result.Trace.ErrorMsg = "未知错误";
                    else
                        result.Trace.ErrorMsg = error;
                    return result;
                }
            }
            catch
            {
                if (!string.IsNullOrEmpty(xmlRet))
                    Common.LogIt("平安投保返回:" + xmlRet);
                throw;
            }
        }

        public TraceEntity Withdraw(WithdrawEntity entity)
        {
            TraceEntity result = new TraceEntity();
            RevokePolicy[] rp = new RevokePolicy[1];
            rp[0].certNo = entity.PolicyNo;
            string xmlRet = "";

            try
            {
                xmlRet = Withdraw(rp);
            }
            catch
            {
                Common.LogIt(url);
                throw;
            }

            try
            {
                if (string.IsNullOrEmpty(xmlRet))
                    throw new Exception("平安退保返回为空!");

                XmlDocument xd = new XmlDocument();
                xd.LoadXml(xmlRet);
                XmlNodeList nodes = xd.SelectNodes("Values/value");

                if (GetValue(nodes, "processFlag") == "1")
                {
                    return result;
                }
                else
                {
                    Common.LogIt("平安退保返回:" + xmlRet);
                    string error = GetValue(nodes, "processMessage");
                    if (string.IsNullOrEmpty(error))
                        result.ErrorMsg = "未知错误";
                    else
                        result.ErrorMsg = error;
                    return result;
                }
            }
            catch
            {
                if (!string.IsNullOrEmpty(xmlRet))
                    Common.LogIt("平安退保返回:" + xmlRet);
                throw;
            }
        }

        static string GetValue(XmlNodeList nodes, string attribute)
        {
            foreach (XmlNode xn in nodes)
            {
                foreach (XmlAttribute xa in xn.Attributes)
                {
                    if (xa.Value == attribute)
                    {
                        return xn.InnerText;
                    }
                }
            }

            throw new Exception("未找到包含该属性值的节点!");
        }

        static string GetIdType(IdentityType type)
        {
            switch (type)
            {
                case IdentityType.身份证:
                    return "1";
                case IdentityType.护照:
                    return "2";
                case IdentityType.军官证:
                    return "3";
                default:
                    return "9";//其他
            }
        }

        public List<PolicyResult> policyIssuing(PolicyCert[] policycert)
        {
            string Result = GetResult(GetPolicycert(policycert));
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(Result);
            XmlNode array = xmldoc.SelectSingleNode("Values/array");
            List<PolicyResult> result;
            if (array == null)
                result = null;
            else
            {
                result = new List<PolicyResult>();
                for (int i = 0; i < array.ChildNodes.Count; i++) 
                {
                    PolicyResult policyresult = new PolicyResult();
                    policyresult.GetData(array.ChildNodes[i]);
                    policyresult.idNo = policycert[i].idNo;
                    result.Add(policyresult);
                }


                //foreach (XmlNode node in array.ChildNodes)
                //{
                //    PolicyResult policyresult = new PolicyResult();
                //    policyresult.GetData(node);
                    
                //    result.Add(policyresult);
                //}
            }
            return result;
        }
        public string Printing(PinganPrint[] print)
        {
            return GetResult(GetPrintstring(print));
        }
        public List<PrintResult> policyPrinting(PinganPrint[] print)
        {
            string Result = GetResult(GetPrintstring(print));
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(Result);
            XmlNode array = xmldoc.SelectSingleNode("Values/array");
            List<PrintResult> result;
            if (array == null)
                result = null;
            else
            {
                result = new List<PrintResult>();
                foreach (XmlNode node in array.ChildNodes)
                {
                    PrintResult printresult = new PrintResult();
                    printresult.GetData(node);
                    result.Add(printresult);
                }
            }
            return result;
        }
        public string Withdraw(RevokePolicy[] revoke)
        {
            return GetResult(GetRevokePolicy(revoke));
        }
        public List<RevokeResult> policyWithdraw(RevokePolicy[] revoke)
        {
            string Result = GetResult(GetRevokePolicy(revoke));
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(Result);
            XmlNode array = xmldoc.SelectSingleNode("Values/array");
            List<RevokeResult> result;
            if (array == null)
                result = null;
            else
            {
                result = new List<RevokeResult>();
                foreach (XmlNode node in array.ChildNodes)
                {
                    RevokeResult revokeresult = new RevokeResult();
                    revokeresult.GetData(node);
                    result.Add(revokeresult);
                }
            }
            return result;
        }

    }
}