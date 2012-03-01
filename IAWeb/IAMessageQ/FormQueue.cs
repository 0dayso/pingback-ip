using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Configuration;
using log4net;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using IAClass.Entity;
using IAClass;
using IAClass.Issuing;
//using Amib.Threading;

namespace IAMessageQ
{
    public partial class FormQueue : Form
    {
        int countRunningThread;
        bool IsRunning = false;
        //SmartThreadPool stp = new SmartThreadPool();
        ActiveMQClient MQClient;
        Thread workItemsProducerThread;
        string AppName = ConfigurationManager.AppSettings["AppName"];
        string QueueName;
        /// <summary>
        /// 正常处理消息实体的方法
        /// </summary>
        Func<object, TraceEntity> NormalWork;
        /// <summary>
        /// 失败后的处理消息实体方法
        /// </summary>
        Func<object, TraceEntity, string> FailureWork;
        /// <summary>
        /// 显示消息实体的方法
        /// </summary>
        Func<object, string> MessageToString;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="normalWork">正常处理消息实体的方法</param>
        /// <param name="failureWork">失败后的处理消息实体方法</param>
        /// <param name="messageToString">显示消息实体的方法</param>
        public FormQueue(string queueName, Func<object, TraceEntity> normalWork, Func<object, TraceEntity, string> failureWork, Func<object, string> messageToString)
        {
            InitializeComponent();

            this.Text += " - " + AppName;
            this.QueueName = queueName;
            this.NormalWork = normalWork;
            this.MessageToString = messageToString;

            if (failureWork != null)
                this.FailureWork = failureWork;
            else
                this.FailureWork = defaultFailureWork;
        }

        /// <summary>
        /// 更改控件状态
        /// </summary>
        /// <param name="isStart">true-启动 false-停止</param>
        private void UpdateControls(bool isStart)
        {
            txtLogInfo.AppendText(isStart ? "队列启动…" : "队列暂停…");
            txtLogInfo.AppendText(Environment.NewLine);
            this.IsRunning = isStart;
            btnIssueStart.Enabled = !isStart;
            btnIssueStop.Enabled = isStart;
            nudThreads.Enabled = !isStart;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            UpdateControls(true);
            workItemsProducerThread = new Thread(MessageProducer);
            workItemsProducerThread.IsBackground = true;
            workItemsProducerThread.Start();
            //MessageProducer();
        }

        //private void WorkItemsProducer()
        //{
        //    mqArray = new MessageQueue[(int)nudThreads.Value];
        //    for (int i = 0; i < nudThreads.Value; i++)
        //    {
        //        mqArray[i] = new MessageQueue();
        //        stp.QueueWorkItem(Dequeue, mqArray[i]);
        //    }
        //    stp.WaitForIdle();
        //    stp.Shutdown();
        //}

        private void MessageProducer()
        {
            try
            {
                if(MQClient == null)
                    MQClient = new ActiveMQClient(this.QueueName, (int)nudThreads.Value);
                MQClient.Start();
                MQClient.Dequeue(consumer_Listener);
            }
            catch(Exception e)
            {
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    txtLogInfo.AppendText(e.ToString());
                    UpdateControls(false);
                }));
            }
        }

        void SetRunningThreadCount(bool plus)
        {
            if (plus)
                Interlocked.Increment(ref countRunningThread);
            else
                Interlocked.Decrement(ref countRunningThread);

            this.BeginInvoke(new MethodInvoker(delegate
            {
                lblCountIdle.Text = countRunningThread.ToString();
                if (countRunningThread == 0 && !IsRunning)
                    this.btnIssueStart.Enabled = true;
            }));
        }

        void consumer_Listener(IMessage message)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                SetRunningThreadCount(true);
                IObjectMessage amqMsg = message as IObjectMessage;
                sb.Append(MessageToString(amqMsg.Body));

                if (IsRunning)
                {
                    //事务开始
                    TraceEntity result = NormalWork(amqMsg.Body);
                    if (string.IsNullOrEmpty(result.ErrorMsg))
                    {
                        message.Acknowledge();//事务结束,出队列确认
                        sb.Append(" 完成！");
                        sb.AppendLine(result.Detail);//显示保单号
                    }
                    else
                    {
                        //if (IsRunning)
                        {
                            string fail = FailureWork(amqMsg.Body, result);
                            message.Acknowledge();//事务结束
                            sb.AppendLine(fail);
                        }
                        //else
                        //{
                        //    sb.Append(" 失败:"); sb.Append(result.ErrorMsg);
                        //    sb.AppendLine(" 回滚!");
                        //    throw new Exception("rollback!");
                        //}
                    }
                    //这里若用Invoke将导致一个非常隐秘的Bug
                    //现象：点击Stop按钮后，应用程序将挂起
                    //原因:A:主线程在Stop中因调用ActiveMQ session.close()而阻塞等待；
                    //     B：ActiveMQ session.close()由于listener中调用了Invoke主线程更新界面而阻塞等待
                    //     A和B产生死锁
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        txtLogInfo.AppendText(sb.ToString());
                    }));
                }
            }
            catch (Exception e)
            {
                string error = string.Format("{3}{0} : 消息{1} {2}{4}   发生异常,消息事务回滚!",
                    DateTime.Now.ToLongTimeString(), message.NMSMessageId, e.ToString(), Environment.NewLine, Environment.NewLine);
                Common.LogIt(error);
                sb.AppendLine(error);
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    txtLogInfo.AppendText(sb.ToString());
                }));

                throw;//直接抛出异常,引发Redelivery(默认最多7次,之后被移至死信队列)
            }
            finally
            { SetRunningThreadCount(false); }
        }

        /// <summary>
        /// 失败后的默认处理方法
        /// </summary>
        /// <param name="objEntity">消息实体</param>
        /// <param name="result">NormalWork的返回结果</param>
        /// <returns>反馈信息</returns>
        string defaultFailureWork(object objEntity, TraceEntity result)
        {
            StringBuilder sb = new StringBuilder();
            MessageEntity entity = objEntity as MessageEntity;

            if (entity.RedeliveryCount < entity.MaxRedelivery)
            {
                //重发,并设置延迟时间,每次重发延迟时间加倍
                entity.RedeliveryCount++;
                MQClient.EnqueueObject(entity, 60 * 1000 * entity.MinDelayMinutes * entity.RedeliveryCount);
                //message.Acknowledge();//事务结束
                sb.Append(" 失败:"); sb.Append(result.ErrorMsg);
                sb.Append(string.Format(" {0}分钟后重发!", entity.MinDelayMinutes * entity.RedeliveryCount));
            }
            else
            {
                //message.Acknowledge();//事务结束
                sb.Append(" 失败:"); sb.Append(result.ErrorMsg);
                sb.Append(string.Format(" {0}次重发失败,放弃!", entity.MaxRedelivery));
                Common.LogIt(sb.ToString());
            }

            return sb.ToString();
        }

        private void btnIssueStop_Click(object sender, EventArgs e)
        {
            UpdateControls(false);

            new Thread(() =>
            {
                MQClient.Stop();
            }).Start();

            //workItemsProducerThread.Interrupt();
            //stp.Shutdown(true, 3000);
            //stp.Dispose();
            //stp = null;
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
        }

        private void FormQueue_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MQClient != null)
                MQClient.Close();
        }
    }
}
