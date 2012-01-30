using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Drawing.Printing;

namespace ePlus
{
    public partial class PrintTicket : Form
    {
        static public string retstring = "";
        public PrintTicket()
        {
            InitializeComponent();
        }
        public static bool opened = false;
        private void Print0_Load(object sender, EventArgs e)
        {
            opened = true;
            connect_4_Command.PrintWindowOpen = true;
            Context = this;
            Print0_show1();
            comboBox_票版类型.SelectedIndex = 0;
            init_restrictions();
            if (comboBox_签注.Items.Count > 0) comboBox_签注.SelectedIndex = 0;
            retstring = "";
            this.ActiveControl = this.textBox_订座编号;
        }

        //0.显示所有控件
        private void Print0_ShowAll()
        {
            panel1.Visible = true;
         checkBox1.Visible = true;
         textBox_人数.Visible = true;
         textBox_出票地点.Visible = true;
         textBox_换开凭证.Visible = true;
         textBox_订座编号.Visible = true;
         textBox_连续客票2.Visible = true;
         textBox20c.Visible = true;
         textBox10c.Visible = true;
        textBox20b.Visible = true;
         textBox10b.Visible = true;
        textBox209.Visible = true;
        textBox109.Visible = true;
        textBox207.Visible = true;
        textBox107.Visible = true;
        textBox206.Visible = true;
        textBox106.Visible = true;
        textBox20a.Visible = true;
        textBox10a.Visible = true;
        textBox208.Visible = true;
        textBox108.Visible = true;
        textBox204.Visible = true;
        textBox104.Visible = true;
        textBox205.Visible = true;
        textBox105.Visible = true;
        textBox203.Visible = true;
        textBox103.Visible = true;
        textBox202.Visible = true;
        textBox102.Visible = true;
        textBox_控制号.Visible = true;
        textBox_总数_燃油税.Visible = true;
        textBox_付款方式_总数.Visible = true;
        textBox_税款.Visible = true;
        textBox_实付.Visible = true;
        textBox_票价计算.Visible = true;
        textBox_票价.Visible = true;
        textBox_checkbox.Visible = true;
        textBox301.Visible = true;
        textBox201.Visible = true;
        textBox101.Visible = true;
        textBox_旅游编号1.Visible = true;
        textBox_始发_目的地.Visible = true;
        textBox_旅客姓名.Visible = true;
        label9.Visible = true;
        label6.Visible = true;
        label5.Visible = true;
        label_换开凭证.Visible = true;
        label_连续客票2.Visible = true;
        label_旅游编号1.Visible = true;
        label3.Visible = true;
        label11.Visible = true;
        label_控制号.Visible = true;
        label_付款方式_总数.Visible = true;
        label_总数_燃油税.Visible = true;
        label14.Visible = true;
        label13.Visible = true;
        label18.Visible = true;
        label12.Visible = true;
        label_旅客姓名.Visible = true;
        label_本航票.Visible = true;
        label_签注.Visible = true;
        button_修改.Visible = true;
        button_连续打印.Visible = true;
        button_删除.Visible = true;
        button_签注维护.Visible = true;
        comboBox_出票模板.Visible = true;
        comboBox_签注.Visible = true;
        textBox_税款合计.Visible = true;
        textBox_货币收受.Visible = true;
        textBox_手续费率.Visible = true;
        label_航空公司代号.Visible = true;
        label_货币收受.Visible = true;
        label_手续费率.Visible = true;
        label_税款合计.Visible = true;
        textBox_连续客票1.Visible = true;
        textBox_出票日期.Visible = true;
        textBox_填开单位.Visible = true;
        label_连续客票1.Visible = true;
        label_出票日期.Visible = true;
        label_填开单位.Visible = true;
        label_航空公司.Visible = true;
        comboBox_航空公司.Visible = true;
        textBox_付款方式.Visible = true;
        textBox_旅游编号2.Visible = true;
        label_旅游编号2.Visible = true;
        label_付款方式.Visible = true;
        label_中心票.Visible = true;
        textBox_行程单编号.Visible = true;
        label_行程单编号.Visible = true;
        label_南航.Visible = true;
        textBox_检查号.Visible = true;
        textBox_客票号.Visible = true;
        textBox_航空公司代号.Visible = true;
        label_航_客_检.Visible = true;
        label_东航.Visible = true;
        comboBox_票版类型.Visible = true;
        radioButton_小编码.Visible = true;
        radioButton_大编码.Visible = true;
        button_清除.Visible = true;
        button_打印.Visible = true;
        button_退出.Visible = true;
        label20.Visible = true;
        label1.Visible = true;
        listBox_姓名组.Visible = true;
        label_航协票.Visible = true;

        label_总数_燃油税.Text = "总　　数";
        textBox_总数_燃油税.Text = "CNY";
        label_付款方式_总数.Text = "付款方式";
        textBox_付款方式_总数.Text = "CASH(CNY)";

        radioButton_小编码.Checked = true;
        radioButton_大编码.Checked = false;
        }

        //1.显示本航票控件
        private void Print0_show1()
        {
            Print0_ShowAll();

            label_航空公司.Visible = false;
            comboBox_航空公司.Visible = false;
            label_连续客票1.Visible = false;
            textBox_连续客票1.Visible = false;

            label_南航.Visible = false;
            label_东航.Visible = false;
            label_中心票.Visible = false;
            label_航协票.Visible = false;
            label_填开单位.Visible = false;
            textBox_填开单位.Visible = false;

            label_出票日期.Visible = false;
            textBox_出票日期.Visible = false;
            label_行程单编号.Visible = false;
            textBox_行程单编号.Visible = false;

            label_付款方式.Visible = false;
            textBox_付款方式.Visible = false;

            label_旅游编号2.Visible = false;
            textBox_旅游编号2.Visible = false;

            label_航_客_检.Visible = false;
            label_航空公司代号.Visible = false;
            textBox_航空公司代号.Visible = false;
            textBox_客票号.Visible = false;
            textBox_检查号.Visible = false;

            label_货币收受.Visible = false;
            textBox_货币收受.Visible = false;

            label_手续费率.Visible = false;
            textBox_手续费率.Visible = false;

            label_税款合计.Visible = false;
            textBox_税款合计.Visible = false;
        }

        //2.显示中心票控件
        private void Print0_show2()
        {
            Print0_ShowAll();
            label_本航票.Visible = false;
            label_南航.Visible = false;
            label_东航.Visible = false;
            label_航协票.Visible = false;

            label_旅游编号1.Visible = false;
            textBox_旅游编号1.Visible = false;

            label_连续客票2.Visible = false;
            textBox_连续客票2.Visible = false;

            label_行程单编号.Visible = false;
            textBox_行程单编号.Visible = false;

            label_航_客_检.Visible = false;
            textBox_客票号.Visible = false;
            textBox_检查号.Visible = false;

            label_总数_燃油税.Text = "燃 油 税";
            textBox_总数_燃油税.Text = "YQ";
            label_付款方式_总数.Text = "总　　数";
            textBox_付款方式_总数.Text = "CNY";


        }

        //3.显示南航电子客票控件
        private void Print0_show3()
        {
            Print0_ShowAll();
            label_本航票.Visible = false;
            label_中心票.Visible = false;
            label_东航.Visible = false;
            label_航协票.Visible = false;

            label_航空公司.Visible = false;
            comboBox_航空公司.Visible = false;
            label_连续客票1.Visible = false;
            textBox_连续客票1.Visible = false;

            label_旅游编号1.Visible = false;
            label_旅游编号2.Visible = false;
            textBox_旅游编号1.Visible = false;
            textBox_旅游编号2.Visible = false;

            label_付款方式.Visible = false;
            textBox_付款方式.Visible = false;
            label_航_客_检.Visible = false;
            label_航空公司代号.Visible = false;
            textBox_航空公司代号.Visible = false;
            textBox_客票号.Visible = false;
            textBox_检查号.Visible = false;

            label_货币收受.Visible = false;
            textBox_货币收受.Visible = false;

            label_手续费率.Visible = false;
            textBox_手续费率.Visible = false;

            label_税款合计.Visible = false;
            textBox_税款合计.Visible = false;

            label_填开单位.Visible = false;
            textBox_填开单位.Visible = false;

            label_出票日期.Visible = false;
            textBox_出票日期.Visible = false;

            label_换开凭证.Visible = false;
            textBox_换开凭证.Visible = false;


        }

        //4.显示东航电子客票控件
        private void Print0_show4()
        {
            Print0_ShowAll();
            label_航空公司.Visible = false;
            comboBox_航空公司.Visible = false;
            label_连续客票1.Visible = false;
            textBox_连续客票1.Visible = false;

            label_南航.Visible = false;
            label_本航票.Visible = false;
            label_中心票.Visible = false;
            label_航协票.Visible = false;
            label_填开单位.Visible = false;
            textBox_填开单位.Visible = false;

            label_出票日期.Visible = false;
            textBox_出票日期.Visible = false;
            label_行程单编号.Visible = false;
            textBox_行程单编号.Visible = false;

            label_付款方式.Visible = false;
            textBox_付款方式.Visible = false;

            label_旅游编号2.Visible = false;
            textBox_旅游编号2.Visible = false;

            label_货币收受.Visible = false;
            textBox_货币收受.Visible = false;

            label_手续费率.Visible = false;
            textBox_手续费率.Visible = false;

            label_税款合计.Visible = false;
            textBox_税款合计.Visible = false;
        }

        //5.显示航协电子客票控件
        private void Print0_show5()
        {
            Print0_ShowAll();
            label_本航票.Visible = false;
            label_南航.Visible = false;
            label_东航.Visible = false;
            label_中心票.Visible = false;

            label_旅游编号1.Visible = false;
            textBox_旅游编号1.Visible = false;

            label_连续客票2.Visible = false;
            textBox_连续客票2.Visible = false;

            label_行程单编号.Visible = false;
            textBox_行程单编号.Visible = false;

            label_航空公司代号.Visible = false;

            label_总数_燃油税.Text = "燃 油 税";
            textBox_总数_燃油税.Text = "YQ";
            label_付款方式_总数.Text = "总　　数";
            textBox_付款方式_总数.Text = "CNY";

            label_货币收受.Visible = false;
            textBox_货币收受.Visible = false;

            label_手续费率.Visible = false;
            textBox_手续费率.Visible = false;

            label_税款合计.Visible = false;
            textBox_税款合计.Visible = false;

            label_控制号.Visible = false;
            textBox_控制号.Visible = false;
        }

        //6.改变票版类型，显示相应机票
        private static bool b中心票 = false;
        private void comboBox_票版类型_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_票版类型.SelectedIndex == 0) Print0_show1();
            if (comboBox_票版类型.SelectedIndex == 1) Print0_show2();
            if (comboBox_票版类型.SelectedIndex == 2) Print0_show3();
            if (comboBox_票版类型.SelectedIndex == 3) Print0_show4();
            if (comboBox_票版类型.SelectedIndex == 4) Print0_show5();
            comboBox_出票模板.Items.Clear();

            string sqlstring = string.Format("select * from ModelList where 票版类型='{0}'", comboBox_票版类型.Text);
            DataTable dt = ShortCutKeySettingsForm.dataBaseProcess.ExcuteQuery(sqlstring);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox_出票模板.Items.Add(dt.Rows[i][2].ToString());
            }
            if (comboBox_出票模板.Items.Count > 0)
                comboBox_出票模板.SelectedIndex = 0;
            if (retstring != "")
            {
                if (!b中心票)//改变之前为本票
                {
                    //改变之后为中心票
                    if (Context.comboBox_票版类型.SelectedIndex == 1 || Context.comboBox_票版类型.SelectedIndex == 4)
                    {
                        //Context.textBox_税款.Text = "CN" + EagleAPI.GetTaxBuild(retstring).Substring(3);
                        //Context.textBox_总数_燃油税.Text = "YQ" + EagleAPI.GetTaxFuel(retstring).Substring(3);
                        //Context.textBox_付款方式_总数.Text = "CNY" + EagleAPI.GetTatol(retstring).Substring(3);
                        string temp = EagleAPI.GetTaxBuild(retstring);
                        if (temp.Length > 3)
                            Context.textBox_税款.Text = "CN" + temp.Substring(3);
                        temp = EagleAPI.GetTaxFuel(retstring);
                        if (temp.Length > 3)
                            Context.textBox_总数_燃油税.Text = "YQ" + temp.Substring(3);
                        temp = EagleAPI.GetTatol(retstring);
                        if (temp.Length > 3)
                            Context.textBox_付款方式_总数.Text = "CNY" + temp.Substring(3);

                        textBox_旅游编号2.Text = textBox_旅游编号1.Text;
                        textBox_连续客票1.Text = textBox_连续客票2.Text;
                        b中心票 = true;
                    }
                }
                else//改变之前为中心票
                {
                    //改变之后为本票
                    if (comboBox_票版类型.SelectedIndex == 2 || comboBox_票版类型.SelectedIndex == 3 || comboBox_票版类型.SelectedIndex == 0)
                    {
                        //Context.textBox_税款.Text = "CN" + EagleAPI.GetTaxBuild(retstring).Substring(3) + " YQ" + EagleAPI.GetTaxFuel(retstring).Substring(3);
                        //Context.textBox_总数_燃油税.Text = "CNY" + EagleAPI.GetTatol(retstring).Substring(3);
                        //Context.textBox_付款方式_总数.Text = EagleAPI.GetFP(retstring);
                        string temp1 = EagleAPI.GetTaxBuild(retstring);
                        string temp2 = EagleAPI.GetTaxFuel(retstring);
                        if (temp1.Length > 3 && temp2.Length > 3)
                            Context.textBox_税款.Text = "CN" + temp1.Substring(3) + " YQ" + temp2.Substring(3);
                        string temp = EagleAPI.GetTatol(retstring);
                        if (temp.Length > 3)
                            Context.textBox_总数_燃油税.Text = "CNY" + temp.Substring(3);
                        Context.textBox_付款方式_总数.Text = EagleAPI.GetFP(retstring);

                        textBox_旅游编号1.Text = textBox_旅游编号2.Text;
                        textBox_连续客票2.Text = textBox_连续客票1.Text;
                        b中心票 = false;
                    }
                }
            }

        }

        private void button_签注维护_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath;
            //RunProgram(Environment.SystemDirectory + "\\notepad.exe restictions.mp3");
            PrintTicket.RunProgram(Environment.SystemDirectory + "\\notepad.exe",path + "\\restictions.mp3");
        }

        private void PrintTicket_FormClosed(object sender, FormClosedEventArgs e)
        {
            opened = false;
            connect_4_Command.PrintWindowOpen = false;
            Context = null;
            //EagleAPI.CLEARCMDLIST(3);
            EagleAPI.CLEARCMDLIST(7);
        }

        public static void RunProgram(string fileName,string arg)
        {
            if (!System.IO.File.Exists(fileName))
                throw new Exception(Properties.Resources.NoThisProgram);

            Process proc = new Process();
            proc.StartInfo.FileName = fileName;
            proc.StartInfo.Arguments = arg;
            proc.Start();
        }

        //7.初始化签注下拉框
        private void init_restrictions()
        {
            FileStream fs = new FileStream(Application.StartupPath + "\\restictions.mp3", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs,System.Text.Encoding.GetEncoding("gb2312"));

            string sline = sr.ReadLine();
            while (sline != null)
            {
                comboBox_签注.Items.Add(sline);
                sline = sr.ReadLine();
            }
            sr.Close();
            fs.Close();            
        }

        private void comboBox_签注_Click(object sender, EventArgs e)
        {
            //comboBox_签注.Items.Clear();
            //init_restrictions();
            //if (comboBox_签注.Items.Count > 0) comboBox_签注.SelectedIndex = 0;
        }

        //8.航空公司对应填开单位
        /*3U 四川航空公司       CHINA SICHUAN AIRLINE
          BK 奥凯航空公司       OKAY AIRWAYS COMPANY LIMITED
          CA 中国国际航空公司   AIR CHINA
          CZ 中国南方航空公司   CHINA SOUTHERN AIRLINES
          EU 鹰联航空公司       UNITED EAGLE AIRLINES
          FM 上海航空公司       SHANGHAI AIRLINE
          HU 海南航空公司       HAINAN AIRLINES
          MF 厦门航空公司       XIAMEN AIRLINES
          MU 中国东方航空公司   CHINA EASTERN AIRLINES
          SC 山东航空公司       SHANDONG AIRLINE
          ZH 深圳航空公司       CHINA SHENZHEN AIRLINE*/
        private string airline_issued(string airlinecode)
        {
            return EagleAPI.GetAirLineEnglishName(airlinecode);
        }
        //输入记录编号
        private static PrintTicket Context = null;
        public static string ReturnString
        {
            set
            {
                if (Context != null)
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
                            EventHandler eh = new EventHandler(sendpn);
                            TabControl tc = frmMain.st_tabControl;
                            frmMain.st_tabControl.Invoke(eh, new object[] { tc, EventArgs.Empty });
                        }
                    }
                    //if (retstring.Substring(retstring.Length - 3) != "+\r\n") rs = retstring;
                    else rs = retstring;

                }

            }
        }
        static public void sendpn(object sender, EventArgs e)
        {
            //int i = frmMain.st_tabControl.SelectedIndex;
            WindowInfo wndInfo = frmMain.windowSwitch[0];
            wndInfo.SendData("pn");
        }
        static public void sendpn7(object sender, EventArgs e)
        {
            //int i = frmMain.st_tabControl.SelectedIndex;
            WindowInfo wndInfo = frmMain.windowSwitch[0];
            wndInfo.SendData("pn",7);
        }
        static public string rs
        {
            set
            {
                //在这里拆分retstring
                if (retstring.Split('\n').Length < 4 || retstring.IndexOf("NO PNR")>-1)
                {
                    MessageBox.Show("NO PNR");
                    return;
                }
                string temp = EagleAPI.GetNames(retstring)[0];
                if (Context.InvokeRequired)
                {
                    //ePlus.PrintTicket.Context.textBox_旅客姓名.Text = temp;
                   // Context.textBox_始发_目的地.Text = EagleAPI.GetStartCity(retstring) + "/" + EagleAPI.GetEndCity(retstring);
                    EventHandler eh = new EventHandler(setcontrol);
                    PrintTicket pt = PrintTicket.Context;
                    PrintTicket.Context.Invoke(eh, new object[] { pt, EventArgs.Empty });
                }
            }
        }
        static private void setcontrol(object sender, EventArgs e)
        {
            retstring = retstring.Replace('+', ' ');
            retstring = retstring.Replace('-', ' ');
            if (!EagleAPI.GetNoPnr(retstring)) return;
            List<string> names = new List<string>();
            names = EagleAPI.GetNames(retstring);
            Context.textBox_旅客姓名.Text = names[0];
            for (int i = 0; i < names.Count; i++)
            {
                Context.listBox_姓名组.Items.Add(names[i]);
            }

            Context.textBox_始发_目的地.Text = EagleAPI.GetFromTo(retstring);
            Context.textBox_出票日期.Text = EagleAPI.GetDatePrint();
            Context.textBox101.Text = EagleAPI.GetStartCityCn(retstring);
            Context.textBox102.Text = EagleAPI.GetStartCity(retstring);
            Context.textBox103.Text = EagleAPI.GetCarrier(retstring);
            //Context.comboBox_航空公司
            //Context.textBox_填开单位
            string temp = Context.textBox103.Text;
            for (int i = 0; i < Context.comboBox_航空公司.Items.Count; i++)
            {
                if (Context.comboBox_航空公司.Items[i].ToString().Substring(0, 2) == temp)
                {
                    Context.comboBox_航空公司.SelectedIndex = i;
                    Context.switchairline(temp);
                }
            }
            
            Context.textBox104.Text = EagleAPI.GetFlight(retstring);
            Context.textBox105.Text = EagleAPI.GetClass(retstring);
            Context.textBox106.Text = EagleAPI.GetDateStart(retstring);
            Context.textBox107.Text = EagleAPI.GetTimeStart(retstring);
            Context.textBox108.Text = EagleAPI.GetBook(retstring);
            Context.textBox109.Text = EagleAPI.GetFC(retstring);

            Context.textBox10c.Text = EagleAPI.GetAllow(retstring);

            Context.textBox201.Text = EagleAPI.GetEndCityCn(retstring);
            Context.textBox202.Text = EagleAPI.GetEndCity(retstring);

            Context.textBox203.Text = EagleAPI.GetCarrier2(retstring);
            Context.textBox204.Text = EagleAPI.GetFlight2(retstring);
            Context.textBox205.Text = EagleAPI.GetClass2(retstring);
            Context.textBox206.Text = EagleAPI.GetDateStart2(retstring);
            Context.textBox207.Text = EagleAPI.GetTimeStart2(retstring);
            Context.textBox208.Text = EagleAPI.GetBook2(retstring);
            if(Context.textBox203.Text!="")Context.textBox209.Text = EagleAPI.GetFC(retstring);

            Context.textBox20c.Text = EagleAPI.GetAllow2(retstring);

            Context.textBox302.Text = EagleAPI.GetEndCity2(retstring);
            Context.textBox301.Text = EagleAPI.GetCityCn(retstring, Context.textBox302.Text);

            temp = EagleAPI.GetFare(retstring);
            if (temp.Length > 3)
                Context.textBox_票价.Text = "CNY" + EagleAPI.GetFare(retstring).Substring(3);
            else Context.textBox_票价.Text = "CNY";
            Context.textBox_票价计算.Text = EagleAPI.GetFareCal(retstring);
            if (Context.comboBox_票版类型.SelectedIndex == 1 || Context.comboBox_票版类型.SelectedIndex == 4)
            {
                temp = EagleAPI.GetTaxBuild(retstring);
                if (temp.Length > 3)
                    Context.textBox_税款.Text = "CN" + temp.Substring(temp[3] > '9' ? 0 : 3);
                else Context.textBox_税款.Text = "CN";
                temp = EagleAPI.GetTaxFuel(retstring);
                if (temp.Length > 3)
                    Context.textBox_总数_燃油税.Text = "YQ" + temp.Substring(3);
                else Context.textBox_总数_燃油税.Text = "YQ";
                temp = EagleAPI.GetTatol(retstring);
                if (temp.Length > 3)
                    Context.textBox_付款方式_总数.Text = "CNY" + temp.Substring(3);
                else Context.textBox_付款方式_总数.Text = "CNY";
            }
            else
            {
                string temp1 = EagleAPI.GetTaxBuild(retstring);
                string temp2 = EagleAPI.GetTaxFuel(retstring);
                if(temp1.Length>3 &&temp2.Length>3)
                    Context.textBox_税款.Text = "CN" + temp1.Substring(temp1[3]>'9'?0:3) + " YQ" + temp2.Substring(3);
                temp = EagleAPI.GetTatol(retstring);
                if (temp.Length > 3)
                    Context.textBox_总数_燃油税.Text = "CNY" + temp.Substring(3);
                else Context.textBox_总数_燃油税.Text = "CNY";
                Context.textBox_付款方式_总数.Text = EagleAPI.GetFP(retstring);
            }
            Context.textBox_付款方式.Text = EagleAPI.GetFP(retstring);

            Context.textBox_人数.Text = string.Format("共{0}人，选中{1}人", Context.listBox_姓名组.Items.Count, Context.listBox_姓名组.SelectedItems.Count);

            Context.textBox_航空公司代号.Text = EagleAPI.GetAirlineCode(retstring);

            Context.textBox_货币收受.Text = EagleAPI.GetCash(retstring);
            Context.textBox_手续费率.Text = EagleAPI.GetCommRate(retstring);
            Context.textBox_税款合计.Text = EagleAPI.GetTaxTotal(retstring);

            Context.textBox_订座编号.Text = Context.tempNO;
        }
        public string tempNO = "";
        private void textBox_订座编号_KeyUp(object sender, KeyEventArgs e)
        {
            
            if (e.KeyValue == 13)//回车
            {
                this.textBox_订座编号.Text = this.textBox_订座编号.Text.ToUpper();
                retstring = "";
                EagleAPI.CLEARCMDLIST(3);

                button_清除_Click(this.button_清除, e);
                if (textBox_订座编号.Text.Trim().Length != 5) return;
                //a.发送"rT"+textBox_订座编号
                EagleAPI.EagleSendCmd("rT:n/" + textBox_订座编号.Text.Trim());
                tempNO = this.textBox_订座编号.Text;
                this.textBox_订座编号.Text = GlobalVar.WaitString;
            }
        }

        private void button_清除_Click(object sender, EventArgs e)
        {
            listBox_姓名组.Items.Clear();
            textBox_checkbox.Text = "";
            //textBox_出票地点.Text = "";
            textBox_出票日期.Text = "";
            //textBox_订座编号.Text = "";
            textBox_付款方式.Text = "CASH(CNY)";
            
            textBox_航空公司代号.Text = "";
            textBox_换开凭证.Text = "";
            textBox_货币收受.Text = "CNY";
            textBox_检查号.Text = "0";
            textBox_客票号.Text = "";
            textBox_控制号.Text = "";
            textBox_连续客票1.Text = "";
            textBox_连续客票2.Text = "";
            textBox_旅客姓名.Text = "";
            textBox_旅游编号1.Text = "";
            textBox_旅游编号2.Text = "";
            textBox_票价.Text = "CNY";
            textBox_票价计算.Text = "";
            textBox_人数.Text = "";
            textBox_实付.Text = "";
            textBox_始发_目的地.Text = "";
            textBox_手续费率.Text = "300";
            textBox_税款.Text = "CN";
            textBox_税款合计.Text = "0.00";
            textBox_填开单位.Text = "";
            textBox_行程单编号.Text = "";
            if (comboBox_票版类型.SelectedIndex == 0 || comboBox_票版类型.SelectedIndex == 2 || comboBox_票版类型.SelectedIndex == 3)
            {
                textBox_总数_燃油税.Text = "CNY";
                textBox_付款方式_总数.Text = "CASH(CNY)";
            }

            else
            {
                textBox_总数_燃油税.Text = "YQ";
                textBox_付款方式_总数.Text = "CNY";
            }
            textBox101.Text = textBox102.Text = textBox103.Text = textBox104.Text = textBox105.Text = textBox106.Text = textBox107.Text = textBox109.Text = textBox10a.Text = textBox10b.Text =  "";
            //textBox108.Text ="OK";
            //textBox10c.Text = "";
            textBox201.Text = textBox202.Text = textBox203.Text = textBox204.Text = textBox205.Text = textBox206.Text = textBox207.Text = textBox208.Text = textBox209.Text = textBox20a.Text = textBox20b.Text = textBox20c.Text = "";
            textBox301.Text = ""; 
        }

        private void button_退出_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_打印_Click(object sender, EventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            pd.Document = ptDoc;

            if (!EagleAPI.PrinterSetup(ptDoc)) return;

            DialogResult dr = pd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //开始打印
                ptDoc_Print();
                //ptDoc.Print();                
            }
        }
        void ptDoc_Print()
        {
            if (!EagleAPI2.ExistFont("TEC"))
            {
                if (MessageBox.Show("未找到正常打印字体,是否继续?", "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    ptDoc.Print();
            }
            else
                ptDoc.Print();
        }
        private void ptDoc_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //PageSetupDialog psd = new PageSetupDialog();
            //psd.Document = ptDoc;
            //psd.PageSettings.Margins.Left = 0;
            //psd.PageSettings.Margins.Right = 0;
            //psd.PageSettings.Margins.Top = 0;
            //psd.PageSettings.Margins.Bottom = 0;
            //psd.ShowDialog();
        }
        
        private void ptDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            //e.PageSettings.PrinterSettings.PrintFileName = "机票打印";
            //e.PageSettings.PaperSize = new PaperSize("", 1000, 420);
            //e.PageSettings.PrinterSettings.DefaultPageSettings.PaperSize = e.PageSettings.PaperSize;



            e.PageSettings.Margins.Left = 0;
            e.PageSettings.Margins.Right = 0;
            e.PageSettings.Margins.Top = 0;
            e.PageSettings.Margins.Bottom = 0;

            
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;

            Font ptFontEn = new Font("tec", GlobalVar.fontsizeen, System.Drawing.FontStyle.Regular);
            Font ptFontCn = new Font("tec", GlobalVar.fontsizecn, System.Drawing.FontStyle.Regular);
            Brush ptBrush = Brushes.Black;

            //边线右下角237,102，测试用
            Pen pen = new Pen (ptBrush);            
            //e.Graphics.DrawRectangle(pen, 0F, 0F, 237F, 102F);
            //e.Graphics.DrawLine(pen, 0F, 0F, 237F, 102F);
            bool b中心票 = (comboBox_票版类型.SelectedIndex == 1 || comboBox_票版类型.SelectedIndex == 4);
            bool b本票 = (comboBox_票版类型.SelectedIndex == 2 || comboBox_票版类型.SelectedIndex == 3 || comboBox_票版类型.SelectedIndex == 0);

            //sample:e.Graphics.DrawString("KFC", ptFontEn, ptBrush, 0F, 0F);
            PointF o = new PointF();
            o = GlobalVar.o_ticket;
            if(comboBox_票版类型.SelectedIndex == 2)
            {
                o.X -= 25.5F;
                o.Y -= 1F;
            }
            if (comboBox_票版类型.SelectedIndex == 0 || comboBox_票版类型.SelectedIndex == 3)
            {
                o.Y -= 4F;
            }
            if (comboBox_票版类型.SelectedIndex == 1 || comboBox_票版类型.SelectedIndex == 4)
            {
                o.Y -= 5F;
            }
            ////2.comboBox_签注.SelectedText                                  (41,20)     中本电
            //e.Graphics.DrawString(comboBox_签注.Text, ptFontCn, ptBrush, 41F, 20F);
            ////3.textBox_旅客姓名.Text                                       (41,28)     中本电
            //e.Graphics.DrawString(textBox_旅客姓名.Text, ptFontCn, ptBrush, 41F, 28F);
            ////4.textBox_连续客票.Text                                       (115,12.5)  中      (125,30.8)  本电
            //if(comboBox_票版类型.SelectedIndex ==1 ||comboBox_票版类型.SelectedIndex ==4)
            //    e.Graphics.DrawString(textBox_连续客票1.Text, ptFontEn, ptBrush, 115F, 12.5F);
            //if(comboBox_票版类型.SelectedIndex ==2 ||comboBox_票版类型.SelectedIndex ==3||comboBox_票版类型.SelectedIndex ==0)
            //    e.Graphics.DrawString(textBox_连续客票2.Text, ptFontEn, ptBrush, 125F, 30.8F);
            ////5.textBox_出票日期.Text                                       (123,23.5)  中
            //if(comboBox_票版类型.SelectedIndex ==1 ||comboBox_票版类型.SelectedIndex ==4)
            //    e.Graphics.DrawString(textBox_出票日期.Text, ptFontEn, ptBrush, 123F, 23.5F);

            ////6.textBox_始发_目的地.Text    (156,14.9)   中      (145,17)    本电
            //if (comboBox_票版类型.SelectedIndex == 1 || comboBox_票版类型.SelectedIndex == 4)
            //    e.Graphics.DrawString(textBox_始发_目的地.Text, ptFontEn, ptBrush, 156F, 14.9F);
            //if (comboBox_票版类型.SelectedIndex == 2 || comboBox_票版类型.SelectedIndex == 3 || comboBox_票版类型.SelectedIndex == 0)
            //    e.Graphics.DrawString(textBox_始发_目的地.Text, ptFontEn, ptBrush, 145F, 17F);
            ////7.textBox_订座编号.Text       (156,19.1)  中      (156,22.2)  本电
            //if (comboBox_票版类型.SelectedIndex == 1 || comboBox_票版类型.SelectedIndex == 4)
            //    e.Graphics.DrawString(textBox_订座编号.Text, ptFontEn, ptBrush, 156F, 19.1F);
            //if (comboBox_票版类型.SelectedIndex == 2 || comboBox_票版类型.SelectedIndex == 3 || comboBox_票版类型.SelectedIndex == 0)
            //    e.Graphics.DrawString(textBox_订座编号.Text, ptFontEn, ptBrush, 156F, 22.2F);


            ////多行文本，要进一步处理
            ////8.textBox_出票地点.Text       (172,32)    中      (182,29.5)  本电
            //string[] WherePrint = textBox_出票地点.Text.Split('\n');
            //for (int i = WherePrint.Length - 1; i >= 0; i--)
            //{
            //    float d_line = 3F;
            //    if (comboBox_票版类型.SelectedIndex == 1 || comboBox_票版类型.SelectedIndex == 4)
            //        e.Graphics.DrawString(WherePrint[i], ptFontEn, ptBrush, 172F, 32F - (WherePrint.Length - 1-i) * 3F);
            //    if (comboBox_票版类型.SelectedIndex == 2 || comboBox_票版类型.SelectedIndex == 3 || comboBox_票版类型.SelectedIndex == 0)
            //        e.Graphics.DrawString(WherePrint[i], ptFontEn, ptBrush, 182F, 29.5F - (WherePrint.Length - 1 - i) * 3F);
            //}




            ////9.textBox_换开凭证.Text       (156,23.4)  中      (156,26.5)  本
            //if (comboBox_票版类型.SelectedIndex == 1 || comboBox_票版类型.SelectedIndex == 4)
            //    e.Graphics.DrawString(textBox_换开凭证.Text, ptFontEn, ptBrush, 156F, 23.4F);
            //if (comboBox_票版类型.SelectedIndex == 3 || comboBox_票版类型.SelectedIndex == 0)
            //    e.Graphics.DrawString(textBox_换开凭证.Text, ptFontEn, ptBrush, 156F, 26.5F);

            ////10.textBox_旅游编号1.Text     (196,74.4)  中      (115,26.5)  本
            //if (comboBox_票版类型.SelectedIndex == 1 || comboBox_票版类型.SelectedIndex == 4)
            //    e.Graphics.DrawString(textBox_旅游编号1.Text, ptFontEn, ptBrush, 196F, 74.4F);
            //if (comboBox_票版类型.SelectedIndex == 3 || comboBox_票版类型.SelectedIndex == 0)
            //    e.Graphics.DrawString(textBox_旅游编号1.Text, ptFontEn, ptBrush, 115F, 26.5F);
            ////11.textBox_行程单号.Text                          (125,30.8)  电
            //if (comboBox_票版类型.SelectedIndex == 2)
            //    e.Graphics.DrawString(this.textBox_行程单编号.Text, ptFontEn, ptBrush, 125F, 30.8F);

            ////12.票价                       (48.2,61.8) 中      (41,64.6)   本电
            //if (comboBox_票版类型.SelectedIndex == 1 || comboBox_票版类型.SelectedIndex == 4)
            //    e.Graphics.DrawString(this.textBox_票价.Text, ptFontEn, ptBrush, 48.2F, 61.8F);
            //if (comboBox_票版类型.SelectedIndex == 2 || comboBox_票版类型.SelectedIndex == 3 || comboBox_票版类型.SelectedIndex == 0)
            //    e.Graphics.DrawString(textBox_票价.Text, ptFontEn, ptBrush, 41F, 64.6F);
            ////13.实付等值货币               (43,66)     中      (50,69)     本电
            //if (comboBox_票版类型.SelectedIndex == 1 || comboBox_票版类型.SelectedIndex == 4)
            //    e.Graphics.DrawString(this.textBox_实付.Text, ptFontEn, ptBrush, 43F, 66F);
            //if (comboBox_票版类型.SelectedIndex == 2 || comboBox_票版类型.SelectedIndex == 3 || comboBox_票版类型.SelectedIndex == 0)
            //    e.Graphics.DrawString(textBox_实付.Text, ptFontEn, ptBrush, 50F, 69F);
            ////14.税款                       (43,70.2)   中      (41,73.3)   本电
            //if (comboBox_票版类型.SelectedIndex == 1 || comboBox_票版类型.SelectedIndex == 4)
            //    e.Graphics.DrawString(this.textBox_税款.Text, ptFontEn, ptBrush, 43F, 70.2F);
            //if (comboBox_票版类型.SelectedIndex == 2 || comboBox_票版类型.SelectedIndex == 3 || comboBox_票版类型.SelectedIndex == 0)
            //    e.Graphics.DrawString(textBox_税款.Text, ptFontEn, ptBrush, 41F, 73.3F);
            ////15.燃油税                     (43,74.4)   中  
            //if (comboBox_票版类型.SelectedIndex == 1 || comboBox_票版类型.SelectedIndex == 4)
            //    e.Graphics.DrawString(this.textBox_总数_燃油税.Text, ptFontEn, ptBrush, 43F, 74.4F);
            ////16.总数                       (50,78.6)   中      (41,77.5)   本电
            //if (comboBox_票版类型.SelectedIndex == 1 || comboBox_票版类型.SelectedIndex == 4)
            //    e.Graphics.DrawString(this.textBox_付款方式_总数.Text, ptFontEn, ptBrush, 50F, 78.6F);
            //if (comboBox_票版类型.SelectedIndex == 2 || comboBox_票版类型.SelectedIndex == 3 || comboBox_票版类型.SelectedIndex == 0)
            //    e.Graphics.DrawString(textBox_总数_燃油税.Text, ptFontEn, ptBrush, 41F, 77.5F);

            ////17.航空公司代理人             (50,82.8)   中      
            ////无

            ////18.付款方式                   (80,74.4)   中      (50,82)     本电
            //if (comboBox_票版类型.SelectedIndex == 1 || comboBox_票版类型.SelectedIndex == 4)
            //    e.Graphics.DrawString(this.textBox_付款方式.Text, ptFontEn, ptBrush, 80F, 74.4F);
            //if (comboBox_票版类型.SelectedIndex == 2 || comboBox_票版类型.SelectedIndex == 3 || comboBox_票版类型.SelectedIndex == 0)
            //    e.Graphics.DrawString(textBox_付款方式_总数.Text, ptFontEn, ptBrush, 50F, 82F);
            ////19.控制号                     (50,87)     中      (50,88)     本
            //if (comboBox_票版类型.SelectedIndex == 1 || comboBox_票版类型.SelectedIndex == 4)
            //    e.Graphics.DrawString(this.textBox_控制号.Text, ptFontEn, ptBrush, 50F, 87F);
            //if (comboBox_票版类型.SelectedIndex == 3 || comboBox_票版类型.SelectedIndex == 0)
            //    e.Graphics.DrawString(textBox_控制号.Text, ptFontEn, ptBrush, 50F, 88F);
            ////20.票价计算                   (80,61.8)   中      (90,64.6)   本电
            //if (comboBox_票版类型.SelectedIndex == 1 || comboBox_票版类型.SelectedIndex == 4)
            //    e.Graphics.DrawString(this.textBox_票价计算.Text, ptFontEn, ptBrush, 80F, 61.8F);
            //if (comboBox_票版类型.SelectedIndex == 2 || comboBox_票版类型.SelectedIndex == 3 || comboBox_票版类型.SelectedIndex == 0)
            //    e.Graphics.DrawString(textBox_票价计算.Text, ptFontEn, ptBrush, 90F, 64.6F);
            ////21.航空公司代号               (83,84.3)   中      (85,84)  电
            //if (comboBox_票版类型.SelectedIndex == 1 || comboBox_票版类型.SelectedIndex == 4)
            //    e.Graphics.DrawString(this.textBox_航空公司代号.Text, ptFontEn, ptBrush, 83F, 84.3F);
            ////22.客票顺序号                                     (96,84)  电
            ////23.检查号                                         (127.8,84)  电
            //if (comboBox_票版类型.SelectedIndex == 2)
            //{
            //    e.Graphics.DrawString(this.textBox_航空公司代号.Text, ptFontEn, ptBrush, 85F, 84F);
            //    e.Graphics.DrawString(this.textBox_客票号.Text, ptFontEn, ptBrush, 96F, 84F);
            //    e.Graphics.DrawString(this.textBox_检查号.Text, ptFontEn, ptBrush, 127.8F, 84F);
            //}
            ////24.货币现金收受               (144,84.3)  中
            ////25.手续费率                   (196,84.3)  中
            ////26.税款合计                   (206,84.3)  中
            //if (comboBox_票版类型.SelectedIndex == 1)
            //{
            //    e.Graphics.DrawString(this.textBox_货币收受.Text, ptFontEn, ptBrush, 144F, 84.3F);
            //    e.Graphics.DrawString(this.textBox_手续费率.Text, ptFontEn, ptBrush, 196F, 84.3F);
            //    e.Graphics.DrawString(this.textBox_税款合计.Text, ptFontEn, ptBrush, 206F, 84.3F);
            //}
            ////航班信息
            //e.Graphics.DrawString(textBox101.Text, ptFontCn, ptBrush, 45F, 40F);
            //e.Graphics.DrawString(textBox102.Text, ptFontEn, ptBrush, 72F, 40F);
            //e.Graphics.DrawString(textBox103.Text, ptFontEn, ptBrush, 83F, 40F);
            //e.Graphics.DrawString(textBox104.Text, ptFontEn, ptBrush, 95F, 40F);
            //e.Graphics.DrawString(textBox105.Text, ptFontEn, ptBrush, 108F, 40F);
            //e.Graphics.DrawString(textBox106.Text, ptFontEn, ptBrush, 114F, 40F);
            //e.Graphics.DrawString(textBox107.Text, ptFontEn, ptBrush, 128F, 40F);
            //e.Graphics.DrawString(textBox108.Text, ptFontEn, ptBrush, 142F, 40F);
            //e.Graphics.DrawString(textBox109.Text, ptFontEn, ptBrush, 151F, 40F);
            //e.Graphics.DrawString(textBox10a.Text, ptFontEn, ptBrush, 188F, 40F);
            //e.Graphics.DrawString(textBox10b.Text, ptFontEn, ptBrush, 201F, 40F);
            //e.Graphics.DrawString(textBox10c.Text, ptFontEn, ptBrush, 214F, 40F);

            //e.Graphics.DrawString(textBox201.Text, ptFontCn, ptBrush, 45F, 49F);
            //e.Graphics.DrawString(textBox202.Text, ptFontEn, ptBrush, 72F, 49F);
            //e.Graphics.DrawString(textBox203.Text, ptFontEn, ptBrush, 83F, 49F);
            //e.Graphics.DrawString(textBox204.Text, ptFontEn, ptBrush, 95F, 49F);
            //e.Graphics.DrawString(textBox205.Text, ptFontEn, ptBrush, 108F, 49F);
            //e.Graphics.DrawString(textBox206.Text, ptFontEn, ptBrush, 114F, 49F);
            //e.Graphics.DrawString(textBox207.Text, ptFontEn, ptBrush, 128F, 49F);
            //e.Graphics.DrawString(textBox208.Text, ptFontEn, ptBrush, 142F, 49F);
            //e.Graphics.DrawString(textBox209.Text, ptFontEn, ptBrush, 151F, 49F);
            //e.Graphics.DrawString(textBox20a.Text, ptFontEn, ptBrush, 188F, 49F);
            //e.Graphics.DrawString(textBox20b.Text, ptFontEn, ptBrush, 201F, 49F);
            //e.Graphics.DrawString(textBox20c.Text, ptFontEn, ptBrush, 214F, 49F);

            //e.Graphics.DrawString(textBox301.Text, ptFontCn, ptBrush, 45F, 58F);
            //1.textBox_填开单位.Text                                       (41,12.5)   中
            if (comboBox_票版类型.SelectedIndex == 1 || comboBox_票版类型.SelectedIndex == 4)
                e.Graphics.DrawString(textBox_填开单位.Text, ptFontEn, ptBrush, 41F + o.X, 12.5F + o.Y);

            //2.comboBox_签注.SelectedText                                  (41,20)     中本电
            if (b中心票)
                e.Graphics.DrawString(comboBox_签注.Text, ptFontCn, ptBrush, 40F + o.X, 19F + o.Y);
            if (b本票)
                e.Graphics.DrawString(comboBox_签注.Text, ptFontCn, ptBrush, 40F + o.X, 22F + o.Y);
            //3.textBox_旅客姓名.Text                                       (41,28)     中本电
            if (b中心票)
                e.Graphics.DrawString(textBox_旅客姓名.Text, ptFontCn, ptBrush, 41F + o.X, 27.4F + o.Y);
            if (b本票)
                e.Graphics.DrawString(textBox_旅客姓名.Text, ptFontCn, ptBrush, 41F + o.X, 30F + o.Y);
            //4.textBox_连续客票.Text                                       (115,12.5)  中      (125,30.8)  本电
            if (b中心票)
                e.Graphics.DrawString(textBox_连续客票1.Text, ptFontEn, ptBrush, 115F + o.X, 12.5F + o.Y);
            if (b本票)
                e.Graphics.DrawString(textBox_连续客票2.Text, ptFontEn, ptBrush, 125F + o.X, 30.8F + o.Y);
            //5.textBox_出票日期.Text                                       (123,23.5)  中
            if (b中心票)
                e.Graphics.DrawString(textBox_出票日期.Text, ptFontEn, ptBrush, 123F + o.X, 23.5F + o.Y);

            //6.textBox_始发_目的地.Text    (156,14.9)   中      (145,17)    本电
            if (b中心票)
                e.Graphics.DrawString(textBox_始发_目的地.Text, ptFontEn, ptBrush, 156F + o.X, 14.9F + o.Y);
            if (b本票)
                e.Graphics.DrawString(textBox_始发_目的地.Text, ptFontEn, ptBrush, 145F + o.X, 17F + o.Y);
            //7.textBox_订座编号.Text       (156,19.1)  中      (156,22.2)  本电
            if (b中心票)
                e.Graphics.DrawString(textBox_订座编号.Text, ptFontEn, ptBrush, 156F + o.X, 19.1F + o.Y);
            if (b本票)
                e.Graphics.DrawString(textBox_订座编号.Text, ptFontEn, ptBrush, 156F + o.X, 22.2F + o.Y);


            //多行文本，要进一步处理
            //8.textBox_出票地点.Text       (172,32)    中      (182,29.5)  本电
            string[] WherePrint = textBox_出票地点.Text.Split('\n');
            for (int i = WherePrint.Length - 1; i >= 0; i--)
            {
                float d_line = 3F;
                if(b中心票)
                    e.Graphics.DrawString(WherePrint[i], ptFontEn, ptBrush, 172F + o.X, 32F - (WherePrint.Length - 1 - i) * 3.3F + o.Y);
                if (b本票)
                    e.Graphics.DrawString(WherePrint[i], ptFontEn, ptBrush, 182F + o.X, 29.5F - (WherePrint.Length - 1 - i) * 3.3F + o.Y);
            }




            //9.textBox_换开凭证.Text       (156,23.4)  中      (156,26.5)  本
            if (b中心票) 
                e.Graphics.DrawString(textBox_换开凭证.Text, ptFontEn, ptBrush, 156F + o.X, 23.4F + o.Y);
            if (comboBox_票版类型.SelectedIndex == 3 || comboBox_票版类型.SelectedIndex == 0)
                e.Graphics.DrawString(textBox_换开凭证.Text, ptFontEn, ptBrush, 156F + o.X, 26.5F + o.Y);

            //10.textBox_旅游编号1.Text     (196,74.4)  中      (115,26.5)  本
            if (b中心票)
                e.Graphics.DrawString(textBox_旅游编号1.Text, ptFontEn, ptBrush, 196F + o.X, 74.4F + o.Y);
            if (comboBox_票版类型.SelectedIndex == 3 || comboBox_票版类型.SelectedIndex == 0)
                e.Graphics.DrawString(textBox_旅游编号1.Text, ptFontEn, ptBrush, 115F + o.X, 26.5F + o.Y);
            //11.textBox_行程单号.Text                          (125,26.5)  电
            if (comboBox_票版类型.SelectedIndex == 2)
                e.Graphics.DrawString(this.textBox_行程单编号.Text, ptFontEn, ptBrush, 125F + o.X, 26.5F + o.Y);

            //12.票价                       (48.2,61.8) 中      (41,64.6)   本电
            if (b中心票)
                e.Graphics.DrawString(this.textBox_票价.Text, ptFontEn, ptBrush, 48.2F + o.X, 61.8F + o.Y);
            if (b本票)
                e.Graphics.DrawString(textBox_票价.Text, ptFontEn, ptBrush, 41F + o.X, 64.6F + o.Y);
            //13.实付等值货币               (43,66)     中      (50,69)     本电
            if (b中心票)
                e.Graphics.DrawString(this.textBox_实付.Text, ptFontEn, ptBrush, 43F + o.X, 66F + o.Y);
            if (b本票)
                e.Graphics.DrawString(textBox_实付.Text, ptFontEn, ptBrush, 50F + o.X, 69F + o.Y);
            //14.税款                       (43,70.2)   中      (41,73.3)   本电
            if (b中心票)
                e.Graphics.DrawString(this.textBox_税款.Text, ptFontEn, ptBrush, 43F + o.X, 70.2F + o.Y);
            if (b本票)
                e.Graphics.DrawString(textBox_税款.Text, ptFontEn, ptBrush, 41F + o.X, 73.3F + o.Y);
            //15.燃油税                     (43,74.4)   中  
            if (b中心票)
                e.Graphics.DrawString(this.textBox_总数_燃油税.Text, ptFontEn, ptBrush, 43F + o.X, 74.4F + o.Y);
            //16.总数                       (50,78.6)   中      (41,77.5)   本电
            if (b中心票)
                e.Graphics.DrawString(this.textBox_付款方式_总数.Text, ptFontEn, ptBrush, 50F + o.X, 78.6F + o.Y);
            if (b本票)
                e.Graphics.DrawString(textBox_总数_燃油税.Text, ptFontEn, ptBrush, 41F + o.X, 77.5F + o.Y);

            //17.航空公司代理人             (50,82.8)   中      
            //无

            //18.付款方式                   (80,74.4)   中      (50,82)     本电
            if (b中心票)
                e.Graphics.DrawString(this.textBox_付款方式.Text, ptFontEn, ptBrush, 80F + o.X, 74.4F + o.Y);
            if (b本票)
                e.Graphics.DrawString(textBox_付款方式_总数.Text, ptFontEn, ptBrush, 50F + o.X, 82F + o.Y);
            //19.控制号                     (50,87)     中      (50,88)     本
            if (b中心票)
                e.Graphics.DrawString(this.textBox_控制号.Text, ptFontEn, ptBrush, 50F + o.X, 87F + o.Y);
            if (comboBox_票版类型.SelectedIndex == 3 || comboBox_票版类型.SelectedIndex == 0 || comboBox_票版类型.SelectedIndex == 2)
                e.Graphics.DrawString(this.textBox_控制号.Text, ptFontEn, ptBrush, 50F + o.X, 88F + o.Y);
            //20.票价计算                   (80,61.8)   中      (90,64.6)   本电
            if (b中心票)
                e.Graphics.DrawString(this.textBox_票价计算.Text, ptFontEn, ptBrush, 80F + o.X, 61.8F + o.Y);
            if (b本票)
                e.Graphics.DrawString(textBox_票价计算.Text, ptFontEn, ptBrush, 90F + o.X, 64.6F + o.Y);
            //21.航空公司代号               (83,84.3)   中      (85,84)  电
            if (comboBox_票版类型.SelectedIndex == 1)
                e.Graphics.DrawString(this.textBox_航空公司代号.Text, ptFontEn, ptBrush, 83F + o.X, 84.3F + o.Y);
            //22.客票顺序号                                     (96,84)  电
            //23.检查号                                         (127.8,84)  电
            if (comboBox_票版类型.SelectedIndex == 3 || comboBox_票版类型.SelectedIndex == 4)
            {
                //21,22,23
                e.Graphics.DrawString(this.textBox_航空公司代号.Text, ptFontEn, ptBrush, 85F + o.X, 84F + o.Y);
                e.Graphics.DrawString(this.textBox_客票号.Text, ptFontEn, ptBrush, 96F + o.X, 84F + o.Y);
                e.Graphics.DrawString(this.textBox_检查号.Text, ptFontEn, ptBrush, 127.8F + o.X, 84F + o.Y);
            }
            //24.货币现金收受               (144,84.3)  中
            //25.手续费率                   (196,84.3)  中
            //26.税款合计                   (206,84.3)  中
            if (comboBox_票版类型.SelectedIndex == 1)
            {
                e.Graphics.DrawString(this.textBox_货币收受.Text, ptFontEn, ptBrush, 144F + o.X, 84.3F + o.Y);
                e.Graphics.DrawString(this.textBox_手续费率.Text, ptFontEn, ptBrush, 196F + o.X, 84.3F + o.Y);
                e.Graphics.DrawString(this.textBox_税款合计.Text, ptFontEn, ptBrush, 206F + o.X, 84.3F + o.Y);
            }
            //航班信息
            if (b本票) o.Y += 2F;
            e.Graphics.DrawString(textBox101.Text, ptFontCn, ptBrush, 45F + o.X, 40F + o.Y);
            e.Graphics.DrawString(textBox102.Text, ptFontEn, ptBrush, 72F + o.X, 40F + o.Y);
            e.Graphics.DrawString(textBox103.Text, ptFontEn, ptBrush, 83F + o.X, 40F + o.Y);
            e.Graphics.DrawString(textBox104.Text, ptFontEn, ptBrush, 95F + o.X, 40F + o.Y);
            e.Graphics.DrawString(textBox105.Text, ptFontEn, ptBrush, 108F + o.X, 40F + o.Y);
            e.Graphics.DrawString(textBox106.Text, ptFontEn, ptBrush, 114F + o.X, 40F + o.Y);
            e.Graphics.DrawString(textBox107.Text, ptFontEn, ptBrush, 128F + o.X, 40F + o.Y);
            e.Graphics.DrawString(textBox108.Text, ptFontEn, ptBrush, 142F + o.X, 40F + o.Y);
            e.Graphics.DrawString(textBox109.Text, ptFontEn, ptBrush, 151F + o.X, 40F + o.Y);
            e.Graphics.DrawString(textBox10a.Text, ptFontEn, ptBrush, 188F + o.X, 40F + o.Y);
            e.Graphics.DrawString(textBox10b.Text, ptFontEn, ptBrush, 201F + o.X, 40F + o.Y);
            e.Graphics.DrawString(textBox10c.Text, ptFontEn, ptBrush, 214F + o.X, 40F + o.Y);

            e.Graphics.DrawString(textBox201.Text, ptFontCn, ptBrush, 45F + o.X, 49F + o.Y);
            e.Graphics.DrawString(textBox202.Text, ptFontEn, ptBrush, 72F + o.X, 49F + o.Y);
            e.Graphics.DrawString(textBox203.Text, ptFontEn, ptBrush, 83F + o.X, 49F + o.Y);
            e.Graphics.DrawString(textBox204.Text, ptFontEn, ptBrush, 95F + o.X, 49F + o.Y);
            e.Graphics.DrawString(textBox205.Text, ptFontEn, ptBrush, 108F + o.X, 49F + o.Y);
            e.Graphics.DrawString(textBox206.Text, ptFontEn, ptBrush, 114F + o.X, 49F + o.Y);
            e.Graphics.DrawString(textBox207.Text, ptFontEn, ptBrush, 128F + o.X, 49F + o.Y);
            e.Graphics.DrawString(textBox208.Text, ptFontEn, ptBrush, 142F + o.X, 49F + o.Y);
            e.Graphics.DrawString(textBox209.Text, ptFontEn, ptBrush, 151F + o.X, 49F + o.Y);
            e.Graphics.DrawString(textBox20a.Text, ptFontEn, ptBrush, 188F + o.X, 49F + o.Y);
            e.Graphics.DrawString(textBox20b.Text, ptFontEn, ptBrush, 201F + o.X, 49F + o.Y);
            e.Graphics.DrawString(textBox20c.Text, ptFontEn, ptBrush, 214F + o.X, 49F + o.Y);

            e.Graphics.DrawString(textBox301.Text, ptFontCn, ptBrush, 45F + o.X, 58F + o.Y);
            e.Graphics.DrawString(textBox302.Text, ptFontCn, ptBrush, 72F + o.X, 58F + o.Y);


        }

        private void button_模板管理_Click(object sender, EventArgs e)
        {
            ModelMangementForm form = new ModelMangementForm();
            form.ShowDialog();
            form.Dispose();
        }

        private void listBox_姓名组_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox_姓名组.SelectedItem!=null)
                textBox_旅客姓名.Text = listBox_姓名组.SelectedItem.ToString();
            Context.textBox_人数.Text = string.Format("共{0}人，选中{1}人", Context.listBox_姓名组.Items.Count, Context.listBox_姓名组.SelectedItems.Count);
        }

        private void comboBox_出票模板_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sqlstring = string.Format("select * from ModelList where 票版类型='{0}' and 出票模板='{1}'", comboBox_票版类型.Text,comboBox_出票模板.Text);
            DataTable dt = ShortCutKeySettingsForm.dataBaseProcess.ExcuteQuery(sqlstring);
            textBox_出票地点.Text = dt.Rows[0][3].ToString();
            if(comboBox_票版类型.SelectedIndex ==0||comboBox_票版类型.SelectedIndex ==2||comboBox_票版类型.SelectedIndex ==3)
                textBox_出票地点.Text =EagleAPI.GetDatePrint() +"\r\n" + dt.Rows[0][3].ToString();


        }

        private void comboBox_航空公司_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (comboBox_航空公司.SelectedItem == null) return;
            if (comboBox_航空公司.SelectedItem.ToString().Length < 2) return;
            switchairline(comboBox_航空公司.SelectedItem.ToString().Substring(0, 2));
        }
        public void switchairline(string al)
        {
            textBox_填开单位.Text = EagleAPI.GetAirLineEnglishName(al);
        }

        private void textBox_旅游编号2_TextChanged(object sender, EventArgs e)
        {
            textBox_旅游编号1.Text = textBox_旅游编号2.Text;
        }

        private void textBox_旅游编号1_TextChanged(object sender, EventArgs e)
        {
            textBox_旅游编号2.Text = textBox_旅游编号1.Text;
        }

        private void textBox_连续客票1_TextChanged(object sender, EventArgs e)
        {
            textBox_连续客票2.Text = textBox_连续客票1.Text;
        }

        private void textBox_连续客票2_TextChanged(object sender, EventArgs e)
        {
            textBox_连续客票1.Text = textBox_连续客票2.Text;
        }

        private void button_删除_Click(object sender, EventArgs e)
        {

        }

        private void button_打印设置_Click(object sender, EventArgs e)
        {
            PrintSetup ps = new PrintSetup();
            ps.ShowDialog();
        }

        private void textBox_总数_燃油税_Enter(object sender, EventArgs e)
        {
            if (comboBox_票版类型.SelectedIndex == 0 || comboBox_票版类型.SelectedIndex == 2 || comboBox_票版类型.SelectedIndex == 3)
            {
                //textBox_总数_燃油税 = 票价 + 税款
                try
                {
                    float a = float.Parse(textBox_票价.Text.Trim().Substring(3).Trim());
                    int ib = textBox_税款.Text.Trim().IndexOf("CN");
                    int ic = textBox_税款.Text.Trim().IndexOf("YQ");
                    float b = float.Parse(textBox_税款.Text.Trim().Substring(ib+2,ic-ib-2));
                    float c = float.Parse(textBox_税款.Text.Trim().Substring(ic+2));
                    float t = a+b+c;
                    textBox_总数_燃油税.Text = "CNY " + t.ToString("n");
                }
                catch
                {
                }
            }
        }

        private void textBox_付款方式_总数_Enter(object sender, EventArgs e)
        {
            if (comboBox_票版类型.SelectedIndex == 1 || comboBox_票版类型.SelectedIndex == 4)
            {
                //textBox_付款方式_总数 = 票价 + 税款 + 总数_燃油税
                try
                {
                    float a = float.Parse(textBox_票价.Text.Trim().Substring(3).Trim());
                    float b = float.Parse(textBox_税款.Text.Trim().Substring(2).Trim());
                    float c = float.Parse(textBox_总数_燃油税.Text.Trim().Substring(2).Trim());
                    float t = a + b + c;
                    textBox_付款方式_总数.Text = "CNY " + t.ToString("n");
                }
                catch
                {
                }

            }
        }

    }
}