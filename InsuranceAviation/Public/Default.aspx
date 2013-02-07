<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Public_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input type="hidden" name="_mvcCaptchaGuid" id="_mvcCaptchaGuid" value="a9dcd7f22c1b4e0aa5d82d7bee98d3a2" /><a
            href="javascript:_reloadMvcCaptchaImage()"><img src="/_MvcCaptcha/MvcCaptchaImage?a9dcd7f22c1b4e0aa5d82d7bee98d3a2"
                alt="MvcCaptcha" title="刷新图片" width="160" height="40" id="a9dcd7f22c1b4e0aa5d82d7bee98d3a2"
                border="0" /></a><a href="javascript:_reloadMvcCaptchaImage()">换一张</a>
        <script language="javascript" type="text/javascript">            function _reloadMvcCaptchaImage() { var ci = document.getElementById("a9dcd7f22c1b4e0aa5d82d7bee98d3a2"); var sl = ci.src.length; if (ci.src.indexOf("&") > -1) sl = ci.src.indexOf("&"); ci.src = ci.src.substr(0, sl) + "&" + (new Date().valueOf()); }</script>
    </div>
    </form>
</body>
</html>
