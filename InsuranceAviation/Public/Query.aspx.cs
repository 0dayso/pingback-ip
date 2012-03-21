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
using NBearLite;

public partial class Public_Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["customerId"]))
            {
                this.txtIdendity.Text = Request.QueryString["customerId"];
                btnQueryIdendity_Click(sender, e);
            }
        }
    }

    protected void btnQueryIdendity_Click(object sender, EventArgs e)
    {
        string idendity = this.txtIdendity.Text.Trim();
        DataSet ds = Common.DB.Select(Tables.t_Case, QueryColumn.All(Tables.t_Case), Tables.t_User.displayname, Tables.t_Product.productName)
                                    .Join(Tables.t_User, Tables.t_User.username == Tables.t_Case.caseOwner)
                                    .Join(Tables.t_Product, Tables.t_Product.productID == Tables.t_Case.productID)
                                    .Where(Tables.t_Case.customerID == idendity)
                                    .OrderBy(Tables.t_Case.datetime.Desc)
                                    .ToDataSet();

        if (ds.Tables[0].Rows.Count > 0)
        {
            this.GridView1.DataSource = ds;
            this.GridView1.DataBind();
            this.MultiView1.ActiveViewIndex = 1;

            //DataRow dr = ds.Tables[0].Rows[0];
            //bool enabled = Convert.ToBoolean(dr["enabled"]);

            //if (enabled)
            //{
            //    this.DetailsView1.DataSource = ds;
            //    this.DetailsView1.DataBind();
            //    this.MultiView1.ActiveViewIndex = 1;

            Common.DB.Update(Tables.t_Case)
                .AddColumn(Tables.t_Case.isQueryed, true)
                .AddColumn(Tables.t_Case.dateQueryed, DateTime.Now)
                .Where(Tables.t_Case.customerID == idendity)
                .Execute();
            //}
            //else
            //    this.lblInfo.Text = "该身份证号码相关的信息已被作废！";
        }
        else
        {
            this.lblInfo.Text = "未查询到与该身份证号码相关的信息！";
        }
    }

    public string GetCaseDuration(object duration, object flightDate)
    {
        switch (Convert.ToInt32(duration))
        {
            case 1:
                return "本次航班";
            case 7:
                DateTime dtStart = Convert.ToDateTime(flightDate).Date;
                DateTime dtEnd = dtStart.AddDays(6);
                return dtStart.ToShortDateString() + " 0:00:00 至<br>" + dtEnd.ToShortDateString() + " 23:59:59";
            default:
                return "";
        }
    }
}
