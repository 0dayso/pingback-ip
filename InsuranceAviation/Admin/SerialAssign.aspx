<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="SerialAssign.aspx.cs"
    Inherits="Admin_SerialAssign" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>:::商务会员:::</title>
    <style type="text/css">
        .panelNormal
        {
            cursor:pointer;
            text-decoration: underline;
        }
        .panelUpdating
        {
            background-image: url("../images/spinner03.gif");
            background-repeat: no-repeat;
            width:16px;
            height:16px;
        }
    </style>
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
                        SelectCommand="SELECT username, displayname, phone FROM t_User with(nolock) WHERE (parent = @parent) AND (usertype &lt;&gt; 99) AND (username LIKE '%' + @keyword + '%') OR (parent = @parent) AND (usertype &lt;&gt; 99) AND (displayname LIKE '%' + @keyword + '%') ORDER BY datetime DESC">
                        <SelectParameters>
                            <asp:Parameter Name="parent" Type="String" />
                            <asp:ControlParameter ControlID="txtKeyword" ConvertEmptyStringToNull="False" Name="keyword"
                                PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                    </ajaxToolkit:ToolkitScriptManager>
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
                                        单证号分配
                                    </div>
                                    <br />
                                    模糊搜索<asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox><asp:Button ID="btnSearch"
                                        runat="server" Text="搜索" OnClick="btnSearch_Click" /><br />
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                DataKeyNames="username" DataSourceID="SqlDataSource1" BackColor="White" BorderColor="#DEDFDE"
                                                BorderStyle="Solid" BorderWidth="1px" CellPadding="4" ForeColor="Black" 
                                                GridLines="Vertical">
                                                <Columns>
                                                    <asp:BoundField DataField="username" HeaderText="用户名" ReadOnly="True" SortExpression="username" />
                                                    <asp:BoundField DataField="displayname" HeaderText="单位名称" SortExpression="displayname" />
                                                    <asp:BoundField DataField="phone" HeaderText="联系电话" SortExpression="phone" />
                                                    <asp:TemplateField HeaderText="库存单证">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# GetCount(Eval("username")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="已消耗">
                                                        <ItemTemplate>
                                                            <asp:Panel ID="pnlConsumed" runat="server" CssClass="panelNormal">
                                                                <asp:Label ID="lblConsumed" runat="server">查询</asp:Label>
                                                                <ajaxToolkit:DynamicPopulateExtender ID="lblConsumed_DynamicPopulateExtender" runat="server"
                                                                    ContextKey='<%# Eval("username") %>' PopulateTriggerControlID="lblConsumed" ServiceMethod="GetConsumed"
                                                                    TargetControlID="pnlConsumed" UpdatingCssClass="panelUpdating">
                                                                </ajaxToolkit:DynamicPopulateExtender>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="作废">
                                                        <ItemTemplate>
                                                            <asp:Panel ID="pnlDiscarded" runat="server" CssClass="panelNormal">
                                                                <asp:Label ID="lblDiscarded" runat="server">查询</asp:Label>
                                                                <ajaxToolkit:DynamicPopulateExtender ID="lblDiscarded_DynamicPopulateExtender" runat="server"
                                                                    ContextKey='<%# Eval("username") %>' PopulateTriggerControlID="lblDiscarded" ServiceMethod="GetDiscarded"
                                                                    TargetControlID="pnlDiscarded" UpdatingCssClass="panelUpdating">
                                                                </ajaxToolkit:DynamicPopulateExtender>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:HyperLinkField Text="分配单证号" DataNavigateUrlFields="username" DataNavigateUrlFormatString="SerialAssignStep2.aspx?id={0}" />
                                                    <asp:HyperLinkField Text="收回单证号" DataNavigateUrlFields="username" DataNavigateUrlFormatString="SerialWithdrawal.aspx?id={0}" />
                                                </Columns>
                                                <FooterStyle BackColor="#CCCC99" />
                                                <RowStyle BackColor="#F7F7DE" />
                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                <AlternatingRowStyle BackColor="White" />
                                                <PagerSettings Mode="NumericFirstLast" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
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
            if (typeof (event.keyCode) != "undefined") {
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
