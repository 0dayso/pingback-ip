using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Public_PaypalCallback : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        foreach (object a in Request.Form)
        {
            string name = a.ToString();
            Response.Write(name + ":" + Request.Form[name] + "<br>");
        }
        /* 返回变量输出如下：
transaction_subject:���ó�ֵ
txn_type:web_accept
payment_date:02:44:47 Jul 28, 2011 PDT
last_name:��
residence_country:CN
item_name:���ó�ֵ
payment_gross:
mc_currency:CNY
business:2140361@qq.com
payment_type:instant
protection_eligibility:Ineligible
payer_status:unverified
verify_sign:AG4zAOQDzeuizqRLtYwXzek24VyaAjkhgJO.7SPCJsgLWZ82wFtLTYAj
tax:0.00
payer_email:pingback@163.com
txn_id:3BP30702SP2242804
quantity:1
receiver_email:2140361@qq.com
first_name:����
payer_id:LRQGA9PDRWA2A
receiver_id:8JRKA4A8R283U
item_number:
handling_amount:0.00
payment_status:Completed
shipping:0.00
mc_gross:0.01
custom:
charset:gb2312
notify_version:3.2
merchant_return_link:返回到惠旅
         * */
    }
}