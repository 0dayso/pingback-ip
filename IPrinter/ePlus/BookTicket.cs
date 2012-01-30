#define policy1
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using gs.para;

using System.Data.OleDb;
using System.Xml;


namespace ePlus
{
    public partial class BookTicket : Form
    {
        EasyTax easyTax = new EasyTax();
        bool bHasChild = false;


        bool b_3Code = true;
        public static bool b_BookTicketAv = false;//标志是否在本类中查询，是则把返回结果传回到ret_one中
        static bool b_pn = false;
        static bool b_pb = false;
        static public BookTicket context = null;
        static public bool b_BookWndOpen = false;

        string child_birth = "";

        string fdIbe1 = "";
        string fdIbe2 = "";

        bool b_reSelected = false;

        public EagleControls.LV_Lowest lvLowest = new EagleControls.LV_Lowest();
        public EagleControls.LV_GroupList lvGroup = new EagleControls.LV_GroupList ();
        public EagleControls.LV_SpecTicList lvBunkFix = new EagleControls.LV_SpecTicList (true);
        public EagleControls.LV_SpecTicList lvBunkFlow = new EagleControls.LV_SpecTicList (false);
        public void AddLvs()
        {
            pnlGroup.Controls.Add(lvGroup);
            pnlFixBunk.Controls.Add(lvBunkFix);
            pnlFlowBunk.Controls.Add(lvBunkFlow);
            lvGroup.MouseDoubleClick += new MouseEventHandler(lvGroup_MouseDoubleClick);
            lvBunkFix.MouseDoubleClick += new MouseEventHandler(lvBunkFix_MouseDoubleClick);
            lvBunkFlow.MouseDoubleClick += new MouseEventHandler(lvBunkFlow_MouseDoubleClick);
        }

        void lvBunkFlow_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem lvi = (sender as ListView).SelectedItems[0];
            BookSimple.AddPassenger ap = new ePlus.BookSimple.AddPassenger();
            ap.bSpecTickFlag = true;
            ap.flight = lvi.SubItems[2].Text;
            ap.promote = "申请" + lvi.SubItems[2].Text + "的" + lvi.SubItems[3].Text + "舱位";
            ap.total = "9999";
            ap.booked = "0";
            ap.groupid = lvi.SubItems[0].Text;
            ap.date = Convert.ToDateTime(lvi.SubItems[6].Text);
            ap.fromto = lvi.SubItems[1].Text;
            string flight = lvi.SubItems[2].Text;
            char bunk = ' ';
            for (int i = 0; i < lvLowest.Items.Count; ++i)
            {
                ListViewItem L = lvLowest.Items[i];
                if (L.SubItems[1].Text == flight)
                {
                    bunk = L.SubItems[4].Text[0];
                    break;
                }
            }
            if (bunk == ' ')
            {
                MessageBox.Show("AV结果中无对应航班");
                return;
            }

            ap.CombboxSet(false, lvi.SubItems[2].Text.Substring(0, 2), bunk, Convert.ToInt32((sender as ListView).SelectedItems[0].SubItems[5].Text));
            ap.ShowDialog();
        }

        void lvBunkFix_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem lvi = (sender as ListView).SelectedItems[0];
            BookSimple.AddPassenger ap = new ePlus.BookSimple.AddPassenger();
            ap.bSpecTickFlag = true;
            ap.flight = lvi.SubItems[2].Text;
            ap.promote = "申请" + lvi.SubItems[2].Text + "的" + lvi.SubItems[3].Text + "舱位";
            ap.total = "9999";
            ap.booked = "0";
            ap.groupid = lvi.SubItems[0].Text;
            ap.date = Convert.ToDateTime(lvi.SubItems[6].Text);
            ap.CombboxSet(true, lvi.SubItems[2].Text.Substring(0, 2), lvi.SubItems[3].Text[0], 0);
            ap.fromto = lvi.SubItems[1].Text;
            ap.ShowDialog();
        }

        void lvGroup_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BookSimple.AddPassenger ap = new ePlus.BookSimple.AddPassenger();
            string id = "加入：";
            for (int i = 2; i < 6; i++)
            {
                id += " →" + (sender as ListView).SelectedItems[0].SubItems[i].Text;
            }
            ap.promote = id;
            ap.total = (sender as ListView).SelectedItems[0].SubItems[6].Text;
            ap.booked = (sender as ListView).SelectedItems[0].SubItems[8].Text;
            ap.booked = Convert.ToString(int.Parse(ap.total) - int.Parse(ap.booked));
            ap.groupid = (sender as ListView).SelectedItems[0].SubItems[0].Text;
            ap.ShowDialog();
        }

        public void AddPassenger(string name, string id)
        {
            lb_姓名.Items.Add(name);
            lb_CardNo.Items.Add(id);
        }

        static public string stringDisplay
        {
            set
            {
                try
                {
                    if (context.lbDisplay.InvokeRequired)
                    {
                        EventHandler eh = new EventHandler(setlbdisplay);
                        Label lb = context.lbDisplay;
                        context.lbDisplay.Invoke(eh, new object[] { lb, EventArgs.Empty });

                    }
                    else
                    {
                        context.lbDisplay.Text = connect_4_Command.AV_String;
                        context.Activate();
                    }
                }
                catch
                {
                }
                
            }
        }
        static void setlbdisplay(object sender, EventArgs e)
        {
            try
            {
                context.lbDisplay.Text = connect_4_Command.AV_String
                    .Replace("NO ROUTINGS", "无对应航线或航班")
                    .Replace("NO ROUTING", "无对应航线或航班")
                    .Replace("SIGN IN FIRST", "配置管理员输入 SI:工作号/密码")
                    .Replace("RE-INPUT WITH D OPTION FOR DIRECTS", "请尝试去掉 直飞 选项");
                context.Activate();
            }
            catch
            {
            }
        }

        //用于连接本地数据库data.mdb
        OleDbConnection cn = new OleDbConnection();

        static public string returnstring = "";//连pn到pn完毕后，所有返回的结果总合
        public static string ret_one//单次返回
        {
            set
            {
                try
                {
                    if (connect_4_Command.AV_String == null) return;

                    if (connect_4_Command.AV_String.IndexOf("NO ROUTING") > -1)
                    {

                        return;
                    }
                    returnstring = connect_4_Command.AV_String;

                    b_pn = (connect_4_Command.AV_String.IndexOf('+') >= 0);
                    b_pb = (connect_4_Command.AV_String.IndexOf('-') >= 0);
                    
                    ret_end = returnstring;
                }
                catch
                {
                }
            }
        }
        public static string ret_end//没有下一页
        {
            set
            {
                try
                {
                    if (context.lv_查询结果.InvokeRequired)
                    {
                        EventHandler eh = new EventHandler(setcontrol);
                        ListView lv = context.lv_查询结果;
                        context.lv_查询结果.Invoke(eh, new object[] { lv, EventArgs.Empty });
                    }
                }
                catch
                {
                }

            }
        }
        /// <summary>
        /// 得到航班查询结果或订票结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static private void setcontrol(object sender, EventArgs e)
        {
            if (context.b_bookticket_other)
            {
                returnstring = returnstring.Replace("FF:", "航班为").Replace("/", "日期为");
                int pos1 = returnstring.IndexOf("\r\n") + 2;
                returnstring = returnstring.Insert(pos1, "\r\n城市  抵时    离时     机型\r\n");
                MessageBox.Show(returnstring);
                context.b_bookticket_other = false;
                return;
            }

            //context.lbDisplay.Text = returnstring;
            context.Activate();
            if (b_BookTicketAv)
            {
                AVResult ar = new AVResult();
                ar.avResult = returnstring;
                string rev2 = returnstring;
                ar.SetVar();
                EagleString.AvResult avres = new EagleString.AvResult(rev2);
                
                try
                {
                    if (avres.SUCCEED)
                    {

                        //if (!((ar.avDate.Substring(0, 5).Substring(2)) == EagleAPI.GetMonthCode(context.dtp_机票日期.Value.Month) && int.Parse((ar.avDate.Substring(0, 5).Substring(0, 2))) == context.dtp_机票日期.Value.Day))
                        if (avres.FlightDate_DT.ToShortDateString() != context.dtp_机票日期.Value.ToShortDateString())
                        { MessageBox.Show("没有当天航班或当天航班已到最后一页！"); return; }
                        context.lv_查询结果.Items.Clear();
                        Application.DoEvents();
                        ar.SetToListview(context.lv_查询结果, context.dtp_机票日期.Value);
                        Application.DoEvents();
                    }
                    else
                    {
                        EagleString.PatResult patres = new EagleString.PatResult(rev2);
                        if (patres.SUCCEED)
                        {
                            string m = "";
                            m = "航班：" + context.lv_查询结果.Items[context.m_clickrow].SubItems[1].Text
                             + "舱位：" + context.lv_查询结果.Items[context.m_clickrow].SubItems[context.m_clickcol].Text.Substring(0, 1)+"\n\n";
                            m += "票价：" + patres.FARE.ToString() + "\n";
                            m += "机建：" + patres.TAX_BUILD.ToString() + "\n";
                            m += "燃油：" + patres.TAX_FUEL.ToString() + "\n\n";
                            m += "合计：" + patres.TOTAL.ToString();
                            MessageBox.Show(m);
                        }
                    }
                }
                catch
                {
                    return;
                }
                
#if !policy
                if (context.checkBox2.Checked)
                {
                    ListPolicy lp = new ListPolicy();
                    lp.SetAllPolicy(context.lv_查询结果, context.dtp_机票日期.Value.ToShortDateString());
                }
#else
                context.checkBox2.Visible = false;
#endif
                context.tb_avStat.Text = "";
                context.bt_查询.Enabled = true;
                Application.DoEvents();
                context.ReadLocalFC(context.fromto.Substring(0, 3).ToUpper(), context.fromto.Substring(3, 3).ToUpper());
                EasyPrice.insertPrice(BookTicket.f_Bunk_Y, BookTicket.f_Bunk_F.ToString(), context.lv_查询结果);

                {//NKG
                    //context.dgFlightInfo.DataSource = EasyPrice.avDS.TableLowestPerFlight[EasyPrice.avDS.TableLowestPerFlight.Count - 1];
                    //context.dgFlightInfo.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    //context.dgFlightInfo_AddSelectBox();
                }
                {
                    System.Data.OleDb.OleDbConnection cn = new System.Data.OleDb.OleDbConnection();
                    ListView lview = (ListView)context.lvLowest;
                    int price = 0;
                    int distance = 0;
                    //显示到最低价与返点列表
                    EagleExtension.EagleExtension.AvResultToListView_Lowest
                        (rev2, cn, "", GlobalVar.WebServer,GlobalVar2.xmlPolicies, lview,  ref price, ref distance);
                    lview = (ListView)context.lvGroup;
                    //显示散拼列表
                    EagleExtension.EagleExtension.GroupResultToListView_Group(GlobalVar.loginName, 'A', GlobalVar.WebServer, rev2, lview);
                    lview = (ListView)context.lvBunkFix;
                    ListView lview2 = (ListView)context.lvBunkFlow;
                    //显示固定与浮动列表
                    EagleExtension.EagleExtension.SpecTickResultToListView_Spec(rev2, GlobalVar.WebServer, lview, lview2, price);

                }

            }
            if (b_book)
            {
                EagleString.SsResult ssres = new EagleString.SsResult(returnstring);
                if (ssres.SUCCEED)
                {
                    context.tb_电脑号.Text = ssres.PNR;
                    string show = "";
                    for (int i = 0; i < ssres.SEGS.Count; ++i)
                    {
                        show += ssres.SEGS[i].ToChineseString();
                    }
                    MessageBox.Show(show);
                }
                else
                {
                    context.tb_电脑号.Text = "预订失败";
                    MessageBox.Show(ssres.STRING);
                }

            }
            context.Activate();
        }



        /// <summary>
        /// 取月份的三字码
        /// </summary>
        string getmonthstring(int Month)
        {
            string month = "";
            month = EagleAPI.GetMonthCode(Month);
            return month;
        }


        public BookTicket()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
        public BookTicket(bool checkfor)
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            CheckForIllegalCrossThreadCalls = false;
            //GlobalVar2.bookTicket.setToBooktktExtDisplay();
            //GlobalVar2.bookTicket.TopMost = true;
            //GlobalVar2.bookTicket.FormBorderStyle = FormBorderStyle.None;
            setToBooktktExtDisplay();
            TopMost = true;
            FormBorderStyle = FormBorderStyle.None;

        }
        private void BookTicket_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.cb_到达地.Text = "PVG";
            this.MinimizeBox = true;
            AddLvs();
            this.Hide();
            Application.DoEvents();
            {
                this.Text = GlobalVar.exeTitle + "（服务器位置：" + GlobalVar.loginLC.SrvName + "）";
                if (GlobalVar.serverAddr == GlobalVar.ServerAddr.ZhenZhouJiChang)
                {
                    this.button9.Text = "竞价平台";
                }
                button9.Enabled = !Model.md.b_00H;
                ///*简易版屏幕
                System.Windows.Forms.Screen screen = Screen.PrimaryScreen;
                if (screen.Bounds.Width < 1024 || screen.Bounds.Height < this.Height)
                {
                    this.Width = screen.Bounds.Width;
                    this.Height = screen.Bounds.Height;
                }
                ///*简易版屏幕
                if (GlobalVar.serverAddr == GlobalVar.ServerAddr.HangYiWang)
                {
                    this.Close();
                    return;
                }
                cbCarrier.Text = cbCarrier.Items[0].ToString();
                cbAvTime.Text = cbAvTime.Items[0].ToString();
                if (Model.md.b_016) this.bt_订票.Visible = false;
                cbTicketType.SelectedIndex = 0;

                this.cb_出发地.Text = GlobalVar.LocalCityCode;
                SetFromToCombox();
                b_BookTicketAv = false;
                b_BookWndOpen = true;
                context = this;

                //dtp_机票日期.Value = System.DateTime.Now;
                dtp_时限日期.Value = System.DateTime.Now;
                try
                {
                    string ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\data.mdb;";
                    if (cn.State != ConnectionState.Open)//Modify by Eric
                    {
                        cn.ConnectionString = ConnStr;
                        cn.Open();
                    }
                }
                catch
                {
                    MessageBox.Show("本地数据库连接失败");
                    this.Close();
                }
                ReadSavedPNR();
                //button3_Click(sender, e);
                //button5_Click(sender, e);
                //button7_Click(sender, e);
                timer1.Start();

                //获取公告
                //{
                //    Notice nt = new Notice();
                //    string tempnt = nt.get_notice_scroll(BookTicket.b_BookWndOpen ? "1" : "0");
                //    lblNotice.ForeColor = Color.Lime;
                //    lblNotice.Text = (tempnt == "" ? "滚动式公告栏" : tempnt);
                //}
                System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(initialList));
                th.Start();

                this.ActiveControl = this.cb_出发地;

                if (GlobalVar.gbIsNkgFunctions) initNkgControls();
            }
            this.Show();
            wf_init();
        }
        void initialList()
        {
            try
            {
                CheckForIllegalCrossThreadCalls = false;
                OnButton3Click();
                OnButton5Click();
                OnButton7Click();
                {
                    Notice nt = new Notice();
                    string tempnt = nt.get_notice_scroll(BookTicket.b_BookWndOpen ? "1" : "0");
                    lblNotice.ForeColor = Color.Lime;
                    lblNotice.Text = (tempnt == "" ? "滚动式公告栏" : tempnt);
                }

                CheckForIllegalCrossThreadCalls = true;
            }
            catch
            {
            }
        }
        /// <summary>
        /// 置起点，终点下拉框
        /// </summary>
        private void SetFromToCombox()
        {
            List<string> ls = 
                EagleString.EagleFileIO.WhiteWindowCity(Convert.ToInt32(GlobalVar.SelectCityType), IsList国内City(), IsList国外City());
            cb_出发地.Items.AddRange(ls.ToArray());
            cb_到达地.Items.AddRange(ls.ToArray());
            return;
        }
        private bool IsList国内City()
        {
            string temp = EagleAPI.egReadFile(System.Windows.Forms.Application.StartupPath + "\\options.XML");
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);
            XmlNode xn = xd.SelectSingleNode("eg");
            try
            {
                xn = xn.SelectSingleNode("isListChinaCity");
                if (xn == null) throw new Exception();
            }
            catch
            {
                XmlElement xe;
                xe = xd.CreateElement("isListChinaCity");
                xe.InnerText = "1";
                xn = xd.SelectSingleNode("eg");
                xn.AppendChild(xe);
                xd.Save(System.Windows.Forms.Application.StartupPath + "\\options.XML");
            }
            xn = xd.SelectSingleNode("eg").SelectSingleNode("isListChinaCity");
            if (xn.InnerText.Trim() == "1") return true;
            else return false;
        }
        private bool IsList国外City()
        {
            string temp = EagleAPI.egReadFile(System.Windows.Forms.Application.StartupPath + "\\options.XML");
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);
            XmlNode xn = xd.SelectSingleNode("eg");
            try
            {
                xn = xn.SelectSingleNode("isListForeignCity");
                if (xn == null) throw new Exception();
            }
            catch
            {
                XmlElement xe;
                xe = xd.CreateElement("isListForeignCity");
                xe.InnerText = "1";
                xn = xd.SelectSingleNode("eg");
                xn.AppendChild(xe);
                xd.Save(System.Windows.Forms.Application.StartupPath + "\\options.XML");
            }
            xn = xd.SelectSingleNode("eg").SelectSingleNode("isListForeignCity");
            if (xn.InnerText.Trim() == "1") return true;
            else return false;
        }
        string fromto = "";
        string avcmd = "";
        private void bt_查询_Click(object sender, EventArgs e)
        {
            DateSearch = dtp_机票日期.Value;
            _420Init();

            {//FOR NKG
                //dgFlightInfo.DataSource = null;
                //dgFlightInfo.Rows.Clear();
                //dgFlightInfo.Columns.Clear();
                //Application.DoEvents();
            }


            #region 指令组合前序
            distance = 0;
            this.Activate();
            oldid = "";
            cb_出发地.Text = cb_出发地.Text.ToUpper();
            cb_到达地.Text = cb_到达地.Text.ToUpper();
            b_bookticket_提取 = false;
            b_bookticket_fd = false;
            b_BookTicketAv = true;
            b_book = false;
            if (cb_出发地.Text.Length < 3 || cb_到达地.Text.Length < 3 || (cb_出发地.Text == cb_到达地.Text))
            {
                MessageBox.Show("请正确输入起始地与到达地！");
                return;
            }
            //原查询价格清0
            f_Bunk_C = f_Bunk_F = f_Bunk_Y = 0.0F;

            lv_查询结果.Items.Clear();
            b_BookTicketAv = true;

            ret_one = "";


            //av的日期
            string str_day = dtp_机票日期.Value.Day.ToString("d2");
            string str_mon = getmonthstring(dtp_机票日期.Value.Month);

            //发送av指令
            string str_send;
            if (GlobalVar.SelectCityType == "0")
            {

                if (cb_返程.Checked)
                {
                    fromto = cb_到达地.Text.Substring(0, 3) + cb_出发地.Text.Substring(0, 3);
                }
                else
                {
                    fromto = cb_出发地.Text.Substring(0, 3) + cb_到达地.Text.Substring(0, 3);
                }

            }
            else
            {
                if (cb_返程.Checked)
                {
                    fromto = cb_到达地.Text.Substring(cb_到达地.Text.Length - 3) + cb_出发地.Text.Substring(cb_出发地.Text.Length - 3);
                }
                else
                {
                    fromto = cb_出发地.Text.Substring(cb_出发地.Text.Length - 3) + cb_到达地.Text.Substring(cb_到达地.Text.Length - 3);
                }
            }
            GlobalVar2.differenceFC(fromto.Substring(0, 3), fromto.Substring(3));
            str_send = EagleString.CommandCreate.Create_AV_String(fromto.Substring(0, 3),
                                                                    fromto.Substring(3),
                                                                    dtp_机票日期.Value,
                                                                    Convert.ToInt32(cbAvTime.Text.Remove(2, 1)),
                                                                    checkBox1.Checked,
                                                                    (cbCarrier.SelectedIndex==0 ? "" : mystring.right(cbCarrier.Text, 2))
                                                                    );

            avcmd = str_send;

            #endregion
            if (bIbe)
            {

                try
                {
                    tb_avStat.Text = GlobalVar.WaitString;

                    wk.Hide();
                    wk.timeStart = DateTime.Now;
                    wk.Show();
                    //ibeAvResult = ib.av(fromto,
                    //    dtp_机票日期.Value.Year.ToString() + dtp_机票日期.Value.Month.ToString("d2") + dtp_机票日期.Value.Day.ToString("d2")
                    //    + " " + cbAvTime.Text + ":00",
                    //    cbCarrier.Text == cbCarrier.Items[0].ToString() ? "ALL" : mystring.right(cbCarrier.Text, 2),
                    //    checkBox1.Checked, true);

                    string[] parametres = new string[] {fromto,
                        dtp_机票日期.Value.ToString("yyyyMMdd") + " " + cbAvTime.Text + ":00",
                        cbCarrier.Text == cbCarrier.Items[0].ToString() ? "ALL" : mystring.right(cbCarrier.Text, 2),
                        checkBox1.Checked?"1":"0", "1",ibeAvResult,ibeAvResultTemp };
                    t = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(ib.av));
                    t.Start(parametres);

                    System.Threading.Thread t0 = new System.Threading.Thread(new System.Threading.ThreadStart(getavresult));
                    t0.Start();
                }
                catch (Exception ex)
                {
                    lbDisplay.Text = "IBE:查询报错-" + ex.Message;
                }
                //分析PN，插入票价行

            }
            else
            {
                EagleAPI.CLEARCMDLIST(3);
                EagleAPI.EagleSendCmd(str_send, 3);
                tb_avStat.Text = GlobalVar.WaitString;
            }
            //...等待回应

            //lv_查询结果.Items.Clear();            

            //1.设avResult值
            //ar.avResult = 
            //ar.SetToListview(this.lv_查询结果);

        }


        System.Threading.Thread t = null;
        string ibeAvResultTemp = "";
        string ibeAvResult = "";
        Options.ibe.ibeInterface ib = new Options.ibe.ibeInterface();
        string[] pages = null;
        int currentpage = 1;
        static public bool bIbe = true;
        void ibeAvSuccess(string avCmd,string sFromto)
        {
            if (ibeAvResult == "")
            {
                lbDisplay.Text = "IBE查询失败！";
                return;
            }
            pages = ibeAvResult.Split(new string[] { "\n##########\n" }, StringSplitOptions.RemoveEmptyEntries);

            if (pages.Length > 1)
            {
                AVResult ar = new AVResult();
                ar.avResult = pages[0] + pages[1];
                connect_4_Command.AV_String = ar.avResult;
                lv_查询结果.Items.Clear();
                ar.SetToListview(lv_查询结果, dtp_机票日期.Value);
                lbDisplay.Text = "IBE查询结果:\n" + ar.avResult;
                Application.DoEvents();

                if (checkBox2.Checked)
                {
                    ListPolicy lp = new ListPolicy();
                    lp.SetAllPolicy(lv_查询结果, dtp_机票日期.Value.ToShortDateString());
                }
                Application.DoEvents();
                ReadLocalFC(sFromto.Substring(0, 3).ToUpper(), sFromto.Substring(3, 3).ToUpper());
                EasyPrice.insertPrice(BookTicket.f_Bunk_Y, BookTicket.f_Bunk_F.ToString(), lv_查询结果);
                {//NKG
                    //context.dgFlightInfo.DataSource = EasyPrice.avDS.TableLowestPerFlight[EasyPrice.avDS.TableLowestPerFlight.Count - 1];
                    //context.dgFlightInfo.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    //context.dgFlightInfo_AddSelectBox();
                }
                //加入散客拼团
                PassengerToGroup tg = new PassengerToGroup();
                tg.avstring = avCmd;// str_send;
                tg.fromto = sFromto.ToUpper();
                tg.date = dtp_机票日期.Value.ToShortDateString();
                System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(tg.execute));
                th.Start();

            }
            else
            {
                lbDisplay.Text = "IBE查询结果(无航班):\n" + ibeAvResult;
            }
            if (pages.Length > 2) b_pn = true;
        }
        void getavresult()
        {
            while (true)
            {
                if (t.ThreadState == System.Threading.ThreadState.Stopped && ib.avresult != "")
                {
                    ibeAvResult = ib.avresult;
                    ibeAvResultTemp = ib.avresult;
                    ib.avresult = "";
                    CheckForIllegalCrossThreadCalls = false;
                    wk.Hide();
                    ibeAvSuccess(avcmd, fromto);
                    //if (wk.InvokeRequired)
                    //{
                    //    EventHandler eh = new EventHandler(setcontrol);
                    //    SubmitPnr pt = SubmitPnr.Context;
                    //    SubmitPnr.Context.Invoke(eh, new object[] { pt, EventArgs.Empty });

                    //}
                    CheckForIllegalCrossThreadCalls = true;
                    break;
                }
            }
        }

        private void lv_查询结果_Click(object sender, EventArgs e)
        {
            
        }
        private void selectibe(object sender,EventArgs e)
        {
            bIbe = !bIbe;
        }

        private void back(object sender, EventArgs e)
        {
            if (bIbe)
            {
                if (currentpage < 2)
                {
                    lbDisplay.Text = "IBE:已经到达最前页";
                    return;
                }
                else
                {
                    try
                    {
                        currentpage--;
                        AVResult ar = new AVResult();
                        ar.avResult = pages[0] + pages[currentpage];
                        lv_查询结果.Items.Clear();
                        ar.SetToListview(lv_查询结果, dtp_机票日期.Value);
                        b_pn = true;
                        if (currentpage < 2) b_pb = false;
                        else b_pb = true;
                        lbDisplay.Text = "IBE查询结果:\n" + ar.avResult;
                        ListPolicy lp = new ListPolicy();
                        lp.SetAllPolicy(lv_查询结果, dtp_机票日期.Value.ToShortDateString());

                        ReadLocalFC(fromto.Substring(0, 3).ToUpper(), fromto.Substring(3, 3).ToUpper());
                        EasyPrice.insertPrice(BookTicket.f_Bunk_Y, BookTicket.f_Bunk_F.ToString(), lv_查询结果);
                        {
                            //context.dgFlightInfo.DataSource = EasyPrice.avDS.TableLowestPerFlight[EasyPrice.avDS.TableLowestPerFlight.Count - 1];
                            //context.dgFlightInfo.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                            //context.dgFlightInfo_AddSelectBox();
                        }

                    }
                    catch
                    {
                        lbDisplay.Text = "IBE:向上翻页报错";
                    }
                }
                return;
            }
            else
            {
                b_bookticket_fd = false;
                EagleAPI.EagleSendCmd("pb");
            }
        }
        private void next(object sender, EventArgs e)
        {
            if (bIbe)
            {
                if (currentpage >= pages.Length - 1)
                {
                    lbDisplay.Text = "IBE:已经到达最后页";
                    return;
                }
                else
                {
                    try
                    {
                        currentpage++;
                        AVResult ar = new AVResult();
                        ar.avResult = pages[0] + pages[currentpage];
                        connect_4_Command.AV_String = ar.avResult;
                        lv_查询结果.Items.Clear();
                        ar.SetToListview(lv_查询结果, dtp_机票日期.Value);
                        b_pb = true;
                        if (currentpage >= pages.Length - 1) b_pn = false;
                        else b_pn = true;
                        lbDisplay.Text = "IBE查询结果:\n" + ar.avResult;
                        ListPolicy lp = new ListPolicy();
                        lp.SetAllPolicy(lv_查询结果, dtp_机票日期.Value.ToShortDateString());

                        ReadLocalFC(fromto.Substring(0, 3).ToUpper(), fromto.Substring(3, 3).ToUpper());
                        EasyPrice.insertPrice(BookTicket.f_Bunk_Y, BookTicket.f_Bunk_F.ToString(), lv_查询结果);
                        {//NKG
                            //context.dgFlightInfo.DataSource = EasyPrice.avDS.TableLowestPerFlight[EasyPrice.avDS.TableLowestPerFlight.Count - 1];
                            //context.dgFlightInfo.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                            //context.dgFlightInfo_AddSelectBox();
                        }
                    }
                    catch
                    {
                        lbDisplay.Text = "IBE:向下翻页报错";
                    }
                }
                return;
            }
            b_bookticket_fd = false;
            EagleAPI.EagleSendCmd("pn");
        }
        public static bool b_bookticket_fd = false;//是否正在查票价的标志
        public bool b_bookticket_other = false;//是则只显示在信息栏中，否则原样
        public static float f_Bunk_F = 0F;//头等舱
        public static float f_Bunk_C = 0F;//商务舱
        public static float f_Bunk_Y = 0F;//经济舱
        public static int distance = 0;//里程
        /// <summary>
        /// 价格查询返回
        /// </summary>
        public static string bookticket_fd_return
        {
            set
            {
                try
                {
                    
                    EagleString.FdResult fdres = new EagleString.FdResult(connect_4_Command.AV_String);
                    distance = fdres.DISTANCE;
                    f_Bunk_Y = (float)fdres.PRICE;
                    f_Bunk_F = (float)EagleString.egString.TicketPrice(fdres.PRICE, 150);
                    f_Bunk_C = (float)EagleString.egString.TicketPrice(fdres.PRICE, 130);
                    
                    //存入价格
                    if (context.cb_出发地.InvokeRequired)
                    {
                        
                        EventHandler eh = new EventHandler(setfc);
                        
                        ComboBox cb = context.cb_出发地;
                        
                        context.cb_出发地.Invoke(eh, new object[] { cb, EventArgs.Empty });
                    }
                    
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// 返回后，存入票价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static private void setfc(object sender, EventArgs e)
        {
            string _fromto = "";
            if (GlobalVar.SelectCityType == "0")
            {

                if (context.cb_返程.Checked)
                {
                    _fromto = context.cb_到达地.Text.Substring(0, 3) + context.cb_出发地.Text.Substring(0, 3);
                }
                else
                {
                    _fromto = context.cb_出发地.Text.Substring(0, 3) + context.cb_到达地.Text.Substring(0, 3);
                }

            }
            else
            {
                if (context.cb_返程.Checked)
                {
                    _fromto = context.cb_到达地.Text.Substring(context.cb_到达地.Text.Length - 3) + context.cb_出发地.Text.Substring(context.cb_出发地.Text.Length - 3);
                }
                else
                {
                    _fromto = context.cb_出发地.Text.Substring(context.cb_出发地.Text.Length - 3) + context.cb_到达地.Text.Substring(context.cb_到达地.Text.Length - 3);
                }
            }

            try
            {
                string s_temp = "";
                s_temp = string.Format("SELECT * FROM t_fc WHERE [FROM]='{0}' AND [TO]='{1}'", _fromto.Substring(0, 3).ToUpper(), _fromto.Substring(3, 3).ToUpper());
                OleDbCommand cmd = new OleDbCommand(s_temp, context.cn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                DataTable dtTmp = new DataTable();
                adapter.Fill(dtTmp);
                if (dtTmp.Rows.Count == 0)
                {
                    s_temp = string.Format("insert into t_fc ([From],[To],BunkF,BunkC,BunkY) values ('{0}','{1}','{2}','{3}','{4}')",
                        _fromto.Substring(0, 3).ToUpper(),
                        _fromto.Substring(3, 3).ToUpper(),
                        distance.ToString(),//f_Bunk_F.ToString("f2"),
                        f_Bunk_C.ToString("f2"),
                        f_Bunk_Y.ToString("f2"));
                    OleDbCommand t_cmd = new OleDbCommand(s_temp, context.cn);
                    if (t_cmd.ExecuteNonQuery() < 1)
                    {
                        MessageBox.Show("未写入本地数据库票价表");
                    }
                    
                }
                else
                {
                    s_temp = string.Format("update t_fc set BunkF='{0}',BunkC='{1}',BunkY='{2}' where [From]='{3}' and [To] = '{4}'",
                        distance.ToString(),//f_Bunk_F.ToString("f2"),
                        f_Bunk_C.ToString("f2"),
                        f_Bunk_Y.ToString("f2"),
                        _fromto.Substring(0, 3),
                        _fromto.Substring(3, 3));
                    OleDbCommand t_cmd = new OleDbCommand(s_temp, context.cn);
                    if (t_cmd.ExecuteNonQuery() < 1)
                    {
                        MessageBox.Show("未更新本地数据库票价表");
                    }
                }
            }
            catch
            {
                MessageBox.Show("写入本地数据库票价表失败");
                return;
            }
            context.tb_avStat.Text = "";
            context.SaveServerFC();
            context.lv_查询结果.Items.RemoveAt(0);
            EasyPrice.insertPrice(f_Bunk_Y, distance.ToString(), context.lv_查询结果);
            {
                //context.dgFlightInfo.DataSource = EasyPrice.avDS.TableLowestPerFlight[EasyPrice.avDS.TableLowestPerFlight.Count - 1];
                //context.dgFlightInfo.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                //context.dgFlightInfo_AddSelectBox();
            }

            EagleAPI.WritePriceTable(context.cb_出发地.Text.Substring(0, 3).ToUpper() + context.cb_到达地.Text.Substring(0, 3).ToUpper(), f_Bunk_Y.ToString("f2"), distance.ToString());
            context.Activate();
        }


        string ibeFdResult = "";
        string ibeFdResultTemp = "";
        void getfdresult()
        {
            while (true)
            {
                if (t.ThreadState == System.Threading.ThreadState.Stopped && ib.fdresult != "")
                {
                    ibeFdResult = ib.fdresult;
                    ibeFdResultTemp = ib.fdresult;
                    ib.fdresult = "";
                    CheckForIllegalCrossThreadCalls = false;
                    wk.Hide();
                    ibeFdSuccess();
                    CheckForIllegalCrossThreadCalls = true;
                    break;
                }
            }
        }
        void ibeFdSuccess()
        {
            try
            {
                BookTicket.f_Bunk_F = float.Parse(ibeFdResult.Split('~')[0]);
                BookTicket.f_Bunk_C = float.Parse(ibeFdResult.Split('~')[1]);
                BookTicket.f_Bunk_Y = float.Parse(ibeFdResult.Split('~')[2]);
                
                BookTicket.setfc(null, null);
            }
            catch
            {
                BookTicket.f_Bunk_F = BookTicket.f_Bunk_C = BookTicket.f_Bunk_Y = 0;
            }
        }
        private void querryBunkFC(object sender, EventArgs e)
        {
            try
            {
                ListViewItem lvi = lv_查询结果.Items[m_clickrow];
                string flight = lvi.SubItems[1].Text.Replace("*", "");
                char bunk = lvi.SubItems[m_clickcol].Text[0];
                DateTime date = dtp_机票日期.Value;
                string citypair = this.fromto;
                int peoplei = 1;
                string[] names = new string[] { "ce/shi"};
                string[] cards = new string[] { "123456"};
                string phone = "95161";
                string office = "WUH128";
                string[] remarks = null;
                string ss = EagleString.CommandCreate.Create_SS_String(flight, bunk, date, citypair, peoplei, names, cards, phone, office, remarks);
                ss = ss.Split('\r')[0];
                string pata = "i~" + ss + "~PAT:A";
                EagleAPI.EagleSendOneCmd(pata);
            }
            catch
            {
            }

        }
        private void querryFC(object sender, EventArgs e)
        {
            //if (bIbe)
            //{
            //    try
            //    {
            //        tb_avStat.Text = GlobalVar.WaitString;

            //        wk.Hide();
            //        wk.timeStart = DateTime.Now;
            //        wk.Show();

            //        string[] parametres = new string[] {
            //            fromto,
            //            dtp_机票日期.Value.Day.ToString("d2")+EagleAPI.GetMonthCode(dtp_机票日期.Value.Month)
            //            +dtp_机票日期.Value.Year.ToString().Substring(2),

            //            cbCarrier.Text == cbCarrier.Items[0].ToString() ? "ALL" : mystring.right(cbCarrier.Text, 2),
            //            };
            //        t = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(ib.fd));
            //        t.Start(parametres);

            //        System.Threading.Thread t0 = new System.Threading.Thread(new System.Threading.ThreadStart(getfdresult));
            //        t0.Start();
            //    }
            //    catch (Exception ex)
            //    {
            //        lbDisplay.Text = "IBE:查询报错-" + ex.Message;
            //    }
            //}
            //else
            {
                tb_avStat.Text = GlobalVar.WaitString;
                b_bookticket_fd = true;
                string al = lv_查询结果.SelectedItems[0].SubItems[1].Text.Replace("*", "").Substring(0, 2);
                EagleAPI.EagleSendOneCmd("i~fd:" + fromto + "/" + dtp_机票日期.Value.ToString("ddMMM",EagleString.egString.dtFormat) + "/" +al);
            }
            
        }
        private void querryFF(object sender, EventArgs e)
        {
            string str_day = dtp_机票日期.Value.Day.ToString("d2");
            string str_mon = getmonthstring(dtp_机票日期.Value.Month);

            b_bookticket_other = true;
            EagleAPI.EagleSendOneCmd("i~ff:" + lv_查询结果.SelectedItems[0].SubItems[1].Text + "/" + str_day + str_mon);
        }
        private void lsNoSeatBunk(object sender, EventArgs e)
        {
            GlobalVar.b_ListNoSeatBunk = !GlobalVar.b_ListNoSeatBunk;
        }

        int Len1 = 28;
        int Len2 = 28;
        private void lsT(object sender, EventArgs e)
        {
            //if (this.columnHeaderR.Width == 0)
            //{
                
                this.columnHeaderA.Width = Len1;
                this.columnHeaderB.Width = Len1;
                this.columnHeaderC.Width = Len1;
                this.columnHeaderD.Width = Len1;
                this.columnHeaderE.Width = Len1;
                this.columnHeaderF.Width = Len1;
                this.columnHeaderG.Width = Len1;
                this.columnHeaderH.Width = Len1;
                this.columnHeaderI.Width = Len1;
                this.columnHeaderJ.Width = Len1;
                this.columnHeaderK.Width = Len1;
                this.columnHeaderL.Width = Len1;
                this.columnHeaderM.Width = Len1;
                this.columnHeaderN.Width = Len1;
                this.columnHeaderO.Width = Len1;
                this.columnHeaderP.Width = Len1;
                this.columnHeaderQ.Width = Len1;


                this.columnHeaderR.Width = Len2;
                this.columnHeaderS.Width = Len2;
                this.columnHeaderT.Width = Len2;
                this.columnHeaderU.Width = Len2;
                this.columnHeaderV.Width = Len2;
                this.columnHeaderW.Width = Len2;
                this.columnHeaderX.Width = Len2;
                this.columnHeaderY.Width = Len2;
                this.columnHeaderZ.Width = Len2;
                this.columnHeaderZA.Width = Len2;
                this.columnHeaderZB.Width = Len2;

                this.columnHeaderZC.Width = Len2;
                this.columnHeaderZD.Width = Len2;
                this.columnHeaderZE.Width = Len2;
                this.columnHeaderZF.Width = Len2;
                this.columnHeaderZG.Width = Len2;
                this.columnHeaderZH.Width = Len2;
            //}
            //else
            //{
            //    this.columnHeaderA.Width = 46;
            //    this.columnHeaderB.Width = 46;
            //    this.columnHeaderC.Width = 46;
            //    this.columnHeaderD.Width = 46;
            //    this.columnHeaderE.Width = 46;
            //    this.columnHeaderF.Width = 46;
            //    this.columnHeaderG.Width = 46;
            //    this.columnHeaderH.Width = 46;
            //    this.columnHeaderI.Width = 46;
            //    this.columnHeaderJ.Width = 46;
            //    this.columnHeaderK.Width = 46;
            //    this.columnHeaderL.Width = 46;
            //    this.columnHeaderM.Width = 46;
            //    this.columnHeaderN.Width = 46;
            //    this.columnHeaderO.Width = 46;
            //    this.columnHeaderP.Width = 46;
            //    this.columnHeaderQ.Width = 46;


            //    this.columnHeaderR.Width = 0;
            //    this.columnHeaderS.Width = 0;
            //    this.columnHeaderT.Width = 0;
            //    this.columnHeaderU.Width = 0;
            //    this.columnHeaderV.Width = 0;
            //    this.columnHeaderW.Width = 0;
            //    this.columnHeaderX.Width = 0;
            //    this.columnHeaderY.Width = 0;
            //    this.columnHeaderZ.Width = 0;
            //    this.columnHeaderZA.Width = 0;
            //    this.columnHeaderZB.Width = 0;
            //    this.columnHeaderZC.Width = 0;
            //    this.columnHeaderZD.Width = 0;
            //    this.columnHeaderZE.Width = 0;
            //    this.columnHeaderZF.Width = 0;
            //    this.columnHeaderZG.Width = 0;
            //    this.columnHeaderZG.Width = 0;
                

            //}
        }
        string oldid ="";
        int oldcol = -1;
        int[] flightInItem = new int[2];//选中的航班在items中的索引

        float zhekou1 = 1F;
        float zhekou2 = 1F;
        string[] a_flightno = new string[4] { "","","",""};
        string[] a_citypair= new string[4] { "","","",""};
        string[] a_bunk= new string[4] { "","","",""};
        string[] a_date= new string[4] { "","","",""};

        int m_clickrow = 0;
        int m_clickcol = 0;
        private void lv_查询结果_MouseClick(object sender, MouseEventArgs e)
        {

            EagleControls.Operators.ListView_PositionOfMouse(sender as ListView, e, ref m_clickrow, ref m_clickcol);
            #region//右键翻页
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu rightMenu = new ContextMenu();
                if (b_BookTicketAv)
                {
                    rightMenu.MenuItems.Add("查询价格", new EventHandler(querryFC));
                    rightMenu.MenuItems.Add("查询该舱位价格", new EventHandler(querryBunkFC));
                    rightMenu.MenuItems.Add("查询经停", new EventHandler(querryFF));
                }
                
                if (b_pb)
                {
                    rightMenu.MenuItems.Add("上一页", new EventHandler(back));
                }
                if (b_pn)
                {
                    rightMenu.MenuItems.Add("下一页", new EventHandler(next));
                }
                rightMenu.MenuItems.Add("-");

                EventHandler eh2 = new EventHandler(lsT);
                string temp = "";
                if (this.columnHeaderR.Width == 0)
                {
                    Len1 = 0;
                    Len2 = 46;
                    temp = "显示特殊舱位";
                }
                else
                {
                    Len1 = 46;
                    Len2 = 0;
                    temp = "显示正常折扣舱位";// "隐藏特殊舱位";
                }
                rightMenu.MenuItems.Add(temp, eh2);

                EventHandler eh5 = new EventHandler(lsNoSeatBunk);
                if (GlobalVar.b_ListNoSeatBunk) temp = "隐藏无座舱位";
                else temp = "显示无座舱位";
                rightMenu.MenuItems.Add(temp, eh5);


                rightMenu.MenuItems.Add("-");

                EventHandler eh4 = new EventHandler(selectibe);
                temp = (bIbe ? "取消使用IBE接口" : "选择使用IBE查询");
                rightMenu.MenuItems.Add(temp, eh4);

                System.Drawing.Point ep = new System.Drawing.Point(e.X, e.Y);
                rightMenu.Show(lv_查询结果, ep);
                return;
            }
            #endregion
            //中键
            if (e.Button == MouseButtons.Middle)
            {
                return;
            }
            //取得点击列位置

            b_reSelected = true;

            //a_flightno = new string[] { tb_航班1.Text, tb_航班2.Text, textBox5.Text, textBox9.Text };
            //a_citypair = new string[] { tb_CityPare1.Text, tb_CityPare2.Text, textBox4.Text, textBox8.Text };
            //a_bunk = new string[] { tb_舱位1.Text, tb_舱位2.Text, textBox3.Text, textBox7.Text };
            //a_date = new string[] { tb_日期1.Text, tb_日期2.Text, textBox2.Text, textBox6.Text };

            if (!EagleControls.Operators.ListView_FlightInfoOfClickItem(lv_查询结果, e, imageList1, dtp_机票日期.Value,
                ref a_flightno, ref a_citypair, ref a_bunk, ref a_date)) return;
            tb_航班1.Text = a_flightno[0];
            tb_航班2.Text = a_flightno[1];
            textBox5.Text = a_flightno[2];
            textBox9.Text = a_flightno[3];

            tb_CityPare1.Text = EagleString.EagleFileIO.CityPairCnName(a_citypair[0]);
            tb_CityPare2.Text = EagleString.EagleFileIO.CityPairCnName(a_citypair[1]);
            textBox4.Text = EagleString.EagleFileIO.CityPairCnName(a_citypair[2]);
            textBox8.Text = EagleString.EagleFileIO.CityPairCnName(a_citypair[3]);

            tb_舱位1.Text = t_bunkstring(a_bunk[0], a_flightno[0]);
            tb_舱位2.Text = t_bunkstring(a_bunk[1], a_flightno[1]);
            textBox3.Text = t_bunkstring(a_bunk[2], a_flightno[2]);
            textBox7.Text = t_bunkstring(a_bunk[3], a_flightno[3]);

            tb_日期1.Text = t_datestring(a_date[0]);
            tb_日期2.Text = t_datestring(a_date[1]);
            textBox2.Text = t_datestring(a_date[2]);
            textBox6.Text = t_datestring(a_date[3]);

            string s_tktltime = "";
            string s_tktlday = "";
            EagleControls.Operators.LimitTime(a_date, ref s_tktlday, ref s_tktltime);

            tb_时限时间.Text = s_tktltime;
            dtp_时限日期.Value = EagleString.BaseFunc.str2datetime(s_tktlday, true);

            this.ActiveControl = tb_人数;

            return;
            #region 注释掉的旧代码
            //try
            //{
                
            //    int clickcolumn = -1;
            //    int clickrow = -1;
            //    EagleControls.Operators.ListView_PositionOfMouse(lv_查询结果, e, imageList1, ref clickrow, ref clickcolumn);

            //    clickrow = lv_查询结果.SelectedItems[0].Index;
            //    if (clickcolumn < 0) return;


            //    //根据点的位置取得ID
            //    string id = lv_查询结果.Items[clickrow].SubItems[0].Text;
            //    int curcol = clickcolumn;

            //    //判断该航程是否为联程，即有两个ID相同
            //    int sameid = 0;
            //    for (int i = 0; i < lv_查询结果.Items.Count; i++)
            //    {
            //        if (lv_查询结果.Items[i].SubItems[0].Text == id)
            //        {
            //            sameid++;
            //        }
            //    }
            //    bool b_联程 = false;
            //    if (sameid > 1) b_联程 = true;
            //    if ((b_联程 || tb_航班1.Text.Trim() == "" || tb_航班2.Text.Trim() != "" || oldcol != curcol || oldid !=id) 
            //        && !cb_返程.Checked)
            //    {
            //        oldcol = curcol;
            //        if (oldid != id)
            //        {
            //            oldid = id;

            //            tb_航班1.Text = tb_航班2.Text = tb_舱位1.Text = tb_舱位2.Text = tb_日期2.Text = tb_日期1.Text = "";
            //            lb_舱位1.Text = lb_舱位2.Text = "";
            //            tb_CityPare1.Text = tb_CityPare2.Text = "";
            //            //取该航程的航班数
            //            int count = 0;

            //            for (int i = 0; i < lv_查询结果.Items.Count; i++)
            //            {
            //                if (lv_查询结果.Items[i].SubItems[0].Text == id)
            //                {
            //                    count++;
            //                    if (count > 2) break;
            //                    flightInItem[count - 1] = i;
            //                }
            //            }
            //            //置tb_航班
            //            tb_航班1.Text = lv_查询结果.Items[flightInItem[0]].SubItems[1].Text;
            //            if (tb_航班1.Text.IndexOf("*") > -1)
            //            {
            //                MessageBox.Show("注意:共享航班可能订座无效,需要确认请联系您的机票供应商!否则产生的亏损自负!");
            //            }
            //            tb_CityPare1.Text = lv_查询结果.Items[flightInItem[0]].SubItems[3].Text;
            //            if (count == 2)
            //            {
            //                tb_航班2.Text = lv_查询结果.Items[flightInItem[1]].SubItems[1].Text;
            //                string tempA = lv_查询结果.Items[flightInItem[1]].SubItems[3].Text.Trim();
            //                tb_CityPare2.Text = tb_CityPare1.Text.Trim().Substring(3, 3) + tempA.Substring(tempA.Length - 3);
            //                //if (tb_CityPare2.Text.Length > 6) tb_CityPare2.Text = tb_CityPare2.Text.Substring(tb_CityPare2.Text.Length - 6);
            //            }
            //        }

            //        //置tb_舱位
            //        if (clickcolumn >= 7)
            //        {
            //            try
            //            {
            //                for (int i = 0; i < flightInItem.Length; i++)
            //                {
            //                    if (clickrow == flightInItem[i])
            //                    {
            //                        char cw;
            //                        cw = (char)(clickcolumn + ('A' - 7));
            //                        if (i == 0 && lv_查询结果.Items[clickrow].SubItems[clickcolumn].Text.Length > 1)
            //                        {
            //                            //tb_舱位1.Text = cw.ToString();
            //                            tb_舱位1.Text = lv_查询结果.Items[clickrow].SubItems[clickcolumn].Text[0].ToString();
            //                            lb_舱位1.Text = lv_查询结果.Items[clickrow].SubItems[clickcolumn].Text[1].ToString();
            //                            zhekou1 = (29F - clickcolumn) / 20F;
            //                            #region 计算票面返点及实收
            //                            try
            //                            {
            //                                string strNeed = EagleAPI2.getRebateFromPolicyXml(tb_航班1.Text.ToUpper(), tb_舱位1.Text.ToUpper());
            //                                int zk100 = (29 - clickcolumn) * 5;// (int)(zhekou1 * 100F);
            //                                if (zk100 < 0) zk100 = 0;
            //                                if (strNeed != "")
            //                                {

            //                                    if (zk100 > 40 && zk100.ToString() != strNeed.Split('~')[0])
            //                                    {
            //                                        MessageBox.Show("本地折扣与服务器折扣不一致！具体票价请联系管理员！");
            //                                    }
            //                                    zk100 = int.Parse(strNeed.Split('~')[0]);
            //                                }
                                            
            //                                string fare1 = string.Format("{0}", ((int)((float)(zk100) * f_Bunk_Y / 100F + 5F) / 10 * 10));
            //                                int taxb = int.Parse(easyTax.GetTaxBuild(lv_查询结果.Items[clickrow].SubItems[6].Text));
            //                                int taxf = int.Parse(easyTax.GetTaxFuel(distance.ToString()));
            //                                int tax = taxb + taxf;
            //                                GlobalVar2.strFare_Tax_Gain_1 = fare1 + "~" + taxb + "~" + taxf + "~" +
            //                                    (strNeed == "" ? ListPolicy.defaultGain : strNeed.Split('~')[1]);
            //                            }
            //                            catch
            //                            {
            //                                GlobalVar2.strFare_Tax_Gain_1 = "0~0~0~0";
            //                            }
            //                            #endregion
            //                        }
            //                        if (i == 1 && lv_查询结果.Items[clickrow].SubItems[clickcolumn].Text.Length > 1)
            //                        {
            //                            //tb_舱位2.Text = cw.ToString();
            //                            tb_舱位2.Text = lv_查询结果.Items[clickrow].SubItems[clickcolumn].Text[0].ToString();
            //                            lb_舱位2.Text = lv_查询结果.Items[clickrow].SubItems[clickcolumn].Text[1].ToString();
            //                            zhekou2 = (29F - clickcolumn) / 20F;
            //                            #region 计算票面返点及实收
            //                            try
            //                            {
            //                                string strNeed = EagleAPI2.getRebateFromPolicyXml(tb_航班2.Text.ToUpper(), tb_舱位2.Text.ToUpper());
            //                                int zk100 = (29 - clickcolumn) * 5;
            //                                if (zk100 < 0) zk100 = 0;
            //                                if (strNeed != "")
            //                                {

            //                                    if (zk100 > 40 && zk100.ToString() != strNeed.Split('~')[0])
            //                                    {
            //                                        MessageBox.Show("本地折扣与服务器折扣不一致！具体票价请联系管理员！");
            //                                    }
            //                                    zk100 = int.Parse(strNeed.Split('~')[0]);
            //                                }
                                            
            //                                string fare1 = string.Format("{0}", ((int)((float)(zk100) * f_Bunk_Y / 100F + 5F) / 10 * 10));
            //                                int taxb = int.Parse(easyTax.GetTaxBuild(lv_查询结果.Items[clickrow].SubItems[6].Text));
            //                                int taxf = int.Parse(easyTax.GetTaxFuel(distance.ToString()));
            //                                int tax = taxb + taxf;
            //                                GlobalVar2.strFare_Tax_Gain_2 = fare1 + "~" + taxb + "~" + taxf + "~" +
            //                                    (strNeed == "" ? "0" : strNeed.Split('~')[1]);
            //                            }
            //                            catch
            //                            {
            //                                GlobalVar2.strFare_Tax_Gain_2 = "0~0~0~0";
            //                            }
            //                            #endregion
            //                        }
            //                    }
            //                }
            //            }
            //            catch
            //            {
            //            }
            //        }
            //        //置tb_日期
            //        tb_日期1.Text = dtp_机票日期.Value.Day.ToString("d2") + getmonthstring(dtp_机票日期.Value.Month);
            //        fdIbe1 = dtp_机票日期.Value.ToString("yyyy-MM-dd");
            //        if (flightInItem.Length > 1) tb_日期2.Text = tb_日期1.Text;
            //        if (tb_航班2.Text.Trim() == "")
            //        {
            //            tb_舱位2.Text = tb_日期2.Text = lb_舱位2.Text = tb_CityPare2.Text = "";
            //        }
            //    }
            //    else//追加第二个航班
            //    {
            //        if (cb_返程.Checked)
            //        {
            //            int flightPos = -1;
            //            for (int i = 0; i < lv_查询结果.Items.Count; i++)
            //            {
            //                if (lv_查询结果.Items[i].SubItems[0].Text == id)
            //                {
            //                    flightPos = i;
            //                    break;
            //                }
            //            }
            //            if (tb_航班1.Text != lv_查询结果.Items[flightPos].SubItems[1].Text)
            //            {
            //                tb_航班2.Text = lv_查询结果.Items[flightPos].SubItems[1].Text;
            //                tb_CityPare2.Text = lv_查询结果.Items[flightPos].SubItems[3].Text;
            //                tb_日期2.Text = dtp_机票日期.Value.Day.ToString("d2") + getmonthstring(dtp_机票日期.Value.Month);
            //                fdIbe2 = dtp_机票日期.Value.ToString("yyyy-MM-dd");
            //                //置tb_舱位

            //                if (clickcolumn >= 7)
            //                {
            //                    char cw = (char)(clickcolumn + ('A' - 7));
            //                    tb_舱位2.Text = lv_查询结果.Items[clickrow].SubItems[clickcolumn].Text[0].ToString();
            //                    lb_舱位2.Text = lv_查询结果.Items[clickrow].SubItems[clickcolumn].Text[1].ToString();

            //                    #region 计算票面返点及实收
            //                    try
            //                    {
            //                        string strNeed = EagleAPI2.getRebateFromPolicyXml(tb_航班2.Text.ToUpper(), tb_舱位2.Text.ToUpper());
            //                        int zk100 = (29 - clickcolumn) * 5;
            //                        if (zk100 < 0) zk100 = 0;
            //                        if (strNeed != "")
            //                        {

            //                            if (zk100 > 40 && zk100.ToString() != strNeed.Split('~')[0])
            //                            {
            //                                MessageBox.Show("本地折扣与服务器折扣不一致！具体票价请联系管理员！");
            //                            }
            //                            zk100 = int.Parse(strNeed.Split('~')[0]);
            //                        }
                                    
            //                        string fare1 = string.Format("{0}", ((int)((float)(zk100) * f_Bunk_Y / 100F + 5F) / 10 * 10));
            //                        int taxb = int.Parse(easyTax.GetTaxBuild(lv_查询结果.Items[clickrow].SubItems[6].Text));
            //                        int taxf = int.Parse(easyTax.GetTaxFuel(distance.ToString()));
            //                        int tax = taxb + taxf;
            //                        GlobalVar2.strFare_Tax_Gain_2 = fare1 + "~" + taxb + "~" + taxf + "~" +
            //                            (strNeed == "" ? "0" : strNeed.Split('~')[1]);
            //                    }
            //                    catch
            //                    {
            //                        GlobalVar2.strFare_Tax_Gain_2 = "0~0~0~0";
            //                    }
            //                    #endregion
            //                }
            //            }
            //        }
            //    }
            //    if (tb_航班1.Text.Length > 0) tb_航班1.Text = tb_航班1.Text.Substring(tb_航班1.Text[0] == '*' ? 1 : 0);
            //    if (tb_航班2.Text.Length > 0) tb_航班2.Text = tb_航班2.Text.Substring(tb_航班2.Text[0] == '*' ? 1 : 0);
            //    b_reSelected = true;

            //    //时限
            //    if (!cb_返程.Checked)
            //    {
            //        try
            //        {
            //            string limitime = lv_查询结果.Items[clickrow].SubItems[4].Text;
            //            int lt = int.Parse(limitime);
            //            lt = lt - 300;
            //            if (lt < 0)
            //            {
            //                dtp_时限日期.Value = dtp_时限日期.Value.AddDays(-1);
            //                lt += 2400;

            //            }
            //            tb_时限时间.Text = lt.ToString("D4");
            //        }
            //        catch
            //        {
            //            tb_时限时间.Text = "0000";
            //        }
            //    }

            //}
            //catch
            //{
            //}
            //this.ActiveControl = tb_人数;
#endregion
        }
        string t_bunkstring(string b, string f)
        {
            if (b == "") return "";
            if (f == "") return "";
            return b + EagleString.EagleFileIO.RebateOf(b[0], f).ToString();
        }
        string t_datestring(string d)
        {
            try
            {
                return EagleString.BaseFunc.str2datetime(d, true).ToString("MM.dd");
            }
            catch
            {
                return "";
            }
        }

        private void bt_添加姓名_Click(object sender, EventArgs e)
        {
            if (tb_姓名.Text.Trim() == "") return;
            if (tb_CardNo.Text.Trim() == "")
            {
                tb_CardNo.Text = "0";
            }
            for (int i = 0; i < tb_CardNo.Text.Length; i++)
            {
                if (tb_CardNo.Text[i] > 'z')
                {
                    MessageBox.Show("请确认使用半角输入证件号码！");
                    return;
                }
            }
            string temp = "";
            if (!cbPspt.Checked)
            {
                switch (cbTicketType.SelectedIndex)
                {
                    case 0:
                        temp = tb_姓名.Text.Trim();
                        break;
                    case 1:
                        string dd = DateTime.Parse(child_birth).Day.ToString("d2");
                        string mm = EagleAPI.GetMonthCode(DateTime.Parse(child_birth).Month);
                        string yy = DateTime.Parse(child_birth).Year.ToString().Substring(2);
                        temp = tb_姓名.Text.Trim() + "CHD" + "(" + dd + mm + yy + ")";
                        bHasChild = true;
                        break;
                    case 2:
                        temp = tb_姓名.Text.Trim() + "YSD";
                        break;
                    case 3:
                        temp = tb_姓名.Text.Trim() + "YDT";
                        break;

                }
            }
            else
            {
                string[] testpspt = tb_CardNo.Text.Split('/');
                if (testpspt.Length < 3)
                {
                    MessageBox.Show("护照输入格式：护照号码/国籍/旅客生日/旅客姓/旅客名/性别及婴儿标识/持有人标识");
                    return;
                }
            }
            lb_姓名.Items.Add(temp);
            lb_CardNo.Items.Add(tb_CardNo.Text.Trim());
            tb_姓名.Clear();
            tb_CardNo.Clear();
        }

        private void bt_删除_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lb_姓名.SelectedItems.Count; i++)
            {
                
                lb_CardNo.Items.Remove(lb_CardNo.Items[lb_姓名.SelectedIndices[i]]);
                lb_姓名.Items.Remove(lb_姓名.SelectedItems[i]);
                
            }
        }

        private void bt_清除_Click(object sender, EventArgs e)
        {
            tb_航班1.Text = tb_CityPare1.Text = tb_舱位1.Text = tb_日期1.Text = "";
            int i = 0;
            a_date[i] = a_flightno[i] = a_citypair[i] = a_bunk[i] = "";
        }
        private void btnClear2Click(object sender, EventArgs e)
        {
            tb_航班2.Text = tb_CityPare2.Text = tb_舱位2.Text = tb_日期2.Text = "";
            int i = 1;
            a_date[i] = a_flightno[i] = a_citypair[i] = a_bunk[i] = "";

        }
        private void btnClear3Click(object sender, EventArgs e)
        {
            textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = "";
            int i = 2;
            a_date[i] = a_flightno[i] = a_citypair[i] = a_bunk[i] = "";

        }
        private void btnClear4Click(object sender, EventArgs e)
        {
            textBox6.Text = textBox7.Text = textBox8.Text = textBox9.Text = "";
            int i = 3;
            a_date[i] = a_flightno[i] = a_citypair[i] = a_bunk[i] = "";

        }
        private void bt_退出_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lv_查询结果_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void lv_查询结果_MouseMove(object sender, MouseEventArgs e)
        {
            int clickcolumn = -1;
            int clickrow = -1;

            EagleControls.Operators.ListView_PositionOfMouse(lv_查询结果, e, imageList1, ref clickrow, ref clickcolumn);
            if (lv_查询结果.Items.Count < clickrow) return;
            //toolTip.SetToolTip(lv_查询结果, clickrow.ToString());
            try
            {
                float ftemp = 0F;
                string tipstring = "";
                #region tootip
                string txt = lv_查询结果.Items[clickrow].SubItems[clickcolumn].Text;
                if (txt.Length == 0) return;
                int s_price = 0;
                if (clickcolumn >= 7)
                {
                    char s_bunk = txt[0];//here if error, catched......
                    
                    string s_bunkCn = "经济舱";
                    int s_rebate = 0;
                    if (clickcolumn == 7) { s_bunkCn = "头等舱"; s_rebate = 150; }
                    else if (clickcolumn == 8) { s_bunkCn = "商务舱"; s_rebate = 130; }
                    //else if (clickcolumn >= 9 && clickcolumn <= 23) { s_rebate = 5 * (29 - clickcolumn); }//5->4
                    else if (clickcolumn >= 9 && clickcolumn <= 23) { s_rebate = 100-(clickcolumn-9)*4; }//5->4
                    else if (clickcolumn > 23) { s_bunkCn = "特殊舱"; s_rebate = 0; }
                    s_price = EagleString.egString.TicketPrice((int)f_Bunk_Y, s_rebate);
                    tipstring += string.Format("舱位代号：\t{0}\n", s_bunk);
                    tipstring += string.Format("价格(不含税)：\t{0}\t{1}\n", s_price.ToString("f2"), s_bunkCn);
                    tipstring += string.Format("备注：\t{0}\n\t",txt);
                    if (txt.Length ==2)
                        tipstring += getbunkinfo(txt[1]);
                    else
                        tipstring += "此航班无此舱位编号！";
                }
                if (clickcolumn == 1)
                {
                    tipstring = txt;
                    tipstring = tipstring.Substring(tipstring[0] == '*' ? 1 : 0, 2);
                    tipstring = "[" + tipstring + "] - " + EagleAPI.GetAirLineName(tipstring);
                }
                #endregion
                string s_planetype = lv_查询结果.Items[clickrow].SubItems[6].Text;
                string s_distance = (distance == 0 ? "“请点右键查询价格”" : distance.ToString());
                int s_taxbuild = EagleString.EagleFileIO.TaxOfBuildBy(s_planetype);
                int s_taxfuel = EagleString.EagleFileIO.TaxOfFuelBy(distance);
                tipstring += "\n直飞距离：" + s_distance + "公里";//机建燃油税请参考实时价格！";
                tipstring += "\n机建：" + s_taxbuild.ToString();
                tipstring += "\n(机场建设税一般以机型为依据,普通客机为50,货客混运为10,国际运输为90.以民航实际定价为准";
                tipstring += "\n燃油：" + s_taxfuel.ToString();
                tipstring += "\n(800公里以下"
                    +EagleString.EagleFileIO.TaxOfFuelBy(799).ToString()
                    + "元,800公里(含)以上" 
                    + EagleString.EagleFileIO.TaxOfFuelBy(799).ToString() 
                    + "元,以民航实际定价为准,列表中第一行兰色行中,\"距离\"右边显示为公里数)";
                int s_sum = s_price + s_taxbuild + s_taxfuel;
                tipstring += "\n合计：" + (s_price != 0 ? s_sum.ToString() : "无票面价") + "实际价格以PAT为准!";
                tipstring += "\n\n注：以上均以成人计算（儿童：无机建，燃油为成人的一半）";

                string s_flight = lv_查询结果.Items[clickrow].SubItems[1].Text;
                if (clickcolumn >= 7)
                {
                    try
                    {
                        string al = lv_查询结果.Items[clickrow].SubItems[1].Text;
                        tipstring += EagleString.EagleFileIO.TGQ_RULE(al, txt[0].ToString());

                        if (clickcolumn > 23)
                        {
                            string fno = EagleString.egString.trim(s_flight, " *");
                            string rb = easyTax.GetSpecialRebate(fno + "-" + txt[0].ToString()).Trim();
                            int spPrice = EagleString.egString.TicketPrice((int)f_Bunk_Y, Convert.ToInt32(rb));
                            string strSp = string.Format("特价票计算：原价{0}*折扣{1}%+机建{2}+燃油{3}=合计{4}(实际票价以PAT:A为准！)",
                                f_Bunk_Y, 
                                rb, 
                                s_taxbuild,
                                s_taxfuel,
                                spPrice + s_taxbuild + s_taxfuel
                                );
                            tipstring += "\r\n****************************************************************\r\n";
                            tipstring += "\r\n" + strSp;

                        }
                    }
                    catch
                    { }
                   
                }
                if(clickrow==0)
                    toolTip.SetToolTip(lv_查询结果, "此行为距离价格，请往下移动鼠标：）");
                else
                    toolTip.SetToolTip(lv_查询结果, tipstring);
            }
            catch(Exception ex)
            {
                //EagleString.EagleFileIO.LogWrite("中文专业版的AV查询结果MouseMove : " + ex.Message);
            }
            
            
        }
        string getbunkinfo(char ch)
        {
            return EagleString.BaseFunc.BunkCnMean(ch);
        }

        private void BookTicket_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (GlobalVar2.gbUsingBlackWindows)
            //    this.Hide();
            //else
            {
                timer1.Stop();
                b_BookTicketAv = false;
                b_book = false;
                b_BookWndOpen = false;
                cn.Close();
                if (!Model.md.b_004) Application.Exit();
            }
        }


        public static bool b_book = false;

        private void bt_订票_Click(object sender, EventArgs e)
        {

            if (!b_reSelected) { MessageBox.Show("请重新选择航班！"); bt_提取_Click(sender, e); return; }
            b_bookticket_提取 = false;
            b_bookticket_fd = false;
            b_BookTicketAv = false;
            b_book = true;

            tb_电脑号.Text = "";
            //人数是否一致
            char newline = '\xD';
            if (tb_人数.Text.Trim() != lb_姓名.Items.Count.ToString())
            {
                MessageBox.Show("人数不一致！");
                return;
            }
            try
            {
                if (int.Parse(tb_人数.Text.Trim()) > 9)
                {
                    MessageBox.Show("简易界面不支持团体订票功能");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("人数只能为数字");
                return;
            }
            //出票时限是否正确
            if(tb_时限时间.Text.Trim().Length!=4)
            {
                MessageBox.Show("时限时间错误，格式为HHMM，如21:18表示为2118");
                return;
            }
            //联程航段的正确性
            bool bAddsa = false;
            if (tb_航班2.Text.Trim() != "")
            {
                //if (EagleAPI.substring(this.tb_CityPare1.Text.Trim(), 3, 3) != EagleAPI.substring(this.tb_CityPare2.Text.Trim(), 0, 3))
                //{
                //    if (MessageBox.Show("联程航段中间点不对应，是否添加地面段", "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                //        == DialogResult.No)
                //        return;
                //    else bAddsa = true;
                //}
            }
            int i_hour = 0;
            int i_minute = 0;
            try
            {
                i_hour = int.Parse(tb_时限时间.Text.Trim().Substring(0, 2));
                i_minute = int.Parse(tb_时限时间.Text.Trim().Substring(2, 2));
            }
            catch
            {
                MessageBox.Show("时限时间错误，格式为HHMM，如21:18表示为2118");
                return;
            }
            System.DateTime dt = new DateTime(dtp_时限日期.Value.Year,dtp_时限日期.Value.Month,dtp_时限日期.Value.Day,i_hour,i_minute,0);



            if (System.DateTime.Now > dt)
            {
                MessageBox.Show("出票时限不正确，请正确设置系统时间，或选择正确出票时限\r\n时限时间必须小于飞机起飞时间，并且大于当前时间");
                return;
            }
            string tele = tb_电话.Text.Trim();
            for (int i = 0; i < tele.Length; i++)
            {
                if ((tele[i] >= 'A' && tele[i] <= 'Z') || (
                    tele[i] >= '0' && tele[i] <= '9') || 
                    (tele[i] >= 'a' && tele[i] <= 'z') || tele [i]==' ' || tele[i]=='-') ;
                else
                {
                    //MessageBox.Show("电话号码错误，只能包含数字及字母、空格、-，否则将导致订票失败");
                    //return;
                }
            }

            //string[] a_flightno = new string[] { tb_航班1.Text, tb_航班2.Text, textBox5.Text, textBox9.Text };
            //string[] a_citypair = new string[] { tb_CityPare1.Text, tb_CityPare2.Text, textBox4.Text, textBox8.Text };
            //string[] a_bunk = new string[] { tb_舱位1.Text, tb_舱位2.Text, textBox3.Text, textBox7.Text };
            //string[] a_date = new string[] { tb_日期1.Text, tb_日期2.Text, textBox2.Text, textBox6.Text };

            char[] c_bunk = new char[a_bunk.Length];
            DateTime[] c_date = new DateTime[a_bunk.Length];
            for (int i = 0; i < c_bunk.Length; ++i)
            {
                if (a_bunk[i] != "") c_bunk[i] = a_bunk[i][0];
                if (a_date[i] != "") c_date[i] = EagleString.BaseFunc.str2datetime(a_date[i],true);
            }
            List<string> _ls = new List<string>();
            for (int i = 0; i < lb_姓名.Items.Count; i++)
            {
                _ls.Add(lb_姓名.Items[i].ToString() + "-" + lb_CardNo.Items[i].ToString());
            }
            _ls.Sort();//这里严格来说要按照拼音的顺序排列，否则在黑屏中有可能导致错误

            List<string>ls_name = new List<string> ();
            List<string>ls_card = new List<string> ();
            for(int i=0;i<_ls.Count;++i)
            {
                ls_name.Add(_ls[i].Split('-')[0]);
                ls_card.Add(_ls[i].Split('-')[1]);
            }



            string sellseat = EagleString.CommandCreate.Create_SS_String
                (a_flightno, c_bunk, c_date, a_citypair, 
                _ls.Count, ls_name.ToArray(), ls_card.ToArray(), 
                tb_电话.Text.Trim(), "BJS757", new string[] { GlobalVar.loginName }
                );

            string cmd = sellseat;
            bHasChild = false;
            for (int i = 0; i < lb_姓名.Items.Count; i++)
            {

                if (lb_姓名.Items[i].ToString().IndexOf('(') > 0)
                {
                    bHasChild = true;//判断是否有儿童
                    break;
                }
            }
            tb_电脑号.Text = GlobalVar.WaitString;
            if (bHasChild  || !BookTicket.bIbe)
            {
                EagleAPI.CLEARCMDLIST(3);
                EagleAPI.EagleSendCmd(cmd, 3);
                b_reSelected = false;
                tb_电脑号.Text = GlobalVar.WaitString;
            }
            else
            {//使用ＩＢＥ接口订票
                //int segs = 0;
                //if (tb_航班1.Text != "") segs++;
                //if (tb_航班2.Text != "") segs++;
                //if (segs == 0) return;
                //string[] fn = new string[segs];
                //string[] ac = new string[segs];
                //string[] fd = new string[segs];
                //string[] oc = new string[segs];
                //string[] dc = new string[segs];
                //string[] bk = new string[segs];
                //string[] name = new string[int.Parse(tb_人数.Text.Trim())];
                //string[] ci = new string[int.Parse(tb_人数.Text.Trim())];
                //string[] rmk = null;
                //string ld;
                //fn[0] = tb_航班1.Text;
                //ac[0] = "LL";
                //fd[0] = fdIbe1;
                //oc[0] = tb_CityPare1.Text.Substring(0, 3);
                //dc[0] = tb_CityPare1.Text.Substring(3);
                //bk[0] = tb_舱位1.Text;

                //if (segs > 1)
                //{
                //    fn[1] = tb_航班2.Text;
                //    ac[1] = "LL";
                //    fd[1] = fdIbe2;
                //    oc[1] = tb_CityPare2.Text.Substring(0, 3);
                //    dc[1] = tb_CityPare2.Text.Substring(3);
                //    bk[1] = tb_舱位2.Text;
                //}
                //for (int nCounts = 0; nCounts < lb_姓名.Items.Count; nCounts++)
                //{
                //    string xm = lb_姓名.Items[nCounts].ToString();
                //    string hm = lb_CardNo.Items[nCounts].ToString();
                //    if (xm.IndexOf('(') > 0)
                //    {
                //        name[nCounts] = xm.Substring(0, xm.IndexOf('(') - 3);
                //    }
                //    else
                //    {
                //        name[nCounts] = xm;
                //        ci[nCounts] = hm;
                //    }
                //}
                //Options.ibe.ibeInterface ib = new Options.ibe.ibeInterface();
                //ld = dtp_时限日期.Value.ToString("yyyy-MM-dd") + " " +
                //    tb_时限时间.Text.Substring(0, 2) + ":" + tb_时限时间.Text.Substring(2) + ":00";
                string[] rmk = null;
                string ln = GlobalVar.loginName;
                for (int i = 0; i < ln.Length; i++)
                {
                    if ((ln[i] >= 'A' && ln[i] <= 'Z') || (
                    ln[i] >= '0' && ln[i] <= '9') ||
                    (ln[i] >= 'a' && ln[i] <= 'z'))
                    {
                        rmk = new string[] { "TELEPHONE " + tb_电话.Text, "USER " + ln };
                    }
                    else
                    {
                        rmk = new string[] { "TELEPHONE " + tb_电话.Text };
                    }
                }
                
                string pnr = "";
                //pnr = ib.ss(fn, ac, fd, oc, dc, bk, name, ld, ci, rmk);

                EagleExtension.EagleExtension.CreatePnrFromIbe
                    (a_flightno, c_date, a_citypair, c_bunk, ls_name.ToArray(), ls_card.ToArray(), rmk,ref pnr);
                if (pnr == null)
                {
                    tb_电脑号.Text = "订票错误";
                    return;
                }
                if (pnr.Length == 5)
                {
                    tb_电脑号.Text = pnr;
                    pnr_statistics ps = new pnr_statistics();
                    ps.pnr = pnr;
                    ps.state = "1";//产生,提交PNR状态
                    System.Threading.Thread subTh = new System.Threading.Thread(new System.Threading.ThreadStart(ps.submit1));
                    subTh.Start();
                }
                else
                {
                    tb_电脑号.Text = "订票错误";
                    MessageBox.Show(pnr);
                }
            }
            
        }

        private void tb_电脑号_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip.SetToolTip(this.tb_电脑号, tb_电脑号.Text);
        }
        string bookingpnr = "";
        public static bool b_bookticket_提取 = false;
        /// <summary>
        /// 将PNR保存至数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_提取_Click(object sender, EventArgs e)
        {
            if (!EagleAPI.IsRtCode(tb_电脑号.Text.Trim()))
            {
                //MessageBox.Show("请核查订座记录号，可能订票错误！");
                return;
            }
            if(lb_Saved .Items.Contains (tb_电脑号.Text ))
            {
                tb_电脑号.Text += "已保存";
                return;
            }
            //先保存入数据库
            string spliter = ";";
            try
            {
                string names = "";
                string cardnos = "";
                for(int i=0;i<lb_姓名.Items.Count;i++)
                {
                    names += spliter + lb_姓名.Items[i].ToString();
                    cardnos += spliter + lb_CardNo.Items[i].ToString();
                }
                if (names[0] == spliter[0]) names = names.Substring(1, names.Length - 1);
                if (cardnos[0] == spliter[0]) cardnos = cardnos.Substring(1, cardnos.Length - 1);
                string remark = GlobalVar2.strFare_Tax_Gain_1 + ";" + GlobalVar2.strFare_Tax_Gain_2;
                GlobalVar2.strFare_Tax_Gain_1 = GlobalVar2.strFare_Tax_Gain_2 = "0~0~0~0";

                string cmString = "insert into t_SimpleBookPNR ([User],Pnr,FlightNumber1,Bunk1,Date1,CityPair1,FlightNumber2,Bunk2,Date2,CityPair2,Phone,PersonCount,[Names],CardNumbers,TicketLimit,[State],remark1) values ";
                cmString += "('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}')";
                cmString = string.Format(cmString,
                    GlobalVar.loginName, 
                    tb_电脑号.Text.Trim(), 
                    a_flightno[0], 
                    a_bunk[0], 
                    a_date[0], 
                    a_citypair[0],
                    a_flightno[1], 
                    a_bunk[1], 
                    a_date[1], 
                    a_citypair[1],
                    tb_电话.Text.Trim(), 
                    tb_人数.Text.Trim(), 
                    names, 
                    cardnos,
                    dtp_时限日期.Value.ToShortDateString() + " " + tb_时限时间.Text.Trim(),
                    "0",
                    remark);
                bookingpnr = tb_电脑号.Text.Trim();
                OleDbCommand cmd = new OleDbCommand(cmString,cn);
                if (cmd.ExecuteNonQuery() != 1)
                {
                    MessageBox.Show("保存数据失败，可能导致提交不正常！");
                    return;
                }
            }
            catch
            {
                if (!GlobalVar.gbIsNkgFunctions)
                    MessageBox.Show("保存数据失败");
                else//提交订单按钮
                {
                }
                return;
            }

            //加入lb_Saved，重新读一次数据库，使dt同步
            ReadSavedPNR();
            tb_searchPNR.Text = tb_电脑号.Text;
            bHasChild = false;
            //tb_电脑号.Text += "已保存";
            try
            {
                lb_Saved.Items.Remove(tb_searchPNR.Text);
            }
            catch { }
            lb_Saved.Items.Insert(0, tb_searchPNR.Text);
        }
        private void dtp_机票日期_CloseUp(object sender, EventArgs e)
        {
            if (!cb_返程.Checked)
                dtp_时限日期.Value = dtp_机票日期.Value.AddDays(0);
            //if (bIbe) return;
            this.bt_查询_Click(sender, e);
            //this.bt_查询.Enabled = false;
        }
        private void dtp_机票日期_ValueChanged(object sender, EventArgs e)
        {

            
        }
        DataTable dt = new DataTable();
        /// <summary>
        /// 读数据库表t_SimpleBookPNR中保存(state = '0')的PNR至DT，并将PNR显示在lb_Saved中
        /// </summary>
        void ReadSavedPNR()
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand("select * from t_SimpleBookPNR where [user]='"+GlobalVar.loginName+"' AND [State]='0'", cn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                dt.Clear();
                adapter.Fill(dt);
                lb_Saved.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lb_Saved.Items.Add(dt.Rows[i]["Pnr"].ToString());
                }

            }
            catch
            {
                MessageBox.Show("读取本地数据库PNR表失败");
                return;
            }
        }

        /// <summary>
        /// 读取存在的票价
        /// </summary>
        /// <param name="sFrom"></param>
        /// <param name="sTo"></param>
        public void ReadLocalFC(string sFrom,string sTo)
        {
            int p = EagleString.EagleFileIO.PriceOf(sFrom + sTo);
            if (p != 0)
            {
                f_Bunk_Y = (float)p;
                distance = EagleString.EagleFileIO.DistanceOf(sFrom + sTo);
                f_Bunk_F = (float)distance;
            }
            else
            {
                GetServerFC();
            }
            return;
            //以下为以前的代码   2009-2-5
            if (cn.State != ConnectionState.Open)
                try
                {

                    string ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\data.mdb;";
                    cn.ConnectionString = ConnStr;
                    cn.Open();
                }
                catch
                {
                    MessageBox.Show("本地数据库连接失败");
                    this.Close();
                }
            try
            {
                OleDbCommand cmd = new OleDbCommand("select * from t_fc where [From]='" +sFrom + "'" + "and [To]='"+sTo+"'", cn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                DataTable dtTmp = new DataTable();
                adapter.Fill(dtTmp);
                if (dtTmp.Rows.Count != 0)
                {
                    f_Bunk_F = float.Parse(dtTmp.Rows[0]["BunkF"].ToString());
                    f_Bunk_C = float.Parse(dtTmp.Rows[0]["BunkC"].ToString());
                    f_Bunk_Y = float.Parse(dtTmp.Rows[0]["BunkY"].ToString());
                    distance = (int)f_Bunk_F;
                    if (f_Bunk_F == (float)((1.5F * f_Bunk_Y + 5F) / 10) * 10)
                        GetServerFC();
                }
                else//取服务器价格
                {
                    GetServerFC();
                }
            }
            catch(Exception ex)
            {
                EagleAPI .LogWrite("ReadLocalFC" +ex.Message);
                return;
            }
        }
        /// <summary>
        /// 取服务器价格
        /// </summary>
        void GetServerFC()
        {
            EagleWebService.kernalFunc kf = new EagleWebService.kernalFunc(GlobalVar.WebServer);
            int tf2 = 0;
            int tc2 = 0;
            int ty2 = 0;
            kf.FC_Read(fromto, ref tf2, ref tc2, ref ty2);
            f_Bunk_F = (float)tf2;
            f_Bunk_Y = (float)ty2;
            f_Bunk_C = (float)tc2;
            distance = tf2;
        }
        /// <summary>
        /// 将价格存入服务器
        /// </summary>
        void SaveServerFC()
        {
            EagleWebService.kernalFunc fc = new EagleWebService.kernalFunc(GlobalVar.WebServer);
            bool bsucc = true;
            fc.FC_Write(fromto, distance, (int)f_Bunk_C, (int)f_Bunk_Y, ref bsucc);

        }
        private void cb_到达地_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                bt_查询_Click(sender, e);
            }
        }

        private void DeleteOnePNR(string pnr)
        {
            if (pnr.Trim().Length != 5) return;
            try
            {
                OleDbCommand cmd = new OleDbCommand("Delete * FROM t_SimpleBookPNR where [User]='" + GlobalVar.loginName + "' AND Pnr = '"+pnr+"'", cn);
                if (cmd.ExecuteNonQuery() < 1)
                {
                    //MessageBox.Show("删除本地数据失败");
                }
            }
            catch
            {
                MessageBox.Show("删除本地数据库PNR表记录失败");
                return;
            }
        }
        /// <summary>
        /// lb_Saved的提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < lb_Saved.SelectedItems.Count; i++)
            {
                string passStr = "";

                DataTable dtTmp = new DataTable();
                string sqlString = "";
                sqlString = string.Format("SELECT * FROM t_SimpleBookPNR WHERE [User]='{0}' AND Pnr='{1}'", GlobalVar.loginName, lb_Saved.SelectedItems[i].ToString());
                OleDbCommand cmd = new OleDbCommand(sqlString, cn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                adapter.Fill(dtTmp);
                if (dtTmp != null)
                {
                    try
                    {
                        string NAMES = "";
                        string[] arr_name = dtTmp.Rows[0]["Names"].ToString().Split(';');
                        string[] arr_cardid = dtTmp.Rows[0]["CardNumbers"].ToString().Split(';');
                        for (int j = 0; j < arr_name.Length; j++)
                        {
                            NAMES += ";" + arr_name[j] + "-" + arr_cardid[j];
                        }
                        NAMES = NAMES.Substring(1);
                        passStr = NAMES;
                    }
                    catch
                    {
                        MessageBox.Show("组合名字发生意外错误，请再次尝试提交！");
                        return;
                    }
                }

                //for (int i = 0; i < lb_CardNo.Items.Count; i++)
                //{
                //    passStr += lb_姓名.Items[i].ToString() + "-" + lb_CardNo.Items[i].ToString() + ";";
                //}
                //if (passStr.Length > 0) passStr = passStr.Substring(0, passStr.Length - 1);
                try
                {
                    Options.BookSimpleRemark bs = new Options.BookSimpleRemark(passStr);
                    bs.ShowDialog();
                    strSubmitRemarkField = bs.remark;
                    GlobalVar2.gbPassegersInEasyVersion = bs.passString;

                }
                catch
                {
                }
                //1.上传数据，失败则返回
                if (!submitPnr(lb_Saved.SelectedItems[i].ToString())) continue; 
                //2.上传成功，将本地数据库中该记录状态(0:表示保存 1:表示其它)
                if (!updatePnrState(lb_Saved.SelectedItems[i].ToString())) continue;
                //2.1提交完后，清除全局变量
                GlobalVar2.gbPassegersInEasyVersion = "";
            }
            //3.同步dt
            ReadSavedPNR();
            //4.请求一次提交与已处理数据，并更新lb_Submited与lb_Operated
            button3_Click(sender, e);
            button5_Click(sender, e);
            
        }
        //
        /// <summary>
        /// 上传一个pnr
        /// </summary>
        /// <param name="pnr"></param>
        /// <returns></returns>
        bool submitPnr(string pnr)
        {
            BookSimple.SubmitPnr sp = 
                new ePlus.BookSimple.SubmitPnr(pnr, this.tb_电话.Text,
                dtp_时限日期.Value.ToShortDateString() + " " + tb_时限时间.Text.Trim(), strSubmitRemarkField);
            sp.Show();
            sp.threadSubmit();
            bool ret = sp.bSubmitSuccess;
            sp.Close();
            return ret;
            //DataTable dtTmp = new DataTable();
            //string sqlString = "";
            //sqlString=string.Format("SELECT * FROM t_SimpleBookPNR WHERE [User]='{0}' AND Pnr='{1}'", GlobalVar.loginName, pnr);
            //OleDbCommand cmd = new OleDbCommand(sqlString, cn);
            //OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            //adapter.Fill(dtTmp);
            //if (dtTmp.Rows.Count < 1) return false;

            //EasySubmitPnr sp = new EasySubmitPnr();
            //if (sp.submit_easy_pnr(pnr, dtp_时限日期.Value.ToShortDateString() + " " + tb_时限时间.Text.Trim(), dtTmp,strSubmitRemarkField,
            //    "0","0","0","0","0","0","0"))
            //{
            //    MessageBox.Show("恭喜" + pnr + "-提交成功！", "CONTRATUATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return true;
            //}
            //return false;
        }
        string strSubmitRemarkField = "";
        /// <summary>
        /// 更改本地数据库记录状态为1
        /// </summary>
        /// <param name="pnr"></param>
        /// <returns></returns>
        bool updatePnrState(string pnr)
        {
            try
            {
                string sqlString = "";
                sqlString = string.Format("update t_SimpleBookPNR set [State]='1' where [User]='{0}' AND Pnr='{1}'", GlobalVar.loginName, pnr);
                OleDbCommand cmd = new OleDbCommand(sqlString, cn);
                if (cmd.ExecuteNonQuery() < 1)
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 请求指令状态为state的PNR数据，并更新lb_Submited，state="0"为未处理，state="1"为已处理通过2为已处理未通过3为已经删除
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        string getsubmittedPNRs(string state)
        {
                            EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
            NewPara np = new NewPara();
            np.AddPara("cm", "GetPNRs");
            np.AddPara("User", GlobalVar.loginName);
            np.AddPara("PNRState", state);
            string strReq = np.GetXML();
            string strRet = ws.getEgSoap(strReq);
            if (strRet != "")
            {
                NewPara np1 = new NewPara(strRet);
                if (np1.FindTextByPath("//eg/cm") == "RetGetPNRs")
                {
                    strRet = np1.FindTextByPath("//eg/PNR");
                }
                else
                {
                    strRet ="";
                }
            }
            return strRet;
        }
        /// <summary>
        /// lb_Submited的更新按钮：更新lb_Submitted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            //System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(OnButton3Click));
            //th.Start();
            OnButton3Click();
        }
        void OnButton3Click()
        {
           // CheckForIllegalCrossThreadCalls = false;
            string[] lbString = getsubmittedPNRs("0").Split(';');
            lb_Submited.Items.Clear();
            for (int i = 0; i < lbString.Length; i++)
            {
                lb_Submited.Items.Add(lbString[i]);
            }
            //CheckForIllegalCrossThreadCalls = true;
        }
        /// <summary>
        /// lb_Operated的更新按钮：更新lb_Submitted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            OnButton5Click();
        }
        void OnButton5Click()
        {
            
            string[] lbString1 = getsubmittedPNRs("1").Split(';');
            lb_Operated.Items.Clear();
            for (int i = 0; i < lbString1.Length; i++)
            {
                lb_Operated.Items.Add(lbString1[i]);
            }
            

        }
        /// <summary>
        /// lb_Cancel的更新按钮：更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            //System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(OnButton7Click));
            //th.Start();
            OnButton7Click();
        }
        void OnButton7Click()
        {
            //CheckForIllegalCrossThreadCalls = false;
            string[] lbString1 = getsubmittedPNRs("2").Split(';');
            this.lb_Cancel.Items.Clear();
            for (int i = 0; i < lbString1.Length; i++)
            {
                this.lb_Cancel.Items.Add(lbString1[i]);
            }
            //CheckForIllegalCrossThreadCalls = true;
        }
        #region 所有删除按钮
        /// <summary>
        /// lb_Saved的删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK != MessageBox.Show("将删除，并且取消PNR(若已经出票，机票将无效!客人无法登机!)！！！\r请谨慎！！", "删除编码", MessageBoxButtons.OKCancel,MessageBoxIcon.Warning)) return;
            

            b_bookticket_提取 = false;
            b_bookticket_fd = false;
            b_BookTicketAv = false;
            b_book = false;
            for (int i = 0; i < lb_Saved.SelectedItems.Count; i++)
            {
                string pnr = lb_Saved.SelectedItems[i].ToString().Trim();
                //1.从数据库删除
                DeleteOnePNR(pnr);
                //2.执行删除指令
                EagleAPI.etstatic.Pnr = pnr;
                if (!BookTicket.bIbe)
                {
                    if (pnr.Length == 5) EagleAPI.EagleSendOneCmd("i~" + "rT" + pnr + "~xepnr@");
                }
                else
                {
                    Options.ibe.ibeInterface ib = new Options.ibe.ibeInterface();
                    MessageBox.Show(ib.xepnr(pnr, ""));
                }
                pnr_statistics ps = new pnr_statistics();
                ps.pnr = pnr;
                ps.state = "2";
                System.Threading.Thread th1 = new System.Threading.Thread(new System.Threading.ThreadStart(ps.submit1));
                th1.Start();
            }
            //3.同步dt
            ReadSavedPNR();

        }
        /// <summary>
        /// lb_Submited的删除按钮:取消PNR，删除数据库记录，删除服务器记录，更新lb_Submitted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// lb_Operated的删除按钮：删除数据库记录，更改服务器记录状态为删除，更新lb_Operated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lb_Operated.SelectedItems.Count; i++)
            {
                //1.更服服务器记录状态为删除，失败则下一下pnr
                if(!updateserverPNRtoDelete(lb_Operated.SelectedItems[i].ToString()))continue;
                //2.删除数据库记录
                DeleteOnePNR(lb_Operated.SelectedItems[i].ToString());
            }
            //3.更新lb_Operated
            button5_Click(sender, e);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.lb_Cancel.SelectedItems.Count; i++)
            {
                //1.更服服务器记录状态为删除，失败则下一下pnr
                //if (!updateserverPNRtoDelete(lb_Cancel.SelectedItems[i].ToString())) continue;
                //2.删除数据库记录
                DeleteOnePNR(lb_Cancel.SelectedItems[i].ToString());
            }
            //3.更新lb_Operated
            button7_Click(sender, e);
        }
        #endregion//02987433033
        /// <summary>
        /// 将服务器上的对应PNR设为删除状态,3
        /// </summary>
        /// <param name="pnr"></param>
        /// <returns></returns>
        bool updateserverPNRtoDelete(string pnr)
        {
                            EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
            NewPara np = new NewPara();
            np.AddPara("cm", "SetPNRStateDelete");
            np.AddPara("User", GlobalVar.loginName);
            np.AddPara("PNR", pnr.Trim());
            string strReq = np.GetXML();
            string strRet = ws.getEgSoap(strReq);
            if (strRet != "")
            {
                NewPara np1 = new NewPara(strRet);
                if (np1.FindTextByPath("//eg/cm") == "RetSetPNRStateDelete" && np1.FindTextByPath("//eg/OperationFlag")=="SaveSucc")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;

        }
        /// <summary>
        /// 读取指定PNR的所有信息，返回字符串，用于tooltip
        /// </summary>
        /// <param name="pnr"></param>
        /// <returns></returns>
        string readPNRinfo(string pnr)
        {
            string ret = "";
            try
            {
                string sqlString = "";
                sqlString = string.Format("SELECT * FROM t_SimpleBookPNR WHERE [User]='{0}' AND Pnr='{1}'", GlobalVar.loginName, pnr);
                OleDbCommand cmd = new OleDbCommand(sqlString, cn);
                OleDbDataAdapter adpater = new OleDbDataAdapter(cmd);
                DataTable dtTmp = new DataTable();
                adpater.Fill(dtTmp);
                if (dtTmp.Rows.Count < 1)
                {
                    return "";
                }
                else
                {
                    ret += "订座记录号(PNR)：" + pnr;
                    ret += "\r  航段1：";
                    ret += "\r  航班：　　　" + dtTmp.Rows[0]["FlightNumber1"].ToString()
                        + "\r  舱位：　　　" + dtTmp.Rows[0]["Bunk1"].ToString()
                        + "\r  乘机日期：　" + dtTmp.Rows[0]["Date1"].ToString()
                        + "\r  城市对：　　" + dtTmp.Rows[0]["CityPair1"].ToString();
                    if (dtTmp.Rows[0]["FlightNumber2"].ToString() != "")
                    {
                        ret += "\r  航段2：";
                        ret += "\r  航班：　　　" + dtTmp.Rows[0]["FlightNumber2"].ToString()
                            + "\r  舱位：　　　" + dtTmp.Rows[0]["Bunk2"].ToString()
                            + "\r  乘机日期：　" + dtTmp.Rows[0]["Date2"].ToString()
                            + "\r  城市对：　　" + dtTmp.Rows[0]["CityPair2"].ToString();
                    }
                    ret += "\r  姓名组：　　" + dtTmp.Rows[0]["Names"].ToString();
                    ret += "\r  证件号码组：" + dtTmp.Rows[0]["CardNumbers"].ToString();
                    ret += "\r  出票时限：　" + dtTmp.Rows[0]["TicketLimit"].ToString();
                }
               
            }
            catch
            {
                ret="数据错误！";
            }
            return ret;
        }
        public void setDisplayBook()
        {
            try
            {
                lbDisplay.Text = returnstring;
                lbDisplay.Text += "\r";
                if (tb_航班1.Text != "")
                {
                    lbDisplay.Text += "航班号：　" + tb_航班1.Text + "－" + EagleAPI.GetAirLineName(EagleAPI.substring(tb_航班1.Text, 0, 2)) + "\r";
                    if (tb_舱位1.Text.Trim().ToUpper() == "F")
                    {
                        lbDisplay.Text += "舱位：　　" + tb_舱位1.Text + "－头等舱\r";
                    }
                    else if (tb_舱位1.Text.Trim().ToUpper() == "C")
                    {
                        lbDisplay.Text += "舱位：　　" + tb_舱位1.Text + "－商务舱\r";
                    }
                    else if (tb_舱位1.Text.Trim().ToUpper() == "Y")
                    {
                        lbDisplay.Text += "舱位：　　" + tb_舱位1.Text + "－经济舱全价\r";
                    }
                    else
                    {
                        float fc = (float)((int)((zhekou1 * f_Bunk_Y + 5) / 10) * 10);
                        lbDisplay.Text += "舱位：　　" + tb_舱位1.Text + "－" + zhekou1.ToString("f2") + "折\r";// +"价格为(不含税)：" + fc.ToString("f2") + "\r";
                    }
                }
                if (tb_航班2.Text != "")
                {
                    lbDisplay.Text += "航班号：　" + tb_航班2.Text + "－" + EagleAPI.GetAirLineName(EagleAPI.substring(tb_航班2.Text, 0, 2)) + "\r";
                    if (tb_舱位2.Text.Trim().ToUpper() == "F")
                    {
                        lbDisplay.Text += "舱位：　　" + tb_舱位2.Text + "－头等舱\r";
                    }
                    else if (tb_舱位2.Text.Trim().ToUpper() == "C")
                    {
                        lbDisplay.Text += "舱位：　　" + tb_舱位2.Text + "－商务舱\r";
                    }
                    else if (tb_舱位2.Text.Trim().ToUpper() == "Y")
                    {
                        lbDisplay.Text += "舱位：　　" + tb_舱位2.Text + "－经济舱全价\r";
                    }
                    else
                    {
                        lbDisplay.Text += "舱位：　　" + tb_舱位2.Text + "－" + zhekou2.ToString("f2") + "折\r";// +"价格为(不含税)：" + fc.ToString("f2") + "\r";
                    }
                }
                else
                {
                    int fare = (int)((zhekou1 * f_Bunk_Y + 5) / 10) * 10;
                    float fc = (float)fare;// zhekou1* f_Bunk_Y;
                    lbDisplay.Text += "价格为(不含税)：" + fc.ToString("f2") + "\r";
                }
                lbDisplay.Text += "城市对：　" + tb_CityPare1.Text + "（飞城市：" + EagleAPI.GetCityCn("", tb_CityPare1.Text.Substring(0, 3)) + "－\t到达城市：" + EagleAPI.GetCityCn("", tb_CityPare1.Text.Substring(3)) + "）\r";
                lbDisplay.Text += "乘机日期：" + tb_日期1.Text + "\t－" + EagleAPI.GetMonthInt(tb_日期1.Text.Substring(tb_日期1.Text.Length - 3)) + "月" + tb_日期1.Text.Substring(0, tb_日期1.Text.Length - 3) + "日\r";
                lbDisplay.Text += "人数：　　" + lb_姓名.Items.Count.ToString() + "\r";
                lbDisplay.Text += "姓名：　　";
                for (int i = 0; i < lb_姓名.Items.Count; i++)
                {
                    lbDisplay.Text += lb_姓名.Items[i].ToString() + "，";
                }
                lbDisplay.Text += "\r联系电话：" + tb_电话.Text + "\r";
                lbDisplay.Text += "出票时限：" + dtp_时限日期.Value.ToShortDateString() + "  " + tb_时限时间.Text.Substring(0, 2) + "点" + tb_时限时间.Text.Substring(2, 2) + "分\r";
            }
            catch
            {
            }
        }
        #region ListBox的MouseHover事件
        private void lb_Saved_MouseHover(object sender, EventArgs e)
        {
            if (lb_Saved.SelectedItems.Count != 1) return;
            toolTip.SetToolTip(lb_Saved,readPNRinfo(lb_Saved.SelectedItem.ToString()));
        }


        private void lb_Submited_MouseHover(object sender, EventArgs e)
        {
            if (lb_Submited.SelectedItems.Count != 1) return;
            toolTip.SetToolTip(lb_Submited, readPNRinfo(lb_Submited.SelectedItem.ToString()));

        }

        private void lb_Operated_MouseHover(object sender, EventArgs e)
        {
            if (lb_Operated.SelectedItems.Count != 1) return;
            toolTip.SetToolTip(lb_Operated, readPNRinfo(lb_Operated.SelectedItem.ToString()));

        }

        private void lb_Cancel_MouseHover(object sender, EventArgs e)
        {
            if (lb_Cancel.SelectedItems.Count != 1) return;
            toolTip.SetToolTip(lb_Cancel, readPNRinfo(lb_Cancel.SelectedItem.ToString()));

        }
        #endregion

        private void bt_searchPNR_Click(object sender, EventArgs e)
        {
            Data.avDataSet ds = new ePlus.Data.avDataSet();
            ds.init(lv_查询结果,DateTime.Now .ToShortDateString ());
            if (tb_searchPNR.Text.Trim().Length != 5)
            {
                lbDisplay.Text = "注意：请输入正确的PNR！";
                return;
            }
            lbDisplay.Text = readPNRinfo(tb_searchPNR.Text.Trim().ToUpper());
            if (lbDisplay.Text == "")
            {
                lbDisplay.Text = "注意：没有相应的PNR信息！";
            }
        }

        private void tb_searchPNR_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                bt_searchPNR_Click(sender, e);
            }
        }

        private void lb_Submited_Click(object sender, EventArgs e)
        {
            if (lb_Submited.SelectedItems.Count != 1) return;
            tb_searchPNR.Text = lb_Submited.SelectedItem.ToString();
        }

        private void lb_Saved_Click(object sender, EventArgs e)
        {
            if (lb_Saved.SelectedItems.Count != 1) return;
            tb_searchPNR.Text = lb_Saved.SelectedItem.ToString();
        }

        private void lb_Operated_Click(object sender, EventArgs e)
        {
            if (lb_Operated.SelectedItems.Count != 1) return;
            tb_searchPNR.Text = lb_Operated.SelectedItem.ToString();
        }

        private void lb_Cancel_Click(object sender, EventArgs e)
        {
            if (lb_Cancel.SelectedItems.Count != 1) return;
            tb_searchPNR.Text = lb_Cancel.SelectedItem.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button3_Click(sender, e);
            button5_Click(sender, e);
            button7_Click(sender, e);

            Notice nt = new Notice();
            string tempnt = nt.get_notice_scroll(BookTicket.b_BookWndOpen ? "1" : "0");
            //lblNotice.Text = (tempnt == "" ? "滚动式公告栏" : tempnt);
        }
        private void lb_Operated_MouseUp(object sender, MouseEventArgs e)
        {
            if (lb_Operated.SelectedItems.Count != 1) return;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip rightMenu = new ContextMenuStrip();
                rightMenu.Items.Add("机票打印", null, new EventHandler(print0));
                rightMenu.Items.Add("行程单打印", null, new EventHandler(print1));


                if (Model.md.b_B01) rightMenu.Items.Add("华安－交通意外伤害保险单", null, new EventHandler(printB01));
                if (Model.md.b_B02) rightMenu.Items.Add("人寿－航空人身意外伤害保险单", null, new EventHandler(printB02));
                if (Model.md.b_B03) rightMenu.Items.Add("都邦－航翼网航意险", null, new EventHandler(printB03));
                if (Model.md.b_B04) rightMenu.Items.Add("都邦－出行无忧", null, new EventHandler(printB04));
                if (Model.md.b_001) rightMenu.Items.Add("PICC－任我游", null, new EventHandler(print001));
                if (Model.md.b_007) rightMenu.Items.Add("永安－交意险", null, new EventHandler(print007));
                if (Model.md.b_009) rightMenu.Items.Add("新华－保险打印", null, new EventHandler(print009));
                if (Model.md.b_B05) rightMenu.Items.Add("都帮－出行乐", null, new EventHandler(printB05));
                if (Model.md.b_B06) rightMenu.Items.Add("平安－周游列国", null, new EventHandler(printB06));
                if (Model.md.b_B09) rightMenu.Items.Add("易格－新华人寿", null, new EventHandler(printB09));
                if (Model.md.b_B0B) rightMenu.Items.Add("易格－安帮商行通", null, new EventHandler(printB0B));
                rightMenu.Show(lb_Operated,e.X, e.Y);
            }
        }
        private void printB09(object sender, EventArgs e)
        {
            PrintHyx.EagleIns pt = new ePlus.PrintHyx.EagleIns();
            pt.ShowDialog();
        }
        private void printB0B(object sender, EventArgs e)
        {
            PrintHyx.EagleAnbang  pt = new ePlus.PrintHyx.EagleAnbang ();
            pt.ShowDialog();
        }
        private void print0(object sender, EventArgs e)
        {//机票打印
            PrintTicket pt = new PrintTicket();
            pt.textBox_订座编号.Text = lb_Operated.SelectedItem.ToString();
            pt.ShowDialog();
        }
        private void print1(object sender, EventArgs e)
        {//行程单打印

            PrintReceipt pr = new PrintReceipt();
            pr.textBox_订座号.Text = lb_Operated.SelectedItem.ToString();
            pr.ShowDialog();
        }
        //private void print2(object sender, EventArgs e)
        //{//保险单打印
        //    PrintHyx.PingAn01 pi = new PrintHyx.PingAn01();
        //    pi.tbPnr.Text = lb_Operated.SelectedItem.ToString();
        //    pi.ShowDialog();
            
        //}
        private void printB01(object sender, EventArgs e)
        {//华安            
            PrintHyx.SinoSafe dlg = new PrintHyx.SinoSafe();
            dlg.Show();
        }
        private void printB02(object sender, EventArgs e)
        {//人寿            
            PrintHyx.ChinaLife dlg = new PrintHyx.ChinaLife();
            dlg.Show();
        }
        private void printB03(object sender, EventArgs e)
        {//都邦航意险            
            PrintHyx.DuBang01 dlg = new PrintHyx.DuBang01();
            dlg.Show();
        }
        private void printB04(object sender, EventArgs e)
        {//都邦航意险            
            PrintHyx.DuBang02 dlg = new PrintHyx.DuBang02();
            dlg.Show();
        }
        private void print001(object sender, EventArgs e)
        {//PICC            
            PrintHyx.PrintPICC2 dlg = new PrintHyx.PrintPICC2();
            dlg.Show();
        }
        private void print007(object sender, EventArgs e)
        {//永安
            PrintHyx.Yongan dlg = new PrintHyx.Yongan();
            dlg.Show();
        }
        private void print009(object sender, EventArgs e)
        {//新华
            PrintHyx.NewChina dlg = new PrintHyx.NewChina();
            dlg.Show();
        }
        private void printB05(object sender, EventArgs e)
        {//新华
            PrintHyx.DuBang02 dlg = new PrintHyx.DuBang02();
            dlg.Dubang03();
            dlg.Show();
        }
        private void printB06(object sender, EventArgs e)
        {//平安－周游列国
            PrintHyx.PingAn01 dlg = new PrintHyx.PingAn01();
            dlg.Show();
        }
        private void tb_姓名_Leave(object sender, EventArgs e)
        {
            if (GlobalVar2.gbUserModel == 0)
            {
                if (tb_姓名.Text.Trim() == "") return;

                EagleWebService.kernalFunc kf = new EagleWebService.kernalFunc(GlobalVar.WebServer);
                string temp = kf.GetPassenger(tb_姓名.Text.Trim());
                if (temp.Split(',').Length < 2)
                {
                    tb_CardNo.Text = temp;
                    return;
                }
                else
                {
                    try
                    {
                        BookSimple.CardIDs ci = new ePlus.BookSimple.CardIDs();
                        ci.cards = temp;
                        ci.ShowDialog();
                        tb_CardNo.Text = ci.selectcardid;
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void label出发地_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("切换排序方式：三字码排序<->拼音排序", "注意", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                b_3Code = !b_3Code;
                SetFromToCombox();
            }
            catch
            {
            }
        }

        private void lbDisplay_Click(object sender, EventArgs e)
        {
            MessageBox.Show(lbDisplay.Text);
        }

        private void btOther_Click(object sender, EventArgs e)
        {
            EagleForms.General.PasswordModify pm = new EagleForms.General.PasswordModify(GlobalVar.WebServer, GlobalVar.loginName, GlobalVar.loginPassword);
            pm.ShowDialog();
            GlobalVar.loginPassword = pm.PASSWORD_NEW;
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            if (!Model.md.b_00B)
            {
                MessageBox.Show("您无权发布公告!");
                return;
            }
            try
            {
                BookSimple.NoticeScroll ns = new ePlus.BookSimple.NoticeScroll(lblNotice.Text);
                ns.ShowDialog();
                lblNotice.Text = Notice.NOTICESCROLL;
            }
            catch
            {
            }
        }

        private void lblNotice_Click(object sender, EventArgs e)
        {
            try
            {
                Notice nt = new Notice();
                string tempnt = nt.get_notice_scroll(BookTicket.b_BookWndOpen ? "1" : "0");
                lblNotice.Text = (tempnt == "" ? "滚动式公告栏" : tempnt);
            }
            catch { }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            string u = GlobalVar.WebUrl;;
            string n = GlobalVar.loginName;
            string p = GlobalVar.loginPassword;
            string prog = "C:\\Program Files\\Internet Explorer\\IEXPLORE.EXE";
            string prog2 = "D:\\Program Files\\Internet Explorer\\IEXPLORE.EXE";

            if (GlobalVar.serverAddr == GlobalVar.ServerAddr.ZhenZhouJiChang)
            {
                //u = "http://yinge.eg66.com/EagleWeb2/login.aspx";
                u = "http://b2b.cn95161.com/login.aspx";
            }
            u = u + "?user=" + n + "&pwd=" + p;
            try
            {
                EagleString.EagleFileIO.RunProgram(prog, u);
            }
            catch
            {
                EagleString.EagleFileIO.RunProgram(prog2, u);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                Options.Options ot = new Options.Options();
                ot.cbIbeUse.Checked = bIbe;
                ot.ShowDialog();
                bool btemp = ot.cbIbeUse.Checked;
                if (btemp != bIbe)
                {
                    bIbe = btemp;
                    b_pn = b_pb = false;
                }
                EagleAPI.GetPrintConfig();
                EagleAPI.GetOptions();
                SetFromToCombox();
                GlobalVar.b_ListNoSeatBunk = ot.cbListNoSeatBunk.Checked;
                ot.Dispose();
            }
            catch
            {
            }
        }

        private void cbTicketType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTicketType.Text == "儿童票")
            {
                Options.ChildBirth cb = new Options.ChildBirth();
                cb.ShowDialog();
                child_birth = cb.dtBirth.ToShortDateString();
                cb.Dispose();
            }
        }

        private void lb_姓名_MouseHover(object sender, EventArgs e)
        {
            try
            {
                toolTip.SetToolTip(lb_姓名, lb_姓名.SelectedItems[0].ToString());
            }
            catch
            {
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
#if policy
            if (checkBox2.Checked)
            {
                this.columnHeader3.Width = 0;
                this.columnHeader4.Width = 0;
                this.columnHeader5.Width = 0;
                this.columnHeader6.Width = 0;
                this.columnHeader7.Width = 0;

                this.columnHeaderA.Width = 53;
                this.columnHeaderB.Width = 53;
                this.columnHeaderC.Width = 53;
                this.columnHeaderD.Width = 53;
                this.columnHeaderE.Width = 53;
                this.columnHeaderF.Width = 53;
                this.columnHeaderG.Width = 53;
                this.columnHeaderH.Width = 53;
                this.columnHeaderI.Width = 53;
                this.columnHeaderJ.Width = 53;
                this.columnHeaderK.Width = 53;
                this.columnHeaderL.Width = 53;
                this.columnHeaderM.Width = 53;
                this.columnHeaderN.Width = 53;
                this.columnHeaderO.Width = 53;
                this.columnHeaderP.Width = 53;
                this.columnHeaderQ.Width = 53;
            }
            else
            {
                this.columnHeader3.Width = 0;
                this.columnHeader4.Width = 49;
                this.columnHeader5.Width = 38;
                this.columnHeader6.Width = 38;
                this.columnHeader7.Width = 38;

                this.columnHeaderA.Width = 46;
                this.columnHeaderB.Width = 46;
                this.columnHeaderC.Width = 46;
                this.columnHeaderD.Width = 46;
                this.columnHeaderE.Width = 46;
                this.columnHeaderF.Width = 46;
                this.columnHeaderG.Width = 46;
                this.columnHeaderH.Width = 46;
                this.columnHeaderI.Width = 46;
                this.columnHeaderJ.Width = 46;
                this.columnHeaderK.Width = 46;
                this.columnHeaderL.Width = 46;
                this.columnHeaderM.Width = 46;
                this.columnHeaderN.Width = 46;
                this.columnHeaderO.Width = 46;
                this.columnHeaderP.Width = 46;
                this.columnHeaderQ.Width = 46;
            }
#endif
        }

        private void btClearSavedPnr_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK != MessageBox.Show("将删除所选择的PNR，但并不取消！", "删除编码", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)) return;
            for (int i = 0; i < lb_Saved.SelectedItems.Count; i++)
            {
                string pnr = lb_Saved.SelectedItems[i].ToString().Trim();
                //1.从数据库删除
                DeleteOnePNR(pnr);
                //2.执行删除指令
                //if (pnr.Length == 5) EagleAPI.EagleSendOneCmd("i~" + "rT" + pnr + "~xepnr@");
                //pnr_statistics ps = new pnr_statistics();
                //ps.pnr = pnr;
                //ps.state = "2";
                //ps.submit();
            }
            //3.同步dt
            ReadSavedPNR();
        }

        private void cb_出发地_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void cb_出发地_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        Options.ibe.Working wk = new Options.ibe.Working();
        private void bt_查询_MouseDown(object sender, MouseEventArgs e)
        {
            //wk.Show();
            //System.Threading.Thread thWorking = new System.Threading.Thread(new System.Threading.ThreadStart(wk.Show));
            //thWorking.Start();

        }

        public void setToBooktktExtDisplay()
        {
            this.AutoScroll = false;
            this.lv_查询结果.Location = new Point(6, 0);
            this.Size = new Size(this.Width, lv_查询结果.Height + 24);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
        }
        public void setToBooktktListView(string resultOfAv,string cpFromto)
        {
            CheckForIllegalCrossThreadCalls = false;
            if (cpFromto.Length != 6) return;
            try
            {
                if (lv_查询结果.InvokeRequired)
                {
                    EventHandler eh = new EventHandler(setToBooktktListViewCallBack);
                    ListView lvTemp = lv_查询结果;
                    lv_查询结果.Invoke(eh, new object[] { lvTemp, EventArgs.Empty });

                }
                else
                {
                    try
                    {
                        lv_查询结果.Items.Clear();
                        AVResult ar = new AVResult();
                        ar.avResult = resultOfAv;
                        ar.SetToListview(lv_查询结果, DateTime.Now);
                        ListPolicy lp = new ListPolicy();
                        //lp.SetAllPolicy(lv_查询结果, DateTime.Now.ToShortDateString());
                        string day = ar.avDate.Substring(0, 2);
                        string mon = ar.avDate.Substring(2, 3);
                        int iDay = int.Parse(day);
                        int iMon = int.Parse(EagleAPI.GetMonthInt(mon));
                        int iYear = DateTime.Now.Year;
                        if (iMon < DateTime.Now.Month) iYear++;
                        lp.SetAllPolicy(lv_查询结果, iYear.ToString() + "-" + iMon.ToString() + "-" + iDay.ToString());
                        //ReadLocalFC(cpFromto.Substring(0, 3).ToUpper(), cpFromto.Substring(3, 3).ToUpper());
                        ReadLocalFC(GlobalVar2.gbFromto.Substring(0, 3).ToUpper(), GlobalVar2.gbFromto.Substring(3, 3).ToUpper());
                        EasyPrice.insertPrice(f_Bunk_Y , f_Bunk_F.ToString (), lv_查询结果);

                    }
                    catch
                    {
                    }
                }
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }


#if policy
                ListPolicy lp = new ListPolicy();
                lp.SetAllPolicy(lv_查询结果, "");
#endif
        }
        void setToBooktktListViewCallBack(object sender,EventArgs e)//(string resultOfAv, string cpFromto)
        {
            try
            {
                lv_查询结果.Items.Clear();
                AVResult ar = new AVResult();
                ar.avResult = connect_4_Command.AV_String;
                ar.SetToListview(lv_查询结果, DateTime.Now);
                ListPolicy lp = new ListPolicy();
                lp.SetAllPolicy(lv_查询结果, DateTime.Now.ToShortDateString());
                //ReadLocalFC(cpFromto.Substring(0, 3).ToUpper(), cpFromto.Substring(3, 3).ToUpper());
                EasyPrice.insertPrice(0F, "0", lv_查询结果);
            }
            catch
            {
            }
        }
        private void lb_Saved_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ListBox lbSender = (ListBox)sender;
                string pnr = lbSender.SelectedItem.ToString();
                Options.ibe.ibeInterface ib = new Options.ibe.ibeInterface();
                MessageBox.Show(ib.rt(pnr,GlobalVar.serverAddr== GlobalVar.ServerAddr.HangYiWang));
            }
            catch
            {
            }
        }

        private void lv_查询结果_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27) this.WindowState = FormWindowState.Minimized;
        }

        private void tb_电脑号_TextChanged(object sender, EventArgs e)
        {
            if (tb_电脑号.Text == "") return;
            bt_提取_Click(sender, e);
        }

        private void lb_Saved_MouseUp(object sender, MouseEventArgs e)
        {
            if (lb_Saved.SelectedItems.Count != 1) return;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip rightMenu = new ContextMenuStrip();
                rightMenu.Items.Add("(正常票)PAT:", null, new EventHandler(pat1));
                rightMenu.Items.Add("(特价票)PAT:A", null, new EventHandler(pat2));
                rightMenu.Items.Add("(儿童票)PAT:*CH", null, new EventHandler(pat3));
                rightMenu.Items.Add("-");
                rightMenu.Items.Add("隐藏", null, new EventHandler(hidelocalpnr));
                //rightMenu.Items.Add("本地查找", null, new EventHandler(localfind));
                rightMenu.Show(sender as Control, e.X, e.Y);
            }
        }
        void pat1(object sender, EventArgs e)
        {
            lbDisplay.Text = "";
            EagleAPI.CLEARCMDLIST(3);
            //EagleAPI.EagleSendOneCmd("i~rT" + lb_Saved.SelectedItems[0].ToString().Trim() + "~pat:");
            EagleAPI.EagleSendOneCmd("i~rT" + lb_Saved.SelectedItems[0].ToString().Trim() + "~pat:");
        }
        void pat2(object sender, EventArgs e)
        {
            lbDisplay.Text = "";
            EagleAPI.CLEARCMDLIST(3);
            EagleAPI.EagleSendOneCmd("i~rT" + lb_Saved.SelectedItems[0].ToString().Trim() + "~pat:a");
        }
        void pat3(object sender, EventArgs e)
        {
            lbDisplay.Text = "";
            EagleAPI.CLEARCMDLIST(3);
            EagleAPI.EagleSendOneCmd("i~rT" + lb_Saved.SelectedItems[0].ToString().Trim() + "~pat:*ch");
        }
        void hidelocalpnr(object sender, EventArgs e)
        {
            btClearSavedPnr_Click(sender, e);
        }
        void localfind(object sender, EventArgs e)
        {
            tb_searchPNR.Text = (sender as ListBox).SelectedItem.ToString();
            bt_searchPNR_Click(sender, e);
        }
        private void lb_姓名_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                tb_姓名.Text = lb_姓名.SelectedItem.ToString();
                tb_CardNo.Text = lb_CardNo.SelectedItem.ToString();
            }
            catch
            {
            }
        }

        private void EnterKeyDown(object sender, EventArgs e)
        {
            string ctrlName = ((Control)sender).Name;
            switch (ctrlName)
            {
                case "cb_出发地":
                    for (int i = 0; i < cb_出发地.Items.Count; i++)
                    {
                        if (cb_出发地.Items[i].ToString().IndexOf(cb_出发地.Text.ToUpper()) >= 0)
                        {
                            cb_出发地.SelectedIndex = i;
                            break;
                        }
                    }
                    this.ActiveControl = cb_到达地;
                    break;
                case "cb_到达地":
                    for (int i = 0; i < cb_到达地.Items.Count; i++)
                    {
                        if (cb_到达地.Items[i].ToString().IndexOf(cb_到达地.Text.ToUpper()) >= 0)
                        {
                            cb_到达地.SelectedIndex = i;
                        }
                    }
                    this.ActiveControl = cbCarrier;
                    break;
                case "cbCarrier":
                    for (int i = 0; i < cbCarrier.Items.Count; i++)
                    {
                        if (cbCarrier.Items[i].ToString().IndexOf(cbCarrier.Text.ToUpper()) >= 0)
                        {
                            cbCarrier.SelectedIndex = i;
                        }
                    }
                    this.ActiveControl = dtp_机票日期;
                    break;
                case "dtp_机票日期":
                    this.ActiveControl = cbAvTime;
                    break;
                case "cbAvTime":
                    this.ActiveControl = bt_查询;
                    break;
                case "tb_人数":
                    this.ActiveControl = tb_电话;
                    break;
                case "tb_电话":
                    this.ActiveControl = tb_姓名;
                    break;
                case "tb_姓名":
                    this.ActiveControl = tb_CardNo;
                    break;
                case "tb_CardNo":
                    this.bt_添加姓名_Click(null, null);
                    this.ActiveControl = tb_姓名;
                    try
                    {
                        if (lb_CardNo.Items.Count == int.Parse(tb_人数.Text))
                        {
                            this.ActiveControl = bt_订票;
                        }
                    }
                    catch
                    {
                    }
                    break;
            }
        }

        private void BookTicket_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                EnterKeyDown(sender, null);
            }
        }

        private void tb_姓名_Click(object sender, EventArgs e)
        {
            if (GlobalVar2.gbUserModel == 1)
            {
                BookSimple.CardIDs ci = new ePlus.BookSimple.CardIDs();
                ci.parentWin = this;
                ci.ShowDialog();
            }
        }
    }

}