using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EagleForms.General
{
    public partial class PassengerAdd : Form
    {
        private EagleString.LoginInfo m_li;
        private EagleProtocal.MyTcpIpClient m_socket;
        private EagleString.CommandPool m_cmdpool;

        public PassengerAdd()
        {
            InitializeComponent();
        }
        string m_flight;
        int m_time;
        int m_time2;
        char m_bunk;
        int m_price;
        float m_profit;
        int m_rebate;
        string m_citypair;
        int m_type;
        DateTime m_date;
        
        
        public void DisplayLowestString()
        {
            lblFlight.Text = "����:" + EagleString.BaseFunc.AirLineCnName(m_flight) + m_flight;
            lblBunk.Text = "��λ:" + m_bunk.ToString();
            lblTime.Text = "���:"+EagleString.EagleFileIO.CityCnName(m_citypair.Substring(0,3))
                + m_time.ToString("d4").Insert(2, ":");
            lblTime2.Text = "�ִ�:" + EagleString.EagleFileIO.CityCnName(m_citypair.Substring(3))
                + m_time2.ToString("d4").Insert(2, ":");
            lblPrice.Text = "Ʊ���:" + m_price.ToString();
            lblRebate.Text = "�ۿ�:" + m_rebate.ToString();
            lblProfit.Text = "����:" + m_profit.ToString("f2");
            lblDate.Text = "����:" + m_date.ToShortDateString();
        }
        /// <summary>
        /// ��ListViewItem��Ϊ����0:��ʾ��ͼ��뷵��,1:ɢ��ƴ��,2:�̶���λ����3,:������λ����
        /// </summary>
        public PassengerAdd(ListViewItem lvi,EagleString.AvResult avresult,int type
            ,EagleString.LoginInfo li,EagleProtocal.MyTcpIpClient sk,EagleString.CommandPool pool)
        {
            InitializeComponent();

            m_li = li;
            m_socket = sk;
            m_cmdpool = pool;


            m_type = type;
            avResult = avresult;
            switch (type)
            {
                case 0:
                    InitByLowest(lvi, avresult);
                    break;
                case 1:
                    InitByGroup(lvi);
                    break;
                case 2:
                    InitBySpecTick(lvi, avresult);
                    break;
                case 3:
                    InitBySpecTick(lvi, avresult);
                    break;
            }
        }
        private void InitByLowest(ListViewItem lvi,EagleString.AvResult avres)
        {
            m_flight = lvi.SubItems[1].Text;
            m_time = Convert.ToInt32(lvi.SubItems[2].Text);
            m_time2 = Convert.ToInt32(lvi.SubItems[3].Text);
            m_bunk = lvi.SubItems[4].Text[0];
            m_price = Convert.ToInt32(lvi.SubItems[5].Text);
            m_profit = (float)Convert.ToDouble(lvi.SubItems[6].Text.Replace("%", ""));
            m_rebate = Convert.ToInt32(lvi.SubItems[7].Text);
            m_citypair = avres.CityPair;
            m_date = avres.FlightDate_DT;
            DisplayLowestString();
            cbBunkSelectable.Enabled = false;
            btnOK.Text = "Ԥ��";
        }
        /*
            this.ch_Id,
            this.ch_Title,
            this.ch_Citypair,
            this.ch_flightno,
            this.ch_Date,
            this.ch_Rebate,
            this.ch_Total,
            this.ch_Pnr,
            this.ch_Remain,
            this.ch_Remark,
        */
        int m_groupId;
        int m_groupTotal = 9999;
        int m_groupRemain = 9999;
        private void InitByGroup(ListViewItem lvi)
        {
            m_groupId = Convert.ToInt32(lvi.Text);
            lblTitle.Text = "ɢƴ:" + lvi.SubItems[1].Text;
            lblBunk.Text = "";
            m_flight = lvi.SubItems[3].Text;
            lblFlight.Text = "����:" + EagleString.BaseFunc.AirLineCnName(m_flight) + m_flight;
            m_citypair = lvi.SubItems[2].Text;
            lblTime.Text = "���:" + EagleString.EagleFileIO.CityCnName(m_citypair.Substring(0, 3));
            lblTime2.Text = "�ִ�:" + EagleString.EagleFileIO.CityCnName(m_citypair.Substring(3));
            lblDate.Text = "����:" + lvi.SubItems[4].Text;
            lblPrice.Text = "�ۿۻ�۸�:" + lvi.SubItems[5].Text;
            m_groupTotal = Convert.ToInt32(lvi.SubItems[6].Text);
            lblRebate.Text = "������:" + lvi.SubItems[6].Text;
            m_groupRemain = Convert.ToInt32(lvi.SubItems[8].Text);
            lblProfit.Text = "ʣ����:" + lvi.SubItems[8].Text;
            cbBunkSelectable.Enabled = false;
            btnOK.Text = "����";
        }
        /*
            this.ch_id,
            this.ch_cp,
            this.ch_flight,
            this.ch_bunk,
            this.ch_price,
            this.ch_rebate,
            this.ch_date,
            this.ch_remark});
        */
        private void InitBySpecTick(ListViewItem lvi,EagleString.AvResult avres)
        {
            m_groupId = Convert.ToInt32(lvi.Text);
            m_citypair = lvi.SubItems[1].Text;
            m_flight = lvi.SubItems[2].Text;
            lblFlight.Text = "����:" + EagleString.BaseFunc.AirLineCnName(m_flight) + m_flight;
            lblTime.Text = "���:" + EagleString.EagleFileIO.CityCnName(m_citypair.Substring(0, 3));
            lblTime2.Text = "�ִ�:" + EagleString.EagleFileIO.CityCnName(m_citypair.Substring(3));
            m_date = avres.FlightDate_DT;
            lblDate.Text = "����:" + m_date.ToShortDateString();
            lblProfit.Text = "";

            btnOK.Text = "����";

            string bunk = lvi.SubItems[3].Text;
            if (m_type==3)//������
            {
                bool bfind = false;
                for (int i = 0; i < avres.si.Length; ++i)
                {
                    if (bfind) break;
                    for (int j = 0; j < avres.si[i].fi.Length; ++j)
                    {
                        if (avres.si[i].fi[j].info_Flight == m_flight.ToUpper())
                        {
                            m_bunk = avres.si[i].fi[j].info_Bunk_Lowest;
                            bfind = true;
                            break;
                        }
                    }
                }
                int dec = Convert.ToInt32(lvi.SubItems[5].Text);
                string bunks = EagleString.EagleFileIO.BunksOf(m_bunk, m_flight, dec);
                for (int i = 0; i < bunks.Length; ++i)
                {
                    if (bunks[i] != ' ')
                    {
                        cbBunkSelectable.Items.Add(bunks[i].ToString());
                    }
                }
                cbBunkSelectable.Text = cbBunkSelectable.Items[cbBunkSelectable.Items.Count - 1].ToString();
                m_bunk = cbBunkSelectable.Text[0];
                lblBunk.Text = "��λ:" + m_bunk.ToString();
                m_rebate = EagleString.EagleFileIO.RebateOf(m_bunk,m_flight);
                lblRebate.Text = "�ۿ�:" + m_rebate.ToString();
                if (EagleString.egString.LargeThan420(m_date)) lblRebate.Text = "";
                m_price = EagleString.egString.TicketPrice(avres.Price, m_rebate);
                lblPrice.Text = "Ʊ���:" + m_price.ToString();
            }
            else if (m_type == 2)//�̶���
            {
                m_bunk = bunk[0];

                lblBunk.Text = "��λ:" + m_bunk.ToString();
                m_price = Convert.ToInt32(lvi.SubItems[4].Text);
                lblPrice.Text = "�۸�:" + m_price.ToString();
                m_rebate = Convert.ToInt32(lvi.SubItems[5].Text);
                lblRebate.Text = "�ۿ�:" + m_rebate.ToString();
                if (EagleString.egString.LargeThan420(m_date)) lblRebate.Text = "";
                cbBunkSelectable.Enabled = false;
            }
            
            
        }
        private void PassengerAdd_Load(object sender, EventArgs e)
        {

        }
        EagleString.AvResult avResult;
        private void cbBunkSelectable_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                m_bunk = cbBunkSelectable.Text[0];
                lblBunk.Text = "��λ:" + m_bunk.ToString();
                m_rebate = EagleString.EagleFileIO.RebateOf(m_bunk, m_flight);
                lblRebate.Text = "�ۿ�:" + m_rebate.ToString();
                m_price = EagleString.egString.TicketPrice(avResult.Price, m_rebate);
                if (EagleString.egString.LargeThan420(m_date)) lblRebate.Text = "";
                lblPrice.Text = "Ʊ���:" + m_price.ToString();
            }
            catch
            {
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void txtCard_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                PassengerAddToList();
            }
        }

        private void btnPsgAdd_Click(object sender, EventArgs e)
        {
            PassengerAddToList();
        }
        private void PassengerAddToList()
        {
            try
            {
                if (txtCard.Text.Trim() == "") throw new Exception("������֤����");
                if (txtName.Text.Trim() == "") throw new Exception("����������");
                if (lbName.Items.Count >= m_groupRemain) throw new Exception("����������");
                lbName.Items.Add(txtName.Text);
                lbCard.Items.Add(txtCard.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void PassengerDelFromList()
        {
            try
            {
                if (lbName.SelectedIndices.Count == 0) throw new Exception("����ѡ��");
                int i = lbName.SelectedIndices[0];
                lbName.Items.RemoveAt(i);
                lbCard.Items.RemoveAt(i);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lbName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                PassengerDelFromList();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            names.Clear();
            cards.Clear();
            if (lbName.Items.Count == 0) return;
            for (int i = 0; i < lbName.Items.Count; ++i)
            {
                names.Add(lbName.Items[i].ToString());
                cards.Add(lbCard.Items[i].ToString());
            }
            try
            {
                switch (m_type)
                {
                    case 0://��������PNR��ָ������رնԻ���
                        CreatePnr();
                        break;
                    case 1://���ţ��ɹ���ر�
                        GroupIn();
                        break;
                    case 2://�̶���λ����(����PNR,����������)
                        CreatePnr();
                        break;
                    case 3://��ѡ���˲�λ��������PNR,�ٷ������룬����ֱ�ӷ���
                        m_bunk = cbBunkSelectable.Text[0];
                        CreatePnr();
                        break;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        List<string> names = new List<string>();
        List<string> cards = new List<string>();

        private void CreatePnr()
        {
            m_cmdpool.SetType(EagleString.ETERM_COMMAND_TYPE.SS_4PassengerAddForm);

            EagleString.egString.sortNameAndCardList(names, cards);
            string cmd = EagleString.CommandCreate.Create_SS_String(
                m_flight,
                m_bunk,
                m_date,
                m_citypair,
                names.Count,
                names.ToArray(),
                cards.ToArray(),
                EagleString.EagleFileIO.ValueOf("PNRORDERSUBMITPHONE"),
                m_li.b2b.lr.UsingOffice(),
                new string[] { m_li.b2b.username }
            );
            m_socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.Multi);
            btnOK.Enabled = false;//��ֹ�ظ����,���յ����������
        }

        private void GroupIn()//���ź���
        {
            EagleWebService.kernalFunc fc = new EagleWebService.kernalFunc(m_li.b2b.webservice);
            for (int i = 0; i < lbName.Items.Count; ++i)
            {
                if (!fc.Group_Add(m_li.b2b.username,
                    lbName.Items[i].ToString(),
                    lbCard.Items[i].ToString(),
                    m_groupId.ToString()))
                {
                    MessageBox.Show(lbName.Items[i].ToString() + "����ʧ��");
                }
                else
                {
                    lblTitle.Text = lbName.Items[i].ToString() + "���ųɹ�";
                    Application.DoEvents();
                }
            }
            lblTitle.Text = "������ϣ�";
        }

        public void RecvSS(string txt)
        {
            btnOK.Enabled = true;
            EagleString.SsResult ssres = new EagleString.SsResult(txt);
            if (ssres.SUCCEED)
            {
                switch (m_type)
                {
                    case 0:
                        MessageBox.Show("������¼����PNR=" + ssres.PNR);
                        break;
                    case 1:
                        break;
                    case 2:
                        ApplyBunk(ssres.PNR);
                        break;
                    case 3:
                        ApplyBunk(ssres.PNR);
                        break;
                }
            }
            else
            {
                MessageBox.Show("����ʧ�ܣ�");
            }
        }
        private void ApplyBunk(string pnr)
        {
            string[] passport = null;
            bool bFlag = false;
            string[] phones = new string[names.Count];
            for (int i = 0; i < phones.Length; ++i) phones[i] = "";
            EagleExtension.EagleExtension.SpecTickRequest
                (m_li.b2b.webservice
                , m_li.b2b.username
                , m_groupId
                , m_date
                , m_bunk
                , names.Count
                , pnr
                , names.ToArray()
                , cards.ToArray()
                , phones
                , ref bFlag
                , ref passport);
            if (bFlag)
            {
                string promopt = "";
                if (pnr != "")
                {
                    promopt = "��Ϊ�����ɵ�PNRΪ:" + pnr + "(���μ�)";
                }
                if (passport != null)
                {
                    EagleProtocal.PACKET_PROMOPT_NEW_APPLY ep =
                        new EagleProtocal.PACKET_PROMOPT_NEW_APPLY(EagleProtocal.EagleProtocal.MsgNo++, passport);
                    m_socket.Send(ep.ToBytes());
                    MessageBox.Show("�ѷ������룡" + promopt);
                }
                else
                {
                    MessageBox.Show("�ѷ������룬����Kλ����Ա���ߣ�����������ʽ��ϵ��" + promopt);
                }
            }
            else
            {
                MessageBox.Show("����ʧ�ܣ�������");
            }
        }
    }
}