<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Statistics.aspx.cs"
    Inherits="Admin_Statistics" %>

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
    <style type="text/css">
        .style1
        {
            width: 229px;
        }
    </style>
</head>
<body leftmargin="0" topmargin="0" onload="HighLight('<%= this.txtKeyword.Text.Trim() %>')">
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" align="left" border="0">
        <tbody>
            <tr>
                <td valign="top" style="height: 50px">
                    
                    
                    <asp:SqlDataSource ID="sdsCount" runat="server" ConnectionString="<%$ ConnectionStrings:InsuranceAviation %>"
                        SelectCommand="SELECT t_Serial.caseOwner AS 用户帐号, COUNT(*) AS 库存, t_User.displayname AS 用户名称, t_User.userGroup AS 用户组, t_User.parent AS 父级帐号, userP.displayname AS 父级用户名称 FROM t_Serial INNER JOIN t_User ON t_Serial.caseOwner = t_User.username INNER JOIN t_User AS userP ON t_User.parent = userP.username WHERE (t_User.username LIKE '%' + @keyword + '%') OR (t_User.displayname LIKE '%' + @keyword + '%') GROUP BY t_Serial.caseOwner, t_User.displayname, t_User.userGroup, t_User.parent, userP.displayname ORDER BY 库存 DESC">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtKeyword" ConvertEmptyStringToNull="False" Name="keyword"
                                PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="sdsHot" runat="server" ConnectionString="<%$ ConnectionStrings:InsuranceAviation %>"
                        SelectCommand="SELECT t_Serial.caseOwner AS 用户帐号, COUNT(*) AS 库存, t_User.displayname AS 用户名称, t_User.userGroup AS 用户组, t_User.parent AS 父级帐号, t_User_1.displayname AS 父级用户名称, t_User.lastLoginDate AS 最近活跃时间 FROM t_Serial INNER JOIN t_User ON t_Serial.caseOwner = t_User.username INNER JOIN t_User AS t_User_1 ON t_User.parent = t_User_1.username GROUP BY t_Serial.caseOwner, t_User.displayname, t_User.userGroup, t_User.parent, t_User_1.displayname, t_User.lastLoginDate ORDER BY 最近活跃时间">
                    </asp:SqlDataSource>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
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
                                        <br />
                                        单证号状态统计<br />
                                        <br />
                                    </div>
                                    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>--%>
                                    <table>
                                        <tr>
                                            <td valign="top" class="style1">
                                                模糊搜索：<asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox><br />
                                                <asp:Button ID="btnCount" runat="server" Text="库存单证分布情况" OnClick="btnCount_Click" />
                                            </td>
                                            <td>
                                                <asp:GridView ID="gvCount" runat="server" AllowPaging="True" BackColor="White" BorderColor="#DEDFDE"
                                                    BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" BorderStyle="Solid"
                                                    OnRowDataBound="gvCount_RowDataBound">
                                                    <FooterStyle BackColor="#CCCC99" />
                                                    <SelectedRowStyle BackColor="#CE5D5A" ForeColor="White" Font-Bold="True" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <RowStyle BackColor="#F7F7DE" />
                                                </asp:GridView>
                                                <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="导 出" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" class="style1">
                                                <asp:Button ID="btnHot" runat="server" Text="单证活跃度" OnClick="btnHot_Click" />
                                            </td>
                                            <td>
                                                <asp:GridView ID="gvHot" runat="server" AllowPaging="True" BackColor="White" BorderColor="#DEDFDE"
                                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                                                    <FooterStyle BackColor="#CCCC99" />
                                                    <RowStyle BackColor="#F7F7DE" />
                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" class="style1">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                    <%--</ContentTemplate>
                                        </asp:UpdatePanel>--%>
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
