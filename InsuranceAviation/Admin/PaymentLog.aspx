<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentLog.aspx.cs" Inherits="Admin_PaymentLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Css/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
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
                                        客户充值记录
                                    </div>
                                    <br>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="sdsList"
                                        ForeColor="Black" GridLines="Vertical" AllowPaging="True" 
                                        EnableViewState="False" PageSize="15">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="date_created" HeaderText="创建时间" SortExpression="date_created" />
                                            <asp:BoundField DataField="amount" DataFormatString="{0:c}" HeaderText="金额" SortExpression="amount" />
                                            <asp:BoundField DataField="payer" HeaderText="付款用户" SortExpression="payer" />
                                            <asp:BoundField DataField="payer_account" HeaderText="支付账号" SortExpression="payer_account" />
                                            <asp:BoundField DataField="payer_tradeNo" HeaderText="支付交易号" SortExpression="payer_tradeNo" />
                                            <asp:BoundField DataField="date_Payed" HeaderText="支付时间" SortExpression="date_Payed" />
                                            <asp:TemplateField HeaderText="状态" SortExpression="status">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Common.GetPayStatus(Eval("status")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#CCCC99" />
                                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                        <RowStyle BackColor="#F7F7DE" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                        <SortedAscendingHeaderStyle BackColor="#848384" />
                                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                        <SortedDescendingHeaderStyle BackColor="#575357" />
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="sdsList" runat="server" ConnectionString="<%$ ConnectionStrings:InsuranceAviation %>"
                                        SelectCommand="SELECT amount, payer, payer_account, payer_tradeNo, status, date_created, date_Payed FROM t_Payment ORDER BY date_created DESC">
                                    </asp:SqlDataSource>
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
