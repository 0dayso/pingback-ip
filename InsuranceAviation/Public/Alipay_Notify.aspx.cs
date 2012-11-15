using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using Com.Alipay;

public partial class Public_Alipay_Notify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IAClass.Bussiness.Payment.Callback_Notify(System.Configuration.ConfigurationManager.AppSettings["PaymentGatewayForAdmin"]);
    }
}