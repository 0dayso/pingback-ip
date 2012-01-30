using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
namespace IAClass.Bussiness
{
    public class Payment
    {
        public static string CreateOrder(string gateway, decimal amount)
        {
            string strSql = "insert into t_Payment(gateway, amount, payer, status) output inserted.id values ('{0}', {1}, '{2}', 'waiting_for_payment')";
            strSql = string.Format(strSql, gateway, amount, HttpContext.Current.User.Identity.Name);
            object obj = SqlHelper.ExecuteScalar(Common.ConnectionString, System.Data.CommandType.Text, strSql);
            return obj.ToString();
        }

        public static void Pay(string orderId, string payer_tradeNo)
        {
            SqlHelper.ExecuteNonQuery(Common.ConnectionString, "PayingCallback", orderId, payer_tradeNo);
        }
    }
}
