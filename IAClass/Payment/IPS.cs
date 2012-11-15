using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAClass.Entity;
using IAClass.Payment;
using System.Configuration;
using System.Web;
using System.Web.Security;

namespace IPS
{
    class Payment : IBank
    {
        //Mer_code, Mer_key, Merchanturl, ServerUrl
        static string[] config = ConfigurationManager.AppSettings["IPS"].Split(',');
        //支付结果成功返回的商户URL
        static string Merchanturl = "http://" + HttpContext.Current.Request.Url.Host + "/public/ips_return.aspx";
        //Server to Server 返回页面URL
        static string ServerUrl = "http://" + HttpContext.Current.Request.Url.Host + "/public/ips_notify.aspx";

        public void Transfer(PaymentEntity entity)
        {
            string form_url = "https://pay.ips.com.cn/ipayment.aspx";
            //商户号
            string Mer_code = config[0];

            //商户证书：登陆http://merchant.ips.com.cn/商户后台下载的商户证书内容
            string Mer_key = config[1];

            //商户订单编号
            string Billno = entity.OrderId;

            //订单金额(保留2位小数)
            string Amount = entity.Amount.ToString("F2");

            //订单日期
            string BillDate = DateTime.Now.ToString("yyyyMMdd");

            //币种
            string Currency_Type = "RMB";

            //支付卡种 借记卡
            string Gateway_Type = "01";

            //语言
            string Lang = "GB";

            //支付结果失败返回的商户URL
            string FailUrl = "";//Request.Form["FailUrl"];

            //支付结果错误返回的商户URL
            string ErrorUrl = "";//Request.Form["ErrorUrl"];

            //商户数据包
            string Attach = entity.Attach;

            //显示金额
            string DispAmount = Amount;

            //订单支付接口加密方式 md5
            string OrderEncodeType = "5";

            //交易返回接口加密方式 md5
            string RetEncodeType = "17";

            //返回方式 有Server to Server方式
            string Rettype = "1";

            //订单支付接口的Md5摘要，原文="billno{订单编号}currencytype{支付币种}amount{金额}date{日期}orderencodetype{支付接口加密方式}{IPS证书}"
            string toBeSigned = "billno{0}currencytype{1}amount{2}date{3}orderencodetype{4}{5}";
            toBeSigned = string.Format(toBeSigned, Billno, Currency_Type, Amount, BillDate, OrderEncodeType, Mer_key);
            string SignMD5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(toBeSigned, "MD5").ToLower();

            string postForm = "<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + form_url + "\">";

            postForm += "<input type=\"hidden\" name=\"Mer_code\" value=\"" + Mer_code + "\" />";
            postForm += "<input type=\"hidden\" name=\"Billno\" value=\"" + Billno + "\" />";
            postForm += "<input type=\"hidden\" name=\"Amount\" value=\"" + Amount + "\" />";
            postForm += "<input type=\"hidden\" name=\"Date\" value=\"" + BillDate + "\" />";
            postForm += "<input type=\"hidden\" name=\"Currency_Type\" value=\"" + Currency_Type + "\" />";
            postForm += "<input type=\"hidden\" name=\"Gateway_Type\" value=\"" + Gateway_Type + "\" />";
            postForm += "<input type=\"hidden\" name=\"Lang\" value=\"" + Lang + "\" />";
            postForm += "<input type=\"hidden\" name=\"Merchanturl\" value=\"" + Merchanturl + "\" />";
            postForm += "<input type=\"hidden\" name=\"FailUrl\" value=\"" + FailUrl + "\" />";
            postForm += "<input type=\"hidden\" name=\"ErrorUrl\" value=\"" + ErrorUrl + "\" />";
            postForm += "<input type=\"hidden\" name=\"Attach\" value=\"" + Attach + "\" />";
            postForm += "<input type=\"hidden\" name=\"DispAmount\" value=\"" + DispAmount + "\" />";
            postForm += "<input type=\"hidden\" name=\"OrderEncodeType\" value=\"" + OrderEncodeType + "\" />";
            postForm += "<input type=\"hidden\" name=\"RetEncodeType\" value=\"" + RetEncodeType + "\" />";
            postForm += "<input type=\"hidden\" name=\"Rettype\" value=\"" + Rettype + "\" />";
            postForm += "<input type=\"hidden\" name=\"ServerUrl\" value=\"" + ServerUrl + "\" />";
            postForm += "<input type=\"hidden\" name=\"SignMD5\" value=\"" + SignMD5 + "\" />";
            postForm += "</form>";

            //自动提交该表单到测试网关
            postForm += "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";

            System.Web.HttpContext.Current.Response.Write(postForm);
        }

        public void Callback_Return(PayingCallbackEntity entity)
        {
            HttpRequest Request = HttpContext.Current.Request;
            HttpResponse Response = HttpContext.Current.Response;

            //接收数据
            string billno = Request["billno"];
            string amount = Request["amount"];
            string currency_type = Request["Currency_type"];
            string mydate = Request["date"];
            string succ = Request["succ"];
            string msg = Request["msg"];
            string attach = Request["attach"];
            string ipsbillno = Request["ipsbillno"];
            string retEncodeType = Request["retencodetype"];
            string signature = Request["signature"];

            if (succ != "Y")
            {
                Response.Write("交易失败！");
            }
            else
            {
                //签名原文 billno{订单编号}currencytype{币种}amount{订单金额}date{订单日期}succ{成功标志}ipsbillno{IPS订单编号}retencodetype{交易返回签名方式}{商户内部证书字符串}
                string toBeSigned = "billno{0}currencytype{1}amount{2}date{3}succ{4}ipsbillno{5}retencodetype{6}{7}";
                toBeSigned = string.Format(toBeSigned, billno, currency_type, amount, mydate, succ, ipsbillno, retEncodeType, config[1]);

                //签名是否正确
                Boolean verify = false;

                //验证方式：16-md5withRSA  17-md5
                //if (retEncodeType == "16")
                //{
                //    string pubfilename = "C:\\PubKey\\publickey.txt";
                //    RSAMD5Class m_RSAMD5Class = new RSAMD5Class();
                //    int result = m_RSAMD5Class.VerifyMessage(pubfilename, toBeSigned, signature);

                //    //result=0   表示签名验证成功
                //    //result=-1  表示系统错误
                //    //result=-2  表示文件绑定错误
                //    //result=-3  表示读取公钥失败
                //    //result=-4  表示签名长度错
                //    //result=-5  表示签名验证失败
                //    //result=-99 表示系统锁定失败
                //    if (result == 0)
                //    {
                //        verify = true;
                //    }
                //}
                //else if (retEncodeType == "17")
                {
                    //Md5摘要
                    string signature1 = FormsAuthentication.HashPasswordForStoringInConfigFile(toBeSigned, "MD5").ToLower();

                    if (signature1 == signature)
                    {
                        verify = true;
                    }
                }

                //判断签名验证是否通过
                if (verify == true)
                {
                    //判断交易是否成功
                    if (succ != "Y")
                    {
                        Response.Write("交易失败！");
                        Response.End();
                    }
                    else
                    {
                        /****************************************************************
                        //比较返回的订单号和金额与您数据库中的金额是否相符			
                        if(不等)
                        {
                            Response.Write("从IPS返回的数据和本地记录的不符合，失败！");
                            Response.End(); 
                        }
                        else
                        {
                            Reponse.Write("交易成功，处理您的数据库！");
                        }
                        ****************************************************************/

                        if (IAClass.Bussiness.Payment.CheckIn(billno, ipsbillno))
                            Response.Write("付款成功");
                        else
                            Response.Write("订单确认失败,请联系管理员进行人工核对!");
                    }
                }
                else
                {
                    Response.Write("签名不正确！");
                }
            }
        }

        public void Callback_Notify(PayingCallbackEntity entity)
        {
            HttpRequest Request = HttpContext.Current.Request;
            HttpResponse Response = HttpContext.Current.Response;

            //接收数据
            string billno = Request["billno"];
            string amount = Request["amount"];
            string currency_type = Request["Currency_type"];
            string mydate = Request["date"];
            string succ = Request["succ"];
            string msg = Request["msg"];
            string attach = Request["attach"];
            string ipsbillno = Request["ipsbillno"];
            string retEncodeType = Request["retencodetype"];
            string signature = Request["signature"];

            if (succ != "Y")
            {
                Response.Write("交易失败！");
            }
            else
            {
                //签名原文 billno{订单编号}currencytype{币种}amount{订单金额}date{订单日期}succ{成功标志}ipsbillno{IPS订单编号}retencodetype{交易返回签名方式}{商户内部证书字符串}
                string toBeSigned = "billno{0}currencytype{1}amount{2}date{3}succ{4}ipsbillno{5}retencodetype{6}{7}";
                toBeSigned = string.Format(toBeSigned, billno, currency_type, amount, mydate, succ, ipsbillno, retEncodeType, config[1]);

                //签名是否正确
                Boolean verify = false;

                //验证方式：16-md5withRSA  17-md5
                //if (retEncodeType == "16")
                //{
                //    string pubfilename = "C:\\PubKey\\publickey.txt";
                //    RSAMD5Class m_RSAMD5Class = new RSAMD5Class();
                //    int result = m_RSAMD5Class.VerifyMessage(pubfilename, toBeSigned, signature);

                //    //result=0   表示签名验证成功
                //    //result=-1  表示系统错误
                //    //result=-2  表示文件绑定错误
                //    //result=-3  表示读取公钥失败
                //    //result=-4  表示签名长度错
                //    //result=-5  表示签名验证失败
                //    //result=-99 表示系统锁定失败
                //    if (result == 0)
                //    {
                //        verify = true;
                //    }
                //}
                //else if (retEncodeType == "17")
                {
                    //Md5摘要
                    string signature1 = FormsAuthentication.HashPasswordForStoringInConfigFile(toBeSigned, "MD5").ToLower();

                    if (signature1 == signature)
                    {
                        verify = true;
                    }

                }

                //判断签名验证是否通过
                if (verify == true)
                {
                    //判断交易是否成功
                    if (succ != "Y")
                    {
                        Response.Write("交易失败！");
                        Response.End();
                    }
                    else
                    {
                        IAClass.Bussiness.Payment.CheckIn(billno, ipsbillno);
                        Response.Write("ipscheckok");
                        Response.End();
                        /****************************************************************
                        //比较返回的订单号和金额与您数据库中的金额是否相符			
                        if(不等)
                        {
                            Response.Write("从IPS返回的数据和本地记录的不符合，失败！");
                            Response.End(); 
                        }
                        else
                        {
                            '***注：由于使用server to server的方式和browser两种方式一起使用，在数据库处理上面，需要对接收到的订单做状态判断，判断下是否已经对这笔订单做过处理了，如果处理过了就不再处理了，如果没有的话，就进行确认。
                            '***注：处理您的数据库的时候，请仔细参阅<<环迅IPS3.0系统接口手册>>中关于<<附录2. 交易返回方式>>的说明
                            'Response.Write "交易成功，处理您的数据库！"
                             'If 若您确认接收，并且交易处理完成 then
                                '请务必发送 ipscheckok(小写)给 ips，否则 IPS 将不断发送该笔定单信息给商户
                                 'Response.Write("ipscheckok");
                                 'Response.End(); 
                             'Else
                                'Response.End
                             'End If   
                        }
                        ****************************************************************/
                    }
                }
                else
                {
                    Response.Write("签名不正确！");
                }
            }
        }
    }
}
