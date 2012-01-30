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

public partial class Agent_SerialWithdrawal : NBear.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string toUser = Request.QueryString["id"];
        this.lblToUser.Text = toUser;

        //DataSet dsSerialMinMax = Common.DB.Select(Tables.t_Serial, Tables.t_Serial.caseNo.Min(), Tables.t_Serial.caseNo.Max())
        //                            .Where(Tables.t_Serial.caseOwner == toUser)
        //                            .ToDataSet();
        //if (dsSerialMinMax.Tables[0].Rows.Count > 0)
        //{
        //    DataRow dr = dsSerialMinMax.Tables[0].Rows[0];
        //    this.lblSerialStart.Text = dr[0].ToString();
        //    this.lblSerialEnd.Text = dr[1].ToString();
        //}
        //else
        //{
        //    this.lblError.Text = "该用户没有库存单证号！";
        //    this.lblSerialEnd.Text = string.Empty;
        //    this.lblSerialStart.Text = string.Empty;
        //}
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string pipelineStart = this.txtNewStart.Text.Trim();
            string pipelineEnd = this.txtNewEnd.Text.Trim();
            string userChild = Request.QueryString["id"];
            int count = 0;

            try
            {
                count = Serial.Withdrawal(pipelineStart, pipelineEnd, User.Identity.Name, userChild);
            }
            catch (Exception ePipelineExc)
            {
                this.lblError.Text = "收回单证号发生错误！";
                this.lblError.Text += "<br /><span style='color:gray'>（详情：" + ePipelineExc.Message + ")</span>";
                return;
            }

            string scriptSucc = this.ClientScriptFactory.PopAlert("成功收回 " + count.ToString() + " 个单证号！");
            this.ClientScript.RegisterClientScriptBlock(GetType(), "", scriptSucc, true);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("SerialAssign.aspx");
    }
}
