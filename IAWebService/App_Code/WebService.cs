using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using IAClass;
using IAClass.Entity;
using IAClass.WebService;

/// <summary>
/// WebService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "测试接口")]
    public string HelloWorld(string test)
    {
        return "Hello World：" + test;
    }

    [WebMethod(Description = "登入")]
    public UserLoginResponse Login(string username, string password)
    {
        return WebServiceClass.Login(username, password);
    }

    [WebMethod(Description = "登出")]
    public TraceEntity Logout(LogoutRequestEntity request)
    {
        return WebServiceClass.Logout(request);
    }

    [WebMethod(Description = "取一条单证记录")]
    public PolicyResponseEntity GetPolicy(string username, string password, string caseNo)
    {
        return WebServiceClass.GetPolicy(username, password, caseNo);
    }

    [WebMethod(Description = "出单记录列表")]
    public PolicyListResponseEntity GetPolicyList(string username, string password, int pageSize)
    {
        return WebServiceClass.GetPolicyList(username, password, pageSize);
    }

    [WebMethod(Description = "出单记录列表2")]
    public PolicyListResponseEntity GetPolicyListBetween(string username, string password, DateTime dtStart, DateTime dtEnd)
    {
        return WebServiceClass.GetPolicyListBetween(username, password, dtStart, dtEnd);
    }

    [WebMethod(Description = "作废")]
    public TraceEntity DiscardIt(string username, string password, string caseNo)
    {
        return WebServiceClass.DiscardIt(username, password, caseNo);
    }

    [WebMethod(Description = "查询库存单证")]
    public int CountBalance(string username, string password)
    {
        return WebServiceClass.CountBalance(username, password);
    }

    [WebMethod(Description = "查询已消耗单证")]
    public int CountConsumed(string username, string password)
    {
        return WebServiceClass.CountConsumed(username, password);
    }

    [WebMethod(Description = "出单")]
    public PurchaseResponseEntity Purchase(PurchaseRequestEntity request)
    {
        return WebServiceClass.Purchase(request);
    }

    [WebMethod(Description = "反馈")]
    public TraceEntity Feedback(t_Feedback request)
    {
        return WebServiceClass.Feedback(request);
    }

    [WebMethod(Description = "产品列表")]
    public ProductListResponseEntity GetProductList(string username, string password)
    {
        return WebServiceClass.GetProductList(username, password);
    }
}

