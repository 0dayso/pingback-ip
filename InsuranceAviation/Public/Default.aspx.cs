using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAClass.Miscel;

public partial class Public_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext context = HttpContext.Current;

        MvcCaptchaOptions options = new MvcCaptchaOptions();
        var image = new MvcCaptchaImage(options);
        image.ResetText();
        HttpContext.Current.Session.Add("captcha", image.Text);

        using (var b = image.RenderImage())
        {
            b.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
        }

        context.Response.Cache.SetNoStore();
        context.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        context.Response.ContentType = "image/gif";
        context.Response.StatusCode = 200;
        context.Response.StatusDescription = "OK";
        context.ApplicationInstance.CompleteRequest();
    }
}