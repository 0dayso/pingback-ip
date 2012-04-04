using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;

namespace IAClass
{
    public abstract class Unity
    {
        protected static IUnityContainer container;

        static Unity()
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
    }
}
