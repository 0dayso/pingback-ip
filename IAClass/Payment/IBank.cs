using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAClass.Entity;

namespace IAClass.Payment
{
    /// <summary>
    /// 支付接口
    /// 通常支付功能必须参数有:订单号,订单金额,订单标题,订单描述
    /// </summary>
    interface IBank
    {
        void Transfer(PaymentEntity entity);

        void Callback_Return(PayingCallbackEntity entity);

        void Callback_Notify(PayingCallbackEntity entity);
    }
}
