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
    public partial class DuBang01 : Form
    {
        public DuBang01()
        {
            InitializeComponent();
        }
        static public bool b_opened = false;
        static public string retstring = "";
        static public PrintHyx.DuBang01 context = null;
        string eNumberHead = "";

        private void DuBang01_Load(object sender, EventArgs e)
        {
            throw new Exception("Unknown Error!");
            cfg_dubang01 cc = new cfg_dubang01();
            cc.GetConfig();

            this.tbSignature.Text = cc.Signature;
            this.numericUpDown1.Value = cc.OffsetX;
            this.numericUpDown2.Value = cc.OffsetY;
            this.tbNo.Text = cc.SaveNo;
            this.tbPrintTime.Text = System.DateTime.Now.ToShortDateString();
            this.eNumberHead = cc.ENumberHead;
            retstring = "";
            b_opened = true;
            connect_4_Command.PrintWindowOpen = true;
            context = this;

            if (tbNo.Text.Trim() == "") tbNo.Text = "11009407";
        }

        private void bt_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
            if (!Model.md.b_004) Application.Exit();            

        }

        private void tbPnr_KeyUp(object sender, KeyEventArgs e)
        {
            PrintHyx.PrintHyxPublic.PnrTextBoxKeyUp(tbPnr, cbName, e, ref retstring);
        }
        static public string returnstring
        {
            set
            {
                if (context != null)
                {
                    string temp = "";
                    PrintHyx.PrintHyxPublic.GetRetString(ref retstring, ref temp);
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
                    PrintHyx.DuBang01 pt = PrintHyx.DuBang01.context;
                    PrintHyx.DuBang01.context.Invoke(eh, new object[] { pt, EventArgs.Empty });
                }
            }

        }
        static private void setcontrol(object sender, EventArgs e)
        {
            retstring = retstring.Replace('+', ' ');
            retstring = retstring.Replace('-', ' ');
            if (!EagleAPI.GetNoPnr(retstring)) return;
            context.cbName.Items.Clear();
            List<string> names = new List<string>();
            names = EagleAPI.GetNames(retstring);
            for (int i = 0; i < names.Count; i++)
            {
                context.cbName.Items.Add(names[i]);
            }
            context.cbName.Text = context.cbName.Items[0].ToString();
            context.tbCardID.Text = EagleAPI.GetIDCardNo(retstring)[0];
            string date = EagleAPI.GetDateStart(retstring);
            int imm = int.Parse(EagleAPI.GetMonthInt(date.Substring(date.Length - 3)));
            int idd = int.Parse(date.Substring(date.Length - 5).Substring(0, 2));
            int iyy = System.DateTime.Now.Year;
            System.DateTime dt = new DateTime(iyy, imm, idd, 23, 59, 59);
            while (dt < System.DateTime.Now)
            {
                dt = dt.AddYears(1);
            }
            //context.dtp_Start.Value = dt;

            context.tbFlightNo.Text = EagleAPI.GetFlightNo(retstring) + EagleAPI.GetFlightNo2(retstring);
            context.tbDate.Text = dt.ToShortDateString();
        }

        private void DuBang01_FormClosed(object sender, FormClosedEventArgs e)
        {
            b_opened = false;
            connect_4_Command.PrintWindowOpen = false;
            if (!Model.md.b_004) Application.Exit();

        }

        private void cbName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbName.SelectedItem != null)
            {
                this.tbCardID.Text = EagleAPI.GetIDCardNo(retstring)[cbName.SelectedIndex];
            }

        }

        private void bt_SetToDefault_Click(object sender, EventArgs e)
        {
            Default yd = new Default();
            yd.xmlFirst = "DuBang01";
            if (DialogResult.OK == yd.ShowDialog())
            {
                cfg_dubang01 cfg = new cfg_dubang01();
                cfg.GetConfig();
                eNumberHead = cfg.ENumberHead;
                this.tbSignature.Text = cfg.Signature;
                this.numericUpDown1.Value = cfg.OffsetX;
                this.numericUpDown2.Value = cfg.OffsetY;
                this.tbNo.Text = cfg.SaveNo;
            }

        }

        private void bt_Print_Click(object sender, EventArgs e)
        {
            save_phone();
            if (tbNo.Text.Trim().Length != 16)
            {
                MessageBox.Show("保单号长度错误");
                return;
            }
            try
            {
                long.Parse(tbNo.Text.Trim());
            }
            catch
            {
                MessageBox.Show("保单号只能为数字");
                return;
            }
            if (cbName.Text.Trim() == "")
            {
                MessageBox.Show("姓名不能为空");
                return;
            }
            if (tbCardID.Text.Trim() == "")
            {
                MessageBox.Show("证件号码不能为空");
                return;
            }
            
            this.tbPolicyNo.Text = this.eNumberHead + "0" + tbNo.Text.Trim().Substring(8, 8);
            if (!GlobalVar.b_OffLine)
            {
                if (cbName.Text.Trim() != GlobalVar.HYXTESTPRINT)
                {
                    HyxStructs hs = new HyxStructs();
                    hs.UserID = GlobalVar.loginName;
                    hs.eNumber = tbPolicyNo.Text;
                    hs.IssueNumber = tbNo.Text;
                    hs.NameIssued = cbName.Text;
                    hs.CardType = "航班号" + tbFlightNo.Text + "乘机日" + tbDate.Text; ;
                    hs.CardNumber = tbCardID.Text;
                    hs.Remark = "B03"; //7"都险航翼网";B03
                    hs.IssuePeriod = "";
                    hs.IssueBegin = System.DateTime.Now.ToShortDateString();// dtp_Start.Value.ToShortDateString() + " 00:00:00";
                    hs.IssueEnd = System.DateTime.Now.ToShortDateString();// dtp_End.Value.ToShortDateString() + " 00:00:00";
                    hs.SolutionDisputed = "";
                    hs.NameBeneficiary = this.tbBeneficiary.Text;
                    hs.Signature = tbSignature.Text;// this.tbSignatureDate.Text;
                    hs.SignDate = this.tbPrintTime.Text;//dtp_Date.Value.ToShortDateString();
                    hs.InssuerName = "";
                    hs.Pnr = this.tbPnr.Text;
                    if (!hs.SubmitInfo())
                    {
                        MessageBox.Show("数据提交失败！请检查保单号是否已被使用，或网络是否正常！");
                        return;
                    }
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
        void save_phone()
        {
            FileStream fs = new FileStream(GlobalVar.s_configfile, FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);
            XmlNode xn = xd.SelectSingleNode("eg");
            //xn = xn.SelectSingleNode("DuBang01");
            //xn = xn.SelectSingleNode("Phone");
            //xn.InnerText = tbPhone.Text;

            //xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("DuBang01");
            xn = xn.SelectSingleNode("OffsetX");
            xn.InnerText = numericUpDown1.Value.ToString();

            xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("DuBang01");
            xn = xn.SelectSingleNode("OffsetY");
            xn.InnerText = numericUpDown2.Value.ToString();

            xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("DuBang01");
            xn = xn.SelectSingleNode("Signature");
            xn.InnerText = this.tbSignature.Text;

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
            ptString.Add(tbPolicyNo.Text); ptPoint.Add(new PointF(55F, 26F));
            ptString.Add(cbName.Text); ptPoint.Add(new PointF(55F, 36F));
            ptString.Add(tbCardID.Text); ptPoint.Add(new PointF(152F, 36F));
            ptString.Add(tbFlightNo.Text); ptPoint.Add(new PointF(55F, 48F));
            ptString.Add(tbDate.Text); ptPoint.Add(new PointF(152F, 48F));
            ptString.Add(tbRelationShip.Text); ptPoint.Add(new PointF(55F, 73F));
            ptString.Add(tbBeneficiary.Text); ptPoint.Add(new PointF(55F, 66F));
            ptString.Add(tbSignature.Text); ptPoint.Add(new PointF(55F, 86F));
            ptString.Add(tbPrintTime.Text); ptPoint.Add(new PointF(174F, 86F));


            PrintHyx.PrintHyxPublic.PrintItems(ptString.ToArray(), ptPoint.ToArray(), o, ptFontEn, ptBrush, e);

        }

        private void ptDoc_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                long it = long.Parse(tbNo.Text.Trim()) + 1L;
                tbNo.Text = it.ToString("D16");//8位航空旅客人身意外伤害保险单
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
            xn = xn.SelectSingleNode("DuBang01");
            xn = xn.SelectSingleNode("SaveNo");
            xn.InnerText = this.tbNo.Text;

            xd.Save(GlobalVar.s_configfile);

        }

        private void DuBang01_MouseClick(object sender, MouseEventArgs e)
        {
            rightMenuHYX menu = new rightMenuHYX(this);
            menu.ShowHYXMenu(this, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cbName.Text = GlobalVar.HYXTESTPRINT ;
            bt_Print_Click(sender, e);
        }

    }
}