using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace EagleForms.General
{
    public partial class SpecTickHandle : Form
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wsaddr">WebService地址</param>
        /// <param name="username">当前帐号用户名</param>
        public SpecTickHandle(string wsaddr,string username)
        {
            InitializeComponent();
            m_wsaddr = wsaddr;
            m_username = username;
        }

        private string m_xml = "";
        private string m_wsaddr = "";
        private string m_username = "";
        private string m_passport = "";
        private void btn_ReadNoHandle_Click(object sender, EventArgs e)
        {
            READ_NO_HANDLE();
        }
        public void READ_NO_HANDLE()
        {
            try
            {
                EagleWebService.kernalFunc kf = new EagleWebService.kernalFunc(m_wsaddr);
                string s = "";
                kf.SpecialTicketAppliedNoHandleToDisplay(ref s);
                set_var_with_xml(s);
            }
            catch
            {
            }
        }
        private void set_var_with_xml(string xml)
        {
            this.SuspendLayout();
            try
            {
                string s = xml;
                string encode = "<?xml version=\"1.0\" encoding=\"gb2312\" ?>";
                if (s.IndexOf(encode) < 0) s = encode + s;
                EagleWebService.NewPara np = new EagleWebService.NewPara(s);

                bool bDone = (np.FindTextByPath("//eg/result").Trim() == "Error!");

                string file = Path.GetTempPath() + "spectick.html";
                if (bDone)
                {
                    s = "处理完毕！";


                    

                    File.WriteAllText(file, s, Encoding.Default);
                    webBrowser1.Navigate(file);

                    return;
                }

                m_xml = s;
                if (m_xml == "") return;
                
                int.TryParse(np.FindTextByPath("//eg/result/order_id"), out m_id);
                int.TryParse(np.FindTextByPath("//eg/result/process_state"), out m_stat);
                m_pnr = np.FindTextByPath("//eg/result/PNR");
                m_flight = np.FindTextByPath("//eg/result/flight_number");
                char.TryParse(np.FindTextByPath("//eg/result/bunk"), out m_bunk);
                int.TryParse(np.FindTextByPath("//eg/result/process_price"), out m_handle_price);
                int.TryParse(np.FindTextByPath("//eg/result/original_price1"), out m_original_price);
                m_remark = np.FindTextByPath("//eg/result/remark") + np.FindTextByPath("//eg/reslult/reamrk1");
                m_applyuser = np.FindTextByPath("//eg/result/apply_user");
                set_control();

                File.WriteAllText(file, xml2html(), Encoding.Default);
                webBrowser1.Navigate(file);

            }
            catch
            {
            }
            this.ResumeLayout();
        }
        private int m_id = 0;
        /// <summary>
        /// 0:未处理 1:申请成功 2:申请失败
        /// </summary>
        private int m_stat = 0;
        private string m_pnr = "";
        private string m_flight = "";
        private char m_bunk = ' ';
        private int m_handle_price =0;
        private int m_original_price =0;
        private string m_remark ="";
        private string m_applyuser = "";

        private void set_control()
        {
            textBox1.Text = m_id.ToString();
            switch (m_stat)
            {
                case 0:
                    comboBox1.SelectedIndex = 2;
                    break;
                case 1:
                    comboBox1.SelectedIndex = 0;
                    break;
                case 2:
                    comboBox1.SelectedIndex = 1;
                    break;
            }
            textBox2.Text = m_pnr;
            textBox3.Text = m_flight;
            textBox4.Text = m_bunk.ToString();
            textBox5.Text = m_handle_price.ToString();
            textBox6.Text = m_original_price.ToString();
            textBox7.Text = m_remark;
        }

        /// <summary>
        /// 将控件值传给成员变量
        /// </summary>
        private void get_control()
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    m_stat = 1;
                    break;
                case 1:
                    m_stat = 2;
                    break;
                case 2:
                    m_stat = 0;
                    break;
            }
            m_pnr = textBox2.Text;
            m_flight = textBox3.Text;
            if (textBox4.Text.Length > 0)
                m_bunk = textBox4.Text[0];
            else m_bunk = ' ';
            int.TryParse(textBox5.Text, out m_handle_price);
            int.TryParse(textBox6.Text, out m_original_price);
            m_remark = textBox7.Text;
        }
        /// <summary>
        /// 提交K位处理情况的XML串
        /// </summary>
        public string SUBMIT_XML;


        private void button1_Click(object sender, EventArgs e)
        {
            get_control();
            bool bflag = false;
            EagleWebService.kernalFunc kf = new EagleWebService.kernalFunc(m_wsaddr);
            kf.SpecialTicketAppliedHandle(m_id, m_username, DateTime.Now, m_stat, m_pnr, m_flight, m_bunk, 
                m_handle_price, m_original_price, m_remark,m_applyuser, ref bflag,ref m_passport, ref SUBMIT_XML);
            if (bflag)
            {
                EagleProtocal.PACKET_PROMOPT_FINISH_APPLAY ep = new EagleProtocal.PACKET_PROMOPT_FINISH_APPLAY
(EagleProtocal.EagleProtocal.MsgNo++, new string[] { m_passport }, SUBMIT_XML);

                dg_spectick.Invoke(ep.ToBytes());

                MessageBox.Show("提交成功");
                READ_NO_HANDLE();
            }
            else
            {
                MessageBox.Show("提交失败，请重试！");
            }
        }
        /// <summary>
        /// 申请人的passport
        /// </summary>
        public string PASSPORT
        {
            get
            {
                return m_passport;
            }
        }

        private string xml2html()
        {
            XmlDocument xd = new XmlDocument ();
            xd.LoadXml(m_xml);
            string xml0 = m_xml;
            string htm = "";
            htm += "<table border=0 width=100% cellpadding=2 cellspacing=1 bgcolor=#0000FF style=font-size:12px>";
            //标题
            htm += "<tr>";
            int substart = xml0.IndexOf("<cm>");
            if (substart < 0) throw new Exception("");
            substart += 4;
            int subend = xml0.IndexOf("</cm>");
            htm += "<td bgcolor=#FFFFFF>" + /*xml0.Substring(substart, subend - substart)*/"未处理的申请" + "</td>";
            htm += "</tr>";
            //客户ID
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>特殊舱ID</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/product_id").InnerText + "</td>";
            htm += "</tr>";
            //客户号
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>生效日期</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/start_time").InnerText + "</td>";

            htm += "</tr>";
            //姓名
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>失效日期</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/end_time").InnerText + "</td>";

            htm += "</tr>";
            //电话
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>城市对</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/from_to1").InnerText + "</td>";

            htm += "</tr>";
            //证件类型及证件号
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>航空公司</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/airways").InnerText + "</td>";

            htm += "</tr>";
            //用户类型
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>适用航班</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/AddaptFlight").InnerText + "</td>";

            htm += "</tr>";
            //用户来源
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>舱位</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/bunk").InnerText + "</td>";

            htm += "</tr>";
            //积分
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>价格</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/price").InnerText + "</td>";

            htm += "</tr>";
            //建立时间
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>折扣或低点</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/discount").InnerText + "</td>";

            htm += "</tr>";
            //上次消费
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>返点</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/regain").InnerText + "</td>";

            htm += "</tr>";
            //归属代理号
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>供应商代号</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/provider").InnerText + "</td>";

            htm += "</tr>";
            //建档人
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>发布时间</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/publishdate").InnerText + "</td>";

            htm += "</tr>";
            //归属名
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>发布人</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/publisher").InnerText + "</td>";

            htm += "</tr>";
            //客户地址
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>底价</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/original_price").InnerText + "</td>";

            htm += "</tr>";
            //客户生日
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>申请序号</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/order_id").InnerText + "</td>";

            htm += "</tr>";
            //信用卡
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>申请人</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/apply_user").InnerText + "</td>";

            htm += "</tr>";

            //信用卡
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>申请时间</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/apply_date").InnerText + "</td>";

            htm += "</tr>";

            //信用卡
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>申请数量</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/bunk_amount").InnerText + "</td>";

            htm += "</tr>";
            //信用卡
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>航班日期</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/flight_date").InnerText + "</td>";

            htm += "</tr>";
            //信用卡
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>申请价格</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/apply_price").InnerText + "</td>";

            htm += "</tr>";
            //处理人
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>处理人</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/process_user").InnerText + "</td>";

            htm += "</tr>";
            //处理时间
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>处理时间</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/process_date").InnerText + "</td>";

            htm += "</tr>";
            //处理状态
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>处理状态</td>";
            string temp = xd.SelectSingleNode("//eg/result/process_state").InnerText.Trim();
            string temp2 = (temp=="1"?"申请成功":"申请失败");
            htm += "<td bgcolor=#FFFFFF>" + temp2 + "</td>";

            htm += "</tr>";

            //PNR
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>处理PNR</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/PNR").InnerText + "</td>";

            htm += "</tr>";

            //航班号
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>处理航班号</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/flight_number").InnerText + "</td>";

            htm += "</tr>";

            //航班号
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>处理舱位</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/new_bunk").InnerText + "</td>";

            htm += "</tr>";
            //航班号
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>处理价格</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/process_price").InnerText + "</td>";

            htm += "</tr>";
            htm += "</table>";
            return htm;
        }
        public delegate void deleg4SepcTick(byte[] b);
        public deleg4SepcTick dg_spectick;
        private void btnPnrView_Click(object sender, EventArgs e)
        {
            get_control();
            EagleWebService.IbeFunc fc = new EagleWebService.IbeFunc();
            MessageBox.Show(fc.RT(m_pnr));
        }
    }
}