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
        MessageQClient MQClient;
        Thread workItemsProducerThread;
        string AppName = ConfigurationManager.AppSettings["AppName"];
        string QueueName;
        /// <summary>
        /// 处理消息实体的方法
        /// </summary>
        Func<object, TraceEntity> Consume;
        /// <summary>
        /// 显示消息实体的方法
        /// </summary>
        Func<object, string> MessageToString;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="consume">处理消息实体的方法</param>
        /// <param name="messageToString">显示消息实体的方法</param>
        public FormQueue(string queueName, Func<object, TraceEntity> consume, Func<object, string> messageToString)
        {
            InitializeComponent();

            this.Text += " - " + AppName;
            this.QueueName = queueName;
            this.Consume = consume;
            this.MessageToString = messageToString;
            MQClient = new MessageQClient(this.QueueName, (int)nudThreads.Value);
        }

        /// <summary>
        /// 更改控件状态
        /// </summary>
        /// <param name="isStart">true-启动 false-停止</param>
        private void UpdateControls(bool isStart)
        {
            txtLogInfo.AppendText(isStart ? "队列启动…" : "队列暂停…");
            txtLogInfo.AppendText(System.Environment.NewLine);
            this.IsRunning = isStart;
            btnIssueStart.Enabled = false;
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
                    MQClient = new MessageQClient(this.QueueName, (int)nudThreads.Value);
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
                if (countRunningThread == 0)
                    this.btnIssueStart.Enabled = true;
            }));
        }

        void consumer_Listener(IMessage message)
        {
            try
            {
                SetRunningThreadCount(true);
                IObjectMessage amqMsg = message as IObjectMessage;
                StringBuilder sb = new StringBuilder();
                sb.Append(MessageToString(amqMsg.Body));

                if (IsRunning)
                {
                    //事务开始
                    TraceEntity result = Consume(amqMsg.Body);
                    if (string.IsNullOrEmpty(result.ErrorMsg))
                    {
                        message.Acknowledge();//事务结束,出队列确认
                        sb.AppendLine(" 出单成功!");
                    }
                    else
                    {
                        if (IsRunning)
                        {
                            //重发,并设置延迟时间为一小时
                            MQClient.EnqueueObject(amqMsg.Body, 60 * 1000 * 30);
                            message.Acknowledge();//事务结束,出队列确认
                            sb.Append(" 失败:"); sb.Append(result.ErrorMsg);
                            sb.AppendLine(" 1小时候后重发!");
                        }
                        else
                        {
                            sb.Append(" 失败:"); sb.Append(result.ErrorMsg);
                            sb.AppendLine(" 回滚!");
                        }
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
            catch (Apache.NMS.NMSException e)//"The Consumer has been Closed" 点击Stop按钮后consumer被关闭,由message.Acknowledge()引发异常
            {
                //已出单,却未能提交事务;将导致少量重复投保.
                string error = message.NMSMessageId + " : " + e.ToString() + System.Environment.NewLine;
                Common.LogIt(error);
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    txtLogInfo.AppendText(error);
                }));
            }
            catch (Exception e)
            {
                string error = message.NMSMessageId + " : " + e.ToString() + System.Environment.NewLine;
                Common.LogIt(error);
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    txtLogInfo.AppendText(error);
                }));
            }
            finally
            { SetRunningThreadCount(false); }
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
            MQClient.Close();
        }
    }
}
