﻿using System;
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
        string username = ((TextBox)this.gvInsurerList.FooterRow.FindControl("txtUsername")).Text.Trim();
        string password = ((TextBox)this.gvInsurerList.FooterRow.FindControl("txtPassword")).Text.Trim();
        string displayname = ((TextBox)this.gvInsurerList.FooterRow.FindControl("txtDisplayname")).Text.Trim();

        this.sdsInsurerList.InsertParameters[0].DefaultValue = username;
        this.sdsInsurerList.InsertParameters[1].DefaultValue = password;
        this.sdsInsurerList.InsertParameters[2].DefaultValue = displayname;
        this.sdsInsurerList.InsertParameters[3].DefaultValue = "true";
        this.sdsInsurerList.InsertParameters[4].DefaultValue = User.Identity.Name;

        this.sdsInsurerList.InsertParameters[5].DefaultValue = "99";//类型 99 代表是保险公司
        this.sdsInsurerList.InsertParameters[6].DefaultValue = "/Admin/";//
        this.sdsInsurerList.Insert();
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

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static string GetConsumed(string contextKey)
    {
        return Case.CountConsumedIncludingChild(contextKey).ToString();
    }

    public object GetInterfaceStatus(object isIssuingRequired, object name)
    {
        bool required = Convert.ToBoolean(isIssuingRequired);
        if (required)
            return name;
        else
            return "<DEL>" + name + "</DEL>";
    }
}
