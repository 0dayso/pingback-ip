using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAClass.Entity
{
    [Serializable]
    public abstract class UnityEntity
    {
        /// <summary>
        /// IOC容器中注册的对象名称（name属性，非类名）
        /// </summary>
        public string IOC_Class_Alias;
        /// <summary>
        /// 接口类所需要的参数
        /// </summary>
        public string IOC_Class_Parameters;
    }
}
