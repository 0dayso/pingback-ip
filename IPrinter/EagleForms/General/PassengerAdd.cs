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
            lblFlight.Text = "航班:" + EagleString.BaseFunc.AirLineCnName(m_flight) + m_flight;
            lblBunk.Text = "舱位:" + m_bunk.ToString();
            lblTime.Text = "离港:"+EagleString.EagleFileIO.CityCnName(m_citypair.Substring(0,3))
                + m_time.ToString("d4").Insert(2, ":");
            lblTime2.Text = "抵达:" + EagleString.EagleFileIO.CityCnName(m_citypair.Substring(3))
                + m_time2.ToString("d4").Insert(2, ":");
            lblPrice.Text = "票面价:" + m_price.ToString();
            lblRebate.Text = "折扣:" + m_rebate.ToString();
            lblProfit.Text = "返点:" + m_profit.ToString("f2");
            lblDate.Text = "日期:" + m_date.ToShortDateString();
        }
        /// <summary>
        /// 用ListViewItem作为参数0:表示最低价与返点,1:散客拼团,2:固定舱位申请3,:浮动舱位申请
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
            btnOK.Text = "预定";
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
            lblTitle.Text = "散拼:" + lvi.SubItems[1].Text;
            lblBunk.Text = "";
            m_flight = lvi.SubItems[3].Text;
            lblFlight.Text = "航班:" + EagleString.BaseFunc.AirLineCnName(m_flight) + m_flight;
            m_citypair = lvi.SubItems[2].Text;
            lblTime.Text = "离港:" + EagleString.EagleFileIO.CityCnName(m_citypair.Substring(0, 3));
            lblTime2.Text = "抵达:" + EagleString.EagleFileIO.CityCnName(m_citypair.Substring(3));
            lblDate.Text = "日期:" + lvi.SubItems[4].Text;
            lblPrice.Text = "折扣或价格:" + lvi.SubItems[5].Text;
            m_groupTotal = Convert.ToInt32(lvi.SubItems[6].Text);
            lblRebate.Text = "总人数:" + lvi.SubItems[6].Text;
            m_groupRemain = Convert.ToInt32(lvi.SubItems[8].Text);
            lblProfit.Text = "剩余数:" + lvi.SubItems[8].Text;
            cbBunkSelectable.Enabled = false;
            btnOK.Text = "入团";
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
            lblFlight.Text = "航班:" + EagleString.BaseFunc.AirLineCnName(m_flight) + m_flight;
            lblTime.Text = "离港:" + EagleString.EagleFileIO.CityCnName(m_citypair.Substring(0, 3));
            lblTime2.Text = "抵达:" + EagleString.EagleFileIO.CityCnName(m_citypair.Substring(3));
            m_date = avres.FlightDate_DT;
            lblDate.Text = "日期:" + m_date.ToShortDateString();
            lblProfit.Text = "";

            btnOK.Text = "申请";

            string bunk = lvi.SubItems[3].Text;
            if (m_type==3)//浮动舱
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
                lblBunk.Text = "舱位:" + m_bunk.ToString();
                m_rebate = EagleString.EagleFileIO.RebateOf(m_bunk,m_flight);
                lblRebate.Text = "折扣:" + m_rebate.ToString();
                if (EagleString.egString.LargeThan420(m_date)) lblRebate.Text = "";
                m_price = EagleString.egString.TicketPrice(avres.Price, m_rebate);
                lblPrice.Text = "票面价:" + m_price.ToString();
            }
            else if (m_type == 2)//固定舱
            {
                m_bunk = bunk[0];

                lblBunk.Text = "舱位:" + m_bunk.ToString();
                m_price = Convert.ToInt32(lvi.SubItems[4].Text);
                lblPrice.Text = "价格:" + m_price.ToString();
                m_rebate = Convert.ToInt32(lvi.SubItems[5].Text);
                lblRebate.Text = "折扣:" + m_rebate.ToString();
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
                lblBunk.Text = "舱位:" + m_bunk.ToString();
                m_rebate = EagleString.EagleFileIO.RebateOf(m_bunk, m_flight);
                lblRebate.Text = "折扣:" + m_rebate.ToString();
                m_price = EagleString.egString.TicketPrice(avResult.Price, m_rebate);
                if (EagleString.egString.LargeThan420(m_date)) lblRebate.Text = "";
                lblPrice.Text = "票面价:" + m_price.ToString();
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
                if (txtCard.Text.Trim() == "") throw new Exception("请输入证件号");
                if (txtName.Text.Trim() == "") throw new Exception("请输入姓名");
                if (lbName.Items.Count >= m_groupRemain) throw new Exception("到达总人数");
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
                if (lbName.SelectedIndices.Count == 0) throw new Exception("请先选中");
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
                    case 0://发送生成PNR的指令，并不关闭对话框
                        CreatePnr();
                        break;
                    case 1://入团，成功则关闭
                        GroupIn();
                        break;
                    case 2://固定舱位申请(生成PNR,并发出申请)
                        CreatePnr();
                        break;
                    case 3://若选择了舱位，则生成PNR,再发出申请，否则直接发出
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
            btnOK.Enabled = false;//防止重复点击,在收到结果后重置
        }

        private void GroupIn()//入团函数
        {
            EagleWebService.kernalFunc fc = new EagleWebService.kernalFunc(m_li.b2b.webservice);
            for (int i = 0; i < lbName.Items.Count; ++i)
            {
                if (!fc.Group_Add(m_li.b2b.username,
                    lbName.Items[i].ToString(),
                    lbCard.Items[i].ToString(),
                    m_groupId.ToString()))
                {
                    MessageBox.Show(lbName.Items[i].ToString() + "入团失败");
                }
                else
                {
                    lblTitle.Text = lbName.Items[i].ToString() + "入团成功";
                    Application.DoEvents();
                }
            }
            lblTitle.Text = "入团完毕！";
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
                        MessageBox.Show("订座记录编码PNR=" + ssres.PNR);
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
                MessageBox.Show("订座失败！");
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
                    promopt = "并为您生成的PNR为:" + pnr + "(请牢记)";
                }
                if (passport != null)
                {
                    EagleProtocal.PACKET_PROMOPT_NEW_APPLY ep =
                        new EagleProtocal.PACKET_PROMOPT_NEW_APPLY(EagleProtocal.EagleProtocal.MsgNo++, passport);
                    m_socket.Send(ep.ToBytes());
                    MessageBox.Show("已发出申请！" + promopt);
                }
                else
                {
                    MessageBox.Show("已发出申请，但无K位组人员在线，请用其它方式联系！" + promopt);
                }
            }
            else
            {
                MessageBox.Show("申请失败，请重试");
            }
        }
    }
}