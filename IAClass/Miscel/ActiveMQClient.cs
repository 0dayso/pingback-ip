using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using System.Collections;

namespace IAClass
{
    public enum ClientType
    {
        Producer, Consumer
    }

    public class ActiveMQClient : IDisposable
    {
        public  IConnection connection;
        private ArrayList sessionArray = new ArrayList();
        private ArrayList consumerArray = new ArrayList();
        private IMessageProducer producer;
        private MessageListener listener;
        private string queueName;
        private int sessionCount;

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            //if (!m_disposed)
            {
                if (disposing)
                {
                    foreach(ISession session in sessionArray)
                        session.Close();
                    if(connection != null)
                        connection.Close();
                }
                //Marshal.FreeCoTaskMem(m_unmanagedResource);
                //m_disposed = true;
            }
        }

        public void Close()
        {
            ((IDisposable)this).Dispose();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="queueName">消息队列的名称</param>
        /// <param name="sessionCount">并发的session数目</param>
        public ActiveMQClient(string queueName, int sessionCount)
        {
            if (sessionCount < 1)
                throw new Exception("并发数不能小于１,初始化失败!");
            this.queueName = queueName;
            this.sessionCount = sessionCount;
        }

        public void Start()
        {
            if (connection == null)
            {
                Common.LogIt("ActiveMQ Service Start..." + queueName);
                //使用failover前缀后,即使AMQ服务被重启,生产和消费也不会抛出异常(自动重连)
                //但同时会导致Start()的超时机制失效!
                IConnectionFactory factory = new ConnectionFactory("failover:tcp://localhost:61616");//"failover:tcp://localhost:61616?nms.PrefetchPolicy.QueuePrefetch=1"
                connection = factory.CreateConnection();

                Connection cnn = connection as Connection;
                cnn.PrefetchPolicy.QueuePrefetch = 1;//队列预取值
                //cnn.AsyncSend = true;//异步发送
                cnn.RequestTimeout = TimeSpan.FromSeconds(10);//设置cnn.Start()方法的超时
                cnn.Start();//Tcp连接开始真正建立
            }

            if (sessionArray.Count == 0)
            {
                for (int i = 0; i < sessionCount; i++)
                {
                    //以message.Acknowledge()主动应答的方式来使用事务;如不使用事务,则在stop()后,文本框中会发现有重复的消息
                    ISession session = connection.CreateSession(AcknowledgementMode.IndividualAcknowledge);
                    sessionArray.Add(session);
                }
            }
        }

        public void Stop()
        {
            foreach(IMessageConsumer consumer in consumerArray)
                //consumer.Close();//直接关闭,将导致message.Acknowledge()引发异常,破坏事务!
                consumer.Listener -= new MessageListener(listener);
        }

        public void EnqueueObject(object entity)
        {
            EnqueueObject(entity, 0);
        }

        /// <summary>
        /// 生产,入队列
        /// </summary>
        /// <param name="entity">对象</param>
        /// <param name="delay">延迟时间,单位毫秒</param>
        public void EnqueueObject(object entity, long delay)
        {
            if (producer == null)
            {
                if (sessionArray.Count == 0)
                    throw new Exception("队列服务尚未启动,请使用Start()方法.");
                
                ISession session = sessionArray[0] as ISession;
                producer = session.CreateProducer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(queueName));
            }

            IObjectMessage msg = producer.CreateObjectMessage(entity);
            if(delay >0)
                msg.Properties["AMQ_SCHEDULED_DELAY"] = delay;
            producer.Send(msg, Apache.NMS.MsgDeliveryMode.Persistent, Apache.NMS.MsgPriority.Normal, TimeSpan.MinValue);
            //msg.Acknowledge();//不需要??
        }

        /// <summary>
        /// 消费,出队列操作
        /// </summary>
        /// <param name="listener">监听方法</param>
        public void Dequeue(MessageListener listener)
        {
            this.listener = listener;

            if (consumerArray.Count == 0)
            {
                if (sessionArray.Count == 0)
                    throw new Exception("队列服务尚未启动,请使用Start()方法.");
                foreach(Session session in sessionArray)
                {
                    IDestination dest = session.GetQueue(queueName);
                    IMessageConsumer consumer = session.CreateConsumer(dest);
                    consumerArray.Add(consumer);
                }
            }

            foreach (IMessageConsumer consumer in consumerArray)
                consumer.Listener += new MessageListener(listener);
        }
    }
}
