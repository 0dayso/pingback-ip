#define newHYX
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


using System.Data.SqlClient;
using System.Xml;
using gs.para;
using System.IO;


namespace ePlus.PrintHyx
{
    public partial class PrintPICC2 : Form
    {
        public PrintPICC2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //this.Dispose();
            //PrintPICC pp = new PrintPICC();
            //pp.Show();

        }

        private void bt_Exit_Click(object sender, EventArgs e)
        {
            //conn.Close();
            //Application.Exit();
            connect_4_Command.PrintWindowOpen = false;
            this.Close();
        }
        private picc2 GetConfig()
        {
            FileStream fs = new FileStream(GlobalVar.s_configfile, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            picc2 ret = new picc2();
            NewPara np = new NewPara(temp);
           
            ret.Signature = np.FindTextByPath("//eg/PrintPICC2/Signature");
            ret.Conflict = np.FindTextByPath("//eg/PrintPICC2/Conflict");
            ret.OffsetX = int.Parse(np.FindTextByPath("//eg/PrintPICC2/OffsetX"));
            ret.OffsetY = int.Parse(np.FindTextByPath("//eg/PrintPICC2/OffsetY"));
            ret.SaveNo = np.FindTextByPath("//eg/PrintPICC2/SaveNo");
            return ret;
        }
        picc2 pi;
        private void PrintPICC2_Load(object sender, EventArgs e)
        {
            throw new Exception("Unknown Error!");
            DateTime dt = new DateTime();
            dt = System.DateTime.Now;
            dtp_Start.Value = dt;
            dtp_Date.Value = dt;
            dtp_End.Value = dt.AddDays(6);

            pi = GetConfig();

            tb_Signature.Text = pi.Signature;
            cb_Method.Text = pi.Conflict;
            numericUpDown1.Value = pi.OffsetX;
            numericUpDown2.Value = pi.OffsetY;
            tb_NO.Text = pi.SaveNo;

            retstring = "";
            b_opened = true;
            connect_4_Command.PrintWindowOpen = true;
            context = this;

            this.button1.Visible = false;

            //try { conn.Open(); }
            //catch
            //{
            //    MessageBox.Show("数据库连接失败！");
            //    Application.Exit();
            //}

        }
        //SqlConnection conn = new SqlConnection("server=61.183.220.162,2433;database=HYX;uid=sa;pwd=sa;");
        static public PrintHyx.PrintPICC2 context = null;
        static public bool b_opened = false;
        static public string retstring = "";

        private void tb_PNR_KeyUp(object sender, KeyEventArgs e)
        {
            
            if (e.KeyValue == 13)//回车
            {
                this.tb_PNR.Text = this.tb_PNR.Text.ToUpper();
                retstring = "";
                if (tb_PNR.Text.Trim().Length != 5) return;

                EagleAPI.CLEARCMDLIST(3);

                cb_Name.Items.Clear();
                cb_Name.Text = "请稍等……";

                EagleAPI.EagleSendCmd("rT:n/" + tb_PNR.Text.Trim());
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
                    PrintHyx.PrintPICC2 pt = PrintHyx.PrintPICC2.context;
                    PrintHyx.PrintPICC2.context.Invoke(eh, new object[] { pt, EventArgs.Empty });
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
            while (dt.AddDays(1) < System.DateTime.Now)
            {
                dt = dt.AddYears(1);
            }
            context.dtp_Start.Value = dt;
        }

        private void bt_SetToDefault_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(GlobalVar.s_configfile, FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);
            XmlNode xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("PrintPICC2");
            xn = xn.SelectSingleNode("Signature");
            xn.InnerText = tb_Signature.Text;

            xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("PrintPICC2");
            xn = xn.SelectSingleNode("Conflict");
            xn.InnerText = cb_Method.Text;

            xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("PrintPICC2");
            xn = xn.SelectSingleNode("OffsetX");
            xn.InnerText = numericUpDown1.Value.ToString();

            xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("PrintPICC2");
            xn = xn.SelectSingleNode("OffsetY");
            xn.InnerText = numericUpDown2.Value.ToString();

            xd.Save(GlobalVar.s_configfile);
        }

        private void bt_Print_Click(object sender, EventArgs e)
        {
            #region 判断是否符合打印条件
            if (!EagleAPI.PrinterSetup(ptDoc)) return;
            //if (tb_PNR.Text.Trim().Length != 5)
            //{
            //    MessageBox.Show("订座记录号错误");
            //    return;
            //}
            //任我游为10位数
            if (tb_NO.Text.Trim().Length != 10)
            {
                MessageBox.Show("保单号长度错误");
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
            //if (tb_Signature.Text.Trim() == "")
            //{
            //    MessageBox.Show("经办人不能为空");
            //    return;
            //}
            //if (tb_Insuror.Text.Trim() == "")
            //{
            //    MessageBox.Show("保险人不能为空");
            //    return;
            //}
            #endregion

            //6+凑足八位
            this.tb_RandomNo.Text = "PEED2006420105"+System.DateTime.Now.Month.ToString("x")+EagleAPI.GetRandom62();

#if newHYX
            if (!GlobalVar.b_OffLine)
            if (cb_Name.Text != GlobalVar.HYXTESTPRINT)
            {
                HyxStructs hs = new HyxStructs();
                hs.UserID = GlobalVar.loginName;
                hs.eNumber = tb_RandomNo.Text;
                hs.IssueNumber = tb_NO.Text;
                hs.NameIssued = cb_Name.Text;
                hs.CardType = "";
                hs.CardNumber = tb_CardNo.Text;
                hs.Remark = "001";//"中国人保财险-任我游";001
                hs.IssuePeriod = "7天";
                hs.IssueBegin = dtp_Start.Value.ToShortDateString() + " 00:00:00";
                hs.IssueEnd = dtp_End.Value.ToShortDateString() + " 23:59:59";
                hs.SolutionDisputed = cb_Method.Text;
                hs.NameBeneficiary = tb_Benefit.Text;
                hs.Signature = this.tb_Signature.Text;
                hs.SignDate = dtp_Date.Value.ToShortDateString();
                hs.Pnr = this.tb_PNR.Text;
                if (!hs.SubmitInfo())
                {
                    MessageBox.Show("数据提交失败！请检查保单号是否已被使用，或网络是否正常！");
                    return;
                }
            }
#else
            #region 上传数据
            if (conn.State != ConnectionState.Connecting)
            {
                conn.Close();
                try
                {
                    conn.Open();
                }
                catch
                {
                    MessageBox.Show("数据库连接失败！！");
                    Application.Exit();
                }
            }

            string cmdstring = "insert t_hyx (UserID,InsuranceName,PNR,[NO],Policy,IssuedName,IDCard,FlightNo,";
            cmdstring += "FlyDate,EffictiveTime,EffictiveStart,EffictiveEnd,Beneficiary,BeneficiaryInfo,IssuedDate,";
            cmdstring += "IssuedBy) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}')";
            cmdstring = string.Format(cmdstring, GlobalVar.loginName, "中国人保财险-任我游", tb_PNR.Text, tb_RandomNo.Text, tb_NO.Text,
               cb_Name.Text, this.tb_CardNo.Text, "", "", "7天",
               dtp_Start.Value.ToShortDateString()+" 00:00:00",
               dtp_End.Value.ToShortDateString()+" 24:00:00",
               "", "", dtp_Date.Value.ToString(), tb_Signature.Text);
            try
            {
                SqlCommand cmd = new SqlCommand(cmdstring, conn);
                if (cmd.ExecuteNonQuery() != 1)
                {
                    MessageBox.Show("数据上传失败！");
                    return;
                }

            }
            catch
            {
                MessageBox.Show("请正确输入！");
                return;
            }
            #endregion
#endif
            #region 打印
            PrintDialog pd = new PrintDialog();
            pd.Document = ptDoc;
            DialogResult dr = pd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                ptDoc.Print();
            }
            #endregion
        }

        private void ptDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            Font ptFontEn = new Font("system", 9, System.Drawing.FontStyle.Regular);
            //Font ptFontCn = new Font("tec", EagleAPI.fontsizecn, System.Drawing.FontStyle.Regular);
            Brush ptBrush = Brushes.Black;
            e.PageSettings.Margins.Left = 0;
            e.PageSettings.Margins.Right = 0;
            e.PageSettings.Margins.Top = 0;
            e.PageSettings.Margins.Bottom = 0;
            PointF o = new PointF();
            o.X = float.Parse(numericUpDown1.Value.ToString());
            o.Y = float.Parse(numericUpDown2.Value.ToString());

            //1.被保人姓名
            e.Graphics.DrawString(tb_RandomNo.Text, ptFontEn, ptBrush, 55F + o.X, 20F + o.Y);
            e.Graphics.DrawString(cb_Name.Text, ptFontEn, ptBrush, 52F + o.X, 29.3F + o.Y);
            e.Graphics.DrawString(tb_CardNo.Text, ptFontEn, ptBrush, 133F + o.X, 29.3F + o.Y);
            e.Graphics.DrawString(dtp_Start.Value.Year.ToString(), ptFontEn, ptBrush, 62F + o.X, 57.4F + o.Y);
            e.Graphics.DrawString(dtp_Start.Value.Month.ToString(), ptFontEn, ptBrush, 80F + o.X, 57.4F + o.Y);
            e.Graphics.DrawString(dtp_Start.Value.Day.ToString(), ptFontEn, ptBrush, 92.8F + o.X, 57.4F + o.Y);
            e.Graphics.DrawString(dtp_End.Value.Year.ToString(), ptFontEn, ptBrush, 118F + o.X, 57.4F + o.Y);
            e.Graphics.DrawString(dtp_End.Value.Month.ToString(), ptFontEn, ptBrush, 136F + o.X, 57.4F + o.Y);
            e.Graphics.DrawString(dtp_End.Value.Day.ToString(), ptFontEn, ptBrush, 148F + o.X, 57.4F + o.Y);
            e.Graphics.DrawString(cb_Method.Text, ptFontEn, ptBrush, 52F + o.X, 62.8F + o.Y);
            e.Graphics.DrawString(tb_Benefit.Text, ptFontEn, ptBrush, 102F + o.X, 68.2F + o.Y);
            e.Graphics.DrawString(tb_Signature.Text, ptFontEn, ptBrush, 40F + o.X, 92F + o.Y);
            e.Graphics.DrawString(tb_Insuror.Text, ptFontEn, ptBrush, 180F + o.X, 86.3F + o.Y);
            e.Graphics.DrawString(dtp_Date.Value.Year.ToString(), ptFontEn, ptBrush, 115F + o.X, 92F + o.Y);
            e.Graphics.DrawString(dtp_Date.Value.Month.ToString(), ptFontEn, ptBrush, 132F + o.X, 92F + o.Y);
            e.Graphics.DrawString(dtp_Date.Value.Day.ToString(), ptFontEn, ptBrush, 145F + o.X, 92F + o.Y);
        }

        private void dtp_Start_ValueChanged(object sender, EventArgs e)
        {
            dtp_End.Value = dtp_Start.Value.AddDays(6);
        }

        private void ptDoc_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //打印后，保单号+1，并保存至配置文件中
            try
            {
                long it = long.Parse(tb_NO.Text.Trim()) + 1L;
                tb_NO.Text = it.ToString("D10");//10位权益告知单号
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
            xn = xn.SelectSingleNode("PrintPICC2");
            xn = xn.SelectSingleNode("SaveNo");
            xn.InnerText = this.tb_NO.Text;

            xd.Save(GlobalVar.s_configfile);
        }

        private void PrintPICC2_FormClosed(object sender, FormClosedEventArgs e)
        {
            b_opened = false;
            connect_4_Command.PrintWindowOpen = false;

            if (!Model.md.b_004) Application.Exit();
        }

        private void cb_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Name.SelectedItem != null)
            {
                tb_CardNo.Text = EagleAPI.GetIDCardNo(retstring)[cb_Name.SelectedIndex];
            }
        }

        private void PrintPICC2_MouseClick(object sender, MouseEventArgs e)
        {
            rightMenuHYX menu = new rightMenuHYX(this);
            menu.ShowHYXMenu(this, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cb_Name.Text = GlobalVar.HYXTESTPRINT;
            this.bt_Print_Click(sender, e);
        }
    }
}