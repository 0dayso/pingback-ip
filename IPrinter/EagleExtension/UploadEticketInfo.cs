using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using EagleString;
using EagleProtocal;
using System.IO;
using System.Collections;

namespace EagleExtension
{
    /// <summary>
    /// 上传电子客票信息类,使用简单单例模式
    /// </summary>
    public class UploadEticketInfo
    {

        public bool RUNNING { get { return m_running; } }

        private MyTcpIpClient m_socket;
        private CommandPool m_pool;
        private LoginInfo m_li;

        private List<string> ls_pnr;
        private string file;
        private EagleWebService.kernalFunc fc;

        private DateTime m_date = new DateTime ();
        private int m_interval = 15;//minute
        private bool m_running;
        public UploadEticketInfo(MyTcpIpClient sk,CommandPool pool_background,LoginInfo li)
        {
            fc = new EagleWebService.kernalFunc(li.b2b.webservice);
            m_socket = sk;
            m_pool = pool_background;
            m_li = li;
            file = Application.StartupPath + "\\4ULticket.txt";
            ls_pnr = new List<string>();
            try
            {
                string[] a = File.ReadAllLines(file);
                foreach (string s in a)
                {
                    ls_pnr.Add(s);

                }
                File.WriteAllText(file, "");
            }
            catch
            {

            }
        }
        /// <summary>
        /// public 调用
        /// </summary>
        public void Start()
        {
            if (m_date.AddMinutes(m_interval) > DateTime.Now) return;
            m_date = DateTime.Now;
            EagleFileIO.LogWrite("启动后台上传电子客票信息");
            start();

        }
        /// <summary>
        /// 启动，在计时器里定时调用
        /// </summary>
        private void start()
        {
            m_running = true;

            string pnr = fc.PnrUnchecked(m_li.b2b.username).Trim();
            if (pnr != "")
            {
                EagleFileIO.LogWrite("PNR=" + pnr);
                if (BaseFunc.PnrValidate(pnr))
                {
                    m_pool.Clear();
                    string cmd = m_pool.HandleCommand("rt:n/" + pnr + "/eg");
                    m_socket.SendCommandBack(cmd, EagleProtocal.TypeOfCommand.AutoPnBack);
                }
                else
                {
                    bool flag = false;
                    fc.SubmitEticketInfomation("INVALID PNR", "", ' ', "", DateTime.Now, "", ' ', "", DateTime.Now,0, 1, "", "", 0, 0, 0, ref flag);
                }
            }
            else if (ls_pnr.Count > 0)
            {
                pnr = ls_pnr[0];
                EagleFileIO.LogWrite("PNR=" + pnr);
                ls_pnr.RemoveAt(0);
                m_pool.Clear();
                string cmd = m_pool.HandleCommand("rt:n/" + pnr + "/eg");
                m_socket.SendCommandBack(cmd, EagleProtocal.TypeOfCommand.AutoPnBack);
            }
            else
            {
                EagleFileIO.LogWrite("There are no eticket infomation to upload!");
                m_running = false;
            }
        }
        public void Recv(RtResult rtres)
        {
            bool flag = false;
            switch (rtres.FLAG_OF_PNR)
            {
                case PNR_FLAG.CANCELLED:
                    
                    fc.SubmitEticketInfomation(rtres, 0, 1, 0, 0, int.Parse(m_li.b2b.lr.IpidUsing[0]), ref flag);
                    return;
                    break;
                case PNR_FLAG.ETICKET:
                    break;
                case PNR_FLAG.MARRIED:
                    File.AppendAllText(file, "\r\n" + rtres.PNR);
                    break;
                case PNR_FLAG.NORMAL:
                    File.AppendAllText(file, "\r\n" + rtres.PNR);
                    break;
            }
            int total=0;
            int count = rtres.SEGMENG.Length;
            int[] sFare = new int[count];
            int[] sBuild = new int[count];
            int[] sFuel = new int[count];
            int[] sFareY = new int[count];
            global::EagleExtension.EagleExtension.CalPnrsTotalPrice(
                rtres, m_li.b2b.webservice, ref total, ref sFare, ref sBuild, ref sFuel, ref sFareY
                );
            int builda = 0;
            int buildc = 0;
            int fuela = 0;
            int fuelc = 0;
            foreach (int i in sBuild) builda += i;
            foreach (int i in sFuel)
            {
                fuela += i;
                fuelc += EagleString.egString.TicketPrice(i,50);
            }
            int build = rtres.CHILDRENCOUNT * buildc + rtres.ADULTCOUNT * builda;
            int fuel = rtres.CHILDRENCOUNT * fuelc + rtres.ADULTCOUNT * fuela;
            fc.SubmitEticketInfomation(rtres, total, 1, build, fuel, int.Parse(m_li.b2b.lr.IpidUsing[0]), ref flag);
            if (flag) EagleFileIO.LogWrite("电子客票提交成功：" + rtres.PNR);
            start();

        }
    }
}
