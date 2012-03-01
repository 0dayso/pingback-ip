using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Ajax : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string func = Request.QueryString["func"];
        string ret = string.Empty;
        string dateStart = Request.QueryString["dateStart"];
        string dateEnd = Request.QueryString["dateEnd"];
        string productId = Request.QueryString["productId"];

        switch (func)
        {
            case "FlashTopEverydayTotal":
                ret = Case.FlashCountEveryday(dateStart, int.Parse(productId));
                break;
            case "FlashTopEverydayUser":
                ret = Case.FlashCountEveryday(dateStart, User.Identity.Name);
                break;
            case "FlashTopRange":
                ret = Case.FlashTopRange(dateStart, dateEnd, int.Parse(productId));
                break;
        }

        Response.Write(ret);
    }
}