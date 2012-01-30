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
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        IssueEntity entity = new IssueEntity();
        entity.Name = "刘德华";
        entity.ID = "352224198204120013";
        entity.IDType = IdentityType.身份证;
        entity.Birthday = DateTime.Parse("1982-04-12");
        entity.Gender = Gender.Male;
        Response.Write(Common.SoapSerialize<IssueEntity>(entity));
        entity.IOC_TypeName = "PingAn";

        IssuingResultEntity ret = new IssuingFacade().Issue(entity);
        Response.Write("<BR>" + Common.SoapSerialize<IssuingResultEntity>(ret));
    }

    protected void Button2_Click1(object sender, EventArgs e)
    {
        Common.messageQ.Start();

        for (int i = 0; i < 500; i++)
        {
            IssueEntity entity = new IssueEntity();
            entity.Name = "刘德华";
            entity.ID = "352224198204120013";
            entity.IDType = IdentityType.身份证;
            entity.Birthday = DateTime.Parse("1982-04-12");
            entity.Gender = Gender.Male;
            entity.CaseNo = "PIC" + i.ToString().PadLeft(25, '0');
            entity.IOC_TypeName = "PingAn";
            Common.messageQ.EnqueueObject(entity);
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        
    }
}