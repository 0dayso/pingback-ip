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

public partial class Admin_SerialAssignStep2 : NBear.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.SqlDataSource2.SelectParameters[0].DefaultValue = User.Identity.Name;
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string pipelineStart = this.txtNewStart.Text.Trim();
            string pipelineEnd = this.txtNewEnd.Text.Trim();
            string userChild = Request.QueryString["id"];
            int count;

            try
            {
                count = Serial.Assign(pipelineStart, pipelineEnd, User.Identity.Name, userChild);
            }
            catch (Exception ePipelineExc)
            {
                this.lblError.Text = "分配单证号发生错误！";
                this.lblError.Text += "<br /><span style='color:gray'>（详情：" + ePipelineExc.Message + ")</span>";
                return;
            }

            string scriptSucc = this.ClientScriptFactory.PopAlert("成功分配 " + count + " 个单证号！");
            this.ClientScript.RegisterClientScriptBlock(GetType(), "", scriptSucc, true);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("SerialAssign.aspx");
    }
}
