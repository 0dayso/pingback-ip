<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Web.UI;
using System.IO;

public class Handler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Write("Hello World");

        //context.Response.ContentType = "text/plain";

        //ViewManager<Admin_test_WebUserControl> viewManager = new ViewManager<Admin_test_WebUserControl>();
        //Admin_test_WebUserControl control = viewManager.LoadViewControl("~/ItemComments.ascx");

        //control.PageIndex = Int32.Parse(context.Request.QueryString["page"]);
        //control.PageSize = 3;

        //context.Response.Write(viewManager.RenderView(control));

    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}

public class ViewManager<T> where T : UserControl
{
    private Page m_pageHolder;

    public T LoadViewControl(string path)
    {
        this.m_pageHolder = new IAClass.MyPage();
        return (T)this.m_pageHolder.LoadControl(path);
    }

    public string RenderView(T control)
    {
        StringWriter output = new StringWriter();

        this.m_pageHolder.Controls.Add(control);
        HttpContext.Current.Server.Execute(this.m_pageHolder, output, false);

        return output.ToString();
    }
}