<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Statistics.aspx.cs"
    Inherits="Admin_Statistics" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>:::商务会员:::</title>
        <style type="text/css">
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
    <script type="text/javascript" src="../Script/jquery.min.js"></script>
   <script type="text/javascript">
       $(document).ready(function () {
           $("#btnQueryStat_1").click();
       });
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
        function GetStat_Interface() {
            var dateStart = $("#txtDateStart").val();
            var dateEnd = $("#txtDateEnd").val();
            //$.get("Ajax_Stat_Interface.aspx", { t: new Date().toString(), dateStart: dateStart, dateEnd: dateEnd }, callback);
            $.ajax({
                type: "get",
                url: "Ajax_Stat_Interface.aspx?t=" + new Date() + "&dateStart=" + dateStart + "&dateEnd=" + dateEnd,
                beforeSend: function () {
                    $("#btnQueryStat_1").val("请等待...").attr("disabled", "true");
                    $("#stat_1").html("").addClass("panelUpdating");
                },
                success: callback
            });
        }
        function callback(result) {
            $("#stat_1").removeClass("panelUpdating").html(result);
            $("#btnQueryStat_1").val("查询").removeAttr("disabled");
        }
    </script>
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
                                <td valign="top" align="center" style="width: 50%">
                                    <div style="font-weight: bold; font-size: 1.5em" nowrap>
                                        数据接口</div>
                                    <br />
                                    <table>
                                        <tr>
                                            <td valign="top" align="left">
                                                <asp:TextBox ID="txtDateStart" runat="server" Width="80px"></asp:TextBox>
                                                &nbsp;－
                                                <asp:TextBox ID="txtDateEnd" runat="server" Width="80px"></asp:TextBox>
                                                <input id="btnQueryStat_1" type="button" value="查询" onclick="GetStat_Interface()" />
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
                                            <td valign="top" align="left">
                                                <div id="stat_1">
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top" style="border-left: 1px solid gray;">
                                    &nbsp;
                                </td>
                                <td valign="top" align="center">
                                    <div style="font-weight: bold; font-size: 1.5em" nowrap>
                                        单证号状态统计<br />
                                        <br />
                                    </div>
                                    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>--%>
                                    <table>
                                        <tr>
                                            <td valign="top" align="left">
                                                模糊搜索：<asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <asp:Button ID="btnCount" runat="server" Text="库存单证分布情况" OnClick="btnCount_Click" />
                                                <asp:GridView ID="gvCount" runat="server" AllowPaging="True" BackColor="White" BorderColor="#DEDFDE"
                                                    BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" BorderStyle="Solid">
                                                    <FooterStyle BackColor="#CCCC99" />
                                                    <SelectedRowStyle BackColor="#CE5D5A" ForeColor="White" Font-Bold="True" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <RowStyle BackColor="#F7F7DE" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <asp:Button ID="btnHot" runat="server" Text="单证活跃度" OnClick="btnHot_Click" />
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
                                            <td valign="top">
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
