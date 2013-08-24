using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAClass;
using IAClass.Entity;
using IAClass.Issuing;
using log4net;

public partial class t : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write(System.Security.Principal.WindowsIdentity.GetCurrent().Name);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Write(StringHelper.EncryptionHelper.Encrypt(TextBox1.Text.Trim()));
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Write(StringHelper.EncryptionHelper.Decrpyt(TextBox1.Text.Trim()));
    }
}