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
    public partial class PrintPICC : Form
    {
        public PrintPICC()
        {
            InitializeComponent();
        }

        private picc GetConfig()
        {
            FileStream fs = new FileStream(GlobalVar.s_configfile, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            picc ret = new picc();
            NewPara np = new NewPara(temp);
            ret.CardType = np.FindTextByPath("//eg/PrintPICC/CardType");
            ret.Signature = np.FindTextByPath("//eg/PrintPICC/Signature");
            ret.OffsetX = int.Parse(np.FindTextByPath("//eg/PrintPICC/OffsetX"));
            ret.OffsetY = int.Parse(np.FindTextByPath("//eg/PrintPICC/OffsetY"));
            ret.SaveNo = np.FindTextByPath("//eg/PrintPICC/SaveNo");

            return ret;
        }
        private picc pi;
        private void PrintPICC_Load(object sender, EventArgs e)
        {
            throw new Exception("Unknown Error!");
            DateTime dt = new DateTime();
            dt = System.DateTime.Now;

            dtp1.Value = dt;
            dtp_Date.Value = dt;
            dtp2.Value = dt.AddDays(2);

            pi = GetConfig();
            tb_Signture.Text = pi.Signature;
            cb_cardType.Text = pi.CardType;
            numericUpDown1.Value = pi.OffsetX;
            numericUpDown2.Value = pi.OffsetY;
            tb_No.Text = pi.SaveNo;

            retstring = "";
            b_opened = true;
            connect_4_Command.PrintWindowOpen = true;
            context = this;
            this.ActiveControl = this.tb_eNumber; 
            
            //try { conn.Open(); }
            //catch
            //{
            //    MessageBox.Show("数据库连接失败！");
            //    Application.Exit();
            //}
        }

        //SqlConnection conn = new SqlConnection("server=61.183.220.162,2433;database=HYX;uid=sa;pwd=sa;");
        static public PrintHyx.PrintPICC context = null;
        static public bool b_opened = false;
        static public string retstring = "";
        private void tb_eNumber_KeyUp(object sender, KeyEventArgs e)
        {
            
            if (e.KeyValue == 13)//回车
            {
                this.tb_eNumber.Text = this.tb_eNumber.Text.ToUpper();
                retstring = "";

                EagleAPI.CLEARCMDLIST(3);

                cb_name.Items.Clear();
                cb_name.Text = "请稍等……";
                EagleAPI.EagleSendCmd("rT:n/" + tb_eNumber.Text.Trim());
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
                    PrintHyx.PrintPICC pt = PrintHyx.PrintPICC.context;
                    PrintHyx.PrintPICC.context.Invoke(eh, new object[] { pt, EventArgs.Empty });
                }
            }

        }
        static private void setcontrol(object sender, EventArgs e)
        {
            retstring = retstring.Replace('+', ' ');
            retstring = retstring.Replace('-', ' ');
            if (!EagleAPI.GetNoPnr(retstring)) return;
            context.cb_name.Items.Clear();
            List<string> names = new List<string>();
            names = EagleAPI.GetNames(retstring);
            for (int i = 0; i < names.Count; i++)
            {
                context.cb_name.Items.Add(names[i]);
            }
            context.cb_name.Text = context.cb_name.Items[0].ToString();
            context.tb_cardNo.Text = EagleAPI.GetIDCardNo(retstring)[0];
            string date = EagleAPI.GetDateStart(retstring);
            int imm = int.Parse(EagleAPI.GetMonthInt(date.Substring(date.Length - 3)));
            int idd = int.Parse(date.Substring(date.Length - 5).Substring(0, 2));
            int iyy = System.DateTime.Now.Year;
            System.DateTime dt = new DateTime(iyy, imm, idd, 23, 59, 59);
            while (dt < System.DateTime.Now)
            {
                dt = dt.AddYears(1);
            }
            context.dtp1.Value = dt;
        }

        private void cb_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_name.SelectedItem != null)
            {
                tb_cardNo.Text = EagleAPI.GetIDCardNo(retstring)[cb_name.SelectedIndex];
            }
        }

        private void bt_SetToDefault_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(GlobalVar.s_configfile, FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            XmlDocument xd = new XmlDocument ();
            xd.LoadXml(temp);
            XmlNode xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("PrintPICC");
            xn = xn.SelectSingleNode("CardType");
            xn.InnerText = cb_cardType.Text;

            xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("PrintPICC");
            xn = xn.SelectSingleNode("Signature");
            xn.InnerText = tb_Signture.Text;

            xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("PrintPICC");
            xn = xn.SelectSingleNode("OffsetX");
            xn.InnerText = numericUpDown1.Value.ToString();

            xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("PrintPICC");
            xn = xn.SelectSingleNode("OffsetY");
            xn.InnerText = numericUpDown2.Value.ToString();

            xd.Save(GlobalVar.s_configfile);

        }

        private void bt_Print_Click(object sender, EventArgs e)
        {
            #region 判断是否符合打印条件
            if (!EagleAPI.PrinterSetup(ptDoc)) return;



            //if (tb_eNumber.Text.Trim().Length != 5)
            //{
            //    MessageBox.Show("订座记录号错误");
            //    return;
            //}
            //权益告知单7位数
            if (tb_No.Text.Trim().Length != 7)
            {
                MessageBox.Show("保单号错误");
                return;
            }
            try
            {
                int.Parse(tb_No.Text.Trim());
            }
            catch
            {
                MessageBox.Show("保单号只能为数字");
                return;
            }
            if(cb_name.Text.Trim() =="")
            {
                MessageBox.Show("姓名不能为空");
                return;
            }
            //if (cb_cardType.Text.Trim() == "")
            //{
            //    MessageBox.Show("证件类型不能为空");
            //    return;
            //}
            if (tb_cardNo.Text.Trim() == "")
            {
                MessageBox.Show("证件号码不能为空");
                return;
            }
            //if (tb_Signture.Text.Trim() == "")
            //{
            //    MessageBox.Show("经办人不能为空");
            //    return;
            //}
            #endregion


            //告知单为7位
#if newHYX
            this.tb_RandomNo.Text ="PEED2006YCX"+ EagleAPI.GetRandom62();

            HyxStructs hs = new HyxStructs();
            hs.UserID = GlobalVar.loginName;
            hs.eNumber = tb_RandomNo.Text;
            hs.IssueNumber = tb_No.Text;
            hs.NameIssued = cb_name.Text;
            hs.CardType = cb_cardType.Text;
            hs.CardNumber = tb_cardNo.Text;
            hs.Remark = "002";// "中国人保财险-告知单";002
            hs.IssuePeriod = "3天";
            hs.IssueBegin = dtp1.Value.ToShortDateString() + " 00:00:00";
            hs.IssueEnd = dtp2.Value.ToShortDateString() + " 23:59:59";
            hs.SolutionDisputed = "";
            hs.NameBeneficiary = "";
            hs.Signature = tb_Signture.Text;
            hs.SignDate = dtp_Date.Value.ToShortDateString();
            hs.Pnr = this.tb_eNumber.Text;
            if (!hs.SubmitInfo())
            {
                MessageBox.Show("数据提交失败！请检查保单号是否已被使用或网络是否正常！");
                return;
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
            cmdstring = string.Format(cmdstring, GlobalVar.loginName, "中国人保财险-告知单", tb_eNumber.Text,tb_RandomNo.Text, tb_No.Text,
               cb_name.Text,this.tb_cardNo.Text, "", "", "3天",
               dtp1.Value.ToShortDateString() + " 00:00:00",
               dtp2.Value.ToShortDateString() + " 24:00:00",
               "","", dtp_Date.Value.ToShortDateString(), tb_Signture.Text);
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
            PointF o = new PointF ();
            o.X = float.Parse(numericUpDown1.Value.ToString());
            o.Y = float.Parse(numericUpDown2.Value.ToString());
            
            //1.电子单号
            e.Graphics.DrawString(tb_RandomNo.Text, ptFontEn, ptBrush, 95F + o.X, 42F + o.Y);
            //2.姓名
            e.Graphics.DrawString(cb_name.Text, ptFontEn, ptBrush, 50F + o.X, 51F + o.Y);
            //3.证件种类
            e.Graphics.DrawString(cb_cardType.Text, ptFontEn, ptBrush, 99F + o.X, 51F + o.Y);
            //4.证件号码
            e.Graphics.DrawString(tb_cardNo.Text, ptFontEn, ptBrush, 148F + o.X, 51F + o.Y);
            //5.起始年月日
            e.Graphics.DrawString(dtp1.Value.Year.ToString(), ptFontEn, ptBrush, 109F + o.X, 84F + o.Y);
            e.Graphics.DrawString(dtp1.Value.Month.ToString(), ptFontEn, ptBrush, 123.5F + o.X, 84F + o.Y);
            e.Graphics.DrawString(dtp1.Value.Day.ToString(), ptFontEn, ptBrush, 133F + o.X, 84F + o.Y);
            //6.结束年月日
            e.Graphics.DrawString(dtp2.Value.Year.ToString(), ptFontEn, ptBrush, 155.5F + o.X, 84F + o.Y);
            e.Graphics.DrawString(dtp2.Value.Month.ToString(),ptFontEn, ptBrush, 167.5F + o.X, 84F + o.Y);
            e.Graphics.DrawString(dtp2.Value.Day.ToString(), ptFontEn,ptBrush, 177F + o.X, 84F + o.Y);
            //7.经办人
            e.Graphics.DrawString(tb_Signture.Text, ptFontEn, ptBrush, 70F + o.X, 93.5F + o.Y);
            //8.经办日期
            e.Graphics.DrawString(dtp_Date.Value.Year.ToString(), ptFontEn, ptBrush, 113.5F + o.X, 93.5F + o.Y);
            e.Graphics.DrawString(dtp_Date.Value.Month.ToString(), ptFontEn, ptBrush, 127F + o.X, 93.5F + o.Y);
            e.Graphics.DrawString(dtp_Date.Value.Day.ToString(), ptFontEn, ptBrush, 136.5F + o.X, 93.5F + o.Y);
        }

        private void bt_Exit_Click(object sender, EventArgs e)
        {
            //conn.Close();
            //Application.Exit();
            connect_4_Command.PrintWindowOpen = false;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Dispose();
            PrintHyx.PrintPICC2 pp2 = new PrintPICC2();
            pp2.Show();
        }

        private void dtp1_ValueChanged(object sender, EventArgs e)
        {
            dtp2.Value = dtp1.Value.AddDays(2);
        }

        private void ptDoc_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //打印后，保单号+1，并保存至配置文件中
            try
            {
                int it = int.Parse(tb_No.Text.Trim()) + 1;
                tb_No.Text = it.ToString("d7");//七位权益告知单号
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
            xn = xn.SelectSingleNode("PrintPICC");
            xn = xn.SelectSingleNode("SaveNo");
            xn.InnerText = this.tb_No.Text;

            xd.Save(GlobalVar.s_configfile);

        }

        private void PrintPICC_FormClosed(object sender, FormClosedEventArgs e)
        {
            b_opened = false;
            connect_4_Command.PrintWindowOpen = false;
            if (!Model.md.b_004) Application.Exit();
        }




    }
}