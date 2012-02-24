using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EagleWebService;
using Options;
using System.Threading;

namespace EagleForms.Printer
{
    public partial class FrmInsuranceList : Form
    {
        EagleWebService.wsInsurrance ws = new EagleWebService.wsInsurrance();

        public FrmInsuranceList()
        {
            InitializeComponent();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.btnRefresh.Enabled = false;
            this.picLoading.Visible = true;
            Thread th = new Thread(new ThreadStart(GetList));
            th.Start();
        }

        private void GetList()
        {
            try
            {
                DateTime dtStart = DateTime.Today, dtEnd = DateTime.Today;
                this.Invoke(new MethodInvoker(delegate()
                    {
                        dtStart = dtpStartDate.Value.Date;
                        dtEnd = dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1);
                    }));

                PolicyListResponseEntity response = ws.GetPolicyListBetween(GlobalVar.IAUsername, GlobalVar.IAPassword, dtStart, dtEnd);
                if (string.IsNullOrEmpty(response.Trace.ErrorMsg))
                    this.Invoke(new MethodInvoker(
                        delegate() { this.dataGridView1.DataSource = response.PolicyList; }
                        ));
            }
            catch (Exception ee)
            {
                EagleString.EagleFileIO.LogWrite(ee.ToString());
            }

            this.Invoke(new MethodInvoker(delegate()
            {
                this.picLoading.Visible = false;
                this.btnRefresh.Enabled = true;
            }));
        }

        private void FrmInsuranceList_Load(object sender, EventArgs e)
        {
            this.dataGridView1.AutoGenerateColumns = false;
            this.dtpStartDate.Value = DateTime.Today.AddDays(-2);
            this.dtpEndDate.Value = DateTime.Today;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count < 1)
                e.Cancel = true;
        }

        private void 补打PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(this, "请选中一条单证！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string caseNo = this.dataGridView1.SelectedRows[0].Cells["colCaseNo"].Value.ToString();
            PolicyResponseEntity response = ws.GetPolicy(GlobalVar.IAUsername, GlobalVar.IAPassword, caseNo);

            if (string.IsNullOrEmpty(response.Trace.ErrorMsg))
            {
                if (MessageBox.Show(this, "请在打印机中准备好单证，确定补打？", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                    == System.Windows.Forms.DialogResult.OK)
                {
                    t_Case policy = response.Policy;
                    Insurance.Instance.PrintIt(policy);
                    this.dataGridView1.SelectedRows[0].DefaultCellStyle.BackColor = Color.Gray;
                }
            }
            else
                MessageBox.Show(this, response.Trace.ErrorMsg, response.Trace.Detail, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            补打PToolStripMenuItem_Click(sender, e);
        }

        private void 作废DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(this, "请选中一条单证！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.作废DToolStripMenuItem.Enabled = false;
            this.picLoading.Visible = true;
            Thread th = new Thread(new ThreadStart(Discard));
            th.Start();
        }

        private void Discard()
        {
            try
            {
                string caseNo = this.dataGridView1.SelectedRows[0].Cells["colCaseNo"].Value.ToString();
                TraceEntity result = ws.DiscardIt(GlobalVar.IAUsername, GlobalVar.IAPassword, caseNo);

                if (string.IsNullOrEmpty(result.ErrorMsg))
                {
                    this.Invoke(new MethodInvoker(delegate()
                    {
                        MessageBox.Show(this, "作废成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.dataGridView1.SelectedRows[0].DefaultCellStyle.ForeColor = Color.Red;
                    }));
                }
                else
                {
                    this.Invoke(new MethodInvoker(delegate()
                    {
                        MessageBox.Show(this, result.ErrorMsg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }));
                }
            }
            catch (Exception ee)
            {
                EagleString.EagleFileIO.LogWrite(ee.ToString());
            }

            this.Invoke(new MethodInvoker(delegate()
                {
                    this.作废DToolStripMenuItem.Enabled = true;
                    this.picLoading.Visible = false;
                }));
        }
    }
}
