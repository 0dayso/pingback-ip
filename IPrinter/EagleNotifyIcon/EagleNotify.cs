using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace EagleNotifyIcon
{
    /// <summary>
    /// �׸�����ͼ����
    /// </summary>
    public class EagleNotify
    {
        NotifyIcon m_notify = new NotifyIcon();
        Timer m_timer = new Timer();
        System.Timers.Timer m_timer2 = new System.Timers.Timer(500);
        Icon [] m_icon;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="icofiles">�任ͼ���ļ��б�</param>
        public EagleNotify(string [] icofiles)
        {
            m_icon = new Icon[icofiles.Length];
            for (int i = 0; i < m_icon.Length; ++i)
                m_icon[i] = new Icon(icofiles[i]);
            m_notify.Icon = m_icon[0];
            m_timer.Interval = 500;
            m_timer.Tick += new EventHandler(m_timer_Tick);
            m_timer2.Elapsed += new System.Timers.ElapsedEventHandler(m_timer_Tick);
            m_notify.MouseDoubleClick += new MouseEventHandler(m_notify_MouseDoubleClick);
            m_notify.MouseClick += new MouseEventHandler(m_notify_MouseDoubleClick);
            m_notify.BalloonTipClicked += new EventHandler(m_notify_BalloonTipClicked);

            m_notify.Visible = true;
            m_notify.ContextMenu = new ContextMenu();
            //m_notify.ContextMenu.MenuItems.Add("���������", new EventHandler(hide_right_panel));
            //m_notify.ContextMenu.MenuItems.Add("��ʾ�����", new EventHandler(show_right_panel));
            m_notify.MouseClick += new MouseEventHandler(m_notify_MouseClick);

        }

        void m_notify_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                m_notify.ContextMenu.Show(sender as Control, new Point(0, 0));
            }
            catch
            {
            }
        }
        void hide_right_panel(object o,EventArgs e)
        {
            Hashtable ht = new Hashtable();
            ht.Add("SHOW_RIGHT_PANEL", "0");
            EagleString.EagleFileIO.WiteHashTableToEagleDotTxt(ht);
            MessageBox.Show("�´������׸���Ч");
        }
        void show_right_panel(object o, EventArgs e)
        {
            Hashtable ht = new Hashtable();
            ht.Add("SHOW_RIGHT_PANEL", "1");
            EagleString.EagleFileIO.WiteHashTableToEagleDotTxt(ht);
            MessageBox.Show("�´������׸���Ч");
        }
        public void Dispose()
        {
            m_notify.Dispose();
        }
        ~EagleNotify()
        {
            try
            {
                m_notify.Visible = false;
                m_notify.Dispose();
            }
            catch
            {
            }
        }
        void m_notify_BalloonTipClicked(object sender, EventArgs e)
        {
            showOneMessage();
        }
        private bool m_enable = true;
        public bool Enable { get { return m_enable; } set { m_enable = value; } }
        void showOneMessage()
        {
            if (!m_enable) return;
            if (m_lsMessage.Count == 0) return;
            //MessageBox.Show(m_lsMessage.Dequeue());
            {
                string msg = m_lsMessage.Dequeue();
                string xml = msg.Substring(msg.IndexOf("\r\n") + 2);
                string id = msg.Split(':')[0];
                switch (id)
                {
                    case "6":
                        MessageBox.Show("���¶�����");
                        break;
                    case "9"://�����λ��Ϣ
                        MessageBox.Show("���µĲ�λ���룡���ں����е���Ҽ�->��λ���봦��");
                        break;
                    case "A":
                        /*<eg>
                            <cm>ProcessKOrder</cm>
	                        <order_id>16</order_id>
                            <process_user>008</process_user>
                            <process_date>YYYY-mm-dd hh:mm:ss</process_date>
                            <process_state>0Ϊδ����1Ϊ����ɹ���2Ϊ����ʧ��</process_state>
                            <PNR>ttt  </PNR>
                            <flight_number>ttt</flight_number>
                            <new_bunk>t </new_bunk>
                            <process_price>555</process_price>
                            <original_price>666</original_price>
                            <remark1>�˸�ǩ ��Ʊ��ע</remark1>
                        </eg>
                        <!--����һ������-->*/
                        try
                        {
                            Hashtable ht = EagleString.egXmlString.ToHashTable(xml, "//eg");
                            string show = "";
                            if (ht["process_state"].ToString() == "1")
                            {
                                show = "(����ɹ�)" + "PNRΪ : " + ht["PNR"].ToString();
                                ht["process_state"] = "(����ɹ�)";
                            }
                            else
                            {
                                show = "(����ʧ��)" + "PNRΪ : " + ht["PNR"].ToString();
                                ht["process_state"] = "(����ʧ��)";
                            }
                            MessageBox.Show(show);
                            return;
                            Hashtable ht2 = new Hashtable(10,1F);

                            List<string> ls = new List<string> ();
                            ls.Add("order_id");
                            ls.Add("process_user");
                            ls.Add("process_date");
                            ls.Add("process_state");
                            ls.Add("PNR");
                            ls.Add("flight_number");
                            ls.Add("new_bunk");
                            ls.Add("process_price");
                            ls.Add("original_price");
                            ls.Add("remark1");
                            int index=0;
                            ht2.Add(ls[index++], "    �����");
                            ht2.Add(ls[index++], "    ����Ա");
                            ht2.Add(ls[index++], "  ����ʱ��");
                            ht2.Add(ls[index++], "  ������");
                            ht2.Add(ls[index++], "  ��¼����");
                            ht2.Add(ls[index++], "    �����");
                            ht2.Add(ls[index++], "      ��λ");
                            ht2.Add(ls[index++], "�����۸�");
                            ht2.Add(ls[index++], "����ǰ�۸�");
                            ht2.Add(ls[index++], "      ��ע");
                            
                            //foreach (DictionaryEntry de in ht2)
                            for(int i=0;i<ls.Count;++i)
                            {
                                try
                                {
                                    show += ht2[ls[i]].ToString() + " : " + ht[ls[i]].ToString() + "\r\n";
                                }
                                catch
                                {
                                }
                            }
                            //show += "\r\n\r\nע��������(0-δ����1-����ɹ���2-����ʧ��)";
                            MessageBox.Show(show);
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message + "\r\n" + xml);
                        }
                        break;
                }
            }
            if (m_lsMessage.Count == 0) stop();
        }

        /// <summary>
        /// ˫��ȡ��һ����Ϣ����ʾ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_notify_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            showOneMessage();
        }
        int nIco = 0;
        void m_timer_Tick(object sender, EventArgs e)
        {
            nIco = nIco % m_icon.Length;
            m_notify.Icon = m_icon[nIco++];
        }
        /// <summary>
        /// ����Ϣ������ͼ����˸��ʾ
        /// </summary>
        public void start(string msg)
        {
            if (!m_enable) return;
            AddMessage(msg);
            if (!m_notify.Visible)
            {
                m_notify.Visible = true;
            }
            string msgtype = "";
            switch (msg[0].ToString())
            {
                case "6":
                    msgtype = "PNR����";
                    break;
                case "9"://�����λ��Ϣ
                    msgtype = "��λ����";
                    break;
                case "A":
                    msgtype = "�����λ����";
                    break;
            }
            m_notify.Text = "\r\n\r\n�յ�����Ϣ����Ϣ����Ϊ��" + msgtype + "\r\n\r\n";
            m_notify.BalloonTipText = m_notify.Text;
            m_notify.ShowBalloonTip(3000);//��ʾ����
            //m_timer.Start();
            m_timer2.Start();
            
        }
        /// <summary>
        /// ֹͣͼ����˸
        /// </summary>
        public void stop()
        {
            //m_timer.Stop();
            m_timer2.Stop();
            m_notify.Icon = m_icon[0];
            m_notify.Text = "";
        }
        private Queue<string> m_lsMessage = new Queue<string>();
        /// <summary>
        /// ����һ����Ϣ
        /// </summary>
        /// <param name="msg">Ҫ��ʾ��Ϣ������</param>
        private void AddMessage(string msg)
        {
            m_lsMessage.Enqueue(msg);
        }
    }
}
