<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefaultCN161.aspx.cs" Inherits="DefaultCN161" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>:::商务会员:::</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <link href="css/index.css" rel="stylesheet" type="text/css">
    <style type="text/css">
<!--
.style1 {
	color: #000000;
	font-weight: bold;
}
-->
</style>
</head>
<body bgcolor="#fff7e2" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<form id="form1" runat="server">
<!-- Save for Web Slices (main_2.psd) -->
<table id="__01" width="1025" height="768" border="0" cellpadding="0" 
    cellspacing="0" align="center">
	<tr>
		<td>
			<img src="imagesCN161/main_2_01.jpg" width="310" height="133" alt=""></td>
		<td colspan="3" rowspan="2">
			<img src="imagesCN161/main_2_02.jpg" width="714" height="265" alt=""></td>
		<td>
			<img src="imagesCN161/spacer.gif" width="1" height="133" alt=""></td>
	</tr>
	<tr>
		<td rowspan="2" height="480" 
            style="background-image: url('imagesCN161/main_2_03.jpg')" valign="top">
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
                                <img src="images/check.gif"></td>
                            <td align="left" style="height: 21px">
                                <a href="Public/Query.aspx">公众防伪查询</a></td>
                            <td align="left" style="height: 21px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 70px; height: 21px;">
                                <img src="images/print.gif"></td>
                            <td align="left" style="height: 21px">
                                <a href="setupia.exe">下载打印程序</a></td>
                            <td align="left" style="height: 21px">
                                &nbsp;</td>
                        </tr>
                       <tr>
                            <td align="right" style="width: 70px; height: 21px;">
                                <img src="images/print.gif"></td>
                            <td align="left" style="height: 21px">
                                <a href="dotnetfx2.exe">插件下载</a>（如果无法运行打印程序，请运行该插件）
</td>
                            <td align="left" style="height: 21px">
                                &nbsp;</td>
                        </tr>
                       </table></td>
		<td>
			<img src="imagesCN161/spacer.gif" width="1" height="132" alt=""></td>
	</tr>
	<tr>
		<td colspan="2" rowspan="2">
			<img src="imagesCN161/main_2_04.jpg" width="616" height="349" alt=""></td>
		<td rowspan="3">
			<img src="imagesCN161/main_2_05.jpg" width="98" height="503" alt=""></td>
		<td>
			<img src="imagesCN161/spacer.gif" width="1" height="348" alt=""></td>
	</tr>
	<tr>
		<td rowspan="2">
			<img src="imagesCN161/main_2_06.jpg" width="310" height="155" alt=""></td>
		<td>
			<img src="imagesCN161/spacer.gif" width="1" height="1" alt=""></td>
	</tr>
	<tr>
		<td>
			<img src="imagesCN161/main_2_07.jpg" width="291" height="154" alt=""></td>
		<td>
			<img src="imagesCN161/main_2_08.jpg" width="325" height="154" alt=""></td>
		<td>
			<img src="imagesCN161/spacer.gif" width="1" height="154" alt=""></td>
	</tr>
</table>
<!-- End Save for Web Slices -->
</form>
</body>
</html>
