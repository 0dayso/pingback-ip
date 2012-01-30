<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="Admin_test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"> <html xmlns="http://www.w3.org/1999/xhtml" dir="ltr" lang="en"> <head> 

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function GetTab(sender,e) {
            alert(1);
        }

        function GetServer() {
            var param = $get("Text1").value;
            PageMethods.ServerFun(param, callback);
        }

        function callback(result) {
            $get("resultDiv").innerHTML = result;
        }
    </script>
    		<style type="text/css">
			
.awesome, .awesome:visited {
	background: #222 url(../../images/alert-overlay.png) repeat-x; 
	display: inline-block; 
	padding: 5px 10px 6px; 
	color: #fff; 
	text-decoration: none;
	-moz-border-radius: 5px; 
	-webkit-border-radius: 5px;
	-moz-box-shadow: 0 1px 3px rgba(0,0,0,0.5);
	-webkit-box-shadow: 0 1px 3px rgba(0,0,0,0.5);
	text-shadow: 0 -1px 1px rgba(0,0,0,0.25);
	border-bottom: 1px solid rgba(0,0,0,0.25);
	position: relative;
	cursor: pointer;
}

	.awesome:hover							{ background-color: #111; color: #fff; }
	.awesome:active							{ top: 1px; }
	.small.awesome, .small.awesome:visited 			{ font-size: 11px; padding: ; }
	.awesome, .awesome:visited,
	.medium.awesome, .medium.awesome:visited 		{ font-size: 13px; font-weight: bold; line-height: 1; text-shadow: 0 -1px 1px rgba(0,0,0,0.25); }
	.large.awesome, .large.awesome:visited 			{ font-size: 14px; padding: 8px 14px 9px; }
	
	.green.awesome, .green.awesome:visited		{ background-color: #91bd09; }
	.green.awesome:hover						{ background-color: #749a02; }
	.blue.awesome, .blue.awesome:visited		{ background-color: #2daebf; }
	.blue.awesome:hover							{ background-color: #007d9a; }
	.red.awesome, .red.awesome:visited			{ background-color: #e33100; }
	.red.awesome:hover							{ background-color: #872300; }
	.magenta.awesome, .magenta.awesome:visited		{ background-color: #a9014b; }
	.magenta.awesome:hover							{ background-color: #630030; }
	.orange.awesome, .orange.awesome:visited		{ background-color: #ff5c00; }
	.orange.awesome:hover							{ background-color: #d45500; }
	.yellow.awesome, .yellow.awesome:visited		{ background-color: #ffb515; }
	.yellow.awesome:hover							{ background-color: #fc9200; }
		</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
       <a href="/register/" rel="nofollow" class="btn_awesome btn_orange">测试按钮</a>

        <input id="Text1" type="text" value="hello" /><input id="Button1" type="button" value="button" onclick="GetServer()" />
        <div id="resultDiv"></div></div>
    <p>
        &nbsp;
        <button class="small magenta awesome">Super Awesome Button &raquo;</button></p>
    <asp:Button ID="Button2" runat="server" Text="Button" class="small magenta awesome"/>
    <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" class="small magenta awesome">LinkButton</asp:LinkButton>
    </form>
    </body>
</html>
