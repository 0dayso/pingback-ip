using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ePlus.PrintHyx
{
    public partial class Yongan : Form
    {
        public Yongan()
        {
            InitializeComponent();
        }
        public string eNumberHead;
        static public bool b_opened = false;
        static public string retstring = "";
        static public PrintHyx.Yongan context = null;
        private void Yongan_Load(object sender, EventArgs e)
        {
            throw new Exception("Unknown Error!");
            LogoPicture.pictures pic = new LogoPicture.pictures();
            this.pictureBox1.Image = pic.pictureBox5.Image;
            //1初始化时间
            dtpSignDate.Value = System.DateTime.Now;
            dtpBegin.Value = System.DateTime.Now;
            dtpEnd.Value = System.DateTime.Now.AddDays(6);

            //2读配置:保单号YA2006WUH,经办人,打印边距,序号
            cfg_yongan cfg = new cfg_yongan();
            cfg.GetConfig();
            eNumberHead = cfg.eNumberHead;
            this.tbSignature.Text = cfg.Signature;
            this.numericUpDownX.Value = cfg.OffsetX;
            this.numericUpDownY.Value = cfg.OffsetY;
            this.tbInsuranceNo.Text = cfg.SaveNo;
            //3初始化全局变量
            retstring = "";
            b_opened = true;
            connect_4_Command.PrintWindowOpen = true;
            context = this;
            this.bt_Print.Enabled = false;
        }

        private void tbPnr_KeyUp(object sender, KeyEventArgs e)
        {
            
            if (e.KeyValue == 13)//回车
            {
                this.tbPnr.Text = this.tbPnr.Text.ToUpper();
                retstring = "";
                if (tbPnr.Text.Trim().Length != 5) return;

                EagleAPI.CLEARCMDLIST(3);

                cbName.Items.Clear();
                cbName.Text = "请稍等……";

                EagleAPI.EagleSendCmd("rT:n/" + tbPnr.Text.Trim());
            }
        }
        static public string returnstring
        {
            set
            {
                if (context != null)
                {

                    //追加结果
                    retstring += "\n" + connect_4_Command.AV_String;
                    //no pnr
                    if (retstring.Split('\n').Length < 4)
                    {
                        MessageBox.Show("NO PNR");
                        return;
                    }
                    //多页
                    string plustemp = mystring.trim(retstring);
                    if (plustemp[plustemp.Length - 1] == '+' && retstring.IndexOf("*THIS PNR WAS ENTIRELY CANCELLED*") < 0)

                    {
                        //发送PN指令
                        retstring = retstring.Replace('+', ' ');
                        if (frmMain.st_tabControl.InvokeRequired)
                        {
                            EventHandler eh = new EventHandler(PrintTicket.sendpn);
                            TabControl tc = frmMain.st_tabControl;
                            frmMain.st_tabControl.Invoke(eh, new object[] { tc, EventArgs.Empty });
                        }
                    }
                    //if (retstring.Substring(retstring.Length - 3) != "+\r\n") rs = retstring;
                    else rs = retstring;
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
                    PrintHyx.Yongan pt = PrintHyx.Yongan.context;
                    PrintHyx.Yongan.context.Invoke(eh, new object[] { pt, EventArgs.Empty });
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
            //System.DateTime dt = new DateTime(iyy, imm, idd, 23, 59, 59);
            System.DateTime dt = new DateTime(iyy, imm, idd, 23, 59, 59);
            while (dt < System.DateTime.Now)
            {
                dt = dt.AddYears(1);
            }
            context.dtpBegin.Value = dt;
        }

        private void cbName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbName.SelectedItem != null)
            {
                tbCardID.Text = EagleAPI.GetIDCardNo(retstring)[cbName.SelectedIndex];
            }
        }

        private void bt_SetToDefault_Click(object sender, EventArgs e)
        {
            Default yd = new Default();
            yd.xmlFirst = "YongAn";
            if (DialogResult.OK == yd.ShowDialog())
            {
                cfg_yongan cfg = new cfg_yongan();
                cfg.GetConfig();
                eNumberHead = cfg.eNumberHead;
                this.tbSignature.Text = cfg.Signature;
                this.numericUpDownX.Value = cfg.OffsetX;
                this.numericUpDownY.Value = cfg.OffsetY;
                this.tbInsuranceNo.Text = cfg.SaveNo;
            }
        }



        private void btSubmit_Click(object sender, EventArgs e)
        {
            if (tbInsuranceNo.Text.Trim().Length != 7)
            {
                MessageBox.Show("右上角保单号有误，应为七位数字");
                return;
            }
            try
            {
                long.Parse(tbInsuranceNo.Text.Trim());
            }
            catch
            {
                MessageBox.Show("右上角保单号有误，应为七位数字");
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

            this.tbENumber.Text = eNumberHead + EagleAPI.GetRandom62(10000L);
            if (!GlobalVar.b_OffLine)
            if (cbName.Text != GlobalVar.HYXTESTPRINT)
            {
                HyxStructs hs = new HyxStructs();
                hs.UserID = GlobalVar.loginName;
                hs.eNumber = tbENumber.Text;
                hs.IssueNumber = tbInsuranceNo.Text;
                hs.NameIssued = cbName.Text;
                hs.CardType = "";
                hs.CardNumber = tbCardID.Text;
                hs.Remark = "007";//3永安交意险007
                hs.IssuePeriod = "7天";
                hs.IssueBegin = this.dtpBegin.Value.ToShortDateString() + " 00:00:00";
                hs.IssueEnd = this.dtpEnd.Value.ToShortDateString() + " 23:59:59";

                hs.NameBeneficiary = this.tbBeneficiary.Text;
                hs.Signature = this.tbSignature.Text;
                hs.SignDate = this.dtpSignDate.Value.ToString();
                hs.Pnr = this.tbPnr.Text;
                if (!hs.SubmitInfo())
                {
                    MessageBox.Show("数据提交失败！请检查保单号及是否分配或网络是否正常！");
                    return;
                }
            }
            MessageBox.Show("上传成功");
            this.bt_Print.Enabled = true;
        }
        private void bt_Print_Click(object sender, EventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            pd.Document = ptDoc;
            DialogResult dr = pd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                ptDoc.Print();
            }
            this.bt_Print.Enabled = false;
        }

        private void ptDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            Font ptFontEn = new Font("System", GlobalVar.fontsize, System.Drawing.FontStyle.Regular);
            //Font ptFontCn = new Font("tec", EagleAPI.fontsizecn, System.Drawing.FontStyle.Regular);
            Brush ptBrush = Brushes.Black;
            e.PageSettings.Margins.Left = 0;
            e.PageSettings.Margins.Right = 0;
            e.PageSettings.Margins.Top = 0;
            e.PageSettings.Margins.Bottom = 0;
            PointF o = new PointF();
            o.X = float.Parse(numericUpDownX.Value.ToString());
            o.Y = float.Parse(numericUpDownY.Value.ToString());

            //1.被保人姓名
            e.Graphics.DrawString(this.tbENumber.Text, ptFontEn, ptBrush, 32.8F + o.X, 22.8F + o.Y);
            e.Graphics.DrawString(cbName.Text, ptFontEn, ptBrush, 47.8F + o.X, 31.4F + o.Y);
            e.Graphics.DrawString(tbCardID.Text, ptFontEn, ptBrush, 126.2F + o.X, 31.4F + o.Y);
            e.Graphics.DrawString(this.dtpBegin.Value.ToShortDateString(), ptFontEn, ptBrush, 60F + o.X, 58.2F + o.Y);
            e.Graphics.DrawString(this.dtpEnd.Value.ToShortDateString(), ptFontEn, ptBrush, 93.2F + o.X, 58.2F + o.Y);

            
            e.Graphics.DrawString(this.tbBeneficiary.Text, ptFontEn, ptBrush, 111.8F + o.X, 67.7F + o.Y);
            e.Graphics.DrawString(this.tbSignature.Text, ptFontEn, ptBrush, 100F + o.X, 87F + o.Y);
            e.Graphics.DrawString(dtpSignDate.Value.Year.ToString(), ptFontEn, ptBrush, 128.8F + o.X, 22.8F + o.Y);
            e.Graphics.DrawString(dtpSignDate.Value.Month.ToString(), ptFontEn, ptBrush, 143.5F + o.X, 22.8F + o.Y);
            e.Graphics.DrawString(dtpSignDate.Value.Day.ToString(), ptFontEn, ptBrush, 150.9F + o.X, 22.8F + o.Y);
            e.Graphics.DrawString(dtpSignDate.Value.Hour.ToString(), ptFontEn, ptBrush, 158.2F + o.X, 22.8F + o.Y);
            e.Graphics.DrawString(dtpSignDate.Value.Minute.ToString(), ptFontEn, ptBrush, 165.9F + o.X, 22.8F + o.Y);
        }

        private void bt_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Yongan_FormClosed(object sender, FormClosedEventArgs e)
        {
            b_opened = false;
            connect_4_Command.PrintWindowOpen = false;
            if (!Model.md.b_004) Application.Exit();
        }

        private void dtpBegin_ValueChanged(object sender, EventArgs e)
        {
            dtpEnd.Value = dtpBegin.Value.AddDays(6);
        }

        private void Yongan_MouseClick(object sender, MouseEventArgs e)
        {
            rightMenuHYX menu = new rightMenuHYX(this);
            menu.ShowHYXMenu(this, e);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.cbName.Text = GlobalVar.HYXTESTPRINT;
            this.bt_Print_Click(sender, e);
        }

    }
}