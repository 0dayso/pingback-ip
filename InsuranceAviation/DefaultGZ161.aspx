<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefaultGZ161.aspx.cs" Inherits="DefaultGZ161" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>:::惠盟之旅:::</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <link href="css/index.css" rel="stylesheet" type="text/css">
    <link rel="stylesheet" type="text/css" href="css/forModal.css" />
    <style type="text/css">
<!--
    img
    {
    	border:none;
    }
        .style2
        {
            width: 70px;
            height: 20px;
            color: #000000;
            font-weight: bold;
        }
        .style3
        {
            width: 70px;
            height: 18px;
            color: #000000;
            font-weight: bold;
        }
-->
</style>
</head>
<body bgcolor="#FFFFFF" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<form id="form1" runat="server" defaultbutton="btnLogin">
<!-- Save for Web Slices (main_2.psd) -->
<table id="Table_01" width="971" height="751" border="0" cellpadding="0" cellspacing="0">
	<tr>
		<td colspan="9">
			<img src="imagesGZ161/main_2_01.gif" width="970" height="38" alt=""></td>
		<td>
			<img src="imagesGZ161/spacer.gif" width="1" height="38" alt=""></td>
	</tr>
	<tr>
		<td>
			<img src="imagesGZ161/main_2_02.gif" width="49" height="29" alt=""></td>
		<td>
			<img src="imagesGZ161/main_2_03.gif" width="261" height="29" alt=""></td>
		<td>
			<a href="htmlShouye.htm"><img src="imagesGZ161/main_2_04.gif" width="134" height="29" alt=""></a></td>
		<td>
			<a href="#"><img src="imagesGZ161/main_2_05.gif" width="114" height="29" alt="酒店预订"></a></td>
		<td>
			<a href="#" onclick="showPopWin('indexStuff/slgl.htm', 820, 500, null);"><img src="imagesGZ161/main_2_06.gif" width="121" height="29" alt="商旅管理"></a></td>
		<td colspan="2">
			<a href="#" onclick="showPopWin('indexStuff/download.htm', 820, 300, null);"><img src="imagesGZ161/main_2_07.gif" width="118" height="29" alt="打印软件下载"></a></td>
		<td colspan="2">
			<a href="#" onclick="showPopWin('indexStuff/about.htm', 820, 250, null);"><img src="imagesGZ161/main_2_08.gif" width="173" height="29" alt="关于我们"></a></td>
		<td>
			<img src="imagesGZ161/spacer.gif" width="1" height="29" alt=""></td>
	</tr>
	<tr>
		<td colspan="9">
			<img src="imagesGZ161/main_2_09.jpg" width="970" height="388" alt=""></td>
		<td>
			<img src="imagesGZ161/spacer.gif" width="1" height="388" alt=""></td>
	</tr>
	<tr>
		<td>
			<img src="imagesGZ161/main_2_10.gif" width="49" height="33" alt=""></td>
		<td rowspan="3" valign="top">
			<strong>
            <br />
            广州白云国际机场航班延误定点服务机构：</strong><br />
            卡纷休闲会所（航站楼主楼C8003）<br />
            红树园咖啡（航站楼二楼）<br />
            金龙酒家（航站楼首层）</td>
		<td colspan="3" rowspan="2" height="92" width="369">
			身份证号码：<asp:TextBox ID="txtQueryCustomerID" runat="server" Width="180px"></asp:TextBox>
            <asp:Button ID="btnQuery" runat="server" CausesValidation="False" 
                onclick="btnQuery_Click" Text="查询" TabIndex="9" />
        </td>
		<td colspan="4">
			<img src="imagesGZ161/main_2_13.gif" width="291" height="33" alt=""></td>
		<td>
			<img src="imagesGZ161/spacer.gif" width="1" height="33" alt=""></td>
	</tr>
	<tr>
		<td rowspan="2">
			<img src="imagesGZ161/main_2_14.gif" width="49" height="141" alt=""></td>
		<td rowspan="2">
			<img src="imagesGZ161/main_2_15.gif" width="69" height="141" alt=""></td>
		<td colspan="2" rowspan="2" height="141" width="162">
			<table border="0" cellpadding="3">
                        <tr>
                            <td align="right" class="style2">
                                用户名：</td>
                            <td align="left" style="height: 20px">
                                <asp:TextBox ID="txtUsername" runat="server" BorderStyle="Groove" Width="80px" AutoCompleteType="DisplayName"
                                    CssClass="button"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername"
                                    ErrorMessage="不能为空！" Display="Dynamic"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="right" class="style3">
                                                                密&nbsp; 码：</td>
                            <td align="left" style="height: 18px">
                                <asp:TextBox ID="txtPassword" runat="server" BorderStyle="Groove" TextMode="Password"
                                    Width="80px" CssClass="button"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                    ErrorMessage="不能为空！" Display="Dynamic"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 70px; height: 20px;">
                                <asp:CheckBox ID="chkPersistCookie" runat="server" Text="记住我" Checked="True" />
                            </td>
                            <td align="left" style="height: 20px">
                                <asp:ImageButton ID="btnLogin" runat="server" ImageUrl="~/images/001.gif" OnClick="btnLogin_Click" />
                                </td>
                        </tr>
                        </table></td>
		<td rowspan="2">
			<img src="imagesGZ161/main_2_17.gif" width="60" height="141" alt=""></td>
		<td>
			<img src="imagesGZ161/spacer.gif" width="1" height="59" alt=""></td>
	</tr>
	<tr>
		<td colspan="3">
			<img src="imagesGZ161/main_2_18.gif" width="369" height="82" alt=""></td>
		<td>
			<img src="imagesGZ161/spacer.gif" width="1" height="82" alt=""></td>
	</tr>
	<tr>
		<td colspan="9">
			<img src="imagesGZ161/main_2_19.gif" width="970" height="121" alt=""></td>
		<td>
			<img src="imagesGZ161/spacer.gif" width="1" height="121" alt=""></td>
	</tr>
	<tr>
		<td>
			<img src="imagesGZ161/spacer.gif" width="49" height="1" alt=""></td>
		<td>
			<img src="imagesGZ161/spacer.gif" width="261" height="1" alt=""></td>
		<td>
			<img src="imagesGZ161/spacer.gif" width="134" height="1" alt=""></td>
		<td>
			<img src="imagesGZ161/spacer.gif" width="114" height="1" alt=""></td>
		<td>
			<img src="imagesGZ161/spacer.gif" width="121" height="1" alt=""></td>
		<td>
			<img src="imagesGZ161/spacer.gif" width="69" height="1" alt=""></td>
		<td>
			<img src="imagesGZ161/spacer.gif" width="49" height="1" alt=""></td>
		<td>
			<img src="imagesGZ161/spacer.gif" width="113" height="1" alt=""></td>
		<td>
			<img src="imagesGZ161/spacer.gif" width="60" height="1" alt=""></td>
		<td></td>
	</tr>
</table>
<!-- End Save for Web Slices -->
</form>
        <script type="text/javascript" src="Script/common.js"></script>

        <script type="text/javascript" src="Script/forModal.js"></script>
</body>
</html>
