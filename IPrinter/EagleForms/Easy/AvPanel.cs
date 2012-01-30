using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EagleForms.Easy
{
    public partial class AvPanel : Form
    {
        private EagleString.LoginInfo m_li;
        private EagleString.CommandPool m_pool;
        private EagleProtocal.MyTcpIpClient m_socket;

        private string m_av_carrier = "";
        private string m_av_from = "";
        private string m_av_to = "";
        private DateTime m_av_date = new DateTime();
        private bool m_av_return = false;//返程
        private bool m_av_direct = true;

        private bool m_display_spectick = false;
        private bool m_display_noseat = false;

        private TypeOfSelect m_select_type = TypeOfSelect.Chinese;
        private bool m_select_inside = true;
        private bool m_select_outside = false;

        /// <summary>
        /// 需要放在一个PANEL里.
        /// </summary>
        public AvPanel(EagleString.LoginInfo li,EagleString.CommandPool pool,EagleProtocal.MyTcpIpClient sk)
        {
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;
            InitializeComponent();
            set_input_args(li, pool, sk);
        }
        public void set_input_args(EagleString.LoginInfo li, EagleString.CommandPool pool, EagleProtocal.MyTcpIpClient sk)
        {
            m_li = li;
            m_pool = pool;
            m_socket = sk;
        }
        private void InitCarrier()
        {
            this.cbCarrier.Items.AddRange(new object[] {
            "承运人(全部)",
            "川航-3U",
            "东星-8C",
            "鹏祥-8L",
            "奥凯-BK",
            "国航-CA",
            "南航-CZ",
            "鹰联-EU",
            "上航-FM",
            "吉祥-HO",
            "海航-HU",
            "厦航-MF",
            "东航-MU",
            "东北航-NS",
            "西航-PN",
            "山东航-SC",
            "深航-ZH"});
            cbCarrier.SelectedIndex = 0;
            cbCarrier.SelectedIndexChanged += new EventHandler(cbCarrier_SelectedIndexChanged);
            cbCarrier.Leave += new EventHandler(ComboxFocusLeave);
        }

        private void InitCitySelect()
        {
            cbCityEnd.Items.Clear();
            cbCityBeg.Items.Clear();
            List<string> ls =
                EagleString.EagleFileIO.WhiteWindowCity((int)m_select_type, m_select_inside, m_select_outside);
            cbCityBeg.Items.AddRange(ls.ToArray());
            cbCityEnd.Items.AddRange(ls.ToArray());
            cbCityBeg.Leave += new EventHandler(ComboxFocusLeave);
            cbCityEnd.Leave += new EventHandler(ComboxFocusLeave);
            cbCityBeg.SelectedIndexChanged += new EventHandler(cbCityBeg_SelectedIndexChanged);
            cbCityEnd.SelectedIndexChanged += new EventHandler(cbCityEnd_SelectedIndexChanged);
        }

        void cbCityBeg_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cb = (sender as ComboBox);
                string citycode = "";
                switch (m_select_type)
                {
                    case TypeOfSelect.ThreeCode:
                        citycode = cb.Text.Trim().Substring(0, 3);
                        break;
                    default:
                        citycode = EagleString.egString.right(cb.Text.Trim(), 3);
                        break;
                }
                if (m_av_return) m_av_to = citycode;
                else m_av_from = citycode;
            }
            catch
            {
            }
        }
        void cbCityEnd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cb = (sender as ComboBox);
                string citycode = "";
                switch (m_select_type)
                {
                    case TypeOfSelect.ThreeCode:
                        citycode = cb.Text.Trim().Substring(0, 3);
                        break;
                    default:
                        citycode = EagleString.egString.right(cb.Text.Trim(), 3);
                        break;
                }
                if (m_av_return) m_av_from = citycode;
                else m_av_to = citycode;
            }
            catch
            {
            }
        }
        void cbCarrier_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbCarrier.SelectedIndex == 0) m_av_carrier = "";
                else m_av_carrier = cbCarrier.SelectedItem.ToString().Split('-')[1];
            }
            catch
            {
                m_av_carrier = "";
            }
        }

        void ComboxFocusLeave(object sender,EventArgs e)
        {
            string name = (sender as ComboBox).Name;
            switch (name)
            {
                case "cbCityBeg":
                    combox_leave_change_select(cbCityBeg);
                    break;
                case "cbCityEnd":
                    combox_leave_change_select(cbCityEnd);
                    break;
                case "cbCarrier":
                    combox_leave_change_select(cbCarrier);
                    break;
            }
        }
        void combox_leave_change_select(ComboBox cb)
        {
            for (int i = 0; i < cb.Items.Count; ++i)
            {
                if (cb.Items[i].ToString().ToLower().IndexOf(cb.Text.ToLower()) >= 0)
                {
                    cb.SelectedIndex = i;
                    break;
                }
            }
        }


        private void EasyForm_Load(object sender, EventArgs e)
        {
            InitCarrier();
            
            InitRadios();
            dtpAv.Value = DateTime.Now;
            m_av_date = DateTime.Now;
            LoadConfig();
            initButton();
        }

        private void checkInside_CheckedChanged(object sender, EventArgs e)
        {
            m_select_inside = checkInside.Checked;
            InitCitySelect();
        }

        private void checkOutside_CheckedChanged(object sender, EventArgs e)
        {
            m_select_outside = checkOutside.Checked;
            InitCitySelect();
        }

        private void InitRadios()
        {
            radioThreeCode.CheckedChanged += new EventHandler(radioChanged);
            radioPinyin.CheckedChanged += new EventHandler(radioChanged);
            radioChinese.CheckedChanged += new EventHandler(radioChanged);
        }
        private void radioChanged(object sender, EventArgs e)
        {
            switch ((sender as Control).Name)
            {
                case "radioChinese":
                    m_select_type = TypeOfSelect.Chinese;
                    break;
                case "radioPinyin":
                    m_select_type = TypeOfSelect.Pinyin;
                    break;
                case "radioThreeCode":
                    m_select_type = TypeOfSelect.ThreeCode;
                    break;
            }
            InitCitySelect();
        }

        private void checkReturn_CheckedChanged(object sender, EventArgs e)
        {
            m_av_return = checkReturn.Checked;
            string temp = m_av_from;
            m_av_from = m_av_to;
            m_av_to = temp;
        }

        private void checkDirect_CheckedChanged(object sender, EventArgs e)
        {
            m_av_direct = checkDirect.Checked;
        }

        private void checkListNoSeatBunk_CheckedChanged(object sender, EventArgs e)
        {
            m_display_noseat = checkListNoSeatBunk.Checked;
        }

        private void checkSpecBunk_CheckedChanged(object sender, EventArgs e)
        {
            m_display_spectick = checkSpecBunk.Checked;
        }

        private void dtpAv_ValueChanged(object sender, EventArgs e)
        {
            m_av_date = dtpAv.Value;
        }


        private void SaveConfig()
        {
            string head = "EASYFORM_";
            System.Collections.Hashtable ht = new System.Collections.Hashtable();
            ht.Add(head + "CITYSTART", cbCityBeg.Text);
            ht.Add(head + "CITYEND", cbCityEnd.Text);
            ht.Add(head + "DIRECT", m_av_direct ? "1" : "0");
            ht.Add(head + "DISPLAY_NO_SEAT_BUNK", m_display_noseat ? "1" : "0");
            ht.Add(head + "DISPLAY_SPEC_BUNK", m_display_spectick ? "1" : "0");
            ht.Add(head + "CITY_INSIDE", m_select_inside ? "1" : "0");
            ht.Add(head + "CITY_OUTSIDE", m_select_outside ? "1" : "0");
            ht.Add(head + "CITY_METHOD", Convert.ToString((int)m_select_type));

            EagleString.EagleFileIO.WiteHashTableToEagleDotTxt(ht);
        }
        private void LoadConfig()
        {
            try
            {
                string head = "EASYFORM_";

                m_select_type = (TypeOfSelect)(int.Parse(EagleString.EagleFileIO.ValueOf(head + "CITY_METHOD")));
                InitCitySelect();
                cbCityBeg.Text = EagleString.EagleFileIO.ValueOf(head + "CITYSTART");
                combox_leave_change_select(cbCityBeg);
                cbCityEnd.Text = EagleString.EagleFileIO.ValueOf(head + "CITYEND");
                combox_leave_change_select(cbCityEnd);
                checkDirect.Checked = (EagleString.EagleFileIO.ValueOf(head + "DIRECT")=="1");
                checkListNoSeatBunk.Checked = (EagleString.EagleFileIO.ValueOf(head + "DISPLAY_NO_SEAT_BUNK") == "1");
                checkSpecBunk.Checked = (EagleString.EagleFileIO.ValueOf(head + "DISPLAY_SPEC_BUNK") == "1");
                checkInside.Checked = (EagleString.EagleFileIO.ValueOf(head + "CITY_INSIDE") == "1");
                checkOutside.Checked = (EagleString.EagleFileIO.ValueOf(head + "CITY_OUTSIDE") == "1");
                
                switch (m_select_type)
                {
                    case TypeOfSelect.ThreeCode:
                        radioThreeCode.Checked = true;
                        radioPinyin.Checked = false;
                        radioChinese.Checked = false;
                        break;
                    case TypeOfSelect.Pinyin:
                        radioThreeCode.Checked = false;
                        radioPinyin.Checked = true;
                        radioChinese.Checked = false;
                        break;
                    case TypeOfSelect.Chinese:
                        radioThreeCode.Checked = false;
                        radioPinyin.Checked = false;
                        radioChinese.Checked = true;
                        break;
                }

            }
            catch
            {
                SaveConfig();
            }
        }

        private void EasyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfig();
        }

        private void btnAv_Click(object sender, EventArgs e)
        {
            SaveConfig();
            m_pool.Clear();
            string av =
                EagleString.CommandCreate.Create_AV_String(m_av_from, m_av_to, m_av_date, 0, m_av_direct, m_av_carrier);
            string cmd = m_pool.HandleCommand(av);
            m_socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.Multi);
        }
        private void initButton()
        {
            DateTime dt1 = DateTime.Parse(dtpAv.Value.ToShortDateString());
            DateTime dt2 = DateTime.Parse(DateTime.Now.ToShortDateString());
            btnBackDay.Enabled = (dt1 >= dt2.AddDays(1));
        }
        private void btnBackDay_Click(object sender, EventArgs e)
        {
            dtpAv.Value = dtpAv.Value.AddDays(-1);
            initButton();
            btnAv_Click(null, null);
        }

        private void btnNextDay_Click(object sender, EventArgs e)
        {
            dtpAv.Value = dtpAv.Value.AddDays(1);
            initButton();
            btnAv_Click(null, null);
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            dtpAv.Value = DateTime.Now;
            initButton();
            btnAv_Click(null, null);

        }

        private void btnTomorrow_Click(object sender, EventArgs e)
        {
            dtpAv.Value = DateTime.Now.AddDays(1);
            initButton();
            btnAv_Click(null, null);

        }

        private void btnAfterTomorrow_Click(object sender, EventArgs e)
        {
            dtpAv.Value = DateTime.Now.AddDays(2);
            initButton();
            btnAv_Click(null, null);

        }
    }

    public enum TypeOfSelect : int
    {
        ThreeCode =0,
        Chinese=1,
        Pinyin=2
    }
}