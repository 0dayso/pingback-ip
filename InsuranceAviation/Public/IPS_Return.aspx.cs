using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using IPSVERIFYLib;

public partial class Public_IPS_Return : System.Web.UI.Page
{
    //Mer_code, Mer_key, Merchanturl, ServerUrl
    static string[] config = ConfigurationManager.AppSettings["IPS"].Split(',');

    private void Page_Load(object sender, System.EventArgs e)
    {
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
            if (retEncodeType == "16")
            {
                string pubfilename = "C:\\PubKey\\publickey.txt";
                RSAMD5Class m_RSAMD5Class = new RSAMD5Class();
                int result = m_RSAMD5Class.VerifyMessage(pubfilename, toBeSigned, signature);

                //result=0   表示签名验证成功
                //result=-1  表示系统错误
                //result=-2  表示文件绑定错误
                //result=-3  表示读取公钥失败
                //result=-4  表示签名长度错
                //result=-5  表示签名验证失败
                //result=-99 表示系统锁定失败
                if (result == 0)
                {
                    verify = true;
                }
            }
            else if (retEncodeType == "17")
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

                    if (IAClass.Bussiness.Payment.Callback(billno, ipsbillno))
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
}

