<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Public_Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language ="javascript" type="text/javascript" >
        function RefreshImage() {
            var el = document.getElementById("Image1");
            el.src = el.src + '?'; //这个特别重要
        }
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <img id="Image1" src="default.aspx" style="cursor:pointer" onclick="RefreshImage()" alt="点击重刷新"/>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>
