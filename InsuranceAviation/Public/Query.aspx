<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Query.aspx.cs" Inherits="Public_Query" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>:::商务会员:::</title>
    <link href="../Css/Styles.css" type="text/css" rel="stylesheet">
    <style type="text/css">
<!--
body {
	background-image: url(../images/002.gif);
}
.style2 {font-size: 14px}
.style3 {
        font-size: xx-large;
        line-height: 35px;
        font-family: 黑体
}
-->
</style>
</head>
<body leftmargin="0" topmargin="0" bottommargin="0" rightmargin="0">
    <form id="form1" runat="server">
        <table width="798" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td height="84" align="right" valign="bottom">
                    <br />
                    <a href="#" onclick="history.go(-1)" style="color:White">返 回</a></td>
            </tr>
            <tr>
                <td height="581" align="center" valign="top" style="background-image: url(../images/top.jpg);
                    background-repeat: no-repeat">
                    <br />
                    <table style="height: 79px; background-color: white;" width="90%">
                        <tr>
                            <td>
                                <span class="style3">商务会员验证查询系统</span></td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <table cellpadding="0" cellspacing="0" style="width: 95%">
                        <tr>
                            <td>
                                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="View1" runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    &nbsp;身份证查询：<asp:TextBox ID="txtIdendity" runat="server" Width="238px"></asp:TextBox><asp:Button ID="btnQueryIdendity" runat="server" OnClick="btnQueryIdendity_Click"
                                                        Text="查 询" />&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblInfo" runat="server" ForeColor="Red"></asp:Label>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <br />
                                                    <textarea name="S1" style="width: 637px; height: 374px; visibility:hidden">
                                附赠保险的保险责任简介

在保险期间内，被保险人因遭受意外伤害导致身故，残疾或烧伤的，本公司按以下约定给付保险金；

    1.被保险人自意外伤害事故发生之日起180日内以该次意外伤害为直接原因身故的，或被保险人因意外伤害、自然灾害被人民法院宣告死亡的，本公司按保险单所载意外伤害保险金额给付意外身故保险金。
    2.被保险人自意外伤害事故发生之日起180日内以该次意外伤害为直接原因导致残疾的，本公司按保险单所载意外伤害保险金额及该项身体残疾所对应的给付比例给付意外残疾保险金。
    3.被保险人因意外伤害事故导致烧伤的，本公司按照保险单所载意外伤害保险金额以及《意外伤害事故烧伤保险金给付比例表》给付意外烧伤保险金。
    4.本责任中的意外残疾保险金和意外烧伤保险金给付互不冲减。
    
重要提示：本简介依据为太平洋人寿《旅游安全人身意外伤害保险条款》详细内容见原条款。理赔以原条款为准。
</textarea></td>
                                            </tr>
                                        </table>
                                        </asp:View>
                                    <asp:View ID="View2" runat="server">
                                        <table width="673" border="0" align="center">
                                            <tr align="left">
                                                <td height="39" colspan="2">
                                                    <p class="style2">
                                                        感谢您加入我们的商务会员，您的相关信息已经提交至保险公司。以下是您的个人信息，请仔细核对，如有错误请联系我们。<br />
                                                    </p>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td width="294" height="28">
                                                    &nbsp;</td>
                                                <td width="369">
                                                    <a href="002.htm">
                                                        <img src="../images/an-1.gif" width="111" height="23" border="0" id="IMG1" runat="server"
                                                            visible="false"></a></td>
                                            </tr>
                                            <tr>
                                                <td height="116" colspan="2">
                                                    <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" BackColor="LightGoldenrodYellow"
                                                        BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None"
                                                        Height="50px">
                                                        <FooterStyle BackColor="Tan" />
                                                        <EditRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                                        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="Tan" Font-Bold="True" />
                                                        <AlternatingRowStyle BackColor="PaleGoldenrod" />
                                                        <Fields>
                                                            <asp:BoundField DataField="customerName" HeaderText="姓 名" SortExpression="customerName" />
                                                            <asp:BoundField DataField="customerID" HeaderText="身份证" SortExpression="customerID" />
                                                            <asp:BoundField DataField="customerPhone" HeaderText="电 话" SortExpression="customerPhone" />
                                                            <asp:BoundField DataField="customerFlightNo" HeaderText="航班号" SortExpression="customerFlightNo" />
                                                            <asp:BoundField DataField="customerFlightDate" HeaderText="乘机时间" SortExpression="customerFlightDate" />
                                                        </Fields>
                                                    </asp:DetailsView>
                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                                        CellPadding="4" ForeColor="#333333" GridLines="None">
                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                        <Columns>
                                                            <asp:BoundField DataField="customerName" HeaderText="姓名" />
                                                            <asp:BoundField DataField="customerID" HeaderText="身份证号" />
                                                            <asp:BoundField DataField="customerPhone" HeaderText="电话" />
                                                            <asp:BoundField DataField="customerFlightNo" HeaderText="航班号" />
                                                            <asp:BoundField DataField="customerFlightDate" DataFormatString="{0:MM-dd HH:mm}" 
                                                                HeaderText="乘机时间" HtmlEncode="False" />
                                                            <asp:BoundField DataField="displayName" HeaderText="经办商户" />
                                                            <asp:TemplateField HeaderText="承保期限">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# GetCaseDuration(Eval("caseDuration"),Eval("customerFlightDate")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="caseNo" HeaderText="单证号" />
                                                            <asp:BoundField DataField="productName" HeaderText="单证类型" />
                                                            <asp:BoundField DataField="datetime" HeaderText="出单时间" />
                                                            <asp:CheckBoxField DataField="enabled" HeaderText="是否有效">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:CheckBoxField>
                                                        </Columns>
                                                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="height: 30px">
                                                    <input id="Button1" onclick="history.go(-1)" type="button" value="返 回" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:View>
                                </asp:MultiView></td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
