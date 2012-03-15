using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using IAClass.Entity;

namespace IAClass.SMS
{
    public class SMSFacade
    {
        static IUnityContainer container;
        ISMS issuingInstance;

        /// <summary>
        /// 静态构造函数,优先于普通构造函数，只执行一次，被所有实例共享
        /// </summary>
        static SMSFacade()
        {
            Initiate();
        }

        /// <summary>
        /// 普通构造函数
        /// </summary>
        public SMSFacade()
        {
            Initiate();
        }

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

        public TraceEntity Send(SMSEntity entity)
        {
            TraceEntity result;

            try
            {
                //注入实例
                issuingInstance = container.Resolve<ISMS>(entity.IOC_Class_Alias);
            }
            catch (Exception e)
            {
                Common.LogIt(e.ToString());
                throw;
            }

            result = issuingInstance.Send(entity);
            return result;
        }

        public int GetBalance(string IOC_Class_Alias)
        {
            try
            {
                //注入实例
                issuingInstance = container.Resolve<ISMS>(IOC_Class_Alias);
            }
            catch (Exception e)
            {
                Common.LogIt(e.ToString());
                throw;
            }

            int result = issuingInstance.GetBalance(IOC_Class_Alias);
            return result;
        }
    }
}
