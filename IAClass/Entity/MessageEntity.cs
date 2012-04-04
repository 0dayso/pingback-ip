using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAClass.Entity
{
    /// <summary>
    /// 消息队列中实体的抽象基类
    /// </summary>
    [Serializable]
    public abstract class MessageEntity : UnityEntity
    {
        /// <summary>
        /// 消息已被重发的次数
        /// </summary>
        public int RedeliveryCount;
        /// <summary>
        /// 消息的最大重发次数,默认5次
        /// </summary>
        public int MaxRedelivery = 5;
        /// <summary>
        /// 最小重发延迟时间,单位:分钟,默认30分钟
        /// </summary>
        public int MinDelayMinutes = 30;
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString;
    }
}
