using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Ajax_Stat_Interface : IAClass.MyPage
{
    DateTime dtStart;
    DateTime dtEnd;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string dateStart = Request.QueryString["dateStart"];
            string dateEnd = Request.QueryString["dateEnd"];

            dtStart = DateTime.Parse(dateStart);
            dtEnd = DateTime.Parse(dateEnd);
        }
        catch(Exception ee)
        {
            Response.Write(ee.Message);
            Response.End();
        }
    }

    public int CountIssued(object interfaceId)
    {
        return InterfaceStat.CountIssued(dtStart, dtEnd, Convert.ToInt32(interfaceId));
    }

    public int CountWithdrawed(object interfaceId)
    {
        return InterfaceStat.CountWithdrawed(dtStart, dtEnd, Convert.ToInt32(interfaceId));
    }

    public int CountDone(object interfaceId)
    {
        return InterfaceStat.CountDone(dtStart, dtEnd, Convert.ToInt32(interfaceId));
    }
}