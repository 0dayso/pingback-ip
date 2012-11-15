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
    private void Page_Load(object sender, System.EventArgs e)
    {
        IAClass.Bussiness.Payment.Callback_Return(System.Configuration.ConfigurationManager.AppSettings["PaymentGatewayForAgent"]);
    }
}

