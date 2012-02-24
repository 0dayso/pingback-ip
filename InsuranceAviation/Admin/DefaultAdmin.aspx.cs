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
using System.IO;
using System.Data.SqlClient;
using System.Text;
using InfoSoftGlobal;


public partial class DefaultAdmin : NBear.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IAClass.Entity.UserType LoginUseType = UserClass.GetUserType(User.Identity.Name);

        if (LoginUseType == IAClass.Entity.UserType.Default)
            Response.Redirect("DefaultUser.aspx", true);
        else if (LoginUseType == IAClass.Entity.UserType.Insurer)
            Response.Redirect("DefaultInsurer.aspx", true);

        BindIt();

        if (!IsPostBack)
        {
            this.ddlProduct.DataBind();
            this.ddlProduct.Items.Add(new ListItem("--全部产品--", "0"));
            this.ddlProduct.SelectedIndex = this.ddlProduct.Items.Count - 1;

            this.txtDateStart.Text = this.txtDateEnd.Text = DateTime.Today.ToShortDateString();

            this.lblUsername.Text = User.Identity.Name;

            string count = Serial.GetBalance(User.Identity.Name).ToString();
            string balance = UserClass.GetBalance(User.Identity.Name).ToString();

            this.lblCount.Text = count;
            this.lblBalance.Text = balance;
        }
    }

    /// <summary>
    /// 每天出单总量
    /// </summary>
    [System.Web.Services.WebMethod]
    public static string TopEveryday(string dateStart, string productId)
    {
        StringBuilder xmlData = new StringBuilder();
        xmlData.Append("<chart caption='每日出单总量' showAboutMenuItem='0' showValues='1' labelDisplay='Stagger' formatNumberScale='0' showBorder='0' outCnvBaseFontSize='12' >");

        DateTime dtToday = DateTime.Today;
        const int len = 7;

        if (dateStart != string.Empty)
        {
            dtToday = DateTime.Parse(dateStart);
        }

        for (int i = 0; i < len; i++)
        {
            DateTime dt = dtToday.AddDays(1 - len + i);
            string strDate = dt.ToString("M月d日");
            object strValue;
            strValue = Case.TopEveryday(dt, productId);

            xmlData.AppendFormat("<set label='{0}' value='{1}' />", strDate, strValue);
        }

        xmlData.Append(@"<styles><definition><style type='font' name='myToolTipFont' size='12' /></definition><application><apply toObject='ToolTip' styles='myToolTipFont' /></application></styles>");
        xmlData.Append("</chart>");
        //2011.9.21 为配合ajax，改RenderCharT为RenderCharTHTML
        return FusionCharts.RenderChartHTML("../images/Column3D.swf", "", xmlData.ToString(), "TopEveryday", "400", "200", false, true);
    }

    /// <summary>
    /// 每日排名
    /// </summary>
    [System.Web.Services.WebMethod]
    public static string TopToday(string dateStart, string dateEnd, string productId)
    {
        StringBuilder xmlData = new StringBuilder();
        xmlData.Append("<chart caption='每日出单量排名' showAboutMenuItem='0' showValues='1' formatNumberScale='0' showBorder='0' outCnvBaseFontSize='12' >");

        DataSet ds = Case.TopToday(dateStart, dateEnd, productId);

        xmlData.Append("<categories>");
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            xmlData.AppendFormat("<category label='{0}' />", dr[0]);
        }
        xmlData.Append("</categories>");

        xmlData.Append("<dataset showValues='1'>");
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            xmlData.AppendFormat("<set value='{0}' />", dr[1]);
        }
        xmlData.Append("</dataset>");

        xmlData.Append(@"<styles><definition><style type='font' name='myToolTipFont' size='12' /></definition><application><apply toObject='ToolTip' styles='myToolTipFont' /></application></styles>");
        xmlData.Append("</chart>");
        return FusionCharts.RenderChartHTML("../images/MSBar3D.swf", "", xmlData.ToString(), "TopToday", "400", "200", false, true);
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
        if (TabContainer1.ActiveTab.ID == "tabForAdmin")
        {
            PrepareQueryParameters();
            sdsListForAdmin.SelectParameters["pageSize"].DefaultValue = int.MaxValue.ToString();
            //pn1.SelectedPage = 1;//this.SqlDataSource1.SelectParameters["pageNum"].DefaultValue = "1"; 无效，因为 pageNum 不取 DefaultValue 值，取的是 pn1 控件的 SelectedPage 值

            //在页面而非代码中指定了DataSourceID的情况下（注意这个条件），这里为什么不需要重新绑定？？ 猜测原因：导出操作中的 GridView.RenderControl() 方法将触发绑定事件
            gvListForAdmin.DataBind();

            this.gvListForAdmin.Columns[0].Visible = true;//是否作废
            this.gvListForAdmin.Columns[7].Visible = true;//出生日期
            this.gvListForAdmin.Columns[12].Visible = false;//短信
            Common.ExportExcelFromGridView(this.gvListForAdmin);

            //为什么注释掉下面的初始化语句也能让 GridView1 恢复正常？？
            //      猜测原因：aspx 页面中定义的标签替代了以下代码的设置
            //      实际原因：据测试发现，由于该按钮的事件不引发页面的刷新，似乎导致所有的赋值都是“临时的”，相比之下 PrepareQueryParameters() 方法中
            //对 dateStart 和 dateEnd 的赋值就能够正常保存（即页面ViewState）
            //GridView1.Columns[0].Visible = true;
            //SqlDataSource1.SelectParameters["pageSize"].DefaultValue = "10";
        }
        else if (TabContainer1.ActiveTab.ID == "tabForAdmin_NotIssued" && ddlProduct.SelectedValue != "0")
        {
            //导出20分钟内的单证
            foreach (DataControlField column in gvListForAdmin.Columns)
            {
                column.Visible = false;
            }
            this.gvListForAdmin.Columns[1].Visible = true;//提交时间
            this.gvListForAdmin.Columns[5].Visible = true;//姓名
            this.gvListForAdmin.Columns[6].Visible = true;//身份证
            this.gvListForAdmin.Columns[7].Visible = true;//出生日期
            this.gvListForAdmin.Columns[8].Visible = true;//性别
            this.gvListForAdmin.Columns[11].Visible = true;//乘机时间
            gvListForAdmin.DataSource = Case.ExportForAdmin_NotIssued(10000, ddlProduct.SelectedValue, txtDateStart.Text, txtDateEnd.Text);
            gvListForAdmin.DataSourceID = string.Empty;
            gvListForAdmin.DataBind();
            Common.ExportExcelFromGridView(this.gvListForAdmin);
        }
        else if (TabContainer1.ActiveTab.ID == "tabForAdmin_Discarded" && ddlProduct.SelectedValue != "0")
        {
            foreach (DataControlField field2 in this.gvListForAdmin.Columns)
            {
                field2.Visible = false;
            }
            this.gvListForAdmin.Columns[1].Visible = true;//提交时间
            this.gvListForAdmin.Columns[5].Visible = true;//姓名
            this.gvListForAdmin.Columns[6].Visible = true;//身份证
            this.gvListForAdmin.Columns[7].Visible = true;//出生日期
            this.gvListForAdmin.Columns[8].Visible = true;//性别
            this.gvListForAdmin.Columns[11].Visible = true;//乘机时间
            this.gvListForAdmin.DataSource = Case.ExportForAdmin_Discarded(10000, ddlProduct.SelectedValue, txtDateStart.Text, txtDateEnd.Text);
            this.gvListForAdmin.DataSourceID = string.Empty;
            this.gvListForAdmin.DataBind();
            Common.ExportExcelFromGridView(this.gvListForAdmin);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }

    protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        int pages;
        pages = Convert.ToInt32(e.Command.Parameters["@pageCount"].Value);
        pn1.Count = pages;
        pn2.Count = pages;
        pn3.Count = pages;
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

            this.sdsListForAdmin.SelectParameters["dateStart"].DefaultValue = dateStart;
            this.sdsListForAdmin.SelectParameters["dateEnd"].DefaultValue = dateEnd;

            this.sdsListForAdmin_NotIssued.SelectParameters["dateStart"].DefaultValue = dateStart;
            this.sdsListForAdmin_NotIssued.SelectParameters["dateEnd"].DefaultValue = dateEnd;

            this.sdsListForAdmin_Discarded.SelectParameters["dateStart"].DefaultValue = dateStart;
            this.sdsListForAdmin_Discarded.SelectParameters["dateEnd"].DefaultValue = dateEnd;
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#ffd7d4'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");

            int i = 0;

            GridView gv = (GridView)sender;

            foreach (DataControlField field in gv.Columns)
            {
                if (field.HeaderText.Split(new string[] { "证件号", "保单号", "单证号", "印刷号", "手机号" }, StringSplitOptions.None).Length == 2)
                    e.Row.Cells[i].Attributes.Add("style", "vnd.ms-excel.numberformat: @");// 文本格式

                i++;
            }
        }
    }

    public string GetSMStatus(object status)
    {
        string imgHtml = @"<img src=""../images/{0}"" alt=""{1}"" />";
        bool isSMSent = Convert.ToBoolean(status);

        if (isSMSent)
            return string.Format(imgHtml, "smSent.gif", "短信已发送");
        else
            return string.Format(imgHtml, "smNotSent.gif", "短信未发送"); 
    }

    //是否作废
    public string GetCaseStatus(object status)
    {
        bool isValid = Convert.ToBoolean(status);

        if (isValid)
            return "有效";
        else
            return "作废";
    }

    public string GetIpLocation(object obj)
    {
        string loca = obj.ToString();
        loca = loca.Split(new char[] { ' ', '　' }, StringSplitOptions.RemoveEmptyEntries)[0];
        return loca;
    }

    protected void BindIt()
    {
        string tabName = TabContainer1.ActiveTab.ID;
        switch (tabName)
        {
            case "tabForAdmin":
                this.gvListForAdmin.DataSourceID = "sdsListForAdmin";
                gvListForAdmin.DataBind();
                break;
            case "tabForAdmin_NotIssued":
                this.gvListForCPIC.DataSourceID = "sdsListForAdmin_NotIssued";
                gvListForCPIC.DataBind();
                break;
            case "tabForAdmin_Discarded":
                this.gvListForCPIC_Discarded.DataSourceID = "sdsListForAdmin_Discarded";
                gvListForCPIC_Discarded.DataBind();
                break;
        }
    }
}
