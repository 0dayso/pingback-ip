using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace ePlus.PrintHyx
{
    public partial class SinoSafe : Form
    {
        public SinoSafe()
        {
            InitializeComponent();
        }
        static public bool b_opened = false;
        static public string retstring = "";
        static public PrintHyx.SinoSafe context = null;
        string eNumberHead = "";
        private void SinoSafe_Load(object sender, EventArgs e)
        {
            throw new Exception("Unknown Error!");
            LogoPicture.pictures pic = new LogoPicture.pictures();
            this.pictureBox1.Image = pic.pictureBox3.Image;
            cbInsureType.Text = cbInsureType.Items[0].ToString();

            DateTime dt = new DateTime();
            dt = System.DateTime.Now;
            dtp_Start.Value = dt;
            dtp_Date.Value = dt;
            dtp_End.Value = dt.AddDays(7);

            cfg_sinosafe cs = new cfg_sinosafe();
            cs.GetConfig();
            this.tb_Signature.Text = cs.Signature;
            this.numericUpDown1.Value = cs.OffsetX;
            this.numericUpDown2.Value = cs.OffsetY;
            this.tb_NO.Text = cs.SaveNo;
            this.tbPhone.Text = cs.Phone;
            this.tbCompany.Text = cs.CompanyAddr;

            retstring = "";
            b_opened = true;
            connect_4_Command.PrintWindowOpen = true;
            context = this;
            if (this.tbPhone.Text.Trim() == "82424242") tbPhone.Text = "";

        }

        private void bt_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
            if (!Model.md.b_004) Application.Exit();            
        }

        private void tb_PNR_KeyUp(object sender, KeyEventArgs e)
        {
            PrintHyx.PrintHyxPublic.PnrTextBoxKeyUp(tb_PNR, cb_Name, e, ref retstring);
        }
        static public string returnstring
        {
            set
            {
                if (context != null)
                {
                    string temp = "";
                    PrintHyx.PrintHyxPublic.GetRetString(ref retstring,ref temp);
                    if (temp != "") rs = temp;
                }

            }
        }
        static public string rs
        {
            set
            {
                if (context.InvokeRequired)
                {
                    EventHandler eh = new EventHandler(setcontrol);
                    PrintHyx.SinoSafe pt = PrintHyx.SinoSafe.context;
                    PrintHyx.SinoSafe.context.Invoke(eh, new object[] { pt, EventArgs.Empty });
                }
            }

        }
        static private void setcontrol(object sender, EventArgs e)
        {
            retstring = retstring.Replace('+', ' ');
            retstring = retstring.Replace('-', ' ');
            if (!EagleAPI.GetNoPnr(retstring)) return;
            context.cb_Name.Items.Clear();
            List<string> names = new List<string>();
            names = EagleAPI.GetNames(retstring);
            for (int i = 0; i < names.Count; i++)
            {
                context.cb_Name.Items.Add(names[i]);
            }
            context.cb_Name.Text = context.cb_Name.Items[0].ToString();
            context.tb_CardNo.Text = EagleAPI.GetIDCardNo(retstring)[0];
            string date = EagleAPI.GetDateStart(retstring);
            int imm = int.Parse(EagleAPI.GetMonthInt(date.Substring(date.Length - 3)));
            int idd = int.Parse(date.Substring(date.Length - 5).Substring(0, 2));
            int iyy = System.DateTime.Now.Year;
            System.DateTime dt = new DateTime(iyy, imm, idd, 23, 59, 59);
            while (dt < System.DateTime.Now)
            {
                dt = dt.AddYears(1);
            }
            context.dtp_Start.Value = dt;

            context.tbFlightNo.Text = EagleAPI.GetFlightNo(retstring) + EagleAPI.GetFlightNo2(retstring);
            context.tbFlightDate.Text = dt.ToShortDateString();
        }

        private void SinoSafe_FormClosed(object sender, FormClosedEventArgs e)
        {
            b_opened = false;
            connect_4_Command.PrintWindowOpen = false;
            if (!Model.md.b_004) Application.Exit();
        }

        private void dtp_Start_ValueChanged(object sender, EventArgs e)
        {
            dtp_End.Value = dtp_Start.Value.AddDays(7);
        }

        private void cb_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Name.SelectedItem != null)
            {
                this.tb_CardNo.Text = EagleAPI.GetIDCardNo(retstring)[cb_Name.SelectedIndex];
            }

        }

        private void bt_SetToDefault_Click(object sender, EventArgs e)
        {
            Default yd = new Default();
            yd.xmlFirst = "SinoSafe";
            if (DialogResult.OK == yd.ShowDialog())
            {
                cfg_sinosafe cfg = new cfg_sinosafe();
                cfg.GetConfig();
                eNumberHead = cfg.ENumberHead;
                this.tb_Signature.Text = cfg.Signature;
                this.numericUpDown1.Value = cfg.OffsetX;
                this.numericUpDown2.Value = cfg.OffsetY;
                this.tb_NO.Text = cfg.SaveNo;
            }
        }

        private void bt_Print_Click(object sender, EventArgs e)
        {
            save_phone_addr();
            if (tb_NO.Text.Trim().Length != 10)
            {
                MessageBox.Show("保单号长度错误，需10位");
                return;
            }
            try
            {
                long.Parse(tb_NO.Text.Trim());
            }
            catch
            {
                MessageBox.Show("保单号只能为数字");
                return;
            }
            if (cb_Name.Text.Trim() == "")
            {
                MessageBox.Show("姓名不能为空");
                return;
            }
            if (tb_CardNo.Text.Trim() == "")
            {
                MessageBox.Show("证件号码不能为空");
                return;
            }
            this.tb_RandomNo.Text = System.DateTime.Now.Year.ToString().Remove(1,1) + EagleAPI.GetRandom62();
            if (!GlobalVar.b_OffLine)
            if (cb_Name.Text != GlobalVar.HYXTESTPRINT)
            {
                HyxStructs hs = new HyxStructs();
                hs.UserID = GlobalVar.loginName;
                hs.eNumber = tb_RandomNo.Text;
                hs.IssueNumber = tb_NO.Text;
                hs.NameIssued = cb_Name.Text;
                hs.CardType = "航班号" + tbFlightNo.Text + "乘机日" + tbFlightDate.Text;
                hs.CardNumber = tb_CardNo.Text;
                hs.Remark = "B01";//5"华安交通意外伤害保险";B01
                hs.IssuePeriod = "";
                hs.IssueBegin = dtp_Start.Value.ToShortDateString() + " 00:00:00";
                hs.IssueEnd = dtp_End.Value.ToShortDateString() + " 00:00:00";
                hs.SolutionDisputed = "";
                hs.NameBeneficiary = tb_Benefit.Text;
                hs.Signature = this.tb_Signature.Text;
                hs.SignDate = dtp_Date.Value.ToShortDateString();
                hs.InssuerName = "";
                hs.Pnr = this.tb_PNR.Text;
                if (!hs.SubmitInfo())
                {
                    MessageBox.Show("数据提交失败！请检查保单号是否已被使用，或网络是否正常！");
                    return;
                }
            }

            PrintDialog pd = new PrintDialog();
            pd.Document = ptDoc;
            DialogResult dr = pd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                ptDoc.Print();
            }

        }
        private void save_phone_addr()
        {
            FileStream fs = new FileStream(GlobalVar.s_configfile, FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);
            XmlNode xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("SinoSafe");
            xn = xn.SelectSingleNode("Phone");
            xn.InnerText = tbPhone.Text;

            xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("SinoSafe");
            xn = xn.SelectSingleNode("CompanyAddr");
            xn.InnerText = this.tbCompany.Text;

            xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("SinoSafe");
            xn = xn.SelectSingleNode("OffsetX");
            xn.InnerText = numericUpDown1.Value.ToString();

            xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("SinoSafe");
            xn = xn.SelectSingleNode("OffsetY");
            xn.InnerText = numericUpDown2.Value.ToString();

            xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("SinoSafe");
            xn = xn.SelectSingleNode("Signature");
            xn.InnerText = this.tb_Signature.Text;

            xd.Save(GlobalVar.s_configfile);
        }

        private void ptDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            Font ptFontEn = new Font("system", GlobalVar.fontsize, System.Drawing.FontStyle.Regular);
            //Font ptFontCn = new Font("tec", EagleAPI.fontsizecn, System.Drawing.FontStyle.Regular);
            Brush ptBrush = Brushes.Black;
            e.PageSettings.Margins.Left = 0;
            e.PageSettings.Margins.Right = 0;
            e.PageSettings.Margins.Top = 0;
            e.PageSettings.Margins.Bottom = 0;
            PointF o = new PointF();
            o.X = float.Parse(numericUpDown1.Value.ToString());
            o.Y = float.Parse(numericUpDown2.Value.ToString());
            //打印项
            List<string> ptString = new List<string>();
            List<PointF> ptPoint = new List<PointF>();
            ptString.Add(cbInsureType.Text);
            ptPoint.Add(new PointF(53F, 34F));
            ptString.Add(cb_Name.Text);
            ptPoint.Add(new PointF(65F, 44F));
            ptString.Add(this.tb_RandomNo.Text);
            ptPoint.Add(new PointF(148F, 34F));
            ptString.Add(tb_CardNo.Text);
            ptPoint.Add(new PointF(148F, 44F));
            ptString.Add(tbInsureAmount.Text);
            ptPoint.Add(new PointF(60F, 60F));
            ptString.Add(tbFareChina.Text);
            ptPoint.Add(new PointF(152F, 64F));
            ptString.Add(tbFareNumber.Text);
            ptPoint.Add(new PointF(182F, 64F));
            ptString.Add("7天" + dtp_Start.Value.ToShortDateString() + "00时00分起 到" + dtp_End.Value.ToShortDateString() + "00时00分止");
            ptPoint.Add(new PointF(60F, 74F));
            ptString.Add(tbRelation.Text);
            ptPoint.Add(new PointF(109F, 78F));
            ptString.Add(tb_Benefit.Text);
            ptPoint.Add(new PointF(109F, 84F));
            ptString.Add(tbCompany.Text);
            ptPoint.Add(new PointF(50F, 97F));
            ptString.Add(tbPhone.Text);
            ptPoint.Add(new PointF(132F, 97F));
            ptString.Add(dtp_Date.Value.ToShortDateString());
            ptPoint.Add(new PointF(138F, 92F));
            ptString.Add(tb_Signature.Text);
            ptPoint.Add(new PointF(183F, 92F));
            ptString.Add(tbFlightNo.Text);
            ptPoint.Add(new PointF(65F, 52F));
            ptString.Add(tbFlightDate.Text);
            ptPoint.Add(new PointF(148F, 52F));
            PrintHyx.PrintHyxPublic.PrintItems(ptString.ToArray(), ptPoint.ToArray(), o, ptFontEn, ptBrush, e);


        }

        private void ptDoc_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                long it = long.Parse(tb_NO.Text.Trim()) + 1L;
                tb_NO.Text = it.ToString("D10");//10位华安交意险序号
            }
            catch
            {
                MessageBox.Show("保单号只能为数值");
            }
            FileStream fs = new FileStream(GlobalVar.s_configfile, FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);
            XmlNode xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("SinoSafe");
            xn = xn.SelectSingleNode("SaveNo");
            xn.InnerText = this.tb_NO.Text;

            xd.Save(GlobalVar.s_configfile);
        }

        private void SinoSafe_MouseClick(object sender, MouseEventArgs e)
        {
            rightMenuHYX menu = new rightMenuHYX(this);
            menu.ShowHYXMenu(this, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cb_Name.Text = GlobalVar.HYXTESTPRINT;
            this.bt_Print_Click(sender, e);
        }
    }
}