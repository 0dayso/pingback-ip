using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EagleString;
using EagleProtocal;

namespace EagleForms.Easy
{
    public partial class EasyMain : Form
    {
        MyTcpIpClient m_socket;
        LoginInfo m_li;
        CommandPool m_pool;

        AvPanel avPanel;
        LvPanel lvPanel;

        SsResult ssres;

        EagleControls.lvPassengerInEasy lvPassenger = new EagleControls.lvPassengerInEasy();
        EagleControls.PnrListView pnrLV;
        TextBox txtNameCardPhone = new TextBox();

        public EasyMain(MyTcpIpClient socket,LoginInfo li,CommandPool pool)
        {
            InitializeComponent();
            set_input_args(socket, li, pool);
            InitForm();
        }
        public void set_input_args(MyTcpIpClient socket, LoginInfo li, CommandPool pool)
        {
            m_socket = socket;
            m_li = li;
            m_pool = pool;
        }
        private void InitForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;
            this.txtPhone.Text = EagleString.EagleFileIO.ValueOf("PNRORDERSUBMITPHONE");

            avPanel = new AvPanel(m_li, m_pool, m_socket);
            lvPanel = new LvPanel(m_socket, m_pool, m_li);
            panel1.Controls.Add(avPanel);
            panel2.Controls.Add(lvPanel);
            panel3.Controls.Add(lvPanel.lvSelected);
            panel4.Controls.Add(lvPassenger);
            panel4.Controls.Add(txtNameCardPhone);
            lvPassenger.Enter += new EventHandler(lvPassenger_Enter);
            lvPassenger.SelectedIndexChanged += new EventHandler(lvPassenger_SelectedIndexChanged);
            
            txtNameCardPhone.Location = new Point(0, 0);
            txtNameCardPhone.Size = new Size(panel4.Width, txtNameCardPhone.Height);
            txtNameCardPhone.Leave += new EventHandler(txtNameCardPhone_Leave);
            txtNameCardPhone.KeyUp += new KeyEventHandler(txtNameCardPhone_KeyUp);
            txtNameCardPhone.Enter += new EventHandler(txtNameCardPhone_Enter);
            txtNameCardPhone.Text = "姓名，证件号，电话号码 (逗号隔开)";
            txtNameCardPhone.TextAlign = HorizontalAlignment.Center;

            pnrLV = new EagleControls.PnrListView(m_li);

            panel6.Controls.Add(pnrLV);
            pnrLV.UpdatePnr();
        }

        void txtNameCardPhone_Enter(object sender, EventArgs e)
        {
        }

        void lvPassenger_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListViewItem lvi = lvPassenger.SelectedItems[0];
                txtNameCardPhone.Text = lvi.Text + "," + lvi.SubItems[1].Text + "," + lvi.SubItems[2].Text;
            }
            catch
            {
            }
        }

        void txtNameCardPhone_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                string text = txtNameCardPhone.Text.Trim();
                text = EagleString.egString.Full2Half(text);
                string[] a = text.Split(',');
                if (a.Length == 2)
                {
                    lvPassenger.Add(a[0].Trim(), a[1].Trim(), "");
                    txtNameCardPhone.Text = "";
                }
                else if (a.Length > 2)
                {
                    lvPassenger.Add(a[0].Trim(), a[1].Trim(), a[2].Trim());
                    txtNameCardPhone.Text = "";
                }
            }
        }

        void txtNameCardPhone_Leave(object sender, EventArgs e)
        {
            //lvPassenger.Dock = DockStyle.Fill;
        }

        void lvPassenger_Enter(object sender, EventArgs e)
        {
            if (lvPassenger.Dock == DockStyle.Fill)
            {
                lvPassenger.Dock = DockStyle.Bottom;
                lvPassenger.Size = new Size(lvPassenger.Size.Width, panel4.Height - txtNameCardPhone.Height);
                txtNameCardPhone.Focus();
                
                txtNameCardPhone.SelectAll();
            }
        }
        public void AddResult(AvResult avres,bool profit)
        {
            lvPanel.AddResult(avres,profit);
        }
        public void AddResult(FdResult fdres)
        {
            lvPanel.AddResult(fdres);
        }
        private void EasyMain_Load(object sender, EventArgs e)
        {
            avPanel.Show();
            lvPanel.Show();
        }

        private void btnSellSeat_Click(object sender, EventArgs e)
        {
            try
            {
                SendSS();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SendSS()
        {
            txtPnr.Text = "";
            ListView lv = lvPanel.lvSelected2;
            if (lv.Items.Count == 0) throw new Exception("未选航段");

            List<string> lsFlight = new List<string>();
            List<char> lsBunk = new List<char>();
            List<DateTime> lsDate = new List<DateTime>();
            List<string> lsCitypair = new List<string>();

            for (int i = 0; i < lv.Items.Count; i++)
            {
                ListViewItem lvi = lv.Items[i];
                lsDate.Add(DateTime.Parse(lvi.Text));
                lsFlight.Add(lvi.SubItems[1].Text);
                lsBunk.Add(lvi.SubItems[2].Text[0]);
                lsCitypair.Add(lvi.SubItems[3].Text);
            }
            int ppcount = lvPassenger.Items.Count;
            if (lvPassenger.Items.Count == 0) throw new Exception("未添加乘客");
            List<string> lsName = new List<string>();
            List<string> lsCard = new List<string>();
            List<string> lsPhone = new List<string>();
            for (int i = 0; i < lvPassenger.Items.Count; i++)
            {
                ListViewItem lvi = lvPassenger.Items[i];
                lsName.Add(lvi.Text);
                lsCard.Add(lvi.SubItems[1].Text);
                lsPhone.Add(lvi.SubItems[2].Text);
            }
            string phone = this.txtPhone.Text;
            phone = EagleString.egString.Full2Half(phone);
            {//save phone
                System.Collections.Hashtable ht = new System.Collections.Hashtable();
                ht.Add("PNRORDERSUBMITPHONE", phone);
                EagleString.EagleFileIO.WiteHashTableToEagleDotTxt(ht);
            }
            string office = m_li.b2b.lr.UsingOffice();
            string ss = EagleString.CommandCreate.Create_SS_String(
                lsFlight.ToArray(),
                lsBunk.ToArray(),
                lsDate.ToArray(),
                lsCitypair.ToArray(),
                ppcount,
                lsName.ToArray(),
                lsCard.ToArray(),
                phone,
                office,
                null
            );
            m_pool.HandleCommand(ss);
            m_socket.SendCommand(ss, EagleProtocal.TypeOfCommand.Multi);
        }
        public void RecvSS(string ss)
        {
            try
            {
                EagleString.EagleFileIO.LogWrite("Into RecvSS");
                ssres = new SsResult(ss);
                if (ssres.SUCCEED)
                {
                    txtPnr.Text = ssres.PNR;
                    MessageBox.Show(ssres.CHINESESTRING);
                }
                else
                {
                    txtPnr.Text = "错误";
                    MessageBox.Show(ssres.STRING);
                }
                EagleString.EagleFileIO.LogWrite("finish RecvSS");

            }
            catch
            {
            }
        }
        /// <summary>
        /// 提交成功后，在HandlePnrOrderSubmitRt中处理保存
        /// </summary>
        private void btnSaveAndSubmit_Click(object sender, EventArgs e)
        {
            string pnr = txtPnr.Text.Trim();
            if (EagleString.BaseFunc.PnrValidate(pnr))
            {
                string cmd = m_pool.HandleCommand("rt" + pnr);
                m_pool.SetType(EagleString.ETERM_COMMAND_TYPE.PNR_ORDER_SUBMIT);
                m_socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.Multi);
            }
            else
            {
                MessageBox.Show("PNR错误");
                return;
            }
        }
    }
}