<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ajax_Stat_Interface.aspx.cs"
    Inherits="Admin_Ajax_Stat_Interface" %>

<asp:sqldatasource id="sdsIOC" runat="server" connectionstring="<%$ ConnectionStrings:InsuranceAviation %>"
    deletecommand="DELETE FROM [t_Interface] WHERE [Id] = @Id" insertcommand="INSERT INTO [t_Interface] ([Id], [IOC_Class_Alias], [Description]) VALUES (@Id, @IOC_Class_Alias, @Description)"
    selectcommand="SELECT * FROM [t_Interface]" updatecommand="UPDATE [t_Interface] SET [IOC_Class_Alias] = @IOC_Class_Alias, [Description] = @Description WHERE [Id] = @Id">
                        <DeleteParameters>
                            <asp:Parameter Name="Id" Type="Int16" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="Id" Type="Int16" />
                            <asp:Parameter Name="IOC_Class_Alias" Type="String" />
                            <asp:Parameter Name="Description" Type="String" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="IOC_Class_Alias" Type="String" />
                            <asp:Parameter Name="Description" Type="String" />
                            <asp:Parameter Name="Id" Type="Int16" />
                        </UpdateParameters>
                    </asp:sqldatasource>
<asp:gridview id="gvIOC" runat="server" backcolor="White" bordercolor="#DEDFDE" borderstyle="None"
    borderwidth="1px" cellpadding="4" forecolor="Black" gridlines="Vertical" autogeneratecolumns="False"
    datasourceid="sdsIOC">
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:BoundField DataField="Id" HeaderText="编号" SortExpression="IOC_Id" />
        <asp:BoundField DataField="IOC_Class_Alias" HeaderText="接口类名" SortExpression="IOC_Class_Alias" />
        <asp:BoundField DataField="Description" HeaderText="接口说明" SortExpression="Description" />
        <asp:TemplateField HeaderText="已发送">
            <ItemTemplate>
            <%# CountIssued(Eval("id"))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="已撤销">
            <ItemTemplate>
            <%# CountWithdrawed(Eval("id"))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="已生效">
            <ItemTemplate>
            <%# CountDone(Eval("id"))%>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <FooterStyle BackColor="#CCCC99" />
    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
    <RowStyle BackColor="#F7F7DE" />
    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#FBFBF2" />
    <SortedAscendingHeaderStyle BackColor="#848384" />
    <SortedDescendingCellStyle BackColor="#EAEAD3" />
    <SortedDescendingHeaderStyle BackColor="#575357" />
</asp:gridview>