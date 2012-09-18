using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAClass.Entity;
using IAClass.Payment;
using System.Configuration;
using System.Web;

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
    }
}
