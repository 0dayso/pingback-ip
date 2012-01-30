using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;


namespace ePlus
{
    public class ShortCutHandle
    {
        public static List<string> c_order = new List<string>();//在小苹果方式下存放订座指令
        public static List<string> c_list = new List<string>();//在小苹果方式下存放在内存中显示的数据

        public static int keyvalue = 0;//存放e.keyvalue
        public static bool b_arrow = false;//存放是否加箭头

        public static BookSimple.CardIDsB2C frmPassengerList;

        public static void NewShorCutHandle(System.Windows.Forms.RichTextBox editor)
        {
            if (editor != null)
            {
                editor.KeyDown += new System.Windows.Forms.KeyEventHandler(editor_KeyDown);
                editor.KeyDown += new System.Windows.Forms.KeyEventHandler(editor_KeyUp);
                editor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(editor_KeyPress);
                editor.MouseClick += new System.Windows.Forms.MouseEventHandler(editor_MouseClick);
                editor.MouseUp += new System.Windows.Forms.MouseEventHandler(editor_MouseUp);
                GlobalVar.stdRichTB = editor;
                

            }
           
        }

        static void editor_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                try
                {
                    System.Windows.Forms.RichTextBox textBox = sender as System.Windows.Forms.RichTextBox;
                    int cursorPos = textBox.SelectionStart;
                    int sanjiaoPos = textBox.Text.LastIndexOf(">",cursorPos) + 1;
                    string cmd = textBox.Text.Substring(sanjiaoPos, cursorPos - sanjiaoPos);
                    WindowInfo wndInfo = (WindowInfo)textBox.Tag;
                    wndInfo.SendData(cmd);
                }
                catch
                {
                }
            }
        }

        static void editor_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
        }

        //static Data.DataBaseProcess dataBaseProcess = new ePlus.Data.DataBaseProcess(Data.DataBaseProcess.GetConnectionString(AppDomain.CurrentDomain.BaseDirectory + "data\\data.mdb"));
        static Data.DataBaseProcess dataBaseProcess = new ePlus.Data.DataBaseProcess(Data.DataBaseProcess.GetConnectionString(AppDomain.CurrentDomain.BaseDirectory + "data.mdb"));
        //private Socket socket = null;
        static string gotstringtosend = "";

        static YzpBtoC.EgtermCmdEx egEx = new YzpBtoC.EgtermCmdEx(GlobalVar2.gbUserModel == 1 ? true : false);//chenqj
        static void editor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            {//先处理EG扩展指令
                
                System.Windows.Forms.RichTextBox textBox = sender as System.Windows.Forms.RichTextBox;
                if (e.KeyValue == 13 || e.KeyValue == 123)
                {
                    int pEnd = textBox.SelectionStart;
                    int pBeg = textBox.Text.LastIndexOf('>', pEnd-1);
                    if (pBeg < 0) pBeg = 0;
                    string txt = textBox.Text.Substring(pBeg + 1, pEnd - pBeg - 1);
                    egEx.cmdex = txt.Trim();

                    if (egEx.IsEgExCmd())
                    {
                        //egEx.asyncRun();
                        egEx.ExeCmds(null);
                        return;
                    }
                }
            }
            gotstringtosend = "";
            //是否抢占
            if (GlobalVar2.gbEplusStyle)
            {
                if (!GlobalVar.bUsingConfigLonely && e.KeyValue==13)
                {
                    try
                    {
                        if (Model.md.b_00D)
                        {
                            //if (GlobalVar.current_using_config.IndexOf('~') >= 0)
                            //    throw new Exception("独占配置失败，可能选择了多个配置：" + GlobalVar.current_using_config.Replace("~", "/"));
                            //GlobalVar.stdRichTB.AppendText("\r\n" + "开始尝试独占当前配置……" + "\r\n>");

                            EagleString.EagleFileIO.LogWrite("尝试抢占配置！");

                            EagleAPI.EagleSend_useconfiglonely_Static(int.Parse(GlobalVar.current_using_config.Split('~')[0]));
                            System.Threading.Thread.Sleep(500);
                        }
                    }
                    catch (Exception ex)
                    {
                        //GlobalVar.stdRichTB.AppendText("\r\n" + ex.Message + "\r\n>");
                    }
                }
            }
            //如果在独占，并且输入指定键时，增加为30秒
            if (GlobalVar.bUsingConfigLonely)
            {
                if (e.Alt)
                {
                    GlobalVar.dtLonelyStart = DateTime.Now;
                    EagleAPI.EagleSendOneCmd("help", 3);
                }
            }
            else
            {
                if (GlobalVar.current_using_config.IndexOf('~') >= 0)
                {
                    
                }
                else
                {
                    if (Model.md.b_AutoLockConfig)
                    {
                        //EagleAPI.EagleSend_useconfiglonely_Static(int.Parse(GlobalVar.current_using_config));
                    }
                }
            }
            if (GlobalVar.b_EtermType)
                editor_KeyDown_New(sender, e);
            else
                editor_KeyDown_Old(sender, e);
            localCommand(gotstringtosend);
        }
        public static string order_display()
        {
            string d_string = "";//要显示的串
            for (int i = 0; i < c_list.Count; i++)
            {
                //去掉前三个字符
                //c_list[i] = c_list[i].Substring(3);
                int j = i + 1;
                d_string = "\r"+d_string + " " + j.ToString() + ". " + c_list[i] +"\r";
            }
            while (d_string.IndexOf("\r\r") > -1) d_string = d_string.Replace("\r\r", "\r");
            return d_string;
        }

        public static void SetBoxCursor(System.Windows.Forms.RichTextBox rtb)
        {
            int a = rtb.Text.LastIndexOf('>')+1;
            int b = rtb.Text.LastIndexOf((char)0x13);
            rtb.SelectionStart = a;
            rtb.SelectionLength = 0;
        }

        static void editor_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            
            System.Windows.Forms.RichTextBox textBox = sender as System.Windows.Forms.RichTextBox;


            if (GlobalVar.qHistoryCmd.Count <= 0) return;
            if (e.KeyValue == 45)
            {
                
                //textBox.Cursor.
            }
            if (e.KeyValue == 37 && e.Control)//ctrl + ←
            {
                GlobalVar.iHistory++;
                if (GlobalVar.iHistory > GlobalVar.qHistoryCmd.Count) GlobalVar.iHistory = GlobalVar.qHistoryCmd.Count;


                GlobalVar.stdRichTB.Text = GlobalVar.stdRichTB.Text.Substring(0, GlobalVar.stdRichTB.Text.Length - GlobalVar.CurrentAddHistory.Length);

                GlobalVar.CurrentAddHistory = (GlobalVar.qHistoryCmd[GlobalVar.qHistoryCmd.Count - GlobalVar.iHistory]);
                GlobalVar.stdRichTB.AppendText(GlobalVar.CurrentAddHistory);
                GlobalVar.stdRichTB.SelectionStart = GlobalVar.stdRichTB.Text.Length * 2;

            }
            if (e.KeyValue == 39 && e.Control)//ctrl + →
            {
                GlobalVar.iHistory--;
                if (GlobalVar.iHistory < 1) GlobalVar.iHistory = 1;


                GlobalVar.stdRichTB.Text = GlobalVar.stdRichTB.Text.Substring(0, GlobalVar.stdRichTB.Text.Length - GlobalVar.CurrentAddHistory.Length);

                GlobalVar.CurrentAddHistory = (GlobalVar.qHistoryCmd[GlobalVar.qHistoryCmd.Count - GlobalVar.iHistory]);//length-1 -(iHistory -1)
                GlobalVar.stdRichTB.AppendText(">" + GlobalVar.CurrentAddHistory);
                GlobalVar.stdRichTB.SelectionStart = GlobalVar.stdRichTB.Text.Length;

            }
            
        }

        static void editor_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            System.Windows.Forms.RichTextBox textBox = sender as System.Windows.Forms.RichTextBox;
            if (e.KeyChar =='\r'||keyvalue  == 13 || keyvalue == 123)
            {
                if (egEx.bEgExCmd)
                {
                    textBox.AppendText(">\r\n" + egEx.retString + "\n>");
                    if (egEx.bFlag == false) textBox.AppendText(">\r\n" + egEx.errorString + "\n>");
                    if (egEx.retString.Split (' ')[0] == "TJDD")
                    {
                        BookSimple.SubmitPnr dlg = new ePlus.BookSimple.SubmitPnr();
                        dlg.ShowInTaskbar = false;
                        dlg.Show();
                        dlg.正常提交按钮(egEx.retString .Split (' ')[1],"95161");
                    }
                    else if (egEx.cmdex.Split(' ')[0] == "KLD")
                    {
                        if (frmPassengerList == null || frmPassengerList.IsDisposed)
                        {
                            frmPassengerList = new ePlus.BookSimple.CardIDsB2C();
                            frmPassengerList.TopMost = true;
                            frmPassengerList.Show();
                        }

                        frmPassengerList.LoadPassengers();
                        GlobalVar.stdRichTB.Focus();
                    }
                    return;
                }
            }
            if (GlobalVar.b_EtermType)

                editor_KeyPress_New(sender, e);
            else
                editor_KeyPress_Old(sender, e);
        }

        static void editor_KeyDown_Old(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            System.Windows.Forms.RichTextBox textBox = sender as System.Windows.Forms.RichTextBox;
            if (GlobalVar.etProcessing)
            {
                //textBox.AppendText("正在处理电子客票，稍等\r>"); return; 
            }
            keyvalue = e.KeyValue;
            b_arrow = false;
            string sqlStatement = "select ID,Func from ShortCutKey where Alt = {0} and Shift = {1} and Ctrl ={2} and ShortCutKey={3}";
            if (e.KeyValue == 13)
            {
                if (!EagleProtocol.b_passport)
                {
                    textBox.AppendText(GlobalVar.Notice + "\r未通过您的身份验证，请稍候。。。\r>");
                    return;
                }
                sqlStatement = String.Format(sqlStatement, e.Alt.ToString(), e.Shift.ToString(), e.Control.ToString(), "123");
            }
            else
                sqlStatement = String.Format(sqlStatement, e.Alt.ToString(), e.Shift.ToString(), e.Control.ToString(), e.KeyValue);


            if (e.KeyValue == 27)//esc键滚屏
            {
                //textBox.AppendText(">\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r");
                //textBox.SelectionStart = textBox.SelectionStart - 28;
                int pos = textBox.SelectionStart;
                textBox.Text = textBox.Text.Insert(textBox.SelectionStart, ">");
                textBox.SelectionStart = pos + 1;

            }
            int line = textBox.GetLineFromCharIndex(textBox.SelectionStart);
            ;
            WindowInfo wndInfo = (WindowInfo)textBox.Tag;

            System.Data.DataTable dt = dataBaseProcess.ExcuteQuery(sqlStatement);
            if (dt != null && dt.Rows.Count > 0)
            {
                #region 快键
                {
                    string quickCmdString = dt.Rows[0][1].ToString().Trim();
                    if (quickCmdString[0] == '[' && quickCmdString[quickCmdString.Length - 1] == ']')
                    {
                    }
                    else
                    {
                        textBox.AppendText(quickCmdString);
                        return;
                    }

                }
                #endregion
                string resultstring = "";
                switch ((int)dt.Rows[0][0])
                {

                    case 21://3..这里发送数据的计算是从光标位置开始，向前，一直找到"＞"为止
                        //取最后一个'>'位置
                        try
                        {
                            int MarkPos = -1;
                            try
                            {
                                for (int i = textBox.SelectionStart - 1; i >= 0; i--)
                                {
                                    if (textBox.Text[i] == '>')
                                    {
                                        MarkPos = i;
                                        break;
                                    }
                                }
                            }
                            catch
                            {
                                textBox.AppendText(GlobalVar.Notice + "\r错误号：ShortCutHandle-10000");
                            }
                            int cmdLength = 0;
                            if (MarkPos == -1)
                            {
                                cmdLength = textBox.Text.Length;
                            }
                            else
                            {
                                cmdLength = textBox.SelectionStart - (MarkPos + 1);
                            }
                            if (wndInfo != null && MarkPos >= -1)
                            {
                                string strClaw = textBox.Text.Substring(MarkPos == -1 ? 0 : MarkPos + 1, cmdLength);
                                strClaw = strClaw.Trim();
                                //保存历史指令
                                GlobalVar.qHistoryCmd.Add(strClaw);
                                GlobalVar.iHistory = 0;
                                GlobalVar.CurrentAddHistory = "";
                                try
                                {
                                    //if (EagleAPI.GetCmdName(strClaw) == null)//判断指令列表中是否有此指令。
                                    //{
                                    //    textBox.AppendText(GlobalVar.Notice + "\r错误指令");
                                    //    b_arrow = true;
                                    //    break;
                                    //}
                                    if (EagleAPI.GetCmdName(strClaw, GlobalVar.loginLC.VisuableCommand) == "" || EagleAPI.GetCmdName(strClaw, GlobalVar.loginLC.VisuableCommand) == null)
                                    {
                                        gotstringtosend = strClaw;
                                        if (strClaw.Trim() != "")
                                            textBox.AppendText(GlobalVar.Notice + "\r您无权使用该指令，请与管理员联系");
                                        b_arrow = true;
                                        break;
                                    }
                                }
                                catch
                                {
                                    textBox.AppendText(GlobalVar.Notice + "\r错误号：ShortCutHandle-10002");
                                }
                                //历史命令记录

                                if (strClaw.Length == 0) break;
                                //非订座指令，直接发送出去
                                try
                                {
                                    if (e.KeyValue == 123 || (e.KeyValue == 13 && GlobalVar.b_NumpadEnter))//F12  + 或者是小回车
                                    {
                                        if (frmMain.b_s2)//逐条显示方式
                                        {
                                            string temp = "";
                                            for (int i = 0; i < c_order.Count; i++)
                                            {
                                                temp += c_order[i] + ((char)0x0D).ToString();
                                            }
                                            temp += textBox.Lines[textBox.GetLineFromCharIndex(textBox.SelectionStart)].Trim();
                                            //发送如上结果
                                            while (temp[0] == '>')
                                            {
                                                temp = temp.Substring(1);
                                            }
                                            temp = temp.Trim();
                                            if (EagleAPI.substring(temp, 0, 2).ToLower() == "sd")
                                            {
                                                av_sd avsd = new av_sd();
                                                avsd.avstring = connect_4_Command.AV_String;
                                                temp = avsd.sd2ss(temp);
                                            }
                                            #region 发送点
                                            streamctrl.switch_office(temp, GlobalVar.current_using_config, GlobalVar.etdz_config);
                                            #endregion
                                            //wndInfo.SendData(temp);
                                            resultstring = temp;
                                            c_order.Clear();
                                            c_list.Clear();
                                            break;
                                        }
                                        if (EagleAPI.substring(strClaw, 0, 2).ToLower() == "sd")
                                        {
                                            av_sd avsd = new av_sd();
                                            avsd.avstring = connect_4_Command.AV_String;
                                            strClaw = avsd.sd2ss(strClaw);
                                        }
                                        #region 发送点
                                        streamctrl.switch_office(strClaw, GlobalVar.current_using_config, GlobalVar.etdz_config);
                                        #endregion
                                        //wndInfo.SendData(strClaw);
                                        resultstring = strClaw;
                                        break;
                                    }
                                }
                                catch
                                {
                                    textBox.AppendText(GlobalVar.Notice + "\r错误号：ShortCutHandle-10004");
                                }
                                //若当前行的指令为订座指令

                                int CurrentLine = textBox.GetLineFromCharIndex(textBox.SelectionStart);
                                if (CurrentLine >= textBox.Lines.Length)//输入有自动换行
                                {
                                    textBox.Clear();
                                    textBox.AppendText(GlobalVar.Notice + "\r有自动换行，请重新输入！或者->设置->选项->将字体调小\r>");
                                    break;
                                }
                                #region//如果是回车
                                if (e.KeyValue == 13 && !GlobalVar.b_NumpadEnter)//如果是回车 +大回车
                                {
                                    string CurrentLineText = textBox.Lines[CurrentLine].ToString().Trim();
                                    if (CurrentLineText.Length > 0)
                                    {
                                        int i_pos = CurrentLineText.LastIndexOf('>');
                                        CurrentLineText = CurrentLineText.Substring(i_pos + 1);    //取当前行的字串
                                    }
                                    try
                                    {
                                        if (CurrentLineText.Length <= 2 || CurrentLineText[0] == '@' || CurrentLineText[0] == '\\') //该行小于2个字节，一定是非订座
                                        {
                                            if ((frmMain.b_s2 || frmMain.b_s1) && CurrentLineText.Length > 0)
                                            {
                                                if (CurrentLineText[0] == '@' || CurrentLineText[0] == '\\')
                                                {
                                                    //把c_order的指令加起来中间用0x0d分开，然后加上CurrentLineText
                                                    string temp = "";
                                                    for (int i = 0; i < c_order.Count; i++)
                                                    {
                                                        temp += c_order[i] + ((char)0x0D).ToString();
                                                    }
                                                    temp += CurrentLineText;
                                                    //发送如上结果
                                                    #region 发送点

                                                    #endregion
                                                    if (GlobalVar.b_pat)
                                                    {
                                                        streamctrl.switch_office(strClaw, GlobalVar.current_using_config, GlobalVar.etdz_config);
                                                        //wndInfo.SendData(strClaw);
                                                        resultstring = strClaw;
                                                    }
                                                    else
                                                    {
                                                        streamctrl.switch_office(temp, GlobalVar.current_using_config, GlobalVar.etdz_config);
                                                        //wndInfo.SendData(temp);
                                                        resultstring = temp;
                                                    }

                                                    c_order.Clear();
                                                    c_list.Clear();
                                                    break;
                                                }
                                            }
                                            #region 发送点
                                            streamctrl.switch_office(strClaw, GlobalVar.current_using_config, GlobalVar.etdz_config);
                                            #endregion
                                            //wndInfo.SendData(strClaw);
                                            resultstring = strClaw;
                                            break;
                                        }
                                    }
                                    catch
                                    {
                                        textBox.AppendText(GlobalVar.Notice + "\r错误号：ShortCutHandle-10006");
                                    }
                                    #region
                                    //1.EPLUS方式
                                    //if (frmMain.b_s1)
                                    //{
                                    //    if (
                                    //        CurrentLineText.Substring(0, 2).ToLower() == "c:" ||
                                    //        //CurrentLineText.Substring(0, 2).ToLower() == "rT" ||
                                    //        //CurrentLineText.Substring(0, 2).ToLower() == "xe" ||
                                    //        CurrentLineText.Substring(0, 2).ToLower() == "nm" ||
                                    //        CurrentLineText.Substring(0, 2).ToLower() == "ss" ||
                                    //        CurrentLineText.Substring(0, 2).ToLower() == "sd" ||
                                    //        CurrentLineText.Substring(0, 2).ToLower() == "sa" ||
                                    //        CurrentLineText.Substring(0, 2).ToLower() == "sn" ||
                                    //        CurrentLineText.Substring(0, 2).ToLower() == "ct" ||
                                    //        CurrentLineText.Substring(0, 2).ToLower() == "tk" ||
                                    //        CurrentLineText.Substring(0, 2).ToLower() == "ma" ||
                                    //        CurrentLineText.Substring(0, 2).ToLower() == "op" ||
                                    //        CurrentLineText.Substring(0, 2).ToLower() == "ba" ||
                                    //        CurrentLineText.Substring(0, 2).ToLower() == "cs" ||
                                    //        CurrentLineText.Substring(0, 2).ToLower() == "es" ||
                                    //        CurrentLineText.Substring(0, 2).ToLower() == "sp" ||
                                    //        CurrentLineText.Substring(0, 2).ToLower() == "ni" ||
                                    //        CurrentLineText.Substring(0, 2).ToLower() == "fc" ||
                                    //        CurrentLineText.Substring(0, 2).ToLower() == "fn" ||
                                    //        CurrentLineText.Substring(0, 2).ToLower() == "fp" ||
                                    //        //CurrentLineText.Substring(0, 2).ToLower() == "hb" ||
                                    //        CurrentLineText.Substring(0, 3).ToLower() == "osi" ||
                                    //        CurrentLineText.Substring(0, 3).ToLower() == "rmk" ||
                                    //        CurrentLineText.Substring(0, 3).ToLower() == "ssr" //||
                                    //        //(CurrentLineText[0] > '0' && CurrentLineText[0] < '9')
                                    //        )
                                    //    {
                                    //        break;
                                    //    }
                                    //    else
                                    //    {
                                    //        wndInfo.SendData(strClaw);
                                    //        break;
                                    //    }
                                    //}
                                    #endregion
                                    //2.小苹果方式-begin
                                    if (frmMain.b_s2 || frmMain.b_s1)
                                    {

                                        string[] av_string = connect_4_Command.AV_String.Split('\r');
                                        if (
                                            CurrentLineText.Substring(0, 2).ToLower() == "c:" ||
                                            //CurrentLineText.Substring(0, 2).ToLower() == "ei" ||
                                            //CurrentLineText.Substring(0, 2).ToLower() == "rT" ||
                                            //CurrentLineText.Substring(0, 2).ToLower() == "xe" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "nm" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "xn" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "gn" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "ss" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "sd" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "sa" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "sn" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "ct" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "tk" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "ma" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "op" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "ba" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "cs" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "es" ||
                                            //CurrentLineText.Substring(0, 2).ToLower() == "sp" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "ni" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "fc" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "fn" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "fp" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "hb" ||
                                            //CurrentLineText.Substring(0, 3).ToLower() == "osi" ||
                                            CurrentLineText.Substring(0, 3).ToLower() == "rmk" ||
                                            CurrentLineText.Substring(0, 3).ToLower() == "ssr" //||
                                            //(CurrentLineText[0] > '0' && CurrentLineText[0] < '9')
                                            )
                                        {
                                            b_arrow = true;
                                            #region sd
                                            if (CurrentLineText.Substring(0, 2).ToLower() == "sd")
                                            {
                                                if (CurrentLineText[2] > '0' && CurrentLineText[2] < '9')
                                                {

                                                    string temp = "";
                                                    //temp =  "   ";
                                                    string av_temp = "";
                                                    try
                                                    {
                                                        if (av_string.Length >= int.Parse(CurrentLineText[2].ToString()))
                                                        {
                                                            for (int i = 0; i < av_string.Length; i++)
                                                            {
                                                                if (av_string[i].Substring(0, 2) == "\n" + CurrentLineText[2].ToString())
                                                                {
                                                                    av_temp = av_string[i];
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            textBox.AppendText(GlobalVar.Notice + "\r请先执行查询指令或超出标号！\r>");
                                                            break;
                                                        }
                                                    }
                                                    catch
                                                    {
                                                        textBox.AppendText(GlobalVar.Notice + "\r错误号：ShortCutHandle-10008");
                                                    }
                                                    if (av_temp.Length < 12)
                                                    {
                                                        textBox.AppendText(GlobalVar.Notice + "\r请先执行AV查询指令！\r>");
                                                        break;
                                                    }
                                                    try
                                                    {
                                                        temp = temp + av_temp.Substring(4, 8);
                                                        temp += CurrentLineText[3].ToString().ToUpper() + "  ";
                                                        temp += av_string[0].Substring(0, 6) + "  ";
                                                        temp += av_string[0].Substring(12, 6) + " ";//wuhsha
                                                    }
                                                    catch
                                                    {
                                                        textBox.AppendText(GlobalVar.Notice + "\r错误号：ShortCutHandle-10010");
                                                    }
                                                    string number = "";
                                                    try
                                                    {
                                                        if (CurrentLineText.Length > 4)
                                                        {
                                                            if (CurrentLineText[CurrentLineText.Length - 1] >= '0' && CurrentLineText[CurrentLineText.Length - 1] <= '9')
                                                            {
                                                                number = CurrentLineText.Substring(CurrentLineText.Length - 1);
                                                                if (CurrentLineText[CurrentLineText.Length - 2] >= '0' && CurrentLineText[CurrentLineText.Length - 2] <= '9')//大于10张票
                                                                {
                                                                    number = CurrentLineText.Substring(CurrentLineText.Length - 2);
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            textBox.AppendText(GlobalVar.Notice + "\r输入错误\r>");
                                                            break;
                                                        }
                                                    }
                                                    catch
                                                    {
                                                        textBox.AppendText(GlobalVar.Notice + "\r错误号：ShortCutHandle-10012");
                                                    }
                                                    try
                                                    {
                                                        temp += number + "   ";//张数
                                                        try
                                                        {
                                                            temp += av_temp.Substring(48, 32);//起飞时间后的串
                                                        }
                                                        catch { }
                                                        c_list.Add(temp);
                                                        //c_order.Add(CurrentLineText);//把当前行加入
                                                        //转换sd2ss
                                                        av_sd avsd = new av_sd();
                                                        avsd.avstring = connect_4_Command.AV_String;
                                                        c_order.Add(avsd.sd2ss(CurrentLineText));
                                                        if (frmMain.b_s2) textBox.AppendText(order_display());//1
                                                    }
                                                    catch
                                                    {
                                                        textBox.AppendText(GlobalVar.Notice + "\r错误号：ShortCutHandle-10014");
                                                    }

                                                    //SetBoxCursor(textBox);
                                                    break;
                                                }
                                                else
                                                {
                                                    textBox.AppendText(GlobalVar.Notice + "输入有误，请仔细核对\n");
                                                    break;
                                                }
                                            }
                                            #endregion
                                            try
                                            {
                                                if (CurrentLineText.Substring(0, 2).ToLower() == "nm")
                                                {
                                                    string[] nm_array = CurrentLineText.Split('1');
                                                    for (int i = nm_array.Length - 1; i > 0; i--)
                                                    {
                                                        c_list.Insert(0, nm_array[i]);

                                                    }
                                                    egEx.cmdex = CurrentLineText;
                                                    egEx.NM();
                                                    if (egEx.bFlag)
                                                    {
                                                        string[] tarr = egEx.retString.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                                                        for (int i = 0; i < tarr.Length; i++)
                                                        {
                                                            c_order.Add(tarr[i]);
                                                        }
                                                        textBox.AppendText("\r\n" + egEx.retString);
                                                    }
                                                    else
                                                    {
                                                        textBox.AppendText(egEx.errorString);
                                                        c_order.Add(CurrentLineText);
                                                    }
                                                    if (frmMain.b_s2) textBox.AppendText(order_display());//2
                                                    break;
                                                }
                                            }
                                            catch
                                            {
                                                textBox.AppendText(GlobalVar.Notice + "\r错误号：ShortCutHandle-10016");
                                            }
                                            try
                                            {
                                                if (CurrentLineText.Substring(0, 2).ToLower() == "ct"
                                                    || CurrentLineText.Substring(0, 2).ToLower() == "c:"
                                                    || CurrentLineText.Substring(0, 2).ToLower() == "tk"
                                                    || CurrentLineText.Substring(0, 2).ToLower() == "gn"
                                                    )
                                                {
                                                    c_list.Add(CurrentLineText.Substring(2));
                                                    c_order.Add(CurrentLineText);
                                                    if (frmMain.b_s2) textBox.AppendText(order_display());//3
                                                    break;
                                                }
                                                if (CurrentLineText == "rt")
                                                {
                                                    //显示c_list
                                                    break;
                                                }

                                                c_list.Add(CurrentLineText);
                                                c_order.Add(CurrentLineText);
                                                if (frmMain.b_s2) textBox.AppendText(order_display());
                                            }
                                            catch
                                            {
                                                textBox.AppendText(GlobalVar.Notice + "\r错误号：ShortCutHandle-10018");
                                            }
                                            break;

                                        }
                                        else
                                        {
                                            #region 发送点
                                            streamctrl.switch_office(strClaw, GlobalVar.current_using_config, GlobalVar.etdz_config);
                                            #endregion
                                            resultstring = strClaw;//wndInfo.SendData(strClaw);
                                            c_list.Clear();
                                            c_order.Clear();
                                            break;
                                        }
                                    }//2.小苹果方式-end

                                    //3.直接发送方式
                                    if (frmMain.b_s3)
                                    {
                                        if (EagleAPI.substring(strClaw, 0, 2).ToLower() == "sd")
                                        {
                                            av_sd avsd = new av_sd();
                                            avsd.avstring = connect_4_Command.AV_String;
                                            strClaw = avsd.sd2ss(strClaw);
                                        }
                                        #region 发送点
                                        streamctrl.switch_office(strClaw, GlobalVar.current_using_config, GlobalVar.etdz_config);
                                        #endregion
                                        resultstring = strClaw;//wndInfo.SendData(strClaw);
                                        break;
                                    }
                                }
                                #endregion
                            }
                            break;
                        }
                        catch
                        {
                            textBox.AppendText(GlobalVar.Notice + "\r操作错误，请记录错误操作过程，与软件开发商联系！\r>");
                            textBox.AppendText(GlobalVar.Notice + "正在尝试重新连接。。。\r>");

                            b_arrow = true;
                            break;
                        }
                    case 22:
                        break;
                }
                if (resultstring.IndexOf('~') > -1)
                {
                    textBox.AppendText(GlobalVar.Notice + "您无权使用该指令\r>");
                    return;
                }
                ;
                if ((EagleAPI.GetCmdName("rr", GlobalVar.loginLC.VisuableCommand) == ""))//无权使用rr
                {
                    string[] lins = resultstring.Split((char)0x0d);
                    for (int i = 0; i < lins.Length; i++)
                    {
                        if (lins[i].ToLower().IndexOf("sd") == 0 || lins[i].ToLower().IndexOf("ss") == 0)
                        {
                            if (lins[i].ToLower().IndexOf("/rr") > 0)
                            {
                                textBox.AppendText(GlobalVar.Notice + "您无权直接使用RR状态指令\r>");
                                return;
                            }
                        }

                    }
                }
                wndInfo.SendData(resultstring);
                if(resultstring !="")
                    gotstringtosend = resultstring;
            }
        }

        static void editor_KeyDown_New(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            System.Windows.Forms.RichTextBox textBox = sender as System.Windows.Forms.RichTextBox;
            keyvalue = e.KeyValue;
            b_arrow = false;
            //string sqlStatement = "select ID,Func from ShortCutKey where Alt = {0} and Shift = {1} and Ctrl ={2} and ShortCutKey={3}";
            //if (e.KeyValue == 13)//回车
            //{
            //    if (!EagleProtocol.b_passport)
            //    {
            //        textBox.AppendText(GlobalVar.Notice + "\r未通过您的身份验证，请稍候。。。\r>");
            //        return;
            //    }
            //    sqlStatement = String.Format(sqlStatement, e.Alt.ToString(), e.Shift.ToString(), e.Control.ToString(), "123");
            //}
            //else
            //    sqlStatement = String.Format(sqlStatement, e.Alt.ToString(), e.Shift.ToString(), e.Control.ToString(), e.KeyValue);


            if (e.KeyValue == 27)//esc放置">"
            {
                //textBox.AppendText(">\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r");
                //textBox.SelectionStart = textBox.SelectionStart - 28;
                int pos = textBox.SelectionStart;
                textBox.Text = textBox.Text.Insert(textBox.SelectionStart, ">");
                textBox.SelectionStart = pos + 1;

            }
            
            int line = textBox.GetLineFromCharIndex(textBox.SelectionStart);
            WindowInfo wndInfo = (WindowInfo)textBox.Tag;
            if (ShortKeysHandle(wndInfo,e)) return;
            //System.Data.DataTable dt = dataBaseProcess.ExcuteQuery(sqlStatement);
            //if (dt != null && dt.Rows.Count > 0)
            {
                #region 快键
                {
                    //string quickCmdString = dt.Rows[0][1].ToString().Trim();
                    //if (quickCmdString[0] == '[' && quickCmdString[quickCmdString.Length - 1] == ']')
                    //{
                    //}
                    //else
                    //{
                    //    textBox.AppendText(quickCmdString);
                    //    return;
                    //}
                }
                #endregion
                string resultstring = "";
                //switch ((int)dt.Rows[0][0])
                switch(e.KeyValue)
                {

                    case 13://3..这里发送数据的计算是从光标位置开始，向前，一直找到"＞"为止
                        //取最后一个'>'位置
                        try
                        {
                            int MarkPos = -1;
                            try
                            {
                                for (int i = textBox.SelectionStart - 1; i >= 0; i--)
                                {
                                    if (textBox.Text[i] == '>')
                                    {
                                        MarkPos = i;
                                        break;
                                    }
                                }
                            }
                            catch
                            {
                                textBox.AppendText(GlobalVar.Notice + "\r错误号：ShortCutHandle-10000");
                            }
                            int cmdLength = 0;
                            if (MarkPos == -1)
                            {
                                cmdLength = textBox.Text.Length;
                            }
                            else
                            {
                                cmdLength = textBox.SelectionStart - (MarkPos + 1);
                            }
                            if (wndInfo != null && MarkPos >= -1)
                            {
                                string strClaw = textBox.Text.Substring(MarkPos + 1, cmdLength);
                                strClaw = strClaw.Trim();
                                //保存历史指令
                                GlobalVar.qHistoryCmd.Add(strClaw);
                                GlobalVar.iHistory = 0;
                                GlobalVar.CurrentAddHistory = "";
                                try
                                {
                                    //if (EagleAPI.GetCmdName(strClaw) == null)//判断指令列表中是否有此指令。
                                    //{
                                    //    textBox.AppendText(GlobalVar.Notice + "\r错误指令");
                                    //    b_arrow = true;
                                    //    break;
                                    //}
                                    if (EagleAPI.GetCmdName(strClaw, GlobalVar.loginLC.VisuableCommand) == "" || EagleAPI.GetCmdName(strClaw, GlobalVar.loginLC.VisuableCommand) == null)
                                    {
                                        gotstringtosend = strClaw;
                                        textBox.AppendText(GlobalVar.Notice + "\r您无权使用该指令，请与管理员联系");
                                        b_arrow = true;
                                        break;
                                    }
                                }
                                catch
                                {
                                    textBox.AppendText(GlobalVar.Notice + "\r错误号：ShortCutHandle-10002");
                                }
                                //历史命令记录

                                if (strClaw.Length == 0) break;
                                //非订座指令，直接发送出去
                                try
                                {
                                    if (e.KeyValue == 123 || (e.KeyValue == 13 && GlobalVar.b_NumpadEnter))//F12  + 或者是小回车
                                    {
                                        if (frmMain.b_s2)//逐条显示方式
                                        {
                                            string temp = "";
                                            for (int i = 0; i < c_order.Count; i++)
                                            {
                                                temp += c_order[i] + ((char)0x0D).ToString();
                                            }
                                            temp += textBox.Lines[textBox.GetLineFromCharIndex(textBox.SelectionStart)].Trim();
                                            //发送如上结果
                                            while (temp[0] == '>')
                                            {
                                                temp = temp.Substring(1);
                                            }
                                            temp = temp.Trim();
                                            if (EagleAPI.substring(temp, 0, 2).ToLower() == "sd")
                                            {
                                                av_sd avsd = new av_sd();
                                                avsd.avstring = connect_4_Command.AV_String;
                                                temp = avsd.sd2ss(temp);
                                            }
                                            #region 发送点
                                            streamctrl.switch_office(temp, GlobalVar.current_using_config, GlobalVar.etdz_config);
                                            #endregion
                                            //wndInfo.SendData(temp);
                                            resultstring = temp;
                                            c_order.Clear();
                                            c_list.Clear();
                                            break;
                                        }
                                        //sd转换为ss
                                        if (EagleAPI.substring(strClaw, 0, 2).ToLower() == "sd")
                                        {
                                            av_sd avsd = new av_sd();
                                            avsd.avstring = connect_4_Command.AV_String;
                                            strClaw = avsd.sd2ss(strClaw);
                                        }
                                        #region 发送点
                                        streamctrl.switch_office(strClaw, GlobalVar.current_using_config, GlobalVar.etdz_config);//无用
                                        #endregion
                                        //wndInfo.SendData(strClaw);
                                        resultstring = strClaw;
                                        break;
                                    }
                                }
                                catch
                                {
                                    textBox.AppendText(GlobalVar.Notice + "\r错误号：ShortCutHandle-10004");
                                }
                                //若当前行的指令为订座指令

                                int CurrentLine = textBox.GetLineFromCharIndex(textBox.SelectionStart);
                                if (CurrentLine >= textBox.Lines.Length)//输入有自动换行
                                {
                                    textBox.Clear();
                                    textBox.AppendText(GlobalVar.Notice + "\r有自动换行，请重新输入！\r>");
                                    break;
                                }
                                #region//如果是回车
                                if (e.KeyValue == 13 && !GlobalVar.b_NumpadEnter)//如果是回车 +大回车
                                {
                                    string CurrentLineText = textBox.Lines[CurrentLine].ToString().Trim();
                                    if (CurrentLineText.Length > 0)
                                    {
                                        int i_pos = CurrentLineText.LastIndexOf('>');
                                        CurrentLineText = CurrentLineText.Substring(i_pos + 1);    //取当前行的字串
                                    }
                                    try
                                    {
                                        if (CurrentLineText.Length <= 2 || CurrentLineText[0] == '@' || CurrentLineText[0] == '\\') //该行小于2个字节，一定是非订座
                                        {
                                            if ((frmMain.b_s2 || frmMain.b_s1) && CurrentLineText.Length > 0)
                                            {
                                                if (CurrentLineText[0] == '@' || CurrentLineText[0] == '\\')
                                                {
                                                    //把c_order的指令加起来中间用0x0d分开，然后加上CurrentLineText
                                                    string temp = "";
                                                    for (int i = 0; i < c_order.Count; i++)
                                                    {
                                                        temp += c_order[i] + ((char)0x0D).ToString();
                                                    }
                                                    temp += CurrentLineText;
                                                    //发送如上结果
                                                    #region 发送点

                                                    #endregion
                                                    if (GlobalVar.b_pat)
                                                    {
                                                        streamctrl.switch_office(strClaw, GlobalVar.current_using_config, GlobalVar.etdz_config);
                                                        //wndInfo.SendData(strClaw);
                                                        resultstring = strClaw;
                                                    }
                                                    else
                                                    {
                                                        streamctrl.switch_office(temp, GlobalVar.current_using_config, GlobalVar.etdz_config);
                                                        //wndInfo.SendData(temp);
                                                        resultstring = temp;
                                                    }
                                                    c_order.Clear();
                                                    c_list.Clear();
                                                    break;
                                                }
                                            }
                                            #region 发送点
                                            streamctrl.switch_office(strClaw, GlobalVar.current_using_config, GlobalVar.etdz_config);
                                            #endregion
                                            //wndInfo.SendData(strClaw);
                                            resultstring = strClaw;
                                            break;
                                        }
                                    }
                                    catch
                                    {
                                        textBox.AppendText(GlobalVar.Notice + "\r错误号：ShortCutHandle-10006");
                                    }
                                    #region

                                    #endregion
                                    //2.小苹果方式-begin(回车中)
                                    if (frmMain.b_s2 || frmMain.b_s1)
                                    {
                                        string[] av_string = connect_4_Command.AV_String.Split('\r');
                                        if (
                                            CurrentLineText.Substring(0, 2).ToLower() == "c:" ||
                                            //CurrentLineText.Substring(0, 2).ToLower() == "ei" ||
                                            //CurrentLineText.Substring(0, 2).ToLower() == "rT" ||
                                            //CurrentLineText.Substring(0, 2).ToLower() == "xe" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "nm" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "xn" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "gn" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "ss" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "sd" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "sa" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "sn" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "ct" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "tk" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "ma" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "op" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "ba" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "cs" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "es" ||
                                            //CurrentLineText.Substring(0, 2).ToLower() == "sp" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "ni" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "fc" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "fn" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "fp" ||
                                            CurrentLineText.Substring(0, 2).ToLower() == "hb" ||
                                            //CurrentLineText.Substring(0, 3).ToLower() == "osi" ||
                                            CurrentLineText.Substring(0, 3).ToLower() == "rmk" ||
                                            CurrentLineText.Substring(0, 3).ToLower() == "ssr" //||
                                            //(CurrentLineText[0] > '0' && CurrentLineText[0] < '9')
                                            )
                                        {
                                            b_arrow = true;
                                            #region sd
                                            if (CurrentLineText.Substring(0, 2).ToLower() == "sd")
                                            {
                                                if (CurrentLineText[2] > '0' && CurrentLineText[2] < '9')
                                                {

                                                    string temp = "";
                                                    //temp =  "   ";
                                                    string av_temp = "";
                                                    try
                                                    {
                                                        if (av_string.Length >= int.Parse(CurrentLineText[2].ToString()))
                                                        {
                                                            for (int i = 0; i < av_string.Length; i++)
                                                            {
                                                                if (av_string[i].Substring(0, 2) == "\n" + CurrentLineText[2].ToString())
                                                                {
                                                                    av_temp = av_string[i];
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            textBox.AppendText(GlobalVar.Notice + "\r请先执行查询指令或超出标号！\r>");
                                                            break;
                                                        }
                                                    }
                                                    catch
                                                    {
                                                        textBox.AppendText(GlobalVar.Notice + "\r错误号：ShortCutHandle-10008");
                                                    }
                                                    if (av_temp.Length < 12)
                                                    {
                                                        textBox.AppendText(GlobalVar.Notice + "\r请先执行AV查询指令！\r>");
                                                        break;
                                                    }
                                                    try
                                                    {
                                                        temp = temp + av_temp.Substring(4, 8);
                                                        temp += CurrentLineText[3].ToString().ToUpper() + "  ";
                                                        temp += av_string[0].Substring(0, 6) + "  ";
                                                        temp += av_string[0].Substring(12, 6) + " ";//wuhsha
                                                    }
                                                    catch
                                                    {
                                                        textBox.AppendText(GlobalVar.Notice + "\r错误号：ShortCutHandle-10010");
                                                    }
                                                    string number = "";
                                                    try
                                                    {
                                                        if (CurrentLineText.Length > 4)
                                                        {
                                                            if (CurrentLineText[CurrentLineText.Length - 1] >= '0' && CurrentLineText[CurrentLineText.Length - 1] <= '9')
                                                            {
                                                                number = CurrentLineText.Substring(CurrentLineText.Length - 1);
                                                                if (CurrentLineText[CurrentLineText.Length - 2] >= '0' && CurrentLineText[CurrentLineText.Length - 2] <= '9')//大于10张票
                                                                {
                                                                    number = CurrentLineText.Substring(CurrentLineText.Length - 2);
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            textBox.AppendText(GlobalVar.Notice + "\r输入错误\r>");
                                                            break;
                                                        }
                                                    }
                                                    catch
                                                    {
                                                        textBox.AppendText(GlobalVar.Notice + "\r错误号：ShortCutHandle-10012");
                                                    }
                                                    try
                                                    {
                                                        temp += number + "   ";//张数
                                                        try
                                                        {
                                                            temp += av_temp.Substring(48, 32);//起飞时间后的串
                                                        }
                                                        catch { }
                                                        c_list.Add(temp);
                                                        //c_order.Add(CurrentLineText);//把当前行加入
                                                        //转换sd2ss
                                                        av_sd avsd = new av_sd();
                                                        avsd.avstring = connect_4_Command.AV_String;
                                                        c_order.Add(avsd.sd2ss(CurrentLineText));
                                                        if (frmMain.b_s2) textBox.AppendText(order_display());//1
                                                    }
                                                    catch
                                                    {
                                                        textBox.AppendText(GlobalVar.Notice + "\r错误号：ShortCutHandle-10014");
                                                    }

                                                    //SetBoxCursor(textBox);
                                                    break;
                                                }
                                                else
                                                {
                                                    textBox.AppendText(GlobalVar.Notice + "输入有误，请仔细核对\n");
                                                    break;
                                                }
                                            }
                                            #endregion
                                            try
                                            {
                                                if (CurrentLineText.Substring(0, 2).ToLower() == "nm")
                                                {
                                                    string[] nm_array = CurrentLineText.Split('1');
                                                    for (int i = nm_array.Length - 1; i > 0; i--)
                                                    {
                                                        c_list.Insert(0, nm_array[i]);

                                                    }
                                                    c_order.Add(CurrentLineText);
                                                    if (frmMain.b_s2) textBox.AppendText(order_display());//2
                                                    break;
                                                }
                                            }
                                            catch
                                            {
                                                textBox.AppendText(GlobalVar.Notice + "\r错误号：ShortCutHandle-10016");
                                            }
                                            try
                                            {
                                                if (CurrentLineText.Substring(0, 2).ToLower() == "ct"
                                                    || CurrentLineText.Substring(0, 2).ToLower() == "c:"
                                                    || CurrentLineText.Substring(0, 2).ToLower() == "tk"
                                                    || CurrentLineText.Substring(0, 2).ToLower() == "gn"
                                                    )
                                                {
                                                    c_list.Add(CurrentLineText.Substring(2));
                                                    c_order.Add(CurrentLineText);
                                                    if (frmMain.b_s2) textBox.AppendText(order_display());//3
                                                    break;
                                                }
                                                if (CurrentLineText == "rt")
                                                {
                                                    //显示c_list
                                                    break;
                                                }

                                                c_list.Add(CurrentLineText);
                                                c_order.Add(CurrentLineText);
                                                if (frmMain.b_s2) textBox.AppendText(order_display());
                                            }
                                            catch
                                            {
                                                textBox.AppendText(GlobalVar.Notice + "\r错误号：ShortCutHandle-10018");
                                            }
                                            break;

                                        }
                                        else
                                        {
                                            #region 发送点
                                            streamctrl.switch_office(strClaw, GlobalVar.current_using_config, GlobalVar.etdz_config);
                                            #endregion
                                            resultstring = strClaw;//wndInfo.SendData(strClaw);
                                            c_list.Clear();
                                            c_order.Clear();
                                            break;
                                        }
                                    }//2.小苹果方式-end

                                    //3.直接发送方式
                                    if (frmMain.b_s3)
                                    {
                                        if (EagleAPI.substring(strClaw, 0, 2).ToLower() == "sd")
                                        {
                                            av_sd avsd = new av_sd();
                                            avsd.avstring = connect_4_Command.AV_String;
                                            strClaw = avsd.sd2ss(strClaw);
                                        }
                                        #region 发送点
                                        streamctrl.switch_office(strClaw, GlobalVar.current_using_config, GlobalVar.etdz_config);
                                        #endregion
                                        resultstring = strClaw;//wndInfo.SendData(strClaw);
                                        break;
                                    }
                                }
                                #endregion
                            }
                            break;
                        }
                        catch
                        {
                            textBox.AppendText(GlobalVar.Notice + "\r操作错误，请记录错误操作过程，与软件开发商联系！\r>");
                            textBox.AppendText(GlobalVar.Notice + "正在尝试重新连接。。。\r>");

                            b_arrow = true;
                            break;
                        }
                }
                //检测指令是否分隔符
                if (resultstring.IndexOf('~') > -1)
                {
                    textBox.AppendText(GlobalVar.Notice + "您无权使用该指令\r>");
                    return;
                }
                //控制rr
                if ((EagleAPI.GetCmdName("rr", GlobalVar.loginLC.VisuableCommand) == ""))//无权使用rr
                {
                    string[] lins = resultstring.Split((char)0x0d);
                    for (int i = 0; i < lins.Length; i++)
                    {
                        if (lins[i].ToLower().IndexOf("sd") == 0 || lins[i].ToLower().IndexOf("ss") == 0)
                        {
                            if (lins[i].ToLower().IndexOf("/rr") > 0)
                            {
                                textBox.AppendText(GlobalVar.Notice + "您无权直接使用RR状态指令\r>");
                                return;
                            }
                        }

                    }
                }

                wndInfo.SendData(resultstring);
                if (resultstring != "")
                gotstringtosend = resultstring;
                
            }
        }

        static void editor_KeyPress_Old(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

            System.Windows.Forms.RichTextBox textBox = sender as System.Windows.Forms.RichTextBox;
            if ((keyvalue == 13 || keyvalue == 123) && Math.Abs(textBox.SelectionStart - textBox.Text.Length) > 20)
            {
                textBox.AppendText(log.strSend);
            }

            if (keyvalue == 13 && b_arrow)
            {

                textBox.AppendText(">");
            }
            if (GlobalVar.bSendTypeFastFunction && keyvalue == 13)//多一个回车符，去之
            {
                textBox.SelectionStart = textBox.SelectionStart - 1;
            }

            switch (GlobalVar.commandName)
            {
                case GlobalVar.CommandName.CP:
                    textBox.AppendText(">\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r");
                    textBox.SelectionStart = textBox.SelectionStart - 28;
                    GlobalVar.commandName = GlobalVar.CommandName.NONE;
                    break;
                default:
                    break;
            }
        }
        static void editor_KeyPress_New(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

            System.Windows.Forms.RichTextBox textBox = sender as System.Windows.Forms.RichTextBox;
            if ((keyvalue == 13 || keyvalue == 123) && Math.Abs(textBox.SelectionStart - textBox.Text.Length) > 20)
            {
                textBox.AppendText(log.strSend);
            }

            if (keyvalue == 13 && b_arrow)
            {

                //textBox.AppendText(">");
            }
            if (GlobalVar.bSendTypeFastFunction && keyvalue == 13)//多一个回车符，去之
            {
                textBox.SelectionStart = textBox.SelectionStart - 1;
            }

            switch (GlobalVar.commandName)
            {
                case GlobalVar.CommandName.CP:
                    textBox.AppendText(">\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r");
                    textBox.SelectionStart = textBox.SelectionStart - 28;
                    GlobalVar.commandName = GlobalVar.CommandName.NONE;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 处理快键:有系统快键时,返回true,有对应发送指令时,直接发送;否则返回false,并且上级函数继续执行;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>

        static bool ShortKeysHandle(WindowInfo wInfo, System.Windows.Forms.KeyEventArgs e)
        {
            
            switch (keyvalue)
            {
                case 116:
                    wInfo.SendData("pn");
                    break;
                case 117:
                    wInfo.SendData("pb");
                    break;
                case 'a':
                    if (e.Control) wInfo.SendData("cp");
                    return false;
                case 'A':
                    if (e.Control) wInfo.SendData("cp");
                    return false;
                default:
                    return false;
            }
            return true;
        }
        static private bool localCommand(string cmdstring)
        {
            switch (cmdstring.ToLower().Trim())
            {
                case  "tjpnr":
                    BookSimple.SubmitPnr sp = new ePlus.BookSimple.SubmitPnr(EagleAPI.etstatic.Pnr,"");
                    sp.Show();
                    return true;
                    break;
            }
            return false;
        }
    }
}
