<%@ Page Language="C#" AutoEventWireup="true" CodeFile="t.aspx.cs" Inherits="t" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="txtName" runat="server">张山</asp:TextBox>
        <br />
        <asp:TextBox ID="txtMobile" runat="server">18607100437</asp:TextBox>
        <br />
    
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Issue" 
            style="height: 21px" />
        <br />
        <br />
        <br />
        <asp:TextBox ID="txtPlicyNo" runat="server"></asp:TextBox>
        <asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
            Text="withdrawal" />
        <br />
        <br />
        <asp:TextBox ID="txtItemCount" runat="server">1</asp:TextBox>
        <br />
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click1" 
            Text="Button" />
        <br />
        <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Button" />
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
