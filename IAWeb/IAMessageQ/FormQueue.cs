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
        MessageConfig messageConfig;
        /// <summary>
        /// 正常处理消息实体的方法
        /// </summary>
        Func<MessageEntity, TraceEntity> NormalWork;
        /// <summary>
        /// 失败后的处理消息实体方法
        /// </summary>
        Func<MessageEntity, TraceEntity, string> FailureWork;
        /// <summary>
        /// 显示消息实体的方法
        /// </summary>
        Func<MessageEntity, string> MessageToString;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="normalWork">正常处理消息实体的方法</param>
        /// <param name="failureWork">失败后的处理消息实体方法</param>
        /// <param name="messageToString">显示消息实体的方法</param>
        public FormQueue(MessageConfig messageConfig, Func<MessageEntity, TraceEntity> normalWork, Func<MessageEntity, TraceEntity, string> failureWork, Func<MessageEntity, string> messageToString)
        {
            InitializeComponent();

            this.messageConfig = messageConfig;
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
                    MQClient = new ActiveMQClient(messageConfig.QueueName, (int)nudThreads.Value);
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
                MessageEntity entity = amqMsg.Body as MessageEntity;
                sb.Append(MessageToString(entity));

                if (IsRunning)
                {
                    //事务开始
                    TraceEntity result = new TraceEntity();
                    try
                    {
                        result = NormalWork(entity);
                    }
                    catch (Exception e)
                    {
                        Common.LogIt(e.ToString());
                        result.ErrorMsg = e.Message;
                    }

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
                            string fail = FailureWork(entity, result);
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
                    DateTime.Now.ToLongTimeString(), message.NMSMessageId, e.Message, Environment.NewLine, Environment.NewLine);
                Common.LogIt(error);
                sb.AppendLine(error);
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    txtLogInfo.AppendText(sb.ToString());
                }));

                throw;//直接抛出异常,将立即触发Redelivery(ActiveMQ默认最多7次,之后被移至死信队列)
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
        string defaultFailureWork(MessageEntity entity, TraceEntity result)
        {
            StringBuilder sb = new StringBuilder();

            if (result.ErrorMsg.ToLower().Contains("special redelivery"))
            {
                //自定义重发,并设置延迟时间为delayDay天后
                int delayDay;
                if (int.TryParse(result.Detail, out delayDay))
                {
                    MQClient.EnqueueObject(entity, delayDay * 24 * 3600 * 1000L);
                    sb.Append(" 推迟:");
                    sb.AppendLine(string.Format(" {0}天后重发!", delayDay));
                    return sb.ToString();
                }
                else
                    sb.AppendLine(" special redelivery 未指明延迟时间，尝试按默认机制重发！");
            }

            if (entity.RedeliveryCount < entity.MaxRedelivery)
            {
                //默认重发,每次重发延迟时间加倍
                entity.RedeliveryCount++;
                MQClient.EnqueueObject(entity, entity.MinDelayMinutes * entity.RedeliveryCount * 60 * 1000);
                sb.Append(" 失败:"); sb.Append(result.ErrorMsg);
                sb.Append(string.Format(" {0}分钟后重发!", entity.MinDelayMinutes * entity.RedeliveryCount));
            }
            else
            {
                sb.Append(" 失败:");
                sb.Append(result.ErrorMsg);
                sb.Append(string.Format(" {0}次重发失败,放弃!", entity.MaxRedelivery));
            }

            StringBuilder sbLog = new StringBuilder();
            sbLog.Append(MessageToString(entity));
            sbLog.Append(sb.ToString());
            Common.LogIt(sbLog.ToString());
            return sb.ToString();
        }

        private void btnIssueStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void Stop()
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
            if (MQClient != null)
            {
                Stop();
                while (countRunningThread != 0 || IsRunning)
                {
                    Thread.Sleep(5000);
                }

                MQClient.Close();
            }
        }

        bool stopScanning = true;

        private void button1_Click(object sender, EventArgs e)
        {
            stopScanning = !stopScanning;

            if (stopScanning)
            {
                btnScan.Text = "Scan";
                timer1.Enabled = false;
            }
            else
            {
                btnScan.Text = "Stop";
                timer1.Enabled = true;
            }
        }

        private void Scan()
        {
            DataSet ds;
            string strsql = @"
select * from t_Case a 
 inner join t_interface b on a.interface_id = b.id
 where (CertNo is null or caseNo = CertNo)
 and (IssuingFailed is null or IssuingFailed not like '%份数超限%')
 and customerFlightDate > GETDATE()
 and enabled = 1
";
            ds = SqlHelper.ExecuteDataset(Common.ConnectionString, CommandType.Text, strsql);

            using (System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection(Common.ConnectionString))
            {
                cnn.Open();
                System.Data.SqlClient.SqlCommand cmm = new System.Data.SqlClient.SqlCommand("", cnn);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (stopScanning)
                        break;
                    IssueEntity entity = new IssueEntity();
                    entity.Name = dr["customerName"].ToString();
                    entity.ID = dr["customerID"].ToString();
                    entity.IDType = IdentityType.其他证件;
                    entity.Gender = dr["customerGender"].ToString() == "男" ? Gender.Male : Gender.Female;
                    entity.Birthday = DateTime.Parse(dr["customerBirth"].ToString());
                    //如果是今天之后的乘机日期,则时间部分置为0
                    DateTime dtf = DateTime.Parse(dr["customerFlightDate"].ToString());
                    int caseDuration = Convert.ToInt32(dr["caseDuration"]);
                    entity.EffectiveDate = dtf > DateTime.Today ? dtf.Date : dtf;
                    entity.ExpiryDate = dtf.AddDays(caseDuration - 1);
                    entity.PhoneNumber = dr["customerPhone"].ToString();
                    entity.FlightNo = dr["customerFlightNo"].ToString();

                    entity.IsLazyIssue = false;
                    entity.DbCommand = cmm;
                    entity.IOC_Class_Alias = dr["IOC_Class_Alias"].ToString();
                    entity.IOC_Class_Parameters = dr["IOC_Class_Parameters"].ToString();
                    entity.CaseNo = dr["caseNo"].ToString();
                    entity.CaseId = dr["caseId"].ToString();
                    entity.InterfaceId = Convert.ToInt32(dr["interface_Id"]);
                    //entity.Title = productName.ToString();

                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        txtLogInfo.AppendText(MessageToString(entity));
                    }));

                    IssuingResultEntity result = new IssuingResultEntity();
                    try
                    {
                        result = Case.Issue(entity);
                    }
                    catch (Exception ee)
                    {
                        StringBuilder sbLog = new StringBuilder();
                        sbLog.AppendLine(MessageToString(entity));
                        sbLog.Append(ee.ToString());
                        Common.LogIt( sbLog.ToString());
                        result.Trace.ErrorMsg = ee.Message;
                    }

                    if (string.IsNullOrEmpty(result.Trace.ErrorMsg))
                    {
                        //strsql = @"update t_Case set CertNo = '{0}' where caseNo = '{1}'";
                        //strsql = string.Format(strsql, result.PolicyNo, entity.CaseNo);
                        //SqlHelper.ExecuteNonQuery(Common.ConnectionString, CommandType.Text, strsql);
                        this.BeginInvoke(new MethodInvoker(delegate
                            {
                                txtLogInfo.AppendText(" " + result.PolicyNo + Environment.NewLine);
                            }));
                    }
                    else
                        this.BeginInvoke(new MethodInvoker(delegate
                            {
                                txtLogInfo.AppendText(" " + result.Trace.ErrorMsg + Environment.NewLine);
                            }));
                }

                cnn.Close();
            }

            if (!stopScanning)
            {
                this.BeginInvoke(new MethodInvoker(delegate
                    {
                        timer1.Enabled = true;
                    }));
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Thread td = new Thread(Scan);
            td.Start();
        }
    }
}
