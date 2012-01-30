using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Management;
using ePlus.Model;
using System.Threading;
using System.Xml;
using mshtml;
using gs.para;
using System.Net;
using System.Net.Sockets;
using DevComponents.DotNetBar;

namespace ePlus
{
    public partial class frmMain
    {
        private void wf_int()
        {
#if RWY//modified by king
            try { this.Icon = new Icon("ico.ico"); }
            catch { }
            //this.ShowIcon = false;
            this.Text = "��֤��ӡ";
            GlobalVar.exeTitle = this.Text;
            this.btnConnect.Visible = false;
            this.btnDisconnect.Visible = false;
            this.statusBar.Visible = false;
            this.tabControl.Alignment = TabAlignment.Top;
            this.Book.Text = "ͼ�ν���";
            this.Book.Image = null;
            this.Book.Visible = false;

            this.tsb_WebBrowser.Visible = false;
            this.toolStripSeparator7.Visible = false;//�ָ���
            this.toolStripSeparator8.Visible = false;
            this.toolStripSeparator9.Visible = false;
            this.toolStripSeparator10.Visible = false;
            this.toolStripSeparator6.Visible = false;
            this.toolStripSeparator5.Visible = false;
            this.toolStripSeparator4.Visible = false;

            //������
            this.toolStrip.Visible = false;
            this.toolStripButton1.Visible = false;
            this.toolStripButton_ClearQ.Visible = false;//�Զ���Q
            this.toolStripButton2.Visible = false;//һ����Ʊ
            this.ttsddbBookModel.Visible = false;//����ģʽ
            this.toolStripButtonCall.Visible = false;//������Ϣ
            this.toolStripButtonNewOrder.Visible = false;
            this.tsSubmitPnr.Visible = false;

            splitContainer1.Panel1MinSize = 0;
            splitContainer1.Panel2MinSize = 0;
            //splitContainer1.SplitterDistance = 0;
            this.tabControl.TabPages[0].Text = "";// "����";
            //this.tabControl.Selecting += new TabControlCancelEventHandler(tabControl_Selecting);

            //�˵�
            MainMenu.Visible = false;
            miPrint.Visible = false;
            miPrintReceipt.Text = "�г̵�";
            miPrintInsurance.Visible = false;
            toolStripSeparator10.Visible = false;
            ��ӡ����SToolStripMenuItem.Visible = false;
            toolStripSeparator8.Visible = false;
            �����մ�ӡToolStripMenuItem.Visible = false;
            ���β�Ʒ��ӡToolStripMenuItem.Visible = false;
            �г̵����ToolStripMenuItem.Visible = false;
            miTools.Visible = false;
            ������ToolStripMenuItem.Visible = false;
            miHelp.Visible = false;
            cTICToolStripMenuItem.Visible = false;

            this.miConnect.Visible = false;
            this.miDisconnect.Visible = false;
            miServerInfo.Visible = false;
            toolStripMenuItem2.Visible = false;

            �л��û�SToolStripMenuItem.Visible = false;
            �������ʳ���ToolStripMenuItem.Visible = false;
            �������ض��ʳ���ToolStripMenuItem.Visible = false;
            r�ָ��϶��˳���ToolStripMenuItem.Visible = false;
            toolStripSeparator19.Visible = false;
            miSetup.Visible = false;

            this.Width = 680;
            this.Height = 450;
#endif
        }

        void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == 0)
                e.Cancel = true;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            ReadCmdSendTypeFromOptionsTxt();

            EagleAPI.LogWrite("");//ʱ����

            //ʱ���У
            Thread thGetServerTime = new Thread(new ThreadStart(Options.gbOption.isDifferCompareTimeWithServerThan12));
            thGetServerTime.Start();
            //��ʾ�¶���ToolStripMenuItem.Checked = EagleAPI2.initNewOrder();

            EagleAPI.LogWrite("");//ʱ����

            try
            {
                GlobalVar.mainMenu = this.MainMenu;
                //this.Text = Properties.Resources.MainFormTitle;//commentted by king

                //EagleAPI.GetPrintConfig();
                //EagleAPI.GetOptions();

                {
                    //������ƿ��ò˵�
                    int iConflict = 0;
                    if (md.b_001) iConflict++;
                    if (md.b_006) iConflict++;
                    if (md.b_007) iConflict++;
                    if (md.b_009) iConflict++;
                    Model.md.SetBoolVars();

                    if (GlobalVar.serverAddr == GlobalVar.ServerAddr.ZhenZhouJiChang)
                    {
                        BookTicket.bIbe = false;
                    }
                    //BookTicket.bIbe = Model.md.b_00E;//�ɣ£�


                    this.Visible = Model.md.b_004;
                    #region �ն˹��ܿ���


                    if (Model.md.b_004)
                    {//���к���
                        this.tsb_WebBrowser.Enabled = !md.b_00H;
                        this.style3.Enabled = Model.md.b_F12;//S3��ť
                        this.mi_style3.Enabled = Model.md.b_F12;
                        this.��ɣã�ToolStripMenuItem.Visible = Model.md.b_001;
                        this.miPrint.Visible = Model.md.b_002;
                        this.miPrintReceipt.Visible = Model.md.b_003;
                        //this.miPrintInsurance.Visible = Model.md.b_005;
                        this.Book.Enabled = Model.md.b_006;//���

                        this.toolStripButton_ClearQ.Enabled = Model.md.b_QQQ;//�Զ���Q
                        this.��������ToolStripMenuItem.Visible = Model.md.b_007;
                        this.�»�����ToolStripMenuItem.Visible = Model.md.b_009;

                        this.��ͨ�����˺����յ�ToolStripMenuItem.Visible = Model.md.b_B01;//����
                        if (!Model.md.b_B01)
                            this.����ToolStripMenuItem.Visible = false;

                        this.�����ÿ����������˺����յ�ToolStripMenuItem.Visible = Model.md.b_B02;//����
                        if (!Model.md.b_B02)
                            this.����ToolStripMenuItem.Visible = false;

                        this.��������ToolStripMenuItem.Visible = Model.md.b_B04;//���ﺽ����

                        this.���������������˺����յ�ToolStripMenuItem.Visible = Model.md.b_B03;//�����������
                        this.������ToolStripMenuItem.Visible = Model.md.b_B05;//���������

                        if (!(Model.md.b_B03 || Model.md.b_B04 || Model.md.b_B05))
                            this.����ToolStripMenuItem.Visible = false;

                        this.SunShineToolStripMenuItem.Visible = Model.md.b_B07;//һ·����

                        if (!(Model.md.b_B07))
                            this.����ToolStripMenuItem.Visible = false;

                        this.��������Ա���տ�ToolStripMenuItem.Visible = Model.md.b_B08;//��������Ա���տ�
                        if (!(Model.md.b_B08))
                            this.������ToolStripMenuItem.Visible = false;

                        this.�»�����ToolStripMenuItem.Visible = Model.md.b_B09;//�׸�����Ա���տ�
                        this.��������ͨToolStripMenuItem.Visible = Model.md.b_B0B;//��������ͨ

                        this.pICCToolStripMenuItem.Visible = Model.md.b_B0D;//�׸�PICC
                        if (!(Model.md.b_B09 || Model.md.b_B0B || Model.md.b_B0D)) this.�׸���ToolStripMenuItem.Visible = false;

                        this.�����й�.Visible = Model.md.b_B06;//�����й�
                        //if (!(Model.md.b_B06)) 
                        this.ƽ��ToolStripMenuItem.Visible = false;

                        this.����������ToolStripMenuItem.Visible = Model.md.b_B0A;//̫ƽ��������
                        if (!(Model.md.b_B0A))
                            this.̫ƽ��ToolStripMenuItem.Visible = false;

                        if (EagleAPI.GetCmdName("etdz", GlobalVar.loginLC.VisuableCommand) == "" || EagleAPI.GetCmdName("etdz", GlobalVar.loginLC.VisuableCommand) == null) toolStripButton2.Enabled = false;

                        if (!GlobalVar.bPekGuangShunUser) toolStripButton1.Visible = toolStripSeparator16.Visible = false;

                        this.cTICToolStripMenuItem.Visible = Model.md.b_0CTI;//��������

                    }
                    //else if (iConflict > 1)
                    //{//��ͻ��û�к����£�����ͬʱΪ���ռ�����û�

                    //    MessageBox.Show("ģ������ͻ���������Ա��ϵ��");
                    //    Application.Exit();
                    //}
                    else if (Model.md.b_006)
                    {//û��������Ϊ����û�

                        BookTicket bt = new BookTicket();
                        bt.Show();
                        BookTicket.bIbe = !Model.md.b_00F;
                        MessageBox.Show("�����ʻ����Ϊ����" + GlobalVar.f_CurMoney);
                    }

                    else if (Model.md.b_001)
                    {//û��������ΪPICC���մ�ӡ
                        PrintHyx.PrintPICC2 pp = new ePlus.PrintHyx.PrintPICC2();
                        pp.Show();
                    }
                    else if (Model.md.b_007)
                    {//û��������Ϊ�������մ�ӡ
                        PrintHyx.Yongan ya = new ePlus.PrintHyx.Yongan();
                        ya.Show();
                    }
                    else if (Model.md.b_009)
                    {//�»�����
                        PrintHyx.NewChina nc = new ePlus.PrintHyx.NewChina();
                        nc.Show();
                    }
                    else if (Model.md.b_B01)
                    {//��������
                        PrintHyx.SinoSafe ss = new ePlus.PrintHyx.SinoSafe();
                        ss.Show();
                    }
                    else if (Model.md.b_B02)
                    {//���ٺ�����

                        PrintHyx.ChinaLife cl = new ePlus.PrintHyx.ChinaLife();
                        cl.Show();
                    }
                    else if (Model.md.b_B03)
                    {//���ﺽ����

                        PrintHyx.DuBang01 db1 = new ePlus.PrintHyx.DuBang01();
                        db1.Show();
                    }
                    else if (Model.md.b_B04)
                    {//�����������
                        PrintHyx.DuBang02 db2 = new ePlus.PrintHyx.DuBang02();
                        db2.Show();
                    }
                    else if (Model.md.b_B05)
                    {//���������

                        PrintHyx.DuBang02 db3 = new ePlus.PrintHyx.DuBang02();
                        db3.Dubang03();
                        db3.Show();
                    }
                    else if (Model.md.b_B06)
                    {
                        PrintHyx.PingAn01 pa = new ePlus.PrintHyx.PingAn01();
                        pa.Show();
                    }
                    else if (Model.md.b_B07)
                    {
                        PrintHyx.Sunshine ins = new ePlus.PrintHyx.Sunshine();
                        ins.Show();
                    }
                    else if (Model.md.b_B08)
                    {
                        PrintHyx.Hangyiwang ins = new ePlus.PrintHyx.Hangyiwang();
                        ins.Show();
                    }
                    else if (Model.md.b_B09)
                    {
                        PrintHyx.bxLogin bx = new ePlus.PrintHyx.bxLogin();
                        if (bx.ShowDialog() != DialogResult.OK) return;

                        PrintHyx.EagleIns ins = new ePlus.PrintHyx.EagleIns();
                        ins.Text = ins.lb��˾����.Text = "�»����ٱ��չɷ����޹�˾�����˺����ճб���֪��";
                        ins.Show();
                    }
                    else if (Model.md.b_B0A)
                    {
                        PrintHyx.Pacific ins = new ePlus.PrintHyx.Pacific();
                        ins.Show();
                    }
                    else if (Model.md.b_B0B)
                    {
                        PrintHyx.EagleAnbang ea = new ePlus.PrintHyx.EagleAnbang();
                        ea.Show();
                    }
                    else if (Model.md.b_003)//ֻ��ʾ�г̵���ӡ
                    {
                        PrintReceipt pr = new PrintReceipt();

                        pr.Show();
                    }
                    else
                    {//��ģ��ʹ��

                        //MessageBox.Show("����Ȩʹ�ø�ϵͳ���������Ĺ���Ա�򿪱�ҪȨ��");
                        //Application.Exit();
                    }

                    #endregion

                    this.ttsddbBookModel.Text = this.����ģʽToolStripMenuItem.Text;
                    GlobalVar.commandSendtype = GlobalVar.CommandSendType.Fast;
                    //
                    //this.ttsddbBookModel.Text = this.��ͨģʽToolStripMenuItem.Text;
                    //GlobalVar.commandSendtype = GlobalVar.CommandSendType.A;
                }

#if receipt
                
                this.Visible = false;
                PrintReceipt pr = new PrintReceipt();
                pr.Text = "ClawSoft - �г̵���ӡ";
                pr.Icon = new Icon(Application.StartupPath + "\\claw.ico");
                pr.Show();
#endif
                if (Options.GlobalVar.QueryType == XMLConfig.QueryType.Eterm)
                {
                    #region//����ѡ��ť����ip
                    {
                        string[] ipls = GlobalVar.loginLC.IPsString.Split('~');
                        List<string> lsip = new List<string>();
                        List<string> addedIP = new List<string>();
                        for (int i = 0; i < ipls.Length; i++)
                        {

                            string[] cfgs = EagleAPI.GetConfigNumberByIP(ipls[i]).Split('~');//��ͬIP��ͬ����Ҳ�ܵõ�
                            if (addedIP.Contains(ipls[i])) continue;
                            addedIP.Add(ipls[i]);
                            for (int j = 0; j < cfgs.Length; j++)
                                lsip.Add(cfgs[j]);
                        }
                        //��cfg����
                        lsip.Sort();
                        mi_CONFIG.DropDownItems.Add("ȫ������");
                        for (int i = 0; i < lsip.Count; i++)
                        {
                            //mi_CONFIG.DropDownItems.Add(EagleAPI.GetConfigNumberByIP(ipls[i]));
                            if (md.b_00I || lsip[i].ToLower().IndexOf("tao") >= 0 || GlobalVar.serverAddr == GlobalVar.ServerAddr.ZhenZhouJiChang || GlobalVar.serverAddr == GlobalVar.ServerAddr.KunMing)
                                mi_CONFIG.DropDownItems.Add(lsip[i]);
                        }




                        int theLastOne = lsip.Count - 1;
                        if (lsip.Count > 0)
                        {
                            //Ӧָ����ǰ�����ӷ������Ŀ���cfg�����һ����������δ�����ip�����һ��


                            for (int i = lsip.Count - 1; i > 0; i--)
                            {
                                XmlDocument xd = new XmlDocument();
                                xd.LoadXml(GlobalVar.loginXml);
                                XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode("IPS");
                                for (int j = 0; j < xn.ChildNodes.Count; j++)
                                {
                                    if (lsip[i].Trim() == xn.ChildNodes[j].SelectSingleNode("PeiZhi").InnerText.Trim())
                                    {
                                        theLastOne = i;

                                        break;
                                    }
                                }
                            }
                            try
                            {
#if ȫ������
                        ((ToolStripMenuItem)(mi_CONFIG.DropDownItems[theLastOne + 1])).Checked = true;//����ʱΪ���һ�����Ժ����û���Ч

                        mi_CONFIG.Text = mi_CONFIG.DropDownItems[theLastOne + 1].Text;
                        GlobalVar.officeNumberCurrent = mi_CONFIG.Text;
#else
                                //bool bSpecifyDefaultConfig = false;
                                //for (int i = 0; i < mi_CONFIG.DropDownItems.Count; i++)
                                //{

                                //}
                                mi_CONFIG.Text = "ȫ������";
                                ((ToolStripMenuItem)(mi_CONFIG.DropDownItems[0])).Checked = true;
#endif

                            }
                            catch
                            {
                                ((ToolStripMenuItem)(mi_CONFIG.DropDownItems[theLastOne])).Checked = true;
                                mi_CONFIG.Text = mi_CONFIG.DropDownItems[theLastOne].Text;
                                GlobalVar.officeNumberCurrent = mi_CONFIG.Text;

                            }
                        }
                        mi_CONFIG.DropDownItems.Add("-");
                        GlobalAPI.NotGlobal ng = new ePlus.GlobalAPI.NotGlobal();
                        List<string> ipgroup = ng.GetConfigGroupsBy(lsip);
                        for (int iip = 0; iip < ipgroup.Count; iip++)
                        {
                            //#if RWY
                            //                        break;
                            //#endif
                            mi_CONFIG.DropDownItems.Add(ipgroup[iip]);
                        }
                    }
                    #endregion

                    //Thread th = new Thread(new ThreadStart(connect));
                    //th.Start();
                    connect_1();
                }

#if !RWY
                this.Text = GlobalVar.exeTitle + "��������λ�ã�" + GlobalVar.loginLC.SrvName + "��";
#endif
                //if (GlobalVar.loginLC.SrvName.IndexOf("��Χ") >= 0) BookTicket.bIbe = true;
                //timerNotice1();
            }
            catch (Exception ex1)
            {
                EagleAPI.LogWrite(ex1.Message);//ʱ����
            }
            initStatusBar();
            //SetNkgMode();

            instance = this;
            SetIA10();
            ShowIA();//added by king
            EagleAPI.LogWrite("init finished");//ʱ����
        }

        public EagleForms.Printer.PrintIA10.MiddleClassCallIA ia10 = new EagleForms.Printer.PrintIA10.MiddleClassCallIA();
 
        void SetIA10()
        {
            ia10.SetLoginInfo(GlobalVar.loginName, GlobalVar.loginPassword, EagleString.LineProvider.DianXin);
            ia10.SetSocket(Options.GlobalVar.socketGlobal);
            ia10.SetCommandPool();
        }
        /// <summary>
        /// ��ʾ���մ�ӡ����
        /// </summary>
        void ShowIA()
        {
            int index = this.tabControl.TabPages.Count;
            this.tabControl.TabPages.Add("��ӡ����");
            Form dlg = (Form)ia10.GetIA(Options.GlobalVar.IACode);
            dlg.TopLevel = false;
            dlg.FormBorderStyle = FormBorderStyle.None;
            tabControl.TabPages[index].Controls.Add(dlg);
            dlg.Show();
            tabControl.SelectedIndex = index;

            this.tabControl.TabPages.Add("������¼");
            dlg = new EagleForms.Printer.FrmInsuranceList();
            dlg.TopLevel = false;
            dlg.FormBorderStyle = FormBorderStyle.None;
            dlg.Dock = DockStyle.Fill;
            tabControl.TabPages[index + 1].Controls.Add(dlg);
            dlg.Show();

            this.tabControl.TabPages.Add("��̨����");
            TabPage tp = tabControl.TabPages[tabControl.TabPages.Count - 1];
            WebBrowser wb = new WebBrowser();
            wb.Dock = DockStyle.Fill;
            wb.ScriptErrorsSuppressed = true;
            tp.Controls.Add(wb);
        }
        public static frmMain instance = null;
        /// <summary>
        /// ��link.txt�ļ��ж�ȡ��ַ�б��м����Ӽ�����ť
        /// </summary>
        void ZzLink()
        {
            try
            {
                string[] links = File.ReadAllLines(Application.StartupPath + "\\link.txt",Encoding.Default);
                ToolStripButton[] btnLink = new ToolStripButton[links.Length];
                for (int i = 0; i < links.Length; i++)
                {
                    string title = links[i].Split(',')[0];
                    string icofile = links[i].Split(',')[1];
                    string dest = links[i].Split(',')[2];
                    Image image;
                    try
                    {
                        image = Image.FromFile(Application.StartupPath + "\\" + icofile);
                    }
                    catch
                    {
                        image = null;
                    }
                    btnLink[i] = new ToolStripButton(title, image, btnLinkClick);
                    btnLink[i].TextImageRelation = TextImageRelation.ImageAboveText;
                    btnLink[i].ToolTipText = dest;
                    
                }
                //if (GlobalVar.serverAddr == GlobalVar.ServerAddr.ZhenZhouJiChang)
                {
                    this.toolStrip.Items.AddRange(btnLink);
                }
            }
            catch
            {
            }
        }
        void btnLinkClick(object sender, EventArgs e)
        {
            ToolStripButton btn = sender as ToolStripButton;
            string prog = "C:\\Program Files\\Internet Explorer\\IEXPLORE.EXE";
            string prog2 = "D:\\Program Files\\Internet Explorer\\IEXPLORE.EXE";
            try
            {
                EagleString.EagleFileIO.RunProgram(prog, btn.ToolTipText);
            }
            catch
            {
                EagleString.EagleFileIO.RunProgram(prog2, btn.ToolTipText);
            }
        }
    }
}
