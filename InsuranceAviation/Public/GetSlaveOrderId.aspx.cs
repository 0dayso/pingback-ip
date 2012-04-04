using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Public_GetSlaveOrderId : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string orderId = Request.QueryString["orderId"];
        string resp = "-1";

        if(!string.IsNullOrEmpty(orderId))
            resp = IAClass.Bussiness.Payment.GetSlaveOrderId(orderId);

        Response.Clear();
        Response.Write(resp);
        Response.End();
    }
}