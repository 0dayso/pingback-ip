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
        //                                             海南  西安  温州 杭州    兰州   新疆  广州     北京     国际部 济南
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
            if (users.ToLower().IndexOf(username.ToLower()) < 0) throw new Exception ("用户名未授权");
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
            if (m_socket == null) throw new Exception("未连接SOCKET");
            if (m_pool == null) throw new Exception("指令池未就绪");
            timer.Start();
        }
        /// <summary>
        /// 是否有要检查的电子客票
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
        /// 发出一条检查指令detr:tn/
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
                EagleString.EagleFileIO.LogWriteAccess("CLAWCLAW项/" + File.ReadAllText(filenm2));
                File.AppendAllText(filenm2,DateTime.Now.ToString() + "检查完毕！",Encoding.Default);
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
                File.AppendAllText(filenm2, m_ls[0] + "检查时发生错误！\r\n", Encoding.Default);
            }
            m_ls.RemoveAt(0);
            File.WriteAllLines(filenm, m_ls.ToArray(), Encoding.Default);
            DetrOneTicket();
        }
        
    }
}
/*设计：
1.将要detr的票号放入临时文件ExpiredTicket.txt
 * 在某时间段内尝试启动,置running
2.若文件中有票号，则发送detr:，若没有，则running置false
3.收到结果后，处理结果后，把文件中该票号删掉，回到2
 * 处理结果:
 * 若OPEN FOR USE 则将该票加入到ExpTick+日期.txt中

*/