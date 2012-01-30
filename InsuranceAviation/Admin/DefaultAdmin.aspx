<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefaultAdmin.aspx.cs" Inherits="DefaultAdmin" %>

<%@ Register Assembly="IAClass" Namespace="MyWebControl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>:::商务会员:::</title>
    <style type="text/css">
        .updating
        {
            background-image: url("../images/spinner03.gif");
            background-repeat: no-repeat;
            width: 16px;
            height: 16px;
        }
    </style>
    <link href="../Css/Styles.css" type="text/css" rel="stylesheet"/>
    <script type="text/javascript" src="../Script/Cal.aspx"></script>
    <script type="text/javascript" src="../Script/jquery.min.js"></script>
    <script type="text/javascript" src="../Script/jquery.timeago.js"></script>
    <script type="text/javascript">
<!--
        $(document).ready(function () {
            timeAgo();
            PageMethods.TopEveryday($get("txtDateStart").value, $get("ddlProduct").value, callbackTop1);
            PageMethods.TopToday($get("txtDateStart").value, $get("txtDateEnd").value, $get("ddlProduct").value, callbackTop2);
            //每次ajax翻页完成后，调用该方法，否则timeago效果会消失
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(timeAgo);

        });

        function callbackTop1(result) {
            var div = $get("TopEverydayParent");
            div.className = "";
            div.innerHTML = result;
        }

        function callbackTop2(result) {
            var div = $get("TopTodayParent");
            div.className = "";
            div.innerHTML = result;
        }

        function timeAgo() {
            $("abbr.timeago").timeago();
        }
//-->
    </script>
</head>
<body leftmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" align="left" border="0">
        <tbody>
            <tr>
                <td valign="top" style="height: 50px">
                    
                    
                    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True">
                    </ajaxToolkit:ToolkitScriptManager>
                    <asp:SqlDataSource ID="sdsProduct" runat="server" ConnectionString="<%$ ConnectionStrings:InsuranceAviation %>"
                        SelectCommand="SELECT productID, productName FROM t_Product ORDER BY productName">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td class="bodyText" valign="top">
                    <table>
                        <tbody>
                            <tr>
                                <td style="width: 3%" valign="top">
                                </td>
                                <td style="width: 225px" valign="top">
                                    <div style="font-weight: bold; font-size: 1.5em" nowrap>
                                        欢迎使用
                                    </div>
                                </td>
                                <td valign="top" rowspan="2">
                                    <table width="100%" height="150">
                                        <tr>
                                            <td valign="top">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <div id="TopEverydayParent" class="updating">
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div id="TopTodayParent" class="updating">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" AutoPostBack="True">
                                                <ajaxToolkit:TabPanel runat="server" HeaderText="实时列表" ID="tabForAdmin">
                                                    <HeaderTemplate>
                                                        实时清单
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <asp:SqlDataSource ID="sdsListForAdmin" runat="server" ConnectionString="<%$ ConnectionStrings:InsuranceAviation %>"
                                                            DeleteCommand="select 1" OnSelected="SqlDataSource1_Selected" SelectCommand="PagedCaseListForAdmin"
                                                            SelectCommandType="StoredProcedure">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="pn1" Name="pageNum" PropertyName="SelectedPage" />
                                                                <asp:Parameter DefaultValue="10" Name="pageSize" Type="Int32" />
                                                                <asp:Parameter Direction="ReturnValue" Name="pageCount" Type="Int32" />
                                                                <asp:Parameter Name="dateStart" Type="DateTime" DefaultValue="1900-1-1" />
                                                                <asp:Parameter Name="dateEnd" Type="DateTime" DefaultValue="2099-1-1" />
                                                                <asp:ControlParameter ControlID="ddlProduct" Name="productId" PropertyName="SelectedValue"
                                                                    Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                        <asp:GridView ID="gvListForAdmin" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                            BorderColor="#DEDFDE" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                                            GridLines="Vertical" DataKeyNames="caseNo" EnableViewState="False" OnRowDataBound="GridView1_RowDataBound">
                                                            <AlternatingRowStyle BackColor="White" />
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
                                                                <asp:TemplateField HeaderText="地区">
                                                                    <ItemTemplate>
                                                                        <%# GetIpLocation(Eval("IpLocation"))%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="displayname" HeaderText="加盟商户" />
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
                                                                    HtmlEncode="False">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="短信">
                                                                    <ItemTemplate>
                                                                        <%# GetSMStatus(Eval("isSMSent")) %>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="CertNo" HeaderText="保单号" />
                                                                <asp:BoundField DataField="productName" HeaderText="产品" />
                                                            </Columns>
                                                            <FooterStyle BackColor="#CCCC99" />
                                                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                            <PagerSettings Mode="NumericFirstLast" />
                                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                            <RowStyle BackColor="#F7F7DE" />
                                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                        </asp:GridView>
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <cc1:Pager ID="pn1" runat="server" CssClass="pager" DisplayedPages="10"
                                                                        Count="1" SelectedPage="1" />
                                                                </td>
                                                                <td align="right">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" HeaderText="导出专用" ID="tabForAdmin_NotIssued">
                                                    <HeaderTemplate>
                                                        未投保清单
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <asp:SqlDataSource ID="sdsListForAdmin_NotIssued" runat="server" ConnectionString="<%$ ConnectionStrings:InsuranceAviation %>"
                                                            DeleteCommand="select 1" OnSelected="SqlDataSource1_Selected" SelectCommand="PagedCaseListForAdmin_NotIssued"
                                                            SelectCommandType="StoredProcedure">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="pn2" Name="pageNum" PropertyName="SelectedPage" />
                                                                <asp:Parameter DefaultValue="10" Name="pageSize" Type="Int32" />
                                                                <asp:Parameter Direction="ReturnValue" Name="pageCount" Type="Int32" />
                                                                <asp:Parameter Name="dateStart" Type="DateTime" DefaultValue="1900-1-1" />
                                                                <asp:Parameter Name="dateEnd" Type="DateTime" DefaultValue="2099-1-1" />
                                                                <asp:ControlParameter ControlID="ddlProduct" Name="productId" PropertyName="SelectedValue"
                                                                    Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                        <asp:GridView ID="gvListForCPIC" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                            BorderColor="#DEDFDE" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                                            GridLines="Vertical" DataKeyNames="caseNo" EnableViewState="False" OnRowDataBound="GridView1_RowDataBound">
                                                            <AlternatingRowStyle BackColor="White" />
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
                                                                <asp:TemplateField HeaderText="地区">
                                                                    <ItemTemplate>
                                                                        <%# GetIpLocation(Eval("IpLocation"))%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="displayname" HeaderText="加盟商户" />
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
                                                                    HtmlEncode="False">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="productName" HeaderText="产品" />
                                                            </Columns>
                                                            <FooterStyle BackColor="#CCCC99" />
                                                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                            <RowStyle BackColor="#F7F7DE" />
                                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                        </asp:GridView>
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <cc1:Pager ID="pn2" runat="server" CssClass="pager" DisplayedPages="10"
                                                                        Count="1" SelectedPage="1" />
                                                                </td>
                                                                <td align="right">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" HeaderText="作废列表" ID="tabForAdmin_Discarded">
                                                    <HeaderTemplate>
                                                        作废清单
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <asp:SqlDataSource ID="sdsListForAdmin_Discarded" runat="server" ConnectionString="<%$ ConnectionStrings:InsuranceAviation %>"
                                                            DeleteCommand="select 1" OnSelected="SqlDataSource1_Selected" SelectCommand="PagedCaseListForAdmin_Discarded"
                                                            SelectCommandType="StoredProcedure">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="pn3" Name="pageNum" PropertyName="SelectedPage" />
                                                                <asp:Parameter DefaultValue="10" Name="pageSize" Type="Int32" />
                                                                <asp:Parameter Direction="ReturnValue" Name="pageCount" Type="Int32" />
                                                                <asp:Parameter Name="dateStart" Type="DateTime" DefaultValue="1900-1-1" />
                                                                <asp:Parameter Name="dateEnd" Type="DateTime" DefaultValue="2099-1-1" />
                                                                <asp:ControlParameter ControlID="ddlProduct" Name="productId" PropertyName="SelectedValue"
                                                                    Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                        <asp:GridView ID="gvListForCPIC_Discarded" runat="server" AutoGenerateColumns="False"
                                                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="Solid" BorderWidth="1px"
                                                            CellPadding="4" ForeColor="Black" GridLines="Vertical" DataKeyNames="caseNo"
                                                            EnableViewState="False" OnRowDataBound="GridView1_RowDataBound">
                                                            <AlternatingRowStyle BackColor="White" />
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
                                                                <asp:TemplateField HeaderText="地区">
                                                                    <ItemTemplate>
                                                                        <%# GetIpLocation(Eval("IpLocation"))%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="displayname" HeaderText="加盟商户" />
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
                                                                    HtmlEncode="False">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="productName" HeaderText="产品" />
                                                            </Columns>
                                                            <FooterStyle BackColor="#CCCC99" />
                                                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                            <RowStyle BackColor="#F7F7DE" />
                                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                        </asp:GridView>
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <cc1:Pager ID="pn3" runat="server" CssClass="pager" DisplayedPages="10"
                                                                        Count="1" SelectedPage="1" />
                                                                </td>
                                                                <td align="right">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                            </ajaxToolkit:TabContainer>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 3%" valign="top">
                                </td>
                                <td style="width: 225px;" valign="top">
                                    <b>登录用户 : </b>
                                    <asp:Label ID="lblUsername" runat="server"></asp:Label><br>
                                    <b>用户身份 : </b>系统管理员<br />
                                    <strong>账户余额 : </strong><asp:Label ID="lblBalance" runat="server"></asp:Label> 元<br />
                                    <strong>库存单证 : </strong><a href="#" onclick="window.showModalDialog('CorruptCaseNo.aspx','pop','scroll=no;status=no;center=yes;resizable=no;dialogHeight=250px;dialogWidth=450px')">
                                        <asp:Label ID="lblCount" runat="server"></asp:Label></a> 份<br />
                                    <br />
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="True" DataSourceID="sdsProduct"
                                                    DataTextField="productName" DataValueField="productId">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                开始日期：<asp:TextBox ID="txtDateStart" runat="server" onclick="ShowCalendar(this)" Width="80px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                截止日期：<asp:TextBox ID="txtDateEnd" runat="server" onclick="ShowCalendar(this)" Width="80px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnQuery" runat="server" Text="查 询" OnClick="btnQuery_Click" />
                                                &nbsp;
                                                <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="导 出" />
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
