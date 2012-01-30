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

public partial class passwordChange : NBear.Web.UI.Page
{
    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            string user = User.Identity.Name;

            string password = txtPassOld.Text.Trim();
            //password = this.Cryptography.ComputeHash(password);

            DataSet ds = Common.DB.Select(Tables.t_User, Tables.t_User.username)
                .Where(Tables.t_User.username == user && Tables.t_User.password == password).ToDataSet();

            if (ds.Tables[0].Rows.Count != 0)
            {
                Common.DB.Update(Tables.t_User).AddColumn(Tables.t_User.password, txtPassNew1.Text.Trim())
                    .Where(Tables.t_User.username == user && Tables.t_User.password == password).Execute();

                this.ClientScript.RegisterClientScriptBlock(typeof(string), "1", this.ClientScriptFactory.PopAlert("密码修改成功！"), true);
            }
            else
                this.ClientScript.RegisterClientScriptBlock(typeof(string), "1", this.ClientScriptFactory.PopAlert("您输入的旧密码不正确！"), true);
        }
    }
}
