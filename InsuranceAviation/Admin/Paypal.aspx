<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Paypal.aspx.cs" Inherits="Admin_Paypal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Css/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" action="https://www.paypal.com/cgi-bin/webscr">
        <div nowrap style="font-weight: bold; font-size: 1.5em;margin-top:70px">
            账户充值
        </div>
    <div style="margin-top: 20px">
        <input type="hidden" name="cmd" value="_xclick">
        <input type="hidden" name="business" value="2140361@qq.com">
        <%--指完成付款后客户的浏览器返回到的 URL--%>
        <input type="hidden" name="return" value="http://localhost/InsuranceAviation/public/PaypalCallback.aspx">
        <%--如果为 2 并设置了 return，则客户的浏览器由 POST 方法返回至返回URL，同时将所有可用交易变量发送至该 URL--%>
        <input type="hidden" name="rm" value="2">
        <%--付款货币:人民币--%>
        <input type="hidden" name="currency_code" value="CNY">
        <input type="hidden" name="image_url" value="http://www.renwox.net/images/branding_Full2.gif">
        <input type="hidden" name="cancel_return" value="http://localhost/InsuranceAviation/public/PaypalCancel.aspx">
        <input type="hidden" name="custom" value="">
        <input type="hidden" name="charset" value="utf-8">
        <input type="hidden" name="item_name" value="惠旅充值">
        <input type="hidden" name="no_shipping" value="1">
        充值金额:￥<input type="text" name="amount" value="">
        元
        <input id="btnPay" type="submit" value="支付" />
    </div>
    </form>
</body>
</html>
