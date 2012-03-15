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
    public class IssuingFacade
    {
        static IUnityContainer container;
        IIssuing issuingInstance;

        /// <summary>
        /// 静态构造函数,优先于普通构造函数，只执行一次，被所有实例共享
        /// </summary>
        static IssuingFacade()
        {
            Initiate();
        }

        /// <summary>
        /// 普通构造函数
        /// </summary>
        //public IssuingFacade()
        //{
        //    Initiate();
        //}

        /// <summary>
        /// 初始化，注册容器。
        /// 注意：所有配置文件中的注册的对象，需已经存在，否则出错
        /// </summary>
        static void Initiate()
        {
            if (container == null)
            {
                try
                {

                    //注册对象
                    container = new UnityContainer();
                    UnityConfigurationSection section = ConfigurationManager.GetSection("unity") as UnityConfigurationSection;
                    section.Containers.Default.Configure(container);
                }
                catch (Exception e)
                {
                    container = null;
                    Common.LogIt(e.ToString());
                    throw;
                }
            }
        }

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
                issuingInstance = container.Resolve<IIssuing>(entity.IOC_Class_Alias);
            }
            catch (Exception e)
            {
                Common.LogIt(e.ToString());
                throw;
            }

            result = issuingInstance.Issue(entity);
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
                issuingInstance = container.Resolve<IIssuing>(entity.IOC_Class_Alias);
            }
            catch (Exception e)
            {
                Common.LogIt(e.ToString());
                throw;
            }

            result = issuingInstance.Withdraw(entity);
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
                issuingInstance = container.Resolve<IIssuing>(entity.IOC_Class_Alias);
            }
            catch (Exception e)
            {
                Common.LogIt(e.ToString());
                throw;
            }

            result = issuingInstance.Validate(entity);
            return result;
        }
    }
}
