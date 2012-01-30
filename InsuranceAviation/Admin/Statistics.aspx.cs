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

public partial class Admin_Statistics : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!UserClass.IsAdmin(User.Identity.Name))
            Response.End();
    }
    protected void btnCount_Click(object sender, EventArgs e)
    {
        this.gvCount.DataSourceID = "sdsCount";
    }
    protected void btnHot_Click(object sender, EventArgs e)
    {
        this.gvHot.DataSourceID = "sdsHot";
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        this.gvCount.PageSize = 999;
        this.gvCount.DataBind();
        Common.ExportExcelFromGridView(this.gvCount);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }
    protected void gvCount_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Attributes.Add("style", "vnd.ms-excel.numberformat: @");// 文本格式
            e.Row.Cells[4].Attributes.Add("style", "vnd.ms-excel.numberformat: @");// 文本格式
        }
    }
}
