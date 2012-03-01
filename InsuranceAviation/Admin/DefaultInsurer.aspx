<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefaultInsurer.aspx.cs" Inherits="Admin_DefaultInsurer" %>

<%@ Register Assembly="IAClass" Namespace="MyWebControl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>:::商务会员:::</title>
    <style type="text/css">
        .panelNormal
        {
            cursor: pointer;
            text-decoration: underline;
        }
        .panelUpdating
        {
            background-image: url("../images/spinner00.gif");
            background-repeat: no-repeat;
            width: 32px;
            height: 32px;
        }
    </style>
    <link href="../Css/Styles.css" type="text/css" rel="stylesheet">

    <link rel="stylesheet" type="text/css" href="../css/jscal2.css" />
    <script type="text/javascript" src="../script/jscal2.js"></script>
    <script type="text/javascript" src="../script/cn.js"></script>
    <script language="Javascript" src="../Script/FusionCharts.js"></script>
    
    <script type="text/javascript" src="../Script/jquery.min.js"></script>

    <script type="text/javascript" src="../Script/jquery.timeago.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("abbr.timeago").timeago();
        });
    </script>
</head>
<body leftmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" align="left" border="0">
        <tbody>
            <tr>
                <td valign="top" style="height: 50px">
                    
                    
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:InsuranceAviation %>"
                        DeleteCommand="select 1" OnSelected="SqlDataSource1_Selected" SelectCommand="PagedCaseListForInsurer"
                        SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="pn1" Name="pageNum" PropertyName="SelectedPage" />
                            <asp:Parameter DefaultValue="10" Name="pageSize" Type="Int32" />
                            <asp:Parameter Direction="ReturnValue" Name="pageCount" Type="Int32" />
                            <asp:Parameter Name="dateStart" Type="DateTime" DefaultValue="1900-1-1" />
                            <asp:Parameter Name="dateEnd" Type="DateTime" DefaultValue="2099-1-1" />
                            <asp:Parameter Name="insurer" Type="String" />
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
                                <td style="padding-left: 0px; width: 225px" valign="top">
                                    <div style="font-weight: bold; font-size: 1.5em" nowrap>
                                        欢迎使用
                                    </div>
                                </td>
                                <td valign="top" rowspan="2">
                                    <table>
                                        <tr>
                                            <td style="height: 150px">
                                                <%=TopEveryday()%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                    BorderColor="#DEDFDE" BorderStyle="Solid" BorderWidth="1px" 
                                                    CellPadding="4" ForeColor="Black"
                                                    GridLines="Vertical" DataKeyNames="caseNo" OnRowDataBound="GridView1_RowDataBound"
                                                    EnableViewState="False" DataSourceID="SqlDataSource1">
                                                    <FooterStyle BackColor="#CCCC99" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="是否作废" Visible="False">
                                                            <ItemTemplate>
                                                                <%# GetCaseStatus(Eval("enabled")) %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="提交时间">
                                                            <ItemTemplate>
                                                                <abbr class="timeago" title="<%# Eval("datetime", "{0:yyyy-MM-dd HH:mm:ss}") %>">
                                                                    <%# Eval("datetime", "{0:yyyy-MM-dd HH:mm:ss}") %>
                                                                </abbr>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="userGroup" HeaderText="地区" />
                                                        <asp:TemplateField HeaderText="单证号">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("caseNo") %>' ForeColor='<%# DiscardedColor(Eval("enabled")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="customerName" HeaderText="姓名" />
                                                        <asp:BoundField DataField="customerID" HeaderText="证件号码" />
                                                        <asp:TemplateField HeaderText="出生日期" Visible="False">
                                                            <ItemTemplate>
                                                                <%# Eval("customerBirth", "{0:yyyy-MM-dd}")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="customerGender" HeaderText="性别" />
                                                        <asp:BoundField DataField="customerPhone" HeaderText="电话" />
                                                        <asp:BoundField DataField="customerFlightNo" HeaderText="航班号" />
                                                        <asp:BoundField DataField="customerFlightDate" HeaderText="乘机时间" DataFormatString="{0:yyyy-MM-dd HH:mm}"
                                                            HtmlEncode="False" />
                                                    </Columns>
                                                    <RowStyle BackColor="#F7F7DE" />
                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <cc1:Pager ID="pn1" runat="server" CssClass="PageNumbers" DisplayedPages="10" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="导 出" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 0px; width: 3%" valign="top">
                                </td>
                                <td style="padding-left: 0px; width: 225px;" valign="top">
                                    <b>登录用户 : </b>
                                    <asp:Label ID="lblUsername" runat="server"></asp:Label><br>
                                    <b>用户身份 : </b>保险公司
                                    <asp:Panel ID="pnlConsumed" runat="server" CssClass="panelNormal">
                                        <asp:Image ID="imgConsumed" runat="server" ImageUrl="~/images/info.gif" />
                                        <ajaxToolkit:DynamicPopulateExtender ID="lblConsumed_DynamicPopulateExtender" runat="server"
                                            PopulateTriggerControlID="pnlConsumed" ServiceMethod="GetConsumed"
                                            TargetControlID="pnlConsumed" UpdatingCssClass="panelUpdating">
                                        </ajaxToolkit:DynamicPopulateExtender>
                                    </asp:Panel>
                                    <br />
                                    <br />
                                    <table width="100%">
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                开始日期：<asp:TextBox ID="txtDateStart" runat="server" Width="80px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                截止日期：<asp:TextBox ID="txtDateEnd" runat="server" Width="80px"></asp:TextBox>
                                                <script type="text/javascript">
                                                    Calendar.setup({
                                                        trigger: "txtDateStart",
                                                        inputField: "txtDateStart",
                                                        onSelect: function () { this.hide() }
                                                    });
                                                    Calendar.setup({
                                                        trigger: "txtDateEnd",
                                                        inputField: "txtDateEnd",
                                                        onSelect: function () { this.hide() }
                                                    });
                                                        </script>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查 询" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
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
