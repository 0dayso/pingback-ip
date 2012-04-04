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

public partial class Public_IPS_Notify : System.Web.UI.Page
{
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

        //签名原文
        string content = billno + amount + mydate + succ + ipsbillno + currency_type;

        //签名是否正确
        Boolean verify = false;

        //验证方式：11-md5withRSA  12-md5
        if (retEncodeType == "11")
        {
            string pubfilename = "C:\\PubKey\\publickey.txt";
            RSAMD5Class m_RSAMD5Class = new RSAMD5Class();
            int result = m_RSAMD5Class.VerifyMessage(pubfilename, content, signature);

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
        else if (retEncodeType == "12")
        {
            //登陆http://merchant.ips.com.cn/商户后台下载的商户证书内容
            string merchant_key = "GDgLwwdK270Qj1w4xho8lyTpRQZV9Jm5x4NwWOTThUa4fMhEBK9jOXFrKRT6xhlJuU2FEa89ov0ryyjfJuuPkcGzO5CeVx5ZIrkkt1aBlZV36ySvHOMcNv8rncRiy3DQ";
            //Md5摘要
            string signature1 = FormsAuthentication.HashPasswordForStoringInConfigFile(content + merchant_key, "MD5").ToLower();

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
                IAClass.Bussiness.Payment.Callback(billno, ipsbillno);
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

