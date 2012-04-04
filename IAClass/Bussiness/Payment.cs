using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using IAClass.Entity;
using IAClass.Payment;
using System.Data;
using System.Configuration;

namespace IAClass.Bussiness
{
    public class Payment
    {
        public static string CreateOrder(PaymentEntity entity)
        {
            string strSql = @"
insert into t_Payment(gateway, website, amount, payer, orderID_salve, url_return_slave, url_callback_slave, status)
output inserted.id
values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', 1)";
            strSql = string.Format(strSql, entity.Gateway, entity.Website, entity.Amount, entity.Payer,
                                            entity.OrderID_Salve, entity.URL_Return_Slave, entity.URL_Callback_Slave);
            object obj = SqlHelper.ExecuteScalar(Common.ConnectionString, System.Data.CommandType.Text, strSql);
            return obj.ToString();
        }

        public static void Transfer(PaymentEntity entity)
        {
            new BankFacade().Transfer(entity);
        }

        public static void TransferByMasterSite(PaymentEntity entity)
        {
            string urlReturn = "http://{0}/public/alipay_return.aspx";
            string urlCallback = "http://{0}/public/alipay_notify.aspx";
            urlReturn = string.Format(urlReturn, HttpContext.Current.Request.Url.Host);
            urlCallback = string.Format(urlCallback, HttpContext.Current.Request.Url.Host);

            string urlPay = "http://www.yoyoyn.cn/public/pay.aspx?website={0}&amount={1}&title={2}&desc={3}&payer={4}&OrderID_Salve={5}&URL_Return_Slave={6}&URL_Callback_Slave={7}";
            urlPay = string.Format(urlPay, entity.Website, entity.Amount, entity.Title, entity.Description, HttpContext.Current.User.Identity.Name, entity.OrderId, urlReturn, urlCallback);
            HttpContext.Current.Response.Redirect(urlPay);
        }

        public static bool Callback(string orderId, string payer_tradeNo)
        {
            //if (HttpContext.Current.Request.Url.Host == Common.PaymentDomainName)//若是主站
            {
                DataSet ds = SqlHelper.ExecuteDataset(Common.ConnectionString, "PayingCallback", orderId, payer_tradeNo);

                if (ds.Tables.Count != 0)
                {
                    //DataRow dr = ds.Tables[0].Rows[0];
                    //string URL_Return_Slave = dr["URL_Return_Slave"].ToString();
                    //string URL_Callback_Slave = dr["URL_Callback_Slave"].ToString();

                    ////转发以通知附属站点
                    //if (!string.IsNullOrEmpty(URL_Return_Slave))
                    //{
                    //    string para = Common.GetRequestGet();
                    //    System.Web.HttpContext.Current.Response.Redirect(URL_Return_Slave + "?" + para);
                    //}

                    //if (!string.IsNullOrEmpty(URL_Callback_Slave))
                    //{
                    //    //Common.AQ_Payment.EnqueueObject(orderId);
                    //}

                    return true;
                }
                else//无效订单号
                {
                    string error = string.Format("订单确认失败!orderId={0} tradeNo={1}", orderId, payer_tradeNo);
                    Common.LogIt(error);
                    return false;
                }
            }
            //else//附属站
            //{
            //    string orderId_Slave = Common.HttpGet("http://www.yoyoyn.cn/public/GetSlaveOrderId.aspx?orderId=" + orderId);
            //    DataSet ds = SqlHelper.ExecuteDataset(Common.ConnectionString, "PayingCallback", orderId_Slave, payer_tradeNo);
            //    if (ds.Tables.Count != 0)
            //    {
            //        string error = string.Format("订单确认失败!orderId={0} tradeNo={1}", orderId_Slave, payer_tradeNo);
            //        Common.LogIt(error);
            //        return false;
            //    }
            //    else
            //        return false;
            //}
        }

        public static string GetSlaveOrderId(string orderId)
        {
            string strSql = "SELECT orderID_salve FROM [t_Payment] where id = '" + orderId + "'";
            object obj = SqlHelper.ExecuteScalar(Common.ConnectionString, CommandType.Text, strSql);
            if (obj != null)
                return obj.ToString();
            else
                return "-1";
        }
    }
}
