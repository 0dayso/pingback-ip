<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="passwordChange.aspx.cs" Inherits="passwordChange" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>:::商务会员:::</title>
    <link href="../Css/Styles.css" type="text/css" rel="stylesheet">
</head>
<body leftmargin="0" topmargin="0">
    <form id="form1" runat="server">
        <table cellspacing="0" cellpadding="0" width="100%" align="left" border="0">
            <tbody>
                <tr>
                    <td valign="top" style="height: 50px">
                        
                        
                    </td>
                </tr>
                <tr>
                    <td class="bodyText" valign="top">
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td style="padding-left: 0px; width: 3%" valign="top">
                                    </td>
                                    <td valign="top">
                                        <div style="font-weight: bold; font-size: 1.5em" nowrap>
                                            密码修改
                                        </div>
                                        <br>
                                        <table style="width: 397px; height: 134px">
                                            <tr>
                                                <td align="right">
                                                    旧密码</td>
                                                <td style="width: 142px">
                                                    <asp:TextBox ID="txtPassOld" runat="server" TextMode="Password"></asp:TextBox></td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入密码！"
                                                        ControlToValidate="txtPassOld"></asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    新密码</td>
                                                <td style="width: 142px">
                                                    <asp:TextBox ID="txtPassNew1" runat="server" TextMode="Password"></asp:TextBox></td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassNew1"
                                                        ErrorMessage="请输入密码！"></asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    再次输入</td>
                                                <td align="right" style="width: 142px">
                                                    <asp:TextBox ID="txtPassNew2" runat="server" TextMode="Password"></asp:TextBox></td>
                                                <td>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="两次输入不一致！"
                                                        ControlToCompare="txtPassNew1" ControlToValidate="txtPassNew2"></asp:CompareValidator></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td style="width: 142px" align="right">
                                                    <asp:Button ID="btnOk" runat="server" Text="确 定" OnClick="btnOk_Click" /></td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>

        <script language="javascript">
<!--
function __keyPress(event, href) {
    var keyCode;
    if (typeof(event.keyCode) != "undefined") {
        keyCode = event.keyCode;
    }
    else {
        keyCode = event.which;
    }

    if (keyCode == 13) {
        window.location = href;
    }
}
//-->
        </script>

    </form>
</body>
</html>