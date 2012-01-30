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

public partial class SerialMngUser : NBear.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
            this.SqlDataSource1.SelectParameters[0].DefaultValue = User.Identity.Name;

            if (!IsPostBack)
            {
                this.DropDownList1.DataSource = Common.DB.Select(Tables.t_Log, Tables.t_Log.LogType.Distinct).ToDataSet();
                this.DropDownList1.DataTextField = "LogType";
                this.DropDownList1.DataValueField = "LogType";
                this.DropDownList1.DataBind();
            }
    }

    protected void gvLog_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#ffd7d4'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
        }
    }
}
