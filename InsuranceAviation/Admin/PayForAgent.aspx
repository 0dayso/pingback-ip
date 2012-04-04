<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayForAgent.aspx.cs" Inherits="PayForAgent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>支付宝即时到帐接口</title>

    <script language="JavaScript">
        function CheckForm() {
            if (document.forms["alipayment"].TxtSubject.value.length == 0) {
                alert("请输入商品名称.");
                document.forms["alipayment"].TxtSubject.focus();
                return false;
            }
            if (document.forms["alipayment"].TxtTotal_fee.value.length == 0) {
                alert("请输入付款金额.");
                document.forms["alipayment"].TxtTotal_fee.focus();
                return false;
            }
            var reg = new RegExp(/^\d*\.?\d{0,2}$/);
            if (!reg.test(document.forms["alipayment"].TxtTotal_fee.value)) {
                alert("请正确输入付款金额");
                document.forms["alipayment"].TxtTotal_fee.focus();
                return false;
            }
            if (Number(document.forms["alipayment"].TxtTotal_fee.value) < 0.01) {
                alert("付款金额金额最小是0.01.");
                document.forms["alipayment"].TxtTotal_fee.focus();
                return false;
            }
            function getStrLength(value) {
                return value.replace(/[^\x00-\xFF]/g, '**').length;
            }
            if (getStrLength(document.forms["alipayment"].TxtBody.value) > 200) {
                alert("备注过长！请在100个汉字以内");
                document.forms["alipayment"].TxtBody.focus();
                return false;
            }
            if (getStrLength(document.forms["alipayment"].TxtSubject.value) > 256) {
                alert("标题过长！请在128个汉字以内");
                document.forms["alipayment"].TxtSubject.focus();
                return false;
            }

            document.forms["alipayment"].TxtBody.value = document.forms["alipayment"].TxtBody.value.replace(/\n/g, '');
            return true;
        }  

    </script>

    <style>
dl *, #head *{
	margin:0;
	padding:0;
}
ul,ol{
	list-style:none;
}
.title{
    color: #ADADAD;
    font-size: 14px;
    font-weight: bold;
    padding: 8px 16px 5px 10px;
}
.hidden{
	display:none;
}

.new-btn-login-sp{
	border:1px solid #D74C00;
	padding:1px;
	display:inline-block;
}

.new-btn-login{
    background-color: transparent;
    background-image: url("../images/new-btn-fixed.png");
    border: medium none;
}
.new-btn-login{
    background-position: 0 -198px;
    width: 82px;
	color: #FFFFFF;
    font-weight: bold;
    height: 28px;
    line-height: 28px;
    padding: 0 10px 3px;
}
.new-btn-login:hover{
	background-position: 0 -167px;
	width: 82px;
	color: #FFFFFF;
    font-weight: bold;
    height: 28px;
    line-height: 28px;
    padding: 0 10px 3px;
}
.bank-list{
	overflow:hidden;
	margin-top:5px;
}
.bank-list li{
	float:left;
	width:153px;
	margin-bottom:5px;
}

#main{
	width:750px;
	margin:0 auto;
	font-size:14px;
	font-family:'宋体';
}
#logo{
	background-color: transparent;
    background-image: url("../images/ips.jpg");
    border: medium none;
	background-position:0 0;
	width:196px;
	height:45px;
    float:left;
}
.red-star{
	color:#f00;
	width:10px;
	display:inline-block;
}
.null-star{
	color:#fff;
}
.content{
	margin-top:5px;
}

.content dt{
	width:100px;
	display:inline-block;
	text-align:right;
	float:left;
	
}
.content dd{
	margin-left:100px;
	margin-bottom:5px;
}
#foot{
	margin-top:10px;
}
.foot-ul li {
	text-align:center;
}
.note-help {
    color: #999999;
    font-size: 12px;
    line-height: 130%;
    padding-left: 3px;
}

.cashier-nav {
    font-size: 14px;
    margin: 15px 0 10px;
    text-align: left;
    height:30px;
    border-bottom:solid 2px #CFD2D7;
}
.cashier-nav ol li {
    float: left;
}
.cashier-nav li.current {
    color: #AB4400;
    font-weight: bold;
}
.cashier-nav li.last {
    clear:right;
}
.alipay_link {
    text-align:right;
}
.alipay_link a:link{
    text-decoration:none;
    color:#8D8D8D;
}
.alipay_link a:visited{
    text-decoration:none;
    color:#8D8D8D;
}
</style>
</head>
<body>
    <form id="alipayment" runat="server">
        <div id="main">
            <div id="head">
                <div id="logo">
                </div>
                <dl class="alipay_link">
                    <a target="_blank" href="http://www.alipay.com/"><span>首页</span></a>| <a target="_blank"
                        href="https://b.alipay.com/home.htm"><span>商家服务</span></a>| <a target="_blank" href="http://help.alipay.com/support/index_sh.htm">
                            <span>帮助中心</span></a>
                </dl>
                <span class="title">即时到帐付款快速通道</span>
                <!--<div id="title" class="title">支付宝即时到帐付款快速通道</div>-->
            </div>
            <div class="cashier-nav">
                <ol>
                    <li class="current">1、确认付款信息 →</li>
                    <li>2、付款 →</li>
                    <li class="last">3、付款完成</li>
                </ol>
            </div>
            <div id="body" style="clear: left">
                <dl class="content">
                    <dt>标题：</dt>
                    <dd>
                        <span class="red-star">*</span>
                        <asp:TextBox ID="TxtSubject" name="TxtSubject" runat="server" ReadOnly="True"></asp:TextBox><span>如：7月5日定货款。</span>
                    </dd>
                    <dt>付款金额：</dt>
                    <dd>
                        <span class="red-star">*</span>
                        <asp:TextBox ID="TxtTotal_fee" name="TxtTotal_fee" runat="server" onfocus="if(Number(this.value)==0){this.value='';}"
                            MaxLength="10"></asp:TextBox>
                        <span>如：112.21</span>
                    </dd>
                    <dt>备注：</dt>
                    <dd>
                        <span class="null-star">*</span>
                        <asp:TextBox ID="TxtBody" name="TxtBody" runat="server" MaxLength="100" TextMode="MultiLine"></asp:TextBox><br />
                        <span>（如联系方法，商品要求、数量等。100汉字内）</span>
                    </dd>
                    <dt></dt>
                    <dd>
                        <span class="new-btn-login-sp">
                            <asp:Button ID="BtnAlipay" name="BtnAlipay" class="new-btn-login" 
                            Text="确认付款" Style="text-align: center;"
                                runat="server" OnClick="BtnAlipay_Click" 
                            onclientclick="return CheckForm()" /></span></dd></dl>
            </div>
            <div id="foot">
                <ul class="foot-ul">
                    <li><font class="note-help">如果您点击“确认付款”按钮，即表示您同意向卖家购买此物品。
                        <br />
                        您有责任查阅完整的物品登录资料，包括卖家的说明和接受的付款方式。卖家必须承担物品信息正确登录的责任！ </font></li>
                </ul>
            </div>
            <div class="cashier-nav">
                <ol>
                    <li class="current">支付记录</li>
                </ol>
            </div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4" DataSourceID="sdsPaymentList" ForeColor="Black" 
                GridLines="Vertical">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="date_created" HeaderText="创建日期" 
                        SortExpression="date_created" />
                    <asp:BoundField DataField="amount" DataFormatString="{0:c}" HeaderText="支付金额" 
                        SortExpression="amount" />
                    <asp:TemplateField HeaderText="交易状态" SortExpression="status">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# GetStatus(Eval("status")) %>'></asp:Label>
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
            </asp:GridView>
        </div>
        <asp:SqlDataSource ID="sdsPaymentList" runat="server" 
            ConnectionString="<%$ ConnectionStrings:InsuranceAviation %>" 
            SelectCommand="SELECT [amount], [date_created], [status] FROM [t_Payment] WHERE ([payer] = @payer) ORDER BY [date_created] DESC">
            <SelectParameters>
                <asp:Parameter Name="payer" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>