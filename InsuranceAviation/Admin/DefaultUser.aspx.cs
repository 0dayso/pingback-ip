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
using System.Text;
using InfoSoftGlobal;

public partial class DefaultUser : NBear.Web.UI.Page
{
    public string strJavascript = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.SqlDataSource1.SelectParameters["caseOwner"].DefaultValue = User.Identity.Name;

        if (!IsPostBack)
        {
            this.txtDateStart.Text = this.txtDateEnd.Text = DateTime.Today.ToShortDateString();
            this.lblUsername.Text = User.Identity.Name;

            string count = Serial.GetBalance(User.Identity.Name).ToString();
            string balance = UserClass.GetBalance(User.Identity.Name).ToString();

            this.lblCount.Text = count;
            this.lblBalance.Text = balance;
        }
    }

    public System.Drawing.Color DiscardedColor(object enabled)
    {
        bool isEnabled = Convert.ToBoolean(enabled);

        if (isEnabled)
            return System.Drawing.Color.Black;
        else
            return System.Drawing.Color.Red;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        PrepareQueryParameters();
        SqlDataSource1.SelectParameters["pageSize"].DefaultValue = "99999";
        this.GridView1.Columns[0].Visible = true;
        Common.ExportExcelFromGridView(this.GridView1);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        PrepareQueryParameters();
    }

    void PrepareQueryParameters()
    {
        string dateStart = txtDateStart.Text.Trim();
        string dateEnd = txtDateEnd.Text.Trim();

        if (dateStart != string.Empty && dateEnd != string.Empty)
        {
            //DateTime dtEnd = DateTime.Parse(dateEnd).AddDays(1);
            dateEnd += " 23:59:59";

            this.SqlDataSource1.SelectParameters["dateStart"].DefaultValue = dateStart;
            this.SqlDataSource1.SelectParameters["dateEnd"].DefaultValue = dateEnd;
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#ffd7d4'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");

            int i = 0;

            foreach (DataControlField field in GridView1.Columns)
            {
                if (field.HeaderText.Split(new string[] { "证件号", "保单号", "单证号", "印刷号", "手机号" }, StringSplitOptions.None).Length == 2)
                    e.Row.Cells[i].Attributes.Add("style", "vnd.ms-excel.numberformat: @");// 文本格式

                i++;
            }
        }
    }
    protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        int pages;
        pages = Convert.ToInt32(e.Command.Parameters["@pageCount"].Value);
        pn1.Count = pages;
    }

    /// <summary>
    /// 是否作废
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public string GetCaseStatus(object status)
    {
        bool isValid = Convert.ToBoolean(status);

        if (isValid)
            return "有效";
        else
            return "作废";
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static string GetConsumed()
    {
        return Case.CountConsumedIncludingChild(HttpContext.Current.User.Identity.Name).ToString();
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static string GetDiscarded()
    {
        return Case.CountDiscardedIncludingChild(HttpContext.Current.User.Identity.Name).ToString();
    }
}
