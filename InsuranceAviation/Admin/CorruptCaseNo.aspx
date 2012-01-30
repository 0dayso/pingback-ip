<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CorruptCaseNo.aspx.cs" Inherits="Admin_CorruptCaseNo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>库存的号段</title>
    <META HTTP-EQUIV="CACHE-CONTROL" CONTENT="NO-CACHE">
    <link href="../css/Styles.css" type="text/css" rel="stylesheet">
</head>
<body leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <div>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:InsuranceAviation %>"
            SelectCommand="GetCorruptCaseNo" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter Name="caseOwner" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        </div>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4" DataSourceID="SqlDataSource2"
            ForeColor="Black" GridLines="Vertical" Width="100%" 
        EnableModelValidation="True">
            <FooterStyle BackColor="#CCCC99" />
            <Columns>
                <asp:BoundField DataField="displayName" HeaderText="产品来源" />
                <asp:BoundField DataField="startNo" HeaderText="起始单证号" ReadOnly="True" SortExpression="startNo" />
                <asp:BoundField DataField="endNo" HeaderText="结束单证号" ReadOnly="True" SortExpression="endNo" />
                <asp:BoundField DataField="number" HeaderText="数量" SortExpression="number" />
            </Columns>
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    </form>
</body>
</html>
