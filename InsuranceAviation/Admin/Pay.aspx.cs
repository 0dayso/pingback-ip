using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAClass.Bussiness;
using IAClass.Entity;
using Com.Alipay;
using System.Configuration;

public partial class Public_Pay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.sdsPaymentList.SelectParameters["payer"].DefaultValue = User.Identity.Name;

        if (!UserClass.IsAdmin(User.Identity.Name))
            Server.Transfer("PayForAgent.aspx");
        //if (Request.QueryString.Count > 1)
        //{
        //    TxtSubject.Text = Request.QueryString["title"];
        //    TxtTotal_fee.Text = Request.QueryString["amount"];
        //}
        //else
        {
            TxtSubject.Text = User.Identity.Name + "用户充值";
        }
    }

    protected void BtnAlipay_Click(object sender, EventArgs e)
    {
        Pay();
    }

    private void Pay()
    {
        PaymentEntity entity = new PaymentEntity();
        entity.IOC_Class_Alias = ConfigurationManager.AppSettings["PaymentGatewayForAdmin"];
        entity.Gateway = entity.IOC_Class_Alias;

        //if (Request.QueryString.Count > 1)
        //{
        //    entity.Website = Request.QueryString["website"];
        //    entity.Amount = decimal.Parse(TxtTotal_fee.Text.Trim());
        //    entity.Title = Request.QueryString["title"];
        //    entity.Description = Request.QueryString["desc"];
        //    entity.Payer = Request.QueryString["payer"];
        //    entity.OrderID_Salve = Request.QueryString["OrderID_Salve"];
        //    entity.URL_Return_Slave = Request.QueryString["URL_Return_Slave"];
        //    entity.URL_Callback_Slave = Request.QueryString["URL_Callback_Slave"];
        //    entity.OrderId = Payment.CreateOrder(entity);
        //}
        //else
        {
            entity.Website = Request.Url.Host;
            entity.Amount = decimal.Parse(TxtTotal_fee.Text.Trim());
            entity.Title = TxtSubject.Text.Trim();
            entity.Description = TxtBody.Text.Trim();
            entity.Payer = User.Identity.Name;

            entity.OrderId = Payment.CreateOrder(entity);
        }

        //if (Request.Url.Host == Common.PaymentDomainName)
            Payment.Transfer(entity);
        //else
        //    Payment.TransferByMasterSite(entity);
    }

    public string GetStatus(object obj)
    {
        PaymentStatus status = (PaymentStatus)Enum.Parse(typeof(PaymentStatus), obj.ToString());
        if (status == PaymentStatus.交易完成)
            return "<font style='color:red'>" + status.ToString() + "</font>";
        return status.ToString();
    }
}