<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsurerMng.aspx.cs" Inherits="Admin_InsurerMng" ValidateRequest="false" %>
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
                            SelectCommand="SELECT username, password, displayname, address, phone, enabled FROM t_User with(nolock) WHERE (usertype = 99) AND (parent = @parent) OR (username = @parent) ORDER BY usertype, datetime DESC"
                            InsertCommand="INSERT INTO t_User(username, password, displayname, phone, address, enabled, parent, usertype, parentPath) VALUES (@username, @password, @displayname, @phone, @address, @enabled, @parent, @usertype, @parentPath)"
                            UpdateCommand="UPDATE t_User SET displayname = @displayname, phone = @phone, address = @address, enabled = @enabled, password = @password WHERE (username = @username)">
                            <UpdateParameters>
                                <asp:Parameter Name="username" Type="String" />
                                <asp:Parameter Name="password" />
                                <asp:Parameter Name="displayname" Type="String" />
                                <asp:Parameter Name="address" Type="String" />
                                <asp:Parameter Name="phone" Type="String" />
                                <asp:Parameter Name="enabled" Type="Boolean" />
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
                                <asp:Parameter Name="parentPath" Type="String" />
                            </InsertParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="hfUsername" Name="parent" PropertyName="Value" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="sdsProduct" runat="server" ConnectionString="<%$ ConnectionStrings:InsuranceAviation %>"
                            SelectCommand="SELECT * FROM t_Product WHERE (productID = @productID) AND (productSupplier = @productSupplier)"
                            InsertCommand="INSERT INTO t_Product(Enabled, productName, productSupplier, productDuration, productComment, PrintingConfig, FilterInclude, FilterExclude, FilterComment, IsIssuingRequired, IOC_TypeName, IsIssuingLazyEnabled, WithdrawRatio) VALUES (@Enabled, @productName, @productSupplier, @productDuration, @productComment, @PrintingConfig, @FilterInclude, @FilterExclude, @FilterComment, @IsIssuingRequired, @IOC_TypeName, @IsIssuingLazyEnabled, @WithdrawRatio)"
                            
                            
                            
                            
                            
                            
                            
                            
                            
                            
                            UpdateCommand="UPDATE t_Product SET Enabled = @Enabled, productName = @productName, productDuration = @productDuration, productComment = @productComment, PrintingConfig = @PrintingConfig, FilterInclude = @FilterInclude, FilterExclude = @FilterExclude, FilterComment = @FilterComment, IsIssuingRequired = @IsIssuingRequired, IOC_TypeName = @IOC_TypeName, IsIssuingLazyEnabled = @IsIssuingLazyEnabled, WithdrawRatio = @WithdrawRatio WHERE (productID = @productID)">
                            <UpdateParameters>
                                <asp:Parameter Name="productName" />
                                <asp:Parameter Name="productDuration" />
                                <asp:Parameter Name="productComment" />
                                <asp:Parameter Name="PrintingConfig" />
                                <asp:Parameter Name="FilterInclude" />
                                <asp:Parameter Name="FilterExclude" />
                                <asp:Parameter Name="FilterComment" />
                                <asp:Parameter Name="productID" />
                                <asp:Parameter Name="Enabled" />
                                <asp:Parameter Name="IsIssuingRequired" />
                                <asp:Parameter Name="IOC_TypeName" />
                                <asp:Parameter Name="IsIssuingLazyEnabled" />
                                <asp:Parameter Name="WithdrawRatio" />
                            </UpdateParameters>
                            <InsertParameters>
                                <asp:Parameter Name="productName" />
                                <asp:Parameter Name="productSupplier" />
                                <asp:Parameter Name="productDuration" />
                                <asp:Parameter Name="productComment" />
                                <asp:Parameter Name="PrintingConfig" />
                                <asp:Parameter Name="FilterInclude" />
                                <asp:Parameter Name="FilterExclude" />
                                <asp:Parameter Name="FilterComment" />
                                <asp:Parameter Name="Enabled" />
                                <asp:Parameter Name="IsIssuingRequired" />
                                <asp:Parameter Name="IOC_TypeName" />
                                <asp:Parameter Name="IsIssuingLazyEnabled" />
                                <asp:Parameter Name="WithdrawRatio" />
                            </InsertParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="gvProductList" Name="productID" 
                                    PropertyName="SelectedValue" />
                                <asp:ControlParameter ControlID="GridView1" Name="productSupplier" 
                                    PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
                        </asp:ScriptManager>
                        <asp:HiddenField ID="hfUsername" runat="server" />
                        <asp:SqlDataSource ID="sdsProductList" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:InsuranceAviation %>" 
                            ProviderName="<%$ ConnectionStrings:InsuranceAviation.ProviderName %>" 
                            
                            SelectCommand="SELECT Enabled, productID, productName, productDuration FROM t_Product WHERE (productSupplier = @productSupplier)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="GridView1" Name="productSupplier" 
                                    PropertyName="SelectedValue" />
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
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                                                AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                                                                BorderStyle="Solid" BorderWidth="1px" CellPadding="4" DataKeyNames="username" 
                                                                DataSourceID="SqlDataSource1" EnableViewState="False" ForeColor="Black" 
                                                                GridLines="Vertical" OnRowDataBound="GridView1_RowDataBound" 
                                                                ShowFooter="True">
                                                                <Columns>
                                                                    <asp:CommandField ShowEditButton="True" ShowSelectButton="True">
                                                                        <ItemStyle Wrap="False" />
                                                                    </asp:CommandField>
                                                                    <asp:TemplateField HeaderText="启用" SortExpression="enabled">
                                                                        <EditItemTemplate>
                                                                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("enabled") %>' />
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Button ID="btnNewUser" runat="server" OnClick="btnNewUser_Click" 
                                                                                Text="添 加" />
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Bind("enabled") %>' 
                                                                                Enabled="false" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="用户名" SortExpression="username">
                                                                        <EditItemTemplate>
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("username") %>'></asp:Label>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox ID="txtUsername" runat="server" Width="80px"></asp:TextBox>
                                                                            &nbsp;
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("username") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="密码">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("password") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox ID="txtPassword" runat="server" Width="80px"></asp:TextBox>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="单位名称" SortExpression="displayname">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("displayname") %>' 
                                                                                Width="200px"></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox ID="txtDisplayname" runat="server" Width="200px"></asp:TextBox>
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("displayname") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="单位地址" SortExpression="address">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("address") %>' 
                                                                                Width="220px"></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox ID="txtAddress" runat="server" Width="220px"></asp:TextBox>
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("address") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="联系电话" SortExpression="phone">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("phone") %>' 
                                                                                Width="100px"></asp:TextBox>
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
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:GridView ID="gvProductList" runat="server" AutoGenerateColumns="False" 
                                                                            CellPadding="4" DataKeyNames="productID" DataSourceID="sdsProductList" 
                                                                            ForeColor="Black" GridLines="Vertical" 
                                                                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="Solid" 
                                                                            BorderWidth="1px" EnableModelValidation="True">
                                                                            <FooterStyle BackColor="#CCCC99" />
                                                                            <RowStyle BackColor="#F7F7DE" />
                                                                            <Columns>
                                                                                <asp:CommandField ShowSelectButton="True" />
                                                                                <asp:BoundField DataField="productID" HeaderText="产品编号" />
                                                                                <asp:BoundField DataField="productName" HeaderText="产品名称" />
                                                                                <asp:BoundField DataField="productDuration" HeaderText="产品期限" />
                                                                                <asp:CheckBoxField DataField="Enabled" HeaderText="启用" />
                                                                            </Columns>
                                                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                                            <EmptyDataTemplate>
                                                                                <asp:Button ID="btnNewProduct" runat="server" CommandName="New" Text="新建产品类型" 
                                                                                    onclick="btnNewProduct_Click" />
                                                                            </EmptyDataTemplate>
                                                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                                            <AlternatingRowStyle BackColor="White" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                    <td valign="top">
                                                                        &nbsp;</td>
                                                                    <td valign="top">
                                                                        <asp:DetailsView ID="dvProduct" runat="server" AutoGenerateRows="False" 
                                                                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="Solid" BorderWidth="1px" 
                                                                            CellPadding="4" DataKeyNames="productID" DataSourceID="sdsProduct" 
                                                                            ForeColor="Black" GridLines="Vertical" 
                                                                            oniteminserted="dvProduct_ItemInserted" EnableModelValidation="True">
                                                                            <FooterStyle BackColor="#CCCC99" />
                                                                            <RowStyle BackColor="#F7F7DE" />
                                                                            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
                                                                                HorizontalAlign="Center" />
                                                                            <Fields>
                                                                                <asp:CheckBoxField DataField="Enabled" HeaderText="启用" />
                                                                                <asp:BoundField DataField="productName" HeaderText="产品名称" />
                                                                                <asp:BoundField DataField="productSupplier" HeaderText="保险公司帐号" 
                                                                                    ReadOnly="True" />
                                                                                <asp:BoundField DataField="productDuration" HeaderText="产品期限" />
                                                                                <asp:TemplateField HeaderText="打印格式">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtPrinting" runat="server" Height="100px" 
                                                                                            Text='<%# Eval("PrintingConfig") %>' TextMode="MultiLine" Width="520px" 
                                                                                            ReadOnly="true"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                        <asp:TextBox ID="txtPrinting" runat="server" Height="300px" 
                                                                                            Text='<%# Bind("PrintingConfig") %>' TextMode="MultiLine" Width="520px"></asp:TextBox>
                                                                                    </EditItemTemplate>
                                                                                    <InsertItemTemplate>
                                                                                        <asp:TextBox ID="txtPrinting" runat="server" Height="300px" 
                                                                                            Text='<%# Bind("PrintingConfig") %>' TextMode="MultiLine" Width="520px"></asp:TextBox>
                                                                                    </InsertItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="FilterInclude" HeaderText="可用地区" />
                                                                                <asp:BoundField DataField="FilterExclude" HeaderText="排除地区（空格隔开）" />
                                                                                <asp:BoundField DataField="FilterComment" HeaderText="过滤说明" />
                                                                                <asp:BoundField DataField="productComment" HeaderText="产品备注" />
                                                                                <asp:CheckBoxField DataField="IsIssuingRequired" HeaderText="数据对接" />
                                                                                <asp:BoundField DataField="IOC_TypeName" HeaderText="IOC对象名" />
                                                                                <asp:CheckBoxField DataField="IsIssuingLazyEnabled" HeaderText="可否延迟投保（追溯）" />
                                                                                <asp:BoundField DataField="WithdrawRatio" HeaderText="作废率" />
                                                                                <asp:CommandField ShowEditButton="True" ShowInsertButton="True" />
                                                                            </Fields>
                                                                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                                            <EditRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                                            <AlternatingRowStyle BackColor="White" />
                                                                        </asp:DetailsView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
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
