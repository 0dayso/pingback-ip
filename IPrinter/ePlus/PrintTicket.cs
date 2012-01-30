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
            comboBox_Ʊ������.SelectedIndex = 0;
            init_restrictions();
            if (comboBox_ǩע.Items.Count > 0) comboBox_ǩע.SelectedIndex = 0;
            retstring = "";
            this.ActiveControl = this.textBox_�������;
        }

        //0.��ʾ���пؼ�
        private void Print0_ShowAll()
        {
            panel1.Visible = true;
         checkBox1.Visible = true;
         textBox_����.Visible = true;
         textBox_��Ʊ�ص�.Visible = true;
         textBox_����ƾ֤.Visible = true;
         textBox_�������.Visible = true;
         textBox_������Ʊ2.Visible = true;
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
        textBox_���ƺ�.Visible = true;
        textBox_����_ȼ��˰.Visible = true;
        textBox_���ʽ_����.Visible = true;
        textBox_˰��.Visible = true;
        textBox_ʵ��.Visible = true;
        textBox_Ʊ�ۼ���.Visible = true;
        textBox_Ʊ��.Visible = true;
        textBox_checkbox.Visible = true;
        textBox301.Visible = true;
        textBox201.Visible = true;
        textBox101.Visible = true;
        textBox_���α��1.Visible = true;
        textBox_ʼ��_Ŀ�ĵ�.Visible = true;
        textBox_�ÿ�����.Visible = true;
        label9.Visible = true;
        label6.Visible = true;
        label5.Visible = true;
        label_����ƾ֤.Visible = true;
        label_������Ʊ2.Visible = true;
        label_���α��1.Visible = true;
        label3.Visible = true;
        label11.Visible = true;
        label_���ƺ�.Visible = true;
        label_���ʽ_����.Visible = true;
        label_����_ȼ��˰.Visible = true;
        label14.Visible = true;
        label13.Visible = true;
        label18.Visible = true;
        label12.Visible = true;
        label_�ÿ�����.Visible = true;
        label_����Ʊ.Visible = true;
        label_ǩע.Visible = true;
        button_�޸�.Visible = true;
        button_������ӡ.Visible = true;
        button_ɾ��.Visible = true;
        button_ǩעά��.Visible = true;
        comboBox_��Ʊģ��.Visible = true;
        comboBox_ǩע.Visible = true;
        textBox_˰��ϼ�.Visible = true;
        textBox_��������.Visible = true;
        textBox_��������.Visible = true;
        label_���չ�˾����.Visible = true;
        label_��������.Visible = true;
        label_��������.Visible = true;
        label_˰��ϼ�.Visible = true;
        textBox_������Ʊ1.Visible = true;
        textBox_��Ʊ����.Visible = true;
        textBox_���λ.Visible = true;
        label_������Ʊ1.Visible = true;
        label_��Ʊ����.Visible = true;
        label_���λ.Visible = true;
        label_���չ�˾.Visible = true;
        comboBox_���չ�˾.Visible = true;
        textBox_���ʽ.Visible = true;
        textBox_���α��2.Visible = true;
        label_���α��2.Visible = true;
        label_���ʽ.Visible = true;
        label_����Ʊ.Visible = true;
        textBox_�г̵����.Visible = true;
        label_�г̵����.Visible = true;
        label_�Ϻ�.Visible = true;
        textBox_����.Visible = true;
        textBox_��Ʊ��.Visible = true;
        textBox_���չ�˾����.Visible = true;
        label_��_��_��.Visible = true;
        label_����.Visible = true;
        comboBox_Ʊ������.Visible = true;
        radioButton_С����.Visible = true;
        radioButton_�����.Visible = true;
        button_���.Visible = true;
        button_��ӡ.Visible = true;
        button_�˳�.Visible = true;
        label20.Visible = true;
        label1.Visible = true;
        listBox_������.Visible = true;
        label_��ЭƱ.Visible = true;

        label_����_ȼ��˰.Text = "�ܡ�����";
        textBox_����_ȼ��˰.Text = "CNY";
        label_���ʽ_����.Text = "���ʽ";
        textBox_���ʽ_����.Text = "CASH(CNY)";

        radioButton_С����.Checked = true;
        radioButton_�����.Checked = false;
        }

        //1.��ʾ����Ʊ�ؼ�
        private void Print0_show1()
        {
            Print0_ShowAll();

            label_���չ�˾.Visible = false;
            comboBox_���չ�˾.Visible = false;
            label_������Ʊ1.Visible = false;
            textBox_������Ʊ1.Visible = false;

            label_�Ϻ�.Visible = false;
            label_����.Visible = false;
            label_����Ʊ.Visible = false;
            label_��ЭƱ.Visible = false;
            label_���λ.Visible = false;
            textBox_���λ.Visible = false;

            label_��Ʊ����.Visible = false;
            textBox_��Ʊ����.Visible = false;
            label_�г̵����.Visible = false;
            textBox_�г̵����.Visible = false;

            label_���ʽ.Visible = false;
            textBox_���ʽ.Visible = false;

            label_���α��2.Visible = false;
            textBox_���α��2.Visible = false;

            label_��_��_��.Visible = false;
            label_���չ�˾����.Visible = false;
            textBox_���չ�˾����.Visible = false;
            textBox_��Ʊ��.Visible = false;
            textBox_����.Visible = false;

            label_��������.Visible = false;
            textBox_��������.Visible = false;

            label_��������.Visible = false;
            textBox_��������.Visible = false;

            label_˰��ϼ�.Visible = false;
            textBox_˰��ϼ�.Visible = false;
        }

        //2.��ʾ����Ʊ�ؼ�
        private void Print0_show2()
        {
            Print0_ShowAll();
            label_����Ʊ.Visible = false;
            label_�Ϻ�.Visible = false;
            label_����.Visible = false;
            label_��ЭƱ.Visible = false;

            label_���α��1.Visible = false;
            textBox_���α��1.Visible = false;

            label_������Ʊ2.Visible = false;
            textBox_������Ʊ2.Visible = false;

            label_�г̵����.Visible = false;
            textBox_�г̵����.Visible = false;

            label_��_��_��.Visible = false;
            textBox_��Ʊ��.Visible = false;
            textBox_����.Visible = false;

            label_����_ȼ��˰.Text = "ȼ �� ˰";
            textBox_����_ȼ��˰.Text = "YQ";
            label_���ʽ_����.Text = "�ܡ�����";
            textBox_���ʽ_����.Text = "CNY";


        }

        //3.��ʾ�Ϻ����ӿ�Ʊ�ؼ�
        private void Print0_show3()
        {
            Print0_ShowAll();
            label_����Ʊ.Visible = false;
            label_����Ʊ.Visible = false;
            label_����.Visible = false;
            label_��ЭƱ.Visible = false;

            label_���չ�˾.Visible = false;
            comboBox_���չ�˾.Visible = false;
            label_������Ʊ1.Visible = false;
            textBox_������Ʊ1.Visible = false;

            label_���α��1.Visible = false;
            label_���α��2.Visible = false;
            textBox_���α��1.Visible = false;
            textBox_���α��2.Visible = false;

            label_���ʽ.Visible = false;
            textBox_���ʽ.Visible = false;
            label_��_��_��.Visible = false;
            label_���չ�˾����.Visible = false;
            textBox_���չ�˾����.Visible = false;
            textBox_��Ʊ��.Visible = false;
            textBox_����.Visible = false;

            label_��������.Visible = false;
            textBox_��������.Visible = false;

            label_��������.Visible = false;
            textBox_��������.Visible = false;

            label_˰��ϼ�.Visible = false;
            textBox_˰��ϼ�.Visible = false;

            label_���λ.Visible = false;
            textBox_���λ.Visible = false;

            label_��Ʊ����.Visible = false;
            textBox_��Ʊ����.Visible = false;

            label_����ƾ֤.Visible = false;
            textBox_����ƾ֤.Visible = false;


        }

        //4.��ʾ�������ӿ�Ʊ�ؼ�
        private void Print0_show4()
        {
            Print0_ShowAll();
            label_���չ�˾.Visible = false;
            comboBox_���չ�˾.Visible = false;
            label_������Ʊ1.Visible = false;
            textBox_������Ʊ1.Visible = false;

            label_�Ϻ�.Visible = false;
            label_����Ʊ.Visible = false;
            label_����Ʊ.Visible = false;
            label_��ЭƱ.Visible = false;
            label_���λ.Visible = false;
            textBox_���λ.Visible = false;

            label_��Ʊ����.Visible = false;
            textBox_��Ʊ����.Visible = false;
            label_�г̵����.Visible = false;
            textBox_�г̵����.Visible = false;

            label_���ʽ.Visible = false;
            textBox_���ʽ.Visible = false;

            label_���α��2.Visible = false;
            textBox_���α��2.Visible = false;

            label_��������.Visible = false;
            textBox_��������.Visible = false;

            label_��������.Visible = false;
            textBox_��������.Visible = false;

            label_˰��ϼ�.Visible = false;
            textBox_˰��ϼ�.Visible = false;
        }

        //5.��ʾ��Э���ӿ�Ʊ�ؼ�
        private void Print0_show5()
        {
            Print0_ShowAll();
            label_����Ʊ.Visible = false;
            label_�Ϻ�.Visible = false;
            label_����.Visible = false;
            label_����Ʊ.Visible = false;

            label_���α��1.Visible = false;
            textBox_���α��1.Visible = false;

            label_������Ʊ2.Visible = false;
            textBox_������Ʊ2.Visible = false;

            label_�г̵����.Visible = false;
            textBox_�г̵����.Visible = false;

            label_���չ�˾����.Visible = false;

            label_����_ȼ��˰.Text = "ȼ �� ˰";
            textBox_����_ȼ��˰.Text = "YQ";
            label_���ʽ_����.Text = "�ܡ�����";
            textBox_���ʽ_����.Text = "CNY";

            label_��������.Visible = false;
            textBox_��������.Visible = false;

            label_��������.Visible = false;
            textBox_��������.Visible = false;

            label_˰��ϼ�.Visible = false;
            textBox_˰��ϼ�.Visible = false;

            label_���ƺ�.Visible = false;
            textBox_���ƺ�.Visible = false;
        }

        //6.�ı�Ʊ�����ͣ���ʾ��Ӧ��Ʊ
        private static bool b����Ʊ = false;
        private void comboBox_Ʊ������_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_Ʊ������.SelectedIndex == 0) Print0_show1();
            if (comboBox_Ʊ������.SelectedIndex == 1) Print0_show2();
            if (comboBox_Ʊ������.SelectedIndex == 2) Print0_show3();
            if (comboBox_Ʊ������.SelectedIndex == 3) Print0_show4();
            if (comboBox_Ʊ������.SelectedIndex == 4) Print0_show5();
            comboBox_��Ʊģ��.Items.Clear();

            string sqlstring = string.Format("select * from ModelList where Ʊ������='{0}'", comboBox_Ʊ������.Text);
            DataTable dt = ShortCutKeySettingsForm.dataBaseProcess.ExcuteQuery(sqlstring);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox_��Ʊģ��.Items.Add(dt.Rows[i][2].ToString());
            }
            if (comboBox_��Ʊģ��.Items.Count > 0)
                comboBox_��Ʊģ��.SelectedIndex = 0;
            if (retstring != "")
            {
                if (!b����Ʊ)//�ı�֮ǰΪ��Ʊ
                {
                    //�ı�֮��Ϊ����Ʊ
                    if (Context.comboBox_Ʊ������.SelectedIndex == 1 || Context.comboBox_Ʊ������.SelectedIndex == 4)
                    {
                        //Context.textBox_˰��.Text = "CN" + EagleAPI.GetTaxBuild(retstring).Substring(3);
                        //Context.textBox_����_ȼ��˰.Text = "YQ" + EagleAPI.GetTaxFuel(retstring).Substring(3);
                        //Context.textBox_���ʽ_����.Text = "CNY" + EagleAPI.GetTatol(retstring).Substring(3);
                        string temp = EagleAPI.GetTaxBuild(retstring);
                        if (temp.Length > 3)
                            Context.textBox_˰��.Text = "CN" + temp.Substring(3);
                        temp = EagleAPI.GetTaxFuel(retstring);
                        if (temp.Length > 3)
                            Context.textBox_����_ȼ��˰.Text = "YQ" + temp.Substring(3);
                        temp = EagleAPI.GetTatol(retstring);
                        if (temp.Length > 3)
                            Context.textBox_���ʽ_����.Text = "CNY" + temp.Substring(3);

                        textBox_���α��2.Text = textBox_���α��1.Text;
                        textBox_������Ʊ1.Text = textBox_������Ʊ2.Text;
                        b����Ʊ = true;
                    }
                }
                else//�ı�֮ǰΪ����Ʊ
                {
                    //�ı�֮��Ϊ��Ʊ
                    if (comboBox_Ʊ������.SelectedIndex == 2 || comboBox_Ʊ������.SelectedIndex == 3 || comboBox_Ʊ������.SelectedIndex == 0)
                    {
                        //Context.textBox_˰��.Text = "CN" + EagleAPI.GetTaxBuild(retstring).Substring(3) + " YQ" + EagleAPI.GetTaxFuel(retstring).Substring(3);
                        //Context.textBox_����_ȼ��˰.Text = "CNY" + EagleAPI.GetTatol(retstring).Substring(3);
                        //Context.textBox_���ʽ_����.Text = EagleAPI.GetFP(retstring);
                        string temp1 = EagleAPI.GetTaxBuild(retstring);
                        string temp2 = EagleAPI.GetTaxFuel(retstring);
                        if (temp1.Length > 3 && temp2.Length > 3)
                            Context.textBox_˰��.Text = "CN" + temp1.Substring(3) + " YQ" + temp2.Substring(3);
                        string temp = EagleAPI.GetTatol(retstring);
                        if (temp.Length > 3)
                            Context.textBox_����_ȼ��˰.Text = "CNY" + temp.Substring(3);
                        Context.textBox_���ʽ_����.Text = EagleAPI.GetFP(retstring);

                        textBox_���α��1.Text = textBox_���α��2.Text;
                        textBox_������Ʊ2.Text = textBox_������Ʊ1.Text;
                        b����Ʊ = false;
                    }
                }
            }

        }

        private void button_ǩעά��_Click(object sender, EventArgs e)
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

        //7.��ʼ��ǩע������
        private void init_restrictions()
        {
            FileStream fs = new FileStream(Application.StartupPath + "\\restictions.mp3", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs,System.Text.Encoding.GetEncoding("gb2312"));

            string sline = sr.ReadLine();
            while (sline != null)
            {
                comboBox_ǩע.Items.Add(sline);
                sline = sr.ReadLine();
            }
            sr.Close();
            fs.Close();            
        }

        private void comboBox_ǩע_Click(object sender, EventArgs e)
        {
            //comboBox_ǩע.Items.Clear();
            //init_restrictions();
            //if (comboBox_ǩע.Items.Count > 0) comboBox_ǩע.SelectedIndex = 0;
        }

        //8.���չ�˾��Ӧ���λ
        /*3U �Ĵ����չ�˾       CHINA SICHUAN AIRLINE
          BK �¿����չ�˾       OKAY AIRWAYS COMPANY LIMITED
          CA �й����ʺ��չ�˾   AIR CHINA
          CZ �й��Ϸ����չ�˾   CHINA SOUTHERN AIRLINES
          EU ӥ�����չ�˾       UNITED EAGLE AIRLINES
          FM �Ϻ����չ�˾       SHANGHAI AIRLINE
          HU ���Ϻ��չ�˾       HAINAN AIRLINES
          MF ���ź��չ�˾       XIAMEN AIRLINES
          MU �й��������չ�˾   CHINA EASTERN AIRLINES
          SC ɽ�����չ�˾       SHANDONG AIRLINE
          ZH ���ں��չ�˾       CHINA SHENZHEN AIRLINE*/
        private string airline_issued(string airlinecode)
        {
            return EagleAPI.GetAirLineEnglishName(airlinecode);
        }
        //�����¼���
        private static PrintTicket Context = null;
        public static string ReturnString
        {
            set
            {
                if (Context != null)
                {
                    
                    //׷�ӽ��
                    retstring += "\n" + connect_4_Command.AV_String;
                    //no pnr
                    if (retstring.Split('\n').Length < 4)
                    {
                        MessageBox.Show("NO PNR");
                        return;
                    }
                    //��ҳ
                    string plustemp = mystring.trim(retstring);
                    if (plustemp[plustemp.Length - 1] == '+' && retstring.IndexOf("*THIS PNR WAS ENTIRELY CANCELLED*") < 0)

                    {
                        //����PNָ��
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
                //��������retstring
                if (retstring.Split('\n').Length < 4 || retstring.IndexOf("NO PNR")>-1)
                {
                    MessageBox.Show("NO PNR");
                    return;
                }
                string temp = EagleAPI.GetNames(retstring)[0];
                if (Context.InvokeRequired)
                {
                    //ePlus.PrintTicket.Context.textBox_�ÿ�����.Text = temp;
                   // Context.textBox_ʼ��_Ŀ�ĵ�.Text = EagleAPI.GetStartCity(retstring) + "/" + EagleAPI.GetEndCity(retstring);
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
            Context.textBox_�ÿ�����.Text = names[0];
            for (int i = 0; i < names.Count; i++)
            {
                Context.listBox_������.Items.Add(names[i]);
            }

            Context.textBox_ʼ��_Ŀ�ĵ�.Text = EagleAPI.GetFromTo(retstring);
            Context.textBox_��Ʊ����.Text = EagleAPI.GetDatePrint();
            Context.textBox101.Text = EagleAPI.GetStartCityCn(retstring);
            Context.textBox102.Text = EagleAPI.GetStartCity(retstring);
            Context.textBox103.Text = EagleAPI.GetCarrier(retstring);
            //Context.comboBox_���չ�˾
            //Context.textBox_���λ
            string temp = Context.textBox103.Text;
            for (int i = 0; i < Context.comboBox_���չ�˾.Items.Count; i++)
            {
                if (Context.comboBox_���չ�˾.Items[i].ToString().Substring(0, 2) == temp)
                {
                    Context.comboBox_���չ�˾.SelectedIndex = i;
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
                Context.textBox_Ʊ��.Text = "CNY" + EagleAPI.GetFare(retstring).Substring(3);
            else Context.textBox_Ʊ��.Text = "CNY";
            Context.textBox_Ʊ�ۼ���.Text = EagleAPI.GetFareCal(retstring);
            if (Context.comboBox_Ʊ������.SelectedIndex == 1 || Context.comboBox_Ʊ������.SelectedIndex == 4)
            {
                temp = EagleAPI.GetTaxBuild(retstring);
                if (temp.Length > 3)
                    Context.textBox_˰��.Text = "CN" + temp.Substring(temp[3] > '9' ? 0 : 3);
                else Context.textBox_˰��.Text = "CN";
                temp = EagleAPI.GetTaxFuel(retstring);
                if (temp.Length > 3)
                    Context.textBox_����_ȼ��˰.Text = "YQ" + temp.Substring(3);
                else Context.textBox_����_ȼ��˰.Text = "YQ";
                temp = EagleAPI.GetTatol(retstring);
                if (temp.Length > 3)
                    Context.textBox_���ʽ_����.Text = "CNY" + temp.Substring(3);
                else Context.textBox_���ʽ_����.Text = "CNY";
            }
            else
            {
                string temp1 = EagleAPI.GetTaxBuild(retstring);
                string temp2 = EagleAPI.GetTaxFuel(retstring);
                if(temp1.Length>3 &&temp2.Length>3)
                    Context.textBox_˰��.Text = "CN" + temp1.Substring(temp1[3]>'9'?0:3) + " YQ" + temp2.Substring(3);
                temp = EagleAPI.GetTatol(retstring);
                if (temp.Length > 3)
                    Context.textBox_����_ȼ��˰.Text = "CNY" + temp.Substring(3);
                else Context.textBox_����_ȼ��˰.Text = "CNY";
                Context.textBox_���ʽ_����.Text = EagleAPI.GetFP(retstring);
            }
            Context.textBox_���ʽ.Text = EagleAPI.GetFP(retstring);

            Context.textBox_����.Text = string.Format("��{0}�ˣ�ѡ��{1}��", Context.listBox_������.Items.Count, Context.listBox_������.SelectedItems.Count);

            Context.textBox_���չ�˾����.Text = EagleAPI.GetAirlineCode(retstring);

            Context.textBox_��������.Text = EagleAPI.GetCash(retstring);
            Context.textBox_��������.Text = EagleAPI.GetCommRate(retstring);
            Context.textBox_˰��ϼ�.Text = EagleAPI.GetTaxTotal(retstring);

            Context.textBox_�������.Text = Context.tempNO;
        }
        public string tempNO = "";
        private void textBox_�������_KeyUp(object sender, KeyEventArgs e)
        {
            
            if (e.KeyValue == 13)//�س�
            {
                this.textBox_�������.Text = this.textBox_�������.Text.ToUpper();
                retstring = "";
                EagleAPI.CLEARCMDLIST(3);

                button_���_Click(this.button_���, e);
                if (textBox_�������.Text.Trim().Length != 5) return;
                //a.����"rT"+textBox_�������
                EagleAPI.EagleSendCmd("rT:n/" + textBox_�������.Text.Trim());
                tempNO = this.textBox_�������.Text;
                this.textBox_�������.Text = GlobalVar.WaitString;
            }
        }

        private void button_���_Click(object sender, EventArgs e)
        {
            listBox_������.Items.Clear();
            textBox_checkbox.Text = "";
            //textBox_��Ʊ�ص�.Text = "";
            textBox_��Ʊ����.Text = "";
            //textBox_�������.Text = "";
            textBox_���ʽ.Text = "CASH(CNY)";
            
            textBox_���չ�˾����.Text = "";
            textBox_����ƾ֤.Text = "";
            textBox_��������.Text = "CNY";
            textBox_����.Text = "0";
            textBox_��Ʊ��.Text = "";
            textBox_���ƺ�.Text = "";
            textBox_������Ʊ1.Text = "";
            textBox_������Ʊ2.Text = "";
            textBox_�ÿ�����.Text = "";
            textBox_���α��1.Text = "";
            textBox_���α��2.Text = "";
            textBox_Ʊ��.Text = "CNY";
            textBox_Ʊ�ۼ���.Text = "";
            textBox_����.Text = "";
            textBox_ʵ��.Text = "";
            textBox_ʼ��_Ŀ�ĵ�.Text = "";
            textBox_��������.Text = "300";
            textBox_˰��.Text = "CN";
            textBox_˰��ϼ�.Text = "0.00";
            textBox_���λ.Text = "";
            textBox_�г̵����.Text = "";
            if (comboBox_Ʊ������.SelectedIndex == 0 || comboBox_Ʊ������.SelectedIndex == 2 || comboBox_Ʊ������.SelectedIndex == 3)
            {
                textBox_����_ȼ��˰.Text = "CNY";
                textBox_���ʽ_����.Text = "CASH(CNY)";
            }

            else
            {
                textBox_����_ȼ��˰.Text = "YQ";
                textBox_���ʽ_����.Text = "CNY";
            }
            textBox101.Text = textBox102.Text = textBox103.Text = textBox104.Text = textBox105.Text = textBox106.Text = textBox107.Text = textBox109.Text = textBox10a.Text = textBox10b.Text =  "";
            //textBox108.Text ="OK";
            //textBox10c.Text = "";
            textBox201.Text = textBox202.Text = textBox203.Text = textBox204.Text = textBox205.Text = textBox206.Text = textBox207.Text = textBox208.Text = textBox209.Text = textBox20a.Text = textBox20b.Text = textBox20c.Text = "";
            textBox301.Text = ""; 
        }

        private void button_�˳�_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_��ӡ_Click(object sender, EventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            pd.Document = ptDoc;

            if (!EagleAPI.PrinterSetup(ptDoc)) return;

            DialogResult dr = pd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //��ʼ��ӡ
                ptDoc_Print();
                //ptDoc.Print();                
            }
        }
        void ptDoc_Print()
        {
            if (!EagleAPI2.ExistFont("TEC"))
            {
                if (MessageBox.Show("δ�ҵ�������ӡ����,�Ƿ����?", "ע��", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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

            //e.PageSettings.PrinterSettings.PrintFileName = "��Ʊ��ӡ";
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

            //�������½�237,102��������
            Pen pen = new Pen (ptBrush);            
            //e.Graphics.DrawRectangle(pen, 0F, 0F, 237F, 102F);
            //e.Graphics.DrawLine(pen, 0F, 0F, 237F, 102F);
            bool b����Ʊ = (comboBox_Ʊ������.SelectedIndex == 1 || comboBox_Ʊ������.SelectedIndex == 4);
            bool b��Ʊ = (comboBox_Ʊ������.SelectedIndex == 2 || comboBox_Ʊ������.SelectedIndex == 3 || comboBox_Ʊ������.SelectedIndex == 0);

            //sample:e.Graphics.DrawString("KFC", ptFontEn, ptBrush, 0F, 0F);
            PointF o = new PointF();
            o = GlobalVar.o_ticket;
            if(comboBox_Ʊ������.SelectedIndex == 2)
            {
                o.X -= 25.5F;
                o.Y -= 1F;
            }
            if (comboBox_Ʊ������.SelectedIndex == 0 || comboBox_Ʊ������.SelectedIndex == 3)
            {
                o.Y -= 4F;
            }
            if (comboBox_Ʊ������.SelectedIndex == 1 || comboBox_Ʊ������.SelectedIndex == 4)
            {
                o.Y -= 5F;
            }
            ////2.comboBox_ǩע.SelectedText                                  (41,20)     �б���
            //e.Graphics.DrawString(comboBox_ǩע.Text, ptFontCn, ptBrush, 41F, 20F);
            ////3.textBox_�ÿ�����.Text                                       (41,28)     �б���
            //e.Graphics.DrawString(textBox_�ÿ�����.Text, ptFontCn, ptBrush, 41F, 28F);
            ////4.textBox_������Ʊ.Text                                       (115,12.5)  ��      (125,30.8)  ����
            //if(comboBox_Ʊ������.SelectedIndex ==1 ||comboBox_Ʊ������.SelectedIndex ==4)
            //    e.Graphics.DrawString(textBox_������Ʊ1.Text, ptFontEn, ptBrush, 115F, 12.5F);
            //if(comboBox_Ʊ������.SelectedIndex ==2 ||comboBox_Ʊ������.SelectedIndex ==3||comboBox_Ʊ������.SelectedIndex ==0)
            //    e.Graphics.DrawString(textBox_������Ʊ2.Text, ptFontEn, ptBrush, 125F, 30.8F);
            ////5.textBox_��Ʊ����.Text                                       (123,23.5)  ��
            //if(comboBox_Ʊ������.SelectedIndex ==1 ||comboBox_Ʊ������.SelectedIndex ==4)
            //    e.Graphics.DrawString(textBox_��Ʊ����.Text, ptFontEn, ptBrush, 123F, 23.5F);

            ////6.textBox_ʼ��_Ŀ�ĵ�.Text    (156,14.9)   ��      (145,17)    ����
            //if (comboBox_Ʊ������.SelectedIndex == 1 || comboBox_Ʊ������.SelectedIndex == 4)
            //    e.Graphics.DrawString(textBox_ʼ��_Ŀ�ĵ�.Text, ptFontEn, ptBrush, 156F, 14.9F);
            //if (comboBox_Ʊ������.SelectedIndex == 2 || comboBox_Ʊ������.SelectedIndex == 3 || comboBox_Ʊ������.SelectedIndex == 0)
            //    e.Graphics.DrawString(textBox_ʼ��_Ŀ�ĵ�.Text, ptFontEn, ptBrush, 145F, 17F);
            ////7.textBox_�������.Text       (156,19.1)  ��      (156,22.2)  ����
            //if (comboBox_Ʊ������.SelectedIndex == 1 || comboBox_Ʊ������.SelectedIndex == 4)
            //    e.Graphics.DrawString(textBox_�������.Text, ptFontEn, ptBrush, 156F, 19.1F);
            //if (comboBox_Ʊ������.SelectedIndex == 2 || comboBox_Ʊ������.SelectedIndex == 3 || comboBox_Ʊ������.SelectedIndex == 0)
            //    e.Graphics.DrawString(textBox_�������.Text, ptFontEn, ptBrush, 156F, 22.2F);


            ////�����ı���Ҫ��һ������
            ////8.textBox_��Ʊ�ص�.Text       (172,32)    ��      (182,29.5)  ����
            //string[] WherePrint = textBox_��Ʊ�ص�.Text.Split('\n');
            //for (int i = WherePrint.Length - 1; i >= 0; i--)
            //{
            //    float d_line = 3F;
            //    if (comboBox_Ʊ������.SelectedIndex == 1 || comboBox_Ʊ������.SelectedIndex == 4)
            //        e.Graphics.DrawString(WherePrint[i], ptFontEn, ptBrush, 172F, 32F - (WherePrint.Length - 1-i) * 3F);
            //    if (comboBox_Ʊ������.SelectedIndex == 2 || comboBox_Ʊ������.SelectedIndex == 3 || comboBox_Ʊ������.SelectedIndex == 0)
            //        e.Graphics.DrawString(WherePrint[i], ptFontEn, ptBrush, 182F, 29.5F - (WherePrint.Length - 1 - i) * 3F);
            //}




            ////9.textBox_����ƾ֤.Text       (156,23.4)  ��      (156,26.5)  ��
            //if (comboBox_Ʊ������.SelectedIndex == 1 || comboBox_Ʊ������.SelectedIndex == 4)
            //    e.Graphics.DrawString(textBox_����ƾ֤.Text, ptFontEn, ptBrush, 156F, 23.4F);
            //if (comboBox_Ʊ������.SelectedIndex == 3 || comboBox_Ʊ������.SelectedIndex == 0)
            //    e.Graphics.DrawString(textBox_����ƾ֤.Text, ptFontEn, ptBrush, 156F, 26.5F);

            ////10.textBox_���α��1.Text     (196,74.4)  ��      (115,26.5)  ��
            //if (comboBox_Ʊ������.SelectedIndex == 1 || comboBox_Ʊ������.SelectedIndex == 4)
            //    e.Graphics.DrawString(textBox_���α��1.Text, ptFontEn, ptBrush, 196F, 74.4F);
            //if (comboBox_Ʊ������.SelectedIndex == 3 || comboBox_Ʊ������.SelectedIndex == 0)
            //    e.Graphics.DrawString(textBox_���α��1.Text, ptFontEn, ptBrush, 115F, 26.5F);
            ////11.textBox_�г̵���.Text                          (125,30.8)  ��
            //if (comboBox_Ʊ������.SelectedIndex == 2)
            //    e.Graphics.DrawString(this.textBox_�г̵����.Text, ptFontEn, ptBrush, 125F, 30.8F);

            ////12.Ʊ��                       (48.2,61.8) ��      (41,64.6)   ����
            //if (comboBox_Ʊ������.SelectedIndex == 1 || comboBox_Ʊ������.SelectedIndex == 4)
            //    e.Graphics.DrawString(this.textBox_Ʊ��.Text, ptFontEn, ptBrush, 48.2F, 61.8F);
            //if (comboBox_Ʊ������.SelectedIndex == 2 || comboBox_Ʊ������.SelectedIndex == 3 || comboBox_Ʊ������.SelectedIndex == 0)
            //    e.Graphics.DrawString(textBox_Ʊ��.Text, ptFontEn, ptBrush, 41F, 64.6F);
            ////13.ʵ����ֵ����               (43,66)     ��      (50,69)     ����
            //if (comboBox_Ʊ������.SelectedIndex == 1 || comboBox_Ʊ������.SelectedIndex == 4)
            //    e.Graphics.DrawString(this.textBox_ʵ��.Text, ptFontEn, ptBrush, 43F, 66F);
            //if (comboBox_Ʊ������.SelectedIndex == 2 || comboBox_Ʊ������.SelectedIndex == 3 || comboBox_Ʊ������.SelectedIndex == 0)
            //    e.Graphics.DrawString(textBox_ʵ��.Text, ptFontEn, ptBrush, 50F, 69F);
            ////14.˰��                       (43,70.2)   ��      (41,73.3)   ����
            //if (comboBox_Ʊ������.SelectedIndex == 1 || comboBox_Ʊ������.SelectedIndex == 4)
            //    e.Graphics.DrawString(this.textBox_˰��.Text, ptFontEn, ptBrush, 43F, 70.2F);
            //if (comboBox_Ʊ������.SelectedIndex == 2 || comboBox_Ʊ������.SelectedIndex == 3 || comboBox_Ʊ������.SelectedIndex == 0)
            //    e.Graphics.DrawString(textBox_˰��.Text, ptFontEn, ptBrush, 41F, 73.3F);
            ////15.ȼ��˰                     (43,74.4)   ��  
            //if (comboBox_Ʊ������.SelectedIndex == 1 || comboBox_Ʊ������.SelectedIndex == 4)
            //    e.Graphics.DrawString(this.textBox_����_ȼ��˰.Text, ptFontEn, ptBrush, 43F, 74.4F);
            ////16.����                       (50,78.6)   ��      (41,77.5)   ����
            //if (comboBox_Ʊ������.SelectedIndex == 1 || comboBox_Ʊ������.SelectedIndex == 4)
            //    e.Graphics.DrawString(this.textBox_���ʽ_����.Text, ptFontEn, ptBrush, 50F, 78.6F);
            //if (comboBox_Ʊ������.SelectedIndex == 2 || comboBox_Ʊ������.SelectedIndex == 3 || comboBox_Ʊ������.SelectedIndex == 0)
            //    e.Graphics.DrawString(textBox_����_ȼ��˰.Text, ptFontEn, ptBrush, 41F, 77.5F);

            ////17.���չ�˾������             (50,82.8)   ��      
            ////��

            ////18.���ʽ                   (80,74.4)   ��      (50,82)     ����
            //if (comboBox_Ʊ������.SelectedIndex == 1 || comboBox_Ʊ������.SelectedIndex == 4)
            //    e.Graphics.DrawString(this.textBox_���ʽ.Text, ptFontEn, ptBrush, 80F, 74.4F);
            //if (comboBox_Ʊ������.SelectedIndex == 2 || comboBox_Ʊ������.SelectedIndex == 3 || comboBox_Ʊ������.SelectedIndex == 0)
            //    e.Graphics.DrawString(textBox_���ʽ_����.Text, ptFontEn, ptBrush, 50F, 82F);
            ////19.���ƺ�                     (50,87)     ��      (50,88)     ��
            //if (comboBox_Ʊ������.SelectedIndex == 1 || comboBox_Ʊ������.SelectedIndex == 4)
            //    e.Graphics.DrawString(this.textBox_���ƺ�.Text, ptFontEn, ptBrush, 50F, 87F);
            //if (comboBox_Ʊ������.SelectedIndex == 3 || comboBox_Ʊ������.SelectedIndex == 0)
            //    e.Graphics.DrawString(textBox_���ƺ�.Text, ptFontEn, ptBrush, 50F, 88F);
            ////20.Ʊ�ۼ���                   (80,61.8)   ��      (90,64.6)   ����
            //if (comboBox_Ʊ������.SelectedIndex == 1 || comboBox_Ʊ������.SelectedIndex == 4)
            //    e.Graphics.DrawString(this.textBox_Ʊ�ۼ���.Text, ptFontEn, ptBrush, 80F, 61.8F);
            //if (comboBox_Ʊ������.SelectedIndex == 2 || comboBox_Ʊ������.SelectedIndex == 3 || comboBox_Ʊ������.SelectedIndex == 0)
            //    e.Graphics.DrawString(textBox_Ʊ�ۼ���.Text, ptFontEn, ptBrush, 90F, 64.6F);
            ////21.���չ�˾����               (83,84.3)   ��      (85,84)  ��
            //if (comboBox_Ʊ������.SelectedIndex == 1 || comboBox_Ʊ������.SelectedIndex == 4)
            //    e.Graphics.DrawString(this.textBox_���չ�˾����.Text, ptFontEn, ptBrush, 83F, 84.3F);
            ////22.��Ʊ˳���                                     (96,84)  ��
            ////23.����                                         (127.8,84)  ��
            //if (comboBox_Ʊ������.SelectedIndex == 2)
            //{
            //    e.Graphics.DrawString(this.textBox_���չ�˾����.Text, ptFontEn, ptBrush, 85F, 84F);
            //    e.Graphics.DrawString(this.textBox_��Ʊ��.Text, ptFontEn, ptBrush, 96F, 84F);
            //    e.Graphics.DrawString(this.textBox_����.Text, ptFontEn, ptBrush, 127.8F, 84F);
            //}
            ////24.�����ֽ�����               (144,84.3)  ��
            ////25.��������                   (196,84.3)  ��
            ////26.˰��ϼ�                   (206,84.3)  ��
            //if (comboBox_Ʊ������.SelectedIndex == 1)
            //{
            //    e.Graphics.DrawString(this.textBox_��������.Text, ptFontEn, ptBrush, 144F, 84.3F);
            //    e.Graphics.DrawString(this.textBox_��������.Text, ptFontEn, ptBrush, 196F, 84.3F);
            //    e.Graphics.DrawString(this.textBox_˰��ϼ�.Text, ptFontEn, ptBrush, 206F, 84.3F);
            //}
            ////������Ϣ
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
            //1.textBox_���λ.Text                                       (41,12.5)   ��
            if (comboBox_Ʊ������.SelectedIndex == 1 || comboBox_Ʊ������.SelectedIndex == 4)
                e.Graphics.DrawString(textBox_���λ.Text, ptFontEn, ptBrush, 41F + o.X, 12.5F + o.Y);

            //2.comboBox_ǩע.SelectedText                                  (41,20)     �б���
            if (b����Ʊ)
                e.Graphics.DrawString(comboBox_ǩע.Text, ptFontCn, ptBrush, 40F + o.X, 19F + o.Y);
            if (b��Ʊ)
                e.Graphics.DrawString(comboBox_ǩע.Text, ptFontCn, ptBrush, 40F + o.X, 22F + o.Y);
            //3.textBox_�ÿ�����.Text                                       (41,28)     �б���
            if (b����Ʊ)
                e.Graphics.DrawString(textBox_�ÿ�����.Text, ptFontCn, ptBrush, 41F + o.X, 27.4F + o.Y);
            if (b��Ʊ)
                e.Graphics.DrawString(textBox_�ÿ�����.Text, ptFontCn, ptBrush, 41F + o.X, 30F + o.Y);
            //4.textBox_������Ʊ.Text                                       (115,12.5)  ��      (125,30.8)  ����
            if (b����Ʊ)
                e.Graphics.DrawString(textBox_������Ʊ1.Text, ptFontEn, ptBrush, 115F + o.X, 12.5F + o.Y);
            if (b��Ʊ)
                e.Graphics.DrawString(textBox_������Ʊ2.Text, ptFontEn, ptBrush, 125F + o.X, 30.8F + o.Y);
            //5.textBox_��Ʊ����.Text                                       (123,23.5)  ��
            if (b����Ʊ)
                e.Graphics.DrawString(textBox_��Ʊ����.Text, ptFontEn, ptBrush, 123F + o.X, 23.5F + o.Y);

            //6.textBox_ʼ��_Ŀ�ĵ�.Text    (156,14.9)   ��      (145,17)    ����
            if (b����Ʊ)
                e.Graphics.DrawString(textBox_ʼ��_Ŀ�ĵ�.Text, ptFontEn, ptBrush, 156F + o.X, 14.9F + o.Y);
            if (b��Ʊ)
                e.Graphics.DrawString(textBox_ʼ��_Ŀ�ĵ�.Text, ptFontEn, ptBrush, 145F + o.X, 17F + o.Y);
            //7.textBox_�������.Text       (156,19.1)  ��      (156,22.2)  ����
            if (b����Ʊ)
                e.Graphics.DrawString(textBox_�������.Text, ptFontEn, ptBrush, 156F + o.X, 19.1F + o.Y);
            if (b��Ʊ)
                e.Graphics.DrawString(textBox_�������.Text, ptFontEn, ptBrush, 156F + o.X, 22.2F + o.Y);


            //�����ı���Ҫ��һ������
            //8.textBox_��Ʊ�ص�.Text       (172,32)    ��      (182,29.5)  ����
            string[] WherePrint = textBox_��Ʊ�ص�.Text.Split('\n');
            for (int i = WherePrint.Length - 1; i >= 0; i--)
            {
                float d_line = 3F;
                if(b����Ʊ)
                    e.Graphics.DrawString(WherePrint[i], ptFontEn, ptBrush, 172F + o.X, 32F - (WherePrint.Length - 1 - i) * 3.3F + o.Y);
                if (b��Ʊ)
                    e.Graphics.DrawString(WherePrint[i], ptFontEn, ptBrush, 182F + o.X, 29.5F - (WherePrint.Length - 1 - i) * 3.3F + o.Y);
            }




            //9.textBox_����ƾ֤.Text       (156,23.4)  ��      (156,26.5)  ��
            if (b����Ʊ) 
                e.Graphics.DrawString(textBox_����ƾ֤.Text, ptFontEn, ptBrush, 156F + o.X, 23.4F + o.Y);
            if (comboBox_Ʊ������.SelectedIndex == 3 || comboBox_Ʊ������.SelectedIndex == 0)
                e.Graphics.DrawString(textBox_����ƾ֤.Text, ptFontEn, ptBrush, 156F + o.X, 26.5F + o.Y);

            //10.textBox_���α��1.Text     (196,74.4)  ��      (115,26.5)  ��
            if (b����Ʊ)
                e.Graphics.DrawString(textBox_���α��1.Text, ptFontEn, ptBrush, 196F + o.X, 74.4F + o.Y);
            if (comboBox_Ʊ������.SelectedIndex == 3 || comboBox_Ʊ������.SelectedIndex == 0)
                e.Graphics.DrawString(textBox_���α��1.Text, ptFontEn, ptBrush, 115F + o.X, 26.5F + o.Y);
            //11.textBox_�г̵���.Text                          (125,26.5)  ��
            if (comboBox_Ʊ������.SelectedIndex == 2)
                e.Graphics.DrawString(this.textBox_�г̵����.Text, ptFontEn, ptBrush, 125F + o.X, 26.5F + o.Y);

            //12.Ʊ��                       (48.2,61.8) ��      (41,64.6)   ����
            if (b����Ʊ)
                e.Graphics.DrawString(this.textBox_Ʊ��.Text, ptFontEn, ptBrush, 48.2F + o.X, 61.8F + o.Y);
            if (b��Ʊ)
                e.Graphics.DrawString(textBox_Ʊ��.Text, ptFontEn, ptBrush, 41F + o.X, 64.6F + o.Y);
            //13.ʵ����ֵ����               (43,66)     ��      (50,69)     ����
            if (b����Ʊ)
                e.Graphics.DrawString(this.textBox_ʵ��.Text, ptFontEn, ptBrush, 43F + o.X, 66F + o.Y);
            if (b��Ʊ)
                e.Graphics.DrawString(textBox_ʵ��.Text, ptFontEn, ptBrush, 50F + o.X, 69F + o.Y);
            //14.˰��                       (43,70.2)   ��      (41,73.3)   ����
            if (b����Ʊ)
                e.Graphics.DrawString(this.textBox_˰��.Text, ptFontEn, ptBrush, 43F + o.X, 70.2F + o.Y);
            if (b��Ʊ)
                e.Graphics.DrawString(textBox_˰��.Text, ptFontEn, ptBrush, 41F + o.X, 73.3F + o.Y);
            //15.ȼ��˰                     (43,74.4)   ��  
            if (b����Ʊ)
                e.Graphics.DrawString(this.textBox_����_ȼ��˰.Text, ptFontEn, ptBrush, 43F + o.X, 74.4F + o.Y);
            //16.����                       (50,78.6)   ��      (41,77.5)   ����
            if (b����Ʊ)
                e.Graphics.DrawString(this.textBox_���ʽ_����.Text, ptFontEn, ptBrush, 50F + o.X, 78.6F + o.Y);
            if (b��Ʊ)
                e.Graphics.DrawString(textBox_����_ȼ��˰.Text, ptFontEn, ptBrush, 41F + o.X, 77.5F + o.Y);

            //17.���չ�˾������             (50,82.8)   ��      
            //��

            //18.���ʽ                   (80,74.4)   ��      (50,82)     ����
            if (b����Ʊ)
                e.Graphics.DrawString(this.textBox_���ʽ.Text, ptFontEn, ptBrush, 80F + o.X, 74.4F + o.Y);
            if (b��Ʊ)
                e.Graphics.DrawString(textBox_���ʽ_����.Text, ptFontEn, ptBrush, 50F + o.X, 82F + o.Y);
            //19.���ƺ�                     (50,87)     ��      (50,88)     ��
            if (b����Ʊ)
                e.Graphics.DrawString(this.textBox_���ƺ�.Text, ptFontEn, ptBrush, 50F + o.X, 87F + o.Y);
            if (comboBox_Ʊ������.SelectedIndex == 3 || comboBox_Ʊ������.SelectedIndex == 0 || comboBox_Ʊ������.SelectedIndex == 2)
                e.Graphics.DrawString(this.textBox_���ƺ�.Text, ptFontEn, ptBrush, 50F + o.X, 88F + o.Y);
            //20.Ʊ�ۼ���                   (80,61.8)   ��      (90,64.6)   ����
            if (b����Ʊ)
                e.Graphics.DrawString(this.textBox_Ʊ�ۼ���.Text, ptFontEn, ptBrush, 80F + o.X, 61.8F + o.Y);
            if (b��Ʊ)
                e.Graphics.DrawString(textBox_Ʊ�ۼ���.Text, ptFontEn, ptBrush, 90F + o.X, 64.6F + o.Y);
            //21.���չ�˾����               (83,84.3)   ��      (85,84)  ��
            if (comboBox_Ʊ������.SelectedIndex == 1)
                e.Graphics.DrawString(this.textBox_���չ�˾����.Text, ptFontEn, ptBrush, 83F + o.X, 84.3F + o.Y);
            //22.��Ʊ˳���                                     (96,84)  ��
            //23.����                                         (127.8,84)  ��
            if (comboBox_Ʊ������.SelectedIndex == 3 || comboBox_Ʊ������.SelectedIndex == 4)
            {
                //21,22,23
                e.Graphics.DrawString(this.textBox_���չ�˾����.Text, ptFontEn, ptBrush, 85F + o.X, 84F + o.Y);
                e.Graphics.DrawString(this.textBox_��Ʊ��.Text, ptFontEn, ptBrush, 96F + o.X, 84F + o.Y);
                e.Graphics.DrawString(this.textBox_����.Text, ptFontEn, ptBrush, 127.8F + o.X, 84F + o.Y);
            }
            //24.�����ֽ�����               (144,84.3)  ��
            //25.��������                   (196,84.3)  ��
            //26.˰��ϼ�                   (206,84.3)  ��
            if (comboBox_Ʊ������.SelectedIndex == 1)
            {
                e.Graphics.DrawString(this.textBox_��������.Text, ptFontEn, ptBrush, 144F + o.X, 84.3F + o.Y);
                e.Graphics.DrawString(this.textBox_��������.Text, ptFontEn, ptBrush, 196F + o.X, 84.3F + o.Y);
                e.Graphics.DrawString(this.textBox_˰��ϼ�.Text, ptFontEn, ptBrush, 206F + o.X, 84.3F + o.Y);
            }
            //������Ϣ
            if (b��Ʊ) o.Y += 2F;
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

        private void button_ģ�����_Click(object sender, EventArgs e)
        {
            ModelMangementForm form = new ModelMangementForm();
            form.ShowDialog();
            form.Dispose();
        }

        private void listBox_������_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox_������.SelectedItem!=null)
                textBox_�ÿ�����.Text = listBox_������.SelectedItem.ToString();
            Context.textBox_����.Text = string.Format("��{0}�ˣ�ѡ��{1}��", Context.listBox_������.Items.Count, Context.listBox_������.SelectedItems.Count);
        }

        private void comboBox_��Ʊģ��_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sqlstring = string.Format("select * from ModelList where Ʊ������='{0}' and ��Ʊģ��='{1}'", comboBox_Ʊ������.Text,comboBox_��Ʊģ��.Text);
            DataTable dt = ShortCutKeySettingsForm.dataBaseProcess.ExcuteQuery(sqlstring);
            textBox_��Ʊ�ص�.Text = dt.Rows[0][3].ToString();
            if(comboBox_Ʊ������.SelectedIndex ==0||comboBox_Ʊ������.SelectedIndex ==2||comboBox_Ʊ������.SelectedIndex ==3)
                textBox_��Ʊ�ص�.Text =EagleAPI.GetDatePrint() +"\r\n" + dt.Rows[0][3].ToString();


        }

        private void comboBox_���չ�˾_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (comboBox_���չ�˾.SelectedItem == null) return;
            if (comboBox_���չ�˾.SelectedItem.ToString().Length < 2) return;
            switchairline(comboBox_���չ�˾.SelectedItem.ToString().Substring(0, 2));
        }
        public void switchairline(string al)
        {
            textBox_���λ.Text = EagleAPI.GetAirLineEnglishName(al);
        }

        private void textBox_���α��2_TextChanged(object sender, EventArgs e)
        {
            textBox_���α��1.Text = textBox_���α��2.Text;
        }

        private void textBox_���α��1_TextChanged(object sender, EventArgs e)
        {
            textBox_���α��2.Text = textBox_���α��1.Text;
        }

        private void textBox_������Ʊ1_TextChanged(object sender, EventArgs e)
        {
            textBox_������Ʊ2.Text = textBox_������Ʊ1.Text;
        }

        private void textBox_������Ʊ2_TextChanged(object sender, EventArgs e)
        {
            textBox_������Ʊ1.Text = textBox_������Ʊ2.Text;
        }

        private void button_ɾ��_Click(object sender, EventArgs e)
        {

        }

        private void button_��ӡ����_Click(object sender, EventArgs e)
        {
            PrintSetup ps = new PrintSetup();
            ps.ShowDialog();
        }

        private void textBox_����_ȼ��˰_Enter(object sender, EventArgs e)
        {
            if (comboBox_Ʊ������.SelectedIndex == 0 || comboBox_Ʊ������.SelectedIndex == 2 || comboBox_Ʊ������.SelectedIndex == 3)
            {
                //textBox_����_ȼ��˰ = Ʊ�� + ˰��
                try
                {
                    float a = float.Parse(textBox_Ʊ��.Text.Trim().Substring(3).Trim());
                    int ib = textBox_˰��.Text.Trim().IndexOf("CN");
                    int ic = textBox_˰��.Text.Trim().IndexOf("YQ");
                    float b = float.Parse(textBox_˰��.Text.Trim().Substring(ib+2,ic-ib-2));
                    float c = float.Parse(textBox_˰��.Text.Trim().Substring(ic+2));
                    float t = a+b+c;
                    textBox_����_ȼ��˰.Text = "CNY " + t.ToString("n");
                }
                catch
                {
                }
            }
        }

        private void textBox_���ʽ_����_Enter(object sender, EventArgs e)
        {
            if (comboBox_Ʊ������.SelectedIndex == 1 || comboBox_Ʊ������.SelectedIndex == 4)
            {
                //textBox_���ʽ_���� = Ʊ�� + ˰�� + ����_ȼ��˰
                try
                {
                    float a = float.Parse(textBox_Ʊ��.Text.Trim().Substring(3).Trim());
                    float b = float.Parse(textBox_˰��.Text.Trim().Substring(2).Trim());
                    float c = float.Parse(textBox_����_ȼ��˰.Text.Trim().Substring(2).Trim());
                    float t = a + b + c;
                    textBox_���ʽ_����.Text = "CNY " + t.ToString("n");
                }
                catch
                {
                }

            }
        }

    }
}