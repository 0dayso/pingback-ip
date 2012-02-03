using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAClass.SMS;
using IAClass.Entity;

namespace ShangTong
{
    class ShangTong : ISMS
    {
        static SmsService ws = new SmsService();

        public TraceEntity Send(SMSEntity entity)
        {
            TraceEntity result = new TraceEntity();
            
            //使用BASE64对用户名转码
            String userID = Convert.ToBase64String(System.Text.ASCIIEncoding.Default.GetBytes("yourname"));
            //使用BASE64对密码转码
            String pwd = Convert.ToBase64String(System.Text.ASCIIEncoding.Default.GetBytes("yourpwd"));
            //使用BASE64对短信内容进行转码
            String smsContent = Convert.ToBase64String(System.Text.ASCIIEncoding.Default.GetBytes(entity.Content));
            //调用接口类中的send方法发送短信

            try
            {
                String ret = ws.send(userID, pwd, entity.MobilePhone, smsContent, "", "");

                switch (ret)
                {
                    case "100":
                        break;
                    case "102":
                        result.ErrorMsg = "用户或密码错误";
                        break;
                    case "103":
                        result.ErrorMsg = "余额不足";
                        break;
                    default:
                        result.ErrorMsg = "未知错误（系统异常）";
                        break;
                }
            }
            catch
            {
                Common.LogIt(ws.Url);
                throw;
            }

            return result;
        }

        public int GetBalance(string ioc)
        {
            return 0;
        }
    }
}
