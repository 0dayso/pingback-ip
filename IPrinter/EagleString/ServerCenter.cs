using System;
using System.Collections.Generic;
using System.Text;

namespace EagleString
{
    /// <summary>
    /// 仅取b2b后台地址及b2b的web服务地址
    /// </summary>
    public class ServerCenterB2B
    {
        /// <summary>
        /// 取对应运营商的Web服务地址与后台管理地址
        /// </summary>
        /// <param name="sa">运营商</param>
        /// <param name="lp">线路(电信或网通)</param>
        /// <param name="pos">当运营商有多个地址指向同一台服务器时，取pos对应的地址</param>
        /// <param name="urlWebServer">返回的Web服务地址</param>
        /// <param name="urlWebSite">返回的后台地址</param>
        public void ServerAddressB2B(ServerAddr sa, LineProvider lp ,int pos,ref string urlWebService,ref string urlWebSite)
        {
            try
            {
                urlWebService = "";
                urlWebSite = "";
                //该运营商有多少个地址？
                int count = 0;
                for (int i = 0; i < m_ls.Count; ++i)
                {
                    if (m_ls[i].SA == sa && m_ls[i].LP == lp)
                    {
                        count++;
                    }
                }
                //计算pos对应的index
                if (count == 0) return;
                int index = pos % count;
                //再按sa,lp,index找对应的地址
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
        /// 服务中心
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
            //易格，电信
            index = 0;
            s = ServerAddr.Eagle;
#if SHANGHAI
            m_ls.Add(new SC(s, dx, index++, "http://b2bws.cn95161.com/egws.asmx", "http://b2b.cn95161.com/login.aspx"));
#else
            m_ls.Add(new SC(s, dx, index++, "http://yinge.eg66.com/WS3/egws.asmx", "http://yinge.eg66.com/EagleWeb2/login.aspx"));
#endif
            //易格，网通
            index = 0;
#if SHANGHAI
            m_ls.Add(new SC(s, dx, index++, "http://b2bws.cn95161.com/egws.asmx", "http://b2b.cn95161.com/login.aspx"));
#else
            m_ls.Add(new SC(s, wt, index++, "http://wangtong.eg66.com/WS3/egws.asmx", "http://wangtong.eg66.com/EagleWeb2/login.aspx"));
#endif

            //郑州机场，电信
            s = ServerAddr.ZhenZhouJiChang;
            index = 0;
            m_ls.Add(new SC(s, dx, index++, "http://10.2.1.23/ws/egws.asmx", "http://10.2.1.23/eagleweb/login.aspx"));
            //郑州机场，网通
            index = 0;
            m_ls.Add(new SC(s, wt, index++, "http://10.2.1.23/ws/egws.asmx", "http://10.2.1.23/eagleweb/login.aspx"));
            //郑州机场，VPN
            index = 0;
            m_ls.Add(new SC(s, vpn, index++, "http://www.zza96666.cn/ws/egws.asmx", "http://www.zza96666.cn/EagleWeb/login.aspx"));


            //昆明，电信
            index = 0;
            s = ServerAddr.KunMing;
            m_ls.Add(new SC(s, dx, index++, "http://220.165.8.58/Ws3/egws.asmx", "http://220.165.8.58/EagleWeb/login.aspx"));
            //昆明，网通
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

            //共享平台
            index = 0;
            s = ServerAddr.EgShare;
            m_ls.Add(new SC(s, dx, index++, "http://59.175.179.130/egsharews/egws.asmx", "http://220.165.8.58/EagleWeb/login.aspx"));

        }
    }
    
    /// <summary>
    /// 数据中心服务器地址
    /// </summary>
    public enum ServerAddr : byte
    {
        /// <summary>
        /// 易格
        /// </summary>
        Eagle = 0,
        /// <summary>
        /// 郑州机场
        /// </summary>
        ZhenZhouJiChang = 1,
        /// <summary>
        /// 南京
        /// </summary>
        //NKG = 2,
        /// <summary>
        /// 昆明
        /// </summary>
        KunMing = 3,
        /// <summary>
        /// 共享平台
        /// </summary>
        EgShare=4,
        RWY=5
    };
    public enum LineProvider : byte
    {
        /// <summary>
        /// 电信
        /// </summary>
        DianXin = 0,
        /// <summary>
        /// 网通
        /// </summary>
        WangTong = 1,
        /// <summary>
        /// VPN
        /// </summary>
        VPN = 2
        
    }
}
