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
        bool exit = false;

        public Form1()
        {
            InitializeComponent();
            this.notifyIcon1.Text = this.Text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Close();

            XMLConfigMQ configList = new XMLConfigMQ().Read() as XMLConfigMQ;

            foreach (var config in configList.MessageConfigList)
            {
                AddForm(config);
            }
        }

        void AddForm(MessageConfig config)
        {
            TabPage page = new System.Windows.Forms.TabPage();
            page.Text = config.AppName;
            page.UseVisualStyleBackColor = true;
            tabControl1.Controls.Add(page);

            FormQueue fm = new FormQueue(config, Issue, null, IssueMessageToString);
            fm.TopLevel = false;
            fm.Dock = DockStyle.Fill;
            page.Controls.Add(fm);
            fm.Show();
        }

        TraceEntity Issue(MessageEntity message)
        {
            IssuingResultEntity result = Case.IssueAsync(message);
            return result.Trace;
        }

        string IssueMessageToString(MessageEntity message)
        {
            IssueEntity entity = message as IssueEntity;
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToLongTimeString());
            sb.Append(" :"); //sb.Append(Thread.CurrentThread.ManagedThreadId);
            sb.Append(" "); sb.Append(message.Title);
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