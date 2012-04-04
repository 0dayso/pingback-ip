<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserMng.aspx.cs"
    Inherits="Admin_UserMng" %>

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
                        
                        
                        <asp:SqlDataSource ID="sdsUserList" runat="server" ConnectionString="<%$ ConnectionStrings:InsuranceAviation %>"
                            SelectCommand="SELECT username, password, displayname, address, phone, enabled, userGroup, enabled_Issuing, price, balance FROM t_User WITH (nolock) WHERE (parent = @parent) AND (usertype &lt;&gt; 99) AND (username LIKE '%' + @keyword + '%') OR (parent = @parent) AND (usertype &lt;&gt; 99) AND (displayname LIKE '%' + @keyword + '%') OR (username = @parent) ORDER BY usertype, datetime DESC"
                            InsertCommand="INSERT INTO t_User(username, password, displayname, phone, address, enabled, parent, usertype, userGroup, parentPath, distributor, price) VALUES (@username, @password, @displayname, @phone, @address, @enabled, @parent, @usertype, @userGroup, @parentPath, @distributor, @price)"
                            
                            
                            
                            UpdateCommand="UPDATE t_User SET displayname = @displayname, phone = @phone, address = @address, enabled = @enabled, password = @password, userGroup = @userGroup, enabled_Issuing = @enabled_Issuing, price = @price WHERE (username = @username)">
                            <SelectParameters>
                                <asp:Parameter Name="parent" Type="String" />
                                <asp:ControlParameter ControlID="txtKeyword" ConvertEmptyStringToNull="False" 
                                    Name="keyword" PropertyName="Text" Type="String" />
                            </SelectParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="username" Type="String" />
                                <asp:Parameter Name="password" />
                                <asp:Parameter Name="displayname" Type="String" />
                                <asp:Parameter Name="address" Type="String" />
                                <asp:Parameter Name="phone" Type="String" />
                                <asp:Parameter Name="enabled" Type="Boolean" />
                                <asp:Parameter Name="userGroup" />
                                <asp:Parameter Name="price" DbType="Decimal" />
                            </UpdateParameters>
                            <InsertParameters>
                                <asp:Parameter Name="username" Type="String" />
                                <asp:Parameter Name="password" />
                                <asp:Parameter Name="displayname" Type="String" />
                                <asp:Parameter Name="address" Type="String" />
                                <asp:Parameter Name="phone" Type="String" />
                                <asp:Parameter Name="enabled" Type="Boolean" />
                                <asp:Parameter Name="parent" />
                                <asp:Parameter Name="usertype" />
                                <asp:Parameter Name="userGroup" />
                                <asp:Parameter Name="parentPath" />
                                <asp:Parameter Name="distributor" />
                                <asp:Parameter Name="price" DbType="Decimal" DefaultValue="0" />
                            </InsertParameters>
                        </asp:SqlDataSource>
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:SqlDataSource ID="sdsProvince" runat="server" ConnectionString="<%$ ConnectionStrings:InsuranceAviation %>"
                            SelectCommand="SELECT [ProvinceID], [ProvinceName] FROM [t_Province]">
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="sdsCity" runat="server" ConnectionString="<%$ ConnectionStrings:InsuranceAviation %>"
                            
                            
                            SelectCommand="SELECT CityID, CityName FROM t_City WHERE (ProvinceID = @ProvinceID)">
                            <SelectParameters>
                                <asp:Parameter Name="ProvinceID" />
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
                                            用户管理</div>
                                        <br />
                                        模糊搜索<asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox><asp:Button 
                                            ID="btnSearch" runat="server" Text="搜索" onclick="btnSearch_Click" 
                                            CausesValidation="False" />
                                        <br />
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                    DataKeyNames="username" DataSourceID="sdsUserList" BackColor="White" BorderColor="#DEDFDE"
                                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                                    ShowFooter="True" OnRowDataBound="GridView1_RowDataBound" 
                                                    OnDataBound="GridView1_DataBound" onrowupdating="GridView1_RowUpdating">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="删除">
                                                            <EditItemTemplate>
                                                                <asp:CheckBox ID="cbDel" runat="server" />
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cbDel" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowEditButton="True" CausesValidation="False" >
                                                            <ItemStyle Wrap="False" />
                                                        </asp:CommandField>
                                                        <asp:TemplateField HeaderText="启用" SortExpression="enabled">
                                                            <EditItemTemplate>
                                                                <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("enabled") %>' />
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnNewUser" runat="server" Text="添 加" OnClick="btnNewUser_Click" />
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("enabled") %>' Enabled="false" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="出单权限" SortExpression="enabled_Issuing">
                                                            <EditItemTemplate>
                                                                <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Bind("enabled_Issuing") %>' />
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Bind("enabled_Issuing") %>' Enabled="false" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="用户名" SortExpression="username">
                                                            <EditItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("username") %>'></asp:Label>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtUsername" runat="server" Width="80px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                                    ControlToValidate="txtUsername" Display="Dynamic" ErrorMessage="用户名不能为空！"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtUsername"
                                                                    Display="Dynamic" ErrorMessage="至少包含4个字符（字母或数字或下划线）！" 
                                                                    ValidationExpression="\w{4,20}"></asp:RegularExpressionValidator>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("username") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="密码">
                                                            <EditItemTemplate>
                                                                <%--<asp:TextBox ID="txtPasswword" runat="server" Text='<%# Bind("password") %>' 
                                                                    Width="80px" TextMode="Password" value='<%# Eval("password") %>'></asp:TextBox>--%>
                                                                    <asp:TextBox ID="txtPasswword" runat="server" Text='<%# Bind("password") %>' 
                                                                    Width="80px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtPassword" runat="server" Width="80px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                                    ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="密码不能为空！"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="price" DataFormatString="{0:c}" HeaderText="单价" />
                                                        <asp:BoundField DataField="balance" DataFormatString="{0:c}" HeaderText="余额" />
                                                        <asp:TemplateField HeaderText="用户组">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("userGroup") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("userGroup") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:DropDownList ID="ddlUserProvince" runat="server" 
                                                                    DataSourceID="sdsProvince" DataTextField="ProvinceName" 
                                                                    DataValueField="ProvinceID" 
                                                                    onselectedindexchanged="ddlUserProvince_SelectedIndexChanged" 
                                                                    AutoPostBack="True">
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="ddlUserCity" runat="server" DataSourceID="sdsCity" 
                                                                    DataTextField="CityName" DataValueField="CityID">
                                                                </asp:DropDownList>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="单位名称" SortExpression="displayname">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("displayname") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtDisplayname" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                                    ControlToValidate="txtDisplayname" Display="Dynamic" ErrorMessage="单位名称不能为空！"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                                                    ControlToValidate="txtDisplayname" Display="Dynamic" ErrorMessage="请如实填写！" 
                                                                    
                                                                    ValidationExpression="[\w\u4e00-\u9fa5]*[\u4e00-\u9fa5]{2,30}[\w\u4e00-\u9fa5]*"></asp:RegularExpressionValidator>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("displayname") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="单位地址" SortExpression="address">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("address") %>' Width="200px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddress" runat="server" Width="220px"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("address") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="联系电话" SortExpression="phone">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("phone") %>' Width="100px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtPhone" runat="server" Width="100px"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("phone") %>'></asp:Label>
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
                                                <asp:Button ID="btnDelete" runat="server" BackColor="Thistle" Height="38px" OnClick="btnDelete_Click"
                                                    Text="删除选中的帐户" OnClientClick='return confirm("确定要删除所有选择的账号？")' />
                                                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
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
