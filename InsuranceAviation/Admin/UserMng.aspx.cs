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

public partial class Admin_UserMng : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.sdsUserList.SelectParameters[0].DefaultValue = User.Identity.Name;
    }
    protected void btnNewUser_Click(object sender, EventArgs e)
    {
        string username = ((TextBox)this.GridView1.FooterRow.FindControl("txtUsername")).Text.Trim();
        username = Common.Full2Half(username);
        string password = ((TextBox)this.GridView1.FooterRow.FindControl("txtPassword")).Text.Trim();
        string displayname = ((TextBox)this.GridView1.FooterRow.FindControl("txtDisplayname")).Text.Trim();
        string address = ((TextBox)this.GridView1.FooterRow.FindControl("txtAddress")).Text.Trim();
        string phone = ((TextBox)this.GridView1.FooterRow.FindControl("txtPhone")).Text.Trim();
        string userGroup = ((DropDownList)this.GridView1.FooterRow.FindControl("ddlUserCity")).SelectedItem.Text.Trim();

        this.sdsUserList.InsertParameters[0].DefaultValue = username;
        this.sdsUserList.InsertParameters[1].DefaultValue = password;
        this.sdsUserList.InsertParameters[2].DefaultValue = displayname;
        this.sdsUserList.InsertParameters[3].DefaultValue = address;
        this.sdsUserList.InsertParameters[4].DefaultValue = phone;
        this.sdsUserList.InsertParameters[5].DefaultValue = "true";
        this.sdsUserList.InsertParameters[6].DefaultValue = User.Identity.Name;

        DataSet ds = Common.DB.Select(Tables.t_User, Tables.t_User.usertype, Tables.t_User.parentPath)
                                            .Where(Tables.t_User.username == User.Identity.Name).ToDataSet();
        object parentUserType = ds.Tables[0].Rows[0][0];
        object parentPath = ds.Tables[0].Rows[0][1];
        int userType = Convert.ToInt32(parentUserType) + 1;
        string path = parentPath.ToString() + User.Identity.Name + "/";
        string[] pathArray = path.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
        string distributor = pathArray.Length > 1 ? pathArray[1] : username;//如果路径分组长度小于1,则表示当下是管理员在创建用户,则经销商就是该用户自己.

        this.sdsUserList.InsertParameters[7].DefaultValue = userType.ToString();
        this.sdsUserList.InsertParameters[8].DefaultValue = userGroup;
        this.sdsUserList.InsertParameters[9].DefaultValue = path;
        this.sdsUserList.InsertParameters[10].DefaultValue = distributor;

        try
        {
            this.sdsUserList.Insert();
        }
        catch(Exception ee)
        {
            this.lblError.Text = "添加失败，该用户名可能已经被占用！<span style='color:gray'>（详情：" + ee.Message + ")</span>";
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#ffd7d4'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string arrayUsername = string.Empty;
        string arrayUsernameFaild = string.Empty;
        this.lblError.Text = string.Empty;

        foreach (GridViewRow gvr in this.GridView1.Rows)
        {
            CheckBox cbDel = (CheckBox)gvr.FindControl("cbDel");
            if (cbDel.Checked)
            {
                //string username = gvr.Cells[3].Text;
                string username = this.GridView1.DataKeys[gvr.RowIndex].Value.ToString();

                object count = Common.DB.Select(Tables.t_Serial, Tables.t_Serial.datetime.Count()).Where(Tables.t_Serial.caseOwner == username).ToScalar();

                if (count != null)
                {
                    arrayUsernameFaild += username + ",";
                    continue;
                }
                else
                {
                    count = Common.DB.Select(Tables.t_Case, Tables.t_Case.datetime.Count()).Where(Tables.t_Case.caseOwner == username).ToScalar();

                    if (count != null)
                    {
                        arrayUsernameFaild += username + ",";
                        continue;
                    }
                }

                arrayUsername += username + ",";
            }
        }

        arrayUsername = arrayUsername.TrimEnd(',');
        arrayUsernameFaild = arrayUsernameFaild.TrimEnd(',');

        string[] array = arrayUsername.Split(',');
        Common.DB.Delete(Tables.t_User).Where(Tables.t_User.username.In(array)).Execute();
        this.GridView1.DataBind();

        if(!string.IsNullOrEmpty(arrayUsernameFaild))
            this.lblError.Text = "因仍拥有单证号记录，无法删除的账号：" + arrayUsernameFaild;
    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        DropDownList ddlProvince = (DropDownList)this.GridView1.FooterRow.FindControl("ddlUserProvince");
        DropDownList ddlCity = (DropDownList)this.GridView1.FooterRow.FindControl("ddlUserCity");
        this.sdsCity.SelectParameters[0].DefaultValue = ddlProvince.SelectedValue;
        ddlCity.DataBind();
    }

    protected void ddlUserProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlProvince = (DropDownList)sender;
        DropDownList ddlCity = (DropDownList)this.GridView1.FooterRow.FindControl("ddlUserCity");
        this.sdsCity.SelectParameters[0].DefaultValue = ddlProvince.SelectedValue;
        ddlCity.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.GridView1.DataBind();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (e.Keys[0].ToString().Equals(User.Identity.Name, StringComparison.CurrentCultureIgnoreCase))
            e.Cancel = true;
    }
}
