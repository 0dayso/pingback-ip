using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAClass.Entity
{
    public class PaymentEntity : UnityEntity
    {
        public string Gateway;
        public string Website;
        public string OrderId;
        public decimal Amount;
        public string Title;
        public string Description = "";
        public string Payer;
        /// <summary>
        /// 附加信息
        /// </summary>
        public string Attach = "";
        /// <summary>
        /// 附属站点订单号
        /// </summary>
        public string OrderID_Salve;
        /// <summary>
        /// 附属站点的返回页面地址
        /// </summary>
        public string URL_Return_Slave = "";
        /// <summary>
        /// 附属站点的回调地址
        /// </summary>
        public string URL_Callback_Slave = "";
    }

    public class PayingCallbackEntity : UnityEntity
    {
        ///// <summary>
        ///// 回调参数
        ///// </summary>
        //public System.Web.HttpRequest HttpRequest;
    }
}
