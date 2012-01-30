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

public partial class Admin_DefaultInsurer : NBear.Web.UI.Page
{
    public string strJavascript = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.SqlDataSource1.SelectParameters["insurer"].DefaultValue = User.Identity.Name;

        if (!IsPostBack)
        {
            this.txtDateStart.Text = this.txtDateEnd.Text = DateTime.Today.ToShortDateString();
            this.lblUsername.Text = User.Identity.Name;
        }
    }

    /// <summary>
    /// 每天提交量
    /// </summary>
    public string TopEveryday()
    {
        StringBuilder xmlData = new StringBuilder();
        xmlData.Append("<chart caption='每日出单总量' showAboutMenuItem='0' showValues='1' labelDisplay='Stagger' formatNumberScale='0' showBorder='0' outCnvBaseFontSize='12'>");

        DateTime dtToday = DateTime.Today;
        string dateStart = txtDateStart.Text.Trim();
        const int len = 7;

        if (dateStart != string.Empty)
        {
            dtToday = DateTime.Parse(dateStart);
        }

        for (int i = 0; i < len; i++)
        {
            DateTime dt = dtToday.AddDays(1 - len + i);
            string strDate = dt.ToString("M月d日");
            string strValue = Case.CountEnableddByInsurer(User.Identity.Name, dt).ToString();

            xmlData.AppendFormat("<set label='{0}' value='{1}' />", strDate, strValue);
        }

        xmlData.Append(@"<styles><definition><style type='font' name='myToolTipFont' size='12' /></definition><application><apply toObject='ToolTip' styles='myToolTipFont' /></application></styles>");
        xmlData.Append("</chart>");

        return FusionCharts.RenderChart("../images/Column3D.swf", "", xmlData.ToString(), "myNext", "400", "200", false, true);
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
                if (field.HeaderText.Contains("身份证"))
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
        return "作废:" + Case.CountDiscardedIncludingChild(HttpContext.Current.User.Identity.Name).ToString();
    }
}
