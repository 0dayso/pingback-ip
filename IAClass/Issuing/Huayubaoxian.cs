using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAClass;
using IAClass.Entity;

namespace Huayubaoxian
{
    public class Issuing : IAClass.Issuing.IIssuing
    {
        static Service webService = new Service();
        static Random random = new Random();

        /// <summary>
        /// 投保
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IssuingResultEntity Issue(IssueEntity entity)
        {
            IssuingResultEntity result = new IssuingResultEntity();
            policy po = new policy();
            po.userid = "hgy02";
            po.password = "hgy02";
            po.PlanNo = "";
            po.begdate = entity.EffectiveDate.ToString("yyyy-MM-dd");
            po.App_name = entity.Name;
            po.IDType = GetIdType(entity.IDType);
            po.App_id = entity.ID;
            po.App_cellno = entity.PhoneNumber;
            po.GoflightNo = entity.FlightNo;
            po.OPER_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (string.IsNullOrEmpty(po.App_cellno))
            {
                //模拟电话号码
                string no = DateTime.Now.Ticks.ToString();
                string[] type = { "130", "131", "132", "155", "156", "186", "133", "153", "189", "180" };
                int index = random.Next(0, type.Length);
                po.App_cellno = type[index] + no.Substring(no.Length - 8);
            }

            string ret = string.Empty;
            try
            {
                ret = webService.confirmpolicy(po);

                if (string.IsNullOrEmpty(ret))
                    throw new Exception("Huayubaoxian WebService返回为空！");
            }
            catch(Exception e)
            {
                Common.LogIt(webService.Url + e.ToString());
                result.Trace.Detail = e.Message;
                return result;
            }

            //Common.LogIt("Huayubaoxian Issue：" + ret);
            if (Common.IsNumeric(ret))
                result.PolicyNo = ret;
            else
            {
                string request = Common.XmlSerializer<policy>(po);
                Common.LogIt("投保参数" + request + System.Environment.NewLine + "Huayubaoxian投保：" + ret);
                result.Trace.ErrorMsg = ret;
            }
            return result;
        }

        /// <summary>
        /// 退保
        /// </summary>
        /// <param name="policyNo"></param>
        /// <returns></returns>
        public TraceEntity Withdraw(WithdrawEntity entity)
        {
            TraceEntity result = new TraceEntity();
            result.ErrorMsg = "该产品仅支持退单。";
            return result;
            //string ret = string.Empty;
            //try
            //{
            //    ret = webService.quitpolicy("hgy02", "hgy02", entity.PolicyNo);

            //    if (string.IsNullOrEmpty(ret))
            //        throw new Exception("WebService返回为空！");
            //    else
            //    {
            //        if (ret.Contains("已经退保"))
            //            return result;
            //        else
            //        {
            //            Common.LogIt("Huayubaoxian 退保：" + ret);
            //            result.ErrorMsg = ret;
            //            return result;
            //        }
            //    }
            //}
            //catch
            //{
            //    Common.LogIt(webService.Url);
            //    throw;
            //}
        }

        static string GetIdType(IdentityType type)
        {
            switch (type)
            {
                case IdentityType.身份证:
                    return "01";
                case IdentityType.护照:
                    return "02";
                case IdentityType.军官证:
                    return "03";
                case IdentityType.港澳通行证:
                    return "99";
                default:
                    return "99";//其他
            }
        }
    }
}
