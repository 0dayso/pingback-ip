<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefaultHLLogin.aspx.cs" Inherits="DefaultHLLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/index.css" rel="stylesheet" type="text/css">
</head>
<body>
    <form id="form1" runat="server">
    <table style="width:100%;">
        <tr>
            <td>
                <img alt="" src="imagesHL/logo.gif" style="width: 250px; height: 98px" /></td>
            <td align="right" valign="top">
                <a href="#" onclick="window.parent.hidePopWin(false)">关闭</a></td>
        </tr>
    </table>
<table border="0" cellpadding="3">
                        <tr>
                            <td align="right" style="width: 70px; height: 20px;">
                                <span class="style1">用户名：</span></td>
                            <td align="left" style="height: 20px">
                                <asp:TextBox ID="txtUsername" runat="server" BorderStyle="Groove" Width="100px" AutoCompleteType="DisplayName"
                                    CssClass="button"></asp:TextBox></td>
                            <td align="left" style="height: 20px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername"
                                    ErrorMessage="请输入用户名！"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 70px; height: 18px;">
                                <span class="style1">密 码：</span></td>
                            <td align="left" style="height: 18px">
                                <asp:TextBox ID="txtPassword" runat="server" BorderStyle="Groove" TextMode="Password"
                                    Width="100px" CssClass="button"></asp:TextBox></td>
                            <td align="left" style="height: 18px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                    ErrorMessage="请输入密码！"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 70px; height: 20px;">
                            </td>
                            <td align="left" style="height: 20px">
                                <asp:CheckBox ID="chkPersistCookie" runat="server" Text="保持登录状态" Checked="True" /></td>
                            <td align="left" style="height: 20px">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 70px; height: 21px;">
                                &nbsp;</td>
                            <td align="left" style="height: 21px">
                                <asp:ImageButton ID="btnLogin" runat="server" ImageUrl="~/images/001.gif" OnClick="btnLogin_Click" />
                                </td>
                            <td align="left" style="height: 21px">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 70px; height: 21px;">
                                <img alt="" src="images/print.gif" /></td>
                            <td align="left" style="height: 21px">
                                <a href="setupIA.exe">下载打印程序</a></td>
                            <td align="left" style="height: 21px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 70px; height: 21px;">
                                <img alt="" src="images/info.gif" /></td>
                            <td align="left" style="height: 21px">
                                <a href="dotnetfx2.exe">插件下载</a>（如果无法运行打印程序，请运行该插件）</td>
                            <td align="left" style="height: 21px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 70px; height: 21px;">
                                <img alt="" src="images/check.gif" /></td>
                            <td align="left" style="height: 21px">
                                <a href="任我行出单系统说明书.pdf">使用说明书</a></td>
                            <td align="left" style="height: 21px">
                                &nbsp;</td>
                        </tr>
                    </table>
    </form>
</body>
</html>
