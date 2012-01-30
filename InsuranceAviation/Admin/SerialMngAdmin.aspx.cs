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
using System.Data.SqlClient;
using IAClass.Entity;
using System.Collections.Generic;

public partial class SerialMngAdmin : NBear.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!UserClass.IsAdmin(User.Identity.Name))
            Response.Redirect("SerialMngUser.aspx", true);

        if (!IsPostBack)
        {
            this.hfUsername.Value = User.Identity.Name;

            this.DropDownList1.DataSource = Common.DB.Select(Tables.t_Log, Tables.t_Log.LogType.Distinct).ToDataSet();
            this.DropDownList1.DataTextField = "LogType";
            this.DropDownList1.DataValueField = "LogType";
            this.DropDownList1.DataBind();

            this.ddlInsurer.DataBind();
        }

        this.SqlDataSource1.SelectParameters[0].DefaultValue = User.Identity.Name;
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string pipelineStart = this.txtNewStart.Text.Trim().ToUpper();
            string pipelineEnd = this.txtNewEnd.Text.Trim().ToUpper();
            string include = txtInclude.Text.Trim();
            string exclude = txtExclude.Text.Trim();
            string locationComment = txtLocationComment.Text.Trim();
            int count;

            try
            {
                count = Serial.Create(pipelineStart, pipelineEnd, this.ddlInsurer.SelectedValue, ddlInsurer.SelectedItem.Text,
                    include, exclude, locationComment);
            }
            catch (Exception ePipelineExc)
            {
                this.lblError.Text = "创建单证号发生错误！";
                this.lblError.Text += "<br /><span style='color:gray'>（详情：" + ePipelineExc.Message + ")</span>";
                return;
            }

            string scriptSucc = this.ClientScriptFactory.PopAlert("成功创建 " + count + " 个单证号！");
            this.ClientScript.RegisterClientScriptBlock(GetType(), "", scriptSucc, true);
            this.gvLog.DataBind();
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
    protected void btnDiscard_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string pipelineStart = this.txtDiscardStart.Text.Trim();
            string pipelineEnd = this.txtDiscardEnd.Text.Trim();

            string[] arrangedNumber = Common.GetArranged(pipelineStart, pipelineEnd);

            if (arrangedNumber.Length == 0)
            {
                this.lblErrorDiscard.Text = "操作失败，请正确填写起止单证号！";
                return;
            }

            int count = 0, countEffect = 0;
            System.Data.Common.DbTransaction tran = Common.DB.BeginTransaction();

            foreach (string number in arrangedNumber)
            {
                countEffect = Common.DB.Delete(Tables.t_Serial)
                    .Where(Tables.t_Serial.caseNo == number)
                    .Where(Tables.t_Serial.caseOwner == User.Identity.Name)
                    .SetTransaction(tran)
                    .Execute();

                if (countEffect > 0)
                    count++;
                else
                {
                    tran.Rollback();
                    this.lblErrorDiscard.Text = string.Format("销毁单证号 {0} 发生错误！请确认您是否拥有该单证号。", number);
                    return;
                }
            }

            if (count > 0)
            {
                Common.DB.Insert(Tables.t_Log)
                    .AddColumn(Tables.t_Log.LogContent, "销毁 " + pipelineStart + "－" + pipelineEnd + " 共 " + count + " 个单证号")
                    .AddColumn(Tables.t_Log.LogType, "销毁")
                    .AddColumn(Tables.t_Log.LogOwner, User.Identity.Name)
                    .AddColumn(Tables.t_Log.datetime, DateTime.Now)
                    .SetTransaction(tran)
                    .Execute();
                tran.Commit();
                string scriptSucc = this.ClientScriptFactory.PopAlert("成功销毁 " + count.ToString() + " 个单证号！");
                this.ClientScript.RegisterClientScriptBlock(GetType(), "", scriptSucc, true);
                this.gvLog.DataBind();
            }
            else
            {
                this.lblErrorDiscard.Text = "没有销毁任何单证号！";
                tran.Commit();
            }
        }
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
            DateTime dtEnd = DateTime.Parse(dateEnd).AddDays(1);

            this.SqlDataSource1.SelectParameters["date1"].DefaultValue = dateStart;
            this.SqlDataSource1.SelectParameters["date2"].DefaultValue = dtEnd.ToShortDateString();
        }
    }

    void GetProductList()
    {
        //this.ddlProductList.Items.Clear();

        //List<t_Product> productList = Product.GetProductList(this.ddlInsurer.SelectedValue);
        //foreach (t_Product product in productList)
        //{
        //    ListItem item = new ListItem();
        //    item.Text = product.productName;
        //    item.Value = product.productID + "|" + product.productDuration;
        //    this.ddlProductList.Items.Add(item);
        //}
    }

    protected void ddlInsurer_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetProductList();
    }
}
