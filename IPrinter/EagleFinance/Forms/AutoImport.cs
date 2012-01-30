using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace EagleFinance.Forms
{
    public partial class AutoImport : Form
    {
        AutoImportTpr autoImportTpr;
        AutoIncoming autoIncoming;
        EagleProtocal.MyTcpIpClient m_socket;
        EagleString.LoginInfo m_li;
        EagleString.CommandPool m_cmdpool;
        bool m_checkXls = false;
        public AutoImport(EagleProtocal.MyTcpIpClient sk,EagleString.LoginInfo li,EagleString.CommandPool pool)
        {
            InitializeComponent();
            m_socket = sk;
            m_li = li;
            m_cmdpool = pool;
            InitListBox();
        }
        public void set_args_constructor(EagleProtocal.MyTcpIpClient sk, EagleString.LoginInfo li, EagleString.CommandPool pool)
        {
            m_socket = sk;
            m_li = li;
            m_cmdpool = pool;
        }
        private void AutoImport_Load(object sender, EventArgs e)
        {
            dtpTpr.Value = DateTime.Now.AddDays(-1);
        }
        private void InitListBox()
        {
            int count = m_li.b2b.lr.m_ls_office.Count;
            for (int i = 0; i < count; ++i)
            {
                try
                {
                    if (!lbOfficeVisable.Items.Contains(m_li.b2b.lr.m_ls_office[i].OFFICE_NO.ToUpper()))
                    {
                        lbOfficeVisable.Items.Add(m_li.b2b.lr.m_ls_office[i].OFFICE_NO.ToUpper());
                    }
                }
                catch(Exception ex)
                {
                    AddText(ex.Message + "\r\n");
                }
            }
        }

        private void lbOfficeVisable_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string office = lbOfficeVisable.SelectedItem.ToString();
            AddOffice(office);
        }

        private void AddOffice(string office)
        {
            int printno = EagleString.EagleFileIO.EtdzPrinterNumber(office);
            if (printno <= 0)
            {
                AddText(string.Format("未找到{0}的打票机号，请将您的打票机号通知易格网科技", office));
                return;
            }
            int ipid = -1;
            if (m_li.b2b.lr.SameConfigs(office).Count == 0)
            {
                AddText(string.Format("未找到{0}的配置编号", office));
                return;
            }
            else
            {
                ipid = m_li.b2b.lr.SameConfigs(office)[0];
            }
            ListViewItem lvi = new ListViewItem();
            lvi.Text = office;
            lvi.SubItems.Add(ipid.ToString());
            lvi.SubItems.Add(
                string.Format("tpr:{0}/{1}/eg", printno, dtpTpr.Value.ToString("ddMMM", EagleString.egString.dtFormat)));
            lvi.SubItems.Add("否");
            lvPlan.Items.Add(lvi);
            checkTpr.Checked = true;
        }

        private void lvPlan_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                lvPlan.Items.RemoveAt(lvPlan.SelectedIndices[0]);
                if (lvPlan.Items.Count == 0) checkTpr.Checked = false;
            }
            catch
            {
            }
        }

        private void btnResetTpr_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lvPlan.Items.Count; ++i)
            {
                lvPlan.Items[i].SubItems[3].Text = "否";
            }
        }
        
        private void btnTprStart_Click(object sender, EventArgs e)
        {
            m_allStart = false;
            try
            {
                AddText("准备导入TPR报表");
                autoImportTpr = new AutoImportTpr(m_socket, m_cmdpool, m_li);
                autoImportTpr.SetAutoInfo(lvPlan);
                autoImportTpr.Run();
                AddText("开始");
            }
            catch (Exception ex)
            {
                AddText(ex.Message);
            }
        }

        object o = new object();
        public void TprRecv(string t)
        {
            lock (o)
            {
                if (t.Trim() != "")
                {

                    try
                    {
                        AddText(DateTime.Now.ToString() + "收到一个TPR报表");
                        Thread.Sleep(2000);
                        autoImportTpr.Recv(t);
                        AddText(DateTime.Now.ToString() + "导入下一个TPR报表");
                    }
                    catch (Exception ex)
                    {
                        AddText(DateTime.Now.ToString() + ex.Message);
                    }
                }

                else
                {
                    autoImportTpr.Send();
                }
            }
            return;
        }
        private void AddText(string text)
        {
            if (rtbInfomation.InvokeRequired)
            {
                deleg4AddText deleg = AddTextTemp;
                rtbInfomation.EndInvoke(rtbInfomation.BeginInvoke(deleg, new object[] { text }));
            }
            else
            {
                AddTextTemp(text);
            }
        }
        private void AddTextTemp(string text)
        {
            rtbInfomation.Text = rtbInfomation.Text.Insert(0, text + "\r\n");
            Application.DoEvents();
        }
        delegate void deleg4AddText(string text);

        Thread th;
        private void AutoImport_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (th.IsAlive) th.Abort();
            }
            catch
            {
            }
        }

        private void AutoImport_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void lbOfficeVisable_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            rightMenu(sender, null);
        }
        private void rightMenu(object sender, EventArgs e)
        {
            switch ((sender as Control).Name)
            {
                case "lbOfficeVisable":
                    lvPlan.Items.Clear();
                    for (int i = 0; i < lbOfficeVisable.Items.Count; i++)
                    {
                        string office = lbOfficeVisable.Items[i].ToString();
                        AddOffice(office);
                    }
                    break;
                case "lvPlan":
                    lvPlan.Items.Clear();
                    break;
            }
        }

        private void btnXlsStart_Click(object sender, EventArgs e)
        {
            m_allStart = false;
            start = dateTimePicker1.Value;
            end = dateTimePicker2.Value;

            xlsstart();
        }
        private void xlsstart()
        {
            
            Thread thread = new Thread(new ThreadStart(XlsImport));
            thread.Start();
            th = thread;
        }
        private void XlsImport()
        {
            AddText("启动自动导入xls易格报表");
            string temp = zOther.ws.services.XlsAutoImport(m_li.b2b.username, start, end);
            temp = temp.Substring(temp.LastIndexOf("\\") + 1);
            string webfile = "http://download.eg66.com/excel/" + temp;
            AddText("获得目标下载地址");
            
            string destfile = textBox1.Text.Trim() + temp;
            System.Net.WebClient myWebClient = new System.Net.WebClient();
            AddText("开始下载-易格报表之电子客票");
            myWebClient.DownloadFile(webfile, destfile);
            AddText("下载完毕,开始导入");
            GlobalApi.importEagleReport(destfile);
            AddText("导入xls易格报表之电子客票导入完毕!");
            if (checkXlsPnrReport.Checked)
            {
                AddText("启动自动导入xls易格报表之PNR订单!");

                AddText("导入xls易格报表之PNR订单完毕!");
                AddText("完成!");
            }
        }
        private DateTime start = new DateTime();
        private DateTime end = new DateTime();


        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Value = dateTimePicker2.Value.AddDays(0);
            checkXls.Checked = true;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(0);
            checkXls.Checked = true;
        }
        private bool m_allStart = false;
        private void btnAllStart_Click(object sender, EventArgs e)
        {
            m_allStart = true;
            if (checkTpr.Checked)
            {
                try
                {
                    autoImportTpr = new AutoImportTpr(m_socket, m_cmdpool, m_li);
                    autoImportTpr.SetAutoInfo(lvPlan);
                    autoImportTpr.Run();
                }
                catch (Exception ex)
                {
                    AddText(ex.Message);
                }
            }
        }

        private void btnAutoImcoming_Click(object sender, EventArgs e)
        {
            try
            {
                AddText("准备入库");
                autoIncoming = new AutoIncoming(m_socket, m_cmdpool, m_li);
                autoIncoming.SetAutoInfo(lvPlan);
                AddText("开始……");
                autoIncoming.Run();
            }
            catch (Exception ex)
            {
                AddText(ex.Message);
            }
        }
        
        private bool running = false;
     
        public void TolRecv(string t)
        {
            try
            {
                autoIncoming.Recv(t);
            }
            catch(Exception ex)
            {
                AddText(ex.Message);
            }

        }
    }


    public class AutoIncoming
    {
        private List<EagleString.Structs.TPR_AUTO_INFO> m_tprInfo = new List<EagleString.Structs.TPR_AUTO_INFO>();
        private EagleProtocal.MyTcpIpClient m_socket;
        private EagleString.CommandPool m_cmdpool;
        private EagleString.LoginInfo m_li;
        public AutoIncoming(EagleProtocal.MyTcpIpClient socket, EagleString.CommandPool pool, EagleString.LoginInfo li)
        {
            m_socket = socket;
            m_cmdpool = pool;
            m_li = li;
        }
        public void SetAutoInfo(ListView lv)
        {
            for (int i = 0; i < lv.Items.Count; i++)
            {
                EagleString.Structs.TPR_AUTO_INFO temp = new EagleString.Structs.TPR_AUTO_INFO();
                temp.FromListViewItem(lv.Items[i]);
                m_tprInfo.Add(temp);
            }
        }
        public void Run()
        {
            if (m_tprInfo.Count == 0) throw new Exception("未选择要入库的配置");
            Send();
        }
        /// <summary>
        /// 开始发送TOL:指令
        /// </summary>
        /// <param name="flag"></param>
        private void Send()
        {
            for (int i = 0; i < m_tprInfo.Count; i++)
            {
                m_tprInfo[i].COMMAND = "TOL:/eg";
                if (!m_tprInfo[i].STAT)
                {
                    EagleString.Imported.SendMessage
                            (0xFFFF, (int)EagleString.Imported.egMsg.SWITCH_CONFIG, m_tprInfo[i].IPID, 0);
                    //AddText(string.Format("正在尝试切换配置到{0}", m_tprInfo[i].OFFICE));
                    //System.Threading.Thread.Sleep(2000);
                    //AddText(string.Format("准备发送指令TOL:，等待5秒"));
                    System.Threading.Thread.Sleep(5000);
                    m_cmdpool.SetType(EagleString.ETERM_COMMAND_TYPE.TOL_INCOMING);
                    m_socket.SendCommand(m_tprInfo[i].COMMAND, EagleProtocal.TypeOfCommand.AutoPn);
                    return;
                }
            }
            throw new Exception("入库完毕");
        }
        /// <summary>
        /// 接收到TOL:结果
        /// </summary>
        /// <param name="s"></param>
        public void Recv(string t)
        {
            EagleString.TolResult tolResult = new EagleString.TolResult(t);
            if (tolResult.SUCCEED)
            {
                for (int i = 0; i < m_tprInfo.Count; ++i)
                {
                    if (m_tprInfo[i].OFFICE == tolResult.OFFICE)
                    {
                        m_tprInfo[i].STAT = true;
                    }
                }

                for (int i = 0; i < tolResult.ls_tktArrangeStart.Count; ++i)
                {
                    try
                    {
                        GlobalApi.Incoming(tolResult.OFFICE,
                            tolResult.ls_tktArrangeStart[i],
                            tolResult.ls_tktArrangeEnd[i]);

                    }
                    catch (Exception ex)
                    {
                    }
                }
                System.Threading.Thread.Sleep(2000);
                Send();

            }
        }
    }
    public class AutoImportTpr
    {
        private List<EagleString.Structs.TPR_AUTO_INFO> m_tprInfo = new List<EagleString.Structs.TPR_AUTO_INFO>();
        private EagleProtocal.MyTcpIpClient m_socket;
        private EagleString.CommandPool m_cmdpool;
        private EagleString.LoginInfo m_li;
        public AutoImportTpr(EagleProtocal.MyTcpIpClient socket, EagleString.CommandPool pool, EagleString.LoginInfo li)
        {
            m_socket = socket;
            m_cmdpool = pool;
            m_li = li;
        }
        public void SetAutoInfo(ListView lv)
        {
            for (int i = 0; i < lv.Items.Count; i++)
            {
                EagleString.Structs.TPR_AUTO_INFO temp = new EagleString.Structs.TPR_AUTO_INFO();
                temp.FromListViewItem(lv.Items[i]);
                m_tprInfo.Add(temp);
            }
        }
        public void Run()
        {
            if (m_tprInfo.Count == 0) throw new Exception("未选择要入库的配置");
            Send();
        }
        /// <summary>
        /// 开始发送TPR:指令
        /// </summary>
        /// <param name="flag"></param>
        public void Send()
        {
            for (int i = 0; i < m_tprInfo.Count; i++)
            {
                if (!m_tprInfo[i].STAT)
                {
                    EagleString.Imported.SendMessage
                            (0xFFFF, (int)EagleString.Imported.egMsg.SWITCH_CONFIG, m_tprInfo[i].IPID, 0);
                    //AddText(string.Format("正在尝试切换配置到{0}", m_tprInfo[i].OFFICE));
                    //System.Threading.Thread.Sleep(2000);
                    //AddText(string.Format("准备发送指令TOL:，等待5秒"));
                    System.Threading.Thread.Sleep(5000);
                    m_cmdpool.SetType(EagleString.ETERM_COMMAND_TYPE.TPR_IMPORT);
                    m_socket.SendCommand(m_tprInfo[i].COMMAND, EagleProtocal.TypeOfCommand.AutoPn);
                    return;
                }
            }
            throw new Exception("导入TPR完毕");
        }
        /// <summary>
        /// 接收到TPR:结果
        /// </summary>
        /// <param name="s"></param>
        public void Recv(string t)
        {

            //try
            {
                EagleString.TprResult tprResult = new EagleString.TprResult(t);
                if (tprResult.SUCCEED)
                {
                    for (int i = 0; i < m_tprInfo.Count; ++i)
                    {
                        if (m_tprInfo[i].OFFICE == tprResult.OFFICE)
                        {
                            m_tprInfo[i].STAT = true;
                        }

                    }

                    string file = Application.StartupPath
                        + "\\tpr\\"
                        + tprResult.OFFICE
                        + tprResult.DATE.ToShortDateString()
                        + ".tpr";
                    System.IO.File.WriteAllText(file, tprResult.TXT);
                    GlobalApi.importAllLineFromTpr(file);
                    System.Threading.Thread.Sleep(2000);
                    
                }
                Send();
            }

        }
    }
}