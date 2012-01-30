using System.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using EagleString;
using EagleControls;
using EagleProtocal;
using EagleExtension;

namespace EagleForms
{
    public partial class Primary
    {
        ContextMenuStrip rightMenu = new ContextMenuStrip();
        void blackWindow_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 123 || m_littleenter)//�޸�Ϊ�Ƿ�Ϊ����ָ��
                {
                    m_littleenter = false;//С�س���־��ԭ
                    string command = blackWindow.EagleCommand;
                    if (string.IsNullOrEmpty(command)) return;
                    if (!loginInfo.b2b.lr.AuthorityOfCommand(command))
                    {
                        AppendBlackWindow("\r\n***û�и�ָ�������û��Ȩ��ʹ�ø�ָ��\r\n>");
                        return;
                    }
                    string history = command;
                    command = commandPool.HandleCommand(command);
                    if (!socket.Activ)
                    {
                        AppendBlackWindow("\r\n***δ���ӷ�����\r\n>");
                    }
                    else
                    {
                        CheckBeforeSend();
                        blackWindow.m_history.History_Add(history);
                        blackWindow.AppendResult("\r\n>");
                        socket.SendCommand(command, EagleProtocal.TypeOfCommand.Multi);
                    }
                }
                else if (e.KeyValue == 13 && !m_littleenter)
                {
                    string txt = blackWindow.EagleCommand;
                    egCommandFixed(txt);
                }
                try
                {
                    blackWindow.InsertShortCutEtermCommand(e);
                    blackWindow.ShowShortCutFunction(e);
                    blackWindow.ShowCommandFunction(e);
                }
                catch (Exception ex)//�˴���ӿ������
                {
                    BlackWindowShortCutHandle(ex.Message);
                }
            }
            catch (Exception ex)
            {
                AppendBlackWindow(ex.Message + "\r\n>");
            }
        }
        /// <summary>
        /// ժҪ���̶���ʽ��egָ��
        /// </summary>
        /// <param name="egcmd"></param>
        private void egCommandFixed(string egcmd)
        {
            switch (egcmd.ToUpper())
            {
                case "EG CREATE TEST PNR":
                    socket.SendCommand("i~av h pvg+~sd1y1\rnm1ce/shi\rct123\rtktl2300/./wuh128\r@", EagleProtocal.TypeOfCommand.Multi);
                    return;
                case "CP":
                    blackWindow.Text = ">";
                    return;
                case "EG BALANCE":
                    menu_event_balance();
                    return;
                case "EG O":
                    displayOffice();
                    return;
                case "EG EXPFIND":
                    expireTicketFinder.Run();
                    return;
                case "EG EXPSETUP":
                    General.frmExpireTicketFind finderSetup = new global::EagleForms.General.frmExpireTicketFind();
                    if (finderSetup.ShowDialog() == DialogResult.OK)
                    {
                        InitExpireTicketFinder();
                        expireTicketFinder.Run();
                    }
                    return;
                case "EG TEST":
                    OuterEtdzOneKey("ABCDE");
                    break;

            }
            //B2C�׸�ָ��
            if (egcmd.ToUpper().StartsWith("KLD") || egcmd.ToUpper().StartsWith("KHZLLD"))
            {
                m_b2c_kldinfo = EagleDllImport.CtiFunction.kld(egcmd.Trim(), loginInfo.b2c.webservice);
            }
            else if (egcmd.ToUpper().StartsWith("EGYD"))
            {
                string temp = EagleDllImport.CtiFunction.egyd(
                    egcmd.Trim(),
                    m_b2c_kldinfo,
                    DateTime.Now.AddHours(2).ToString("hhmm"),
                    DateTime.Now.AddHours(2).ToString("ddMMM", EagleString.egString.dtFormat),
                    loginInfo.b2b.lr.UsingOffice()
                );
                AppendBlackWindow(temp);
            }
            else if (egcmd.ToUpper().StartsWith("BAOXIAN"))
            {
                AppendBlackWindow(EagleDllImport.CtiFunction.baoxian(egcmd.Trim(), m_b2c_baoxianPnr));
            }
        }
        string m_b2c_kldinfo = "";
        Hashtable m_b2c_baoxianPnr = new Hashtable();
        /// <summary>
        /// ��ӡ��������
        /// </summary>
        void displayOffice()
        {
            for (int i = 0; i < loginInfo.b2b.lr.m_ls_office.Count; i++)
            {
                OFFICE_INFO oi = loginInfo.b2b.lr.m_ls_office[i];
                string d = oi.IP_ID + " " + oi.SERVER_IP + ":" + oi.SERVER_PORT + " \n";
                AppendBlackWindow(d);
            }
        }
        void blackWindow_MouseUp(object sender, MouseEventArgs e)
        {
            rightMenu.Items.Clear();
            rightMenu.Items.Add("(&C)����", null, new EventHandler(copy));
            rightMenu.Items.Add("(&V)ճ��", null, new EventHandler(paste));
            rightMenu.Items.Add("-");
            rightMenu.Items.Add("(&P)��ӡѡ������", null, new EventHandler(print));
            rightMenu.Items.Add("-");
            rightMenu.Items.Add("(&T)�ύPNR", null, new EventHandler(pnrsubmit));
            rightMenu.Items.Add("-");
            rightMenu.Items.Add("(&H)��λ���봦��", null, new EventHandler(bunkapplyhandle));
            //rightMenu.Items.Add("����");
            if (e.Button == MouseButtons.Right)
            {
                rightMenu.Show(blackWindow, new Point(e.X, e.Y));
            }
        }
        void copy(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(blackWindow.SelectedText, true, 5, 10);
        }
        void cut(object sender, EventArgs e)
        {
        }
        void paste(object sender, EventArgs e)
        {
            blackWindow.InsertString(Clipboard.GetText(TextDataFormat.Text));
        }
        void print(object sender, EventArgs e)
        {
            PrintDocument ptDoc = new PrintDocument();
            ptDoc.PrintPage += new PrintPageEventHandler(ptDoc_PrintPage);
            ptDoc.DocumentName = "Eagle��Ļ��ӡ";
            ptDoc.Print();
        }
        void pnrsubmit(object sender, EventArgs e)
        {
            ToCommand.PnrOrderSubmit dlg = new global::EagleForms.ToCommand.PnrOrderSubmit(commandPool, socket);
            dlg.Show();
        }
        void bunkapplyhandle(object sender, EventArgs e)
        {
            
            General.SpecTickHandle ef =
                new General.SpecTickHandle(loginInfo.b2b.webservice, loginInfo.b2b.username);
            ef.dg_spectick = new General.SpecTickHandle.deleg4SepcTick(socket.Send);
            ef.Show();
        }
        int pages = 0;
        int curpage = 0;

        void ptDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            string ptstr = blackWindow.SelectedText;
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            Font ptFontCn = new Font("system", 10.5F, System.Drawing.FontStyle.Regular);
            Brush ptBrush = Brushes.Black;
            e.PageSettings.Margins.Left = 0;
            e.PageSettings.Margins.Right = 0;
            e.PageSettings.Margins.Top = 0;
            e.PageSettings.Margins.Bottom = 0;
            try
            {
                string[] strings = ptstr.Split('\n');
                pages = strings.Length / 64 + 1;//ҳ��



                string printstring = "";
                for (int i = 0; i < 64; i++)
                {
                    try
                    {
                        printstring += strings[curpage * 64 + i] + "\n";
                    }
                    catch
                    {
                        break;
                    }
                }
                curpage++;
                e.Graphics.DrawString(printstring, ptFontCn, ptBrush, 0F, 0F);
                if (curpage > pages) e.HasMorePages = false;
                else e.HasMorePages = true;
            }
            catch
            {
            }
        }

        private EagleNotifyIcon.EagleNotify m_notifyIcon = new EagleNotifyIcon.EagleNotify(
new string[] { 
                Application.StartupPath+"\\e0.ico",
                Application.StartupPath+"\\e1.ico",
                Application.StartupPath+"\\e2.ico",
                Application.StartupPath+"\\e3.ico",
                Application.StartupPath+"\\e4.ico",
            });

        private void BlackWindowShortCutHandle(string key)
        {
            switch (key)
            {
                case "��ӡ�г̵�":
                    menu_event_print_receipt();
                    break;
                case "���ú�������":
                    menu_event_window_blackwindow_colorfont();
                    break;
                case "���������":
                    menu_event_window_rightpanel_hide();
                    break;
                case "��ʾ�����":
                    menu_event_window_rightpanel_show();
                    break;
                case "����ȫ����":
                    menu_event_window_blackwindow_max();
                    break;
                case "������ԭȫ��":
                    menu_event_window_blackwindow_normal();
                    break;
                case "��һ��ָ��":
                    menu_event_operation_history_cmd(false);
                    break;
                case "��һ��ָ��":
                    menu_event_operation_history_cmd(true);
                    break;
                case "�鿴������־":
                    menu_event_viewlog(true);
                    break;
                case "�鿴��־":
                    menu_event_viewlog(false);
                    break;
                case "�鿴���":
                    menu_event_balance();
                    break;
                case "����":
                    menu_event_window_blackwindow_clear();
                    break;
                case "����_����רҵ��":
                    tcMain.SelectedTab = tpEasy;
                    break;
                case "����_��̨����":
                    tcMain.SelectedTab = tpManager;
                    break;
                case "����_����ƽ̨":
                    tcMain.SelectedTab = tpFinance;
                    break;
                case "�ֶ���Ʊ":
                    menu_event_operation_refund();
                    break;
                case "һ����Ʊ":
                    menu_event_operation_etdz_one_key();
                    break;
                case "�Զ���Q":
                    menu_event_operation_queue_clear();
                    break;
                case "PNR�����ύ":
                    menu_event_operation_submit_pnr_order();
                    break;
                case "�޸�B2B�ʺ�����":
                    menu_event_modifypassword();
                    break;
                case "������":
                    menu_event_help_calculator();
                    break;
                case "���±�":
                    menu_event_help_notepad();
                    break;
                case "����":
                    menu_event_help_paint();
                    break;
                case "���תС��":
                    menu_event_operation_aircode2pnr();
                    break;
                case "�л���������":
                    menu_event_operation_switchconfig();
                    break;
                case "������ݼ�����":
                    menu_event_window_shortcutkey();
                    break;
                case "B2C����":
                    break;
                case "�鿴��ʹ��ָ��":
                    menu_event_can_use_command();
                    break;
                case "�л�IBE������":
                    menu_event_operation_ibe2config();
                    break;
                case "����_����":
                    tcMain.SelectedTab = tpBlack;
                    break;
                case "��ӡ���յ�":
                    MessageBox.Show("���յ���ӡ�������ÿ��");
                    break;


            }
            //            ��ӡ�г̵� = 0,             //--mnPrint Finished                 toolbar

            //���ú������� = 1,           //--mnWindow          7
            //��������� = 2,             //--mnWindow          5
            //��ʾ����� = 3,             //--mnWindow          4
            //����ȫ���� = 4,             //--mnWindow          2
            //������ԭȫ�� = 5,           //--mnWindow          3
            //��һ��ָ�� = 6,             //--mnOperation 6
            //��һ��ָ�� = 7,             //--mnOperation 7
            //�鿴������־ = 8,           //--mnFile Finished 
            //�鿴��־ = 9,               //--mnFile Finished 
            //�鿴��� = 10,              //--mnFile Finished 
            //���� = 11,                  //--mnWindow           1                     toolbar

            //����_����רҵ�� = 12,            //--
            //����_��̨���� = 13,              //--
            //����_����ƽ̨ = 14,              //--
            //�ֶ���Ʊ = 15,              //--mnOperation 2                     toolbar
            //һ����Ʊ = 16,              //--mnOperation 1                     toolbar
            //�Զ���Q = 17,               //--mnOperation 4
            //PNR�����ύ = 18,           //--mnOperation 3                     toolbar
            //�޸�B2B�ʺ����� = 19,       //--mnFile Finished
            //������ = 20,                //--mnHelp                             1
            //���±� = 21,                //--mnHelp                             2
            //���� = 22,                  //--mnHelp                             3
            //���תС�� = 23,            //--mnOperation 5
            //�л��������� = 24,          //--mnOperation 0     Finished                toolbar
            ////25�ճ�,���ظ���ɾ��
            //������ݼ����� = 26,        //--mnWindow            6
            //B2C���� = 27,               //--mnHelp                             4
            //�鿴��ʹ��ָ�� = 28,        //--mnFile Finished
            //�л�IBE������ = 29,         //--mnOperation 8

            //����_���� = 30,              //--
            //��ӡ���յ� = 31                //--mnPrint Finished 
        }
    }
}
