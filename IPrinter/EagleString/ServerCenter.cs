using System;
using System.Collections.Generic;
using System.Text;

namespace EagleString
{
    /// <summary>
    /// ��ȡb2b��̨��ַ��b2b��web�����ַ
    /// </summary>
    public class ServerCenterB2B
    {
        /// <summary>
        /// ȡ��Ӧ��Ӫ�̵�Web�����ַ���̨�����ַ
        /// </summary>
        /// <param name="sa">��Ӫ��</param>
        /// <param name="lp">��·(���Ż���ͨ)</param>
        /// <param name="pos">����Ӫ���ж����ַָ��ͬһ̨������ʱ��ȡpos��Ӧ�ĵ�ַ</param>
        /// <param name="urlWebServer">���ص�Web�����ַ</param>
        /// <param name="urlWebSite">���صĺ�̨��ַ</param>
        public void ServerAddressB2B(ServerAddr sa, LineProvider lp ,int pos,ref string urlWebService,ref string urlWebSite)
        {
            try
            {
                urlWebService = "";
                urlWebSite = "";
                //����Ӫ���ж��ٸ���ַ��
                int count = 0;
                for (int i = 0; i < m_ls.Count; ++i)
                {
                    if (m_ls[i].SA == sa && m_ls[i].LP == lp)
                    {
                        count++;
                    }
                }
                //����pos��Ӧ��index
                if (count == 0) return;
                int index = pos % count;
                //�ٰ�sa,lp,index�Ҷ�Ӧ�ĵ�ַ
                for (int i = 0; i < m_ls.Count; ++i)
                {
                    if (m_ls[i].SA == sa && m_ls[i].LP == lp && index == m_ls[i].INDEX)
                    {
                        urlWebService = m_ls[i].WEBSERVICE;
                        urlWebSite = m_ls[i].WEBSITE;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                EagleFileIO.LogWrite("ServerCenterB2B.ServerAddressB2B : " + ex.Message);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        struct SC
        {
            public ServerAddr SA;
            public LineProvider LP;
            public int INDEX;
            public string WEBSERVICE;
            public string WEBSITE;
            public SC(ServerAddr sa, LineProvider lp, int index, string wservice, string wsite)
            {
                SA = sa;
                LP = lp;
                INDEX = index;
                WEBSERVICE = wservice;
                WEBSITE = wsite;
            }
        }
        List<SC> m_ls = new List<SC>();
       
        public ServerCenterB2B()
        {
            int index = 0;
            LineProvider dx = LineProvider.DianXin;
            LineProvider wt = LineProvider.WangTong;
            LineProvider vpn = LineProvider.VPN;
            ServerAddr s;
            //�׸񣬵���
            index = 0;
            s = ServerAddr.Eagle;
#if SHANGHAI
            m_ls.Add(new SC(s, dx, index++, "http://b2bws.cn95161.com/egws.asmx", "http://b2b.cn95161.com/login.aspx"));
#else
            m_ls.Add(new SC(s, dx, index++, "http://yinge.eg66.com/WS3/egws.asmx", "http://yinge.eg66.com/EagleWeb2/login.aspx"));
#endif
            //�׸���ͨ
            index = 0;
#if SHANGHAI
            m_ls.Add(new SC(s, dx, index++, "http://b2bws.cn95161.com/egws.asmx", "http://b2b.cn95161.com/login.aspx"));
#else
            m_ls.Add(new SC(s, wt, index++, "http://wangtong.eg66.com/WS3/egws.asmx", "http://wangtong.eg66.com/EagleWeb2/login.aspx"));
#endif

            //֣�ݻ���������
            s = ServerAddr.ZhenZhouJiChang;
            index = 0;
            m_ls.Add(new SC(s, dx, index++, "http://10.2.1.23/ws/egws.asmx", "http://10.2.1.23/eagleweb/login.aspx"));
            //֣�ݻ�������ͨ
            index = 0;
            m_ls.Add(new SC(s, wt, index++, "http://10.2.1.23/ws/egws.asmx", "http://10.2.1.23/eagleweb/login.aspx"));
            //֣�ݻ�����VPN
            index = 0;
            m_ls.Add(new SC(s, vpn, index++, "http://www.zza96666.cn/ws/egws.asmx", "http://www.zza96666.cn/EagleWeb/login.aspx"));


            //����������
            index = 0;
            s = ServerAddr.KunMing;
            m_ls.Add(new SC(s, dx, index++, "http://220.165.8.58/Ws3/egws.asmx", "http://220.165.8.58/EagleWeb/login.aspx"));
            //��������ͨ
            index = 0;
            m_ls.Add(new SC(s, wt, index++, "http://220.165.8.58/Ws3/egws.asmx", "http://220.165.8.58/EagleWeb/login.aspx"));

            //RWY=5
            index = 0;
            s = ServerAddr.RWY;
            //m_ls.Add(new SC(s, dx, index++, "http://222.73.204.236/egws.asmx", "http://222.73.204.236/egws.asmx"));
            m_ls.Add(new SC(s, dx, index++, "http://www.gz161.com/ws/egws.asmx", "http://www.gz161.com/ws/egws.asmx"));
            index = 0;
            //m_ls.Add(new SC(s, wt, index++, "http://222.73.204.236/egws.asmx", "http://222.73.204.236/egws.asmx"));
            m_ls.Add(new SC(s, wt, index++, "http://www.gz161.com/ws/egws.asmx", "http://www.gz161.com/ws/egws.asmx"));

            //����ƽ̨
            index = 0;
            s = ServerAddr.EgShare;
            m_ls.Add(new SC(s, dx, index++, "http://59.175.179.130/egsharews/egws.asmx", "http://220.165.8.58/EagleWeb/login.aspx"));

        }
    }
    
    /// <summary>
    /// �������ķ�������ַ
    /// </summary>
    public enum ServerAddr : byte
    {
        /// <summary>
        /// �׸�
        /// </summary>
        Eagle = 0,
        /// <summary>
        /// ֣�ݻ���
        /// </summary>
        ZhenZhouJiChang = 1,
        /// <summary>
        /// �Ͼ�
        /// </summary>
        //NKG = 2,
        /// <summary>
        /// ����
        /// </summary>
        KunMing = 3,
        /// <summary>
        /// ����ƽ̨
        /// </summary>
        EgShare=4,
        RWY=5
    };
    public enum LineProvider : byte
    {
        /// <summary>
        /// ����
        /// </summary>
        DianXin = 0,
        /// <summary>
        /// ��ͨ
        /// </summary>
        WangTong = 1,
        /// <summary>
        /// VPN
        /// </summary>
        VPN = 2
        
    }
}
