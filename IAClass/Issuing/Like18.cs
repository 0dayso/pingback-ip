using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAClass;
using IAClass.Entity;
using System.Configuration;
using System.Net.Mail;

namespace Like18
{
    public class Issuing : IAClass.Issuing.IIssuing
    {
        static Like18Insurance ws = new Like18Insurance();
        private static string[] config = ConfigurationManager.AppSettings["Like18_Issuing"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        static string GetIdType(IdentityType type)
        {
            switch (type)
            {
                case IdentityType.护照:
                    return "2";
                case IdentityType.军官证:
                    return "3";
                default:
                    return "4";
            }
        }

        /// <summary>
        /// 投保
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IssuingResultEntity Issue(IssueEntity entity)
        {
            IssuingResultEntity result = new IssuingResultEntity();
            inMessage msg = new inMessage();

            try
            {
                BirthAndGender birthAndGender;
                birthAndGender = Common.GetBirthAndSex(entity.ID);
                entity.Birthday = birthAndGender.Birth;
                entity.Gender = birthAndGender.Gender;
                msg.applicantCertType = "1";//身份证
            }
            catch
            {
                entity.Birthday = DateTime.Parse("1901-1-1");
                entity.Gender = Gender.Male;
                msg.applicantCertType = GetIdType(entity.IDType);//护照
            }

            msg.agencyID = config[1];//用户名："bjjingji";
            msg.outerOrderID = entity.CaseNo;
            msg.productCode = entity.EffectiveDate.Date == DateTime.Today ? config[2] : config[3];//可保当日的产品代码："9971";次日：9982
            if (entity.EffectiveDate.Date == DateTime.Today)
            {
                //当日起保的产品不做了
                result.Trace.ErrorMsg = "下单失败！";
                result.Trace.Detail = "该款产品不支持当日下单、当日起保。";
                return result;
            }
            msg.policyBeginDate = entity.EffectiveDate;
            msg.unitCount = 1;//投保一份

            msg.applicantName = entity.Name;//投保人
            msg.applicantMobile = entity.PhoneNumber;
            
            msg.applicantCercCode = entity.ID;
            msg.applicantSex = entity.Gender == Gender.Male ? "1" : "0";
            msg.applicantBirth = entity.Birthday;

            msg.insuredName = msg.applicantName;
            msg.insuredMobile = msg.applicantMobile;
            msg.insuredCertType = msg.applicantCertType;
            msg.insuredCercCode = msg.applicantCercCode;
            msg.insuredSex = msg.applicantSex;
            msg.insuredBirth = msg.applicantBirth;
            msg.insurantNexus = "01";//投保人和被保人关系：本人

            try
            {
                rtnMessage ret = ws.standardApproval(msg);
                if (ret == null)
                    throw new Exception("WebService返回为空！");
                if (ret.returnCode != "S000")
                {
                    Common.LogIt("like8:" + ret.returnMessage);

                    if (ret.returnCode == "F011")//returnMessage.Contains("重复投保")
                    {
                        //result.PolicyNo = entity.CaseNo;
                        return result;
                    }

                    result.Trace.ErrorMsg = ret.returnMessage;
                    result.Trace.Detail = ret.returnCode;
                }
                else
                    result.PolicyNo = ret.policyNumber;
            }
            catch
            {
                Common.LogIt(ws.Url);
                throw;
            }

            return result;
        }

        public TraceEntity Withdraw(WithdrawEntity entity)
        {
            TraceEntity trace = new TraceEntity();
            return trace;
#region 邮件退保
            //string mailContent = "邮件内容";
            //string mailTo = config[4];//收件人地址

            //string mailFrom = "pingback@gmail.com";
            //string smtp = "smtp.gmail.com";
            //int smtpPort = 587;
            //string username = "pingback@gmail.com";
            //string password = "NIwls..w89";

            //MailMessage msg = new System.Net.Mail.MailMessage();
            //msg.To.Add(mailTo); //收件人

            ////发件人信息
            //msg.From = new MailAddress(mailFrom, "发送人姓名", System.Text.Encoding.UTF8);
            //msg.Subject = "这是测试邮件";   //邮件标题
            //msg.SubjectEncoding = System.Text.Encoding.UTF8;    //标题编码
            //msg.Body = mailContent; //邮件主体
            //msg.BodyEncoding = System.Text.Encoding.UTF8;
            //msg.IsBodyHtml = true;  //是否HTML
            //msg.Priority = MailPriority.High;   //优先级

            //SmtpClient client = new SmtpClient();
            ////设置邮箱账号和密码 
            //client.Credentials = new System.Net.NetworkCredential(username, password);
            //client.Port = smtpPort;
            //client.Host = smtp;
            //client.EnableSsl = true;

            //try
            //{
            //    client.Send(msg);
            //}
            //catch (Exception ex)
            //{
            //    trace.ErrorMsg = "发送退保邮件失败（立刻保）！";
            //    trace.Detail = ex.ToString();
            //}
            //return trace;
#endregion
        }
    }
}
