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

public partial class Admin_InsurerMng : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!UserClass.IsAdmin(User.Identity.Name))
            Response.End();

        if (!IsPostBack)
            this.hfUsername.Value = User.Identity.Name;
    }
    protected void btnNewUser_Click(object sender, EventArgs e)
    {
        string username = ((TextBox)this.GridView1.FooterRow.FindControl("txtUsername")).Text.Trim();
        string password = ((TextBox)this.GridView1.FooterRow.FindControl("txtPassword")).Text.Trim();
        string displayname = ((TextBox)this.GridView1.FooterRow.FindControl("txtDisplayname")).Text.Trim();
        string address = ((TextBox)this.GridView1.FooterRow.FindControl("txtAddress")).Text.Trim();
        string phone = ((TextBox)this.GridView1.FooterRow.FindControl("txtPhone")).Text.Trim();

        this.SqlDataSource1.InsertParameters[0].DefaultValue = username;
        this.SqlDataSource1.InsertParameters[1].DefaultValue = password;
        this.SqlDataSource1.InsertParameters[2].DefaultValue = displayname;
        this.SqlDataSource1.InsertParameters[3].DefaultValue = address;
        this.SqlDataSource1.InsertParameters[4].DefaultValue = phone;
        this.SqlDataSource1.InsertParameters[5].DefaultValue = "true";
        this.SqlDataSource1.InsertParameters[6].DefaultValue = User.Identity.Name;

        this.SqlDataSource1.InsertParameters[7].DefaultValue = "99";//类型 99 代表是保险公司
        this.SqlDataSource1.InsertParameters[8].DefaultValue = "/Admin/";//
        this.SqlDataSource1.Insert();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#ffd7d4'");
        //    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
        //}
    }

    protected void btnNewProduct_Click(object sender, EventArgs e)
    {
        this.dvProduct.ChangeMode(DetailsViewMode.Insert);
    }
    protected void dvProduct_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
        this.gvProductList.DataBind();
    }
}
