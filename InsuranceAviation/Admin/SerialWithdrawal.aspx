<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SerialWithdrawal.aspx.cs"
    Inherits="Agent_SerialWithdrawal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>:::商务会员:::</title>
    <link href="../Css/Styles.css" type="text/css" rel="stylesheet">
</head>
<body leftmargin="0" topmargin="0" bottommargin="0" rightmargin="0">
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" align="left" border="0">
        <tbody>
            <tr>
                <td valign="top" style="height: 50px">
                    
                    
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:InsuranceAviation %>"
                        SelectCommand="SELECT username, displayname, phone FROM t_User WHERE (parent = @parent)">
                        <SelectParameters>
                            <asp:Parameter Name="parent" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:InsuranceAviation %>"
                        SelectCommand="GetCorruptCaseNo" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="lblToUser" Name="caseOwner" PropertyName="Text"
                                Type="String" />
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
                                        单证号收回
                                    </div>
                                    <br />
                                    <table style="height: 140px">
                                        <tr>
                                            <td valign="top">
                                            </td>
                                            <td valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                被收回用户：
                                            </td>
                                            <td valign="top">
                                                <asp:Label ID="lblToUser" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                库存单证号：
                                            </td>
                                            <td valign="top">
                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                    BorderColor="#DEDFDE" BorderStyle="Solid" BorderWidth="1px" 
                                                    CellPadding="4" DataSourceID="SqlDataSource2"
                                                    EnableViewState="False" ForeColor="Black" GridLines="Vertical" 
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
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                收回单证号：
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox ID="txtNewStart" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNewStart"
                                                    Display="Dynamic" ErrorMessage="请完整填写起始单证号！"></asp:RequiredFieldValidator>－<asp:TextBox
                                                        ID="txtNewEnd" runat="server"></asp:TextBox>&nbsp;
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNewEnd"
                                                    Display="Dynamic" ErrorMessage="请完整填写截止单证号！"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                            </td>
                                            <td valign="top">
                                                <asp:Button ID="btnOk" runat="server" OnClick="btnOk_Click" Text="确 定" />&nbsp;
                                                <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="返 回" CausesValidation="False" />
                                                <asp:Label ID="lblError" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label>
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
