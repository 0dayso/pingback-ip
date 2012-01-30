using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Threading;
using System.Collections;

namespace ePlus.BookSimple
{
    public partial class SubmitPnr : Form
    {
        string[] nkg航班信息;
        string[] nkg乘客信息;
        string[] nkg政策选择;


        public bool bSubmitSuccess = false;
        private string strTktl = "黑屏提交!";
        private string strBz = "";
        public SubmitPnr()
        {
            InitializeComponent();
        }
        public SubmitPnr(string pnr, string tele)
        {
            InitializeComponent();
            this.textBox1.Text = pnr;
            this.tbPhone.Text = tele;
            opened = true;
            Context = this;
        }
        public SubmitPnr(string pnr, string tele, string tktl, string bz)
        {
            InitializeComponent();
            this.textBox1.Text = pnr;
            this.tbPhone.Text = tele;
            this.textBox2.Text = bz;
            strTktl = tktl;
            opened = true;
            Context = this;
        }
        public void 正常提交按钮(string pnr, string phone)
        {
            this.textBox1.Text = pnr;
            this.tbPhone.Text = phone;
            Application.DoEvents();
            button1_Click(null, null);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (tbPhone.Text.Trim() == "" || tbPhone.Text.Trim().Length < 5)
            {
                //tbPhone.Text = ("请输入电话号码!");
                tbPhone.SelectedText = ("请输入正确电话号码!");
                this.ActiveControl = tbPhone;
                return;
            }
            //else
            //{
            //    try
            //    {
            //        long.Parse(tbPhone.Text.Trim());
            //    }
            //    catch
            //    {
            //        tbPhone.SelectedText = ("请输入正确电话号码!");
            //        this.ActiveControl = tbPhone;
            //        return;
            //    }
            //}
            string pnrstring = textBox1.Text.Trim();
            string[] pnrs = textBox1.Text.Trim().Split(',');
            for (int i = 0; i < pnrs.Length; i++)
            {
                textBox1.Text = pnrs[i].Trim();
                this.Text = "正在提交" + pnrs[i];
                Application.DoEvents();
                threadSubmit();
            }
            textBox1.Text = pnrstring;
            this.Text = "提交完毕";
            //Thread th = new Thread(new ThreadStart(threadSubmit));
            //th.Start();
        }
        static public bool opened = false;
        private static SubmitPnr Context = null;
        static public string ReturnString
        {
            set
            {
                if (Context.InvokeRequired)
                {
                    EventHandler eh = new EventHandler(setcontrol);
                    SubmitPnr pt = SubmitPnr.Context;
                    SubmitPnr.Context.Invoke(eh, new object[] { pt, EventArgs.Empty });
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length != 5 || !EagleAPI.IsRtCode(textBox1.Text.Trim()))
            {
                //this.textBox1.Text += "错误PNR！";
                MessageBox.Show
                    (textBox1.Text + ":错误PNR！PNR只能为五个字节，PNR中不可能有字母0,I,U", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //EagleAPI.EagleSendOneCmd("i~rt:n/" + this.textBox1.Text);
            try
            {
                Options.ibe.ibeInterface ib = new Options.ibe.ibeInterface();
                MessageBox.Show(ib.rt(textBox1.Text.Trim(), GlobalVar.serverAddr == GlobalVar.ServerAddr.HangYiWang));
            }
            catch
            {
                EagleAPI.EagleSendOneCmd("i~rt:n/" + this.textBox1.Text);
            }
        }

        private void SubmitPnr_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            setcontrol(null, null);
        }
        static private void setcontrol(object sender, EventArgs e)
        {
            try
            {
                Context.textBox3.Text = GlobalVar.newEticket.carrier1 + GlobalVar.newEticket.flightno1;
                Context.textBox4.Text = GlobalVar.newEticket.bunk1;
                Context.textBox5.Text = GlobalVar.newEticket.date1 + " " + GlobalVar.newEticket.hour1 + GlobalVar.newEticket.minute1;
                Context.textBox6.Text = GlobalVar.newEticket.citypair1;

                Context.textBox7.Text = GlobalVar.newEticket.carrier2 + GlobalVar.newEticket.flightno2;
                Context.textBox8.Text = GlobalVar.newEticket.bunk2;
                Context.textBox9.Text = GlobalVar.newEticket.date2 + " " + GlobalVar.newEticket.hour2 + GlobalVar.newEticket.minute2;
                Context.textBox10.Text = GlobalVar.newEticket.citypair2;

                Context.txtName.Text = GlobalVar.newEticket.psgname;
            }
            catch
            {
                //MessageBox.Show("异常！");
            }
        }
        //string[] planeStyle = new string[4] { "","","",""};//commentted by chenqj
        List<String> planeStyleList = new List<string>();
        int ChildCount = 0;
        /// <summary>
        /// 取航段信息
        /// </summary>
        /// <param name="pnr"></param>
        public void step1(string pnr)
        {
            this.Text = "正在清理控件……";
            Application.DoEvents();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();
            textBox14.Clear();
            textBox15.Clear();
            textBox16.Clear();
            textBox17.Clear();
            textBox18.Clear();
            //for (int i = 0; i < 4; i++) planeStyle[i] = "";
            planeStyleList.Clear();
            txtName.Text = "";
#if REALIBE
            string dsgxml = step2(pnr);
            Options.ibe.ibeInterface ib = new Options.ibe.ibeInterface();
            string xml = ib.rt2(pnr, GlobalVar.serverAddr == GlobalVar.ServerAddr.HangYiWang);
#else
            logic.IBEAgent ibeagent = new logic.IBEAgent();
            ibeagent.IpID = GlobalVar.CurIPUsing;
            string xml = ibeagent.wsRT2(pnr.ToUpper());
#endif
            if (string.IsNullOrEmpty(xml) || xml.Contains("<ibe>err</ibe>"))
            {
                EagleWebService.IbeStuff.RefreshIbeUrl();
                MessageBox.Show("查询PNR失败！返回信息如下：\n" + xml);
                return;
            }
            this.Text = "正在获取PNR信息……";
            Application.DoEvents();
            Options.ibe.IbeRt ir = new Options.ibe.IbeRt(xml);
            ChildCount = 0;
            nkg乘客信息 = new string[ir.getpeopleinfo(0).Length];
            for (int i = 0; i < ir.getpeopleinfo(0).Length; i++)
            {
                //if (ir.getpeopleinfo(1)[i] == "") throw new Exception("ErrorCardId"); //commentted by chenqj
                txtName.Text += ir.getpeopleinfo(0)[i] + "-" + ir.getpeopleinfo(1)[i] + (i == ir.getpeopleinfo(0).Length - 1 ? "" : ";");
                if (ir.getpeopleinfo(0)[i].IndexOf("(CHD)") > 0) ChildCount++;
                nkg乘客信息[i] = ir.getpeopleinfo(0)[i] + "-" + ir.getpeopleinfo(1)[i] + "-"
                    + (ir.getpeopleinfo(0)[i].IndexOf("(CHD)") > 0 ? "儿童" : "成人");
            }
            string[] fi = ir.getflightsegsinfo();
            nkg航班信息 = new string[fi.Length];
            for (int i = 0; i < fi.Length; i++)
            {
                string[] segment = fi[i].Split('~');

                //more than 4 destinations,modified by chenqj
                {
                    int index = i * 4 + 3;
                    groupBox1.Controls["textBox" + index.ToString()].Text = segment[0];//航班號
                    groupBox1.Controls["textBox" + (++index).ToString()].Text = segment[1];//艙位
                    System.Globalization.DateTimeFormatInfo myDTFI = new System.Globalization.CultureInfo("en-us", false).DateTimeFormat;
                    DateTime dt;
                    try
                    {
                        dt = DateTime.ParseExact(segment[3].Replace(" CST ", " "), "ddd MMM dd HH:mm:ss yyyy", myDTFI);
                    }
                    catch
                    {
                        dt = DateTime.Parse(segment[3]);
                    }

                    groupBox1.Controls["textBox" + (++index).ToString()].Text = dt.ToString("yyyy-MM-dd HHmm", myDTFI).ToUpper();//時間
                    groupBox1.Controls["textBox" + (++index).ToString()].Text = segment[2];//日期
#if REALIBE
                    planeStyleList.Add(getPlaneByFlightno(dsgxml, segment[0]));
#else
                    planeStyleList.Add(segment[6]);
#endif
                }
                Application.DoEvents();
                {
                    //到达时间segmeng[4];
                    System.Globalization.DateTimeFormatInfo myDTFI = new System.Globalization.CultureInfo("en-us", false).DateTimeFormat;
                    DateTime leavetime;
                    try
                    {
                        leavetime = DateTime.ParseExact(segment[4].Replace(" CST ", " "), "ddd MMM dd HH:mm:ss yyyy", myDTFI);
                    }
                    catch
                    {
                        leavetime = DateTime.Parse(segment[4]);
                    }
                    string lt = leavetime.ToString("yyyy-MM-dd HHmm", myDTFI).ToUpper();

                    DateTime arrivetime;
                    try
                    {
                        arrivetime = DateTime.ParseExact(segment[5].Replace(" CST ", " "), "ddd MMM dd HH:mm:ss yyyy", myDTFI);
                    }
                    catch
                    {
                        arrivetime = DateTime.Parse(segment[5]);
                    }
                    string at = arrivetime.ToString("yyyy-MM-dd HHmm", myDTFI).ToUpper();
                    nkg航班信息[i] = lt.Split(' ')[0] + ",";
                    nkg航班信息[i] += lt.Split(' ')[1].Insert(2, ":") + "(" + segment[2].Substring(0, 3) + ")" + ",";
                    nkg航班信息[i] += at.Split(' ')[1].Insert(2, ":") + "(" + segment[2].Substring(3, 3) + ")" + ",";
                    nkg航班信息[i] += pnr + ",";
                    nkg航班信息[i] += "HK" + ",";//订座状态
                    nkg航班信息[i] += EagleAPI.GetAirLineName(segment[0].Substring(0, 2)) + ",";
                    nkg航班信息[i] += segment[0] + ",";
                    nkg航班信息[i] += segment[1] + ",";
                    nkg航班信息[i] += "#票价#" + ",";
                    //nkg航班信息[i] += planeStyle[i] + ",";
                    nkg航班信息[i] += planeStyleList[i] + ",";
                    nkg航班信息[i] += "#机建燃油#";
                }

            }
            //**************************2008-7-16置gbYzpOrder中的航班信息变量**********************************开始
            if (GlobalVar2.gbYzpOrder == null) GlobalVar2.gbYzpOrder = new YzpBtoC.YZPnewOrder();
            GlobalVar2.gbYzpOrder.rFLIGHTINFO = new YzpBtoC.YZPflightINFO[fi.Length];
            GlobalVar2.gbYzpOrder.rPASSINFO = new YzpBtoC.YZPpassINFO[ir.getpeopleinfo(0).Length];
            for (int count = 0; count < fi.Length; count++)
            {
                GlobalVar2.gbYzpOrder.rFLIGHTINFO[count] = new YzpBtoC.YZPflightINFO();
                string[] segment = fi[count].Split('~');
                GlobalVar2.gbYzpOrder.rFLIGHTINFO[count].rFLIGHTNO = segment[0];
                GlobalVar2.gbYzpOrder.rFLIGHTINFO[count].rFLIGHTDATE = getflightdate(segment[3]);
                GlobalVar2.gbYzpOrder.rFLIGHTINFO[count].rCITYLEAVE = segment[2].Substring(0, 3);
                GlobalVar2.gbYzpOrder.rFLIGHTINFO[count].rCITYARRIVE = segment[2].Substring(3);
                GlobalVar2.gbYzpOrder.rFLIGHTINFO[count].rBUNK = segment[1];
#if REALIBE
                GlobalVar2.gbYzpOrder.rFLIGHTINFO[count].rPLANETYPE = planeStyleList[count];//planeStyle[count];
                GlobalVar2.gbYzpOrder.rFLIGHTINFO[count].rTAXBUILD = step5(planeStyleList[count]);//step5(planeStyle[count]);
                GlobalVar2.gbYzpOrder.rFLIGHTINFO[count].rTIMELEAVE = getNormalDateStringFromIBEDate(segment[4]).Split(' ')[1];
                GlobalVar2.gbYzpOrder.rFLIGHTINFO[count].rTIMEARRIVE = getNormalDateStringFromIBEDate(segment[5]).Split(' ')[1];
#else
                //GlobalVar2.gbYzpOrder.rFLIGHTINFO[count].rPLANETYPE = segment[6];
                
                //GlobalVar2.gbYzpOrder.rFLIGHTINFO[count].rTAXOIL = segment[8];
                //GlobalVar2.gbYzpOrder.rFLIGHTINFO[count].rTAXBUILD = EagleString.EagleFileIO.TaxOfBuildBy(segment[6]).ToString();
                
                //GlobalVar2.gbYzpOrder.rFLIGHTINFO[count].rTIMELEAVE = DateTime.Parse(segment[4]).ToString("HHmm");
                
                //GlobalVar2.gbYzpOrder.rFLIGHTINFO[count].rTIMEARRIVE = DateTime.Parse(segment[5]).ToString("HHmm");
#endif
                GlobalVar2.gbYzpOrder.rFLIGHTINFO[count].rSTOPSTATION = "略";
                GlobalVar2.gbYzpOrder.rFLIGHTINFO[count].rAIRLINEID = "1";
                GlobalVar2.gbYzpOrder.rFLIGHTINFO[count].rAIRLINEINDEX = count.ToString();

            }
            //**************************2008-7-16置gbYzpOrder中的航班信息变量**********************************结束
        }
        string getNormalDateStringFromIBEDate(string ibedate)
        {
            try
            {
                System.Globalization.DateTimeFormatInfo myDTFI = new System.Globalization.CultureInfo("en-us", false).DateTimeFormat;
                DateTime dt = DateTime.ParseExact(ibedate.Replace(" CST ", " "), "ddd MMM dd HH:mm:ss yyyy", myDTFI);
                return dt.ToString("yyyy-MM-dd HH:mm:ss", myDTFI).ToUpper();//時間
            }
            catch
            {
                return ibedate;
            }

        }
        string getflightdate(string datestring)
        {
            try
            {
                System.Globalization.DateTimeFormatInfo myDTFI = new System.Globalization.CultureInfo("en-us", false).DateTimeFormat;
                DateTime dt = DateTime.ParseExact(datestring.Replace(" CST ", " "), "ddd MMM dd HH:mm:ss yyyy", myDTFI);
                //return dt.ToString("yyyy-MM-dd HHmm", myDTFI).ToUpper();//時間
                return dt.ToString("yyyy-MM-dd", myDTFI).ToUpper();//時間
            }
            catch
            {
                return datestring;
            }

        }
        /// <summary>
        /// 取機型
        /// </summary>
        /// <param name="pnr"></param>
        public string step2(string pnr)
        {
            this.Text = "正在获取机型……";
            Application.DoEvents();
            Options.ibe.ibeInterface ib = new Options.ibe.ibeInterface();
            return ib.dsg(pnr);
        }
        public string getPlaneByFlightno(string xmlstring, string flightno)
        {
            string ret = "";
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(xmlstring);
            XmlNode xn = xd.SelectSingleNode("ibe");
            for (int i = 0; i < xn.ChildNodes.Count; i++)
            {
                XmlNode xnTmp = xn.ChildNodes[i].SelectSingleNode("flightno");
                if (xnTmp.InnerText == flightno)
                {
                    ret = xn.ChildNodes[i].SelectSingleNode("planestyle").InnerText;
                    return ret;
                }
            }
            return ret;
        }
        /// <summary>
        /// 取全價票價及距離"fareY~distance"
        /// </summary>
        /// <param name="fromto"></param>
        /// <returns></returns>
        public string step3(string fromto)
        {
            this.Text = "正在获取票价及飞行距离信息……";
            Application.DoEvents();
            string ret = "";
            try
            {
                ret = "";// EagleAPI2.getFareLocal(fromto);
                if (ret == "") ret = EagleAPI2.getFareServer(fromto);
                if (ret == "") ret = "0~0";
            }
            catch
            {
                ret = "0~0";
            }
            return ret;
        }
        /// <summary>
        /// 取對應艙位的折扣
        /// </summary>
        /// <param name="flightno"></param>
        /// <param name="bunk"></param>
        /// <returns></returns>
        public string step4(string flightno, string bunk)
        {
            this.Text = "正在取得舱位对应折扣……";
            Application.DoEvents();
            return EagleAPI2.getRebateByAirlineAndBunk(flightno, bunk);
        }
        /// <summary>
        /// 取對應機型的機場建設稅
        /// </summary>
        /// <param name="planetype"></param>
        /// <returns></returns>
        public string step5(string planetype)
        {
            string ret = "0";
            this.Text = "正在取机场建设税……";
            Application.DoEvents();
            if (planetype == "" || planetype == null || ChildCount > 0) return "0";
            EasyTax et = new EasyTax();
            return et.GetTaxBuild(planetype);
        }
        /// <summary>
        /// 取對應距離的燃油稅
        /// </summary>
        /// <param name="dis"></param>
        /// <returns></returns>
        public string step6(string dis)
        {
            try
            {
                this.Text = "正在取燃油税……";
                Application.DoEvents();
                if (dis == "" || dis == null) return "0";
                EasyTax et = new EasyTax();
                string ret = et.GetTaxFuel(dis);
                if (ChildCount > 0)
                    ret = string.Format("{0}", (int.Parse(ret) / 2 + 5) / 10 * 10);
                return ret;
            }
            catch
            {
                return "0";
            }

        }
        /// <summary>
        /// 取政策返点
        /// </summary>
        /// <param name="flightno"></param>
        /// <param name="bunk"></param>
        /// <param name="date"></param>
        /// <param name="fromto"></param>
        /// <returns>0-100</returns>
        public string step7(string flightno, string bunk, string date, string fromto)
        {
            this.Text = "正在取政策返点……";
            Application.DoEvents();
            string ret = "0";
            string defaultpolicy = "0";
            try
            {
                if (flightno == "" || bunk == "" || date == "" || fromto == "") return "0";
                string xmlpolicy = WebService.wsGetPolicies(flightno, date, fromto.Substring(0, 3), fromto.Substring(3));
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(xmlpolicy);
                defaultpolicy = xd.SelectSingleNode("eg").SelectSingleNode("RetGain").InnerText;
                if (defaultpolicy == "") defaultpolicy = "0";
                XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode("Promots");
                ret = defaultpolicy;
                for (int i = 0; i < xn.ChildNodes.Count; i++)
                {
                    try
                    {
                        XmlNode nodePolicy = xn.ChildNodes[i];
                        string userpolicy = nodePolicy.ChildNodes[4].ChildNodes[0].Value.ToString().Trim();
                        string key = nodePolicy.ChildNodes[9].ChildNodes[0].Value.ToString().Trim();
                        if (key == flightno + "-" + bunk)
                        {
                            if (userpolicy == "") userpolicy = defaultpolicy;
                            ret = userpolicy;
                            break;
                        }

                    }
                    catch
                    {
                        ret = defaultpolicy;
                    }
                }
            }
            catch
            {
                ret = defaultpolicy;
            }
            return ret;
        }

        public string[] step8(string[] instring, int peopleCount)
        {
            this.Text = "正在计算数据……";
            Application.DoEvents();
            string[] ret;
            int count = 0;
            double tFare = 0;
            double tTaxb = 0;
            double tTaxf = 0;
            double tLirun = 0.0;


            for (int i = 0; i < instring.Length; i++)
            {
                if (instring[i] == "0~0~0~0") continue;
                count++;
                double dTemp = 0.0;
                dTemp = double.Parse(instring[i].Split('~')[0]);
                tFare += dTemp;
                //**************************2008-7-16置gbYzpOrder中的航班信息变量**********************************开始
                if (i < GlobalVar2.gbYzpOrder.rFLIGHTINFO.Length)
                {
                    GlobalVar2.gbYzpOrder.rFLIGHTINFO[i].rSALEPRICE = instring[i].Split('~')[0];
                }
                //**************************2008-7-16置gbYzpOrder中的航班信息变量**********************************结束

                dTemp = double.Parse(instring[i].Split('~')[1]);
                tTaxb += dTemp;
                dTemp = double.Parse(instring[i].Split('~')[2]);
                tTaxf += dTemp;
                dTemp = double.Parse(instring[i].Split('~')[0]) * double.Parse(instring[i].Split('~')[3]) / 100.0;
                tLirun += dTemp;
            }
            double iReal = tFare - tLirun;
            double dGain = tLirun * 100.0 / tFare;
            string str1 = tFare.ToString("f0");
            string str2 = iReal.ToString("f0");
            string str3 = tTaxb.ToString("f0");
            string str4 = tTaxf.ToString("f0");
            string str5 = dGain.ToString("f2");
            for (int i = 0; i < GlobalVar2.gbYzpOrder.rPASSINFO.Length; i++)
            {
                GlobalVar2.gbYzpOrder.rPASSINFO[i] = new YzpBtoC.YZPpassINFO();
                GlobalVar2.gbYzpOrder.rPASSINFO[i].bSALEPRICE = str1;
                GlobalVar2.gbYzpOrder.rPASSINFO[i].bPAYPRICE = str2;
                GlobalVar2.gbYzpOrder.rPASSINFO[i].bHANDBACKPRICE = "0";
                GlobalVar2.gbYzpOrder.rPASSINFO[i].bTKTNO = "";
                GlobalVar2.gbYzpOrder.rPASSINFO[i].bINSURANCETYPE = "0";
                GlobalVar2.gbYzpOrder.rPASSINFO[i].bINSURANCEBASICBPRICE = "3";
                GlobalVar2.gbYzpOrder.rPASSINFO[i].bINSURANCESALEPRICE = "20";
                GlobalVar2.gbYzpOrder.rPASSINFO[i].bINSURANCEISFREE = "0";
                GlobalVar2.gbYzpOrder.rPASSINFO[i].bINSURANCESTATUS = "1";
                GlobalVar2.gbYzpOrder.rPASSINFO[i].bTICKETCONFIGID = "0";
                GlobalVar2.gbYzpOrder.rPASSINFO[i].bISBUY = "1";
                GlobalVar2.gbYzpOrder.rPASSINFO[i].bISESPECIAL = "1";
                GlobalVar2.gbYzpOrder.rPASSINFO[i].bBackHandMoney = tLirun.ToString("f0");
                GlobalVar2.gbYzpOrder.rPASSINFO[i].bTaxTotal = string.Format("{0}", (tTaxb + tTaxf));

            }
            double mt = 0.1;//误差范围
            if (count == 1)
            {
                double dGain1 = double.Parse(instring[0].Split('~')[3]);
                if (Math.Abs(dGain - dGain1) > mt) throw new Exception("error1");
            }
            if (count == 2)
            {
                double dgain1 = double.Parse(instring[0].Split('~')[3]);
                double dgain2 = double.Parse(instring[1].Split('~')[3]);
                mt = Math.Abs(dgain2 - dgain1);
                if (Math.Abs(dGain - dgain1) > mt && Math.Abs(dGain - dgain2) > mt) throw new Exception("error1");
            }

            string str6 = tLirun.ToString("f0");
            //double dstr7 = (iReal * (double)peopleCount);
            string str7 = string.Format("{0}", int.Parse(str2) * peopleCount);
            ret = new string[7] { str1, str2, str3, str4, str5, str6, str7 };
            return ret;


        }
        public void step9(string[] inarray, string telephone)
        {
            this.Text = "正在向服务器传送数据……";
            Application.DoEvents();
            if (txtName.Text.Trim() == "" || textBox3.Text.Trim() == "")
            {
                MessageBox.Show("请先提取ＰＮＲ或者正确输入姓名身份证及所订航班信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (textBox1.Text.Trim().Length != 5 || !EagleAPI.IsRtCode(textBox1.Text.Trim()))
            {
                //if (EagleAPI.IsRtCode(textBox1.Text.Trim()))
                {
                    MessageBox.Show("错误PNR！PNR只能为五个字节，PNR中不可能有字母0,I,U", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (GlobalVar2.gbUserModel == 0)//b2c用户无视余额限制 added by chenqj
            {
                GetRemainMoney rm = new GetRemainMoney();
                rm.getcurmoney();
                string cmoney = rm.getcurmoney();
                if (cmoney != "A")
                {
                    GlobalVar.f_CurMoney = cmoney;
                    //GlobalVar.f_CurMoney = EagleAPI.substring(GlobalVar.f_CurMoney, 0, GlobalVar.f_CurMoney.Length - 2) + "00";
                    if (float.Parse(GlobalVar.f_CurMoney) <= -500F)
                    {
                        //MessageBox.Show("对不起，您的余额不足！");
                        MessageBox.Show("抱歉，您的余额不足！请及时冲款", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    float yujiYe = float.Parse(GlobalVar.f_CurMoney) - float.Parse(inarray[6]);
                    if (yujiYe < 0)
                        MessageBox.Show("严重警告，本笔金额预计将超支,请及时冲款", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (yujiYe < 500)
                        MessageBox.Show("警告:本笔金额扣款后预计余额将少于500,请及时冲款", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (yujiYe < 1000)
                        MessageBox.Show("提示:本笔金额扣款后预计余额将少于1000,请及时冲款", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //MessageBox.Show("获取余额失败！");
                    MessageBox.Show("抱歉，获取余额失败！请尝试重新提交", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
            }
            if (textBox9.Text.Trim() == "") textBox9.Text = textBox5.Text;
            EasySubmitPnr sp = new EasySubmitPnr();
            //if (sp.submit_easy_pnr(this.textBox1.Text, "黑屏提交！", null,this.textBox2.Text))
            try
            {
                if (sp.submit_easy_pnr(textBox1.Text, strTktl,
                    textBox2.Text + (ChildCount > 0 ? "有" + ChildCount.ToString() + "名儿童旅客" : ""),
                    txtName.Text, telephone, txtName.Text.Split(';').Length.ToString(),
                    new string[] { textBox3.Text, textBox7.Text },
                    new string[] { textBox4.Text, textBox8.Text },
                    new string[] { textBox5.Text, textBox9.Text },
                    new string[] { textBox6.Text, textBox10.Text },
                    inarray[0], inarray[1], inarray[2], inarray[3], inarray[4], inarray[5], inarray[6])
                    )
                {
                    //this.textBox2.Text += "恭喜，提交成功！";
                    bSubmitSuccess = true;
                    MessageBox.Show("恭喜，提交成功", "CONGRATUATIONS", MessageBoxButtons.OK, MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                }
                else //this.textBox1.Text += "提交失败！";
                {
                    if (sp.submit_easy_pnr(textBox1.Text, strTktl,
                    textBox2.Text + "(系统增加:有航线未取到票价信息)", txtName.Text, "", txtName.Text.Split(';').Length.ToString(),
                    new string[] { textBox3.Text, textBox7.Text },
                    new string[] { textBox4.Text, textBox8.Text },
                    new string[] { textBox5.Text, textBox9.Text },
                    new string[] { textBox6.Text, textBox10.Text },
                    "0", "0", "0", "0", "0", "0", "0")
                    )
                        MessageBox.Show("恭喜，提交成功", "CONGRATUATIONS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    else
                        MessageBox.Show("抱歉，提交失败，请检查提交项是否完整", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "BTOC") this.Close();
            }
            this.Text = "提交PNR";

        }

        public void steps(string pnr)
        {

            //1.置對話框值
            step1(textBox1.Text.Trim());//包含了step2
            string[] result = new string[planeStyleList.Count];
            string[] fareAndistance = new string[planeStyleList.Count];
            string[] rebate = new string[planeStyleList.Count];

            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show
                ("PNR不存在!或者服务器不可用,可能需要紧急提交", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //2.取票面價 more than 4 destinations,modified by chenqj
            for (int i = 0; i < planeStyleList.Count; i++)
            {
                int index = i * 4 + 3;

                if (GetTextBox(index + 3).Text.Trim().Length == 6)
                {
                    fareAndistance[i] = step3(GetTextBox(index + 3).Text.Trim());
                    rebate[i] = step4(GetTextBox(index).Text, GetTextBox(index + 1).Text);
                    double f = double.Parse(fareAndistance[i].Split('~')[0].Trim());
                    double r = double.Parse(rebate[i]);
                    if (ChildCount > 0) r = 50;//儿童
                    int fr = (int)((f * r) / 100.0 + 5.0) / 10 * 10;//票价
                    result[i] = fr.ToString() + "~";// fareAndistance[0].Split('~')[0].Trim() + "~";
                    nkg航班信息[i] = nkg航班信息[i].Replace("#票价#", fr.ToString());
                }
                else
                    result[i] = "0~";
            }

            for (int i = 0; i < nkg航班信息.Length; i++) nkg航班信息[i] = nkg航班信息[i].Replace("#票价#", "0");//如果票价没替换,则以0替换

            int[] tax = new int[nkg航班信息.Length];
            //3.取機建
            //for (int i = 0; i < planeStyle.Length; i++)
            for (int i = 0; i < planeStyleList.Count; i++)
            {
                try
                {
                    //string taxbuild = step5(planeStyle[i]).Trim();
                    string taxbuild = step5(planeStyleList[i]).Trim();
                    result[i] += taxbuild + "~";
                    tax[i] = int.Parse(taxbuild);//可能不足四个航段,被CATCH掉
                }
                catch
                {
                }
            }
            //4.取燃油
            //for (int i = 0; i < planeStyle.Length; i++)
            for (int i = 0; i < planeStyleList.Count; i++)
            {
                string tmpTaxOil = "";
                try
                {
                    tmpTaxOil = step6(fareAndistance[i].Split('~')[1]).Trim();
                }
                catch
                {
                    tmpTaxOil = "0";
                }

                //**************************2008-7-16置gbYzpOrder中的航班信息变量**********************************开始
                if (i < GlobalVar2.gbYzpOrder.rFLIGHTINFO.Length)
                {
                    GlobalVar2.gbYzpOrder.rFLIGHTINFO[i].rTAXOIL = tmpTaxOil;
                    GlobalVar2.gbYzpOrder.rFLIGHTINFO[i].rDisCount = rebate[i];
                }
                //**************************2008-7-16置gbYzpOrder中的航班信息变量**********************************结束
                result[i] += tmpTaxOil + "~";

                try
                {
                    tax[i] += int.Parse(tmpTaxOil);
                    nkg航班信息[i] = nkg航班信息[i].Replace("#机建燃油#", tax[i].ToString());//可能不足四个航段被捕捉掉
                }
                catch
                {
                }
            }
            //////////////////////////////////////////////////////////NKG航班信息已经完整
            //5.取返点 more than 4 destinations,modified by chenqj
            for (int i = 0; i < planeStyleList.Count; i++)
            {
                int index = i * 4 + 3;
                string strFlight = this.GetTextBox(index).Text;
                string strClass = this.GetTextBox(index + 1).Text;
                string strDate = this.GetTextBox(index + 2).Text;
                string strCity = this.GetTextBox(index + 3).Text;

                try
                {
                    result[i] += step7(strFlight, strClass, strDate.Substring(0, strDate.IndexOf(' ')), strCity);
                }
                catch
                {
                    result[i] += "0";
                }
            }

            //6.计算数据
            string[] args = step8(result, txtName.Text.Split(';').Length);

            //6.5 NKG创建订单
            if (GlobalVar.gbIsNkgFunctions)
            {
                ePlus.Data.PolicySelect ps = new ePlus.Data.PolicySelect();
                ps.FillTables(nkg航班信息, nkg乘客信息, nkg政策选择);
                ePlus.Data.dlgPolicySelect dlgPS = new ePlus.Data.dlgPolicySelect(ps);
                dlgPS.ShowDialog();
            }
            //7.提交
            step9(args, this.tbPhone.Text);
        }

        public void threadSubmit()
        {
            this.button1.Text = "正在处理……";
            this.button2.Text = "请稍等";
            this.button1.Enabled = false;
            this.button2.Enabled = false;
            try
            {
                steps(textBox1.Text.Trim());
            }
            catch (System.Net.Sockets.SocketException e1)
            {
                string msg = "IBE {0}号线路故障，将启用其他线路，请重新尝试。附详细信息：\n\n{1}";
                EagleWebService.IbeStuff.RefreshIbeUrl();
                msg = string.Format(msg, Options.GlobalVar.IbeID, e1.Message);
                MessageBox.Show(msg);
                this.Text = "IBE 故障？！";
                return;
            }
            catch (System.Net.WebException e2)
            {
                string msg = "IBE {0}号线路故障，将启用其他线路，请重新尝试。附详细信息：\n\n{1}";
                EagleWebService.IbeStuff.RefreshIbeUrl();
                msg = string.Format(msg, Options.GlobalVar.IbeID, e2.Message);
                MessageBox.Show(msg);
                this.Text = "IBE 故障？！";
                return;
            }
            catch (Exception ex)
            {
                if (ex.Message == "error1")
                {
                    MessageBox.Show("计算返点时发生错误,请重新提交");
                    this.Text = "请重新提交,或者使用紧急提交";
                    return;
                }
                if (ex.Message == "ErrorCardId")
                {
                    MessageBox.Show("请在PNR中输入身份证SSR项后重新提交");
                    this.Text = "无身份证";
                    return;

                }
                EasySubmitPnr sp = new EasySubmitPnr();
                try
                {
                    if (sp.submit_easy_pnr(textBox1.Text, strTktl,
                    textBox2.Text + "(系统增加:有航线未取到票价信息)", txtName.Text, "", txtName.Text.Split(';').Length.ToString(),
                    new string[] { textBox3.Text, textBox7.Text },
                    new string[] { textBox4.Text, textBox8.Text },
                    new string[] { textBox5.Text, textBox9.Text },
                    new string[] { textBox6.Text, textBox10.Text },
                    "0", "0", "0", "0", "0", "0", "0")
                    )
                    {
                        bSubmitSuccess = true;
                        MessageBox.Show("恭喜，提交成功", "CONGRATUATIONS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else
                        MessageBox.Show("抱歉，提交失败，请检查提交项是否完整", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Text = "已处理完毕，若未提示成功，请再次提交";
                }
                catch (Exception ex1)
                {
                    if (ex1.Message == "BTOC") this.Close();
                }

            }

            this.button1.Text = "提交";
            this.button2.Text = "查看PNR";
            this.button1.Enabled = true;
            this.button2.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("请在无法正常提交情况下使用紧急提交！是否继续？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                == DialogResult.No) return;
            if (!EagleAPI.IsRtCode(textBox1.Text.Trim()))
            {
                MessageBox.Show("PNR有错误！请仔细核对");
                return;
            }
            try
            {
                EasySubmitPnr sp = new EasySubmitPnr();
                if (sp.submit_easy_pnr(textBox1.Text, "紧急提交！",
                "紧急提交备注" + "(系统增加:有航线未取到票价信息)", "紧急提交姓名", "", "0",
                new string[] { "EG66", "Y" },
                new string[] { DateTime.Now.ToShortDateString() +" "+ DateTime.Now.Hour.ToString("D2")+ DateTime.Now.Minute.ToString("D2")
                    , "AAAZZZ" },
                new string[] { "EG66", "Y" },
                new string[] { DateTime.Now.ToShortDateString() +" "+ DateTime.Now.Hour.ToString("D2")+ DateTime.Now.Minute.ToString("D2")
                    , "AAAZZZ"  },
                "0", "0", "0", "0", "0", "0", "0")
                )
                {
                    bSubmitSuccess = true;
                    MessageBox.Show("恭喜，紧急提交成功", "CONGRATUATIONS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                    MessageBox.Show("抱歉，紧急提交失败，请检查提交项是否完整", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch
            {
                this.Text = "已处理完毕，若未提示成功，请再次提交";
            }
        }

        private void tbPhone_KeyUp(object sender, KeyEventArgs e)
        {

        }

        Control GetTextBox(int index)
        {
            return groupBox1.Controls["textBox" + index.ToString()];
        }
    }
}