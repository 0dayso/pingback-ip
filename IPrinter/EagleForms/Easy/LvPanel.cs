using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EagleForms.Easy
{
    public partial class LvPanel : Form
    {
        public EagleControls.lvSelectedBunkInEasy lvSelected = new EagleControls.lvSelectedBunkInEasy(true);
        public EagleControls.lvSelectedBunkInEasy lvSelected2 = new EagleControls.lvSelectedBunkInEasy(false);


        ColumnHeader[] columnHeader = new ColumnHeader[41];
        ColumnHeader[] columnHeader2 = new ColumnHeader[41];
        string[] m_header_title = new string[] { "序号","航空公司","航班","起飞城市","抵达城市",
            "起飞时间","抵达时间","机型","F","C","Y",
        "95","90","85","80","75","70","65","60","55","50","45","40","35","30"};

        bool m_show_profit;
        bool m_show_noseat;
        bool m_show_specbunk;

        EagleProtocal.MyTcpIpClient m_socket;
        EagleString.CommandPool m_pool;
        EagleString.LoginInfo m_li;

        List<EagleString.AvResult> ls_av = new List<EagleString.AvResult>();

        public LvPanel(EagleProtocal.MyTcpIpClient sk,EagleString.CommandPool pool,EagleString.LoginInfo li)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.TopLevel = false;
            m_socket = sk;
            m_pool = pool;
            m_li = li;
        }

        private void LvPanel_Load(object sender, EventArgs e)
        {
            if (DateTime.Compare(DateTime.Now, DateTime.Parse("2009-4-20"))>0)
            {
                m_header_title = new string[] { "序号","航空公司","航班","起飞城市","抵达城市",
            "起飞时间","抵达时间","机型","F","C","Y",
        "舱","舱","舱","舱","舱","舱","舱","舱","舱","舱","舱","舱","舱","舱"};
            }

            lv.Visible = true;
            lv2.Visible = false;
            InitListView(lv, columnHeader);
            InitListView(lv2, columnHeader2);

            lvSelected.MouseDoubleClick += new MouseEventHandler(lvSelected_MouseDoubleClick);
        }

        void lvSelected_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (lvSelected.SelectedIndices.Count == 1)
                {
                    int index = lvSelected.SelectedIndices[0];
                    lvSelected2.Items.RemoveAt(index);
                    lvSelected.Items.RemoveAt(index);
                }
            }
            catch
            {
            }
        }
        private void InitListView(ListView lv, ColumnHeader[] columnHeader)
        {
            lv.Dock = DockStyle.Fill;
            lv.Clear();
            for (int i = 0; i < columnHeader.Length; ++i)
            {
                columnHeader[i] = new ColumnHeader();
                columnHeader[i].TextAlign = HorizontalAlignment.Right;
                if (i >= m_header_title.Length)
                {
                    columnHeader[i].Text = "";
                }
                else
                {
                    columnHeader[i].Text = m_header_title[i];
                }
            }
            lv.Columns.AddRange(columnHeader);
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lv.MouseUp += new MouseEventHandler(lv_MouseUp);
            lv.MouseMove += new MouseEventHandler(lv_MouseMove);
            
        }

        void lv_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                ListView list = (sender as ListView);
                int row = 0;
                int col = 0;
                EagleControls.Operators.ListView_PositionOfMouse(list, e, ref row, ref col);
                if (row > list.Items.Count) return;
                if (col > list.Columns.Count) return;
            }
            catch
            {
            }
        }


        void lv_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ShowRightMenu(sender, e);
            }
            else if (e.Button == MouseButtons.Left)
            {
                int row = 0;
                int col = 0;
                EagleControls.Operators.ListView_PositionOfMouse(sender as ListView, e, ref row, ref col);
                //MessageBox.Show(row.ToString() + "," + col.ToString());
                try
                {
                    ListViewItem lvi = lv2.Items[row];
                    if (lvi.Text == "") lvi = lv2.Items[row - 1];
                    string flightno = lvi.SubItems[2].Text;
                    char bunk = lvi.SubItems[col].Text[0];
                    if (bunk == ' ') throw new Exception("");
                    string citypair = lvi.SubItems[3].Text + lvi.SubItems[4].Text;
                    DateTime date = ls_av[0].FlightDate_DT;
                    lvSelected.Add(date, flightno, bunk, citypair);
                    lvSelected2.Add(date, flightno, bunk, citypair);
                }
                catch
                {
                }
            }
        }
        
        void ShowRightMenu(object sender, MouseEventArgs e)
        {
            ContextMenu rightmenu = new ContextMenu();
            string temp = "";
            if ((sender as ListView) == lv) temp = "显示舱位代号";
            else temp = "显示舱位剩余数量";
            rightmenu.MenuItems.Add(temp,new EventHandler (right_menu_item_show_other_lv));
            rightmenu.MenuItems.Add("显示",new MenuItem[]{
                new MenuItem("全部", new EventHandler(right_menu_item_show_all)),
                new MenuItem("8折以下",new EventHandler(right_menu_item_show_8)),
                new MenuItem("5折以下",new EventHandler (right_menu_item_show_5))
                });

            rightmenu.MenuItems.Add("-");
            rightmenu.Show(sender as Control, e.Location);
        }
        void right_menu_item_show_all(object sender, EventArgs e)
        {
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lv2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
        void right_menu_item_show_8(object sender, EventArgs e)
        {
            lv.SuspendLayout();
            right_menu_item_show_all(null, null);
            for (int i = 8; i < 14; i++)
            {
                lv.Columns[i].Width = 0;
                lv2.Columns[i].Width = 0;
            }
            lv.ResumeLayout();
        }
        void right_menu_item_show_5(object sender, EventArgs e)
        {
            right_menu_item_show_all(null, null);
            for (int i = 8; i < 20; i++)
            {
                lv.Columns[i].Width = 0;
                lv2.Columns[i].Width = 0;
            }
        }
        void right_menu_item_show_other_lv(object sender,EventArgs e)
        {
            lv.Visible = !lv.Visible;
            lv2.Visible = !lv2.Visible;
        }

        public void AddResult(EagleString.AvResult avres,bool profit)
        {
            m_show_profit = profit;
            bool clear = (m_pool.TYPE == EagleString.ETERM_COMMAND_TYPE.AV);
            bool sendpn = true;
            if (clear)
            {
                ls_av.Clear();
                lv.Items.Clear();
            }
            if (ls_av.Count == 0)
            {
                ls_av.Add(new EagleString.AvResult(avres.AvString,avres.Price,avres.Distance));
            }
            else
            {
                if (avres.FlightDate_DT == ls_av[0].FlightDate_DT && avres.AvString != ls_av[ls_av.Count-1].AvString)
                {
                    ls_av.Add(new EagleString.AvResult(avres.AvString, avres.Price, avres.Distance));
                }
                else
                {
                    sendpn = false;
                    SetLV();
                }
            }
            if (sendpn)
            {
                string cmd = m_pool.HandleCommand("pn");
                m_socket.SendCommand(cmd,EagleProtocal.TypeOfCommand.Multi);
            }
            
        }
        private void SetLV()
        {
            if (true)
            {
                lv.Items.Clear();//这里可根据用户选择是否清空
                lv2.Items.Clear();
            }
            m_show_noseat = (EagleString.EagleFileIO.ValueOf("EASYFORM_DISPLAY_NO_SEAT_BUNK") == "1");
            m_show_specbunk = (EagleString.EagleFileIO.ValueOf("EASYFORM_DISPLAY_SPEC_BUNK") == "1");
            int base_id = 0;
            for (int i = 0; i < ls_av.Count; ++i)
            {
                try
                {
                    EagleString.AvResult ar = ls_av[i];
                    ar.BASE_ID = base_id;
                    ar.DISPLAY_SEAT_AMOUNT = true;
                    ar.ToListView(lv, m_show_noseat, EagleString.TO_LIST_WAYS.ALL, true);
                    ar.DISPLAY_SEAT_AMOUNT = false;
                    ar.ToListView(lv2, m_show_noseat, EagleString.TO_LIST_WAYS.ALL, true);
                    base_id += ar.si.Length;
                }
                catch
                {
                }
            }
            if (m_show_profit)
            {
                new System.Threading.Thread(
                    new System.Threading.ThreadStart(SetLvProfit)).Start();
            }
            int price = EagleString.EagleFileIO.PriceOf(ls_av[0].CityPair);
            if (price != 0)
            {
                InsertPrice(price,lv);
                InsertPrice(price, lv2);
            }
            else
            {
                m_pool.Clear();
                string cmd = m_pool.HandleCommand("fd:" + ls_av[0].CityPair);
                m_socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.Multi);
            }
        }
        public void AddResult(EagleString.FdResult fdres)
        {
            InsertPrice(fdres.PRICE,lv);
            InsertPrice(fdres.PRICE, lv2);
        }
        private void InsertPrice(int price,ListView lv)
        {
            try
            {
                if (string.IsNullOrEmpty(lv.Items[0].Text))
                {
                    lv.Items.RemoveAt(0);
                }
                ListViewItem lvi = new ListViewItem();
                lvi.ForeColor = Color.LimeGreen;
                lvi.Text = "";
                lvi.SubItems.Add("");// lvi.SubItems.Add(m_airline + m_int_flighno.ToString());
                lvi.SubItems.Add("");//lvi.SubItems.Add(m_policy_string);
                lvi.SubItems.Add("");//lvi.SubItems.Add(m_citypair);
                lvi.SubItems.Add("");//lvi.SubItems.Add(m_time_begin);
                lvi.SubItems.Add("");//lvi.SubItems.Add(m_time_end);
                lvi.SubItems.Add("");//lvi.SubItems.Add(m_plane_type);
                lvi.SubItems.Add("");
                int[] a_rebates = new int[] { 150, 130, 100, 95, 90, 85, 80, 75, 70, 65, 60, 55, 50, 45, 40, 35, 30 };
                for (int i = 0; i < a_rebates.Length; ++i)
                {
                    lvi.SubItems.Add(EagleString.egString.TicketPrice(price, a_rebates[i]).ToString() + "元");
                }
                lv.Items.Insert(0, lvi);
                lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            catch
            {
            }
        }
        private void SetLvProfit()
        {
            List<string> flight = new List<string>();
            for (int i = 0; i < ls_av.Count; ++i)
            {
                flight.AddRange(ls_av[i].Flights);
            }
            if (flight.Count == 0) return;
            EagleWebService.kernalFunc fc = new EagleWebService.kernalFunc(m_li.b2b.webservice);
            string policy = fc.GetPolicies(m_li.b2b.username
            , string.Join(",", flight.ToArray())
            , ls_av[0].FlightDate_DT.ToShortDateString()
            , ls_av[0].CityPair);
            EagleString.ProfitResult pr = new EagleString.ProfitResult(policy);
            if (lv.InvokeRequired)
            {
                deleg4SetProfit deleg = SetLvItemProfit;
                lv.Invoke(deleg, new object[] { pr });
            }
            else
            {
                SetLvItemProfit(pr);
            }
        }
        delegate void deleg4SetProfit(EagleString.ProfitResult pr);
        private void SetLvItemProfit(EagleString.ProfitResult pr)
        {
            int len = lv2.Items.Count;
            for (int i = len - 1; i >= 0; --i)
            {
                    ListViewItem lvi2 = lv2.Items[i];
                    string flight = lvi2.SubItems[2].Text;
                    //不见航班号，下一条
                    if (string.IsNullOrEmpty(flight)) continue;
                    //不是直飞的航段，城市对不立，下一条
                    if (ls_av[0].CityPair != (lvi2.SubItems[3].Text + lvi2.SubItems[4].Text)) continue;
                    ListViewItem templvi = new ListViewItem("");
                    for (int j = 0; j < 7; j++) templvi.SubItems.Add("");
                    try
                    {
                        for (int j = 8; j < lvi2.SubItems.Count; j++)
                        {
                            try
                            {
                                if (string.IsNullOrEmpty(lvi2.SubItems[j].Text.Trim()))
                                {
                                    templvi.SubItems.Add("");
                                }
                                else
                                {
                                    char bunk = lvi2.SubItems[j].Text[0];
                                    templvi.SubItems.Add(pr.ProfitWithFlightAndBunk(flight, bunk));
                                }
                            }
                            catch
                            {
                                templvi.SubItems.Add("");
                            }
                        }
                    }
                    catch
                    {
                    }
                    ListViewItem templvi2 = new ListViewItem();
                    for (int j = 1; j < templvi.SubItems.Count; j++)
                    {
                        templvi2.SubItems.Add(templvi.SubItems[j].Text);
                    }
                
                lv.Items.Insert(i+1, templvi);
                lv2.Items.Insert(i+1, templvi2);
            }
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lv2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
    }
}