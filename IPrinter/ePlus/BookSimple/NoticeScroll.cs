using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ePlus.BookSimple
{
    public partial class NoticeScroll : Form
    {
        public NoticeScroll()
        {
            InitializeComponent();
        }
        public NoticeScroll(string txt)
        {
            InitializeComponent();
            this.tbNotice.Text = txt;
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
            Notice nt = new Notice();
            nt.dtBeg = cbBeg.Checked ? dtpBeg.Value.ToShortDateString() : System.DateTime.Now.ToShortDateString();
            nt.dtEnd = cbEnd.Checked ? dtpEnd.Value.ToShortDateString() : "";
            nt.context = this.tbNotice.Text;
            nt.strType = (BookTicket.b_BookWndOpen ? "1" : "0");
            Thread th = new Thread(new ThreadStart(nt.submit_notice_scroll));
            th.Start();
        }

        private void NoticeScroll_FormClosed(object sender, FormClosedEventArgs e)
        {
            Notice nt = new Notice();

            string temp = nt.get_notice_scroll(BookTicket.b_BookWndOpen ? "1" : "0");
            Notice.NOTICESCROLL = (temp == "" ? "滚动式公告栏" : temp);
        }
    }
}