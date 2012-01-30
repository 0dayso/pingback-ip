using System;
using System.Collections.Generic;
using System.Text;
using gs.para;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace ePlus.PrintHyx
{
    class PrintHyxPublic
    {
        static public void PnrTextBoxKeyUp(TextBox tb, ComboBox cb, KeyEventArgs e,ref string retstring)
        {
            if (e.KeyValue == 13)//回车
            {
                tb.Text = tb.Text.ToUpper();
                retstring = "";
                if (tb.Text.Trim().Length != 5) return;

                EagleAPI.CLEARCMDLIST(3);

                cb.Items.Clear();
                cb.Text = "请稍等……";

                EagleAPI.EagleSendCmd("rT:n/" + tb.Text.Trim());
            }

        }
        static public void PnrTextBoxKeyUp(TextBox tb, ComboBox cb, KeyEventArgs e, ref string retstring,string cmd)
        {
            if (e.KeyValue == 13)//回车
            {
                tb.Text = tb.Text.ToUpper();
                retstring = "";
                //if (tb.Text.Trim().Length != 5) return;

                EagleAPI.CLEARCMDLIST(3);

                cb.Items.Clear();
                cb.Text = "请稍等……";

                EagleAPI.EagleSendCmd(cmd);
            }

        }

        static public void GetRetString(ref string retstring,ref string rs)
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
            //if (retstring.IndexOf('+') > -1)// == "+\r\n")
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
        static public void GetRetString(ref string retstring, ref string rs,bool nopn)
        {
            //追加结果
            retstring += "\n" + connect_4_Command.AV_String;
            //no pnr
            if (retstring.Split('\n').Length < 4)
            {
                //MessageBox.Show("NO PNR");
                return;
            }
            //多页
            if (retstring.IndexOf('+') < -1)// == "+\r\n")//改为<-1，始张不会成立，即不会发送pn
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
        static public void PrintItems(string[] strs, PointF[] pts, PointF o, Font ft, Brush bs, System.Drawing.Printing.PrintPageEventArgs e)
        {
            for (int i = 0; i < strs.Length; i++)
                e.Graphics.DrawString(strs[i], ft, bs, pts[i].X + o.X, pts[i].Y + o.Y);            
        }
    }
    class picc
    {
        public string CardType;
        public string Signature;
        public int OffsetX;
        public int OffsetY;
        public string SaveNo;
    }
    class picc2
    {
        public string Signature;
        public string Conflict;
        public int OffsetX;
        public int OffsetY;
        public string SaveNo;
    }
    class cfg_yongan
    {
        public string eNumberHead;
        public string Signature;
        public int OffsetX;
        public int OffsetY;
        public string SaveNo;
        public void GetConfig()
        {
            FileStream fs = new FileStream(GlobalVar.s_configfile, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            NewPara np = new NewPara(temp);

            eNumberHead = np.FindTextByPath("//eg/YongAn/ENumberHead");
            Signature = np.FindTextByPath("//eg/YongAn/Signature");
            OffsetX = int.Parse(np.FindTextByPath("//eg/YongAn/OffsetX"));
            OffsetY = int.Parse(np.FindTextByPath("//eg/YongAn/OffsetY"));
            SaveNo = np.FindTextByPath("//eg/YongAn/SaveNo");
        }
    }
    class cfg_newchina
    {
        public string eNumberHead;
        public string Signature;
        public int OffsetX;
        public int OffsetY;
        public string SaveNo;
        public void GetConfig()
        {
            FileStream fs = new FileStream(GlobalVar.s_configfile, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            NewPara np = new NewPara(temp);

            eNumberHead = np.FindTextByPath("//eg/NewChina/ENumberHead");
            Signature = np.FindTextByPath("//eg/NewChina/Signature");
            OffsetX = int.Parse(np.FindTextByPath("//eg/NewChina/OffsetX"));
            OffsetY = int.Parse(np.FindTextByPath("//eg/NewChina/OffsetY"));
            SaveNo = np.FindTextByPath("//eg/NewChina/SaveNo");
        }
    }
    class cfg_sinosafe
    {
        public string ENumberHead;
        public string Signature;
        public int OffsetX;
        public int OffsetY;
        public string SaveNo;
        public string CompanyAddr;
        public string Phone;
        public void GetConfig()
        {
            FileStream fs = new FileStream(GlobalVar.s_configfile, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            NewPara np = new NewPara(temp);
            ENumberHead = np.FindTextByPath("//eg/SinoSafe/ENumberHead");
            Signature = np.FindTextByPath("//eg/SinoSafe/Signature");
            OffsetX = int.Parse(np.FindTextByPath("//eg/SinoSafe/OffsetX"));
            OffsetY = int.Parse(np.FindTextByPath("//eg/SinoSafe/OffsetY"));
            SaveNo = np.FindTextByPath("//eg/SinoSafe/SaveNo");
            CompanyAddr = np.FindTextByPath("//eg/SinoSafe/CompanyAddr");
            Phone = np.FindTextByPath("//eg/SinoSafe/Phone");
        }
    }
    class cfg_chinalife
    {
        public string ENumberHead;
        public int OffsetX;
        public int OffsetY;
        public string SaveNo;
        public string Phone;
        public string CompanyAddr;
        public string Signature;
        public void GetConfig()
        {
            FileStream fs = new FileStream(GlobalVar.s_configfile, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            NewPara np = new NewPara(temp);
            ENumberHead = np.FindTextByPath("//eg/ChinaLife/ENumberHead");
            Signature = np.FindTextByPath("//eg/ChinaLife/Signature");
            OffsetX = int.Parse(np.FindTextByPath("//eg/ChinaLife/OffsetX"));
            OffsetY = int.Parse(np.FindTextByPath("//eg/ChinaLife/OffsetY"));
            SaveNo = np.FindTextByPath("//eg/ChinaLife/SaveNo");
            CompanyAddr = np.FindTextByPath("//eg/ChinaLife/CompanyAddr");
            Phone = np.FindTextByPath("//eg/ChinaLife/Phone");

        }

    }
    class cfg_pingan01
    {
        public string ENumberHead;
        public int OffsetX;
        public int OffsetY;
        public string SaveNo;
        public string Phone;
        public string CompanyAddr;
        public string Signature;
        public void GetConfig()
        {
            FileStream fs = new FileStream(GlobalVar.s_configfile, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            NewPara np = new NewPara(temp);
            ENumberHead = np.FindTextByPath("//eg/PingAn01/ENumberHead");
            Signature = np.FindTextByPath("//eg/PingAn01/Signature");
            OffsetX = int.Parse(np.FindTextByPath("//eg/PingAn01/OffsetX"));
            OffsetY = int.Parse(np.FindTextByPath("//eg/PingAn01/OffsetY"));
            SaveNo = np.FindTextByPath("//eg/PingAn01/SaveNo");
            CompanyAddr = np.FindTextByPath("//eg/PingAn01/CompanyAddr");
            Phone = np.FindTextByPath("//eg/PingAn01/Phone");

        }

    }
    class cfg_insurance
    {
        public string ENumberHead;
        public int OffsetX;
        public int OffsetY;
        public string SaveNo;
        public string Phone;
        public string CompanyAddr;
        public string Signature;
        public string Term;
        public void GetConfig(string insCode)
        {
            if (insCode == "") return;
            FileStream fs = new FileStream(GlobalVar.s_configfile, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            NewPara np = new NewPara(temp);
            ENumberHead = np.FindTextByPath("//eg/" + insCode + "/ENumberHead");
            Signature = np.FindTextByPath("//eg/" + insCode + "/Signature");
            OffsetX = int.Parse(np.FindTextByPath("//eg/" + insCode + "/OffsetX"));
            OffsetY = int.Parse(np.FindTextByPath("//eg/" + insCode + "/OffsetY"));
            SaveNo = np.FindTextByPath("//eg/" + insCode + "/SaveNo");
            CompanyAddr = np.FindTextByPath("//eg/" + insCode + "/CompanyAddr");
            Phone = np.FindTextByPath("//eg/" + insCode + "/Phone");
            Term = np.FindTextByPath("//eg/" + insCode + "/Term");
        }

    }
    class cfg_dubang01
    {
        public string ENumberHead;
        public int OffsetX;
        public int OffsetY;
        public string SaveNo;
        public string Phone;
        public string CompanyAddr;
        public string Signature;
        public void GetConfig()
        {
            FileStream fs = new FileStream(GlobalVar.s_configfile, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            NewPara np = new NewPara(temp);
            ENumberHead = np.FindTextByPath("//eg/DuBang01/ENumberHead");
            Signature = np.FindTextByPath("//eg/DuBang01/Signature");
            OffsetX = int.Parse(np.FindTextByPath("//eg/DuBang01/OffsetX"));
            OffsetY = int.Parse(np.FindTextByPath("//eg/DuBang01/OffsetY"));
            SaveNo = np.FindTextByPath("//eg/DuBang01/SaveNo");
            CompanyAddr = np.FindTextByPath("//eg/DuBang01/CompanyAddr");
            Phone = np.FindTextByPath("//eg/DuBang01/Phone");

        }

    }
    class cfg_dubang02
    {
        public string ENumberHead;
        public int OffsetX;
        public int OffsetY;
        public string SaveNo;
        public string Phone;
        public string CompanyAddr;
        public string Signature;
        public string Term;
        public void GetConfig()
        {
            FileStream fs = new FileStream(GlobalVar.s_configfile, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            NewPara np = new NewPara(temp);
            ENumberHead = np.FindTextByPath("//eg/DuBang02/ENumberHead");
            Signature = np.FindTextByPath("//eg/DuBang02/Signature");
            OffsetX = int.Parse(np.FindTextByPath("//eg/DuBang02/OffsetX"));
            OffsetY = int.Parse(np.FindTextByPath("//eg/DuBang02/OffsetY"));
            SaveNo = np.FindTextByPath("//eg/DuBang02/SaveNo");
            CompanyAddr = np.FindTextByPath("//eg/DuBang02/CompanyAddr");
            Phone = np.FindTextByPath("//eg/DuBang02/Phone");
            Term = np.FindTextByPath("//eg/DuBang02/Term");

        }

    }
    class rightMenuHYX
    {
        Form sendForm = null;
        public rightMenuHYX(Form form)
        {
            sendForm = form;
        }
        public void closesendform()
        {
            sendForm.Close();
        }
        public void ShowHYXMenu(Control c,MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip rightMenu = new ContextMenuStrip();
                EventHandler eh = new EventHandler(admin);
                EventHandler eh1 = new EventHandler(printB01);
                EventHandler eh2 = new EventHandler(printB02);
                EventHandler eh3 = new EventHandler(printB03);
                EventHandler eh4 = new EventHandler(printB04);
                EventHandler eh5 = new EventHandler(print001);
                EventHandler eh6 = new EventHandler(print007);
                EventHandler eh7 = new EventHandler(print009);
                EventHandler eh8 = new EventHandler(printB05);
                EventHandler eh9 = new EventHandler(printB06);
                EventHandler eh10 = new EventHandler(printB07);

                if (GlobalVar2.bTempus) rightMenu.Items.Add("后台管理", null, eh);
                else
                {
                    if (Model.md.b_B01) rightMenu.Items.Add("华安－交通意外伤害保险单", null, eh1);
                    if (Model.md.b_B02) rightMenu.Items.Add("人寿－航空人身意外伤害保险单", null, eh2);
                    if (Model.md.b_B03) rightMenu.Items.Add("都邦－航翼网航意险", null, eh3);
                    if (Model.md.b_B04) rightMenu.Items.Add("都邦－出行无忧", null, eh4);
                    if (Model.md.b_001) rightMenu.Items.Add("PICC－任我游", null, eh5);
                    if (Model.md.b_007) rightMenu.Items.Add("永安－交意险", null, eh6);
                    if (Model.md.b_009) rightMenu.Items.Add("新华－保险打印", null, eh7);
                    if (Model.md.b_B05) rightMenu.Items.Add("都帮－出行乐", null, eh8);
                    //if (Model.md.b_B06) rightMenu.Items.Add("平安－周游列国", null, eh9);
                    if (Model.md.b_B07) rightMenu.Items.Add("阳光－一路阳光", null, eh10);
                    if (Model.md.b_B08) rightMenu.Items.Add("航翼网会员保险卡", null, new EventHandler(printB08));
                    if (Model.md.b_B09 || GlobalVar.printProgram) rightMenu.Items.Add("易格－新华人寿", null, new EventHandler(printB09));
                    if (Model.md.b_B0A) rightMenu.Items.Add("太平洋－航意险", null, new EventHandler(printB0A));
                    if (Model.md.b_B0B) rightMenu.Items.Add("安邦－商行通", null, new EventHandler(printB0B));
                    if (Model.md.b_B0C) rightMenu.Items.Add("人寿-航空意外险(2008)", null, new EventHandler(printB0C));
                    if(!GlobalVar.printProgram)
                        rightMenu.Items.Add("后台管理", null, eh);
                }
                rightMenu.Show(c,e.X, e.Y);
            }
        }
        private void printB01(object sender, EventArgs e)
        {//华安  
            closesendform();
            PrintHyx.SinoSafe dlg = new SinoSafe();
            dlg.Show();
            
        }
        private void printB02(object sender, EventArgs e)
        {//人寿    
            closesendform();
            PrintHyx.ChinaLife dlg = new ChinaLife();
            dlg.Show();
            
        }
        private void printB03(object sender, EventArgs e)
        {//都邦航意险   
            closesendform();
            PrintHyx.DuBang01 dlg = new DuBang01();
            dlg.Show();
            
        }
        private void printB04(object sender, EventArgs e)
        {//都邦航意险    
            closesendform();
            PrintHyx.DuBang02 dlg = new DuBang02();
            dlg.Show();
            
        }
        private void print001(object sender, EventArgs e)
        {//PICC       
            closesendform();
            PrintHyx.PrintPICC2 dlg = new PrintPICC2();
            dlg.Show();
            
        }
        private void print007(object sender, EventArgs e)
        {//永安
            closesendform();
            PrintHyx.Yongan dlg = new Yongan();
            dlg.Show();
            
        }
        private void print009(object sender, EventArgs e)
        {//新华
            closesendform();
            PrintHyx.NewChina dlg = new NewChina();
            dlg.Show();
            
        }
        private void printB05(object sender, EventArgs e)
        {//新华
            closesendform();
            PrintHyx.DuBang02 dlg = new DuBang02();
            dlg.Dubang03();
            dlg.Show();
            
        }
        private void printB06(object sender, EventArgs e)
        {//平安－周游列国
            closesendform();
            PrintHyx.PingAn01 dlg = new PingAn01();
            dlg.Show();
            
        }
        private void printB07(object sender, EventArgs e)
        {//平安－周游列国
            closesendform();
            PrintHyx.Sunshine dlg = new Sunshine();
            dlg.Show();
            
        }
        private void printB08(object sender, EventArgs e)
        {//平安－周游列国
            closesendform();
            PrintHyx.Hangyiwang dlg = new Hangyiwang();
            dlg.Show();
            
        }
        private void printB09(object sender, EventArgs e)
        {
            if(!GlobalVar.printProgram)closesendform();
            PrintHyx.bxLogin bx = new ePlus.PrintHyx.bxLogin();
            if (bx.ShowDialog() != DialogResult.OK) return;

            PrintHyx.EagleIns dlg = new EagleIns();
            dlg.Text = dlg.lb公司名称.Text = "新华人寿保险股份有限公司意外伤害保险承保告知单";
            dlg.Show();
            
        }
        private void printB0A(object sender, EventArgs e)
        {
            closesendform();
            PrintHyx.Pacific dlg = new ePlus.PrintHyx.Pacific();
            dlg.Show();
            
        }
        private void printB0B(object sender, EventArgs e)
        {
            closesendform();
            PrintHyx.EagleAnbang dlg = new ePlus.PrintHyx.EagleAnbang();
            dlg.Show();
            
        }
        private void printB0C(object sender, EventArgs e)
        {
            closesendform();
            PrintHyx.ChinaLife2 dlg = new ePlus.PrintHyx.ChinaLife2();
            dlg.Show();

        }
        private void admin(object sender, EventArgs e)
        {
            try
            {
                PrintTicket.RunProgram("C:\\Program Files\\Internet Explorer\\IEXPLORE.EXE", GlobalVar.WebUrl + "?user=" + GlobalVar.loginName + "&pwd=" + GlobalVar.loginPassword);
            }
            catch
            {
                try
                {
                    PrintTicket.RunProgram("D:\\Program Files\\Internet Explorer\\IEXPLORE.EXE", GlobalVar.WebUrl + "?user=" + GlobalVar.loginName + "&pwd=" + GlobalVar.loginPassword);
                }
                catch
                {
                }
            }
        }
    }
}
