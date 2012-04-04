using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using IAClass.Entity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace IAClass.Issuing
{
    public class IssuingFacade : Unity
    {
        IIssuing instance;

        /// <summary>
        /// 投保代理
        /// 返回保单号，则表示投保成功
        /// Trace.ErrorMsg为空，而Trace.Detail不为空，表示投保失败，但仍然接单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IssuingResultEntity Issue(IssueEntity entity)
        {
            IssuingResultEntity result;

            try
            {
                //注入实例
                instance = container.Resolve<IIssuing>(entity.IOC_Class_Alias);
            }
            catch (Exception e)
            {
                Common.LogIt(e.ToString());
                throw;
            }

            result = instance.Issue(entity);
            return result;
        }

        /// <summary>
        /// 退保代理
        /// </summary>
        /// <param name="policyNo"></param>
        /// <returns></returns>
        public TraceEntity Withdraw(WithdrawEntity entity)
        {
            TraceEntity result = new TraceEntity();

            try
            {
                //注入实例
                instance = container.Resolve<IIssuing>(entity.IOC_Class_Alias);
            }
            catch (Exception e)
            {
                Common.LogIt(e.ToString());
                throw;
            }

            result = instance.Withdraw(entity);
            return result;
        }

        /// <summary>
        /// 检查数据完整性
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TraceEntity Validate(IssueEntity entity)
        {
            TraceEntity result = new TraceEntity();

            try
            {
                //注入实例
                instance = container.Resolve<IIssuing>(entity.IOC_Class_Alias);
            }
            catch (Exception e)
            {
                Common.LogIt(e.ToString());
                throw;
            }

            result = instance.Validate(entity);
            return result;
        }
    }
}
