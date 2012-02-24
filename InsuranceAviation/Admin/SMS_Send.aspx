<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SMS_Send.aspx.cs" Inherits="Admin_SMS_Send" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../Css/nyroModal.css" type="text/css" media="screen" />
    <link href="../Css/Styles.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Script/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            window.attachEvent('onbeforeunload', function () { $("#btnSend").val("请等待...").attr("disabled", "true"); });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table>
            <tr>
                <td>
                    单　号：</td>
                <td>
                    <asp:TextBox ID="txtCaseNo" runat="server" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    手机号：</td>
                <td>
                    <asp:TextBox ID="txtMobile" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtMobile" Display="Dynamic" 
                        ErrorMessage="请输入手机号码！" 
                        ValidationExpression="^1[3458][0-9]{9}$" CssClass="redWarning"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtMobile" CssClass="redWarning" Display="Dynamic" 
                        ErrorMessage="手机号码不能为空！"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    短信内容：</td>
                <td>
                    <asp:TextBox ID="txtContent" runat="server" Height="105px" TextMode="MultiLine" 
                        Width="290px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnSend" runat="server" onclick="btnSend_Click" Text="发送" />
&nbsp;<input id="Button1" type="button" value="关闭" onclick="parent.$.nmTop().close(); return false;" /></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="lblResult" runat="server" CssClass="redWarning"></asp:Label>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
