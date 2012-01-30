<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CaseMng.aspx.cs" Inherits="Aviation_CaseMng" %>
<%@ Register Assembly="IAClass" Namespace="MyWebControl" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>:::商务会员:::</title>
    <link href="../Css/Styles.css" type="text/css" rel="stylesheet">
    <script>
        function FindHighLight(nWord) {
            if (nWord != '') {
                var keyword = document.body.createTextRange();
                while (keyword.findText(nWord)) {
                    keyword.pasteHTML("<span style='background:yellow'>" + keyword.text + "</span>");
                    keyword.moveStart('character', 1);
                }
            }
        }
        function HighLight(nWord) {
            var array = nWord.split(",");
            for (var i = 0; i < array.length; i++) {
                FindHighLight(array[i]);
            }
        } 
    </script>
</head>
<body leftmargin="0" topmargin="0" bottommargin="0" rightmargin="0" onload="HighLight('<%= this.txtKeyword.Text.Trim() %>')">
    <form id="form1" runat="server">
        <table cellspacing="0" cellpadding="0" width="100%" align="left" border="0">
            <tbody>
                <tr>
                    <td valign="top" style="height: 50px">
                        
                        
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:InsuranceAviation %>"
                            SelectCommand="PagedCaseListForSelf" SelectCommandType="StoredProcedure" OnSelected="SqlDataSource1_Selected" DeleteCommand="select 1">
                            <SelectParameters>
                                <asp:ControlParameter Name="pageNum" ControlID="pn1" PropertyName="SelectedPage" />
                                <asp:Parameter Name="pageSize" Type="Int32" DefaultValue="10" />
                                <asp:Parameter Name="caseOwner" Type="String" />
                                <asp:Parameter Name="pageCount" Direction="ReturnValue" Type="Int32" />
                                <asp:ControlParameter ControlID="txtKeyword" ConvertEmptyStringToNull="False" 
                                    Name="keyword" PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
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
                                            单证作废
                                        </div>
                                        <br />
                                        模糊查询：<asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnSearch" runat="server" onclick="btnSearch_Click" Text="搜索" />
                                        <br />
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                            DataKeyNames="caseNo" DataSourceID="SqlDataSource1" BackColor="White" 
                                            BorderColor="#DEDFDE" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                            ForeColor="Black" GridLines="Vertical" OnRowDeleting="GridView1_RowDeleting" 
                                            EnableViewState="False">
                                            <Columns>
                                                <asp:BoundField DataField="productName" HeaderText="单证类型" SortExpression="productName" />
                                                <asp:BoundField DataField="caseNo" HeaderText="单证号" ReadOnly="True" SortExpression="caseNo" />
                                                <asp:BoundField DataField="customerName" HeaderText="姓名" SortExpression="customerName" />
                                                <asp:BoundField DataField="customerID" HeaderText="身份证号" SortExpression="customerID" />
                                                <asp:BoundField DataField="customerPhone" HeaderText="电话" SortExpression="customerPhone" />
                                                <asp:BoundField DataField="customerFlightNo" HeaderText="航班号" SortExpression="customerFlightNo" />
                                                <asp:BoundField DataField="customerFlightDate" HeaderText="乘机时间" SortExpression="customerFlightDate" DataFormatString="{0:MM-dd HH:mm}" HtmlEncode="False"/>
                                                <asp:TemplateField HeaderText="作废">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="Delete"
                                                            ImageUrl='<%# GetImagePath(Eval("enabled")) %>' Enabled='<%# Eval("enabled") %>' OnClientClick="return confirm('确定要作废该单证？');" ToolTip="作废单证" Visible='<%# IsOver(Eval("customerFlightDate")) %>'/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" />
                                            <RowStyle BackColor="#F7F7DE" />
                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <PagerSettings Mode="NumericFirstLast" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 0px; width: 3%" valign="top">
                                    </td>
                                    <td valign="top">
                                    <cc1:Pager ID="pn1" runat="server" CssClass="PageNumbers" DisplayedPages="7" />
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