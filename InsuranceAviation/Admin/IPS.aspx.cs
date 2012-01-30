using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_IPS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.lblBalance.Text = UserClass.GetBalance(User.Identity.Name).ToString();
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {

    }
}