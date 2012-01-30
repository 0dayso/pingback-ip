<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SerialMngAdmin.aspx.cs" Inherits="SerialMngAdmin"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>:::商务会员:::</title>
    <link href="../Css/Styles.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../Script/Cal.aspx" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 125px;
        }
    </style>
</head>
<body leftmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" align="left" border="0">
        <tbody>
            <tr>
                <td valign="top" style="height: 50px">
                    
                    
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:InsuranceAviation %>"
                        SelectCommand="SELECT LogID, LogType, LogContent, datetime FROM t_Log WHERE (LogOwner = @LogOwner) AND (LogType = @LogType) AND (datetime BETWEEN @date1 AND @date2) AND (LogContent LIKE '%' + @LogContent + '%') ORDER BY datetime DESC">
                        <SelectParameters>
                            <asp:Parameter Name="LogOwner" />
                            <asp:ControlParameter ControlID="DropDownList1" Name="LogType" PropertyName="SelectedValue" />
                            <asp:ControlParameter ControlID="txtDateStart" DefaultValue="1900-1-1" Name="date1"
                                PropertyName="Text" />
                            <asp:ControlParameter ControlID="txtDateEnd" DefaultValue="2099-1-1" Name="date2"
                                PropertyName="Text" />
                            <asp:ControlParameter ControlID="txtKeyword" ConvertEmptyStringToNull="False" Name="LogContent"
                                PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="sdsInsurerList" runat="server" ConnectionString="<%$ ConnectionStrings:InsuranceAviation %>"
                        SelectCommand="SELECT username, displayname FROM t_User WHERE (usertype = 99) AND (parent = @parent) ORDER BY displayname">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="hfUsername" Name="parent" PropertyName="Value" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:HiddenField ID="hfUsername" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="bodyText" valign="top">
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td valign="top" style="width: 3%">
                                </td>
                                <td valign="top">
                                    <div style="font-weight: bold; font-size: 1.5em" nowrap>
                                        单证号管理<br />
                                    </div>
                                    <br />
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <table border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        操作类型：
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        日期范围：
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDateStart" runat="server" onclick="ShowCalendar(this)" Width="80px"></asp:TextBox>
                                                        &nbsp;－
                                                        <asp:TextBox ID="txtDateEnd" runat="server" onclick="ShowCalendar(this)" Width="80px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td align="right">
                                                        模糊搜索：
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                                                        <asp:Button ID="btnQuery" runat="server" CausesValidation="False" OnClick="btnQuery_Click"
                                                            Text="查询" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:GridView ID="gvLog" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#DEDFDE" BorderStyle="Solid" BorderWidth="1px"
                                                CellPadding="4" DataKeyNames="LogID" DataSourceID="SqlDataSource1" ForeColor="Black"
                                                GridLines="Vertical" OnRowDataBound="gvLog_RowDataBound" EnableViewState="False">
                                                <FooterStyle BackColor="#CCCC99" />
                                                <Columns>
                                                    <asp:BoundField DataField="LogID" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                                                        SortExpression="LogID">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="datetime" HeaderText="日期" SortExpression="datetime" />
                                                    <asp:BoundField DataField="LogType" HeaderText="操作类型" SortExpression="LogType">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LogContent" HeaderText="操作内容" SortExpression="LogContent" />
                                                </Columns>
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
                            <tr>
                                <td valign="top">
                                </td>
                                <td valign="top">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                </td>
                                <td valign="top">
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 30px">
                                </td>
                                <td valign="top">
                                    <div style="font-weight: bold; font-size: 1.5em" nowrap>
                                        创建单证号</div>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 30px">
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td class="style1">
                                                保险公司：
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlInsurer" runat="server" DataSourceID="sdsInsurerList" DataTextField="displayName"
                                                    DataValueField="username" OnSelectedIndexChanged="ddlInsurer_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                号段范围：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewStart" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNewStart"
                                                    Display="Dynamic" ErrorMessage="请完整填写起始单证号！" ValidationGroup="NewCase"></asp:RequiredFieldValidator>－<asp:TextBox
                                                        ID="txtNewEnd" runat="server"></asp:TextBox>&nbsp;
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNewEnd"
                                                    Display="Dynamic" ErrorMessage="请完整填写截止单证号！" ValidationGroup="NewCase"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                仅在以下地区可用：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtInclude" runat="server" Width="290px"></asp:TextBox>
                                                （若有多个地区，请用空格隔开；例如“北京 上海 广州”，请勿添加“省”“市”“县”等字眼）</td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                禁止在以下地区使用：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtExclude" runat="server" Width="290px"></asp:TextBox>
                                                （若有多个地区，请用空格隔开；例如“北京 上海 广州”，请勿添加“省”“市”“县”等字眼）</td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                对区域限制的说明：</td>
                                            <td>
                                                <asp:TextBox ID="txtLocationComment" runat="server" Width="451px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Button ID="btnOk" runat="server" OnClick="btnOk_Click" Text="确 定" ValidationGroup="NewCase" />
                                                <asp:Label ID="lblError" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30px" valign="top">
                                </td>
                                <td valign="top">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 30px">
                                </td>
                                <td valign="top">
                                    <div style="font-weight: bold; font-size: 1.5em" nowrap>
                                        销毁单证号</div>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 30px">
                                </td>
                                <td valign="top">
                                    <asp:TextBox ID="txtDiscardStart" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDiscardStart"
                                        Display="Dynamic" ErrorMessage="请完整填写起始单证号！" ValidationGroup="DiscardCase"></asp:RequiredFieldValidator>－<asp:TextBox
                                            ID="txtDiscardEnd" runat="server"></asp:TextBox>&nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDiscardEnd"
                                        Display="Dynamic" ErrorMessage="请完整填写截止单证号！" ValidationGroup="DiscardCase"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 30px">
                                </td>
                                <td valign="top">
                                    <asp:Button ID="btnDiscard" runat="server" OnClick="btnDiscard_Click" Text="确 定"
                                        ValidationGroup="DiscardCase" />
                                    <asp:Label ID="lblErrorDiscard" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30px" valign="top">
                                </td>
                                <td valign="top">
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
