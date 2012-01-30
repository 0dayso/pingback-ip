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

public partial class Admin_SerialAssign : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
            this.SqlDataSource1.SelectParameters[0].DefaultValue = User.Identity.Name;
    }

    public object GetCount(object username)
    {
        return Serial.GetBalance(username.ToString());
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridView1.DataBind();
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static string GetConsumed(string contextKey)
    {
        return Case.CountConsumedIncludingChild(contextKey).ToString();
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static string GetDiscarded(string contextKey)
    {
        return Case.CountDiscardedIncludingChild(contextKey).ToString();
    }

}
