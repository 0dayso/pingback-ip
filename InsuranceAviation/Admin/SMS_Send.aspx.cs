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
    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtCaseNo.Text = Request.QueryString["case"];
        this.txtMobile.Text = Request.QueryString["mobile"];
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            SMSEntity sms = new SMSEntity();
            sms.MobilePhone = txtMobile.Text.Trim();
            sms.Content = txtContent.Text.Trim();
            sms.CaseNo = txtCaseNo.Text.Trim();
            sms.IOC_TypeName = "ShangTong";
            TraceEntity result = SMS.Send(sms);

            if (!string.IsNullOrEmpty(result.ErrorMsg))
                this.lblResult.Text = result.ErrorMsg;
            else
            {
                this.lblResult.Text = "发送成功!";
            }
        }
        catch(Exception ee)
        {
            this.lblResult.Text = ee.ToString();
        }
    }
}