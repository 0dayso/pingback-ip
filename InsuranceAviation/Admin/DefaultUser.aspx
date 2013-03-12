<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefaultUser.aspx.cs" Inherits="DefaultUser" %>

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
            background-image: url("../images/spinner03.gif");
            background-repeat: no-repeat;
            width: 16px;
            height: 16px;
        }
        .updating
        {   /*图表Loading*/
            background-image: url("../images/spinner00.gif");
            background-repeat: no-repeat;
            width: 32px;
            height: 32px;
        }
    </style>
    <link href="../Css/Styles.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../css/jscal2.css" />
    <script type="text/javascript" src="../script/jscal2.js"></script>
    <script type="text/javascript" src="../script/cn.js"></script>
    <script type="text/javascript" src="../Script/jquery.min.js"></script>
    <script type="text/javascript" src="../Script/jquery.timeago.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            timeAgo();
            $.get("ajax.aspx?t=" + new Date(), { func: "FlashTopEverydayUser", dateStart: $get("txtDateStart").value }, callbackTop1);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(timeAgo);
        });

        function callbackTop1(result) {
            var div = $get("TopEverydayParent");
            div.className = "";
            div.innerHTML = result;
        }

        function timeAgo() {
            $("abbr.timeago").timeago();
        }
    </script>
</head>
<body leftmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" align="left" border="0">
        <tbody>
            <tr>
                <td valign="top" style="height: 50px">
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:InsuranceAviation %>"
                        DeleteCommand="select 1" OnSelected="SqlDataSource1_Selected" SelectCommand="PagedCaseListWithChild"
                        SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="pn1" Name="pageNum" PropertyName="SelectedPage" />
                            <asp:Parameter DefaultValue="10" Name="pageSize" Type="Int32" />
                            <asp:Parameter Direction="ReturnValue" Name="pageCount" Type="Int32" />
                            <asp:Parameter Name="dateStart" Type="DateTime" DefaultValue="1900-1-1" />
                            <asp:Parameter Name="dateEnd" Type="DateTime" DefaultValue="2099-1-1" />
                            <asp:Parameter Name="caseOwner" Type="String" />
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
                                                <div id="TopEverydayParent" class="updating">
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="Solid" BorderWidth="1px" 
                                                            CellPadding="4" DataKeyNames="caseNo" DataSourceID="SqlDataSource1" 
                                                            EnableViewState="False" ForeColor="Black" GridLines="Vertical" 
                                                            OnRowDataBound="GridView1_RowDataBound">
                                                            <FooterStyle BackColor="#CCCC99" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="是否作废" Visible="False">
                                                                    <ItemTemplate>
                                                                <%# GetCaseStatus(Eval("enabled")) %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="提交时间">
                                                                    <ItemTemplate>
                                                                        <abbr class="timeago" 
                                                                            title='<%# Eval("datetime", "{0:yyyy-MM-dd HH:mm:ss}") %>'>
                                                                    <%# Eval("datetime", "{0:yyyy-MM-dd HH:mm:ss}") %>
                                                                        </abbr>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="displayname" HeaderText="加盟商户" />
                                                                <asp:TemplateField HeaderText="单证号">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" 
                                                                            ForeColor='<%# DiscardedColor(Eval("enabled")) %>' Text='<%# Eval("caseNo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="customerName" HeaderText="姓名" />
                                                                <asp:BoundField DataField="customerGender" HeaderText="性别" />
                                                                <asp:BoundField DataField="customerID" HeaderText="证件号码" />
                                                                <asp:BoundField DataField="customerBirth" DataFormatString="{0:yyyy-MM-dd}" 
                                                                    HeaderText="出生日期" />
                                                                <asp:BoundField DataField="customerPhone" HeaderText="电话" />
                                                                <asp:BoundField DataField="customerFlightNo" HeaderText="航班号" />
                                                                <asp:BoundField DataField="customerFlightDate" 
                                                                    DataFormatString="{0:yyyy-MM-dd HH:mm}" HeaderText="乘机时间" HtmlEncode="False" />
                                                                <asp:BoundField DataField="productName" HeaderText="产品" />
                                                                <asp:BoundField DataField="CertNo" HeaderText="保单号" Visible="False" />
                                                            </Columns>
                                                            <RowStyle BackColor="#F7F7DE" />
                                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                            <AlternatingRowStyle BackColor="White" />
                                                        </asp:GridView>
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <cc1:Pager ID="pn1" runat="server" CssClass="pager" DisplayedPages="10" />
                                                                </td>
                                                                <td align="right">
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 0px; width: 3%" valign="top">
                                </td>
                                <td style="padding-left: 0px; width: 225px;" valign="top">
                                    <table>
                                        <tr>
                                            <td>
                                                <b>登录用户 :</b>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblUsername" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>账户余额：</b>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblBalance" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>库存单证 : </strong>
                                            </td>
                                            <td>
                                                <a href="#" onclick="window.showModalDialog('CorruptCaseNo.aspx?r=' + Math.random(),'pop','scroll=no;status=no;center=yes;resizable=no;dialogHeight=250px;dialogWidth=250px')">
                                                    <asp:Label ID="lblCount" runat="server"></asp:Label></a> 份
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>消耗单证 :</strong>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlConsumed" runat="server" CssClass="panelNormal" Style="float: left">
                                                    查询
                                                    <ajaxToolkit:DynamicPopulateExtender ID="lblConsumed_DynamicPopulateExtender" runat="server"
                                                        PopulateTriggerControlID="pnlConsumed" ServiceMethod="GetConsumed" TargetControlID="pnlConsumed"
                                                        UpdatingCssClass="panelUpdating">
                                                    </ajaxToolkit:DynamicPopulateExtender>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>作废单证 : </strong>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlDiscarded" runat="server" CssClass="panelNormal" Style="float: left">
                                                    查询
                                                    <ajaxToolkit:DynamicPopulateExtender ID="lblDiscarded_DynamicPopulateExtender" runat="server"
                                                        PopulateTriggerControlID="pnlDiscarded" ServiceMethod="GetDiscarded" TargetControlID="pnlDiscarded"
                                                        UpdatingCssClass="panelUpdating">
                                                    </ajaxToolkit:DynamicPopulateExtender>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
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
                                            &nbsp;<asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" 
                                                    Text="导 出" />
                                            </td>
                                        </tr>
                                        <tr>
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
