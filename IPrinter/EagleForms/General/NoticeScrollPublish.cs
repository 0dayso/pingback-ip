using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace EagleForms.General
{
    public partial class NoticeScrollPublish : Form
    {
        public NoticeScrollPublish()
        {
            InitializeComponent();
        }
        EagleString.LoginInfo m_li;
        public NoticeScrollPublish(EagleString.LoginInfo li)
        {
            InitializeComponent();
            m_li = li;
        }
        private void NoticeScroll_Load(object sender, EventArgs e)
        {
            cbBeg.Checked = true;
            dtpBeg.Enabled = cbBeg.Checked;
            cbEnd.Checked = true;
            cbEnd.Enabled = false;
            dtpEnd.Enabled = cbEnd.Checked;
        }

        private void cbBeg_CheckedChanged(object sender, EventArgs e)
        {
            dtpBeg.Enabled = cbBeg.Checked;
        }

        private void cbEnd_CheckedChanged(object sender, EventArgs e)
        {
            dtpEnd.Enabled = cbEnd.Checked;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (this.tbNotice.Text.Trim() == "") return;
            if (dtpEnd.Value <= dtpBeg.Value)
            {
                MessageBox.Show("抱歉：结束日期必须大于开始日期", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            EagleWebService.kernalFunc fc = new EagleWebService.kernalFunc(m_li.b2b.webservice);
            fc.Submit_Notice_Scroll(
                m_li.b2b.username,
                tbNotice.Text,
                cbBeg.Checked ? dtpBeg.Value : System.DateTime.Now,
                 cbEnd.Checked ? dtpEnd.Value : dtpEnd.Value.AddYears(1),
                 "0");
        }

        private void NoticeScroll_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}