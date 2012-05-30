using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAClass;
using IAClass.Entity;
using System.Configuration;
using System.Xml;
using System.Text.RegularExpressions;

namespace Jiandanbao
{
    public class Issuing_All : IAClass.Issuing.IIssuing
    {
        static Service ws = new Service();
        static MyWebService wsCheck = new MyWebService();

        static string GetIdType(IdentityType type)
        {
            switch (type)
            {
                case IdentityType.身份证:
                    return "1";
                case IdentityType.护照:
                    return "2";
                case IdentityType.军官证:
                    return "0";
                case IdentityType.港澳通行证:
                    return "3";
                default:
                    return "6";//其他
            }
        }

        public TraceEntity Validate(IssueEntity entity)
        {
            TraceEntity ret = new TraceEntity();

            //if (entity.EffectiveDate.Date == DateTime.Today)
            //{
            //    ret.ErrorMsg = "该产品不支持当日投保.";
            //}

            return ret;
        }

        /// <summary>
        /// 投保
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IssuingResultEntity Issue(IssueEntity entity)
        {
            IssuingResultEntity result = new IssuingResultEntity();

            if (entity.EffectiveDate.Date == DateTime.Today)
            {//该产品不支持当日投保.留待手工处理
                result.PolicyNo = entity.CaseNo;
                return result;
            }

            if (!Regex.IsMatch(entity.PhoneNumber, "^1[3458][0-9]{9}$"))
            {
                result.Trace.ErrorMsg = "请正确填写手机号码！";
                return result;
            }

            bool retCheck;//默认为false
            try
            {
                retCheck = wsCheck.DataCheck(entity.ID, entity.PhoneNumber, "");
                //if (retCheck == null)
                //    throw new Exception("WebService返回为空！");
            }
            catch (Exception e)
            {
                Common.LogIt(wsCheck.Url + System.Environment.NewLine + e.ToString());
                result.Trace.ErrorMsg = e.Message;
                return result;
            }

            if (retCheck)
            {
                //提交订单
                string xmlString = GetIssuingXML(entity);
                string ret = "";

                try
                {
                    ret = ws.InsertOrder(xmlString);

                    if (string.IsNullOrEmpty(ret))
                        throw new Exception("简单保WebService返回为空！");
                }
                catch (Exception e)
                {
                    Common.LogIt(ws.Url + System.Environment.NewLine + e.ToString());
                    result.Trace.ErrorMsg = e.Message;
                    return result;
                }

                ret = ret.ToUpper();
                if (!ret.StartsWith("Z"))
                {
                    Common.LogIt("投保参数" + xmlString + System.Environment.NewLine + "简单保投保：" + ret);
                    result.Trace.ErrorMsg = ret;
                }
                else
                {
                    result.PolicyNo = ret;
                    result.Insurer = "昆仑健康保险股份有限公司";
                    result.AmountInsured = "";
                    result.Website = "http://www.kunlunhealth.com";
                    result.CustomerService = "400-811-8899";
                }

                return result;
            }
            else
            {
                string msg = "dataCheck未通过(id={0}, phone={1})";
                msg = string.Format(msg, entity.ID, entity.PhoneNumber);
                Common.LogIt(msg);
                //result.Trace.ErrorMsg = msg;
                result.PolicyNo = entity.CaseNo;//留待手工投保平安团单
                result.Insurer = "中国平安";
                result.AmountInsured = "";
                result.Website = "http://www.pingan.com/";
                result.CustomerService = "95511";
                return result;
                //entity.IOC_Class_Alias = "chinalife_bj";
                //string msg = "jiandanbao_all核保未通过：id={0}, phone={1}  转入其他接口：{2}";
                //msg = string.Format(msg, entity.ID, entity.PhoneNumber, entity.IOC_Class_Alias);
                //Common.LogIt(msg);

                //result = new IAClass.Issuing.IssuingFacade().Issue(entity);
                //return result;
            }
        }

        public TraceEntity Withdraw(WithdrawEntity entity)
        {
            TraceEntity trace = new TraceEntity();
            trace.ErrorMsg = "该产品仅支持退单。";
            return trace;
        }

        private static string GetIssuingXML(IssueEntity entity)
        {
            XmlDocument xmlDoc = XMLString.Issuing_All.Clone() as XmlDocument;

            //处理基本信息节点
            xmlDoc.SelectSingleNode("OrderInfo/OrderNum").InnerText = entity.CaseNo;

            //处理投保人信息节点
            XmlNode xn = xmlDoc.SelectSingleNode("OrderInfo/Person/Policy");
            XmlNode xnNew = xn.Clone();
            xnNew.SelectSingleNode("UserName").InnerText = entity.Name;
            xnNew.SelectSingleNode("Certificate").InnerText = GetIdType(entity.IDType);
            xnNew.SelectSingleNode("CertificateNum").InnerText = entity.ID;
            xnNew.SelectSingleNode("Sex").InnerText = entity.Gender == Gender.Female ? "F" : "M";
            xnNew.SelectSingleNode("Birthday").InnerText = entity.Birthday.ToString("yyyy-M-d");
            xnNew.SelectSingleNode("Mobile").InnerText = entity.PhoneNumber;
            xn.ParentNode.AppendChild(xnNew);
            xn.ParentNode.RemoveChild(xn);

            string xml = xmlDoc.OuterXml;
            xml = xml.Replace("{起飞日期}", entity.EffectiveDate.ToString("yyyy-M-d"));
            xml = xml.Replace("{航班号}", entity.FlightNo);
            return xml;
        }

        private static string GetWithdrawXML(string certNo)
        {
            XmlDocument xmlDoc = XMLString.Withdraw.Clone() as XmlDocument;
            XmlNode xn = xmlDoc.SelectSingleNode("OrderInfo/OrderNum");
            xn.InnerText = certNo;
            return xmlDoc.OuterXml;
        }
    }

    public class Issuing_Free : IAClass.Issuing.IIssuing
    {
        static Service ws = new Service();
        static MyWebService wsCheck = new MyWebService();

        static string GetIdType(IdentityType type)
        {
            switch (type)
            {
                case IdentityType.身份证:
                    return "1";
                case IdentityType.护照:
                    return "2";
                case IdentityType.军官证:
                    return "0";
                case IdentityType.港澳通行证:
                    return "3";
                default:
                    return "6";//其他
            }
        }

        public TraceEntity Validate(IssueEntity entity)
        {
            TraceEntity result = new TraceEntity();

            double age = (DateTime.Today - entity.Birthday).TotalDays / 365;

            if (age >= 20 && age <= 45)
            {
                if (!string.IsNullOrEmpty(entity.PhoneNumber))
                {
                    if (!Regex.IsMatch(entity.PhoneNumber, "^1[3458][0-9]{9}$"))
                    {
                        result.ErrorMsg = "请正确填写手机号码！";
                        return result;
                    }
                }
                else
                {
                    result.ErrorMsg = "请填写手机号码！";
                    return result;
                }
            }

            return result;
        }

        /// <summary>
        /// 投保
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IssuingResultEntity Issue(IssueEntity entity)
        {
            IssuingResultEntity result = new IssuingResultEntity();

            bool retCheck;//默认为false
            try
            {
                retCheck = wsCheck.DataCheck(entity.ID, entity.PhoneNumber, "");
                //if (retCheck == null)
                //    throw new Exception("WebService返回为空！");

                if (retCheck)
                {
                    //提交订单
                    string xmlString = GetIssuingXML(entity);
                    Common.LogIt("简单保debug:" + xmlString);
                    string ret = "";

                    //try
                    {
                        ret = ws.InsertOrder(xmlString);

                        if (string.IsNullOrEmpty(ret))
                            throw new Exception("简单保WebService返回为空！");
                    }
                    //catch (Exception e)
                    {
                        //Common.LogIt(ws.Url + System.Environment.NewLine + e.ToString());
                        //result.Trace.Detail = "简单保InsertOrder访问失败！";
                        //return result;
                    }

                    ret = ret.ToUpper();
                    if (!ret.StartsWith("Z"))
                    {
                        Common.LogIt("投保参数" + xmlString + System.Environment.NewLine + "简单保投保：" + ret);
                        //result.Trace.Detail = ret;
                        throw new Exception(ret);
                    }
                    else
                        result.PolicyNo = ret;

                    return result;
                }
                else
                {
                    string msg = "dataCheck未通过(id={0}, phone={1})";
                    msg = string.Format(msg, entity.ID, entity.PhoneNumber);
                    Common.LogIt(msg);
                    //result.Trace.ErrorMsg = msg;
                    result.PolicyNo = entity.CaseNo;
                    return result;
                }
            }
            catch (Exception e)
            {
                Common.LogIt(e.ToString());
                //Common.LogIt("step1---------" + System.Environment.NewLine + e.ToString());
                ////result.Trace.Detail = "简单保DataCheck访问失败！";
                ////return result;
                //entity.IOC_Class_Alias = "JinHang";
                ////string msg = "简单保核保未通过：id={0}, phone={1}  转入其他接口：{2}";
                ////msg = string.Format(msg, entity.ID, entity.PhoneNumber, entity.IOC_Class_Alias);
                ////Common.LogIt(msg);

                //try
                //{
                //    result = new IAClass.Issuing.IssuingFacade().Issue(entity);
                //    if (!string.IsNullOrEmpty(result.Trace.ErrorMsg))
                //    {
                //        throw new Exception(result.Trace.ErrorMsg);
                //    }
                //}
                //catch(Exception ee)
                //{
                //    Common.LogIt("step2---------" + System.Environment.NewLine + ee.ToString());
                //    entity.IOC_Class_Alias = "chinalife_bj";
                //    result = new IAClass.Issuing.IssuingFacade().Issue(entity);
                //}
            }

            result.PolicyNo = "Z";
            return result;
        }

        public TraceEntity Withdraw(WithdrawEntity entity)
        {
            TraceEntity trace = new TraceEntity();
            trace.ErrorMsg = "该产品仅支持退单。";
            return trace;
        }

        private static string GetIssuingXML(IssueEntity entity)
        {
            XmlDocument xmlDoc = XMLString.Issuing_Free.Clone() as XmlDocument;

            //处理基本信息节点
            xmlDoc.SelectSingleNode("OrderInfo/CustomerOrderNum").InnerText = entity.CaseNo;
            xmlDoc.SelectSingleNode("OrderInfo/CreationDate").InnerText = DateTime.Today.ToString("yyyy-M-d");

            //处理投保人信息节点
            XmlNode xn = xmlDoc.SelectSingleNode("OrderInfo/Person/Policy");
            XmlNode xnNew = xn.Clone();
            xnNew.SelectSingleNode("UserName").InnerText = entity.Name;
            xnNew.SelectSingleNode("Certificate").InnerText = GetIdType(entity.IDType);
            xnNew.SelectSingleNode("CertificateNum").InnerText = entity.ID;
            xnNew.SelectSingleNode("Sex").InnerText = entity.Gender == Gender.Female ? "F" : "M";
            xnNew.SelectSingleNode("Birthday").InnerText = entity.Birthday.ToString("yyyy-M-d");
            xnNew.SelectSingleNode("Mobile").InnerText = entity.PhoneNumber;
            xn.ParentNode.AppendChild(xnNew);
            xn.ParentNode.RemoveChild(xn);

            //处理被保人信息节点(即本人，直接复制节点)
            xn = xmlDoc.SelectSingleNode("OrderInfo/Person/BePolicy");
            xn.RemoveAll();
            foreach (XmlNode x in xnNew.ChildNodes)
            {
                xn.AppendChild(x.Clone());
            }

            string xml = xmlDoc.OuterXml;
            xml = xml.Replace("{起飞日期}", entity.EffectiveDate.ToString("yyyy-M-d"));
            xml = xml.Replace("{航班号}", entity.FlightNo);
            return xml;
        }

        private static string GetWithdrawXML(string certNo)
        {
            XmlDocument xmlDoc = XMLString.Withdraw.Clone() as XmlDocument;
            XmlNode xn = xmlDoc.SelectSingleNode("OrderInfo/OrderNum");
            xn.InnerText = certNo;
            return xmlDoc.OuterXml;
        }
    }

    public class Issuing_7 : IAClass.Issuing.IIssuing
    {
        static iOrderWebService ws = new iOrderWebService();

        static string GetIdType(IdentityType type)
        {
            switch (type)
            {
                case IdentityType.身份证:
                    return "1";
                case IdentityType.护照:
                    return "2";
                case IdentityType.军官证:
                    return "0";
                case IdentityType.港澳通行证:
                    return "3";
                default:
                    return "6";//其他
            }
        }

        public TraceEntity Validate(IssueEntity entity)
        {
            TraceEntity ret = new TraceEntity();

            //if (entity.EffectiveDate.Date == DateTime.Today)
            //{
            //    ret.ErrorMsg = "该产品不支持当日投保.";
            //}

            return ret;
        }

        /// <summary>
        /// 投保
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IssuingResultEntity Issue(IssueEntity entity)
        {
            IssuingResultEntity result = new IssuingResultEntity();

            //if (entity.EffectiveDate.Date == DateTime.Today)
            //{//该产品不支持当日投保.留待手工处理
            //    result.PolicyNo = entity.CaseNo;
            //    return result;
            //}

            //if (!Regex.IsMatch(entity.PhoneNumber, "^1[3458][0-9]{9}$"))
            //{
            //    result.Trace.ErrorMsg = "请正确填写手机号码！";
            //    return result;
            //}

            if (true)
            {
                //提交订单
                string xmlString = GetIssuingXML(entity);
                string ret = "";

                try
                {
                    ret = ws.SendOrder(xmlString);

                    if (string.IsNullOrEmpty(ret))
                        throw new Exception("简单保WebService返回为空！");
                }
                catch (Exception e)
                {
                    Common.LogIt(ws.Url + System.Environment.NewLine + e.ToString());
                    result.Trace.ErrorMsg = e.Message;
                    return result;
                }

                ret = ret.ToUpper();
                if (!ret.StartsWith("Z"))
                {
                    Common.LogIt("投保参数" + xmlString + System.Environment.NewLine + "简单保返回：" + ret);
                    result.Trace.ErrorMsg = ret;
                }
                else
                {
                    result.PolicyNo = ret;
                    result.Insurer = "昆仑健康保险股份有限公司";
                    result.AmountInsured = "";
                    result.Website = "http://www.kunlunhealth.com";
                    result.CustomerService = "400-811-8899";
                }

                return result;
            }
        }

        public TraceEntity Withdraw(WithdrawEntity entity)
        {
            TraceEntity trace = new TraceEntity();

            XmlDocument xmlDoc = XMLString.Withdraw.Clone() as XmlDocument;
            xmlDoc.SelectSingleNode("OrderInfo/OrderNum").InnerText = entity.PolicyNo;
            string req = xmlDoc.OuterXml;
            string ret = "";
            //<?xml version="1.0" encoding="utf-8"?><OrderInfo><Flag>0</Flag><Message>订单不存在，或不是您的订单</Message></OrderInfo>
            try
            {
                ret = ws.CancelOrder(req);
                xmlDoc.LoadXml(ret);
                if(xmlDoc.SelectSingleNode("OrderInfo/Flag").InnerText =="0")
                    Common.LogIt("退保参数:" + req + Environment.NewLine
                                    + "简单保返回:" + ret);
            }
            catch (Exception e)
            {
                Common.LogIt(ws.Url + Environment.NewLine
                            + "退保异常:" + e.Message);
            }

            return trace;
        }

        private static string GetIssuingXML(IssueEntity entity)
        {
            XmlDocument xmlDoc = XMLString.Issuing_7.Clone() as XmlDocument;

            //处理基本信息节点
            xmlDoc.SelectSingleNode("OrderInfo/CustomerOrderNum").InnerText = entity.CaseNo;

            //处理投保人信息节点
            XmlNode xn = xmlDoc.SelectSingleNode("OrderInfo/Person/Policy");
            XmlNode xnNew = xn.Clone();
            xnNew.SelectSingleNode("UserName").InnerText = entity.Name;
            xnNew.SelectSingleNode("Certificate").InnerText = GetIdType(entity.IDType);
            xnNew.SelectSingleNode("CertificateNum").InnerText = entity.ID;
            xnNew.SelectSingleNode("Sex").InnerText = entity.Gender == Gender.Female ? "F" : "M";
            xnNew.SelectSingleNode("Birthday").InnerText = entity.Birthday.ToString("yyyy-M-d");
            xnNew.SelectSingleNode("Mobile").InnerText = entity.PhoneNumber;
            xn.ParentNode.AppendChild(xnNew);
            xn.ParentNode.RemoveChild(xn);

            //处理被保人信息节点
            xn = xmlDoc.SelectSingleNode("OrderInfo/Person/BePolicy");
            xnNew = xn.Clone();
            xnNew.SelectSingleNode("UserName").InnerText = entity.Name;
            xnNew.SelectSingleNode("Certificate").InnerText = GetIdType(entity.IDType);
            xnNew.SelectSingleNode("CertificateNum").InnerText = entity.ID;
            xnNew.SelectSingleNode("Sex").InnerText = entity.Gender == Gender.Female ? "F" : "M";
            xnNew.SelectSingleNode("Birthday").InnerText = entity.Birthday.ToString("yyyy-M-d");
            xnNew.SelectSingleNode("Mobile").InnerText = entity.PhoneNumber;
            xn.ParentNode.AppendChild(xnNew);
            xn.ParentNode.RemoveChild(xn);

            string xml = xmlDoc.OuterXml;
            xml = xml.Replace("{起飞日期}", entity.EffectiveDate.ToString("yyyy-M-d"));
            xml = xml.Replace("{航班号}", entity.FlightNo);
            return xml;
        }

        private static string GetWithdrawXML(string certNo)
        {
            XmlDocument xmlDoc = XMLString.Withdraw.Clone() as XmlDocument;
            XmlNode xn = xmlDoc.SelectSingleNode("OrderInfo/OrderNum");
            xn.InnerText = certNo;
            return xmlDoc.OuterXml;
        }
    }

    public static class XMLString
    {
        private static XmlDocument issuing_Free;
        private static XmlDocument issuing_all;
        private static XmlDocument issuing_7;
        private static XmlDocument withdraw;
        static object mutex1 = new object();
        static object mutex2 = new object();
        static object mutex3 = new object();

        public static XmlDocument Issuing_All
        {
            get
            {
                lock (mutex1)
                {
                    if (issuing_all == null)
                    {
                        issuing_all = new XmlDocument();
                        issuing_all.Load(System.IO.Path.Combine(Common.BaseDirectory, "App_Data/Jiandanbao/iOrder_All.xml"));
                    }
                }

                return issuing_all;
            }
        }

        public static XmlDocument Issuing_Free
        {
            get
            {
                lock (mutex2)
                {
                    if (issuing_Free == null)
                    {
                        issuing_Free = new XmlDocument();
                        issuing_Free.Load(System.IO.Path.Combine(Common.BaseDirectory, "App_Data/Jiandanbao/iOrder_Free.xml"));
                    }
                }

                return issuing_Free;
            }
        }

        public static XmlDocument Issuing_7
        {
            get
            {
                lock (mutex3)
                {
                    if (issuing_7 == null)
                    {
                        issuing_7 = new XmlDocument();
                        issuing_7.Load(System.IO.Path.Combine(Common.BaseDirectory, "App_Data/Jiandanbao/iOrder_7.xml"));
                    }
                }

                return issuing_7;
            }
        }

        public static XmlDocument Withdraw
        {
            get
            {
                lock (mutex2)
                {
                    if (withdraw == null)
                    {
                        withdraw = new XmlDocument();
                        withdraw.Load(System.IO.Path.Combine(Common.BaseDirectory, "App_Data/Jiandanbao/iCancel.xml"));
                    }
                }

                return withdraw;
            }
        }
    }
}
