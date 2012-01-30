using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml;
using IAClass;
using IAClass.Entity;

namespace YongCheng
{
    public struct PolicyCert
    {
        public string T_APP_TM;
        public string T_SIGN_TM;
        public string T_OPER_DATE;
        public string name;
        public string idType;
        public string idNo;
        public string birthDate;
        public string gender;
        public string effDate;
        public string expDate;
    }

    /// <summary>
    /// 永诚数据接口类
    /// </summary>
    public class Issuing : IAClass.Issuing.IIssuing
    {
        /// <summary>
        /// 投保
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IssuingResultEntity Issue(IssueEntity entity)
        {
            IssuingResultEntity result = new IssuingResultEntity();

            if (string.IsNullOrEmpty(entity.CaseNo))
            {
                result.Trace.ErrorMsg = "请正确输入当前单证的印刷号，否则无法出单！";
                return result;
            }

            string requestString = GetIssuingXML(entity);
            string responseString = GetResponse(requestString);
            result = ScanIssuingResult(responseString);

            if (string.IsNullOrEmpty(result.Trace.ErrorMsg))
            {
                TraceEntity printResult = Print(result, entity);

                if (string.IsNullOrEmpty(printResult.ErrorMsg))
                {
                    result.Trace.Detail = "";
                    return result;
                }
                else
                {
                    result.Trace.Detail = "已投保，但该单证未能成功核销！";
                    return result;
                }
            }
            else
            {
                Common.LogIt("永城接口 投保：" + responseString);
                return result;
            }
        }

        /// <summary>
        /// 退保
        /// 注意可能返回该错误：“该保单尚未同步到核心系统,稍后退保”
        /// 永诚方面同步到核心系统的周期大约是5分钟
        /// </summary>
        /// <param name="certNo">保单号</param>
        /// <returns></returns>
        public TraceEntity Withdraw(WithdrawEntity entity)
        {
            TraceEntity result = new TraceEntity();
            string requestString = GetWithdrawXML(entity.PolicyNo);
            string responseString = GetResponse(requestString);
            result = ScanWithdrawResult(responseString);

            if (!string.IsNullOrEmpty(result.ErrorMsg))
                Common.LogIt("永城接口 退保：" + responseString);

            return result;
        }

        /// <summary>
        /// 打印核销
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static TraceEntity Print(IssuingResultEntity issuingResult, IssueEntity entity)
        {
            TraceEntity result = new TraceEntity();
            string requestString = GetPrintXML(issuingResult.PolicyNo, entity.CaseNo);
            string responseString = GetResponse(requestString);
            result = ScanPrintResult(responseString);
            string strSql;

            if (string.IsNullOrEmpty(result.ErrorMsg))
            {
                strSql = "update t_case set printingNo = '{0}', [isPrinted] = 1 where caseNo = '{1}'";
                strSql = string.Format(strSql, entity.CaseNo, entity.CaseNo);
                entity.DbCommand.CommandText = strSql;
                entity.DbCommand.ExecuteNonQuery();
            }
            else
            {
                //strSql = "update t_case set printingNo = '{0}', [isPrinted] = 0 where caseNo = '{1}'";
                Common.LogIt("永城接口 核销：" + responseString);
            }

            return result;
        }

        /// <summary>
        /// 连接永诚socket，10秒内必须完成，否则超时处理
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private static string GetResponse(string xml)
        {
            string[] config = System.Configuration.ConfigurationManager.AppSettings["YongChengIssuing"].Split(',');
            IPHostEntry ipHost = Dns.GetHostEntry(config[0].Trim());//域名转ip
            IPEndPoint iep = new IPEndPoint(ipHost.AddressList[0], int.Parse(config[1].Trim()));

            try
            {
                using (TcpClient tcpClient = TimeOutSocket.Connect(iep, Timeout.Infinite))
                {
                    using (NetworkStream netStream = tcpClient.GetStream())
                    {
                        StringBuilder myCompleteMessage = new StringBuilder();

                        if (netStream.CanWrite)
                        {
                            Byte[] sendBytes = Encoding.Default.GetBytes(xml);
                            netStream.Write(sendBytes, 0, sendBytes.Length);
                        }
                        else
                        {
                            throw new Exception("You cannot write data to this stream.");
                        }

                        if (netStream.CanRead)
                        {
                            byte[] myReadBuffer = new byte[1024];
                            int numberOfBytesRead = 0;

                            // Incoming message may be larger than the buffer size.
                            do
                            {
                                //try
                                {
                                    //若已过了前面connect阶段所设定的超时时间则该 Read 方法会报IOException
                                    numberOfBytesRead = netStream.Read(myReadBuffer, 0, myReadBuffer.Length);
                                }
                                //catch (System.IO.IOException ee)
                                //{
                                //    Common.LogIt(ee.ToString());
                                //    throw new Exception("与永诚核心数据传输超时，请稍后重新尝试。");
                                //}

                                myCompleteMessage.AppendFormat("{0}", Encoding.Default.GetString(myReadBuffer, 0, numberOfBytesRead));
                            }
                            while (netStream.DataAvailable);
                        }
                        else
                        {
                            throw new Exception("Sorry.  You cannot read from this NetworkStream.");
                        }

                        return myCompleteMessage.ToString();
                    }
                }
            }
            catch
            {
                Common.LogIt(iep.ToString());
                throw;
            }
        }

        /// <summary>
        /// 解析投保接口返回值
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private static IssuingResultEntity ScanIssuingResult(string xml)
        {
            xml = xml.Trim();
            IssuingResultEntity entity = new IssuingResultEntity();

            if (string.IsNullOrEmpty(xml))
            {
                entity.Trace.ErrorMsg = "投保接口返回内容为空!";
                return entity;
            }

            XmlDocument xd = new XmlDocument();

            try
            {
                xd.LoadXml(xml);
                XmlNode xn = xd.SelectSingleNode("PACKET/HEAD/RESPONSECOMPLETEMESSAGESTATUS/MESSAGESTATUSCODE");
                string code = xn.InnerText;

                if (code == "1")//投保成功，返回正式保单号！
                {
                    xn = xd.SelectSingleNode("PACKET/INFORMATION/RESULT/C_PLY_NO");
                    entity.PolicyNo = xn.InnerText;
                    entity.Trace.Detail = xd.OuterXml;
                }
                else
                {
                    xn = xd.SelectSingleNode("PACKET/HEAD/RESPONSECOMPLETEMESSAGESTATUS/MESSAGESTATUSDESCRIPTION");
                    entity.Trace.ErrorMsg = xn.InnerText;
                    entity.Trace.Detail = xd.OuterXml;
                }
            }
            catch
            {
                entity.Trace.ErrorMsg = "解析投保接口返回xml字符串发生错误!";
                entity.Trace.Detail = xml;
            }

            return entity;
        }

        /// <summary>
        /// 扫描退保接口返回值
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private static TraceEntity ScanWithdrawResult(string xml)
        {
            xml = xml.Trim();
            TraceEntity entity = new TraceEntity();
            if (string.IsNullOrEmpty(xml))
            {
                entity.ErrorMsg = "退保接口返回内容为空!";
                return entity;
            }
            
            XmlDocument xd = new XmlDocument();
            try
            {
                xd.LoadXml(xml);
                XmlNode xn = xd.SelectSingleNode("PACKET/HEAD/RESPONSECOMPLETEMESSAGESTATUS/MESSAGESTATUSCODE");
                string code = xn.InnerText;

                if (code == "1")//成功！
                {
                    entity.Detail = xd.OuterXml;
                }
                else
                {
                    xn = xd.SelectSingleNode("PACKET/HEAD/RESPONSECOMPLETEMESSAGESTATUS/MESSAGESTATUSDESCRIPTION");
                    if (xn.InnerText.Contains("已退保"))
                        entity.Detail = xd.OuterXml;
                    else
                    {
                        entity.ErrorMsg = xn.InnerText;
                        entity.Detail = xd.OuterXml;
                    }
                }
            }
            catch
            {
                entity.ErrorMsg = "解析退保接口返回xml字符串发生错误!";
                entity.Detail = xml;
            }

            return entity;
        }

        /// <summary>
        /// 扫描打印核销接口的返回值
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private static TraceEntity ScanPrintResult(string xml)
        {
            xml = xml.Trim();
            TraceEntity entity = new TraceEntity();
            if (string.IsNullOrEmpty(xml))
            {
                entity.ErrorMsg = "打印核销接口返回内容为空!";
                return entity;
            }

            XmlDocument xd = new XmlDocument();
            try
            {
                xd.LoadXml(xml);
                XmlNode xn = xd.SelectSingleNode("PACKET/PersonalPackagePolicyNewResult/ResponseCompleteMessageStatus/MessageStatusCode");
                string code = xn.InnerText;

                if (code == "1")//成功！
                {
                    entity.Detail = xd.OuterXml;
                }
                else
                {
                    xn = xd.SelectSingleNode("PACKET/PersonalPackagePolicyNewResult/ResponseCompleteMessageStatus/MessageStatusDescription");
                    entity.ErrorMsg = xn.InnerText;
                    entity.Detail = xd.OuterXml;
                }
            }
            catch
            {
                entity.ErrorMsg = "解析打印核销接口返回的xml字符串发生错误!";
                entity.Detail = xml;
            }

            return entity;
        }

        /// <summary>
        /// 根据投保结构体数组获取投保 XML 字符串参数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static string GetIssuingXML(IssueEntity entity)
        {
            try
            {
                BirthAndGender birthAndGender;
                birthAndGender = Common.GetBirthAndSex(entity.ID);
                entity.Birthday = birthAndGender.Birth;
                entity.Gender = birthAndGender.Gender;
            }
            catch
            {
                entity.Birthday = DateTime.Parse("1901-1-1");
                entity.Gender = Gender.Male;//"009";//未说明
            }
            PolicyCert pc = new PolicyCert();
            pc.T_APP_TM = pc.T_OPER_DATE = pc.T_SIGN_TM = DateTime.Today.ToString("yyyy-MM-dd");
            pc.name = entity.Name;
            pc.idNo = entity.ID;
            pc.idType = "005";//其他证件类型  
            pc.birthDate = entity.Birthday.ToString("yyyy-MM-dd");
            pc.gender = entity.Gender == Gender.Male ? "001" : "002";
            pc.effDate = entity.EffectiveDate.ToString("yyyy-MM-dd");
            pc.expDate = entity.ExpiryDate.ToString("yyyy-MM-dd");

            XmlDocument policy = XMLString.Issuing.Clone() as XmlDocument;

            //1。处理Base节点
            XmlNode para = policy.SelectSingleNode("PACKET/BODY/BASE");
            XmlNode parameter = para.Clone();
            parameter.SelectSingleNode("T_APP_TM").InnerText = pc.T_APP_TM;
            parameter.SelectSingleNode("T_SIGN_TM").InnerText = pc.T_SIGN_TM;
            parameter.SelectSingleNode("T_OPER_DATE").InnerText = pc.T_OPER_DATE;

            para.ParentNode.AppendChild(parameter);
            para.ParentNode.RemoveChild(para);

            //2。处理CUSTOMER节点
            para = policy.SelectSingleNode("PACKET/BODY/CUSTOMER_LIST/CUSTOMER");
            parameter = para.Clone();
            parameter.SelectSingleNode("C_APP_NME").InnerText = pc.name;
            // parameter.SelectSingleNode("productNo").InnerText = pc.productNo;
            parameter.SelectSingleNode("C_APP_CERT_TYP").InnerText = pc.idType;
            parameter.SelectSingleNode("C_APP_CERT_NO").InnerText = pc.idNo;
            parameter.SelectSingleNode("C_APP_SEX").InnerText = pc.gender;
            parameter.SelectSingleNode("N_APP_AGE").InnerText = pc.birthDate;

            parameter.SelectSingleNode("C_INSRNT_NME").InnerText = pc.name;
            parameter.SelectSingleNode("C_INSRNT_CERT_TYP").InnerText = pc.idType;
            parameter.SelectSingleNode("C_INSRNT_CERT_NO").InnerText = pc.idNo;
            parameter.SelectSingleNode("C_INSRNT_SEX").InnerText = pc.gender;
            parameter.SelectSingleNode("N_INSRNT_AGE").InnerText = pc.birthDate;

            parameter.SelectSingleNode("T_INSRNC_BGN_TM").InnerText = pc.effDate;
            parameter.SelectSingleNode("T_INSRNC_END_TM").InnerText = pc.expDate;

            para.ParentNode.AppendChild(parameter);
            para.ParentNode.RemoveChild(para);
            return policy.OuterXml;
        }

        /// <summary>
        /// 获取退保接口的 XML 字符串参数
        /// </summary>
        /// <param name="certNo"></param>
        /// <returns></returns>
        private static string GetWithdrawXML(string certNo)
        {
            XmlDocument policy = XMLString.Withdraw.Clone() as XmlDocument;
            XmlNode para = policy.SelectSingleNode("PACKET/BODY/RETPLY");
            XmlNode parameter = para.Clone();
            parameter.SelectSingleNode("C_PLY_NO").InnerText = certNo;
            parameter.SelectSingleNode("T_EDR_BGN_TM").InnerText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            para.ParentNode.AppendChild(parameter);
            para.ParentNode.RemoveChild(para);

            return policy.OuterXml;
        }

        /// <summary>
        /// 获取打印核销接口的 XML 串
        /// </summary>
        /// <param name="certNo"></param>
        /// <param name="printingNo"></param>
        /// <returns></returns>
        private static string GetPrintXML(string certNo, string printingNo)
        {
            XmlDocument policy = XMLString.Print.Clone() as XmlDocument;
            XmlNode para = policy.SelectSingleNode("PACKET/BODY/VCH");
            XmlNode parameter = para.Clone();
            parameter.SelectSingleNode("C_PRN_NO").InnerText = printingNo;
            parameter.SelectSingleNode("C_SYS_PRN_NO").InnerText = certNo;

            para.ParentNode.AppendChild(parameter);
            para.ParentNode.RemoveChild(para);

            return policy.OuterXml;
        }
    }

    public static class XMLString
    {
        private static XmlDocument issuing;
        private static XmlDocument withdraw;
        private static XmlDocument print;

        public static XmlDocument Issuing
        {
            get
            {
                if (issuing == null)
                {
                    issuing = new XmlDocument();
                    issuing.Load(System.IO.Path.Combine(Common.BaseDirectory, "App_Data/YongCheng/Issuing.xml"));
                }

                return issuing;
            }
        }

        public static XmlDocument Withdraw
        {
            get
            {
                if (withdraw == null)
                {
                    withdraw = new XmlDocument();
                    withdraw.Load(System.IO.Path.Combine(Common.BaseDirectory, "App_Data/YongCheng/Withdraw.xml"));
                }

                return withdraw;
            }
        }

        /// <summary>
        /// 打印核销
        /// </summary>
        public static XmlDocument Print
        {
            get
            {
                if (print == null)
                {
                    print = new XmlDocument();
                    print.Load(System.IO.Path.Combine(Common.BaseDirectory, "App_Data/YongCheng/Print.xml"));
                }

                return print;
            }
        }
    }
}
