/*Create by 易格网科技
 * 创建人：Wang.Clawc 创建时间：2007年
 * 一键自动出票使用方法:输入编码,选择是否自动PAT按钮,点击一键自动出票使用方法
 * 功能:根据黑屏选择的配置,得到打票机号;根据PNR信息,自动选择PAT项,可能不准确
 * 尽量在黑屏中做好PAT,这里就把是否自动PAT选项去掉.
 * 
 * ETDZ出票使用方法:输入编码,选择PAT指令,(若已输入PAT项,在已做PAT项上打勾!),点击ETDZ出票
 * 区别:前者自动识别,可能存在错误PAT隐患,后者人工选择PAT项.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace ePlus
{
    public partial class CreateETicket : Form
    {

        bool bRtBotton = true;
        bool bAutoEtdzButton = false;
        public CreateETicket()
        {
            InitializeComponent();
        }
        private void bt查看PNR_Click(object sender, EventArgs e)
        {
            bRtBotton = true;
            EagleAPI.CLEARCMDLIST(3);
            EagleAPI.EagleSendOneCmd("i~rT" + tbPnr.Text.Trim());
        }

        enum pattype { pat, patch, pata };
        pattype pt = pattype.pat;
        private void btGetPat_Click(object sender, EventArgs e)
        {
            bRtBotton = false;
            EagleAPI.CLEARCMDLIST(3);
            EagleAPI.EagleSendOneCmd("i~rT" + tbPnr.Text.Trim() + "~pat:");
            pt = pattype.pat;
        }
        private void btPATCH_Click(object sender, EventArgs e)
        {
            bRtBotton = false;
            EagleAPI.CLEARCMDLIST(3);
            EagleAPI.EagleSendOneCmd("i~rT" + tbPnr.Text.Trim() + "~pat:*ch");
            pt = pattype.patch;

        }

        private void btPATA_Click(object sender, EventArgs e)
        {
            bRtBotton = false;
            EagleAPI.CLEARCMDLIST(3);
            EagleAPI.EagleSendOneCmd("i~rT" + tbPnr.Text.Trim() + "~pat:A");
            pt = pattype.pata;
        }
        private void CreateETicket_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            context = this;
            //本地城市

            tbLocalCity.Text = "";//GetLocalCity(System.Windows.Forms.Application.StartupPath + "\\options.XML") + "CA";
            //改签
            init_restrictions();
            cbRestrictions.Text = cbRestrictions.Items[0].ToString();

            GlobalVar.b_GetPatItem = true;

            nPeople_ValueChanged(null, null);
            nPrinter_Click(null, null);
            cbRestrictions_TextChanged(null, null);
            tbLocalCity_TextChanged(null, null);
            nPrinter.Value = GetPrinterNumber(CreateETicket.configstring1);
            nPrinter_Click(null,null);
            isPATed_CheckedChanged(null, null);
        }

        private void CreateETicket_FormClosed(object sender, FormClosedEventArgs e)
        {
            GlobalVar.b_GetPatItem = false;
            this.Dispose();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (float.Parse(GlobalVar.f_CurMoney) < 110.0F)
            {
                MessageBox.Show("余额不足！");
                return;
            }

            if (DialogResult.Cancel == MessageBox.Show("确定要出票吗？您是否选择了正确的配置？", "警告", MessageBoxButtons.OKCancel)) return;
            if (tbPnr.Text.Trim().Length != 5) { MessageBox.Show("编码不正确！"); return; }
            //string temp = "rT" + tbPnr.Text.Trim() + "~pat:~" + GlobalVar.strPatItem;
            //temp = temp.Replace("\r\n", "\xd");
            //temp = mystring.trim(temp);//去掉最后一个不可见字符
            //temp += "~rt";// +tbPnr.Text.Trim();

            //int nseg1 = int.Parse(nPeople.Value.ToString()) + 1;
            //int nseg2 = nseg1 + 1;
            //temp += "~" + nseg1.ToString() + "rr";
            //if (nSegment.Value == 2) temp += "~rt~" + nseg2.ToString() + "rr";

            //int ntktl = (int)nSegment.Value + (int)nPeople.Value + 3;
            //temp += "~rt~" + ntktl.ToString() + "at/" + tbLocalCity.Text.Trim();

            //if (cbRestrictions.Text.Trim() != "") temp += "~ei:" + cbRestrictions.Text;

            //temp += "~etdz:" + nPrinter.Value.ToString();
            EagleAPI.etstatic.Pnr = tbPnr.Text;
            string temp = "";
            temp += "i~" + tbRTPNR.Text.Trim();
            if (tbPAT.Text.Trim() != "")
            {
                switch (pt)
                {
                    case pattype.pat:
                        temp += "~pat:~" + tbPAT.Text.Trim();
                        break;
                    case pattype.patch:
                        temp += "~pat:*ch~" + tbPAT.Text.Trim();
                        break;
                    case pattype.pata:
                        temp += "~pat:a~" + tbPAT.Text.Trim();
                        break;
                }
                
            }
            if (tbRRA.Text.Trim() != "")
            {
                temp += "~" + tbRRA.Text.Trim();// +"~rt";
            }
            if (tbRRB.Text.Trim() != "")
            {
                temp += "~" + tbRRB.Text.Trim();// +"~rt";
            }
            if (tbRRC.Text.Trim() != "")
            {
                temp += "~" + tbRRC.Text.Trim();// +"~rt";

            }
            if (tbRRD.Text.Trim() != "")
            {
                temp += "~" + tbRRD.Text.Trim();// +"~rt";
            }
            if (tbATWUH.Text.Trim() != "")
            {
                temp += "~" + tbATWUH.Text.Trim() + "~rt";
            }
            if (tbEI.Text.Trim() != "")
            {
                temp += "~" + tbEI.Text.Trim();
            }
            if (tbETDZ.Text.Trim() != "")
            {
                temp += "~" + tbETDZ.Text.Trim();
            }
            char ttt = (char)0x0d;
            EagleAPI.EagleSendOneCmd(temp.Replace("\r\n",ttt.ToString()));
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private string GetLocalCity(string fn)
        {
            FileStream fs = new FileStream(fn, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);

            XmlNode xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("LocalCityCode");
            return xn.InnerText;
        }
        private void init_restrictions()
        {
            FileStream fs = new FileStream(Application.StartupPath + "\\restictions.mp3", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.GetEncoding("gb2312"));

            string sline = sr.ReadLine();
            while (sline != null)
            {
                this.cbRestrictions.Items.Add(sline);
                sline = sr.ReadLine();
            }
            sr.Close();
            fs.Close();
        }

        private void tbPnr_TextChanged(object sender, EventArgs e)
        {
            if (tbPnr.Text.Trim().Length > 0) tbRTPNR.Text = "rT:" + tbPnr.Text;
            if (tbPnr.Focused)
            {
                tbPNR2.Text = tbPnr.Text;
            }
            else if (tbPNR2.Focused)
            {
                tbPnr.Text = tbPNR2.Text;

            }
            if (EagleString.BaseFunc.PnrValidate(tbPnr.Text.Trim()))
            {
                bt查看PNR_Click(null, null);
            }
        }

        private void tbPAT_Enter(object sender, EventArgs e)
        {
            //tbPAT.Text = GlobalVar.strPatItem;
        }

        private void nPeople_ValueChanged(object sender, EventArgs e)
        {
            int iSeg = (int)this.nSegment.Value;
            int no = 0;
            switch (iSeg)
            {
                    
                case 1:
                    no = (int)nPeople.Value + 1;
                    tbRRA.Text = no.ToString() + "rR";
                    tbRRB.Text = tbRRC.Text = tbRRD.Text = "";
                    break;
                case 2:
                    no = (int)nPeople.Value + 1;
                    tbRRA.Text = no.ToString() + "rR";

                    no = (int)nPeople.Value + 2;
                    tbRRB.Text = no.ToString() + "rR";
                    tbRRC.Text = tbRRD.Text = "";
                    break;
                case 3:
                    no = (int)nPeople.Value + 1;
                    tbRRA.Text = no.ToString() + "rR";

                    no = (int)nPeople.Value + 3;
                    tbRRC.Text = no.ToString() + "rR";

                    no = (int)nPeople.Value + 2;
                    tbRRB.Text = no.ToString() + "rR";
                    tbRRD.Text = "";
                    break;
                case 4:
                    no = (int)nPeople.Value + 4;
                    tbRRD.Text = no.ToString() + "rR";

                    no = (int)nPeople.Value + 3;
                    tbRRC.Text = no.ToString() + "rR";

                    no = (int)nPeople.Value + 2;
                    tbRRB.Text = no.ToString() + "rR";

                    no = (int)nPeople.Value + 1;
                    tbRRA.Text = no.ToString() + "rR";
                    break;
                default:
                    MessageBox.Show("航段数错误");
                    break;

            }
            tbLocalCity_TextChanged(null, null);
        }

        private void nSegment_ValueChanged(object sender, EventArgs e)
        {
            nPeople_ValueChanged(sender, e);
        }

        private void nPrinter_Click(object sender, EventArgs e)
        {
            tbETDZ.Text = "eTdZ:" + nPrinter.Value.ToString();
        }

        private void cbRestrictions_TextChanged(object sender, EventArgs e)
        {
            if (cbRestrictions.Text.Trim().Length > 0)
                tbEI.Text = "EI:" + cbRestrictions.Text;
            else
                tbEI.Text = "";
        }

        private void tbLocalCity_TextChanged(object sender, EventArgs e)
        {
            int no = (int)nPeople.Value + (int)nSegment.Value + 3;
            if (tbLocalCity.Text.Trim() == "")
            {
                tbATWUH.Text = "xE" + no.ToString();
            }
            else
            {
                tbATWUH.Text = no.ToString() + "aT/" + tbLocalCity.Text;
            }
        }
        public static CreateETicket context = null;
        public static string returnstring = "";
        public static string retstring
        {
            set
            {
                try
                {
                    context.tbPAT.Text = GlobalVar.strPatItem;
                    if (context.bRtBotton)
                    {
                        context.tbPAT.Text = CreateETicket.returnstring;
                        context.nPeople.Value = EagleAPI.GetNames("\n" + connect_4_Command.AV_String).Count;
                        context.nSegment.Value = EagleAPI.GetSegmentNumber(CreateETicket.returnstring);
                    }
                    if (context.bAutoEtdzButton)
                    {
                        context.autoEtdz(context.tbPnr.Text, false);
                        context.bAutoEtdzButton = false;
                    }
                }
                catch
                {
                }
            }
        }
        public static string configstring1 = "";
        public static string configstring2
        {
            set
            {
                context.nPrinter.Value = GetPrinterNumber(CreateETicket.configstring1);
            }

        }
        public static int GetPrinterNumber(string config)
        {
            try
            {
                if (config.Length == 4) return 0;//全部配置
                string c = config.Substring(0, 6).ToUpper();
                string str = "ENH101:1~WUH128:4~WUH129:7~WUH169:2~WUH402:2~WUH270:2~WUH285:2~CGO171:16";
                FileStream fs = new FileStream(Application.StartupPath + "\\printerno.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs, Encoding.Default);
                string linestring = "";
                while (!sr.EndOfStream)
                {
                    linestring += sr.ReadLine();
                }
                sr.Close();
                fs.Close();
                if (linestring != "") str = linestring;
                string[] arr = str.Split('~');
                for (int i = 0; i < arr.Length; i++)
                {
                    if (c == arr[i].Substring(0, 6)) return int.Parse(arr[i].Substring(7));
                }
            }
            catch
            {
            }
            return 0;
        }
        public static int GetRefundNumber(string config)
        {
            try
            {
                if (config.Length == 4) return 0;//全部配置
                string c = config.Substring(0, 6).ToUpper();
                string str = "ENH101:1~WUH128:4~WUH129:7~WUH169:2~WUH402:2~WUH270:2~CGO171:16";
                string[] arr = str.Split('~');
                for (int i = 0; i < arr.Length; i++)
                {
                    if (c == arr[i].Substring(0, 6)) return int.Parse(arr[i].Substring(7));
                }
            }
            catch
            {
            }
            return 0;
        }

        private void btAutoETDZ_Click(object sender, EventArgs e)
        {
            if (float.Parse(GlobalVar.f_CurMoney) < 110.0F)
            {
                MessageBox.Show("余额不足！");
                return;
            }
            //if (cbAutoPat.Checked)//需要PAT
            {
                bAutoEtdzButton = cbAutoPat.Checked;
                autoEtdz(this.tbPnr.Text, cbAutoPat.Checked);
            }
            //else//不需要PAT
            //{
            //    bAutoEtdzButton = false;
            //    autoEtdz(this.tbPnr.Text, false);
            //}

        }
        public void autoEtdz(string pnr,bool isPatOperation)
        {
            string prestring = "EEEEEEEEEE    ";
            try
            {
                if(!isPatOperation)EagleAPI.LogWrite(prestring + "开始进行自动出票：");
                Options.ibe.ibeInterface ib = new Options.ibe.ibeInterface();
                string rtXml = ib.rt2(pnr);
                Options.ibe.IbeRt rtResult = new Options.ibe.IbeRt(rtXml);
                //注：自动出票不检查身份证项，请在出票前输好身份证
                //0.检查PNR是否存在
                if (!isPatOperation) EagleAPI.LogWrite("检查PNR是否存在：");
                if (rtXml == "") throw new Exception("终止出票，原因：PNR不存在！");

                //1.检查是否有票号
                if (!isPatOperation) EagleAPI.LogWrite("检查是否有票号：");
                if (rtResult.getpeopleinfo(2)[0] != "") throw new Exception("终止出票，原因：已经存在票号");

                //2.根据出发城市，找到对应配置，此步骤以后增加。暂时使用当前配置
                string startCity = rtResult.getflightsegsinfo()[0].Split('~')[2].Substring(0, 3);
                //todo: 找配置，并切换配置……

                //3.检验配置是否切换成功，若成功，则应该在返回变量中，处理后面的步骤。
                //todo:EagleAPI.EagleSendOneCmd("qt");

                //4.根据PNR信息及配置定制指令串
                if (!isPatOperation) EagleAPI.LogWrite("开始生成指令串");
                List<string> cmdString = new List<string>();
                cmdString.Add("rt" + pnr);
                //      I.根据人数及航段，指定RR项
                int pCount = rtResult.getpeopleinfo(0).Length;
                { this.nPeople.Value = pCount; Application.DoEvents(); }                                        //置控件
                int fCount = rtResult.getflightsegsinfo().Length;
                { this.nSegment.Value = fCount; Application.DoEvents(); }                                       //置控件
                for (int i = 0; i < fCount; i++)
                {
                    cmdString.Add(string.Format("{0}RR", pCount + i + 1));
                }
                //      II.根据航空公司及配置，指定AT/WUH项
                int no = pCount + fCount + 3;
                string atItem = no.ToString() + "aT/" + startCity + "YY";
                cmdString.Add(atItem);
                { tbLocalCity.Text = startCity + "YY"; tbATWUH.Text = atItem; Application.DoEvents(); }         //置控件

                //      III.检查舱位是否为特价舱。来决定PAT项
                string[] fInfo = rtResult.getflightsegsinfo();
                int normalBunkCount = 0;
                for (int i = 0; i < fInfo.Length; i++)
                {
                    if (EagleAPI.isNormalBunk(fInfo[i].Split('~')[0].Substring(0, 2), fInfo[i].Split('~')[1])) normalBunkCount++;
                }
                bRtBotton = false;
                EagleAPI.CLEARCMDLIST(3);
                if (btAutoETDZ.Focused)//原一键出票
                {
                    if (cbAutoPat.Checked)
                    {
                        EagleAPI.LogWrite("发送相应的PAT指令");
                        if (normalBunkCount == fInfo.Length)//全部航段为正常舱位
                        {
                            if (fCount == 1)
                            {
                                if (isPatOperation)
                                {
                                    EagleAPI.EagleSendOneCmd("i~rT" + tbPnr.Text.Trim() + "~pat:");
                                    return;
                                }
                                cmdString.Add("pat:");
                            }
                            else
                            {
                                switch (fInfo[0].Split('~')[0].Substring(0, 2))
                                {
                                    case "CZ":
                                        if (isPatOperation)
                                        {
                                            EagleAPI.EagleSendOneCmd("i~rT" + tbPnr.Text.Trim() + "~pat:#yzzs");
                                            return;
                                        }
                                        cmdString.Add("pat:#yzzs");
                                        break;
                                    case "MU":
                                        if (isPatOperation)
                                        {
                                            EagleAPI.EagleSendOneCmd("i~rT" + tbPnr.Text.Trim() + "~pat:#muytr");
                                            return;
                                        }
                                        cmdString.Add("pat:#muytr");
                                        break;
                                    case "3U":
                                        if (isPatOperation)
                                        {
                                            EagleAPI.EagleSendOneCmd("i~rT" + tbPnr.Text.Trim() + "~pat:#3UZZ");
                                            return;
                                        }
                                        cmdString.Add("pat:#3UZZ");
                                        break;
                                    default:
                                        if (isPatOperation)
                                        {
                                            EagleAPI.EagleSendOneCmd("i~rT" + tbPnr.Text.Trim() + "~pat:");
                                            return;
                                        }
                                        cmdString.Add("pat:");
                                        break;
                                }
                            }
                        }
                        else if (normalBunkCount == 0)//全部为特价舱
                        {
                            if (isPatOperation)
                            {
                                EagleAPI.EagleSendOneCmd("i~rT" + tbPnr.Text.Trim() + "~pat:a");
                                return;
                            }
                            cmdString.Add("pat:a");
                        }
                        else//其中一部分为特价舱
                        {
                            throw new Exception("终止出票，原因：包含正常舱位和特殊舱位！不知道如何PAT！");
                        }

                        if (!isPatOperation) cmdString.Add(GlobalVar.strPatItem);
                        DialogResult dr = MessageBox.Show("PAT指令：    " + cmdString[cmdString.Count - 1] + "\r\n" +
                            "票价组：　　　" + cmdString[cmdString.Count - 1] + "\r\n\r\n是否继续出票？", "注意", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.No) return;
                    }
                }
                else if (button1.Focused)//2008-7-21  一键出票
                {
                    if (!isPATed.Checked)
                    {
                        string patStr = "";
                        if (radioButton1.Checked) patStr = "pat:";
                        if (radioButton2.Checked) patStr = "pat:A";
                        if (radioButton3.Checked) patStr = "pat:*ch";
                        if (radioButton4.Checked) patStr = "pat:#yzzs";
                        if (radioButton5.Checked) patStr = "pat:#muytr";
                        if (radioButton6.Checked) patStr = "pat:#3UZZ";
                        if (!isPatOperation) cmdString.Add(GlobalVar.strPatItem);
                        if (isPatOperation)
                        {
                            EagleAPI.EagleSendOneCmd("i~rT" + tbPnr.Text.Trim() + "~" + patStr);
                            return;
                        }
                        cmdString.Add(patStr);
                        DialogResult dr = MessageBox.Show("PAT指令：    " + cmdString[cmdString.Count - 1] + "\r\n" +
                            "票价组：　　　" + cmdString[cmdString.Count - 1] + "\r\n\r\n是否继续出票？", "注意", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.No) return;

                    }
                }
                //      IV. 签注EI项
                int bunkindex = 0;
                for (int i = 0; i < fInfo.Length; i++)
                {
                    int index = EagleAPI.IndexOfBunk(fInfo[i].Split('~')[0].Substring(0, 2), fInfo[i].Split('~')[1]);
                    if (index > bunkindex) bunkindex = index;
                }
                if (bunkindex < 3)
                {
                    cbRestrictions.Text = "";
                }
                else if(bunkindex < 6)
                {
                    cmdString.Add("EI:不得签转");
                    cbRestrictions.Text = cmdString[cmdString.Count - 1].Substring(3);      //置控件
                }
                else if (bunkindex < 16)
                {
                    cmdString.Add("EI:不得签转更改");
                    cbRestrictions.Text = cmdString[cmdString.Count - 1].Substring(3);      //置控件
                }
                else
                {
                    cmdString.Add("EI:不得签转更改退票");
                    cbRestrictions.Text = cmdString[cmdString.Count - 1].Substring(3);      //置控件
                }
                Application.DoEvents();
                //      V.出票ETDZ项
                int pn = CreateETicket.GetPrinterNumber(GlobalVar.officeNumberCurrent);
                cmdString.Add("ETDZ:" + pn.ToString());
                tbETDZ.Text = "ETDZ:" + pn.ToString();                              //置控件
                Application.DoEvents();
                if (pn == 0) throw new Exception("终止出票，原因：未选择配置！");
                string cmd = "i";
                for (int i = 0; i < cmdString.Count; i++)
                {
                    cmd += "~" + cmdString[i];
                }
                EagleAPI.LogWrite(prestring + "得到指令并发送：" + cmd);
                EagleAPI.EagleSendOneCmd(cmd);
            }
            catch (Exception ex)
            {
                EagleAPI.LogWrite(prestring + ex.Message);
                //控件值。
                this.tbPAT.Text = ex.Message;                                           //置控件
            }
        }

        private void isPATed_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (isPATed.Checked)
                {
                    for (int i = 1; i <= 6; i++)
                    {
                        ((RadioButton)groupBox2.Controls["radioButton" + i.ToString()]).Checked = false;
                        ((RadioButton)groupBox2.Controls["radioButton" + i.ToString()]).Enabled = false;
                    }
                }
                else
                {
                    for (int i = 1; i <= 6; i++)
                    {
                        ((RadioButton)groupBox2.Controls["radioButton" + i.ToString()]).Checked = false;
                        ((RadioButton)groupBox2.Controls["radioButton" + i.ToString()]).Enabled = true;
                    }
                    radioButton2.Checked = true;
                }
            }
            catch
            {
            }
        }
    }
}