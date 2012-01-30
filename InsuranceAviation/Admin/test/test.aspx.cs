using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [System.Web.Services.WebMethod]
    public static string ServerFun(string param)
    {
        return  DateTime.Now.ToString() + "server:" + param;
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
}