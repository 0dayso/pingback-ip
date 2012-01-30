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
        /// <param name="wsaddr">WebService��ַ</param>
        /// <param name="username">��ǰ�ʺ��û���</param>
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
                    s = "������ϣ�";


                    

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
        /// 0:δ���� 1:����ɹ� 2:����ʧ��
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
        /// ���ؼ�ֵ������Ա����
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
        /// �ύKλ���������XML��
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

                MessageBox.Show("�ύ�ɹ�");
                READ_NO_HANDLE();
            }
            else
            {
                MessageBox.Show("�ύʧ�ܣ������ԣ�");
            }
        }
        /// <summary>
        /// �����˵�passport
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
            //����
            htm += "<tr>";
            int substart = xml0.IndexOf("<cm>");
            if (substart < 0) throw new Exception("");
            substart += 4;
            int subend = xml0.IndexOf("</cm>");
            htm += "<td bgcolor=#FFFFFF>" + /*xml0.Substring(substart, subend - substart)*/"δ���������" + "</td>";
            htm += "</tr>";
            //�ͻ�ID
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>�����ID</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/product_id").InnerText + "</td>";
            htm += "</tr>";
            //�ͻ���
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>��Ч����</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/start_time").InnerText + "</td>";

            htm += "</tr>";
            //����
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>ʧЧ����</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/end_time").InnerText + "</td>";

            htm += "</tr>";
            //�绰
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>���ж�</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/from_to1").InnerText + "</td>";

            htm += "</tr>";
            //֤�����ͼ�֤����
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>���չ�˾</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/airways").InnerText + "</td>";

            htm += "</tr>";
            //�û�����
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>���ú���</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/AddaptFlight").InnerText + "</td>";

            htm += "</tr>";
            //�û���Դ
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>��λ</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/bunk").InnerText + "</td>";

            htm += "</tr>";
            //����
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>�۸�</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/price").InnerText + "</td>";

            htm += "</tr>";
            //����ʱ��
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>�ۿۻ�͵�</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/discount").InnerText + "</td>";

            htm += "</tr>";
            //�ϴ�����
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>����</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/regain").InnerText + "</td>";

            htm += "</tr>";
            //���������
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>��Ӧ�̴���</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/provider").InnerText + "</td>";

            htm += "</tr>";
            //������
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>����ʱ��</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/publishdate").InnerText + "</td>";

            htm += "</tr>";
            //������
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>������</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/publisher").InnerText + "</td>";

            htm += "</tr>";
            //�ͻ���ַ
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>�׼�</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/original_price").InnerText + "</td>";

            htm += "</tr>";
            //�ͻ�����
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>�������</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/order_id").InnerText + "</td>";

            htm += "</tr>";
            //���ÿ�
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>������</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/apply_user").InnerText + "</td>";

            htm += "</tr>";

            //���ÿ�
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>����ʱ��</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/apply_date").InnerText + "</td>";

            htm += "</tr>";

            //���ÿ�
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>��������</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/bunk_amount").InnerText + "</td>";

            htm += "</tr>";
            //���ÿ�
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>��������</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/flight_date").InnerText + "</td>";

            htm += "</tr>";
            //���ÿ�
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>����۸�</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/apply_price").InnerText + "</td>";

            htm += "</tr>";
            //������
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>������</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/process_user").InnerText + "</td>";

            htm += "</tr>";
            //����ʱ��
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>����ʱ��</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/process_date").InnerText + "</td>";

            htm += "</tr>";
            //����״̬
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>����״̬</td>";
            string temp = xd.SelectSingleNode("//eg/result/process_state").InnerText.Trim();
            string temp2 = (temp=="1"?"����ɹ�":"����ʧ��");
            htm += "<td bgcolor=#FFFFFF>" + temp2 + "</td>";

            htm += "</tr>";

            //PNR
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>����PNR</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/PNR").InnerText + "</td>";

            htm += "</tr>";

            //�����
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>�������</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/flight_number").InnerText + "</td>";

            htm += "</tr>";

            //�����
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>�����λ</td>";
            htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//eg/result/new_bunk").InnerText + "</td>";

            htm += "</tr>";
            //�����
            htm += "<tr>";
            htm += "<td bgcolor=#FFFFFF>����۸�</td>";
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