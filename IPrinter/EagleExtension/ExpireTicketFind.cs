using System;
using System.Collections.Generic;
using System.Text;
using EagleString;
using EagleProtocal;
using System.Windows.Forms;
using System.IO;


namespace EagleExtension
{
    public class ExpireTicketFind
    {
        //                                             ����  ����  ���� ����    ����   �½�  ����     ����     ���ʲ� ����
        string users = "lxadmin,claw,yxh,zxgjhk,xiaohu,xdzeng,tsd002,wzzy,hzch95161,lzadmin,zq_01,GZMINGYANG,z1,dlnbhk,lxwmagf,jnshy,heling,shhl";

        string filenm = Application.StartupPath + "\\ExpiredTicket.txt";
        string filenm2 = Application.StartupPath + "\\Expired\\" + "ExpTick" + DateTime.Now.ToString("yyyyMMdd") + ".egexp";
        List<string> m_ls = new List<string>();
        bool m_running = false;
        EagleProtocal.MyTcpIpClient m_socket;
        EagleString.CommandPool m_pool;
        DateTime m_startTime = new DateTime();
        
        int hStart = 0;
        int mStart = 0;
        int hEnd = 0;
        int mEnd = 0;
        DateTime tStart;
        DateTime tEnd;

        System.Timers.Timer timer = new System.Timers.Timer(1000);
        public ExpireTicketFind(string username)
        {
            if (users.ToLower().IndexOf(username.ToLower()) < 0) throw new Exception ("�û���δ��Ȩ");
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            if (!Directory.Exists(Application.StartupPath + "\\Expired")) Directory.CreateDirectory(Application.StartupPath + "\\Expired");
            try
            {
                hStart = int.Parse(EagleString.EagleFileIO.ValueOf("ExpTickFinderhStart"));
                mStart = int.Parse(EagleString.EagleFileIO.ValueOf("ExpTickFindermStart"));
                hEnd = int.Parse(EagleString.EagleFileIO.ValueOf("ExpTickFinderhEnd"));
                mEnd = int.Parse(EagleString.EagleFileIO.ValueOf("ExpTickFindermEnd"));
                tStart = new DateTime(1, 1, 1, hStart, mStart, 0);
                tEnd = new DateTime(1, 1, 1, hEnd, mEnd, 0);
            }
            catch
            {
            }
        }
        public void SetArgs(EagleProtocal.MyTcpIpClient socket, EagleString.CommandPool cmdpool)
        {
            m_socket = socket;
            m_pool = cmdpool;
        }
        
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DateTime tNow = new DateTime(1, 1, 1, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            if (tNow < tStart || tNow > tEnd) return;
            if (m_running) return;
            if (!HasExpiredTick())
            {
                timer.Stop();
                return;
            }
            else
            {
                DetrOneTicket();
            }
        }
        public void Run()
        {
            if (m_socket == null) throw new Exception("δ����SOCKET");
            if (m_pool == null) throw new Exception("ָ���δ����");
            timer.Start();
        }
        /// <summary>
        /// �Ƿ���Ҫ���ĵ��ӿ�Ʊ
        /// </summary>
        bool HasExpiredTick()
        {
            m_ls.Clear();
            string [] tickets=File.ReadAllLines(filenm);
            for (int i = 0; i < tickets.Length; i++)
            {
                string t="";
                if (EagleString.BaseFunc.TicketNumberValidate(tickets[i], ref t))
                {
                    m_ls.Add(tickets[i]);
                }
            }
            if (m_ls.Count > 0) return true;
            return false;
        }
        /// <summary>
        /// ����һ�����ָ��detr:tn/
        /// </summary>
        void DetrOneTicket()
        {
            m_running = true;
            if (m_ls.Count > 0)
            {
                string cmd = m_pool.HandleCommand("detr:tn/" + m_ls[0]);
                m_pool.SetType(ETERM_COMMAND_TYPE.DETR_ExpiredTicketFind);
                m_socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.Multi);
            }
            else
            {
                EagleString.EagleFileIO.LogWriteAccess("CLAWCLAW��/" + File.ReadAllText(filenm2));
                File.AppendAllText(filenm2,DateTime.Now.ToString() + "�����ϣ�",Encoding.Default);
                m_running = false;
            }
        }
        public void Recv(string res)
        {
            
            try
            {
                DetrResult dr = new DetrResult(res);
                if ((res.ToUpper().IndexOf("OPEN") > 0 || res.ToUpper().IndexOf("SUSPENDED") > 0))
                {
                    if (res.Split(new string[] { "OPEN" }, StringSplitOptions.None).Length > res.Split(new string[] { "REFUNDED" }, StringSplitOptions.None).Length * 2)
                    {
                        File.AppendAllText(filenm2, m_ls[0] + "\r\n", Encoding.Default);
                    }
                    //File.AppendAllText(filenm2, res + "\r\n", Encoding.Default);
                }
            }
            catch
            {
                File.AppendAllText(filenm2, m_ls[0] + "���ʱ��������\r\n", Encoding.Default);
            }
            m_ls.RemoveAt(0);
            File.WriteAllLines(filenm, m_ls.ToArray(), Encoding.Default);
            DetrOneTicket();
        }
        
    }
}
/*��ƣ�
1.��Ҫdetr��Ʊ�ŷ�����ʱ�ļ�ExpiredTicket.txt
 * ��ĳʱ����ڳ�������,��running
2.���ļ�����Ʊ�ţ�����detr:����û�У���running��false
3.�յ�����󣬴������󣬰��ļ��и�Ʊ��ɾ�����ص�2
 * ������:
 * ��OPEN FOR USE �򽫸�Ʊ���뵽ExpTick+����.txt��

*/