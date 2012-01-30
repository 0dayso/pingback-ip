﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class DefaultHLLogin : NBear.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void btnPublicQuery_Click(object sender, EventArgs e)
    {
        Response.Redirect("Public/Query.aspx");
    }

    void Login()
    {
        //if (this.txtSerial.Text.Equals(Session["ValidateCode"].ToString()))
        {
            string user = this.txtUsername.Text.Trim();
            string pass = this.txtPassword.Text.Trim();
            //pass = this.Cryptography.ComputeHash(pass);

            IAClass.Entity.UserLoginResponse loginResult = UserClass.Login(user, pass, Request.UserHostAddress);

            if (string.IsNullOrEmpty(loginResult.Trace.ErrorMsg))
            {
                IAClass.Entity.UserType userType = loginResult.Type;
                bool isPersist = chkPersistCookie.Checked;
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user, DateTime.Now, DateTime.Now.AddMinutes(30), isPersist, "your custom data");
                string strCookie = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookieTicket = new HttpCookie(FormsAuthentication.FormsCookieName, strCookie);

                if (isPersist)
                {
                    cookieTicket.Expires = ticket.Expiration;
                }

                cookieTicket.Path = FormsAuthentication.FormsCookiePath;
                Response.Cookies.Add(cookieTicket);

                string strRedirect = Request["ReturnUrl"];

                if (string.IsNullOrEmpty(strRedirect))
                {
                    strRedirect = "Admin/Index.htm";
                }

                //由于本页是被js控制在iframe中打开的，所以必须指定父窗口跳转，否则登陆后的页面将出现在iframe中
                Response.Write("<script language=\"javascript\">window.parent.location.href='" + strRedirect + "';</script>");
            }
            else
            {
                this.ClientScript.RegisterClientScriptBlock(GetType(), "1", this.ClientScriptFactory.PopAlert(loginResult.Trace.ErrorMsg), true);
                this.txtPassword.Focus();
            }
        }
        //else
        //    this.ClientScript.RegisterClientScriptBlock(GetType(), "1", this.ClientScriptFactory.PopAlert("验证码不正确！"), true);
    }
    protected void btnLogin_Click(object sender, ImageClickEventArgs e)
    {
        Login();
    }
}