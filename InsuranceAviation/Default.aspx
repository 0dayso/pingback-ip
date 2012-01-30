<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::电子商务数据采集系统::</title>
    <link rel="stylesheet" href="Css/nyroModal.css" type="text/css" media="screen" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.1/jquery.js"></script>
    <script type="text/javascript" src="Script/jquery.nyroModal.custom.min.js"></script>
<script type="text/javascript">
    $(function () {
        $('.nyroModal').nyroModal(
        {
            showCloseButton: false,
            _transition: true
    });
    $('.nyroModal').click()
});
</script>
    <style type="text/css">
        body
        {
            margin: 0px;
            font-size: 14px;
            line-height: 22px;
            color: #000000;
            background: url(images/bj.jpg) #CACBCD repeat-x;
        }
        
        .input2
        {
            border: 1px solid #0C64B4;
            background-color: #B6DEF5;
            color: #FF6600;
        }
        
        .number
        {
            font-size: 20px;
            color: #D8412E;
            font-weight: bold;
            font-style: italic;
        }
        .input1
        {
            background-color: #02A3DF;
            color: #FFFFFF;
            border: 1px solid #1480CF;
        }
        .style2
        {
            width: 72%;
        }
        .style3
        {
            width: 75px;
            height: 34px;
        }
        .style4
        {
            height: 34px;
        }
        .style7
        {
            color: #413a3b;
        }
        .style8
        {
            width: 20px;
        }
        .style9
        {
            color: #51494a;
            width: 20px;
        }
        .style10
        {
            width: 20px;
            height: 20px;
        }
        .style11
        {
            height: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnLogin" defaultfocus="txtUsername">
    <table width="1000" height="610" border="0" align="center" cellpadding="0" cellspacing="0"
        background="images/login.jpg">
        <tr>
            <td valign="top">
                <table width="1000" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="281">
                        </td>
                    </tr>
                    <tr>
                        <td height="132" valign="top">
                            <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td height="131" align="left" valign="top" class="style2">
                                        <table style="width:100%;">
                                            <tr>
                                                <td class="style8">
                                                    &nbsp;</td>
                                                <td>
                                        <table border="0" cellspacing="0" cellpadding="0" align="left">
                                            <tr>
                                                <td align="right" class="style3">
                                                    &nbsp;
                                                </td>
                                                <td align="left" class="style4">
                                                    &nbsp;
                                                </td>
                                                <td width="94" align="left" class="style4">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="style7">
                                                    证件号码：
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtQuery1" runat="server" CssClass="input2"></asp:TextBox>
                                                </td>
                                                <td width="94" align="left">
                                                    <asp:Button ID="btnQuery" runat="server" CssClass="input1" Text="查询" 
                                                        OnClick="btnQuery_Click" UseSubmitBehavior="False" />
                                                </td>
                                            </tr>
                                            </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style10">
                                                    </td>
                                                <td class="style11">
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td class="style9">
                                                    &nbsp;</td>
                                                <td class="style7">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="50%" align="left" valign="top">
                                        <table width="300" border="0" cellspacing="0" cellpadding="2">
                                            <tr>
                                                <td width="69" align="right" colspan="2" style="width: 292px">
                                                    &nbsp;用户名：chenqj 
                                                    密码：chenqj（测试账号）
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="69" align="right">
                                                    用 户：
                                                </td>
                                                <td width="223" align="left">
                                                    <asp:TextBox ID="txtUsername" runat="server" CssClass="input2" Width="120px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    密 码：
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="input2" TextMode="Password"
                                                        Width="120px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    &nbsp;
                                                </td>
                                                <td align="left">
                                                    <label>
                                                        <asp:Button ID="btnLogin" runat="server" CssClass="input1" Text="登录" 
                                                        OnClick="btnLogin_Click" Width="50px" />
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="171" align="center">
                            <span style="font-size: 12px">&copy;2011 任我行网络科技有限公司</span><a href="Public/Notice.html" class="nyroModal">[公告]</a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
