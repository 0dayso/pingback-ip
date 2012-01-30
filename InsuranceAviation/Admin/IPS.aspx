<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IPS.aspx.cs" Inherits="Admin_IPS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Css/Styles.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/forModal.css" />
    <script type="text/javascript" src="../Script/common.js"></script>
    <script type="text/javascript" src="../Script/forModal.js"></script>
    <script type="text/javascript">
        function Pay() {
            var amount = document.getElementById("txtAmount").value;
            showPopWin("PayStep1.aspx?a=" + amount, 500, 400, null);
        }
    </script>
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
                                        在线充值
                                    </div>
                                    <br>
                                    <table>
                                        <tr>
                                            <td align="right" valign="bottom">
                                                当前余额：</td>
                                            <td valign="bottom">
                                                ￥<asp:Label ID="lblBalance" 
                                                    runat="server"></asp:Label>
                                            </td>
                                            <td valign="bottom">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="bottom">
                                                &nbsp;</td>
                                            <td valign="bottom">
                                                &nbsp;</td>
                                            <td valign="bottom">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="bottom">
                                                充值金额：
                                            </td>
                                            <td valign="bottom">
                                                ￥<asp:TextBox ID="txtAmount" runat="server" Width="80px"></asp:TextBox>元
                                            </td>
                                            <td valign="bottom">
                                                <input id="Button1" type="button" value="支 付" onclick="Pay()" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
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
    </form>
</body>
</html>
