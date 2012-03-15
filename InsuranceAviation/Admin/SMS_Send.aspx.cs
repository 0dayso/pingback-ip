using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAClass.Entity;
using IAClass.SMS;

public partial class Admin_SMS_Send : System.Web.UI.Page
{
    static string smsContent = "$name$您好,您的航空意外保险已生效,生效期为$date$零时起至24时止,保单号:***********,可查询95511中国平安";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txtCaseNo.Text = Request.QueryString["case"];
            this.txtMobile.Text = Request.QueryString["mobile"];

            string name = HttpUtility.UrlDecode(Request.QueryString["name"]);
            string date = Request.QueryString["date"];
            this.txtContent.Text = smsContent.Replace("$name$", name).Replace("$date$", date);
        }
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                SMSEntity sms = new SMSEntity();
                sms.MobilePhone = txtMobile.Text.Trim();
                sms.Content = txtContent.Text.Trim();
                sms.CaseNo = txtCaseNo.Text.Trim();
                sms.IOC_Class_Alias = "ShangTong";
                TraceEntity result = SMS.Send(sms);

                if (!string.IsNullOrEmpty(result.ErrorMsg))
                    this.lblResult.Text = result.ErrorMsg;
                else
                {
                    this.lblResult.Text = "发送成功!";
                }
            }
            catch (Exception ee)
            {
                this.lblResult.Text = ee.ToString();
            }
        }
    }
}