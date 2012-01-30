using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using EagleWebService;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using EagleString;
using System.Collections;

namespace EagleForms.Printer
{
    public enum PRINT_TYPE:byte
    {
        /// <summary>
        /// ����
        /// </summary>
        INSURANCE = 0,
        /// <summary>
        /// �г̵�
        /// </summary>
        RECEIPT = 1
    }
    public class PrintXmlHandle
    {
        private System.Drawing.Printing.PrintDocument ptDoc = new System.Drawing.Printing.PrintDocument();
        private XmlHandler m_hander;
        private PRINT_TYPE m_pType;
        private string m_iType;
        private XmlNode m_root;
        public PRINT_INFO m_pInfo;
        private PRINT_ITEM[] m_pItem;
        private PRINT_FIX[] m_pFix;
        //string file = Application.StartupPath + "\\PrinterDesign.xml";//2010.08.11 ���ǵ�������,���ս��������ļ�

        /// <summary>
        /// ����,�������ļ���ȡֵ->m_pItem,m_pInfo
        /// </summary>
        /// <param name="pType">��ӡ���ͣ����գ��г̵�</param>
        /// <param name="iType">�����г̵���Ч</param>
        public PrintXmlHandle(PRINT_TYPE pType, string printingXML, int productID)
        {
            Initia(pType, printingXML, productID);
        }

        public void Initia(PRINT_TYPE pType, string printingXML, int productID)
        {
            //if (!File.Exists(file))
            //{
            //    File.Copy(Path.Combine(Application.StartupPath, "PrinterDesign.default"), file);
            //}

            m_hander = new XmlHandler();

            try
            {
                //if (File.Exists(file))
                //    m_hander.LoadXml(File.ReadAllText(file, Encoding.UTF8));
                //else
                {
                    string fileProduct = "Pt" + productID.ToString() + ".xml";
                    if (File.Exists(fileProduct))
                        m_hander.LoadXml(File.ReadAllText(fileProduct, Encoding.UTF8));
                    else
                        m_hander.LoadXml(printingXML);//2010.08.11 ��ӡ�����ļ����浽���ݿ���
                }
            }
            catch
            {
                throw new Exception("��ӡ��ʽ��Ϣ�д���,����������Ա��");
            }

            m_pType = pType;
            //m_iType = iType;
            switch (pType)
            {
                case PRINT_TYPE.INSURANCE:
                    //if (File.Exists(file))
                    //{
                    //    m_root = m_hander.root.SelectSingleNode("insurrances");
                    //    for (int i = 0; i < m_root.ChildNodes.Count; ++i)
                    //    {
                    //        if (m_hander.FindXnByNameAndValue(m_root.ChildNodes[i].ChildNodes, "code", Options.GlobalVar.IACode) != null)
                    //        {
                    //            m_root = m_root.ChildNodes[i];
                    //            break;
                    //        }
                    //        if (i == m_root.ChildNodes.Count) m_root = null;
                    //    }
                    //}
                    //else
                        m_root = m_hander.root.SelectSingleNode("/insurrance");
                    if (m_root == null)
                        throw new Exception("��ӡ��ʽ��Ϣ�д���,����������Ա��");
                    break;
                case PRINT_TYPE.RECEIPT:
                    m_root = m_hander.root.SelectSingleNode("tickets/receipt");
                    break;
            }
            m_pInfo = new PRINT_INFO();
            m_pInfo.FromXmlNode(m_root);
            int iItem = m_root.SelectSingleNode("items").ChildNodes.Count;
            m_pItem = new PRINT_ITEM[iItem];
            for (int i = 0; i < iItem; ++i)
            {
                m_pItem[i].FromXmlNode(m_root.SelectSingleNode("items").ChildNodes[i], m_pInfo.m_font);
            }
            int iFix = m_root.SelectSingleNode("fixdata").ChildNodes.Count;
            m_pFix = new PRINT_FIX[iFix];
            for (int i = 0; i < iFix; ++i)
            {
                m_pFix[i].FromXmlNode(m_root.SelectSingleNode("fixdata").ChildNodes[i], m_pInfo.m_font);
            }
            //m_pInfo.m_password = (m_pInfo.m_password);
            ptDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(ptDoc_PrintPage);
        }

        void ptDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            e.PageSettings.Margins.Left = 0;
            e.PageSettings.Margins.Right = 0;
            e.PageSettings.Margins.Top = 0;
            e.PageSettings.Margins.Bottom = 0;

            Font f = m_pInfo.m_font;//ȫ������

            Brush b = Brushes.Black;
            PointF o = m_pInfo.m_offset;

            List<string> ptString = new List<string>();
            List<PointF> ptPoint = new List<PointF>();
            for (int i = 0; i < m_pItem.Length; ++i)
            {
                ptString.Add(m_pItem[i].m_value);
                ptPoint.Add(m_pItem[i].m_position);
            }
            for (int i = 0; i < m_pFix.Length; ++i)
            {
                ptString.Add(m_pFix[i].m_value);
                ptPoint.Add(m_pFix[i].m_position);
            }
            PrintItems(ptString.ToArray(), ptPoint.ToArray(), o, f, b, e);
        }
        void PrintItems(string[] strs, PointF[] pts, PointF o, Font ft, Brush bs, System.Drawing.Printing.PrintPageEventArgs e)
        {
            for (int i = 0; i < strs.Length; i++)
                e.Graphics.DrawString(strs[i], ft, bs, pts[i].X + o.X, pts[i].Y + o.Y);
        }
        /// <summary>
        /// ȡ���ж�Ӧ�ؼ�ֵ,�����ӡʱ����
        /// </summary>
        private void FromForm(Form form)
        {
            m_pInfo.m_offset.X = (int)((NumericUpDown)BaseFunc.SearchSubCtrl(form.Controls, "udLeftMargin")).Value;
            m_pInfo.m_offset.Y = (int)((NumericUpDown)BaseFunc.SearchSubCtrl(form.Controls, "udUpMargin")).Value;

            if (m_pType == PRINT_TYPE.INSURANCE)
            {
                try
                {
                    string font = BaseFunc.SearchSubCtrl(form.Controls, "btnFont").Text;
                    m_pInfo.m_font = new Font(font.Split(',')[0], float.Parse(font.Split(',')[1]));
                }
                catch
                {
                    m_pInfo.m_font = new Font("System", 12F);
                }
            }
            for (int i = 0; i < m_pItem.Length; ++i)
            {
                m_pItem[i].m_value = EagleString.BaseFunc.SearchSubCtrl(form.Controls, m_pItem[i].m_ctrlname).Text;
            }
        }
        public void ToForm(Form form)
        {
            BaseFunc.SearchSubCtrl(form.Controls, "pnlTitle").BackgroundImage = m_pInfo.TitleBgImage;
            BaseFunc.SearchSubCtrl(form.Controls, "pnlInsueInfo").BackColor = m_pInfo.BackColor;
            BaseFunc.SearchSubCtrl(form.Controls, "pnlCustom").BackColor = m_pInfo.BackColor;

            //��ӡƫ����,������ӡ������Ϣ��ֱ�Ӵ������ݿ��ֶ�
            //((NumericUpDown)BaseFunc.SearchSubCtrl(form.Controls, "udLeftMargin")).Value = (decimal)m_pInfo.m_offset.X;
            //((NumericUpDown)BaseFunc.SearchSubCtrl(form.Controls, "udUpMargin")).Value = (decimal)m_pInfo.m_offset.Y;
            ((NumericUpDown)BaseFunc.SearchSubCtrl(form.Controls, "udLeftMargin")).Value = Options.GlobalVar.IAOffsetX;
            ((NumericUpDown)BaseFunc.SearchSubCtrl(form.Controls, "udUpMargin")).Value = Options.GlobalVar.IAOffsetY;

            BaseFunc.SearchSubCtrl(form.Controls, "lblTitle1").Text = m_pInfo.m_title1;
            BaseFunc.SearchSubCtrl(form.Controls, "lblTitle2").Text = m_pInfo.m_title2;
            BaseFunc.SearchSubCtrl(form.Controls, "btnFont").Text = m_pInfo.m_font.Name + "," + m_pInfo.m_font.Size.ToString("f2");

            for (int i = 0; i < m_pItem.Length; ++i)
            {
                BaseFunc.SearchSubCtrl(form.Controls, m_pItem[i].m_ctrlname).Text = m_pItem[i].m_value;
            }
        }
        public void ToFormReceipt(Form form)
        {
            for (int i = 0; i < m_pItem.Length; ++i)
            {
                BaseFunc.SearchSubCtrl(form.Controls, m_pItem[i].m_ctrlname).Text = m_pItem[i].m_value;
            }
        }
        /// <summary>
        /// ���ӡ�󽫿ؼ����ݱ���,�Ա������һ�δ�ӡ��Ϣ
        /// </summary>
        private void Save(Form form)
        {
            m_pInfo.ToXmlNode();
            for (int i = 0; i < m_pItem.Length; ++i)
            {
                m_pItem[i].ToXmlNode();
            }

            //if (File.Exists(file))
            //    m_hander.SaveXml(file);
            //else
            {
                m_hander.SaveXml("Pt" + m_pInfo.m_code + ".xml");
                //�������һ��ѡ�еĲ�Ʒ����
                ComboBox cmbInsuranceType = (ComboBox)BaseFunc.SearchSubCtrl(form.Controls, "cmbInsuranceType");
                string productString = ((EagleWebService.t_Product)(cmbInsuranceType.Items[cmbInsuranceType.SelectedIndex])).FilterComment;
                string[] productIdAndDuration = productString.Split('|');
                string productId = productIdAndDuration[0];
                Options.GlobalVar.IACode = productId;
            }

            Options.GlobalVar.IAOffsetX = m_pInfo.m_offset.X;
            Options.GlobalVar.IAOffsetY = m_pInfo.m_offset.Y;
        }

        public void Print(Form form)
        {
            if (m_pType == PRINT_TYPE.RECEIPT)
            {
                ptDoc.DocumentName = "�г̵���ӡ";
                if (EagleString.BaseFunc.FontExist("TEC"))
                {
                    m_pInfo.m_font = new Font("TEC", 18F);
                }
                else
                {
                    EagleString.BaseFunc.FontTecSetupMethodPromopt();
                    return;
                }
            }
            else if (m_pType == PRINT_TYPE.INSURANCE)
            {
                ptDoc.DocumentName = "������ӡ";
            }
            FromForm(form);
            PrinterSetupCostom(ptDoc, m_pInfo.m_width, m_pInfo.m_height);
            ptDoc.Print();
            Save(form);
        }
        private bool PrinterSetupCostom(System.Drawing.Printing.PrintDocument ptDoc, int w, int h)
        {//�ڴ�ӡ֮ǰ����ӡʱ��e.Graphics.PageUnit�����˺��׵�λ��������Ķ�����λӦ����1/100Ӣ�磬�����Ŀ�ȴ����240���ף�ת�����ž���950����λ��240*0.3937*10��
            try
            {
                System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
                ps.PaperSize = new System.Drawing.Printing.PaperSize("Custom", w, h);
                ps.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
                ptDoc.DefaultPageSettings = ps;
            }
            catch
            { return false; }
            return true;
        }

        public void ModifyReceiptFixDataXXX(string s)
        {
            //m_hander.root.SelectSingleNode("tickets/receipt/fixdata/data/value").InnerText = s;
            //m_hander.SaveXml(file);
        }

        public class SomeFunc
        {
            /// <summary>
            /// ʹ������Ҫ���ú�����m_socket
            /// </summary>
            public static EagleProtocal.MyTcpIpClient socket;
            /// <summary>
            /// ���ú�����ָ���
            /// </summary>
            public static EagleString.CommandPool cmdpool;
            /// <summary>
            /// ����Ȩ�޼�PrintDesign.xml���ݴ������մ�ӡ�Ŀ�ݲ˵�(EagleString.Structs.I_HASH
            /// </summary>
            public static ContextMenuStrip MenuInsurance(EagleString.LoginResult lr)
            {
                ContextMenuStrip menu = new ContextMenuStrip();
                EagleString.Structs.INSURANCE_HASH.Clear();
                XmlDocument xd = new XmlDocument();
                xd.Load(Application.StartupPath + "\\PrinterDesign.xml");
                XmlNode xn = xd.SelectSingleNode("//printer/insurrances");
                for (int i = 0; i < xn.ChildNodes.Count; ++i)
                {
                    XmlNode node = xn.ChildNodes[i];
                    string code = node.SelectSingleNode("code").InnerText.Trim();
                    string name = node.SelectSingleNode("name_cn").InnerText.Trim();
                    if (lr.AuthorityOfFunction(code))
                    {
                        EagleString.Structs.INSURANCE_HASH.Add(code, name);
                        menu.Items.Add(name, null, new EventHandler(OpenInsur));
                    }
                }
                return menu;
            }
            /// <summary>
            /// ���ݲ˵���ʾ���ı����ҵ���Ӧ�������ͣ�����ʾ
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private static void OpenInsur(object sender, EventArgs e)
            {
                string name = "";
                try
                {
                    name = (sender as ToolStripDropDownItem).Text;
                }
                catch
                {
                    name = (sender as MenuItem).Text;
                }
                finally
                {
                    if (name == "") name = (sender as Control).Text;
                }
                foreach (DictionaryEntry de in EagleString.Structs.INSURANCE_HASH)
                {
                    if (de.Value.ToString() == name)
                    {
                        Insurance dlg = new Insurance(PRINT_TYPE.INSURANCE, de.Key.ToString(),socket,cmdpool);
                        dlg.Show();
                    }
                }
            }
        }
    }
    /// <summary>
    /// ��ӡ��Ϣ��λ��insurance���
    /// </summary>
    public struct PRINT_INFO
    {
        /// <summary>
        /// ���ʹ��룬�磺B10
        /// </summary>
        public string m_code;
        public string m_name_en;
        public string m_name_cn;
        public string m_title1;
        public string m_title2;
        public Point m_offset;
        public string m_ehead;
        public Font m_font;
        public string m_icofile;
        public int m_len_eno;
        public int m_len_seq;
        public XmlNode m_xn;
        public string m_last_seq;
        public int m_width;
        public int m_height;
        public string m_username;
        public string m_password;
        public Image TitleBgImage;
        public Color BackColor;

        /// <summary>
        /// �Ӷ�Ӧinsurance����ȡֵs
        /// </summary>
        public void FromXmlNode(XmlNode xn)
        {
            m_xn = xn;    
            m_code = xn.SelectSingleNode("code").InnerText.Trim();
            m_name_en = xn.SelectSingleNode("name_en").InnerText.Trim();
            m_name_cn = xn.SelectSingleNode("name_cn").InnerText.Trim();
            m_title1 = xn.SelectSingleNode("title1").InnerText.Trim();
            m_title2 = xn.SelectSingleNode("title2").InnerText.Trim();
            string offset = xn.SelectSingleNode("offset").InnerText.Trim();
            m_offset.X = int.Parse(offset.Split(',')[0]);
            m_offset.Y = int.Parse(offset.Split(',')[1]);
            m_ehead = xn.SelectSingleNode("ehead").InnerText.Trim();
            try
            {
                string font = xn.SelectSingleNode("font").InnerText.Trim();
                m_font = new Font(font.Split(',')[0], float.Parse(font.Split(',')[1]));
            }
            catch
            {
                m_font = new Font("System", 12F);
            }
            m_icofile = Application.StartupPath + "\\" + xn.SelectSingleNode("icofile").InnerText.Trim();
            m_len_eno = int.Parse(xn.SelectSingleNode("len_eno").InnerText.Trim());
            m_len_seq = int.Parse(xn.SelectSingleNode("len_seq").InnerText.Trim());
            m_last_seq = xn.SelectSingleNode("seq_last").InnerText.Trim();
            m_width = int.Parse(xn.SelectSingleNode("pagewidth").InnerText.Trim());
            m_height = int.Parse(xn.SelectSingleNode("pageheight").InnerText.Trim());
            try
            { BackColor = ColorTranslator.FromHtml(xn.SelectSingleNode("BackColor").InnerText.Trim()); }
            catch
            {
                try
                { BackColor = Color.FromName(xn.SelectSingleNode("BackColor").InnerText.Trim()); }
                catch
                {
                    try
                    {
                        string[] rgb = xn.SelectSingleNode("BackColor").InnerText.Trim().Split(',');
                        int r = int.Parse(rgb[0].Trim());
                        int g = int.Parse(rgb[1].Trim());
                        int b = int.Parse(rgb[2].Trim());
                        BackColor = Color.FromArgb(r, g, b);
                    }
                    catch { }
                }
            }
            try
            {
                TitleBgImage = Image.FromFile(xn.SelectSingleNode("TitleBgImage").InnerText.Trim());
            }
            catch { }

            //m_username = xn.SelectSingleNode("username").InnerText.Trim();
            //m_password = xn.SelectSingleNode("password").InnerText.Trim();
            //m_password = BaseFunc.Crypt.CryptString.DeCode(m_password);//��ȡ�����
            m_username = Options.GlobalVar.IAUsername;
            m_password = Options.GlobalVar.IAPassword;
        }
        public void ToXmlNode()
        {
            XmlNode xn = m_xn;
            xn.SelectSingleNode("offset").InnerText = m_offset.X.ToString() + "," + m_offset.Y.ToString();
            xn.SelectSingleNode("font").InnerText = m_font.Name + "," + m_font.Size.ToString("f2");
            xn.SelectSingleNode("seq_last").InnerText = m_last_seq;

            xn.SelectSingleNode("username").InnerText = m_username;
            xn.SelectSingleNode("password").InnerText = BaseFunc.Crypt.CryptString.EnCode(m_password);//���ܺ󱣴�
        }

        
    }
    /// <summary>
    /// Ҫ��ӡ���λ��items
    /// </summary>
    public struct PRINT_ITEM
    {
        public PointF m_position;
        public string m_ctrlname;
        public Font m_font;
        public string m_value;
        public XmlNode m_xn;
        /// <summary>
        /// ��Ӧitem����л�ȡֵ
        /// </summary>
        /// <param name="xn"></param>
        public void FromXmlNode(XmlNode xn,Font ParentFont)
        {
            m_xn = xn;
            string pos = xn.SelectSingleNode("position").InnerText.Trim();
            m_position.X = float.Parse(pos.Split(',')[0].Trim());
            m_position.Y = float.Parse(pos.Split(',')[1].Trim());
            m_ctrlname = xn.SelectSingleNode("name_ctrl").InnerText.Trim();
            try
            {
                string font = xn.SelectSingleNode("font").InnerText.Trim();
                m_font = new Font(font.Split(',')[0], float.Parse(font.Split(',')[1]));
            }
            catch
            {
                m_font = ParentFont;
            }
            m_value = xn.SelectSingleNode("value").InnerText.Trim();
        }
        public void ToXmlNode()
        {
            XmlNode xn = m_xn;
            xn.SelectSingleNode("position").InnerText = string.Format("{0},{1}", m_position.X, m_position.Y);
            xn.SelectSingleNode("value").InnerText = m_value;
        }
    }
    /// <summary>
    /// Ҫ��ӡ�Ĺ̶��λ��fixdata
    /// </summary>
    public struct PRINT_FIX
    {
        public Font m_font;
        public PointF m_position;
        public string m_value;
        public XmlNode m_xn;
        public void FromXmlNode(XmlNode xn,Font ParentFont)
        {
            m_xn = xn;
            try
            {
                string font = xn.SelectSingleNode("font").InnerText.Trim();
                m_font = new Font(font.Split(',')[0], float.Parse(font.Split(',')[1]));
            }
            catch
            {
                m_font = ParentFont;
            }
            string pos = xn.SelectSingleNode("position").InnerText.Trim();
            m_position.X = float.Parse(pos.Split(',')[0].Trim());
            m_position.Y = float.Parse(pos.Split(',')[1].Trim());
            m_value = xn.SelectSingleNode("value").InnerText.Trim();
        }
    }

}
/*
 * һЩע��˵��
 * //001Ϊ �����մ�ӡ--PICC
        static public bool b_002 = false;//002Ϊ ��Ʊ��ӡ
        static public bool b_003 = false;//003Ϊ �����г̵���ӡ
        static public bool b_004 = false;//004Ϊ �ն�
        static public bool b_005 = false;//005Ϊ���յ���ӡ
        static public bool b_006 = false;//006��Ϊ����û�
        static public bool b_016 = false;//016����û���ֹ��Ʊ
        static public bool b_026 = false;//026����û���ʾ����
        static public bool b_007 = false;//007Ϊ���������մ�ӡ

        static public bool b_008 = false;//008Ϊɢ��ƴ��A
        static public bool b_018 = false;//018Ϊɢ��ƴ��B
        static public bool b_028 = false;//028Ϊɢ��ƴ��c
        static public bool b_038 = false;//038Ϊɢ��ƴ��D
        static public bool b_048 = false;//048Ϊɢ��ƴ��E

        static public bool b_009 = false;//009Ϊ�»����մ�ӡ
        static public bool b_00A = false;//00AΪ�г̵��ѻ���ӡ
        static public bool b_00B = false;//00BΪ���������淢��

        static public bool b_B01 = false;//B01Ϊ���գ���������ͨ�����˺����յ�
        static public bool b_B02 = false;//B02Ϊ���գ����٣������ÿ����������˺����յ�
        static public bool b_B03 = false;//B03Ϊ���գ��������
        static public bool b_B04 = false;//B04Ϊ���գ������������
        static public bool b_B05 = false;//B05Ϊ���գ����������

        static public bool b_B06 = false;//B06Ϊ�����й���Ա���տ�
        static public bool b_B07 = false;//B07ΪSunshine�������
        static public bool b_B08 = false;//B08Ϊ���������տ�
        static public bool b_B09 = false;//B09Ϊ�׸������տ�
        static public bool b_B0A = false;//B0AΪ̫ƽ������
        static public bool b_B0B = false;//B0AΪ��������ͨ
        static public bool b_B0C = false;//BOCΪ���ٺ���������2

        static public bool b_B0D = false;//BODΪ�׸���֮PICC
        //B10-����(����)
        //b11-����(����)
        static public bool b_QQQ = false;//QQQΪ�Զ���Q

        static public bool b_0FN = false;//�ر�FN��
        static public bool b_0FM = false;//�ر��г̵�����

        static public bool b_F12 = false;//ֱ�ӷ��ͷ�ʽ

        static public bool b_00C = false;//�Ƿ���ȡ������PNR��Ϊtrue����ȡ����Ϊfalse����ȡ��
        static public bool b_00D = false;//��ռ����

        static public bool b_00E = false;//IBE�������Ƿ�Ĭ��Ϊ����(��IBE) 
        static public bool b_00F = false;//IBE�����(����רҵ�����)�Ƿ�Ĭ��Ϊ����(��IBE) 
        static public bool b_00G = false;//IBE������(���ӡ)�Ƿ�Ĭ������(��IBE) 

        static public bool b_00H = false;//���غ�̨����ť

        static public bool b_0CTI = false;//�������Ĺ���
*/