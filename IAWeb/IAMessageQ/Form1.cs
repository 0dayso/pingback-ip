using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Configuration;
using IAClass.Entity;

namespace IAMessageQ
{
    public partial class Form1 : Form
    {
        string AppName = ConfigurationManager.AppSettings["AppName"];
        bool exit = false;

        public Form1()
        {
            InitializeComponent();
            this.Text += " - " + AppName;
            this.notifyIcon1.Text = this.Text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AddForm(tabPage1, Common.Queue_Issuing);
            AddForm(tabPage2, Common.Queue_SMS);
            AddForm(tabPage3, Common.Queue_Withdraw);
        }

        void AddForm(TabPage page, string queueName)
        {
            FormQueue fm = new FormQueue(queueName, Issue, null, IssueMessageToString);
            fm.TopLevel = false;
            fm.Dock = DockStyle.Fill;
            page.Controls.Add(fm);
            fm.Show();
        }

        TraceEntity Issue(object message)
        {
            IssueEntity entity = (IssueEntity)message;
            IssuingResultEntity result = Case.IssueAsync(entity);
            return result.Trace;
        }

        string IssueMessageToString(object message)
        {
            IssueEntity entity = (IssueEntity)message;
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToLongTimeString());
            sb.Append(" : 线程"); sb.Append(Thread.CurrentThread.ManagedThreadId);
            sb.Append(" "); sb.Append(entity.Name);
            sb.Append(" "); sb.Append(entity.ID);
            sb.Append(" "); sb.Append(entity.CaseNo);
            return sb.ToString();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!exit)
            {
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                e.Cancel = true;
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            打开ToolStripMenuItem_Click(sender, e);
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void 退出QToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                //不需要 Application.Exit();会触发所有的Form.Closing
                //foreach (TabPage page in tabControl1.TabPages)
                //{
                //    foreach (var ctr in page.Controls)
                //    {
                //        if (ctr is FormQueue)
                //        {
                //            ((FormQueue)ctr).Close();
                //            break;
                //        }
                //    }
                //}

                exit = true;
                Application.Exit();
                //Environment.Exit(0);
            }
        }
    }
}