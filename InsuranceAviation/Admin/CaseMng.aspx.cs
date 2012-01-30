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

public partial class Aviation_CaseMng : NBear.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.SqlDataSource1.SelectParameters["caseOwner"].DefaultValue = User.Identity.Name;
    }
    protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        int pages;
        pages = Convert.ToInt32(e.Command.Parameters["@pageCount"].Value);
        pn1.Count = pages;
    }

    public string GetImagePath(object enable)
    {
        if (Convert.ToBoolean(enable))
            return "~/images/cyclebin.gif";
        else
            return "~/images/cyclebin_gray.gif";
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        IAClass.Entity.TraceEntity trace = Case.Discard(User.Identity.Name, e.Keys[0].ToString());
        if (!string.IsNullOrEmpty(trace.ErrorMsg))
        {
            this.ClientScript.RegisterClientScriptBlock(GetType(), "1", this.ClientScriptFactory.PopAlert(trace.ErrorMsg), true);
        }
    }
    public bool IsOver(object flightDate)
    {
        DateTime dtFlightDate = Convert.ToDateTime(flightDate);
        DateTime dtNow = DateTime.Today;

        if (dtFlightDate > dtNow)
            return true;
        else
            return false;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
}
